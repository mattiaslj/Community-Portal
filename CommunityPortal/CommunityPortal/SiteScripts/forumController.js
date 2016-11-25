(function () {
    'use strict'

    var app = angular.module('communityPortal');

    app.controller('forumController', function (forumThread, authentication, forumPost, $scope, $route) {

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


        // Create thread and post
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
                              forumThread.addThread(thread, vm.username).then(function (data) {
                                  //------ Send Post to database -----------------
                                  var threadId = data;
                                  console.log('addThread response data')
                                  console.log(data);

                                  var post = {
                                      Body : vm.body,
                                      PostTime: new Date()
                                  }

                                  forumPost.addPost(post, data, vm.username);
                                  // -----------------------------------------------------

                              })
                              $route.reload();
                          })
        }




        // lägger till forumThread till db
        // used for testing
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