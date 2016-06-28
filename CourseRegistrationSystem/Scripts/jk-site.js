$(function () {
    $("#accordion").accordion({
        active: false,
        collapsible: true,
        icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" },
        heightStyle: "content"
    });
});


//function tool_tip() {
//    $('[data-toggle="tooltip"]').tooltip()
//}

//tool_tip();  // Call in document ready for elements already present

//$.ajax({
//    success: function (data) {
//        tool_tip();  // Call function again for AJAX loaded content
//    }
//})