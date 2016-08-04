(function () {
    'use strict';

    function trustUrl($sce) {
        return function (url) {
            return $sce.trustAsResourceUrl(url);
        };
    }

    angular.module('myApp.services')
        .filter('trustUrl', ['$sce', trustUrl]);
}());