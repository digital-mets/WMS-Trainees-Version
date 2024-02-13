<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMaterialPlanning.aspx.cs" Inherits="GWL.frmMaterialPlanning" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
<title></title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <script src="../js/jquery-1.6.1.min.js" type="text/javascript"></script><%--NEWADD--%>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script><%--NEWADD--%>
     <!--#region Region Javascript-->


        <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
#form1 {
height: 580px; /*Change this whenever needed*/
}

.Entry {
/*width: 806px; /*Change this whenever needed*/
/*padding: 30px;
margin: 40px auto;
background: #FFF;
border-radius: 10px;
-webkit-border-radius: 10px;
-moz-border-radius: 10px;
box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
-moz-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
-webkit-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);*/

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
       var count = 1;

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

           var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
           for (var i = 0; i < indicies.length; i++) {
               if (gv1.batchEditHelper.IsNewItem(indicies[i])) {
                   gv1.batchEditApi.ValidateRow(indicies[i]);
                   //gv1.batchEditApi.StartEdit(indicies[i], gv1.GetColumnByField("Qty").index);
               }
               else {
                   var key = gv1.GetRowKey(indicies[i]);
                   if (gv1.batchEditHelper.IsDeletedItem(key))
                       console.log("deleted row " + indicies[i]);
                   else {
                       gv1.batchEditApi.ValidateRow(indicies[i]);
                      // gv1.batchEditApi.StartEdit(indicies[i], gv1.GetColumnByField("Qty").index);
                   }
               }
           }


           if (isValid && counterror < 1 || btnmode == "Close") { //check if there's no error then proceed to callback
               //Sends request to server side
               if (btnmode == "Save") {
                   cp.PerformCallback("Save");
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


       var initgv = 'true';
       var vatrate = 0;
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

           if (s.cp_forceclose) {//NEWADD
               delete (s.cp_forceclose);
               window.close();
           }

           if(s.cp_clicked == "JO")
           {
               delete (s.cp_clicked);
               console.log('JO')
               CINJOTab.SetEnabled(false);
               CINConsolidated.SetEnabled(true);
           }

           if(s.cp_clicked == "CONSOLIDATED")
           {
               delete (s.cp_clicked);
               CINJOTab.SetEnabled(true);
               CINConsolidated.SetEnabled(false);
           }

           if (s.cp_multiplepr) {
               delete (s.cp_multiplepr);
               CINMPR.Hide();
           }

       }

       var index;
       var closing;
       var valchange = false;
       var itemc; //variable required for lookup
       var currentColumn = null;
       var valchange_VAT = false;
       var isSetTextRequired = false;
       var linecount = 1;
       var VATCode = "";
       function OnStartEditing(s, e) {//On start edit grid function     
           currentColumn = e.focusedColumn;
           var cellInfo = e.rowValues[e.focusedColumn.index];
           itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "PropertyNumber"); //needed var for all lookups; this is where the lookups vary for
           //if (e.visibleIndex < 0) {//new row
           //    var linenumber = s.GetColumnByField("LineNumber");
           //    e.rowValues[linenumber.index].value = linecount++; // or any other default value
           //}

           index = e.visibleIndex;
           var entry = getParameterByName('entry');

           if (entry == "V") {
               e.cancel = true; //this will made the gridview readonly
           }
           if (entry != "V")
           {
               if (e.focusedColumn.fieldName === "KeySupplier") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "MOQ") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "ReorderLevel") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "LeadTime") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "OrderingUnit") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "ForOrder") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "PendingOrder") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "DeliveryDate") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "CreatePR") {
                   e.cancel = true;
               } 

               if (e.focusedColumn.fieldName === "Supplier") {
                   e.cancel = true;
               }
               if (e.focusedColumn.fieldName === "VATCode") {
                   if (s.batchEditApi.GetCellValue(e.visibleIndex, "IsVat") == false) {
                       e.cancel = true;
                   }
                   else {
                       CINVATCode.GetInputElement().value = cellInfo.value; //Gets the column value
                       isSetTextRequired = true;
                   }
               }

                   //if (e.focusedColumn.fieldName === "PropertyNumber") { //Check the column name
                   //    gl.GetInputElement().value = cellInfo.value; //Gets the column value
                   //    isSetTextRequired = true;
                   //    index = e.visibleIndex;
                   //    closing = true;
                   //}
                   //if (e.focusedColumn.fieldName === "ColorCode") {
                   //    gl2.GetInputElement().value = cellInfo.value;
                   //}
                   //if (e.focusedColumn.fieldName === "ClassCode") {
                   //    gl3.GetInputElement().value = cellInfo.value;
                   //}
                   //if (e.focusedColumn.fieldName === "SizeCode") {
                   //    gl4.GetInputElement().value = cellInfo.value;
                   //}
                   //if (e.focusedColumn.fieldName === "Qty") {
                   //    CINQty.GetInputElement().value = cellInfo.value;
                   //}
                   //if (e.focusedColumn.fieldName === "UnitCost") {
                   //    CINUnitCost.GetInputElement().value = cellInfo.value;
                   //}
                   //if (e.focusedColumn.fieldName === "AccumulatedDepreciation") {
                   //    CINAccumulatedDepreciation.GetInputElement().value = cellInfo.value;
                   //}
                   if (e.focusedColumn.fieldName === "Supplier") {
                       CINSupplier.GetInputElement().value = cellInfo.value;
                   }
                   if (e.focusedColumn.fieldName === "DeliveryDate") {
                       CINDeliveryDate.GetInputElement().value = cellInfo.value;
                   }
           }
           
       }


       //Kapag umalis ka sa field na yun. hindi mawawala yung value.
       function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
           var cellInfo = e.rowValues[currentColumn.index];
           //if (currentColumn.fieldName === "PropertyNumber") {
           //    cellInfo.value = gl.GetValue();
           //    cellInfo.text = gl.GetText();
           //}
           //if (currentColumn.fieldName === "ColorCode") {
           //    cellInfo.value = gl2.GetValue();
           //    cellInfo.text = gl2.GetText();
           //}
           //if (currentColumn.fieldName === "ClassCode") {
           //    cellInfo.value = gl3.GetValue();
           //    cellInfo.text = gl3.GetText();
           //}
           //if (currentColumn.fieldName === "Qty") {
           //    cellInfo.value = CINQty.GetValue();
           //    cellInfo.text = CINQty.GetText();
           //} 
           //if (currentColumn.fieldName === "UnitCost") {
           //    cellInfo.value = CINUnitCost.GetValue();
           //    cellInfo.text = CINUnitCost.GetText();
           //}
           //if (currentColumn.fieldName === "AccumulatedDepreciation") {
           //    cellInfo.value = CINAccumulatedDepreciation.GetValue();
           //    cellInfo.text = CINAccumulatedDepreciation.GetText();
           //}
           if (currentColumn.fieldName === "Supplier") {
               cellInfo.value = CINSupplier.GetValue();
               cellInfo.text = CINSupplier.GetText();
           }
           if (currentColumn.fieldName === "DeliveryDate") {
               cellInfo.value = CINDeliveryDate.GetValue();
               cellInfo.text = CINDeliveryDate.GetText();
           }

           //if (valchange) {

           //    valchange = false;
           //    closing = false;
           //    for (var i = 0; i < s.GetColumnsCount() ; i++) {
           //        var column = s.GetColumn(i);
           //        if (column.visible == false || column.fieldName == undefined)
           //            continue;
           //        ProcessCells(0, e, column, s);
           //    }
           //}

       }

       var val;
       var temp;
       function ProcessCells(selectedIndex, e, column, s) {
           var totalcostasset = 0.00;
           if (val == null) {
               val = ";;;;;;;;;;";
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
           if (temp[10] == null) {
               temp[10] = "";
           }
           if (selectedIndex == 0) {
               if (column.fieldName == "ColorCode") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[0]);
               }
               if (column.fieldName == "ClassCode") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[1]);
               }
               if (column.fieldName == "SizeCode") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[2]);
               }
               if (column.fieldName == "ItemCode") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[3]);
               }
               if (column.fieldName == "Qty") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[4]);
               }
               if (column.fieldName == "UnitCost") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[5]);
               }
               if (column.fieldName == "AccumulatedDepreciation") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[6]);
               }
               if (column.fieldName == "PropertyStatus") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[7]);
               }
               if (column.fieldName == "IsVat") {
                   if (temp[8] == "True") {
                       s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, CINIsVAT.SetChecked = true);
                   }
                   else {
                       s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, CINIsVAT.SetChecked = false);
                   }
               }
               if (column.fieldName == "VATCode") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[9]);
               }
               if (column.fieldName == "Rate") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[10]);
               }
               if (column.fieldName == "OrigQty") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, temp[4]);
               }


               

           }
       }

       var identifier;
       function GridEndChoice(s, e) {

           identifier = s.GetGridView().cp_identifier;
           val = s.GetGridView().cp_codes;
           temp = val.split(';');
           val_VAT = s.GetGridView().cp_codes;
           temp_VAT = val_VAT.split(';');

           //console.log(identifier + " idetifier")
           //console.log(val + " VAL")
           //console.log(val_VAT + " VAL_VAT")

           console.log(temp + " ito sila!")
           if (identifier == "ItemCode") {
               GridEnd();
           }

           if (identifier == "VAT") {
               GridEnd_VAT();
               gv1.batchEditApi.EndEdit();
           }
       }


       function GridEnd(s, e) {

           if (closing == true) {
               var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
               for (var i = 0; i < indicies.length; i++) {
                   gv1.batchEditApi.ValidateRow(indicies[i]);
                   gv1.batchEditApi.StartEdit(indicies[i], gv1.GetColumnByField("Qty").index);
               }
               gv1.batchEditApi.EndEdit();
               loader.Hide();
           }
       }


       function GridEnd_VAT(s, e) {
           if (valchange_VAT) {
               valchange_VAT = false;
               var column = gv1.GetColumn(12);
               ProcessCells_VAT(0, index, column, gv1);
           }
       }


       function ProcessCells_VAT(selectedIndex, focused, column, s) {//Auto calculate qty function :D
           console.log("ProcessCells_VAT")
           if (val_VAT == null) {
               val_VAT = ";";
               temp_VAT = val_VAT.split(';');
           }
           if (temp_VAT[0] == null) {
               temp_VAT[0] = 0;
           }
           if (selectedIndex == 0) {
               console.log(temp_VAT[0] + "TEMPVAT")
               s.batchEditApi.SetCellValue(focused, "Rate", temp_VAT[0]);
               autocalculate();
           }
       }
       
       //function lookup(s, e) {
           //if (isSetTextRequired) {//Sets the text during lookup for item code
              // s.SetText(s.GetInputElement().value);
              // isSetTextRequired = false;
         //  }
    //   }

       function lookup(s, e) {
           if (isSetTextRequired) {//Sets the text during lookup for item code
               //s.SetText(s.GetInputElement().value);
               var propertynum;
               var getallpropertynum;
               isSetTextRequired = false;
               var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
               for (var i = 0; i < indicies.length; i++) {
                   if (gv1.batchEditHelper.IsNewItem(indicies[i])) {
                       console.log(indicies)
                       propertynum = gv1.batchEditApi.GetCellValue(indicies[i], "PropertyNumber") + ";";

                       getallpropertynum += propertynum;
                       console.log(getallpropertynum + " ALL")
                   }

                   else {
                       var keyB = gv1.GetRowKey(indicies[i]);
                       if (gv1.batchEditHelper.IsDeletedItem(keyB))
                           console.log("deleted row " + indicies[i]);
                       else {
                           propertynum = gv1.batchEditApi.GetCellValue(indicies[i], "PropertyNumber") + ";";
                           getallpropertynum += propertynum;

                       }
                   }

               }
               //gl2.GetGridView().PerformCallback('CheckPropertyNumber' + '|' + getallpropertynum + '|' + 'itemc');
               console.log(gl.GetGridView() + '        gl.GetGridView()');
               gl.GetGridView().PerformCallback('CheckPNumber' + '|' + getallpropertynum + '|' + 'itemc');
               e.processOnServer = false;
           }
       }

       function CheckPropertyNumber(s, e) {
           var propertynum;
           var getallpropertynum;
           var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
           for (var i = 0; i < indicies.length; i++) {
               if (gv1.batchEditHelper.IsNewItem(indicies[i])) {
                   propertynum = gv1.batchEditApi.GetCellValue(indicies[i], "PropertyNumber") + ";";

                   getallpropertynum += propertynum;
                   console.log(getallpropertynum + " ALL")
               }

               else {
                   var keyB = gv1.GetRowKey(indicies[i]);
                   if (gv1.batchEditHelper.IsDeletedItem(keyB))
                       console.log("deleted row " + indicies[i]);
                   else {
                       propertynum = gv1.batchEditApi.GetCellValue(indicies[i], "PropertyNumber") + ";";
                       getallpropertynum += propertynum;

                   }
               }

           }
           console.log('ditopota')
           gl.GetGridView().PerformCallback('CheckPropertyNumber' + '|' + getallpropertynum + '|' + 'itemc');
           e.processOnServer = false;
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

       function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
           gv1.batchEditApi.EndEdit();
       }

       //validation
       function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields)

           for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
               var column = s.GetColumn(i);
               var chckd;
               var chckd2;
               var bulk;
               var bulk2;

               if (column.fieldName == "IsVat") {
                   //console.log('isvat')
                   var cellValidationInfo = e.validationInfo[column.index];
                   if (!cellValidationInfo) continue;
                   var value = cellValidationInfo.value;

                   //console.log(value + ' IsVat value')
                   if (value == true) {
                       chckd2 = true;
                   }
               }
               if (column.fieldName == "VATCode") {
                   var cellValidationInfo = e.validationInfo[column.index];
                   if (!cellValidationInfo) continue;
                   var value = cellValidationInfo.value;

                   //console.log(value + ' value')

                   //console.log(chckd2 + ' chckd2')
                   if ((!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "" || ASPxClientUtils.Trim(value) == "NONV") && chckd2 == true) {
                       cellValidationInfo.isValid = false;
                       cellValidationInfo.errorText = column.fieldName + " is required!";
                       isValid = false;
                   }
               }

               if (column.fieldName == "Qty") {
                   var originalqty = s.batchEditApi.GetCellValue(e.visibleIndex, "OrigQty");
                   var tempqty = s.batchEditApi.GetCellValue(e.visibleIndex, "Qty");
                   
                   var cellValidationInfo = e.validationInfo[column.index];
                   if (!cellValidationInfo) continue;
                   var value = cellValidationInfo.value;
                   if ((!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "0" || ASPxClientUtils.Trim(value) == "0.00" || ASPxClientUtils.Trim(value) == null || ASPxClientUtils.Trim(value) < 1)) {
                       cellValidationInfo.isValid = false;
                       cellValidationInfo.errorText = column.fieldName + " is required!";
                       isValid = false;
                       //console.log(ASPxClientUtils.Trim(value) + ' ASPxClientUtils.Trim(value)')
                   }
                   if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) > originalqty) {
                       cellValidationInfo.isValid = false;
                       cellValidationInfo.errorText = column.fieldName + " exceed the quantity in record!";
                       isValid = false;
                   }
               }
           }
       }


       function OnCustomClick(s, e) {
           if (e.buttonID == "Details") {
               var itemcode = s.batchEditApi.GetCellValue(e.visibleIndex, "PropertyNumber");
               var colorcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ColorCode");
               var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
               var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
               var unitbase = s.batchEditApi.GetCellValue(e.visibleIndex, "UnitBase");
               factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode
               + '&colorcode=' + colorcode + '&classcode=' + classcode + '&sizecode=' + sizecode + '&unitbase=' + unitbase);
           }

           if (e.buttonID == "Delete") {
               gv1.DeleteRow(e.visibleIndex);
               autocalculate(s, e);
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


       function autocalculate(s, e) {
           OnInitTrans();

           var qty = 0.00;
           var unitprice = 0.00;
           var unitcost = 0.00;
           var depreciation = 0.00;
           var soldamount = 0.00;
           var costamount = 0.00;
           var depreciationsmount = 0.00;

           var qtyVAT = 0.00;
           var unitpriceVAT = 0.00;
           var soldamountVAT = 0.00;
           var qtyNVAT = 0.00;
           var unitpriceNVAT = 0.00;
           var soldamountNVAT = 0.00;
           var totalamountsoldNVAT = 0.00;

           var totalamountsold = 0.00;
           var totalcostasset = 0.00;
           var totalaccumulateddepreciation = 0.00;
           var netbookvalue = 0.00;
           var totalgainloss = 0.00;
           var grossnonvatableamount = 0.00;
           var grossvatableamount = 0.00;
           var rate = 0.00;

           setTimeout(function () { //New Rows
               var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();

                   for (var i = 0; i < indicies.length; i++)
                   {
                       if (gv1.batchEditHelper.IsNewItem(indicies[i])) {
                           qty = gv1.batchEditApi.GetCellValue(indicies[i], "Qty");
                           unitprice = gv1.batchEditApi.GetCellValue(indicies[i], "UnitPrice");
                           unitcost = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                           depreciation = gv1.batchEditApi.GetCellValue(indicies[i], "AccumulatedDepreciation");
                           
                           //Check if input Quanties are Negative
                           //qty = qty < 0 ? 0 : qty;
                           //unitprice = unitprice < 0 ? 0 : unitprice;
                           if (qty < 0)
                           {
                               qty = 0;
                               gv1.batchEditApi.SetCellValue(indicies[i], "Qty", qty.toFixed(2));
                           }
                           if (unitprice < 0) {
                               unitprice = 0;
                               gv1.batchEditApi.SetCellValue(indicies[i], "UnitPrice", unitprice.toFixed(2));
                           }
                           // End Of Negative Checking - LGE (02/03/2016)

                           soldamount = qty * unitprice;
                           costamount = qty * unitcost;
                           depreciationsmount = depreciation * 1;
                           totalamountsold += soldamount;
                           totalcostasset += costamount;
                           totalaccumulateddepreciation += depreciationsmount;
                           netbookvalue = totalcostasset - totalaccumulateddepreciation;
                           totalgainloss = totalamountsold - netbookvalue;

                           var cb = gv1.batchEditApi.GetCellValue(indicies[i], "IsVat");

                           if (cb == true)
                           {

                               console.log("checkpasok");
                               qtyVAT = gv1.batchEditApi.GetCellValue(indicies[i], "Qty");
                               unitpriceVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitPrice");
                               rate = gv1.batchEditApi.GetCellValue(indicies[i], "Rate");
                               console.log(rate + ' r a t e');
                               soldamountVAT = qtyVAT * unitpriceVAT;

                               grossvatableamount += soldamountVAT * rate;
                           }
                           else
                           {

                               console.log("unchekpasok");
                               qtyNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "Qty");
                               unitpriceNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitPrice");

                               soldamountNVAT += qtyNVAT * unitpriceNVAT;
                               totalamountsoldNVAT += soldamountNVAT

                           }

                           gv1.batchEditApi.SetCellValue(indicies[i], "SoldAmount", soldamount.toFixed(2));

                       } //END OF IsNewRow indicies


                       else { //Existing Rows
                           var key = gv1.GetRowKey(indicies[i]);
                           if (gv1.batchEditHelper.IsDeletedItem(key))
                               console.log("deleted row " + indicies[i]);
                           else {
                               qty = gv1.batchEditApi.GetCellValue(indicies[i], "Qty");
                               unitprice = gv1.batchEditApi.GetCellValue(indicies[i], "UnitPrice");
                               unitcost = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                               depreciation = gv1.batchEditApi.GetCellValue(indicies[i], "AccumulatedDepreciation");

                               //Check if input Quanties are Negative
                               if (qty < 0) {
                                   qty = 0;
                                   gv1.batchEditApi.SetCellValue(indicies[i], "Qty", qty.toFixed(2));
                               }
                               if (unitprice < 0) {
                                   unitprice = 0;
                                   gv1.batchEditApi.SetCellValue(indicies[i], "UnitPrice", unitprice.toFixed(2));
                               }
                               // End Of Negative Checking - LGE (02/03/2016)

                               soldamount = qty * unitprice;
                               costamount = qty * unitcost;
                               depreciationsmount = depreciation * 1;
                               totalamountsold += soldamount;
                               totalcostasset += costamount;
                               totalaccumulateddepreciation += depreciationsmount;
                               netbookvalue = totalcostasset - totalaccumulateddepreciation;
                               totalgainloss = netbookvalue - totalamountsold;

                               var cb = gv1.batchEditApi.GetCellValue(indicies[i], "IsVat")
                               
                               if (cb == true) {
                                   qtyVAT = gv1.batchEditApi.GetCellValue(indicies[i], "Qty");
                                   unitpriceVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitPrice");

                                   soldamountVAT = qtyVAT * unitpriceVAT;

                                   grossvatableamount += soldamountVAT * vatrate;
                               }
                               else {
                                   qtyNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "Qty");
                                   unitpriceNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitPrice");

                                   soldamountNVAT = qtyNVAT * unitpriceNVAT;
                                   totalamountsoldNVAT += soldamountNVAT

                                   
                               }
                               gv1.batchEditApi.SetCellValue(indicies[i], "SoldAmount", soldamount.toFixed(2));
                           }


                       } // END OF ELSE EXISTING ROWS

                   } //END OF FOR LOOP (indicies)
               
                   CINTotalAmountSold.SetText(totalamountsold.toFixed(2));
                   CINTotalCostAsset.SetText(totalcostasset.toFixed(2));
                   CINTotalAccumulatedDepreciationRecord.SetText(totalaccumulateddepreciation.toFixed(2));
                   CINNetBookValue.SetText(netbookvalue.toFixed(2));
                   CINTotalGainLoss.SetText(totalgainloss.toFixed(2));
                   CINTotalNonGrossVatableAmount.SetText(totalamountsoldNVAT.toFixed(2));
                   CINTotalGrossVatableAmount.SetText(grossvatableamount.toFixed(2));
                   //console.log(CINTotalAmountSold.GetValue() + " FINAL TotalAmountSold")
                   //console.log(CINSoldAmount.GetValue() + "  FINAL")
                   //console.log(CINTotalCostAsset.GetValue() + "  FINAL TotalCostAsset")


           }, 500);
       }

       function Generate(s, e) {
           var generate = confirm("Are you sure you want to generate the selected Job Order Number?");
           if (generate) {
               gv1.CancelEdit();
               cp.PerformCallback('Generate');
               e.processOnServer = false;
               //console.log(CINDueDateFrom.GetValue());
               //console.log(CINDueDateTo.GetValue());


               if (!CINJobOrder.GetText() && !CINCustomer.GetText() && !CINDueDateFrom.GetValue() && !CINDueDateTo.GetValue()) {
                   alert('Select at least one parameter filter!');
               }
           }
           
       }
       
       function MultiplePR(s, e) {
           var generate = confirm("Generate multiple Item using selected Job Order Number?");
           if (generate) {
               CINMPR.Show();
               CINgvMultiplePR.CancelEdit();
               cp.PerformCallback('MultiplePR');
               e.processOnServer = false;   
           }
           CINMultiplePR.SetEnabled(true);
       }

       function CreatePRPOPUP(s, e) {
           var generate = confirm("Are you sure do you want to create PR using this items?");
           if (generate) {
               //if (e.isSelected) {
               var keys = CINgvMultiplePR.GetSelectedKeysOnPage();
               console.log(keys + 'dito')
               gvdetailgrid.PerformCallback('MultiplePRCreation' + '|' + keys);
               //gvdetailgrid.PerformCallback('');
               //}
           }
       }

       function OnGetSelectedFieldValues(selectedVal) {
           return selectedVal;
       }

       function CreatePR(s, e) {
           var supplier = '';
           var deliverydate = '';
           var generate = confirm("Are you sure you want to Create PR using this detail?");
           if (generate) {
               //CINCreatePR.PerformCallback('CreatePR' + '|' + CINSupplier.GetValue() + '|' + CINDeliveryDate.GetValue());
               //e.processOnServer = false;
               //var cellInfo = e.rowValues[currentColumn.index];
               //if (currentColumn.fieldName === "Supplier") {
               //    cellInfo.value = gl.GetValue();
               //    cellInfo.text = gl.GetText();
               //var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
               //for (var i = 0; i < indicies.length; i++) {
               //gv1.batchEditApi.SetCellValue(indicies[i], 'VATCode', VATCode);
               var indicies = gvdetailgrid.batchEditHelper.GetDataItemVisibleIndices();
               
               for (var i = 0; i < indicies.length; i++) {
                   supplier = gvdetailgrid.batchEditApi.GetCellValue(indicies[i], "Supplier");
                   deliverydate = gvdetailgrid.batchEditApi.GetCellValue(indicies[i], "DeliveryDate");
                   deliverydate = deliverydate.toLocaleDateString();
               }
               //console.log(deliverydate)
               gvdetailgrid.PerformCallback('CreatePR' + '|' + supplier + '|' + deliverydate);
               
           }
       }

       function EndCallbackParaSaGrid(s, e)
       {
           var id = s.cp_creation;
           delete (s.cp_creation);

           if (id == "CreateSinglePR" || id == "CreateMultiplePR") {
               alert('Successfully Created!');
           }
       }


       function OnSelectionChanged(s, e) {
           if (e.isSelected)
           {
               var keys = s.GetSelectedKeysOnPage();
               console.log('dito')
               gvdetailgrid.PerformCallback(keys);
               CINMultiplePR.SetEnabled(true);

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
           gv1.SetWidth(width - 120);
           gvdetailgrid.SetWidth(width - 120);
       }

       function btnjoclicked(s, e)
       {
           
           cp.PerformCallback('joclickedcase');
           e.processOnServer = false;
       }

       function btnconsolidatedclicked(s, e) {
           
           cp.PerformCallback('consolidatedclickedcase');
           e.processOnServer = false;
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
       //    s.GetRowValues(e.visibleIndex, 'ItemCode;ColorCode;ClassCode;SizeCode', function (data) {
       //        console.log(data[0], data[1], data[2], data[3]);
       //        //splitter.GetPaneByName("Factbox").SetContentUrl('../FactBox/fbBizPartner.aspx?itemcode=' + data[0]
       //        //+ '&colorcode='+data[1]+'&classcode='+data[2]+'&sizecode='+data[3]);
       //        factbox.SetContentUrl('../FactBox/fbBizPartner.aspx?itemcode=' + data[0]
       //        + '&colorcode=' + data[1] + '&classcode=' + data[2] + '&sizecode=' + data[3]);
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
                                <dx:ASPxLabel runat="server" Text="Material Planning" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
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
        
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="910px" ClientInstanceName="cp" OnCallback="cp_Callback">
           <%-- <ClientSideEvents EndCallback="gridView_EndCallback" Init="function(){ if(initgv == 'true'){ cp.PerformCallback('getvat'); initgv = 'false'; }}"></ClientSideEvents>
            --%><ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
            </PanelCollection>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxPopupControl ID="MPR" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="CINMPR" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" HeaderText="" Height="400px" ShowHeader="true" Width="950px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
             ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    
                    <dx:ASPxGridView ID="gvMultiplePR" runat="server" AutoGenerateColumns="False" Width="900px" 
                            OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="CINgvMultiplePR" 
                            OnRowValidating="grid_RowValidating" OnBatchUpdate="gv1_BatchUpdate" OnCustomButtonInitialize="gv1_CustomButtonInitialize">
                                                                            
                                <ClientSideEvents BatchEditConfirmShowing="OnConfirm"
                                    BatchEditStartEditing="OnStartEditing"  BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                <ClientSideEvents EndCallback="EndCallbackParaSaGrid" />
                                <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                <SettingsPager Mode="ShowAllRecords"/> 
                                <SettingsEditing Mode="Batch" />
                                <Settings VerticalScrollableHeight="250" VerticalScrollBarMode="Auto" /> 
                                <SettingsBehavior AllowSelectSingleRowOnly="false" />
                                <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Image" Width="50px" SelectAllCheckboxMode="Page">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataTextColumn Caption="LineNumber" FieldName="LineNumber" Name="LineNumber" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="0px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Item" FieldName="ItemCode" Name="ItemCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="200">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Color" FieldName="ColorCode" Name="ColorCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="150">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Class" FieldName="ClassCode" Name="ClassCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="150">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Size" FieldName="SizeCode" Name="SizeCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="6" Width="150">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="NeededQty" FieldName="NeededQty" Name="NeededQty" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="9">
                                        </dx:GridViewDataTextColumn>        
                                </Columns>
                            </dx:ASPxGridView>
                            <dx:ASPxButton ID="btnCreatePRPOPUP" ClientInstanceName="CINbtnCreatePRPOPUP" runat="server" Width="170px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" onload="Generate_Btn" ClientVisible="true" Text="Create PR" Theme="MetropolisBlue">
                                <ClientSideEvents Click="CreatePRPOPUP" />
                            </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <%--<ClientSideEvents CloseUp="function (s, e) { cp.PerformCallback('refgrid') }" />--%>
        </dx:ASPxPopupControl>
                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="565px" Width="850px" style="margin-left: -3px">
                         <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                       
                        <Items>
                            <dx:TabbedLayoutGroup ActiveTabIndex="0">
                                <Items>
                                    <dx:LayoutGroup Caption="General" ColCount="2">
                                        <Items>

                                            <dx:LayoutItem Caption="Document Number" Name="DocNumber">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDocnumber" runat="server" Width="170px" OnLoad="TextboxLoad" Enabled="false">
                                                        <ClientSideEvents Validation="OnValidation"/>
                                                            <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True"/>
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>


                                            <dx:LayoutItem Caption="Document Date" Name="DocDate" RequiredMarkDisplayMode="Required">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtDocDate" runat="server" Width="170px" OnLoad="Date_Load" OnInit="dtpDocDate_Init" >
                                                        <ClientSideEvents Validation="OnValidation"/>
                                                            <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True"/>
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>


                                            <dx:LayoutItem Caption="Job Order">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="glJobOrder" ClientInstanceName="CINJobOrder" runat="server" AutoGenerateColumns="true" Width="170px" DataSourceID="JobOrderLookup" KeyFieldName="DocNumber" OnLoad="LookupLoad" TextFormatString="{0}">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="DocNumber" ReadOnly="True" VisibleIndex="0" Settings-AutoFilterCondition="Contains">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <ClientSideEvents DropDown="function(s,e){//CINJobOrder.GetGridView().Refresh();
                                                                                                    s.SetText(s.GetInputElement().value);
                                                                                                  }" />
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>


                                            <dx:LayoutItem Caption="Customer">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="glCustomer" ClientInstanceName="CINCustomer" runat="server" AutoGenerateColumns="false" Width="170px" DataSourceID="CustomerLookup" KeyFieldName="BizPartnerCode" OnLoad="LookupLoad" TextFormatString="{0}"
                                                            >
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                            <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="True" VisibleIndex="0">
                                                              <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Name" ReadOnly="True" VisibleIndex="1">
                                                              <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>                                        
                                                            </Columns>
                                                            <ClientSideEvents />
                                                            <ClientSideEvents DropDown="function(s,e){//CINCustomer.GetGridView().Refresh();
                                                                                                    s.SetText(s.GetInputElement().value);
                                                                                                  }" />
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Due Date From" Name="DueDateFrom">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtDueDateFrom" ClientInstanceName="CINDueDateFrom" runat="server" Width="170px" OnLoad="Date_Load" OnInit="dtpDocDate_Init" >
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>


                                            <dx:LayoutItem Caption="Due Date To" Name="DueDateTo">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtDueDateTo" ClientInstanceName="CINDueDateTo" runat="server" Width="170px" OnLoad="Date_Load" OnInit="dtpDocDate_Init" >
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="" Name="btnExplodeMaterial">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxButton ID="btnExplodeMaterial" ClientInstanceName="CINExplodeMaterial" runat="server" Width="170px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" onload="Generate_Btn" ClientVisible="true" Text="Generate" Theme="MetropolisBlue">
                                                            <ClientSideEvents Click="Generate" />
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="" Name="btnMultiplePR">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxButton ID="btnMultiplePR" ClientInstanceName="CINMultiplePR" runat="server" Width="170px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" onload="Generate_Btn" ClientVisible="true" Text="Multiple Item PR" Theme="MetropolisBlue">
                                                            <ClientSideEvents Click="MultiplePR" />
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                                            
                                                    
                                                    
                                                <dx:LayoutGroup Caption="Job Order Detail">
                                                    <items>
                                                        <dx:LayoutItem Caption="" ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxButton ID="btnJOTab" ClientInstanceName="CINJOTab" runat="server" Width="150px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" onload="Generate_Btn" ClientVisible="true" Text="JO Tab" Theme="MetropolisBlue">
                                                                                <ClientSideEvents Click="btnjoclicked" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                        <td> 
                                                                            <dx:ASPxButton ID="btnConsolidated" ClientInstanceName="CINConsolidated" runat="server" Width="150px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" onload="Generate_Btn" ClientVisible="true" Text="Consolidated Tab" Theme="MetropolisBlue">
                                                                                <ClientSideEvents Click="btnconsolidatedclicked" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                             
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                        <dx:LayoutItem ShowCaption="False" >
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <%--<table>
                                                                    <tr>
                                                                        <td>--%>
                                                                            <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" Width="850px" 
                                                                            OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gv1" 
                                                                            OnRowValidating="grid_RowValidating" OnBatchUpdate="gv1_BatchUpdate" OnCustomButtonInitialize="gv1_CustomButtonInitialize" SettingsBehavior-AllowSort="False">
                                                                            
                                                                                <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="OnInitTrans"
                                                                                    BatchEditStartEditing="OnStartEditing"  BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                                                <ClientSideEvents SelectionChanged="OnSelectionChanged"/>
                                                                                <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                                                                <SettingsPager Mode="ShowPager" /> 
                                                                                <SettingsEditing Mode="Batch" />
                                                                                <Settings VerticalScrollableHeight="250" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" ColumnMinWidth="150" /> 
                                                                                <SettingsBehavior AllowSelectSingleRowOnly="true" />
                                                                                <Columns>
                                                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Image" Width="50px">
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="LineNumber" FieldName="LineNumber" Name="LineNumber" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="0px" >
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Job Order" FieldName="JobOrder" Name="JorOrder" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="150" >
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Stock Number" FieldName="StockNumber" Name="StockNumber" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="150" >
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="JO Due Date" FieldName="JODueDate" Name="JODueDate" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Step" FieldName="Step" Name="Step" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Item" FieldName="ItemCode" Name="ItemCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="6" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Color" FieldName="ColorCode" Name="ColorCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="7" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Class" FieldName="ClassCode" Name="ClassCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="8" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Size" FieldName="SizeCode" Name="SizeCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="9" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="RequiredQty" FieldName="RequiredQty" Name="RequiredQty" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="10" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="UnallocatedQty" FieldName="UnallocatedQty" Name="UnallocatedQty" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="11" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NeededQty" FieldName="NeededQty" Name="NeededQty" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="12" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="OnhandQty" FieldName="OnhandQty" Name="OnhandQty" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="13" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="OrderQty" FieldName="OrderQty" Name="OrderQty" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="14" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="AllocatedQty" FieldName="AllocatedQty" Name="AllocatedQty" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="15" Width="150">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ETC" FieldName="ETC" Name="" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="16" Width="0">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    
                                                                                </Columns>
                                                                            </dx:ASPxGridView>
                                                                        <%--</td>
                                                                    </tr>
                                                                </table>--%>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    </items>
                                                </dx:LayoutGroup>
                                                
                                                <dx:LayoutGroup Caption="Item Supplier Information">
                                                    <Items>
                                                        <dx:LayoutItem ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxGridView ID="gvdetailgrid" runat="server" AutoGenerateColumns="False" Width="850px" 
                                                                            OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gvdetailgrid" 
                                                                            OnRowValidating="grid_RowValidating" OnBatchUpdate="gv1_BatchUpdate" OnCustomButtonInitialize="gv1_CustomButtonInitialize" 
                                                                            OnCustomCallback="gvdetailgrid_CustomCallback" SettingsBehavior-AllowSort="False">
                                                                                <ClientSideEvents BatchEditStartEditing="OnStartEditing"  BatchEditEndEditing="OnEndEditing" />
                                                                                <ClientSideEvents EndCallback="EndCallbackParaSaGrid" />
                                                                                <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                                                                <SettingsPager Mode="ShowAllRecords"/> 
                                                                                <SettingsEditing Mode="Batch" />
                                                                                <Settings VerticalScrollableHeight="200" VerticalScrollBarMode="Auto" />
                                                                        <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="Supplier" FieldName="Supplier" Name="Supplier" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="100px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxTextBox ID="txtSupplier" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                     ClientInstanceName="CINSupplier" TextFormatString="{0}" Width="100px" ReadOnly="true">
                                                                                </dx:ASPxTextBox>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="KeySupplier" FieldName="KeySupplier" Name="KeySupplier" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="100px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="MOQ" FieldName="MOQ" Name="MOQ" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="ReorderLevel" FieldName="ReorderLevel" Name="ReorderLevel" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="LeadTime(days)" FieldName="LeadTime" Name="LeadTime" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="OrderingUnit" FieldName="OrderingUnit" Name="OrderingUnit" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="ForOrder" FieldName="ForOrder" Name="ForOrder" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="DeliveryDate" FieldName="DeliveryDate" Name="DeliveryDate" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="8" Width="100px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxTextBox ID="txtDeliveryDate" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                     ClientInstanceName="CINDeliveryDate" TextFormatString="{0}" Width="100px" ReadOnly="true">
                                                                                </dx:ASPxTextBox>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="PendingOrder" FieldName="PendingOrder" Name="PendingOrder" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="7" Width="100px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataButtonEditColumn FieldName="CreatePR" Caption="CreatePR" Name="CreatePR" VisibleIndex="8" Width="100px" UnboundType="Object" ReadOnly="true">
                                                                            <DataItemTemplate >
                                                                                <dx:ASPxButton ID="btnCreatePR"  ClientInstanceName="CINCreatePR" runat="server" Width="100%" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" ClientVisible="true" Text="Create PR" Theme="MetropolisBlue">
                                                                                    <ClientSideEvents Click="CreatePR"/>
                                                                                </dx:ASPxButton>
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataButtonEditColumn>
                                                                            
                                                                        
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
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 3:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 4:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 5:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 6:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField8" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 7:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField4" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 8:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 9:">
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
                                        </Items>
                                    </dx:LayoutGroup>




                                </Items>
                            </dx:TabbedLayoutGroup>
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
                         <td><dx:ASPxButton ID="Ok" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                             <ClientSideEvents Click="function (s, e){ cp.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
                             </dx:ASPxButton>
                         <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" AutoPostBack="false" UseSubmitBehavior="false">
                             <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                             </dx:ASPxButton> 
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Loading..." Modal="true"
            ClientInstanceName="loader" ContainerElementID="gv1">
             <LoadingDivStyle Opacity="0"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
    </form>

    <!--#region Region Datasource-->
    <%--<!--#region Region Header --> --%>
    <asp:ObjectDataSource ID="odsHeader" runat="server" SelectMethod="getdata" TypeName="Entity.AssetDisposal" DataObjectTypeName="Entity.AssetDisposal" InsertMethod="InsertData" UpdateMethod="UpdateData">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="DocNumber" QueryStringField="docnumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDetail" runat="server" SelectMethod="getdetail" TypeName="Entity.AssetDisposal+AssetDisposalDetail" DataObjectTypeName="Entity.AssetDisposal+AssetDisposalDetail" DeleteMethod="DeleteAssetDisposalDetail" InsertMethod="AddAssetDisposalDetail" UpdateMethod="UpdateAssetDisposalDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsJournalEntry" runat="server" SelectMethod="getJournalEntry" TypeName="Entity.AssetDisposal+JournalEntry" >
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsReference" runat="server" SelectMethod="getreftransaction" TypeName="Entity.AssetDisposal+RefTransaction" >
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.MaterialPlanning where DocNumber is null"
         OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsDetail2" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.MaterialPlanning where DocNumber is null"
         OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="MultiplePRsds" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.MaterialPlanning where DocNumber is null"
         OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="Masterfileitem" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="Masterfileitemdetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>


    <%------------SQL DataSource------------%>


    <%--Receiving Warehouse Code Look Up--%>
    <asp:SqlDataSource ID="ReceivingWarehouselookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT WarehouseCode FROM Masterfile.[Warehouse] WHERE ISNULL([IsInactive],0) = 0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>


     <%--Customer Code Look Up--%>
    <asp:SqlDataSource ID="CustomerCodelookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BizPartnerCode,Name FROM Masterfile.[BPCustomerInfo] WHERE ISNULL([IsInactive],0) = 0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>


    <%--Cost Center Look Up--%>
    <asp:SqlDataSource ID="CostCenterlookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT CostCenterCode,Description FROM Accounting.[CostCenter] WHERE ISNULL([IsInactive],0) = 0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>



    <asp:SqlDataSource ID="CustomerLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="JobOrderLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=";"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="PropertyNumberLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT PropertyNumber,ItemCode FROM Accounting.[AssetInv] WHERE Status IN ('O','F')"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="AssetAcquisitionLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT PropertyNumber,ItemCode,ColorCode,ClassCode,SizeCode,Qty,0 AS UnitPrice,0 AS UnitCost,0 AS SoldAmount,0 AS IsVat,0 AS Rate,Status AS PropertyStatus,'' AS VATCode FROM Accounting.[AssetInv]"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="VatCodeLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT Rate,TCode,Description FROM Masterfile.Tax WHERE ISNULL(IsInactive,0) = 0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    
    <!--#endregion-->
</body>
</html>


