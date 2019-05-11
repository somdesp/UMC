MeHelp.controller('denunciaController', function ($scope, denunciaService) {

    $scope.DenunciaUsuario = function (topico) {

        var Denuncia = {
            Id_Usu_Pen: topico.usuario,
            Id_Usu_Sol: JSON.parse(localStorage.getItem('model')),
            Descricao: $scope.mensagem,
            Topico: topico
        };



        var respostaUsuario = denunciaService.DenunciaUsuario(Denuncia);
        respostaUsuario.then(function (response) {
            if (response.data === true) {
                alert("Denuncia enviada");
            } else {
  
            }
        });
    };

});