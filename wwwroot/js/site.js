$(document).ready(function ()
{
     $(".datepicker").datepicker(); //This code binds a calender to any input field that has a class datapicker in  html code.

//=========================================================================================================================================================

    //THIS METHOD PREDICTS THE 'Employee' THE USER IS LOOKING FOR.
    var URL = $("#HiddenBaseFolder").val() + "Index?handler=EmployeeName";
    $("#txtEmployeeName").autocomplete({
        source: URL,
        Method: "GET",
        minLength: 1 //AFTER THE 1ST CHARACTER TYPED IN , THE LIST COMES UP.
    });

//=========================================================================================================================================================

    //THIS METHOD PREDICTS THE 'Employee NUID' THE USER IS LOOKING FOR.
    var URL = $("#HiddenBaseFolder").val() + "Index?handler=EmployeeNUID";
    $("#txtNUID").autocomplete({
        source: URL,
        Method: "GET",
        minLength: 1 //AFTER THE 1ST CHARACTER TYPED IN , THE LIST COMES UP.
    });
 //=========================================================================================================================================================

    //THIS METHOD PREDICTS THE 'Employee PersonsNumber' THE USER IS LOOKING FOR.
    var URL = $("#HiddenBaseFolder").val() + "Index?handler=PersonsNumber";
    $("#txtPersonNumber").autocomplete({
        source: URL,
        Method: "GET",
        minLength: 3 //AFTER THE 1ST CHARACTER TYPED IN , THE LIST COMES UP.
    });
 //=========================================================================================================================================================

    //THIS METHOD PREDICTS THE 'Employee Supervisor' THE USER IS LOOKING FOR.
    var URL = $("#HiddenBaseFolder").val() + "Index?handler=Supervisor";
    $("#txtSupervisor").autocomplete({
        source: URL,
        Method: "GET",
        minLength: 1 //AFTER THE 1ST CHARACTER TYPED IN , THE LIST COMES UP.
    });
 //=========================================================================================================================================================

    //THIS METHOD PREDICTS THE 'Employee PositionNumber' THE USER IS LOOKING FOR.
    var URL = $("#HiddenBaseFolder").val() + "Index?handler=PositionNumber";
    $("#txtPositionNumber").autocomplete({
        source: URL,
        Method: "GET",
        minLength: 1 //AFTER THE 1ST CHARACTER TYPED IN , THE LIST COMES UP.
    });

//=========================================================================================================================================================

    //THIS METHOD PREDICTS THE 'Employee JobCode' THE USER IS LOOKING FOR.
    var URL = $("#HiddenBaseFolder").val() + "Index?handler=JobCode";
    $("#txtJobCode").autocomplete({
        source: URL,
        Method: "GET",
        minLength: 1 //AFTER THE 1ST CHARACTER TYPED IN , THE LIST COMES UP.
    });
//=========================================================================================================================================================

    //THIS METHOD PREDICTS THE 'Employee JobTitle' THE USER IS LOOKING FOR.
    var URL = $("#HiddenBaseFolder").val() + "Index?handler=JobTitle";
    $("#txtJobTitle").autocomplete({
        source: URL,
        Method: "GET",
        minLength: 1 //AFTER THE 1ST CHARACTER TYPED IN , THE LIST COMES UP.
    });
//=========================================================================================================================================================
    $('#btnSearchEmployee').click(function (e)
    {
       
        //validate required fields.
        var foundempty = false;
        if ($("#txtEmployeeName").val() == 0 && $("#txtNUID").val() == "" && $("#txtPersonNumber").val() == "" && $("#cboStatus").val() == "" && $("#cboExempt").val() == ""
            && $("#txtSupervisor").val() == "" && $("#txtPositionNumber").val() == "" && $("#txtJobCode").val() == "" && $("#txtJobTitle").val() == "")
        {
            $("#txtEmployeeName").css('background', 'yellow')
            $("#txtNUID").css('background', 'yellow')
            $("#txtPersonNumber").css('background', 'yellow')
            $("#cboStatus").css('background', 'yellow')
            $("#cboExempt").css('background', 'yellow')
            $("#txtSupervisor").css('background', 'yellow')
            $("#txtPositionNumber").css('background', 'yellow')
            $("#txtJobCode").css('background', 'yellow')
            $("#txtJobTitle").css('background', 'yellow')
            $("#txtEmployeeName").focus();

            foundempty = true;

            var $dialog = $('<div>Please provide a value for one of the highlighted fields before searching.</div>')
                .dialog
                ({
                    title: 'One of the search fields must have a value',
                    width: 500,
                    height: 260,
                    buttons:
                        [
                            {
                                text: "OK", click: function () {
                                    $dialog.dialog('close');

                                }
                            }]
                });


        }

        if (foundempty) {
            return false
        }
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

            data: { viewActiveEmployees_HRapp: viewActiveEmployees_HRapp },
            success: function (data)
            {
               
                $("#divEmployeeSearchResult").html(data)
                HideWait();
            },
            error: function (args)
            {
                var $dialog = $('<div>Error occured while getting data</div>')
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
});