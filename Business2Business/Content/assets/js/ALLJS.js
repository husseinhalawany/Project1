/******************************************************************************
                        General
******************************************************************************/

function adjustCircle(graphValue, id) {
    var rotation = graphValue * 7.20; // Mathing up some rotational degrees
    var leftRotation;
    var rightRotation;
    if (rotation <= 180 && rotation >= 0) {
        leftRotation = 180;
        rightRotation = rotation;
        $('#progress-doughnut' + '-' + id).find('#p-r-cover' + '-' + id).css('opacity', '1');
    }
    else if (rotation >= 180) {
        leftRotation = rotation;
        rightRotation = 0;
        $('#progress-doughnut' + '-' + id).find('#p-r-cover' + '-' + id).css('opacity', '0');
        $('#progress-doughnut' + '-' + id).find('#p-l-cover' + '-' + id, '#p-l' + '-' + id, '#p-r' + '-' + id).css('opacity', '1');
    }
    if (rotation >= 360) {
        $('#p-l-cover' + '-' + id, '#p-r-cover' + '-' + id, '#p-l' + '-' + id, '#p-r' + '-' + id).css('opacity', '0');
    }
    else if (rotation <= 0) {
        $('#p-l-cover' + '-' + id, '#p-r-cover' + '-' + id, '#p-l' + '-' + id, '#p-r' + '-' + id).css('opacity', '1');
    }
    $('#progress-doughnut' + '-' + id).find('#p-l-cover' + '-' + id).css({ 'transform': 'rotate(' + leftRotation + 'deg)' });
    $('#progress-doughnut' + '-' + id).find('#p-r-cover' + '-' + id).css({ 'transform': 'rotate(' + rightRotation + 'deg)' });
    $('#percent' + '-' + id).text(graphValue);
}
function dialog_box() {
    $("#myModal").on("show", function () {    // wire up the OK button to dismiss the modal when shown
        $("#myModal a.btn").on("click", function (e) {
            console.log("button pressed");   // just as an example...
            $("#myModal").modal('hide');     // dismiss the dialog
        });
    });
    $("#myModal").on("hide", function () {    // remove the event listeners when the dialog is dismissed
        $("#myModal a.btn").off("click");
    });

    $("#myModal").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
        $("#myModal").remove();
    });

    $("#myModal").modal({                    // wire up the actual modal functionality and show the dialog
        "backdrop": "static",
        "keyboard": true,
        "show": true,                     // ensure the modal is shown immediately

    });
}
function showImage(imageUrl) {
    jQuery.ajax({
        url: "/ImageUpload/showImage",
        type: "GET",
        data: { "imageUrl": imageUrl },
        success: function (data) {
            jQuery("#Image-dialog").empty();
            jQuery("#Image-dialog").html(data);
        }
    });
    dialog_box();
}
/******************************************************************************
                        Items
******************************************************************************/

