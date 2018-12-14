MeHelp.controller('hankCtrl', function ($scope, hankService) {

    carregarHank();

    function carregarHank() {
        var listarUsuarios = hankService.rankUsuario();
        listarUsuarios.then(function (d) {
            $scope.hankeamento = d.data;
        },
            function () {
                console.log("Erro ao carregar a lista de usuario");
            });
    };
});