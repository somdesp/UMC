MeHelp.controller('MainController', function ($scope, $firebaseArray) {
    var ref = new Firebase("https://mehelp-tcc-umc.firebaseio.com/");

    $scope.data = $firebaseArray(ref);
    $scope.addMessage = function (e) {
        var ModelUsuario = JSON.parse(localStorage.getItem('model'));

        //LISTEN FOR RETURN KEY
        if (e.keyCode === 13 && $scope.msg) {
            //ALLOW CUSTOM OR ANONYMOUS USER NAMES
            var name = ModelUsuario.Nome;
            $scope.data.$add({ from: name, body: $scope.msg });
            //RESET MESSAGE
            $scope.msg = "";
        }
    }
});