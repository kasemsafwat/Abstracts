var selected = 0;
var lastSelected = "";
var row;
var RequestID = 0;
var locked = "";
var WorkOrderCode = "";
var RequestName = "";
var RequestDate = "";
var RequestAmount = "";
var RequestStartDate = "";
var RequestDuration = "";
var RequestEndDate = "";
var Notes = "";
var FileName = "";
var WorkOrderID = 0;
var fileNotSelected = false;

var RequestDetailID = 0;
var RequestDetailCode = "";
var RequestDetailSerial = "";
var RequestDetailName = "";
var UnitID = "";
var Qty = "";
var UnitPrice = "";
var TotalPrice = "";
var DepartmentID = "";

var WorkOrderNoteID = "";
var NoteTitle = "";
var NoteStatus = "";
var NotePercentage = "";

function Search() {
    if (!$("#requestfrm").valid()) {
        return;
    }
    $.ajax({
        type: "POST",
        url: '/Request/GetRequestDataByID',
        data: {
            RequestNo: $("#RequestNo").val(),
            RequestDate: $("#RequestDate").val()
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            if (result !== -1) {
                //$("#gridNotes").data("kendoGrid").dataSource.data([]);

                getWorkOrderData(result.RequestId);
                fillWorkOrderCombobox(result.RequestId);
                $("#RequestNo").val(result.RequestNo);
                RequestCode = result.RequestCode;
                RequestNo = result.RequestNo;
                $("#RequestName").val(result.RequestName);
                RequestName = result.RequestName;

                $("#RequestDate").val(result.RequestDateString);
                RequestDate = result.RequestDateString;
                $("#RequestAmount").val(result.RequestAmount);
                RequestAmount = result.RequestAmount;
                $("#Notes").val(result.Notes);
                Notes = result.Notes;
                $("#Company").val(result.Company);
                CompanyID = result.CompanyId;
                RequestID = result.RequestId;
                $("#RequestDuration").val(result.RequestDuration);
                RequestDuration = result.RequestDuration;
                if (result.RequestStartDateString !== null) {
                    $("#RequestStartDate").val(result.RequestStartDateString);
                    RequestStartDate = result.RequestStartDateString;
                }
                else {
                    $("#RequestStartDate").val("")
                }
                if (result.RequestEndDateString !== null) {
                    $("#RequestEndDate").val(result.RequestEndDateString);
                    RequestEndDate = result.RequestEndDateString;
                }
                else {
                    $("#RequestEndDate").val("")
                }
                if (result.FileName === null) {
                    $("#BrowseFile").css('display', 'none');
                }
                else {
                    FileName = result.FileName;
                    $("#BrowseFile").css('display', 'block');
                }
            }
            else {
                alert('لا يوجد أمر إسناد');
                $('#lock').prop('checked', false);
                $("#RequestNo").val('');
                $("#RequestName").val('');
                $("#RequestDate").val('');
                $("#RequestAmount").val('');
                $("#RequestStartDate").val('');
                $("#RequestDuration").val('');
                $("#RequestEndDate").val('');
                $("#Notes").val('');
                $("#Company").val('');
                WorkOrderID = 0;
                $("#Browse").css('display', 'none');
                $("#WorkOrderCode").val('');
                $("#WorkOrderStartDate").val('');
                $("#WorkOrderEndDate").val('');
                $("#Note").val('');
                $("#grid").data("kendoGrid").dataSource.data([]);
                //$("#gridRequestDetails").data("kendoGrid").dataSource.data([]);
            }

        }
    });
}
function onChangeSelection(arg) {

    var grid = $("#grid").data("kendoGrid");

    selected = this.selectedKeyNames();
    if (selected.length === 0) {
        grid.select(row);
        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length <= 1 && selectedDataItems !== []) {
        //$("#EditRequestDetailsModalPopup").modal('show');

        RequestID = selectedDataItems[0].RequestId;
        RequestDetailID = selectedDataItems[0].RequestDetailId;

        $("#EditRequestDetailCode").val(selectedDataItems[0].RequestDetailCode);
        RequestDetailCode = selectedDataItems[0].RequestDetailCode;
        $("#EditRequestDetailName").val(selectedDataItems[0].RequestDetailName);
        RequestDetailName = selectedDataItems[0].RequestDetailName;
        $("#EditRequestDetailSerial").val(selectedDataItems[0].RequestDetailSerial);
        RequestDetailSerial = selectedDataItems[0].RequestDetailSerial;

        //$("#EditUnit").val(selectedDataItems[0].UnitId);
        Unit = selectedDataItems[0].Unit;
        var unit = $("#EditUnit").data("kendoComboBox");
        unit.text(Unit);

        $("#EditQty").val(selectedDataItems[0].Qty);
        Qty = selectedDataItems[0].Qty;
        $("#EditUnitPrice").val(selectedDataItems[0].UnitPrice);
        UnitPrice = selectedDataItems[0].UnitPrice;
        $("#EditTotalPrice").val(selectedDataItems[0].TotalPrice);
        TotalPrice = selectedDataItems[0].TotalPrice;


        //$("#EditDepartmentID").val(selectedDataItems[0].DepartmentId);
        DepartmentID = selectedDataItems[0].Department;
        var Department = $("#EditDepartment").data("kendoComboBox");
        Department.text(DepartmentID);
        if (selectedDataItems[0].FileNameDetails === null) {
            $("#Browse").css('display', 'none');
        }
        else {
            FileNameDetails = selectedDataItems[0].FileNameDetails;
            $("#Browse").css('display', 'block');
        }
    }
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

        $('#lock').prop('checked', false);
        $("#RequestNo").val('');
        $("#RequestName").val('');
        $("#RequestDate").val('');
        $("#RequestAmount").val('');
        $("#RequestStartDate").val('');
        $("#RequestDuration").val('');
        $("#RequestEndDate").val('');
        $("#Notes").val('');
        $("#Company").val('');
        $("#BrowseFile").css('display', 'none');
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
       
        getWorkOrderData(selectedDataItems[0].RequestId);
        fillWorkOrderCombobox(selectedDataItems[0].RequestId);
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
        CompanyID = selectedDataItems[0].CompanyId;
        RequestID = selectedDataItems[0].RequestId;
        $("#RequestDuration").val(selectedDataItems[0].RequestDuration);
        RequestDuration = selectedDataItems[0].RequestDuration;
        if (selectedDataItems[0].RequestStartDateString !== null) {
            $("#RequestStartDate").val(selectedDataItems[0].RequestStartDateString);
            RequestStartDate = selectedDataItems[0].RequestStartDateString;
        }
        else {
            $("#RequestStartDate").val("")
        }
        if (selectedDataItems[0].RequestEndDateString !== null) {
            $("#RequestEndDate").val(selectedDataItems[0].RequestEndDateString);
            RequestEndDate = selectedDataItems[0].RequestEndDateString;
        }
        else {
            $("#RequestEndDate").val("")
        }
        $("#RequestSearchModalPopup").modal('hide');

        onChangeRequest(selectedDataItems[0].RequestId);
    }
}
function onChangeRequest(RequestID) {
    $.ajax({
        type: "POST",
        url: '/Request/GetRequestData',
        data: {
            RequestId: RequestID
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {

            $("#RequestName").val(result.RequestName);
            RequestName = result.RequestName;
            $("#RequestDate").val(result.RequestDateString);
            RequestDate = result.RequestDateString;
            $("#RequestAmount").val(result.RequestAmount);
            RequestAmount = result.RequestAmount;

            $("#Company").val(result.Company);
            CompanyID = result.CompanyId;
            RequestID = result.RequestId;
            $("#RequestDuration").val(result.RequestDuration);
            RequestDuration = result.RequestDuration;
            if (result.RequestStartDateString !== null) {
                $("#RequestStartDate").val(result.RequestStartDateString);
                RequestStartDate = result.RequestStartDateString;
            }
            else {
                $("#RequestStartDate").val("")
            }
            if (result.RequestEndDateString !== null) {
                $("#RequestEndDate").val(result.RequestEndDateString);
                RequestEndDate = result.RequestEndDateString;
            }
            else {
                $("#RequestEndDate").val("")
            }

        }
    });
}
function onSelect(e) {
    var upload = $("#FileName").data("kendoUpload");


    if (e.files.length > 1) {
        alert("Please select max 20 files.");
        upload.removeAllFiles();
        e.preventDefault();
    }
    $(".k-action-buttons").hide();
    FileName = e.files[0].name;
}
function getFileInfo(e) {
    return $.map(e.files, function (file) {
        var info = file.name;
        DocumentFile = file.name;
        // File size is not available in all browsers
        if (file.size > 0) {
            info += " (" + Math.ceil(file.size / 1024) + " KB)";
        }
        return info;
    }).join(", ");
}
function browse() {
    if (WorkOrderCode === "") WorkOrderCode = $("#WorkOrderCode").val();
    if (FileName === "") FileName = $("#WorkOrderFileName").val()
    window.open(WorkOrderURL + WorkOrderCode + "/" + FileName, '_blank');
}
function multiply(x, y) {
    return x * y;
}
function convertDate(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    year = date.getFullYear();
    return [day, mnth, year].join("-");
}
function productNameEditable() {
    // do not allow editing for product with ProductID=3
    return false;
}
function upload() {

    if (WorkOrderID === 0) {
        alert('يجب اختيار المستخلص');

    }
    else {
        if (FileName !== null) {
            var upload = $("#FileName").data("kendoUpload");
            upload.upload();
            //alert('تم الرفع');
            $("#Browse").css('display', 'block');
        }
    }
}
function getWorkOrderData(RequestID) {
    $.ajax({
        type: "POST",
        url: '/Request/GetRequestByIDForWorkOrderData',
        data: {
            RequestId: RequestID
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            if (result !== -1) {
                $("#submitbtn").css('display', 'block');
                $("#WorkOrderCode").val(result.WorkOrderCode);
                $("#WorkOrderStartDate").val(result.WorkOrderStartDateString);
                $("#WorkOrderEndDate").val(result.WorkOrderEndDateString);
                $("#Note").val(result.Notes);

                WorkOrderID = result.WorkOrderId;
                WorkOrderCode = result.WorkOrderCode;
                //var grid = $("#grid").data("kendoGrid");
                //grid.dataSource.read();





                //var grid2 = $("#gridRequestDetails").data("kendoGrid");
                //grid2.dataSource.read();

                if (result.FileName === null) {
                    $("#Browse").css('display', 'none');
                }
                else {
                    FileName = result.FileName;
                    $("#Browse").css('display', 'block');
                }
            }
            else {
                $("#submitbtn").css('display', 'none');
                WorkOrderID = 0;
                $("#Browse").css('display', 'none');
                $("#WorkOrderCode").val('');
                $("#WorkOrderStartDate").val('');
                $("#WorkOrderEndDate").val('');
                $("#Note").val('');
                $("#grid").data("kendoGrid").dataSource.data([]);
                //$("#gridRequestDetails").data("kendoGrid").dataSource.data([]);
            }
        }
    });
}
$(document).ready(function () {
    $("#EditQty").on('input', function (e) {
        $("#EditTotalPrice").val(multiply($("#EditQty").val(), $("#EditUnitPrice").val()));
    });
    $("#EditUnitPrice").on('input', function (e) {
        $("#EditTotalPrice").val(multiply($("#EditQty").val(), $("#EditUnitPrice").val()));
    });
    $("#CreateQty").on('input', function (e) {
        $("#CreateTotalPrice").val(multiply($("#CreateQty").val(), $("#CreateUnitPrice").val()));
    });
    $("#CreateUnitPrice").on('input', function (e) {
        $("#CreateTotalPrice").val(multiply($("#CreateQty").val(), $("#CreateUnitPrice").val()));
    });

    $("#RequestDate").addClass("form-control");
    $("#RequestEndDate").addClass("form-control");
    $("#RequestStartDate").addClass("form-control");
    $("#EditUnit").addClass("form-control");
    $(".k-datepicker").addClass("form-control");
    $("#CreateUnit").addClass("form-control");
    $(".form-group .k-datepicker").addClass("form-control");
    $(".form-group .k-content .k-upload").addClass("form-control");
    //$(".form-group .k-combobox").addClass("form-control");
    $(".k-combobox").addClass("form-control");
    $("#requestDetailsfrm").validate({
        rules:
        {
            RequestCode:
            {
                required: true
            },
            RequestName:
            {
                required: true
            },
            RequestDate:
            {
                required: true
            },
            RequestAmount:
            {
                required: true
            },
            RequestStartDate:
            {
                required: true
            },
            RequestDuration:
            {
                required: true
            },
            RequestEndDate:
            {
                required: true
            },
            Notes:
            {
                required: true
            },
            CompanyID:
            {
                required: true
            }
        },
        messages: {
            RequestCode:
            {
                required: "من فضلك أدخل أمر الإسناد"
            },
            RequestName:
            {
                required: "من فضلك أدخلك اسم المشروع او العملية"
            },
            RequestDate:
            {
                required: "من فضلك أدخل تاريخ العملية"
            },
            RequestAmount:
            {
                required: "من فضلك ادخل قيمة العملية"
            },
            RequestStartDate:
            {
                required: "من فضلك أدخل أول تاريخ للعملية"
            },
            RequestDuration:
            {
                required: "من فضلك أدخل مدة العملية"
            },
            RequestEndDate:
            {
                required: "من فضلك أدخل أخر تاريخ العملية"
            },
            Notes:
            {
                required: "من فضلك أدخل ملاحظات"
            },
            CompanyID:
            {
                required: "من فضلك أدخل إسم الشركة"
            }
        }
    });
    $("#Notefrm").validate({
        rules:
        {
            NoteTitle:
            {
                required: true
            },
            NotePercentage:
            {
                required: true
            },
            NoteStatus:
            {
                required: true
            }
        },
        messages: {
            NoteTitle:
            {
                required: "من فضلك أدخل بيان الملاحظة"
            },
            NotePercentage:
            {
                required: "من فضلك أدخلك نسبة الملاحظة"
            },
            NoteStatus:
            {
                required: "من فضلك أدخل وصف الملاحظة"
            }
        }
    });
    //for search on any char like string or numbers or boolean or date
    $('#grid2 .k-toolbar .k-grid-search').on('input', function (e) {
        var grid = $('#grid2').data('kendoGrid');
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
                } else if (type === 'number') {
                    if (isNumeric(e.target.value)) {
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: e.target.value
                        });
                    }

                }
            }
        });
        grid.dataSource.filter(filter);
    });
    $('#grid .k-toolbar .k-grid-search').on('input', function (e) {
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
                else if (type === 'number') {
                    if (isNumeric(e.target.value)) {
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: e.target.value
                        });
                    }

                }
            }
        });
        grid.dataSource.filter(filter);
    });
    $('.k-grid-CreateNote').attr('data-toggle', 'modal');
    $('.k-grid-CreateNote').attr('href', '#');
    $('.k-grid-CreateNote').attr('data-target', '#CreateNoteModalPopup');
    $('.k-grid-CreateModal').attr('data-toggle', 'modal');
    $('.k-grid-CreateModal').attr('href', '#');
    $('.k-grid-CreateModal').attr('data-target', '#WorkOrderModalPopup');
    $('#popover-left,#popover-top,#popover-bottom,#popover-right').popover();
    $('#tooltip-left,#tooltip-top,#tooltip-bottom,#tooltip-right').tooltip();
    var element = document.querySelector('[aria-label="Select all rows"]');
    element.disabled = true;
});
function saveWorkOrder() {
    if (!$("#requestDetailsfrm").valid()) {
        return;
    }

    $.ajax(
        {
            type: "POST",
            url: '/WorkOrder/SaveWorkOrder',
            data: {
                RequestId: RequestID,
                WorkOrderId: WorkOrderID,
                WorkOrderStartDate: $("#WorkOrderStartDate").val(),
                WorkOrderEndDate: $("#WorkOrderEndDate").val(),
                Notes: $("#Note").val()
            },
            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                WorkOrderID = result.WorkOrderId;
                alert("تم الحفظ بنجاح");
                $("#submitbtn").css('display', 'block');
                upload();
                $("#WorkOrderCode").val(result.WorkOrderCode);
                WorkOrderCode = result.WorkOrderCode;

            }
        });


}
function saveNote() {
    if (!$("#Notefrm").valid()) {
        return;
    }
    if (window.confirm("هل انت متأكد؟")) {
        $.ajax(
            {
                type: "POST",
                url: '/Note/saveNote',
                data: {
                    WorkOrderNoteID: WorkOrderNoteID,
                    NoteTitle: $("#NoteTitle").val(),
                    NotePercentage: $("#NotePercentage").val(),
                    NoteStatus: $("#NoteStatus").val()
                },

                error: function (result) {
                    alert("There is a Problem, Try Again!");
                },
                success: function (result) {

                    var grid = $("#gridNotes").data("kendoGrid");
                    grid.dataSource.read();

                    $(".k-grid-search .k-input").val("");

                    $("#NoteTitle").val('');
                    $("#NotePercentage").val('');
                    $("#NoteStatus").val('');
                    alert("تم الحفظ بنجاح");

                }
            });
    }
}
function SetWorkOrderSubmitted() {
    if (window.confirm("هل انت متأكد؟")) {
        $.ajax(
            {
                type: "POST",
                url: '/WorkOrder/SetWorkOrderSubmitted',
                data: {
                    WorkOrderId: WorkOrderID
                },
                error: function (result) {
                    alert("There is a Problem, Try Again!");
                },
                success: function (result) {

                    if (result !== 0) {
                        alert("تم التأكيد بنجاح");
                        $("#RequestNo").val('');
                        $("#RequestName").val('');
                        $("#RequestDate").val('');
                        $("#RequestAmount").val('');
                        $("#RequestStartDate").val('');
                        $("#RequestDuration").val('');
                        $("#RequestEndDate").val('');
                        $("#Note").val('');
                        $("#Company").val('');
                        $("#Browse").css('display', 'none');
                        $("#submitbtn").css('display', 'none');
                        $("#WorkOrderCode").val('');
                        $("#WorkOrderStartDate").val('');
                        $("#WorkOrderEndDate").val('');
                        var grid = $("#grid").data("kendoGrid");
                        grid.dataSource.read();

                    }
                    else alert("حدث خطأ في التأكيد")

                }
            });
    }
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
function onChangeNote(arg) {

    $.ajax({
        type: "POST",
        url: '/WorkOrder/SetNoteIDForGrid',
        data: {
            NoteID: arg.dataItem.WorkOrderNoteId
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {



        }
    });
}
function AddWorkOrderDetails() {
    if (WorkOrderID === 0) {
        alert('يجب إختيار المستخلص');
    }
    else {
        $.ajax({
            type: "POST",
            url: '/WorkOrder/SaveWorkOrderDetails',
            data: {
                WorkOrderDetailId: 0,
                WorkOrderId: WorkOrderID,
                RequestDetailIdList: RequestDetailID,
                Qty: Qty
            },
            error: function (result) {
                alert("error");
            },
            success: function (result) {
                $("#WorkOrderModalPopup").modal('hide');

                var grid = $("#grid").data("kendoGrid");
                grid.dataSource.read();


            }
        });
    }
}
function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
function onChangeWorkOrder(arg) {
    var dataItem = $("#WorkOrder").data("kendoComboBox").dataItem();
    $.ajax({
        type: "POST",
        url: '/WorkOrder/GetWorkOrderDataByID',
        data: {
            RequestID: RequestID,
            WorkOrderId: dataItem.WorkOrderId
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            var grid = $("#grid").data("kendoGrid");
            grid.dataSource.read();

            WorkOrderID = dataItem.WorkOrderId;
            WorkOrderStartDate = result.WorkOrderStartDateString;
            WorkOrderEndDate = result.WorkOrderEndDateString;
            WorkOrderCode = result.WorkOrderCode;

            $("#WorkOrderStartDate").val(WorkOrderStartDate)
            $("#WorkOrderEndDate").val(WorkOrderEndDate)
            if (result.FileName === null) {
                $("#Browse").css('display', 'none');
            }
            else {
                FileName = result.FileName;
                $("#Browse").css('display', 'block');
            }


        }
    });
}

function SetWorkOrderApproved() {
    if (WorkOrderID === 0) { alert('يجب إختيار المستخلص'); }
    else {
        if (window.confirm("هل انت متأكد؟")) {
            $.ajax(
                {
                    type: "POST",
                    url: '/WorkOrder/SetWorkOrderApproved',
                    data: {
                        WorkOrderId: WorkOrderID
                    },
                    error: function (result) {
                        alert("There is a Problem, Try Again!");
                    },
                    success: function (result) {

                        if (result !== 0) {
                            updateNotifications();
                            alert("تم التأكيد بنجاح");
                            $("#Approval").css('display', 'none');
                        }
                        else alert("حدث خطأ في التأكيد")

                    }
                });
        }
    }
}

function getDataForApprovalBtn(workOrderID) {
    $.ajax({
        type: "POST",
        url: '/WorkOrder/getDataForApprovalBtn',
        data: {
            WorkOrderId: workOrderID
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            if (result === 1) {
                $("#Approval").css('display', 'block');
            }
            else {
                FileName = result.FileName;
                $("#Approval").css('display', 'none');
            }
        }
    });
}
function fillWorkOrderCombobox(reqID) {
    var dropdownlist2 = $("#WorkOrder").data("kendoComboBox");
    dropdownlist2.select(-1);
    $.ajax({
        type: "POST",
        url: '/WorkOrder/GetWorkOrderListByRequestIDForStatus',
        data: {
            RequestId: reqID
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            $("#WorkOrder").kendoComboBox({
                dataTextField: "WorkOrderCode",
                dataValueField: "WorkOrderId",
                dataSource: result,
                index: -1,
                change: onChangeWorkOrder
            });
        }
    });
}

function onChangeSelectionNote(arg) {

    selected = this.selectedKeyNames();
    var grid = $("#gridNotes").data("kendoGrid");

    if (LastselectedLength === selected[0]) {
        selected = [];
        grid.clearSelection();
    }
    LastselectedLength = selected[0];
    if (selected.length === 0) {
        $("#savebutton").html('<i class="fa fa-save"></i> إضافة');
        $("#WorkOrderNoteID").val('');
        $("#NoteTitle").val('');
        $("#NotePercentage").val('');
        //$("#NoteStatus").val('');
        WorkOrderNoteID = 0;
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
        WorkOrderNoteID = selectedDataItems[0].WorkOrderNoteId;
        NoteTitle = selectedDataItems[0].NoteTitle;
        NotePercentage = selectedDataItems[0].NotePercentage;
        NoteStatus = selectedDataItems[0].NoteStatus;

        $("#WorkOrderNoteID").val(WorkOrderNoteID);
        $("#NoteTitle").val(NoteTitle);
        $("#NotePercentage").val(NotePercentage);

        var combobox = $("#NoteStatus").data("kendoComboBox");
        //NoteStatus = "تعلية"
        combobox.select(function (dataItem) {
            return dataItem.Value === NoteStatus;
        });
        $("#NoteStatus").val(NoteStatus);
    }

}
function onNotePaging(arg) {
    LastselectedLength = 0;
    var grid = $("#gridNotes").data("kendoGrid");
    grid.clearSelection();
}