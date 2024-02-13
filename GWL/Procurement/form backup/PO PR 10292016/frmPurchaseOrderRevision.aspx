﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPurchaseOrderRevision.aspx.cs" Inherits="GWL.frmPurchaseOrderRevision" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <title></title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>

     <!--#region Region Javascript-->


        <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
#form1 {
height: 500px; /*Change this whenever needed*/
}

 .Entry {
 padding: 20px;
 margin: 10px auto;
 background: #FFF;
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
               delete (cp_delete);
               DeleteControl.Show();
           }
           if (s.cp_generated) {
               delete (s.cp_generated);
               console.log('autocalculate');
               autocalculate();
           }

       }

       var itemc; //variable required for lookup
       var currentColumn = null;
       var isSetTextRequired = false;
       var linecount = 1;
       function OnStartEditing(s, e) {//On start edit grid function     
           currentColumn = e.focusedColumn;
           var cellInfo = e.rowValues[e.focusedColumn.index];
           itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode"); //needed var for all lookups; this is where the lookups vary for
           //if (e.visibleIndex < 0) {//new row
           //    var linenumber = s.GetColumnByField("LineNumber");
           //    e.rowValues[linenumber.index].value = linecount++; // or any other default value
           //}
           var entry = getParameterByName('entry');
           if (entry == "V") {
               e.cancel = true; //this will made the gridview readonly
           }
           if (entry != "V")

           {
               if (e.focusedColumn.fieldName === "ItemCode") { //Check the column name
                   gl.GetInputElement().value = cellInfo.value; //Gets the column value
                   isSetTextRequired = true;
               }
               if (e.focusedColumn.fieldName === "ColorCode") {
                   gl2.GetInputElement().value = cellInfo.value;
               }
               if (e.focusedColumn.fieldName === "ClassCode") {
                   gl3.GetInputElement().value = cellInfo.value;
               }
               if (e.focusedColumn.fieldName === "SizeCode") {
                   gl4.GetInputElement().value = cellInfo.value;
               }
               if (e.focusedColumn.fieldName === "ItemCode" || e.focusedColumn.fieldName === "ColorCode" ||
                       e.focusedColumn.fieldName === "ClassCode" || e.focusedColumn.fieldName === "SizeCode" ||
                       e.focusedColumn.fieldName === "OldUnitCost" || e.focusedColumn.fieldName === "Unit" ||
                       e.focusedColumn.fieldName === "BaseQty" || e.focusedColumn.fieldName === "BaseQty" ||
                       e.focusedColumn.fieldName === "UnitCost" || 
                       e.focusedColumn.fieldName === "IsVat" || e.focusedColumn.fieldName === "VatCode")
               
                   {
                       e.cancel = true;
                   }
               }
           }

       function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
           var cellInfo = e.rowValues[currentColumn.index];
           if (currentColumn.fieldName === "ItemCode") {
               cellInfo.value = gl.GetValue();
               cellInfo.text = gl.GetText();
           }
           if (currentColumn.fieldName === "ColorCode") {
               cellInfo.value = gl2.GetValue();
               cellInfo.text = gl2.GetText();
           }
           if (currentColumn.fieldName === "ClassCode") {
               cellInfo.value = gl3.GetValue();
               cellInfo.text = gl3.GetText();
           }
           if (currentColumn.fieldName === "SizeCode") {
               cellInfo.value = gl4.GetValue();
               cellInfo.text = gl4.GetText();
           }
       }
       function autocalculate(s, e) {
           //console.log(txtNewUnitCost.GetValue());
           OnInitTrans();
           var orderqty = 0;
           var totalqty = 0;
          

           setTimeout(function () {
               for (var i = 0; i < gv1.GetVisibleRowsOnPage() ; i++) {
                   orderqty = gv1.batchEditApi.GetCellValue(i, "OrderQty");
                   totalqty += orderqty;
                   


               }
               txtQty.SetText(totalqty);
               

           }, 500);
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

       function getParameterByName(name) {
           name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
           var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
               results = regex.exec(location.search);
           return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
       }
       function OnCustomClick(s, e) {

           if (e.buttonID == "Details") {
               var itemcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode");
               var colorcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ColorCode");
               var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
               var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
               var unitbase = s.batchEditApi.GetCellValue(e.visibleIndex, "Unit");
               var fulldesc = s.batchEditApi.GetCellValue(e.visibleIndex, "FullDesc");
               var Warehouse = "";
  
               factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode
               + '&colorcode=' + colorcode + '&classcode=' + classcode + '&sizecode=' + sizecode +  '&Warehouse=' + Warehouse);

          


           }

           if (e.buttonID == "CountSheet") {
               CSheet.Show();
               var linenum = s.batchEditApi.GetCellValue(e.visibleIndex, "LineNumber");
               var docnumber = getParameterByName('docnumber');
               var transtype = getParameterByName('transtype');
               var entry = getParameterByName('entry');
               CSheet.SetContentUrl('../wms/frmCountSheet.aspx?entry=' + entry + '&docnumber=' + docnumber + '&transtype=' + transtype +
                    '&linenumber=' + linenum);
           }
           if (e.buttonID == "Delete") {
               gv1.DeleteRow(e.visibleIndex);
               autocalculate(s, e);
               console.log('test')
           }
           if (e.buttonID == "ViewTransaction") {

               //var url = window.location.pathname;

               //console.log(url);
               //str.substring(0, str.lastIndexOf("/"));

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
       function Generate(s, e) {
           var generate = confirm("Are you sure that you want to generate this PO?");
           if (generate) {
               cp.PerformCallback('Generate');
           }
       }

       function OnInitTrans(s, e) {

           var BizPartnerCode = glSupplierCode.GetText();


           factbox2.SetContentUrl('../FactBox/fbBizPartner.aspx?BizPartnerCode=' + BizPartnerCode);
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
                                <dx:ASPxLabel runat="server" Text="Purchase Order Revision" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
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


    <dx:ASPxPopupControl ID="popup2" runat="server" AllowDragging="True" AllowResize="True" ClientInstanceName="factbox2" CloseAction="None" 
        EnableViewState="False" HeaderText="BizPartner info" Height="207px" Width="245px" PopupHorizontalOffset="1085" PopupVerticalOffset="260"
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
        <ClientSideEvents CloseUp="function (s, e) { cp.PerformCallback('RefGrid') }" />
    </dx:ASPxPopupControl>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="641px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="565px" Width="850px" style="margin-left: -3px">
                       <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>

                          <%--<!--#region Region Header --> --%>
                            <%-- <!--#endregion --> --%>
                            
                          <%--<!--#region Region Details --> --%>
                            
                            <%-- <!--#endregion --> --%>
                            <dx:TabbedLayoutGroup>
                                <SettingsTabPages EnableClientSideAPI="True">
                                </SettingsTabPages>
                                <Items>
                                    <dx:LayoutGroup Caption="General" ColCount="2" Width="850px">
                                        <Items>
                                            <dx:LayoutItem Caption="Document Number:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDoc" runat="server" Width="170px" OnLoad="LookupLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Document Date:" RequiredMarkDisplayMode="Required">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtpDate" runat="server" Width="170px" OnLoad="Date_Load" OnInit="dtpDate_Init">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="PO DocNumber">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="gvPODoc" runat="server" Width="170px" AutoGenerateColumns="False" DataSourceID="PODoc" OnLoad="LookupLoad" TextFormatString="{0}"  KeyFieldName="DocNumber">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                            <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>

                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="DocNumber" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="SupplierCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Status" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Remarks" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                            </Columns>

                                                            <ClientSideEvents Validation="OnValidation" ValueChanged="function(s,e){cp.PerformCallback('PO')} "/>
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                                       <dx:LayoutItem Caption="Target Delivery Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtpTarget" runat="server" OnInit="dtpTarget_Init" OnLoad="Date_Load" Width="170px">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                       
                                            <dx:LayoutItem Caption="Supplier Code">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="glSupplierCode" runat="server" ClientInstanceName="glSupplierCode" DataSourceID="sdsSupplier" KeyFieldName="SupplierCode" OnLoad="LookupLoad" TextFormatString="{0}" Width="170px">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Commitment Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtpComm" runat="server" Width="170px" OnInit="dtpComm_Init" OnLoad="Date_Load">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                       
                                            <dx:LayoutItem Caption="Total Qty">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtQty" runat="server" Width="170px" ReadOnly="True"  ClientInstanceName="txtQty" >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                                           <dx:LayoutItem Caption="Cancellation Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtpCancel" runat="server" Width="170px" OnInit="dtpCancel_Init" OnLoad="Date_Load">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                           <%-- <dx:LayoutItem Caption="Remarks">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtRemarks" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>--%>
                                                 <dx:LayoutItem Caption="Remarks">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxMemo ID="txtRemarks" runat="server" Width="170px" Height="50" OnLoad="memoremarks_Load">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                              <dx:LayoutItem Caption="Reference:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtPQ" runat="server" OnLoad="TextboxLoad" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="" ClientVisible="False" Name="Genereatebtn">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxButton ID="Generatebtn" runat="server" AutoPostBack="False" CausesValidation="False" Text="Generate" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="Generate" />
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                     <dx:LayoutGroup Caption="User Defined" ColCount="2">
                                        <Items>
                                          <dx:LayoutItem Caption="Field 1:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server"  OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>
                                          <dx:LayoutItem Caption="Field 6:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server"  OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>
                                          <dx:LayoutItem Caption="Field 2:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server"   OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>
                                               <dx:LayoutItem Caption="Field 7:" >
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
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" OnLoad="TextboxLoad" >
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
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server" OnLoad="TextboxLoad" >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 5:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server" OnLoad="TextboxLoad" >
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
                                                        <dx:ASPxTextBox ID="txtHAddedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Added Date:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHAddedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited By:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHLastEditedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited Date:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHLastEditedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Submitted By:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Submitted Date:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Cancelled By:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHCancelledBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Cancelled Date:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHCancelledDate" runat="server" Width="170px" ReadOnly="true">
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
                                             <%--   </Items>
                                            </dx:LayoutGroup>--%>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:TabbedLayoutGroup>
                            
                            <dx:LayoutGroup Caption="Purchase Order Detail">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" Width="747px" 
                                                    OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gv1" 
                                                    OnRowValidating="grid_RowValidating" OnBatchUpdate="gv1_BatchUpdate" KeyFieldName="DocNumber;LineNumber">
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="OnInitTrans" BatchEditRowValidating="Grid_BatchEditRowValidating"
                                                        BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                                                    <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                                    <SettingsPager PageSize="5"/> 
                                                            <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" ShowFooter="True"  /> 
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="DocNumber" Visible="true" Width="0px"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="3" Caption="Line" ReadOnly="True" Width="50px" Visible="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" VisibleIndex="4" Width="100px" Name="glItemCode" ReadOnly="True">
                                                             <EditItemTemplate>
                                                                <dx:ASPxGridLookup ID="glItemCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                    DataSourceID="Masterfileitem" KeyFieldName="ItemCode" ClientInstanceName="gl" TextFormatString="{0}" Width="80px">
                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                            AllowSelectSingleRowOnly="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" ReadOnly="True" VisibleIndex="0" />
                                                                        <dx:GridViewDataTextColumn FieldName="FullDesc" ReadOnly="True" VisibleIndex="1" />
                                                                    </Columns>
                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" CloseUp="gridLookup_CloseUp" />
                                                                </dx:ASPxGridLookup>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" VisibleIndex="5" Width="80px" Caption="Color" ReadOnly="True">   
                                                                                                                        <EditItemTemplate>
                                                                <dx:ASPxGridLookup ID="glColorCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                    KeyFieldName="ColorCode" ClientInstanceName="gl2" TextFormatString="{0}" Width="80px" OnInit="lookup_Init">
                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" ReadOnly="True" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown"
                                                                        DropDown="function dropdown(s, e){
                                                                        gl2.GetGridView().PerformCallback('ColorCode' + '|' + itemc + '|' + s.GetInputElement().value);
                                                                        }" CloseUp="gridLookup_CloseUp"/>
                                                                </dx:ASPxGridLookup>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" VisibleIndex="6" Width="80px" Name="glClassCode" Caption="Class" ReadOnly="True">
                                                             <EditItemTemplate>
                                                                <dx:ASPxGridLookup ID="glClassCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" OnInit="lookup_Init"
                                                                KeyFieldName="ClassCode" ClientInstanceName="gl3" TextFormatString="{0}" Width="80px" >
                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                            AllowSelectSingleRowOnly="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" ReadOnly="True" VisibleIndex="0" />
                                                                    </Columns>
                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" 
                                                                        DropDown="function dropdown(s, e){
                                                                        gl3.GetGridView().PerformCallback('ClassCode' + '|' + itemc + '|' + s.GetInputElement().value);
                                                                        }" CloseUp="gridLookup_CloseUp"/>
                                                                </dx:ASPxGridLookup>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" VisibleIndex="7" Width="80px" Name ="glSizeCode" Caption="Size" ReadOnly="True">
 <EditItemTemplate>
                                                                <dx:ASPxGridLookup ID="glSizeCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" OnInit="lookup_Init"
                                                                KeyFieldName="SizeCode" ClientInstanceName="gl4" TextFormatString="{0}" Width="80px" >
                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                            AllowSelectSingleRowOnly="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" ReadOnly="True" VisibleIndex="0" />
                                                                    </Columns>
                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" 
                                                                        DropDown="function dropdown(s, e){
                                                                        gl4.GetGridView().PerformCallback('SizeCode' + '|' + itemc + '|' + s.GetInputElement().value);
                                                                        }" CloseUp="gridLookup_CloseUp"/>
                                                                </dx:ASPxGridLookup>
                                                            </EditItemTemplate>
                                                          
                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Unit" ShowInCustomizationForm="True" VisibleIndex="11"  UnboundType="Decimal" FieldName="Unit"> 
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="StatusCode" ShowInCustomizationForm="True" VisibleIndex="12" UnboundType="String" ReadOnly="true" FieldName="StatusCode">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image"  ShowInCustomizationForm="True"  VisibleIndex="0" Width="60px">
                                                                                                               <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="Details">
                                                               <Image IconID="support_info_16x16"></Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                       
                                                        </CustomButtons>
                                                            
                                                             </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="DocNumber" Name="IncomingDocNumber" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="IncomingDocNumber" UnboundType="String" Visible="False" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="OldUnitCost" ShowInCustomizationForm="True" VisibleIndex="8" UnboundType="String" FieldName="OldUnitCost">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field1" Name="Field1" ShowInCustomizationForm="True" VisibleIndex="18" FieldName="Field1" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field2" Name="Field2" ShowInCustomizationForm="True" VisibleIndex="19" FieldName="Field2" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field3" Name="Field3" ShowInCustomizationForm="True" VisibleIndex="20" FieldName="Field3" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field4" Name="Field4" ShowInCustomizationForm="True" VisibleIndex="21" FieldName="Field4" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field5" Name="Field5" ShowInCustomizationForm="True" VisibleIndex="22" FieldName="Field5" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field6" Name="Field6" ShowInCustomizationForm="True" VisibleIndex="23" FieldName="Field6" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field7" Name="Field7" ShowInCustomizationForm="True" VisibleIndex="24" FieldName="Field7" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field8" Name="Field8" ShowInCustomizationForm="True" VisibleIndex="25" FieldName="Field8" UnboundType="String" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field9" Name="Field9" ShowInCustomizationForm="True" VisibleIndex="26" FieldName="Field9" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="BaseQty" ShowInCustomizationForm="True" VisibleIndex="14" FieldName="BaseQty">
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataCheckColumn Caption="IsVAT" ShowInCustomizationForm="True" VisibleIndex="16" FieldName="IsVAT" ReadOnly="true">
                                                        </dx:GridViewDataCheckColumn>
                                                        <dx:GridViewDataTextColumn Caption="VATCode" ShowInCustomizationForm="True" VisibleIndex="17" FieldName="VATCode" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataSpinEditColumn Caption="OrderQty" FieldName="OrderQty" ShowInCustomizationForm="True" VisibleIndex="10" Width="70px" >
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="g" MinValue="0" MaxValue="9999999999" AllowMouseWheel="false" >
                                                                <ClientSideEvents ValueChanged="autocalculate" />
                                                                <SpinButtons ShowIncrementButtons ="false" />
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn Caption="UnitCost" FieldName="UnitCost" ShowInCustomizationForm="True" Width="0" UnboundType="String" VisibleIndex="13">
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="g" MinValue="0" MaxValue="9999999999" AllowMouseWheel="false" >
                                                                <SpinButtons ShowIncrementButtons ="false" />
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>

