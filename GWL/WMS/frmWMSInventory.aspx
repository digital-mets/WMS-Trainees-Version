﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmWMSInventory.aspx.cs" Inherits="GWL.frmWMSInventory" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <title>WMS Inventory</title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
            height: 300px; /*Change this wheever needed*/
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

var module = getParameterByName("transtype");
        var id = getParameterByName("docnumber");
        var entry = getParameterByName("entry");

        $(document).ready(function () {
            PerfStart(module, entry, id);
        });

function OnValidation(s, e) { //Validation function for header controls (Set this for each header controls)
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

         if (btnmode == "Delete") {
             cp.PerformCallback("Delete");
         }
     }

     function OnConfirm(s, e) {//function upon saving entry
         if (e.requestTriggerID === "cp")//disables confirmation message upon saving.
             e.cancel = true;
     }

     function gridView_EndCallback(s, e) {//End callback function if (s.cp_success) {
         if (s.cp_success) {
             //alert(s.cp_valmsg);
             alert(s.cp_message);
             //delete (s.cp_valmsg);
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
     }

     var itemc; //variable required for lookup
     var currentColumn = null;
     var isSetTextRequired = false;
     var linecount = 1;

     
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
         if (gv1.batchEditApi[moveActionName]()) {
             ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
         }
     }

     function gridLookup_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
         var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
         if (keyCode == ASPxKey.Enter)
             gv1.batchEditApi.EndEdit();
         //ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
     }

     function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
         gv1.batchEditApi.EndEdit();
     }

     //validation
     function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields)
         for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
             var column = s.GetColumn(i);
             if (column != s.GetColumn(6) && column != s.GetColumn(1) && column != s.GetColumn(7) && column != s.GetColumn(5) && column != s.GetColumn(8) && column != s.GetColumn(9) && column != s.GetColumn(10) && column != s.GetColumn(11) && column != s.GetColumn(12) && column != s.GetColumn(13) && column != s.GetColumn(14) && column != s.GetColumn(15) && column != s.GetColumn(16) && column != s.GetColumn(17) && column != s.GetColumn(18) && column != s.GetColumn(19) && column != s.GetColumn(20) && column != s.GetColumn(21) && column != s.GetColumn(22) && column != s.GetColumn(23) && column != s.GetColumn(24) && column != s.GetColumn(13)) {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
                 var cellValidationInfo = e.validationInfo[column.index];
                 if (!cellValidationInfo) continue;
                 var value = cellValidationInfo.value;
                 if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "") {
                     cellValidationInfo.isValid = false;
                     cellValidationInfo.errorText = column.fieldName + " is required";
                     isValid = false;
                 }
                 else {
                     isValid = true;
                 }
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
         //gv1.SetWidth(width - 120);
     }


    </script>
    
    <!--#endregion-->
</head>
<body style="height: 910px">
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
<form id="form1" runat="server" class="Entry">
    <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel runat="server" Text="WMS Inventory" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
   
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="586px" Height="220px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="form1_layout" runat="server" Height="269px" Width="1052px" style="margin-left: -20px">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>


                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="General" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Business Partner">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="gvBizPartner" runat="server" DataSourceID="Masterfilebiz" KeyFieldName="BizPartnerCode" OnLoad="LookupLoad" Width="170px" TextFormatString="{0}">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                            <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Unit Of Measure">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="gvUnitOfMeasure" runat="server" DataSourceID="UnitOfMeasure" KeyFieldName="UnitOfMeasure" OnLoad="LookupLoad" TextFormatString="{0}" Width="170px">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                            <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Warehouse Code">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="gvWarehouse" runat="server" DataSourceID="sdsWarehouse" Width="170px" KeyFieldName="WarehouseCode" TextFormatString="{0}" OnLoad="LookupLoad">
                                                            <GridViewProperties Settings-ShowFilterRow="true">
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                            </GridViewProperties>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Prod. Number">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtProdNum" runat="server" ReadOnly="True" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Service Type">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="gvServiceType" runat="server" DataSourceID="ServiceType" KeyFieldName="ServiceType" OnLoad="LookupLoad" TextFormatString="{0}" Width="170px">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                            <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Pallet Ending Bal.">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxSpinEdit ID="spPallet" runat="server" Number="0" MinValue="0" MaxValue="999999999" Width="170px" SpinButtons-ShowIncrementButtons="false" OnLoad="SpinEdit_Load" AllowMouseWheel="False">
                                                        <SpinButtons ShowIncrementButtons ="false" />
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="As Of Date">
                                                    <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtpAsOfDate" runat="server" OnInit="dtpAsOfDate_Init" Width="170px" OnLoad="Date_Load">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Ending Balance">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxSpinEdit ID="spEndBal" runat="server" MaxValue="999999999" Number="0" Width="170px" SpinButtons-ShowIncrementButtons="false" OnLoad="SpinEdit_Load" AllowMouseWheel="False">
                                                       <SpinButtons ShowIncrementButtons ="false" />
                                                             </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="RecordId" Visible="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtId" runat="server" OnLoad="TextboxLoad" ReadOnly="True" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                               <dx:LayoutItem Caption="RR Date">
                                                    <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="speRRDate" runat="server" OnInit="dtpAsOfDate_Init" Width="170px" OnLoad="Date_Load">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="User Defined" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Field1" Name="txtHField1">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field2" Name="txtHField2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field3" Name="txtHField3">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server"  OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field4" Name="txtHField4">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField4" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field5" Name="txtHField5">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field6" Name="txtHField6">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field7" Name="txtHField7">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server"  OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field8" Name="txtHField8">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField8" runat="server"  OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field9" Name="txtHField9">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server"  OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Audit Trail" ColCount="2" ColSpan="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Added By:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedBy" runat="server" ColCount="1" ReadOnly="True" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Added Date:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedDate" runat="server" ColCount="1" ReadOnly="True" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited By:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedBy" runat="server" ColCount="1" ReadOnly="True" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited Date:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedDate" runat="server" ColCount="1" ReadOnly="True" Width="170px">
                                                        </dx:ASPxTextBox>
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
                            <td>
                                <dx:ASPxButton ID="Ok" runat="server" Text="Ok" AutoPostBack="False" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function (s, e){  cp.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
    <!--#region Region Datasource-->
    <%--<!--#region Region Header --> --%>
    <asp:ObjectDataSource ID="odsHeader" runat="server" DataObjectTypeName="Entity.WMSInventory" InsertMethod="InsertData" SelectMethod="getdata" TypeName="Entity.WMSInventory" UpdateMethod="UpdateData">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="DocNumber" SessionField="DocNumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDetail" runat="server" DataObjectTypeName="Entity.WMSInventory+TransactionDetail" SelectMethod="getdetail" UpdateMethod="UpdateTransactionDetail" TypeName="Entity.Transaction+TransactionDetail" DeleteMethod="DeleteTransactionDetail" InsertMethod="AddTransactionDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="ServiceType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT ServiceType,Description from Masterfile.WMSServiceType where ISNULL(IsInActive,0)=0" OnInit = "Connection_init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="UnitOfMeasure" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT UnitOfMeasure,Description from Masterfile.WMSUnitOfMeasure" OnInit = "Connection_init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Masterfilebiz" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BizPartnerCode, Name FROM Masterfile.[BPCustomerInfo] where isnull(IsInactive,0)=0" OnInit = "Connection_init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsWarehouse" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT WarehouseCode, Description FROM Masterfile.[Warehouse] WHERE ISNULL([IsInactive],0) = 0" OnInit = "Connection_init">
    </asp:SqlDataSource>
    <!--#endregion-->
    </body>
</html>


