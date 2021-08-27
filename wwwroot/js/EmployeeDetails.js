$(document).ready(function ()
{
    // alert()
    $(".datepicker").datepicker();

    $("#divEmployeeDetails").on("click", '#txtPhonedate', function () {
        $(".datepicker").datepicker();
    });
});