(function () {
    'use strict';

    function HomeController($scope, $http, homeService) {
        var vm = this;

        $scope.unfollowFriend = function (id) {
            homeService.unfollowFriend(id)
                .then(function (response) {
                    if (response === true) {
                        vm.getFriends();
                    }
                }, function (error) {

                });
        }

        vm.getFriends = function () {
            homeService.getFriends()
                .then(function (response) {
                    $scope.friends = JSON.parse(response);
                });
        }

        vm.getFriends();
    }

    angular.module('myApp.controllers')
        .controller('HomeController', ['$scope', '$http', 'homeService', HomeController]);
}());