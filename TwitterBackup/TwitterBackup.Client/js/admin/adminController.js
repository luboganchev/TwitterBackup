(function () {
    'use strict';

    function AdminController($scope, $http, adminService, cacheService, notifier) {
        var vm = this;
        var adminCacheKey = 'admin';

        vm.getAdminData = function (useCache) {
            useCache = useCache !== false ? true : useCache;
            var cachedValue = cacheService.get(adminCacheKey);
            if (useCache && cachedValue) {
                $scope.adminData = cachedValue;
            } else {
                adminService.getAdminData()
                    .then(function (response) {
                        $scope.adminData = response;
                        cacheService.set(adminCacheKey, response);
                    });
            }
        }

        vm.getAdminData();
    }

    angular.module('myApp.controllers')
        .controller('AdminController', ['$scope', '$http', 'adminService', 'cacheService', 'notifier', AdminController]);
}());