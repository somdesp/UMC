using PFC.DAO;
using PFC.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class DenunciaBLL
    {
        DenunciaDAO denunciaDAO = new DenunciaDAO();
        EmailBLL emailBLL = new EmailBLL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        public async Task<bool> DenunciaUsuario(Denuncia denuncia)
        {

            List<Usuario> usuarios = new List<Usuario>();
            usuarios= await usuarioBLL.ConsultaUsuarioMasterADM();
            await emailBLL.EnviarEmail(usuarios,null,"Existe novas notificaçoes");
            return await denunciaDAO.DenunciaUsuarioAsync(denuncia);
        }

        public async Task<bool> RemoverResposta(Denuncia denuncia)
        {

            List<Usuario> usuarios = new List<Usuario>();
            denuncia.Resposta += " Ação feita por "+denuncia.Id_Usu_Pen.Nome;
            usuarios.Add(denuncia.Id_Usu_Pen);      

            await emailBLL.EnviarEmail(usuarios, null, "Sua Pergunta/Reposta foi deletada do topico  O Topico http://www.mehelpehml.tk/Topico/TopicoSelecionado?topicoId=" + denuncia.Topico.Id + " Por denuncias");
            return await denunciaDAO.RemoverRespostaAsync(denuncia);
        }


    }
}
