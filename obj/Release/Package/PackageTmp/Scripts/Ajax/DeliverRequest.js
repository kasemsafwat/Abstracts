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


function Search() {
    var upload = $("#FileName").data("kendoUpload");
    upload.removeAllFiles();
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
function onChangeSelection2(arg) {
    var upload = $("#FileName").data("kendoUpload");
    upload.clearAllFiles();
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
        lastSelected = selectedDataItems[0].uid;
        $('#lock').prop('checked', selectedDataItems[0].IsLocked);
        locked = selectedDataItems[0].IsLocked;
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
function browse() {
    window.open(DeliverRequestURL + RequestCode + "/" + FileName, '_blank');
}

function convertDate(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    year = date.getFullYear();
    return [day, mnth, year].join("/");
}
function onChangeRequestStartDate(arg) {
    if (RequestID !== 0) {
        if (RequestDuration !== null) {
            $.ajax({
                type: "POST",
                url: '/Request/GetRequestEndDate',
                data: {
                    Duration: RequestDuration,
                    StartRequestDate: $("#RequestStartDate").val()
                },
                error: function (result) {
                    alert("error");
                },
                success: function (result) {
                    RequestStartDate = result;
                    $("#RequestEndDate").val(result);
                }
            });
        }
        else {
            alert('من فضلك أدخل مدة العملية');
        }
    }
    else {
        alert('من فضلك أدخل العملية');
        $("#RequestEndDate").val('');
    }
}

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
$(document).ready(function () {
    //$('#RequestStartDate').on('input', function (e) {
    //    onChangeRequestStartDate();
    //});
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
var fileNotSelected = false;
function saveRequest() {
    if (!$("#requestResultfrm").valid()) {
        return;
    }

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
            alert('يجب إختيار صورة المحضر')
        }
        return;
    }
    $.ajax(
        {
            type: "POST",
            url: '/Request/saveRequest',
            data: {
                RequestId: RequestID,
                RequestNo: RequestNo,
                RequestName: RequestName,
                RequestCode: RequestCode,
                RequestDate: RequestDate,
                RequestAmount: RequestAmount,
                RequestStartDate: $("#RequestStartDate").val(),
                RequestDuration: $("#RequestDuration").val(),
                RequestEndDate: $("#RequestEndDate").val(),
                Notes: Notes,
                CompanyId: CompanyID
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

                   

                    //var grid = $("#grid2").data("kendoGrid");
                    //$(".k-grid-search .k-input").val("");
                    ////grid.dataSource.filter(null);
                    ////grid.clearSelection();
                    //grid.dataSource.read();

                    //var ComboBox = $("#RequestCode").data("kendoMultiColumnComboBox");
                    //ComboBox.dataSource.read();

                    //$("#RequestCode").data("kendoMultiColumnComboBox").select(-1);

                    //$("#RequestName").val('');
                    //$("#RequestDate").val('');
                    //$("#RequestAmount").val('');
                    //$("#RequestStartDate").val('');
                    //$("#RequestDuration").val('');
                    //$("#RequestEndDate").val('');
                    //$("#Notes").val('');
                    //$("#Company").val('');
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