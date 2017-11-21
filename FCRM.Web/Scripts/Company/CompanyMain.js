$(document).ready(function () {
    get_orders();
});

var get_orders = function () {
    $.get("/api/company_orders",
    function (data) {
        if (data.Success) {
            $("#order_list").empty();
            $("#ordersTemplate").tmpl(data).appendTo("#order_list");
            $(".main_info").click(additional_info);
            $(".flip-container").click(function () {
                $(this).toggleClass('flip');
            });
            var arr = [];
            for (var i = 0; i < 4; i++)
                arr[i] = 0;
            $.each(data.List, function (ind, val) {
                arr[val.Stage]++;
            });
            console.log(arr);
            for (var i = 0; i < 4; i++)
                $("#orders" + i).text(arr[i]);
        }
    });
};

var additional_info = function (eventObject) {
    var target = $(eventObject.target);
    $(".order_info").empty();
    var html = target.parents(".order").first().children(".additional_info").first().html();
    $(".order_count").hide();
    $(".order_info").append(html);
    $(".bnt_back").click(back);
};

var back = function () {
    $(".order_count").show();
    $(".order_info").empty();
};