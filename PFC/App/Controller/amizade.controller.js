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

        var amizadeSolicitada = amizadeService.amizadeSolicitada(UsuarioLogado, Usu_Sol);

        amizadeSolicitada.then(function (d) {
            if (d.data === true) {
                alert("Solicitação enviada");
            } else {
                alert("Solicitação Nao enviada");
            }
        },
            function () {
                $("#resposta").text("Error Critico");
            });
    };

    //Valida se ja tem Amizade
    function ValidaAmizade(UsuarioLogado, UsuarioSolicitado) {

        var ValidaAmizade = amizadeService.ValidaAmizade(UsuarioLogado, UsuarioSolicitado);

        ValidaAmizade.then(function (d) {
            if (d.data === true) {
                $scope.valAmi = true;
            } else {
                $scope.valAmi = false;

            }
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

})