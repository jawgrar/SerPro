'use strict';
app.controller('indexController', ['$scope', '$cookies', '$location', 'authService', '$translate', function ($scope, $cookies, $location, authService, $translate) {

    $scope.setLanguage = setLanguage;

    function setLanguage(lang) {
        $cookies.__APPLICATION_LANGUAGE = lang;
        $translate.use(lang);
    }

    function init() {
        var lang = $cookies.__APPLICATION_LANGUAGE || 'en';
        setLanguage(lang);
    }

    init();

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;

}]);