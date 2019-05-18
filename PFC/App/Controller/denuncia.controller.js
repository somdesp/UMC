MeHelp.controller('denunciaController', function ($scope, denunciaService) {

    $scope.DenunciaUsuario = function (topico) {

        var Denuncia = {
            Id_Usu_Pen: topico.usuario,
            Id_Usu_Sol: JSON.parse(localStorage.getItem('model')),
            Descricao: $scope.denuncia,
            Topico: topico
        };



        var respostaUsuario = denunciaService.DenunciaUsuario(Denuncia);
        respostaUsuario.then(function (response) {
            if (response.data === true) {
                alert("Denuncia enviada");
            } else {
                console.log("Erro ao enviar a Denuncia");
            }
        });
    };

    $scope.submitForm = function () {

        // verifica se o formulário é válido
        if ($scope.userForm.$valid) {
            alert('our form is amazing');
        }

    };

    $scope.RemoverResposta = function (topico) {

        var Denuncia = {
            Id_Usu_Pen: topico.usuario,
            Id_Usu_Sol: JSON.parse(localStorage.getItem('model')),
            Resposta: $scope.respostaRemove,
            Topico: topico
        };

        var respostaUsuario = denunciaService.RemoverResposta(Denuncia);
        respostaUsuario.then(function (response) {
            if (response.data === true) {
                alert("Resposta removida!!");
            } else {
                console.log("Erro ao enviar a Denuncia");
            }
        });
    };
});