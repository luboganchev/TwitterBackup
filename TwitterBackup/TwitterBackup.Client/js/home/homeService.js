(function () {
    'use strict';

    function homeService(dataService) {

        var TWITTER_URL = 'api/Twitter';

        function getFriends(tweetId) {
            return dataService.get(TWITTER_URL + '/GetFriends');
        }

        function unfollowFriend(userId) {
            return dataService.post(TWITTER_URL + '/UnfollowFriend', '"' + userId + '"');
        }

        return {
            getFriends: getFriends,
            unfollowFriend: unfollowFriend
        };
    }

    angular.module('myApp.services')
        .factory('homeService', ['dataService', homeService]);
}());