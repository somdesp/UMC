//Serviços do Topico
MeHelp.service('topicoService', function ($http,$q) {

    //Servico de listar os Topicos abertos
    this.getTodosTopicos = function () {
        return $http.get("/Topico/ListarTopico");
    };
    //getStatusTopico
    this.getStatusTopico = function () {
        return $http.post("../api/TopicoAPI/GetStatusTopico");
    };
    //Servico por Novo Topico
    this.novoTopico = function (topico) {
        var request = $http({
            method: 'post',
            url: '/Topico/AdicionarTopico',
            data: topico
        });

        return request;
    };

    //alterarTopico
    this.alterarTopico = function (topico) {
        var request = $http({
            method: 'post',
            url: '../api/TopicoAPI/AlterarTopico',
            data: topico
        });

        return request;
    };

    //Servico Pesquisa no gerenciamento
    this.getTopicosPesquisa = function (topico) {
        var request = $http({
            method: 'post',
            url: '../api/TopicoAPI/GetTopicosPesquisa',
            data: topico
        });

        return request;
    };

    //Servico por Novo Post
    this.novoPost = function (topico) {
        var request = $http({
            method: 'post',
            url: '/Topico/AdicionarResposta',
            data: topico
        });

        return request;
    };

    //Servico de listar os Temas
    this.getTodosTemas = function () {
        return $http.get("/Tema/GetTema");
    };

    //Servico de listar os Topicos abertos
    this.visualizarTopico = function (topico) {
        var request = $http({
            method: 'post',
            url: '/Topico/TopicoSelecionadoJson',
            data: topico
        });
        return request;
    };

    //ExcluirTopicoUsuario
    this.ExcluirTopicoUsuario = function (topico) {
        var request = $http({
            method: 'post',
            url: '../api/TopicoAPI/ExcluirTopicoUsuario',
            data: topico
        });
        return request;
    };

    // descobrir o id do Usuario
    this.idUsuarioatual = function () {
        var request = $http({
            method: 'post',
            url: '/Topico/verificarIdUsuario',
            data: '{}'
        });
        return request;
    };

    this.pesquisar = function (pesquisa) {
        var deffered = $q.defer();
        var request = $http({
            method: 'post',
            url: '/Topico/ListarTopicoPesquisa',
            data: JSON.stringify({ pesquisa })
        });
        deffered.resolve(request);
        return deffered.promise;
    };

    //Fecha topico apos votação
    this.FechaTopico = function(topico) {
        var request = $http({
            method: 'post',
            url: '../api/TopicoAPI/FechaTopico',
            data: topico
        });
        return request;
    };

    ////Envia Email apos fechar topico
    //this.FechaTopico = function (topico) {
    //    var request = $http({
    //        method: 'post',
    //        url: '../api/TopicoAPI/EnviaEmailTopicoFechado',
    //        data: topico
    //    });
    //    return request;
    //};


});