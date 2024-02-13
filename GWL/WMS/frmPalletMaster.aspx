<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPalletMaster.aspx.cs" Inherits="GWL.frmPalletMaster" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <title></title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
#form1 {
height: 350px; /*Change this whenever needed*/
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

         var baseqty = 0;
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
                 alert(s.cp_message);
                 delete (s.cp_success);//deletes cache variables' data
                 delete (s.cp_message);
                 if (s.cp_forceclose) {
                     delete (s.cp_forceclose);
                     window.close();
                 }
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
                     delete (s.cp_close);
                     window.location.reload();
                 }
                 else {
                     delete (s.cp_close);
                     window.close();//close window if callback successful
                 }
             }
             if (s.cp_delete) {
                 delete (cp_delete);
                 DeleteControl.Show();
             }
             if (s.cp_clear) {
                 delete (s.cp_clear);
                 gv1.CancelEdit();
             }
         }

         var isBusy = false;
         var index;
         var index2;
         var closing;
         var valchange;
         var valchange2;
         var valchange3;
         var valchange4;
         var val;
         var temp;
         var bulkqty;
         var itemc; //variable required for lookup
         var currentColumn = null;
         var isSetTextRequired = false;
         var linecount = 1;

         function OnStartEditing(s, e) {

             if (entry != "V" && entry != "D") {
                 currentColumn = e.focusedColumn;
                 var cellInfo = e.rowValues[e.focusedColumn.index];
                 itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "Item");
                 bulkqty = s.batchEditApi.GetCellValue(e.visibleIndex, "BulkQty");
                 console.log('Start:' + e.focusedColumn.fieldName);
                 if (bulkqty == null) {
                     bulkqty = 0;
                 }

                 if (e.focusedColumn.fieldName == "Item")
                 console.log('item2');
                 gl.GetInputElement().value = cellInfo.value;
                 isSetTextRequired = true;
                 index = e.visibleIndex;
             }
             if (e.focusedColumn.fieldName == "Customer") {
                 glStorerKey.GetInputElement().value = cellInfo.value;
             }
         }
         

         function OnEndEditing(s, e) {//end edit grid function, sets text after 
             //select leaving the current lookup
             console.log('end');
             //displays the value of selected from the lookup
             if (entry != "V" && entry != "D") {
                 var cellInfo = e.rowValues[currentColumn.index];
                 if (currentColumn.fieldName === "Item") {
                     console.log(gl.GetText());
                     cellInfo.value = gl.GetValue();
                     cellInfo.text = gl.GetText();
                 }
                 if (currentColumn.fieldName == "Customer") {
                     cellInfo.value = glStorerKey.GetValue();
                     cellInfo.text = gl.GetText();
                 }
                 if (currentColumn.fieldName == "Customer") {
                     cellInfo.value = glStorerKey.GetValue();
                     cellInfo.text = glStorerKey.GetText();
                 }
             }
             
         }

         var val;
         var temp;

         function GridEnd(s, e) {
             console.log('gridend');
             val = s.GetGridView().cp_codes;

             temp = val.split(';');
             if (valchange) {
                 console.log(val);
                 valchange = false;
                 var column = gv1.GetColumn(6);
                 ProcessCells2(0, index2, column, gv1);
                 isBusy = false;
             }
             if (valchange3) {
                 valchange3 = false;
                 var column = gv1.GetColumn(8);
                 ProcessCells4(0, index2, column, gv1);
                 isBusy = false;
             }
             if (valchange4) {
                 console.log(val);
             }
             if (valchange2) {
                 valchange2 = false;
                 closing = false;
                 for (var i = 0; i < gv1.GetColumnCount(); i++) {
                     var column = gv1.GetColumn(i);
                     if (column.visible == false || column.fieldName == undefined)
                         continue;
                     ProcessCells3(0, e, column, gv1);
                 }
                 gv1.batchEditApi.EndEdit();
             }
             loader.Hide();
         }

         function ProcessCells3(selectedIndex, e, column, s) {
             if (val == null) {
                val = ";;;;;";
                temp = val.split(';');
            }
            if (temp[0] == null || temp[0] == "") {
                temp[0] = "";
            }
            if (temp[1] == null || temp[1] == "") {
                temp[1] = "";
            }
            if (temp[2] == null || temp[2] == "") {
                temp[2] = "";
            }
            if (temp[3] == null || temp[3] == "") {
                temp[3] = "";
            }
            if (temp[4] == null || temp[4] == "") {
                temp[4] = "";
            }
            if (temp[5] == null || temp[5] == "") {
                temp[5] = 0;
             }
             if (selectedIndex == 0) {
                 if (column.fieldName == "ColorCode") {
                     s.batchEditApi.SetCellValue(index, column.fieldName, temp[0]);
                 }
                 if (column.fieldName == "ClassCode") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp[1]);
                 }
                 if (column.fieldName == "SizeCode") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp[2]);
                 }
                  if (column.fieldName == "FullDesc") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp[3]);
                 }
                 if (column.fieldName == "BulkUnit") { //Change fieldname according to your main qty
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp[4]);
                 }
                 if (column.fieldName == "InputBaseQty") { //Change fieldname according to your main qty
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp[5]);
                }
             }
         }

         function ProcessCells2(selectedIndex, focused, column, s) {
            if (val == null) {
                val = ";";
                temp = val.split(';');
            }
            if (temp[0] == null) {
                temp[0] = 0;
            }
            if (selectedIndex == 0) {
                s.batchEditApi.SetCellValue(focused, "InputBaseQty", temp[0]);//Change fieldname according to your main qty
            }
         }
         function ProcessCells4(selectedIndex, focused, column, s) {//Auto calculate qty function :D
            if (val == null) {
                val = ";";
                temp = val.split(';');
            }
            if (temp[0] == null) {
                temp[0] = 0;
            }
            if (selectedIndex == 0) {
                s.batchEditApi.SetCellValue(focused, "DocBaseQty", temp[0]);//Change fieldname according to your main qty
            }
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
             if (keyCode !== 9) return;
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
         function DeleteDetail(s, e) {


            var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();

            for (var i = 0; i < indicies.length; i++) {
                if (gv1.batchEditHelper.IsNewItem(indicies[i])) {

                    gv1.DeleteRow(indicies[i]);
                }


                else {
                    var key = gv1.GetRowKey(indicies[i]);
                    if (gv1.batchEditHelper.IsDeletedItem(key))
                        console.log("deleted row " + indicies[i]);
                    else {
                        gv1.DeleteRow(indicies[i]);

                    }
                }
            }

         }

         function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
             setTimeout(function () {
                 gv1.batchEditApi.EndEdit();
             }, 500);
         }

         //validation
         function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields)
             for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
                 var column = s.GetColumn(i);
                 //if (column != s.GetColumn(6) && column != s.GetColumn(1) && column != s.GetColumn(7) && column != s.GetColumn(5) && column != s.GetColumn(8) && column != s.GetColumn(9) && column != s.GetColumn(10) && column != s.GetColumn(11) && column != s.GetColumn(12) && column != s.GetColumn(13) && column != s.GetColumn(14) && column != s.GetColumn(15) && column != s.GetColumn(16) && column != s.GetColumn(17) && column != s.GetColumn(18) && column != s.GetColumn(19) && column != s.GetColumn(20) && column != s.GetColumn(21) && column != s.GetColumn(22) && column != s.GetColumn(23) && column != s.GetColumn(24) && column != s.GetColumn(13)) {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
                 //    var cellValidationInfo = e.validationInfo[column.index];
                 //    if (!cellValidationInfo) continue;
                 //    var value = cellValidationInfo.value;
                 //    if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "") {
                 //        cellValidationInfo.isValid = false;
                 //        cellValidationInfo.errorText = column.fieldName + " is required";
                 //        isValid = false;
                 //    }
                 //    else {
                 //        isValid = true;
                 //    }
                 //}
             }
         }

         function OnCustomClick(s, e) {
            if (e.buttonID == "Details") {
                var itemcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode");
                //var colorcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ColorCode");
                //var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
                //var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
                factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode);
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
             gv1.SetHeight(height - 120);
             gv1.SetWidth(width - 120);
         }

         jQuery(document).ready(function (event) {//ADDNEW
            var isExpired = false;
            setInterval(checkSession, 2000);

            function checkSession() {
                var xhr = $.ajax({
                    type: "POST",
                    url: "../checksession.aspx/check",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d && !isExpired) {
                            isExpired = true;
                            alert('User Session Expired. Please click Ok to close the window.');
                            window.close();
                        }
                    }
                });
            }
         });

    </script>
