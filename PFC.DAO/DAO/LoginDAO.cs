using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFC.Model;

namespace PFC.DAO
{
    public class LoginDAO
    {
        private Contexto contexto;
        /// //////////////////////LOGIN USUARIO//////////////////////////////
        #region Login USUARIO
        public LoginViewModel Login(LoginViewModel login)
        {

            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                SqlCommand objCmd = new SqlCommand("SELECT * FROM Usuario WHERE" +
                    "(Email = @login OR Login =  @login ) AND PWDCOMPARE( @senha ,Senha) = 1;", contexto.forumConexao);
                objCmd.Parameters.AddWithValue("@login", login.Login);
                objCmd.Parameters.AddWithValue("@senha", login.Senha);

                reader = objCmd.ExecuteReader();

                while (reader.Read())
                {
                    var temObjeto = new LoginViewModel();

                    temObjeto.Email = reader["Email"].ToString();
                    temObjeto.Login = reader["Login"].ToString();
                    temObjeto.Nome = reader["Nome"].ToString();
                    temObjeto.Id = Convert.ToInt16(reader["Id"].ToString());
                    temObjeto.UploadArquivo.Id = Convert.ToInt32(reader["Id_Arquivo"].ToString());
                    temObjeto.Permissao.Id = Convert.ToInt32(reader["Id_Permissoes"].ToString());
                    login = (temObjeto);
                }
            }

            reader.Close();
            return login;
        }
        #endregion

        /// //////////////////////FIM LOGIN USUARIO//////////////////////////////
    }
}
