(function () {
    'use strict';

    function authorization(identity) {
        return {
            //getAuthorizationHeader: function () {
            //    return {
            //        'Authorization': 'Bearer ' + identity.getTwitterCookieData()['access_token']
            //    }
            //},
            //getAuthorizationToken: function () {
            //    var currentAuthData = identity.getTwitterCookieData();

            //    if (!currentAuthData) {
            //        return null;
            //    }
                
            //    return currentAuthData['access_token'];
            //}
        }
    }

    angular.module('myApp.services')
        .factory('authorization', ['identity', authorization]);
}());