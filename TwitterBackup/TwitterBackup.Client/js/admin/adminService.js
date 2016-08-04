(function () {
    'use strict';

    function adminService(dataService) {

        var ADMIN_URL = 'api/Admin';

        function getAdminData(tweetId) {
            return dataService.get(ADMIN_URL + '/GetAdminData');
        }

        return {
            getAdminData: getAdminData
        };
    }

    angular.module('myApp.services')
        .factory('adminService', ['dataService', adminService]);
}());