// Controle Topico
MeHelp.controller('managertopicoCtrl', function ($scope, topicoService, toaster) {

    carregarStatusTopico();

    $scope.buscarTopicos = function () {
        toaster.pop('wait', "", "Buscando topicos!!");
        var buscaTopico = {
            status: $scope.cmbStatus,
            datAbertIni: $scope.datAbertIni,
            datAbertFim: $scope.datAbertFim
        };
        var listarTopico = topicoService.getTopicosPesquisa(buscaTopico);
        listarTopico.then(function (d) {
            toaster.clear();
            $scope.Topico = d.data;
        },
            function () {
                console.log("Erro ao Carregar CMBSemestre");
            });

    };
    

    $scope.alterarTopico = function (Topico) {
        toaster.pop('wait', "", "Alterando topico!!");

        var listarTopico = topicoService.alterarTopico(Topico);
        listarTopico.then(function (d) {
            toaster.clear();
            toaster.pop('success', "", "Topico alterado!!",2000);
            $scope.buscarTopicos();
        },
            function () {
                console.log("Erro ao Carregar CMBSemestre");
            });

    };

    // Carregar StatusTopico 
    function carregarStatusTopico() {
        var listarStatusTopico = topicoService.getStatusTopico();
        listarStatusTopico.then(function (d) {
            $scope.StatusTopico = d.data;
        },
            function () {
                console.log("Erro ao carregar StatusTopico");
            });
    };

});