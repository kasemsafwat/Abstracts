var DepartmentID = 0;
var SettingID = 0;

$(document).ready(function () {
    $("#RequestDate").addClass("form-control");
    $("#Company").addClass("form-control");
    $(".k-combobox").addClass("form-control");
    $('#VicePresidentchk').on('click', function (e) {
        if ($('#VicePresidentchk').is(":checked") === true)
            $("#VicePresident").prop('disabled', false);
        else
            $("#VicePresident").prop('disabled', true);
    });
    $('#Engineerchk').on('click', function (e) {
        if ($('#Engineerchk').is(":checked") === true)
            $("#Engineer").prop('disabled', false);
        else
            $("#Engineer").prop('disabled', true);
    });
    $('#GeneralDirectorchk').on('click', function (e) {
        if ($('#GeneralDirectorchk').is(":checked") === true)
            $("#GeneralDirector").prop('disabled', false);
        else
            $("#GeneralDirector").prop('disabled', true);
    });
    $('#GeneralManagerchk').on('click', function (e) {
        if ($('#GeneralManagerchk').is(":checked") === true)
            $("#GeneralManager").prop('disabled', false);
        else
            $("#GeneralManager").prop('disabled', true);
    });

});
function onChangeDepartment(args) {

    $.ajax(
        {
            type: "POST",
            url: '/Setting/GetSettingData',
            data: {
                DepartmentID: args.dataItem.DepartmentId
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {
                if (result === -1) {
                }
                else if (result.Engineer !== null) {
                    $("#Engineer").val(result.Engineer);
                    $("#Engineer").prop('disabled', false);
                    $('#Engineerchk').prop('checked', true);
                }
                else {
                    $("#Engineer").val('');
                    $("#Engineer").prop('disabled', true);
                    $('#Engineerchk').prop('checked', false);
                }
                if (result.GeneralManager !== null) {
                    $("#GeneralManager").val(result.GeneralManager);
                    $("#GeneralManager").prop('disabled', false);
                    $('#GeneralManagerchk').prop('checked', true);
                }
                else {
                    $("#GeneralManager").val('');
                    $("#GeneralManager").prop('disabled', true);
                    $('#GeneralManagerchk').prop('checked',false);
                }


                if (result.GeneralDirector !== null) {
                    $("#GeneralDirector").val(result.GeneralDirector);
                    $("#GeneralDirector").prop('disabled', false);
                    $('#GeneralDirectorchk').prop('checked', true);
                }
                else {
                    $("#GeneralDirector").val('');
                    $("#GeneralDirector").prop('disabled', true);
                    $('#GeneralDirectorchk').prop('checked', false);
                }
                if (result.VicePresident !== null) {
                    $("#VicePresident").val(result.VicePresident);
                    $("#VicePresident").prop('disabled', false);
                    $('#VicePresidentchk').prop('checked', true);
                }
                else {
                    $("#VicePresident").val('');
                    $("#VicePresident").prop('disabled', true);
                    $('#VicePresidentchk').prop('checked',false);
                }
               

                //$("#GeneralManager").val(result.GeneralManager);
                //$("#GeneralDirector").val(result.GeneralDirector);
             
                //$("#VicePresident").val(result.VicePresident);
             
            }
        });

}

function saveSetting() {
    if ($('#Engineerchk').is(":checked") === false) {
        $('#Engineer').val('');
    }
    if ($('#GeneralManagerchk').is(":checked") === false) {
        $('#GeneralManager').val('');
    }
    if ($('#GeneralDirectorchk').is(":checked") === false) {
        $('#GeneralDirector').val('');
    }
    if ($('#VicePresidentchk').is(":checked") === false) {
        $('#VicePresident').val('');
    }

    $.ajax(
        {
            type: "POST",
            url: '/Setting/SaveSetting',
            data: {
                Engineer: $("#Engineer").val(),
                GeneralManager: $("#GeneralManager").val(),
                GeneralDirector: $("#GeneralDirector").val(),
                Technical: $("#Technical").val(),
                VicePresident: $("#VicePresident").val(),
                President: $("#President").val(),
                DepartmentId: $("#Department").val()

            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {
              
                    alert("تم الحفظ بنجاح");
                
            }
        });
}