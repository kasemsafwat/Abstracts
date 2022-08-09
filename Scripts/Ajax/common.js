////var RequestDetailsURL = "http://196.221.166.86:91/UploadedFiles/RequestDetails/";
////var DeliverRequestURL = "http://196.221.166.86:91/UploadedFiles/Requests/";
////var RequestLogURL = "http://196.221.166.86:91/UploadedFiles/RequestLog/";
////var WorkOrderURL = "http://196.221.166.86:91/UploadedFiles/WorkOrder/";
////var ApprovedWorkOrderURL = "http://196.221.166.86:91/UploadedFiles/ApprovedWorkOrder/";


//var RequestDetailsURL = "http://paradiseapps-001-site2.itempurl.com/UploadedFiles/requestdetails/";
//var DeliverRequestURL = "http://paradiseapps-001-site2.itempurl.com/UploadedFiles/requests/";
//var RequestLogURL = "http://paradiseapps-001-site2.itempurl.com/UploadedFiles/RequestLog/";
//var WorkOrderURL = "http://paradiseapps-001-site2.itempurl.com/UploadedFiles/workorder/";
//var ApprovedWorkOrderURL = "http://paradiseapps-001-site2.itempurl.com/UploadedFiles/ApprovedWorkOrder/";

var RequestDetailsURL = "http://192.168.16.11:192/UploadedFiles/RequestDetails/";
var RequestLogURL = "http://192.168.16.11:192/UploadedFiles/RequestLog/";
var DeliverRequestURL = "http://192.168.16.11:192/UploadedFiles/Requests/";
var WorkOrderURL = "http://192.168.16.11:192/UploadedFiles/WorkOrder/";
var ApprovedWorkOrderURL = "http://192.168.16.11:192/UploadedFiles/ApprovedWorkOrder/";

var LastselectedLengthgrid2 = 0;
$(function () {
    updateNotifications();
    $('#grid2 .k-grid-search').on('input', function (e) {
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

                }
            }
        });
        grid.dataSource.filter(filter);
    });

});
function SearchForRequest() {

    $.ajax({
        type: "POST",
        url: '/Request/SearchForRequest/',
        data: {
            RequestNo: $("#RequestNo").val(),
            RequestDate: $("#RequestDateLayout").val()
        },
        error: function (result) {
            alert("error");
        },
        success: function (result) {
            var grid = $("#grid2").data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

function updateNotifications() {
    var dataobj = { allItems: true };
    $.ajax({
        type: "POST",
        url: "/WorkOrder/GetWorkOrderApprovedList", 
        datatype: "json",
        data: JSON.stringify(dataobj),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $(".cart-items-count").html(result.resultCount);
            $(".NumOfWorkOrderItems").html(result.resultCount);
            DrawNotifications(result.result);
        }
    })
}
function DrawNotifications(list) {
  
    $.ajax({
        type: "POST",
        url: "/WorkOrder/DrawNotifications", 
        datatype: "json",
        data: JSON.stringify(list),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $("#tbl").html(result);
        }
    })
}
function GetWorkOrderFromNotification(WorkOrderID) {
    var dataobj = { WorkOrderID: WorkOrderID };
    $.ajax({
        type: "POST",
        url: "/WorkOrder/GetWorkOrderFromNotification", 
        datatype: "json",
        data: JSON.stringify(dataobj),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            window.location = result;
        }
    })
}

function onPaging2(arg) {
    LastselectedLengthgrid2 = 0;
    var grid = $("#grid2").data("kendoGrid");
    grid.clearSelection();
}

