var selected = 0;
var CompanyId = 0;
var CompanyUsersID = 0;
var CompanyName = "";
var RequestId = "";
var UserName = "";
var Password = "";
var RequestName = "";
var Company = "";
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
function FilterRequest() {
    return {
        CompanyID: $("#Company").data("kendoMultiColumnComboBox").dataItem().CompanyId
    };
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
        $("#UserName").val('');
        $("#Password").val('');
        $("#savebutton").html('<i class="fa fa-save"></i> إضافة');
        CompanyUsersID = 0;
        Company = $("#Company").data("kendoMultiColumnComboBox");
        Company.select(-1);
        Request = $("#Request").data("kendoMultiColumnComboBox");
        Request.select(-1);
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

        RequestId = selectedDataItems[0].RequestId;
        CompanyUsersID = selectedDataItems[0].CompanyUsersId;
        Password = selectedDataItems[0].Password;
        UserName = selectedDataItems[0].UserName;


        $("#UserName").val(UserName);
        $("#Password").val(Password);

        Company = $("#Company").data("kendoMultiColumnComboBox");
        //Company.text(selectedDataItems[0].CompanyName);
        Company.select(function (dataItem) {
            return dataItem.CompanyId === selectedDataItems[0].CompanyId;
        });
        Request = $("#Request").data("kendoMultiColumnComboBox");
        //Request.text(selectedDataItems[0].RequestName);
        Request.select(function (dataItem) {
            return dataItem.RequestId === selectedDataItems[0].RequestId;
        });

    }
}
$(document).ready(function () {
    $("#Request").addClass("form-control");
    $("#Company").addClass("form-control");

    $("#companyfrm").validate({
        rules:
        {
            Company:
            {
                required: true
            },
            Request:
            {
                required: true
            },
            UserName:
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
            Company:
            {
                required: "من فضلك أدخل الشركة"
            },
            Request:
            {
                required: "من فضلك أدخل العملية"
            },
            UserName:
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
function saveCompanyUsers() {
    if (!$("#companyfrm").valid()) {
        return;
    }
    $.ajax(
        {
            type: "POST",
            url: '/Company/SaveCompanyUsers',
            data: {
                CompanyId: $("#Company").val(),
                CompanyUsersId: CompanyUsersID,
                UserName: $("#UserName").val(),
                Password: $("#Password").val(),
                RequestId: $("#Request").val()
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                $("#UserName").val('');
                $("#Password").val('');

                CompanyUsersID = 0;
                Company = $("#Company").data("kendoMultiColumnComboBox");
                Company.select(-1);
                Request = $("#Request").data("kendoMultiColumnComboBox");
                Request.select(-1);

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