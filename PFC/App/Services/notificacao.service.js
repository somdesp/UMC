//Notificacao USUARIO---------------------
MeHelp.service('notificacaoService', function ($http) {

    //Notificacao Amizade
    this.VisualizarNotificacao = function (usuario) {
        var request = $http({
            method: 'post',
            url: '../api/Notificacao/NotificacaoAmizade',
            data: usuario
        });

        return request;
    };



});