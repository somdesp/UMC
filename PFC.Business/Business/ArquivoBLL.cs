using PFC.DAO;
using PFC.Model;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class ArquivoBLL
    {
        ArquivoDAO arquivoDao = new ArquivoDAO();


        #region Carregar Imagem

        public async Task< Anexos> RecuperarImagem(Anexos arquivo)
        {
            return await arquivoDao.CarregarArquivo(arquivo);
        }

        #endregion
        #region Anexo de arquivos
        public bool AnexoArquivos(Anexos arquivo)
        {
            return arquivoDao.AnexoArquivos(arquivo);
        }
        #endregion

        #region Carregar Arq TOpico

        public async Task<Anexos> RecuperarArqTopico(Anexos arquivo)
        {
            return await arquivoDao.CarregarArquivoTopico(arquivo);
        }

        #endregion
    }
}
