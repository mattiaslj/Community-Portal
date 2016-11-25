(function () {

    var app = angular.module('communityPortal');

    app.controller('threadController', function (forumPost, authentication, $scope, $route, $routeParams) {
        var vm = this;
        $scope.forumPosts = [];
        vm.id = $routeParams.threadId.replace(':', '');
        vm.body = '';

        var threadTitle = $routeParams.threadTitle;
        vm.threadTitle = threadTitle.replace(':', '');


        //Get all threads and add pagination
        vm.forumPosts = {};
        console.log($routeParams.threadId)
        forumPost.getPosts($routeParams.threadId.replace(':',''))
                   .then(function (data) {
                       vm.forumPosts = JSON.parse(data);
                       //vm.forumPosts = vm.forumPosts.reverse();
                       //------------- Pagination ------------------
                       $scope.totalItems = vm.forumPosts.length;
                       $scope.currentPage = 1;
                       $scope.itemsPerPage = 10;

                       $scope.$watch("currentPage", function () {
                           setPagingData($scope.currentPage);
                       });

                       function setPagingData(page) {
                           var pagedData = vm.forumPosts.slice(
                             (page - 1) * $scope.itemsPerPage,
                             page * $scope.itemsPerPage
                           );

                           $scope.forumPosts = pagedData;
                       }
                       //------------------------------------------
                   });


        vm.createPost = function () {
            vm.username = '';
            authentication.getCurrentUser()
                          .then(function (data) {
                              var post = {
                                  Body: vm.body,
                                  PostTime: new Date()
                              }

                              forumPost.addPost(post, vm.id, data);
                              $route.reload();
                          })

        }

    });
}());