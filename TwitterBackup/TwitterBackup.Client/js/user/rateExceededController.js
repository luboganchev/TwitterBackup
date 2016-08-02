(function () {
    'use strict';

    function RateExceededController($scope, $location, $timeout, authService) {

    }

    angular.module('myApp.controllers')
        .controller('RateExceededController', ['$scope', '$location', '$timeout', 'authService', RateExceededController]);
}());