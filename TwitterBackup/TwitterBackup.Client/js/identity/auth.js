(function () {
    'use strict';

    function auth($http, $q, $window, identity, authorization, baseServiceUrl) {
        var accountApi = baseServiceUrl + '/api/Account';
        var socialAuthWindow = null;
        //$scope.authEnd = function() {
        //    //AuthService.login();
        //};

        function getUrlParameters (staticURL, decode) {
            var currLocation = (staticURL.length) ? staticURL : window.location.search;
            var parArr = currLocation.split("#")[1].split("&");

            var credentialsObject = {};
            for (var i = 0; i < parArr.length; i++) {
                var parr = parArr[i].split("=");
                credentialsObject[parr[0]] = parr[1];
            }

            if ('access_token' in credentialsObject) {
                return credentialsObject;
            }

            return null;
        };

        function checkAuthStatus() {
            var deferred = $q.defer();
            function waitForUrlParams() {
                //check popup window url
                try {
                    console.log(socialAuthWindow.location.href);
                    var credentialData = getUrlParameters(socialAuthWindow.location.href, true);
                    if (credentialData) {
                        localStorage.setItem('credentials', JSON.stringify(credentialData));
                        console.log('AUTH COMPLETE.');
                        socialAuthWindow.close();
                        deferred.resolve(credentialData);
                    }
                    //}
                } catch (e) {
                    //domain mismatch catch
                    console.log('Checking auth...');
                }

                //on window close
                if (socialAuthWindow.closed) {
                    console.log('AUTH WINDOW CLOSED.');
                    //$scope.authEnd();
                }
                else {
                    setTimeout(waitForUrlParams, 200);
                }
            }
            waitForUrlParams();

            return deferred.promise;
        };

        return {
            signup: function (user) {
                var deferred = $q.defer();

                $http.post(usersApi + '/register', user)
                    .then(function () {
                        deferred.resolve();
                    }, function (response) {
                        deferred.reject(response.message);
                    });

                return deferred.promise;
            },
            login: function (user) {
                var deferred = $q.defer();

                //First get externalLogins
                var redirectUri = location.protocol + '//' + location.host + '/';
                $http.get(accountApi + '/ExternalLogins', { params: { returnUrl: redirectUri, generateState: true, }, headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                    .then(function (providersResponse) {
                        //Get twitter provider
                        var twitterProvider = null;
                        for (var index in providersResponse.data) {
                            var currentProvider = providersResponse.data[index];
                            if (providersResponse.data[index].Name == 'Twitter') {
                                twitterProvider = currentProvider;
                                break;
                            }
                        }
                        if (twitterProvider) {
                            //$window.$windowScope = $scope;
                            socialAuthWindow = $window.open(baseServiceUrl + twitterProvider.Url, "Authenticate Account", "location=0,status=0,width=600,height=750"); //window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left);

                            //puts focus on the newWindow
                            if (window.focus) {
                                socialAuthWindow.focus();
                            }

                            //check the status of the popup window.
                            checkAuthStatus().then(function (credentialData) {
                                if (credentialData.access_token) {
                                    identity.setCurrentUser(credentialData);
                                    //deferred.resolve(true);
                                }

                                var headersObject = authorization.getAuthorizationHeader();
                                //Check if user is authenticated
                                $http.get(accountApi + '/UserInfo', { headers: headersObject })
                                    .then(function (response) {
                                        debugger;
                                    });
                            });

                            // deferred.resolve(baseServiceUrl + twitterProvider.Url);
                            //$http.get(baseServiceUrl + twitterProvider.Url)
                            //    .then(function (response) {
                            //        debugger;
                            //        if (response.data["access_token"]) {
                            //            identity.setCurrentUser(response.data);
                            //            deferred.resolve(true);
                            //        }
                            //        else {
                            //            deferred.resolve(false);
                            //        }
                            //    });
                        } else {
                            deferred.resolve(false);
                        }
                    });
                //user['grant_type'] = 'password';
                //$http.post(usersApi + '/login', 'username=' + user.username + '&password=' + user.password + '&grant_type=password', { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                //    .then(function (response) {
                //        if (response.data["access_token"]) {
                //            identity.setCurrentUser(response.data);
                //            deferred.resolve(true);
                //        }
                //        else {
                //            deferred.resolve(false);
                //        }
                //    });

                return deferred.promise;
            },
            logout: function () {
                var deferred = $q.defer();

                var headers = authorization.getAuthorizationHeader();
                $http.post(usersApi + '/logout', {}, { headers: headers })
                    .then(function () {
                        identity.setCurrentUser(undefined);
                        deferred.resolve();
                    });

                return deferred.promise;
            },
            isAuthenticated: function () {
                if (identity.isAuthenticated()) {
                    return true;
                }
                else {
                    return $q.reject('not authorized');
                }
            }
        }
    }

    angular.module('myApp.services')
        .factory('auth', ['$http', '$q', '$window', 'identity', 'authorization', 'baseServiceUrl', auth]);
}());