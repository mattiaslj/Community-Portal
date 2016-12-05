
(function () {
    'use strict'

    var app = angular.module('communityPortal');

    app.controller('userPageController', function (authentication, $scope, $route, $routeParams, chat) {

        var vm = this;

        vm.message = '';
        vm.messages = [];
        vm.currentUser = {};
        // username is the username of the user that is being watched
        vm.userName = $routeParams.userName.replace(':', '');
        vm.users = [];
        $scope.users = [];

        authentication.getCurrentUser().then(function (data) {
            vm.currentUser = data;
            chat.getMessages(vm.currentUser, vm.userName).then(function (data) {
                vm.messages = data;
            })
        })

        authentication.getUsers().then(function (data) {
            console.log(data);
            vm.users = angular.copy(data);

            $scope.totalItems = vm.users.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 10;

            $scope.$watch("currentPage", function () {
                setPagingData($scope.currentPage);
            });

            function setPagingData(page) {
                var pagedData = vm.users.slice(
                  (page - 1) * $scope.itemsPerPage,
                  page * $scope.itemsPerPage
                );

                $scope.users = pagedData;
            }
        });
    })
}());