(function () {
    'use strict';

    function HomeController($scope, $http,cacheService, homeService) {
        var vm = this;
        var friendsCacheKey = 'friends';
        $scope.unfollowFriend = function (id) {
            homeService.unfollowFriend(id)
                .then(function (response) {
                    if (response === true) {
                        vm.getFriends();
                    }
                }, function (error) {

                });
        }

        $scope.searchFriends = function (keyword) {
            return homeService.searchFriends(keyword)
                .then(function (response) {
                    return response;
                });
        }

        vm.getFriends = function () {
            var cachedValue = cacheService.get(friendsCacheKey);
            if (cachedValue) {
                $scope.friends = JSON.parse(cachedValue);
            } else {
                homeService.getFriends()
                    .then(function (response) {
                        $scope.friends = JSON.parse(response);
                        cacheService.set(friendsCacheKey, response);
                    });
            }
        }

        vm.getFriends();
    }

    angular.module('myApp.controllers')
        .controller('HomeController', ['$scope', '$http', 'cacheService', 'homeService', HomeController]);
}());