(function () {
    'use strict';

    function config($routeProvider, $httpProvider, $locationProvider, $sceDelegateProvider, authConstants) {
        $httpProvider.interceptors.push('authInterceptorService');

        $sceDelegateProvider.resourceUrlWhitelist([
          // Allow same origin resource loads.
          'self',
          // Allow loading from our assets domain.  Notice the difference between * and **.
          authConstants.apiServiceBaseUrl
        ]);

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
            .when("/admin", {
                templateUrl: 'views/partials/admin.html',
                controller: "AdminController",
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
    angular.module('myApp.filters', []);
    //angular.module('myApp.directives', []);
    angular.module('myApp.controllers', ['myApp.services']);
    angular.module('myApp', ['ngRoute', 'ngCookies', 'ui.bootstrap', 'ngSanitize', 'myApp.controllers'])
        .config(['$routeProvider', '$httpProvider', '$locationProvider', '$sceDelegateProvider', 'authConstants', config])
        .value('toastr', toastr)
        .constant('baseServiceUrl', baseServiceUrl)
        .constant('authConstants', {
            apiServiceBaseUrl: baseServiceUrl,
            clientId: 'Progress'
        })
        //.run(['applicationAuthorization', function () { }])
}());