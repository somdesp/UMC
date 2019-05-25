MeHelp.controller('denunciaController', function ($window,$scope, denunciaService) {
    ListaDenuncia();

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
                location.reload();
            } else {
                console.log("Erro ao enviar a Denuncia");
            }
        });
    };


    function ListaDenuncia() {

        var ListaDenuncia = denunciaService.ListaDenuncia();

        ListaDenuncia.then(function (d) {

            if (d.data !== null) {
                $scope.ListaDenuncia = d.data;
            } else {
                $scope.ListaDenuncia = false;
            }
        },
            function () {
                console.log("Erro ao ListaDenuncia");
            });
    };


});