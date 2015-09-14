
var app = angular.module('AngularAuthApp', ['ngResource', 'ngCookies', 'ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'pascalprecht.translate']);

app.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {

    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
    }).when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    }).when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    }).when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    }).when('/FileUpload', {
        templateUrl: 'app/fileUpload/photos.html',
        controller: 'photos',
        controllerAs: 'vm',
        caseInsensitiveMatch: true
    }).when('/landing', {
        templateUrl: 'app/views/landing.html',
        controller: 'photos',
        controllerAs: 'vm',
        caseInsensitiveMatch: true
    }).when("/product", {
        templateUrl: "app/views/product.html",
        controller: "productController"
    }).when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    }).when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    }).when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    }).when("/productlist", {
        controller: "productController",
        templateUrl: "/app/views/productlist.html"
    });

    //$routeProvider.otherwise({ redirectTo: "/home" });

    $locationProvider.html5Mode(true);


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


