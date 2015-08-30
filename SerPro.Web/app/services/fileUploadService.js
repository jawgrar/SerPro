'use strict';
app.factory('fileUploadService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    //var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var fileUploadServiceFactory = {};

    var _saveFile = function (ProductInfo) {
        return saveModel(ProductInfo);
    };

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var getModelAsFormData = function (data) {
        var dataAsFormData = new FormData();
        angular.forEach(data, function (value, key) {
            dataAsFormData.append(key, value);
        });
        return dataAsFormData;
    };

    var saveModel = function (data) {
        var deferred = $http.defer();
        $http({
            url: serviceBase + 'api/orders',
            method: "POST",
            data: getModelAsFormData(data),
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (result) {
            deferred.resolve(result);
        }).error(function (result, status) {
            deferred.reject(status);
        });
        return deferred.promise;
    };

    fileUploadServiceFactory.saveFileService = _saveFile;

    return fileUploadServiceFactory;
}]);