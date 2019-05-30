using PFC.DAO;
using PFC.Model;
using System;
using System.Text;
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

        public async Task<Usuario> RecuperarLogin(Usuario usuarioRecuperar)
        {
            //Primeira coisa verificar se esse usuario existe
            EmailBLL email = new EmailBLL();

            Usuario usuario = await loginDao.RecuperacaoSenha(usuarioRecuperar);
            
            

            if (usuario.Id != 0)
            {
                string senhanova = RandomString(5, false);
                usuario.Senha = senhanova;
                await email.EnviarEmail(usuario, null, "Caro usuario a senha nova para acesso é essa:" + senhanova + "");
            }

            return usuario;

        }

        //Randomizar Strings
        public  string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                return  builder.ToString().ToLower();
            }
                
            return  builder.ToString();
        }
    }
}
