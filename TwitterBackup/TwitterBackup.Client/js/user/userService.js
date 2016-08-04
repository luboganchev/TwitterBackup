(function () {
    'use strict';

    function userService(dataService) {

        var TWITTER_URL = 'api/Twitter';

        function getUserDetails(tweetId) {
            return dataService.get(TWITTER_URL + '/UserDetails', { userId: tweetId });
        }

        function publishRetweet(tweetId) {
            return dataService.post(TWITTER_URL + '/Retweet', '"' + tweetId + '"');
        }

        function storeUserTweet(tweetData) {
            return dataService.post(TWITTER_URL + '/StoreTweet', tweetData);
        }

        return {
            getUserDetails: getUserDetails,
            publishRetweet: publishRetweet,
            storeUserTweet: storeUserTweet
        };
    }

    angular.module('myApp.services')
        .factory('userService', ['dataService', userService]);
}());