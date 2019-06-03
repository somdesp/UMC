var MeHelp;

(function () {
    'use strict'
    MeHelp = angular.module('MeHelp', [
        'ngRoute',
        'ngAnimate',
        'ui.bootstrap',
        'flash',
        'jkAngularRatingStars',
        'akFileUploader',
        'growlNotifications',
        'SignalR',
        'toaster',
        'zxcvbn'
    ]);

    MeHelp.directive('compareTo', function () {
        return {
            require: "NgModel",
            scope: {
                ConfirmarSenha: "=compareTo"
            },
            link: function (scope, element, attributes, modelVal) {
                modelVal.$validators.compareTo = function (modelValue) {
                    return modelValue === scope.ConfirmarSenha;
                };

                scope.$watch("ConfirmarSenha", function () {
                    modelVal.$validate();
                });
            }

        };
    });

})();