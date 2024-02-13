<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLoan.aspx.cs" Inherits="GWL.frmLoan" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Loan</title><link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <script src="../js/jquery-1.6.1.min.js" type="text/javascript"></script><%--NEWADD--%>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script><%--NEWADD--%>
    
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
        height: 580px; /*Change this whenever needed*/
        }

        .Entry {
         padding: 20px;
         margin: 10px auto;
         background: #FFF;
        }

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

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function OnValidation(s, e) {
            if (s.GetText() == "" || e.value == "" || e.value == null) {
                counterror++;
                isValid = false
            }
            else {
                isValid = true;
            }
        }

        function OnUpdateClick(s, e) { //Add/Edit/Close button function
            var btnmode = btn.GetText(); //gets text of button
            if (btnmode == "Delete") {
                cp.PerformCallback("Delete");
            }

            if (isValid && counterror < 1 || btnmode == "Close") { //check if there's no error then proceed to callback
                //Sends request to server side
                if (btnmode == "Add") {
                    cp.PerformCallback("Add");
                }
                else if (btnmode == "Update") {
                    cp.PerformCallback("Update");
                }
                else if (btnmode == "Close") {
                    cp.PerformCallback("Close");
                }
            }
            else {
                counterror = 0;
                alert('Please check all the fields!');
            }
        }

        function OnConfirm(s, e) {//function upon saving entry
            if (e.requestTriggerID === "cp")//disables confirmation message upon saving.
                e.cancel = true;
        }

        function cp_EndCallback(s, e) {//End callback function if (s.cp_success) {
            if (s.cp_success) {
                if (s.cp_valmsg != null) {
                    alert(s.cp_valmsg);
                }
                if(s.cp_message != null) {
                    alert(s.cp_message);
                }
                delete (s.cp_valmsg);
                delete (s.cp_success);//deletes cache variables' data
                delete (s.cp_message);
            }

            if (s.cp_close) {
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

            if (s.cp_forceclose) {//NEWADD
                delete (s.cp_forceclose);
                window.close();
            }
        }

        var index;
        var closing;
        var valchange = false;
        var currentColumn = null;
        var isSetTextRequired = false;
        var linecount = 1;

        var customerload;
        function OnStartEditing(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];
            var entry = getParameterByName('entry');
            var param = getParameterByName('parameters');

            if (entry == "V" || entry == "D") {
                e.cancel = true; //this will made the gridview readonly
            }
            else if (!(entry == "M" && param == "paysched")) {
                e.cancel = true;
            }
            else if (e.rowValues[(s.GetColumnByField('CVNumber').index)].value != null&&
                e.rowValues[(s.GetColumnByField('CVNumber').index)].value != '') {
                e.cancel = true;
            }
            else if (currentColumn.fieldName === "Penalty" && CINLoanType.GetText() != "Bank") {
                e.cancel = true;
            }
            else if (!(currentColumn.fieldName === "PeriodTo" ||
                       currentColumn.fieldName === "Principal" ||
                       currentColumn.fieldName === "Interest" ||
                       currentColumn.fieldName === "Penalty")) {
                e.cancel = true;
            }
        }

        function OnEndEditing(s, e) {    //end edit grid function, sets text after select/leaving the current lookup
            var cellInfo = e.rowValues[currentColumn.index];
            var indicies = gv1.batchEditHelper.GetDataRowIndices();
            var Principal = 0;
            var Days = 0;
            var IntRate = CINInterestRate.GetValue()/100;
            var Denominator = CINLoanClass.GetValue();
            var Interest = 0;

            if (currentColumn.fieldName == 'PeriodTo') {
                var NewValue = e.rowValues[(s.GetColumnByField('PeriodTo').index)].value;
                var OldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'PeriodTo');
                
                if (NewValue.toString() != OldValue.toString()) {
                    Days = (NewValue - s.batchEditApi.GetCellValue(e.visibleIndex, 'PeriodFrom')) / 86400000;
                    Interest = parseFloat((s.batchEditApi.GetCellValue(e.visibleIndex, 'BegBalofPrincipal') * IntRate * Days / Denominator).toFixed(2));
                    s.batchEditApi.SetCellValue(e.visibleIndex, 'NumOfDays', Days);
                    s.batchEditApi.SetCellValue(e.visibleIndex, 'Interest', Interest);
                    s.batchEditApi.SetCellValue(e.visibleIndex, 'PaymentAmount',
                        Interest + s.batchEditApi.GetCellValue(e.visibleIndex, 'Principal') +
                        s.batchEditApi.GetCellValue(e.visibleIndex, 'Penalty'));
                    for (var i = e.visibleIndex + 1; i < indicies.length; i++) {
                        if (s.batchEditApi.GetCellValue(i, 'PeriodFrom').toString() == OldValue.toString()) {
                            Days = (s.batchEditApi.GetCellValue(i, 'PeriodTo') - NewValue) / 86400000;
                            Interest = parseFloat((s.batchEditApi.GetCellValue(i, 'BegBalofPrincipal') * IntRate * Days / Denominator).toFixed(2));

                            s.batchEditApi.SetCellValue(i, 'PeriodFrom', NewValue);
                            s.batchEditApi.SetCellValue(i, 'NumOfDays', Days);
                            s.batchEditApi.SetCellValue(i, 'Interest', Interest);
                            s.batchEditApi.SetCellValue(i, 'PaymentAmount',
                                Interest + s.batchEditApi.GetCellValue(i, 'Principal') +
                                s.batchEditApi.GetCellValue(i, 'Penalty'));
                        }
                    }
                }
            }
            else if (currentColumn.fieldName == 'Interest') {
                var NewValue = e.rowValues[(s.GetColumnByField('Interest').index)].value

                s.batchEditApi.SetCellValue(e.visibleIndex, 'PaymentAmount',
                    NewValue + s.batchEditApi.GetCellValue(e.visibleIndex, 'Principal') +
                    s.batchEditApi.GetCellValue(e.visibleIndex, 'Penalty'));
            }
            else if (currentColumn.fieldName == 'Principal') {
                var NewValue = e.rowValues[(s.GetColumnByField('Principal').index)].value;
                var OldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'Principal');

                if (NewValue != OldValue) {
                    s.batchEditApi.SetCellValue(e.visibleIndex, 'PaymentAmount',
                        NewValue + s.batchEditApi.GetCellValue(e.visibleIndex, 'Interest') +
                        s.batchEditApi.GetCellValue(e.visibleIndex, 'Penalty'));
                    var EndBal = s.batchEditApi.GetCellValue(e.visibleIndex, 'BegBalofPrincipal') - NewValue;
                    s.batchEditApi.SetCellValue(e.visibleIndex, 'EndBalofPrincipal', EndBal);

                    for (var i = e.visibleIndex + 1; i < indicies.length; i++) {
                        s.batchEditApi.SetCellValue(i, 'BegBalofPrincipal', EndBal);

                        Days = (s.batchEditApi.GetCellValue(i, 'PeriodTo') - s.batchEditApi.GetCellValue(i, 'PeriodFrom')) / 86400000;
                        Interest = parseFloat((EndBal * IntRate * Days / Denominator).toFixed(2));
                        s.batchEditApi.SetCellValue(i, 'Interest', Interest);

                        Principal = s.batchEditApi.GetCellValue(i, 'Principal');
                        if (Principal > EndBal) {
                            s.batchEditApi.SetCellValue(i, 'Principal', EndBal);
                            Principal = EndBal;
                            EndBal = 0;
                        }
                        else {
                            EndBal = EndBal - Principal;
                        }

                        s.batchEditApi.SetCellValue(i, 'EndBalofPrincipal', EndBal);
                        s.batchEditApi.SetCellValue(i, 'PaymentAmount', Principal + Interest + s.batchEditApi.GetCellValue(i, 'Penalty'));
                    }
                }
            }
            else if (currentColumn.fieldName == 'Penalty') {
                var NewValue = e.rowValues[(s.GetColumnByField('Penalty').index)].value;

                s.batchEditApi.SetCellValue(e.visibleIndex, 'PaymentAmount',
                    NewValue + s.batchEditApi.GetCellValue(e.visibleIndex, 'Principal') +
                    s.batchEditApi.GetCellValue(e.visibleIndex, 'Interest'));
            }
        }

        var identifier;
        var val_ALL;

        function gridLookup_KeyDown(s, e) { //Allows tabbing between gridlookup on details
            isSetTextRequired = false;
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode !== 9) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (gv1.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }
        }

        function gridLookup_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode == 13) {
                gv1.batchEditApi.EndEdit();
            }
        }

        function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
            gv1.batchEditApi.EndEdit();
        }

        function OnInitTrans(s, e) {
            AdjustSize();
        }

        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }

        function AdjustSize() {
            var width = Math.max(0, document.documentElement.clientWidth);
            gv1.SetWidth(width - 120);
        }

        function LoanCategoryChanged(s, e)
        {
            if(CINLoanCategory.GetValue() == "NewAvailment")
            {
                aglNewLoans.SetVisible(true);
                aglLoanRenewal.SetVisible(false);
                aglLoanRenewal.SetValue(null);
                CINLoanReference.SetEnabled(true);
            }
            else
            {
                aglNewLoans.SetVisible(false);
                aglNewLoans.SetValue(null);
                aglLoanRenewal.SetVisible(true);
                CINLoanReference.SetEnabled(false);
            }
            CINLoanAmount.SetValue(0);
            CINBizPartnerCode.SetValue(null);
            CINBizPartnerName.SetValue(null);
            CINLoanReference.SetValue(null);
        }

        function SetLoanInfo(values)
        {
            CINBizPartnerCode.SetValue(values[0]);
            CINBizPartnerName.SetValue(values[1]);
            CINLoanAmount.SetValue(values[2]);
            CINLoanType.SetValue(values[3]);
            if (values.length > 4) { CINLoanReference.SetValue(values[4]); }
        }
    </script>
    <!--#endregion-->
