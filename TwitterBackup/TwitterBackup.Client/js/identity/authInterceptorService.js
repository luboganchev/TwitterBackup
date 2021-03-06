﻿(function () {
    'use strict';

    var authorizationStarted = false;

    function authInterceptor($q, $injector, $location, $timeout, identity) {
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

                if (response.status === 403) {
                    identity.setAuthorizationData(null);

                    $timeout(function () {
                        $location.path('/rate-exceeded');
                    });
                }

                return $q.reject(response);
            }
        };
    }

    angular.module('myApp.services')
        .factory('authInterceptorService', ['$q', '$injector', '$location', '$timeout', 'identity', authInterceptor]);
}());