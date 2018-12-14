using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PFC.DAO
{
    public class AutorizaçoesDAO
    {

        private Contexto contexto;

        #region Listar Permissoes
        public List<Autorizaçoes> ListarAutorizaçoes()
        {
            var Autorizaçoes = new List<Autorizaçoes>();
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM Permissoes ");
                reader = contexto.ExecutaComandoComRetorno(strQuery);
                while (reader.Read())
                {
                    var temObjeto = new Autorizaçoes()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Permissao = reader["Nome"].ToString()
                    };
                    Autorizaçoes.Add(temObjeto);
                }
            }
            reader.Close();
            return Autorizaçoes;
        }
        #endregion

        public Autorizaçoes ReturnAutPorID(Autorizaçoes auth)
        {
            var Autorizaçoes = new Autorizaçoes();
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM Permissoes WHERE Id={0} ",auth.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);
                while (reader.Read())
                {
                    var temObjeto = new Autorizaçoes()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Permissao = reader["Nome"].ToString()
                    };
                    Autorizaçoes =(temObjeto);
                }
            }
            reader.Close();
            return Autorizaçoes;
        }

    }
}
