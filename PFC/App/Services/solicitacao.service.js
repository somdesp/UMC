//AMIZADE USUARIO---------------------
MeHelp.service('amizadeService', function ($http) {

    //Servico por Vusualizar usuarios
    this.visualizarPerfil = function (Usuario) {
        var request = $http({
            method: 'post',
            url: '/Usuario/VisualizarPerfil',
            data: Usuario
        });

        return request;
    };
    //Solicitar Amizade
    this.amizadeSolicitada = function (Solicitacao) {
        var request = $http({
            method: 'post',
            url: '../api/Solicitacao/AmizadeSolicitada',
            data: Solicitacao
        });

        return request;
    };

    //Valida Amizade
    this.ValidaAmizade = function (amizade) {
        var request = $http({
            method: 'post',
            url:'../api/Solicitacao/ValidaAmizade',
            data: amizade 
        });

        return request;
    };

    //Aceita Amizade
    this.AceitaAmizade = function (amizade) {
        var request = $http({
            method: 'post',
            url: '../api/Solicitacao/AceitaAmizade',
            data: amizade
        });

        return request;
    };
});