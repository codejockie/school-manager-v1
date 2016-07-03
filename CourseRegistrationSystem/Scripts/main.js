$(function () {
    $("#accordion").accordion({
        active: false,
        collapsible: true,
        icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" },
        heightStyle: "content"
    });

    $("#alertdismiss").on("click", function () {
        $("#alerthr").hide();
    });
});