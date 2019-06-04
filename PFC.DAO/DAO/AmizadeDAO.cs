using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PFC.Model;

namespace PFC.DAO
{
    public class AmizadeDAO
    {
        private Contexto contexto;
        private UsuarioDAO usuarioDAO = new UsuarioDAO();

        #region SolicitaAMizade
        public async Task<int> SolicitacaoAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            int Id_Amizade = 0;
            SqlDataReader reader;
            var strQuery = "";
            strQuery += "INSERT INTO Amizade (Id_Usu_Sol,Id_Usu_Pen,Id_Status) ";
            strQuery += string.Format("VALUES('{0}','{1}',1);SELECT SCOPE_IDENTITY() AS Id_Amizade",
                usuario.Id, usuarioSolicitado.Id);

            using (contexto = new Contexto())
            {
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Id_Amizade = Convert.ToInt32(reader["Id_Amizade"].ToString());
                }
                reader.Close();

                return Id_Amizade;
            }

        }

        #endregion

        #region ValidaAmizade
        public async Task<bool> ValidaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("select * from Amizade WHERE (Id_Usu_Sol = '{0}' AND Id_Usu_Pen='{1}') OR (Id_Usu_Sol = '{1}' AND Id_Usu_Pen='{0}')   ",
                    usuario.Id, usuarioSolicitado.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

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



        #region AceitaAmizade
        public async Task<bool> AceitaAmizadeAsync(Usuario usuario, Usuario usuarioSolicitado)
        {

            using (contexto = new Contexto())
            {
                try
                {
                    SqlDataReader reader;
                    int Notificacao = 0;
                    string strQuery = string.Format("UPDATE Amizade SET Id_Status = 4  WHERE Id_Usu_Pen = '{0}' AND Id_Usu_Sol='{1}' ",
                    usuario.Id, usuarioSolicitado.Id);
                    contexto.ExecutarInsert(strQuery);

                    strQuery = string.Format("SELECT * FROM  Amizade WHERE Id_Usu_Pen = '{0}' AND Id_Usu_Sol='{1}' ",
                    usuario.Id, usuarioSolicitado.Id);

                    reader = await contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        Notificacao = Convert.ToInt32(reader["Id"].ToString());
                    }
                    reader.Close();

                    strQuery = string.Format("UPDATE Notificacao SET Status = 0  WHERE Id_Amizade = '{0}'",
                    Notificacao);

                    contexto.ExecutarInsert(strQuery);

                    return true;

                }
                catch (Exception ex)
                {
                    return false;

                }
            }

        }

        #endregion


        #region CancelaAmizade
        public async Task<bool> CancelaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            using (contexto = new Contexto())
            {
                SqlDataReader reader;
                try
                {
                    string strQuery = string.Format("SELECT * FROM Amizade WHERE (Id_Usu_Pen = {0} AND Id_Usu_Sol={1}) OR (Id_Usu_Pen = {1} AND Id_Usu_Sol={0}) ",
                    usuario.Id, usuarioSolicitado.Id);

                    reader= await contexto.ExecutaComandoComRetorno(strQuery);
                    int idNot = 0;
                    while (reader.Read())
                    {
                        idNot = int.Parse(reader["Id"].ToString());         
                    }
                    reader.Close();
                    strQuery = string.Format("DELETE FROM Notificacao WHERE Id_Amizade ={0};", idNot);

                    contexto.ExecutarInsert(strQuery);


                    strQuery = string.Format(" DELETE Amizade WHERE (Id_Usu_Pen = {0} AND Id_Usu_Sol={1}) OR (Id_Usu_Pen = {1} AND Id_Usu_Sol={0}) ",
    usuario.Id, usuarioSolicitado.Id);

                    contexto.ExecutarInsert(strQuery);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }


            }


        }

        #endregion

        #region Carrega Lista Amizade
        public async Task<List<Usuario>> ListaAmizade(Usuario usuario)
        {
            SqlDataReader reader;
            List<Usuario> usuarios = new List<Usuario>();

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("select * from Amizade WHERE Id_Status = 4 and (Id_Usu_Pen = {0} or Id_Usu_Sol = {0})",
                    usuario.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    reader.Close();
                    return null;
                }
                else
                {
                    while (reader.Read())
                    {
                        Usuario temObjeto = new Usuario();
                        temObjeto.Id = Convert.ToInt32(reader["Id_Usu_Pen"].ToString());
                        if (temObjeto.Id != usuario.Id)
                        {
                            temObjeto = await usuarioDAO.ConsultaUsuarioInt(temObjeto);

                        }
                        temObjeto.Id = Convert.ToInt32(reader["Id_Usu_Sol"].ToString());
                        if (temObjeto.Id != usuario.Id)
                        {
                            temObjeto = await usuarioDAO.ConsultaUsuarioInt(temObjeto);

                        }

                        usuarios.Add(temObjeto);
                    }
                    reader.Close();
                }
            }
            return usuarios;
        }
        #endregion

    }
}
