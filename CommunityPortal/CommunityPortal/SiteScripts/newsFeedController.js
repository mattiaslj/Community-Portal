(function () {
    'use strict'

    var app = angular.module("communityPortal");


    app.controller("newsFeedController", function ($http, $route) {

        var vm = this;
        vm.title = '';
        vm.body = '';

        $http({
            method: 'GET',
            url: 'Event/GetEvents'
        }).then(function successCallback(response) {
            vm.events = angular.copy(response.data, vm.events);

            // parse all events datetime 
            for (var i = 0; i < vm.events.length; i++) {
                vm.events[i].PostTime = new Date(parseInt(vm.events[i].PostTime.substr(6)));
            }
            vm.events.reverse();
        });

        vm.submitForm = function () {
            // create event object
            vm.newEvent = {
                Title: vm.title,
                Body: vm.body,
                PostTime: new Date()
            }
            // post event object to server
            $http({
                method: 'POST',
                url: 'Event/AddEvent',
                data: vm.newEvent
            })
            $route.reload();
        }
    });


}());
