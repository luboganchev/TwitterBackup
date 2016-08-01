(function () {
    'use strict';

    function UserProfileController($scope, $location, $routeParams, userService) {
        var vm = this;
        vm.id = $routeParams.id;
        $scope.userProfile = null;
        userService.getUserDetails(vm.id)
            .then(function (response) {
                $scope.userProfile = JSON.parse(response);
            });
    }

    angular.module('myApp.controllers')
        .controller('UserProfileController', ['$scope', '$location', '$routeParams', 'userService', UserProfileController]);
}());