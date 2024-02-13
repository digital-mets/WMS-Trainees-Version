﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSKU.aspx.cs" Inherits="GWL.frmSKU" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <title>FG-SKU</title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" />
    <%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
                  #form1 {
            height: 250px; /*Change this whenever needed*/
            }

            .Entry {
             padding: 20px;
             margin: 10px auto;
             background: #FFF;
            }

            .invalidlength *
            {
                background-color:#FFCCCC !important;
            }

            .validlength
            {
                background-color:transparent; 
            }

        .dxeButtonEditSys input,
        .dxeTextBoxSys input{
            /*text-transform:uppercase;*/
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
               console.log(s.GetText());
               console.log(e.value);
           }
           else {
               isValid = true;
           }
       }

       function OnUpdateClick(s, e) { //Add/Edit/Close button function
           console.log(counterror);
           console.log(isValid);
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
               console.log(this);
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
               if (s.cp_valmsg != null) {
                   alert(s.cp_valmsg);
               }
               alert(s.cp_message);
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
           if (e.focusedColumn.fieldName === "CookingStage") {
               glCookingstage.GetInputElement().value = cellInfo.value;
               index = e.visibleIndex;
               closing = true;
               valchange = true;
               console.log('start');
           }

           if (e.focusedColumn.fieldName === "ItemCode") { //Check the column name
               gl.GetInputElement().value = cellInfo.value; //Gets the column value
               isSetTextRequired = true;
           }
           if (e.focusedColumn.fieldName === "PMType") {
               gl2.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "ClassCode") {
               gl3.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "SizeCode") {
               gl4.GetInputElement().value = cellInfo.value;
           }
           
       }

       function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
           var cellInfo = e.rowValues[currentColumn.index];
           if (currentColumn.fieldName === "CookingStage") {
               cellInfo.value = glCookingstage.GetValue();
               cellInfo.text = glCookingstage.GetText().toUpperCase();
               console.log('end');
           }
           if (currentColumn.fieldName === "ItemCode") {
               cellInfo.value = gl.GetValue();
               cellInfo.text = gl.GetText().toUpperCase();
           }
           if (currentColumn.fieldName === "PMType") {
               cellInfo.value = gl2.GetValue();
               cellInfo.text = gl2.GetText().toUpperCase();
           }
           if (currentColumn.fieldName === "ClassCode") {
               cellInfo.value = gl3.GetValue();
               cellInfo.text = gl3.GetText().toUpperCase();
           }
           if (currentColumn.fieldName === "SizeCode") {
               cellInfo.value = gl4.GetValue();
               cellInfo.text = gl4.GetText().toUpperCase();
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
           if (keyCode !== ASPxKey.Tab) return;
           var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
           if (gvClient.batchEditApi[moveActionName]()) {
               ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
           }
       }

       function gridLookup_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
           var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
           if (keyCode == ASPxKey.Enter)
               gvClient.batchEditApi.EndEdit();
           //ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
       }

       function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
           gvClient.batchEditApi.EndEdit();
       }

       //validation
       function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields)
           for (var i = 0; i < gvClient.GetColumnsCount() ; i++) {
               var column = s.GetColumn(i);
               if (column != s.GetColumn(6) && column != s.GetColumn(1) && column != s.GetColumn(7) && column != s.GetColumn(8) && column != s.GetColumn(9) && column != s.GetColumn(10) && column != s.GetColumn(11) && column != s.GetColumn(12) && column != s.GetColumn(13) && column != s.GetColumn(14) && column != s.GetColumn(15) && column != s.GetColumn(16) && column != s.GetColumn(17) && column != s.GetColumn(18) && column != s.GetColumn(19) && column != s.GetColumn(20) && column != s.GetColumn(21) && column != s.GetColumn(22) && column != s.GetColumn(23) && column != s.GetColumn(24) && column != s.GetColumn(13)) {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
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
               var PMType = s.batchEditApi.GetCellValue(e.visibleIndex, "PMType");
               var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
               var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
               factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode
               + '&PMType=' + PMType + '&classcode=' + classcode + '&sizecode=' + sizecode);
           }
           if (e.buttonID == "CountSheet") {
               CSheet.Show();
               var linenum = s.batchEditApi.GetCellValue(e.visibleIndex, "LineNumber");
               var docnumber = getParameterByName('docnumber');
               var transtype = getParameterByName('transtype');
               var entry = getParameterByName('entry');
               CSheet.SetContentUrl('frmCountSheet.aspx?entry=' + entry + '&docnumber=' + docnumber + '&transtype=' + transtype +
                   '&linenumber=' + linenum);
           }
       }


       //function LengthValidation(s, e)
       //{

       //        var value = s.GetValue();
       //        var objInst = CINPMType.GetMainElement();
       //        var str = CINPMType.GetText();
       //        var count = str.length;
       //        if (count > 19) {
       //            if (objInst.className.indexOf('invalidlength') == -1) {
       //                objInst.className += ' invalidlength';
       //            }

       //        }

       //        else if(count < 20)
       //        {
       //            console.log('HI')
       //            if (objInst.className.indexOf('validlength') == -1) {
       //                objInst.className += ' validlength';
       //            } 
       //        }



       //}

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
           gvClient.SetWidth(width - 120);
       }
       //#region For future reference JS 

       //Debugging purposes
       //function start(s, e) {
       //    pass = fieldValue;
       //    console.log("start callback " + pass);
       //}

       //function end(s, e) {
       //    console.log("end callback");
       //}
       //function rowclick(s, e) {
       //    s.GetRowValues(e.visibleIndex, 'ItemCode;PMType;ClassCode;SizeCode', function (data) {
       //        console.log(data[0], data[1], data[2], data[3]);
       //        //splitter.GetPaneByName("Factbox").SetContentUrl('../FactBox/fbBizPartner.aspx?itemcode=' + data[0]
       //        //+ '&PMType='+data[1]+'&classcode='+data[2]+'&sizecode='+data[3]);
       //        factbox.SetContentUrl('../FactBox/fbBizPartner.aspx?itemcode=' + data[0]
       //        + '&PMType=' + data[1] + '&classcode=' + data[2] + '&sizecode=' + data[3]);
       //    });
       //}

       //function getParameterByName(name) {
       //    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
       //    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
       //        results = regex.exec(location.search);
       //    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
       //}

       //function OnControlInitialized(event) {
       //    var entry = getParameterByName('entry');
       //    if (entry == "N") {
       //        splitter.GetPaneByName("Factbox").SetContentUrl('../FactBox/fbBizPartner.aspx');
       //        //splitter.GetPaneByName("Factbox2").SetContentUrl('../FactBox/fbBizPartner.aspx');
       //        //splitter.GetPaneByName("Factbox3").SetContentUrl('../FactBox/fbBizPartner.aspx');
       //        //splitter.GetPaneByName("Factbox4").SetContentUrl('../FactBox/fbBizPartner.aspx');
       //    }
       //}
       //#endregion
       function autoCalc() {

           //console.log("asd");
           var BatchWeight = document.getElementById("cp_frmlayout1_PC_0_spBatchWeight_I").value;
           var YieldPercent = document.getElementById("cp_frmlayout1_PC_0_spYieldPercent_I").value;
           var YieldedBatchWeight = document.getElementById("cp_frmlayout1_PC_0_spYieldedBatchWeight_I").value;
           
           //var YieldedVal = YieldedVal.toFixed(4);

           var product = BatchWeight * parseFloat(YieldPercent) * 0.01;
         
           product = product.toFixed(4);
       
          
           CINYieldedBatchWeight.SetNumber(product);

           //CINYieldedBatchWeight.SetText(product);
           //console.log(product);
           //YieldedBatchWeight.value = product;


           //document.getElementById("cp_frmlayout1_PC_0_spYieldedBatchWeight_I").focus();
           //document.getElementById("cp_frmlayout1_PC_0_spYieldedBatchWeight_I").blur();
           
       }



    </script>
    <!--#endregion-->
