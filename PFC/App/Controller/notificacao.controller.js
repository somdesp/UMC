MeHelp.controller('notificationController', function ($scope, notificacaoService, amizadeService, toaster) {
    var $self = this;
    var ModelUsuario = JSON.parse(localStorage.getItem('model'));
    visualizarNotificacaoAmizade(ModelUsuario);
    if (ModelUsuario.Auth.Id === 1 || ModelUsuario.Auth.Id === 3) {
        visualizarNotificacaoDenuncia(ModelUsuario);
    }


    $self.notifications = [];

    $self.count = function () {
        return $self.notifications.length;
    };

    $self.read = function (index) {
        $self.notifications.splice(index, 1);
    };

    //valida notificação Amizade
    function visualizarNotificacaoAmizade(UsuarioSolicitado) {
        var Notificacao = notificacaoService.VisualizarNotificacaoAmizade(UsuarioSolicitado);



        Notificacao.then(function (d) {

            if (d.data !== false) {
                $self.notifications.push("Voce tem novas Mensagens");
                NotificacaoAmizade(UsuarioSolicitado);
                $scope.$apply();
            }
        },
            function () {
                console.log("Erro ao visualizarNotificacao");
            });

    };

    //valida notificação denuncia
    function visualizarNotificacaoDenuncia(UsuarioSolicitado) {
        var Notificacao = notificacaoService.VisualizarNotificacaoDenuncia(UsuarioSolicitado);



        Notificacao.then(function (d) {

            if (d.data !== false) {
                $self.notifications.push("Voçe tem novas Mensagens");
                NotificacaoDenuncia(UsuarioSolicitado);
                $scope.$apply();
            }
        },
            function () {
                console.log("Erro ao visualizarNotificacao");
            });

    };


    function NotificacaoAmizade(UsuarioSolicitado) {
        var Notificacao = notificacaoService.NotificacaoAmizade(UsuarioSolicitado);

        Notificacao.then(function (d) {

            if (d.data !== true) {
                $scope.notifiamizade = d.data;
            } else {
                $scope.notifiamizade = false;
            }
        },
            function () {
                console.log("Erro ao visualizarNotificacaoAmizade");
            });
        return Notificacao;
    };

    function NotificacaoDenuncia(UsuarioSolicitado) {
        var Notificacao = notificacaoService.visualizarNotificacaoDenuncia(UsuarioSolicitado);

        Notificacao.then(function (d) {

            if (d.data !== null) {
                $scope.notifidenuncia = d.data;
            } else {
                $scope.notifidenuncia = false;
            }
        },
            function () {
                console.log("Erro ao visualizarNotificacaoAmizade");
            });
        return Notificacao;
    };

    //Aceitar Amizade
    $scope.aceitarAmizade = function (usuarios) {

        var aceitarAmizade = amizadeService.AceitaAmizade(usuarios);

        aceitarAmizade.then(function (d) {
            if (d.data === true) {
                toaster.pop('success', "", "Solicitação aceita!!", 2000);          
                NotificacaoAmizade(ModelUsuario);

            } else {
                toaster.pop('warn', "", "Solicitação não aceita!!", 2000);        

            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };


    //Recusa Amizade
    $scope.recusarAmizade = function (usuarios) {

        var aceitarAmizade = amizadeService.CancelaAmizade(usuarios);

        aceitarAmizade.then(function (d) {
            if (d.data === true) {
                toaster.pop('warn', "", "Solicitação não aceita!!", 2000);      

                NotificacaoAmizade(ModelUsuario);

            } else {
                alert("Solicitação Nao enviada");
            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };

    

});

