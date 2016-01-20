(function () {
    angular.module("accomodationModule")
        .controller("accomodationDetailsController", ["accomodationService", "$state", function (accomodationService, $state) {
            var ctrl = this;
          
            ctrl.service = accomodationService;
            ctrl.service.accId = $state.params.accomodationId;
            ctrl.x = accomodationService.x;
            ctrl.y = accomodationService.y;
            accomodationService.accomodationDetails($state.params.accomodationId);
            ctrl.accomodation = accomodationService.accomodationDetailsInfo;

            accomodationService.TemporaryAccomodationId = $state.params.accomodationId;
 
        }]);
})();