</head>
<body style="height: 400px">
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <form id="form1" runat="server" class="Entry">
        <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel runat="server" Text="FG-SKU" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
        <%--<!--#region Region Factbox --> --%><%--<!--#endregion --> --%>
                  <dx:ASPxPopupControl ID="CSheet" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="CSheet" CloseAction="CloseButton" CloseOnEscape="true"
        EnableViewState="False" HeaderImage-Height="10px" HeaderText="" Height="400px" ShowHeader="true" Width="950px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
         ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents CloseUp="function (s, e) { window.location.reload(); }" />
    </dx:ASPxPopupControl>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="400px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="400px" Width="850px" Style="margin-left: -3px; margin-right: 0px;">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                            <%--<!--#region Region Header --> --%>
                            
                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="Header">
                                        <Items>
                                            <dx:LayoutGroup Caption="Information" ColCount="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="SKU Code" Name="SKUCode">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtSKUCode" runat="server" ClientInstanceName="CINSKUCode" MaxLength="10" Width="170px">
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
                                                    <dx:LayoutItem Caption="Product Name" Name="ProductName">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtProductName" runat="server" ClientInstanceName="CINProductName" OnLoad="TextboxLoad" Width="170px">
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

                                                    <dx:LayoutItem Caption="Product Category">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridLookup ID="glProductCategory" DataSourceID="sdsProductCategory" runat="server" OnLoad="LookupLoad" Width="170px" KeyFieldName="ProductCategoryCode" TextFormatString="{0}">
                                                                    <GridViewProperties>
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"/>
                                                                    </GridViewProperties>
                                                                </dx:ASPxGridLookup>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="SKU Type">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridLookup ID="glSKU" DataSourceID="sdsProductType" runat="server" OnLoad="LookupLoad" Width="170px" KeyFieldName="SKUTypeCode" TextFormatString="{0}">
                                                                    <GridViewProperties>
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                    </GridViewProperties>
                                                                </dx:ASPxGridLookup>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutGroup Caption="Unit Weight" ColCount="2" ColSpan="2" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Weight">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxSpinEdit ID="spWeight" runat="server" OnLoad="SpinEdit_Load" Width="170px">
                                                                            
                                                                        </dx:ASPxSpinEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Unit">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxGridLookup ID="glUnit" DataSourceID="sdsUnit" runat="server" OnLoad="LookupLoad" Width="170px" KeyFieldName="UnitCode" TextFormatString="{0}">
                                                                            <GridViewProperties>
                                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                            </GridViewProperties>
                                                                        </dx:ASPxGridLookup>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:LayoutItem Caption="Pc/Pcs">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="spPiece" runat="server" OnLoad="SpinEdit_Load" Width="170px" Number="0">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Batch Weight">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="spBatchWeight" ClientInstanceName="CINBatchWeight" runat="server" OnLoad="SpinEdit_Load" Width="170px" Number="0">
                                                                     <ClientSideEvents KeyUp="autoCalc" />
                                                                    <%--<ClientSideEvents LostFocus="autoCalc" />--%>
                                                                </dx:ASPxSpinEdit>
                                                               
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Yield Percentage (%)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="spYieldPercent" ClientInstanceName="CINYieldPercent" runat="server" OnLoad="SpinEdit_Load" Width="170px" Number="0" DisplayFormatString="{0}%">
                                                                     <ClientSideEvents KeyUp="autoCalc" />
                                                                    <%--<ClientSideEvents LostFocus="autoCalc" />--%>
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Yielded Batch Weight">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="spYieldedBatchWeight" ClientInstanceName="CINYieldedBatchWeight" runat="server" SpinButtons-ClientVisible="false" ReadOnly="true" Width="170px" Number="0" DecimalPlaces="4" >
                                                                    
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Is Small SKU">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxCheckBox ID="cbIsSmallSKU" runat="server" CheckState="Unchecked">
                                                                </dx:ASPxCheckBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="With Cheese">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxCheckBox ID="cbWithCheese" runat="server" CheckState="Unchecked">
                                                                </dx:ASPxCheckBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutGroup Caption="Capacity Planning Color Reference">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Back Color">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxColorEdit ID="txtBackColor" runat="server" OnLoad="ColorEditLoad" Width="170px">
                                                                        </dx:ASPxColorEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                                    <dx:LayoutItem Caption="Packaging Type">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridLookup ID="glPackagingType" DataSourceID="sdsPackagingType" runat="server" OnLoad="LookupLoad" Width="170px" KeyFieldName="PackagingTypeCode" TextFormatString="{0}">
                                                                    <GridViewProperties>
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                    </GridViewProperties>
                                                                </dx:ASPxGridLookup>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="No.of packs per box">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="Quantity" runat="server" OnLoad="SpinEdit_Load" Width="170px" Number="0">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutGroup Caption="" ColCount="2" ColSpan="2" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Client Code" Name="SAPCode">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtSAPCode" runat="server" OnLoad="TextboxLoad" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Client Description">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtSAPDescription" runat="server" OnLoad="TextboxLoad" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>



                                                </Items>
                                            </dx:LayoutGroup>
                                             

                                                <dx:LayoutGroup Caption="Cooking Stage">
                                                <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridView ID="gvClient" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvClient"   KeyFieldName="SKUCode;RecordID" OnBatchUpdate="gv1_BatchUpdate" OnCommandButtonInitialize="gv_CommandButtonInitialize" Width="500px">
                                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="OnInitTrans" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" />
                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                    </SettingsPager>
                                                                    <SettingsEditing Mode="Batch">
                                                                    </SettingsEditing>
                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" ShowFooter="True" ShowStatusBar="Hidden" VerticalScrollBarMode="Auto" />
                                                                    <SettingsBehavior AllowSort="False" />
                                                                    <SettingsCommandButton>
                                                                        <NewButton>
                                                                            <Image IconID="actions_addfile_16x16">
                                                                            </Image>
                                                                        </NewButton>
                                                                        <EditButton>
                                                                            <Image IconID="actions_addfile_16x16">
                                                                            </Image>
                                                                        </EditButton>
                                                                        <DeleteButton>
                                                                            <Image IconID="actions_cancel_16x16">
                                                                            </Image>
                                                                        </DeleteButton>
                                                                    </SettingsCommandButton>
                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowDeleteButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="60px">
                                                                        </dx:GridViewCommandColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="SKUCode" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="CookingStage" FieldName="CookingStage" ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                                                             <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="glCookingstage" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="glCookingstage" DataSourceID="sdsCookingstage" KeyFieldName="CookingStage" TextFormatString="{0}" Width="100px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="CookingStage" ReadOnly="True" Settings-AutoFilterCondition="Contains" VisibleIndex="0">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                      
                                                                                    </Columns>
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                    
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataSpinEditColumn Caption="Std Cooking Time" FieldName="StdCookingTime" Name="StdCookingTime" ShowInCustomizationForm="True" UnboundType="String"  VisibleIndex="20" Width="120px" HeaderStyle-BackColor="#EBEBEB" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" >
                                                                                    <PropertiesSpinEdit  ClientInstanceName="StdCookingTime"  NullDisplayText="0.00" NullText="0.00" DisplayFormatString="{0:N2}"> 
                                                                                     </PropertiesSpinEdit>  
                                                                         </dx:GridViewDataSpinEditColumn> 
                                                                        <dx:GridViewDataSpinEditColumn Caption="Stove Temp Std" FieldName="StoveTempStd" Name="StoveTempStd" ShowInCustomizationForm="True" UnboundType="String"  VisibleIndex="21" Width="120px" HeaderStyle-BackColor="#EBEBEB" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" >
                                                                                    <PropertiesSpinEdit  ClientInstanceName="StoveTempStd"  NullDisplayText="0.00" NullText="0.00" DisplayFormatString="{0:N2}"> 
                                                                                     </PropertiesSpinEdit>  
                                                                         </dx:GridViewDataSpinEditColumn> 
                                                                        <dx:GridViewDataSpinEditColumn Caption="Percent Humidity Std" FieldName="PercentHumidityStd" Name="PercentHumidityStd" ShowInCustomizationForm="True" UnboundType="String"  VisibleIndex="21" Width="120px" HeaderStyle-BackColor="#EBEBEB" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" >
                                                                                    <PropertiesSpinEdit  ClientInstanceName="PercentHumidityStd"  NullDisplayText="0.00" NullText="0.00" DisplayFormatString="{0:N2}"> 
                                                                                     </PropertiesSpinEdit>  
                                                                         </dx:GridViewDataSpinEditColumn> 
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
                                            <dx:LayoutItem Caption="Field 1:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server" OnLoad="TextboxLoad">
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
                                            <dx:LayoutItem Caption="Field 3:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" OnLoad="TextboxLoad">
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
                                            <dx:LayoutItem Caption="Field 5:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server"  OnLoad="TextboxLoad">
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
                                            <dx:LayoutItem Caption="Field 7:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server" OnLoad="TextboxLoad">
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
                                            <dx:LayoutItem Caption="Field 9:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Audit Trail" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Added By" Name="AddedBy">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Added Date" Name="AddedDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedDate" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited By" Name="LastEditedBy">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited Date" Name="LastEditedDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedDate" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Activated By" Name="ActivatedBy">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtActivatedBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Activated Date" Name="ActivatedDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtActivatedDate" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Deactivated By" Name="DeactivatedBy">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDeactivatedBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Deactivated Date" Name="DeactivatedDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDeactivatedDate" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:TabbedLayoutGroup>
                            <%-- <!--#endregion --> --%>

                            <%--<!--#region Region Details --> --%>


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
    <asp:ObjectDataSource ID="odsHeader" runat="server" SelectMethod="getdata" TypeName="Entity.SKU" DataObjectTypeName="Entity.SKU" InsertMethod="InsertData" UpdateMethod="UpdateData">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="SKUCode" SessionField="SKUCode" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDetail" runat="server" SelectMethod="getdetail" TypeName="Entity.SKU+ItemMasterClient" DataObjectTypeName="Entity.SKU+ItemMasterClient" DeleteMethod="DeleteItemDetail" InsertMethod="AddItemDetail" UpdateMethod="UpdateItemDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="SKUCode" QueryStringField="SKUCode" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM  wms.ItemAdjustmentDetail where DocNumber  is null " >
  
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsDetail4" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM  masterfile.FGSKUDetailCooking where SKUCode  is null "
            OnInit="Connection_Init"></asp:SqlDataSource>
     <asp:ObjectDataSource ID="odsDetail4" runat="server" SelectMethod="getdetail" TypeName="Entity.SKU+ItemMasterClient" DataObjectTypeName="Entity.SKU+ItemMasterClient" DeleteMethod="DeleteItemDetail" InsertMethod="AddItemDetail" UpdateMethod="UpdateItemDetail">
            <SelectParameters>
                <asp:QueryStringParameter Name="SKUCode" QueryStringField="SKUCode" Type="String" />
                <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

     <%-- Product Category Lookup --%>
    <asp:SqlDataSource ID="sdsProductCategory" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT ProductCategoryCode,Description FROM Masterfile.ProductCategory WHERE ISNULL([IsInactive],0) = 0 and ProductCategoryCode='FG'" OnInit = "Connection_Init"></asp:SqlDataSource>

     <%--Product Type Lookup--%>
    <asp:SqlDataSource ID="sdsProductType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT SKUTypeCode,Description FROM Masterfile.ProductType WHERE ISNULL([IsInactive],0) = 0" OnInit = "Connection_Init"></asp:SqlDataSource>

    <%--Product Type Lookup--%>
    <asp:SqlDataSource ID="sdsPackagingType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT PackagingTypeCode,Description FROM Masterfile.PackagingType WHERE ISNULL([IsInactive],0) = 0" OnInit = "Connection_Init"></asp:SqlDataSource>

    <%--Unit Lookup--%>
    <asp:SqlDataSource ID="sdsUnit" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT UnitCode,Description FROM Masterfile.Unit WHERE ISNULL([IsInactive],0) = 0" OnInit = "Connection_Init"></asp:SqlDataSource>
     <%--Unit Lookup--%>
    <asp:SqlDataSource ID="sdsCookingstage" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select distinct CookingStage from Masterfile.FGSKUDetailCooking" OnInit = "Connection_Init"></asp:SqlDataSource>

    
    
     <!--#endregion-->
</body>
</html>
