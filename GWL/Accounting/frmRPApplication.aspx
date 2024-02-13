﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRPApplication.aspx.cs" Inherits="GWL.frmRPApplication" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AR/AP Application</title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <script src="../js/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
        height: 710px; /*Change this whenever needed*/
        }

        .Entry {
        padding: 20px;
        margin: 10px auto;
        background: #FFF;
        }

        /*.dxeButtonEditSys input,
        .dxeTextBoxSys input{
            text-transform:uppercase;
        }*/

         .pnl-content
        {
            text-align: right;
        }


    </style>
    <!--#endregion-->
    
    <!--#region Region Javascript-->
    <script>
        var isValid = true;
        var counterror = 0;

        function OnValidation(s, e) { //Validation function for header controls (Set this for each header controls)
            if (s.GetText() == "" || e.value == "" || e.value == null) {
                counterror++;
                isValid = false
                console.log(s);
                console.log(e);
            }
            else {
                isValid = true;
            }
        }

        function OnInitTrans(s, e) {

            var BizPartnerCode = aglBizPartner.GetText();
            factbox.SetContentUrl('../FactBox/fbBizPartner.aspx?BizPartnerCode=' + BizPartnerCode);
            AdjustSize();
        }

        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }

        function AdjustSize() {
            var width = Math.max(0, document.documentElement.clientWidth);
            gvApplication.SetWidth(width - 120);
            gvAdjustment.SetWidth(width - 120);
            gvJournal.SetWidth(width - 120);
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function OnUpdateClick(s, e) { //Add/Edit/Close button function
            var btnmode = btn.GetText(); //gets text of button
            if (btnmode == "Delete") {
                cp.PerformCallback("Delete");
            }
            console.log(isValid + ' ' + counterror);

            var indicies = gvApplication.batchEditHelper.GetDataItemVisibleIndices();
            var cntdetails = indicies.length;

            if (isValid && counterror < 1 || btnmode == "Close") { //check if there's no error then proceed to callback
                //Sends request to server side
                if (btnmode == "Add") {
                    if (cntdetails == 0) {
                        cp.PerformCallback("AddZeroDetail");
                    }
                    else {
                        cp.PerformCallback("Add");
                    }
                }
                else if (btnmode == "Update") {
                    if (cntdetails == 0) {
                        cp.PerformCallback("UpdateZeroDetail");
                    }
                    else {
                        cp.PerformCallback("Update");
                    }
                }
                else if (btnmode == "Close") {
                    cp.PerformCallback("Close");
                }
            }
            else {
                counterror = 0;
                alert('Please check all the fields!');
                console.log(counterror);
            }


        }

        function OnConfirm(s, e) {//function upon saving entry
            if (e.requestTriggerID === "cp")//disables confirmation message upon saving.
                e.cancel = true;
        }

        var vatrate = 0;
        var vatdetail1 = 0;

        function gridView_EndCallback(s, e) {//End callback function if (s.cp_success) {
            if (s.cp_success) {
                alert(s.cp_message);
                delete (s.cp_success);//deletes cache variables' data
                delete (s.cp_message);
                if (s.cp_forceclose) {
                    delete (s.cp_forceclose);
                    window.close();
                }

            }

            if (s.cp_close) {
                gvApplication.CancelEdit();
                if (s.cp_message != null) {
                    alert(s.cp_message);
                    delete (s.cp_message);
                }
                if (s.cp_valmsg != null) {
                    alert(s.cp_valmsg);
                    delete (s.cp_valmsg);
                }
                if (glcheck.GetChecked()) {
                    delete (cp_close);
                    window.location.reload();
                }
                else {
                    delete (cp_close);
                    window.close();//close window if callback successful
                }
            }

            if (s.cp_delete) {
                delete (cp_delete);
                DeleteControl.Show();
            }
            if (s.cp_generated) {
                delete (s.cp_generated);
                autocalculate();
            }

        }

        var index;
        var closing;
        var account; //variable required for lookup
        var valchange = false;
        var currentColumn = null;
        var isSetTextRequired = false;
        var linecount = 1;

        function OnStartEditing(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];
            account = s.batchEditApi.GetCellValue(e.visibleIndex, "AccountCode");

            var entry = getParameterByName('entry');

            if (entry == "V") {
                e.cancel = true; //this will made the gridview readonly
            }

            if (entry != "V") {
                if (e.focusedColumn.fieldName === "DiscountType") { //Check the column name
                    DiscountType.GetInputElement().value = cellInfo.value; //Gets the column value
                    isSetTextRequired = true;
                    index = e.visibleIndex;
                    closing = true;
                }

                if (e.focusedColumn.fieldName === "DiscountType") {
                    DiscountType.GetInputElement().value = cellInfo.value;
                }
            }
        }

        function OnStartEditing_gvReceipts(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];
            account = s.batchEditApi.GetCellValue(e.visibleIndex, "AccountCode");

            var entry = getParameterByName('entry');

            if (entry == "V") {
                e.cancel = true; //this will made the gridview readonly
            }

            var fordeposit = chkIsForDeposit.GetValue();

            if (fordeposit == true) {
                e.cancel = true;
            }
            else {
                e.cancel = false;
            }
        }

        function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
            var cellInfo = e.rowValues[currentColumn.index];

            var entry = getParameterByName('entry');

            if (currentColumn.fieldName === "Bank") {
                cellInfo.value = colBank.GetValue();
                cellInfo.text = colBank.GetText();
            }
            if (currentColumn.fieldName === "Branch") {
                cellInfo.value = colBranch.GetValue();
                cellInfo.text = colBranch.GetText();
            }
            if (currentColumn.fieldName === "ProfitCenter") {
                cellInfo.value = colProfitCenter.GetValue();
                cellInfo.text = colProfitCenter.GetText();
            }
            if (currentColumn.fieldName === "AccountCode") {
                cellInfo.value = colAccountCode.GetValue();
                cellInfo.text = colAccountCode.GetText();
            }
            if (currentColumn.fieldName === "BizPartnerCode") {
                cellInfo.value = colBizPartner.GetValue();
                cellInfo.text = colBizPartner.GetText();
            }
            if (currentColumn.fieldName === "CostCenter") {
                cellInfo.value = colCostCenter.GetValue();
                cellInfo.text = colCostCenter.GetText();
            }
            if (currentColumn.fieldName === "BankAccountCode") {
                cellInfo.value = colBankAccount.GetValue();
                cellInfo.text = colBankAccount.GetText();
            }
            if (currentColumn.fieldName === "SubsidiaryCode") {
                cellInfo.value = colSubsiCode.GetValue();
                cellInfo.text = colSubsiCode.GetText();
            }


            if (valchange) {

                valchange = false;
                closing = false;
                for (var i = 0; i < s.GetColumnsCount() ; i++) {
                    var column = s.GetColumn(i);
                    if (column.visible == false || column.fieldName == undefined)
                        continue;
                    ProcessCells(0, e, column, s);
                }
            }
        }

        var val;
        var temp;
        var check_rate = false;
        function ProcessCells(selectedIndex, e, column, s) {
            if (val == null) {
                val = ";;;;";
                temp = val.split(';');
            }
            if (temp[0] == null) {
                temp[0] = "";
            }
            if (temp[1] == null) {
                temp[1] = "";
            }
            if (temp[2] == null) {
                temp[2] = "";
            }
            if (temp[3] == null) {
                temp[3] = "";
            }
            if (temp[4] == null) {
                temp[4] = "";
            }
            if (temp[5] == null) {
                temp[5] = "";
            }
            if (temp[6] == null) {
                temp[6] = "";
            }
            if (temp[7] == null) {
                temp[7] = "";
            }
            if (temp[8] == null) {
                temp[8] = "";
            }
            if (temp[9] == null) {
                temp[9] = "";
            }

            if (selectedIndex == 0) {
                if (column.fieldName == "Rate") {
                    s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[0]);
                }
            }
        }

        function GridEnd(s, e) {

        }


        function lookup(s, e) {
            if (isSetTextRequired) {//Sets the text during lookup for item code
                s.SetText(s.GetInputElement().value);
                isSetTextRequired = false;
            }
        }

        //var preventEndEditOnLostFocus = false;
        function gridLookup_KeyDown(s, e) { //Allows tabbing between gridlookup on details
            isSetTextRequired = false;
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode !== ASPxKey.Tab) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (gvApplication.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }
            if (gvAdjustment.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }

        }

        function gvAdjustment_KeyDown(s, e) { //Allows tabbing between gridlookup on details
            isSetTextRequired = false;
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode !== ASPxKey.Tab) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (gvAdjustment.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }

        }

        function gvApplication_KeyDown(s, e) { //Allows tabbing between gridlookup on details
            isSetTextRequired = false;
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode !== ASPxKey.Tab) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (gvApplication.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }

        }

        function gvApplication_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode == ASPxKey.Enter) {
                gvApplication.batchEditApi.EndEdit();
            }
        }

        function gvAdjustment_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode == ASPxKey.Enter) {
                gvAdjustment.batchEditApi.EndEdit();
            }
        }


        function gvApplication_CloseUp(s, e) {
            gvApplication.batchEditApi.EndEdit();
        }

        function gvAdjustment_CloseUp(s, e) {
            gvAdjustment.batchEditApi.EndEdit();
        }

        function OnCustomClick(s, e) {
            if (e.buttonID == "Details") {
                var itemcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode");
                var colorcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ColorCode");
                var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
                var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
                var unitbase = s.batchEditApi.GetCellValue(e.visibleIndex, "Unit");
                var fulldesc = s.batchEditApi.GetCellValue(e.visibleIndex, "FullDesc");
                //factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode
                //+ '&colorcode=' + colorcode + '&classcode=' + classcode + '&sizecode=' + sizecode + '&unit=' + unitbase + '&fulldesc=' + fulldesc);
            }

            if (e.buttonID == "ApplicationDelete") {
                gvApplication.DeleteRow(e.visibleIndex);
                autocalculate();
            }

            if (e.buttonID == "AdjustmentDelete") {
                gvAdjustment.DeleteRow(e.visibleIndex);
                autocalculate();
            }

            if (e.buttonID == "ViewTransaction") {

                var transtype = s.batchEditApi.GetCellValue(e.visibleIndex, "TransType");
                var docnumber = s.batchEditApi.GetCellValue(e.visibleIndex, "DocNumber");
                var commandtring = s.batchEditApi.GetCellValue(e.visibleIndex, "CommandString");

                window.open(commandtring + '?entry=V&transtype=' + transtype + '&parameters=&iswithdetail=true&docnumber=' + docnumber, '_blank', "", false);
                console.log('ViewTransaction')
            }
            if (e.buttonID == "ViewReferenceTransaction") {

                var rtranstype = s.batchEditApi.GetCellValue(e.visibleIndex, "RTransType");
                var rdocnumber = s.batchEditApi.GetCellValue(e.visibleIndex, "REFDocNumber");
                var rcommandtring = s.batchEditApi.GetCellValue(e.visibleIndex, "RCommandString");
                window.open(rcommandtring + '?entry=V&transtype=' + rtranstype + '&parameters=&iswithdetail=true&docnumber=' + rdocnumber, '_blank');
                console.log('ViewTransaction')
            }
        }


        function endcp(s, e) {
            var endg = s.GetGridView().cp_endgl1;
            if (endg == true) {
                console.log(endg);
                sup_cp_Callback.PerformCallback(aglBizPartner.GetValue().toString());
                e.processOnServer = false;
                endg = null;
            }
        }

        Number.prototype.format = function (d, w, s, c) {
            var re = '\\d(?=(\\d{' + (w || 3) + '})+' + (d > 0 ? '\\b' : '$') + ')',
                num = this.toFixed(Math.max(0, ~~d));

            return (c ? num.replace(',', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || '.'));
        };

        function autocalculate(s, e) {

            OnInitTrans();
            console.log('auto')

            //genrev

            //for header
            var HCash = 0.00;
            var HCredit = 0.00;
            var HCheck = 0.00;
            var HDue = 0.00;
            var HApplied = 0.00;
            var HAdjustment = 0.00;

            //for detail
            var DCash = 0.00;
            var DCredit = 0.00;
            var DCheck = 0.00;
            var DDue = 0.00;
            var DApplied = 0.00;
            var DAdjustment = 0.00;

            var arrTrans = [];
            var cntr = 0;
            var holder = 0;
            var txt = "";

            var arrLoan = [];
            var cntr1 = 0;
            var holder1 = 0;
            var txt1 = "";

            setTimeout(function () {

                var iApplication = gvApplication.batchEditHelper.GetDataItemVisibleIndices();
                var iGrid = gvApplication.batchEditHelper.GetDataItemVisibleIndices();
                var iAdjustment = gvAdjustment.batchEditHelper.GetDataItemVisibleIndices();

                //Application
                for (var i = 0; i < iApplication.length; i++) {
                    if (gvApplication.batchEditHelper.IsNewItem(iApplication[i])) {

                        DDue = gvApplication.batchEditApi.GetCellValue(iApplication[i], "TransAmountDue");
                        DApplied = gvApplication.batchEditApi.GetCellValue(iApplication[i], "TransAmountApplied");

                        HDue += DDue;
                        HApplied += DApplied;
                    }
                    else {
                        var key = gvApplication.GetRowKey(iApplication[i]);
                        if (gvApplication.batchEditHelper.IsDeletedItem(key)) {
                            console.log("deleted row " + iApplication[i]);
                        }
                        else {
                            DDue = gvApplication.batchEditApi.GetCellValue(iApplication[i], "TransAmountDue");
                            DApplied = gvApplication.batchEditApi.GetCellValue(iApplication[i], "TransAmountApplied");

                            HDue += DDue;
                            HApplied += DApplied;
                        }
                    }
                }

                //FOR RefTrans
                for (var x = 0; x <= iGrid.length; x++) {
                    if (gvApplication.batchEditHelper.IsNewItem(iGrid[x])) {
                        for (var y = 0; y <= iGrid.length; y++) {
                            if (gvApplication.batchEditApi.GetCellValue(iGrid[x], "RecordID") + '-' + gvApplication.batchEditApi.GetCellValue(iGrid[x], "TransDoc") == arrTrans[y]) {
                                cntr++;
                            }
                        }
                        if (cntr == 0) {
                            holder++;
                            arrTrans[holder] = gvApplication.batchEditApi.GetCellValue(iGrid[x], "RecordID") + '-' + gvApplication.batchEditApi.GetCellValue(iGrid[x], "TransDoc");
                        }
                        else cntr = 0;
                    }
                    else {
                        var key = gvApplication.GetRowKey(iGrid[x]);
                        if (gvApplication.batchEditHelper.IsDeletedItem(key))
                            console.log("deleted row " + iGrid[x]);
                        else {
                            for (var y = 0; y <= iGrid.length; y++) {
                                if (gvApplication.batchEditApi.GetCellValue(iGrid[x], "RecordID") + '-' + gvApplication.batchEditApi.GetCellValue(iGrid[x], "TransDoc") == arrTrans[y]) {
                                    cntr++;
                                }
                            }
                            if (cntr == 0) {
                                holder++;
                                if (gvApplication.batchEditApi.GetCellValue(iGrid[x], "RecordID") != null && gvApplication.batchEditApi.GetCellValue(iGrid[x], "TransDoc") != null) {
                                    arrTrans[holder] = gvApplication.batchEditApi.GetCellValue(iGrid[x], "RecordID") + '-' + gvApplication.batchEditApi.GetCellValue(iGrid[x], "TransDoc");
                                }
                            }
                            else cntr = 0;
                        }
                    }
                }

                for (var z = 0; z <= holder; z++) {
                    if (z == 0 && z == null && z == "undefined")
                        console.log('skip');
                    else {
                        if (arrTrans[z] != 0 && arrTrans[z] != null && z != "undefined")
                        { txt += arrTrans[z] + ";"; }
                    }
                }

                var str = txt;
                str = str.slice(0, -1);
                //END RefTrans
                //end

                //Adjustment
                for (var i = 0; i < iAdjustment.length; i++) {
                    if (gvAdjustment.batchEditHelper.IsNewItem(iAdjustment[i])) {

                        DAdjustment = gvAdjustment.batchEditApi.GetCellValue(iAdjustment[i], "Amount");

                        HAdjustment += DAdjustment;
                    }
                    else {
                        var key = gvAdjustment.GetRowKey(iAdjustment[i]);
                        if (gvAdjustment.batchEditHelper.IsDeletedItem(key)) {
                            console.log("deleted row " + iAdjustment[i]);
                        }
                        else {
                            DAdjustment = gvAdjustment.batchEditApi.GetCellValue(iAdjustment[i], "Amount");

                            HAdjustment += DAdjustment;
                        }
                    }
                }
                //end

                aglTransNo.SetText(str);
                speVariance.SetValue((HApplied - HAdjustment).toFixed(2));
                speAdjustment.SetValue(HAdjustment.toFixed(2));
            }, 100);
        }

    </script>
    <!--#endregion-->
