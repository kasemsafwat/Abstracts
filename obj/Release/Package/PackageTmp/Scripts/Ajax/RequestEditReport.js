var selected = 0;
var RequestSelected = 0;
var RequestID = 0;
var locked = "";
var RequestCode = "";
var RequestName = "";
var RequestDate = "";
var RequestAmount = "";
var RequestStartDate = "";
var RequestDuration = "";
var RequestEndDate = "";
var Notes = "";
var CompanyID = "";
var unit = "";
var RequestDetailID = 0;
var RequestDetailCode = "";
var RequestDetailSerial = "";
var RequestDetailName = "";
var UnitID = "";
var Qty = "";
var UnitPrice = "";
var TotalPrice = "";
var DepartmentID = "";
var FileName = "";
var Department = "";
var LastselectedLength = 0;
var RequestExcelFileName = "";
var Type = 1;
var LogFileName = "";

function saveRequest() {
    if (!$("#requestfrm").valid()) {
        return;
    }
    $.ajax(
        {
            type: "POST",
            url: '/Request/saveRequestLog',
            data: {
                RequestId: RequestID,
                RequestDuration: $("#RequestDuration").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                var upload = $("#FileName").data("kendoUpload");
                if (FileName !== null) {
                    upload.upload();
                    $("#Browse").css('display', 'block');
                }
                $('#lock').prop('checked', false);
                //var grid2 = $("#RequestGrid").data("kendoGrid");
                //$(".k-grid-search .k-input").val('');

                //grid2.dataSource.read();
                alert(result);
                $('#lock').prop('');
                $("#RequestNo").val('');
                $("#RequestName").val('');
                $("#RequestDate").val('');
                $("#RequestAmount").val('');
                $("#RequestStartDate").val('');
                $("#RequestDuration").val('');
                $("#RequestEndDate").val('');
                $("#Notes").val('');
                RequestID = 0;
                //dropdownlist = $("#Company").data("kendoMultiColumnComboBox");
                //dropdownlist.select(-1);

            }
        });
}
function Search() {
    //var upload = $("#FileName").data("kendoUpload");
    //upload.clearAllFiles();
    if (!$("#requestfrm").valid()) {
        return;
    }
    if ($("#RequestNo").val() !== "") {
        if ($("#RequestDate").val() !== "") {
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
                        onChangeRequest(result.RequestId);
                        $('#lock').prop('checked', result.IsLocked);
                        locked = result.IsLocked;
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
                        //if (result.FileName === null) {
                        //    $("#BrowseFile").css('display', 'none');
                        //}
                        //else {
                        //    FileName = result.FileName;
                        //    $("#BrowseFile").css('display', 'block');
                        //}
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
                    }

                }
            });
        }
        else { alert('يجب إدخال تاريخ أمر الإسناد'); }
    }
    else {
        alert('يجب إدخال رقم العملية');
    }
}
function onPaging(arg) {
    LastselectedLength = 0;
    var grid = $("#RequestDetailsGrid").data("kendoGrid");
    grid.clearSelection();
}
function onPagingLog(arg) {
    LastselectedLength = 0;
    var grid = $("#RequestLogGrid").data("kendoGrid");
    grid.clearSelection();
}
function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
function onChangeWorkOrder(arg) {
    $.ajax({
        type: "POST",
        url: '/Request/GetRequestDataByID',
        data: {
            RequestId: arg.dataItem.RequestId
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            $('#lock').prop('checked', result.IsLocked);
            locked = result.IsLocked;
            $("#RequestCode").val(result.RequestCode);
            RequestCode = result.RequestCode;
            $("#RequestName").val(result.RequestName);
            RequestName = result.RequestName;
            $("#RequestDate").val(convertDate(result.RequestDateString));
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
                $("#RequestStartDate").val(convertDate(result.RequestStartDateString));
                RequestStartDate = result.RequestStartDateString;
            }
            else {
                $("#RequestStartDate").val("")
            }
            if (result.RequestEndDateString !== null) {
                $("#RequestEndDate").val(convertDate(result.RequestEndDateString));
                RequestEndDate = result.RequestEndDateString;
            }
            else {
                $("#RequestEndDate").val("")
            }
        }
    });
}
function onChangeSelection2(arg) {
    //var upload = $("#FileName").data("kendoUpload");
    //upload.clearAllFiles();

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
        $("#Browse").css('display', 'none');

        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {

        $("#RequestNo").val(selectedDataItems[0].RequestNo);
        RequestNo = selectedDataItems[0].RequestNo;
        onChangeRequest(selectedDataItems[0].RequestId);
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

        if (selectedDataItems[0].LogFileName === "") {
            $("#Browse").css('display', 'none');
        }
        else {
            FileName = selectedDataItems[0].LogFileName;
            $("#Browse").css('display', 'block');
        }
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

            RequestCode = result.RequestCode;
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
            var grid = $("#RequestLogGrid").data("kendoGrid");
            grid.dataSource.read();
            //if (result.FileNameDetails === null) {
            //    $("#Browse").css('display', 'none');
            //}
            //else {
            //    FileNameDetails = result.FileNameDetails;
            //    $("#Browse").css('display', 'block');
            //}
            //fillGrid();
        }
    });
}
function fillGrid() {
    var grid = $("#RequestDetailsGrid").data("kendoGrid");
    grid.dataSource.read();
}
function onSelect(e) {
    var upload = $("#FileName").data("kendoUpload");
    upload.removeAllFiles();

    if (e.files.length > 1) {
        alert("Please select max 20 files.");
        e.preventDefault();
    }
    FileName = e.files[0].name;
}
function onRequestExcelSelect(e) {
    var upload = $("#RequestExcelFileName").data("kendoUpload");
    upload.removeAllFiles();

    if (e.files.length > 1) {
        alert("من فضلك إرفع ملف واحد فقط");
        e.preventDefault();
    }
    RequestExcelFileName = e.files[0].name;
}
function getFileInfo(e) {
    return $.map(e.files, function (file) {
        var info = file.name;
        // File size is not available in all browsers
        if (file.size > 0) {
            info += " (" + Math.ceil(file.size / 1024) + " KB)";
        }
        return info;
    }).join(", ");
}
function uploadExcel() {
    if (RequestID !== 0) {
        if (RequestExcelFileName !== "") {
            $.ajax(
                {
                    type: "POST",
                    url: '/Request/getstatus',
                    error: function (result) {
                        alert("There is a Problem, Try Again!");
                    },
                    success: function (result) {
                        if (result === "done") {
                            alert("تم الحفظ بنجاح");
                            var upload1 = $("#RequestExcelFileName").data("kendoUpload");
                            upload1.removeAllFiles();
                            fillGrid();
                        }
                        else {
                            alert(result);
                        }
                    }
                });
        }
        else {
            alert("يجب رفع مستند النموذج");
        }
    }
    else {
        alert('يجب اختيار رقم العملية')
    }
}
function browse() {
    if (RequestCode !== "")
        window.open(RequestLogURL + RequestCode + "/" + FileName, '_blank');
    else
        window.open(RequestLogURL + RequestDetailCode + "/" + FileName, '_blank');
}
function upload() {
    if (RequestID !== 0) {
        var upload = $("#FileName").data("kendoUpload");
        upload.upload();
        alert('تم التحميل بنجاح');
        $("#Browse").css('display', 'block');
    }
    else {
        alert('يجب اختيار رقم العملية')
    }
}
function onChangeSelection(arg) {
    //var upload = $("#FileName").data("kendoUpload");
    //upload.clearAllFiles();
    var grid = $("#RequestDetailsGrid").data("kendoGrid");

    RequestSelected = this.selectedKeyNames();
    if (LastselectedLength === RequestSelected[0]) {
        RequestSelected = 0;
        grid.clearSelection();
    }
    LastselectedLength = RequestSelected[0];
    if (RequestSelected.length === 0) {
        $("#EditRequestDetailSerial").val('');
        $("#EditRequestDetailName").val('');
        $("#EditRequestDetailCode").val('');
        unit = $("#EditUnit").data("kendoComboBox");
        unit.text('');
        $("#EditQty").val('');
        $("#EditUnitPrice").val('');
        $("#EditTotalPrice").val('');
        var Department = $("#EditDepartment").data("kendoComboBox");
        Department.text('');
        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {

        $.ajax({
            type: "POST",
            url: '/ItemClass/GetItemClassByRequestDetailsID',
            data: {
                RequestDetailsID: selectedDataItems[0].RequestDetailId
            },
            error: function (result) {
                alert("error");
            },
            success: function (result) {
                if (result !== -1) {
                    var multiselect = $("#EditItemClass").data("kendoMultiSelect");
                    multiselect.dataSource.filter({});
                    multiselect.value(result);
                }
            }
        });
        $("#EditRequestDetailsModalPopup").modal('show');

        RequestID = selectedDataItems[0].RequestId;
        RequestDetailID = selectedDataItems[0].RequestDetailId;

        $("#EditRequestDetailCode").val(selectedDataItems[0].RequestDetailCode);
        RequestDetailCode = selectedDataItems[0].RequestDetailCode;
        $("#EditRequestDetailName").val(selectedDataItems[0].RequestDetailName);
        RequestDetailName = selectedDataItems[0].RequestDetailName;
        $("#EditRequestDetailSerial").val(selectedDataItems[0].RequestDetailSerial);
        RequestDetailSerial = selectedDataItems[0].RequestDetailSerial;
        Unit = selectedDataItems[0].Unit;
        unit = $("#EditUnit").data("kendoComboBox");
        unit.text(Unit);
        $("#EditQty").val(selectedDataItems[0].Qty);
        Qty = selectedDataItems[0].Qty;
        $("#EditUnitPrice").val(selectedDataItems[0].UnitPrice);
        UnitPrice = selectedDataItems[0].UnitPrice;
        $("#EditTotalPrice").val(selectedDataItems[0].TotalPrice);
        TotalPrice = selectedDataItems[0].TotalPrice;
        DepartmentID = selectedDataItems[0].Department;
        Department = $("#EditDepartment").data("kendoComboBox");
        Department.text(DepartmentID);
        //if (selectedDataItems[0].FileNameDetails === null) {
        //    $("#Browse").css('display', 'none');
        //}
        //else {
        //    FileNameDetails = selectedDataItems[0].FileNameDetails;
        //    $("#Browse").css('display', 'block');
        //}
    }
}
function onChangeSelectionLog(arg) {
    //var upload = $("#FileName").data("kendoUpload");
    //upload.clearAllFiles();
    var grid = $("#RequestLogGrid").data("kendoGrid");

    RequestSelected = this.selectedKeyNames();
    if (LastselectedLength === RequestSelected[0]) {
        RequestSelected = 0;
        grid.clearSelection();
    }
    LastselectedLength = RequestSelected[0];
    if (RequestSelected.length === 0) {
        $("#EditRequestDetailSerial").val('');
        $("#EditRequestDetailName").val('');
        $("#EditRequestDetailCode").val('');
        unit = $("#EditUnit").data("kendoComboBox");
        unit.text('');
        $("#EditQty").val('');
        $("#EditUnitPrice").val('');
        $("#EditTotalPrice").val('');
        var Department = $("#EditDepartment").data("kendoComboBox");
        Department.text('');
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
        RequestDetailID = selectedDataItems[0].RequestDetailId;
        //FileName = selectedDataItems[0].LogFileName;
        RequestCode = selectedDataItems[0].RequestCode;

        RequestDetailCode = selectedDataItems[0].RequestDetailCode;

        if (selectedDataItems[0].LogFileName === "") {
            FileName = "";
            $("#Browse").css('display', 'none');
        }
        else {
            FileName = selectedDataItems[0].LogFileName;
            $("#Browse").css('display', 'block');
        }
    }
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
$(document).ready(function () {
    $(".type").on('click', function (e) {
        var type = document.getElementsByName('type');
        for (i = 0; i < type.length; i++) {
            if (type[i].checked) {
                if (type[i].value === "1") { Type = 1; }
                else if (type[i].value === "2") { Type = 2; }
            }
        }
        if (Type === 1) {
            $("#RequestDuration").attr('disabled', false);
            $("#EditQty").attr('disabled', 'disabled');
            $("#EditUnitPrice").attr('disabled', 'disabled');
            $(".k-grid-CreateModal").css('display', 'none');
            $(".k-grid-EditModal").css('display', 'none');
            $("#RequestSaveDiv").css('display', 'block');
            $("#GridView").css('display', 'none');

        }
        else {
            $("#RequestDuration").attr('disabled', 'disabled');
            $("#EditQty").attr('disabled', false);
            $("#EditUnitPrice").attr('disabled', false);
            $(".k-grid-CreateModal").css('display', 'inline');
            $(".k-grid-EditModal").css('display', 'inline');
            $("#RequestSaveDiv").css('display', 'none');
            $("#GridView").css('display', 'block');
        }
    });
    $("#EditQty").on('input', function (e) {
        $("#EditTotalPrice").val(multiply($("#EditQty").val(), $("#EditUnitPrice").val()));
    });
    $("#EditUnitPrice").on('input', function (e) {
        $("#EditTotalPrice").val(multiply($("#EditQty").val(), $("#EditUnitPrice").val()));
    });
    $("#RequestDate").addClass("form-control");
    $("#RequestEndDate").addClass("form-control");
    $("#RequestStartDate").addClass("form-control");
    $("#EditUnit").addClass("form-control");
    $(".form-group .k-datepicker").addClass("form-control");
    $(".k-action-buttons").css('display', 'none');
    $(".k-combobox").addClass("form-control");
    $("#requestfrm").validate({
        rules:
        {
            type:
            {
                required: true
            }
        },
        messages: {
            type:
            {
                required: "من فضلك يجب الإختيار"
            }
        }
    });
    $("#EditRequestDetailsPopup").validate({
        rules:
        {
            EditRequestDetailSerial:
            {
                required: true
            },
            EditRequestDetailName:
            {
                required: true
            },
            EditItemClass:
            {
                required: true
            },
            EditUnit:
            {
                required: true
            },
            EditQty:
            {
                required: true
            },
            EditUnitPrice:
            {
                required: true
            },
            EditDepartment:
            {
                required: true
            }
        },
        messages: {
            EditRequestDetailSerial:
            {
                required: "من فضلك أدخل البند"
            },
            EditRequestDetailName:
            {
                required: "من فضلك أدخلك الوصف"
            },
            EditItemClass:
            {
                required: "من فضلك أدخلك فئة البند"
            },
            EditUnit:
            {
                required: "من فضلك أدخل تاريخ العملية"
            },
            EditQty:
            {
                required: "من فضلك ادخل الكمية المقدره"
            },
            EditUnitPrice:
            {
                required: "من فضلك ادخل الوحدة"
            },
            CreateUnitPrice:
            {
                required: "من فضلك أدخل سعر الوحدة بالجنيه"
            },
            EditDepartment:
            {
                required: "من فضلك أدخل إسم القسم المختص"
            }
        }
    });
    $('.k-grid .k-input').on('input', function (e) {
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
    $('.k-grid .k-input').on('input', function (e) {
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
                }
                else if (type === 'number') {
                    if (isNumeric(e.target.value)) {
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: e.target.value
                        });
                    }

                } else if (type === 'date') {
                    var data = grid.dataSource.data();
                    for (var i = 0; i < data.length; i++) {
                        var dateStr = kendo.format(x.format, data[i][x.field]);
                        if (dateStr.startsWith(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: data[i][x.field]
                            })
                        }
                    }
                }
            }
        });
        grid.dataSource.filter(filter);
    });
    $('.k-grid-EditModal').on('click', function (e) {
        if (RequestSelected === 0 || RequestSelected.length === 0) { alert('يجب إختيار بند للتعديل'); }
        else {
            $("#RequestDetailsEditModalPopup").modal('show');
        }
    });
    $('.k-grid-EditModal').attr('data-toggle', 'modal');
    $('.k-grid-EditModal').attr('href', '#');
    $(".k-grid-EditModal").css('display', 'none');
    $('.k-grid-CreateModal').attr('data-toggle', 'modal');
    $('.k-grid-CreateModal').attr('href', '#'); $('.k-grid-CreateModal').attr('href', '#');
    $(".k-grid-CreateModal").css('display', 'none');
    $('.k-grid-CreateModal').attr('data-target', '#RequestDetailsCreateModalPopup');
    $('#popover-left,#popover-top,#popover-bottom,#popover-right').popover();
    $('#tooltip-left,#tooltip-top,#tooltip-bottom,#tooltip-right').tooltip();
    var element = document.querySelector('[aria-label="Select all rows"]');
    element.disabled = true;
    element.style.visibility = 'hidden';
});
$.validator.setDefaults({ ignore: '' });
function saveRequestDetails() {
    if (!$("#CreateRequestDetailsPopup").valid()) {
        return;
    }
    var itemClass = $("#itemClass").data("kendoMultiSelect");
    $.ajax(
        {
            type: "POST",
            url: '/Request/saveRequestDetailsLog',
            data: {
                RequestId: RequestID,

                Qty: $("#CreateQty").val(),

                UnitPrice: $("#CreateUnitPrice").val(),
                TotalPrice: $("#CreateTotalPrice").val()

            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {
                alert("تم الحفظ بنجاح");
                //fillGrid();
                $(".k-grid-search .k-input").val("");
                $("#CreateRequestDetailSerial").val('');
                $("#CreateRequestDetailName").val('');
                $("#CreateRequestDetailCode").val('');
                $("#CreateUnit").val('');
                $("#CreateQty").val('');
                $("#CreateUnitPrice").val('');
                $("#CreateTotalPrice").val('');
                $("#CreateDepartment").val('');
            }
        });
}
function updateRequestDetails() {
    if (!$("#EditRequestDetailsPopup").valid()) {
        return;
    }
    var itemClass = $("#EditItemClass").data("kendoMultiSelect");
    $.ajax(
        {
            type: "POST",
            url: '/Request/saveRequestDetailsLog',
            data: {

                RequestDetailId: RequestDetailID,
                Qty: $("#EditQty").val(),
                UnitPrice: $("#EditUnitPrice").val(),
                TotalPrice: $("#EditTotalPrice").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {
                var upload = $("#FileName").data("kendoUpload");
                if (FileName !== null) {
                    upload.upload();
                    $("#BrowseFile").css('display', 'block');
                }
                alert("تم الحفظ بنجاح");
                //fillGrid();
                $(".k-grid-search .k-input").val("");
                $("#RequestDetailsEditModalPopup").modal('hide');
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