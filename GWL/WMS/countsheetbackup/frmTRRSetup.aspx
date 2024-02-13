<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTRRSetup.aspx.cs" Inherits="GWL.WMS.frmTRRSetup" %>
 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
<title></title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" />
    <%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">

        #form1 {
            height: 580px; 
        } 

        .Entry {
         padding: 20px;
         margin: 10px auto;
         background: #FFF;
        } 

        .pnl-content {
            text-align: right;
        }

        .statusBar a:first-child {
            display: none;
        }
    </style>
    <!--#endregion-->

    <!--#region Javascript-->
    <script>
        var isValid = false;
        var counterror = 0;
        var ctype = "";
        var totalqty = 0.0000;
        var totalqty2 = 0.0000;
        var costbaseqty = 0.0000;
        var originalqty = 0.0000;
        var subsitotbulk = 0;
        var setuptotbulk = 0;
        var linenum = "";

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function calcuinit(s, e) {
            autocalculateALL();
        }

        function calcuinit2(s, e) {
            autocalculateALL2();
        }

        var cnterr = 0;
        var cnterr1 = 0;
        function OnUpdateClick(s, e) {

            setTimeout(function () {
                if (countsheetsetup.GetVisible()) {
                    var indiciesxxx = countsheetsetup.batchEditHelper.GetDataRowIndices();
                    for (var i = 0; i < indiciesxxx.length; i++) {
                        var key = countsheetsetup.GetRowKey(indiciesxxx[i]);
                        if (!countsheetsetup.batchEditHelper.IsDeletedRow(key)) {
                            countsheetsetup.batchEditApi.SetCellValue(indiciesxxx[i], "Width", " ")
                        }
                    }
                }
                if (countsheetsubsi.GetVisible()) {
                    var indiciesyyy = countsheetsubsi.batchEditHelper.GetDataRowIndices();
                    for (var i = 0; i < indiciesyyy.length; i++) {
                        var key = countsheetsubsi.GetRowKey(indiciesyyy[i]);
                        if (!countsheetsubsi.batchEditHelper.IsDeletedRow(key)) {
                            countsheetsubsi.batchEditApi.SetCellValue(indiciesyyy[i], "Shrinkage", 0.00)
                        }
                    }
                }
            }, 500);

            setTimeout(function () {
                countsheetsetup.batchEditApi.EndEdit();
                countsheetsubsi.batchEditApi.EndEdit();

                //cnterr = 0;
                var transtayp = getParameterByName('transtype');

                setuptotbulk = 0;
                if (countsheetsetup.GetVisible()) {
                    var indicies = countsheetsetup.batchEditHelper.GetDataRowIndices();
                    for (var i = 0; i < indicies.length; i++) {
                        if (countsheetsetup.batchEditHelper.IsNewRow(indicies[i])) {
                            countsheetsetup.batchEditApi.ValidateRow(indicies[i]); 
                            var a = parseFloat(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBaseQty"));
                            a = a || 0;
                            if (a != 0) {
                                setuptotbulk++;
                            }
                        }
                        else {
                            var key = countsheetsetup.GetRowKey(indicies[i]);
                            if (countsheetsetup.batchEditHelper.IsDeletedRow(key))
                                console.log("deleted row " + indicies[i]);
                            else {
                                countsheetsetup.batchEditApi.ValidateRow(indicies[i]); 
                                var a = parseFloat(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBaseQty"));
                                a = a || 0;
                                if (a != 0) {
                                    setuptotbulk++;
                                }
                            }
                        }
                    }
                    autocalculateALL();
                    if (cnterr > 0) {
                        alert('Please check all fields!');
                    }
                    else if (totalqty != parseFloat(countsheetheader.batchEditApi.GetCellValue(0, "ReceivedQty"))) {
                        alert('Total RollQty must be equal to the transaction\'s ReceivedQty!');
                    }
                        //else if (setuptotbulk != parseFloat(countsheetheader.batchEditApi.GetCellValue(0, "BulkQty"))) {
                    else if (setuptotbulk != Math.abs(parseFloat(countsheetheader.batchEditApi.GetCellValue(0, "BulkQty")))) {
                        alert('Unequal number of bulk!');
                    }
                    else
                        cp.PerformCallback("Update");
                }

                //cnterr1 = 0;
                subsitotbulk = 0;
                if (countsheetsubsi.GetVisible()) {
                    if (transtayp == "SLSSRN" || transtayp == "INVSAN" || transtayp == "INVJON") {
                        var indicies = countsheetsubsi.batchEditHelper.GetDataRowIndices();
                        for (var i = 0; i < indicies.length; i++) {
                            if (countsheetsubsi.batchEditHelper.IsNewRow(indicies[i])) {
                                countsheetsubsi.batchEditApi.ValidateRow(indicies[i]); 
                                countsheetsubsi.batchEditApi.StartEdit(indicies[i], countsheetsubsi.GetColumnByField("LineNumber").index);
                                var a = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                                a = a || 0;
                                if (a != 0) {
                                    subsitotbulk++;
                                }
                            }
                            else {
                                var key = countsheetsubsi.GetRowKey(indicies[i]);
                                if (countsheetsubsi.batchEditHelper.IsDeletedRow(key))
                                    console.log("deleted row " + indicies[i]);
                                else {
                                    countsheetsubsi.batchEditApi.ValidateRow(indicies[i]); 
                                    countsheetsubsi.batchEditApi.StartEdit(indicies[i], countsheetsubsi.GetColumnByField("LineNumber").index);
                                    var a = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                                    a = a || 0;
                                    if (a != 0) {
                                        subsitotbulk++;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        var indicies = countsheetsubsi.batchEditHelper.GetDataRowIndices();
                        for (var i = 0; i < indicies.length; i++) {
                            if (countsheetsubsi.batchEditHelper.IsNewRow(indicies[i])) { 
                                var a = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                                a = a || 0;
                                if (a != 0) {
                                    subsitotbulk++;
                                }
                            }
                            else {
                                var key = countsheetsubsi.GetRowKey(indicies[i]);
                                if (countsheetsubsi.batchEditHelper.IsDeletedRow(key))
                                    console.log("deleted row " + indicies[i]);
                                else { 
                                    var a = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                                    a = a || 0;
                                    if (a != 0) {
                                        subsitotbulk++;
                                    }
                                }
                            }
                        }
                    }

                    if (cnterr1 > 0) {
                        alert('Please check all fields!');
                    }
                    else if (totalqty2 != parseFloat(countsheetheader.batchEditApi.GetCellValue(0, "TotalRequired"))) {
                        alert('Total qty must be equal to the transaction\'s total Required qty!');
                    }
                        //else if (subsitotbulk != parseFloat(countsheetheader.batchEditApi.GetCellValue(0, "BulkQty"))) {
                    else if (subsitotbulk != Math.abs(parseFloat(countsheetheader.batchEditApi.GetCellValue(0, "BulkQty")))) {
                        alert('Unequal number of bulk!');
                    }
                    else {
                        cp.PerformCallback("Update");
                    }
                }
            }, 500);
        }

        function OnConfirm(s, e) {
            if (e.requestTriggerID === "cp")
                e.cancel = true;
        }

        function gridView_EndCallback(s, e) {
            if (s.cp_message != null) {
                alert(s.cp_message);
                delete (s.cp_message);
                countsheetheader.Refresh();
                //open(location, '_self').close(); 
            }

            if (s.cp_error != null) {
                alert(s.cp_error);
                delete (s.cp_error);
            }
        }

        function OnStartEditing(s, e) {
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];
            index = e.visibleIndex;
        }

        function OnEndEditing(s, e) {
            var cellInfo = e.rowValues[currentColumn.index];
        }

        function gridLookup_CloseUp(s, e) {
            countsheetsetup.batchEditApi.EndEdit();
        }

        function onload() {
            var type = getParameterByName('type');
            var entry = getParameterByName('entry');
            var linenum = getParameterByName('linenumber');
             
            if (entry == "V") { 
                var g = fl.GetItemByName('LG');
                g.SetVisible(g.GetVisible());
            }
        }

        function OnCancelClick(s, e) {
            if (countsheetsetup.GetVisible()) {
                countsheetsetup.CancelEdit();
                autocalculateALL();
            }
            if (countsheetsubsi.GetVisible()) {
                countsheetsubsi.CancelEdit();
                autocalculateALL2();
            }
        }

        var Nanprocessor = function (entry) {
            if (isNaN(entry) == true)
                return 0;
            else
                return entry;
        }

        Number.prototype.format = function (d, w, s, c) {
            var re = '\\d(?=(\\d{' + (w || 3) + '})+' + (d > 0 ? '\\b' : '$') + ')',
                num = this.toFixed(Math.max(0, ~~d));
            return (c ? num.replace(',', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || '.'));
        };

        //////////////////////////////////////////SETUP AUTO-CALCULATE///////////////////////////////////////////////////
        function autocalculateALL(s, e) {
            var allnum = [];

            var indicies = countsheetsetup.batchEditHelper.GetDataRowIndices();
            for (var i = 0; i < indicies.length; i++) {
                if (countsheetsetup.batchEditHelper.IsNewRow(indicies[i])) {
                    var getnum = parseFloat(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBaseQty"));
                    allnum.push(Nanprocessor(getnum));
                }
                else {
                    var key = countsheetsetup.GetRowKey(indicies[i]);
                    if (countsheetsetup.batchEditHelper.IsDeletedRow(key))
                        console.log("deleted row " + indicies[i]);
                    else {
                        var getnum = parseFloat(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBaseQty"));
                        allnum.push(Nanprocessor(getnum));
                    }
                }
            }

            if (allnum.length > 0) {
                var sum = allnum.reduce(function (a, b) { return a + b; });
                BaseQtyTotal.SetText("Total: " + sum.format(4, 6, ',', '.'));
            }
            if (countsheetsetup.GetVisible()) {
                totalqty = sum.format(4, 6, ',', '.');
            } 
        }

        //////////////////////////////////////////SUBSI AUTO-CALCULATE///////////////////////////////////////////////////
        function autocalculateALL2(s, e) {
            var allnum = [];

            var indicies = countsheetsubsi.batchEditHelper.GetDataRowIndices();
            for (var i = 0; i < indicies.length; i++) {
                if (countsheetsubsi.batchEditHelper.IsNewRow(indicies[i])) {
                    var getnum = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                    var getnum2 = Nanprocessor(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "RemainingQty"));
                    allnum.push(Nanprocessor(getnum));
                }
                else {
                    var key = countsheetsubsi.GetRowKey(indicies[i]);
                    if (countsheetsubsi.batchEditHelper.IsDeletedRow(key))
                        console.log("deleted row " + indicies[i]);
                    else {
                        var getnum = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                        var getnum2 = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "RemainingQty"));
                        allnum.push(Nanprocessor(getnum));
                    }
                }
            }

            if (allnum.length > 0) {
                var sum = +allnum.reduce(function (a, b) { return a + b; });
                BaseQtyTotal.SetText("Total: " + sum.format(4, 6, ',', '.'));
            }
            if (countsheetsubsi.GetVisible()) {
                totalqty2 = sum.format(4, 6, ',', '.');
            }
        }


        function Grid_BatchEditRowValidating(s, e) {
            if (countsheetsubsi.GetVisible()) {
                cnterr1 = 0;
                for (var i = 0; i < countsheetsubsi.GetColumnsCount() ; i++) {
                    var column = s.GetColumn(i);
                    var transtayp = getParameterByName('transtype');

                    if (column.fieldName == "RemainingQty") {
                        var cellValidationInfo = e.validationInfo[column.index];
                        if (!cellValidationInfo) continue;
                        var value = cellValidationInfo.value;
                        costbaseqty = value;
                    }

                    if (column.fieldName == "CostBaseQty") {
                        var cellValidationInfo = e.validationInfo[column.index];
                        if (!cellValidationInfo) continue;
                        var value = cellValidationInfo.value;
                        originalqty = value;
                    }
                    if (column.fieldName == "UsedQty" && transtayp != "SLSSRN" && transtayp != "INVSAN" && transtayp != "INVJON") {
                        var cellValidationInfo = e.validationInfo[column.index];
                        if (!cellValidationInfo) continue;
                        var value = cellValidationInfo.value;

                        if (value > costbaseqty) {
                            cellValidationInfo.isValid = false;
                            ValidityState = false;
                            cellValidationInfo.errorText = "Required Qty should not be greater than Remaining Qty!";
                            isValid = false;
                            cnterr1++;
                        }
                    }
                    //if (column.fieldName == "UsedQty" && (transtayp == "SLSSRN" || transtayp == "INVSAN" || transtayp == "INVJON")) {
                    //    var cellValidationInfo = e.validationInfo[column.index];
                    //    if (!cellValidationInfo) continue;
                    //    var value = cellValidationInfo.value;

                    //    if (value > (originalqty - costbaseqty)) {
                    //        cellValidationInfo.isValid = false;
                    //        ValidityState = false;
                    //        cellValidationInfo.errorText = "Remaining qty plus Required qty to be returned must not be greater than the RR Qty!";
                    //        isValid = false;
                    //        cnterr1++;
                    //    }
                    //}
                }
            }
            if (countsheetsetup.GetVisible()) {
                cnterr = 0;
                for (var i = 0; i < countsheetsetup.GetColumnsCount() ; i++) {
                    var column = s.GetColumn(i);

                    if (column.fieldName == "LineNumber") {
                        var cellValidationInfo = e.validationInfo[column.index];
                        if (!cellValidationInfo) continue;
                        var value = cellValidationInfo.value;
                        linenum = value;

                        var indicies = countsheetsetup.batchEditHelper.GetDataRowIndices();
                        for (var h = 0; h < indicies.length; h++) {
                            var getval = countsheetsetup.batchEditApi.GetCellValue(indicies[h], "LineNumber");

                            if ((e.visibleIndex != indicies[h]) && (linenum == getval)) {
                                cellValidationInfo.isValid = false;
                                ValidityState = false;
                                cellValidationInfo.errorText = "Roll ID must be unique!";

                                isValid = false;
                                cnterr++;
                            }
                        }
                    }
                }
            }
        }

    </script>
    <!--#endregion-->
</head>
<body style="height: 500px" onload="onload()">
    <form id="form1" runat="server" class="Entry">
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="641px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" ClientInstanceName="fl" runat="server" DataSourceID="" Height="565px" Width="810px" Style="margin-left: -3px" ColCount="2">
                        <Items>
                            <dx:LayoutItem Caption="Transaction Type">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtTransType" runat="server" Width="170px" ReadOnly="true">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Doc No.">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">        
                                        <table>
                                            <tr><td>
                                                <dx:ASPxTextBox ID="txtDocNumber" runat="server" Width="120px" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                                </td><td>
                                                <dx:ASPxLabel runat="server" Width="140px">
                                                </dx:ASPxLabel> 
                                                </td><td>
                                                <dx:ASPxLabel runat="server" Text="Total: " Width="100px" ClientInstanceName="BaseQtyTotal">
                                                </dx:ASPxLabel> 
                                            </td></tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutGroup Caption="Information" ColSpan="2" Name="Inf" Width="710px">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="header" runat="server" DataSourceID="countsheetheader" OnCommandButtonInitialize="gv_CommandButtonInitialize" 
                                                    ClientInstanceName="countsheetheader" KeyFieldName="ItemCode" Width="790px" OnCellEditorInitialize="headerline_CellEditorInitialize">
                                                    <%--<Settings HorizontalScrollBarMode="Visible" />--%>
                                                    <SettingsEditing Mode="Batch"/>
                                                    <Styles>
                                                        <StatusBar CssClass="statusBar" />
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Caption="Details" ColSpan="2" Width="710px">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <div id="loadingcont">
                                                <dx:ASPxGridView ID="countsheetsetup" runat="server" AutoGenerateColumns="False" ClientInstanceName="countsheetsetup" DataSourceID="countsheetdetailsetup" 
                                                    KeyFieldName="TransDoc;TransLine;LineNumber" OnCellEditorInitialize="countsheetsetup_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" 
                                                    ClientVisible="False" Width="790px">
                                                    <ClientSideEvents Init="calcuinit" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" BatchEditRowValidating="Grid_BatchEditRowValidating"/>
                                                    <SettingsEditing Mode="Batch">
                                                    </SettingsEditing>
                                                    <Settings ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Visible" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="TransType" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransDoc" ShowInCustomizationForm="True" Visible="true" UnboundType="String" VisibleIndex="1" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransLine" ShowInCustomizationForm="True" UnboundType="String" Visible="true" VisibleIndex="2" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" Caption="RollID" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="3" Width="100px" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber1" Caption="RollID" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="4" Width="0px" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="OriginalBaseQty" Caption="RollQty" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px" ReadOnly="false" >
                                                            <PropertiesSpinEdit NullDisplayText="0.00" NullText="0.00" DisplayFormatString="{0:N4}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false" Increment="0" >
                                                                <ClientSideEvents ValueChanged="function(){setTimeout(function(){autocalculateALL();},500);}"/>
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Width" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="100" Width="1px" ReadOnly="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Shade" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="15" Width="0px" ReadOnly="false" >
                                                            <PropertiesTextEdit DisplayFormatString="{0:N2}" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="Shrinkage" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="20" Width="0px" ReadOnly="false">
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataDateColumn FieldName="ExpirationDate" Caption="ExpDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="10" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="MfgDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="12" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataTextColumn FieldName="BatchNumber" Caption="BatchNo" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="14" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LotNo" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="16" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="AddedBy" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="21" Width="150px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="AddedDate" ShowInCustomizationForm="True" PropertiesTextEdit-ConvertEmptyStringToNull="true" UnboundType="String" VisibleIndex="25" Width="150px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LastEditedBy" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="30" Width="150px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LastEditedDate" ShowInCustomizationForm="True" PropertiesTextEdit-ConvertEmptyStringToNull="true" UnboundType="String" VisibleIndex="35" Width="150px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                    <SettingsBehavior AllowSort ="false" />
                                                </dx:ASPxGridView>
                                                
                                                <dx:ASPxGridView ID="countsheetsubsi" runat="server" AutoGenerateColumns="False" ClientInstanceName="countsheetsubsi" DataSourceID="countsheetdetailsubsi" KeyFieldName="CostTransDoc;CostTransLine;CostLineNumber;CostTransType" 
                                                    OnCellEditorInitialize="countsheetsubsi_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" ClientVisible="False" Width="790px">
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="calcuinit2" BatchEditRowValidating="Grid_BatchEditRowValidating"/>
                                                    <SettingsEditing Mode="Batch">
                                                    </SettingsEditing>
                                                    <Settings ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Visible" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="TransType" ShowInCustomizationForm="True" Visible="True" VisibleIndex="0" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransDoc" ShowInCustomizationForm="True" Visible="True" VisibleIndex="2" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransLine" ShowInCustomizationForm="True" Visible="True" VisibleIndex="4" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" ShowInCustomizationForm="True" Visible="True" VisibleIndex="6" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" ShowInCustomizationForm="True" Visible="True" VisibleIndex="8" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" ShowInCustomizationForm="True" Visible="True" VisibleIndex="10" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" ShowInCustomizationForm="True" Visible="True" VisibleIndex="12" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" ShowInCustomizationForm="True" Visible="True" VisibleIndex="14" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CostTransType" ShowInCustomizationForm="True" Visible="True" VisibleIndex="16" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CostTransDoc" ShowInCustomizationForm="True" Visible="True" VisibleIndex="18" Width="80px" UnboundType="String" Caption="TransDoc">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CostTransLine" ShowInCustomizationForm="True" Visible="True" VisibleIndex="20" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CostLineNumber" ShowInCustomizationForm="True" Visible="True" VisibleIndex="22" Width="80px" UnboundType="String" Caption="LineNumber">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="CostBaseQty" ShowInCustomizationForm="True" VisibleIndex="24" Width="80px" UnboundType="Decimal" Caption="RRQty">
                                                            <PropertiesSpinEdit NullDisplayText="0.00" NullText="0.00" DisplayFormatString="{0:N4}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false" Increment="0">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="RemainingQty" ShowInCustomizationForm="True" VisibleIndex="26" Width="80px" UnboundType="Decimal" Caption="RemainingQty" >
                                                            <PropertiesSpinEdit NullDisplayText="0.00" NullText="0.00" DisplayFormatString="{0:N4}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false" Increment="0">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="UsedQty" ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="28" Width="80px" Caption="RequiredQty" >
                                                            <PropertiesSpinEdit NullDisplayText="0.00" NullText="0.00" DisplayFormatString="{0:N4}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false" Increment="0" >
                                                                <ClientSideEvents ValueChanged="function(){setTimeout(function(){autocalculateALL2();},500);}"/>
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Width" ShowInCustomizationForm="True" VisibleIndex="100" Width="0px" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Shade" ShowInCustomizationForm="True" VisibleIndex="32" Width="0px" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="Shrinkage" ShowInCustomizationForm="True" VisibleIndex="34" Width="1px" UnboundType="String">
                                                            <PropertiesSpinEdit NullDisplayText="0.00" DisplayFormatString="{0:N2}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false" Increment="0">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataDateColumn FieldName="ExpirationDate" Caption="ExpDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="30" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="MfgDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="31" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataTextColumn FieldName="BatchNumber" Caption="BatchNo" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="32" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LotNo" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="33" Width="100px" ReadOnly="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Warehouse" ShowInCustomizationForm="True" VisibleIndex="36" Width="80px" UnboundType="String">
                                                            <PropertiesTextEdit NullDisplayText="N/A">
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                        <%--<dx:GridViewDataDateColumn FieldName="RRDate" ShowInCustomizationForm="True" Visible="True" VisibleIndex="38" Width="0px">
                                                        </dx:GridViewDataDateColumn>--%>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                    <SettingsBehavior AllowSort ="false" />
                                                </dx:ASPxGridView>
                                                </div>
                                                <%--<dx:ASPxLabel runat="server" Text="Total: " Width="100px" ClientInstanceName="BaseQtyTotal">
                                                </dx:ASPxLabel>--%>
                                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel Changes" ClientInstanceName="btnCancel" CausesValidation="false" AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="OnCancelClick" />
                                                </dx:ASPxButton>
                                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" ExportedRowType="All" />
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:ASPxFormLayout>
                    <dx:ASPxPanel ID="BottomPanel" runat="server" FixedPosition="WindowBottom" BackColor="#FFFFFF" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <div class="pnl-content">
                                    <dx:ASPxButton ID="updateBtn" runat="server" Text="Update" AutoPostBack="False" CssClass="btn" ClientInstanceName="btn"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="OnUpdateClick" />
                                    </dx:ASPxButton>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </form>

    <!--#region Region Datasource-->
    <asp:SqlDataSource ID="countsheetheader" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:ObjectDataSource ID="countsheetdetailsubsi" runat="server" DataObjectTypeName="Entity.TRRSubsi" SelectMethod="getdetail" TypeName="Entity.TRRSubsi" UpdateMethod="UpdateTRRSubsi">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:QueryStringParameter Name="LineNumber" QueryStringField="linenumber" Type="String" />
            <asp:QueryStringParameter Name="TransType" QueryStringField="transtype" Type="String" />
            <asp:QueryStringParameter Name="refdocnum" QueryStringField="refdocnum" Type="String" />
            <asp:QueryStringParameter Name="ItemCode" QueryStringField="itemcode" Type="String" />
            <asp:QueryStringParameter Name="ColorCode" QueryStringField="colorcode" Type="String" />
            <asp:QueryStringParameter Name="ClassCode" QueryStringField="classcode" Type="String" />
            <asp:QueryStringParameter Name="SizeCode" QueryStringField="sizecode" Type="String" />
            <asp:QueryStringParameter Name="Warehouse" QueryStringField="warehouse" Type="String" />
            <asp:QueryStringParameter Name="ExpDate" QueryStringField="expdate" Type="String" />
            <asp:QueryStringParameter Name="MfgDate" QueryStringField="mfgdate" Type="String" />
            <asp:QueryStringParameter Name="BatchNo" QueryStringField="batchno" Type="String" />
            <asp:QueryStringParameter Name="LotNo" QueryStringField="lotno" Type="String" />
            <asp:QueryStringParameter Name="BulkQty" QueryStringField="bulkqty" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="countsheetdetailsetup" runat="server" DataObjectTypeName="Entity.TRRSetup" SelectMethod="getdetail" TypeName="Entity.TRRSetup" UpdateMethod="UpdateTRRSetup">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:QueryStringParameter Name="LineNumber" QueryStringField="linenumber" Type="String" />
            <asp:QueryStringParameter Name="TransType" QueryStringField="transtype" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</body>
</html>
