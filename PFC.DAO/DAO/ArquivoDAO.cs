using PFC.Model;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class ArquivoDAO
    {
        private Contexto contexto;

        public bool AnexoArquivos(Anexos arquivo)
        {
           
            var strQuery = "";
            strQuery += "INSERT INTO Arquivos (Arquivo,ID_Topico) ";
            strQuery += string.Format("VALUES('{0}','{1}')",
                arquivo.Caminho,arquivo.id_topico);

            using (contexto = new Contexto())
            {
                return  contexto.ExecutarInsert(strQuery);
            }
        }

        #region Carregar Imagem
        public async Task<Anexos> CarregarArquivo(Anexos arquivo)
        {

            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("SELECT * FROM Arquivos WHERE Id = '{0}' ", arquivo.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

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

        #region Carregar Arq Topico
        public async Task<Anexos> CarregarArquivoTopico(Anexos arquivo)
        {

            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("SELECT * FROM Arquivos WHERE Id_Topico = '{0}' ", arquivo.id_topico);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

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
