using PFC.Model;
using System;
using System.Data.SqlClient;

namespace PFC.DAO
{
    public class ArquivoDAO
    {
        private Contexto contexto;

        public bool AnexoArquivos(Anexos arquivo)
        {
           
            var strQuery = "";
            strQuery += "INSERT INTO Arquivos (Arquivo) ";
            strQuery += string.Format("VALUES('{0}')",
                arquivo.Caminho);

            using (contexto = new Contexto())
            {
                return  contexto.ExecutarInsert(strQuery);
            }
        }

        #region Carregar Imagem
        public Anexos CarregarArquivo(Anexos arquivo)
        {

            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("SELECT * FROM Arquivos WHERE Id = '{0}' ", arquivo.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Anexos temObjeto = new Anexos()
                    {
                        Caminho = reader["Arquivo"].ToString(),
                        Id = Convert.ToInt32(reader["Id"].ToString())
                    };
                    arquivo = (temObjeto);
                }

            }
            reader.Close();
            return arquivo;

        }
        #endregion



    }
}
