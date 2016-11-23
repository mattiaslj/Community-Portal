(function () {
    'use strict'

    var app = angular.module('communityPortal');

    app.controller('forumController', function (forumThread, authentication, $scope, $route) {

        var vm = this;
        $scope.forumThreads = [];

        vm.showCreateThread = false;
        vm.title = '';
        vm.body = '';

        //Get all threads and add pagination
        vm.forumThreads = {};
        forumThread.getThreads()
                   .then(function (data) {
                       vm.forumThreads = data;
                       vm.forumThreads = vm.forumThreads.reverse();
                       //------------- Pagination ------------------
                       $scope.totalItems = vm.forumThreads.length;
                       $scope.currentPage = 1;
                       $scope.itemsPerPage = 10;

                       $scope.$watch("currentPage", function () {
                           setPagingData($scope.currentPage);
                       });

                       function setPagingData(page) {
                           var pagedData = vm.forumThreads.slice(
                             (page - 1) * $scope.itemsPerPage,
                             page * $scope.itemsPerPage
                           );

                           $scope.forumThreads = pagedData;
                       }
                       //------------------------------------------
                   });


        vm.createThread = function () {
            vm.showCreateThread = false;

            vm.username = '';
            authentication.getCurrentUser()
                          .then(function (data) {
                              vm.username = data;
                              console.log("title: " + vm.title);
                              var thread = {
                                  Title: vm.title,
                                  PostTime: new Date()
                              }
                              forumThread.addThread(thread, vm.username);

                              //------ Send Post to database here -----------------



                              // -----------------------------------------------------
                              $route.reload();
                          })
        }




        // lägger till forumThread till db
        vm.test = function () {
            vm.username = '';
            authentication.getCurrentUser()
                          .then(function (data) {
                              vm.username = data;
                              
                              var thread = {
                                  Title: 'Cheat cheat Title',
                                  PostTime: new Date()
                              }
                              forumThread.addThread(thread, vm.username);
                              $route.reload();
                          })
        }
    })

}());