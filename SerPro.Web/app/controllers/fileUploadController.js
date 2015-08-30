'use strict';
app.controller('fileUploadController', ['$scope', 'fileUploadService', function ($scope, fileUploadService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.ProductInfo = {
        title: "",
        description: "",
        attachment: ""
    };

    $scope.saveFile = function () {

        fileUploadService.saveFileService($scope.ProductInfo).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "File hase been saved successfully.";
            startTimer();

        },
    function (response) {
        var errors = [];
        for (var key in response.data.modelState) {
            for (var i = 0; i < response.data.modelState[key].length; i++) {
                errors.push(response.data.modelState[key][i]);
            }
        }
        $scope.message = "Failed to save file due to:" + errors.join(' ');
    });
    };
}]);