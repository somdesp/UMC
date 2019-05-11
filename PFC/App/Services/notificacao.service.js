//Notificacao USUARIO---------------------
MeHelp.service('notificacaoService', function ($http) {

    //Notificacao Amizade
    this.VisualizarNotificacaoAmizade = function (usuario) {
        var request = $http({
            method: 'post',
            url: '../api/Notificacao/NotificacaoAmizade',
            data: usuario
        });

        return request;
    };

    //Notificacao Geral
    this.VisualizarNotificacaoGeral = function (usuario) {
        var request = $http({
            method: 'post',
            
            url: '../api/Notificacao/VerificaNotificacaoAmizade',
            data: usuario
        });

        return request;
    };
});