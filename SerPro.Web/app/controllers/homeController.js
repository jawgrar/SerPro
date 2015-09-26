'use strict';
app.controller('homeController', ['$scope', 'authService', function ($scope, authService) {
    authService.authentication.issignupPage = false;
}]);