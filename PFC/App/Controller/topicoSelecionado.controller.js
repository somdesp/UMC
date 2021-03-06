﻿MeHelp.controller('topicoSelecionadoCtrl', function ($scope, topicoService, entityService) {

    var topico = localStorage.getItem("topico");
    visualizarTopico(localStorage.getItem('IdTopico'));
    $scope.usuario = JSON.parse(localStorage.getItem('model'));
    var cont = 0;
    carregarIdUsuario();

    $scope.salvarAnexos = function (Topico) {
        entityService.saveTutorial(Topico)
            .then(function (data) {
                console.log(data);
            });
    };


    //Listar Topicos
    function carregarTopicos(topico) {
        var x = carregarIdUsuario();
        var listarTopicos = topicoService.getTodosTopicos();
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
    }

    $scope.topicodeTexto = function (topico) {
        cont++;
        if ($scope.itemSelecionado === topico) {
          
            $scope.itemSelecionado = null;

        }
        else {
            $scope.itemSelecionado = topico;
        }
             
               
    };


    function carregarIdUsuario() {

        var IDusuario = topicoService.idUsuarioatual();
        IDusuario.then(function (d) {
            $scope.usuID = d.data;


        });

    }
    function carregarIdUsuarioLogado() {
        var usuarioLog = 0;
        var IDusuario = topicoService.idUsuarioatual();
        IDusuario.then(function (d) {
            var usuarioLog = d.data.idusuario;


        });
        return usuarioLog;
    }




    //Novo Post
    $scope.novoPost = function (Topico2) {

        var Topico = {
            Descricao: Topico2.Descricao,
            TopicoFilho: { Descricao: Topico2.Descricao },
            Anexos: Topico2.Anexos
        };




        var adicionaDadosPost = topicoService.novoPost(Topico);

        adicionaDadosPost.then(function (d) {
            if (d.data !== 0) {
                //  alert("Resposta Enviada");
                //VALIDA SE EXISTE ARQUIVO 
                if (Topico2.Anexos !== undefined) {
                    var Anexos = {
                        ID_Topico: d.data,
                        ArquivoBase: Topico2.Anexos.ArquivoBase
                    };
                    entityService.saveTutorial(Anexos)
                        .then(function (data) {
                            console.log(data);
                        });
                }
                resetDados();
                location.reload();
                $scope.areaResposta = '';
                visualizarTopico(localStorage.getItem('IdTopico'));

            } else {
                alert("Resposta nao Enviada");
            }
        },
            function () {
                console.log("Erro na Resposta");
            });
    };


    //Visualizar Topico
    function visualizarTopico(topicoId) {
        var usuario = carregarIdUsuarioLogado()
        var Topico = { Id: topicoId, usuario: usuario };
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
    };


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



    //função para quebrar o texto
    $scope.mudartext = function (text) {
        if (text===1) {
            $scope.text = 2;
        } else {
            $scope.text = 1;
        }
    };

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
    };


    function resetDados() {
        $scope.areaResposta = {};
    }
});

