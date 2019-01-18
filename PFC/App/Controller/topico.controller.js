// Controle Topico
MeHelp.controller('topicoCtrl', function ($scope, topicoService) {
    var vm = this;
    
    carregarTopicos();
    
    vm.limit = 3;
    
    carregarTemas();
    

    //Listar Topicos
    function carregarTopicos() {
        var listarTopicos = topicoService.getTodosTopicos();
        listarTopicos.then(function (d) {

            var i;
            for (i = 0; i < d.data.length; i++) {
                d.data[i].DataCria = converteDataHora(d.data[i].DataCria);
            }

            
            vm.cruise = d.data;
        },
            function () {
                console.log("Erro ao carregar a lista de Topicos");
            });
    };

    //Novo Topico
    $scope.novoTopico = function () {

        var topico = {
            Titulo: $scope.titulo,
            Descricao: $scope.descricao,
            Tema: { Id: $scope.cmbTema }
        };


        var adicionaDadosTopico = topicoService.novoTopico(topico);

        adicionaDadosTopico.then(function (d) {
            if (d.data === true) {
                alert("Pergunta Cadastrado");
                carregarTopicos();
            } else {
                alert("Pergunta nao Adicionado");
            }
        },
            function () {
                console.log("Erro ao cadastrar");
            });
    };


    $scope.load = function () {
        
        var increment = vm.limit + 3;
        vm.limit = increment > vm.cruise.lenght ? vm.cruise.lenght : increment;

    };


    //Visualizar Topico
    $scope.abrirTopico = function (Topico) {
        localStorage.setItem('IdTopico', JSON.stringify(Topico.Id));
        window.location = "/Topico/TopicoSelecionado?topicoId=" + Topico.Id;
    };


    function visualizarTopico(topicoId) {
        var Topico = { Id: topicoId };
        var DadosTopico = topicoService.visualizarTopico(Topico);
        localStorage.setItem("topico", JSON.stringify(Topico));

        DadosTopico.then(function (d) {
            if (d.data !== null) {
                d.data.DataCria = converteDataHora(d.data.DataCria);
                var i;
                for (i = 0; i < d.data.Resposta.length; i++) {
                    d.data.Resposta[i].DataCria = converteDataHora(d.data.Resposta[i].DataCria);
                }
                $scope.TopicosSelc = d.data;

            } else {
                alert("Pergunta nao Adicionado");
            }
        },
            function () {
                console.log("Erro ao cadastrar");
            });
        return DadosTopico;
    }

    //Listar Temas
    function carregarTemas() {
        var listarTemas = topicoService.getTodosTemas();
        listarTemas.then(function (d) {
            $scope.Tema = d.data;
        },
            function () {
                console.log("Erro ao carregar a lista de temas");
            });
    }


    //correção datas
    function converteDataHora(data) {
        var arrayMes = new Array(12);
        arrayMes[0] = "Jan";
        arrayMes[1] = "Fev";
        arrayMes[2] = "Mar";
        arrayMes[3] = "Abri";
        arrayMes[4] = "Mai";
        arrayMes[5] = "Jun";
        arrayMes[6] = "Jul";
        arrayMes[7] = "Ago";
        arrayMes[8] = "Set";
        arrayMes[9] = "Out";
        arrayMes[10] = "Nov";
        arrayMes[11] = "Dez";



        var dataReplace = data.toString().replace(/\/Date\((-?\d+)\)\//, '$1');
        var conversao = new Date(parseInt(dataReplace));
        return conversao.getDate() + " de " + arrayMes[conversao.getUTCMonth()] + " de " + conversao.getFullYear();
    }

    $scope.pesquisar = function (pesquisa) {

        var listarTopicos = topicoService.pesquisar(pesquisa);
        listarTopicos.then(function (d) {

            var i;
            for (i = 0; i < d.data.length; i++) {
                d.data[i].DataCria = converteDataHora(d.data[i].DataCria);
            }

            $scope.Topicos = d.data;

        },
            function () {
                console.log("Erro ao carregar a lista de Topicos");
            });
    };

});