MeHelp.service('hankService', function ($http) {

    this.rankUsuario = function () {
        var request = $http({
            method: 'POST',
            url: '/Ranking/ListarRank',
            data: '{}'
        });
        return request;
    };

    this.rankUsuarioSemanal = function () {
        var request = $http({
            method: 'POST',
            url: '/Ranking/ListarRankSemanal',
            data: '{}'
        });
        return request;
    };

    this.rankUsuarioMensal = function () {
        var request = $http({
            method: 'POST',
            url: '/Ranking/ListarRankMensal',
            data: '{}'
        });
        return request;
    };


    this.rankInicial = function () {
        var request = $http({
            method: 'POST',
            url: '/Ranking/ListarRankInicial',
            data: '{}'
        });
        return request;
    };



});
