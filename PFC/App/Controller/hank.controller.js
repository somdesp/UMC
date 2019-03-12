MeHelp.controller('hankCtrl', function ($scope, hankService) {

    carregarHank();

    var localid = JSON.parse(localStorage.getItem('model'));

    $scope.Idusuario = localid.Id;

    function carregarHank() {
        var listarUsuarios = hankService.rankUsuario();
        listarUsuarios.then(function (d) {
            $scope.hankeamento = d.data;
        },
            function () {
                console.log("Erro ao carregar a lista de usuario");
            });
    }


    $scope.SelecionarRank = function (opcaoSelecionada) {

        if (opcaoSelecionada === 1) {

            var listarUsuarios = hankService.rankUsuario();
            listarUsuarios.then(function (response) {
                if (response.data) {
                    $scope.hankeamento = response.data;
                } else {
                    $scope.hankeamento = 0;
                }
                

            },
                function () {
                    console.log("Erro ao carregar a lista de rank diario");
                });

        }
        else if (opcaoSelecionada === 2) {
            listarUsuarios = hankService.rankUsuarioSemanal();
            listarUsuarios.then(function (d) {
                $scope.hankeamento = d.data;
            },
                function () {
                    console.log("Erro ao carregar a lista de rank semanal");
                });

        } else if (opcaoSelecionada === 3) {
            listarUsuarios = hankService.rankUsuarioMensal();
            listarUsuarios.then(function (d) {
                $scope.hankeamento = d.data;
            },
                function () {
                    console.log("Erro ao carregar a lista de rank mensal");
                });


        }

    };
});