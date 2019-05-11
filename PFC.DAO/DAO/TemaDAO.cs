using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PFC.DAO
{
    public class TemaDAO
    {
        private Contexto contexto;



        /// //////////////////////ADICIONAR TEMA//////////////////////////////

        public bool AdicionarTema(Tema tema)
        {

            var strQuery = "";
            strQuery += "INSERT INTO Tema (Tema,Id_Usuario) ";
            strQuery += string.Format("VALUES('{0}','{1}')",
               tema.Nome, tema.usuario.Id);

            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }

        }

        /// //////////////////////FIM ADICIONAR TEMA//////////////////////////////
        /// 
        /// 
        //////////////////CONSULTA TEMA//////////////////////////////
        public async Task<Tema> ConsultaTema(Tema tema)
        {
            var Tema = new Tema();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format("SELECT Id,Tema FROM Tema WHERE Id = '{0}' ", tema.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Tema()
                    {
                        Nome = reader["Tema"].ToString(),
                        Id = Convert.ToInt32(reader["Id"].ToString())
                    };
                    Tema = (temObjeto);
                }
            }
            reader.Close();
            return Tema;
        }
        public async Task<Tema> ConsultaTema(int id)
        {
            var Tema = new Tema();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format("SELECT Id,Tema FROM Tema WHERE Id = '{0}' ", id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Tema()
                    {
                        Nome = reader["Tema"].ToString(),
                        Id = Convert.ToInt32(reader["Id"].ToString())
                    };
                    Tema = (temObjeto);
                }
            }
            reader.Close();
            return Tema;
        }





        public async Task<List<Tema>> ConsultaTema(string pesquisa)
        {
            List<Tema> tema = new List<Tema>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format("SELECT Id,Tema FROM Tema WHERE Tema Like '%{0}%'", pesquisa);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Tema temObjeto = new Tema()
                    {
                        Nome = reader["Tema"].ToString(),
                        Id = Convert.ToInt32(reader["Id"].ToString())
                    };
                    tema.Add(temObjeto);
                }
            }
            reader.Close();
            return tema;
        }


        public async Task<List<Tema>> ListarTema()
        {
            var Curso = new List<Tema>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += " SELECT * FROM Tema ";
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Tema()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nome = reader["Tema"].ToString()
                    };
                    Curso.Add(temObjeto);
                }
            }
            reader.Close();
            return Curso;
        }

        public async Task<Tema >ListarTemaTopico(int idtema)
        {
            var tema = new Tema();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" SELECT * FROM Tema WHERE Id= '{0}'", idtema);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Tema()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nome = reader["Tema"].ToString()
                    };
                    tema = (temObjeto);
                }
            }
            reader.Close();
            return tema;
        }

        public async Task<bool> AtualizarTema(Tema tema)
        {
            SqlDataReader reader;
            var validaAlteracao = "SELECT Id_Tema FROM Topico ";
            validaAlteracao += string.Format(" WHERE Id_Tema = {0};", tema.Id);
            int retornoIdTema = 0;
            Boolean retorno = false;

            using (contexto = new Contexto())
            {
                reader = await contexto.ExecutaComandoComRetorno(validaAlteracao);
                while (reader.Read())
                {
                    retornoIdTema = int.Parse(reader["Id_Tema"].ToString());
                }
            }

            //Valida se tema esta em uso
            if (tema.Id == retornoIdTema)
            {
                retorno = false;
            }
            else
            {
                var strQuery = "";
                strQuery += " UPDATE Tema SET ";
                strQuery += string.Format(" Tema = '{0}' ", tema.Nome);
                strQuery += string.Format(" WHERE Id = {0}; ", tema.Id);

                using (contexto = new Contexto())
                {
                    contexto.ExecutarInsert(strQuery);
                    
                }

                retorno = true;
            }

            return retorno;
        }


        public async Task<string> ExcluirTema(Tema tema)
        {

            SqlDataReader reader;
            string retorno;
            var validaExclusao = "SELECT Id_Tema FROM Topico ";
            validaExclusao += string.Format(" WHERE Id_Tema = {0};", tema.Id);
            int retorno2 = 0;

            using (contexto = new Contexto())
            {
                reader = await contexto.ExecutaComandoComRetorno(validaExclusao);
                while (reader.Read())
                {
                    retorno2 = int.Parse(reader["Id_Tema"].ToString());
                }
            }

            //Valida se tema esta em uso
            if (tema.Id == retorno2)
            {
                retorno = "Nao e possivel Excluir o Tema Utilizado";
            }
            else
            {
                var strQuery = "";
                strQuery += " DELETE FROM Tema ";
                strQuery += string.Format(" WHERE Id = {0}; ", tema.Id);

                using (contexto = new Contexto())
                {
                    contexto.ExecutarInsert(strQuery);
                }

                retorno = "Tema excluido com sucesso";
            }

            return retorno;
        }


    }
}
