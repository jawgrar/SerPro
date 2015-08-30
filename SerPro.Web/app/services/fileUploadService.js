"use strict";
(function () {
    angular.module("AngularAuthApp")
           .factory("fileUploadService", ["akFileUploaderService", function (akFileUploaderService) {
               var saveFileData = function (tutorial) {
                   return akFileUploaderService.saveModel(tutorial, "http://localhost:3846/api/FileUpload/saveFile");
               };
               return {
                   saveTutorial: saveTutorial
               };
           }]);
})();