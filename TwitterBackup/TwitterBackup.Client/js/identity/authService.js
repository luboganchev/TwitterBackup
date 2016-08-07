(function () {
    'use strict';

    function authService($http, $q, $window, identity, authorization, baseServiceUrl, $rootScope) {
        var twitterApi = baseServiceUrl + '/api/Twitter';

        return {
            logout: function () {
                identity.setAuthorizationData(null);
            },
            isAuthorized: function () {
                var authorizationData = identity.getAuthorizationData();

                $rootScope.isAdmin = authorizationData.IsAdmin;
                $rootScope.isLoggedIn = !!authorizationData.VerifierCode;

                return $rootScope.isLoggedIn;
            },
            authorize: function () {
                return $http.get(twitterApi + '/Authorize', {
                    withCredentials: true,
                    params: {
                        redirectUrl: location.protocol + '//' + location.host + '/authComplete.html'
                    }
                }).then(function (response) {
                    identity.setAuthorizationData(JSON.parse(response.data));
                });
            },
            authenticate: function(){
                return $http.get(twitterApi + '/VerifyUser');
            },
            isAdmin: function () {
                var authorizationData = identity.getAuthorizationData();

                $rootScope.isAdmin = authorizationData.IsAdmin;

                return $rootScope.isAdmin;
            }
        };
    }

    angular.module('myApp.services')
        .factory('authService', ['$http', '$q', '$window', 'identity', 'authorization', 'baseServiceUrl', '$rootScope', authService]);
}());