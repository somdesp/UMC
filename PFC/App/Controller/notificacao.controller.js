﻿MeHelp.controller('notificationController', function ($scope, notificacaoService, amizadeService) {
    var $self = this;
    var ModelUsuario = JSON.parse(localStorage.getItem('model'));
    visualizarNotificacao(ModelUsuario);


    $self.notifications = [];

    $self.count = function () {
        return $self.notifications.length;
    };

    $self.read = function (index) {
        $self.notifications.splice(index, 1);
    };

    //var signalRClient = new Hub('notification', {
    //    listeners: {
    //        'newContact': function (msg) {
    //            $self.notifications.push(msg);
    //            toaster.pop('success', msg);
    //            $scope.$apply();

    //            console.log('newContact called ' + msg);
    //        },
    //        'deleteContact': function (msg) {
    //            $self.notifications.push(msg);
    //            toaster.pop('error', msg);
    //            $scope.$apply();

    //            console.log('deleteContact called. ' + msg);
    //        }
    //    }
    //});

    function visualizarNotificacao(UsuarioSolicitado) {
        var Notificacao = notificacaoService.VisualizarNotificacao(UsuarioSolicitado);

        Notificacao.then(function (d) {

            if (d.data !== null) {
                $self.notifications.push("Voçe tem novas Mensagens");
                $scope.notifi = d.data;
                $scope.$apply();
            }
        },
            function () {
                console.log("Erro ao visualizarNotificacao");
            });
        return Notificacao;
    };

    //Aceitar Amizade
    $scope.aceitarAmizade = function (usuarios) {

        var aceitarAmizade = amizadeService.AceitaAmizade(usuarios);

        aceitarAmizade.then(function (d) {
            if (d.data === true) {
                alert("Solicitação Aceita");

            } else {
                alert("Solicitação Nao enviada");
            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };



});

