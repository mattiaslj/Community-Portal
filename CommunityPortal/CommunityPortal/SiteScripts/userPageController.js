
(function () {
    'use strict'

    var app = angular.module('communityPortal');

    app.controller('userPageController', function (authentication, $scope, $route, $routeParams) {

        var vm = this;

        vm.userName = $routeParams.userName.replace(':', '');
        vm.users = [];
        $scope.users = [];

        authentication.getUsers().then(function (data) {
            console.log(data);
            vm.users = angular.copy(data);

            //$scope.totalItems = vm.forumThreads.length;
            //$scope.currentPage = 1;
            //$scope.itemsPerPage = 10;

            //$scope.$watch("currentPage", function () {
            //    setPagingData($scope.currentPage);
            //});

            //function setPagingData(page) {
            //    var pagedData = vm.forumThreads.slice(
            //      (page - 1) * $scope.itemsPerPage,
            //      page * $scope.itemsPerPage
            //    );

            //    $scope.forumThreads = pagedData;
            //}
        })

    })
}());