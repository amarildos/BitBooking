(function () {
    //Main module of our app
    //'use strict';
    var app = angular.module("app",
        [
        "ngSanitize",
        "ngAnimate",
        "ngQuantum",
        "mgcrea.ngStrap.datepicker",
        'ngRoute',
        'ngCookies',
        "ngResource",
        "ui.router",
        "jcs-autoValidate",
        "angular-growl",
        "accomodationModule",
        "ngMap",
        "countrySelect",
        "angularFileUpload",
        "datatables",
        
        "slick"
              ]);

    app.directive('ngThumb', ['$window', function ($window) {
        var helper = {
            support: !!($window.FileReader && $window.CanvasRenderingContext2D),
            isFile: function (item) {
                return angular.isObject(item) && item instanceof $window.File;
            },
            isImage: function (file) {
                var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        };

        return {
            restrict: 'A',
            template: '<canvas/>',
            link: function (scope, element, attributes) {
                if (!helper.support) return;

                var params = scope.$eval(attributes.ngThumb);

                if (!helper.isFile(params.file)) return;
                if (!helper.isImage(params.file)) return;

                var canvas = element.find('canvas');
                var reader = new FileReader();

                reader.onload = onLoadFile;
                reader.readAsDataURL(params.file);

                function onLoadFile(event) {
                    var img = new Image();
                    img.onload = onLoadImage;
                    img.src = event.target.result;
                }

                function onLoadImage() {
                    var width = params.width || this.width / this.height * params.height;
                    var height = params.height || this.height / this.width * params.width;
                    canvas.attr({ width: width, height: height });
                    canvas[0].getContext('2d').drawImage(this, 0, 0, width, height);
                }
            }
        };
    }]);

    //angular.module('ngQuantum.carousel').config(function($carouselProvider) {
    //    var mydefaults = {
    //        outerWidth:'2000px',
    //        innerHeight:'500px'
    //        }
    //        angular.extend($carouselProvider.defaults, mydefaults)
    //});

    angular.module('app').config(function ($carouselProvider) {
        var mydefaults = {
            outerWidth: '2000px',
            innerHeight: '500px'
        }
        angular.extend($carouselProvider.defaults, mydefaults)
    });


   

    app.directive('ngReallyClick', [function() {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                element.bind('click', function() {
                    var message = attrs.ngReallyMessage;
                    if (message && confirm(message)) {
                        scope.$apply(attrs.ngReallyClick);
                    }
                });
            }
        }
    }]);

    app.controller('PhotoControllerForRoomPhotos', ['$scope', 'FileUploader', 'accomodationService', "SellerService", "$state","growl", 
    function ($scope, FileUploader, accomodationService, SellerService, $state, growl) {
        // Uploader Plugin Code
        var self = this;
        self.service = SellerService;
        $scope.rooms = SellerService.sellerRooms2;
        //console.log("ROOMS: ", $scope.rooms);

        var accomodationId = $scope.rooms[0].AccomodationId;
       
        var newRoomTypeId = $state.params.roomTypeId;
        self.service.UpdatePhoto();

        var uploader = $scope.uploader = new FileUploader({
            url: window.location.protocol + '//' + window.location.host +
                 window.location.pathname + '/api/Upload/UploadFile'
        });

        // FILTERS

        uploader.filters.push({
            name: 'extensionFilter',
            fn: function (item, options) {
                var filename = item.name;
                var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                if (extension == "png" || extension == "tiff" || extension == "gif" ||
                    extension == "bmp" ||
                    extension == "jpg" ||
                    extension == "jpeg")
                    return true;
                else {
                    growl.warning('Invalid photo format. Please select a photo with png,tiff,bmp,jpg or jpeg format and try again.');
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'sizeFilter',
            fn: function (item, options) {
                var fileSize = item.size;
                fileSize = parseInt(fileSize) / (1024 * 1024);
                if (fileSize <= 5)
                    return true;
                else {
                    growl.warning('Selected photo exceeds the 5MB file size limit. Please choose a new photo and try again.');
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'itemResetFilter',
            fn: function (item, options) {
                if (this.queue.length < 1)
                    return true;
                else {
                    growl.warning('You have exceeded the limit of uploading photos.');
                    return false;
            }
        }
        });

        // CALLBACKS

        uploader.onWhenAddingFileFailed = function (item, filter, options) {
            console.info('onWhenAddingFileFailed', item, filter, options);
        };
        uploader.onAfterAddingFile = function (fileItem) {
            //alert('Files ready for upload.');
            growl.success("Photo is ready for upload.");
        };

        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            $scope.uploader.queue = [];
            $scope.uploader.progress = 0;

            //alert('Selected file has been uploaded successfully.');
            growl.success("Selected photo has been uploaded successfully. Feel free to add more photos!");
            $state.go("manager");
        };
        uploader.onErrorItem = function (fileItem, response, status, headers) {
            //alert('We were unable to upload your file. Please try again.');
            growl.warning("We were unable to upload your file. Please try again.");
        };
        uploader.onCancelItem = function (fileItem, response, status, headers) {
            //alert('File uploading has been cancelled.');
            growl.warning("File uploading has been cancelled.");
        };


        uploader.onAfterAddingAll = function (addedFileItems) {
        };
        
        uploader.onBeforeUploadItem = function (item) {
            growl.success("Photo is uploading. Please wait...");
            //console.log("CCCC ", item.SelectedType);
            //console.log("BBBB ", accomodationId);
            var urlForPhotos = "/api/UploadPhotos/?RoomTypeId=" + newRoomTypeId + "&AccomodationId=" + accomodationId;
            self.service.UpdatePhoto();
            item.url = urlForPhotos;
            //console.info('onBeforeUploadItem ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ', item);
        };

        uploader.onProgressItem = function (fileItem, progress) {
            //console.info('onProgressItem', fileItem, progress);
        };

        uploader.onProgressAll = function (progress) {
            //console.info('onProgressAll', progress);
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            //console.info('onCompleteItem', fileItem, response, status, headers);
            self.service.UpdatePhoto();
        };
        uploader.onCompleteAll = function () {
            //console.info('onCompleteAll');
            self.service.UpdatePhoto();
        };

        //console.info('uploader', uploader);
    }]);

    app.controller('PhotoControllerForFacilityPhotos', ['$scope', 'FileUploader', 'accomodationService', "SellerService", "$state", "growl",
        function ($scope, FileUploader, accomodationService, SellerService, $state, growl) {
        // Uploader Plugin Code
        var self = this;
        self.service = SellerService;
        $scope.rooms = SellerService.sellerRooms2;
        //console.log("ROOMS: ", $scope.rooms);

        var accomodationId = $scope.rooms[0].AccomodationId;

        var facilityId = $state.params.facilityId;

        var uploader = $scope.uploader = new FileUploader({
            url: window.location.protocol + '//' + window.location.host +
                 window.location.pathname + '/api/Upload/UploadFile'
        });

        // FILTERS

        uploader.filters.push({
            name: 'extensionFilter',
            fn: function (item, options) {
                var filename = item.name;
                var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                if (extension == "png" || extension == "tiff" || extension == "gif" ||
                     extension == "bmp" ||
                     extension == "jpg" ||
                     extension == "jpeg")
                    return true;
                else {
                    alert('Invalid file format. Please select a file with pdf/doc/docs or rtf format and try again.');
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'sizeFilter',
            fn: function (item, options) {
                var fileSize = item.size;
                fileSize = parseInt(fileSize) / (1024 * 1024);
                if (fileSize <= 5)
                    return true;
                else {
                    alert('Selected file exceeds the 5MB file size limit. Please choose a new file and try again.');
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'itemResetFilter',
            fn: function (item, options) {
                if (this.queue.length < 1)
                    return true;
                else {
                    alert('You have exceeded the limit of uploading files.');
                    return false;
                }
            }
        });

        // CALLBACKS

        uploader.onWhenAddingFileFailed = function (item, filter, options) {
            console.info('onWhenAddingFileFailed', item, filter, options);
        };
        uploader.onAfterAddingFile = function (fileItem) {
            //alert('Files ready for upload.');
            growl.success("Facility photo is ready for upload.");
        };

        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            $scope.uploader.queue = [];
            $scope.uploader.progress = 0;
            growl.success("Selected facility photo has been uploaded successfully. Feel free to add more photos!");
            $state.go("sellerFacilities");
        };
        uploader.onErrorItem = function (fileItem, response, status, headers) {
            //alert('We were unable to upload your file. Please try again.');
            growl.warning("We were unable to upload your photo. Please try again.");
        };
        uploader.onCancelItem = function (fileItem, response, status, headers) {
            //alert('File uploading has been cancelled.');
            growl.warning("File uploading has been cancelled.");
        };


        uploader.onAfterAddingAll = function (addedFileItems) {
            //console.log("BBBB ", accomodationId);
            //console.log("FACILITY ID: ", facilityId);
        };

        uploader.onBeforeUploadItem = function (item) {
            //console.log("BBBB ", accomodationId);
            //console.log("FACILITY ID: ", facilityId);
            var urlForPhotos = "/api/UploadPhotosFacility/?facilityId=" + facilityId + "&AccomodationId=" + accomodationId;
            self.service.UpdatePhoto();
            item.url = urlForPhotos;
            console.info('onBeforeUploadItem ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ', item);
        };

        uploader.onProgressItem = function (fileItem, progress) {
            console.info('onProgressItem', fileItem, progress);
        };

        uploader.onProgressAll = function (progress) {
            console.info('onProgressAll', progress);
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            console.info('onCompleteItem', fileItem, response, status, headers);
        };
        uploader.onCompleteAll = function () {
            console.info('onCompleteAll');
        };

        console.info('uploader', uploader);
    }]);


    //dodano "angular-growl", "ngAnimate" zbog growl notifikacija                                                                         
    app.run(['defaultErrorMessageResolver',function (defaultErrorMessageResolver) {
        // passing a culture into getErrorMessages('fr-fr') will get the culture specific messages
        // otherwise the current default culture is returned.
        defaultErrorMessageResolver.getErrorMessages().then(function (errorMessages) {
            errorMessages['invalidPassword'] = "Invalid password - Password length - <br/> Required 6 characters (at least one<br/> special character, one<br/> uppercase letter and <br/> a number.)";
            errorMessages['invalidEmail'] = "Please enter valid email address.";
            errorMessages['invalidTimeFormat'] = "Please enter valid time format HH-MM-AM/PM.";
        });

    }]);

    app.filter("jsDate", function () {
        return function (x) {
            return new Date(parseInt(x.substr(6)));
        };
    });
    app.config(function ($datepickerProvider) {
        angular.extend($datepickerProvider.defaults, {
            dateFormat: 'dd/MM/yyyy',
            timezone: "UTC",
            startWeek: 1
        });
    })
    app.controller('EventArgumentsCtrl', function ($scope, $http) {
        var map;
        var temp;
        var markers = [];
        $scope.click = function (event) {
            map.setZoom(8);
            //map.setCenter(marker.getPosition());
        }
        $scope.$on('mapInitialized', function (evt, evtMap) {
            map = evtMap;
            $scope.placeMarker = function (e) {
                for (var i = 0; i < markers.length; i++) {
                    markers[i].setMap(null);
                }
                //console.log(e);
                markers.push(new google.maps.Marker({ position: e.latLng, map: map }));
                //console.log(markers[markers.length - 1]);
                alert("Location Changed!");
               // debugger;
                var dataTosend = { x: e.latLng.lat(), y: e.latLng.lng() };
                $http.post("/AccomodationInfoes/UpdateValues",dataTosend );
               //temp.setMap(null);
              // temp = marker;
                
               // map.panTo(e.latLng);


              //  console.log(marker);


            }
        });
    });

 
    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/");

        $stateProvider

        .state("home", {
            url: "/",
            templateUrl: "/App/accomodation/AccomodationList/template/accomodationList.html",
            controller: "accomodationListController",
            controllerAs: "accomodationListCtrl",
            resolve: {
                user: function (accomodationService) {
                    return accomodationService.getAccomodationsNames();
                }
            }
        })
              .state("sellerRooms", {
                  url: "/rooms",
                  templateUrl: "/App/accomodation/templates/rooms.html",
                  controller: "sellerRoomController",
                  controllerAs: "SRCtrl"
              })
            .state("sellerInfoEdit", {
                url: "/InfoEdit",
                templateUrl: "/App/accomodation/templates/sellerInfoEdit.html",
                controller: "sellerRoomController",
                controllerAs: "SRCtrl"
            })


            .state("inbox", {
                url: "/Inbox/",
                views: {
                    "": {
                        templateUrl: "/App/accomodation/templates/inbox.html",
                        controller: "LoginCtrl",
                        controllerAs: "logCtrl",
                    },
                    "accomodationComments@inbox": {
                        templateUrl: "/App/accomodation/accomodationComments/template/accomodationComments.html",
                        controller: "accomodationCommentsController",
                        controllerAs: "accomodationCommentsCtrl"
                    },
                    "allAccomodationComments@inbox": {
                        templateUrl: "/App/accomodation/accomodationComments/template/allAccomodationComments.html",
                        controller: "accomodationCommentsController",
                        controllerAs: "accomodationCommentsCtrl"
                    }
                }
            })

              .state("sellerPrice", {
                  url: "/SpecialPrices",
                  templateUrl: "/App/accomodation/templates/priceSeller.html",
                  controller: "sellerRoomController",
                  controllerAs: "SRCtrl"
              })
            .state("sellerServices", {
                url: "/Services",
                templateUrl: "/App/accomodation/templates/servicesSeller.html",
                controller: "sellerRoomController",
                controllerAs: "SRCtrl"
            })
              .state("newRoom", {
                url: "/rooms/Add",
                templateUrl: "/Rooms/Create",
                controller: "sellerRoomController",
                controllerAs: "SRCtrl"
              })
                 .state("sellerJobNew", {
                     url: "/newJob",
                     templateUrl: "/Jobs/Create",
                     controller: "sellerRoomController",
                     controllerAs: "SRCtrl"
                 })
               .state("editRoom", {
                   url: "/editRoom/:roomId",
                   templateUrl: function ($stateParams, SellerService) {
                      
                       return 'Rooms/Edit/' + $stateParams.roomId + '';
                   },
                   controller: "sellerRoomController",
                   controllerAs: "SRCtrl"
               })
                   .state("editJob", {
                       url: "/editJob/:jobId",
                       templateUrl: function ($stateParams, SellerService) {

                           return 'Jobs/Edit/' + $stateParams.jobId + '';
                       },
                       controller: "sellerRoomController",
                       controllerAs: "SRCtrl"
                   })
            .state("editFacility", {
                url: "/editFacility/:facId",
                templateUrl: function ($stateParams, SellerService) {

                    return 'AccomodationFacilities/Edit/' + $stateParams.facId + '';
                },
                controller: "sellerRoomController",
                controllerAs: "SRCtrl"
            })
               .state("sellerServicesNew", {
                   url: "/ServicesNew",
                   templateUrl: "/AccomodationServices/Create",
                   controller: "sellerRoomController",
                   controllerAs: "SRCtrl"
               })
               .state("sellerFacilitiesNew", {
                   url: "/FacilitiesNew",
                   templateUrl: "/AccomodationFacilities/Create",
                   controller: "sellerRoomController",
                   controllerAs: "SRCtrl"
               })

              .state("sellerFacilities", {
                  url: "/Facilities",
                  templateUrl: "/App/accomodation/templates/sellerFacilities.html",
                  controller: "sellerRoomController",
                  controllerAs: "SRCtrl"
              })
              .state("sellerJobs", {
                  url: "/Jobs",
                  templateUrl: "/App/accomodation/templates/sellerJobs.html",
                  controller: "sellerRoomController",
                  controllerAs: "SRCtrl"
              })
             .state("sellerInfo", {
                 url: "/info",
                 templateUrl: "/App/accomodation/templates/infoSeller.html",
                 controller: "sellerRoomController",
                 controllerAs: "SRCtrl"
             })

                 .state("sellerPriceNew", {
                     url: "/SpecialPricesNew",
                     templateUrl: "/RoomPrices/Create",
                     controller: "sellerRoomController",
                     controllerAs: "SRCtrl"
                 })
            .state("searchResults", {
                url: "/SearchResults",
                templateUrl: "/App/accomodation/templates/searchResults.html",
                controller: "accomodationListController",
                controllerAs: "accomodationListCtrl"
            })
            .state("searchResultsConfirmation", {
                url: "/SearchResultsConfirmation",
                templateUrl: "/App/accomodation/templates/searchResultsConfirmation.html",
                controller: "accomodationListController",
                controllerAs: "accomodationListCtrl"
            })
                  .state("sellerReservations", {
                      url: "/MyReservations",
                      templateUrl: "/App/accomodation/templates/reservationsSeller.html",
                      controller: "sellerRoomController",
                      controllerAs: "SRCtrl"
                  })
        .state("manager", {
            url: "/manager",
            templateUrl: "/App/accomodation/templates/manager.html",
            controller: "sellerRoomController",
            controllerAs: "SRCtrl"
        })
               .state("about", {
                   url: "/about",
                   templateUrl: "/App/accomodation/templates/about.html",
                   controller: "sellerRoomController",
                   controllerAs: "SRCtrl"
               })
        .state("managePhotos", {
            url: "/photos/:roomTypeId",
            templateUrl: "/App/accomodation/accomodationPhotos/photos.html",
            controller: "PhotoControllerForRoomPhotos",
            controllerAs: "PhotoCtrl"
        })

        .state("managePhotosFacilities", {
            url: "/photosFacilities/:facilityId",
            templateUrl: "/App/accomodation/accomodationPhotos/photosFacilities.html",
            controller: "PhotoControllerForFacilityPhotos"
        })
        
        //ADDITIONAL NEW VIEW (nested views for COMMENTS inside state DETAILS)
        .state("details", {
            url: "/details/:accomodationId",
            views: {
                "": {
                    templateUrl: "/App/accomodation/AccomodationDetails/template/accomodationDetails.html",
                    controller: "accomodationDetailsController",
                    controllerAs: "accomodationDetailsCtrl"
                },
                "accomodationComments@details": {
                    templateUrl: "/App/accomodation/accomodationComments/template/accomodationComments.html",
                    controller: "accomodationCommentsController",
                    controllerAs: "accomodationCommentsCtrl"
                },
                "accomodationContact@details": {
                    templateUrl: "/App/accomodation/accomodationComments/template/accomodationContact.html",
                    controller: "accomodationCommentsController",
                    controllerAs: "accomodationCommentsCtrl"
                },
                "allAccomodationComments@details": {
                templateUrl: "/App/accomodation/accomodationComments/template/allAccomodationComments.html",
                controller: "accomodationCommentsController",
                controllerAs: "accomodationCommentsCtrl"
                }
            }
        })
        //END ADDITIONAL NEW VIEW

        .state("registerSeller", {
            url: "/registerSeller/:token",
            templateUrl: "/App/accomodation/templates/registerSeller.html",
            controller: "RegisterCtrl",
            controllerAs: "regCtrl"
        })
        .state("paymentConfirmation", {
            url: "/paymentConfirmation",
            templateUrl: "/App/accomodation/templates/paymentConfirmation.html"
        }).state("noaccess", {
                    url: "/error",
                    templateUrl: "/App/accomodation/templates/error.html"
                })
        .state("paymentCanceled", {
            url: "/paymentCanceled",
            templateUrl: "/App/accomodation/templates/paymentCanceled.html"
        })

        .state("profile", {
            url: "/profile",

            views: {
                "": {
                    templateUrl: "/App/accomodation/templates/profile.html",
                    controller: "LoginCtrl",
                    controllerAs: "logCtrl",
                },
                "edit@profile": {
                    templateUrl: "/App/accomodation/templates/profileEdit.html",
                    controller: "LoginCtrl",
                    controllerAs: "logCtrl"
                },
                "reservation@profile": {
                    templateUrl: "/App/accomodation/templates/profileReservation.html",
                    controller: "LoginCtrl",
                    controllerAs: "logCtrl"
                },
                "comments@profile": {
                    templateUrl: "/App/accomodation/templates/profileComments.html",
                    controller: "LoginCtrl",
                    controllerAs: "logCtrl"
                }
            }
        })

        .state("login", {
            url: "/login",
            templateUrl: "/App/accomodation/templates/login.html",
            controller: "LoginCtrl",
            controllerAs: "logCtrl"
        })
          .state("register", {
              url: "/register",
              templateUrl: "/App/accomodation/templates/register.html",
              controller: "RegisterCtrl",
              controllerAs: "regCtrl"
          });;
    });

    app.config(['growlProvider', function (growlProvider) {
        growlProvider.globalTimeToLive(4000);
        growlProvider.globalDisableCountDown(true);
        growlProvider.onlyUniqueMessages(false);
        growlProvider.globalPosition('top-center');
    }]);


    app.service("UserService",
        ["$http", "$httpParamSerializer", "$state", "$cookies", "growl",
            function ($http, $httpParamSerializer, $state, $cookies, growl) {

                var self = this;
                self.reservation = false;
                self.edit = false;
                self.comments = false;
                self.firstname = "";
                self.lastname = "";
                self.country = "";
                self.phonenumber = "";
                self.profileEditusername = "";
                self.errorMessages = [];
                self.userReservations = [];
                self.username = "";
                self.access_token = "";
                self.isLoggedIn = false;
                self.logout = logout;
                initUser();
                self.showReservations = showReservations;
                self.showEdit = showEdit;
                self.showComments = showComments;
                self.isSeller = false;
                self.check = checkSeller;
                self.userComments = [];
                self.ShowPayButton = false;
                self.inbox = inbox;
                self.messages = [];
                self.showMessage = showMessage;
                self.currentMessage = "";
                self.openMessage = false;
                self.PayForAccomodation = PayForAccomodation;
                self.sendMessage = sendMessage;
                self.response = "";
                self.replymessage = "";
                self.reply = reply;
                self.profile = profile;
                self.profilename = "";
                self.profilelastname = "";
                self.profilecountry = "";
                self.profilephonenumber = "";
                self.hasNewMessage = "";
                self.intervalfunction = intervalfunction;

                function intervalfunction()
                {
                    $http.get("Messages/hasnew/").then(function (result) { self.hasNewMessage = result.data; })

                };
                setInterval(self.intervalfunction, 5000);

                function profile()
                {
                    $http.get("/Home/ProfileInfo").then(function (response) {
                        self.profilecountry = response.data[0].Country;

                        self.profilename = response.data[0].FirstName;
                        self.profilelastname = response.data[0].LastName;
                        
                        self.profilephonenumber = response.data[0].Phone;


                        //console.log(response);
                    });

                    $state.go("profile");
                };

                function reply()
                {
                    //console.log(self.replymessage);
                    var dataTosend = {

                        ReceiverId: self.currentMessage.SenderId, Content: self.replymessage, AccomodationId: self.currentMessage.AccomodationId


                    };

                    $http.post("Messages/Reply", dataTosend).then(function (response) {
                        self.replymessage = "Message sent!";

                        growl.success("Message sent!");


                    });

                };



                function inbox()
                {
                    $http.get("Messages/hasnew/").then(function (result) { self.hasNewMessage = result.data; })

                    self.openMessage = false;
                    $http.get("/Messages/Index").then(function (e) { self.messages = e.data; });
                    $state.go("inbox");
                };

                function showMessage(index) {
                    self.replymessage = "";
                    self.currentMessage = self.messages[index];
                    //console.log(self.currentMessage);
                    var dataTosend={id:self.currentMessage.MessageId};
                    $http.post("/Messages/Seen", dataTosend).then(function (e) {
                        $http.get("/Messages/Index").then(function (e) {
                            self.messages = e.data;
                            //console.log(e);
                        });
                    });
                    $http.get("Messages/hasnew/").then(function (result) {
                        self.hasNewMessage = result.data;
                        //console.log(result);
                    })

                    self.openMessage = true;
                };

                function sendMessage()
                {
                    var dataToSend = { content: self.response, accomodationId:self.currentMessage.Accomodation.AccomodationId };
                };

                function PayForAccomodation()
                {
                    growl.success("Please wait for redirection to PayPal website!");
                    var model = {
                        paymentId: "test",
                        token: "test",
                        PayerId: "test",
                        userId: "test"
                    };
                    $http.post("/api/PayPal/PostCreatePayment", model).then(function (response) {
                        //console.log("PAYMENT APPROVED", response);
                        window.location = response.data;
                    }, function (error) {
                        //console.log("ERROR PAYMENT: ", error);
                    });
                }

                function checkSeller() {
                    $http.get("/api/values").then(function (response) {
                        self.isSeller = response.data;
                        if(self.isSeller == true)
                        {
                            self.isLoggedIn = false;
                        }
                    });
                };
                function showReservations() {
                    self.reservation = true;
                    self.edit = false;
                    self.comments = false;
                    showReservations();
                };
                function showComments() {
                    self.reservation = false;
                    self.edit = false;
                    self.comments = true;
                    showCommentlist();
                };
                function showEdit() {

                    self.reservation = false;
                    self.edit = true;
                    self.comments = false;
                    updateEdit();
                };


                function updateEdit()
                {
                    $http.get("/Home/GetInfo").then(function (e) {
                        //console.log(e.data);
                        self.country = e.data.Country;
                        self.firstname = e.data.FirstName;
                        self.lastname = e.data.LastName;
                        self.phonenumber = e.data.Phone;
                        self.profilecountry = e.data.Country;

                        self.profilename = e.data.FirstName;
                        self.profilelastname = e.data.LastName;

                        self.profilephonenumber = e.data.Phone;

                    });
                };


                function showCommentlist()
                {
                    $http.get("/UserComments/Index").then(function (e) {
                        self.userComments = e.data;
                        //console.log(e.data);
                    });
                };

                self.totalPriceForPayment = 0;

                function showReservations() {
                    $http.get("/RoomAvailabilities/Index").then(function (response) {
                        //console.log("SHOW RESERVATION:", response);
                        self.totalPriceForPayment = 0;
                        var reservation = response;
                        for (var i = 0; i < response.data.length; i++)
                        {
                            if(response.data[i].IsPaid == false)
                            {
                                self.ShowPayButton = true;
                                self.totalPriceForPayment += response.data[i].TotalPrice;
                            }
                        }
                        self.userReservations = reservation.data;
                        self.reservation = true;
                        self.edit = false;
                        self.comments = false;
                    }, function (error) {
                        //console.log("ERROR SHOW RESERVATION: ", error);
                    });
                };


                function logout() {
                    $http.post("/api/account/logout").then(function () {
                        $cookies.remove("user");
                        self.username = "";
                        self.access_token = "";
                        self.isLoggedIn = false;
                        growl.success("You are logged-out.Thank you for visiting our page. Have a nice day!");
                        self.profileEditusername = "";
                        self.reservation = false;
                        self.edit = false;
                        self.comments = false;
                        self.isSeller = false;
                        $state.go("home");
                    });
                };

                function initUser() {
                    $http.get("Messages/hasnew/").then(function (result) { self.hasNewMessage = result.data;  })

                    var user = $cookies.get("user");
                    if (user) {
                        user = JSON.parse(user);
                        self.username = user.username;
                        self.isLoggedIn = true;
                        self.access_token = user.access_token;
                        self.profileEditusername = user.username;
                        checkSeller();
                    } else {
                        self.isLoggedIn = false;
                    }
                }
            }]);

    app.factory('httpRequestInterceptor', function ($cookies) {
        return {
            request: function (config) {
                var user = $cookies.get("user");
                if (user) {
                    user = JSON.parse(user);
                    config.headers['Authorization'] = 'Bearer ' + user.access_token;
                }
                return config;
            }
        };
    });

    app.config(function ($httpProvider, $cookiesProvider) {
        $httpProvider.interceptors.push('httpRequestInterceptor');
        $cookiesProvider.defaults.path = "/";
    });

    app.controller("LoginCtrl", ["$http", "$httpParamSerializer", "$cookies", "growl", "UserService", "$state", function ($http, $httpParamSerializer, $cookies, growl, UserService, $state) {

        var ctrl = this;
        ctrl.userService = UserService;
        ctrl.user = {
            email: "",
            password: ""
        };
        ctrl.state = "";
        ctrl.name = "";
        ctrl.lastname = "";
        ctrl.phone = "";
        ctrl.SaveProfile = SaveProfile;

        function SaveProfile()
        {
            var dataToSend = { name: ctrl.userService.firstname, county: ctrl.userService.country, phone: ctrl.userService.phonenumber, lastname: ctrl.userService.lastname };
            $http.post("/Home/EditProfile", dataToSend).then(function (response) {
                growl.success("Your profile has been successfully changed! ");
              //  ctrl.userService
                ctrl.userService.profilecountry = ctrl.userService.country;

                ctrl.userService.profilename = ctrl.userService.firstname;
                ctrl.userService.profilelastname = ctrl.userService.lastname;

                ctrl.userService.profilephonenumber = ctrl.userService.phonenumber;
           

            });
           


        };

        ctrl.login = function () {
            ctrl.errorMessages = [];
            var login = {
                username: ctrl.user.email,
                password: ctrl.user.password,
                grant_type: "password"
            };

            $http.post("/token", $httpParamSerializer(login),
                {
                    'Content-Type': 'application/x-www-form-urlencoded' // Note the appropriate header
                }
                ).then(function (response) {
                    var loggedInUser = {
                        username: ctrl.user.email,
                        isLoggedIn: true,
                        access_token: response.data.access_token
                    };
                    growl.success("You are logged in. Welcome to BitBooking <strong> " + loggedInUser.username + "</strong> ");
                    $cookies.putObject("user", loggedInUser);
                    // $cookies.put("token", response.data.access_token);

                    var user = $cookies.get("user");
                    if (user) {
                        user = JSON.parse(user);
                        ctrl.userService.username = user.username;
                        ctrl.userService.isLoggedIn = true;
                        ctrl.userService.access_token = user.access_token;
                        ctrl.userService.profileEditusername = user.username;
                        ctrl.userService.check();
                    }
                    $http.get("Messages/hasnew/").then(function (result) {
                        ctrl.userService.hasNewMessage = result.data;
                        //console.log(result);
                    })


                    $http.get("/Home/GetInfo").then(function (e) {
                        //console.log(e.data);
                        ctrl.userService.country =e.data.Country;
                        ctrl.userService.firstname = e.data.FirstName;
                        ctrl.userService.lastname = e.data.LastName;
                        ctrl.userService.phonenumber = e.data.Phone;

                    });


                    $state.go("home");

                }, function (error) {
                    growl.error(error.data.error_description);
                });
           };
    }]);



    app.controller("NavbarController", ["UserService", function (UserService) {
        var self = this;
        self.userService = UserService;
      
   
    }]);

    app.service("SellerService",
      ["$http", "$httpParamSerializer", "$state", "$cookies", "growl",
          function ($http, $httpParamSerializer, $state, $cookies, growl) {

              var self = this;
              self.info = "";
              self.sellerFacilities = [];
              self.sellerRooms = [];
              self.sellerRooms2 = [];

              self.sellerInfo = "";
              self.sellerInfoEdit = "";
              self.sellerServices = [];
              self.sellerPrices = [];
              self.sellerReservations = [];
              self.sellerJobs = [];
              self.newJob = newJob;
              self.editJob = editJob;

              self.getRooms = getRooms;
              self.getRoomsForPhotos = getRoomsForPhotos;

              self.getReservations = getReservations;
              self.getPrices = getPrices;
              self.getServices = getServices;
              self.getInfo = getInfo;
              self.getInfoEditData = getInfoEditData;
              self.getFacilities = getFacilities;
              self.getJobs = getJobs;
              self.dateFrom = "";
              self.dateTo = "";
              self.newSpecialPrice = newSpecialPrice;
              self.price = "";
              self.newFacilitie = newFacilitie;
              self.startHours="";
              self.endHours="";
              self.facName="";
              self.facDesc = "";
              self.SaveEditInfo = SaveEditInfo;
              self.serviceName = "";
              self.serviceType = "";
              self.newService = newService;
              self.newRoom = newRoom;
              self.roomDetails = "";
              self.roomType = "";
              self.roomNumber = "";
              self.roomCapacity = "";
              self.roomPrice = "";
              self.jobName = "";
              self.jobDescription = "";
              self.jobSallary = "";

              self.x = "";
              self.y = "";
              self.xx = "";
              self.yy= "";
              self.deleteService = deleteService;
              self.deleteFacilitie = deleteFacilitie;
              self.deleteJob = deleteJob;
              self.deleteOffer = deleteOffer;
              self.map = { center: { latitude: 51.219053, longitude: 4.404418 }, zoom: 14 };
              self.options = { scrollwheel: false };
              self.placeMarker = placeMarker;
              self.accPhotos = [];
              self.UpdatePhoto = UpdatePhoto;
              //PHOTOS START

              self.PhotoId = "";
              self.PhotoUrl = "";
              self.AccomodationId = "";
              self.RoomTypeId = "";
              self.Priority = "";
              self.currentEditRoom = "";
              self.currentEditJob = "";
              self.currentEditFac = "";
              self.showPhotoPage = showPhotoPage;
              self.editRoom = editRoom;
              self.editFacilitie = editFacilitie;
              self.deletePhoto = deletePhoto;
              self.deleteRoom = deleteRoom;
            

              function deleteRoom(index)
              {
                  var room = self.sellerRooms[index];

                  $http.post("/Rooms/Delete/" + room.RoomId).then(function (e) { self.sellerRooms.splice(index, 1); growl.success("Room Deleted "); });



              };
              function deletePhoto(id)
              {





                  $http.post("/Photos/Delete/" + id).then(function (e) {

                      growl.success("Picture deleted!");
                      UpdatePhoto();
                  });


              };
              function editFacilitie()
              {


                  $http.post("AccomodationFacilities/Edit", self.currentEditFac).then(function (e) {

                      growl.success("Changes Saved ");
                       getFacilities();
                          
                  });


              };
              function editJob() {


                  $http.post("Jobs/Edit", self.currentEditJob).then(function (e) {

                      growl.success("Changes Saved ");
                      getJobs();

                  });


              };


              function editRoom()
              {
                  $http.post("Rooms/Edit", self.currentEditRoom).then(function (e) {

                      growl.success("Changes Saved ");
                      getRooms();
                  });

              };

              function showPhotoPage() {
                  $http.get("Photos/Index").then(function (e) {
                      self.accPhotos = e.data;
                      //console.log(e);
                  });
                  $state.go("managePhotos");
              };
              function UpdatePhoto() {
                  $http.get("Photos/Index").then(function (e) {
                      self.accPhotos = e.data;
                      //console.log(e);
                  });
                  
              };
              //PHOTOS END

              function placeMarker(e)
              {
                  
                  self.xx = e.Da.x;
                  self.yy = e.Da.y;
                  //console.log(e);

              };
           

              function deleteService(index) {

                  //console.log(index);
                  $http.post("/AccomodationServices/Delete/" + self.sellerServices[index].AccomodationServiceId)
                      .then(function (response) {
                          //console.log("proslo"); 
                          self.sellerServices.splice(index, 1);
                      })
                  
                //  getServices();

              };


              function deleteOffer(index) {

                  //console.log(index);
                  $http.post("/RoomPrices/Delete/" + self.sellerPrices[index].RoomPriceId)
                      .then(function (response) {
                          //console.log("proslo");
                          self.sellerPrices.splice(index, 1);
                      })

                  //  getServices();

              };

              function deleteJob(index) {

                  //console.log(index);
                  $http.post("/Jobs/Delete/" + self.sellerJobs[index].JobId)
                      .then(function (response) {
                          //console.log("proslo");
                          self.sellerJobs.splice(index, 1);
                      })

                  //  getServices();

              };


              function deleteFacilitie(index)
              {
                  $http.post("/AccomodationFacilities/Delete/" + self.sellerFacilities[index].AccomodationFacilityId)
                      .then(function (response) {
                          //console.log("proslo");
                          self.sellerFacilities.splice(index, 1);
                      })
              };
              function newRoom()
              {
                  var dataTosend = { RoomDetails: self.roomDetails, NumberOfRooms: self.roomNumber, Price:self.roomPrice, RoomTypeId:self.roomType, RoomCapacity:self.roomCapacity }

                  $http.post("/Rooms/Create/", dataTosend).then(function (response) {
                      self.roomDetails = "";
                      self.roomType = "";
                      self.roomNumber = "";
                      self.roomCapacity = "";
                      self.roomPrice = "";
                      growl.success("Successfully added new room! ");
                      getRooms();
                  });
              };



              function newJob() {
                  var dataTosend = { Name: self.jobName, Description: self.jobDescription, Salary: self.jobSallary }

                  $http.post("/Jobs/Create/", dataTosend).then(function (response) {
                      self.jobName = "";
                      self.jobDescription = "";
                      self.jobSallary = "";
                    
                      growl.success("Successfully added new job! ");
                      getJobs();
                  });
              };


              function newService()
              {
                  var dataTosend = {Name:self.serviceName, AccomodationServiceTypeId:self.serviceType}

                  $http.post("/AccomodationServices/Create/", dataTosend).then(function (response) {
                      self.serviceName = "";
                      self.serviceType = "";
                      growl.success("Successfully added new service! ");
                      getServices();
                  });


              };
              
              function SaveEditInfo() {
                  $http.get("/AccomodationInfoes/Index").then(function (response) {
                      self.sellerInfoEdit.GoogleX = response.data.GoogleX;
                      self.sellerInfoEdit.GoogleY = response.data.GoogleY;
                   //   self.y = response.data.GoogleY;
                      $http.post("/AccomodationInfoes/Edit/", self.sellerInfoEdit).then(function (response) { growl.success("Successfully changed information! "); getInfo(); });
                  });

               //   $http.post("/AccomodationInfoes/Edit/", self.sellerInfoEdit).then(function (response) { growl.success("Successfully changed information! "); getInfo(); });


              };


              function newFacilitie()
              {
                      var dataTosend = {
                          StartHours: self.startHours, Name: self.facName, Description: self.facDesc, EndHours: self.endHours
                      };
                      if (self.startHours > self.endHours)
                      { growl.error("Check times!"); }
                   else   {
                          $http.post("/AccomodationFacilities/Create", dataTosend).then(function (response) {
                              //console.log(response);
                              growl.success("Successfully added new Facility! ");
                              self.startHours = "";
                              self.endHours = "";
                              self.facName = "";
                              self.facDesc = "";
                              getFacilities();
                          });
                      }
              };

              function newSpecialPrice() {
                  var dataTosend = {
                      RoomTypeId: self.roomType, SpecialPrice: self.price, StartDate: self.dateFrom, EndDate: self.dateTo
                  };
                  $http.post("/RoomPrices/Create", dataTosend).then(function (response) {
                      //console.log(response);
                      growl.success("Successfully added new Price interval! ");
                      self.roomType = "";
                      self.dateFrom = "";
                      self.dateTo = "";
                      self.price = "";
                      getPrices();
                  });
              };

              function getFacilities()
              {
                  $http.get("/AccomodationFacilities/Index").then(function (response) {
                      self.sellerFacilities = response.data;
                      //console.log("FACILITIES: ", response.data);
                      $state.go("sellerFacilities")
                  });
              };

              function getJobs() {
                  $http.get("/Jobs/Index").then(function (response) {
                      self.sellerJobs = response.data;
                      console.log(response.data);
                      $state.go("sellerJobs")
                  });
              };

              function getInfo() {
                  $http.get("/AccomodationInfoes/Index").then(function (response) {
                      self.sellerInfo = response.data;
                      //console.log("ACCOMODATIONS INFOES: ",response.data);
                      self.x = response.data.GoogleX;
                      self.y = response.data.GoogleY;
                      var url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + response.data.City;
                      $.ajax({ url: url }).success(function (msg) {
                          //console.log("proslo");

                          //console.log(msg.results[0].geometry.location.lat);
                          //console.log(msg.results[0].geometry.location.lng);
                          if (self.x === null) { self.x = msg.results[0].geometry.location.lat, self.y = msg.results[0].geometry.location.lng };

                      });
                      $state.go("sellerInfo")
                  });
              };

              function getRooms() {
                  $http.get("/Rooms/Index").then(function (response) {
                      self.sellerRooms = response.data;
                      //console.log("ROOMS: ",response.data);

                      $state.go("sellerRooms");
                  });
              };

              getRoomsForPhotos();

              function getRoomsForPhotos() {
                  $http.get("/Rooms/Index").then(function (response) {
                      self.sellerRooms2 = response.data;

                  });
              };

              function getInfoEditData() {
                  $http.get("/AccomodationInfoes/Index").then(function (response) {
                      self.sellerInfoEdit = response.data;
                      self.x = response.data.GoogleX;
                      self.y = response.data.GoogleY;
                      //console.log(self.x);
                      var url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + response.data.City;
                      $.ajax({ url: url }).success(function (msg) {
                          //console.log("proslo");

                          //console.log(msg.results[0].geometry.location.lat);
                          //console.log(msg.results[0].geometry.location.lng);
                          if (self.x === null) { self.x = msg.results[0].geometry.location.lat, self.y = msg.results[0].geometry.location.lng };
                      
                      });
                    //  if (self.x === null) { self.x = "50", self.y == "50" };
                  //    console.log(self.x);
                     $state.go("sellerInfoEdit")
                  });
              };

              function getServices() {
                  $http.get("/AccomodationServices/Index").then(function (response) {
                      self.sellerServices = response.data;

                      $state.go("sellerServices");
                  });
              };

              function getPrices() {
                  $http.get("/RoomPrices/Index").then(function (response) {
                      self.sellerPrices = response.data;

                      $state.go("sellerPrice");
                  });
              };

              function getReservations() {
                  $http.get("/RoomAvailabilities/Index").then(function (response) {
                      self.sellerReservations = response.data;
                      //console.log(response);
                      $state.go("sellerReservations");
                  });
              };
          }]);

    app.controller("sellerRoomController", ["$http", "SellerService", "$state", function ($http,SellerService, $state) {
        var self = this;

        self.SellerService = SellerService;
        self.getRoomEdit = getRoomEdit;
        getRoomEdit();
        getJobEdit();
        self.getFacEdit = getFacEdit;
        getFacEdit();
        self.getInfo=getInfo;
        getInfo();
        self.info = "";

        function getInfo()
        {
            

            $http.get("/Accomodations/GetName").then(function (response) {
                self.SellerService.info = response.data;

                if (!response.data.AccomodationId > 0)
                {
                    $state.go("noaccess");
                }
               // console.log(response.data);
                // console.log("test");

            });

        };



        function getRoomEdit()
        {
           

            if ($state.params.roomId > 0) {

                $http.get("Rooms/EditData/" + $state.params.roomId).then(function (e) {
                    SellerService.currentEditRoom = e.data;
                    //console.log(e.data);
                });

            };
        };

        function getJobEdit() {


            if ($state.params.jobId > 0) {

                $http.get("Jobs/EditData/" + $state.params.jobId).then(function (e) {
                    SellerService.currentEditJob = e.data;
                    //console.log(e.data);
                });

            };
        };
        function getFacEdit() {
            if ($state.params.facId > 0) {

                $http.get("AccomodationFacilities/EditData/" + $state.params.facId).then(function (e) {
                    SellerService.currentEditFac = e.data;
                    SellerService.currentEditFac.StartHours = new Date(new Date(SellerService.currentEditFac.StartHours).toUTCString());
                    
                    SellerService.currentEditFac.EndHours = new Date(SellerService.currentEditFac.EndHours);
                    //console.log(e);
                  //  console.log(e.data);
                });

            };
        };
    }]);

    app.directive("navigation", function () {
        return {
            templateUrl: "/App/accomodation/templates/navbar.html",
            controller: "NavbarController",
            controllerAs: "navCtrl"
        };
    });

    app.controller("RegisterCtrl", ["$http", "$httpParamSerializer", "growl", "UserService", "$cookies", "$state", function ($http, $httpParamSerializer, growl, UserService, $cookies, $state) {
        var ctrl = this;
        ctrl.userService = UserService;
        ctrl.user = {
            email: "",
            password: "",
            confirmPassword: "",
            Token: ""
        };


        ctrl.checkToken = checkToken;
        checkToken();
        function checkToken() {

            if ($state.params.token === "")
                $state.go("register");

        };

        ctrl.register = function () {
            $http.post("/api/Account/Register", ctrl.user)
            .then(function (response) {
                var login = {
                    username: ctrl.user.email,
                    password: ctrl.user.password,
                    grant_type: "password"
                };
                $http.post("/token", $httpParamSerializer(login),
                    {
                        'Content-Type': 'application/x-www-form-urlencoded' // Note the appropriate header
                    }
                    ).then(function (response) {
                        growl.success("Thank you for registration. Please login.");

                        // $cookies.put("token", response.data.access_token);
                        $state.go("login");
                    })
            }, function (err) {
                var modelState = err.data.ModelState;

                angular.forEach(modelState, function (errorMessages, attribute) {
                    if (attribute != "$id") {
                        angular.forEach(errorMessages, function (value, key) {
                            growl.error(value);
                            ctrl.userService.errorMessages.push(value);
                        });
                    };
                });
            });
        };

        ctrl.registerSeller = function () {

            ctrl.user.Token = $state.params.token;

            $http.post("/api/Account/Register", ctrl.user)
            .then(function (response) {
                var login = {
                    username: ctrl.user.email,
                    password: ctrl.user.password,
                    grant_type: "password"
                };
                $http.post("/token", $httpParamSerializer(login),
                    {
                        'Content-Type': 'application/x-www-form-urlencoded' // Note the appropriate header
                    }
                    ).then(function (response) {
                        growl.success("Thank you for registration. Please login.");

                        // $cookies.put("token", response.data.access_token);
                        $state.go("login");
                    })
            }, function (err) {
                var modelState = err.data.ModelState;

                angular.forEach(modelState, function (errorMessages, attribute) {
                    if (attribute != "$id") {
                        angular.forEach(errorMessages, function (value, key) {
                            growl.error(value);
                            ctrl.userService.errorMessages.push(value);
                        });
                    };
                });
            });
        };


    }]);
})();


