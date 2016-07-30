(function () {
    'use strict';

    function config($routeProvider, $httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');

        $routeProvider
            .when('/', {
                templateUrl: 'views/partials/home.html',
                controller: 'HomeController'
            })
            .when('/login', {
                templateUrl: 'views/partials/twitter-login.html',
                controller: 'LoginController'
            })
            .when("/associate", {
                templateUrl: "/views/partials/associate.html",
                controller: "AssociateController",
            })
            .otherwise({ redirectTo: '/' });
    }
    var baseServiceUrl = 'http://localhost:19169';
    angular.module('myApp.services', []);
    angular.module('myApp.directives', []);
    angular.module('myApp.controllers', ['myApp.services']);
    angular.module('myApp', ['ngRoute', 'ngCookies', 'myApp.controllers'])
        .config(['$routeProvider', '$httpProvider', config])
        .value('toastr', toastr)
        .constant('baseServiceUrl', baseServiceUrl)
        .constant('authConstants', {
            apiServiceBaseUrl: baseServiceUrl,
            clientId: 'Progress'
        });
}());