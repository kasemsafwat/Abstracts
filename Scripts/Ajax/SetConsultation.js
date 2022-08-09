var selected = 0;
var RequestID = 0;
var RequestNo = "";
var RequestCode = "";
var RequestName = "";
var RequestDate = "";
var RequestAmount = "";
var RequestStartDate = "";
var RequestDuration = "";
var RequestEndDate = "";
var FileName = "";
var Notes = "";
var CompanyID = "";
var ConsultationWorkOrderID = 0;
var LastselectedLength = 0;

function Search() {
    ConsultationWorkOrderID = 0;
    var grid = $("#grid").data("kendoGrid");
    grid.clearSelection();
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

                $("#RequestNo").val('');
                $("#RequestName").val('');
                $("#RequestDate").val('');
                $("#Company").val('');
                $("#RequestAmount").val('');
                $("#RequestStartDate").val('');
                $("#RequestDuration").val('');
                $("#RequestEndDate").val('');
                $("#Notes").val('');
                $("#BrowseFile").css('display', 'none');
            }

        }
    });
}
function onSelect(e) {
    var upload = $("#FileName").data("kendoUpload");
    upload.removeAllFiles();

    if (e.files.length > 1) {
        alert("Please select max 20 files.");
        e.preventDefault();
    }
    //$(".k-action-buttons").hide();
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
function onPaging(arg) {
    LastselectedLength = 0;
    var grid = $("#grid").data("kendoGrid");
    grid.clearSelection();
}
function onChangeSelection(arg) {
    //var upload = $("#FileName").data("kendoUpload");
    //upload.clearAllFiles();
    var grid = $("#grid").data("kendoGrid");

    selected = this.selectedKeyNames();
    if (LastselectedLength === selected[0]) {
        selected = [];
        grid.clearSelection();
    }
    LastselectedLength = selected[0];
    if (selected.length === 0) {
        ConsultationWorkOrderID = 0;
        return;
    }
    var selectedRows = this.select();
    var selectedDataItems = [];
    for (var i = 0; i < selectedRows.length; i++) {
        var dataItem = this.dataItem(selectedRows[i]);
        selectedDataItems.push(dataItem);

    }

    if (selectedDataItems.length === 1 && selectedDataItems !== []) {
        ConsultationWorkOrderID = selectedDataItems[0].ConsultationWorkOrderId;



    }
}
function onChangeSelection2(arg) {
    //var upload = $("#FileName").data("kendoUpload");
    //upload.clearAllFiles();
    var grid = $("#grid2").data("kendoGrid");
    ConsultationWorkOrderID = 0;

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

        RequestID = selectedDataItems[0].RequestId
        $("#RequestNo").val(selectedDataItems[0].RequestNo);
        RequestNo = selectedDataItems[0].RequestNo;

        $("#RequestDate").val(selectedDataItems[0].RequestDateString);
        RequestDate = selectedDataItems[0].RequestDateString;

        $("#RequestSearchModalPopup").modal('hide');
        $.ajax({
            type: "POST",
            url: '/Request/SetRequestID',
            data: {
                RequestID: selectedDataItems[0].RequestId
            },
            error: function (result) {
                alert("error");
            },
            success: function (result) {
                var grid = $("#grid").data("kendoGrid");

                grid.dataSource.read();
            }
        });
    }
}

$(document).ready(function () {
    //$('#RequestStartDate').on('input', function (e) {
    //    onChangeRequestStartDate();
    //});
    $('.k-grid-AddConsulation').attr('Onclick', 'saveConsulation()');
    $('.k-grid-AddConsulation').attr('href', '#');
    $('.k-grid-DeleteConsulation').attr('Onclick', 'DeleteConsulation()');
    $('.k-grid-DeleteConsulation').attr('href', '#');
    //$(".k-grid-AddConsulation").css('display', 'none');
    //$('.k-grid-AddConsulation').attr('data-target', '#RequestDetailsCreateModalPopup');
    $('#RequestStartDate').keypress(function (e) {
        var key = e.which;
        if (key === 13)  // the enter key code
        {
            onChangeRequestStartDate();
        }
    });
    $("#RequestDate").addClass("form-control");
    $("#RequestCode").addClass("form-control");
    $("#RequestEndDate").addClass("form-control");
    $("#RequestStartDate").addClass("form-control");
    $("#Company").addClass("form-control");
    $(".k-combobox").addClass("form-control");
    $("#requestfrm").validate({
        rules:
        {
            RequestNo:
            {
                required: true
            },
            RequestDate:
            {
                required: true
            }
        },
        messages: {
            RequestNo:
            {
                required: "من فضلك أدخل أمر الإسناد"
            },
            RequestDate:
            {
                required: "من فضلك أدخل تاريخ العملية"
            }
        }
    });
    $("#requestResultfrm").validate({
        rules:
        {
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

    //for search on any char like string or numbers or boolean or date
    $('#popover-left,#popover-top,#popover-bottom,#popover-right').popover();
    $('#tooltip-left,#tooltip-top,#tooltip-bottom,#tooltip-right').tooltip();
    var element = document.querySelector('[aria-label="Select all rows"]');
    element.disabled = true;
    element.style.visibility = 'hidden';
});
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

function saveConsulation() {
    if (parseInt($("#WorkOrderNoFrom").val()) > parseInt($("#WorkOrderNoTo").val())) {
        alert('يجب إدخال المستخلص قبل أكبر من المستخلص بعد');
    } else {
        $.ajax(
            {
                type: "POST",
                url: '/Consultation/SaveConsultationWorkOrder',
                data: {
                    RequestId: RequestID,
                    ConsultationUserId: $("#ConsultationUser").val(),
                    WorkOrderNoFrom: $("#WorkOrderNoFrom").val(),
                    WorkOrderNoTo: $("#WorkOrderNoTo").val(),
                    IsHaveFinal: $("#IsHaveFinal").prop("checked")
                },

                error: function (result) {
                    alert(result.statusText);
                },
                success: function (result) {
                    if (result === 1) {
                        var grid = $("#grid").data("kendoGrid");

                        grid.dataSource.read();
                        alert("تم الحفظ بنجاح");
                    }
                    else if (result === -1)
                        alert('يجب إضافة مستخلص أكبر من السابق');
                    else if (result === -2)
                        alert('لا يوجد فترة اخري لهذه العملية');

                }
            });
    }
}
function DeleteConsulation() {

    if (ConsultationWorkOrderID !== 0) {
        if (window.confirm("هل انت متأكد؟")) {
            $.ajax(
                {
                    type: "POST",
                    url: '/Consultation/DeleteConsultationWorkOrder',
                    data: {
                        ConsultationWorkOrderId: ConsultationWorkOrderID
                    },

                    error: function (result) {
                        alert("There is a Problem, Try Again!");
                    },
                    success: function (result) {
                        var grid = $("#grid").data("kendoGrid");

                        grid.dataSource.read();
                        alert("تم الحذف بنجاح");

                    }
                });
        }
    } else
        alert('يجب إختيار أحد العناصر للحذف');
}