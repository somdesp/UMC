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
    this.amizadeSolicitada = function (usuario, usuarioSolicitado) {
        var request = $http({
            method: 'post',
            url: '/Usuario/AmizadeSolicitada',
            data: { usuario, usuarioSolicitado }
        });

        return request;
    };

    //Valida Amizade
    this.ValidaAmizade = function (amizade) {
        var request = $http({
            method: 'post',
            url: 'http://localhost:52005/api/Amizade/ValidaAmizade',
            data: amizade 
        });

        return request;
    };
});