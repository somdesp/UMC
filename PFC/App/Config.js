MeHelp.config(function ($routeProvider) {

    // indica que quando tiver uma URL do tipo http://localhost/[id], que deve utilizar este template e controlador
    $routeProvider.when('Topico/index/:id', {

        templateUrl: 'Views/Topico.html',
        controller: 'topicoSelecionadoCtrl' // nome do seu controlador
    });



});

MeHelp.config([
    '$httpProvider', 'fileUploadProvider',
    function ($httpProvider, fileUploadProvider) {
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
        fileUploadProvider.defaults.redirect = window.location.href.replace(
            /\/[^\/]*$/,
            '/cors/result.html?%s'
        );
        if (isOnGitHub) {
            // Demo settings:
            angular.extend(fileUploadProvider.defaults, {
                // Enable image resizing, except for Android and Opera,
                // which actually support image resizing, but fail to
                // send Blob objects via XHR requests:
                disableImageResize: /Android(?!.*Chrome)|Opera/
                    .test(window.navigator.userAgent),
                maxFileSize: 999000,
                acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
            });
        }
    }
])