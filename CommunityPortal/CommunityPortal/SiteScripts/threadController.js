(function () {

    var app = angular.module('communityPortal');

    app.controller('threadController', function (forumPost, authentication, $scope, $route, $routeParams, $location
                                                , $anchorScroll) {
        var vm = this;
        $scope.forumPosts = [];
        vm.threadId = $routeParams.threadId.replace(':', '');

        vm.replyId = '';
        vm.body = '';

        var threadTitle = $routeParams.threadTitle;
        vm.threadTitle = threadTitle.replace(':', '');

        //Get all threads and add pagination
        vm.forumPosts = {};
        console.log($routeParams.threadId)
        forumPost.getPosts(vm.threadId)
                   .then(function (data) {
                       vm.forumPosts = JSON.parse(data);
                       //------------- Pagination ------------------
                       $scope.totalItems = vm.forumPosts.length;
                       $scope.currentPage = 1;
                       $scope.itemsPerPage = 25;

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
                                  PostTime: new Date(),
                                  ReplyPostId: vm.replyId
                              }

                              forumPost.addPost(post, vm.threadId, data).then(function (data) {
                                  $route.reload();
                              })
                          })
        }

        // ser inte bra ut...
        vm.quote = function (id) {
            var quote = $("#postId_" + id).text();
            var reply = '<i>' + quote + '</i>';
            $("#postTextArea").text(reply.trim());
        }

        vm.reply = function (id) {
            vm.quote(id);
            vm.replyId = id;
        }


        vm.scrollToReply = function (id, postId) {
            vm.reply(postId);
            var old = $location.hash();
            $location.hash(id);
            $anchorScroll();
            //reset to old to keep any additional routing logic from kicking in
            $location.hash(old);
        };

        // Get offset for posts in thread
        vm.getOffset = function(post){
            var parentPost = getParent(post);
            var offsetCounter = 1;
            while (parentPost.ReplyPostId != 0) {
                offsetCounter++;
                for (var i = 0; i < vm.forumPosts.length; i++) {
                    if (vm.forumPosts[i].Id === parentPost.ReplyPostId) {
                        parentPost = vm.forumPosts[i];
                    }
                }
            }
            if (offsetCounter > 6) {
                return offsetCounter * 10;
            }
            return offsetCounter * 10;
        };

        var getParent = function (post) {
            var parentPost = {
                    ReplyPostId: 0
            }
            for (var i = 0; i < vm.forumPosts.length ; i++) {
                console.log(vm.forumPosts[i].Id + ': ' + vm.forumPosts[i].ReplyPostId)
                if (vm.forumPosts[i].Id === post.ReplyPostId) {
                    parentPost = vm.forumPosts[i];
                    return parentPost;
                }
            }
            return parentPost;
        }
    });
}());