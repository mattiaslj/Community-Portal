(function(){
    'use strict'

    var app = angular.module('communityPortal');

    app.factory('chat', function ($http) {

        this.getMessages = function (currentUser, username) {
            return $http.post('Chat/GetMessages', { currentUser, username })
                .then(function (data) {
                    return data;
                });
        };

        return {
            getMessages: this.getMessages
        };
    });

}());
   