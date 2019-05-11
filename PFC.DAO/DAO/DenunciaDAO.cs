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
            strQuery += "INSERT INTO Denuncia (Id_Usu_Sol,Id_Usu_Pen,Descricao,Resposta,Status,Id_Topico) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}');SELECT SCOPE_IDENTITY() AS Id_Denuncia",
                denuncia.Id_Usu_Sol.Id, denuncia.Id_Usu_Pen.Id, denuncia.Descricao, denuncia.Resposta, 1, denuncia.Topico.Id);


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
