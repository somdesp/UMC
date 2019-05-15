using PFC.DAO;
using PFC.Model;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class LoginBLL
    {
        LoginDAO loginDao = new LoginDAO();
        AutorizacoesDAO AutorizacoesDao = new AutorizacoesDAO();
        ArquivoDAO arquivoDao = new ArquivoDAO();

        public async Task<LoginViewModel> LoginUsuario(LoginViewModel model)
        {
            
            model = loginDao.Login(model);

            if ((model.Auth.Id == 4) || (model.Nome == null))
            {
                model.success = false;
            }
            else
            {
                model.Auth = await AutorizacoesDao.ReturnAutPorID(model.Auth);
                model.UploadArquivo = await arquivoDao.CarregarArquivo(model.UploadArquivo);
                model.success = true;
            }

            return model;

        }
    }
}
