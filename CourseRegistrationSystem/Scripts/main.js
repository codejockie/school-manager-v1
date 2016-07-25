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

function successAlert(elem, message) {
    $(elem).show().html('<br /><div class="alert alert-success alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + message + '</div>');

    $(elem).alert('close');
}