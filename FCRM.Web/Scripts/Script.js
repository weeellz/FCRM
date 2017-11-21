$(document).ready(function () {
    //$('section[data-type="background"]').each(function () {
    //    var $bgobj = $(this); // создаем объект
    //    $(window).scroll(function () {
    //        var yPos = -($(window).scrollTop() / $bgobj.data('speed')); // вычисляем коэффициент 
    //        // Присваиваем значение background-position
    //        var coords = 'center ' + yPos + 'px';
    //        // Создаем эффект Parallax Scrolling
    //        $bgobj.css({ backgroundPosition: coords });
    //    });
    //});
    $(window).on('load', function () {
        var $preloader = $('#page-preloader'),
            $spinner = $preloader.find('.spinner');
        $spinner.fadeOut();
        $preloader.delay(350).fadeOut('slow');
    });
    $('#reg-choose').click(reg_choose);
    $('.API_menu > span').click(show_info);
});

var reg_choose = function () {
    if ($('#popup').hasClass("active")) {
        $('#popup').hide();
        $('#popup').removeClass("active")
    } else {
        $('#popup').show();
        $('#popup').addClass("active");
    }
};

var show_info = function (eventObject) {
    var target = $(eventObject.target);
    $('.API_info').children().hide();
    $("#" + target.data("info-id")).show();
}