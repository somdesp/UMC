// Controle Usuarios
MeHelp.controller('amizadeCtrl', function($scope, usuarioService) {
    //---------------Visualização de perfil de outros usuarios---------------------------------//
    var Usuario = localStorage.getItem("PerfilUsuario");

    visualizarPerfil(Usuario);

    //Visualizar perfil
    function visualizarPerfil(Usuario) {
        var Perfil = usuarioService.visualizarPerfil(Usuario);

        Perfil.then(function (d) {
                if (d.data !== null) {
                    d.data.DataCad = converteDataHora(d.data.DataCad);
                    $scope.PerfilUsuario = d.data;

                } else {
                    alert("Pergunta nao Adicionado");
                }
            },
            function () {
                console.log("Erro ao cadastrar");
            });
        return Perfil;
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