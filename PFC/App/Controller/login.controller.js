MeHelp.controller('loginCtrl', function ($scope, loginService) {

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

    //Logout  Usuario
    $scope.logoutUsuario = function () {

        var logoutUsuario = loginService.logoutUsuario();

        logoutUsuario.then(function (d) {
            localStorage.clear();
            alert(d.data);
            location.reload();
            localStorage.removeItem("model");
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };
});

$(function () {

    var ModelUsuario = JSON.parse(localStorage.getItem('model'));
    if (ModelUsuario !== null) {
        $("#imgUser").attr("src", " http://localhost:52005/Upload/" + ModelUsuario.UploadArquivo.Caminho);
    }
});