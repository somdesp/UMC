using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class UsuarioDAO
    {
        private Contexto contexto;
        CursoDAO cursoDao = new CursoDAO();
        SemestreDAO semestreDao = new SemestreDAO();
        GeneroDAO generoDao = new GeneroDAO();
        ArquivoDAO arquivoDAO = new ArquivoDAO();

        #region Adicionar Usuario
        public bool AdicionarUsuario(Usuario usuario)
        {

            var strQuery = "";
            strQuery += "INSERT INTO Usuario (Nome,Login,Email,Senha,Id_Genero,DataNasci,RGM,Id_Curso,Id_Semestre,DataCad,Id_Permissoes,Id_Arquivo) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}',PWDENCRYPT('{3}'),'{4}','{5}','{6}','{7}','{8}','{9}','{10}',(select max(Id) from Arquivos))",
                usuario.Nome, usuario.Login, usuario.Email, usuario.Senha, usuario.Sexo.Id, usuario.DataNasci.ToString("dd/MM/yyyy"), usuario.RGM, usuario.Curso.Id, usuario.Semestre.Id, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), 3);

            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }

        }
        #endregion

        #region Consulta Usuario
        public async Task<Usuario> ConsultaUsuario(Usuario cad)
        {
            var usuarios = new Usuario();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format("SELECT Id,Nome,Email,Login,Id_Arquivo FROM Usuario WHERE Email = '{0}' OR Login = '{1}'", cad.Email, cad.Login);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Usuario()
                    {
                        Email = reader["Email"].ToString(),
                        Login = reader["Login"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        //Id_Imagem = Convert.ToInt32(reader["Id_Arquivo"].ToString()),
                        Id = Convert.ToInt32(reader["Id"].ToString())
                    };
                    usuarios = (temObjeto);
                }
            }
            reader.Close();
            return usuarios;
        }
        #endregion

        #region Lista por pesquisa // Sem uso no momento
        public async Task< List<Usuario>> ConsultaUsuario(string pesquisa)
        {
            var usuarios = new List<Usuario>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format("select u.Id,u.Nome,u.Login,u.Email,u.RGM,u.DataNasci,c.Curso,s.Semestre,g.Genero from Usuario u,Genero g, Semestre s,Curso c where u.Id_Curso = c.Id and g.Id = u.Id_Genero and u.Id_Semestre = s.Id and  Id_Permissoes <> 1002 and u.Nome Like '%{0}%'", pesquisa);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Usuario temObjeto = new Usuario();
                        temObjeto.Id = Convert.ToInt16(reader["Id"].ToString());
                        temObjeto.Email = reader["Email"].ToString();
                        temObjeto.Login = reader["Login"].ToString();
                        temObjeto.Nome = reader["Nome"].ToString();
                        temObjeto.RGM = reader["RGM"].ToString();
                        temObjeto.DataNasci = Convert.ToDateTime(reader["DataNasci"].ToString());
                        temObjeto.Semestre.semestre = reader["Semestre"].ToString();
                        temObjeto.Curso.curso = reader["Curso"].ToString();


                        usuarios.Add(temObjeto);
                    }
                }
            }
            reader.Close();
            return usuarios;
        }
        #endregion

        #region Atualiza Usuario
        public bool AtualizarUsuario(Usuario usuario)
        {
            var strQuery = "";
            strQuery += " UPDATE Usuario SET ";
            strQuery += string.Format(" Nome = '{0}', ", usuario.Nome);
            strQuery += string.Format(" RGM = '{0}', ", usuario.RGM);
            strQuery += string.Format(" Email = '{0}', ", usuario.Email);
            strQuery += string.Format(" DataNasci = '{0}', ",usuario.DataNasci.ToString("yyyy-MM-dd"));
            strQuery += string.Format(" Id_Curso = '{0}', ", usuario.Curso.Id);
            strQuery += string.Format(" Id_Semestre = '{0}', ", usuario.Semestre.Id);
            strQuery += string.Format(" Id_Genero = '{0}' ", usuario.Sexo.Id);
            //strQuery += string.Format(" DataCad = '{0}' ", DateTime.Now.ToString());
            strQuery += string.Format(" WHERE Id = {0}; ", usuario.Id);

            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }
        #endregion

        public bool AtualizarSenha(Usuario usuario)
        {
            var strQuery = "";
            strQuery += " UPDATE Usuario SET ";
            strQuery += string.Format(" Senha = PWDENCRYPT('{0}') ", usuario.Senha);
            strQuery += string.Format(" WHERE Id = {0}; ", usuario.Id);
            using (contexto = new Contexto())
            {
                return  contexto.ExecutarInsert(strQuery);
            }
        }





        #region Lista Somente Usuarios Ativos
        public async Task<List<Usuario>> ListarUsuarios()
        {
            var usuarios = new List<Usuario>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = "SELECT * FROM Usuario WHERE Id_Permissoes <> 4";
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {

                    Usuario temObjeto = new Usuario();
                    temObjeto.Id = Convert.ToInt16(reader["Id"].ToString());
                    temObjeto.Nome = reader["Nome"].ToString();
                    temObjeto.Login = reader["Login"].ToString();
                    temObjeto.RGM = reader["RGM"].ToString();
                    temObjeto.Email = reader["Email"].ToString();
                    temObjeto.DataNasci = Convert.ToDateTime(reader["DataNasci"].ToString());
                    temObjeto.Sexo.Id =(Convert.ToInt32(reader["Id_Genero"].ToString()));
                    temObjeto.Semestre.Id = (Convert.ToInt32(reader["Id_Semestre"].ToString()));
                    temObjeto.Curso.Id = (Convert.ToInt32(reader["Id_Curso"].ToString()));

                    usuarios.Add(temObjeto);
                }
            }
            reader.Close();
            return (usuarios);

        }
        #endregion

        #region Consulta Usuario Por ID
        public async Task<Usuario> ConsultaUsuarioInt(Usuario usuario)
        {
            var usuarios = new Usuario();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format("SELECT * FROM Usuario WHERE Id = '{0}' ", usuario.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Usuario();

                    temObjeto.Email = reader["Email"].ToString();
                    temObjeto.Login = reader["Login"].ToString();
                    temObjeto.Nome = reader["Nome"].ToString();
                    temObjeto.Id = Convert.ToInt16(reader["Id"].ToString());
                    temObjeto.RGM = reader["RGM"].ToString();
                    temObjeto.DataCad = Convert.ToDateTime(reader["DataCad"].ToString());
                    temObjeto.Curso.Id = Convert.ToInt16(reader["Id_Curso"].ToString());
                    temObjeto.Semestre.Id = Convert.ToInt16(reader["Id_Semestre"].ToString());
                    temObjeto.DataNasci = Convert.ToDateTime(reader["DataNasci"].ToString());
                    temObjeto.Sexo.Id = Convert.ToInt16(reader["Id_Genero"].ToString());
                    temObjeto.UploadArquivo.Id = Convert.ToInt32(reader["Id_Arquivo"].ToString());
                    temObjeto.Auth.Id = Convert.ToInt32(reader["Id_Permissoes"].ToString());
                   

                    usuarios = (temObjeto);
                }
            }
            reader.Close();
            return usuarios;
        }


        #endregion

        #region Inativar Usuario
        public bool InativarUsuario(Usuario usuario)
        {
            try
            {
                using (contexto = new Contexto())
                {
                    string strQuery = string.Format("UPDATE Usuario SET Id_Permissoes = 4 WHERE Id = '{0}' ", usuario.Id);
                    contexto.ExecutaComandoComRetorno(strQuery);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                throw;
            }



        }


        #endregion



        #region Lista por pesquisa // Sem uso no momento
        public  async Task<List<Usuario>> PesquisarUsuario()
        {
            var usuarios = new List<Usuario>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format("select u.Id'ID',u.Nome'Nome',u.Id,arqu.Arquivo'Anexo'  from Usuario u INNER JOIN Arquivos arqu ON u.Id = arqu.Id");
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Usuario temObjeto = new Usuario();
                        temObjeto.Id = Convert.ToInt16(reader["Id"].ToString());
                        temObjeto.Nome = reader["Nome"].ToString();
                        temObjeto.UploadArquivo.Caminho = reader["Anexo"].ToString();
                        usuarios.Add(temObjeto);
                    }
                }
            }
            reader.Close();
            return usuarios;
        }
        #endregion

    }

}

