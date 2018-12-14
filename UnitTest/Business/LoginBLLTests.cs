using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFC.Model;

namespace PFC.Business.Tests
{
    [TestClass()]
    public class LoginBLLTests
    {
        [TestMethod()]
        public void LoginUsuarioTest()
        {
            var loginviewmodel = new LoginViewModel();
            loginviewmodel.Login = "somdesp";
            loginviewmodel.Senha = "123456";


            var LoginBLL = new LoginBLL();
            loginviewmodel = LoginBLL.LoginUsuario(loginviewmodel);
            Assert.AreEqual(true,loginviewmodel.success);
          //  Assert.IsTrue(loginviewmodel.success);
            //Assert.Fail();
        }
    }
}