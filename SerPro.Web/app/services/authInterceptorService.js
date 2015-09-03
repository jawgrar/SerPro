'use strict';
app.factory('authInterceptorService', ['$q', '$rootScope', '$injector', '$location', 'localStorageService', function ($q, $rootScope, $injector, $location, localStorageService) {


    var authInterceptorServiceFactory = {};

    $rootScope.$on('$routeChangeStart', function (event, next) {

        var userAuthenticated = ''; /* Check if the user is logged in */
        var authData = localStorageService.get('authorizationData');
        if (authData != null) {
            //if (next.$$route.originalPath == '/fileUpload') {
            if (authData.level == 1) {
                $location.path('/fileupload');
            }
            else {
                $location.path('/landing');
            }
            //}
        }

    });

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('authService');
            var authData = localStorageService.get('authorizationData');

            if (authData) {
                if (authData.useRefreshTokens) {
                    $location.path('/refresh');
                    return $q.reject(rejection);
                }
            }
            authService.logOut();
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);