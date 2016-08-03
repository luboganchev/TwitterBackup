(function () {
    'use strict';

    function cacheService($cacheFactory) {
        var cache = $cacheFactory('TweeterCache');

        function get(key) {
            return cache.get(key);
        }

        function set(key, value) {
            cache.put(key, value);
        }

        return {
            get: get,
            set: set
        };
    }

    angular.module('myApp.services')
        .factory('cacheService', ['$cacheFactory', cacheService]);
}());