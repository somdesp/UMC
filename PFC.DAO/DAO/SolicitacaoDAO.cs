using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PFC.Model;

namespace PFC.DAO
{
    public class SolicitacaoDAO
    {
        private Contexto contexto;
        private UsuarioDAO usuarioDAO = new UsuarioDAO();

        #region SolicitaAMizade
        public bool solicitacaoAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            var strQuery = "";
            strQuery += "INSERT INTO Amizade (Id_Usu_Sol,Id_Usu_Pen,Id_Status) ";
            strQuery += string.Format("VALUES('{0}','{1}',1)",
                usuario.Id, usuarioSolicitado.Id);

            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }

        }

        #endregion

        #region ValidaAmizade
        public bool ValidaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("select * from Amizade WHERE Id_Usu_Sol = '{0}' AND Id_Usu_Pen='{1}' ",
                    usuario.Id, usuarioSolicitado.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    reader.Close();
                    return false;
                }


            }
            reader.Close();
            return true;

        }

        #endregion

        #region NotificaçãoAMizade
        public async Task<List<Solicitacao>> NotificacaoAmizade(Usuario usuario)
        {
            SqlDataReader reader;
            List<Solicitacao> solicitacao = new List<Solicitacao>();
            using (contexto = new Contexto())
            {

                string strQuery = string.Format("select * from Amizade WHERE Id_Usu_Pen = {0} AND Id_Status=1 ",
                    usuario.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    reader.Close();
                    return solicitacao;
                }
                else
                {
                    while (reader.Read())
                    {
                        Solicitacao temObjeto = new Solicitacao();
                        temObjeto.usuarioSolicitado.Id = Convert.ToInt32(reader["Id_Usu_Sol"].ToString());
                        temObjeto.usuarioSolicitado = usuarioDAO.ConsultaUsuarioInt(temObjeto.usuarioSolicitado);
                        temObjeto.usuario.Id = Convert.ToInt32(reader["Id_Usu_Pen"].ToString());
                        temObjeto.usuario = usuarioDAO.ConsultaUsuarioInt(temObjeto.usuario);
                        solicitacao.Add(temObjeto);
                    }
                    reader.Close();
                }


            }
            return solicitacao;

        }

        #endregion

        #region AceitaAmizade
        public bool AceitaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("UPDATE Amizade SET Id_Status = 4  WHERE Id_Usu_Pen = '{0}' AND Id_Usu_Sol='{1}' ",
                    usuario.Id, usuarioSolicitado.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    reader.Close();
                    return false;
                }


            }
            reader.Close();
            return true;

        }

        #endregion

    }
}
