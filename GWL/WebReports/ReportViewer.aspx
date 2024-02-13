﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="GWL.WebReports.ReportViewer" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
 <title runat="server" id="txtReport"></title>
   <%--  <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.4.js" type="text/javascript"></sgcript>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.11.4/jquery-ui.js" type="text/javascript"></script>

    <script src="http://ajax.aspnetcdn.com/ajax/globalize/0.1.1/globalize.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/globalize/0.1.1/cultures/globalize.cultures.js" type="text/javascript"></script>

    <script src="http://ajax.aspnetcdn.com/ajax/knockout/knockout-3.4.0.js" type="text/javascript"></script>

    <link href="js/jquery-ui-1.11.4/jquery-ui.css" type="text/css" rel="Stylesheet" />--%>

     <style>
        .custom-multivalue {
            min-height: 26px;
        }

        .dx-list-select-all { display: none; }
    </style>
    <script type="text/html" id="custom-dx-checkbox">
        <div data-bind="dxCheckBox: {
            value: value
        }"></div>
    </script>
    <script type= "text/C#" id="custom-dx-date">
     <div data-bind="dxDateBox: { value: value, showSpinButtons: true, 
        applyValueMode: 'instantly', closeOnValueChange: true, width:'10px', 
        format: 'date', dxValidator: {validationRules: validationRules || [] }}"></div>       
    </script>
    <script type="text/html" id="custom-dxrd-combobox">
        <div data-bind="dxSelectBox: { dataSource: { store: values(), paginate: true, pageSize: 200 }, value: value, valueExpr: 'value', searchEnabled: true, displayExpr: 'displayValue', displayCustomValue: true, disabled: disabled }, dxValidator: { validationRules: $data.validationRules || [] }"></div>
    </script>

    <script type="text/html" id="custom-dxrd-multivalue">
        <!-- ko with: value -->
        <div class="custom-multivalue" data-bind="dxTagBox: {
    dataSource: { pageSize: 20, paginate: true, store: displayItems },
    height: 'auto',
    values: ko.pureComputed(function () {
        if (isPending($data))
            return [];
        return displayItems.filter(function (item) { return item.selected(); });
    }),
    showSelectionControls: true,
    onFocusOut: updateValue,
    hideSelectedItems: true,
    displayExpr: 'displayValue',
    applyValueMode: 'instantly',
    searchEnabled: true,
    onValueChanged: function (e) {
        if (isPending($data))
            return;
        $data['pending'](true);
        try {
            customValueChanged(e);
            updateValue();
        } finally {
            $data['pending'](false);
        }
    }
}"></div>
        <!-- /ko -->
    </script>

    <script>
        var module = getParameterByName("transtype");
        var id = getParameterByName("userid");
        var entry = getParameterByName("param");

        console.log(module,id,entry)

        $(document).ready(function () {
            PerfStart(module, entry+'-i', id);
        });

        function isPending(data) {
            !ko.isObservable(data['pending']) && (data['pending'] = ko.observable(false));
            return data['pending']();
        }

        var getArraysDifference = function (first, second) {
            return $.grep(first, function (element) { return $.inArray(element, second) < 0 });
        };


        function customValueChanged(e) {
            var isItemAdded = e.values.length > e.previousValues.length,
            difference = isItemAdded ? getArraysDifference(e.values, e.previousValues) : getArraysDifference(e.previousValues, e.values);
            difference.forEach(function (item) { item.selected(isItemAdded); });
        }

        var reprint = getParameterByName('reprinted');

        function CustomizeParameterEditors(s, e) {
            if (e.info.editor.header === 'dxrd-multivalue') {
                e.info.editor.header = 'custom-dxrd-multivalue';
            }

            for (var rekt in e.info.editor) {
                if (e.info.editor.header === 'custom-dxrd-multivalue') {
                    (window['multiValueParameterNames'] || (window['multiValueParameterNames'] = [])).push(e.info.propertyName);
                }
            }

            if (e.info.editor.header === 'dx-combobox') {
                e.info.editor.header = 'custom-dxrd-combobox';
            }
            if (e.info.editor.header === 'dx-date') {
                e.info.editor.header = 'custom-dx-date';
            }
            if (e.info.editor.header === 'dx-boolean-select') {
                e.info.editor.header = 'custom-dx-checkbox';
            }
            //var name = e.info.displayName;
            //if (name.includes("Date") && e.info.editor.header == 'dx-text') {
            //    e.info.editor.header = 'custom-dx-date';
            //    console.log(name)
            //}

            //console.log(e.info)
        }

        function customizeMenu(s, e) {
            var actionPrint = e.Actions.filter(function (action) { return action.text === "Print"; })[0];
            var actionPrint2 = e.Actions.filter(function (action) { return action.text === "Print Page"; })[0];
            actionPrint2.visible = false;

            e.Actions.push({
                text: "New Tab",
                imageClassName: "dxrd-image-open",
                disabled: ko.observable(false),
                visible: true,
                //index: 8,
                clickAction: function () {
                    window.open(window.location.href, '_blank');
                }
            });

            var defaultPrintClickAction = actionPrint.clickAction;
            actionPrint.clickAction = function () {
                console.log(reprint);
                if (reprint == "True") {
                    var r = confirm('Are you sure that you want to proceed with printing? ' +
                        'This will be marked as Re-Printed after this process');
                    if (r == true) {
                        defaultPrintClickAction();
                        cp.PerformCallback();
                        window.close();
                    }
                }
                else {
                    defaultPrintClickAction();
                }
            }
        }

        function Init(s, e) {
            s.previewModel.reportPreview.zoom(1);
            s.previewModel.reportPreview.showMultipagePreview(1);
            //console.log(s);
            ResizeReport();
            var previewModel = s.GetPreviewModel();
            if (!previewModel || !previewModel.parametersModel) {
                return;
            }
            (window['multiValueParameterNames'] || []).forEach(function (parameterName) {
                var parameter = null;
                if (parameterName && (parameter = previewModel.parametersModel[parameterName])) {
                    var removeSelectAllItem = function (newValue) {
                        newValue.displayItems && (newValue.displayItems.length > 0) && newValue.displayItems.splice(0, 1);
                    }
                    parameter.subscribe(removeSelectAllItem);
                    removeSelectAllItem(parameter());
                    //console.log('here')
                }
            });          

            var previewModel = s.GetPreviewModel();
            var startTime;
            var elapsedTime;
            var subscription = previewModel.reportPreview.progressBar.visible.subscribe(function (newValue) {
                if (newValue) {
                    startTime = Date.now();
                }
                else {
                    elapsedTime = Date.now() - startTime;
                    perfSend2((elapsedTime / 1000).toFixed(3));
                }
            });
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        var module2 = getParameterByName("val").split('~');

        function perfSend2(loadTime) {
            console.log(loadTime, module)
             $.ajax({
                type: "POST",
                data: JSON.stringify({ ModuleID: module2[1], Entry: 'PView-load', Pkey: null, Interval: loadTime, UserId: id, }),
                url: "../PerformSender.aspx/perfsend",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log('sent');
                }
            });
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxWebDocumentViewer ID="GWLReportViewer" runat="server" Height="300px" ClientInstanceName="report">
            <ClientSideEvents Init="Init" CustomizeParameterEditors="CustomizeParameterEditors" 
                 CustomizeMenuActions="customizeMenu"/>
        </dx:ASPxWebDocumentViewer>
        <dx:ASPxCallback ID="cp" runat="server" OnCallback="cp_Callback"></dx:ASPxCallback>
        <script type="text/javascript">
            function ResizeReport() {
                report.SetHeight(document.documentElement.clientHeight - 45);
            }
            window.onresize = function resize() { ResizeReport(); }
        </script>

    </div>
    </form>
</body>
</html>
