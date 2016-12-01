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

        // Get userRole
        authentication.getUserRole()
                      .then(function (data) {
                          vm.userRole = data;
                          console.log(vm.userRole);
                      })
        console.log(vm.userRole);

        //Get all posts and add pagination
        vm.forumPosts = [];
        forumPost.getPosts(vm.threadId)
                   .then(function (data) {
                       vm.forumPosts = JSON.parse(data);
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
                                  PostTime: new Date(),
                                  ReplyPostId: vm.replyId
                              }

                              forumPost.addPost(post, vm.threadId, data).then(function (data) {
                                  $route.reload();
                              })
                          })
        }

        vm.deletepost = function (post) {
            forumPost.deletePost(post).then(function (data) {
                console.log(data);
                $route.reload();
            })
        };

        vm.quote = function (ReplyPostId, postId, user) {
            if (ReplyPostId === 0) {
                $('#postQuote_' + postId).remove();
                $('#showButton_' + postId).remove();

                return 0;
            }
            if ($("#postId_" + ReplyPostId).text() != 0) {
                var quote = $("#postId_" + ReplyPostId).text().trim();
                var reply = 'Posted by ' + user + '\n\n' + quote;
                return reply;
            } else {
                return '';
            }
        }

        vm.reply = function (id) {
            vm.replyId = id;
        }

        vm.selectedPostId = 0;

        vm.scrollToReply = function (id, postId, user) {
            vm.selectedPostId = postId;
            // set replypostId for use in addPost
            vm.reply(postId);
            // set reply text to show user
            vm.replyCheat = vm.quote(postId, postId, user);

            // scrolling
            var old = $location.hash();
            $location.hash(id);
            $anchorScroll();
            //reset to old to keep any additional routing logic from kicking in
            $location.hash(old);
        };

        vm.removeReply = function () {
            vm.reply(0);
            vm.replyCheat = '';
        }

        vm.slide = function (id) {
            $("#postQuote_" + id).slideToggle();
        }

        //        Might not use - doesnt look good and has really bad performance, gets triggered on every keydown in form
        //
        // Get offset for posts in thread
        //vm.getOffset = function (post) {
        //        var parentPost = getParent(post);
        //        var offsetCounter = 1;
        //        while (parentPost.ReplyPostId != 0) {
        //            offsetCounter++;
        //            for (var i = 0; i < vm.forumPosts.length; i++) {
        //                if (vm.forumPosts[i].Id === parentPost.ReplyPostId) {
        //                    parentPost = vm.forumPosts[i];
        //                }
        //            }
        //        }
        //        if (offsetCounter > 6) {
        //            return offsetCounter * 10;
        //        }
        //        return offsetCounter * 10;
        //};
        //
        //var getParent = function (post) {
        //    var parentPost = {
        //        ReplyPostId: 0
        //    }
        //    for (var i = 0; i < vm.forumPosts.length ; i++) {
        //        console.log(vm.forumPosts[i].Id + ': ' + vm.forumPosts[i].ReplyPostId)
        //        if (vm.forumPosts[i].Id === post.ReplyPostId) {
        //            parentPost = vm.forumPosts[i];
        //            return parentPost;
        //        }
        //    }
        //    return parentPost;
        //}
    });
}());