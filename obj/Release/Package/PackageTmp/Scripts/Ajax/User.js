function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function getBoolean(str) {
    if ("true".startsWith(str)) {
        return true;
    } else if ("false".startsWith(str)) {
        return false;
    } else {
        return null;
    }
}

var selected = 0;
var lastSelected = "";
var row;
var UserID = 0;
var locked = "";
var IsExecutiveEngineer = "";
var UserCode = "";
var UserName = "";
var UserFullName = "";
var UserPass = "";
var Email = "";
var UserStatus = "";
var IsAdmin = "";
var IsFrontOffice = "";
var IsBackOffice = "";
var RoleID = "";
var IsLocked = "";
var LastselectedLength = 0;
var combobox = "";
var combobox2 = "";

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
        UserID = 0;
        $("#savebutton").html('<i class="fa fa-save"></i> إضافة');
        $('#lock').prop('checked', false);
        $("#UserCode").val('');
        $("#UserName").val('');
        $("#UserFullName").val('');
        $("#UserPass").val('');
        $("#Email").val('');
        $("#UserStatus").val('');
        $("#IsAdmin").prop('checked', false);
        $('#IsFrontOffice').prop('checked', false);
        $('#IsBackOffice').prop('checked', false);
        $('#IsExecutiveEngineer').prop('checked', false);
        $("#Role").val('');
        combobox = $("#Role").data("kendoComboBox");
        combobox.select(-1);

        combobox2 = $("#Department").data("kendoComboBox");
        combobox2.select(-1);
        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {
        lastSelected = selectedDataItems[0].uid;
        ARName = selectedDataItems[0].UnitName;
        $("#savebutton").html('<i class="fa fa-save"></i> تعديل');
        UserID = selectedDataItems[0].UserId;
        UserCode = selectedDataItems[0].UserCode;
        UserName = selectedDataItems[0].UserName;
        UserFullName = selectedDataItems[0].UserFullName;
        UserPass = selectedDataItems[0].UserPass;
        Email = selectedDataItems[0].Email;
        UserStatus = selectedDataItems[0].UserStatus;
        IsAdmin = selectedDataItems[0].IsAdmin;
        IsFrontOffice = selectedDataItems[0].IsFrontOffice;
        IsBackOffice = selectedDataItems[0].IsBackOffice;
        RoleID = selectedDataItems[0].RoleID;
        locked = selectedDataItems[0].IsLocked;
        IsExecutiveEngineer = selectedDataItems[0].IsExecutiveEngineer;

        $('#lock').prop('checked', locked);
        $("#UserCode").val(UserCode);
        $("#UserName").val(UserName);
        $("#UserFullName").val(UserFullName);
        $("#UserPass").val(UserPass);
        $("#Email").val(Email);
        $("#UserStatus").val(UserStatus);

        $("#IsAdmin").prop('checked', IsAdmin);
        $('#IsFrontOffice').prop('checked', IsFrontOffice);
        $('#IsBackOffice').prop('checked', IsBackOffice);
        $('#IsExecutiveEngineer').prop('checked', IsExecutiveEngineer);

        combobox = $("#Role").data("kendoComboBox");
        combobox.select(function (dataItem) {
            return dataItem.RoleId === selectedDataItems[0].RoleId;
        });

        combobox2 = $("#Department").data("kendoComboBox");
        combobox2.select(function (dataItem) {
            return dataItem.DepartmentId === selectedDataItems[0].DepartmentId;
        });
    }

}
$.validator.setDefaults({ ignore: '' });
$(document).ready(function () {


    $("#Role").addClass("form-control");
    $("#Department").addClass("form-control");
    $(".k-combobox").addClass("form-control");

    $("#userfrm").validate({
        rules:
        {
            UserCode:
            {
                required: true
            },
            UserName:
            {
                required: true
            },
            UserFullName:
            {
                required: true
            },
            UserPass:
            {
                required: true
            },
            Email:
            {
                required: true,
                email: true
            },
            UserStatus:
            {
                required: true
            },
            Role:
            {
                required: true
            },
            Department:
            {
                required: true
            }
        },
        messages: {
            UserCode:
            {
                required: "من فضلك أدخل كود المستخدم"
            },
            UserName:
            {
                required: "من فضلك أدخلك اسم المستخدم"
            },
            UserFullName:
            {
                required: "من فضلك أدخل الاسم بالكامل"
            },
            UserPass:
            {
                required: "من فضلك ادخل الباسورد"
            },
            Email:
            {
                required: "من فضلك أدخل الإيميل",
                email: "من فضلك أدخل الإيميل في شكلة الصحيح"
            },
            UserStatus:
            {
                required: "من فضلك أدخل حالت المستخدم"
            },
            Role:
            {
                required: "من فضلك أدخل صلاحيت المستخدم"
            },
            Department:
            {
                required: "من فضلك أدخل القسم"
            }
        }
    });

    //for search on any char like string or numbers or boolean or date
    $('#grid .k-grid-search').on('input', function (e) {
        var grid = $('#grid').data('kendoGrid');
        var columns = grid.columns;

        var filter = { logic: 'or', filters: [] };
        columns.forEach(function (x) {
            if (x.field) {
                var type = grid.dataSource.options.schema.model.fields[x.field].type;
                if (type === 'string') {
                    filter.filters.push({
                        field: x.field,
                        operator: 'contains',
                        value: e.target.value
                    })
                }
            }
        });
        grid.dataSource.filter(filter);
    });

    $('#popover-left,#popover-top,#popover-bottom,#popover-right').popover();
    $('#tooltip-left,#tooltip-top,#tooltip-bottom,#tooltip-right').tooltip();
    $('.k-grid-EditModal').attr('data-toggle', 'modal');
    $('.k-grid-EditModal').attr('data-target', '#RoleEditModalPopup');
    $('.k-grid-CreateModal').attr('data-toggle', 'modal');
    $('.k-grid-CreateModal').attr('data-target', '#WorkOrderCreateModalPopup');
    $('.k-grid-DeleteModal').attr('data-toggle', 'modal');
    $('.k-grid-DeleteModal').attr('data-target', '#WorkOrderDeleteModalPopup');
    var element = document.querySelector('[aria-label="Select all rows"]');
    element.disabled = true;
    element.style.visibility = 'hidden';
});

