using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class ArquivoBLL
    {
        ArquivoDAO arquivoDao = new ArquivoDAO();


        #region Carregar Imagem

        public Anexos RecuperarImagem(Anexos arquivo)
        {
            return arquivoDao.CarregarArquivo(arquivo);
        }

        #endregion
        #region Anexo de arquivos
        public bool AnexoArquivos(Anexos arquivo)
        {
            return arquivoDao.AnexoArquivos(arquivo);
        }
        #endregion

        #region Carregar Arq TOpico

        public Anexos RecuperarArqTopico(Anexos arquivo)
        {
            return arquivoDao.CarregarArquivoTopico(arquivo);
        }

        #endregion
    }
}
