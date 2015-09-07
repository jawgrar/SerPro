'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', '$translate', function ($scope, $location, $timeout, authService, $translate) {

    //$scope.setLanguage = setLanguage;

    //function setLanguage(lang) {
    //    $cookies.__APPLICATION_LANGUAGE = lang;
    //    $translate.use(lang);
    //}

    //function init() {
    //    var lang = $cookies.__APPLICATION_LANGUAGE || 'en';
    //    setLanguage(lang);
    //}

    //init();

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: "",
        firstName: "",
        lastName: "",
        roleName: ""
    };

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);