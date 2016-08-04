(function () {
    'use strict';

    function UserProfileController($scope, $location, $routeParams, $filter, cacheService, userService, notifier) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.userDetailsCacheKey = 'userDetails' + vm.id;
        $scope.userProfile = null;

        vm.getTweetObject = function (id) {
            var foundObject = $filter('filter')($scope.userProfile.Tweets, { Id: id }, true);
            if (foundObject.length) {
                return foundObject[0];
            }

            return null;
        }

        vm.getUserDetails = function (useCache) {
            useCache = useCache !== false ? true : useCache;
            var cachedValue = cacheService.get(vm.userDetailsCacheKey);
            if (useCache && cachedValue) {
                $scope.userProfile = JSON.parse(cachedValue);
                $scope.bannerValue = $scope.userProfile.ProfileBannerUrl ? 'url(' + $scope.userProfile.ProfileBannerUrl + ')' : '#' + $scope.userProfile.ProfileLinkColor;
            } else {
                userService.getUserDetails(vm.id)
                    .then(function (response) {
                        $scope.userProfile = JSON.parse(response);
                        vm.ProfileBannerUrl = $scope.userProfile.ProfileBannerUrl;
                        vm.ProfileLinkColor = $scope.userProfile.ProfileLinkColor;
                        $scope.bannerValue = $scope.userProfile.ProfileBannerUrl ? 'url(' + $scope.userProfile.ProfileBannerUrl + ')' : '#' + $scope.userProfile.ProfileLinkColor;
                        cacheService.set(vm.userDetailsCacheKey, response);
                    });
            }
        }

        $scope.storeTweet = function (id) {
            var foundObject = vm.getTweetObject(id);
            if (foundObject) {
                userService.storeUserTweet(foundObject)
                    .then(function (response) {
                        notifier.success('Tweet is successfully saved');
                        vm.getUserDetails(false);
                    });
            } else {
                notifier.error('Tweet not found');
            }
        }

        $scope.publishTweet = function (id) {
            var foundObject = vm.getTweetObject(id);
            if (foundObject) {
                userService.publishRetweet(id)
                   .then(function (response) {
                       notifier.success('Tweet is successfully retweeted');
                       vm.getUserDetails(false);
                   });
            } else {
                notifier.error('Tweet not found');
            }
        }

        vm.getUserDetails();
    }

    angular.module('myApp.controllers')
        .controller('UserProfileController', ['$scope', '$location', '$routeParams', '$filter', 'cacheService', 'userService', 'notifier', UserProfileController]);
}());