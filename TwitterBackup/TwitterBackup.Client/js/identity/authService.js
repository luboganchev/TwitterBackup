(function () {
    'use strict';

    function authService($http, $q, $window, identity, authorization, baseServiceUrl) {
        var accountApi = baseServiceUrl + '/api/Account';
        var twitterApi = baseServiceUrl + '/api/Twitter';
        var socialAuthWindow = null;
        //$scope.authEnd = function() {
        //    //AuthService.login();
        //};

        function getUrlParameters(staticURL, decode) {
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

                                //var headersObject = authorization.getAuthorizationHeader();
                                //Check if user is authenticated
                                $http.get(accountApi + '/UserInfo')
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
            getUserInfo: function (externalToken) {
                var deferred = $q.defer();

                $http.get(accountApi + '/UserInfo', { headers: { 'Authorization': 'Bearer ' + externalToken } }).then(function (response) {
                    deferred.resolve(response);
                }, function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },
            obtainAccessToken: function (externalData) {
                var deferred = $q.defer();

                $http.get(accountApi + '/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {
                    localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.useRefreshTokens = false;

                    deferred.resolve(response);

                }).error(function (err, status) {
                    this.logout();
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            logout: function () {
                identity.setAuthorizationData(null);

                //var deferred = $q.defer();

                ////var headers = authorization.getAuthorizationHeader();
                //$http.post(accountApi + '/logout', {})
                //    .then(function () {
                //        identity.setCurrentUser(undefined);
                //        deferred.resolve();
                //    });

                //return deferred.promise;
            },
            registerExternal: function (externalAuthData) {
                var deferred = $q.defer();

                $http.post(accountApi + '/RegisterExternal', { 'Email': externalAuthData.userName }, { headers: { 'Authorization': 'Bearer ' + externalAuthData.externalAccessToken } }).success(function (response) {
                    //localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });
                    identity.setCurrentUserData({ token: response.access_token, userName: response.userName });
                    //authentication.isAuth = true;
                    //authentication.userName = response.userName;
                    deferred.resolve(response);
                }).error(function (err, status) {
                    this.logout();
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            isAuthorized: function () {
                var authorizationData = identity.getAuthorizationData();

                return !authorizationData.VerifierCode;
            },
            authorize: function () {
                return $http.get(twitterApi + '/Authorize', {
                    withCredentials: true,
                    params: {
                        redirectUrl: location.protocol + '//' + location.host + '/authComplete.html'
                    }
                }).then(function (response) {
                    identity.setAuthorizationData(JSON.parse(response.data));
                });
            },
            externalAuthData: {
                provider: "",
                userName: "",
                externalAccessToken: ""
            },
            authentication: {
                isAuth: false,
                userName: ""
            }
        };
    }

    angular.module('myApp.services')
        .factory('authService', ['$http', '$q', '$window', 'identity', 'authorization', 'baseServiceUrl', authService]);
}());