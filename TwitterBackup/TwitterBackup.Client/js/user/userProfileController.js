(function () {
    'use strict';

    function UserProfileController($scope, $location, $routeParams, userService) {
        var vm = this;
        vm.id = $routeParams.id;
        $scope.userProfile = null;
        userService.getUserDetails(vm.id)
            .then(function (response) {
                $scope.userProfile = JSON.parse(response);
                vm.ProfileBannerUrl = $scope.userProfile.ProfileBannerUrl;
                vm.ProfileLinkColor = $scope.userProfile.ProfileLinkColor;


                $scope.bannerValue = $scope.userProfile.ProfileBannerUrl ? 'url(' +$scope.userProfile.ProfileBannerUrl + ')': '#' +$scope.userProfile.ProfileLinkColor;
            });

        //$scope.setBannerValue = function () {
        //    var attribute = null;
        //    var value = null;

        //    if (vm.ProfileBannerUrl) {
        //        attribute = 'background-url';
        //        value = 'src('+ vm.ProfileBannerUrl+')';
        //    } else {
        //        attribute = 'background-color';
        //        value = '#' + vm.ProfileLinkColor;
        //    }
            
        //    var returnObject = {};
        //    returnObject[attribute] = value;

        //    return returnObject;
        //}
    }

    angular.module('myApp.controllers')
        .controller('UserProfileController', ['$scope', '$location', '$routeParams', 'userService', UserProfileController]);
}());