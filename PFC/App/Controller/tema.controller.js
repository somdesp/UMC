// Controle Tema
MeHelp.controller('temaCtrl', function ($scope, temaService) {

    carregarTemas();

    //Listar Temas
    function limparVariaveis() {
        $scope.nome = '';
        $scope.id = '';
    }

    //Listar Temas
    function carregarTemas() {
        var listarTemas = temaService.getTodosTemas();
        listarTemas.then(function (d) {
            $scope.Tema = d.data;
        },
            function () {
                console.log("Erro ao carregar a lista de temas");
            });
    };

    //Cadastrar Tema
    $scope.cadastrarTema = function () {

        var tema = {
            Nome: $scope.nome
        }


        var adicionaDadosTema = temaService.adicionarTema(tema);

        adicionaDadosTema.then(function (d) {
            if (d.data === true) {
                alert("Tema Cadastrado");
                carregarTemas();
            } else {
                console.log("Tema nao Adicionado");
            }
        },
            function () {
                alert("Erro ao cadastrar");
            });
    };

    //Carregar Modal atualizarTema
    $scope.atualizarTemaID = function (Tema) {
        $scope.id = Tema.Id;
        $scope.nome = Tema.Nome;
    };

    //Atualizar tema
    $scope.atualizarTema = function () {
        var tema = {
            Id: $scope.id,
            Nome: $scope.nome
        };

        var UpdateTema = temaService.atualizarTema(tema);

        UpdateTema.then(function (d) {
            if (d.data === true) {
                alert("Tema Atualizado");
                carregarTemas();
            } else {
                alert("Tema nao Atualizado");
            }
        },
            function () {
                console.log("Erro ao Atualizado");
            });
    };

    //Excluir Tema
    $scope.excluirTema = function (tema) {
        var btnVal = confirm("Deseja Excluir o Tema?");

        if (btnVal == true) {
            var excluiDadosTema = temaService.excluirTema(tema);
            excluiDadosTema.then(function (d) {
                alert(d.data);
                carregarTemas();
            },
                function () {
                    console.log("Erro ao Excluir");
                });
        };
    };

    $scope.resetDados = function () {
        $scope.nome = '';
    };

});