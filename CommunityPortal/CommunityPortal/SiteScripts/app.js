

(function () {
    'use strict'

    var app = angular.module("communityPortal", ['ngRoute']);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/newsFeed/", {
                templateUrl: 'Content/angularHtml/newsFeed.html',
                controller: 'newsFeedController as vm'
            })
            .otherwise({ redirectTo: '/newsFeed' });
    });
}());