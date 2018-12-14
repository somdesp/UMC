using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class GeneroDAO
    {
        private Contexto contexto;

        public List<Genero> ListarGenero()
        {
            var Genero = new List<Genero>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM Genero ";
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Genero()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Sexo = reader["Genero"].ToString()
                    };
                    Genero.Add(temObjeto);
                }
            }
            reader.Close();
            return Genero;
        }

        public Genero BuscaPorID(int idGenero)
        {
            var genero = new Genero();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Genero WHERE Id='{0}'", idGenero);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    var temObjeto = new Genero()
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Sexo = reader["Genero"].ToString()

                    };
                    genero = (temObjeto);
                }
            }
            return genero;
        }
    }
}
