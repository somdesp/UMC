// Controle Tema
MeHelp.controller('temaCtrl', function ($scope, toaster, temaService ) {

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
        toaster.pop('wait', "", "Cadastrando tema!!");

        var tema = {
            Nome: $scope.nome
        };


        var adicionaDadosTema = temaService.adicionarTema(tema);

        adicionaDadosTema.then(function (d) {
            if (d.data === true) {
                toaster.clear();
                toaster.pop('success', "", "Tema cadastrado!!", 3000);
                carregarTemas();
            } else {
                toaster.clear();
                toaster.pop('error', "", 'Tema nao cadastrado!!', 3000);
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
        toaster.pop('wait', "", "Atualizando tema!!");

        var tema = {
            Id: $scope.id,
            Nome: $scope.nome
        };

        var UpdateTema = temaService.atualizarTema(tema);

        UpdateTema.then(function (d) {
            if (d.data === true) {
                toaster.clear();
                toaster.pop('success', "", "Tema atualizado!!",3000);
                carregarTemas();
            } else {
                toaster.clear();
                toaster.pop('error', "", 'Tema nao atualizado por estar em uso!!', 3000);
            }
        },
            function () {
                console.log("Erro ao Atualizado");
            });
    };

    //Excluir Tema
    $scope.excluirTema = function (tema) {
        toaster.pop('wait', "", "Excluindo tema!!");

        var btnVal = confirm("Deseja Excluir o Tema?");

        if (btnVal == true) {
            var excluiDadosTema = temaService.excluirTema(tema);
            excluiDadosTema.then(function (d) {
                if (d.data === true) {
                    toaster.clear();

                    toaster.pop('success', "", "Tema excluido!!", 3000);

                    carregarTemas();
                } else {
                    toaster.clear();
                    toaster.pop('error', "", 'Tema nao excluido por estar em uso!!', 3000);
                }


            },
                function () {
                    console.log("Erro ao Excluir");
                });

        };

        toaster.clear();

    };

    $scope.resetDados = function () {
        $scope.nome = '';
    };

});