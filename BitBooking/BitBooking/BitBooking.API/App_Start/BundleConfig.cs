using System.Web;
using System.Web.Optimization;

namespace BitBooking.API
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/knockout-3.3.0.js",
                "~/Scripts/knockout-3.3.0.debug.js",
                "~/Scripts/star-rating.js",
                "~/Scripts/star-rating.min.js",
                "~/Scripts/bootbox.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                
                "~/Content/bootstrap.css",
                "~/Content/AngularCss/angular-growl.css",  //CSS FOR ANGULAR GROWL (ANGULAR EXEENSION)
                "~/Scripts/AngularQuantumi/quantumui.min.css",   //QUANTUMI CSS
                "~/Scripts/AngularQuantumi/effect-light.min.css", //QUANTUMI CSS
                "~/Content/AngularCss/dateRange.css", 
                "~/Content/site.css",
                "~/Content/NewStyle/style.css",
                "~/Content/NewStyle/star-rating.css",
                "~/Content/NewStyle/star-rating.min.css",
                "~/Content/NewStyle/pop-box.css",
                "~/Content/accIndex/animate.css",
                "~/Content/accIndex/font-awesome.min.css",
                "~/Content/accIndex/jquery.fancybox.css",
                "~/Content/accIndex/jquery.fs.boxer.min.css",
                "~/Content/accIndex/lightview.css",
                "~/Content/accIndex/main.css",
                "~/Content/accIndex/owl.carousel.css",
                "~/Content/accIndex/slit-slider.css",
                "~/Content/accIndex/superslides.css",
                "~/Content/accDetails/bootstrap.min.css",
                "~/Content/accDetails/font-awesome.min.css",
                "~/Content/accDetailsRoomtypes/bootstrap-theme.css",
                "~/Content/accDetailsRoomtypes/bootstrap-theme.min.css",
                "~/Content/accDetailsRoomtypes/flexslider.css",
                "~/Content/angular-carousel.css",
                 "~/Content/slick-theme.css",
                  "~/Content/slick.css",
                "~/Content/accDetailsRoomtypes/queries.css",
                "~/Content/Travolio/styleTravolio.css"
                //"~/Content/accDetailsRoomtypes/styles.css"

                ));

            bundles.Add(new StyleBundle("~/ScriptsAndThemes").Include(
                "~/Scripts/Themes/easing.js",
                "~/Scripts/Themes/jquery.magnific-popup.js",
                "~/Scripts/Themes/modernizr.custom.min.js",
                "~/Scripts/Themes/move-top.js",
                "~/Scripts/Themes/NewScript.js",
                "~/Scripts/Themes/responsiveslides.min.js"

                ));


            bundles.Add(new StyleBundle("~/Scripts/NewThemes").Include(
                   "~/Scripts/NewThemes/main.js",
               "~/Scripts/NewThemes/jquery.ba-cond.min.js",
               "~/Scripts/NewThemes/jquery.easing.min.js",
               "~/Scripts/NewThemes/jquery.fancybox.pack.js",
               "~/Scripts/NewThemes/jquery.parallax-1.1.3.js",
               "~/Scripts/NewThemes/jquery.singlePageNav.min.js",
               "~/Scripts/NewThemes/jquery.slitslider.js",
               "~/Scripts/NewThemes/modernizr-2.6.2.min.js",
               "~/Scripts/NewThemes/owl.carousel.min.js",
               "~/Scripts/NewThemes/wow.min.js"
                ));


            bundles.Add(new StyleBundle("~/Scripts/AccDetailsThemes").Include(
                  "~/Scripts/AccDetailsThemes/main.js",
              "~/Scripts/AccDetailsThemes/bootstrap.min.js",
              "~/Scripts/AccDetailsThemes/jquery-1.11.1.min.js",
              "~/Scripts/AccDetailsThemes/jquery.ba-cond.min.js",
              "~/Scripts/AccDetailsThemes/jquery.easing.min.js",
              "~/Scripts/AccDetailsThemes/jquery.fancybox.pack.js",
              "~/Scripts/AccDetailsThemes/jquery.parallax-1.1.3.js",
              "~/Scripts/AccDetailsThemes/jquery.singlePageNav.min.js",
              "~/Scripts/AccDetailsThemes/jquery.slitslider.js",
           
              "~/Scripts/AccDetailsThemes/main.js",
              "~/Scripts/AccDetailsThemes/modernizr-2.6.2.min.js",
              "~/Scripts/AccDetailsThemes/owl.carousel.min.js",
              "~/Scripts/AccDetailsThemes/wow.min.js"
              
               ));

            bundles.Add(new StyleBundle("~/Scripts/AccDetailsRoomtheme").Include(
             "~/Scripts/AccDetailsRoomtheme/bootstrap.js",
             "~/Scripts/AccDetailsRoomtheme/bootstrap.min.js",
             "~/Scripts/AccDetailsRoomtheme/jquery.flexslider.js",
             "~/Scripts/AccDetailsRoomtheme/jquery.smooth-scroll.js",
             "~/Scripts/AccDetailsRoomtheme/modernizr.js",
             "~/Scripts/AccDetailsRoomtheme/overlay.js",
             "~/Scripts/AccDetailsRoomtheme/scripts-ck.js",
             "~/Scripts/AccDetailsRoomtheme/scripts.js",
             "~/Scripts/AccDetailsRoomtheme/waypoints.js"
              ));


            //MAIN ANGULAR BUNDLES
            bundles.Add(new ScriptBundle("~/bundlesAngular").Include(
                   "~/Scripts/jquery.dataTables.min.js",
            "~/Scripts/Angular/angular.min.js",
            "~/Scripts/Angular/angular-cookies.js",
            "~/Scripts/Angular/angular-resource.min.js",
            "~/Scripts/Angular/angular-route.js",
            "~/Scripts/Angular/jcs-auto-validate.min.js",
            "~/Scripts/Angular/angular-ui-router.min.js",
            "~/Scripts/Angular/angular-animate.min.js",
        
            "~/Scripts/AngularQuantumi/angular-sanitize.js",    //QUANTUMI
            "~/Scripts/AngularQuantumi/quantumui-all.js",       //QUANTUMI
            "~/App/app.js"
            ));

            //THIRD PARTY ANGULAR BUNDLES
            bundles.Add(new ScriptBundle("~/bundlesAngularThrdParty").Include(
            //"~/Scripts/Angular/angular-animate.min.js",
            "~/Scripts/Angular/angular-growl.min.js",     // GROWL pop-up (autohide) messages
            "~/Scripts/angular-strap.min.js",
            "~/Scripts/angular-strap.tpl.min.js",
            "~/Scripts/angular-google-maps.min.js",
            "~/Scripts/angular.country-select.min.js",
            "~/Scripts/Angular/dimensionsDateRange.js",
            "~/Scripts/Angular/dateParserDateRange.js",
          //   "~/Scripts/angular-touch.js", "~/Scripts/slick.min.js",
        //     "~/Scripts/angular-slick.js",
               "~/Scripts/angular-carousel.js",
            "~/Scripts/Angular/angular-file-upload.js",
                 "~/Scripts/angular-datatables.js"
            ));
            
            //APPLICATION ANGULAR BUNDLES
            bundles.Add(new ScriptBundle("~/bundlesAccomodation").Include(
            "~/App/accomodation/accomodation.module.js",
            "~/App/accomodation/accomodation.factory.js",
            "~/App/accomodation/accomodation.service.js",
            "~/App/accomodation/accomodationList/controller/accomodationList.controller.js",
            "~/App/accomodation/accomodationDetails/controller/accomodationDetails.controller.js",
            "~/App/accomodation/accomodationList/directive/accomodationList.directive.js",
            "~/App/accomodation/accomodationComments/controller/accomodationComments.controller.js",
            "~/App/accomodation/accomodationPhotos/accomodationPhotosController.js",
             "~/Scripts/angular-touch.js", "~/Scripts/slick.min.js",
            "~/Scripts/angular-slick.js"
            ));

            
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
