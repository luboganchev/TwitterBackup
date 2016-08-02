(function () {
    'use strict';

    function config($routeProvider, $httpProvider, $locationProvider) {
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
            .when('/logout', {
                template: '',
                controller: ['$timeout', '$location', 'authService', function ($timeout, $location, authService) {
                    $timeout(function () {
                        authService.logout();
                        $location.path('/login');
                    });
                }]
            })
            .when("/rate-exceeded", {
                templateUrl: "views/partials/rate-exceeded.html",
                controller: "RateExceededController",
            })
            .when('/user/profile/:id', {
                templateUrl: 'views/partials/user-profile.html',
                controller: 'UserProfileController'
            })
            .otherwise({ redirectTo: '/' });
    }
    var baseServiceUrl = 'http://localhost:19169';
    angular.module('myApp.services', []);
        //.factory('applicationAuthorization', ['authService', function (authService) {
        //    return authService.authorize();
        //}]);
    //angular.module('myApp.directives', []);
    angular.module('myApp.controllers', ['myApp.services']);
    angular.module('myApp', ['ngRoute', 'ngCookies', 'myApp.controllers'])
        .config(['$routeProvider', '$httpProvider', '$locationProvider', config])
        .value('toastr', toastr)
        .constant('baseServiceUrl', baseServiceUrl)
        .constant('authConstants', {
            apiServiceBaseUrl: baseServiceUrl,
            clientId: 'Progress'
        })
        //.run(['applicationAuthorization', function () { }])
}());