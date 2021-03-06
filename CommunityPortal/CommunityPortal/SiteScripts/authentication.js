﻿(function () {
    'use strict'

    var app = angular.module('communityPortal');

    app.factory('authentication', function ($http) {

        this.getCurrentUser = function () {
            return $http.post('AngularAuthentication/GetCurrentUser')
                        .then(function (response) {
                            return response.data;
                        });
        };

        this.getUserRole = function () {
            return $http.get('AngularAuthentication/GetUserRole')
                        .then(function (response) {
                            return response.data;
                        });
        };

        this.getUsers = function () {
            return $http.get('AngularAuthentication/GetUsers')
                        .then(function (response) {
                            return response.data;
                        });
        }
        return {
            getCurrentUser: this.getCurrentUser,
            getUserRole: this.getUserRole,
            getUsers: this.getUsers
        }
    })

}());