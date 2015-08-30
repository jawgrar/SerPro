"use strict";
(function () {
    angular.module("AngularAuthApp")
           .controller("fileUpload", ["$scope", "fileUploadService",
               function ($scope, fileUploadService) {
                   $scope.saveFile = function (tutorial) {
                       fileUploadService.saveFileData(tutorial)
                                    .then(function (data) {
                                        console.log(data);
                                    });
                   };
               }]);
})();