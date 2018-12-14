using System;
using PFC.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PFC.DAO
{
    public class SemestreDAO
    {
        private Contexto contexto;
        CursoDAO cursoDao = new CursoDAO();

        public List<Semestre> ListarSemestre(int curso)
        {
            var Turma = new List<Semestre>();
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM Semestre S,Curso C WHERE S.Id_Curso = C.Id  AND S.Id_Curso = {0}", curso);
                reader = contexto.ExecutaComandoComRetorno(strQuery);
                while (reader.Read())
                {
                    var temObjeto = new Semestre()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        semestre = reader["Semestre"].ToString()
                    };
                    Turma.Add(temObjeto);
                }
            }
            reader.Close();
            return Turma;
        }

        public Semestre BuscaPorID(int idSemestre)
        {
            var semestre = new Semestre();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Semestre WHERE Id='{0}'", idSemestre);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Semestre()
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        semestre = reader["Semestre"].ToString(),
                        Curso =cursoDao.BuscaPorID(Convert.ToInt32(reader["Id_Curso"].ToString()))

                    };
                    semestre=(temObjeto);
                }
            }
            return semestre;
        }

        public List<Semestre> ListarSemestre()
        {
            var Semestre = new List<Semestre>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM Semestre ";
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Semestre()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        semestre = reader["Semestre"].ToString()
                    };
                    Semestre.Add(temObjeto);
                }
            }
            reader.Close();
            return Semestre;
        }

    }
}

