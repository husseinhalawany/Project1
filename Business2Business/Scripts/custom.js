$(function () {
    "use strict";

    $("html").niceScroll();
    
    $('[placeholder]').focus(function () {
        $(this).attr("data-text", $(this).attr("placeholder"));
        $(this).attr("placeholder", "");
    }).blur(function () {
        $(this).attr("placeholder", $(this).attr("data-Text"));
    });
   
});

$(document).ready(function () {
    $(".headrop").click(function () {
    $('li > ul').not($(this).children("ul").slideToggle(800)).slideUp();      
    });
});