(function () {
    'use strict';

    function userService(dataService) {

        var TWITTER_URL = 'api/Twitter';

        function getUserDetails(tweetId) {
            return dataService.get(TWITTER_URL + '/UserDetails', { userId: tweetId });
        }

        function createRetweet(tweetId) {
            return dataService.post(TWITTER_URL + '/Retweet', { userId: tweetId });
        }

        return {
            getUserDetails: getUserDetails,
            createRetweet: createRetweet
        };
    }

    angular.module('myApp.services')
        .factory('userService', ['dataService', userService]);
}());