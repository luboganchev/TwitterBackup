(function () {
    'use strict';

    function LoginController($scope, $location, $window, $timeout, notifier, identity, authService, authConstants) {
        $scope.identity = identity;

        $scope.login = function () {
            authService.login().then(function (success) {
                if (success) {
                    $window.$windowScope = $scope;
                    $scope.socialAuthWindow = $window.open(success, "Authenticate Account", "location=0,status=0,width=600,height=750"); //window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left);

                    //check the status of the popup window.
                    $scope.checkAuthStatus();

                    //puts focus on the newWindow
                    if (window.focus) {
                        $scope.socialAuthWindow.focus();
                    }

                    notifier.success('Successful login!');
                }
                else {
                    notifier.error('Username/Password combination is not valid!');
                }
            });
        }

        //$scope.login = function (user, loginForm) {
        //    debugger;
        //    if (loginForm.$valid) {
        //        authService.login(user).then(function (success) {
        //            if (success) {
        //                notifier.success('Successful login!');
        //            }
        //            else {
        //                notifier.error('Username/Password combination is not valid!');
        //            }
        //        });
        //    }
        //    else {
        //        notifier.error('Username and password are required fields!')
        //    }
        //}

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

            //var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';
            //var externalProviderUrl = authConstants.apiServiceBaseUrl + "/api/Account/ExternalLogin?provider=" + provider
            //                                                            + "&response_type=token&client_id=" + authConstants.clientId
            //                                                            + "&redirect_uri=" + redirectUri;
        };

        $scope.authCompletedCB = function (queryParams) {
            var authorizationData = identity.getAuthorizationData();
            authorizationData.VerifierCode = /oauth_verifier=(\w+)/.exec(queryParams)[1];
            identity.setAuthorizationData(authorizationData);

            authService.authenticate().then(function () {
                $timeout(function () {
                    $location.path('/');
                });
            });


            //authService.authorize(queryParams).then(function () {
            //});

            //$scope.$apply(function () {
            //    authService.getUserInfo(fragment.access_token).then(function (userInfo) {
            //        if (userInfo.data.HasRegistered === false) {
            //            authService.logout();

            //            authService.externalAuthData = {
            //                provider: userInfo.data.LoginProvider,
            //                userName: userInfo.data.Email,// fragment.external_user_name,
            //                externalAccessToken: fragment.access_token
            //            };

            //            $location.path('/associate');
            //        } else {
            //            //Obtain access token and redirect to home
            //            var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
            //            authService.obtainAccessToken(externalData).then(function (response) {
            //                $location.path('/');
            //            }, function (error) {
            //                notifier.error(error.error_description);
            //            });
            //        }
            //    }, function () {
            //        notifier.error(error.error_description);
            //    });
            //});
        }
    }

    angular.module('myApp.controllers')
        .controller('LoginController', ['$scope', '$location', '$window', '$timeout', 'notifier', 'identity', 'authService', 'authConstants', LoginController]);
}());