(function () {
    angular.module("accomodationModule")
    .factory("accomodationFactory", ["$resource", function ($resource) {
        return $resource("/api/AccomodationsTest/:id");
    }])
    .factory("accomodationSearchFactory", ["$resource", function ($resource) {
        return $resource("/api/AccomodationSuggestion/:id");
    }])
    .factory("photosFactory", ["$resource", function ($resource) {
        return $resource("/api/PhotosApi/",{other: '@other'});
    }]);
})();