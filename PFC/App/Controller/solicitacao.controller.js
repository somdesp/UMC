// Controle Usuarios
MeHelp.controller('amizadeCtrl', function ($scope, amizadeService) {
    //---------------Visualização de perfil de outros usuarios---------------------------------//
    var UsuarioSolicitado = JSON.parse(localStorage.getItem("PerfilUsuario"));
    var UsuarioLogado = JSON.parse(localStorage.getItem('model'));


    visualizarPerfil(UsuarioSolicitado);
    ValidaAmizade(UsuarioLogado, UsuarioSolicitado);

    //Visualizar perfil
    function visualizarPerfil(UsuarioSolicitado) {
        var Perfil = amizadeService.visualizarPerfil(UsuarioSolicitado);

        Perfil.then(function (d) {
            if (d.data !== null) {
                d.data.DataCad = converteDataHora(d.data.DataCad);
                $scope.PerfilUsuario = d.data;
            }
        },
            function () {
                console.log("Erro ao visualizarPerfil");
            });
        return Perfil;
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

            } else {
                alert("Solicitação Nao enviada");
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