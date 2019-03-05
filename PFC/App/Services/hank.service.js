MeHelp.service('hankService', function ($http) {

    this.rankUsuario = function () {
        var request = $http({
            method: 'POST',
            url: '/Ranking/ListarRank',
            data: '{}'
        });
        return request;
    };
});
