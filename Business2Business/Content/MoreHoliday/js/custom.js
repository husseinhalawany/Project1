$(function () {
    "use strict";


    $('[placeholder]').focus(function () {
        $(this).attr("data-text", $(this).attr("placeholder"));
        $(this).attr("placeholder", "");
    }).blur(function () {
        $(this).attr("placeholder", $(this).attr("data-Text"));
    });


    /***************** start evaluate progress ******************/




    // This function tells the layers where to fall depending on the percentage.
    //function adjustCircle(graphValue, Id) {
    //    alert(graphValue + " " + Id);

    //    var rotation = graphValue * 3.60; // Mathing up some rotational degrees

    //    if (rotation <= 180) {
    //        var leftRotation = 180;
    //        var rightRotation = rotation;
    //        $('#progress-doughnut' + '-' + Id).find('.p-r-cover' + '-' + Id).css('opacity', '1');
    //    } else if (rotation >= 180) {
    //        var leftRotation = rotation;
    //        var rightRotation = 0;
    //        $('#progress-doughnut' + '-' + Id).find('#p-r-cover' + '-' + Id).css('opacity', '0');
    //        $('#progress-doughnut' + '-' + Id).find('#p-l-cover' + '-' + Id, '#p-l' + '-' + Id, '#p-r' + '-' + Id).css('opacity', '1');
    //    }
    //    if (rotation >= 360) {
    //        $('#p-l-cover' + '-' + Id, '#p-r-cover' + '-' + Id, '#p-l' + '-' + Id, '#p-r' + '-' + Id).css('opacity', '0');
    //    } else if (rotation <= 0) {
    //        $('#p-l-cover' + '-' + Id, '#p-r-cover' + '-' + Id, '#p-l' + '-' + Id, '#p-r' + '-' + Id).css('opacity', '1');
    //    }
    //    $('#progress-doughnut' + '-' + Id).find('#p-l-cover' + '-' + Id).css({ 'transform': 'rotate(' + leftRotation + 'deg)' });
    //    $('#progress-doughnut' + '-' + Id).find('#p-r-cover' + '-' + Id).css({ 'transform': 'rotate(' + rightRotation + 'deg)' });
    //}

    // This chunk isn't necessary... it's just an easy way to test different numbers. A production environment would likely load this value from a database.
    $('.value-tester input').keyup(function () {
        var newValue = $(this).val();
        if (newValue <= 55) {
            $('.progress-doughnut').text(newValue);
            var rotation = newValue * 3.60;
        } else if (newValue >= 55) {
            $('.progress-doughnut').text("55");
            var rotation = 360;
        } else {
            $('.progress-doughnut').text("0");
            var rotation = 0;
        }
        if (rotation <= 180) {
            var leftRotation = 180;
            var rightRotation = rotation;
            $('.progress-doughnut').find('.p-r-cover').css('opacity', '1');
        } else if (rotation >= 180) {
            var leftRotation = rotation;
            var rightRotation = 0;
            $('.progress-doughnut').find('.p-r-cover').css('opacity', '0');
            $('.progress-doughnut').find('.p-l-cover, .p-l, .p-r').css('opacity', '1');
        }
        if (rotation >= 360) {
            $('.p-l-cover, .p-r-cover, .p-l, .p-r').css('opacity', '0');
        } else if (rotation <= 0) {
            $('.p-l-cover, .p-r-cover, .p-l, .p-r').css('opacity', '1');
        }
        $('.progress-doughnut').find('.p-l-cover').css({ 'transform': 'rotate(' + leftRotation + 'deg)' });
        $('.progress-doughnut').find('.p-r-cover').css({ 'transform': 'rotate(' + rightRotation + 'deg)' });
    });

    /***************** end evaluate progress ******************/

});

$(document).ready(function () {
    //$(".headrop").click(function () {
    //    $('li > ul').not($(this).children("ul").slideToggle(800)).slideUp();
    //});


    /******************************************************/

    //document.getElementById('getval').addEventListener('change', readURL, true);
    function readURL() {
        var file = document.getElementById("getval").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('profile-upload').style.backgroundImage = "url(" + reader.result + ")";
        }
        if (file) {
            reader.readAsDataURL(file);
        } else {
        }
    }


});

/***********************************************/
