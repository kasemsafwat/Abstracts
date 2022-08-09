var selected = 0;
var CompanyId = 0;
var CompanyName = "";
var CompanyCode = "";
var Email = "";
var InsuranceNo = "";
var Mobile = "";
var Phone = "";
var TaxRecordNo = "";
var locked = "";
var LastselectedLength = 0;
$.validator.setDefaults({ ignore: '' });
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
        $("#savebutton").html('<i class="fa fa-save"></i> إضافة');
        $('#lock').prop('checked', false);
        $("#TaxRecordNo").val('');
        $("#CompanyName").val('');
        $("#CompanyCode").val('');
        $("#Email").val('');
        $("#InsuranceNo").val('');
        $("#Mobile").val('');
        $("#Phone").val('');
        CompanyId = 0;

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
        CompanyId = selectedDataItems[0].CompanyId;
        CompanyName = selectedDataItems[0].CompanyName;
        CompanyCode = selectedDataItems[0].CompanyCode;
        Email = selectedDataItems[0].Email;
        InsuranceNo = selectedDataItems[0].InsuranceNo;
        Mobile = selectedDataItems[0].Mobile;
        Phone = selectedDataItems[0].Phone;
        TaxRecordNo = selectedDataItems[0].TaxRecordNo;
        locked = selectedDataItems[0].IsLocked;

        $('#lock').prop('checked', locked);
        $("#TaxRecordNo").val(TaxRecordNo);
        $("#CompanyName").val(CompanyName);
        $("#CompanyCode").val(CompanyCode);
        $("#Email").val(Email);
        $("#InsuranceNo").val(InsuranceNo);
        $("#Mobile").val(Mobile);
        $("#Phone").val(Phone);
    }
}
$(document).ready(function () {
    $("#Phone").addClass("form-control");
    $("#Mobile").addClass("form-control");

    $("#companyfrm").validate({
        rules:
        {
            CompanyName:
            {
                required: true
            },
            Email:
            {
                email:true,
                required: true
            },
            Mobile:
            {   
                required: true
            },
            Phone:
            {
                required: true
            },
            TaxRecordNo:
            {
                required: true
            },
            InsuranceNo:
            {
                required: true
            }

        },
        messages: {
            CompanyName:
            {
                required: "من فضلك أدخل إسم الشركة"
            },
            Email:
            {
                required: "من فضلك أدخل إيميل الشركة",
                email:"من فضلك أدخل الإيميل بشكل صحيح"
            },
            Mobile:
            {
                required: "من فضلك أدخل رقم الموبيل"
            },
            Phone:
            {
                required: "من فضلك أدخل تليفون المكتب"
            },
            TaxRecordNo:
            {
                required: "من فضلك أدخل السجل الضريبي"
            },
            InsuranceNo:
            {
                required: "من فضلك أدخل الرقم التأميني"
            }

        }
    });

    //for search on any char like string or numbers or boolean or date
    $('.k-grid-search').on('input', function (e) {
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

    $('#popover-left,#popover-top,#popover-bottom,#popover-right').popover();
    $('#tooltip-left,#tooltip-top,#tooltip-bottom,#tooltip-right').tooltip();

    var element = document.querySelector('[aria-label="Select all rows"]');
    element.disabled = true;
    element.style.visibility = 'hidden';
});
$.validator.setDefaults({ ignore: '' });
function saveCompany() {
    if (!$("#companyfrm").valid()) {
        return;
    }
    $.ajax(
        {
            type: "POST",
            url: '/Company/SaveCompany',
            data: {
                CompanyId: CompanyId,
                TaxRecordNo: $("#TaxRecordNo").val(),
                IsLocked: $("#lock").prop("checked"),
                CompanyName: $("#CompanyName").val(),
                CompanyCode: $("#CompanyCode").val(),
                Email: $("#Email").val(),
                InsuranceNo: $("#InsuranceNo").val(),
                Mobile: $("#Mobile").val(),
                Phone: $("#Phone").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                $("#CompanyCode").val("Com" + result);
                $("#CompanyName").val("");
                $("#Email").val("");
                $("#InsuranceNo").val("");
                $("#Mobile").val("555 123 45617");
                $("#Phone").val("555 123 4567");
                $("#TaxRecordNo").val("");
                $('#lock').prop('checked', false);

                var grid = $("#grid").data("kendoGrid");
                //grid.clearSelection();
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