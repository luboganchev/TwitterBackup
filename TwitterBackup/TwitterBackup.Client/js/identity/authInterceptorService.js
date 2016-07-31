(function () {
    'use strict';

    var authorizationStarted = false;

    function authInterceptor($q, $injector, $location, $timeout, identity, authorization) {
        return {
            request: function (config) {
                config.headers = config.headers || {};

                var authorizationData = identity.getAuthorizationData();
                if (authorizationData.VerifierCode) {
                    config.headers.AuthorizationData = JSON.stringify(authorizationData);
                }

                return config;
            },
            responseError: function (response) {
                if (response.status === 401) {
                    identity.setAuthorizationData(null);

                    $timeout(function () {
                        $location.path('/login');
                    });
                }

                return $q.reject(response);
            }
        };
    }

    angular.module('myApp.services')
        .factory('authInterceptorService', ['$q', '$injector', '$location', '$timeout', 'identity', 'authorization', authInterceptor]);
}());