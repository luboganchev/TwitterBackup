(function () {
    'use strict';

    function LoginController($scope, $location, $window, $timeout, notifier, identity, authService, authConstants) {
        $scope.identity = identity;

        $scope.authExternalProvider = function (provider) {
            authService.authorize()
                .then(function () {
                    var twitterData = identity.getAuthorizationData();
                    if (twitterData) {
                        window.$windowScope = $scope;

                        var authorizationUrl = twitterData.AuthorizationURL;
                        window.open(authorizationUrl, "Authenticate Account", "location=0,status=0,width=800,height=900");
                    }
            });
        };

        $scope.authCompletedCB = function (queryParams) {
            var authorizationData = identity.getAuthorizationData();
            authorizationData.VerifierCode = /oauth_verifier=(\w+)/.exec(queryParams)[1];
            identity.setAuthorizationData(authorizationData);

            authService.authenticate().then(function () {
                $timeout(function () {
                    $location.path('/');
                    notifier.success('Successful login!');
                });
            });
        }
    }

    angular.module('myApp.controllers')
        .controller('LoginController', ['$scope', '$location', '$window', '$timeout', 'notifier', 'identity', 'authService', 'authConstants', LoginController]);
}());