</head>
<body style="height: 910px">
        <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <form id="form1" runat="server" class="Entry">
            <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel runat="server" Text="Pallet" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>

        <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" ClientInstanceName="factbox" CloseAction="None"
            EnableViewState="False" HeaderText="Item info" Height="207px" Width="245px" PopupHorizontalOffset="1085" PopupVerticalOffset="90"
            ShowCloseButton="False" ShowCollapseButton="True" ShowOnPageLoad="True" ShowPinButton="True" ShowShadow="True">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="1050px" Height="338px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="form1_layout" runat="server" Height="269px" Width="850px" style="margin-left: -20px">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600"></SettingsAdaptivity>
                        
                        <Items>

                          <dx:TabbedLayoutGroup>
                            <Items>
                           <dx:LayoutGroup Caption="General" ColCount="2">
                               <Items>
                                   <dx:LayoutItem Caption="PalletID">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxTextBox ID="txtPallet" runat="server" Width="170px" ReadOnly="true">
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

                                  <%--<dx:LayoutItem Caption="First Name">
                                    <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxTextBox ID="txtFirstName" runat="server" AutoCompleteType="Disabled"  Width="170px" OnLoad="TextboxLoad">
                                         <ClientSideEvents Validation="OnValidation" />
                                          <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                              <RequiredField IsRequired="True" />
                                          </ValidationSettings>
                                          <InvalidStyle BackColor="Pink">
                                          </InvalidStyle>
                                      </dx:ASPxTextBox>
                                  </dx:LayoutItemNestedControlContainer>
                              </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>--%>

                                    <dx:LayoutItem Caption="Area Code">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxGridLookup ID="CustomerCode" runat="server" Width="170px" OnLoad="LookupLoad" ClientInstanceName="AreaCode" DataSourceID="AreaCode" KeyFieldName="CustomerCode" TextFormatString="{0}" >
                                            <ClientSideEvents Validation="OnValidation" ValueChanged="function(s, e) {cp.PerformCallback('SUP');}"/>
                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="Pink">
                                            </InvalidStyle>
                                            <GridViewProperties Settings-ShowFilterRow="true">
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                <Settings ShowFilterRow="True"></Settings>
                                            </GridViewProperties>
                                        </dx:ASPxGridLookup>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                                    <%-- <dx:LayoutItem Caption="Last Name">
                              <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxTextBox ID="txtLastName" runat="server" AutoCompleteType="Disabled"  Width="170px" OnLoad="TextboxLoad">
                                         <ClientSideEvents Validation="OnValidation" />
                                          <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                              <RequiredField IsRequired="True" />
                                          </ValidationSettings>
                                          <InvalidStyle BackColor="Pink">
                                          </InvalidStyle>
                                      </dx:ASPxTextBox>
                                  </dx:LayoutItemNestedControlContainer>
                              </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>--%>

                                    <dx:LayoutItem Caption="Tier Pallet">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxSpinEdit ID="txtTierPallet" runat="server" OnLoad="SpinEdit_Load" Number="0" Width="170px" >
                                        <SpinButtons ShowIncrementButtons="true" />
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                             <%-- <dx:LayoutItem Caption="Middle Name">
                              <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxTextBox ID="txtMidName" runat="server" AutoCompleteType="Disabled"  Width="170px" OnLoad="TextboxLoad">
                                         <ClientSideEvents Validation="OnValidation" />
                                          <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                              <RequiredField IsRequired="True" />
                                          </ValidationSettings>
                                          <InvalidStyle BackColor="Pink">
                                          </InvalidStyle>
                                      </dx:ASPxTextBox>
                                  </dx:LayoutItemNestedControlContainer>
                              </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>--%>

                                     <dx:LayoutItem Caption="Packaging">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxGridLookup ID="txtPackaging" runat="server" Width="170px" DataSourceID="Packaging" Onload="LookupLoad" KeyFieldName="Packaging">
                                            <GridViewProperties Settings-ShowFilterRow="true">
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                <Settings ShowFilterRow="True"></Settings>
                                            </GridViewProperties>
                                        </dx:ASPxGridLookup>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                                   
                            <%--<dx:LayoutItem Caption="Nick Name">
                              <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxTextBox ID="txtNickName" runat="server" AutoCompleteType="Disabled"  Width="170px" OnLoad="TextboxLoad">
                                         <ClientSideEvents Validation="OnValidation" />
                                          <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                          </ValidationSettings>
                                          <InvalidStyle BackColor="Pink">
                                          </InvalidStyle>
                                      </dx:ASPxTextBox>
                                  </dx:LayoutItemNestedControlContainer>
                              </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>--%>

                                     <dx:LayoutItem Caption="Width">
                               <LayoutItemNestedControlCollection>
                                   <dx:LayoutItemNestedControlContainer runat="server">
                                       <dx:ASPxSpinEdit ID="txtWidth" runat="server" OnLoad="SpinEdit_Load" Number="0" Width="170px">
                                       <SpinButtons ShowIncrementButtons="false" />
                                       </dx:ASPxSpinEdit>
                                   </dx:LayoutItemNestedControlContainer>
                               </LayoutItemNestedControlCollection>
                           </dx:LayoutItem>

                                   
                            <%--<dx:LayoutItem Caption="Birthday">
                              <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxDateEdit ID="txtBirthday" runat="server"  Width="170px" OnLoad="Date_Load">
                                         <ClientSideEvents Validation="OnValidation" />
                                          <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                              <RequiredField IsRequired="True" />
                                          </ValidationSettings>
                                      </dx:ASPxDateEdit>
                                  </dx:LayoutItemNestedControlContainer>
                              </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>--%>


                                    <dx:LayoutItem Caption="Length">
                               <LayoutItemNestedControlCollection>
                                   <dx:LayoutItemNestedControlContainer runat="server">
                                       <dx:ASPxSpinEdit ID="txtLength" runat="server" OnLoad="SpinEdit_Load" Number="0" Width="170px">
                                       <SpinButtons ShowIncrementButtons="false" />
                                       </dx:ASPxSpinEdit>
                                   </dx:LayoutItemNestedControlContainer>
                               </LayoutItemNestedControlCollection>
                           </dx:LayoutItem>

                                    <%--<dx:LayoutItem Caption="Age">
                               <LayoutItemNestedControlCollection>
                                   <dx:LayoutItemNestedControlContainer runat="server">
                                       <dx:ASPxSpinEdit ID="txtAge" runat="server" OnLoad="SpinEdit_Load" Number="0" Width="170px">
                                       <SpinButtons ShowIncrementButtons="false" />
                                       </dx:ASPxSpinEdit>
                                   </dx:LayoutItemNestedControlContainer>
                               </LayoutItemNestedControlCollection>
                           </dx:LayoutItem>--%>

                                   
                           <dx:LayoutItem Caption="Height">
                               <LayoutItemNestedControlCollection>
                                   <dx:LayoutItemNestedControlContainer runat="server">
                                       <dx:ASPxSpinEdit ID="txtHeight" runat="server" OnLoad="SpinEdit_Load" Number="0" Width="170px">
                                       <SpinButtons ShowIncrementButtons="false" />
                                       </dx:ASPxSpinEdit>
                                   </dx:LayoutItemNestedControlContainer>
                               </LayoutItemNestedControlCollection>
                           </dx:LayoutItem>

                                    <dx:LayoutItem Caption="Unit Weight">
                               <LayoutItemNestedControlCollection>
                                   <dx:LayoutItemNestedControlContainer runat="server">
                                       <dx:ASPxSpinEdit ID="txtUnitWeight" runat="server" OnLoad="SpinEdit_Load" Number="0" Width="170px">
                                       <SpinButtons ShowIncrementButtons="false" />
                                       </dx:ASPxSpinEdit>
                                   </dx:LayoutItemNestedControlContainer>
                               </LayoutItemNestedControlCollection>
                           </dx:LayoutItem>

                                   
                           <dx:LayoutItem Caption="Pallet Type">
                              <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxTextBox ID="txtPalletType" runat="server" AutoCompleteType="Disabled"  Width="170px" OnLoad="TextboxLoad">
                                        
                                      </dx:ASPxTextBox>
                                  </dx:LayoutItemNestedControlContainer>
                              </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>

                                   <dx:LayoutItem Caption="Case Tier">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxSpinEdit ID="txtCaseTier" runat="server" OnLoad="SpinEdit_Load" Number="0" Width="170px" >
                                        <SpinButtons ShowIncrementButtons="true" />
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                               </Items>
                           </dx:LayoutGroup>

                            </Items>
                       </dx:TabbedLayoutGroup>

                       <dx:LayoutGroup Caption="Pallet Detail">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <div id="loadingcont" >
                                                    <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" Width="747px"
                                                        OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gv1"
                                                        OnBatchUpdate="gv1_BatchUpdate" KeyFieldName="DocNumber;LineNumber"
                                                        SettingsBehavior-AllowSort="false" OnRowValidating="grid_RowValidating">
                                                        <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                                        <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="OnInitTrans"
                                                            BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                        <SettingsPager Mode="ShowAllRecords" />
                                                        <SettingsEditing Mode="Batch" />
                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="530" ShowFooter="True" />

                                                        <SettingsBehavior AllowSort="False"></SettingsBehavior>

                                                        <SettingsCommandButton>
                                                            <NewButton>
                                                                <Image IconID="actions_addfile_16x16"></Image>
                                                            </NewButton>
                                                            <DeleteButton>
                                                                <Image IconID="actions_cancel_16x16"></Image>
                                                            </DeleteButton>
                                                        </SettingsCommandButton>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowDeleteButton="true" ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="60px">
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="Details">
                                                                        <Image IconID="support_info_16x16"></Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>

                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="PalletID" Visible="false" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="LineNumber" Visible="false" Caption="LineNumber" ReadOnly="True" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="PlateNumber" FieldName="PlateNumber" Name="PlateNumber" ReadOnly="True" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn PropertiesTextEdit-Native="true"  Width="120px"  Caption="Batch #" FieldName="Batch" Name="Batch" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                <PropertiesTextEdit Width="100px"  Native="True"></PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn PropertiesTextEdit-Native="true" Width="120px" Caption="Lot" FieldName="lot" Name="lot" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                <PropertiesTextEdit  Width="100px" Native="True"></PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>
                                                       
                                                            <dx:GridViewDataDateColumn FieldName="ExpiryDate" Caption="Expiry Date" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn PropertiesTextEdit-Native="true"  Width="120px" Caption="UOM" FieldName="UOM" Name="UOM" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                <PropertiesTextEdit  Width="100px" Native="True"></PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Customer" VisibleIndex="7" Width="95px" Name="Customer">
                                                               <EditItemTemplate>
                                                                    <dx:ASPxGridLookup Width="80px" ID="glStorerKey" runat="server" AutoGenerateColumns="False" ClientInstanceName="glStorerKey"
                                                                        DataSourceID="Masterfilebiz" KeyFieldName="BizPartnerCode" OnLoad="gvLookupLoad" TextFormatString="{0}">
                                                                        <GridViewProperties>
                                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                            <Settings ShowFilterRow="True" />
                                                                        </GridViewProperties>
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="True" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" />

                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Item" VisibleIndex="8" Width="200px" Name="Item">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxGridLookup ID="glItemCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" OnInit="glItemCode_Init"
                                                                        DataSourceID="Masterfileitem" KeyFieldName="ItemCode" ClientInstanceName="gl" TextFormatString="{0}" Width="200px" OnLoad="gvLookupLoad">
                                                                        <GridViewProperties Settings-ShowFilterRow="true">
                                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                AllowSelectSingleRowOnly="True" AllowDragDrop="False" />
                                                                        </GridViewProperties>
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="ItemCode" ReadOnly="True" VisibleIndex="0" Settings-AutoFilterCondition="Contains" />
                                                                            <dx:GridViewDataTextColumn FieldName="FullDesc" ReadOnly="True" VisibleIndex="1" Settings-AutoFilterCondition="Contains" />
                                                                        </Columns>
                                                                        <ClientSideEvents DropDown="function(s,e){gl.GetGridView().PerformCallback('ItemCodeDropDown' + '|' + glStorerKey.GetValue());}" KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" />
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>    
                                                            <dx:GridViewDataSpinEditColumn Caption="Qty" Name="Quantity" ShowInCustomizationForm="True" VisibleIndex="9" FieldName="Quantity" PropertiesSpinEdit-DisplayFormatString="{0:N}" UnboundType="Decimal" Width="100px">
                                                                <PropertiesSpinEdit Increment="0" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0" DisplayFormatString="{0:N}" MaxValue="9999999999" MinValue="0" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false">
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Weight" Name="weight" ShowInCustomizationForm="True" VisibleIndex="10" FieldName="weight" PropertiesSpinEdit-DisplayFormatString="{0:N}" UnboundType="Decimal" Width="100px">
                                                                <PropertiesSpinEdit Increment="0" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0" DisplayFormatString="{0:N}" MaxValue="9999999999999999" MinValue="0" SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false">
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataTextColumn PropertiesTextEdit-Native="true" Width="120px"  Caption="Checker" FieldName="checker" Name="checker" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            
                                                            </dx:GridViewDataTextColumn>


                                                            <dx:GridViewDataTextColumn Caption="Field1" Name="Field1" ShowInCustomizationForm="True" VisibleIndex="17" FieldName="Field1" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field2" Name="Field2" ShowInCustomizationForm="True" VisibleIndex="18" FieldName="Field2" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field3" Name="Field3" ShowInCustomizationForm="True" VisibleIndex="19" FieldName="Field3" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field4" Name="Field4" ShowInCustomizationForm="True" VisibleIndex="20" FieldName="Field4" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field5" Name="Field5" ShowInCustomizationForm="True" VisibleIndex="21" FieldName="Field5" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field6" Name="Field6" ShowInCustomizationForm="True" VisibleIndex="22" FieldName="Field6" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field7" Name="Field7" ShowInCustomizationForm="True" VisibleIndex="23" FieldName="Field7" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field8" Name="Field8" ShowInCustomizationForm="True" VisibleIndex="24" FieldName="Field8" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Field9" Name="Field9" ShowInCustomizationForm="True" VisibleIndex="25" FieldName="Field9" UnboundType="String">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </div>

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
                         <td>
                             <dx:ASPxButton ID="Ok" runat="server" Text="Ok" AutoPostBack="False" UseSubmitBehavior="false">
                             <ClientSideEvents Click="function (s, e){  cp.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
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
            ClientInstanceName="loader" ContainerElementID="loadingcont" Modal="true">
            <LoadingDivStyle Opacity="0"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        </form>

      <!--#region Region Datasource-->
    <%--<!--#region Region Header --> --%>
    <asp:ObjectDataSource ID="odsHeader" runat="server" DataObjectTypeName="Entity.PalletMaster" SelectMethod="getdata" TypeName="Entity.PalletMaster" UpdateMethod="UpdateData" InsertMethod="InsertData">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="PalletID" SessionField="PalletID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="odsDetail" runat="server" SelectMethod="getdetail" TypeName="Entity.PalletMaster+PalletDetail" DataObjectTypeName="Entity.PalletMaster+PalletDetail"  UpdateMethod="UpdatePalletDetails"  DeleteMethod="DeletePalletDetails" InsertMethod="AddPalletDetails">
        <SelectParameters>
            <asp:QueryStringParameter Name="PalletID" QueryStringField="PalletID" Type="String" />
             <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM  WMS.PalletDetail where PalletID  is null " OnInit="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Masterfileitem" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="" OnInit="Connection_Init"></asp:SqlDataSource>

    <asp:SqlDataSource ID="Warehouse" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0" OnInit="Connection_Init"></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="AreaCode" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT CustomerCode,StorageType FROM PORTAL.IT.PalletInfo" OnInit="Connection_Init"></asp:SqlDataSource>

        <asp:SqlDataSource ID="Customer" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT CustomerCode, StorageType FROM PORTAL.it.PalletInfo where DocNumber is not null"
         OnInit = "Connection_Init">
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="Masterfileitemdetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
         OnInit="Connection_Init">
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="Masterfilebiz" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BizPartnerCode, Name FROM Masterfile.BizPartner WHERE (ISNULL(IsInactive, '') = 0)" 
        OnInit="Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="Packaging" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT 'Box' AS Packaging UNION ALL SELECT 'Pack' AS Packaging UNION ALL SELECT 'Carcass etc' AS Packaging"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>

<%--    <asp:SqlDataSource ID="PalletID" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT CustomerCode,StorageType FROM PORTAL.IT.PalletInfo"
         OnInit = "Connection_Init">
    </asp:SqlDataSource>--%>

    
</body>
</html>
