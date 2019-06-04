using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class NotificacaoDAO
    {
        #region Declaraçoes
        private Contexto contexto;
        private UsuarioDAO usuarioDAO = new UsuarioDAO();
        private TopicoDAO topicoDAO = new TopicoDAO();
        private AmizadeDAO solicitacaoDAO = new AmizadeDAO();
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
            catch (Exception ex)
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

        #region Verifica Notificacao Denuncia Async
        public async Task<bool> VerificaNotificacaoDenunciaAsync(Usuario usuario)
        {
            SqlDataReader reader;
            List<Notificacao> solicitacao = new List<Notificacao>();
            string strQuery;
            contexto = new Contexto();
            strQuery = string.Format("SELECT * FROM Notificacao WHERE Id_Amizade IS NULL AND Status=1");

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

        #region NotificaçãoAMizade
        public async Task<List<Amizade>> NotificacaoAmizade(Usuario usuario)
        {
            SqlDataReader reader;
            List<Amizade> solicitacao = new List<Amizade>();
            using (contexto = new Contexto())
            {

                string strQuery = string.Format("select * from Amizade WHERE Id_Usu_Pen = {0} AND Id_Status=1 ",
                    usuario.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    reader.Close();
                    return solicitacao;
                }
                else
                {
                    while (reader.Read())
                    {
                        Amizade temObjeto = new Amizade();
                        temObjeto.usuarioSolicitado.Id = Convert.ToInt32(reader["Id_Usu_Sol"].ToString());
                        temObjeto.usuarioSolicitado = await usuarioDAO.ConsultaUsuarioInt(temObjeto.usuarioSolicitado);
                        temObjeto.usuario.Id = Convert.ToInt32(reader["Id_Usu_Pen"].ToString());
                        temObjeto.usuario = await usuarioDAO.ConsultaUsuarioInt(temObjeto.usuario);
                        solicitacao.Add(temObjeto);
                    }
                    reader.Close();
                }


            }
            return solicitacao;

        }

        #endregion

        #region Notificação Denuncia
        public async Task<List<Denuncia>> NotificacaoDenunciaAsync(Usuario usuario)
        {
            SqlDataReader reader;
            List<Denuncia> denuncia = new List<Denuncia>();
            using (contexto = new Contexto())
            {

                string strQuery = string.Format("SELECT * FROM Denuncia WHERE Status=1 ");
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    reader.Close();
                    return denuncia;
                }
                else
                {
                    while (reader.Read())
                    {
                        Denuncia temObjeto = new Denuncia();
                        temObjeto.Id = Convert.ToInt32(reader["Id"].ToString());
                        temObjeto.Id_Usu_Sol.Id = Convert.ToInt32(reader["Id_Usu_Sol"].ToString());
                        temObjeto.Id_Usu_Sol = await usuarioDAO.ConsultaUsuarioInt(temObjeto.Id_Usu_Sol);
                        temObjeto.Id_Usu_Pen.Id = Convert.ToInt32(reader["Id_Usu_Pen"].ToString());
                        temObjeto.Id_Usu_Pen = await usuarioDAO.ConsultaUsuarioInt(temObjeto.Id_Usu_Pen);
                        temObjeto.Descricao =reader["Descricao"].ToString();
                        temObjeto.Resposta = reader["Resposta"].ToString();
                        temObjeto.DataCria = DateTime.Parse(reader["DataCria"].ToString(), new CultureInfo("pt-BR"));
                        temObjeto.Status =Convert.ToBoolean(reader["Status"].ToString());
                        temObjeto.Topico.Id = Convert.ToInt32(reader["Id_Topico"].ToString());
                        temObjeto.Topico = await topicoDAO.DetalheTopico(temObjeto.Topico);

                        denuncia.Add(temObjeto);
                    }
                    reader.Close();
                }
            }
            return denuncia;

        }

        #endregion
    }
}
