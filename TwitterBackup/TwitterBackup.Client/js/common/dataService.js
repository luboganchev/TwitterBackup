(function () {
    'use strict';

    function dataService($http, $q, authorization, notifier, baseServiceUrl) {
        var $loaderDiv = $('#loaderDiv');
        function get(url, queryParams) {
            var defered = $q.defer();
            $loaderDiv.show();

            $http.get(baseServiceUrl + '/' + url, { params: queryParams})
                .then(function (response) {
                    $loaderDiv.hide();
                    defered.resolve(response.data);
                }, function (error) {
                    $loaderDiv.hide();
                    error = getErrorMessage(error);
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        function post(url, postData) {
            var defered = $q.defer();
            $loaderDiv.show();
            $http.post(baseServiceUrl + '/' + url, postData)
                .then(function (response) {
                    $loaderDiv.hide();
                    defered.resolve(response.data);
                }, function (error) {
                    $loaderDiv.hide();
                    error = getErrorMessage(error);
                    notifier.error(error);
                    defered.reject(error);
                });

            return defered.promise;
        }

        function put() {
            throw new Error('Not implemented!');
        }

        function getErrorMessage(response) {
            var error = response.data.modelState;
            if (error && error[Object.keys(error)[0]][0]) {
                error = error[Object.keys(error)[0]][0];
            }
            else {
                error = response.data.Message;
            }

            return error;
        }

        return {
            get: get,
            post: post,
            put: put
        };
    }

    angular.module('myApp.services')
        .factory('dataService', ['$http', '$q', 'authorization', 'notifier', 'baseServiceUrl', dataService]);
}());