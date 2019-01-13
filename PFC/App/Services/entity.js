"use strict";
(function () {
    angular.module("MeHelp")
           .factory("entityService", ["akFileUploaderService", function (akFileUploaderService) {
               var saveTutorial = function (Topico) {
                   return akFileUploaderService.saveModel(Topico, "/topico/AnexoArquivos");
               };
               return {
                   saveTutorial: saveTutorial
               };
           }]);
})();