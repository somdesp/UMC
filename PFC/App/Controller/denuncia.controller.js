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


    var modalConfirm = function (callback) {

        $("#btn-confirm").on("click", function () {
            $("#ModalDenunciaConfirma").modal('show');
        });

        $("#modal-btn-si").on("click", function () {
            callback(true);
            $("#ModalDenunciaConfirma").modal('hide');
        });

        $("#modal-btn-no").on("click", function () {
            callback(false);
            $("#ModalDenunciaConfirma").modal('hide');
        });
    };

    modalConfirm(function (confirm) {
        if (confirm) {
            //Acciones si el usuario confirma
        } else {
            //Acciones si el usuario no confirma
        }
    });

});