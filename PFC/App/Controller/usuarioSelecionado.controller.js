MeHelp.controller('usuarioSelecionadoCtrl', function ($scope, usuarioService) {

    carregarUsuarioID(JSON.parse(localStorage.getItem('model')));
    var ModelUsuario = JSON.parse(localStorage.getItem('model'));
    var url_atual = window.location.href;


    //Valida se tem dados na localStorage  
    if (ModelUsuario === null) {
        //

    } else {
        document.getElementById("NomeUsuario").innerHTML = ModelUsuario.Nome;
        document.getElementById("AuthUsuario").innerHTML = ModelUsuario.Auth.Permissao;
    }

    //Função chamada quando carrega a modal do editar.
    function carregarUsuarioID(usuario) {

        var BuscUsuariUni = usuarioService.ConsultaUnicoUsuario(usuario);

        BuscUsuariUni.then(function (d) {
            if (d.data.sucess !== null) {
                carregarCampos(d.data);
                localStorage.setItem('model', JSON.stringify(d.data));
            } else {
                alert("Vazio");
            }
        },
            function () {
                console.log("Erro ao Carregar Campos");
            });
    }

    function carregarCampos(usuario) {
        var result = converteDataHora(usuario.DataNasci)

        var data = new Date(converteDataHora(usuario.DataNasci));
        carregarSemestre(usuario);
        carregarCursos();
        carregarGenero();
        $scope.id = usuario.Id;
        $scope.nome = usuario.Nome;
        $scope.email = usuario.Email;
        $scope.login = usuario.Login;
        $scope.dataNasci = data// $filter('date')(data, 'yyyy/dd/MM');
        $scope.rgm = usuario.RGM;
        $scope.cmbGenero = usuario.Sexo.Id;
        $scope.cmbCurso = usuario.Curso.Id;
        $scope.selSemestre = usuario.Semestre.Id;

    }

    //Atualizar Usuario
    $scope.atualizarUsuario = function () {
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
            Semestre: { Id: $scope.selSemestre }
        };

        var UpdateUser = usuarioService.atualizarUsuario(usuario);

        UpdateUser.then(function (d) {
            if (d.data === true) {
                alert("Usuario Atualizado");
                carregarUsuarioID(JSON.parse(localStorage.getItem('model')));
                window.location.href = url_atual;
            } else {
                alert("Usuario nao Atualizado");
            }
        },
            function () {
                console.log("Erro ao Atualizado");
            });
    }

    //correção datas
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

    //Listar Cursos
    function carregarCursos() {
        var listarCursos = usuarioService.getTodosCursos();
        listarCursos.then(function (d) {
            $scope.Curso = d.data;
        },
            function () {
                console.log("Erro ao carregar Cursos");
            });

    }

    //Listar Semestre 
    function carregarSemestre(usuario) {
        var curso = usuario.Curso.Id
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






});

