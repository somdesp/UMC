using PFC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class EmailBLL
    {
        #region Email
        public async Task EnviarEmail(dynamic usuarios,dynamic topico,string mensagem)
        {
            List<string> email = new List<string>();
            List<string> nome = new List<string>();
            string notificacao;

            if (topico == null)
            {
                for (int i = 0; i < usuarios.Count; i++)
                {
                    email.Add(usuarios[i].Email);
                    nome.Add(usuarios[i].Nome);

                }
                notificacao = mensagem;
            }
            else
            {
                for (int i = 0; i < topico.Resposta.Count; i++)
                {
                    email.Add(topico.Resposta[i].usuario.Email);
                    nome.Add(topico.Resposta[i].usuario.Nome);

                }
                notificacao = "O Topico http://www.mehelpehml.tk/Topico/TopicoSelecionado?topicoId=" + topico.Id + " Foi Fechado";
            }

            try
            {

                string emailSender = ConfigurationManager.AppSettings["username"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                string arqEmail = ConfigurationManager.AppSettings["FilePath"].ToString();

                string FilePath =  System.Web.Hosting.HostingEnvironment.MapPath("/")+ arqEmail;
                StreamReader str = new StreamReader(FilePath);
                string MailText_St = str.ReadToEnd();
                str.Close();



                email = email.Distinct().ToList();

                for (int i = 0; i < email.Count; i++)
                {
                    string MailText = MailText_St;
                    MailText = MailText.Replace("[newusername]", nome[i]);
                    MailText = MailText.Replace("[notificacao]", notificacao);


                    string subject = "MeHelp";

                    MailMessage _mailmsg = new MailMessage();
                    _mailmsg.IsBodyHtml = true;
                    _mailmsg.From = new MailAddress(emailSender);
                    _mailmsg.To.Add(email[i]);
                    _mailmsg.Subject = subject;
                    _mailmsg.Body = MailText;
                    SmtpClient _smtp = new SmtpClient();
                    _smtp.Host = emailSenderHost;
                    _smtp.Port = emailSenderPort;
                    _smtp.EnableSsl = true;
                    NetworkCredential _network = new NetworkCredential(emailSender, emailSenderPassword);
                    _smtp.Credentials = _network;

                    //Send Method will send your MailMessage create above.  
                    await _smtp.SendMailAsync(_mailmsg);

                    //using (SmtpClient client = new SmtpClient("smtpout.secureserver.net", 80)
                    //{
                    //    Credentials = new NetworkCredential("suporte@mehelpehml.tk", "Ema#@2018"),
                    //    EnableSsl = false
                    //})

                    //{
                    //    var message = new MailMessage("suporte@mehelpehml.tk", email[i], "subject", "body");

                    //    await client.SendMailAsync(email[i], email[i], "Forum UMC", "Existe novas Notificaçoes");
                    //}

                }
            }
            catch (Exception ex)
            {
                throw;
            }



        }
        #endregion

    }
}
