var Docnum = "";
function Notifjob(Session) {
    if (Session){
    $.ajax({
        type: "POST",
        url: "GEARSMainMenu.aspx/GetNotifyMe",
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d[3] == "success") {
                let datas = JSON.parse(data.d[1]);
                datas.forEach(function (obj, i) {
                    if (obj.Accept_count > 0) { 
                        console.log("Accepted Inbound");
                        let reject = JSON.parse(data.d[2]);
                        reject.forEach(function (obj, i) {
                            if (obj.TransType == 'Inbound') {
                                Docnum = "<div class=''>" +
                                    "<div class='TestAlign'><b>" + obj.FullName + " has Accepted this transaction</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.AcceptDate + "</div>" +
                                    "</div>";
                            }
                            else {
                                Docnum = "<div class=''>" +
                                    "<div class='TestAlign'><b>" + obj.FullName + " has Accepted this transaction</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.AcceptDate + "</div>" +
                                    "</div>";
                            }

                        });
                        showNotificationforInbound();
                    }
                    if (obj.Reject_count > 0) {
                        console.log("Rejected Inbound");
                        let reject = JSON.parse(data.d[2]);
                        reject.forEach(function (obj, i) {
                            if (obj.TransType == 'Inbound') {
                                Docnum = "<div>" +
                                    "<div class='TestAlign'><b>" + obj.FullName + " has Rejected this transaction</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.RejectDate + "</div>" +
                                    "</div>";
                            }
                            else {
                                Docnum = "<div>" +
                                    "<div class='TestAlign'><b>" + obj.FullName + " has Rejected this transaction</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.RejectDate + "</div>" +
                                    "</div>";
                            }
                        });
                        showNotificationforOutbound();
                    }
                    if (obj.Pending_count > 0) {
                        let reject = JSON.parse(data.d[2]);
                        reject.forEach(function (obj, i) {
                            Docnum = obj.DocNumber + " is Pending!";
                        });
                        showNotificationforPending();
                    }
                    if (obj.Inbound_Count > 0) {
                        console.log("New Inbound");
                        let reject = JSON.parse(data.d[2]);
                        reject.forEach(function (obj, i) {
                            Docnum = "<div class='flex-container'>" +
                                "<div class=''><b>New Inbound Sync from Client Portal</b></div>" +
                                "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.DocDate + "</div>" +
                                "</div>";
                        });
                        showNotificationforNewInbound();
                    }
                    if (obj.Outbound_Count > 0) {
                        console.log("New Outbound");
                        let reject = JSON.parse(data.d[2]);
                        reject.forEach(function (obj, i) {
                            Docnum = "<div class='flex-container'>" +
                                "<div class=''><b>New Picklist Sync from Client Portal</b></div>" +
                                "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.DocDate + "</div>" +
                                "</div>";
                        });
                        showNotificationforNewOutbound();
                    }
                    if (obj.CompleteUnload > 0) {
                        console.log("CompleteUnload Inbound");
                        let reject = JSON.parse(data.d[2]);
                        reject.forEach(function (obj, i) {
                            if (obj.TransType == 'Inbound') {
                                Docnum = "<div class='Load-Unload'>" +
                                    "<div class=''><b>Receiving Completed</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.CompleteUnload + "</div>" +
                                    "</div>";
                            }
                            else {
                                Docnum = "<div class='Load-Unload'>" +
                                    "<div class=''><b>Loading Completed</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.CompleteLoading + "</div>" +
                                    "</div>";
                            }
                        });
                        showNotificationforCompleteUnload();
                    }
                    if (obj.RFPutAwayBy > 0) {
                        console.log("PutAway by RF");
                        let reject = JSON.parse(data.d[2]);
                        reject.forEach(function (obj, i) {
                            if (obj.TransType == 'Inbound') {
                                Docnum = "<div class='Load-Unload'>" +
                                    "<div class=''><b>PutAway by "+obj.FullName+"</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.RFPutAwayDate + "</div>" +
                                    "</div>";
                                //Docnum = obj.DocNumber + " PutAway" + "\n Completed!";
                            }
                            else {
                                Docnum = "<div class='Load-Unload'>" +
                                    "<div class=''><b>Picked by " + obj.FullName + "</b></div>" +
                                    "<div class='flex-item'>" + obj.DocNumber + '\n' + obj.CustomerCode + '\n' + obj.WarehouseCode + '<br>' + obj.RFPickDate + "</div>" +
                                    "</div>";
                            }
                        });
                        showNotificationforCompletePutAway();
                    }
                });


            }
            else if (data.d[3] == "error") {
                Swal.fire({
                    title: data.d[0],
                    text: data.d[1],
                    icon: data.d[2],
                    willClose: function () {
                        $('.scroll-box').fadeIn('fast');
                    }
                });
            }
        }
        
    });
    }else{

    }
    }
function showNotificationforInbound() { 
    var button = document.getElementById("Inboundnotif");
    button.innerHTML = Docnum;
    const $notification = $('#notificationContainerInbound .alert');

    $notification.removeClass('d-none');
    setTimeout(function () {
        $notification.addClass('showAccept');
    }, 500);

    setTimeout(function () {
        $notification.removeClass('showAccept');
        setTimeout(function () {
            $notification.addClass('d-none');
        }, 500);
    }, 30000);
}
function showNotificationforOutbound() {
    var button = document.getElementById("Outboundnotif");
    button.innerHTML = Docnum;
    const $notification = $('#notificationContainerOutbound .alert');
    $notification.removeClass('d-none');
    setTimeout(function () {
        $notification.addClass('showAccept');
    }, 500);

    setTimeout(function () {
        $notification.removeClass('showAccept');
        setTimeout(function () {
            $notification.addClass('d-none');
        }, 500);
    }, 30000);
}

