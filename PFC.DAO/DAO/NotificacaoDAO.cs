using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class NotificacaoDAO
    {
        #region Declaraçoes
        private Contexto contexto;
        private UsuarioDAO usuarioDAO = new UsuarioDAO();
        private SolicitacaoDAO solicitacaoDAO = new SolicitacaoDAO();
        #endregion

        #region Notificação Amizade
        public async Task<bool> NotificaçãoAmizadeAsync(dynamic id_notificacao)
        {
            SqlDataReader reader;
            Notificacao notificacao = new Notificacao();
            try
            {
                using (contexto = new Contexto())
                {
                    string strQuery = string.Format("SELECT * FROM Amizade WHERE Id = {0} ",
                        id_notificacao);
                    reader =await contexto.ExecutaComandoComRetorno(strQuery);


                    while (reader.Read())
                    {
                        notificacao.Solicitacao.usuarioSolicitado.Id = Convert.ToInt32(reader["Id_Usu_Pen"].ToString());
                    }
                    reader.Close();

                    strQuery = string.Format("INSERT INTO Notificacao VALUES({0},{1},{2},{3})",
                       notificacao.Solicitacao.usuarioSolicitado.Id, id_notificacao, "NULL", 1);

                    contexto.ExecutarInsert(strQuery);

                }
                reader.Close();
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }

        #endregion

        #region Notificação Denuncia
        public async Task<bool> NotificaçãoDenuncia(dynamic id_notificacao)
        {
            SqlDataReader reader;
            Notificacao notificacao = new Notificacao();
            try
            {
                using (contexto = new Contexto())
                {
                    string strQuery = string.Format("SELECT * FROM Amizade WHERE Id = {0} ",
                        id_notificacao);
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);


                    while (reader.Read())
                    {
                        notificacao.Solicitacao.usuarioSolicitado.Id = Convert.ToInt32(reader["Id_Usu_Pen"].ToString());
                    }
                    reader.Close();

                    strQuery = string.Format("INSERT INTO Notificacao VALUES({0},{1},{2},{3})",
                       notificacao.Solicitacao.usuarioSolicitado.Id, id_notificacao, "NULL", 1);

                    contexto.ExecutarInsert(strQuery);

                }
                reader.Close();
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }
        #endregion

        #region Verifica Notificacao Amizade Async
        public async Task<bool> VerificaNotificacaoAmizadeAsync(Usuario usuario)
        {
            SqlDataReader reader;
            List<Notificacao> solicitacao = new List<Notificacao>();
            string strQuery;
            contexto = new Contexto();
            strQuery = string.Format("SELECT * FROM Notificacao WHERE Id_Usu_Sol = {0} AND Status=1 ", usuario.Id);                                     

            using (contexto = new Contexto())
            {

                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    reader.Close();
                    return false;
                }

            };
            return true;
        }
        #endregion
    }
}
