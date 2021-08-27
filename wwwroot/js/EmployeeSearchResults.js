$(document).ready(function ()
{
   
    var allth = $('label');
    $(".datepicker").datepicker();
    $("#divEmployeeSearchResult").on("click", '.labelclicked', function () {

       
        //Create an object
        var viewActiveEmployees_HRapp =
        {
            EmpID: $("#txtNUID").val(),
            LastName: $("#txtEmployeeName").val().trim(),
            HREmpID: $("#txtPersonNumber").val().trim(),
            Status: $("#cboStatus").val().trim(),
            Exempt: $("#cboExempt").val().trim(),
            SupFirstName: $("#txtSupervisor").val().trim(),
            PositionID: $("#txtPositionNumber").val().trim(),
            JobCode: $("#txtJobCode").val().trim(),
            JobTitle: $("#txtJobTitle").val().trim(),
        }


        var ColumnID = $(this).attr('id');
        var Label = '#' + ColumnID;
       
        var CurrentSortOrder = $(this).data('sortorder');

        ShowWait();
        var URL = $("#HiddenBaseFolder").val() + "Index?handler=EmployeeSearchResults";
        $.ajax({
            type: 'POST',
            // async: false,
            url: URL,
            headers:
            {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },

            data: {
                viewActiveEmployees_HRapp: viewActiveEmployees_HRapp,
                SortColumn: ColumnID,
                SortOrder: CurrentSortOrder
            },
            success: function (data) {

                $("#divEmployeeSearchResult").html(data)
                if (CurrentSortOrder == "DSD")
                {

                    $(Label).attr('data-sortorder', 'ASD');
                }
                if (ColumnID == "Name") {
                    $("#divName").css({ 'width': 25 + '%' });
                    $("#divName").css('float', 'left');
                    $("#divName").css('margin-left', '1em');
                    $("#divName").css('background', '#9f8341');
                    $("#divName").css({ 'height': 25 + 'px' });
                    $("#divName").css('text-align', 'center');
                }
                if (ColumnID == "JobTitle") {
                    $("#divJobTitle").css({ 'width': 25 + '%' });
                   // $("#divJobTitle").css('margin-left', '1em');
                    $("#divJobTitle").css('background', '#9f8341');
                    $("#divJobTitle").css({ 'height': 25 + 'px' });
                    $("#divJobTitle").css('text-align', 'center');

                }
                if (ColumnID == "EmpID") {
                    $("#divEmpID").css({ 'width': 25 + '%' });
                  //  $("#divEmpID").css('margin-left', '2em');
                    $("#divEmpID").css('background', '#9f8341');
                    $("#divEmpID").css({ 'height': 25 + 'px' });
                    $("#divEmpID").css('text-align', 'center');

                }
                if (ColumnID == "Status") {
                    $("#divStatus").css({ 'width': 25 + '%' });
                    $("#divStatus").css('margin-left', '1em');
                    $("#divStatus").css('background', '#9f8341');
                    $("#divStatus").css({ 'height': 25 + 'px' });
                    $("#divStatus").css('text-align', 'center');

                }
                if (ColumnID == "Supervisor") {
                    $("#divSupervisor").css({ 'width': 25 + '%' });
                    $("#divSupervisor").css('margin-left', '4em');
                    $("#divSupervisor").css('background', '#9f8341');
                    $("#divSupervisor").css({ 'height': 25 + 'px' });
                    $("#divSupervisor").css('text-align', 'center');

                }
                if (ColumnID == "FTE") {
                    $("#divFTE").css({ 'width': 25 + '%' });
                    $("#divFTE").css('margin-right', '9em');
                    $("#divFTE").css('background', '#9f8341');
                    $("#divFTE").css({ 'height': 25 + 'px' });
                    $("#divFTE").css('text-align', 'center');

                }

                HideWait();
            },
            error: function (args)
            {
                var $dialog = $('<div>Error occured while sorting column</div>')
                    .dialog
                    ({
                        title: 'ERROR!',
                        width: 300,
                        height: 200,
                        buttons:
                            [
                                {
                                    text: "Close", click: function () {
                                        $dialog.dialog('close');
                                        // You can do anything else here that needs to be done.
                                    }
                                }]
                    });

            }
        });

    });

//=============================================================================================================================================================
    $("#divEmployeeSearchResult").on("click", '#EditEmpRecord', function ()
    {
        var EmpID = $(this).data('empid');

       // alert(EmpID)

        ShowWait();
        $.ajax({
            cache: false,
            url: $("#HiddenBaseFolder").val() + 'Privacy?handler=EmployeeDetails&EmpID=' + EmpID,
            method: "GET",
            data:
            {

            },
            success: function (data)
            {
                $("#divEmployeeDetails").html(data)
                HideWait();
            }
        })
        return;

        $("#EmployeeSearchResult").remove();
        $("#EmployeeSearchResult").dialog('destroy');

        ShowWait();
        var $dialog = $("<div id='EmployeeSearchResult'><div/>").dialog
            (
                {

                    maxWidth: 3000,
                    maxHeight: 1500,
                    width: 2320,
                    height: 1000,
                    show:
                    {
                        effect: 'fade',
                        duration: 1000
                    },
                    hide: {
                        effect: 'fold',
                        duration: 1000
                    },
                    title: 'Employee Detals',
                    modal: true,
                    open: function () {
                        // $(this).siblings('.ui-dialog-titlebar').hide();
                    },
                    close: function () {
                        $dialog.dialog('destroy');
                        $("#divEmployeeDetails").css('visibility', 'hidden');
                    }


                }).load($("#HiddenBaseFolder").val() + 'Privacy?handler=EmployeeDetails&EmpID=' + EmpID,
                    function () {
                        HideWait();
                        $("#divEmployeeDetails").css('visibility', 'visible');
                       // $("#divEmployeeDetails").html(data)

                    });

    })
});