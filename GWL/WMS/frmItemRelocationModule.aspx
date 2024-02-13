﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmItemRelocationModule.aspx.cs" Inherits="GWL.frmItemRelocationModule" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <title></title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
            height: 875px; /*Change this whenever needed*/
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
       var isValid = false;
       var counterror = 0;

       function getParameterByName(name) {
           name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
           var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
               results = regex.exec(location.search);
           return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
       }

       var entry = getParameterByName('entry');

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
                   gv1.PerformCallback("Add");
               }
               else if (btnmode == "Update") {
                   gv1.PerformCallback("Update");
                   console.log('here');
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
           console.log(e);
           if (e.requestTriggerID === "frmlayout1_ASPxFormLayout2_gv1")//disables confirmation message upon saving.
               e.cancel = true;
       }

       function gridView_EndCallback(s, e) {//End callback function if (s.cp_success) {
           if (s.cp_success) {
               alert(s.cp_valmsg);
               alert(s.cp_message);
               delete (s.cp_valmsg);
               delete (s.cp_success);//deletes cache variables' data
               delete (s.cp_message);

           }
           if (s.cp_error != null) {
               alert(s.cp_error);
               delete (s.cp_error);
           }
           if (s.cp_close) {
               if (s.cp_message != null) {
                   alert(s.cp_message);
                   delete (s.cp_message);
               }
               if (glcheck.GetChecked()) {
                   delete (s.cp_close);
                   window.location.reload();
               }
               else {
                   delete (s.cp_close);
                   window.close();//close window if callback successful
               }
           }
           if (s.cp_delete) {
               delete (s.cp_delete);
               DeleteControl.Show();
           }
       }
       var index;
       var index2;
       var valchange = false;
       var valchange2;
       var val;
       var temp;
       var bulkqty;
       var closing;
       var itemc; //variable required for lookup
       var iteme;
       var currentColumn = null;
       var isSetTextRequired = false;
       var linecount = 1;
       var isbusy;
       var editorobj;
       function OnStartEditing(s, e) {//On start edit grid function     
           currentColumn = e.focusedColumn;
           var cellInfo = e.rowValues[e.focusedColumn.index];
           itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode"); //needed var for all lookups; this is where the lookups vary for
           bulkqty = s.batchEditApi.GetCellValue(e.visibleIndex, "BulkQty");
           //if (e.visibleIndex < 0) {//new row
           //    var linenumber = s.GetColumnByField("LineNumber");
           //    e.rowValues[linenumber.index].value = linecount++; // or any other default value
           //}
           index = e.visibleIndex;
           editorobj = e;
           if (entry == "V") {
               e.cancel = true; //this will made the gridview readonly
           }

           if (entry != "V") {

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
               if (e.focusedColumn.fieldName === "FromLoc") {
                   glLocation.GetInputElement().value = cellInfo.value;
               }
               if (e.focusedColumn.fieldName === "ToLoc") {
                   gl6.GetInputElement().value = cellInfo.value;
               }

               if (e.focusedColumn.fieldName === "Qty") {
                   if (isbusy == true) {
                       e.cancel = true;
                   }
               }

           }
       }

       function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
           var cellInfo = e.rowValues[currentColumn.index];
           if (currentColumn.fieldName === "ItemCode") {
               cellInfo.value = gl.GetValue();
               cellInfo.text = gl.GetText();
               iteme = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode");
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
           if (currentColumn.fieldName === "FromLoc") {
               cellInfo.value = glLocation.GetValue();
               cellInfo.text = glLocation.GetText();
           }
           if (currentColumn.fieldName === "ToLoc") {
               cellInfo.value = gl6.GetValue();
               cellInfo.text = gl6.GetText();
           }
           if (currentColumn.fieldName === "BulkUnit") {
               cellInfo.value = glBulkUnit.GetValue();
               cellInfo.text = glBulkUnit.GetText();
           }

       }
       var val;
       var temp;
       function GridEnd(s, e) {
           val = s.GetGridView().cp_codes;
           if (val != null) {
               temp = val.split(';');
           }
           if (closing == true) {
               gv1.batchEditApi.EndEdit();
           }


           if (valchange2) {
               valchange2 = false;
               for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
                   var column = gv1.GetColumn(i);
                   if (column.visible == false || column.fieldName == undefined)
                       continue;
                   ProcessCells(0, index, column, gv1);
               }
               gv1.batchEditApi.EndEdit();
           }

           if (valchange) {
               valchange = false;
               closing = false;
               for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
                   var column = gv1.GetColumn(i);
                   if (column.visible == false || column.fieldName == undefined)
                       continue;
                   ProcessCells2(0, index, column, gv1);
               }
           }
           loader.Hide();
       }

       function lookup(s, e) {
           //setTimeout(function () {
           //    gl.GetGridView().PerformCallback();
           //}, 500);
           //setTimeout(function () {
           if (isSetTextRequired) {//Sets the text during lookup for item code
               s.SetText(s.GetInputElement().value);
               isSetTextRequired = false;
            }
           //}, 500);
       }

       function GridEnd2(s, e) {
           //if (isSetTextRequired) {//Sets the text during lookup for item code
           //    if (itemc == null) {
           //        console.log('itemc:'+itemc);
           //        s.SetText(null);
           //    }
           //    isSetTextRequired = false;
           //}
       }

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
           if (temp[4] == null || temp[4] == "") {
               temp[4] = 0;
           }
           if (selectedIndex == 0) {
               
               if (column.fieldName == "ColorCode") {
                   gv1.batchEditApi.SetCellValue(index, column.fieldName, temp[0]);
               }
               if (column.fieldName == "ClassCode") {
                   gv1.batchEditApi.SetCellValue(index, column.fieldName, temp[1]);
               }
               if (column.fieldName == "SizeCode") {
                   gv1.batchEditApi.SetCellValue(index, column.fieldName, temp[2]);
               }
               if (column.fieldName == "Qty") {
                   gv1.batchEditApi.SetCellValue(index, column.fieldName, temp[4]);
               }
           }
       }

       function ProcessCells2(selectedIndex, focused, column, s) {//Auto calculate qty function :D
           if (val == null) {
               val = ";";
               temp = val.split(';');
           }
           if (temp[0] == null) {
               temp[0] = 0;
           }
           
           if (selectedIndex == 0) {
               if (column.fieldName == "Qty") {
                   gv1.batchEditApi.SetCellValue(index, column.fieldName, temp[0]);
                   isbusy = false;
                   gv1.batchEditApi.StartEdit(index, gv1.GetColumnByField("Qty").index);
               }
           }
       }

       

       //var preventEndEditOnLostFocus = false;
       function gridLookup_KeyDown(s, e) { //Allows tabbing between gridlookup on details
           isSetTextRequired = false;
           var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
           if (keyCode !== 9 ) return;
           var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
           if (gv1.batchEditApi[moveActionName]()) {
                   ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
           }
       }

       function gridLookup_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
           var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
           if (keyCode == 13)
               gv1.batchEditApi.EndEdit();
           //ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
       }

       function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
           setTimeout(function () {
               gv1.batchEditApi.EndEdit();
           }, 1000);
       }

       //validation
       function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields)
           for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
               var column = s.GetColumn(i);
               if (column.fieldName == "ColorCode") {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
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

       //function getParameterByName(name) {
       //    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
       //    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
       //        results = regex.exec(location.search);
       //    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
       //}
       function OnCustomClick(s, e) {

           if (e.buttonID == "CountSheet") {
               if (s.batchEditApi.GetCellValue(e.visibleIndex, "LineNumber") == null || 
                   s.batchEditApi.GetCellValue(e.visibleIndex, "DocNumber") == null ||
                   s.batchEditApi.GetCellValue(e.visibleIndex, "DocNumber") == '') {
                   e.cancel = true;
               }
               else {
                   CSheet.Show();
                   var linenum = s.batchEditApi.GetCellValue(e.visibleIndex, "LineNumber");
                   var docnumber = getParameterByName('docnumber');
                   var transtype = getParameterByName('transtype');
                   var entry = getParameterByName('entry');
                   CSheet.SetContentUrl('frmCountSheet.aspx?entry=' + entry + '&docnumber=' + docnumber + '&transtype=' + transtype +
                       '&linenumber=' + linenum);
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
           var height = Math.max(0, document.documentElement.clientHeight);
           gv1.SetWidth(width - 120);
           gv1.SetHeight(height - 120);
       }

       //To real Grid
       var arrayGrid = new Array();
       var arrayGrid2 = new Array();
       var arrayGL = new Array();
       var arrayGL2 = new Array();
       var OnConf = false;
       var glText;
       var ValueChanged = false;
       var deleting = false;
       var endcbgrid = false;
       //Function Autobind to GridEnd
       function isInArray(value, array) {
           return array.indexOf(value) > -1;
       }

       function gvExtract_end(s, e) {
           if (endcbgrid) {
               gvExtract.GetSelectedFieldValues('DocNumber;LineNumber;ItemCode;ColorCode;ClassCode;SizeCode;PalletID;ToPalletID;BulkQty;Qty;FromLoc;ToLoc;StatusCode;BatchNumber', OnGetSelectedFieldValues);
               endcbgrid = false;
           }
       }

       function OnGetSelectedFieldValues(selectedValues) {
           //if (selectedValues.length == 0) return;
           //arrayGL.push(glTranslook.GetText().split(';'));
           var item;
           var checkitem;
           for (i = 0; i < selectedValues.length; i++) {
               var s = "";
               for (j = 0; j < selectedValues[i].length; j++) {
                   s = s + selectedValues[i][j] + ";";
               }
               item = s.split(';');
               gv1.AddNewRow();
               getCol(gv1, editorobj, item);
           }
           loader.Hide();
       }

       function getCol(ss, ee, item) {
           for (var i = 0; i < ss.GetColumnsCount() ; i++) {
               var column = ss.GetColumn(i);
               if (column.visible == false || column.fieldName == undefined)
                   continue;
               Bindgrid(item, ee, column, ss);
           }
       }

       function Bindgrid(item, e, column, s) {//Clone function :D
           if (column.fieldName == "DocNumber") {
               console.log('here',item[0])
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[0]);
           }
           if (column.fieldName == "LineNumber") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[1]);
           }
           if (column.fieldName == "ItemCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[2]);
           }
           if (column.fieldName == "ColorCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[3]);
           }
           if (column.fieldName == "ClassCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[4]);
           }
           if (column.fieldName == "SizeCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[5]);
           }
           if (column.fieldName == "PalletID") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[6] == 'null' ? null : item[6]);
           }
           if (column.fieldName == "BulkQty") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[8] == 'null' ? null : item[8]);
           }
           if (column.fieldName == "Qty") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[9] == 'null' ? null : item[9]);
           }
           if (column.fieldName == "FromLoc") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[10] == 'null' ? null : item[10]);
           }
           if (column.fieldName == "StatusCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[12] == 'null' ? null : item[12]);
           }
           if (column.fieldName == "BatchNumber") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[13] == 'null' ? null : item[13]);
           }
       }

    </script>
    <!--#endregion-->
