MeHelp.controller('hankCtrl', function ($scope, hankService) {

    carregarHank();

    var localid = JSON.parse(localStorage.getItem('model'));

    $scope.Idusuario = localid.Id;

    function carregarHank() {
        var listarUsuarios = hankService.rankUsuario();
        $(".loader").show();
        listarUsuarios.then(function (d) {
            $(".loader").hide();
            $scope.hankeamento = d.data;
            
               
        },
            function () {
                console.log("Erro ao carregar a lista de usuario");
            });
    }


    $scope.SelecionarRank = function (opcaoSelecionada) {

        if (opcaoSelecionada === 1) {

            var listarUsuarios = hankService.rankUsuario();
            $(".loader").show();
            $scope.spinner = true;
            listarUsuarios.then(function (d) {
                if (d.data) {
                    $(".loader").hide();
                    $scope.hankeamento = d.data;
                } else {
                    $(".loader").hide();
                    $scope.hankeamento = 0;
                }

            },
                function () {
                    console.log("Erro ao carregar a lista de rank diario");
                });

        }
        else if (opcaoSelecionada === 2) {
            listarUsuarios = hankService.rankUsuarioSemanal();
            $(".loader").show();
            $scope.spinner = true;
            listarUsuarios.then(function (d) {
                if (d.data) {
                    $(".loader").hide();
                    $scope.hankeamento = d.data;
                } else {
                    $(".loader").hide();
                    $scope.hankeamento = 0;
                }
               
            },
                function () {
                    console.log("Erro ao carregar a lista de rank semanal");
                });

        } else if (opcaoSelecionada === 3) {
            
            listarUsuarios = hankService.rankUsuarioMensal();
            $(".loader").show();
            listarUsuarios.then(function (d) {
                if (d.data) {
                    $(".loader").hide();
                    $scope.hankeamento = d.data;
                } else {
                    $(".loader").hide();
                    $scope.hankeamento = 0;
                }
            },
                function () {
                    console.log("Erro ao carregar a lista de rank mensal");
                });


        }

    };
});