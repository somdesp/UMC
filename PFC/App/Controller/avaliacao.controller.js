MeHelp.controller('avaliacao.controller', function ($scope, toaster, avaliacaoService, topicoService) {
    var contadorLike = 0;
    var contadorDesLike = 0;
    var contadorEstrela = 0;
    var sinalizador = 0;
    var avaliacao = {
        usuario: {},
        topico: {}
    };

    

    $scope.inicio = function (deslike, like) {
                   
            $scope.contarDesLike = deslike;
            $scope.contarLike = like;
        
    };



    $scope.dicas = "Clique uma vez para salvar sua nota.";
    $scope.isReadonly = false;

    $scope.avaliacao = function (botao) {


        var resultpoints = avaliacaoService.enviarAvaliacao(botao);
        resultpoints.then(function (d) {
            if (d.data !== null) {
                $scope.isReadonly = d.data;
            }
        });
    };


    $scope.valorpegoLike = function (topico, usuario) {
        // Botão like
        contadorLike += 1;
        contadorDesLike = 0;

        if (contadorLike === 1) {
            $scope.TopicosSelc.avaliacao.pontos = 1;
            avaliacao.idTopico = topico.Id;
            avaliacao.idUsuario = usuario.Id;
            avaliacao.pontos = 1;

            var resultpoints = avaliacaoService.enviarLikeDeslike(avaliacao);
            resultpoints.then(function (d) {
                if (d.data !== null) {

                    $scope.TopicosSelc.avaliacao.contarLike = d.data.contarLike;
                    $scope.TopicosSelc.avaliacao.contarDeslike = d.data.contarDeslike;
                    //$scope.verificarmedia = true;
                }
            });
        } else if (contadorLike === 2) {
            $scope.TopicosSelc.avaliacao.pontos = 0;
            avaliacao.idTopico = topico.Id;
            avaliacao.idUsuario = usuario.Id;
            avaliacao.pontos = 0;
            contadorLike = 0;
            var resultpoints = avaliacaoService.enviarLikeDeslike(avaliacao);
            resultpoints.then(function (d) {
                if (d.data !== null) {

                    $scope.TopicosSelc.avaliacao.contarLike = d.data.contarLike;
                    $scope.verificarmedia = false;
                }
            });
        };

    };

    $scope.valorpegoDeslike = function (topico, usuario) {
        contadorLike = 0;
        contadorDesLike += 1;

        if (contadorDesLike === 1) {
            $scope.TopicosSelc.avaliacao.pontos = -1;
            avaliacao.idTopico = topico.Id;
            avaliacao.idUsuario = usuario.Id;
            avaliacao.pontos = -1;

            var resultpoints = avaliacaoService.enviarLikeDeslike(avaliacao);
            resultpoints.then(function (d) {
                if (d.data !== null) {

                    $scope.TopicosSelc.avaliacao.contarLike = d.data.contarLike;
                    $scope.TopicosSelc.avaliacao.contarDeslike = d.data.contarDeslike;
                    $scope.verificarmedia = true;
                }
            });
        } else if (contadorDesLike === 2) {
            $scope.TopicosSelc.avaliacao.pontos = 0;
            avaliacao.idTopico = topico.Id;
            avaliacao.idUsuario = usuario.Id;
            avaliacao.pontos = 0;
            contadorDesLike = 0;

            var resultpoints = avaliacaoService.enviarLikeDeslike(avaliacao);
            resultpoints.then(function (d) {
                if (d.data !== null) {
                    $scope.idAvaliacaoLike = d.data.idAvaliacao;
                    $scope.TopicosSelc.avaliacao.contarDeslike = d.data.contarDeslike;
                    $scope.verificarmedia = false;
                }
            });

        };

    };

    $scope.valorpegoResposta = function (TopicosSelc) {
        contadorLike = 0;
        contadorDesLike += 1;

        if (contadorDesLike === 1) {
            $scope.TopicosSelc.pontos = -1;
            var resultpoints = avaliacaoService.enviarAvaliacao(TopicosSelc);
            resultpoints.then(function (d) {
                //if (d.data !== null) {

                //};
            });
        };

    };

    $scope.valorClick = function (rating, topico, usuario, TopicosSelc) {
        avaliacao.idTopico = topico.Id;
        avaliacao.idUsuario = usuario.Id;
        avaliacao.pontos = rating;

        var consultarequisicao = avaliacaoService.consultarAvaliacao(avaliacao);
        consultarequisicao.then(function (d) {
            if (d.data.pontos > 0) {
                sinalizador = 1;
                avaliacao.idTopico = topico.Id;
                avaliacao.idUsuario = usuario.Id;

                $scope.dicas = "Clique uma vez para salvar sua nota.";
                $scope.ctrl.rating = 0; //Envio da média efetuar depois
                $scope.ctrl.readOnly = false;
                $scope.nota = 0;
                sinalizador = 0;
                var requisitar = avaliacaoService.apagarNota(avaliacao);
                requisitar.then(function (d) {

                });

            } else {

                $scope.ctrl.readOnly = true;
                //FUNÇÃO FECHA O TOPICO
                if (TopicosSelc.usuario.Id === usuario.Id && rating === 5) {

                    alert('Nota Maxima escolhida.');
                    var r = confirm("Deseja Fechar o Topico ?");
                    if (r == true) {
                        var requisitar = avaliacaoService.enviarEstrela(avaliacao);
                        requisitar.then(function (d) {
                            $scope.ctrl.rating = d.data.mediaPontos;
                            $scope.nota = d.data.pontos;
                            $scope.idAvaliacao = d.data.idAvaliacao;
                            sinalizador = 1;
                        });
                        toaster.pop('wait', "", "Finalizando topico!!", 2000);

                        var FechaTopico = topicoService.FechaTopico(TopicosSelc);
                        //FechaTopico.then(function (d) {
                        //    if (d.data === true) {
                        setTimeout(function () {
                            toaster.pop('success', "", "Topico fechado!!", 3000);
                        },
                            1000
                        );
                        setTimeout(function () {
                            location.reload();
                        },
                            3000
                        );
                     
                        //    };
                        //});

                    } else {
                        $scope.ctrl.rating = 0;

                    };
                } else {
                    var requisitar = avaliacaoService.enviarEstrela(avaliacao);
                    requisitar.then(function (d) {
                        $scope.ctrl.rating = d.data.pontos;
                        $scope.nota = d.data.pontos;
                        $scope.idAvaliacao = d.data.idAvaliacao;
                        sinalizador = 1;
                    });
                };
            };

        });
    };

    $scope.valorduasvezes = function (rating, topico, usuario) {

        avaliacao.idTopico = topico.Id;
        avaliacao.idUsuario = usuario.Id;

        $scope.dicas = "Clique uma vez para salvar sua nota.";
        $scope.ctrl.rating = 0; //Envio da média efetuar depois
        var requisitar = avaliacaoService.apagarNota(avaliacao);
        requisitar.then(function (d) {

        });
        $scope.ctrl.readOnly = false;
        sinalizador = false;
    };
});