<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="GWL.WebReports.ReportViewer" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <title></title>
    <script type="text/html" id="custom-dx-date">
    <div data-bind="dxDateBox: { value: value.extend({ throttle: 500 }), closeOnValueChange: true, format: 'date', disabled: disabled }, dxValidator: { validationRules: validationRules || [] }"></div>
</script>
    <script type="text/javascript">
        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        var reprint = getParameterByName('reprinted');

        function removeExportItemsByText(items, text) {
            for (var i = 0; i < items.length; i++) {
                if (items[i].text === text) {
                    items.splice(i, 1);
                    return;
                }
            }
        }

        function Viewer_CustomizeParameterEditors(s, e) {
            var param = e.parameter.name;
            if (param.indexOf("Date") > -1 && e.parameter.type != 'System.Int32') {
                console.log(e.parameter);
                e.info.editor.header = 'custom-dx-date';
            }
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
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxWebDocumentViewer ID="GWLReportViewer" runat="server" Height="300px" ClientInstanceName="report">
            <ClientSideEvents Init="function(s, e) {
                                s.previewModel.reportPreview.zoom(1);
                                s.previewModel.reportPreview.showMultipagePreview(1);
                                //console.log(s);
                            }" CustomizeParameterEditors="Viewer_CustomizeParameterEditors" 
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