function showNotificationforPending() {
    var button = document.getElementById("Pendingnotif");
    button.textContent = Docnum;
    const $notification = $('#notificationContainerPending .alert');
    $notification.removeClass('d-none');
    setTimeout(function () {
        $notification.addClass('show');
    }, 500);

    setTimeout(function () {
        $notification.removeClass('show');
        setTimeout(function () {
            $notification.addClass('d-none');
        }, 500);
    }, 30000);
}

function showNotificationforNewInbound() {
    var button = document.getElementById("NewInboundnotif");
    button.innerHTML = Docnum;
    const $notification = $('#notificationContainerNewInbound .alert');
    $notification.removeClass('d-none');
    setTimeout(function () {
        $notification.addClass('show');
    }, 500);

    setTimeout(function () {
        $notification.removeClass('show');
        setTimeout(function () {
            $notification.addClass('d-none');
        }, 500);
    }, 30000);
}

function showNotificationforNewOutbound() {
    var button = document.getElementById("NewOutboundnotif");
    button.innerHTML = Docnum;
    const $notification = $('#notificationContainerNewOutbound .alert');
    $notification.removeClass('d-none');
    setTimeout(function () {
    $notification.addClass('show');
    }, 500);
    setTimeout(function () {
        $notification.removeClass('show');
        setTimeout(function () {
            $notification.addClass('d-none');
            }, 500);
    }, 30000);
}

function showNotificationforCompleteUnload() {
    var button = document.getElementById("CompleteUnloadnotif");
    button.innerHTML = Docnum;
    const $notification = $('#notificationContainerCompleteUnload .alert');
    $notification.removeClass('d-none');
    setTimeout(function () {
        $notification.addClass('show');
    }, 500);

    setTimeout(function () {
        $notification.removeClass('show');
        setTimeout(function () {
            $notification.addClass('d-none');
        }, 500);
    }, 30000);
}
function showNotificationforCompletePutAway() {
    var button = document.getElementById("CompletePutAwaynotif");
    button.innerHTML = Docnum;
    const $notification = $('#notificationContainerCompletePutAway .alert');
    $notification.removeClass('d-none');
    setTimeout(function () {
        $notification.addClass('show');
    }, 500);

    setTimeout(function () {
        $notification.removeClass('show');
        setTimeout(function () {
            $notification.addClass('d-none');
        }, 500);
    }, 30000);
}

async function ConstTABLE(Count, Data, TableName, orderbycolumn, orderby) {

    //Initial Table Start
    let tHead = document.createElement("tHead");
    tHead.setAttribute('id', 'DetailHead' + Count);
    let tBody = document.createElement("tBody");// CREATE TABLE BODY .
    tBody.setAttribute('id', 'DetailBody' + Count);
    let hTr = document.createElement("tr"); // CREATE ROW FOR TABLE HEAD .

    let col = []; // define an empty array

    //col.push('Select');
    for (var i = 0; i < Data.length; i++) {

        for (var key in Data[i]) {
            if (col.indexOf(key) === -1) {
                col.push(key);
            }
        }
    }

    // ADD COLUMN HEADER TO ROW OF TABLE HEAD.
    for (var i = 0; i < col.length; i++) {
        let th = document.createElement("th");
        th.setAttribute('class', 'th-lg');
        if (col[i] == 'name') { th.setAttribute('style', 'text-align: center;padding: 0px 2.75rem ;height: 15px;'); }
        else { th.setAttribute('style', 'text-align: center;padding: 0px 2.75em ;height: 15px;'); }

        th.setAttribute('id', col[i]);
        th.scope = "col";
        th.innerHTML = col[i];
        hTr.appendChild(th);

    }
    tHead.appendChild(hTr);

    for (var i = 0; i < Data.length; i++) {
        let bTr = document.createElement("tr"); // CREATE ROW FOR EACH RECORD .
        bTr.setAttribute('style', 'height: 2em !important;');
        for (var j = 0; j < col.length; j++) {

            let td = document.createElement("td");

            if (isNaN(Data[i][col[j]]) || Data[i][col[j]] == null || Data[i][col[j]] == true || Data[i][col[j]] == false) {

                td.innerHTML = Data[i][col[j]];
                td.id = Data[i][col[j]];
                td.innerHTML = Data[i][col[j]];
                td.style.textAlign = "center";
                td.style.padding = "0";
            } else {

                td.innerHTML = Data[i][col[j]];
                td.id = Data[i][col[j]];
                td.style.textAlign = "center";
                td.style.padding = "0";
            }
            bTr.appendChild(td);

        }
        tBody.appendChild(bTr)
    }
    if ($.fn.DataTable.isDataTable('#' + TableName)) {

        $('#' + TableName).DataTable().destroy();
    }
    $('#' + TableName).empty();


    $('#' + TableName).append(tHead);

    $('#' + TableName).append(tBody);
    var x = ($('#' + TableName).parent().height() - 45) + 'px';


    if (Data.length > 0) {
        $('#' + TableName).DataTable({
            "orderCellsTop": false,
            "fixedHeader": true,
            "autoWidth": true,
            "ordering": false,
            "destroy": true,
            "scrollX": true,
            "scrollY": true,
            "scrollCollapse": false,
            "render": false,
            "searching": false,
            "paging": false,
            "info": false,
            "pageLength": 10,
            "columnDefs": [{
                "searchable": false,
                "orderable": false,
                "targets": "_all",
                "autoWidth": true,
            }],
        }).columns.adjust().draw();

    }
    else { }
}