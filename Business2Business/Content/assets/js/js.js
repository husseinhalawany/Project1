/**
* Author: LimpidThemes
* Version: 1.0
* Description: Javascript file for the theme
* Date: 20-07-2015
**/

/**********************************************************
		BEGIN: PRELOADER
**********************************************************/
//$(window).load(function() {
//	"use strict";
//	$("#loader").fadeOut("slow");
//});

/**********************************************************
		BEGIN: OWL CAROUSELS
**********************************************************/
jQuery.noConflict();
jQuery(document).ready(function($) {
   "use strict";
    if(jQuery().owlCarousel) { 
		/* BLOG POST CAROUSEL */
		if (jQuery("#post-list").length){
			jQuery("#post-list").owlCarousel({
				loop:true,
				margin:30,
				responsiveClass:true,
				autoplay:false,
				autoplayTimeout:5000,
				navigation:false,
				stopOnHover:true,
				responsive:{
					0:{
						items:1,
						loop:true
					},
					600:{
						items:2,
						loop:true
					},
					1000:{
						items:4,
						loop:true
					}
				}
			});	
		}
		
		/* HOMEPAGE OFFER SLIDER */
		if (jQuery("#offer1").length){
			jQuery("#offer1").owlCarousel({
				loop:true,
				responsiveClass:true,
				autoplay:true,
				autoplayTimeout:5000,
				navigation:false,
				stopOnHover:true,
				responsive:{
					1000:{
						items:1,
						loop:true
					}
				}
			});	
		}
		
		/* index-4.html FLIGHT OFFER SLIDER */
		if (jQuery("#flightoffer").length){
			jQuery("#flightoffer").owlCarousel({
				loop:true,
				responsiveClass:true,
				autoplay:false,
				autoplayTimeout:5000,
				navigation:false,
				stopOnHover:true,
				responsive:{
					0:{
						items:1,
						loop:true
					}
				}
			});	
		}
		
		/* ABOUT US PAGE PRTNERS SLIDER */
		if (jQuery("#partner").length){
			jQuery("#partner").owlCarousel({
				loop:true,
				margin:20,
				responsiveClass:true,
				autoplay:true,
				autoplayTimeout:5000,
				navigation:false,
				stopOnHover:true,
				responsive:{
					0:{
						items:1,
						loop:true
					},
					600:{
						items:2,
						loop:true
					},
					1000:{
						items:4,
						loop:true
					}
				}
			});	
		}
		
		/* LAST MINUTE DEALS SLIDER */
		
		if (jQuery("#lastminute").length){
			jQuery("#lastminute").owlCarousel({
				loop:true,
				responsiveClass:true,
				margin:30,
				autoplay:false,
				autoplayTimeout:5000,
				navigation:false,
				stopOnHover:true,
				responsive:{
					0:{
						items:1,
						loop:true
					},
					600:{
						items:2,
						loop:true
					},
					1000:{
						items:4,
						loop:true
					}
				}
			});
		}
		if (jQuery("#review-customer").length){
			jQuery("#review-customer").owlCarousel({
				loop:true,
				margin: 10,
				responsiveClass:true,
				autoplay:true,
				autoplayTimeout:5000,
				navigation:false,
				stopOnHover:true,
				responsive:{
					0:{
						items:1,
						loop:true
					},
					600:{
						items:1,
						loop:true
					},
					1000:{
						items:1,
						loop:true
					}
				}
			});
		}
		if (jQuery("#lowest-fare").length){
			jQuery("#lowest-fare").owlCarousel({
				loop:true,
				margin:10,
				responsiveClass:true,
				autoplay:true,
				autoplayTimeout:5000,
				navigation:true,
				stopOnHover:true,
				responsive:{
					0:{
						items:2,
						loop:true,
						navText:["<i class='fa fa-chevron-left owl-navigation-icon-blue'>","<i class='fa fa-chevron-right owl-navigation-icon-blue'>"],
						nav:true
					},
					600:{
						items:3,
						loop:true,
						navText:["<i class='fa fa-chevron-left owl-navigation-icon-blue'>","<i class='fa fa-chevron-right owl-navigation-icon-blue'>"],
						nav:true
					},
					1000:{
						items:5,
						loop:true,
						navText:["<i class='fa fa-chevron-left owl-navigation-icon-blue'>","<i class='fa fa-chevron-right owl-navigation-icon-blue'>"],
						nav:true
					}
				}
			});
		}
	}
});


