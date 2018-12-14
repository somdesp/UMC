using PFC.Model;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace PFC.DAO
{
    public class CursoDAO
    {
        private Contexto contexto;

        public List<Curso> ListarCurso()
        {
            var Curso = new List<Curso>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM Curso ";
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Curso()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        curso = reader["Curso"].ToString()
                    };
                    Curso.Add(temObjeto);
                }
            }
            reader.Close();
            return Curso;
        }

        public Curso BuscaPorID(int idCurso)
        {
            var Curso = new Curso();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Curso WHERE Id='{0}'", idCurso);
                reader = contexto.ExecutaComandoComRetorno(strQuery);
                while (reader.Read())
                {

                    var temObjeto = new Curso()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        curso = reader["Curso"].ToString()
                    };
                    Curso = (temObjeto);
                }
            }
            return Curso;
        }

    }
}
