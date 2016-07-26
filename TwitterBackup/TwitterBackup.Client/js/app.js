(function () {
    'use strict';

    function config($routeProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
        $routeProvider
            .when('/login', {
                templateUrl: 'views/partials/twitter-login.html',
                controller: 'LoginController'
            })
            .otherwise({ redirectTo: '/' });
    }
    
    angular.module('myApp.services', []);
    angular.module('myApp.controllers', ['myApp.services']);
    angular.module('myApp', ['ngRoute', 'ngCookies', 'myApp.controllers']).
        config(['$routeProvider', config])
        .value('toastr', toastr)
        .constant('baseServiceUrl', 'http://localhost:19169');
}());