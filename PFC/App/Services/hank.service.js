MeHelp.service('hankService', function ($http) {

    this.rankUsuario = function () {
        var request = $http({
            method: 'post',
            url: '../api/Ranking/ListarRank',
            data: '{}'
        });
        return request;
    }
});
