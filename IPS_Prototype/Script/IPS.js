$(document).ready(function () {
    $('[data-toggle="popover"]').popover();

});

$(".navbar").hover(function () {
    $("#nav-barDrop").collapse("show");
});



/*rotate nav-bar up icon*/
$(".nav-statistics").click(function () {
    $(".fa-angle-up.statistic").toggleClass("rotate");
});

$('.user_info_btn').click(function () {
    $('.user').toggleClass('rotate');
});

$('#logout').click(function () {
    $('.user').toggleClass('rotate');
});

/* MEMBERSHIP PAGE JS */
$("#dLabel").hover(
    function () {
        $('>.dropdown-menu', this).stop(true, true).fadeIn("fast");
        $(this).addClass('open');
    },
    function () {
        $('>.dropdown-menu', this).stop(true, true).fadeOut("fast");
        $(this).removeClass('open');
    });

$('.dropdown-item').click(function () {
    var selected = $(this).text();
    $('#dLabel').text(selected);
});



