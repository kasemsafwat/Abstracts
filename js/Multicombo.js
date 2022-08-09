$(document).ready(function () {
    $("#button1").click(function () {
        var obj = [
            { Id: '1', Name: 'ahmed', Type: 'developer' }];
        //var obj = [Id: "1", Name: "ahmed", Type: "developer"];
        onSelect(obj);
    });
});

function onOpen() {
    //alert("event: open");
}

function onClose() {
    //alert("event: close");
}

function onChange() {
    //alert("event: change");
}

function onDataBound() {
    //alert("event: dataBound");
}

function onSelect(e) {
    //if ("kendoConsole" in window) {
    if (e.item) {
        var dataItem = this.dataItem(e.item.index());
        alert("" + dataItem.Name + " : " + dataItem.Type + "");
    } else {
        alert("event :: select");
    }
    //}
}

function onFiltering() {
    //alert("event: filtering");
}

function onAdditionalData() {
    return {
        text: $("#customer").val()
    };
}