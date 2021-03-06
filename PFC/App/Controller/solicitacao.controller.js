﻿// Controle Usuarios
MeHelp.controller('amizadeCtrl', function ($scope, amizadeService, usuarioService) {
    //---------------Visualização de perfil de outros usuarios---------------------------------//
    var UsuarioSolicitado = JSON.parse(localStorage.getItem("PerfilUsuario"));
    var UsuarioLogado = JSON.parse(localStorage.getItem('model'));

    // Notificaçao de acerto ou erro

    $scope.notifications = {};
    $scope.notificationsErro = {};
    var indexAcerto = 0;
    var indexErro = 0;
    $scope.max = 4;
    visualizarPerfil(UsuarioSolicitado);
    ValidaAmizade(UsuarioLogado, UsuarioSolicitado);

    //Visualizar perfil


    function visualizarPerfil(UsuarioSolicitado) {
        var Perfil = amizadeService.visualizarPerfil(UsuarioSolicitado);

        Perfil.then(function (d) {
            if (d.data !== null) {
                d.data.DataCad = converteDataHora(d.data.DataCad);
                $scope.PerfilUsuario = d.data;
                carregarCamposEditar($scope.PerfilUsuario);
            }
        },
            function () {
                console.log("Erro ao visualizarPerfil");
            });
        return Perfil;
    };



    function carregarCamposEditar(PerfilUsuario) {
        carregarCursos();
        carregarGenero();
        carregarSemestre(PerfilUsuario);
        var data = new Date(converteDataHora(PerfilUsuario.DataNasci));
        $scope.usuario = PerfilUsuario;
        $scope.usuario.DataNasci = data;
        /*$scope.Nome = PerfilUsuario.Nome;
        $scope.Email = PerfilUsuario.Email;
        $scope.RGM = PerfilUsuario.RGM;
        $scope.dataNasci = data;
        $scope.cmbGenero = PerfilUsuario.Sexo.Id;
        $scope.cmbCurso = PerfilUsuario.Curso.Id;
        $scope.selSemestre = PerfilUsuario.Semestre.Id;*/

    };

    //Editar Usuario
    //Função para notificar usuario
    function notificarUsuario(statusnotificacao) {
        if (statusnotificacao) {
            indexAcerto++;
            $scope.notifications[indexAcerto] = 'Notificacao';
        }
        else {
            indexErro++;
            $scope.notificationsErro[indexErro] = 'notificacaoErro';
        }
    }



    $scope.EnviarPerfil = function (usuario) {
        var statusnotificar = false;
        var respostaUsuario = usuarioService.atualizarUsuario(usuario);
        respostaUsuario.then(function (response) {
            if (response.data === true) {
                statusnotificar = true;
                notificarUsuario(statusnotificar);                
                $('#editarPerfil').modal('hide');
            } else {
                statusnotificar = false;
                notificarUsuario(statusnotificar);             
             }
        });
    };

    $('#editarPerfil').on('hidden.bs.modal', function () {
        visualizarPerfil(UsuarioLogado);
        $('#editarPerfil').modal('hide');
    });


    //Listar Cursos
    function carregarCursos() {
        var listarCursos = usuarioService.getTodosCursos();
        listarCursos.then(function (d) {
            $scope.Curso = d.data;
        },
            function () {
                console.log("Erro ao carregar Cursos");
            });

    };

    //Listar Semestre 
    function carregarSemestre(usuario) {
        var curso = usuario.Curso.Id;
        var listarSemestres = usuarioService.getSemestre(curso);
        listarSemestres.then(function (d) {
            $scope.Semestre = d.data;
        },
            function () {
                console("Erro ao carregar Semestre");
            });

    }


    // Carregar Semestre
    $scope.carregarSemestre = function (curso) {
         var listarSemestre = usuarioService.getSemestre(curso);
        listarSemestre.then(function (d) {
            $scope.Semestre = d.data;
            console.log($scope.Semestre);
            console.log("Carregar CMBSemestre ok");
        },
            function () {
                console.log("Erro ao Carregar CMBSemestre");
            });

    };

    // Carregar Genero 
    function carregarGenero() {
        var listarGenero = usuarioService.getGenero();
        listarGenero.then(function (d) {
            $scope.Genero = d.data;
        },
            function () {
                console.log("Erro ao carregar Generos");
            });

    };



    function converteDataHora(data) {
        var arrayMes = new Array(12);
        arrayMes[0] = "01";
        arrayMes[1] = "02";
        arrayMes[2] = "03";
        arrayMes[3] = "04";
        arrayMes[4] = "05";
        arrayMes[5] = "06";
        arrayMes[6] = "07";
        arrayMes[7] = "08";
        arrayMes[8] = "09";
        arrayMes[9] = "10";
        arrayMes[10] = "11";
        arrayMes[11] = "12";



        var dataReplace = data.toString().replace(/\/Date\((-?\d+)\)\//, '$1');
        var conversao = new Date(parseInt(dataReplace));
        return conversao.getFullYear() + "-" + arrayMes[conversao.getUTCMonth()] + "-" + conversao.getDate();
    }

    //Força da senha

    $scope.verificarForca = function (forcaSenha) {
        
        if (forcaSenha === 1) {
            $scope.type = 'danger';
            $scope.dynamic = forcaSenha;
        } else if (forcaSenha === 2) {
            $scope.type = 'warning';
            $scope.dynamic = forcaSenha;
        } else if (forcaSenha === 3) {
            $scope.type = 'info';
            $scope.dynamic = forcaSenha;
        } else if (forcaSenha === 4) {
            $scope.type = 'sucess';
            $scope.dynamic = forcaSenha;
        } else {
            $scope.dynamic = forcaSenha;
        }
        
    };
         
    $scope.alterarSenha = function () {
        var usuario = {};
        usuario.Senha = $scope.senha;
        var result = usuarioService.AlterarSenha(usuario);
        result.then(function (response) {
            if (response.data === true) {
                statusnotificar = true;
                notificarUsuario(statusnotificar);
                $('#loginSenha').modal('hide');
            } else {
                statusnotificar = false;
                notificarUsuario(statusnotificar);
            }
        });
    };







    //Solicitação Amizade
    $scope.conviteAmizade = function (Usu_Sol) {

        var Solicitacao = {
            usuario: UsuarioLogado,
            usuarioSolicitado: Usu_Sol
        };

        var amizadeSolicitada = amizadeService.amizadeSolicitada(Solicitacao);

        amizadeSolicitada.then(function (d) {
            if (d.data === true) {
                alert("Solicitação enviada");
                $scope.valAmi = true;

            } else {
                alert("Solicitação Nao enviada");
            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };



    //Aceitar Amizade
    $scope.aceitarAmizade = function (usuarios) {

        var aceitarAmizade = amizadeService.AceitaAmizade(usuarios);

        aceitarAmizade.then(function (d) {
            if (d.data === true) {
                alert("Solicitação Aceita");
                ValidaAmizade(UsuarioLogado, UsuarioSolicitado);


            } else {
                alert("Solicitação Nao enviada");
            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };

    //Cancela Amizade
    $scope.cancelaAmizade = function (usuarios) {

        var Solicitacao = {
            usuario: UsuarioLogado,
            usuarioSolicitado: usuarios
        };

        var cancelaAmizade = amizadeService.CancelaAmizade(Solicitacao);




        cancelaAmizade.then(function (d) {
            if (d.data === true) {
                alert("Amizade Cancelada");
                ValidaAmizade(UsuarioLogado, UsuarioSolicitado);

            } else {
                alert("Amizade Nao Cancelada");
            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };

    //Valida se ja tem Amizade
    function ValidaAmizade(UsuarioLogado, UsuarioSolicitadoForm) {

        var Amizade = {
            usuario: UsuarioLogado,
            usuarioSolicitado: UsuarioSolicitadoForm
        };

        $scope.ModelUsuario = JSON.parse(localStorage.getItem('model'));
        var ValidaAmizade = amizadeService.ValidaAmizade(Amizade);
        ValidaAmizade.then(function (d) {
            $scope.valAmi = d.data;
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };

    function converteDataHora(data) {
        var arrayMes = new Array(12);
        arrayMes[0] = "01";
        arrayMes[1] = "02";
        arrayMes[2] = "03";
        arrayMes[3] = "04";
        arrayMes[4] = "05";
        arrayMes[5] = "06";
        arrayMes[6] = "07";
        arrayMes[7] = "08";
        arrayMes[8] = "09";
        arrayMes[9] = "10";
        arrayMes[10] = "11";
        arrayMes[11] = "12";



        var dataReplace = data.toString().replace(/\/Date\((-?\d+)\)\//, '$1');
        var conversao = new Date(parseInt(dataReplace));
        return conversao.getFullYear() + "," + arrayMes[conversao.getUTCMonth()] + "," + conversao.getDate();
    };



    //Editar formulario usuario perfil
    $scope.botaoEditar = function () {
        $(".botao").show();
    };

    $scope.botaoEditarDesaparecer = function () {
        $(".botao").hide();
    };

    $scope.clickBotaoEditar = function () {
        // $("#nome").remove();
        $(".form-group").append("<input type='text' name='form'/>");
    };

})