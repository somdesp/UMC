using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class Contexto : IDisposable
    {
        public readonly SqlConnection forumConexao;

        public Contexto()
        {
            forumConexao =
                new SqlConnection(ConfigurationManager.ConnectionStrings["dbconectionString"].ConnectionString);
            forumConexao.Open();
        }

        #region Insert
        public bool ExecutarInsert(string strQuery)
        {
            bool retorno = false;

            var cmdComando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = forumConexao
            };

            try
            {
                cmdComando.ExecuteNonQuery();
                retorno = true;
                return retorno;
            }
            catch (SqlException e)
            {
                return retorno;
            }


        }
        #endregion


        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando = new SqlCommand(strQuery, forumConexao);
            return cmdComando.ExecuteReader();
        }

        public void Dispose()
        {

            if (forumConexao.State == ConnectionState.Open)
                forumConexao.Close();

        }

    }
}

