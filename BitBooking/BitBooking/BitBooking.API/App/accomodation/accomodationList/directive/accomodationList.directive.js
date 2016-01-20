(function () {
    angular.module("accomodationModule")
        .directive("accomodationListDirective", function () {
            return {
                templateUrl: "/App/accomodation/accomodationList/template/accomodationList.html",
                controller: "accomodationListController as accomodationListCtrl"
            }
        });
})();