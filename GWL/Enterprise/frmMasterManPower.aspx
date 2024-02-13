﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMasterManPower.aspx.cs" Inherits="GWL.frmMasterManPower" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <title></title>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
            height: 800px; /*Change this whenever needed*/
        }

        .Entry {
            /*width: 1280px;*/ /*Change this whenever needed*/
            padding: 20px;
            margin: 10px auto;
            background: #FFF;
            /*border-radius: 10px;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            -moz-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            -webkit-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);*/
        }

        .dxeButtonEditSys input,
        .dxeTextBoxSys input{
            text-transform:uppercase;
        }

        .pnl-content
        {
            text-align: right;
        }
    </style>
    <!--#endregion-->
    <script>
        var gridIsValid = true;
        var isValid = true;
        var entry = getParameterByName('entry');

        //////////////////////////////////////////////////////////////////////
        //                       Standard Functions                         //
        //////////////////////////////////////////////////////////////////////
        function getParameterByName(name)
        {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

               function getParameterByName(name) {
           name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
           var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
               results = regex.exec(location.search);
           return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
       }

var module = getParameterByName("transtype");
        var id = getParameterByName("docnumber");
        var entry = getParameterByName("entry");

        $(document).ready(function () {
            PerfStart(module, entry, id);
        });

function OnValidation(s, e) { //Validation function for header controls (Set this for each header controls)
            if ((s.GetText() == "" || e.value == "" || e.value == null))
            {
                isValid = false;
            }
        }

        function OnUpdateClick(s, e) {
            gvDetail.batchEditApi.EndEdit();

            gridIsValid = true;
            gvDetail.batchEditApi.ValidateRows();
            if (!gridIsValid) { isValid = false; gridIsValid = true; }

            var btnmode = btn.GetText(); //gets text of button

            //check if there's no error then proceed to callback
            if (isValid || btnmode == "Close") {
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
                isValid = true;
                alert('Please check all the fields!');
            }

            if (btnmode == "Delete") {
                cp.PerformCallback("Delete");
            }
        }

        function OnEndCallback(s, e) {//End callback function if (s.cp_success) {
            if (s.cp_success) {
                alert(s.cp_message);
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
                delete (s.cp_delete);
                DeleteControl.Show();
            }
        }

        //////////////////////////////////////////////////////////////////////
        //                 Grid Related Events / Functions                  //
        //////////////////////////////////////////////////////////////////////
        var currentColumn = null;
        var isSetTextRequired = false;

        function OnConfirm(s, e) {//function upon saving entry
            if (e.requestTriggerID == "cp")//disables confirmation message upon saving.
                e.cancel = true;
        }

        function OnStartEditing(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];
            if (entry == "V" || entry == "D") {
                e.cancel = true; //this will made the gridview readonly
            }
            if (entry != "V" && entry != "D") {
                if (e.focusedColumn.fieldName == "CompanyCode") { //Check the column name
                    glCompanyCode.GetInputElement().value = cellInfo.value; //Gets the column value
                    isSetTextRequired = true;
                    index = e.visibleIndex;
                }
            }
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
            gvDetail.SetWidth(width - 120);
        }

        //////////////////////////////////////////////////////////////////////
        //                  Grid with Columns having Lookupgrid             //
        //////////////////////////////////////////////////////////////////////
        function gridLookup_DropDown(s, e) {
            if (isSetTextRequired) {//Sets the text during lookup for item code
                s.SetText(s.GetInputElement().value);
                isSetTextRequired = false;
            }
        }

        function gridLookup_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode == 13)
                gvDetail.batchEditApi.EndEdit();
        }

        function gridLookup_KeyDown(s, e) { //Allows tabbing between gridlookup on details
            isSetTextRequired = false;
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode !== 9) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (gvDetail.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }
        }

        function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
            gvDetail.batchEditApi.EndEdit();
        }

        function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
            var cellInfo = e.rowValues[currentColumn.index];
            if (currentColumn.fieldName == "CompanyCode") {
                cellInfo.value = glCompanyCode.GetValue();
                cellInfo.text = glCompanyCode.GetText();
            }
            else if (currentColumn.fieldName == "ManCom" 
                     || currentColumn.fieldName == "ExeCom"
                     || currentColumn.fieldName == "RegularStaff"
                     || currentColumn.fieldName == "TemporaryStaff")
            {
                var OrigValue = s.batchEditApi.GetCellValue(e.visibleIndex, currentColumn.fieldName);
                var NewValue = e.rowValues[(s.GetColumnByField(currentColumn.fieldName).index)].value;
                var total = s.batchEditApi.GetCellValue(e.visibleIndex, "Total") + NewValue - OrigValue;
                s.batchEditApi.SetCellValue(e.visibleIndex, "Total", total);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //                         Customized Validation                    //
        //////////////////////////////////////////////////////////////////////
        function ValidateYear(s, e) { // Validation of Year
            e.isValid = false;
            if (e.value < 1900) {
                isValid = false;
            }
            else {
                e.isValid = true;
            }
        }

        // Client side validation of gvDetail
        function OnRowValidating(s, e) {
            for (var i = 0; i < gvDetail.GetColumnsCount() ; i++) {
                var column = s.GetColumn(i);
                var cellValidationInfo = e.validationInfo[column.index];

                if (!cellValidationInfo) continue;      // (only visible fields)

                var value = cellValidationInfo.value;

                if ((column.fieldName == "ExeCom" || column.fieldName == "ManCom" 
                     || column.fieldName == "RegularStaff" || column.fieldName == "TemporaryStaff") && value < 0) {
                    cellValidationInfo.isValid = false;
                    cellValidationInfo.errorText = "Please specify a value not less than zero";
                    gridIsValid = false;
                }
            }
        }
        //////////////////////////////////////////////////////////////////////
    </script>
    <!--#endregion-->
</head>

<body style="height: 565px">
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <form id="form1" runat="server" class="Entry">
        <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel runat="server" Text="Master Manpower" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="850px" Height="565px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">

                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="565px" Width="850px" style="margin-left: -3px" ClientInstanceName="frmlayout">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                          <%--<!--#region Region Header --> --%>
                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="Header" ColCount="1">
                                        <Items>
                                            <dx:LayoutItem Caption="Year">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxSpinEdit ID="txtYear" runat="server"
                                                            DisplayFormatString="0" HorizontalAlign="Right" NullText="0" Number="0" 
                                                            OnLoad="SpinEdit_Load" Width="100px">
                                                            <ClientSideEvents Validation="ValidateYear" /> 
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Please sepcify a valid year">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink" />
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                           </dx:TabbedLayoutGroup>
                           <%-- <!--#endregion --> --%>
                            
                           <%--<!--#region Region Details --> --%>
                           <dx:LayoutGroup Caption="Detail" Width="1px">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail"
                                                    runat="server" AutoGenerateColumns="False" 
                                                    Width="650" KeyFieldName="RecordID"
                                                    OnCommandButtonInitialize="gv_CommandButtonInitialize" 
                                                    OnCellEditorInitialize="gv_CellEditorInitialize">
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" BatchEditRowValidating="OnRowValidating"
                                                        BatchEditStartEditing="OnStartEditing" Init="OnInitTrans" BatchEditEndEditing="OnEndEditing"/>
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="80" VerticalScrollableHeight="300" /> 
                                                    <SettingsBehavior AllowSort="false"></SettingsBehavior>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowDeleteButton="True" ShowInCustomizationForm="True" 
                                                            ShowNewButtonInHeader="True" VisibleIndex="0" Width="30px">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CompanyCode" VisibleIndex="1" Width="105px" Name="CompanyCode">
                                                             <EditItemTemplate>
                                                                <dx:ASPxGridLookup ID="glCompanyCode" runat="server" Width="104px" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                    DataSourceID="sdsCompanyCode" KeyFieldName="CompanyCode" ClientInstanceName="glCompanyCode" 
                                                                    TextFormatString="{0}" OnLoad="gv_LookupLoad">
                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="CompanyCode" ReadOnly="True" VisibleIndex="0" />
                                                                        <dx:GridViewDataTextColumn FieldName="CompanyName" ReadOnly="True" VisibleIndex="1" />
                                                                    </Columns>
                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" 
                                                                        DropDown="gridLookup_DropDown" CloseUp="gridLookup_CloseUp" />
                                                                </dx:ASPxGridLookup>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="ExeCom" ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="#,0" NumberFormat="Custom">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="ManCom" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="#,0" NumberFormat="Custom">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="RegularStaff" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="#,0" NumberFormat="Custom">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TemporaryStaff" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="#,0" NumberFormat="Custom">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Total" UnboundType="Decimal" ReadOnly="true" 
                                                            UnboundExpression="[ExeCom]+[ManCom]+[RegularStaff]+[TemporaryStaff]">
                                                            <PropertiesTextEdit DisplayFormatString="#,0" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <%-- <!--#endregion --> --%>
                               
                        </Items>
                    </dx:ASPxFormLayout>
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
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
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
                             </dx:ASPxButton></td>
                         <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                             <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                             </dx:ASPxButton> </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
    <!--#region Region Datasource-->
    <%-- put all datasource codeblock here --%>

    <asp:ObjectDataSource ID="odsDetail" runat="server" DataObjectTypeName="Entity.MasterManPower" 
        TypeName="Entity.MasterManPower" SelectMethod="GetRecords" InsertMethod="InsertData" 
        UpdateMethod="UpdateData" DeleteMethod="DeleteData"
        OnInserting="odsDetail_OnInserting" OnUpdating="odsDetail_OnUpdating">
        <SelectParameters>
            <asp:QueryStringParameter Name="_Year" Type="String"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:SqlDataSource ID="sdsDetail" runat="server" 
        SelectCommand="SELECT * FROM Enterprise.ManPower WHERE RecordID IS NULL" 
        OnInit="Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsCompanyCode" runat="server" 
        SelectCommand="SELECT CompanyCode, CompanyName FROM Enterprise.Company WHERE IsNull(IsInactive,0) = 0" 
        OnInit="Connection_Init">
    </asp:SqlDataSource>

    <!--#endregion-->
</body>
</html>

