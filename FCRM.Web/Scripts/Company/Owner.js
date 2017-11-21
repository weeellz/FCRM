$(document).ready(function () {
    get_users();
    get_company_id();
    get_performers_count();
    get_managers_count();
});

var get_users = function () {
    $.get("/api/users", function (data) {
        $("#users_list").empty();
        $("#usersTemplate").tmpl(data).appendTo("#users_list");
        $(".roles>span").click(role_manage);
    })
};

var role_manage = function (eventObject) {
    var target = $(eventObject.target);
    
    if (target.hasClass("active")) {
        target.removeClass("active");
        $.post("/api/users/" + target.parents(".roles").first().data("user-id") + "/remove_role/" + target.data("role-type"));
    }
    else {
        target.addClass("active");
        $.post("/api/users/" + target.parents(".roles").first().data("user-id") + "/add_role/" + target.data("role-type"));
    }
};

var get_company_id = function () {
    $.get("/api/company_id", function (data) {
        $("#company_guid").text(data.Data);
    });
}

var get_performers_count = function () {
    $.get("/api/performers/count", function (data) {
        $("#count_performers > span").text(data.Data);
    });
}

var get_managers_count = function () {
    $.get("/api/managers/count", function (data) {
        $("#count_managers > span").text(data.Data);
    });
}
