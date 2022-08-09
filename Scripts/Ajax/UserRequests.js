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
var UserRequestID = 0;
var RequestID = 0;
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
var combobox3 = "";

function onPaging(arg) {
    LastselectedLength = 0;
    var grid = $("#RequestGrid").data("kendoGrid");
    grid.clearSelection();
}

function onChangeSelection(arg) {
    var grid = $("#RequestGrid").data("kendoGrid");

    selected = this.selectedKeyNames();
    if (LastselectedLength === selected[0]) {
        selected = [];
        grid.clearSelection();
    }
    LastselectedLength = selected[0];
    if (selected.length === 0) {
        UserID = 0;
        UserRequestID = 0;
        combobox = $("#Request").data("kendoMultiColumnComboBox");
        //NoteStatus = "تعلية"
        combobox.select(-1);
        //combobox3 = $("#User").data("kendoComboBox");
        ////NoteStatus = "تعلية"
        //combobox3.select(-1);

        //combobox2 = $("#Department").data("kendoComboBox");
        ////NoteStatus = "تعلية"
        //combobox2.select(-1);
        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {


        UserID = selectedDataItems[0].UserId;
        RequestID = selectedDataItems[0].RequestId;
        UserRequestID = selectedDataItems[0].UserRequestId;

        //combobox = $("#Role").data("kendoComboBox");
        ////NoteStatus = "تعلية"
        //combobox.select(function (dataItem) {
        //    return dataItem.RoleId === selectedDataItems[0].RoleId;
        //});

        //combobox2 = $("#Department").data("kendoComboBox");
        ////NoteStatus = "تعلية"
        //combobox2.select(function (dataItem) {
        //    return dataItem.DepartmentId === selectedDataItems[0].DepartmentId;
        //});
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
            Department:
            {
                required: true
            },
            User:
            {
                required: true
            },
            Request:
            {
                required: true
            }
        },
        messages: {
            Department:
            {
                required: "من فضلك أدخل القسم"
            },
            User:
            {
                required: "من فضلك أدخلك إسم المهندس التنفيذي"
            },
            Request:
            {
                required: "من فضلك أدخل الاسم العملية"
            }
        }
    });

    //for search on any char like string or numbers or boolean or date
    $('#RequestGrid .k-grid-search').on('input', function (e) {
        var grid = $('#RequestGrid').data('kendoGrid');
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

function deleteUserRequest() {
    //if (!$("#userfrm").valid()) {
    //    return;
    //}
    if (window.confirm("هل انت متأكد؟")) {
        if (UserRequestID !== 0) {
            $.ajax(
                {
                    type: "POST",
                    url: '/User/DeleteUserRequests',
                    data: {
                        UserRequestId: UserRequestID
                    },

                    error: function (result) {
                        alert("There is a Problem, Try Again!");
                    },
                    success: function (result) {

                        alert("تم المسح بنجاح");
                        var grid = $("#RequestGrid").data("kendoGrid");
                        grid.dataSource.read();

                        combobox = $("#Request").data("kendoMultiColumnComboBox");
                        //NoteStatus = "تعلية"
                        combobox.select(-1);
                        onChangeUser1(UserID);

                    }
                });
        }
        else {
            alert('يجب اختيار عملية');

        }
    }
}
function saveUserRequest() {
    if (!$("#userfrm").valid()) {
        return;
    }
    var User = $("#User").data("kendoComboBox").dataItem();
   /* var Request = $("#Request").data("kendoMultiColumnComboBox").dataItem();*/
    $.ajax(
        {
            type: "POST",
            url: '/User/SaveUserRequests',
            data: {
                UserId: User.UserId,
                RequestId: RequestID
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {
                onChangeUser1(UserID);
                alert("تم الحفظ بنجاح");
                setTimeout(
                    function () {
                        var grid = $("#RequestGrid").data("kendoGrid");
                        grid.dataSource.read();
                    }, 1000);
                //combobox = $("#Request").data("kendoMultiColumnComboBox");
                ////NoteStatus = "تعلية"
                //combobox.select(-1);

                //combobox3 = $("#User").data("kendoComboBox");
                ////NoteStatus = "تعلية"
                //combobox3.select(-1);
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
function FilterUser() {
    return {
        DepartmentId: $("#Department").data("kendoComboBox").dataItem().DepartmentId
    };
}
function FilterRequest() {
    return {
        UserId: $("#User").data("kendoComboBox").dataItem().UserId
    };
}
function onChangeDepartment(arg) {

    $.ajax({
        type: "POST",
        url: '/User/GetExecutiveEngineerUsersFromDepartment',
        data: {
            DepartmentId: arg.dataItem.DepartmentId
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            $("#User").kendoComboBox({
                dataTextField: "UserName",
                dataValueField: "UserId",
                dataSource: result,
                index: -1
            });

        }
    });
}
function onChangeRequest(arg2) {
    var dataItem = $("#Request").data("kendoMultiColumnComboBox").dataItem();
    if (dataItem !== undefined) {
        RequestID = dataItem.RequestId;
    }
    var dataItem2 = $("#User").data("kendoComboBox").dataItem();
    UserID = dataItem2.UserId;

}
function onChangeUser(arg) {

    setTimeout(
        function () {
            var grid = $("#RequestGrid").data("kendoGrid");
            grid.dataSource.read();
        }, 1000);
    
    //$("#Request").kendoMultiColumnComboBox({
    //    dataTextField: "RequestNo",
    //    dataValueField: "RequestId",
    //    columns: [
    //        { field: "RequestNo", title: "رقم العملية" },
    //        { field: "RequestName", title: "إسم العملية" }
    //    ],
    //    filter: "contains",
    //    filterFields: ["RequestNo", "RequestName"],
    //    dataSource: result,
    //    index: -1
    //});
    //var dataItem = $("#User").data("kendoComboBox").dataItem();
    //$.ajax({
    //    type: "POST",
    //    url: '/User/GetRequestsNotRelatedToUser',
    //    data: {
    //        UserId: dataItem.UserId
    //    },
    //    error: function (result) {
    //        alert("error");
    //    },
    //    success: function (result) {

           

    //    }
   /* });*/
}
function onChangeUser1(UserID) {
    var User = 0;
    setTimeout(
        function () {
            User = $("#User").data("kendoComboBox").dataItem();
        }, 500);
    setTimeout(
        function () {
    $.ajax({
        type: "POST",
        url: '/User/GetRequestsNotRelatedToUser',
        data: {
            UserId: User.UserId
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            var grid = $("#RequestGrid").data("kendoGrid");
            grid.dataSource.read();
          
            //$("#Request").kendoMultiColumnComboBox({
            //    dataTextField: "RequestNo",
            //    dataValueField: "RequestId",
            //    columns: [
            //        { field: "RequestNo", title: "رقم العملية" },
            //        { field: "RequestName", title: "إسم العملية" }
            //    ],
            //    dataSource: result,
            //    index: -1
            //});

        }
    });
        }, 1000);
}

function onChangeSelection2(arg) {
   
    var grid = $("#grid2").data("kendoGrid");

   

    selected = this.selectedKeyNames();
    if (LastselectedLengthgrid2 === selected[0]) {
        selected = [];
        grid.clearSelection();
    }
    LastselectedLengthgrid2 = selected[0];
    if (selected.length === 0) {
       
       
        WorkOrderID = 0;
        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {
        
        RequestID = selectedDataItems[0].RequestId;
        
        $("#RequestSearchModalPopup").modal('hide');
        $("#RequestNo").val(selectedDataItems[0].RequestNo);
        RequestNo = selectedDataItems[0].RequestNo;

        RequestCode = selectedDataItems[0].RequestCode;

        $("#RequestName").val(selectedDataItems[0].RequestName);
        RequestName = selectedDataItems[0].RequestName;
        $("#RequestDate").val(selectedDataItems[0].RequestDateString);
        RequestDate = selectedDataItems[0].RequestDateString;
        $("#RequestAmount").val(selectedDataItems[0].RequestAmount);
        RequestAmount = selectedDataItems[0].RequestAmount;
        $("#Notes").val(selectedDataItems[0].Notes);
        Notes = selectedDataItems[0].Notes;
        $("#Company").val(selectedDataItems[0].Company);


      
        //return;
    }
}