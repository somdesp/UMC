MeHelp.controller('denunciaController', function ($window, $scope, denunciaService, topicoService, toaster) {
    ListaDenuncia();

    $scope.DenunciaUsuario = function (topico) {
   

        var Denuncia = {
            Id_Usu_Pen: topico.usuario,
            Id_Usu_Sol: JSON.parse(localStorage.getItem('model')),
            Descricao: $scope.denuncia,
            Topico: topico
        };


        toaster.pop('wait', "", "Enviando denuncia");
        var respostaUsuario = denunciaService.DenunciaUsuario(Denuncia);
        respostaUsuario.then(function (response) {
            if (response.data === true) {
                $scope.denuncia = "";
                toaster.clear();
                toaster.pop('success', "", "Denuncia enviada", 3000);
            } else {
                toaster.clear();
                toaster.error("", "Erro ao enviar a denuncia", 3000);
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
        toaster.pop('wait', "", "Removendo resposta");

        var respostaUsuario = denunciaService.RemoverResposta(Denuncia);
        respostaUsuario.then(function (response) {
            if (response.data === true) {    
                $scope.respostaRemove = "";
                topicoService.visualizarTopico(localStorage.getItem('IdTopico'));
                toaster.clear();
                toaster.pop('success', "", "Resposta/Pergunta removida!!", 3000);
                setTimeout(function () {
                location.reload();
                },
                    3000
                );
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