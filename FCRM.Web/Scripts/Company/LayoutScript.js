$(document).ready(function () {
    get_company_name();
});

var get_company_name = function () {
    $.get("/api/company_name", function (data) {
        $("#company_name > span").text(data.Data);
    });
}