</head>
<body style="height: 475px">
    <% if (DesignMode){ %>
    <script src="~/js/ASPxScriptIntelliSense.js" type="text/javascript"></script>
    <% } %> 
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <form id="form1" runat="server" class="Entry">
        <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel runat="server" ID="ASPxLabel" Text="Item Relocation" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>

         <dx:ASPxPopupControl ID="CSheet" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="CSheet" CloseAction="CloseButton" CloseOnEscape="true"
        EnableViewState="False" HeaderImage-Height="10px" HeaderText="" Height="600px" ShowHeader="true" Width="950px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
         ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents CloseUp="function (s, e) { cp.PerformCallback('refgrid'); }" />
    </dx:ASPxPopupControl>
 <%--  --%>      
        <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="400px" Width="1280px" style="margin-left: -3px">
           <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
             <Items>
             <dx:LayoutItem Caption="">
                  <LayoutItemNestedControlCollection>
                      <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="200px" ClientInstanceName="cp">
                            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Height="300px" Width="1280px" style="margin-left: -3px">
                      <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                          <Items>
                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="General" ColCount="2" ColSpan="2">
                                        <Items>
                                       
                                            <dx:LayoutItem Caption="Document Number:" Name="DocNumber">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDocnumber" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                              <dx:LayoutItem Caption="Document Date:" Name="DocDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="deDocDate" runat="server"  Width="170px"  ReadOnly="true" >
                                                            <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None">
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                      
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                              <dx:LayoutItem Caption="Warehouse Code:" Name="WarehouseCode">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="glWarehouseCode" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>


                                              <dx:LayoutItem Caption="Storage Type:" Name="StorageType">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtStorageType" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            



                                         

                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="User Defined" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Field1">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                           
                                            <dx:LayoutItem Caption="Field2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                              
                                            <dx:LayoutItem Caption="Field3">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
             
                                            <dx:LayoutItem Caption="Field4">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField4" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                             
                                            <dx:LayoutItem Caption="Field5">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Field6">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                                          <dx:LayoutItem Caption="Field7">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                                  <dx:LayoutItem Caption="Field8">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField8" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                                           <dx:LayoutItem Caption="Field9">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server"  ReadOnly="True">
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
                                                        <dx:ASPxTextBox ID="txtAddedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>
                                          <dx:LayoutItem Caption="Added Date:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>
                                          <dx:LayoutItem Caption="Last Edited By:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>
                                          <dx:LayoutItem Caption="Last Edited Date:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>  
                                             <dx:LayoutItem Caption="Submitted By:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedBy" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem>
                                          <dx:LayoutItem Caption="Submitted Date:" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedDate" runat="server" Width="170px" ColCount="1" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                               </dx:LayoutItem> 
                                         </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:TabbedLayoutGroup>
                            