function saveUser() {
    if (!$("#userfrm").valid()) {
        return;
    }
    $.ajax(
        {
            type: "POST",
            url: '/User/saveUser',
            data: {
                UserId: UserID,
                UserCode: $("#UserCode").val(),
                UserName: $("#UserName").val(),
                UserFullName: $("#UserFullName").val(),
                UserPass: $("#UserPass").val(),
                Email: $("#Email").val(),
                UserStatus: $("#UserStatus").val(),
                IsAdmin: $("#IsAdmin").prop('checked'),
                IsFrontOffice: $("#IsFrontOffice").prop('checked'),
                IsBackOffice: $("#IsBackOffice").prop('checked'),
                IsExecutiveEngineer: $("#IsExecutiveEngineer").prop('checked'),
                RoleId: $("#Role").val(),
                DepartmentId: $("#Department").val(),
                IsLocked: $("#lock").prop("checked")
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {
                if (result === -1) {
                    alert('المستخدم او الإيميل موجود مسبقا');
                } else {
                    $('#lock').prop('checked', false);
                    alert("تم الحفظ بنجاح");
                    var grid = $("#grid").data("kendoGrid");
                    grid.dataSource.read();
                    $('#lock').prop('checked', false);
                    $("#UserCode").val('');
                    $("#UserName").val('');
                    $("#UserFullName").val('');
                    $("#UserPass").val('');
                    $("#Email").val('');
                    $("#UserStatus").val('');
                    $("#IsAdmin").prop('checked', false);
                    $('#IsFrontOffice').prop('checked', false);
                    $('#IsBackOffice').prop('checked', false);
                    $('#IsExecutiveEngineer').prop('checked', false);
                    $("#Role").val('');
                }
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