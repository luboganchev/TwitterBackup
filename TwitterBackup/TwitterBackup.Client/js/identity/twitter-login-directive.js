(function () {
    'use strict';

    function twitterLogin() {
        return {
            restrict: 'A',
            templateUrl: 'views/partials/twitter-login.html',
            scope: {
                heading: '@'
            },
        }
    }

    angular.module('myApp.directives')
        .directive('twitterLogin', [twitterLogin]);
}());