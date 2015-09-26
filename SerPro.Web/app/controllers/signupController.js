'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', '$translate', function ($scope, $location, $timeout, authService, $translate) {


    authService.authentication.issignupPage = true;

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

    $scope.loginData = {
        userName: "",
        password: ""
    };

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
            $scope.message = "User has been registered successfully, you will be redicted to home page in 2 seconds.";
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

            $scope.loginData.userName = $scope.registration.userName;
            $scope.loginData.password = $scope.registration.password;

            $timeout.cancel(timer);
            authService.login($scope.loginData).then(function (response) {

                //if (response.level == '1') {
                //    $location.path('/product');
                //} else {
                //    $location.path('/productlist');
                //}
                $location.path('/homePage');
            },
              function (err) {
                  $scope.message = err.error_description;
              });
        }, 2000);
    }

}]);