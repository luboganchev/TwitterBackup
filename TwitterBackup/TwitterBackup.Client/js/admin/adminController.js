(function () {
    'use strict';

    function AdminController($scope, $http, adminService, notifier) {
        var vm = this;

        adminService.getAdminData()
            .then(function (response) {
                $scope.adminData = response;
            });
    }

    angular.module('myApp.controllers')
        .controller('AdminController', ['$scope', '$http', 'adminService', 'notifier', AdminController]);
}());