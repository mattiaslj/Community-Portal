(function () {
    'use strict'

    var app = angular.module('communityPortal');

    app.factory('authentication', function ($http) {

        this.getCurrentUser = function () {
            return $http.post('AngularAuthentication/GetCurrentUser')
                        .then(function (response) {
                            return response.data;
                        });
        };
        return {
            getCurrentUser: this.getCurrentUser
        }
    })

}());