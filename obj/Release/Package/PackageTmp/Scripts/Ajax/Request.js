var selected = 0;
var RequestID = 0;
var locked = "";
var RequestNo = "";
var RequestName = "";
var RequestDate = "";
var RequestAmount = "";
var RequestStartDate = "";
var RequestDuration = "";
var RequestEndDate = "";
var Notes = "";
var CompanyID = "";
var CompanyName = "";
var dropdownlist = "";
var LastselectedLength = 0;

function onPaging(arg) {
    LastselectedLength = 0;
    var grid = $("#RequestGrid").data("kendoGrid");
    grid.clearSelection();
}
function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
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
        $('#lock').prop('checked', false);
        $("#RequestNo").val('');
        $("#RequestName").val('');
        $("#RequestDate").val('');
        $("#RequestAmount").val('');
        $("#RequestStartDate").val('');
        $("#RequestDuration").val('');
        $("#RequestEndDate").val('');
        $("#Notes").val('');
        $("#savebutton").html('<i class="fa fa-save"></i> إضافة');

        RequestID = 0;
        dropdownlist = $("#Company").data("kendoMultiColumnComboBox");
        dropdownlist.text('');

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
        RequestID = selectedDataItems[0].RequestId;
        onChangeRequest(RequestID);
        locked = selectedDataItems[0].IsLocked;
        RequestNo = selectedDataItems[0].RequestNo;
        RequestName = selectedDataItems[0].RequestName;
        RequestDate = selectedDataItems[0].RequestDateString;
        RequestAmount = selectedDataItems[0].RequestAmount;
        Notes = selectedDataItems[0].Notes;
        CompanyID = selectedDataItems[0].CompanyId;
        RequestDuration = selectedDataItems[0].RequestDuration;
        RequestStartDate = selectedDataItems[0].RequestStartDateString;
        RequestEndDate = selectedDataItems[0].RequestEndDateString;
        CompanyName = selectedDataItems[0].Company;
        $('#lock').prop('checked', selectedDataItems[0].IsLocked);
        $("#RequestNo").val(RequestNo);
       
        $("#RequestName").val(RequestName);
       
        $("#RequestDate").val(RequestDate);
      
        $("#RequestAmount").val(RequestAmount);
      
        $("#Notes").val(Notes);

        dropdownlist = $("#Company").data("kendoMultiColumnComboBox");
        dropdownlist.text(CompanyName);
      
        $("#RequestDuration").val(selectedDataItems[0].RequestDuration);
      
        if (RequestStartDate !== null) {
            $("#RequestStartDate").val(convertDate(RequestStartDate));
          
        }
        else {
            $("#RequestStartDate").val("")
        }
        if (RequestEndDate !== null) {
            $("#RequestEndDate").val(convertDate(RequestEndDate));
           
        }
        else {
            $("#RequestEndDate").val("")
        }
       
    }
}
function convertDate(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    year = date.getFullYear();
    return [day, mnth, year].join("-");
}
$(document).ready(function () {
    $("#RequestDate").addClass("form-control");
    $("#Company").addClass("form-control");
    $(".k-combobox").addClass("form-control");
    $(".k-datepicker").addClass("form-control");

    $("#requestfrm").validate({
        rules:
        {
            RequestNo:
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
            CompanyID:
            {
                required: true
            }
        },
        messages: {
            RequestNo:
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
            CompanyID:
            {
                required: "من فضلك أدخل إسم الشركة"
            }
        }
    });

    //for search on any char like string or numbers or boolean or date
    $('.k-grid-search').on('input', function (e) {
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


    $('#popover-left,#popover-top,#popover-bottom,#popover-right').popover();
    $('#tooltip-left,#tooltip-top,#tooltip-bottom,#tooltip-right').tooltip();
    var element = document.querySelector('[aria-label="Select all rows"]');
    element.disabled = true;
    element.style.visibility = 'hidden';
});
function saveRequest() {
    if (!$("#requestfrm").valid()) {
        return;
    }
    $.ajax(
        {
            type: "POST",
            url: '/Request/saveRequest',
            data: {
                RequestId: RequestID,
                RequestNo: $("#RequestNo").val(),
                RequestName: $("#RequestName").val(),
                RequestDate: $("#RequestDate").val(),
                RequestAmount: $("#RequestAmount").val(),
                RequestStartDate: $("#RequestStartDate").val(),
                RequestDuration: $("#RequestDuration").val(),
                RequestEndDate: $("#RequestEndDate").val(),
                Notes: $("#Notes").val(),
                CompanyId: $("#Company").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {
                
                    $('#lock').prop('checked', false);
                    var grid2 = $("#RequestGrid").data("kendoGrid");
                    $(".k-grid-search .k-input").val('');
                   
                    grid2.dataSource.read();
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
                    dropdownlist = $("#Company").data("kendoMultiColumnComboBox");
                    dropdownlist.text('');
                
            }
        });
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