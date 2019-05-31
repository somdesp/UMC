MeHelp.controller('loginCtrl', function ($scope, loginService, toaster, $window) {

    var indexAcerto = 0;
    //Login Usuario
    $scope.loginUsuario = function () {

        var LoginViewModel = {
            Login: $scope.login,
            Senha: $scope.senha
        };

        var loginUsuario = loginService.loginUsuario(LoginViewModel);

        loginUsuario.then(function (d) {
            $scope.erro = null;
            if (d.data.success === false) {
                //$("#resposta").text("Usuario ou senha Incorretos");

                $scope.erro = true;
                $scope.descricaoErro = "Usuario ou senha Incorretos";
            } else if (d.data.success === true) {

                localStorage.setItem('model', JSON.stringify(d.data)),

                    location.reload();
            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };

    $scope.pegarIDUsuario = function () {
        var objetoUsuario = JSON.parse(localStorage.getItem('model'));
        window.location.href = "/Usuario/PerfilUsuario?usuarioid=" + objetoUsuario.Id;
    };

    //Logout  Usuario
    $scope.logoutUsuario = function () {

        var logoutUsuario = loginService.logoutUsuario();
       
        toaster.pop('wait', "", "Deslogando usuario",2000);
        logoutUsuario.then(function (d) {
            localStorage.removeItem("model");
            localStorage.clear();
            setTimeout(function () {
                toaster.pop('success', "", "Usuario deslogado");
            },
                1000
            );
         
            setTimeout(function () {
                location.reload();
            },
                3000
            );
           
          
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };

    $scope.recuperarsenha = function () {
        var usuario = {};
        usuario.Login = $scope.loginrecuperacao;
        usuario.Email = $scope.emailrecuperacao;
        var result = loginService.recuperarSenha(usuario);
    };



});





$(function () {
    var url_atual = window.location.href;

    var ModelUsuario = JSON.parse(localStorage.getItem('model'));
    if (ModelUsuario !== null) {
        $("#imgUser").attr("src", "/Upload/" + ModelUsuario.UploadArquivo.Caminho);
    }
});