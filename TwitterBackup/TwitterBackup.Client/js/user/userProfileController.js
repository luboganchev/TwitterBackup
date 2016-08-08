(function () {
    'use strict';

    function UserProfileController($scope, $location, $routeParams, $filter, cacheService, userService, notifier) {
        var vm = this;
        vm.id = $routeParams.screenName;
        vm.userDetailsCacheKey = 'userDetails' + vm.id;
        $scope.userProfile = null;

        vm.getTweetObject = function (id) {
            var foundObject = $filter('filter')($scope.userProfile.Tweets, { IdString: id }, true);
            if (foundObject.length) {
                return foundObject[0];
            }

            return null;
        }

        vm.getBannerValue = function (profileBannerUrl, profileLinkColor) {
            if(profileBannerUrl || profileLinkColor){
                return profileBannerUrl ? 'url(' + profileBannerUrl + ')' : '#' + profileLinkColor;
            }
            
            //default banner color
            return '#f8f8ff'
        }

        vm.getUserDetails = function (useCache) {
            useCache = useCache !== false ? true : useCache;
            var cachedValue = cacheService.get(vm.userDetailsCacheKey);
            if (useCache && cachedValue) {
                $scope.userProfile = cachedValue;
                $scope.bannerValue = vm.getBannerValue($scope.userProfile.ProfileBannerUrl, $scope.userProfile.ProfileLinkColor);
            } else {
                userService.getUserDetails(vm.id)
                    .then(function (response) {
                        $scope.userProfile = response;
                        $scope.bannerValue = vm.getBannerValue($scope.userProfile.ProfileBannerUrl, $scope.userProfile.ProfileLinkColor);
                        cacheService.set(vm.userDetailsCacheKey, response);
                    }, function (error) {
                        $location.path('/');
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

        $scope.publishTweet = function (id, hasAlreadyRetweeted) {
            var foundObject = vm.getTweetObject(id);
            if (foundObject && !hasAlreadyRetweeted) {
                userService.publishRetweet(id)
                   .then(function (response) {
                       notifier.success('Tweet is successfully retweeted');
                       vm.getUserDetails(false);
                   });
            } else {
                if (hasAlreadyRetweeted) {
                    notifier.error('Tweet is already retweeted');
                } else {
                    notifier.error('Tweet not found');
                }
            }
        }

        vm.getUserDetails();
    }

    angular.module('myApp.controllers')
        .controller('UserProfileController', ['$scope', '$location', '$routeParams', '$filter', 'cacheService', 'userService', 'notifier', UserProfileController]);
}());