MeHelp.config(function ($routeProvider) {

    // indica que quando tiver uma URL do tipo http://localhost/[id], que deve utilizar este template e controlador
    $routeProvider.when('Topico/index/:id', {

        templateUrl: 'Views/Topico.html',
        controller: 'topicoSelecionadoCtrl' // nome do seu controlador
    });
})