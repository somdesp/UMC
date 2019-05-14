
//Serviços do Usuario
MeHelp.service('usuarioService', function ($http) {

    //Servico de listar os Usuarios
    this.getTodosUsuarios = function () {
        return $http.get("/Usuario/ListarUsuarios");
    };

    //Servico por adicionar usuario
    this.adicionarUsuario = function (usuario) {
        var request = $http({
            method: 'post',
            url: '/Usuario/AdicionarUsuario',
            data: usuario
        });

        return request;
    };

    this.getTodosCursos = function () {
        return $http.get("/Listas/GetCurso");
    };

    this.getTodosSemestres = function () {
        return $http.get("/Semestre/GetSemestre");
    };

    this.getGenero = function () {
        return $http.get("/Listas/GetGenero");
    };

    this.AlterarSenha = function (usuario) {
        var request = $http({
            method: 'POST',
            url: '/Usuario/AlterarSenha',
            data: usuario
        });
        return request;
    };

    this.getSemestre = function (curso) {

        var request = $http({
            method: 'POST',
            url: '/Semestre/GetSemestre',
            data: JSON.stringify({ CursoId: curso })
        });
        return request;
    };

    //Metodo para chamar dados de somente 1 único usuario
    this.ConsultaUnicoUsuario = function (usuario) {
        var request = $http({
            method: 'POST',
            url: '/Usuario/ConsultarUsuario',
            data: usuario
        });
        return request;
    }


    // Atualizar Usuario
    this.atualizarUsuario = function (usuario) {
        var request = $http({
            method: 'POST',
            url: '/Usuario/AtualizarUsuario',
            data: usuario
        });
        return request;
    }

    //Inativar Usuario
    this.inativarUsuario = function(usuario) {
        var request = $http({
            method: 'POST',
            url: '/Usuario/InativarUsuario',
            data: usuario
        });
        return request;
    };

    //pesquisar no arquivo javascript service
    this.pesquiUsuario = function (pesquisa) {
        var request = $http({
            method: 'POST',
            url: '/Usuario/PesquisarUsuario',
            data: JSON.stringify({ pesquisa: pesquisa })
        });
        return request;
    };







});


//Serviços do Tema
MeHelp.service('temaService', function ($http) {

    //Servico de listar os Temas
    this.getTodosTemas = function () {
        return $http.get("/Tema/GetTema");
    };

    //Servico por adicionar Tema
    this.adicionarTema = function (tema) {
        var request = $http({
            method: 'post',
            url: '/Tema/AdicionarTema',
            data: tema
        });

        return request;
    };

    //Serviço atualizar tema
    this.atualizarTema = function (tema) {
        var request = $http({
            method: 'POST',
            url: '/Tema/AtualizarTema',
            data: tema
        });
        return request;
    }

    this.excluirTema = function (tema) {
        var request = $http({
            method: 'POST',
            url: '/Tema/ExcluirTema',
            data: tema
        });
        return request;
    }


});