</head>
<body style="height: 910px;">
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
<form id="form1" runat="server" class="Entry">
    <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel runat="server" Text="AR/AP Application" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
     <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" ClientInstanceName="factbox" CloseAction="None" 
        EnableViewState="False" HeaderText="Item info" Height="207px" Width="245px" PopupHorizontalOffset="1085" PopupVerticalOffset="50"
         ShowCloseButton="False" ShowCollapseButton="True" ShowOnPageLoad="True" ShowPinButton="True" ShowShadow="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="CSheet" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="CSheet" CloseAction="CloseButton" CloseOnEscape="true"
        EnableViewState="False" HeaderImage-Height="10px" HeaderText="" Height="600px" ShowHeader="true" Width="950px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
         ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents CloseUp="function (s, e) { window.location.reload(); }" />
    </dx:ASPxPopupControl>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="820px" Height="641px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="565px" Width="850px" style="margin-left: -20px">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="General">
                                        <Items>
                                            <dx:LayoutGroup Caption="Information" ColCount="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="Document Number">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtDocNumber" runat="server" OnLoad="TextboxLoad" Width="170px" AutoCompleteType="Disabled" Enabled="False">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Document Date">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxDateEdit ID="dtpDocDate" runat="server" OnInit="dtpDocDate_Init" OnLoad="Date_Load" Width="170px">
                                                                    <ClientSideEvents Validation="OnValidation" ValueChanged="function(s,e){cp.PerformCallback('CallbackDocDate');}"/>
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Business Partner" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridLookup ID="aglBizPartner" runat="server" Width="170px" ClientInstanceName="aglBizPartner" DataSourceID="sdsBizPartner" 
                                                                    KeyFieldName="BizPartnerCode" OnLoad="LookupLoad" TextFormatString="{0}" AutoGenerateColumns="false">
                                                                    <GridViewProperties>
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" AllowSort="false"/>
                                                                        <Settings ShowFilterRow="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="true">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Name" ReadOnly="true">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns> 
                                                                    <ClientSideEvents ValueChanged="function(s,e){cp.PerformCallback('CallbackBizPartner');}" Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxGridLookup>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Total Adjustment">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="speAdjustment" runat="server" Width="170px" Number="0.00" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  
                                                                                DisplayFormatString="{0:N}" SpinButtons-ShowIncrementButtons="false" ClientInstanceName ="speAdjustment" AllowMouseWheel="False"
                                                                                ReadOnly="true">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Name">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtName" runat="server" Width="170px" ReadOnly="true">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Variance">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="speVariance" runat="server" Width="170px" Number="0.00" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  
                                                                                DisplayFormatString="{0:N}" SpinButtons-ShowIncrementButtons="false" ClientInstanceName ="speVariance" AllowMouseWheel="False"
                                                                                ReadOnly="true">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Remarks">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxMemo ID="memRemarks" runat="server" OnLoad="MemoLoad" Width="850px" Height="200px" >
                                                                </dx:ASPxMemo>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                 </Items>
                                            </dx:LayoutGroup>
                                            <dx:LayoutGroup Caption="Applications">
                                                <Items>
                                                    <dx:LayoutItem Caption="Transaction No">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>  
                                                                            <dx:ASPxGridLookup ID="aglTransNo" runat="server" DataSourceID="sdsRefTrans" SelectionMode="Multiple" Width="1000px"
                                                                                KeyFieldName="RecordID" OnLoad="LookupLoad" TextFormatString="{0}-{2}" OnInit="aglTransNo_Init" ClientInstanceName="aglTransNo">
                                                                                <GridViewProperties>
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <Settings ShowFilterRow="True" />
                                                                                </GridViewProperties>
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"  SelectAllCheckboxMode="AllPages" VisibleIndex="0" Width="30px">
                                                                                    </dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="RecordID" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="TransType" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="DocNumber" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="DocDate" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px" ReadOnly ="true">
                                                                                        <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" AllowMouseWheel="false" DropDownButton-Enabled="false" DropDownButton-ClientVisible ="false">
                                                                                        <DropDownButton Enabled="False" ClientVisible="False"></DropDownButton>
                                                                                        </PropertiesDateEdit>
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="AccountCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="SubsiCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ProfitCenter" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CostCenter" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="8">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Amount" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="9">
                                                                                        <PropertiesTextEdit DisplayFormatString="{0:N}" />
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CounterReceiptNo" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="10">
                                                                                        <Settings AutoFilterCondition="Contains" />
                                                                                    </dx:GridViewDataTextColumn>
                                                                                </Columns>                                                                  
                                                                            </dx:ASPxGridLookup>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="lblSpace" runat="server" Width="10" Enabled="false">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
		                                                                    <dx:ASPxButton ID="btnGenerate" runat="server" AutoPostBack="False" Width="150px" Theme="MetropolisBlue" Text="Populate Detail" OnLoad="Button_Load">
                                                                                <ClientSideEvents Click="function(s, e) { cp.PerformCallback('CallbackTransNo') }" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="" ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridView ID="gvApplication" runat="server" AutoGenerateColumns="False" Width="770px" ClientInstanceName="gvApplication" OnBatchUpdate="gvApplication_BatchUpdate" 
                                                                    OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                                    OnInit="gvApplication_Init" OnRowValidating="grid_RowValidating" KeyFieldName ="LineNumber">
                                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" />
                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                    </SettingsPager>
                                                                    <SettingsEditing Mode="Batch">
                                                                    </SettingsEditing>
                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="130" VerticalScrollBarMode="Auto" />
                                                                    <SettingsBehavior AllowSort="False" />
                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0" Width="30px" ShowNewButtonInHeader="False">
                                                                            <CustomButtons>
                                                                                <dx:GridViewCommandColumnCustomButton ID="ApplicationDelete">
                                                                                <Image IconID="actions_cancel_16x16"> </Image>
                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                            </CustomButtons>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Line Number" FieldName="LineNumber" Name="aLineNumber" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Transaction Type" FieldName="TransType" Name="aTransType" ShowInCustomizationForm="True" VisibleIndex="2" Width="100px" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Transaction No" FieldName="TransDoc" Name="aTransDoc" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataDateColumn Caption="Transaction Date" FieldName="TransDate" Name="aTransDate" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px" ReadOnly ="true">
                                                                            <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" AllowMouseWheel="false" DropDownButton-Enabled="false" DropDownButton-ClientVisible ="false">
                                                                            <DropDownButton Enabled="False" ClientVisible="False"></DropDownButton>
                                                                            </PropertiesDateEdit>
                                                                        </dx:GridViewDataDateColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Account Code" FieldName="TransAccountCode" Name="aTransTransAccountCode" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="SubsiCode" FieldName="TransSubsiCode" Name="aTransSubsiCode" ShowInCustomizationForm="True" VisibleIndex="6" Width="100px" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Profit Center" FieldName="TransProfitCenter" Name="aTransType" ShowInCustomizationForm="True" VisibleIndex="7" Width="100px" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Cost Center" FieldName="TransCostCenter" Name="aTransCostCenter" ShowInCustomizationForm="True" VisibleIndex="8" Width="100px" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Business Partner" FieldName="TransBizPartnerCode" Name="aTransBizPartnerCode" ShowInCustomizationForm="True" VisibleIndex="9" Width="100px" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataSpinEditColumn Caption="Amount Due" FieldName="TransAmountDue" Name="aAmountDue" ShowInCustomizationForm="True" VisibleIndex="10" Width="150px" ReadOnly ="true">
                                                                            <PropertiesSpinEdit Increment="0" ClientInstanceName ="aAmountDue" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}" 
                                                                                SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="False">
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                            <ClientSideEvents ValueChanged="autocalculate" />
                                                                            </PropertiesSpinEdit>
                                                                        </dx:GridViewDataSpinEditColumn>
                                                                        <dx:GridViewDataSpinEditColumn Caption="Amount Applied" FieldName="TransAmountApplied" Name="aAmountApplied" ShowInCustomizationForm="True" VisibleIndex="11" Width="150px">
                                                                            <PropertiesSpinEdit Increment="0" ClientInstanceName ="aAmountApplied" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="False">
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                            <ClientSideEvents ValueChanged="autocalculate" />
                                                                            </PropertiesSpinEdit>
                                                                        </dx:GridViewDataSpinEditColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Version" Name="aVersion" Caption="Version" ShowInCustomizationForm="True" VisibleIndex="12" Width="0px" >
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="RecordID" Name="aRecordID" Caption="RecordID" ShowInCustomizationForm="True" VisibleIndex="13" Width="0px" >
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                </dx:ASPxGridView>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                            <dx:LayoutGroup Caption="Adjustments">
                                                <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridView ID="gvAdjustment" runat="server" AutoGenerateColumns="False" Width="770px" ClientInstanceName="gvAdjustment" OnBatchUpdate="gvAdjustment_BatchUpdate" 
                                                                    OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                                    OnRowValidating="grid_RowValidating" KeyFieldName ="LineNumber" OnInitNewRow="gvAdjustment_InitNewRow">
                                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" />
                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                    </SettingsPager>
                                                                    <SettingsEditing Mode="Batch">
                                                                    </SettingsEditing>
                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="130" VerticalScrollBarMode="Auto" />
                                                                    <SettingsBehavior AllowSort="False" />
                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0" Width="30px" ShowNewButtonInHeader="True">
                                                                            <CustomButtons>
                                                                                <dx:GridViewCommandColumnCustomButton ID="AdjustmentDelete">
                                                                                <Image IconID="actions_cancel_16x16"> </Image>
                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                            </CustomButtons>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Line Number" FieldName="LineNumber" Name="yLineNumber" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Account Code" FieldName="AccountCode" Name="yAccountCode" ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="glAccountCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="colAccountCode" DataSourceID="sdsAccountCode" KeyFieldName="AccountCode"  OnLoad="gvLookupLoad" TextFormatString="{0}" Width="100px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="AccountCode" ReadOnly="True" VisibleIndex="0">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="ControlAccount" ReadOnly="True" VisibleIndex="2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns> 
                                                                                    <ClientSideEvents CloseUp ="gvAdjustment_CloseUp" KeyDown="gvAdjustment_KeyDown" KeyPress="gvAdjustment_KeyPress"/>                                                                   
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Subsidiary Code" FieldName="SubsidiaryCode" Name="ySubsidiaryCode" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="glSubsiCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" AutoCallBack="false" ClientInstanceName="colSubsiCode" DataSourceID="sdsSubsi" KeyFieldName="SubsiCode"  OnInit="lookup_Init" OnLoad="gvLookupLoad" TextFormatString="{0}" Width="100px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="SubsiCode" ReadOnly="True" VisibleIndex="0">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>       
																					<ClientSideEvents KeyPress="gvAdjustment_KeyPress" KeyDown="gvAdjustment_KeyDown" DropDown="function dropdown(s, e){
                                                                                        colSubsiCode.GetGridView().PerformCallback('SubsiCode' + '|' + account + '|' + s.GetInputElement().value);
                                                                                        }" CloseUp="gvAdjustment_CloseUp"/>
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="BizPartner Code" FieldName="BizPartnerCode" Name="yBizPartnerCode" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="glBizPartner" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="colBizPartner" DataSourceID="sdsBizPartner" KeyFieldName="BizPartnerCode"  OnLoad="gvLookupLoad" TextFormatString="{0}" Width="100px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="True" VisibleIndex="0">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="Name" ReadOnly="True" VisibleIndex="1">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>       
																					<ClientSideEvents KeyPress="gvAdjustment_KeyPress" KeyDown="gvAdjustment_KeyDown" CloseUp="gvAdjustment_CloseUp"/>                                                                            
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Profit Center" FieldName="ProfitCenter" Name="yProfitCenter" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="glProfitCenter" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="colProfitCenter" DataSourceID="sdsProfitCenter" KeyFieldName="ProfitCenter"  OnLoad="gvLookupLoad" TextFormatString="{0}" Width="100px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="ProfitCenter" ReadOnly="True" VisibleIndex="0">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>              
																					<ClientSideEvents KeyPress="gvAdjustment_KeyPress" KeyDown="gvAdjustment_KeyDown" CloseUp="gvAdjustment_CloseUp"/>                                                               
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Cost Center" FieldName="CostCenter" Name="yCostCenter" ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="glCostCenter" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="colCostCenter" DataSourceID="sdsCostCenter" KeyFieldName="CostCenter"  OnLoad="gvLookupLoad" TextFormatString="{0}" Width="100px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="CostCenter" ReadOnly="True" VisibleIndex="0">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>                     
																					<ClientSideEvents KeyPress="gvAdjustment_KeyPress" KeyDown="gvAdjustment_KeyDown" CloseUp="gvAdjustment_CloseUp"/>                                                        
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataSpinEditColumn Caption="Amount" FieldName="Amount" Name="yAmount" ShowInCustomizationForm="True" VisibleIndex="7" Width="120px">
                                                                            <PropertiesSpinEdit Increment="0" ClientInstanceName ="yAmount" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="False">
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                            <ClientSideEvents ValueChanged="autocalculate" />
                                                                            </PropertiesSpinEdit>
                                                                        </dx:GridViewDataSpinEditColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Reason" FieldName="Reason" Name="yReason" ShowInCustomizationForm="True" VisibleIndex="8" Width="300px">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                </dx:ASPxGridView>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:LayoutGroup>                                 
                                    <dx:LayoutGroup Caption="User Defined" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Field1">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field3">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field4">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField4" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field5">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field6">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field7">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field8">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField8" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field9">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Journal Entries">
                                        <Items>
                                            <dx:LayoutItem Caption="">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridView ID="gvJournal" runat="server" AutoGenerateColumns="False" Width="850px" ClientInstanceName="gvJournal"  KeyFieldName="RTransType;TransType"  >
                                                            <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" CustomButtonClick="OnCustomClick"  />
                                                            <SettingsPager Mode="ShowAllRecords" />  
                                                            <SettingsEditing Mode="Batch"/>
                                                            <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130"  /> 
                                                            <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="AccountCode" Name="jAccountCode" ShowInCustomizationForm="True" VisibleIndex="0" Width ="120px" Caption="Account Code" >
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="AccountDescription" Name="jAccountDescription" ShowInCustomizationForm="True" VisibleIndex="1" Width ="150px" Caption="Account Description" >
                                                                </dx:GridViewDataTextColumn>
																<dx:GridViewDataTextColumn FieldName="SubsidiaryCode" Name="jSubsidiaryCode" ShowInCustomizationForm="True" VisibleIndex="2" Width ="120px" Caption="Subsidiary Code" >
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="SubsidiaryDescription" Name="jSubsidiaryDescription" ShowInCustomizationForm="True" VisibleIndex="3" Width ="150px" Caption="Subsidiary Description" >
                                                                </dx:GridViewDataTextColumn>																
																<dx:GridViewDataTextColumn FieldName="ProfitCenter" Name="jProfitCenter" ShowInCustomizationForm="True" VisibleIndex="4" Width ="120px" Caption="Profit Center" >
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="CostCenter" Name="jCostCenter" ShowInCustomizationForm="True" VisibleIndex="5" Width ="120px" Caption="Cost Center" >
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="Debit" Name="jDebit" ShowInCustomizationForm="True" VisibleIndex="6" Width="120px" Caption="Debit Amount" >
                                                                    <PropertiesSpinEdit Increment="0" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="False">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>                                                                
                                                                <dx:GridViewDataSpinEditColumn FieldName="Credit" Name="jCredit" ShowInCustomizationForm="True" VisibleIndex="7" Width="120px" Caption="Credit Amount" >
                                                                    <PropertiesSpinEdit Increment="0" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="False">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Audit Trail" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Added By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHAddedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Added Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHAddedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHLastEditedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHLastEditedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Submitted By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Submitted Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Posted By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtPostedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Posted Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtPostedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Cancelled By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtCancelledBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Cancelled Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtCancelledDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Reference Transaction" Name="ReferenceTransaction">
                                        <Items>
                                            <dx:LayoutItem Caption="">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                      <dx:ASPxGridView ID="gvRef" runat="server" AutoGenerateColumns="False" Width="608px"  KeyFieldName="RTransType;REFDocNumber;TransType;DocNumber" Settings-ShowStatusBar="Hidden">

