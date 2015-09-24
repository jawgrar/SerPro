'use strict';
app.controller('productController', ["$q", "$scope", "$http", 'ngAuthSettings',
   function ($q, $scope, $http, ngAuthSettings) {

       var serviceBase = ngAuthSettings.apiServiceBaseUri;

       $scope.productList = [];

       $scope.picture = '';

       $scope.product = {
           Name: "",
           description: "",
           Price: "",
           Picture: ""
       };

       $scope.loadPeople = function () {
           var httpRequest = $http({
               method: 'GET',
               url: serviceBase + 'api/product/GetProduct',
               data: {}

           }).success(function (data, status) {
               $scope.productList = data;
           });

       };

       $scope.loadPeople();

       $scope.saveProduct = function () {
           // var data = { 'obj': $scope.attachments }
           //var data = "file=" + $scope.attachments + "&Title=" + tutorial.Name + "&Description=" + tutorial.description;
           $scope.product.Picture = $scope.Picture;
           //$scope.tutorial.Price = "test";
           var deferred = $q.defer();
           $http.post(serviceBase + 'api/Product/post', $scope.product).success(function (response) {

           }).error(function (err, status) {
               deferred.reject(err);
           });

           return deferred.promise;
       };

       $scope.imageUpload = function (event) {
           var files = event.target.files; //FileList object

           for (var i = 0; i < files.length; i++) {
               var file = files[i];
               var reader = new FileReader();
               reader.onload = $scope.imageIsLoaded;
               reader.readAsDataURL(file);
           }
       }

       $scope.imageIsLoaded = function (e) {
           $scope.$apply(function () {
               $scope.Picture = e.target.result;
               //$('#Attachment').val(e.target.result);

               //$scope.stepsModel.push(e.target.result);
           });
       }
   }]);