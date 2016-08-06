(function () {
    'use strict';
    //Store cache only if user is logged with required verifier code.
    function cacheService($cacheFactory, identity) {
        var cache = $cacheFactory('TweeterCache');

        function get(key) {
            var verifierCode = identity.getVerifierCode();

            if (verifierCode) {
                key = key + verifierCode;

                return cache.get(key);
            }

            return null;
        }

        function set(key, value) {
            var verifierCode = identity.getVerifierCode();

            if (verifierCode) {
                key = key + verifierCode;

                cache.put(key, value);
            }
        }

        return {
            get: get,
            set: set
        };
    }

    angular.module('myApp.services')
        .factory('cacheService', ['$cacheFactory', 'identity', cacheService]);
}());