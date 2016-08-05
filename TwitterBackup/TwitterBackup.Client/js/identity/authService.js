(function () {
    'use strict';

    function authService($http, $q, $window, identity, authorization, baseServiceUrl) {
        var twitterApi = baseServiceUrl + '/api/Twitter';

        return {
            logout: function () {
                identity.setAuthorizationData(null);
            },
            isAuthorized: function () {
                var authorizationData = identity.getAuthorizationData();

                return !!authorizationData.VerifierCode;
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
            externalAuthData: {
                provider: "",
                userName: "",
                externalAccessToken: ""
            },
            authentication: {
                isAuth: false,
                userName: ""
            }
        };
    }

    angular.module('myApp.services')
        .factory('authService', ['$http', '$q', '$window', 'identity', 'authorization', 'baseServiceUrl', authService]);
}());