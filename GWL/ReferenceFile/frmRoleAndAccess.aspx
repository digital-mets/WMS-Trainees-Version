﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRoleAndAccess.aspx.cs" Inherits="GWL.frmRoleAndAccess"  %>

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
        var isValid = false;
        var counterror = 0;
        var entry = getParameterByName('entry');

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
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
            if (s.GetText() == "" || e.value == "" || e.value == null) {
                counterror++;
                isValid = false
            }
            else {
                isValid = true;
            }
        }

        function OnUpdateClick(s, e) { //Add/Edit/Close button function
            var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();

            gv1.batchEditApi.EndEdit();

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

        var itemc; //variable required for lookup
        var currentColumn = null;
        var isSetTextRequired = false;
        var linecount = 1;
        function OnStartEditing(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];
            if (entry == "V") {
                e.cancel = true; //this will made the gridview readonly
            }
            
            if (entry != "V") {
                if (e.focusedColumn.fieldName === "ModuleID") { //Check the column name
                    colModuleID.GetInputElement().value = cellInfo.value; //Gets the column value
                    isSetTextRequired = true;
                    index = e.visibleIndex;
                }
            }
        }
       
        function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
            var cellInfo = e.rowValues[currentColumn.index];
            if (currentColumn.fieldName === "ModuleID") {
                cellInfo.value = colModuleID.GetValue();
                cellInfo.text = colModuleID.GetText();
            }
          
        }
        
        function UpperCasing(s, e) {
            var txt = s.GetText();
            s.SetText(txt.toUpperCase());

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
                //if (column.fieldName == "ATCCode") {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
                //    
                //    var cellValidationInfo = e.validationInfo[column.index];
                //    if (!cellValidationInfo) continue;
                //    var value = cellValidationInfo.value;
                //    if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "") {
                //        cellValidationInfo.isValid = false;
                //        cellValidationInfo.errorText = column.fieldName + " is required";
                //        isValid = false;
                //    }
                //}
                //var chckd;

                ////else 
                //if (column.fieldName == "TransAPAmount") {
                //    var cellValidationInfo = e.validationInfo[column.index];
                //    if (!cellValidationInfo) continue;
                //    var value = cellValidationInfo.value;
                //    if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "") {
                //        cellValidationInfo.isValid = false;
                //        cellValidationInfo.errorText = column.fieldName + " is required";
                //        isValid = false;
                //    }
                //}
                //if (column.fieldName == "EWT") {
                //    var cellValidationInfo = e.validationInfo[column.index];
                //    if (!cellValidationInfo) continue;
                //    var value = cellValidationInfo.value;
                //    if (ASPxClientUtils.Trim(value) == true) {
                //        chckd = true;
                //    }
                //}
                //if (column.fieldName == "ATCCode") {
                //    var cellValidationInfo = e.validationInfo[column.index];
                //    if (!cellValidationInfo) continue;
                //    var value = cellValidationInfo.value;
                //    if ((!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "") && chckd == true) {
                //        cellValidationInfo.isValid = false;
                //        cellValidationInfo.errorText = column.fieldName + " is required";
                //        isValid = false;
                //    }
                //}
            }
        }

        function OnFileUploadComplete(s, e) {//Loads the excel file into the grid
            gv1.PerformCallback();
        }

        function OnCustomClick(s, e) {
            if (e.buttonID == "Details") {
                var itemcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode");
                var colorcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ColorCode");
                var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
                var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
                factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode
                + '&colorcode=' + colorcode + '&classcode=' + classcode + '&sizecode=' + sizecode);
            }
        }

        function CloseGridLookup() {
            glInvoice.ConfirmCurrentSelection();
            glInvoice.HideDropDown();
            //glInvoice.Focus();
        }

        function Clear() {
            glInvoice.SetValue(null);
        }

        function autocalculate(s, e) {
            var amount = 0.00;
            var vatrate = 0.00;
            var atcrate = 0.00;
            var totalamount = 0.00;


            setTimeout(function () { //New Rows
                var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
                for (var i = 0; i < indicies.length; i++) {
                    if (gv1.batchEditHelper.IsNewItem(indicies[i])) {
                        console.log("new row " + indicies[i]);
                    }
                    else { //Existing Rows
                        var key = gv1.GetRowKey(indicies[i]);
                        if (gv1.batchEditHelper.IsDeletedItem(key))
                            console.log("deleted row " + indicies[i]);
                        else {
                            amount = gv1.batchEditApi.GetCellValue(indicies[i], "Amount");
                            atcrate = gv1.batchEditApi.GetCellValue(indicies[i], "ATCCode");

                            //console.log(gv1.batchEditApi.GetCellValue(indicies[i], "IsVat"));
                            if (gv1.batchEditApi.GetCellValue(indicies[i], "IsVatable") == true) {
                                vatrate = gv1.batchEditApi.GetCellValue(indicies[i], "vatrate");
                                vatamount += amount * vatrate;
                                grossvatable += amount;
                            }
                            else {
                                grossnonvatable += amount;
                            }

                            if (gv1.batchEditApi.GetCellValue(indicies[i], "IsEWT") == true) {
                                if (gv1.batchEditApi.GetCellValue(indicies[i], "IsVatable") == true) {
                                    atcrate = gv1.batchEditApi.GetCellValue(indicies[i], "atcrate");
                                    whtaxamount += amount * atcrate;
                                }
                            }
                        }
                    }


                }
                totalamountdue = (grossvatable + grossnonvatable + vatamount) - whtaxamount;
                txtvatamount.SetText(vatamount.toFixed(2));
                txtgrossvatable.SetText(grossvatable.toFixed(2));
                txtgrossnonvatable.SetText(grossnonvatable.toFixed(2));
                txtwhvatamount.SetText(whtaxamount.toFixed(2));
                txtamountdue.SetText(totalamountdue.toFixed(2));
            }, 500);
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
                                <dx:ASPxLabel runat="server" Text="Role and Access Control" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                        <%--    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" ClientInstanceName="factbox" CloseAction="None" 
        EnableViewState="False" HeaderText="Item info" Height="207px" Width="245px" PopupHorizontalOffset="1085" PopupVerticalOffset="90"
         ShowCloseButton="False" ShowCollapseButton="True" ShowOnPageLoad="True" ShowPinButton="True" ShowShadow="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>--%><%--    <h1>AP Voucher</h1>--%>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="850px" Height="565px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">

                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="565px" Width="850px" style="margin-left: -3px" ClientInstanceName="frmlayout">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                          <%--<!--#region Region Header --> --%>
                            <%-- <!--#endregion --> --%>
                            
                          <%--<!--#region Region Details --> --%>
                            <%-- <!--#endregion --> --%>
                               

                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="Header">
                                        <Items>
                                            <dx:LayoutItem Caption="Role ID" Name="RoleID">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtRoleID" runat="server" Width="170px"   >
                                                           <ClientSideEvents Validation="OnValidation" KeyUp ="UpperCasing" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                             </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Description" Name="txtDescription">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDescription" runat="server" Width="170px" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Panel" Name="txtPanel">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtPanel" runat="server" Width="170px" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>                                         
                                            <dx:LayoutItem Caption="IsInactive" Name="chkIsInactive">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxCheckBox ID="chkIsInactive" runat="server" CheckState="Unchecked" ReadOnly ="true">
                                                        </dx:ASPxCheckBox>
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
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>                                           
                                            <dx:LayoutItem Caption="Field2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>                              
                                            <dx:LayoutItem Caption="Field3">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>             
                                            <dx:LayoutItem Caption="Field4">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField4" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>                             
                                            <dx:LayoutItem Caption="Field5">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Field6">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field7">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field8">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField8" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field9">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server" OnLoad ="TextboxLoad">
                                                        </dx:ASPxTextBox>
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
                                            <dx:LayoutItem Caption="Activated By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHActivatedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Activated Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHActivatedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Deactivated By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHDeactivatedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Deactivated Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHDeactivatedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            
                                        </Items>
                                    </dx:LayoutGroup>
                                 </Items>
                            </dx:TabbedLayoutGroup>
                            <dx:LayoutGroup Caption="Roles and Access Detail" Width="1px">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" Width="770" KeyFieldName="RoleID;ModuleID"
                                                    OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gv1"
                                                   SettingsBehavior-AllowSort="false" OnCustomButtonInitialize="gv1_CustomButtonInitialize" OnBatchUpdate="gv1_BatchUpdate" OnRowValidating="grid_RowValidating">
                                                     <ClientSideEvents Init="OnInitTrans" />
                                                  <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="RoleID" Visible="false"
                                                            VisibleIndex="0">
                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                          <dx:GridViewCommandColumn ButtonType="Image" ShowDeleteButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="30px">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="prevModuleID" Name="preMod" ShowInCustomizationForm="True" VisibleIndex="1" Width="0px">
                                                        </dx:GridViewDataTextColumn>  
                                                         <dx:GridViewDataTextColumn FieldName="ModuleID" VisibleIndex="2" Width="100px" Name="ModuleID">
                                                             <EditItemTemplate>
                                                                <dx:ASPxGridLookup ID="glModuleID" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="colModuleID" DataSourceID="sdsModuleID" KeyFieldName="ModuleID"  OnLoad="gvLookupLoad" TextFormatString="{0}" Width="80px">
                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ModuleID" ReadOnly="True" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Prompt" ReadOnly="True" VisibleIndex="1">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>                                                                            
                                                                </dx:ASPxGridLookup>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Access" Name="Access" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                            <PropertiesTextEdit ClientSideEvents-KeyUp ="UpperCasing" >
                                                            <ClientSideEvents KeyUp="UpperCasing"></ClientSideEvents>
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="FactBox" Name="FactBox" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>                                                       
                                                        <dx:GridViewDataTextColumn FieldName="Field1"  Caption="Field1" Name="Field1" ShowInCustomizationForm="True" VisibleIndex="5" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field2" Caption="Field2"  Name="Field2" ShowInCustomizationForm="True" VisibleIndex="6" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field3" Caption="Field3"  Name="Field3" ShowInCustomizationForm="True" VisibleIndex="7" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field4" Caption="Field4"  Name="Field4" ShowInCustomizationForm="True" VisibleIndex="8" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field5" Caption="Field5"  Name="Field5" ShowInCustomizationForm="True" VisibleIndex="9" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field6" Caption="Field6"  Name="Field6" ShowInCustomizationForm="True" VisibleIndex="10" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field7" Caption="Field7"  Name="Field7" ShowInCustomizationForm="True" VisibleIndex="11" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field8"  Caption="Field8" Name="Field8" ShowInCustomizationForm="True" VisibleIndex="12" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field9"  Caption="Field9" Name="Field9" ShowInCustomizationForm="True" VisibleIndex="13" UnboundType="Bound">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="300"  /> 
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" BatchEditRowValidating="Grid_BatchEditRowValidating"
                                                        BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing"/>
                                                            <SettingsEditing Mode="Batch" />
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
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
                             </dx:ASPxButton>
                           <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
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
    <asp:ObjectDataSource ID="odsDetail" runat="server" DataObjectTypeName="Entity.RoleAndAccessControl+RolesDetail" SelectMethod="getdetail" UpdateMethod="UpdateRolesDetail" TypeName="Entity.RoleAndAccessControl+RolesDetail" DeleteMethod="DeleteRolesDetail" InsertMethod="AddRolesDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="RoleID" QueryStringField="RoleID" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>    
    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT *,ModuleID as prevModuleID FROM IT.UserRolesDetail WHERE RoleID IS NULL " OnInit="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsModuleID" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DISTINCT ModuleID, Prompt FROM IT.MainMenu WHERE ISNULL(ModuleID ,'') != '' " OnInit="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAcess" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT Code, Description, Value FROM IT.SystemSettings WHERE Code LIKE 'ACC_%' OR Description LIKE '%ACCESS%'" OnInit="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsBizPartner" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select EmployeeCode,LastName + ',' + FirstName + ' ' + MiddleName as FullName  from Masterfile.BPEmployeeInfo where ISNULL(IsInactive,0)=0" OnInit="Connection_Init"></asp:SqlDataSource>    
    <!--#endregion-->
</body>
</html>