<Settings ShowStatusBar="Hidden"></Settings>

                                                        <SettingsBehavior FilterRowMode="OnClick" ColumnResizeMode="NextColumn" />
                                                        <ClientSideEvents BatchEditConfirmShowing="OnConfirm" CustomButtonClick="OnCustomClick"  />
                                                        <SettingsPager PageSize="5">
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="Batch">
                                                        </SettingsEditing>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ButtonType="Image"  ShowInCustomizationForm="True" VisibleIndex="0" Width="90px" >            
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="ViewReferenceTransaction">
                                                                    <Image IconID="functionlibrary_lookupreference_16x16"></Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                    <dx:GridViewCommandColumnCustomButton ID="ViewTransaction">
                                                                    <Image IconID="find_find_16x16"></Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="RTransType" Caption="Reference TransType" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True"  Name="RTransType">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="REFDocNumber" Caption="Reference DocNumber" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="RCommandString" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="True" >
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="TransType" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly="True">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="DocNumber" Name="DocNumber" ShowInCustomizationForm="True" VisibleIndex="5" Caption="DocNumber"  ReadOnly="True">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="CommandString" ShowInCustomizationForm="True" VisibleIndex="6"   ReadOnly="True">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>  
                                </Items>
                            </dx:TabbedLayoutGroup>
                        </Items>
                    </dx:ASPxFormLayout>
                    <dx:ASPxPanel id="BottomPanel" runat="server" fixedposition="WindowBottom" backcolor="#FFFFFF" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <div class="pnl-content">
                                <dx:ASPxCheckBox style="display: inline-block;" ID="glcheck" runat="server" ClientInstanceName="glcheck" TextAlign="Left" Text="Prevent auto-close upon update" Width="200px"></dx:ASPxCheckBox>
                                <dx:ASPxButton ID="updateBtn" runat="server" Text="Add" AutoPostBack="False" CssClass="btn" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="OnUpdateClick" />
                                </dx:ASPxButton>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                    <dx:ASPxPopupControl ID="DeleteControl" runat="server" Width="250px" Height="100px" HeaderText="Warning!"
                    CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="DeleteControl"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Are you sure you want to delete this specific document?" />
                            <table>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                 <td><dx:ASPxButton ID="Ok" runat="server" Text="Ok" UseSubmitBehavior="false" AutoPostBack="false">
                                     <ClientSideEvents Click="function (s, e){ cp.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
                                     </dx:ASPxButton></td>
                                 <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false">
                                     <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                                     </dx:ASPxButton></td>
                                </tr>
                            </table>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</form>
    <!--#region Region Datasource-->
    <asp:ObjectDataSource ID="odsApplication" runat="server" DataObjectTypeName="Entity.RPApplication+RPApplicationTag" SelectMethod="getApplication" UpdateMethod="UpdateRPApplicationTag" TypeName="Entity.RPApplication+RPApplicationTag" DeleteMethod="DeleteRPApplicationTag" InsertMethod="AddRPApplicationTag">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsAdjustment" runat="server" DataObjectTypeName="Entity.RPApplication+RPApplicationAdj" SelectMethod="getAdjustment" UpdateMethod="UpdateRPApplicationAdj" TypeName="Entity.RPApplication+RPApplicationAdj" DeleteMethod="DeleteRPApplicationAdj" InsertMethod="AddRPApplicationAdj">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsJournalEntry" runat="server" SelectMethod="getJournalEntry" TypeName="Entity.RPApplication+JournalEntry" >
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsReference" runat="server" SelectMethod="getreftransaction" TypeName="Entity.RPApplication+RefTransaction" >
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--<asp:SqlDataSource ID="sdsDiscount" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT Description AS DiscountType, Code,  ROW_NUMBER() OVER (PARTITION BY LEN(Description) ORDER BY Description) Ord FROM IT.SystemSettings WHERE Code LIKE 'DISC_%' AND SequenceNumber = 6" OnInit ="Connection_Init"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="sdsBizPartner" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DISTINCT BizPartnerCode, Name FROM Masterfile.BizPartner WHERE ISNULL(IsInActive,0)=0" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsApplication" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber IS NULL" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAdjustment" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber IS NULL" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsTransDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, TransType ,DocNumber AS TransDoc, DocDate AS TransDate, AccountCode AS TransAccountCode, SubsiCode AS TransSubsiCode, ProfitCenter AS TransProfitCenter, CostCenter AS TransCostCenter, BizPartnerCode AS TransBizPartnerCode, Amount AS TransAmountDue, Amount AS TransAmountApplied, '2' AS Version, RecordID FROM Accounting.SubsiLedgerNonInv WHERE TransType IN ('ACTSIN','ACTARM','ACTSEB') AND ISNULL(Applied,0) <> ISNULL(Amount,0)" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsApplicationDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=";WITH A AS (SELECT DocNumber AS TransDoc, DocDate AS TransDate, TotalAmountDue AS TransAmountDue FROM Accounting.SalesInvoice WHERE ISNULL(SubmittedBy,'') != ''
        UNION ALL SELECT DocNumber, DocDate, TotalAmount FROM Accounting.ARMemo WHERE ISNULL(SubmittedBy,'') != '' UNION ALL SELECT DocNumber, DocDate, NetAmount FROM Accounting.ServiceBilling WHERE ISNULL(SubmittedBy,'') != '')
        SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY TransDoc) AS VARCHAR(5)),5) AS LineNumber, TransDoc, TransDate, TransAmountDue, 0.00 AS TransAmountApplied FROM A" OnInit ="Connection_Init"></asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="sdsBizAccount" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BizAccountCode AS BizAccount, BizAccountName AS Name FROM Masterfile.BizAccount WHERE ISNULL(IsInactive,0) = 0 " OnInit ="Connection_Init"></asp:SqlDataSource>--%>
    <%--<asp:SqlDataSource ID="sdsBankAccount" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BankAccountCode, Description FROM Masterfile.BankAccount WHERE ISNULL(IsInactive,0) = 0 " OnInit ="Connection_Init"></asp:SqlDataSource>--%>  
    <%--<asp:SqlDataSource ID="sdsBank" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT A.BankCode AS Bank, C.Description FROM Masterfile.BPBankInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode LEFT JOIN Masterfile.Bank C ON A.BankCode = C.BankCode " OnInit = "Connection_Init"></asp:SqlDataSource>--%>
    <%--<asp:SqlDataSource ID="sdsBranch" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DISTINCT A.Branch FROM Masterfile.BPBankInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode" OnInit ="Connection_Init"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="sdsRefTrans" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DISTINCT RecordID, TransType, DocNumber, DocDate, AccountCode, SubsiCode, ProfitCenter, CostCenter, BizPartnerCode, ISNULL(Amount,0) - ISNULL(Applied,0) AS Amount, CounterDocNumber AS CounterReceiptNo FROM Accounting.SubsiLedgerNonInv WHERE ISNULL(Applied,0) <> ISNULL(Amount,0) AND 1=0" OnInit ="Connection_Init"></asp:SqlDataSource>	
	<asp:SqlDataSource ID="sdsAccountCode" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT AccountCode, Description, ISNULL(ControlAccount,0) AS ControlAccount FROM Accounting.ChartOfAccount WHERE (ISNULL(IsInactive,0) = 0 AND ISNULL(AllowJV,0) = 1) OR AccountCode BETWEEN 4000 AND 4999" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsProfitCenter" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT ProfitCenterCode AS ProfitCenter, Description FROM Accounting.ProfitCenter WHERE ISNULL(IsInactive,0) = 0" OnInit ="Connection_Init"></asp:SqlDataSource>  
    <asp:SqlDataSource ID="sdsCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT CostCenterCode AS CostCenter, Description FROM Accounting.CostCenter WHERE ISNULL(IsInactive,0) = 0" OnInit = "Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsSubsi" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT AccountCode, SubsiCode, Description FROM Accounting.GLSubsiCode" OnInit = "Connection_Init"></asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="sdsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT Currency, CurrencyName FROM Masterfile.Currency WHERE ISNULL(IsInactive,0) = 0" OnInit = "Connection_Init"> </asp:SqlDataSource>--%>
    <%--<asp:SqlDataSource ID="sdsCollector" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DISTINCT Collector, FirstName + ' ' + LastName AS Name FROM Masterfile.BPCustomerInfo A INNER JOIN Masterfile.BPEmployeeInfo B ON A.Collector = B.EmployeeCode WHERE ISNULL(B.IsInactive,0) = 0" OnInit = "Connection_Init"> </asp:SqlDataSource>--%>
    <%--<asp:SqlDataSource ID="sdsRefLoan" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DocNumber, LoanType, LoanClass, LoanAmount, LoanReference, MaturityDate FROM Accounting.Loan WHERE ISNULL(SubmittedBy,'') != ''" OnInit = "Connection_Init"> </asp:SqlDataSource>--%>
    <%--<asp:SqlDataSource ID="sdsLoanDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber,DocNumber AS LoanNumber, LoanType, LoanClass, LoanAmount, LoanReference, '1' AS Version FROM Accounting.Loan WHERE 1=0" OnInit ="Connection_Init"></asp:SqlDataSource>--%>
    <%--<asp:SqlDataSource ID="sdsColType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT 'L' AS Code, 'Loan' AS Info UNION ALL SELECT 'T' AS Code, 'Trade' AS Info UNION ALL SELECT 'N' AS Code, 'Non-Trade' AS Info" OnInit ="Connection_Init"></asp:SqlDataSource>--%>
    <!--#endregion-->
</body>
</html>


