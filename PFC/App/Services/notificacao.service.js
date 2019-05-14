//Notificacao USUARIO---------------------
MeHelp.service('notificacaoService', function ($http) {

    //Notificacao Amizade
    this.NotificacaoAmizade = function (usuario) {
        var request = $http({
            method: 'post',
            url: '../api/Notificacao/NotificacaoAmizade',
            data: usuario
        });

        return request;
    };

    //Notificacao Amizade
    this.VisualizarNotificacaoAmizade = function (usuario) {
        var request = $http({
            method: 'post',
            
            url: '../api/Notificacao/VerificaNotificacaoAmizadeAsync',
            data: usuario
        });

        return request;
    };

    //Notificacao Geral
    this.VisualizarNotificacaoDenuncia = function (usuario) {
        var request = $http({
            method: 'post',

            url: '../api/Notificacao/VerificaNotificacaoDenunciaAsync',
            data: usuario
        });

        return request;
    };

    



    this.visualizarNotificacaoDenuncia = function (usuario) {
        var request = $http({
            method: 'post',
            url: '../api/Notificacao/NotificacaoDenunciaAsync',
            data: usuario
        });

        return request;
    };
});