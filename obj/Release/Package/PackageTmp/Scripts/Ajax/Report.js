var selected = 0;
var CompanyId = 0;
var CompanyUsersID = 0;
var CompanyName = "";
var RequestId = "";
var UserName = "";
var Password = "";
var RequestName = "";

var LastselectedLength = 0;


function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

$(document).ready(function () {
    $("#ItemClass").addClass("form-control");
    $(".k-input").addClass("form-control");
    $("#RequestEndDate").addClass("form-control");
    $("#RequestStartDate").addClass("form-control");

    //$("#companyfrm").validate({
    //    rules:
    //    {
    //        ARName:
    //        {
    //            required: true
    //        }
    //    },
    //    messages: {
    //        ARName:
    //        {
    //            required: "من فضلك أدخل وحده القياس"
    //        }
    //    }
    //});

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
});

function SearchForReport() {
    //if (!$("#companyfrm").valid()) {
    //    return;
    //}
    $.ajax(
        {
            type: "POST",
            url: '/Report/GetReport',
            data: {
                ItemClassId: $("#ItemClass").val(),
                RequestStartDate: $("#RequestStartDate").val(),
                RequestEndDate: $("#RequestEndDate").val(),
            },

            error: function (result) {
                alert("There is a Problem, Try Again!");
            },
            success: function (result) {

                //$("#CompanyCode").val("Com" + result);
                //$("#CompanyName").val("");
                //$("#Email").val("");
                //$("#InsuranceNo").val("");
                //$("#Mobile").val("555 123 45617");
                //$("#Phone").val("555 123 4567");
                //$("#TaxRecordNo").val("");
                //$('#lock').prop('checked', false);

                var grid = $("#grid").data("kendoGrid");
                //grid.clearSelection();
                grid.dataSource.read();
                //alert("تم الحفظ بنجاح");
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