using PFC.DAO;
using PFC.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class DenunciaBLL
    {
        DenunciaDAO denunciaDAO = new DenunciaDAO();
        EmailBLL emailBLL = new EmailBLL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        TopicoBLL topicoBLL = new TopicoBLL();

        public async Task<bool> DenunciaUsuario(Denuncia denuncia)
        {

            List<Usuario> usuarios = new List<Usuario>();
            List<Denuncia> denunciasArray = await denunciaDAO.VerificaDenuncias(denuncia);
            if (denunciasArray.Count >= 10)
            {
                topicoBLL.RemoverResposta(denuncia);
                usuarioBLL.InativarUsuario(denuncia.Id_Usu_Pen);
                denunciaDAO.AtualizaDenuncias(denunciasArray);
                usuarios.Add(denuncia.Id_Usu_Pen);
                await emailBLL.EnviarEmail(usuarios, null,
                    "<b>Sua pergunta/resposta</b> '<i>" + denuncia.Topico.Descricao + "'</i><b> foi excluida por excesso de denuncias!!<br><h1>E sua conta foi inativada!!</h1>");
                return true;

            }
            else
            {                
                usuarios = await usuarioBLL.ConsultaUsuarioMasterADM();
               await emailBLL.EnviarEmail(usuarios, null, "Existe novas notificaçoes");
                return await denunciaDAO.DenunciaUsuarioAsync(denuncia);
            }
        }

        public async Task<bool> RemoverResposta(Denuncia denuncia)
        {

            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                denuncia.Resposta += " Ação feita por " + denuncia.Id_Usu_Sol.Nome;
                usuarios.Add(denuncia.Id_Usu_Pen);
                topicoBLL.RemoverResposta(denuncia);

                await emailBLL.EnviarEmail(usuarios, null, "Sua Pergunta/Reposta foi deletada do topico"+
             "http://www.mehelpehml.tk/Topico/TopicoSelecionado?topicoId=" + denuncia.Topico.Id + " por desrespeitar as regras do fórum");
                return await denunciaDAO.RemoverRespostaAsync(denuncia);
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<List<Denuncia>> ListaDenuncia()
        {
            try
            {
                return await denunciaDAO.ListaDenuncia();
            }
            catch (System.Exception)
            {
                throw;
            }
        }




    }
}
