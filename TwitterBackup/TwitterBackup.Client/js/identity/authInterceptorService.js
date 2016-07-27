(function () {
    'use strict';

    function authInterceptor($q, $injector, $location, authorization) {
        return {
            request: function (config) {
                config.headers = config.headers || {};

                var authToken = authorization.getAuthorizationToken();
                if (authToken) {
                    config.headers.Authorization = 'Bearer ' + authToken;
                }

                return config;
            },
            responseError : function (rejection) {
                if (rejection.status === 401) {
                    var authService = $injector.get('authService');
                    authService.logOut();
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        };
    }

    angular.module('myApp.services')
        .factory('authInterceptorService', ['$q', '$injector', '$location', 'authorization', authInterceptor]);
}());