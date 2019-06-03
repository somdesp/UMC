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
                    temObjeto.Id = Convert.ToInt32(reader["Id"].ToString());
                    temObjeto.UploadArquivo.Id = Convert.ToInt32(reader["Id_Arquivo"].ToString());
                    temObjeto.Auth.Id = Convert.ToInt32(reader["Id_Permissoes"].ToString());
                    login = (temObjeto);
                }
            }

            reader.Close();
            return login;
        }
        #endregion

        /// //////////////////////FIM LOGIN USUARIO//////////////////////////////
        /// 
         #region Recuperação de senha
        public async Task<Usuario> RecuperacaoSenha(Usuario usuario)
        {

            SqlDataReader reader;
            try
            {
                using (contexto = new Contexto())
                {

                    SqlCommand objCmd = new SqlCommand("select Id,Nome,Login,Email from Usuario where Email = @email and Login  = @login", contexto.forumConexao);
                    objCmd.Parameters.AddWithValue("@login", usuario.Login);
                    objCmd.Parameters.AddWithValue("@email", usuario.Email);

                    reader = await objCmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var temObjeto = new Usuario();

                        temObjeto.Id = Convert.ToInt16(reader["Id"].ToString());
                        temObjeto.Email = reader["Email"].ToString();
                        temObjeto.Nome = reader["Nome"].ToString();

                        usuario = (temObjeto);
                    }
                }

                reader.Close();
                return usuario;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        #endregion


        #region
        public async Task<bool> AlteracaoSenha(string senha, int idusuario)
        {
            using (contexto = new Contexto())
            {
                SqlCommand objCmd = new SqlCommand("update Usuario set Senha = PWDENCRYPT(@senha) where Id = @idusuario ", contexto.forumConexao);
                objCmd.Parameters.AddWithValue("@senha", senha);
                objCmd.Parameters.AddWithValue("@idusuario", idusuario);
                int numerolinha = await objCmd.ExecuteNonQueryAsync();
                if (numerolinha > 0)
                {
                    return true;


                }
                else
                {
                    return false;
                }
            }


        }



    }
    #endregion
}

