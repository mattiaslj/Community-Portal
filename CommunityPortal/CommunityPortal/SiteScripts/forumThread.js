(function () {

    var app = angular.module('communityPortal');

    app.factory('forumThread', function ($http) {

        this.getThreads = function () {
            return $http.get('Thread/GetThreads')
                        .then(function (response) {
                            return response.data;
                        });
        };

        this.getThread = function (forumThread) {
            return $http.get('Thread/GetThread', forumThread)
                        .then(function (response) {
                            return response.data;
                        });
        };

        this.addThread = function (forumThread, username) {
            return $http.post('Thread/AddThread', {forumThread, username})
                        .then(function (response) {
                            return response.data;
                        });
        };


        return {
            getThreads: this.getThreads,
            getThread: this.getThread,
            addThread: this.addThread
        }
    });
}());