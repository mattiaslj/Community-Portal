

(function () {
    'use strict'

    var app = angular.module("communityPortal", ['ngRoute', 'ui.bootstrap']);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/newsFeed/", {
                templateUrl: 'Content/angularHtml/newsFeed.html',
                controller: 'newsFeedController as vm'
            })
            .when("/forum", {
                templateUrl: "Content/angularHtml/forum.html",
                controller: "forumController as vm"
            })
            .when("/forum/:threadTitle/:threadId", {
                templateUrl: "Content/angularHtml/thread.html",
                controller: "threadController as vm"
            })
            .when("/user/:userName", {
                templateUrl: "Content/angularHtml/userPage.html",
                controller: "userPageController as vm"
            })
            .otherwise({ redirectTo: '/newsFeed' });
    });

}());