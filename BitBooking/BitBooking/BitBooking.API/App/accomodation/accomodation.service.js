(function () {
    angular.module("accomodationModule")
        .service("accomodationService", ["accomodationFactory", "accomodationSearchFactory", "$http", "$state", "$q", "growl", "UserService", function (accomodationFactory, accomodationSearchFactory, $http, $state, $q, growl, UserService) {

            var service = this;
            
            service.listOfAccomodations = [];
            service.accomodationDetails = getAccDetails;
            service.accomodationDetailsInfo = "";
            service.x = "";
            service.y = "";

            service.Message = "";
            service.SendMessage = SendMessage;

            function SendMessage() {

                //console.log("sent");
                //console.log(service.Message);
                //console.log($state.params.accomodationId);
                var dataTosend = { Content: service.Message, ReceiverId: $state.params.accomodationId, AccomodationId: $state.params.accomodationId, seen: false };

                $http.post("Messages/Create", dataTosend).then(function (response) {
                     service.Message = "Message Sent";

                    growl.success("Message Sent! Thank you for your paitience!");
                });

            };

            service.searchResults = [];
            service.search = search;
            service.getAccomodationsNames = getAccomodationsNames;
            service.accomodationNamesForSearch = [];
            service.accomodationNamesForSearchObject = [];

            service.accomodationCityForSearchObject = [];
            service.accomodationCountryForSearchObject = [];

            //service.SelectedAccomodationCityForSearch = "";
            //service.SelectedAccomodationCountryForSearch = "";

            service.SelectedAccomodationForSearch = "";
            service.SelectedAccomodationCityForSearch = "";
            service.SelectedAccomodationCountryForSearch = "";
            service.sponsored1list = [];
            service.sponsored2list=[];
            service.sponsored3list = [];
            service.numberOfPersonsToSearch = "";

            service.reserveRoom = reserveRoom;
            service.reserveRoomConfirmation = reserveRoomConfirmation;
            service.searchConfirmationResult = "";

            //TEMPORARY PROPERTIES FOR ROOM RESERVATION - START

            service.TemproomId = "";
            service.TempArrivalDate = "";
            service.TempDepartureDate = "";
            service.TempIsPaid = false;
            service.TempTotalPrice = "";
            service.TempUserId = "";
            service.TempPrice = "";

            service.numberOfStarsSearch = 0;
            service.totalPriceSearch = 0;


            service.AllAccomorationComments = []; //ARRAY OF ACCOMODATION COMMENTS

            service.optionsForSelect = [];
            service.optionsForSelectCountry = [];
            service.optionsForSelectAccommodation = [];

            service.filterCities = function (criteria) {
                service.optionsForSelect = [];
                if (service.SelectedAccomodationCountryForSearch.accomodationCountry === "All Countries" || service.SelectedAccomodationCountryForSearch.accomodationCountry == "") {
                    service.optionsForSelect = service.accomodationCityForSearchObject;
                    return;
                }
                angular.forEach(service.accomodationCityForSearchObject, function (value, key) {
                    if (service.SelectedAccomodationCountryForSearch.accomodationCountry == value.accomodationCountry) {
                        service.optionsForSelect.push(value);
                    }
                });
                service.optionsForSelect.push(new AccomodationCity("All Cities", "All Countries"))
            };

            service.filterCountries = function (criteria) {
                service.optionsForSelectCountry = [];

                if (service.SelectedAccomodationCityForSearch.accomodationCountry === "All Countries" || service.SelectedAccomodationCityForSearch.accomodationCountry === "") {
                    service.optionsForSelectCountry = service.accomodationCountryForSearchObject;
                    return;
                }
                angular.forEach(service.accomodationCountryForSearchObject, function (value, key) {
                    if (service.SelectedAccomodationCityForSearch.accomodationCountry == value.accomodationCountry) {
                        service.optionsForSelectCountry.push(value);
                    }
                });
            }


            service.filterAccommodation = function (criteria) {

                //console.log("ACC OPTIONS : ", service.optionsForSelectAccommodation);
                //console.log("ACC NAMESssss : ", service.accomodationNamesForSearchObject);
                service.optionsForSelectAccommodation = [];

                if (service.SelectedAccomodationCityForSearch.accomodationCity === "All Cities") {

                    if (service.SelectedAccomodationCountryForSearch.accomodationCountry == "All Countries" || service.SelectedAccomodationCountryForSearch.accomodationCountry == "") {
                        service.optionsForSelectAccommodation = service.accomodationNamesForSearchObject;
                        return;
                    }

                    angular.forEach(service.accomodationNamesForSearchObject, function (value, key){
                        if (value.accomodationCountry == service.SelectedAccomodationCountryForSearch.accomodationCountry) {
                            service.optionsForSelectAccommodation.push(value);
                        }
                    });
                    return;
                }

                //console.log("SELECTED COUNTRY: ", service.SelectedAccomodationCountryForSearch);
                //console.log("SELECTED COUNTRY NAMEssssssssssssssssssssss: ", service.SelectedAccomodationCountryForSearch == "");
                //console.log("SELECTED ACCOMODATION CITY FOR SEARCH: ",service.SelectedAccomodationCityForSearch.accomodationCity);
                //console.log("ACC NAMES FOR SEARCH OBJECT: ", service.accomodationNamesForSearchObject);

                if (service.SelectedAccomodationCountryForSearch == "All Countries" || service.SelectedAccomodationCountryForSearch == "") {
                    //console.log("u ifu", service.accomodationNamesForSearchObject);
                    service.optionsForSelectAccommodation = service.accomodationNamesForSearchObject;
                    return;
                }

                angular.forEach(service.accomodationNamesForSearchObject, function (value, key) {
                    if (service.SelectedAccomodationCityForSearch.accomodationCity == value.accomodationCity) {
                        service.optionsForSelectAccommodation.push(value);
                    }
                });
            }


            function getAccomodationsNames() {
                if (service.accomodationNamesForSearchObject.length > 0) {
                    return;
                }
                accomodationSearchFactory.query().$promise.then(function (result) {
                    service.accomodationNamesForSearchObject = [];
                    service.accomodationCountryForSearchObject = [];
                    service.accomodationCityForSearchObject = [];

                    var accCity1 = new AccomodationCity(result[0].AccomodationInfo.City, result[0].AccomodationInfo.Country);
                    service.accomodationCityForSearchObject.push(accCity1);

                    var accCountry1 = new AccomodationCountry(result[0].AccomodationInfo.Country);
                    service.accomodationCountryForSearchObject.push(accCountry1);

                    var acc1 = new Accomodation(result[0].AccomodationName, result[0].AccomodationInfo.City, result[0].AccomodationInfo.Country);
                    service.accomodationNamesForSearchObject.push(acc1);

                    for (var i = 1; i < result.length; i++) {
                        //CITY
                        var accCity2 = new AccomodationCity(result[i].AccomodationInfo.City, result[i].AccomodationInfo.Country);
                        var check = false;
                        angular.forEach(service.accomodationCityForSearchObject, function (current) {
                            if (current.accomodationCity == result[i].AccomodationInfo.City) {
                                check = true;
                            }
                        });
                        if (check != true) {
                            service.accomodationCityForSearchObject.push(accCity2);
                        }
                        //COUNTRY

                        var accCountry2 = new AccomodationCountry(result[i].AccomodationInfo.Country);
                        var check1 = false;
                        angular.forEach(service.accomodationCountryForSearchObject, function (current) {
                            if (current.accomodationCountry == result[i].AccomodationInfo.Country) {
                                check1 = true;
                            }
                        });
                        if (check1 != true) {
                            service.accomodationCountryForSearchObject.push(accCountry2);
                        }

                        var acc = new Accomodation(result[i].AccomodationName, result[i].AccomodationInfo.City, result[i].AccomodationInfo.Country);
                        service.accomodationNamesForSearchObject.push(acc);
                    }
                    service.accomodationCityForSearchObject.push(new AccomodationCity("All Cities", "All Countries"));
                    service.accomodationCountryForSearchObject.push(new AccomodationCountry("All Countries"));

                    service.optionsForSelect = service.accomodationCityForSearchObject;                 //FILTER DATA Cities
                    service.optionsForSelectCountry = service.accomodationCountryForSearchObject;       //FILTER DATA Countries
                    service.optionsForSelectAccommodation = service.accomodationNamesForSearchObject;   //FILTER DATA Accommodation Names
                });;
            };

           
            function search() {

                if (service.numberOfPersonsToSearch == "") {
                    growl.error("Please pick number of persons for reservation.");
                    return;
                }
                if (service.fromDate == "")
                {
                    growl.error("Please pick a date of arrival.");
                    return;
                }
                if (service.untilDate == "")
                {
                    growl.error("Please pick a date of departure.");
                    return;
                }

                if (service.SelectedAccomodationCityForSearch.accomodationCity == "") {
                    growl.error("Please pick city of destination.");
                    return;
                }
                if (service.SelectedAccomodationCountryForSearch.accomodationCountry == "") {
                    growl.error("Please pick country of destination.");
                    return;
                }

                if (service.SelectedAccomodationForSearch.accomodationName == "") {
                    growl.error("Please enter accomodation name or city.");
                    return;
                }

                if (service.SelectedAccomodationForSearch.accomodationName == null &&
                    service.SelectedAccomodationCountryForSearch.accomodationCountry == "All Countries" &&
                    service.SelectedAccomodationCityForSearch.accomodationCity == "All Cities")
                {
                    growl.error("Please pick at least one filter (country, city or accommodation name).", { ttl: 4000 });
                    return;
                }

                growl.success("Please wait for search results. Thank you for your paitience!");
                
                var dataForSearchingAccomodation = {
                    content: service.SelectedAccomodationForSearch.accomodationName,
                    SearchCity: service.SelectedAccomodationCityForSearch.accomodationCity,
                    SearchCountry: service.SelectedAccomodationCountryForSearch.accomodationCountry,
                    Arrival_date: service.fromDate,
                    Departure_date: service.untilDate,
                    peopleCapacity: service.numberOfPersonsToSearch,
                    NumberOfStarsSearch: service.numberOfStarsSearch,
                    TotalPriceSearch: service.totalPriceSearch
                };
                $http.post("/api/Search", dataForSearchingAccomodation).then(function (response) {
                    if (response.data.length == 0)
                    {
                        growl.error("There are not suitable rooms which satisfy your request. Please try again.");
                        return;
                    }
                    if (response.data.length == 1 && response.data[0].AccomodationId == 789) {
                        growl.error(response.data[0].AccomodationName, { ttl: 5000 });
                        return;
                    }
                    service.searchResults = response.data;
                    //console.log("FOUND ROOMS DATA: ",response.data);
                    $state.go("searchResults");
                }, function (error) {

                });
            }

            function reserveRoomConfirmation() {

                var dataForRoomReservationConfirmation = {
                    id: service.TemproomId,
                    ArrivalDate: service.TempArrivalDate,
                    DepartureDate: service.TempDepartureDate,
                    IsPaid: service.TempIsPaid,
                    TotalPrice: service.TempTotalPrice,
                    UserId: service.TempUserId,
                    Price: service.TempPrice
                };

                $http.post("/api/Search/PostReserveRoomConfirmation",
                    dataForRoomReservationConfirmation
                    ).then(function (response) {
                        growl.success("Congratulations, You have reserved a room. Please see more details in your profile page.");
                        UserService.showReservations();
                        $state.go("profile");
                    }, function (error) {
                        growl.error(error.data.Message);
                    });
            }

            function reserveRoom($index) {
                
                service.TemproomId = service.searchResults[$index].RoomId;
                service.TempArrivalDate = service.searchResults[$index].TempArrivalDate;
                service.TempDepartureDate = service.searchResults[$index].TempDepartureDate;
                service.TempIsPaid = false;
                service.TempTotalPrice = service.searchResults[$index].TempTotalPrice;
                service.TempUserId = service.searchResults[$index].TempUserId;
                service.TempPrice = service.searchResults[$index].Price;

                var dataForRoomReservation = {
                    id: service.TemproomId,
                    ArrivalDate: service.TempArrivalDate,
                    DepartureDate: service.TempDepartureDate,
                    IsPaid: service.TempIsPaid,
                    TotalPrice: service.TempTotalPrice,
                    UserId: service.TempUserId,
                    Price: service.TempPrice
                };
                $http.post("/api/Search/PostReserveRoom",
                    dataForRoomReservation
                    ).then(function (response) {
                    service.searchConfirmationResult = response.data;
                    $state.go("searchResultsConfirmation");
                }, function (error) {

                });
            }


            function Accomodation(name,city,country) {
                this.accomodationName = name;
                this.accomodationCity = city;
                this.accomodationCountry = country;
            };

            function AccomodationCity(city,country) {
                this.accomodationCity = city;
                this.accomodationCountry = country;
            };

            function AccomodationCountry(name) {
                this.accomodationCountry = name;
            };

            getAccomodations();

            function getAccomodations() {
                accomodationFactory.query().$promise.then(function (result) {

                    service.listOfAccomodations = result;
                });;

                $http.get("Accomodations/SponsoredList/1").then(function (result) { service.sponsored1list=result.data; })
                $http.get("Accomodations/SponsoredList/2").then(function (result) { service.sponsored2list=result.data; })
                $http.get("Messages/hasnew/").then(function (result) {
                    UserService.hasNewMessage = result.data;
                    //console.log(result);
                })
                $http.get("Accomodations/SponsoredList/3").then(function (result) { service.sponsored3list = result.data; })

            };

            service.TemporaryAccomodationId = "";

            




            function getAccDetails(id) {
                //console.log("GET ACC DETAILS ID:", id);
                service.accomodationDetailsInfo = accomodationFactory.get({ id: id });
                //console.log("ACC DETAILS INFO: ", service.accomodationDetailsInfo);
            };

            service.getAllAccomodationComments = getAllAccomodationComments;
            service.AverageUserCommentRating = 0;

            service.totalComments = 0;

            service.rating5commentsCount = 0;
            service.rating4commentsCount = 0;
            service.rating3commentsCount = 0;
            service.rating2commentsCount = 0;
            service.rating1commentsCount = 0;

            service.rating5Precentage = 0;
            service.rating4Precentage = 0;
            service.rating3Precentage = 0;
            service.rating2Precentage = 0;
            service.rating1Precentage = 0;


            function getAllAccomodationComments() {
                service.Message = "";
                var dataToSend = {
                    AccomodationId: service.TemporaryAccomodationId
                };
                $http.post("/api/UserCommentsAPI/PostCommentFromAccomodation",
                    dataToSend
                    )
                    .then(function (response) {
                        service.AllAccomorationComments = response.data;

                        if (service.AllAccomorationComments.length === 0)
                        {
                            service.AverageUserCommentRating = 0;

                            service.totalComments = 0;
                            service.rating5commentsCount = 0;
                            service.rating4commentsCount = 0;
                            service.rating3commentsCount = 0;
                            service.rating2commentsCount = 0;
                            service.rating1commentsCount = 0;

                            service.rating5Precentage = 0;
                            service.rating4Precentage = 0;
                            service.rating3Precentage = 0;
                            service.rating2Precentage = 0;
                            service.rating1Precentage = 0;
                        }
                        else {

                        
                        //console.log("ALL COMMENTS:", response.data);
                        service.totalComments = response.data.length;   //TOTAL COMMENTS

                        service.rating5commentsCount = 0;
                        service.rating4commentsCount = 0;
                        service.rating3commentsCount = 0;
                        service.rating2commentsCount = 0;
                        service.rating1commentsCount = 0;

                        service.rating5Precentage = 0;
                        service.rating4Precentage = 0;
                        service.rating3Precentage = 0;
                        service.rating2Precentage = 0;
                        service.rating1Precentage = 0;

                        service.AverageUserCommentRating = 0;

                        for(var i = 0; i < response.data.length; i++)
                        {
                            service.AverageUserCommentRating += response.data[i].Rating;
                            if(response.data[i].Rating == 5)
                            {
                                service.rating5commentsCount += 1;
                            }
                            else if (response.data[i].Rating == 4) {
                                service.rating4commentsCount += 1;
                            }
                            else if (response.data[i].Rating == 3) {
                                service.rating3commentsCount += 1;
                            }
                            else if (response.data[i].Rating == 2) {
                                service.rating2commentsCount += 1;
                            }
                            else if (response.data[i].Rating == 1) {
                                service.rating1commentsCount += 1;
                            }
                        }

                        service.rating5Precentage = (service.rating5commentsCount / service.totalComments ) * 100;
                        service.rating4Precentage = (service.rating4commentsCount / service.totalComments) * 100;
                        service.rating3Precentage = (service.rating3commentsCount / service.totalComments) * 100;
                        service.rating2Precentage = (service.rating2commentsCount / service.totalComments) * 100;
                        service.rating1Precentage = (service.rating1commentsCount / service.totalComments) * 100;

                        //console.log("Rating 5 :", service.rating5Precentage);
                        //console.log("Rating 4 :", service.rating4Precentage);
                        //console.log("Rating 3 :", service.rating3Precentage);
                        //console.log("Rating 2 :", service.rating2Precentage);
                        //console.log("Rating 1 :", service.rating1Precentage);


                        service.AverageUserCommentRating = service.AverageUserCommentRating / response.data.length;
                            //console.log("AVG RATING: ", service.AverageUserCommentRating);
                        }

                    }, function (error) {
                       //console.log("ERROR ALL COMMENTS: ",error);
                    });
            }

            //COMMENTS REGION

            service.UserCommentId = "";
            service.Comment = "";
            service.ApplicationUserId = "";
            service.UserName = "";
            service.Rating = "";
            service.ReportCount = "";
            service.AccomodationId = "";

            //STAR RATING START
            service.starRating = 1;

            service.hoverRating = 0;


            service.click = function (param) {
                service.starRating = param;
            };

            service.mouseHover = function (param) {
                //console.log('mouseHover(' + param + ')');
                service.hoverRating = param;
            };

            service.mouseLeave = function (param) {
                //console.log('mouseLeave(' + param + ')');
                service.hoverRating = param + '*';
            };
            //STAR RATING END

            service.CreateComment = CreateComment;

            function CreateComment() {
                //console.log("AccomodationID: ", service.TemporaryAccomodationId);
                //console.log("Comment: ", service.Comment);
                //console.log("Rating: ", service.starRating);
                var userComment = {
                    AccomodationId: service.TemporaryAccomodationId,
                    Rating: service.starRating,
                    Comment: service.Comment
                };

                $http.post("/api/UserCommentsAPI/PostComments", userComment)
                    .then(function (response) {
                        growl.success(response.data);
                        getAllAccomodationComments();
                    }, function (error) {
                        growl.error(error.data.Message);
                    });
            };


            service.ReportComment = ReportComment;

            function ReportComment($index) {
                var CommentId = service.AllAccomorationComments[$index].UserCommentId;
                //console.log("COMMENT ID: ", CommentId);
                var ApplicationUserId = service.AllAccomorationComments[$index].ApplicationUserId;
                //console.log("APPLICATION USER ID: ",ApplicationUserId);
                var AccomodationId = service.AllAccomorationComments[$index].AccomodationId;
                //console.log("ACCOMODATION ID: ", AccomodationId);

                var commentToReport = {
                    CommentId: CommentId,
                    ApplicationUserId: ApplicationUserId,
                    AccomodationIdValue: AccomodationId
                };

                $http.post("/api/UserCommentsAPI/PostReport", commentToReport)
                .then(function (response) {
                    //console.log("SUCCESS:");
                    //console.log(response);
                    growl.success(response.data);
                }, function (error) {
                    //console.log("ERROR");
                    //console.log(error);
                    growl.error(error.data.Message);
                });
            };

            //DATE PICKER
            service.selectedDate = "2015-10-12T13:56:07.104Z"; // <- [object Date]
            service.selectedDateAsNumber = 509414400000; // <- [object Number]
            service.fromDate = ""; // <- [object Undefined]
            service.untilDate = ""; // <- [object Undefined]

        }]);
})();