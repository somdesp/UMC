using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PFC.DAO
{
    public class AutorizacoesDAO
    {

        private Contexto contexto;

        #region Listar Permissoes
        public async Task<List<Autorizacoes>> ListarAutorizacoes()
        {
            var Autorizacoes = new List<Autorizacoes>();
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM Permissoes ");
                reader = await contexto.ExecutaComandoComRetorno(strQuery);
                while (reader.Read())
                {
                    var temObjeto = new Autorizacoes()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Permissao = reader["Nome"].ToString()
                    };
                    Autorizacoes.Add(temObjeto);
                }
            }
            reader.Close();
            return Autorizacoes;
        }
        #endregion

        public async Task<Autorizacoes> ReturnAutPorID(Autorizacoes auth)
        {
            var Autorizacoes = new Autorizacoes();
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                var strQuery = string.Format("SELECT * FROM Permissoes WHERE Id={0} ",auth.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);
                while (reader.Read())
                {
                    var temObjeto = new Autorizacoes()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Permissao = reader["Nome"].ToString()
                    };
                    Autorizacoes =(temObjeto);
                }
            }
            reader.Close();
            return Autorizacoes;
        }

    }
}
