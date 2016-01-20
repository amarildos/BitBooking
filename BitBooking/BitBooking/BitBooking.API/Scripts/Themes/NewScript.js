$("span.menu").click(function () {
    $("ul.nav1").slideToggle(300, function () {
        // Animation complete.
    });
});


$(function () {
    // Slideshow 4
    $("#slider3").responsiveSlides({
        auto: true,
        pager: true,
        nav: false,
        speed: 500,
        namespace: "callbacks",
        before: function () {
            $('.events').append("<li>before event fired.</li>");
        },
        after: function () {
            $('.events').append("<li>after event fired.</li>");
        }
    });

});