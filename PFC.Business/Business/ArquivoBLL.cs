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

        public bool CadastroImagem(Anexos arquivo)
        {
            return arquivoDao.CadastroImagem(arquivo);
        }

        #region Carregar Imagem

        public Anexos RecuperarImagem(Anexos arquivo)
        {
            return arquivoDao.CarregarArquivo(arquivo);
        }

        #endregion
    }
}
