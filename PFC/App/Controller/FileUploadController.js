var isOnGitHub = window.location.hostname === 'blueimp.github.io',
    url = isOnGitHub ? '//jquery-file-upload.appspot.com/' : 'server/php/';
MeHelp.controller('DemoFileUploadController', [
    '$scope', '$http', '$filter', '$window',
    function ($scope, $http) {
        $scope.options = {
            url: url
        };
        if (!isOnGitHub) {
            $scope.loadingFiles = true;
            $http.get(url)
                .then(
                    function (response) {
                        $scope.loadingFiles = false;
                        $scope.queue = response.data.files || [];
                    },
                    function () {
                        $scope.loadingFiles = false;
                    }
                );
        }
    }
])

