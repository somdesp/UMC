MeHelp.service('denunciaService', function ($http) {


    //Denuncia Topico
    this.DenunciaUsuario = function (denuncia) {
        var request = $http({
            method: 'post',
            url: '../api/Denuncia/DenunciaUsuario',
            data: denuncia
        });

        return request;
    };

});