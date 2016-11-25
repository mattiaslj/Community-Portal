
(function () {
    'use strict'
    var app = angular.module('communityPortal');

    app.factory('forumPost', function ($http) {

        this.getPosts = function (threadId) {
            return $http.post('ForumPost/GetPosts', { id: threadId })
                        .then(function (response) {
                            return response.data;
                        });
        }

        this.addPost = function (forumPost, threadId, username) {
            return $http.post('ForumPost/AddPost', { forumPost, threadId, username })
                        .then(function (response) {
                            return response.data;
                        });
        }


        return {
            getPosts: this.getPosts,
            addPost: this.addPost
        }
    });

}());


