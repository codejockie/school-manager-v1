function CallPrint(strid) {
    var prtContent = document.getElementById(strid);
    var WinPrint = window.open('', '', 'letf=100,top=100,width=600,height=600');
    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();
    //WinPrint.close()   
}

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