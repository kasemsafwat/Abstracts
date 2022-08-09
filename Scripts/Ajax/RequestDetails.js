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
var FileNameDetails = "";
var Department = "";
var LastselectedLength = 0;
var RequestExcelFileName = "";

function Search() {
    var upload = $("#FileName").data("kendoUpload");
    upload.clearAllFiles();
    var upload1 = $("#RequestExcelFileName").data("kendoUpload");
    upload1.clearAllFiles();
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
                if (result.FileNameDetails === null) {
                    $("#BrowseFile").css('display', 'none');
                }
                else {
                    FileName = result.FileNameDetails;
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
            }

        }
    });
}
function onPaging(arg) {
    LastselectedLength = 0;
    var grid = $("#RequestDetailsGrid").data("kendoGrid");
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
    var upload = $("#FileName").data("kendoUpload");
    upload.clearAllFiles();
    var upload1 = $("#RequestExcelFileName").data("kendoUpload");
    upload1.clearAllFiles();
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

        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);
    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {
        //lastSelected = selectedDataItems[0].uid;
        //$('#lock').prop('checked', selectedDataItems[0].IsLocked);
        //locked = selectedDataItems[0].IsLocked;
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

        if (selectedDataItems[0].FileName === null) {
            $("#BrowseFile").css('display', 'none');
        }
        else {
            FileName = selectedDataItems[0].FileName;
            $("#BrowseFile").css('display', 'block');
        }
        //return;
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
            if (result.FileNameDetails === null) {
                $("#Browse").css('display', 'none');
            }
            else {
                FileNameDetails = result.FileNameDetails;
                $("#Browse").css('display', 'block');
            }
            fillGrid();

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
    FileNameDetails = e.files[0].name;
    //$(".k-action-buttons").hide();
}
function onRequestExcelSelect(e) {
    var upload = $("#RequestExcelFileName").data("kendoUpload");
    upload.removeAllFiles();

    if (e.files.length > 1) {
        alert("من فضلك إرفع ملف واحد فقط");
        e.preventDefault();
    }
    RequestExcelFileName = e.files[0].name;
    //$(".k-action-buttons").hide();
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
            var validator = $("#requestResultfrm").kendoValidator({
                rules: {
                    upload: function (input) {
                        if (input[0].type === "file") {

                            var len = input.closest(".k-upload").find(".k-file").length;
                            fileNotSelected = true;
                            return len > 0;
                        }
                        return true;
                    }
                }
            }).data("kendoValidator");
            if (!validator.validate()) {
                if (fileNotSelected) {
                    alert('يجب تحميل نموذج المقايسة');
                }
                return;
            }
            var upload1 = $("#RequestExcelFileName").data("kendoUpload");
            upload1.upload();
            setTimeout(args => {
              
           
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
                            fillGrid();
                            var upload1 = $("#RequestExcelFileName").data("kendoUpload");
                            upload1.removeAllFiles();
                          
                        }
                        else {
                            alert(result);
                        }
                    }
                });
            }, 1000);
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

    window.open(RequestDetailsURL + RequestCode + "/" + FileNameDetails, '_blank');
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
    var upload = $("#FileName").data("kendoUpload");
    upload.clearAllFiles();
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
        //$("#EditDepartment").val('');
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

    if (selectedDataItems.length === 2 && selectedDataItems !== []) {

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

                    //set value
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
        if (selectedDataItems[0].FileNameDetails === null) {
            $("#Browse").css('display', 'none');
        }
        else {
            FileNameDetails = selectedDataItems[0].FileNameDetails;
            $("#Browse").css('display', 'block');
        }
    }
}
//function onClick(e) {
//    var grid = $("#grid2").data("kendoGrid");
//    var row = $(e.target).closest("tr");

//    if (row.hasClass("k-state-selected")) {
//        setTimeout(function (e) {
//            var grid = $("#grid").data("kendoGrid");
//            grid.clearSelection();
//        })
//    } else {
//        grid.clearSelection();
//    };
//};

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
    $("#CreateUnit").addClass("form-control");
    $(".form-group .k-datepicker").addClass("form-control");
    $(".form-group .k-content .k-upload").addClass("form-control");
    $(".k-combobox").addClass("form-control");
    $("#CreateRequestDetailsPopup").validate({
        rules:
        {
            CreateRequestDetailSerial:
            {
                required: true
            },
            CreateRequestDetailName:
            {
                required: true
            },
            itemClass:
            {
                required: true
            },
            CreateUnit:
            {
                required: true
            },
            CreateQty:
            {
                required: true
            },
            CreateUnitPrice:
            {
                required: true
            },
            CreateDepartment:
            {
                required: true
            }
        },
        messages: {
            CreateRequestDetailSerial:
            {
                required: "من فضلك أدخل البند"
            },
            CreateRequestDetailName:
            {
                required: "من فضلك أدخلك الوصف"
            },
            itemClass:
            {
                required: "من فضلك أدخلك فئة البند"
            },
            CreateQty:
            {
                required: "من فضلك ادخل الكمية المقدره"
            },
            CreateUnit:
            {
                required: "من فضلك ادخل الوحدة"
            },
            CreateUnitPrice:
            {
                required: "من فضلك أدخل سعر الوحدة بالجنيه"
            },
            CreateDepartment:
            {
                required: "من فضلك أدخل إسم القسم المختص"
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

    //for search on any char like string or numbers or boolean or date
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
    //$('.k-grid-EditModal').attr('data-target', '#RequestDetailsEditModalPopup');
    $('.k-grid-CreateModal').attr('data-toggle', 'modal');
    $('.k-grid-CreateModal').attr('href', '#');
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
            url: '/Request/saveRequestDetails',
            data: {
                RequestId: RequestID,
                RequestDetailSerial: $("#CreateRequestDetailSerial").val(),
                RequestDetailName: $("#CreateRequestDetailName").val(),
                RequestDetailCode: $("#CreateRequestDetailCode").val(),
                UnitID: $("#CreateUnit").val(),
                Qty: $("#CreateQty").val(),
                ItemClasses: itemClass.value(),
                UnitPrice: $("#CreateUnitPrice").val(),
                TotalPrice: $("#CreateTotalPrice").val(),
                DepartmentId: $("#CreateDepartment").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                alert("تم الحفظ بنجاح");

                fillGrid();
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
            url: '/Request/saveRequestDetails',
            data: {
                RequestId: RequestID,
                RequestDetailId: RequestDetailID,
                RequestDetailSerial: $("#EditRequestDetailSerial").val(),
                RequestDetailName: $("#EditRequestDetailName").val(),
                RequestDetailCode: $("#EditRequestDetailCode").val(),
                UnitID: $("#EditUnit").val(),
                ItemClasses: itemClass.value(),
                Qty: $("#EditQty").val(),
                UnitPrice: $("#EditUnitPrice").val(),
                TotalPrice: $("#EditTotalPrice").val(),
                DepartmentId: $("#EditDepartment").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                alert("تم الحفظ بنجاح");
                fillGrid();
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