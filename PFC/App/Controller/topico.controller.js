// Controle Topico
MeHelp.controller('topicoCtrl', function ($scope, topicoService) {
    var vm = this;
    $scope.semdados = false;
    carregarTopicos();
    $scope.notifications = {};
    $scope.notificationsErro = {};
    vm.limit = 3;
    var indexAcerto = 0;
    var indexErro = 0;
    carregarTemas();


    //Listar Topicos
    function carregarTopicos() {
        var listarTopicos = topicoService.getTodosTopicos();
        listarTopicos.then(function (d) {

            var i;
            for (i = 0; i < d.data.length; i++) {
                d.data[i].DataCria = converteDataHora(d.data[i].DataCria);
            }

            sessionStorage.setItem('topico', JSON.stringify(d.data));
            vm.cruise = d.data;
        },
            function () {
                console.log("Erro ao carregar a lista de Topicos");
            });
    }

    //Novo Topico
    $scope.novoTopico = function () {
        var acerto;
        var erro;
        var topico = {
            Titulo: $scope.titulo,
            Descricao: $scope.descricao,
            Tema: { Id: $scope.cmbTema }
        };

        
        
        var adicionaDadosTopico = topicoService.novoTopico(topico);

        adicionaDadosTopico.then(function (d) {
            if (d.data === true) {
               // alert("Pergunta Cadastrado");
                

                acerto = indexAcerto++;
                $scope.notifications[acerto] = 'Notificacao';
                carregarTopicos();
            } else {
                erro = indexErro++;
                $scope.notificationsErro[erro] = 'notificacaoErro';
               //alert("Pergunta nao Adicionado");
            }
        },
            function () {
                console.log("Erro ao cadastrar");
            });
    };

    /*$scope.add = function (notification) {

        var i;

        i = index++;
        $scope.invalidNotification = false;
       
    };*/



    $scope.load = function () {

        if (vm.limit >= vm.cruise.length) {
            $scope.semdados = true;
            $scope.informacao = 'End';
        }
        else {

            var increment = vm.limit + 3;
            vm.limit = increment > vm.cruise.length ? vm.cruise.length : increment;
            $scope.informacao = 'Load';
        }



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


        var topicosExistentes = [];
        topicosExistentes = JSON.parse(sessionStorage.getItem('topico'));
        var resultado = topicosExistentes.filter(obj => obj.Titulo.toLowerCase().indexOf(pesquisa,0) > -1);
        // Caso não consiga achar pesquisar no banco de dados
        if (resultado.lenght === 0) {
            var listarTopicos = topicoService.pesquisar(pesquisa);
            listarTopicos.then(function (d) {
               
                resultado = d.data;
                
            });
        }
        
       
        $scope.Topico = resultado;
            
        

    };

    $scope.pesquisaTema = function (temaSelecionado) {
        var topicosExistentes = [];
        topicosExistentes = JSON.parse(sessionStorage.getItem('topico'));
        if (temaSelecionado !== null) {


            var resultado = topicosExistentes.filter(obj => obj.Tema.Id === temaSelecionado.Id);
            // Caso não consiga achar pesquisar no banco de dados
            if (resultado.lenght === 0) {
                var listarTopicos = topicoService.pesquisar(temaSelecionado);
                listarTopicos.then(function (d) {

                    resultado = d.data;

                });
            }
        } else {
            resultado = topicosExistentes;
        }
        $scope.informacao = 'Load';
        vm.cruise = resultado;


    };

    
});