<%--                            <dx:LayoutGroup Caption="Lines">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">--%>
                                               
                                            <%--</dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>--%>
                        </Items>
                    </dx:ASPxFormLayout>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                      </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                  <LayoutItemNestedControlCollection>
                      <dx:LayoutItemNestedControlContainer>
                                  <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Height="400px" Width="1280px" style="margin-left: -3px">
                      <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                          <Items>
                            <dx:LayoutGroup Caption="Item Relocation Detail">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" Width="747px" 
                                                    OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gv1" 
                                                     KeyFieldName="DocNumber;LineNumber;PalletID">
                                                       <ClientSideEvents Init="OnInitTrans" EndCallback="gridView_EndCallback" />
                                                     <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="DocNumber" Width="0px"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Caption="LineNumber" ReadOnly="True" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                       
                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" VisibleIndex="5" Width="120px" Name="glItemCode">
                                                             
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" VisibleIndex="6" Width="0px">   
                                                                  
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" VisibleIndex="7" Width="0px" Name="glClassCode">
                                                           
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" VisibleIndex="8" Width="0px" Name ="glSizeCode">          
                                                         </dx:GridViewDataTextColumn>

                                                          <dx:GridViewDataTextColumn FieldName="FullDesc" Caption="Description" VisibleIndex="8" Width="150px" Name ="glFullDesc">          
                                                         </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn FieldName="PalletID" VisibleIndex="9" Width="80px" Caption="PalletID">
                                                            </dx:GridViewDataTextColumn>
                                                          <dx:GridViewDataTextColumn FieldName="ToPalletID" VisibleIndex="9" Width="80px" Caption="To PalletID">
                                                            </dx:GridViewDataTextColumn>    
                                                          <dx:GridViewDataTextColumn FieldName="FromLoc" VisibleIndex="9" Width="80px" Name="FromLoc">
                                                             
                                                        </dx:GridViewDataTextColumn>
                                                       <%--<dx:GridViewDataTextColumn Caption="FromLoc" FieldName="FromLoc" Name="FromLoc" ShowInCustomizationForm="True" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>--%>
                                                      <%-- <dx:GridViewDataTextColumn Caption="ToLoc" FieldName="ToLoc" Name="ToLoc" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>--%>
                                                          <dx:GridViewDataTextColumn FieldName="ToLoc" VisibleIndex="9" Width="80px" Name="ToLoc">
                                                        </dx:GridViewDataTextColumn>

                                                                   <dx:GridViewDataSpinEditColumn FieldName="Qty" VisibleIndex="9" Width="90px"  UnboundType="Decimal">
                                                                <PropertiesSpinEdit NullDisplayText="0" ConvertEmptyStringToNull="True" NullText="0" DisplayFormatString="{0:N}"
                                                                    MinValue="0">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="BulkQty" VisibleIndex="11" Width="90px" UnboundType="Decimal">
                                                                
                                                            </dx:GridViewDataSpinEditColumn>
                                                       
                                           
                                                        <dx:GridViewDataTextColumn Caption="Field1" Name="Field1" ShowInCustomizationForm="True" VisibleIndex="20" FieldName="Field1" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field2" Name="Field2" ShowInCustomizationForm="True" VisibleIndex="21" FieldName="Field2" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field3" Name="Field3" ShowInCustomizationForm="True" VisibleIndex="22" FieldName="Field3" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field4" Name="Field4" ShowInCustomizationForm="True" VisibleIndex="23" FieldName="Field4" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field5" Name="Field5" ShowInCustomizationForm="True" VisibleIndex="24" FieldName="Field5" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field6" Name="Field6" ShowInCustomizationForm="True" VisibleIndex="25" FieldName="Field6" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field7" Name="Field7" ShowInCustomizationForm="True" VisibleIndex="26" FieldName="Field7" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field8" Name="Field8" ShowInCustomizationForm="True" VisibleIndex="27" FieldName="Field8" UnboundType="String" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Field9" Name="Field9" ShowInCustomizationForm="True" VisibleIndex="28" FieldName="Field9" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                       <%-- <dx:GridViewDataTextColumn Caption="BulkUnit" Name="BulkUnit" ShowInCustomizationForm="True" VisibleIndex="10" FieldName="BulkUnit">
                                                        </dx:GridViewDataTextColumn>--%>
                                                      
                                                     
                                                       
                                                    </Columns>
                                                    <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="300"  /> 
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm"
                                                        BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                                                            <SettingsEditing Mode="Batch" />
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                          </Items>
                    </dx:ASPxFormLayout>
                      </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
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
                                         <ClientSideEvents Click="function (s, e){ gv1.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
                                         </dx:ASPxButton>
                                     <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                         <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                                         </dx:ASPxButton>  
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Calculating..."
            ClientInstanceName="loader" ContainerElementID="gv1" Modal="true">
            <LoadingDivStyle Opacity="0"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
    </form>

    <!--#region Region Datasource-->
    <%--<!--#region Region Header --> --%>

    <asp:ObjectDataSource ID="odsDetail" runat="server" SelectMethod="getdetail" TypeName="Entity.ItemRelocation+ItemRelocationDetail" DataObjectTypeName="Entity.ItemRelocation+ItemRelocationDetail" >
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM  WMS.ItemRelocationDetail  where DocNumber  is null " OnInit="Connection_Init" >
  
    </asp:SqlDataSource>
      
     <!--#endregion-->
</body>
</html>


