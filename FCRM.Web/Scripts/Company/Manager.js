$(document).ready(function () {
    get_unassigned_orders();
    get_my_orders();
    get_performers();
});

var get_unassigned_orders = function () {
    $.get("/api/company_orders/unassigned",
    function (data) {
        if (data.Success) {
            $("#unassigned_order_list").empty();
            $("#unassignedOrdersTemplate").tmpl(data).appendTo("#unassigned_order_list");
            $(".btn_assign_to_order").click(assign_to_order);
        }
    });
};

var get_my_orders = function () {
    $.get("/api/company_orders/my",
    function (data) {
        if (data.Success) {
            $("#my_order_list").empty();
            $("#myOrdersTemplate").tmpl(data).appendTo("#my_order_list");
            $(".myorder>.main_info").click(additional_info);
            $("#btn_back").click(back);

            $("#btn_save_perf").click(save_price);
        }
    });
};

var assign_to_order = function (eventObject) {
    var target = $(eventObject.target);
    $.post("/api/assign_to_order/" + target.parents(".order").first().data("order-id"), function (data) {
        if (data.Success) {
            target.parents(".order").first().remove();
        }
    });
};

var get_performers = function () {
    $.get("/api/performers", function (data) {
        $("#performers_list").empty();
        $("#performersTemplate").tmpl(data).appendTo("#performers_list");
        $(".performer").click(function (eventObject) {
            var target = $(eventObject.target);
            target.toggleClass("chosen");
        });
    });
};

var additional_info = function (eventObject) {
    $(".flip-container").toggleClass('flip');
    var target = $(eventObject.target);
    $("#info").empty();
    var html = target.parents(".myorder").first().children(".additional_info").first().html();
    $.each(target.parents(".myorder").first().children("input[type=hidden]"), function (ind, val) {
        $("#" + v.val()).addClass("chosen");
    });
    $("#info").append(html);
    $("#price").val(target.parents(".myorder").first().children(".price_field").val());
    $("#order_id").val(target.parents(".myorder").first().data("order-id"));
};

var back = function () {
    $(".flip-container").toggleClass('flip');
    $(".back .dashboard-wrapper > .additional_info").empty();
}
var save_price = function () {
    $.post("/api/orders/" + $("#order_id").val() + "/set_price/" + $("#price").val(), function (data) {

    });
};