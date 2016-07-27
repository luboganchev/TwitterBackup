(function () {
    'use strict';

    function auth($http, $q, $window, identity, authorization, baseServiceUrl) {
        var usersApi = baseServiceUrl + '/api/users'

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


                //ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                //+ "&response_type=token&client_id=" + ngAuthSettings.clientId
                //+ "&redirect_uri=" + redirectUri;

                //$http.get(baseServiceUrl + '/api/Account/ExternalLogin', { params: {provider: 'Twitter', response_type: 'token',client_id:'self', redirect_uri:'/' }, headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })

                //?returnUrl=%2F&generateState=true
                //First get externalLogins
                var redirectUri = location.protocol + '//' + location.host + '/';
                $http.get(baseServiceUrl + '/api/Account/ExternalLogins', { params: { returnUrl: redirectUri, generateState: true, }, headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                    .then(function (providersResponse) {
                        //Search twitter provider
                        var twitterProvider = null;
                        for (var index in providersResponse.data) {
                            var currentProvider = providersResponse.data[index];
                            if (providersResponse.data[index].Name == 'Twitter') {
                                twitterProvider = currentProvider;
                                break;
                            }
                        }
                        if (twitterProvider) {
                            deferred.resolve(baseServiceUrl + twitterProvider.Url);
                            //$window.open(baseServiceUrl + twitterProvider.Url, "Authenticate Account", "location=0,status=0,width=600,height=750");

                            //$window.$windowScope = $scope;
                            //$scope.socialAuthWindow = $window.open(baseServiceUrl + twitterProvider.Url, "Authenticate Account", "location=0,status=0,width=600,height=750"); //window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left);

                            ////check the status of the popup window.
                            //$scope.checkAuthStatus();

                            ////puts focus on the newWindow
                            //if (window.focus) {
                            //    $scope.socialAuthWindow.focus();
                            //}


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