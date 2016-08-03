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

        function searchFriends(keyword) {
            return dataService.get(TWITTER_URL + '/SearchFriends', { keyword: keyword })
        }

        return {
            getFriends: getFriends,
            unfollowFriend: unfollowFriend,
            searchFriends: searchFriends
        };
    }

    angular.module('myApp.services')
        .factory('homeService', ['dataService', homeService]);
}());