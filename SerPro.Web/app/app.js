
var app = angular.module('AngularAuthApp', ['ngResource', 'ngCookies', 'ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'pascalprecht.translate']);

app.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.when("/homePage", {
        controller: "productController",
        templateUrl: "/app/views/homePage.html"
    });

    $routeProvider.when("/product", {
        templateUrl: "app/views/product.html",
        controller: "productController"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

}]);

var serviceBase = 'http://localhost:3846/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});


app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.config(['$translateProvider',
        function ($translateProvider) {
            $translateProvider.useUrlLoader('http://localhost:3846/api/translations/Get');
        }]);

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