</head>
    
<body style="height: 910px"><dx:ASPxGlobalEvents ID="ge" runat="server">
    <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <form id="form1" runat="server" class="Entry">
        <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel runat="server" Text="Loan" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>

        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="910px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="cp_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="565px" Width="850px" style="margin-left: -3px">
                       <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="General">
                                        <Items>
                                            <dx:LayoutGroup Caption="Loan Set-Up" ColCount="2" RowSpan="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="Document Number" Name="DocumentNumber" >
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer  runat="server">
                                                                <dx:ASPxTextBox ID="txtDocnumber" runat="server" Enabled="False" Width="170px">
                                                                    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Loan Amount">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="spinLoanAmount" ClientInstanceName="CINLoanAmount" runat="server" Width="170px" 
                                                                    SpinButtons-ShowIncrementButtons="False" ClientEnabled="false" Increment="0" NullDisplayText="0.00" 
                                                                    ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}" HorizontalAlign="Right">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Document Date" Name="DocDate" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxDateEdit ID="dtDocDate" runat="server" ClientInstanceName="CINDocDate" OnLoad="Date_Load" Width="170px">
                                                                    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                    <ClientSideEvents 
                                                                        ValueChanged="function(s,e) { 
                                                                            cp.PerformCallback('SetMaturity');
                                                                        }" />
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Interest Rate">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="spinInterestRate" ClientInstanceName="CINInterestRate" runat="server" Width="170px" 
                                                                    SpinButtons-ShowIncrementButtons="False" Increment="0" OnLoad="SpinEdit_Load" NullDisplayText="0%" 
                                                                    ConvertEmptyStringToNull="False" NullText="0%" DisplayFormatString="{0}%" HorizontalAlign="Right">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Loan Category:" >
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox ID="cbLoanCategory" Width="170px" ClientInstanceName="CINLoanCategory" runat="server" OnLoad="ComboBoxLoad">
                                                                    <ClientSideEvents 
                                                                        Validation="OnValidation" 
                                                                        SelectedIndexChanged="LoanCategoryChanged" />                                                         
                                                                    <Items>
                                                                        <dx:ListEditItem Text="New Availment" Value="NewAvailment" />
                                                                        <dx:ListEditItem Text="Renewal" Value="Renewal" />
                                                                    </Items>
                                                                    <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True"/>
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Terms">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxSpinEdit ID="spinTerms" ClientInstanceName="CINTerms" runat="server" Width="85px" 
                                                                                SpinButtons-ShowIncrementButtons="False" Increment="0" OnLoad="SpinEdit_Load" NullDisplayText="0" 
                                                                                ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0}" HorizontalAlign="Right">
                                                                                <ClientSideEvents 
                                                                                    ValueChanged="function(s,e) { 
                                                                                        cp.PerformCallback('SetMaturity');
                                                                                    }" />
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxComboBox ID="cbTermType" Width="85px" ClientInstanceName="CINTermType" runat="server" OnLoad="ComboBoxLoad">                                                       
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Days" Value="Days" />
                                                                                    <dx:ListEditItem Text="Months" Value="Months" />
                                                                                </Items>
                                                                                <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                                    <RequiredField IsRequired="True"/>
                                                                                </ValidationSettings>
                                                                                <InvalidStyle BackColor="Pink">
                                                                                </InvalidStyle>
                                                                                <ClientSideEvents 
                                                                                    ValueChanged="function(s,e) { 
                                                                                        cp.PerformCallback('SetMaturity');
                                                                                    }" />
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Loan Reference">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridLookup ID="aglNewLoans" runat="server" ClientInstanceName="aglNewLoans" DataSourceID="sdsNewLoans" 
                                                                    KeyFieldName="DocNumber" OnLoad="LookupLoad" TextFormatString="{0}" Width="170px" ClientVisible="true">
                                                                    <GridViewProperties>
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                        <Settings ColumnMinWidth="50" ShowFilterRow="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="DocNumber" Caption="Receipt #" ReadOnly="true" VisibleIndex="1" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="DocDate" Caption="Receipt Date" ReadOnly="true" VisibleIndex="2" PropertiesTextEdit-DisplayFormatString="MM/dd/yyyy"/>
                                                                        <dx:GridViewDataTextColumn FieldName="BizPartnerCode" Caption="BizPartner" ReadOnly="true" VisibleIndex="3" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="Name" ReadOnly="true" VisibleIndex="4" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="Remarks" ReadOnly="true" VisibleIndex="5" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="TotalCashAmount" Caption="Cash Amount" ReadOnly="true" VisibleIndex="6" PropertiesTextEdit-DisplayFormatString="#,0.00" />
                                                                        <dx:GridViewDataTextColumn FieldName="TotalBankCredit" Caption="Bank Credit" ReadOnly="true" VisibleIndex="7" PropertiesTextEdit-DisplayFormatString="#,0.00" />
                                                                        <dx:GridViewDataTextColumn FieldName="TotalCheckAmount" Caption="Check Amount" ReadOnly="true" VisibleIndex="8" PropertiesTextEdit-DisplayFormatString="#,0.00" />
                                                                        <dx:GridViewDataTextColumn FieldName="TotalCharges" Caption="Charges" ReadOnly="true" VisibleIndex="9" PropertiesTextEdit-DisplayFormatString="#,0.00" />
                                                                    </Columns>
                                                                    <ClientSideEvents Validation="OnValidation" 
                                                                        RowClick="function(s,e) { 
                                                                            s.GetRowValues(e.visibleIndex, 'BizPartnerCode;Name;LoanAmount;LoanType', SetLoanInfo); 
                                                                        }"
                                                                        />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxGridLookup>
                                                                <dx:ASPxGridLookup ID="aglLoanRenewal" runat="server" ClientInstanceName="aglLoanRenewal" DataSourceID="sdsLoanRenewal" 
                                                                    KeyFieldName="DocNumber" OnLoad="LookupLoad" TextFormatString="{0}" Width="170px" ClientVisible="false">
                                                                    <GridViewProperties>
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                        <Settings ColumnMinWidth="50" ShowFilterRow="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="DocNumber" Caption="Loan Number" ReadOnly="True" VisibleIndex="1" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="DocDate" Caption="Loan Date" ReadOnly="True" VisibleIndex="2" PropertiesTextEdit-DisplayFormatString="MM/dd/yyyy" />
                                                                        <dx:GridViewDataTextColumn FieldName="LoanType" ReadOnly="True" VisibleIndex="3" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="LoanCategory" ReadOnly="True" VisibleIndex="4" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="Status" Caption="Loan Status" ReadOnly="True" VisibleIndex="5" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="BizPartnerCode" Caption="BizPartner" ReadOnly="True" VisibleIndex="6" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="Name" ReadOnly="true" VisibleIndex="7" Settings-AutoFilterCondition="Contains" />
                                                                        <dx:GridViewDataTextColumn FieldName="LoanAmount" Caption="Loan Amount" ReadOnly="true" VisibleIndex="8" PropertiesTextEdit-DisplayFormatString="#,0.00" />
                                                                        <dx:GridViewDataTextColumn FieldName="ReferenceLN" Caption="Reference Number" ReadOnly="true" VisibleIndex="9" Settings-AutoFilterCondition="Contains" />
                                                                    </Columns>
                                                                    <ClientSideEvents Validation="OnValidation" 
                                                                        RowClick="function(s,e) { s.GetRowValues(e.visibleIndex, 'BizPartnerCode;Name;LoanAmount;LoanType;DocNumber', SetLoanInfo); }"
                                                                        />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxGridLookup>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Maturity Date" Name="MaturityDate">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxDateEdit ID="dtMaturityDate" runat="server" ClientInstanceName="CINMaturityDate" OnLoad="Date_Load" Width="170px">
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Loan Type:" Name="LoanType">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox ID="cbLoanType" Width="170px" runat="server" ClientInstanceName="CINLoanType"
                                                                    OnLoad="ComboBoxLoad" ClientEnabled="false">
                                                                    <ClientSideEvents Validation="OnValidation" />                                                         
                                                                    <Items>
                                                                        <dx:ListEditItem Text="Bank" Value="Bank" />
                                                                        <dx:ListEditItem Text="Private" Value="Private" />
                                                                    </Items>
                                                                    <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True"/>
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Frequency Of Payment">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Loan Class:" Name="LoanClass">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox ID="cbLoanClass" Width="170px" ClientInstanceName="CINLoanClass" runat="server" OnLoad="ComboBoxLoad">
                                                                    <ClientSideEvents Validation="OnValidation"/>                                                         
                                                                    <Items>
                                                                        <dx:ListEditItem Text="Regular" Value="360" />
                                                                        <dx:ListEditItem Text="Term" Value="365" />
                                                                    </Items>
                                                                    <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True"/>
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                    <ClientSideEvents 
                                                                        ValueChanged="function(s,e) { 
                                                                            cp.PerformCallback('SetMaturity');
                                                                        }" />
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem  ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td style="padding-left:20px">
                                                                            <dx:ASPxLabel Text="Interest" runat="server"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td style="padding-left:69px">
                                                                            <dx:ASPxComboBox ID="cbFrequencyInterest" Width="170px" ClientInstanceName="CINFrequencyInterest" runat="server" OnLoad="ComboBoxLoad">
                                                                                <ClientSideEvents Validation="OnValidation" />                                                         
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Weekly" Value="7" />
                                                                                    <dx:ListEditItem Text="Semi-Monthly" Value="15" />
                                                                                    <dx:ListEditItem Text="Monthly" Value="30" />
                                                                                    <dx:ListEditItem Text="Bi-Monthly" Value="60" />
                                                                                    <dx:ListEditItem Text="Quarterly" Value="90" />
                                                                                    <dx:ListEditItem Text="Semi-Annual" Value="180" />
                                                                                </Items>
                                                                                <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                                    <RequiredField IsRequired="True"/>
                                                                                </ValidationSettings>
                                                                                <InvalidStyle BackColor="Pink">
                                                                                </InvalidStyle>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Reference Number">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtLoanReference" runat="server" ClientInstanceName="CINLoanReference" Width="170px" OnLoad="TextboxLoad">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td style="padding-left:20px">
                                                                            <dx:ASPxLabel Text="Principal" runat="server"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td style="padding-left:64px">
                                                                            <dx:ASPxComboBox ID="cbFrequencyPrincipal" Width="170px" ClientInstanceName="CINFrequencyPrincipal" runat="server" OnLoad="ComboBoxLoad">
                                                                                <ClientSideEvents Validation="OnValidation" />                                                         
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Weekly" Value="7" />
                                                                                    <dx:ListEditItem Text="Semi-Monthly" Value="15" />
                                                                                    <dx:ListEditItem Text="Monthly" Value="30" />
                                                                                    <dx:ListEditItem Text="Bi-Monthly" Value="60" />
                                                                                    <dx:ListEditItem Text="Quarterly" Value="90" />
                                                                                    <dx:ListEditItem Text="Semi-Annual" Value="180" />
                                                                                    <dx:ListEditItem Text="Loan Maturiy" Value="0" />
                                                                                </Items>
                                                                                <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                                    <RequiredField IsRequired="True"/>
                                                                                </ValidationSettings>
                                                                                <InvalidStyle BackColor="Pink">
                                                                                </InvalidStyle>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Biz Partner">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td><dx:ASPxTextBox ID="txtBizPartner" runat="server" ClientInstanceName="CINBizPartnerCode" Width="100px" ClientEnabled="false"/></td>
                                                                        <td><dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="CINBizPartnerName" Width="300px" ClientEnabled="false"/></td>
                                                                    </tr>
                                                                </table>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Status">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtStatus" runat="server" ClientInstanceName="CINStatus"  Width="170px" ReadOnly="true">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:LayoutGroup>

                                    <dx:LayoutGroup Caption="User Defined" ColCount="2" Name="udf">
                                        <Items>
											<dx:LayoutItem Caption="Field 1:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 6:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 2:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 7:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 3:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 8:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField8" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 4:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField4" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 9:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 5:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>

                                    <dx:LayoutGroup Caption="Audit Trail" ColSpan="2" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Added By:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHAddedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Added Date:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHAddedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited By:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHLastEditedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited Date:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHLastEditedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>  
                                            <dx:LayoutItem Caption="Submitted By:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtSubmittedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Submitted Date:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtSubmittedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem> 
                                            <dx:LayoutItem Caption="Cancelled By:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtCancelledBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Cancelled Date:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtCancelledDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem> 
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:TabbedLayoutGroup>
                            
                            <dx:LayoutGroup Caption="Payment Schedule">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" Width="850px" 
                                                    OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gv1"
                                                    OnBatchUpdate="gv1_BatchUpdate" OnCustomButtonInitialize="gv1_CustomButtonInitialize" SettingsBehavior-AllowSort="False">
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="OnInitTrans"
                                                        BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                                                    <SettingsPager Mode="ShowAllRecords" /> 
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="VIsible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" 
                                                        ColumnMinWidth="120" ShowFooter="True" ShowStatusBar="Hidden" /> 
                                                    <Styles>
                                                        <Header BackColor="#EEEEEE" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </Styles>
                                                    <Columns>
                                                        <dx:GridViewDataSpinEditColumn FieldName="Period" Name="Period" Caption="Period" VisibleIndex="3" Width="55px" CellStyle-BackColor="#EEEEEE">
                                                            <CellStyle HorizontalAlign="Center" />
                                                            <PropertiesSpinEdit Increment="0" ClientInstanceName="CINPeriod" SpinButtons-ShowIncrementButtons="false">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="BegBalofPrincipal" Name="BegBalofPrincipal" Caption="Beg. Bal of Principal" VisibleIndex="4" 
                                                            Width="100px" HeaderStyle-Wrap="True" CellStyle-BackColor="#EEEEEE">
                                                            <CellStyle HorizontalAlign="Right" />
                                                            <PropertiesSpinEdit Increment="0" ClientInstanceName="CINBegBalofPrincipal" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" DisplayFormatString="{0:N}">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewBandColumn Caption="Period" VisibleIndex="5">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Caption="Date From" FieldName="PeriodFrom" Name="PeriodFrom" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px" CellStyle-BackColor="#EEEEEE">
                                                                    <CellStyle HorizontalAlign="Center"/>
                                                                    <PropertiesDateEdit DisplayFormatString="{0:MM/dd/yyyy}" />
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataDateColumn Caption="Date To" FieldName="PeriodTo" Name="PeriodTo" ShowInCustomizationForm="True" VisibleIndex="2" Width="80px" >
                                                                    <CellStyle HorizontalAlign="Center" />
                                                                    <PropertiesDateEdit DisplayFormatString="{0:MM/dd/yyyy}" />
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn Caption="# of Days" FieldName="NumOfDays" ShowInCustomizationForm="True" VisibleIndex="3" Width="80px" ReadOnly="true" 
                                                                    CellStyle-BackColor="#EEEEEE" CellStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="#,0">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="PaymentAmount" Name="PaymentAmount" Caption="Payment Amount" VisibleIndex="6" Width="100px" CellStyle-BackColor="#EEEEEE">
                                                            <CellStyle HorizontalAlign="Right" />
                                                            <PropertiesSpinEdit Increment="0" ClientInstanceName="CINPaymentAmount" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" DisplayFormatString="{0:N}">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewBandColumn Caption="Payment Application" VisibleIndex="7">
                                                            <Columns>
                                                                <dx:GridViewDataSpinEditColumn FieldName="Interest" Name="Interest" Caption="Interest" VisibleIndex="1" Width="100px">
                                                                    <CellStyle HorizontalAlign="Right" />
                                                                    <PropertiesSpinEdit Increment="0" ClientInstanceName="CINPaymentAmount" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" DisplayFormatString="{0:N}">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="Principal" Name="Principal" Caption="Principal" VisibleIndex="2" Width="100px">
                                                                    <CellStyle HorizontalAlign="Right" />
                                                                    <PropertiesSpinEdit Increment="0" ClientInstanceName="CINPaymentAmount" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" DisplayFormatString="{0:N}">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="Penalty" Name="Penalty" Caption="Penalty" VisibleIndex="3" Width="100px">
                                                                    <CellStyle HorizontalAlign="Right" />
                                                                    <PropertiesSpinEdit Increment="0" ClientInstanceName="CINPaymentAmount" SpinButtons-ShowIncrementButtons="false" ConvertEmptyStringToNull="False" DisplayFormatString="{0:N}">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="EndBalofPrincipal" Name="EndBalofPrincipal" Caption="End Balance of Principal" VisibleIndex="8" Width="100px" CellStyle-BackColor="#EEEEEE">
                                                            <CellStyle HorizontalAlign="Right" />
                                                            <PropertiesSpinEdit Increment="0" ClientInstanceName="CINPaymentAmount" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" DisplayFormatString="{0:N}">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>

                                                        <dx:GridViewDataTextColumn FieldName="CVNumber" VisibleIndex="9" Width="120px" Caption="Reference CV#" ReadOnly="true" CellStyle-BackColor="#EEEEEE">
                                                        </dx:GridViewDataTextColumn>
                                                                           
                                                        <dx:GridViewDataTextColumn FieldName="DocNumber" VisibleIndex="10" Visible="true" Width="0px" Name="DocNumber" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="11" Visible="true" Width="0px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <dx:ASPxPanel id="BottomPanel" runat="server" fixedposition="WindowBottom" backcolor="#FFFFFF" Height="30px">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <div class="pnl-content">
                    <dx:ASPxCheckBox style="display: inline-block;" ID="glcheck" runat="server" ClientInstanceName="glcheck" TextAlign="Left" Text="Prevent auto-close upon update" Width="200px"></dx:ASPxCheckBox>
                    <dx:ASPxButton ID="updateBtn" runat="server" Text="Update" AutoPostBack="False" CssClass="btn" ClientInstanceName="btn"
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
                        <td><dx:ASPxButton ID="Ok" runat="server" Text="Ok" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                <ClientSideEvents Click="function (s, e){ cp.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
                            </dx:ASPxButton>
                        </td>
                        <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                            </dx:ASPxButton> 
                        </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Calculating..."
            ClientInstanceName="loader" ContainerElementID="gv1" Modal="true">
            <LoadingDivStyle Opacity="0"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" Text="Loading..." Modal="true"
            ClientInstanceName="loader" ContainerElementID="cp">
            <LoadingDivStyle Opacity="0"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
    </form>

    <!--#region Region Datasource-->
    <%--<!--#region Region Header --> --%>
    <asp:ObjectDataSource ID="odsHeader" runat="server" SelectMethod="getdata" TypeName="Entity.Loan" DataObjectTypeName="Entity.Loan" InsertMethod="InsertData" UpdateMethod="UpdateData">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="DocNumber" QueryStringField="docnumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDetail" runat="server" SelectMethod="getdetail" TypeName="Entity.Loan+LoanDetail" DataObjectTypeName="Entity.Loan+LoanDetail" DeleteMethod="DeleteLoanDetail" InsertMethod="AddLoanDetail" UpdateMethod="UpdateLoanDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" 
        SelectCommand="SELECT * FROM  Accounting.LoanDetail where DocNumber is null "
        OnInit="Connection_Init">
    </asp:SqlDataSource>
    <%------------SQL DataSource------------%>
    
    <%--Loan Reference Look Up--%>
    <asp:SqlDataSource ID="sdsNewLoans" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" 
        SelectCommand="
		WITH CHRG AS
		(
		SELECT DocNumber, SUM(Amount) AS TotalCharges 
		  FROM Accounting.CollectionLoanCharges
		 GROUP BY DocNumber
		)
        SELECT CR.DocNumber, DocDate, CustomerCode AS BizPartnerCode, Name, Remarks, 
               CASE WHEN CollectionType = 'L' THEN 'Bank' ELSE 'Private' END AS LoanType,
			   ISNULL(TotalCashAmount,0) AS TotalCashAmount,
			   ISNULL(TotalBankCredit,0) AS TotalBankCredit,
			   ISNULL(TotalCheckAmount,0) AS TotalCheckAmount,
			   ISNULL(TotalCharges,0) AS TotalCharges,
	           ISNULL(TotalCashAmount,0)+ISNULL(TotalBankCredit,0)
	           +ISNULL(TotalCheckAmount,0)+ISNULL(TotalCharges,0) AS LoanAmount
          FROM Accounting.Collection CR
			   LEFT JOIN CHRG ON CR.DocNumber = CHRG.DocNumber
         WHERE (CollectionType IN ('L','PL') AND
	            ISNULL(SubmittedBy,'') != '' AND ISNULL(LiquidationNumber,'') = '')
               OR CR.DocNumber = @LoanReference 
        "
        OnInit = "Connection_Init">
        <SelectParameters>
            <asp:Parameter Name="LoanReference" DefaultValue="   " />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsLoanRenewal" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" 
        SelectCommand="
        SELECT DocNumber, DocDate, LoanType, LoanCategory, Status, 
			   BizPartnerCode, Name, LoanAmount, ReferenceLN
          FROM Accounting.Loan 
         WHERE (ISNULL(SubmittedBy,'') != '' AND ISNULL(RenewalRef,'') = '')
               OR DocNumber = @LoanReference 
        "
        OnInit = "Connection_Init">
        <SelectParameters>
            <asp:Parameter Name="LoanReference" DefaultValue="   " />
        </SelectParameters>
    </asp:SqlDataSource>
    <!--#endregion-->
</body>
</html>