/***************************************************************
		BEGIN: VARIOU DATEPICKER & SPINNER INITIALIZATION
***************************************************************/
jQuery(function ($) {
		"use strict";
		$("#departure_date").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy" });
		$(".date").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy" });
		//$(".itinerary-date").datetimepicker({ viewMode: "years", dateFormat: "dd/mm/yy" });
		$('.itinerary-date').datetimepicker({ weekStart: 1, todayBtn: true, autoclose: true, todayHighlight: true, startView: 4, minView: 2, forceParse: 0 });
		//$("#departure_date").datepicker('setDate','today');
		$("#return_date").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy"});
		//$("#return_date").datepicker('setDate', 'today');
		$("#check_out").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy"});
		$("#check_in").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy" });
		$("#package_start").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy"});
		$("#car_start").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy"});
		$("#car_end").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy"});
		$("#cruise_start").datepicker({ minDate: -0, maxDate: "+12M", dateFormat: "dd/mm/yy"});
		$( "#adult_count" ).spinner({
			min: 1
		});
		$( "#child_count" ).spinner( {
			min: 0
		});
		$("#infant_count").spinner({
		    min: 0
		});
		$( "#hotel_adult_count" ).spinner( {
			min: 1
		});
		$( "#hotel_child_count" ).spinner( {
			min: 0
		});
		$("#hotel_infant_count").spinner({
		    min: 0
		});
		$("#hotel_room_count").spinner({
		    min: 1
		});
		$('.selectpicker').selectpicker({
			style: 'custom-select-button'
		});
});
jQuery(function ($) {
    $('.typeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 3,
        limit: 20
    }, {
        source: function (request, response) {

            $.ajax({
                url: '/Home/AutoCompleteCity',
                data: "{ 'term': '" + request + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            value: item.Name + ',' + item.CountryName,
                            id: item.CityCode
                        }
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        }
    });
    $('.typeaheadf').typeahead({
        hint: true,
        highlight: true,
        minLength: 3,
        limit: 20
    }, {
        source: function (request, response) {

            $.ajax({
                url: '/Home/AutoCompleteAirport',
                data: "{ 'term': '" + request + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            value: item.CityName + ', ' + item.CountryName + ', (' + item.Code + ') - ' + item.Name,
                            id: item.Code
                        }
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        }
    });
});

jQuery(function ($) {
    $("#OneWay").click(function () {
        $("#end").hide();
    });
    $("#Round").click(function () {
        $("#end").show();
    });
});
jQuery(function ($) {
    $(function () {
        LoadPreferredAirline();
    });


    function LoadPreferredAirline() {
        $("#Loader").show();
        $.ajax({
            url: "/Home/LoadPreferredAirline",
            cache: false,
            success: function (data) {
                $("#divPreferredAirline").html(data);
                $("#Loader").hide();
            }
        });
    }
});
jQuery(function ($) {
    var page = 0;
    var _inCallback = false;

    function loadFlights() {
        if (page > -1 && !_inCallback) {
            _inCallback = true;
            page++;
            $('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
            $.get("/Flights/FlightsList/" + page, function (data) {
                if (data != '') {
                    $("#FlightsList").append(data);
                }
                else {
                    page = -1;
                }
                $(".viewmodal").click(function () {
                    var previewContainer = $('#flightdetailsmodal #mymodal');
                    var ppkey = $(this).data("ppkey");
                    previewContainer.html('<p>Loading content...</p>');
                    previewContainer.load(displayUrl, { PPKey: ppkey });

                    $('#flightdetailsmodal').modal('show');
                });
                _inCallback = false;
                $('div#loading').empty();
            });

        }
    }

    var dcList = true;

    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {

            loadFlights();
            
        }
    });
});

jQuery(function ($) {
    var NumOfStops = [];
    var priceslider;
    var ClassType = [];
    var AirLines = [];
    var DepartureTime = [];
    var FillNumOfStops = [];
    var FillClassType = [];
    var FillAirLines = [];
    var FillDepartureTime = [];
    $(".i-check1").click(function () {
        $('#NumOfStops .i-check1:checked').each(function () {
            FillNumOfStops.push($(this).val());
        });

        priceslider = $('#price-slider').val();

        $('#ClassType .i-check1:checked').each(function () {
            FillClassType.push($(this).val());
        });
        $('#AirLines .i-check1:checked').each(function () {

            FillAirLines.push($(this).val());

        });


        $('#DepartureTime .i-check1:checked').each(function () {
            FillDepartureTime.push($(this).val());
        });
        NumOfStops = FillNumOfStops;
        FillNumOfStops = [];

        ClassType = FillClassType;
        FillClassType = [];

        AirLines = FillAirLines;
        FillAirLines = [];

        DepartureTime = FillDepartureTime;
        FillDepartureTime = [];
        $.ajax({

            url: "/Flights/filter",
            type: "POST",
            cache: false,
            asyn:false,
            data: { "NumOfStops": NumOfStops, "ClassType": ClassType, "AirLines": AirLines, "DepartureTime": DepartureTime, "priceslider": priceslider },
            success: function (data) {
                $("#FlightsList").html(data);
                $(".viewmodal").click(function () {
                    var previewContainer = $('#flightdetailsmodal #mymodal');
                    var ppkey = $(this).data("ppkey");
                    previewContainer.html('<p>Loading content...</p>');
                    previewContainer.load(displayUrl, { PPKey: ppkey });

                    $('#flightdetailsmodal').modal('show');
                });
            }
        });
        
    });
});
jQuery(function ($) {
    $(".viewmodal").click(function () {

        var previewContainer = $('#flightdetailsmodal #mymodal');
        var ppkey = $(this).data("ppkey");
        previewContainer.html('<p>Loading content...</p>');
        previewContainer.load(displayUrl, { PPKey: ppkey });

        $('#flightdetailsmodal').modal('show');
    });
});
jQuery(function ($) {
    $(".viewhotelpolicy").click(function () {

        var previewContainer = $('#hotelPolicymodal #mymodal');
        var hotelid = $(this).data("hotelid");
        var roomid = $(this).data("roomid");
        previewContainer.html('<p>Loading content...</p>');
        previewContainer.load(confirmUrl, { hotelId: hotelid, roomId: roomid });

        $('#hotelPolicymodal').modal('show');
    });
});
jQuery(function ($) {
    var page = 0;
    var _inCallback = false;

    function loadHotels() {
        if (page > -1 && !_inCallback) {
            _inCallback = true;
            page++;
            $('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
            $.get("/Hotels/HotelsList/" + page, function (data) {
                if (data != '') {
                    $("#HotelsList").append(data);
                }
                else {
                    page = -1;
                }

                _inCallback = false;
                $('div#loading').empty();
            });
        }
    }

    var dcList = true;

    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {

            loadHotels();
        }
    });
});



//.slideToggle = DropDown Slides Down
//.toggle = DropDown Slides from TopLeft

jQuery(document).ready(function ($) {
  $('.main-bar').on('click',function(){
    $('ul').slideToggle(280);
    //$('ul').toggle(280);
  });
})
