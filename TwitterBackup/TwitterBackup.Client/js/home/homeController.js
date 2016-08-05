(function () {
    'use strict';

    function HomeController($scope, $http, cacheService, homeService, notifier) {
        var vm = this;
        var friendsCacheKey = 'friends';
        $scope.selectedUser = null;

        $scope.unfollowFriend = function (id) {
            homeService.unfollowFriend(id)
                .then(function (response) {
                    if (response === true) {
                        vm.getFriends(false);
                    }
                });
        }

        $scope.followFriend = function (id) {
            var userName = $scope.selectedUser.Name;

            homeService.followFriend(id)
                .then(function (response) {
                    if (response === true) {
                        vm.getFriends(false);
                        notifier.success(userName + 'is successfully followed');
                        $scope.selectedUser = null;
                    }
                });
        }

        $scope.searchFriends = function (keyword) {
            return homeService.searchFriends(keyword)
                .then(function (response) {
                    return response;
                });
        }

        vm.getFriends = function (useCache) {
            useCache = useCache !== false ? true : useCache;
            var cachedValue = cacheService.get(friendsCacheKey);
            if (useCache && cachedValue) {
                $scope.friends = cachedValue;
            } else {
                homeService.getFriends()
                    .then(function (response) {
                        $scope.friends = response;
                        cacheService.set(friendsCacheKey, response);
                    });
            }
        }

        vm.getFriends();
    }

    angular.module('myApp.controllers')
        .controller('HomeController', ['$scope', '$http', 'cacheService', 'homeService', 'notifier', HomeController]);
}());