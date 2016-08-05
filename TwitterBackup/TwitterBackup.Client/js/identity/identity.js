(function () {
    'use strict';

    function identity() {
        var twitterAuthorizationName = 'twitter-authorization';

        return {
            getAuthorizationData: function () {
                var authorizationDataJson = localStorage.getItem(twitterAuthorizationName),
                    authorizationData = {};

                if (authorizationDataJson) {
                    authorizationData = JSON.parse(authorizationDataJson);
                }

                return authorizationData;
            },
            setAuthorizationData: function (authorizationData) {
                if (authorizationData) {
                    localStorage.setItem(twitterAuthorizationName, JSON.stringify(authorizationData));
                }
                else {
                    localStorage.removeItem(twitterAuthorizationName);
                }
            }
        }
    }

    angular.module('myApp.services').factory('identity', [identity]);
}());