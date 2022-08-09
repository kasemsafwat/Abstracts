var selected = 0;
var ConsultationId = 0;
var ConsultationUsersID = 0;
var ConsultationName = "";
var Email = "";
var Password = "";
var Name = "";
var Phone = "";

var Consultation = "";
var Request = "";

var LastselectedLength = 0;


function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
function onPaging(arg) {
    LastselectedLength = 0;
    var grid = $("#grid").data("kendoGrid");
    grid.clearSelection();
}

function onChangeSelection(arg) {
    var grid = $("#grid").data("kendoGrid");

    selected = this.selectedKeyNames();
    if (LastselectedLength === selected[0]) {
        selected = [];
        grid.clearSelection();
    }
    LastselectedLength = selected[0];
    if (selected.length === 0) {
        $("#Email").val('');
        $("#Password").val('');
        $("#Name").val('');
        $("#Phone").val('');
        $("#savebutton").html('<i class="fa fa-save"></i> إضافة');
        ConsultationUsersID = 0;
        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {
        $("#savebutton").html('<i class="fa fa-save"></i> تعديل');
        ConsultationId = selectedDataItems[0].ConsultationId;

        ConsultationUsersID = selectedDataItems[0].ConsultationUserId;
        Password = selectedDataItems[0].Password;
        Email = selectedDataItems[0].Email;
        Phone = selectedDataItems[0].PhoneNumber;
        Name = selectedDataItems[0].ConsultationName;


        $("#Email").val(Email);
        $("#Password").val(Password);
        $("#Phone").val(Phone);
        $("#Name").val(Name);




    }
}
$(document).ready(function () {
    $("#frm").validate({
        rules:
        {
            Name:
            {
                required: true
            },
            Phone:
            {
                required: true
            },
            Email:
            {
                required: true,
                email: true
            },
            Password:
            {
                required: true
            }
        },
        messages: {
            Name:
            {
                required: "من فضلك أدخل الإسم"
            },
            Phone:
            {
                required: "من فضلك أدخل رقم التواصل"
            },
            Email:
            {
                required: "من فضلك أدخل الايميل",
                email: "من فضلك أدخل الايميل بطريقة صحيحة"
            },
            Password:
            {
                required: "من فضلك أدخل الباسورد"
            }
        }
    });

    var element = document.querySelector('[aria-label="Select all rows"]');
    element.disabled = true;
    element.style.visibility = 'hidden';
});

$.validator.setDefaults({ ignore: '' });
function saveConsultationUsers() {
    if (!$("#frm").valid()) {
        return;
    }
    $.ajax(
        {
            type: "POST",
            url: '/Consultation/SaveConsultationUsers',
            data: {
                ConsultationName: $("#Name").val(),
                ConsultationUserId: ConsultationUsersID,
                Email: $("#Email").val(),
                Password: $("#Password").val(),
                PhoneNumber: $("#Phone").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                $("#Email").val('');
                $("#Password").val('');
                $("#Phone").val('');

                ConsultationUsersID = 0;


                var grid = $("#grid").data("kendoGrid");
                grid.dataSource.read();
                alert("تم الحفظ بنجاح");
            }
        });
}

function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}