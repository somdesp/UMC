// Avaliação Service
MeHelp.service('avaliacaoService', function ($http) {

    this.enviarAvaliacao = function (TopicosSelc) {
        var request = $http({
            method: 'post',
            url: '/Avaliacao/AvaliacaoPontos',
            data: TopicosSelc
        });
        return request;

    };


    this.enviarEstrela = function(avaliacao) {
        var request = $http({
            method: 'post',
            url: '/Avaliacao/AvaliacaoPontos',
            data: avaliacao
        });
        return request;
    };

    this.enviarLikeDeslike = function(avaliacao) {
        var request = $http({
            method: 'post',
            url: '/Avaliacao/AvaliacaoPontosDeslike',
            data: avaliacao
        });
        return request;
    };


    this.apagarNota = function(avaliacao) {
        var request = $http({
            method: 'post',
            url: '/Avaliacao/ApagarAvaliacao',
            data: avaliacao
        });
        return request;
    };

    this.consultarAvaliacao = function(avaliacao) {
        var request = $http({
            method: 'post',
            url: '/Avaliacao/consultarAvaliacao',
            data: avaliacao
        });
        return request;
    };

    this.FechaTopico = function (topico) {
        var request = $http({
            method: 'post',
            url: '/api/TopicoAPI',
            data: topico
        });
        return request;
    };


});