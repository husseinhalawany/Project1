jQuery(function () {
    jQuery.get("/Account/UserData/", function (data) {
        jQuery("#MenuUser").html(data);
    });
});