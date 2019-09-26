// Controle Usuarios
MeHelp.controller('usuarioCtrl', function ($scope, usuarioService, toaster) {

    carregarUsuarios();
    carregarCursos();
    carregarGenero();
    carregarPermissoes();
    carregarUsuarioID(JSON.parse(localStorage.getItem('model')));
    try {
        var ModelUsuario = JSON.parse(localStorage.getItem('model'), (key, value));
    } catch (ex) {

    }
   

    //Valida se tem dados na localStorage  
    if (ModelUsuario === null || ModelUsuario === undefined) {
        //

    } else {
        document.getElementById("NomeUsuario").innerHTML = ModelUsuario.Nome;
        document.getElementById("AuthUsuario").innerHTML = ModelUsuario.Auth.Permissao;
    };


    //Listar Usuarios
    function carregarUsuarios() {
        var listarUsuarios = usuarioService.getTodosUsuarios();
        listarUsuarios.then(function (d) {
            $scope.Usuarios = d.data;
        },
            function () {
                console.log("Erro ao carregar a lista de usuario");
            });
    };

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
    function carregarSemestre() {
        var listarSemestres = usuarioService.getTodosSemestres();
        listarSemestres.then(function (d) {
            $scope.Semestre = d.data;
        },
            function () {
                console.log("Erro ao carregar Semestre");
            });

    };


    // Carregar Semestre
    $scope.carregarSemestre = function (curso) {
        curso = $scope.cmbCurso;
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

    // Carregar Permissoes 
    function carregarPermissoes() {
        var listarPermissoes = usuarioService.getPermissoes();
        listarPermissoes.then(function (d) {
            $scope.Permissoes = d.data;
        },
            function () {
                console.log("Erro ao carregar Permissoes");
            });

    };



    //Cadastrar Usuario
    $scope.cadastrarUsuario = function () {

        var usuario = {
            Nome: $scope.nome,
            Email: $scope.email,
            Login: $scope.login,
            Senha: $scope.senha,
            DataNasci: $scope.dataNasci,
            Sexo: { Id: $scope.cmbGenero },
            RGM: $scope.rgm,
            Curso: { Id: $scope.cmbCurso },
            Semestre: { Id: $scope.cmbSemestre }
        };

        toaster.pop('wait', "", "Cadastrando usuario!!");

        var adicionaDadosUsu = usuarioService.adicionarUsuario(usuario);

        adicionaDadosUsu.then(function (d) {
            if (d.data.success === true) {
                toaster.clear();
                toaster.pop('success', "", "Usuario cadastrado", 3000);

                $scope.nome = "";
                $scope.email = "";
                $scope.login = "";
                $scope.senha = "";
                $scope.dataNasci = "";
                $scope.cmbGenero = "";
                $scope.rgm = "";
                $scope.cmbCurso = "";
                $scope.cmbSemestre = "";
            } else {
                toaster.clear();
                toaster.pop('error', "", "Usuario não cadastrado", 3000);
           
            };
        },
            function () {
                toaster.clear();
                toaster.pop('error', "", "Usuario não cadastrado", 3000);     
            });
    };

    //Carregar Modal atualizarUsuario

    $scope.atualizarUsuarioID = function (usuario) {
        carregarSemestre();
        $scope.id = usuario.Id;
        $scope.nome = usuario.Nome;
        $scope.email = usuario.Email;
        $scope.login = usuario.Login;
        $scope.dataNasci = usuario.DataNasci;
        $scope.rgm = usuario.RGM;
        $scope.cmbGenero = usuario.Sexo.Id;
        $scope.cmbCurso = usuario.Curso.Id;
        $scope.cmbSemestre = usuario.Semestre.Id;
        $scope.cmbPermissoes = usuario.Auth.Id;

    };

    //Função chamada quando carrega a modal do editar.
    function carregarUsuarioID(usuario) {

        var BuscUsuariUni = usuarioService.ConsultaUnicoUsuario(usuario);

        BuscUsuariUni.then(function (d) {
            if (d.data === "") {
                localStorage.setItem('model', null);
            } else {
                localStorage.setItem('model', JSON.stringify(d.data));

            }


        },
            function () {
                alert("Erro ao Carregar Campos");
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

    //Atualizar Usuario
    $scope.atualizarUsuario = function () {
        toaster.pop('wait', "", "Atualizando usuario!!");

        var usuario = {
            Id: $scope.id,
            Nome: $scope.nome,
            Email: $scope.email,
            Login: $scope.login,
            Senha: $scope.senha,
            DataNasci: $scope.dataNasci,
            Sexo: { Id: $scope.cmbGenero },
            RGM: $scope.rgm,
            Curso: { Id: $scope.cmbCurso },
            Semestre: { Id: $scope.cmbSemestre },
            Auth: { Id: $scope.cmbPermissoes }

        };

        var UpdateUser = usuarioService.atualizarUsuario(usuario);

        UpdateUser.then(function (d) {
            if (d.data === true) {           
                toaster.clear();
                toaster.pop('success', "", "Usuario atualizado!!",3000);
                carregarUsuarios();
            } else {
                toaster.clear();
                toaster.pop('error', "", "Usuario não atualizado!!", 3000);
            }
        },
            function () {
                alert("Erro ao Atualizado");
            });
    };

    //Inativar Usuario
    $scope.inativarUsuario = function (usuario) {
        toaster.pop('wait', "", "Inativando usuario!!");

        var btnVal = confirm("Deseja Inativar o Usuario?");

        if (btnVal === true) {
            var inativarDadosUsuario = usuarioService.inativarUsuario(usuario);
            inativarDadosUsuario.then(function (d) {
                if (d.data === true) {
                    toaster.clear();
                    toaster.pop('success', "", "Usuario inativado!!", 3000);
                    carregarUsuarios();
                }

            },
                function () {
                    toaster.clear();
                    toaster.pop('error', "", "Erro ao inativar!!", 3000);
                });
        };
    };


    //pesquisar usuarios javascript controller
    $scope.pesquisarUsuario = function (pesquisaUsuario) {
        var resultPesquisa = usuarioService.pesquiUsuario(pesquisaUsuario);
        resultPesquisa.then(function (d) {
            $scope.pesquisaUsu = d.data;

        });
    };


});

jQuery(document).ready(function () {
    $(window).scroll(function () {
        set = $(document).scrollTop() + "px";
        jQuery('#float-banner').animate(
            { top: set },
            { duration: 1000, queue: false }
        );
    });
});