(function () {
    'use strict';

    function AssociateController($scope, $location, $timeout, authService) {
        $scope.savedSuccessfully = false;
        $scope.message = "";

        $scope.registerData = {
            userName: authService.externalAuthData.userName,
            provider: authService.externalAuthData.provider,
            externalAccessToken: authService.externalAuthData.externalAccessToken
        };

        $scope.registerExternal = function () {
            authService.registerExternal($scope.registerData).then(function (response) {
                $scope.savedSuccessfully = true;
                //TODO toaster
                $scope.message = "User has been registered successfully, you will be redicted to orders page in 2 seconds.";
                startTimer();
            },
              function (response) {
                  var errors = [];
                  for (var key in response.modelState) {
                      errors.push(response.modelState[key]);
                  }
                  //TODO toaster
                  $scope.message = "Failed to register user due to:" + errors.join(' ');
              });
        };

        //Redirect to home screen after successfull association
        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/');
            }, 2000);
        }
    }

    angular.module('myApp.controllers')
        .controller('AssociateController', ['$scope', '$location', '$timeout', 'auth', AssociateController]);
}());