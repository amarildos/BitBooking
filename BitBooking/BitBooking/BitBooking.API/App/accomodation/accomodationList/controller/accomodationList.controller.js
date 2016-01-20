(function () {
    angular.module("accomodationModule")
        .controller("accomodationListController", ["accomodationService", "$scope", function (accomodationService, $scope) {
            var ctrl = this;
            ctrl.service = accomodationService;

            ctrl.service.getAccomodationsNames();

        }]);
})();