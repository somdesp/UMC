//Serviços do Login
MeHelp.service('loginService', function ($http) {



    //Servico por Login do usuario
    this.loginUsuario = function (LoginViewModel) {
        var request = $http({
            method: 'post',
            url: '/Login/Login',
            data: LoginViewModel
        });

        return request;
    };

    //Servico por Logout do usuario
    this.logoutUsuario = function () {
        var request = $http({
            method: 'post',
            url: '/Login/LogOut'
        });
        return request;
    };

   

});