function AddItem(storyId) {
    jQuery.ajax({
        url: '/Items/Create?storyId=' + storyId,
        type: "GET",
        data: {},
        success: function (data) {

            jQuery("#Create-dialog").html(data);
        }
    });
    dialog_box();
}
function EditItem(id){

    jQuery.ajax({
        url: "/Items/Edit",
        type: "Get",
        data: { "id": id },
        success: function (data) {
            jQuery("#Edit-dialog").html(data);
           
        }
    });
    dialog_box();
}
function IncludeInSprint(itemId) {
    jQuery.ajax({
        url: '/Items/AddOrRemoveItemInSprint?itemId=' + itemId,
        type: "GET",
        data: {},
        success: function (data) {

        }
    });
    
} 
function AddItemsToSprint() {
    jQuery.ajax({
        url: '/Items/AddItemsToSprint',
        type: "POST",
        data: {},
        success: function (data) {

        }
    });

}
function loadSprintItems() {
    
    var searchTxt = $("#searchTxt").val();
    if (!_inCallback) {
       
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        
        jQuery.get("/Items/SprintItemsList?searchTxt=" + searchTxt, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#sprintItemsList").html(data);

            }
            else {

            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
/******************************************************************************
                        Sprints
******************************************************************************/
function loadSprints(projectId) {
    if (!_inCallback) {
        
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Sprints/SprintsList?projectId=" + projectId, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#SprintList").html(data);
            }
            else {
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function loadSprintsDetails(projectId) {

    if (!_inCallback) {
        _inCallback = true;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Sprints/SprintsDetailsList?projectId=" + projectId, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#SprintList").append(data);
            }
            else {
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}

function InsertSprint(projectId) {
    jQuery.ajax({
        url: '/Sprints/Create?projectId=' + projectId,
        type: "GET",
        data: {},
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });
    dialog_box();
}

function EditSprint(id) {
    jQuery.ajax({
        url: '/Sprints/Edit?id=' + id,
        type: "GET",
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });
    dialog_box();
}
function RenameSprint(id) {
    jQuery.ajax({
        url: '/Sprints/Rename?id=' + id,
        type: "GET",
        data: {},
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });
    dialog_box();
}
function SprintDetails() {
    jQuery.get("/Sprints/SprintProjects", function (data) {
        if (data != '') {
            jQuery("form").unbind("submit");
            jQuery("#SprintDetails").html(data);
        }
        else {
        }

        _inCallback = false;
        jQuery('div#loading').empty();
    });
}

function InsertNewProjectSprint(projectName) {
    jQuery.ajax({
        url: '/Projects/CreateNewProjectAndAssignToSprint',
        type: "Post",
        data: { "projectName": projectName },
        success: function (data) {

            if (data != "success") {
                alert("There is Project with Same Name");
            }
            else { window.location.href = "/Sprints/Index"}
        }
    });

}

function InsertSprintProject(projectName) {
    jQuery.ajax({
        url: '/Sprints/AssignProjectToSprint',
        type: "Post",
        data: { "projectName": projectName },
        success: function (data) {

            if (data != "success") {
                alert("There is an error");
            }
            else { window.location.href = "/Sprints/Index" }
        }
    });

}
function loadSprintProjects() {

    if (!_inCallback) {
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Sprints/SprintProjectsList", function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#sprintProjectList").html(data);

            }
            else {

            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function loadSprintProjectsSearched() {
    var searchTxt = $("#searchProjectTxt").val();

    if (!_inCallback) {
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Sprints/SprintProjectsList?searchTxt=" + searchTxt, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#sprintProjectList").html(data);

            }
            else {

            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function searchForProjects() {
    jQuery('div#sprintProjectList').empty();
    jQuery(function () {
        jQuery.ajax({
            url: '/Projects/SearchTextChanged',
            type: "GET",
            data: { "searchProjectTxt": jQuery("#searchProjectTxt").val() },
            success: function (data) {
                loadSprintProjects(jQuery("#searchProjectTxt").val());
            }
        });

    });
}
function SetCurrentSprint() {
    jQuery.ajax({
        url: "/Sprints/SetCurrent",
        type: "GET",
        data: {},
        success: function () {
            window.location.href = "~/Home/Index"
        }
    })
}

/******************************************************************************
                        Projects
******************************************************************************/
function loadProjects() {

    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Projects/ProjectList?pageNo=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#ProjectsList").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}

function InsertProject() {
    jQuery.ajax({
        url: '/Projects/Create',
        type: "GET",
        data: {},
        success: function (data) {

            jQuery("#Create-dialog").html(data);
        }
    });
    dialog_box();
}
function EditProject(ID) {

    jQuery.ajax({
        url: "/Projects/Edit",
        type: "Get",
        data: { "id": ID },
        success: function (id) {
            jQuery("#Create-dialog").html(id);
        }
    });
    dialog_box();
}
/******************************************************************************
                           Users Projects
*******************************************************************************/

function loadProjectsUsers(projectId) {


    jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
    jQuery.get("/UserProjects/UserProjectsList?projectId=" + projectId, function (data) {
        if (data != '') {
            jQuery("form").unbind("submit");
            jQuery("#ProjectsList").html(data);
        }
        jQuery('div#loading').empty();
    });

}
function loadUsersInProject(projectId) {


    jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
    jQuery.get("/UserProjects/UserProjectsListDetails?projectId=" + projectId, function (data) {
        if (data != '') {
            jQuery("form").unbind("submit");
            jQuery("#UsersInProjectList").html(data);
        }
        jQuery('div#loading').empty();
    });

}
function InsertUserProject(projectId) {
    jQuery.ajax({
        url: '/UserProjects/Create',
        type: "GET",
        data: { "projectId": projectId },
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });

    dialog_box();

}

function EditUserProject(userId, projectId) {
    jQuery.ajax({
        url: "/UserProjects/Edit",
        type: "Get",
        data: { "userId": userId, "projectId": projectId },
        success: function (id) {
            jQuery("#modal-one").html(id);
        }
    });

    dialog_box();


}
/******************************************************************************
                        Job Titles
******************************************************************************/
function loadJobTitles() {

    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/JobTitles/JobsList?pageNumber=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#JobsList").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}

function EditJobTitle(e, crntForm) {
    var Id = crntForm.attr('id');
    e.preventDefault();
    jQuery.ajax({
        url: "/JobTitles/Edit",
        type: "Get",
        data: { "id": Id },
        success: function (id) {
            jQuery("#Edit-dialog").html(id);
        }
    });
    dialog_box();
}



/******************************************************************************
                        Suggestions
******************************************************************************/
function loadSuggestions() {

    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;

        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Suggestions/SuggestionsList?pageNo=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#SuggestionsList").append(data);
            }
            else {
                page = -2;
            }
            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function InsertSuggestions() {

    jQuery.ajax({
        url: '/Suggestions/Create',
        type: "GET",
        data: {},
        success: function (data) {

            jQuery("#Create-dialog").html(data);

        }
    });
    dialog_box();
}
function EditSuggestion(SuggestionId) {
    jQuery.ajax({
        url: "/Suggestions/Edit?id=" + SuggestionId,
        type: "Get",
        success: function (id) {
            jQuery("#Edit-dialog").html(id);
        }
    });
    dialog_box();
}
function DetailsSuggestion(SuggestionId) {
    jQuery.ajax({
        url: "/Suggestions/Details?id=" + SuggestionId,
        type: "Get",
        success: function (id) {
            jQuery("#Details-dialog").html(id);

        }
    });
    dialog_box();
}
function sendSuggestion() {
    jQuery.ajax({
        url: "/Suggestions/Suggest",
        success: function (data) {
            jQuery("#Edit-dialog").html(data);


        }
    });
    dialog_box();
}
/******************************************************************************
                        VacationType
******************************************************************************/
function loadVacationTypes() {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;

        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/VacationTypes/VacationTypesList?pageNumber=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#tableList tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function InserVacationType(e) {
    e.preventDefault();
    jQuery.ajax({
        url: '/VacationTypes/Create',
        type: "GET",
        data: {},
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });
    dialog_box();

}
function EditVacationType(VacationTypeId) {
    jQuery.ajax({
        url: "/VacationTypes/Edit?id=" + VacationTypeId,
        type: "Get",
        success: function (id) {
            jQuery("#Edit-dialog").html(id);
        }
    });
    dialog_box();
}
/******************************************************************************
                        VacationYear
******************************************************************************/
function loadVacationYears() {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/VacationYears/VacationYearsList?pageNumber=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#annotations tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}

/******************************************************************************
                        OccasionVacation
******************************************************************************/
function loadOccasionVacations() {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');

        jQuery.get("/OccasionVacations/OccasionVacationsList?pageNumber=" + page + "&vacationYearId=" + vacationYearId + "&startDate=" + startDate, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#annotations tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
/******************************************************************************
                        UsersVacation
********************************************************************************/
function loadUsersVacations(VacationStatusId) {

    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;

        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');

        jQuery.get("/UsersVacations/UsersVacationsList?pageNumber=" + page + "&statusId=" + VacationStatusId, function (data) {

            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#tableList tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }

}
function loadCompletedVacations() {

    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;

        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');

        jQuery.get("/UsersVacations/CompletedVacationsList?pageNumber=" + page, function (data) {

            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#tableList tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }

}
function getRemainingVacationDays(userId, vacationId, year) {
    jQuery.get("/EmployeeVacations/GetRemainingVacationDays?userId=" + userId + "&vacationTypeId=" + vacationId + "&year=" + year, null, function (data) {
        jQuery("#remainingdays").html(data);
    });
}
/******************************************************************************
                        UserSign
******************************************************************************/
function loadUserSign() {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Attendances/UsersNotSignOutList?pageNo=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#UserSignList").append(data);
            }
            else {
                page = -2;
            }
            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function SignOutUser(id) {
    jQuery.ajax({
        url: "/Attendances/SetSignOut?id=" + id,
        type: "Get",
        success: function () {
            window.location.href = "/Attendances/Index";

        }
    });
}
function loadUserSignToDat() {
    if (pageOne > -2 && !_inCallback) {
        _inCallback = true;
        pageOne++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Attendances/GetAllUserSignInList?pageNo=" + pageOne, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#loadUserSignToDat").append(data);
            }
            else {
                pageOne = -2;
            }
            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function loadUserLeave() {
    if (pageTwo > -2 && !_inCallback) {
        _inCallback = true;
        pageTwo++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Attendances/GetUserLeaveList?pageNo=" + pageTwo, function (data) {
            if (data != null) {
                jQuery("form").unbind("submit");
                jQuery("#loadUserLeave").append(data);
            }
            else {
                pageTwo = -2;
            }
            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function loadUserNotCome() {
    if (pageThree > -2 && !_inCallback) {
        _inCallback = true;
        pageThree++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Attendances/GetNoteComeUserList?pageNo=" + pageThree, function (data) {
            if (data != '') {
                //alert(data);
                jQuery("form").unbind("submit");
                jQuery("#loadUserNotCome").append(data);
            }
            else {
                pageThree = -2;
            }
            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function LoadOnlineUser() {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Employees/OnlineUserList?pageNo=" + page, function (data) {
            if (data != '') {
                //alert(data);
                jQuery("form").unbind("submit");
                jQuery("#OnlineUserList").append(data);
            }
            else {
                pageThree = -2;
            }
            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function loadUserProject(userId) {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Projects/GetUserProjectList?pageNo=" + page + "&userId=" + userId, function (data) {

            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#loadUserProject").append(data);
            }
            else {
                page = -2;
            }
            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}

/******************************************************************************
                       Employee Vacations 
******************************************************************************/
function loadEmployeeVacations(VacationStatusId, employeeUserId) {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;

        jQuery.get("/EmployeeVacations/GetVacationStatusDays?userId=" + employeeUserId + "&statusId=" + VacationStatusId
              , null, function (data) {
                  jQuery("#totaldays").html(data);

              })

        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');

        jQuery.get("/EmployeeVacations/EmployeeVacationsList?pageNumber=" + page + "&statusId=" + VacationStatusId + "&employeeUserId=" + employeeUserId, function (data) {

            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#EmployeeVacationsList").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }

}
function InsertEmployeeVacation(e, userId) {

    e.preventDefault();
    jQuery.ajax({
        url: '/EmployeeVacations/Create?userId=' + userId,
        type: "GET",
        data: {},
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });

    dialog_box();
}
function InsertWorkFromHome() {

    jQuery.ajax({
        url: '/EmployeeVacations/WorkFromHomeRequest',
        type: "GET",
        data: {},
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });

    dialog_box();
}

/******************************************************************************
                       WorkFromHome
/*******************************************************************************/
function loadEmployeeWorkFromHomeDays(statusId) {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;

        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');

        jQuery.get("/WorkFromHome/EmployeeWorkFromHomeDaysList?pageNumber=" + page + "&statusId=" + statusId, function (data) {

            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#tableList tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }

}
function InsertEmployeeWorkFromHome(e, userId) {
    e.preventDefault();
    jQuery.ajax({
        url: '/WorkFromHome/CreateWorkFromHome?userId=' + userId,
        type: "GET",
        data: {},
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });

    dialog_box();
}
/******************************************************************************
                       Stories 
******************************************************************************/
function loadStories(ProjectId) {

    if (!_inCallback) {


        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Stories/StoriesList?projectId=" + ProjectId, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#StoriesList").html(data);

            }
            else {

            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function loadStoriesSprints(ProjectId) {

    if (!_inCallback) {


        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Stories/StoriesSprintList?projectId=" + ProjectId, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#StoriesList").html(data);

            }
            else {

            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function loadStoriesSprintsSearch(ProjectId, searchTxt) {

    if (!_inCallback) {


        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Stories/StoriesSprintList?projectId=" + ProjectId + "&searchTxt=" + searchTxt, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#StoriesList").html(data);

            }
            else {

            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}
function InsertNewStory(projectid) {

    jQuery.ajax({
        url: '/Stories/Create',
        type: "GET",
        data: { "projectId": projectid },
        success: function (data) {
            jQuery("#CreateStory").html(data);
        }
    });

}
function InsertNewStorySprint(projectid,storyName) {
    jQuery.ajax({
        url: '/Stories/CreateStorySprint',
        type: "Post",
        data: { "projectId": projectid, "storyName": storyName },
        success: function (data) {

            if (data != "success") {
                alert("There is story With Same Name");
            }
            else { loadStoriesSprints(projectid); }
        }
        ,
    });

}
function EditStory(id) {
    jQuery.ajax({
        url: "/Stories/Edit?id=" + id,
        type: "Get",
        success: function (data) {
            jQuery("#Edit-dialog").html(data);

        }
    });
    dialog_box();
}
/******************************************************************************
                            EmployeePoints
******************************************************************************/

function loadBestEmployeeOfMonthAndQuarter() {
    jQuery.get("/EmployeePoints/BestEmployeeOfMonthAndQuarter", function (data) {
        jQuery("#BestEmployeeOfMonthAndQuarter").append(data);
    });
}
/******************************************************************************
                            UserProfiles
******************************************************************************/

function loadUsers(roleId) {
   

    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Users/UsersList?roleId=" + roleId + "&pageNo=" + page, function (data) {

            if (data != '') {

                jQuery("form").unbind("submit");
                jQuery("#UsersList").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();

        });
    }
}
function createUser(roleId) {
    window.location.href = "/Users/Create?roleId=" + roleId;

}
function loadUsersHistory() {
    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/Users/UsersHistoryList?pageNo=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#UsersHistoryList tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();

        });
    }
}
/******************************************************************************
                        ActionRates
******************************************************************************/
function loadActionRates() {

    if (page > -2 && !_inCallback) {
        _inCallback = true;
        page++;
        jQuery('div#loading').html('<img src="/Content/img/loading-image.gif" style="width:30px; height:30px;">');
        jQuery.get("/ActionRates/ActionRatesList?pageNumber=" + page, function (data) {
            if (data != '') {
                jQuery("form").unbind("submit");
                jQuery("#ActionRatesTable tbody").append(data);
            }
            else {
                page = -2;
            }

            _inCallback = false;
            jQuery('div#loading').empty();
        });
    }
}

function InsertActionRate(e) {
    e.preventDefault();
    jQuery.ajax({
        url: '/ActionRates/Create',
        type: "GET",
        data: {},
        success: function (data) {
            jQuery("#Create-dialog").html(data);
        }
    });
    dialog_box();
}

function EditActionRate(e, id) {
    e.preventDefault();
    jQuery.ajax({
        url: '/ActionRates/Edit?id=' + id,
        type: "GET",
        success: function (data) {
            jQuery("#Edit-dialog").html(data);
        }
    });
    dialog_box();
}


function GoHome() {
    window.location.href = "Home/Index";
}






