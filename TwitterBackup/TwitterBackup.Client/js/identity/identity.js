(function () {
    'use strict';

    function identity($cookieStore) {
        var cookieStorageUserKey = 'currentApplicationUserData';

        var currentUser = null;
        return {
            getCurrentUserData: function () {
                var savedUser = $cookieStore.get(cookieStorageUserKey);
                if (savedUser) {
                    return savedUser;
                }

                return currentUser;
            },
            setCurrentUserData: function (user) {
                if (user) {
                    $cookieStore.put(cookieStorageUserKey, user);
                }
                else {
                    $cookieStore.remove(cookieStorageUserKey);
                }

                currentUser = user;
            },
            isAuthenticated: function () {
                return this.getCurrentUser() ? true : false;
            }
        }
    }

    angular.module('myApp.services').factory('identity', ['$cookieStore', identity]);
}());