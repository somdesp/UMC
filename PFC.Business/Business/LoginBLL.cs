using PFC.DAO;
using PFC.Model;
using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace PFC.Business
{
    public class LoginBLL
    {
        LoginDAO loginDao = new LoginDAO();
        AutorizacoesDAO AutorizacoesDao = new AutorizacoesDAO();
        ArquivoDAO arquivoDao = new ArquivoDAO();

        public LoginViewModel LoginUsuario(LoginViewModel model)
        {
            
            model = loginDao.Login(model);

            if ((model.Permissao.Id == 4) || (model.Nome == null))
            {
                model.success = false;
            }
            else
            {
                model.Permissao = AutorizacoesDao.ReturnAutPorID(model.Permissao);
                model.UploadArquivo = arquivoDao.CarregarArquivo(model.UploadArquivo);
                model.success = true;
            }

            return model;

        }
    }
}
