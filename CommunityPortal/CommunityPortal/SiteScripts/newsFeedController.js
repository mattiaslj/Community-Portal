(function () {
    'use strict'

    var app = angular.module("communityPortal");


    app.controller("newsFeedController", function ($http, $route, $scope) {

        var vm = this;
        $scope.events = [];

        vm.title = '';
        vm.body = '';
        vm.id = '';
        //vm.role = '';
        vm.showEvents = true;
        vm.numberOfEventsToDisplay = 6;

        //  Get all events from database
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

            //------------- Pagination ------------------
            $scope.totalItems = vm.events.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 1;

            $scope.$watch("currentPage", function () {
                setPagingData($scope.currentPage);
            });

            function setPagingData(page) {
                var pagedData = vm.events.slice(
                  (page - 1) * $scope.itemsPerPage,
                  page * $scope.itemsPerPage
                );

                $scope.events = pagedData;
            }
            //------------------------------------------

        });

        // setup for addEvent form,
        // making sure it is empty when adding a new event
        vm.addEvent = function () {
            vm.showEvents = false;
            vm.title = '';
            vm.body = '';
            vm.id = '';
            vm.showEvents = false;
        }

        //  Add new event
        vm.submitForm = function () {
            // create event object
            vm.newEvent = {
                Title: vm.title,
                Body: vm.body,
                PostTime: new Date()
            }
            if (vm.id !== '') {
                vm.newEvent.Id = vm.id;
            }

            // post event object to server
            $http({
                method: 'POST',
                url: 'Event/AddEvent',
                data: vm.newEvent
            }).then(function (data) {
                $route.reload();
            })
            
        }

        // Delete Event
        vm.deleteEvent = function (event) {
            $http({
                method: 'POST',
                url: 'Event/DeleteEvent',
                data: event
            }).then(function (data) {
                $route.reload();
            })
            
        }

        // Edit Event
        vm.editEvent = function (evnt) {
            vm.title = evnt.Title;
            vm.body = evnt.Body;
            vm.id = evnt.Id;
            vm.showEvents = false;
        }
    });
}());

//Get user role
//vm.isAdmin = function () {
//    $http({
//        method: 'GET',
//        url: 'Event/GetRole'
//    }).then(function successCallback(response) {
//        vm.role = response.data;
//    })

//    if (vm.role === "Admin") {
//        return true;
//    }
//    return false;
//}