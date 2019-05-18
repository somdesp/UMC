using PFC.Model;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class DenunciaDAO
    {
        private Contexto contexto;

        #region DenunciaUsuario
        public async Task<bool> DenunciaUsuarioAsync(Denuncia denuncia)
        {
            SqlDataReader reader;

            var strQuery = "";
            strQuery += "INSERT INTO Denuncia (Id_Usu_Sol,Id_Usu_Pen,Descricao,Resposta,Status,Id_Topico,DataCria) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}');SELECT SCOPE_IDENTITY() AS Id_Denuncia",
                denuncia.Id_Usu_Sol.Id, denuncia.Id_Usu_Pen.Id, denuncia.Descricao, denuncia.Resposta, 1, denuncia.Topico.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            try
            {
                int IdDenuncia = 0;
                using (contexto = new Contexto())
                {
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);
                    strQuery = "";
                    while (reader.Read())
                    {
                        IdDenuncia = Convert.ToInt32(reader["Id_Denuncia"].ToString());
                        strQuery += string.Format("INSERT INTO Notificacao (Id_Denuncia,Status)VALUES({0},1)", IdDenuncia);

                    }
                    reader.Close();


                    contexto.ExecutarInsert(strQuery);

                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }



        }

        #endregion

        #region RemoverRespostaAsync
        public async Task<bool> RemoverRespostaAsync(Denuncia denuncia)
        {
            SqlDataReader reader;

            var strQuery = "";

            strQuery = string.Format("SELECT * FROM Denuncia WHERE Id_topico ={0}", denuncia.Topico.Id);

            try
            {
                int IdDenuncia = 0;
                using (contexto = new Contexto())
                {
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);
                    strQuery = "";

                    if (reader.HasRows.Equals(false))
                    {
                        reader.Close();

                        strQuery = "INSERT INTO Denuncia (Id_Usu_Sol,Id_Usu_Pen,Descricao,Resposta,Status,Id_Topico,DataCria) ";
                        strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            denuncia.Id_Usu_Sol.Id, denuncia.Id_Usu_Pen.Id, denuncia.Descricao, denuncia.Resposta, 0, denuncia.Topico.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        contexto.ExecutarInsert(strQuery);

                    }
                    else
                    {
                        while (reader.Read())
                        {
                            IdDenuncia = Convert.ToInt32(reader["Id_Denuncia"].ToString());
                        }
                        reader.Close();

                        strQuery = string.Format("UPDATE Denuncia SET Resposta={0},Status=0 WHERE Id = {1} ; UPDATE Notificacao SET Status ={1}", denuncia.Resposta, IdDenuncia);
                        contexto.ExecutarInsert(strQuery);
                    }

                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }



        }

        #endregion

        #region RemoverRespostaAsync
        public async Task<bool> ValidaDenunciaAs(Denuncia denuncia)
        {
            SqlDataReader reader;

            var strQuery = "";
            strQuery += "INSERT INTO Denuncia (Id_Usu_Sol,Id_Usu_Pen,Descricao,Resposta,Status,Id_Topico,DataCria) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}');SELECT SCOPE_IDENTITY() AS Id_Denuncia",
                denuncia.Id_Usu_Sol.Id, denuncia.Id_Usu_Pen.Id, denuncia.Descricao, denuncia.Resposta, 1, denuncia.Topico.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            try
            {
                int IdDenuncia = 0;
                using (contexto = new Contexto())
                {
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);
                    strQuery = "";
                    while (reader.Read())
                    {
                        IdDenuncia = Convert.ToInt32(reader["Id_Denuncia"].ToString());
                        strQuery += string.Format("INSERT INTO Notificacao (Id_Denuncia,Status)VALUES({0},1)", IdDenuncia);

                    }
                    reader.Close();


                    contexto.ExecutarInsert(strQuery);

                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }



        }

        #endregion


    }
}
