(function () {
    'use strict';

    function LoginController($scope, $location, $window, notifier, identity, auth) {
        $scope.identity = identity;

        $scope.login = function () {
            auth.login().then(function (success) {
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
        //        auth.login(user).then(function (success) {
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

        $scope.logout = function () {
            auth.logout().then(function () {
                notifier.success('Successful logout!');
                if ($scope.user) {
                    $scope.user.email = '';
                    $scope.user.username = '';
                    $scope.user.password = '';
                }

                $scope.loginForm.$setPristine();
                $location.path('/');
            })
        }

        $scope.checkAuthStatus = function () {

            //check popup window url
            try {
                //console.log($scope.socialAuthWindow.location.href);
                //console.log($scope.socialAuthWindow.document.domain);
                //console.log($scope.socialAuthWindow.document.domain);
                //console.log(document.domain);
                //if ($scope.socialAuthWindow.document.domain === document.domain) {
                // console.log($scope.socialAuthWindow.document.URL);
                var credentialData = $scope.getUrlParameters($scope.socialAuthWindow.location.href, true);
                //when auth is complete the code param will be present from facebook
                if (credentialData) {
                    debugger;
                    localStorage.setItem('credentials', JSON.stringify(credentialData));
                    console.log('AUTH COMPLETE.');
                    $scope.socialAuthWindow.close();
                }
                //}
            } catch (e) {
                //domain mismatch catch
                console.log('Checking auth...');
            }

            //on window close
            if ($scope.socialAuthWindow.closed) {
                console.log('AUTH WINDOW CLOSED.');
                $scope.authEnd();
            }
            else setTimeout($scope.checkAuthStatus, 200);

        };

        /**
         * socialAuth Popup Window Handler.
         */
        $scope.authEnd = function () {
            //AuthService.login();
        };

        /**
        * Get the value of URL parameters either from current URL or static URL
        */
        $scope.getUrlParameters = function (staticURL, decode) {
            var currLocation = (staticURL.length) ? staticURL : window.location.search;
            var parArr = currLocation.split("#")[1].split("&");

            var credentialsObject = {};
            for (var i = 0; i < parArr.length; i++) {
                var parr = parArr[i].split("=");
                credentialsObject[parr[0]] = parr[1];
            }

            if ('access_token' in credentialsObject)
            {
                return credentialsObject;
            }

            return null;
        };
    }

    angular.module('myApp.controllers')
        .controller('LoginController', ['$scope', '$location', '$window', 'notifier', 'identity', 'auth', LoginController]);
}());