<%--                                                        <dx:GridViewDataSpinEditColumn Caption="AverageCost" FieldName="AverageCost" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="g" MinValue="0" MaxValue="9999999999">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>--%>
                                                        <dx:GridViewDataSpinEditColumn Caption="NewUnitCost" FieldName="NewUnitCost" ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="9" Width="80px">
                                                            <PropertiesSpinEdit Increment="0" DisplayFormatString="g" MinValue="0" MaxValue="9999999999" AllowMouseWheel="false">
                                                            <SpinButtons ShowIncrementButtons ="false" />
                                                            </PropertiesSpinEdit>
                                                            
                                                        </dx:GridViewDataSpinEditColumn>

                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="InputBaseQty" SummaryType="Sum" ShowInColumn="InputBaseQty" ShowInGroupFooterColumn="InputBaseQty" />
                                                        <dx:ASPxSummaryItem FieldName="BulkQty" ShowInColumn="Bulk" ShowInGroupFooterColumn="Bulk" SummaryType="Sum" />
                                                    </TotalSummary>
                                                    <GroupSummary>
                                                        <dx:ASPxSummaryItem ShowInColumn="InputBaseQty" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem ShowInColumn="BulkQty" SummaryType="Sum" />
                                                    </GroupSummary>
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                        <SettingsEditing Mode="Batch" />
                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="300" ShowFooter="True" />
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
                         <td><dx:ASPxButton ID="Ok" runat="server" Text="Ok" AutoPostBack="False" UseSubmitBehavior="false">
                             <ClientSideEvents Click="function (s, e){  cp.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
                             </dx:ASPxButton>
                         <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel">
                             <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                             </dx:ASPxButton> 
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>

    <!--#region Region Datasource-->
    <%--<!--#region Region Header --> --%>
    <asp:ObjectDataSource ID="odsHeader" runat="server" SelectMethod="getdata" TypeName="Entity.PurchaseOrderRevision" DataObjectTypeName="Entity.PurchaseOrderRevision" InsertMethod="InsertData" UpdateMethod="UpdateData">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="DocNumber" QueryStringField="docnumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDetail" runat="server" SelectMethod="getdetail" TypeName="Entity.PurchaseOrderRevision+PurchaseOrderRevisionDetail" DataObjectTypeName="Entity.PurchaseOrderRevision+PurchaseOrderRevisionDetail" DeleteMethod="DeletePurchaseOrderRevisionDetail" InsertMethod="AddPurchaseOrderRevisionDetail" UpdateMethod="UpdatePurchaseOrderRevisionDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsReference" runat="server" SelectMethod="getreftransaction" TypeName="Entity.PurchaseOrderRevision+RefTransaction" >
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM  Procurement.PurchaseOrderRevisionDetail where DocNumber  is null " OnInit = "Connection_Init">
  
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="Masterfileitem" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] where isnull(IsInactive,0)=0" OnInit = "Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Masterfileitemdetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT [ItemCode], [ColorCode], [ClassCode],[SizeCode] FROM Masterfile.[ItemDetail] where isnull(IsInactive,0)=0" OnInit = "Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="PODoc" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="Select DocNumber, SupplierCode, DocDate, Status, Remarks from Procurement.PurchaseOrder where isnull(SubmittedBy,'') !='' and Status = 'N'" OnInit = "Connection_Init"></asp:SqlDataSource>
  <asp:SqlDataSource ID="Masterfilebiz" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BizPartnerCode, Name FROM Masterfile.BizPartner WHERE (ISNULL(IsInactive, 0) = 0)" OnInit = "Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Masterfilebizcustomer" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BizPartnerCode, Name FROM Masterfile.[BizPartner] where isnull(IsInactive,0)=0 and IsCustomer='1'" OnInit = "Connection_Init"></asp:SqlDataSource>
      <asp:SqlDataSource ID="sdsPODetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT A.DocNumber,LineNumber,ItemCode,ColorCode,ClassCode,SizeCode,UnitCost as OldUnitCost,UnitCost as NewUnitCost,OrderQty,Unit,StatusCode,UnitCost,BaseQty,IsVAT,VATCode,A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7,A.Field8,A.Field9 FROM Procurement.PurchaseOrderDetail A  INNER JOIN Procurement.PurchaseOrder B ON A.DocNumber = B.DocNumber WHERE ISNULL(SubmittedBy,'')!=''" OnInit = "Connection_Init"></asp:SqlDataSource>
          <asp:SqlDataSource ID="sdsSupplier" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select SupplierCode,Name from Masterfile.BPSupplierInfo where ISNULL(IsInactive,0) = 0" OnInit = "Connection_Init"></asp:SqlDataSource>

     <!--#endregion-->
</body>
</html>


