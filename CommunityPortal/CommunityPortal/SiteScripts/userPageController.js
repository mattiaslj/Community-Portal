
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
                vm.messages = data.data; // get data as an array
                var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
                for (var i = 0; i < vm.messages.length; i++) {
                    vm.messages[i].Time = new Date(parseInt(vm.messages[i].Time.substr(6, 18))).toLocaleDateString("se-SE", options);
                }
                vm.messages = vm.messages.reverse();
            })
        })

        authentication.getUsers().then(function (data) {
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


        vm.sendMessage = function () {
            chat.addMessage(vm.currentUser, vm.userName, vm.message)
                .then(function (data) {
                    vm.message = '';
                    $route.reload();
                });
        };

        function timeConverter(UNIX_timestamp) {
            var a = new Date(UNIX_timestamp * 1000);
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            var year = a.getFullYear();
            var month = months[a.getMonth()];
            var date = a.getDate();
            var hour = a.getHours();
            var min = a.getMinutes();
            var sec = a.getSeconds();
            var time = date + ' ' + month + ' ' + year + ' ' + hour + ':' + min + ':' + sec;
            return time;
        }
    });

}());