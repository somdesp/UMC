// Controle Usuarios
angular.module('MeHelp').controller('TopicosControle', function ($rootScope, $location, $ocLazyLoad) {

    $rootScope.activetab = $location.path();

    $ocLazyLoad.load('../../Scripts/ocLazyLoad.js');
    $ocLazyLoad.load('../../Scripts/angular.js');
    $ocLazyLoad.load('../../Scripts/App/Produtos.js');


});
