(function () {
    'use strict';

    function homeService(dataService) {

        var TWITTER_URL = 'api/Twitter';

        function getFriends(tweetId) {
            return dataService.get(TWITTER_URL + '/GetFriends');
        }

        function unfollowFriend(screenName) {
            return dataService.post(TWITTER_URL + '/UnfollowFriend', '"' + screenName + '"');
        }

        function searchFriends(keyword) {
            return dataService.get(TWITTER_URL + '/SearchFriends', { keyword: keyword })
        }

        function followFriend(screenName) {
            return dataService.post(TWITTER_URL + '/FollowFriend', '"' + screenName + '"');
        }

        return {
            getFriends: getFriends,
            unfollowFriend: unfollowFriend,
            searchFriends: searchFriends,
            followFriend: followFriend
        };
    }

    angular.module('myApp.services')
        .factory('homeService', ['dataService', homeService]);
}());