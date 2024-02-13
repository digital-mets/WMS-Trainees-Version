﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmItemMasterfile.aspx.cs" Inherits="GWL.frmItemMasterfile" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Masterfile</title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <script src="../js/jquery-1.6.1.min.js" type="text/javascript"></script><%--NEWADD--%>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script><%--NEWADD--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
            height: 525px; /*Change this whenever needed*/
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

        .ImageRadius
        {
                border-radius: 10px;
        }
        
        .dropZoneExternal > div,
            .dropZoneExternal > img
            {
                position: absolute;
            }
            .dropZoneExternal
            {
                position: relative;
                border: 2px dashed #808080!important;
                cursor: pointer;
                border-radius: 10px;
                margin-top: 1px;
                margin-bottom: 1px;
                background-color: #fafafa;
            }
            .dropZoneExternal,
            .dragZoneText
            {
                width: 240px;
                height: 360px;
            }
            .dropZoneText
            {
                width: 240px;
                height: 360px;
                color: #d5d2cc;
                background-color: #FFF;
                border-radius: 10px;
            }
            .dropZoneText,
            .dragZoneText
            {
                display: table-cell;
                vertical-align: middle;
                text-align: center;
                font-size: 20pt;
            }
            .dragZoneText
            {
                color: #d5d2cc;
            }

        .detailpicture > div,
        .detailpicture > img
        {
            position: absolute;
        }
        .detailpicture
        {
            height: 200px;
            width: 300px;
            position: relative;
            border: 2px dashed #808080!important;
            border-radius: 10px;
            cursor: pointer;
        }
        .dragZoneTextImageDetail
        {
            width: 300px;
            height: 200px;
            display: table-cell;
            vertical-align: middle;
            text-align: center;
            font-size: 20pt;
        }

        .BrowseButton
        {
            border-radius: 100px;
            text-align: center;
        }

        .uploadControlDropZone,
            .hidden
            {
                display: none;
            }
    </style>
    <!--#endregion-->
     <!--#region Region Javascript-->
   <script>
       var isValid = true;
       var counterror = 0;
       var itemImagePath = "";

       function getParameterByName(name) {
           name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
           var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
               results = regex.exec(location.search);
           return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
       }
       var param = getParameterByName("parameters");

       function UploadImageComplete(s, e) {
           if (e.isValid)
               CINItemImage.SetImageUrl(e.callbackData);
           var imagebinary = e.callbackData.replace('data:image/jpg;base64,', '');
           //var imagebinary = ibits[0].replace('data:image/jpg;base64,', '');
           //s.batchEditApi.SetCellValue(index, "PictureEmbroider", imagebinary)

           gv1.batchEditApi.SetCellValue(index, "ItemImage", imagebinary);
           cp.HideLoadingPanel();
       }

       function OnValidation(s, e) { //Validation function for header controls (Set this for each header controls)
           if (s.GetText() == "" || e.value == "" || e.value == null) {
               console.log('invalid');
               //counterror++;
               isValid = false
               e.isValid = false
           }
       }

       function OnValidationProd(s, e) { //Validation function for header controls (Set this for each header controls)
           if (txtHavProdSub.GetText() == "True" && (txtprodsubcat.GetText() == null || txtprodsubcat.GetText() == '')) {
               e.isValid = false
               isValid = false;
           }
           //else {
           //    e.isValid = true;
           //    console.log('true');
           //}
       }

       function Validation() {
           if (txtitemcode.GetIsValid() && txtitemcat.GetIsValid() && txtprodcategory.GetIsValid() && txtunitbulk.GetIsValid()
               && txtbaseunit.GetIsValid() && txtprodsubcat.GetIsValid()) {
               return true;
           }
           else {
               return false;
           }
       }

       function checkPercentage() {
           var perc = 0.00;
           var totalperc = 0.00;
           var indicies = gvFabric.batchEditHelper.GetDataRowIndices();
           for (var i = 0; i < indicies.length; i++) {
               if (gvFabric.batchEditHelper.IsNewRow(indicies[i])) {
                   perc = parseFloat(gvFabric.batchEditApi.GetCellValue(indicies[i], "Percentage"));
                   gvFabric.batchEditApi.ValidateRow(indicies[i]);
                   gvFabric.batchEditApi.StartEdit(indicies[i], gvFabric.GetColumnByField("Percentage").index);
                   gvFabric.batchEditApi.EndEdit();
                   totalperc += perc;
               }
               else {
                   var key = gvFabric.GetRowKey(indicies[i]);
                   if (gvFabric.batchEditHelper.IsDeletedRow(key))
                       console.log("deleted row " + indicies[i]);
                   else {
                       perc = parseFloat(gvFabric.batchEditApi.GetCellValue(indicies[i], "Percentage"));
                       gvFabric.batchEditApi.ValidateRow(indicies[i]);
                       gvFabric.batchEditApi.StartEdit(indicies[i], gvFabric.GetColumnByField("Percentage").index);
                       gvFabric.batchEditApi.EndEdit();
                       totalperc += perc;
                       console.log(perc)
                   }
               }
           }
           totalperc = Math.round(totalperc * 100) / 100
           console.log(totalperc);
           if (totalperc != 100 && cboxItemType.GetText() != 'Limited Quantity')
               return false;
           else
               return true;
       }
       var cnterror =0;
       function OnUpdateClick(s, e) { //Add/Edit/Close button function
           var btnmode = btn.GetText(); //gets text of button
           gv1.batchEditApi.EndEdit();
           gv2.batchEditApi.EndEdit();
           gvFabric.batchEditApi.EndEdit();
           if (cnterror > 0) {
               alert('Please check all fields!');
           }
           else {

               if (Validation() || btnmode == "Close") { //check if there's no error then proceed to callback
                   //Sends request to server side
                   if (!checkPercentage() && param == "2") {
                       alert('Total Percentage is not equal to 100%!');
                       return;
                   }

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
       }

       function OnConfirm(s, e) {//function upon saving entry
           if (e.requestTriggerID === "cp" || e.requestTriggerID === undefined)//disables confirmation message upon saving.
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
                   delete (s.cp_close);
                   if (getParameterByName('entry') === 'N' && getParameterByName('transtype') != 'REFASS') {
                       window.open('../ReferenceFile/frmItemMasterfile.aspx?entry=E&transtype=REFSTK&parameters=1' +
                      '&iswithdetail=0&docnumber=' + txtitemcode.GetText(), '_blank');
                       window.close();
                   } else if (getParameterByName('entry') === 'N' && getParameterByName('transtype') === 'REFASS') {
                       window.open('../ReferenceFile/frmItemMasterfile.aspx?entry=E&transtype=REFASS&parameters=' +
                      '&iswithdetail=0&docnumber=' + txtitemcode.GetText(), '_blank');
                       window.close();
                   }
                   else {
                       window.location.reload();
                   }
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
       var index;
       var editorobj;
       var evn;
       function OnStartEditing(s, e) {//On start edit grid function     
           currentColumn = e.focusedColumn;
           var cellInfo = e.rowValues[e.focusedColumn.index];
           itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode"); //needed var for all lookups; this is where the lookups vary for

           s.batchEditApi.SetCellValue(e.visibleIndex, "BaseUnit", txtbaseunit.GetText());
           editorobj = e;
           evn = e;
           index = e.visibleIndex;
           //if (e.visibleIndex < 0) {//new row
           //    var linenumber = s.GetColumnByField("LineNumber");
           //    e.rowValues[linenumber.index].value = linecount++; // or any other default value
           //}

           //if (e.focusedColumn.fieldName === "ItemCode") { //Check the column name
           //    gl.GetInputElement().value = cellInfo.value; //Gets the column value
           //    isSetTextRequired = true;
           //}
           if (e.focusedColumn.fieldName === "ColorCode") {
               isSetTextRequired = true;
               gl2.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "ClassCode") {
               isSetTextRequired = true;
               gl3.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "SizeCode") {
               isSetTextRequired = true;
               gl4.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "Description") {
               fabtype.GetInputElement().value = cellInfo.value;
           }


           if (e.focusedColumn.fieldName === "IsInactive") {
               console.log("test");
               if ((s.batchEditApi.GetCellValue(e.visibleIndex, "OnHand") != 0 && s.batchEditApi.GetCellValue(e.visibleIndex, "OnHand") != null) ||
	           (s.batchEditApi.GetCellValue(e.visibleIndex, "OnOrder") != 0 && s.batchEditApi.GetCellValue(e.visibleIndex, "OnOrder") != null) ||
		   (s.batchEditApi.GetCellValue(e.visibleIndex, "OnAlloc") != 0 && s.batchEditApi.GetCellValue(e.visibleIndex, "OnAlloc") != null) ||
		   (s.batchEditApi.GetCellValue(e.visibleIndex, "OnBulkQty") != 0 && s.batchEditApi.GetCellValue(e.visibleIndex, "OnBulkQty") != null))
                   e.cancel = true;
           }

           //if (e.focusedColumn.fieldName === "BulkUnit") {
           //    glBulkUnit.GetInputElement().value = cellInfo.value;
           //}
           //if (e.focusedColumn.fieldName === "Unit") {
           //    glUnit.GetInputElement().value = cellInfo.value;
           //}
           // PRINT DETAIL
           if (e.focusedColumn.fieldName === "ColorCode"
               || e.focusedColumn.fieldName === "Description"
               || e.focusedColumn.fieldName === "SizeCode"
               || e.focusedColumn.fieldName === "ClassCode"
               || e.focusedColumn.fieldName === "Barcode"
               || e.focusedColumn.fieldName === "OnHand"
               || e.focusedColumn.fieldName === "OnOrder"
               || e.focusedColumn.fieldName === "OnAlloc"
               || e.focusedColumn.fieldName === "OnBulkQty"
               || e.focusedColumn.fieldName === "InTransit"
               || e.focusedColumn.fieldName === "StatusCode"
               || e.focusedColumn.fieldName === "BaseUnit"
               || e.focusedColumn.fieldName === "IsInactive"
               || e.focusedColumn.fieldName === "Field1"
               || e.focusedColumn.fieldName === "Field2"
               || e.focusedColumn.fieldName === "Field3"
               || e.focusedColumn.fieldName === "Field4"
               || e.focusedColumn.fieldName === "Field5"
               || e.focusedColumn.fieldName === "Field6"
               || e.focusedColumn.fieldName === "Field7"
               || e.focusedColumn.fieldName === "Field8"
               || e.focusedColumn.fieldName === "Field9"
               || e.focusedColumn.fieldName === "ItemImage") {

               var imagebinary = gv1.batchEditApi.GetCellValue(e.visibleIndex, "ItemImage")
               CINItemImage.SetImageUrl('data:image/jpg;base64,' + imagebinary);
           }
           // END OF PRINT DETAIL
           if (e.focusedColumn.fieldName === "ItemImage"
                   || e.focusedColumn.fieldName === "UploadPhoto") {
               e.cancel = true;

               //var imagebinary = CINgvOtherPictures.batchEditApi.GetCellValue(e.visibleIndex, "OtherPicture")
               //CINOtherPicturePicture.SetImageUrl('data:image/jpg;base64,' + imagebinary);
           }
       }

       function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
           var cellInfo = e.rowValues[currentColumn.index];
           //if (currentColumn.fieldName === "ItemCode") {
           //    cellInfo.value = gl.GetValue();
           //    cellInfo.text = gl.GetText();
           //}
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
           if (currentColumn.fieldName === "Description") {
               cellInfo.value = fabtype.GetValue();
               cellInfo.text = fabtype.GetText();
           }
           //if (currentColumn.fieldName === "BulkUnit") {
           //    cellInfo.value = glBulkUnit.GetValue();
           //    cellInfo.text = glBulkUnit.GetText();
           //}
           //if (currentColumn.fieldName === "Unit") {
           //    cellInfo.value = glUnit.GetValue();
           //    cellInfo.text = glUnit.GetText();
           //}
       }

       function OnStartEditing2(s, e) {//On start edit grid function     
           currentColumn = e.focusedColumn;
           var cellInfo = e.rowValues[e.focusedColumn.index];
           itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode"); //needed var for all lookups; this is where the lookups vary for

           //s.batchEditApi.SetCellValue(e.visibleIndex, "BaseUnit", txtbaseunit.GetText());
           if (e.focusedColumn.fieldName === "ItemCode") {
               glItem.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "ColorCode") {
               glColor.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "ClassCode") {
               glClass.GetInputElement().value = cellInfo.value;
           }
           if (e.focusedColumn.fieldName === "SizeCode") {
               glSize.GetInputElement().value = cellInfo.value;
           }
           //if (e.focusedColumn.fieldName === "SubstitutedColor") {
           //    glColor2.GetInputElement().value = cellInfo.value;
           //}
           //if (e.focusedColumn.fieldName === "SubstitutedClass") {
           //    glClass2.GetInputElement().value = cellInfo.value;
           //}
           //if (e.focusedColumn.fieldName === "SubstitutedSize") {
           //    glSize2.GetInputElement().value = cellInfo.value;
           //}
           if (e.focusedColumn.fieldName === "Customer") {
               gvCustomer.GetInputElement().value = cellInfo.value;
           }
       }

       function OnEndEditing2(s, e) {//end edit grid function, sets text after select/leaving the current lookup
           var cellInfo = e.rowValues[currentColumn.index];
           //if (currentColumn.fieldName === "ItemCode") {
           //    cellInfo.value = gl.GetValue();
           //    cellInfo.text = gl.GetText();
           //}
           //if (currentColumn.fieldName === "SubstitutedItem") {
           //    cellInfo.value = glItem.GetValue();
           //    cellInfo.text = glItem.GetText();
           //}
           if (currentColumn.fieldName === "ColorCode") {
               cellInfo.value = glColor.GetValue();
               cellInfo.text = glColor.GetText();
           }
           if (currentColumn.fieldName === "ClassCode") {
               cellInfo.value = glClass.GetValue();
               cellInfo.text = glClass.GetText();
           }
           if (currentColumn.fieldName === "SizeCode") {
               cellInfo.value = glSize.GetValue();
               cellInfo.text = glSize.GetText();
           }
           //if (currentColumn.fieldName === "SubstitutedColor") {
           //    cellInfo.value = glColor2.GetValue();
           //    cellInfo.text = glColor2.GetText();
           //}
           //if (currentColumn.fieldName === "SubstitutedClass") {
           //    cellInfo.value = glClass2.GetValue();
           //    cellInfo.text = glClass2.GetText();
           //}
           //if (currentColumn.fieldName === "SubstitutedSize") {
           //    cellInfo.value = glSize2.GetValue();
           //    cellInfo.text = glSize2.GetText();
           //}
           if (currentColumn.fieldName === "Customer") {
               cellInfo.value = gvCustomer.GetValue();
               cellInfo.text = gvCustomer.GetText();
           }
       }

       function OnStartEditingOutlet(s, e) {
           e.cancel = true;
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

       function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
           gv1.batchEditApi.EndEdit();
           gv2.batchEditApi.EndEdit();
       }

       var colorvalid = "";
       var classvalid = "";
       var sizevalid = "";
       //validation
       function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields)
           for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
               var column = s.GetColumn(i);
               
               if (column != s.GetColumn(3) && column != s.GetColumn(6) && column != s.GetColumn(1) && column != s.GetColumn(7) && column != s.GetColumn(5) && column != s.GetColumn(8) && column != s.GetColumn(9) && column != s.GetColumn(10) && column != s.GetColumn(11) && column != s.GetColumn(12) && column != s.GetColumn(13) && column != s.GetColumn(14) && column != s.GetColumn(15) && column != s.GetColumn(16) && column != s.GetColumn(17) && column != s.GetColumn(18) && column != s.GetColumn(19) && column != s.GetColumn(20) && column != s.GetColumn(21) && column != s.GetColumn(22) && column != s.GetColumn(23) && column != s.GetColumn(24) && column != s.GetColumn(13)
                   && column != s.GetColumn(27) && column != s.GetColumn(28) && column.fieldName != 'Field9' && column.fieldName != 'ItemImage') {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
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
               //console.log(e.validationInfo[9], 'jayonhand');
               //console.log(e.validationInfo[10], 'jayonorder');
               //console.log(e.validationInfo[11], 'jayonlloc');
             //  console.log(e.validationInfo[16].value, 'jayisinactive');
             //  var jayisinactive = e.validationInfo[16].value;
             //  if (jayisinactive == true) {
             //      console.log(e.validationInfo[9], 'jayonhand');
             //      console.log(e.validationInfo[10], 'jayonorder');
             //      console.log(e.validationInfo[11], 'jayonlloc');
             //      console.log(e.validationInfo[16], 'jayisinactive');
             //      isValid = false;
             //      e.validationInfo[9].errorText = "cannot set as inactive";
             //     cp_message = "cannot set as inactive!";
                
             //}
           }

         
           if (gv1.GetVisible()) {
               cnterror = 0;
               for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
                   var column = s.GetColumn(i);

                   if (column.fieldName == "ItemCode" || column.fieldName == "ColorCode" || column.fieldName == "ClassCode" || column.fieldName == "SizeCode") {
                       var cellValidationInfo = e.validationInfo[column.index];
                       var colorcodet = e.validationInfo[5];
                       var classcodet = e.validationInfo[7];
                       var sizecodet = e.validationInfo[6];
                     
                       if (!colorcodet) continue;
                       var value = colorcodet.value;
                       colorvalid = value;

                       if (!classcodet) continue;
                       var value = classcodet.value;
                       classvalid = value;

                       if (!sizecodet) continue;
                       var value = sizecodet.value;
                       sizevalid = value;

                       
                           var indicies = gv1.batchEditHelper.GetDataRowIndices();
                           for (var h = 0; h < indicies.length; h++) {
                             
                               var colorcode = gv1.batchEditApi.GetCellValue(indicies[h], "ColorCode");
                               var classcode = gv1.batchEditApi.GetCellValue(indicies[h], "ClassCode");
                               var sizecode = gv1.batchEditApi.GetCellValue(indicies[h], "SizeCode");
                           
                             
                               if ((e.visibleIndex != indicies[h]) && (colorvalid == colorcode && classvalid == classcode && sizevalid == sizecode)) {

                                   colorcodet.isValid = false;
                                   classcodet.isValid = false;
                                   sizecodet.isValid = false;
                               
                                   ValidityState = false;
                                   colorcodet.errorText = "ColorCode,ClassCode,SizeCode must be unique!";
                                   classcodet.errorText = "ColorCode,ClassCode,SizeCode must be unique!";
                                   sizecodet.errorText = "ColorCode,ClassCode,SizeCode must be unique!";

                                   isValid = false;
                                   cnterror++;
                               }
                           }
                       }
                   }
               }
           
       }

       function Grid_BatchEditRowValidating2(s, e) {//Client side validation. Check empty fields. (only visible fields)
           for (var i = 0; i < gv2.GetColumnsCount() ; i++) {
               var column = s.GetColumn(i);

               if (column != s.GetColumn(3) && column != s.GetColumn(6) && column != s.GetColumn(1) && column != s.GetColumn(7) && column != s.GetColumn(5) && column != s.GetColumn(8) && column != s.GetColumn(9) && column != s.GetColumn(10) && column != s.GetColumn(11) && column != s.GetColumn(12) && column != s.GetColumn(13) && column != s.GetColumn(14) && column != s.GetColumn(15) && column != s.GetColumn(16) && column != s.GetColumn(17) && column != s.GetColumn(18) && column != s.GetColumn(19) && column != s.GetColumn(20) && column != s.GetColumn(21) && column != s.GetColumn(22) && column != s.GetColumn(23) && column != s.GetColumn(24) && column != s.GetColumn(13)
                   && column != s.GetColumn(27) && column != s.GetColumn(28) && column.fieldName != 'Field9' && column.fieldName != 'ItemImage') {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
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
               //console.log(e.validationInfo[9], 'jayonhand');
               //console.log(e.validationInfo[10], 'jayonorder');
               //console.log(e.validationInfo[11], 'jayonlloc');
               //  console.log(e.validationInfo[16].value, 'jayisinactive');
               //  var jayisinactive = e.validationInfo[16].value;
               //  if (jayisinactive == true) {
               //      console.log(e.validationInfo[9], 'jayonhand');
               //      console.log(e.validationInfo[10], 'jayonorder');
               //      console.log(e.validationInfo[11], 'jayonlloc');
               //      console.log(e.validationInfo[16], 'jayisinactive');
               //      isValid = false;
               //      e.validationInfo[9].errorText = "cannot set as inactive";
               //     cp_message = "cannot set as inactive!";

               //}
           }


           if (gv2.GetVisible()) {
               cnterror = 0;
               for (var i = 0; i < gv2.GetColumnsCount() ; i++) {
                   var column = s.GetColumn(i);

                   if (column.fieldName == "ItemCode" || column.fieldName == "ColorCode" || column.fieldName == "ClassCode" || column.fieldName == "SizeCode") {
                       var cellValidationInfo = e.validationInfo[column.index];
                       var colorcodet = e.validationInfo[5];
                       var classcodet = e.validationInfo[7];
                       var sizecodet = e.validationInfo[6];

                       if (!colorcodet) continue;
                       var value = colorcodet.value;
                       colorvalid = value;

                       if (!classcodet) continue;
                       var value = classcodet.value;
                       classvalid = value;

                       if (!sizecodet) continue;
                       var value = sizecodet.value;
                       sizevalid = value;


                       var indicies = gv2.batchEditHelper.GetDataRowIndices();
                       for (var h = 0; h < indicies.length; h++) {

                           var colorcode = gv2.batchEditApi.GetCellValue(indicies[h], "ColorCode");
                           var classcode = gv2.batchEditApi.GetCellValue(indicies[h], "ClassCode");
                           var sizecode = gv2.batchEditApi.GetCellValue(indicies[h], "SizeCode");


                           if ((e.visibleIndex != indicies[h]) && (colorvalid == colorcode && classvalid == classcode && sizevalid == sizecode)) {

                               colorcodet.isValid = false;
                               classcodet.isValid = false;
                               sizecodet.isValid = false;

                               ValidityState = false;
                               colorcodet.errorText = "ColorCode,ClassCode,SizeCode must be unique!";
                               classcodet.errorText = "ColorCode,ClassCode,SizeCode must be unique!";
                               sizecodet.errorText = "ColorCode,ClassCode,SizeCode must be unique!";

                               isValid = false;
                               cnterror++;
                           }
                       }
                   }
               }
           }

       }



       var clonenumber = 0;
       var cloneindex;
       function OnCustomClick(s, e) {
           if (e.buttonID == "Details") {
               var itemcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode");
               var colorcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ColorCode");
               var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
               var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
               factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode
               + '&colorcode=' + colorcode + '&classcode=' + classcode + '&sizecode=' + sizecode);
           }
           if (e.buttonID == "CloneButton") {
               if (!CINClone.GetText()) {
                   alert('Please input a number to Clone textbox!');
                   return;
               }



               cloneloading.Show();
               setTimeout(function () {
                   clonenumber = CINClone.GetText();
                   for (i = 1; i <= clonenumber; i++) {
                       cloneindex = e.visibleIndex;
                       copyFlag = true;
                       gv1.AddNewRow();
                       //  CINTotalLines.SetText(gv1.GetVisibleRowsOnPage()); // Set Total Lines / Rows Of Grid.
                       precopy(gv1, evn);
                       // getgv(s, e)
                   }
               }, 1000);
               //Validate();
           }
           if (e.buttonID == "Delete") {


               if (s.batchEditApi.GetCellValue(e.visibleIndex, "AddedDetail") != "" && s.batchEditApi.GetCellValue(e.visibleIndex, "AddedDetail") != null) {
                   e.cancel = true;
               }

               else {
                   gv1.DeleteRow(e.visibleIndex);
               }
           }

       }

       function precopy(ss, ee) {
           if (copyFlag) {
               copyFlag = false;
               console.log(index);
               
               for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
                   var column = gv1.GetColumn(i);
                   if (column.visible == false || column.fieldName == undefined)
                       continue;
                   ProcessCells(0, ee, column, gv1);
               }
           }
           //Validate();
       }


       var x;
       function ProcessCells(selectedIndex, e, column, s) {//Clone function :D
           if (selectedIndex == 0) {
               //if (column.fieldName == "IsInactive") {
               //    //if (s.batchEditApi.GetCellValue(e.visibleIndex, "QtyCount") === null || s.batchEditApi.GetCellValue(e.visibleIndex, "QtyCount") === "" || s.batchEditApi.GetCellValue(e.visibleIndex, "QtyCount") === 0) {
               //    s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, "1");
               //    //} 
               //}
               //else
               
               if (column.fieldName == "OnHand" || column.fieldName == "OnOrder" || column.fieldName == "OnAlloc" || column.fieldName == "InTransit") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, 0);
               }
               else if (column.fieldName == "IsInactive") {
                   s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, false);
               }
               else if (column.fieldName == "UploadPhoto") {
                   var zx;
               }
               else
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, s.batchEditApi.GetCellValue(cloneindex, column.fieldName));
           }

           cloneloading.Hide();
       }

       function OnInitTrans(s, e) {
           AdjustSize();
           isValid = true;
       }

       function OnControlsInitialized(s, e) {
           ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
               AdjustSize();
           });
       }

       function AdjustSize() {
           var width = Math.max(0, document.documentElement.clientWidth);
           gv1.SetWidth(width - 120);
           gv2.SetWidth(width - 120);
           gv3.SetWidth(width - 120);
           gv4.SetWidth(width - 120);
       }

       function UploadImageClick(s, e) {
           $('#gvuploadimage_TextBox0_Input').click();
       }

       window.onload = function gvExtract_end(s, e) {
           if (getParameterByName('conf') != null)
               gvExtract.GetSelectedFieldValues('ColorCode;SizeCode;ItemImage;', OnGetSelectedFieldValues);
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
           //loader.Hide();
       }

       function getCol(ss, ee, item) {
           for (var i = 0; i < ss.GetColumnsCount() ; i++) {
               var column = ss.GetColumn(i);
               if (column.visible == false || column.fieldName == undefined)
                   continue;
               Bindgrid(item, ee, column, ss);
             
           }
          // itemImagePath = "/9j/4AAQSkZJRgABAgEBLAEsAAD/7gAOQWRvYmUAZAAAAAAB/+EASkV4aWYAAE1NACoAAAAIAAMBGgAFAAAAAQAAADIBGwAFAAAAAQAAADoBKAADAAAAAQACAAAAAAAAASwAAAABAAABLAAAAAEAAP/tACxQaG90b3Nob3AgMy4wADhCSU0D7QAAAAAAEAEsAAAAAQABASwAAAABAAH/4aSdaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/Pg0KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4wLWMwNjAgNjEuMTM0Nzc3LCAyMDEwLzAyLzEyLTE3OjMyOjAwICAgICAgICAiPg0KCTxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+DQoJCTxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyI+DQoJCQk8ZGM6Zm9ybWF0PmltYWdlL2pwZWc8L2RjOmZvcm1hdD4NCgkJCTxkYzp0aXRsZT4NCgkJCQk8cmRmOkFsdD4NCgkJCQkJPHJkZjpsaSB4bWw6bGFuZz0ieC1kZWZhdWx0Ij4yMzggQkxBQ0sgT1JPPC9yZGY6bGk+DQoJCQkJPC9yZGY6QWx0Pg0KCQkJPC9kYzp0aXRsZT4NCgkJPC9yZGY6RGVzY3JpcHRpb24+DQoJCTxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wR0ltZz0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL2cvaW1nLyI+DQoJCQk8eG1wOkNyZWF0b3JUb29sPkFkb2JlIElsbHVzdHJhdG9yIENTNTwveG1wOkNyZWF0b3JUb29sPg0KCQkJPHhtcDpDcmVhdGVEYXRlPjIwMTQtMDgtMTRUMTY6MTI6MDkrMDg6MDA8L3htcDpDcmVhdGVEYXRlPg0KCQkJPHhtcDpNb2RpZnlEYXRlPjIwMTQtMDgtMTRUMDg6MTI6MTdaPC94bXA6TW9kaWZ5RGF0ZT4NCgkJCTx4bXA6TWV0YWRhdGFEYXRlPjIwMTQtMDgtMTRUMTY6MTI6MDkrMDg6MDA8L3htcDpNZXRhZGF0YURhdGU+DQoJCQk8eG1wOlRodW1ibmFpbHM+DQoJCQkJPHJkZjpBbHQ+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHhtcEdJbWc6d2lkdGg+MjU2PC94bXBHSW1nOndpZHRoPg0KCQkJCQkJPHhtcEdJbWc6aGVpZ2h0PjI0MDwveG1wR0ltZzpoZWlnaHQ+DQoJCQkJCQk8eG1wR0ltZzpmb3JtYXQ+SlBFRzwveG1wR0ltZzpmb3JtYXQ+DQoJCQkJCQk8eG1wR0ltZzppbWFnZT4vOWovNEFBUVNrWkpSZ0FCQWdFQXRBQzBBQUQvN1FBc1VHaHZkRzl6YUc5d0lETXVNQUE0UWtsTkErMEFBQUFBQUJBQXRBQUFBQUVBDQpBUUMwQUFBQUFRQUIvKzRBRGtGa2IySmxBR1RBQUFBQUFmL2JBSVFBQmdRRUJBVUVCZ1VGQmdrR0JRWUpDd2dHQmdnTERBb0tDd29LDQpEQkFNREF3TURBd1FEQTRQRUE4T0RCTVRGQlFURXh3Ykd4c2NIeDhmSHg4Zkh4OGZId0VIQndjTkRBMFlFQkFZR2hVUkZSb2ZIeDhmDQpIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGYvOEFBRVFnQThBRUFBd0VSDQpBQUlSQVFNUkFmL0VBYUlBQUFBSEFRRUJBUUVBQUFBQUFBQUFBQVFGQXdJR0FRQUhDQWtLQ3dFQUFnSURBUUVCQVFFQUFBQUFBQUFBDQpBUUFDQXdRRkJnY0lDUW9MRUFBQ0FRTURBZ1FDQmdjREJBSUdBbk1CQWdNUkJBQUZJUkl4UVZFR0UyRWljWUVVTXBHaEJ4V3hRaVBCDQpVdEhoTXhaaThDUnlndkVsUXpSVGtxS3lZM1BDTlVRbms2T3pOaGRVWkhURDB1SUlKb01KQ2hnWmhKUkZScVMwVnROVktCcnk0L1BFDQoxT1QwWlhXRmxhVzF4ZFhsOVdaMmhwYW10c2JXNXZZM1IxZG5kNGVYcDdmSDErZjNPRWhZYUhpSW1LaTR5TmpvK0NrNVNWbHBlWW1aDQpxYm5KMmVuNUtqcEtXbXA2aXBxcXVzcmE2dm9SQUFJQ0FRSURCUVVFQlFZRUNBTURiUUVBQWhFREJDRVNNVUVGVVJOaElnWnhnWkV5DQpvYkh3Rk1IUjRTTkNGVkppY3ZFekpEUkRnaGFTVXlXaVk3TENCM1BTTmVKRWd4ZFVrd2dKQ2hnWkpqWkZHaWRrZEZVMzhxT3p3eWdwDQowK1B6aEpTa3RNVFU1UFJsZFlXVnBiWEYxZVgxUmxabWRvYVdwcmJHMXViMlIxZG5kNGVYcDdmSDErZjNPRWhZYUhpSW1LaTR5TmpvDQorRGxKV1dsNWlabXB1Y25aNmZrcU9rcGFhbnFLbXFxNnl0cnErdi9hQUF3REFRQUNFUU1SQUQ4QTlVNHE3RlhZcTdGWFlxN0ZYWXE3DQpGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYDQpZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZDQpxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxDQprWG5HTHpkTnBKaThyejIxcGZrZ3RkWGJsVlJWM05GOUc0VTE5eHRpckR2eTlsL04rWFQ0cnE3azB5NjBtY1hFMEV0M05kUGV6RmlmDQpRY09zTVVhUXZzNC9kMTRIWlZ4VlpwTjUrY2tIbTZIUzdtKzB6VVluSnVkVWdLdVVzN1NpQ0pZNTRvTFg5OU5JSktCdzN3MDIySktxDQpad2FmK2NwdmhmVFhPaUNSbFpHdGc5ODl1Z0pIRUlnRVFPM3hGbkJibHNDcW5pcXFKOC9UL21ERHBiWE9qM2VuNlZiMnNja3QzZlN6DQpNekFMSU9OSW1zN2tFZWtEVlJ2eklvU0YrTlZCNkRmL0FKejNYbFRUcnFTMjBWdFdsSk53dDQ5MUJXRGpTTmlzY1QwbGNqbXc0cUFEDQpUaURpcWFlV3JEOHl6cWEzbm1qVXJCYlZJMlQ5RzZaR3hoa2RxY1pDODZDWk9JL1o1TURpckxjVmRpcnNWZGlyRWZPbW8vbWRaM2tCDQo4cGFSWWFwWkNKMnVVdXB6Qk1aUjlsWTJxRjM5eDlJeFZqczNtbjg0alBhblVmTDF2b21uUlROTGZYYVhjTTQrclFjR2N5dXdaSVkyDQpYbVNRR2Vtd0FQeFlWVFNUemorWTk1YUdiU3ZKbjdtZUF6V2QxUGZ3SVR5bDR4aG9HQ09yTkQrOElZaWgrRTc3NEZVckx6SitZMGNPDQpuV04zcENKcTExSGQzTTBNMDBFOGlKQlQwb2liZHJlSXRJZHVleUxWYWt0MUtxVmw1cy9ORzM4eXJvK3FhTFpYM3IyVTkvSDlSZjBEDQpFc1lLeFJUTkxOTU9jc2xGMkhFYjhXZWpFQlVUZmViZnpYVVhTV2ZrSlhhTlhOcksrcTJwV1FpVUlvS1VRamtoTDdzS2RLMXhWZmMrDQphdnpTL1NOdEZiK1J4OVJNNWp1cmg5U3RTd2hyUVNJZ0s3SHIzUDhBazRxeW5RYjNXTDNUbG4xZlRScE40V1lHekU2WE5GVTBWdlVRDQpLdnhEZW5iRlV4eFZENmpQZFFXTTgxcGJHOHVvMExRMnFzc1prY0RaZWJrS3RmRTRxODd0dk5QNXYyMnJDTFZQTGtMeFhVaml5V0M0DQp0a1F4UXh5eWVqeGFacFByTW9qRkdMK21BZWc0bXBWR2FONXovTW0vY3h5K1VvWTJzMjRYN3Bmd3ZHN3R3L2R3TU9qUjgyYVRsWDdODQpCdTN3cXEwSG1yODBwMXRnM2taTFJwSnlMZ3lhcGJTcWtDRVZiNEFDWGNmWkFxQisxZ1ZiSjVyL0FEV0xlcEY1RlFJQXRJSk5UdGViDQpNM0xrVEl0VlFMUmYyV3JYMnhWWGJ6VjUybHZ6YVFhREhIY1EyTVZ6YzJobmlsY1R6T1U5SVNoNGtBakh4a3NQajRzcS93QXdLcGJwDQpYbUg4NWhiM2k2ajVWamt2bVRuYlNyZDJzVm9yVm9zYXhpV1daandvekYzQUxWQTRpaHdLbUVmblh6bmNTZWxwM2xaZFIrcnl3MnVvDQozRWQvRkRGSE9RVGRLbnFwV1FXL3dnOGExYXEvc2s0cWw5djV4L01iVElyL0FGRHpGNWJrU0l0RkJwMWpCZFdjcGx1Sm1LeHBHVkt1DQpxY3VLTVc1c1NlUVVLQ01Lb21Qek4rYWozVDNKOG5NbHZIQXFwWW5VTE5TODVLK28zcWZHZUkrTDA2OGFqN1Nna1VDcnAvTm41cXRaDQp4RzM4aUxIZFN6dERJSmRVdFhXR01CYVhKQzhmVVg0ajhBWlcyN2JZcXp6RlhZcTdGWG4rcitmdFpYV05YMDdTSW83bWZUMkVkdmFyDQpibWFXVi9STWpWUDFtQTBWbElQQkdvS2R5QWNpT0lVQ2VxTFF2K052UDBXcTNWdGRXZHF0amJ5cEcxNGx1V0tCd3dEenFid0pHdk5DDQpLK29SVGV2YkNNVWFCNy94M0xiTVBLV3VUYXhwa3M4NWhhNHQ3aWEybGEzTll5WW1vQ1BpZWxWSS9hUGpsTTQwVXNPMUR6UjV4R29YDQpaUzVGcFpMcUQyOXNYZ1pZemF4MFV5aVJvWFZoekJGUTVQZ3ZUTFk0NGtmMklRMXA1dDgyWExRQ0xWb1p1VnpjUXpDS0wxR1ZJbmtTDQpOdUtXek44WlZlZ1BYY0wya2NVUi9hRVd5elJQTlU4WGxXenY5ZWh1amZrbUs4aXRiSzZ1SkZsVm1IeFEyMFVycnNLbjRhZmhtTkt1DQpJZ01xMlpIYVhVTjNicGNROHZTa0ZVTG84Wkk4ZUxoVy9EQXFyaXFDdHJyVVo3ZUtZVzhLaVZGY0w2emJjaFduOTFpcUUxWHpGQnBQDQpvL3BLVzB0RGNFaUFTWERBdVZwWGlQUzNwVVlxZ3B2TnVpM01Ed3pTV1UwRWdJa2plUjNSbDdnZ3drRVpMZ1BjcW9mT2VtRHJjV3YvDQpBQ05rL3dDcU9QQWU1WER6bnBoNlhGci9BTWpaUCtxT1BBZTVWTlBOZWlMTzl3ajJRdUpWVkpKaEk0ZGxRbmdyTjZOU0ZMdFR3cWNlDQpBOXlxbitNOU02L1dMWC9rYko4Lzk4NDhCN2xWckx6VFpYZDdCYXd5UVNQT3pLUFRrZG1IR05uclJvMEg3UGppWWtLbmVSVjJLdXhWDQpMSjRUcXRpcVhlbjJ0MWFUaFpQUXVHOVJUMFplU05FeTFHS3ExdkZkVzBDUVc5bmJRd1JqakhGSElVUlI0S29pQUdLcWQxcXpXa3NFDQpWMDFwQkxkTnd0bzVia28wakQ5bEEwWTVIZm9NYlZFZXBxWCsrSWYrUnpmOVVzVmQ2bXBmNzRoLzVITi8xU3hWM3FhbC92aUgva2MzDQovVkxGVmtFZDNCSDZjRnBieFJnc3dSSkdWYXNTekdnaTdzU1RpcnBZN3VVeG1XMHQ1REUzcVJGcEdKVjZGZVMxaTJOR0lxTVZWclNkDQo1NGVib0VjTTZNb1BJVlJ5bXhJWCtYd3hWV3hWNHpkYUo1L3RyaC8wZHBIbW1WUkpHQ0pQTWRsNlRDMzV1cnFabW5rVkpYNHJJS0FsDQpPMVJ4SlY2UjVkc3RZamxhUyt2dFRrU0pUQ3R2cUEwMHBJVzR5ZXNyV1VTeVZTcGlISmw2RWxUc3hDcDlpcVIrVTdhMVMydnA0MFVYDQpFK29YeHVIQUhJbGJ1UkY1SHJzaXJsbVE4dmNxajVWaGdhKzh3WFFETkpKcUR3TVdKSTRRb3RGVUhzSGtmNzhPUTdSSGtnS25sMUlvDQpOWTh3V3NFYVJRSmRReUtpS0ZBYVMwaTViRGJjclg1NEptd1BkK2txd3E5OHFhOWRXckhUOU50THVEVkxHMllYY3lRR1NLVTJpMnJBDQp2SjhhcWlLc3FsQXg1TFRvVGwrTEpBQVgwL0g3UGNnMmszbUhSTkxnMUMzMUZZckpCRmRYRnBaUTNWMnRuRzl6QmZYUENDU041WUZsDQpWa2xSZ0tQOWdEanhZMVlaWUhZa2ord2ZxKzFKRDBMeTk1ZTBlNTBaOU0xQzJ0ZFV0YlZyZU5CTkhIUEN4WFRvSVM2aGd5L0VqTU51DQp4STZITWFaczJsazF2Ylc5dGJ4MjF0RWtGdkNvamhoalVJaUlvb3FxcTBBQUhRRElxcVlxZzlPbWhUVHJRTzZxVERHQUNRRDlnWkdVDQo0Zzdsa0lrOGdrR3Q2em9NZm1SYmZWcFlGZ2pzMU1IcnhtU05ubmxJa0ZhY2VRRUtkKytJSko5Skg0cnpUVkRjS0ZucFBraWFTUzRoDQpPblBia0s4WDFlVXh5cFRkdVRpVHg5bHAwT1dYazd2eDhtTlI3MHd0dkwrbjNMU0czMUcrU0tOdUt4UlhzaFFBME5CeFp1bTRHUmpJDQo3Mk92NkFtUUFwaVBsM1RibTkwNmFlOXVOVzFkeE5iTkhicGRicDY2L0d3RXNrYThVNUh2V25TdVdaTnFvZHpFZWFFL01qVXRJOGkyDQpzZHpPYjI2Z25DUlEyNnl4cTNxdVhKTFROSEl5cUVSdWdKclFiRGNWNU1uREc2czJtTWJLY2FWYjJXcCtSWnRlMDZYVjlORTFtMTNEDQpGTmNTeG5rWUJLckFCdmlVZlo1ZnRVMjJvY2tKV0FhRy92UVFpdkoxb0ZGbExkYWxkM2wzSGRYRWFMZFhMeVZVUnlEbHdZMHF0ZU5hDQpaUElOelEyUUdjNVVsMkt1eFZMRzFDSFRmTFIxR2Y4QXViS3krc1MwL2xpaTV0K0F4VjVoNUV0TkU5T1pmTVV0emNYU3d3OG5NbDQ5DQpaaEpPa3BwR2RxK212VVpjYm9JUnZtRzE4b3JxV2xmVURjSkNKVmE2SE84V3FmV0xkRHM1QlB3U09LTHZ2Z0ViQnV1WGtrR3VTWjJFDQpIaytUMWYwalp2YmxTQkdZWjlSbTVpbnhFajA0Z3UvUUN1VjR3ZUVjdnNUUG1VQjVOdGZKMG5scXprMVUzRXQrM3Flcy9xWHJrL3ZYDQo0N3hzVit5QjB5Y3Blb2dJNGRyZDlXOG1uenA2VmJuOUZmbzdsNmZPOXA5WTlmN1hHdlA3SGZwa3QrSHA5aUZQemphK1VrMDZFNlNiDQptT2YxSGFRK3BlcjhDVzBzbldSZ1B0S3Bwakc5N3JsNUt6WHlSZSt0NWN0SW5sYVo3ZHA3UlptNUZwRXRKM3Qxa1lrVnF5eGdtdTllDQp1K1k1bUxycXo0VFZwdFlPZ2djRmdDSnB5Ulh0NnovMHlURkVvNnVvWkNHVTdoZ2FnNHE4WmpsMGxiYVcwdS9NWG1hMkZtRGZTV2R4DQpydWlyUFBBaERzdnJSWFJtU0lzbEMzcXgwNUVjZ3RLRldlZVZMMjN1TCtTV3kwM1ZCWnlxNFhVN25Vb2I2ellDUXlEMDBXL3UyVXR6DQorRWlJZkRSZGdBTUNvcS8wN3pYTjVnU2EyMUI0TkdQcHJMYnJMQVBoQUxPeXh0WXlTY2l3VmY4QWVtbkV0VGl3R0pDdCtVR29tdEtUDQo4TWVxM1FXdllFcTM2MnljaHNQZCtrcmJYbHNlbExBU0NHMUN3aHVuYithWU96VHNmY200WERQN2loVThzRXRkYTlJMjduVTNVc2V2DQpGSVlnb3I0QWRNY25UM0tGYXh1L3FQbHBwZVBxZm95S2FLbGVQcWZVaTBWZWg0OC9TcjNwWHZsYVVEYitYTE9SNXRJdVpaM2hpdDdkDQp5OEZ4Y1dwWXlRdGFPRzlDU09vNFd3SXFUUW5iQ3FhYVVvVzYxUlJXaTNTQVZKSjJ0SU9wTzV3S21PS3V4VkxyTnJvVytsckhHalc1DQppSHJ5TTVWMUlpK0RpZ1JnMWQ2MVlVOThqTG1QZitnc284aTg3MSsxMS9WUE9GeGNhSGRSVzl6YXpMOVhlY3VZaUdVMjhpU0JEVUQxDQo3VWNXNHR4WWcwSU5Ea1l4NlQrUGY5akJOeDVHMVMva2oxZnpocWNCbnNPVTBFZHVxQzNRRGx5TjA3cEd0d3ZwOGZ0UnJ4SUo4S1ZTDQpqRVNKamZ4WldhcFI4cWVVTkkxTFdMdlhiaUwxYmVSVERhMi9wckRCNmRWOU9VUXFxOEpHQ0Y2MSt5NHlaQmlFSWp5bDVNRGFWY2pWDQpMRjdUVUZkWTRPY3JENFV0NGxVbjZ0TlIwRWl0VDR2dXd6bjNGQ0pzL0p1bS9wQmJQVVY5ZVNYVHl0NTZNdHpIREp5azRzQkcwMGg0DQpFYmNTeEdSQlBDazgxVnZLK2tYRjFyVURXN1hEckdnaEUwc3NwRFBFZThqc2R6a3BFOElRa1BsanlkZlczbVhTL01GN0ZOWnpzODBEDQpXc2pSRlNwdGZoZGxReUVPeEQvdGRCdU1PUWczUlVJblR2eXc4dzI4enlYdm02OTFKV21hUlZsbDFHSGhHeXNQVFg2dGZ3QS9GeE5YDQpEZE5nSzF4eTVCS3FIRFNnTXkwYXkxYXpoRUY5ZHczVVVjY2NjQmppbVNRRkJSMmtrbnVMcDVPVzFLbXZpV3lsS1k0cXhielQ2citSDQprdElXVVNhaXRucDlINkZMeVdLM2tIUmovZHlOMEczWENGVWRKMG5YdEwxSFVMMkcwRE5xVWdsdVFaQklGb3pOU0pXa1dnUHFNYVY2DQo1YVpSSUEzMlExclVWenFkeFpUM2VsWFMzR215Q2FBUXlsVlorU3RSK0N5S1Y1SXJVNUhCUW9qaXEvSklLWXI1cXRiZU5JcmkwdW8zDQpXaTBhTWlvOFFYOU1rYmRlT1JoaG9VQ21Vck5zWThtYXRaMnVpYWZiV3MxK2tVUklFQWpobGFSV21sa29Td1o5K1lCcDRVSFd1R1lCDQp5RVhHNlRSNFFVNTlhZHZNQTF5UFNycHJsclQ2bDhUU0pINmZxZXJ2RzBRUFBsdFhKVUtxL3NZSWZ6VkJxT3J3V2RuZlc2V2dsblJyDQpTRnA0NDVYbVZINVJBcktmVUhwRnFnRG9EMndDY1k5L2Q4MGdXaWZLY04rdXE2eEZPNndQQmZHYVcxaFA3dGhkV3NEOHZqVjJvWmZVDQpiNFdIeFZHNHltVjhXM0w3V1Fxdk5qWG5ieWQ1aHVvdFgxVFMzVzZhK1NTempzWTlQc2JpZFEwa3FTTVpMdVNFT25GdnNGNmV4eVRGDQpQUHk3c2ZPbHBwOXBiYWo2RnBvOW5FYmEzc0piUkliNmtYd3h5TTl0ZFRXcWdnZlpTTWZSZ1Y1bm8vNTgrYkxjUEJjV0IxR3BZeFhNDQo0azlTa2NZV3JMYldjRWFCbjRscTEzYzBOQUZ5eWVNeDUvZmFBYmVxK1IvTVBuTFZsTW5tTFNMYlRJcFloTFpOYjNEVG1RQWdQeURJDQpoU25JVThjclM2Nzh2ZWZuMU9XN3RQTmkyOXI5WWtsZzArVFQ0Wm92UWFJS2tNakJvcGp3a0hQa3JnbXBCN1VTb1dXWWVMU2ZOcFZ5DQpESGMzYmJBVUxmVlkySm9RZXBPVDRUY2R6OW5lZkpOanVUMHhKRnJWaEVnb2tkbmNxbzY3TEpiZ1lPaUVGNVRZTkpycmpvZFZuQS8yDQpLUm9meFhKWk9udUNBNlgvQUpSUFYvOEF0NS84bnBzZ2xIeC9Ecjl4WGYxTFNDbE42ZW5KTlhsVDdOZlVIR3ZXaHAwT0JYYUgvdkhLDQplNXU3dXArVjFJUDFERlV3eFYyS29MVDRpK24yWjVzdkdLTWdDblhnQjRlK1FsQ3p6TEtNcTZQT3ZLbWs2eHF1cGFycWR0Zm1PeXZGDQpSamJLWTFjU3pmdlpENmp4WE93azVBY1ZXbEtqTWlQcEFzc1NXUlcyaEpyTnhjMitwWDk1ZHgyRXF3eVc5eEc4Y1JkUXNpc3JlbGJRDQpULzYzcHNCNDFvUUJPanNGSVpaYjI4TnZDc01LOEkwK3l2WHJ1U1NkeVNkeVQxeUJOcXFZRlNQVXRNMDdVOVplejFDM2p1cldXeUlrDQpnbEhKV0Jrb2FxZGoxeDRiRnBFa05adHFVRjFxNDB6VDRtZU13eDJsdkxNWUltUkZaQTNOSTVpcThnYWdJYVVPeE8yU2xHZ0tVbTBmDQpydDNkMnVtcGRwR2h1NEZsbFdHcFpESWx0S3dYa2VCSTVDbGR2b3lLRWpsODErWmJhYTF0YjNUVEJlM2pNbHRBUHFQNzEwVXN3aTU2DQpqR3owVVZQdzRxdTBYemxxR28zK2pRQ09OazFHR1NlOFgwMlJyWlFKQkVHWVNTcXpTdkMvSDJSajJ4U3pERkRHTDRwSmNlVUxRL0VUDQpPYmgwN0ZJTEdiNGo0OFpaSXlQZmZ0aXFHdkl0WHV2UE4xcHlheE5iV3hzSWJ1M3Rvd3RGcEswVWxEc1RXZytXV3hBNGJJUWp4NVoxDQppbSt2M1JPMjlCNC9QQnhSN2xTd2ZwYlR2TmNOcCttSjd4SU5PdWI2NnRKVlhpOUdTT0hmYy9hNW5ZakpjSUl1cTNwVXJrdE5WTno1DQpVdHhxWDFXMm1rdkk0MmppVUZHRVA3c2NuTE56SkVueEx4MmJqVHVZUXhEaUpObWgrbG5LZDlLU0FlWHYrY29kNjZ2NWE5djN1cGY5DQpVOEhGNU1FL2dzUFAxalllWGsxL1c0MzF5NzFHT0srdHJBdTFxOEt2Skt3ak13RW45d2c1R2dvMWFkc0pnSnhPMzR0bEdWRzJSNlZBDQpiSHpwcVVFdHhMY3ZlMkZ2UEZKTjZmS2tWeGNoMC9kckdLUnJQR3ExRmFkU1R2bFlqU2sybGR4NU04eDNubUNiV3JQekxQYVdwRThVDQplanNzN1dvazlSd0pXRU56Ymx0OTZDbnp5N3hCd2NOYjk3R3QwKzBiUTlUMHIwb1lyOVpyTGt6WENYSDF5Nm5ZbGVLK25QZFhjN0lCDQpRRWlqRHIwSnJsU1hpdHQ1Ui9NU0piZVMyc0JBQUZNaG10NUxoMkhFZ3g4UzhTS0hyUnVTdCtCck9SSFJoSGk2dlkvSzE1ZDNTUUpjDQphVkxwVFdjVWtKaWRBc1JyNlRLWVNHWWxlcS9GUTFCMnBRbUxOa1dCV0pRZjhjbnpqL3pFM2Y4QTFDUlpiMWorT3BRbjAvOEF4MzdQDQovbUZ1ditUbHZrQnlTbHZrN3ByZi9iV3Uvd0JhNUxKeitBKzVBWFMvOG9ucS93RDI4LzhBazlOa0VwaEIvd0FkKzgvNWhiWC9BSk9YDQpHQlhhSC92RkovekZYbi9VVkxpcVB4VjJLcFBlVHlXL2xDZWVJMGxpMDluUStETENTT252aXFYZmw1REFOSm5uZ1hoSE5PL3BvS1VFDQpaWXl4N0R2eGxwOUF5M0pzQVBKQVpUbFNYWXEwNExLUUdLa2dnTUtWSHVLZ2pGV0ZmVU5kMC96QnAwUXZyalZic1F2Nmw1ZHRIR3JCDQp1UnBKSGJKYlI4UlQ0ZmdZajN5M0NCd3l2eVRJc3EwclQyc3JZcEpJWlo1R01rMGg3c3hxYWUxU1QvQWRNak9WbENDODJLN2FQTXNiDQpjSE1kd0VlbGFINnJOUTA3MHlDdlB2UEZwcTFwcjJsMytxUzJPbzN1azI5MWZXdW9McDRFMEN4UmxuNHJMZVJSTlFKeStKdGp1TWxXDQoxcEFUdjh1L0wxM3BXaGFQYzZpQ05WMU81TnpkS3k4V2pRMnN3Z2c0NzhQVGpweVdwbzVmeHlLcy93QVVNY3QvK1VpOHYvOEFiSHZmDQorVHRoaXFYZWIzZXc4MTZQcXR1Q1p4YjNTU29Pc3NVWEJ6Q3ZpWDVuaVA1Z015TU80TVQxUVVaNjJvS3A4MUV5QzM0MU5nU3hINk9wDQp5NStrQi9mL0FPN09sYWZCOG1oOUgyK2Y2dW4ycWxta0EzZXMrWk5TdUZQMXY2aEVoajJiMDFsRXNnaElvUGlTTVJoaC9OWERQYmhBDQpWdnpIWnRMNU8wRzRqbDlENnZkV2QyOXdhbjBqS0NPZnVGZVlIZnRneG1wbjRxbU1UNnZyNkxmdzg3RjlQNUMyaGNzaVMzaVZTWDFLDQpING9oOFVZNjFxVzdEQ2VHRzNPL3UvV3FBZ3VadGE4N2FSZWxXaFMwaXUyUzBrMmtqRWFpM2xkbE8veFR5bEFlNFNveG1PR0ZlZjQvDQpIbXFmWGY4QXltbWxmOXMzVWY4QWsvWTVqSlRIVC83aC93RGpOUDhBOG5ueFZFNHE3RlhZcTdGV0tXcXMrbCtiMVVWWnJxNkFIdWJTDQpMTGVzZngxS0U1YVJKZGFzSll6VkhzN2xsUFNvTWx1UjF5QTVKUUhrN3ByZi9iV3Uvd0JhNUxKeitBKzVBWFMvOG9ucS93RDI4LzhBDQprOU5rRXBoQi93QWQrOC81aGJYL0FKT1hHQlhhSC92RkovekZYbi9VVkxpcVB4VjJLcEpmamw1WnRZajltWTJVTC82c3MwVWJmOEt4DQp4VlM4aG9vOHIyVHF4SmxqamRsTkNWUHBxdmJ4cHlIc2NzeWMwQmtHVnBkaXFqZUxldERTemVPT2JrUGltUm5Yalg0dGxaRFduVGZGDQpVa3VvNUc4eldFZHd3WnpBUXp4aG85K01sZVB4TXkvOEY5T1NqRzRtMDJuTmhZeFdOcXR0SEpMS3FsbTlTNGxrbmtKZGl6VmVSbWJxDQpkaDBVYkFBQURJb1FQbWYvQUk1ai93Q3BQLzFDeTRxODg4OTZmbzl2NTAwZlM3T3pqRVVrYjN0NXB0cXFKSmRrT2ZUaHBzbjcxNC8zDQpqUFJmU1dUa2FESmJjUG1teXkydzBxNWwxRFNkZTFlUVQ2cGNTbjBZbDNnczRwTFdVbUczcUFUV2c5U1EvRTUvbFVLcXhReXpGVWlUDQpTOVdUVmRPdkJIYm1PeHNyaXpZZXE0WmpNOXV3WWZ1dW4rakd2enhWWDFYVEYxTzI5SFViRzJtaVU4bExUT3BRMG9XVjFqREthZHdjDQpJTmNsWTkvaDJGTGcyLzZhbVcwM0xhWitreUZvUlVxVzlMMXVGTnlPWDA1UHhUNWZKRkptbGphMkdpejJtbVc5amJwZVJ1STJOMHdFDQpqeUtWVm1sTWJzMWY1dHprZU0zYVYyaldWL1A1ZGkwL1VvOVAxS05hb3p3eU1ZSFZKQ1lxSXlQeEtnTCswYUViWUNiTnFnejVKbmlxDQptblhkM3B0czFPZHRiMzBoU283cVpZWkdVOU9oNlpQeFQxM1JTWmFUNWZnMHFXU2F6c0lGdVpoU1c1a3VKWlptQnBVR1I0MmFoNGcwDQpHMlJsSW5tbFVtc3RXazE2ejFJUjI0anRyVzZ0Mmo5VitSYTRrdDNVajkxMEgxYzErZVJWTWJLS2FLRGpNRkVoZVJ5RUpZRG5JekRjDQpoZXg4TVZWOFZkaXJzVlF1b3k2bEhBcmFkYlJYVTVrUlhqbm1hQlJHV283OGxqbUpLamNMeDM4UmlyRzd3YW5vMWg1Z211WW92cWQxDQpKTGRHODlSdU1TUEFrWkxScXJ5SGlVcjhJeXlFaFF2bVAxcWZKUzBEemJvK3UvbzY3OHRYRWVxcEJaenhPdjcyM1lnU1FvV1gxWXdODQptaklJTkQ0WkVWVzZwMzVaMDI5c29MNXJ0VlNXOXZaN3NSbzNQZ3NwRkZMVUZUdDJ3emtDZGxDWFg1MXUzMHZVN0ZyS05iYVQ2NDUxDQpGcGg2U3hUUEpMeU1hcTB0VlI2RUJmdGUyK0RaQ0cwZnpwWWE3ckdxWFBsZG85WWpzUkZZNmpDRExiU3hUeFNTbjRmWGpTTjErSTdxDQozYkFLNnBaSm9zRjFEWUFYU0NPZVNXZVpvMWJsdzlhWjVRcGJvU29laHB0WHBpVlIyQlhZcWhqcGVtSHJhUW4vQUo1cC9URlh6WmQvDQptVitjTnRPeUpwV2xpRXUzcGM5QzFkS2dTbFZSWEVKV2hpVUVQUWpjL1BGVWEzNWpmbS9iM2h0cm5TZEZNYk82aVdQUmRla2FNTTlGDQo1S3RvT1lUcHNCenFLTjN4Vk5QSmY1amVkclRXcDRmUE9rVzF6cElpVjB1TkswWFdCS2hjY2xiaWJONDNXbjJ2aldtLzJpS1lxenpXDQpQTTlyZitTTmExYnlwWUNQVUxGT05xMnBhYmRSeG1Tb3IrNkVYcXlBQ3U2S2ZIcHZpcnlmVC96Qy9PbWZuTkxwZWpCVWtQQlYwTFd3DQpaRjlHU1RpZ2EzRGIrbUtkL2lHeHFCaXFmMkg1d2VabWUyaHV2SjluSVdCV2FhTzMxZU1OSkZReThGT21PQUdCK0NyOXh2OEFzNHE5DQprMFMzYWZUSWJpLzAyMnNyeWRHTXR0RFdSVlJ5ZUtsNUlyZHlTbE9RTVlvYWozeFZCYXQrWC9sTFZaUkxkV0pTUVJtRVBhelQyWjRNDQo2dWY5NXBJdCtTRGZyVGJvU01WdE5yUFNOTHMrQnRiU0tKNDE0TElxRG5TbE4zUHhIM0pPS292RlhZcWdkZTAzOUthSHFPbWZCL3AxDQpyTmJmdkF4VDk5R1UrSUl5UHgrTGZpd1BnUmlyNUlzdnloL05IVDlSamlYOHVkSnZXNUZZcmk3bW5tdHVCNUlwbmplOGZseEFCSEpqDQp3cnQzeFY3Ym9mNVcyMXpmdzIvbUg4dmZLOXRwRWNMb0piU1o1NVVZR29DeFBheExSM1pqOXNFRHJ2c1ZYbytoZVhOQzh2MlRXT2lXDQpFT25XYlNOTTF2Ym9JME1qMDVOeEhjMEdLc0Y4N2ZrcC9panpEY2F3UE4rdmFTTGtSQjdDd3V2U3QxOUtQMDZxbE5pdzNQdVQ0NHFoDQp0Ry9JM1VkR3VoZFdIbi96Q3N3ajlJZXJMQlBHRkpVc2ZTbWprakxOd0h4TXB4VjZwaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyDQpzVmRpcnNWZGlyc1ZkaXFEMW5XTERSdE11TlQxQjJqc3JWZWM3cEhKTXdXb0cwY1N2STNYOWxUaXJENS96MS9LbURTcE5WbTE1VXNvDQpyZzJjaE52ZGVvSmxYa1Y5SDB2VjJIN1hHbnZocFcyL1BQOEFLeGJxenRUcmcrc1g4Wm10WWhiWFpMSUFUVnFSZkJzcDJhaHhwVTM4DQpxZm1ONU84MlN5UmVYNzlyMW9ZMG1rUDFlNGlVSklxc3A1U3h4clVxNE5LMXdVckpNVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkDQppcnNWZGlyc1ZXVHV5UXlPb0JaVkxLRHNDUUs3NUdaSWlTR1VCWkFXcTEwVkhLTkExTndISjMvNERJZ3o3aDgvMkpJajNuNWZ0UU9rDQo2NUZxcXp0YVJTaGJlUXhTRzRndXJVRmxORHcrc1F4ZW90UjlwS2ozdzNMdUh6L1l0Ujd6OHYycWtDNitMNlZwNUxSN0FnZWhESEhJDQpreXR5TmVjaGQxWWNhRFpCdnY3WTNMdUh6L1lpbzkvMmZ0UmZLNC9rVC9nei93QTA0M0x1SHovWW1vOTUrWDdXbWE2Q25qR2hhbXdMDQprYi84QmdKbjNENS9zVUNQZWZsKzFkQzdQREc3QUJtVUVnZEtrVnlVRGNRU2lZb2tMOGt4ZGlyR3ZPbmxXMjErT3ppbG1uaG1TYXR0DQpKQzhhK200amVycUpZcmxPWERrdGVGYUU3NHF4UlB5U3N1VHROZjNjN3lIOTYwajJGWFJsQ1BHM0hUbHFycXFodTlGRytHMVQ4ZVd4DQpwRnRiVzFxMXhGRExNSTFqaXZwSTFEeXNXWitLUkt0UzFXTkJ1Y1VJL3dEdzVlLzh0VjEvM0VKLytxZUtWQzgwZTd0WVZrYTR1MkRTDQp4UlVHb3pkWnBWakIvdSt4ZXVLcS93RGh5OS81YXJyL0FMaUUvd0QxVHhWRDZocEYzWjJrbHkxeGR1c2ZHcWpVWmdmaVlML3Z2M3hWDQpqLzhBakQ4dnVuK09kUHIvQU9CREhpclV2bS95T0kyK3IrY0xPNnVLSDBiV0hYMGFXVjZmREhHb0JMTXgyQUhmRkRLRzh2WG9JSDFtDQo3SlBocUUzYjV4NHBVNTlFdklZSnBUY1haOUpDNVg5SXpiZ0FuL2ZaOE1WWkRaMm90YlpJQkpKS0VyKzhtY3lPYWtuZGp1ZXVCVmJGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTB5cTZsVzNWZ1FSN0hBUllwSU5HMk95YzcxLzBVZEx1cmFJMjdCZFZab0JBek5HMFlVY0ptDQpuTDArUGVPZzhRMjJZMGNVYWh0K0tjZzVaWExmOFdsdmsveTBKUExOZzA5d0x1WXhrTmMzQ3lOSzlDdzVPVWxSYS9JRE5qa2tBYXI3DQp2MU9LOHYxNy9uSWI4czlCMTNVZEZ1OU8xR1M3MHU2bXM3aDRiWkdqTWx2STBibEMxK3BLa3J0VURJY1k3dnUvVXFVYWYvemxEK1hVDQppeWkrMGUvaEN1RmdhS0lURjA0cWVUMXZJdURjdVE0amw0MTNvQkdkQUQ5WDZreU5sTWZMdjU4L2wzNWs4NDZUcGRpdXFXNzM4Z3NvDQpyYVMwZ0ZxODA3QUpKSzR1NUoxNG5wd2FuaU1QR0wvcy9VcjFUem41WHA1WTFBd1hBdEorQytuY1FMS3NpTnlHNkZwWkY2LzVKeVVDDQpKR2lQdS9VdkpsVnZwOXRidGJ4aGVSaWhNZkp0eWVQRVZQdjc1aGVGRVRHM1Evb2J2RWtZbmZyK3RHNWUwdXhWRFhmOS9aZjhaai95DQpaa3hWRGFsYitaSkhjNlpmV2RxaFJCR3R6WnkzSkRoanpZbExtM3FDdEFCUVVPOVQwRVNEMC9IMnRzRGpIMUFuM0d2OTZWbXFpWVE2DQpZSm1WNWhkUUNSMFVvcGFocVZVbHlvSjdjajg4a0dzMWV5Qjh3NmhyMXBlQkxTOWloaG5DcEFuNkh2OEFVU3NoL2FrbHRaNDBDMFJ1DQpvV2xSVStNU0pkQ1B4OFcyRXNkZW9TSjhwQWY3MG8vVWpNZEt0ek82dktibXpMT3FORURXNmkvWVpuWmZrVGtoZlZxbFY3SzJvdzYzDQpJeWZvMjd0clpSL2VDNXRwTGduWS9aS1QyL0hlbmprWkE5R2NERCtJRSs0MStnb1hWMHZFOHVTTGVTeHpYUUNlckxERzBNYkgxQjltDQpObm1LL3dEQm5ERytyQ1ZYdHkvSHVlVmVhZnlXL3dDY2YvTFl0bnYvQUN4OEYyWFdLUTZzMXN2cUl2Sll4OWMxQzE1TS9SZUZmOHFnDQozd0UxMHRuanhpWE9Rajc3L1FDdjBmOEFKZjhBSmE2MEdiek5wUGxpU3h2TEUzRWxxMDE5UE15WEZrN0RsKzV1N21CK01zZjh4RzIrDQpHSnZwU01zQkUwQ0plNzl0UFhkVTB5RFVZMXQ1NUpVam8zTDBaR2pMQTBCVnVKRlJ2MHdzRU1OTmcwN1I3dTFnZVY0bGdiajYwalNFDQpBSVFGQlltaWdEcGlxYllxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGVU5IYlF5eFc3dUNXalVGQ0NSUWxhVkZENEhLWTR4SVJKDQo2QnRsTWdrRHFrZmtpNDR4NnBwekZpMWhmVFJKeTMvZGNxUjlQWWIvQUhucm1abWp0RStUVWt1cWZrTCtVbXFhbGRhbGYrWG81NzYrDQpta3VMcVl6WElMeXlzWGRpRmxBRldZbllaU3FHL3dDaGN2eVcvd0NwWmkvNlNMci9BS3E0cWpkRC9JdjhwOUMxZTIxZlMvTHNFR28yDQpiK3BiVHRKUEx3ZWhBWUxMSTZjaFdxbW14M0crS3ByNXp1RWViUjlNVTFtdXI2SnBFQklQb0thU0dvNmJOOU8rWDRSdEkrU3NpYi9lDQpoUDhBVWY4QVd1WXArb2U0L29aajZUN3grbFV5YkIyS29UVUc0Q0NibkducFNjdjNyK21wcWpMVGxSdC9pOE1WU3ErMUhXM3VZV3NiDQo3U29MWmEvV0lwekpOSSt4cHdkWGhDYjA2bzJLcWwvZUM1RnVmWHNrOUNkSnQ3bXRlSnB4K3dPdGNLcWN1cGEyWkNZcjNTbGlMR2l1DQowaFlMdlRjTUFUMDdERlhUWGQxUGFMRmRYZW5seFBES0pJNWpHdElwVWtDOFdEN3NVcFd2ZnBpcnRYbXU3NkJJclhWMDBwMWJrWjdTDQo0dG5kaFFqaVJkV3R5bE42N0tEdDFwZ1ZaZlhSYlJUYXozdHRLVVJCSmR5VHEwamxDQ1dLUlF4cVdhblJGRy9RWVZTUFYvekVuanU1DQpvYks0U0FSUXNTSk5KMWU4SHFFQW95eXd4eEk2aXU2THVmRVVPS3J0SDgwWDJyYVZkV1dwWDFwSk5lUVRlbGNDeDFEU280NHdQVGIxDQpGdlJMOFZYcW9McVdGU0FRQ2NWVFh6T0x6VjlKZXl0TDZ5c3BtZEc5YzNWeUtCVFVnRzBsc1poWDJscDRnNUV4dG5ESVlteFh4QVAzDQp0YVQra0xiUW0wKyt2dFB1cmx4S0RjeFRUb3RKQ1NQaHVKTHlYYmwzbFBnS0RiQ0JUR1VyTnNvVmxaUXlrRlNLZ2pjRUhGRGVLdXhWDQoyS3V4Vkt0Y3Z0Y3R6QW1qMnRwZHlNeGE2VzZ1bXRpa0s5WFFMRk56UGJmaVBmRFNxdWk2NXArcjJmMWl6bVdaVVlSeWxBNFVTRkZrDQpvcGRVTEtWY01yVW9WSUl5VThaaWFLQVV3eUNYWXE3RlhZcWhOUTB5Qys5UDFaYmlNeEdxL1Y3aWFDdTRKRGVreWN2czkvZnhPS3E5DQp0L3ZQRi9xTCtySVkvcEh1WjVQcVB2WWc2RFQvQURScVdveGN1VUxSR2FNQUV0YnlyVnlDZC9oa1ltZ0kzSXJzTXpZK3FBaitMYTJYDQp0YzI2MjV1VEl2b0JmVTlVR3E4YVZxQ091MlkxRzZTeFp2MHg5Yi94UnpmNnNGRVEwNEVjVGFFOGpMMTQ4eDEvSGx4b0JrK211RDdmDQpOREtWdUlHZ0Z3SFgwQ3ZQMUswWGpTdGFuTWFqZEpZamJoNy9BTTEyR29TQmh6OVpvNG4zQXQxVmxqSTY3bDFERWRLaW96Sm42WVVoDQpsVXNqcmUyNkNKM1YwazVTcVY0cFRpUnlxd2Jmb09JT1lSK29lNC9vWmo2VCtPOUVaTmk3RlVOZC93Qi9aZjhBR1kvOG1aTVZZaDUzDQpHdU5ydG5IcDkrYldGb0Fab3hQTEd4Q3lFTXlScHN4K0lWSk8yMmFUdEtVeGsybEtQcEhMM2xJTEp0Yi9BT1BEL21NaC9qbTdRcTNXDQp1NkphWERXMTFxRnRiM0txc2pRU3pSbzRSeVFyRldJTkNWTkQ3WkV6aU9aYkk0cGtXQVNQY29hbmMyOTFwZHRjMjBxVDI4MXhaU1F6DQpSc0hSMGE2aUtzckxVRUVkQ01rRGJDVVNEUjVxMnFhamVXVVJlMjB1NjFOZ0FSRmF0Ykt4cVFLZjZUTmJyc04rdjQ3WW9RMnN6U1RlDQpYSG1rZ2Uya2tTSjN0cFNoa2pMTXBLT1kya1RrdlE4V0k4Q2NWWDZqNW4wUFRaR2p2cm4wQ3JCQ1dTVGp5WmVRWGtGNGs4ZCt1Uk1nDQpPYktNRExrTFFsN3IyazZwNWUxQjdDZjE0NUxHYWFLUUk0UjR6R2FNak1BckQ0aDBPRUVIa2lVU0RSNXNDL05QekgrYjFsK1pIbFhTDQovS0tXNDBLNzR2ZitxMXN2ck1zanZjUnpOTDZrc2NTVzBQSU5HZ083VUxOUlFtUUczVkloSWl3Tm1VNkorWitoYXJxUWdOMXB0bGJ2DQpGV09PZlZiS1MrYWJacUxiV3IzTVJpOU1rOC9YNVZIMktmRmxjYzBUMSswT1RsME9XSFFrZC9ETDlJREt0TS80NXRwL3hoai9BT0lqDQpMWEVST0t1eFYyS3V4VjVKNS9mOHJaL09zMm5hOWRYMm42cEphYzd5NnQyOU8zOUFKeXJJOUdQU0plMU52RGxYUHc1TXNNZkVLNGJZDQpHaWE2czY4cnorVU5Xc2JlL3dCRHRsTnRieVNOYnp2YXkyN0xLeGFLWmg2OGNiK29lTEs1NitQWE1LVWlUWlpzaHlLdXhWMkt1eFYyDQpLcWR0L3ZQRi9xTCtySVkvcEh1WjVQcVB2U1dkSUI1dE1VeUF4WHRnWStOTm1aWEpZTjgwL1ZtUkUramJvV0NBdDRMKzZ2NVBMczZTDQpEVDlQWlpudTJOUFdqZGkwY2RRRjZVL1pvQjJweDN0SkFIR09aUXl3S29YaUFBb0ZBdmFuaFRNVkxFN3UyMUsydjA4djJ5c05PMUpwDQpKbHVBcEloalVocFl0Z1YrUGwrMVFlNVpzeWhLSkhFZnFIMnJTWTJxQSthNVZpWGhCWTJTMi9BZlpIcU9IWDViTFFmTEtwSDBqektvDQovVko3bUMybm50WXZYdW9yYVo0SWR6emtVQXF1Mis1MnlnRDFqOGR6SWZTZmgrbGdlamVlUHpkdWJTS1M0OGhxN1NlcjZqZlgwc3pHDQpVM1ZURE9yc2VmN0xLMUs5UUJ2bVJtaEdNcWliSGV3aVNSdTlCc0xxZTVoTWs5bkxaT0haZlJuTVRNUXBvSEJoa2xYaTNVVk5mRURLDQpVckwrV09LU3prbGRVUVRHck1RQUt3eURxY1ZlUitmdGF1ZFQ4ME8rbXNaTGFDRVdhVzl4YmF2NlVzNlNua3g0V1VscndQcWJTY21CDQpBclduVFg2dlM1TWtyaWEvSDQ2dCtPT0lqMUVnKzYvOThQdWVnSHpKcG1yV0duWGNNaGovQU5MaTlXT1pKcmRsSzFEZkJjeHdTOGE5DQpHS0N1YkFOQnJvalg4NCtYMHZKN1F6U21XMzRtUmhiWERSSGtLamhLSXpISjc4R05PK0txVjNyMms2aHBjTTl0UFJXdWJOZ3N5UEJJDQpBTGlKeVRITUVjVVhyVWJmUmlxbDVvMUR5UkxhR1BYcmFIVkxZQUgwRFp2cUlvWFgvZGNVVS83UVU5TzFlMkt0MzJxYUxjZVdBYkNSDQpJcmQ0NHZxOXVVTnU2b0hYaXZvU0NONDZEOWtxS1lWUUhtdnpUb2RqY3JGRllRNmxmeVJ0TEl6Mjk0OFhwcEc1SCtrV3RuZkp6NUlGDQo0R2pVTzFUeFZvbStqUEdJbjZpUjdoZjZRc3QvTVdnWDJpNnpEQmJwWTM4VnRPa3lyYjNNRWJBcTRUMHByaTN0Qk5XbFNFQnA3N0VzDQpiNnNaVmUzTDhlOUxmTVBsalNJSUpiK2J6dDVqaGhTNCtzZWxwOTBibHdXS29rYVF3d1R5dEd0UHM4U055V3IxeXJ3UE9YemMyR3VyDQovSjR6dC9OL2F5N3kvcVdpcm8xbkZiMzhzMFVNU1JyUHFEU0xkUHhVRG5OOVlFY2hkdXJFcU44dEFvVTRjNThVaWRoZmR5VERUQVJwDQoxcUQxOUdQL0FJaU1MRkU0cTdGWFlxN0ZWRTJWbWJrWFJnak4wbzRyT1VYMUFPbEExSzk4TnFyWUZkaXJzVmRpcnNWYVplU2xha1ZCDQpGUnNSWHd3RVdFZzBWS08zYU9OVVdaK0tBS3RlSk5CdDFLNUNPT2hRSit6OVRNenMzUVFPcTZGOWZhR1ZieWUzdXJjT0lKNCtBSzg2DQpjcWpqdlhpTXR4eU1mUDNzQ2I2SmZlSHpYREZKYlNJdW9RR09odUxkemIzQUoyRENsS1VJclJhbkpFeE5nYkg3RkE1VzNhZWFOTXRyDQpXS3ppZ3ZHdUlGRVMya29rbXVhSUFBN21Sbm1ldmRtcTNjNUx3dk1VaTFGYmZ6bGZhaTk0c3E2ZGJNZ1NLQ1lSUytudUN6eDBqV1FsDQo2ZkVIWWUyUU1ZaVFQUG15RXZTUW5HazZPdW50TkswOGx4Y1hIRDFwSkNUOWl2RUNwWnYydjJtT0dVN1lveG5UNjNHbkljL1RjOGE3DQowNUx2VEtDUnhnZVIvUXpBUENUNWo5S3JsakIyS29lOGFYbGJ4eHVZL1ZrS002aFNRQkc3N2NndzZyNFlxMTlVbi81Ylp2dWgvd0NxDQplS29UVVRlVzMxYjA3eVUrdmNSd3R5V0xaV3JXbEVHKzJLc0svTnp6enIza3kyMHg5TlkzTDN6eXJJOHlSdWtZaVVNS2hCR2ZpNVVyDQpYcjI4TVhVNXpDcXJmOW42MVRueXg1bnV2TVBrN1QvTVVMeTJwdnBraStyTVlaZUlONTlXYjR2U2pxYUFrYlpmaW54UnRVWEY1aGhiDQp6QmRhUzE4d1czalZsbUVsdXptU2tqU3h0Q0l1Y2ZwcEdyY20yUExiSmNRUmJSMWk5a3NydWVPYVNKb1hwQUdhM2s1eGgxVG0zQ09pDQo4cWtxT1IrR2hORFZRZ2dwVytkdGJsOHQ2TU5TazFKYmVFU3JITE5kK2lrU3F5c2QyNG9CdUIxT1krcXl6aEVHSXMyMjRvQ1IzUnJ6DQozNDh0dHFZdTVQckFzemNoZU1YRG42WE9sT0ZhVjk4dXhTNG9nbnFHRXhSSVc2cHJOdHBrdnAzTStxU05SVFcxMDZlN1g0K1ZQaXRyDQpXVmYyRFhmYmF2VVZKbUIvWVd6SGdsUGxYeGxFZmVRdFRWa3ViZVpyVzQxQkpJNEdtWDZ6WlNXNjdJSEFKbnRveFg0aDhOYTlSMUJvDQppWVA5aFl6eEdQT3ZtRDl4VC9KTmJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXFBMWJYdEYwaU9OOVR2WWJUMW00UUpJNER5DQp1ZGdrU2Zia2NuWUtnSko2REZWRmRPOHNIWGwxSVcxb05mbGdBVzZaSXhlR0RmWU1SNm9URlVScU4zcGRoRUo3emlvZHVLQVJtU1IzDQpOVHhSRURPN1VCTkZHUU9PSjZCbHhrZFZJNnI1Y1ZyTkd1clJaTlEyc1kyZU5YbUk2aUpTYXVSM3BqNFVlNEo4U1hlVVM1MDFKR2pjDQp3cklpZW82SGlHQ1ZweUkvbHFPdVBoeDdndmlTN3lxL1ZyYi9BSDBuL0FqSHc0OXdYeEpkNVZNbXdkaXFHdS83K3kvNHpIL2t6SmlxDQpGdnRKdjdtN2FlSFdyMnlpS0tvdFlFczJqQkRWTGd6Vzhzbkpoc2ZqcFRvQWQ4Z1lrOVQ5bjZtMk9TSUZHSVArbS9RVm1xUnZGQnBrDQpieXRPNlhVQ3ROSUZEdVFDQ3pCRlJLdDFQRlFQQVpNTmNqWmVmL245NVgxZlhOTDB4OU9zWnIvNnE4L09HM0RPNVoxUW9QVEJDbXZBDQo3djhBQ0RUTUxWeGxjVEg4ZnArVENRVGp5RG91b2FKK1YraGFacU1MUVhrRTl1WllucnlIcWFpSkY1Vlp5Q1ZjR2pHbzc3NWtZQVJIDQpmWktRUmFoNStsL01yVnJlWFNOUWk4dFFKZG0ydXVNVFc5dzZ3UHhRUi9WL1crTm4ySWwzS2pzMURBOCtYZjNzcVRUUjdxOHVZTlVlDQpXeHVyT05WRVpGemF5Mnk4MGxqSDdzeVJ4OHdhMURBa0hzY2NIRmU0VXNnODlTckxaUjI0UzUvY3pMSk0wTnJkVC9BWTNVY1BRdHJyDQptM0poc0tVNms3VU1OWERqalE1L0Z5Tk5FM2UzemlQdks2TzdzVDVRdk5PdElKNEYwL1RXaDRTMmx4YlJnSkU4WVdNelJRS3dIcG43DQpQYWhvQVJsK0Ura0E4d1BQOUxYbHhrRzl0L01IN2ltZXBhUGM2aWtTWEYweUNHUlpVTnM5MWFrc3ZRT2JlNGlMcjRvMVZQY1pZMUtCDQowdTYwL1FwN1NDNTlTR09HV2pYQnVMbVUxVW40cHJpZVdSdjlreHhWTzhWZGlyc1ZTL3pCcnRsb1drejZuZThqQkJ4QlZPSVptZGdxDQpxdk5rV3BKN3NNVllaYi9udDVGWVBIZUMrMCsrQ3lTUTZmUGF2SmNTeHhmYWRGdHZyQ2djcXJSMlZxZzFBeFZFWG41M2ZseGFKcUhQDQpVbmU0MHNoTHkyUzJ1R2RaRzVVUU1JK0RieHQ4UWJpS2JtbUt0MmY1eCtUVGJTUzZqZGZVeWc1cVZqdUpvNUk2UkhuR3doVXVCOVlqDQpEVVdnSnBVNGFWZDVWL09EeWY1cTh3RFNkRXVmWERRTlBETTZUeE5LRllxeGpSNGg4Q2tFTVhaVFg5a2dnNEZaeGlyc1ZkaXJzVmRpDQpySHRmL0wveWo1Z3V4ZDZ4WW03bkNoVkxUVHFxaFF5cVZWSFZWSTlScUVDdUtwRGQva3g1TlZMYTMwclQwc3JabVdMVUdFdHd6dmFLDQpqZ3d4aG5ZQXljK0ROMUNNd0c1eFZIeWZsRCtYa2wzRGR2cE5ibTNkNUlaZnJGeUdWcEhaM3BTWG96T2ZoNmR1bUtxVTM1TS9sdE1YDQpNbWtGdVVjY1FIMW03QVJJbFZJeEVCTFNMaXFBRGhURlZXWDhwUElNbG9MVTZkSUlWQTlNTGQzWUtNb1VCMVlTMURqMHg4WFd0ZjVqDQpWVmtPaGFEcFdoYWJIcHVsd21DempaM1ZHa2tsWXRJeGQyYVNWbmRpV1BWbU9Lby9GWFlxbGV0WFR4UENxMnQ1TXlreUxMYUpHM0U4DQpTbEc5UStEZUdLc1R0dENzN2NValR6VzN4ckorODFHNWwzU3RCKzh1VytIZmRlaDdnNFZUQ0lHS0dPRmJYWFhTT2Y2eXBtTVU3YzZrDQovYm1hUnVPK3kxb08xTVZTaldmSjNsYld0WWoxalU5QTFXZlVJdVBHVGpHaUVweTRNOFNPc2Jzdk04V1pTUjJ5Qnh4UFFOc2RSa2lLDQpFcEFlOUY2SG9tazZGcFkwdlNOSjFlMXMvV2p1Q25HR1Jta2hLY0Mwa3JPN1VFU0x1ZWdwaEVRT1RHZVNVemNpVDcxUFcvTGZsL1c3DQoxYjNVOUMxT2E3WDB5WmxqZ2laL1NZUEg2bnBzbnFjR1g0ZWRhYjA2bkFZUlBNQmxIUGtpS0VpQjcyOUQ4djZIb09qUG8ramFMcWxqDQpZeU56bFdPSzNaM2FvUEo1SEx5T2RxVlpqdHRoakVEa2pKbW5QZVJNdmViUW5tZnlWNVU4MFhzVjdyMmc2dGZYRVJRb1g0S3A5T3ZBDQpOR2tpb3lyeWFnWUVmRTM4eHJKclZORDhvZVY5QXQ1NE5GOHY2bHA2WE1CdHAyaGl0K1R4c0FEeVptWXMyMzJqdmlxYmF3cDFhM1NDDQo2dC9NRWFJL3FBMmNvc25xQVJ2SmF5UXV3Myt5VFRGWFdLbXkwNXRQaHQ5ZmtnY09DMXk2WGN2eDlmM3R5ODBueUhMYkZXVldkeWJtDQoyU1l3eVc1ZXY3bVlCWEZDUnVBV0c5SzljQ3EyS3V4VjJLcVY1YmZXYlNlMjlWNGZYamFQMW9pRmtUbXBYa2hJSURDdFJ0aXEyd3NiDQpUVDdLQ3hzNHhEYTIwYXhRUkwwVkVGRkgzWXFyNHE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3DQpGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcThaMW4vQUp5ZThxNlA1bzFMUmJuVHJtNGcwK1o3WVh0b3l0emtqb0hCam0rcjhlTDhsSjVIDQpvS1ZCcUppQklSWVZILzV5aDhsSkF0dytqYTBMWnp4anVQUXQvU1k3L1pmNnh4UDJUME9QaGxIRUUyL0xmODh0STg4Ni9McEZ2cDB0DQpneVc3WE1NbHhLaE1uQjFVcXFLUEJxOWVnd1NqU1FYcG1SU3hUOHpmekF0UElubGs2M2NXeHZHYWVPMnQ3WVA2WWVTU3JHcjhYNGdJDQpqTjlrOUtZWWl5Z2xnRnIvQU01VitSM1dNWGVtYWhDN3F0ZlRGdktvYW54ZkVab3pRTjBOTi9BWlB3eWppQ0lsL3dDY3B2eTVqWUQ2DQpycWJWQUpQcFFBQ3ZqeW5CL0RCNFpYaUQxK0NlRzRnam5nZFpJWlZEeFNLYXF5c0txd0k2Z2pJTWwrS3V4VjJLdXhWMkt1eFYyS3V4DQpWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3FHb1hrVmpZWE43TFgwcldKNXBLZGVNYWxqK0F4VitkTTkyOGtvDQp1V1p6SzNJdTVZazFlcGFuekxObDdXaHhNVkxnZnRIY2lnSTJyVUVlT0t2UnZ5RTFZMkg1bjZGT3RLejNCdFhVMUlwY0kwUTkvd0RkDQpnSTN5TXVTUnpmYitWTTN6L3dEODViWEpGaDVac3VSQ1R6WGNyRHNURXNTaXYvSTA1WmpZeWZPNmg2amt4Q3VRVlVrdHVEeVBjdCt6DQp2WExtRGxTa2FPcXNvY1Via1R2SlFjcUVVcGlyN0kvSWZXMzFiOHJkRmVWaTA5bWoyTXRlMzFkeWtZLzVGY014NWpkc0QwRElwZGlyDQpzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWWS84QW1KSVl2eS84elNBVkthVGZNQnYxDQpGczU3YjRSelV2ejdjRkNhSGJxUmw3V3BrTXFvNVdpbXZId1lEWTc1RkxLZnlza2RQeks4cW5zZFdzVjhOamN4akdYSlErLzhwWnZuDQpQL25MTkpSZmVXWExVajlPN0VkQzFlYXRDVzlodFRmTE1iR1R3RzRtbFJvQ05tWFpPRkFkanlydHVTS0VjdXVXbGdHMVlHSUJSdVFLDQpwUUViVi9hNjRxK3B2K2NXR2MvbDNlcXhydzFXWUFlQU52QWYxbktaODJjWHNlUVpPeFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWDQoyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMktzYy9Nbi93QWwzNW8vN1pOOS93QlF6NFJ6UVh3QkpGOFpJNmRRUjQ5alhMbURLUE5mDQpsNXJEeVA1TDFJS1F1cVEzN0ZqWHJGZHNnRmZzazhlSjI4Y0hWbDBVZnl1Mi9NZnlxZW4rNWl3SGJ2ZFI0SmNrQitnR1ZNM3pyL3psDQpxYTNQbFpCOXJoZmtlMjl2bG1QbXhrOG8vTExSSU5iOCs2RnAwdjd5R2U1VnBRRlAyVVZwSDdNSzhZemxrdVRHUE5qbHphUzJOekxhDQp5cXlUVzhoUmxJSUNsQ1ZOUHBYQ2d2cDMvbkZZcVBJZXB4aGdXWFZaR0svdEFOYlFVSkZlOU1wbnpaeGV6NUJrN0ZYWXE3RlhZcTdGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxeDM4eDZmOHE4ODBWNmZvaS9yVC9tR2t3eDVvTDRGWUhrDQphZEJ2dWZhdENjdmEzdGY1MWFISllmbEIrV2hJQU1GczRrb0FRSHU0WXB5S2cwNnFmbmtJOHl6UEo1aitXY1JINWtlVkRXbE5ZMCtvDQo2LzhBSDFHY011VEVGOS81UzJQbC93RDV5d3ZwNWZOK2k2ZUFQU3R0UGFkVDBQSzRtZEdxYTlLUURMTVlZeVNIL25HNjBhZjgwN0p3DQpLaXpndVptNmJjb2pGMC81NlpLZkpFVWsvT25UQnBYNW4rWWJWR0JFdHdicXRPTkJkS3R3YWZJeThjTVRzcGVxL3dET0pNNU1QbWVBDQo3QldzblhmYjRoT0Qvd0FSeXVhWXZvVElNbllxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYDQpZcTdGV0EvbnplUzJuNVMrWVpZblpHYUtHRWxXS2twTmNSeE9LanN5T1FSM0cyU2p6UWVUNGhTSm5rUlVCTHVRQnQxSlA0NWUxdnF2DQovbkpqUjB0ZnlqMG0zVlZiOUdYbHBGenBVaEZ0NUl0aWQ2RThhNVRBN3M1Y256TDVRdm4wM3pabzJvS28vd0JEdjdhNENzYUNzY3l0DQp1ZTNUTER5WWgraGVVTmo1Qy81eWExRjdyOHo1NFBoSDFDMXQ3WmFEc3krdnZ2MXJQbGtHSlQzL0FKeFBzU2ZOdXMzbERTQ3c5Q3BODQpmNzJkR0E2LzhWWXpXS0UvNXlsMGhMZno5YVgwY2ZFYWhZUmwzM28wc01qeHQveVQ0REREa3NsLy9PTFY3SEI1K3ZiWm00ZlhOT2tFDQphL3pTUnl4dFFmN0RrY0UxaStxTXJaT3hWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMkt1eFYyDQpLdkxQK2NsdFRoc3Z5b3ZvSDVjOVF1TGEyaDQwKzBzb25OYW4rU0J1bVNnTjBTNVBsSHlQcHcxTHpsb2Vuay83MVg5dEExVHRSNWxVDQpuNkFhNWNXQWZYUC9BRGtUWnBjL2xIclRFVmUzTnROSFhzUmN4cVQvQU1DeHltSE5uTGsrS0ZjcTZ1UHRLd1lmUWRqbHpXL1I1V0RLDQpHWGNNS2creHpIYlh4ZCtkMTJ0LythUG1DWUx4NHpyQVJVdFg2dkVzRmEwSCsrNjB5MlBKZ2ViMVgvbkV2VDVJdE04eDNwUWlPYWEyDQpoUjZVRllra1pnRC9BTTlSa1psa0ZmOEE1eXQwYjF0QzBQVmxBLzBhNWx0SElIeEVYQ0NSZTNRZlZ6OStNRVNlVWZrTGVKWi9teG9UDQp6TUlrbWFhRnU5V2x0cEZSZS9WeXVTbHlSRjltNVV6ZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWDQpkaXJzVmRpcnNWWVgrYS81Y256L0FPWElOSEdvZm8wd1hhWFluOUwxK1hDT1NQaHg1eDAvdmExcmtveXBCRnZLL3dBdlArY2JQTXZsDQpyejVwV3RhamVXTjVwZGpNOHpyRTh3bTVMRzNvc0VhTUx0SnhKK1A3OG5LWUlRSXZhL1Bta1hHc2VTZGUwdTFoVzR1N3l3dVlyV0ZpDQpvRFR0RTNwQ3IwVUhuU2hQVEt4elpGOHVhYi96aXgrWjk0dks1K29hZHhOQWx4Y0YyUHVQUVNZZmpscG1HSEMrdE5GdHJ1MTBleHRyDQp4eEplUVc4VWR6SXJGZzBpSUZkZ3pCU1FXSFVqS1did1B6aC96amo1dzFmekxxMnIyV3BXSHA2aGR6M0tKTTh5dXF6U0Z3cHBGSU51DQpWT3VURWtVOUovSlR5TnJIa3p5bGNhWnEvcGZYSjcyVzVwQTVkUWhTT05keUY2K2xXbnY5R1JrYlVCTXZ6UzhreWVjL0oxeG9rRWtjDQpONDBzTTFyY1RjaWtieHlEazFGM05ZaTZqNTRnMGtzSThtLzg0MDZEb09xV2VyWHVzM1Y5ZjJGekhkVzNvcEhiUTFpWU9xdWpldXpDDQpvM280eVJtZ0I3SmtFdXhWLzlrPTwveG1wR0ltZzppbWFnZT4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJPC9yZGY6QWx0Pg0KCQkJPC94bXA6VGh1bWJuYWlscz4NCgkJPC9yZGY6RGVzY3JpcHRpb24+DQoJCTxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1sbnM6c3RNZnM9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9NYW5pZmVzdEl0ZW0jIj4NCgkJCTx4bXBNTTpEb2N1bWVudElEPnhtcC5kaWQ6OUZCNzQ3QjA4QTIzRTQxMUE5QkFBN0Y0MjNCRUUyNzI8L3htcE1NOkRvY3VtZW50SUQ+DQoJCQk8eG1wTU06SW5zdGFuY2VJRD54bXAuaWlkOjlGQjc0N0IwOEEyM0U0MTFBOUJBQTdGNDIzQkVFMjcyPC94bXBNTTpJbnN0YW5jZUlEPg0KCQkJPHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD51dWlkOkRCQjE3RTU2Qzc5NUUxMTFBOUZBRjNBNDFFRjdBQkQxPC94bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ+DQoJCQk8eG1wTU06UmVuZGl0aW9uQ2xhc3M+cHJvb2Y6cGRmPC94bXBNTTpSZW5kaXRpb25DbGFzcz4NCgkJCTx4bXBNTTpEZXJpdmVkRnJvbSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJPHN0UmVmOmluc3RhbmNlSUQ+dXVpZDo5MGY5ZmZmYy0yNWZlLTRlNjItOWVhMy1lMzNlMDBkOGEwODE8L3N0UmVmOmluc3RhbmNlSUQ+DQoJCQkJPHN0UmVmOmRvY3VtZW50SUQ+eG1wLmRpZDo3NkU0REUxMkU1REJFMzExOEU2MTg1M0IyREFCMDNFMDwvc3RSZWY6ZG9jdW1lbnRJRD4NCgkJCQk8c3RSZWY6b3JpZ2luYWxEb2N1bWVudElEPnV1aWQ6REJCMTdFNTZDNzk1RTExMUE5RkFGM0E0MUVGN0FCRDE8L3N0UmVmOm9yaWdpbmFsRG9jdW1lbnRJRD4NCgkJCQk8c3RSZWY6cmVuZGl0aW9uQ2xhc3M+cHJvb2Y6cGRmPC9zdFJlZjpyZW5kaXRpb25DbGFzcz4NCgkJCTwveG1wTU06RGVyaXZlZEZyb20+DQoJCQk8eG1wTU06SGlzdG9yeT4NCgkJCQk8cmRmOlNlcT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjhFRDFCQkRDQzI5NEUxMTFBMTBCRTc1RDEzRjgyNDVBPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxMi0wNS0wM1QwOTo1NDowNCswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNS4xPC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo5MUQxQkJEQ0MyOTRFMTExQTEwQkU3NUQxM0Y4MjQ1QTwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTItMDUtMDNUMTU6MjY6MTUrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzUuMTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6Q0JDRDg2N0Y4NDk1RTExMUEwNDVBOEMwRDgxOEJDMkY8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDEyLTA1LTA0VDEwOjAyOjE3KzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1LjE8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOkE3REJFNjc1NzhEMEUxMTE5MjRCQzAwMTBDNzBDMDU1PC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxMi0wNy0xOFQwOTozNDo0MCswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNS4xPC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo2MjIzNTk5RUY3RjZFMTExQThEMThGQjAwRURGNDA1Njwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTItMDktMDVUMTQ6MzY6NTUrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzUuMTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6Q0RDRjQ4MjhCRkY3RTExMUIzNDhCQUY1NzFEN0RGRDQ8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDEyLTA5LTA2VDExOjQ5OjUzKzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1LjE8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjkxNUZCODkzMzEwMUUyMTE4NkMwRjc5QUJGQkUyOTRFPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxMi0wOS0xOFQwOTozODo0MSswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNS4xPC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo5MzVGQjg5MzMxMDFFMjExODZDMEY3OUFCRkJFMjk0RTwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTItMDktMThUMTQ6MTM6MTIrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzUuMTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6Q0IxMzA3OUY3NjVERTMxMTkxQTFFMkM2MzBERUUxNUI8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDEzLTEyLTA1VDE0OjQ5OjQ1KzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1PC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDozNjlGMkNEQjlDQTVFMzExQjFEMjlFMzM3NDE3QzZCQTwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTQtMDMtMDdUMTA6MDQ6NDYrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzU8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjM4OUYyQ0RCOUNBNUUzMTFCMUQyOUUzMzc0MTdDNkJBPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxNC0wMy0wN1QxMTo1NzoyOSswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6QjRFOEREQkYxQUQwRTMxMUIzNTJDMkZEQ0ZFRjAzNEY8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDE0LTA0LTMwVDE1OjA1OjA1KzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1PC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo3NkU0REUxMkU1REJFMzExOEU2MTg1M0IyREFCMDNFMDwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTQtMDUtMTVUMTE6NTc6NDYrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzU8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjlGQjc0N0IwOEEyM0U0MTFBOUJBQTdGNDIzQkVFMjcyPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxNC0wOC0xNFQxNjoxMjowOSswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCTwvcmRmOlNlcT4NCgkJCTwveG1wTU06SGlzdG9yeT4NCgkJCTx4bXBNTTpNYW5pZmVzdD4NCgkJCQk8cmRmOlNlcT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RNZnM6bGlua0Zvcm0+RW1iZWRCeVJlZmVyZW5jZTwvc3RNZnM6bGlua0Zvcm0+DQoJCQkJCQk8c3RNZnM6cmVmZXJlbmNlIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCQk8c3RSZWY6ZmlsZVBhdGg+RDpcVXNlcnNca3Jpc3RpbmEuc29cRGVza3RvcFxOZXcgZm9sZGVyICgyKVxOZXcgZm9sZGVyXFAxMTUwMTY1LkpQRzwvc3RSZWY6ZmlsZVBhdGg+DQoJCQkJCQk8L3N0TWZzOnJlZmVyZW5jZT4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0TWZzOmxpbmtGb3JtPkVtYmVkQnlSZWZlcmVuY2U8L3N0TWZzOmxpbmtGb3JtPg0KCQkJCQkJPHN0TWZzOnJlZmVyZW5jZSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQkJPHN0UmVmOmZpbGVQYXRoPkQ6XFVzZXJzXGtyaXN0aW5hLnNvXERlc2t0b3BcTmV3IGZvbGRlciAoMilcTmV3IGZvbGRlclxQMTE1MDE2NC5KUEc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5SVlc4NUVTSS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5CVFdCQy5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD53YWlzdCB0aWNrZXQuanBnPC9zdFJlZjpmaWxlUGF0aD4NCgkJCQkJCTwvc3RNZnM6cmVmZXJlbmNlPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RNZnM6bGlua0Zvcm0+RW1iZWRCeVJlZmVyZW5jZTwvc3RNZnM6bGlua0Zvcm0+DQoJCQkJCQk8c3RNZnM6cmVmZXJlbmNlIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCQk8c3RSZWY6ZmlsZVBhdGg+YWxsIGJvdHRvbXMgaGFuZ3RhZy5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5kOlxVc2Vyc1xNRFNHMTBcRGVza3RvcFxXUkFOR0xFUiBUUklNU1xIQU5HVEFHMi5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5kOlxVc2Vyc1xNRFNHMTBcRGVza3RvcFxXUkFOR0xFUiBUUklNU1xIQU5HVEFHMS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5CVDE3TENFU0kgQS5CUkFTUy5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9";
       }

       function Bindgrid(item, e, column, s) {
           if (column.fieldName == "ColorCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[0]);
           }
           if (column.fieldName == "ClassCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, "A");
           }
           if (column.fieldName == "SizeCode") {
               s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, item[1]);
           }
           if (column.fieldName == "BaseUnit") {
               s.batchEditApi.SetCellValue(e.visibleIndex, "BaseUnit", txtbaseunit.GetText());
           }
           //if (column.fieldName == "ItemImage") {
           //  //  var imagePath = item[2];
           //    console.log(item[2]);
           //    s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName,"data:image/jpg;base64,"+item[2]);
           //      //  "/9j/4AAQSkZJRgABAgEBLAEsAAD/7gAOQWRvYmUAZAAAAAAB/+EASkV4aWYAAE1NACoAAAAIAAMBGgAFAAAAAQAAADIBGwAFAAAAAQAAADoBKAADAAAAAQACAAAAAAAAASwAAAABAAABLAAAAAEAAP/tACxQaG90b3Nob3AgMy4wADhCSU0D7QAAAAAAEAEsAAAAAQABASwAAAABAAH/4aSdaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/Pg0KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4wLWMwNjAgNjEuMTM0Nzc3LCAyMDEwLzAyLzEyLTE3OjMyOjAwICAgICAgICAiPg0KCTxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+DQoJCTxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyI+DQoJCQk8ZGM6Zm9ybWF0PmltYWdlL2pwZWc8L2RjOmZvcm1hdD4NCgkJCTxkYzp0aXRsZT4NCgkJCQk8cmRmOkFsdD4NCgkJCQkJPHJkZjpsaSB4bWw6bGFuZz0ieC1kZWZhdWx0Ij4yMzggQkxBQ0sgT1JPPC9yZGY6bGk+DQoJCQkJPC9yZGY6QWx0Pg0KCQkJPC9kYzp0aXRsZT4NCgkJPC9yZGY6RGVzY3JpcHRpb24+DQoJCTxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wR0ltZz0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL2cvaW1nLyI+DQoJCQk8eG1wOkNyZWF0b3JUb29sPkFkb2JlIElsbHVzdHJhdG9yIENTNTwveG1wOkNyZWF0b3JUb29sPg0KCQkJPHhtcDpDcmVhdGVEYXRlPjIwMTQtMDgtMTRUMTY6MTI6MDkrMDg6MDA8L3htcDpDcmVhdGVEYXRlPg0KCQkJPHhtcDpNb2RpZnlEYXRlPjIwMTQtMDgtMTRUMDg6MTI6MTdaPC94bXA6TW9kaWZ5RGF0ZT4NCgkJCTx4bXA6TWV0YWRhdGFEYXRlPjIwMTQtMDgtMTRUMTY6MTI6MDkrMDg6MDA8L3htcDpNZXRhZGF0YURhdGU+DQoJCQk8eG1wOlRodW1ibmFpbHM+DQoJCQkJPHJkZjpBbHQ+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHhtcEdJbWc6d2lkdGg+MjU2PC94bXBHSW1nOndpZHRoPg0KCQkJCQkJPHhtcEdJbWc6aGVpZ2h0PjI0MDwveG1wR0ltZzpoZWlnaHQ+DQoJCQkJCQk8eG1wR0ltZzpmb3JtYXQ+SlBFRzwveG1wR0ltZzpmb3JtYXQ+DQoJCQkJCQk8eG1wR0ltZzppbWFnZT4vOWovNEFBUVNrWkpSZ0FCQWdFQXRBQzBBQUQvN1FBc1VHaHZkRzl6YUc5d0lETXVNQUE0UWtsTkErMEFBQUFBQUJBQXRBQUFBQUVBDQpBUUMwQUFBQUFRQUIvKzRBRGtGa2IySmxBR1RBQUFBQUFmL2JBSVFBQmdRRUJBVUVCZ1VGQmdrR0JRWUpDd2dHQmdnTERBb0tDd29LDQpEQkFNREF3TURBd1FEQTRQRUE4T0RCTVRGQlFURXh3Ykd4c2NIeDhmSHg4Zkh4OGZId0VIQndjTkRBMFlFQkFZR2hVUkZSb2ZIeDhmDQpIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGZIeDhmSHg4Zkh4OGYvOEFBRVFnQThBRUFBd0VSDQpBQUlSQVFNUkFmL0VBYUlBQUFBSEFRRUJBUUVBQUFBQUFBQUFBQVFGQXdJR0FRQUhDQWtLQ3dFQUFnSURBUUVCQVFFQUFBQUFBQUFBDQpBUUFDQXdRRkJnY0lDUW9MRUFBQ0FRTURBZ1FDQmdjREJBSUdBbk1CQWdNUkJBQUZJUkl4UVZFR0UyRWljWUVVTXBHaEJ4V3hRaVBCDQpVdEhoTXhaaThDUnlndkVsUXpSVGtxS3lZM1BDTlVRbms2T3pOaGRVWkhURDB1SUlKb01KQ2hnWmhKUkZScVMwVnROVktCcnk0L1BFDQoxT1QwWlhXRmxhVzF4ZFhsOVdaMmhwYW10c2JXNXZZM1IxZG5kNGVYcDdmSDErZjNPRWhZYUhpSW1LaTR5TmpvK0NrNVNWbHBlWW1aDQpxYm5KMmVuNUtqcEtXbXA2aXBxcXVzcmE2dm9SQUFJQ0FRSURCUVVFQlFZRUNBTURiUUVBQWhFREJDRVNNVUVGVVJOaElnWnhnWkV5DQpvYkh3Rk1IUjRTTkNGVkppY3ZFekpEUkRnaGFTVXlXaVk3TENCM1BTTmVKRWd4ZFVrd2dKQ2hnWkpqWkZHaWRrZEZVMzhxT3p3eWdwDQowK1B6aEpTa3RNVFU1UFJsZFlXVnBiWEYxZVgxUmxabWRvYVdwcmJHMXViMlIxZG5kNGVYcDdmSDErZjNPRWhZYUhpSW1LaTR5TmpvDQorRGxKV1dsNWlabXB1Y25aNmZrcU9rcGFhbnFLbXFxNnl0cnErdi9hQUF3REFRQUNFUU1SQUQ4QTlVNHE3RlhZcTdGWFlxN0ZYWXE3DQpGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYDQpZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZDQpxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxDQprWG5HTHpkTnBKaThyejIxcGZrZ3RkWGJsVlJWM05GOUc0VTE5eHRpckR2eTlsL04rWFQ0cnE3azB5NjBtY1hFMEV0M05kUGV6RmlmDQpRY09zTVVhUXZzNC9kMTRIWlZ4VlpwTjUrY2tIbTZIUzdtKzB6VVluSnVkVWdLdVVzN1NpQ0pZNTRvTFg5OU5JSktCdzN3MDIySktxDQpad2FmK2NwdmhmVFhPaUNSbFpHdGc5ODl1Z0pIRUlnRVFPM3hGbkJibHNDcW5pcXFKOC9UL21ERHBiWE9qM2VuNlZiMnNja3QzZlN6DQpNekFMSU9OSW1zN2tFZWtEVlJ2eklvU0YrTlZCNkRmL0FKejNYbFRUcnFTMjBWdFdsSk53dDQ5MUJXRGpTTmlzY1QwbGNqbXc0cUFEDQpUaURpcWFlV3JEOHl6cWEzbm1qVXJCYlZJMlQ5RzZaR3hoa2RxY1pDODZDWk9JL1o1TURpckxjVmRpcnNWZGlyRWZPbW8vbWRaM2tCDQo4cGFSWWFwWkNKMnVVdXB6Qk1aUjlsWTJxRjM5eDlJeFZqczNtbjg0alBhblVmTDF2b21uUlROTGZYYVhjTTQrclFjR2N5dXdaSVkyDQpYbVNRR2Vtd0FQeFlWVFNUemorWTk1YUdiU3ZKbjdtZUF6V2QxUGZ3SVR5bDR4aG9HQ09yTkQrOElZaWgrRTc3NEZVckx6SitZMGNPDQpuV04zcENKcTExSGQzTTBNMDBFOGlKQlQwb2liZHJlSXRJZHVleUxWYWt0MUtxVmw1cy9ORzM4eXJvK3FhTFpYM3IyVTkvSDlSZjBEDQpFc1lLeFJUTkxOTU9jc2xGMkhFYjhXZWpFQlVUZmViZnpYVVhTV2ZrSlhhTlhOcksrcTJwV1FpVUlvS1VRamtoTDdzS2RLMXhWZmMrDQphdnpTL1NOdEZiK1J4OVJNNWp1cmg5U3RTd2hyUVNJZ0s3SHIzUDhBazRxeW5RYjNXTDNUbG4xZlRScE40V1lHekU2WE5GVTBWdlVRDQpLdnhEZW5iRlV4eFZENmpQZFFXTTgxcGJHOHVvMExRMnFzc1prY0RaZWJrS3RmRTRxODd0dk5QNXYyMnJDTFZQTGtMeFhVaml5V0M0DQp0a1F4UXh5eWVqeGFacFByTW9qRkdMK21BZWc0bXBWR2FONXovTW0vY3h5K1VvWTJzMjRYN3Bmd3ZHN3R3L2R3TU9qUjgyYVRsWDdODQpCdTN3cXEwSG1yODBwMXRnM2taTFJwSnlMZ3lhcGJTcWtDRVZiNEFDWGNmWkFxQisxZ1ZiSjVyL0FEV0xlcEY1RlFJQXRJSk5UdGViDQpNM0xrVEl0VlFMUmYyV3JYMnhWWGJ6VjUybHZ6YVFhREhIY1EyTVZ6YzJobmlsY1R6T1U5SVNoNGtBakh4a3NQajRzcS93QXdLcGJwDQpYbUg4NWhiM2k2ajVWamt2bVRuYlNyZDJzVm9yVm9zYXhpV1daandvekYzQUxWQTRpaHdLbUVmblh6bmNTZWxwM2xaZFIrcnl3MnVvDQozRWQvRkRGSE9RVGRLbnFwV1FXL3dnOGExYXEvc2s0cWw5djV4L01iVElyL0FGRHpGNWJrU0l0RkJwMWpCZFdjcGx1Sm1LeHBHVkt1DQpxY3VLTVc1c1NlUVVLQ01Lb21Qek4rYWozVDNKOG5NbHZIQXFwWW5VTE5TODVLK28zcWZHZUkrTDA2OGFqN1Nna1VDcnAvTm41cXRaDQp4RzM4aUxIZFN6dERJSmRVdFhXR01CYVhKQzhmVVg0ajhBWlcyN2JZcXp6RlhZcTdGWG4rcitmdFpYV05YMDdTSW83bWZUMkVkdmFyDQpibWFXVi9STWpWUDFtQTBWbElQQkdvS2R5QWNpT0lVQ2VxTFF2K052UDBXcTNWdGRXZHF0amJ5cEcxNGx1V0tCd3dEenFid0pHdk5DDQpLK29SVGV2YkNNVWFCNy94M0xiTVBLV3VUYXhwa3M4NWhhNHQ3aWEybGEzTll5WW1vQ1BpZWxWSS9hUGpsTTQwVXNPMUR6UjV4R29YDQpaUzVGcFpMcUQyOXNYZ1pZemF4MFV5aVJvWFZoekJGUTVQZ3ZUTFk0NGtmMklRMXA1dDgyWExRQ0xWb1p1VnpjUXpDS0wxR1ZJbmtTDQpOdUtXek44WlZlZ1BYY0wya2NVUi9hRVd5elJQTlU4WGxXenY5ZWh1amZrbUs4aXRiSzZ1SkZsVm1IeFEyMFVycnNLbjRhZmhtTkt1DQpJZ01xMlpIYVhVTjNicGNROHZTa0ZVTG84Wkk4ZUxoVy9EQXFyaXFDdHJyVVo3ZUtZVzhLaVZGY0w2emJjaFduOTFpcUUxWHpGQnBQDQpvL3BLVzB0RGNFaUFTWERBdVZwWGlQUzNwVVlxZ3B2TnVpM01Ed3pTV1UwRWdJa2plUjNSbDdnZ3drRVpMZ1BjcW9mT2VtRHJjV3YvDQpBQ05rL3dDcU9QQWU1WER6bnBoNlhGci9BTWpaUCtxT1BBZTVWTlBOZWlMTzl3ajJRdUpWVkpKaEk0ZGxRbmdyTjZOU0ZMdFR3cWNlDQpBOXlxbitNOU02L1dMWC9rYko4Lzk4NDhCN2xWckx6VFpYZDdCYXd5UVNQT3pLUFRrZG1IR05uclJvMEg3UGppWWtLbmVSVjJLdXhWDQpMSjRUcXRpcVhlbjJ0MWFUaFpQUXVHOVJUMFplU05FeTFHS3ExdkZkVzBDUVc5bmJRd1JqakhGSElVUlI0S29pQUdLcWQxcXpXa3NFDQpWMDFwQkxkTnd0bzVia28wakQ5bEEwWTVIZm9NYlZFZXBxWCsrSWYrUnpmOVVzVmQ2bXBmNzRoLzVITi8xU3hWM3FhbC92aUgva2MzDQovVkxGVmtFZDNCSDZjRnBieFJnc3dSSkdWYXNTekdnaTdzU1RpcnBZN3VVeG1XMHQ1REUzcVJGcEdKVjZGZVMxaTJOR0lxTVZWclNkDQo1NGVib0VjTTZNb1BJVlJ5bXhJWCtYd3hWV3hWNHpkYUo1L3RyaC8wZHBIbW1WUkpHQ0pQTWRsNlRDMzV1cnFabW5rVkpYNHJJS0FsDQpPMVJ4SlY2UjVkc3RZamxhUyt2dFRrU0pUQ3R2cUEwMHBJVzR5ZXNyV1VTeVZTcGlISmw2RWxUc3hDcDlpcVIrVTdhMVMydnA0MFVYDQpFK29YeHVIQUhJbGJ1UkY1SHJzaXJsbVE4dmNxajVWaGdhKzh3WFFETkpKcUR3TVdKSTRRb3RGVUhzSGtmNzhPUTdSSGtnS25sMUlvDQpOWTh3V3NFYVJRSmRReUtpS0ZBYVMwaTViRGJjclg1NEptd1BkK2txd3E5OHFhOWRXckhUOU50THVEVkxHMllYY3lRR1NLVTJpMnJBDQp2SjhhcWlLc3FsQXg1TFRvVGwrTEpBQVgwL0g3UGNnMmszbUhSTkxnMUMzMUZZckpCRmRYRnBaUTNWMnRuRzl6QmZYUENDU041WUZsDQpWa2xSZ0tQOWdEanhZMVlaWUhZa2ord2ZxKzFKRDBMeTk1ZTBlNTBaOU0xQzJ0ZFV0YlZyZU5CTkhIUEN4WFRvSVM2aGd5L0VqTU51DQp4STZITWFaczJsazF2Ylc5dGJ4MjF0RWtGdkNvamhoalVJaUlvb3FxcTBBQUhRRElxcVlxZzlPbWhUVHJRTzZxVERHQUNRRDlnWkdVDQo0Zzdsa0lrOGdrR3Q2em9NZm1SYmZWcFlGZ2pzMU1IcnhtU05ubmxJa0ZhY2VRRUtkKytJSko5Skg0cnpUVkRjS0ZucFBraWFTUzRoDQpPblBia0s4WDFlVXh5cFRkdVRpVHg5bHAwT1dYazd2eDhtTlI3MHd0dkwrbjNMU0czMUcrU0tOdUt4UlhzaFFBME5CeFp1bTRHUmpJDQo3Mk92NkFtUUFwaVBsM1RibTkwNmFlOXVOVzFkeE5iTkhicGRicDY2L0d3RXNrYThVNUh2V25TdVdaTnFvZHpFZWFFL01qVXRJOGkyDQpzZHpPYjI2Z25DUlEyNnl4cTNxdVhKTFROSEl5cUVSdWdKclFiRGNWNU1uREc2czJtTWJLY2FWYjJXcCtSWnRlMDZYVjlORTFtMTNEDQpGTmNTeG5rWUJLckFCdmlVZlo1ZnRVMjJvY2tKV0FhRy92UVFpdkoxb0ZGbExkYWxkM2wzSGRYRWFMZFhMeVZVUnlEbHdZMHF0ZU5hDQpaUElOelEyUUdjNVVsMkt1eFZMRzFDSFRmTFIxR2Y4QXViS3krc1MwL2xpaTV0K0F4VjVoNUV0TkU5T1pmTVV0emNYU3d3OG5NbDQ5DQpaaEpPa3BwR2RxK212VVpjYm9JUnZtRzE4b3JxV2xmVURjSkNKVmE2SE84V3FmV0xkRHM1QlB3U09LTHZ2Z0ViQnV1WGtrR3VTWjJFDQpIaytUMWYwalp2YmxTQkdZWjlSbTVpbnhFajA0Z3UvUUN1VjR3ZUVjdnNUUG1VQjVOdGZKMG5scXprMVUzRXQrM3Flcy9xWHJrL3ZYDQo0N3hzVit5QjB5Y3Blb2dJNGRyZDlXOG1uenA2VmJuOUZmbzdsNmZPOXA5WTlmN1hHdlA3SGZwa3QrSHA5aUZQemphK1VrMDZFNlNiDQptT2YxSGFRK3BlcjhDVzBzbldSZ1B0S3Bwakc5N3JsNUt6WHlSZSt0NWN0SW5sYVo3ZHA3UlptNUZwRXRKM3Qxa1lrVnF5eGdtdTllDQp1K1k1bUxycXo0VFZwdFlPZ2djRmdDSnB5Ulh0NnovMHlURkVvNnVvWkNHVTdoZ2FnNHE4WmpsMGxiYVcwdS9NWG1hMkZtRGZTV2R4DQpydWlyUFBBaERzdnJSWFJtU0lzbEMzcXgwNUVjZ3RLRldlZVZMMjN1TCtTV3kwM1ZCWnlxNFhVN25Vb2I2ellDUXlEMDBXL3UyVXR6DQorRWlJZkRSZGdBTUNvcS8wN3pYTjVnU2EyMUI0TkdQcHJMYnJMQVBoQUxPeXh0WXlTY2l3VmY4QWVtbkV0VGl3R0pDdCtVR29tdEtUDQo4TWVxM1FXdllFcTM2MnljaHNQZCtrcmJYbHNlbExBU0NHMUN3aHVuYithWU96VHNmY200WERQN2loVThzRXRkYTlJMjduVTNVc2V2DQpGSVlnb3I0QWRNY25UM0tGYXh1L3FQbHBwZVBxZm95S2FLbGVQcWZVaTBWZWg0OC9TcjNwWHZsYVVEYitYTE9SNXRJdVpaM2hpdDdkDQp5OEZ4Y1dwWXlRdGFPRzlDU09vNFd3SXFUUW5iQ3FhYVVvVzYxUlJXaTNTQVZKSjJ0SU9wTzV3S21PS3V4VkxyTnJvVytsckhHalc1DQppSHJ5TTVWMUlpK0RpZ1JnMWQ2MVlVOThqTG1QZitnc284aTg3MSsxMS9WUE9GeGNhSGRSVzl6YXpMOVhlY3VZaUdVMjhpU0JEVUQxDQo3VWNXNHR4WWcwSU5Ea1l4NlQrUGY5akJOeDVHMVMva2oxZnpocWNCbnNPVTBFZHVxQzNRRGx5TjA3cEd0d3ZwOGZ0UnJ4SUo4S1ZTDQpqRVNKamZ4WldhcFI4cWVVTkkxTFdMdlhiaUwxYmVSVERhMi9wckRCNmRWOU9VUXFxOEpHQ0Y2MSt5NHlaQmlFSWp5bDVNRGFWY2pWDQpMRjdUVUZkWTRPY3JENFV0NGxVbjZ0TlIwRWl0VDR2dXd6bjNGQ0pzL0p1bS9wQmJQVVY5ZVNYVHl0NTZNdHpIREp5azRzQkcwMGg0DQpFYmNTeEdSQlBDazgxVnZLK2tYRjFyVURXN1hEckdnaEUwc3NwRFBFZThqc2R6a3BFOElRa1BsanlkZlczbVhTL01GN0ZOWnpzODBEDQpXc2pSRlNwdGZoZGxReUVPeEQvdGRCdU1PUWczUlVJblR2eXc4dzI4enlYdm02OTFKV21hUlZsbDFHSGhHeXNQVFg2dGZ3QS9GeE5YDQpEZE5nSzF4eTVCS3FIRFNnTXkwYXkxYXpoRUY5ZHczVVVjY2NjQmppbVNRRkJSMmtrbnVMcDVPVzFLbXZpV3lsS1k0cXhielQ2citSDQprdElXVVNhaXRucDlINkZMeVdLM2tIUmovZHlOMEczWENGVWRKMG5YdEwxSFVMMkcwRE5xVWdsdVFaQklGb3pOU0pXa1dnUHFNYVY2DQo1YVpSSUEzMlExclVWenFkeFpUM2VsWFMzR215Q2FBUXlsVlorU3RSK0N5S1Y1SXJVNUhCUW9qaXEvSklLWXI1cXRiZU5JcmkwdW8zDQpXaTBhTWlvOFFYOU1rYmRlT1JoaG9VQ21Vck5zWThtYXRaMnVpYWZiV3MxK2tVUklFQWpobGFSV21sa29Td1o5K1lCcDRVSFd1R1lCDQp5RVhHNlRSNFFVNTlhZHZNQTF5UFNycHJsclQ2bDhUU0pINmZxZXJ2RzBRUFBsdFhKVUtxL3NZSWZ6VkJxT3J3V2RuZlc2V2dsblJyDQpTRnA0NDVYbVZINVJBcktmVUhwRnFnRG9EMndDY1k5L2Q4MGdXaWZLY04rdXE2eEZPNndQQmZHYVcxaFA3dGhkV3NEOHZqVjJvWmZVDQpiNFdIeFZHNHltVjhXM0w3V1Fxdk5qWG5ieWQ1aHVvdFgxVFMzVzZhK1NTempzWTlQc2JpZFEwa3FTTVpMdVNFT25GdnNGNmV4eVRGDQpQUHk3c2ZPbHBwOXBiYWo2RnBvOW5FYmEzc0piUkliNmtYd3h5TTl0ZFRXcWdnZlpTTWZSZ1Y1bm8vNTgrYkxjUEJjV0IxR3BZeFhNDQo0azlTa2NZV3JMYldjRWFCbjRscTEzYzBOQUZ5eWVNeDUvZmFBYmVxK1IvTVBuTFZsTW5tTFNMYlRJcFloTFpOYjNEVG1RQWdQeURJDQpoU25JVThjclM2Nzh2ZWZuMU9XN3RQTmkyOXI5WWtsZzArVFQ0Wm92UWFJS2tNakJvcGp3a0hQa3JnbXBCN1VTb1dXWWVMU2ZOcFZ5DQpESGMzYmJBVUxmVlkySm9RZXBPVDRUY2R6OW5lZkpOanVUMHhKRnJWaEVnb2tkbmNxbzY3TEpiZ1lPaUVGNVRZTkpycmpvZFZuQS8yDQpLUm9meFhKWk9udUNBNlgvQUpSUFYvOEF0NS84bnBzZ2xIeC9Ecjl4WGYxTFNDbE42ZW5KTlhsVDdOZlVIR3ZXaHAwT0JYYUgvdkhLDQplNXU3dXArVjFJUDFERlV3eFYyS29MVDRpK24yWjVzdkdLTWdDblhnQjRlK1FsQ3p6TEtNcTZQT3ZLbWs2eHF1cGFycWR0Zm1PeXZGDQpSamJLWTFjU3pmdlpENmp4WE93azVBY1ZXbEtqTWlQcEFzc1NXUlcyaEpyTnhjMitwWDk1ZHgyRXF3eVc5eEc4Y1JkUXNpc3JlbGJRDQpULzYzcHNCNDFvUUJPanNGSVpaYjI4TnZDc01LOEkwK3l2WHJ1U1NkeVNkeVQxeUJOcXFZRlNQVXRNMDdVOVplejFDM2p1cldXeUlrDQpnbEhKV0Jrb2FxZGoxeDRiRnBFa05adHFVRjFxNDB6VDRtZU13eDJsdkxNWUltUkZaQTNOSTVpcThnYWdJYVVPeE8yU2xHZ0tVbTBmDQpydDNkMnVtcGRwR2h1NEZsbFdHcFpESWx0S3dYa2VCSTVDbGR2b3lLRWpsODErWmJhYTF0YjNUVEJlM2pNbHRBUHFQNzEwVXN3aTU2DQpqR3owVVZQdzRxdTBYemxxR28zK2pRQ09OazFHR1NlOFgwMlJyWlFKQkVHWVNTcXpTdkMvSDJSajJ4U3pERkRHTDRwSmNlVUxRL0VUDQpPYmgwN0ZJTEdiNGo0OFpaSXlQZmZ0aXFHdkl0WHV2UE4xcHlheE5iV3hzSWJ1M3Rvd3RGcEswVWxEc1RXZytXV3hBNGJJUWp4NVoxDQppbSt2M1JPMjlCNC9QQnhSN2xTd2ZwYlR2TmNOcCttSjd4SU5PdWI2NnRKVlhpOUdTT0hmYy9hNW5ZakpjSUl1cTNwVXJrdE5WTno1DQpVdHhxWDFXMm1rdkk0MmppVUZHRVA3c2NuTE56SkVueEx4MmJqVHVZUXhEaUpObWgrbG5LZDlLU0FlWHYrY29kNjZ2NWE5djN1cGY5DQpVOEhGNU1FL2dzUFAxalllWGsxL1c0MzF5NzFHT0srdHJBdTFxOEt2Skt3ak13RW45d2c1R2dvMWFkc0pnSnhPMzR0bEdWRzJSNlZBDQpiSHpwcVVFdHhMY3ZlMkZ2UEZKTjZmS2tWeGNoMC9kckdLUnJQR3ExRmFkU1R2bFlqU2sybGR4NU04eDNubUNiV3JQekxQYVdwRThVDQplanNzN1dvazlSd0pXRU56Ymx0OTZDbnp5N3hCd2NOYjk3R3QwKzBiUTlUMHIwb1lyOVpyTGt6WENYSDF5Nm5ZbGVLK25QZFhjN0lCDQpRRWlqRHIwSnJsU1hpdHQ1Ui9NU0piZVMyc0JBQUZNaG10NUxoMkhFZ3g4UzhTS0hyUnVTdCtCck9SSFJoSGk2dlkvSzE1ZDNTUUpjDQphVkxwVFdjVWtKaWRBc1JyNlRLWVNHWWxlcS9GUTFCMnBRbUxOa1dCV0pRZjhjbnpqL3pFM2Y4QTFDUlpiMWorT3BRbjAvOEF4MzdQDQovbUZ1ditUbHZrQnlTbHZrN3ByZi9iV3Uvd0JhNUxKeitBKzVBWFMvOG9ucS93RDI4LzhBazlOa0VwaEIvd0FkKzgvNWhiWC9BSk9YDQpHQlhhSC92RkovekZYbi9VVkxpcVB4VjJLcFBlVHlXL2xDZWVJMGxpMDluUStETENTT252aXFYZmw1REFOSm5uZ1hoSE5PL3BvS1VFDQpaWXl4N0R2eGxwOUF5M0pzQVBKQVpUbFNYWXEwNExLUUdLa2dnTUtWSHVLZ2pGV0ZmVU5kMC96QnAwUXZyalZic1F2Nmw1ZHRIR3JCDQp1UnBKSGJKYlI4UlQ0ZmdZajN5M0NCd3l2eVRJc3EwclQyc3JZcEpJWlo1R01rMGg3c3hxYWUxU1QvQWRNak9WbENDODJLN2FQTXNiDQpjSE1kd0VlbGFINnJOUTA3MHlDdlB2UEZwcTFwcjJsMytxUzJPbzN1azI5MWZXdW9McDRFMEN4UmxuNHJMZVJSTlFKeStKdGp1TWxXDQoxcEFUdjh1L0wxM3BXaGFQYzZpQ05WMU81TnpkS3k4V2pRMnN3Z2c0NzhQVGpweVdwbzVmeHlLcy93QVVNY3QvK1VpOHYvOEFiSHZmDQorVHRoaXFYZWIzZXc4MTZQcXR1Q1p4YjNTU29Pc3NVWEJ6Q3ZpWDVuaVA1Z015TU80TVQxUVVaNjJvS3A4MUV5QzM0MU5nU3hINk9wDQp5NStrQi9mL0FPN09sYWZCOG1oOUgyK2Y2dW4ycWxta0EzZXMrWk5TdUZQMXY2aEVoajJiMDFsRXNnaElvUGlTTVJoaC9OWERQYmhBDQpWdnpIWnRMNU8wRzRqbDlENnZkV2QyOXdhbjBqS0NPZnVGZVlIZnRneG1wbjRxbU1UNnZyNkxmdzg3RjlQNUMyaGNzaVMzaVZTWDFLDQpING9oOFVZNjFxVzdEQ2VHRzNPL3UvV3FBZ3VadGE4N2FSZWxXaFMwaXUyUzBrMmtqRWFpM2xkbE8veFR5bEFlNFNveG1PR0ZlZjQvDQpIbXFmWGY4QXltbWxmOXMzVWY4QWsvWTVqSlRIVC83aC93RGpOUDhBOG5ueFZFNHE3RlhZcTdGV0tXcXMrbCtiMVVWWnJxNkFIdWJTDQpMTGVzZngxS0U1YVJKZGFzSll6VkhzN2xsUFNvTWx1UjF5QTVKUUhrN3ByZi9iV3Uvd0JhNUxKeitBKzVBWFMvOG9ucS93RDI4LzhBDQprOU5rRXBoQi93QWQrOC81aGJYL0FKT1hHQlhhSC92RkovekZYbi9VVkxpcVB4VjJLcEpmamw1WnRZajltWTJVTC82c3MwVWJmOEt4DQp4VlM4aG9vOHIyVHF4SmxqamRsTkNWUHBxdmJ4cHlIc2NzeWMwQmtHVnBkaXFqZUxldERTemVPT2JrUGltUm5Yalg0dGxaRFduVGZGDQpVa3VvNUc4eldFZHd3WnpBUXp4aG85K01sZVB4TXkvOEY5T1NqRzRtMDJuTmhZeFdOcXR0SEpMS3FsbTlTNGxrbmtKZGl6VmVSbWJxDQpkaDBVYkFBQURJb1FQbWYvQUk1ai93Q3BQLzFDeTRxODg4OTZmbzl2NTAwZlM3T3pqRVVrYjN0NXB0cXFKSmRrT2ZUaHBzbjcxNC8zDQpqUFJmU1dUa2FESmJjUG1teXkydzBxNWwxRFNkZTFlUVQ2cGNTbjBZbDNnczRwTFdVbUczcUFUV2c5U1EvRTUvbFVLcXhReXpGVWlUDQpTOVdUVmRPdkJIYm1PeHNyaXpZZXE0WmpNOXV3WWZ1dW4rakd2enhWWDFYVEYxTzI5SFViRzJtaVU4bExUT3BRMG9XVjFqREthZHdjDQpJTmNsWTkvaDJGTGcyLzZhbVcwM0xhWitreUZvUlVxVzlMMXVGTnlPWDA1UHhUNWZKRkptbGphMkdpejJtbVc5amJwZVJ1STJOMHdFDQpqeUtWVm1sTWJzMWY1dHprZU0zYVYyaldWL1A1ZGkwL1VvOVAxS05hb3p3eU1ZSFZKQ1lxSXlQeEtnTCswYUViWUNiTnFnejVKbmlxDQptblhkM3B0czFPZHRiMzBoU283cVpZWkdVOU9oNlpQeFQxM1JTWmFUNWZnMHFXU2F6c0lGdVpoU1c1a3VKWlptQnBVR1I0MmFoNGcwDQpHMlJsSW5tbFVtc3RXazE2ejFJUjI0anRyVzZ0Mmo5VitSYTRrdDNVajkxMEgxYzErZVJWTWJLS2FLRGpNRkVoZVJ5RUpZRG5JekRjDQpoZXg4TVZWOFZkaXJzVlF1b3k2bEhBcmFkYlJYVTVrUlhqbm1hQlJHV283OGxqbUpLamNMeDM4UmlyRzd3YW5vMWg1Z211WW92cWQxDQpKTGRHODlSdU1TUEFrWkxScXJ5SGlVcjhJeXlFaFF2bVAxcWZKUzBEemJvK3UvbzY3OHRYRWVxcEJaenhPdjcyM1lnU1FvV1gxWXdODQptaklJTkQ0WkVWVzZwMzVaMDI5c29MNXJ0VlNXOXZaN3NSbzNQZ3NwRkZMVUZUdDJ3emtDZGxDWFg1MXUzMHZVN0ZyS05iYVQ2NDUxDQpGcGg2U3hUUEpMeU1hcTB0VlI2RUJmdGUyK0RaQ0cwZnpwWWE3ckdxWFBsZG85WWpzUkZZNmpDRExiU3hUeFNTbjRmWGpTTjErSTdxDQozYkFLNnBaSm9zRjFEWUFYU0NPZVNXZVpvMWJsdzlhWjVRcGJvU29laHB0WHBpVlIyQlhZcWhqcGVtSHJhUW4vQUo1cC9URlh6WmQvDQptVitjTnRPeUpwV2xpRXUzcGM5QzFkS2dTbFZSWEVKV2hpVUVQUWpjL1BGVWEzNWpmbS9iM2h0cm5TZEZNYk82aVdQUmRla2FNTTlGDQo1S3RvT1lUcHNCenFLTjN4Vk5QSmY1amVkclRXcDRmUE9rVzF6cElpVjB1TkswWFdCS2hjY2xiaWJONDNXbjJ2aldtLzJpS1lxenpXDQpQTTlyZitTTmExYnlwWUNQVUxGT05xMnBhYmRSeG1Tb3IrNkVYcXlBQ3U2S2ZIcHZpcnlmVC96Qy9PbWZuTkxwZWpCVWtQQlYwTFd3DQpaRjlHU1RpZ2EzRGIrbUtkL2lHeHFCaXFmMkg1d2VabWUyaHV2SjluSVdCV2FhTzMxZU1OSkZReThGT21PQUdCK0NyOXh2OEFzNHE5DQprMFMzYWZUSWJpLzAyMnNyeWRHTXR0RFdSVlJ5ZUtsNUlyZHlTbE9RTVlvYWozeFZCYXQrWC9sTFZaUkxkV0pTUVJtRVBhelQyWjRNDQo2dWY5NXBJdCtTRGZyVGJvU01WdE5yUFNOTHMrQnRiU0tKNDE0TElxRG5TbE4zUHhIM0pPS292RlhZcWdkZTAzOUthSHFPbWZCL3AxDQpyTmJmdkF4VDk5R1UrSUl5UHgrTGZpd1BnUmlyNUlzdnloL05IVDlSamlYOHVkSnZXNUZZcmk3bW5tdHVCNUlwbmplOGZseEFCSEpqDQp3cnQzeFY3Ym9mNVcyMXpmdzIvbUg4dmZLOXRwRWNMb0piU1o1NVVZR29DeFBheExSM1pqOXNFRHJ2c1ZYbytoZVhOQzh2MlRXT2lXDQpFT25XYlNOTTF2Ym9JME1qMDVOeEhjMEdLc0Y4N2ZrcC9panpEY2F3UE4rdmFTTGtSQjdDd3V2U3QxOUtQMDZxbE5pdzNQdVQ0NHFoDQp0Ry9JM1VkR3VoZFdIbi96Q3N3ajlJZXJMQlBHRkpVc2ZTbWprakxOd0h4TXB4VjZwaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyDQpzVmRpcnNWZGlyc1ZkaXFEMW5XTERSdE11TlQxQjJqc3JWZWM3cEhKTXdXb0cwY1N2STNYOWxUaXJENS96MS9LbURTcE5WbTE1VXNvDQpyZzJjaE52ZGVvSmxYa1Y5SDB2VjJIN1hHbnZocFcyL1BQOEFLeGJxenRUcmcrc1g4Wm10WWhiWFpMSUFUVnFSZkJzcDJhaHhwVTM4DQpxZm1ONU84MlN5UmVYNzlyMW9ZMG1rUDFlNGlVSklxc3A1U3h4clVxNE5LMXdVckpNVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkDQppcnNWZGlyc1ZXVHV5UXlPb0JaVkxLRHNDUUs3NUdaSWlTR1VCWkFXcTEwVkhLTkExTndISjMvNERJZ3o3aDgvMkpJajNuNWZ0UU9rDQo2NUZxcXp0YVJTaGJlUXhTRzRndXJVRmxORHcrc1F4ZW90UjlwS2ozdzNMdUh6L1l0Ujd6OHYycWtDNitMNlZwNUxSN0FnZWhESEhJDQpreXR5TmVjaGQxWWNhRFpCdnY3WTNMdUh6L1lpbzkvMmZ0UmZLNC9rVC9nei93QTA0M0x1SHovWW1vOTUrWDdXbWE2Q25qR2hhbXdMDQprYi84QmdKbjNENS9zVUNQZWZsKzFkQzdQREc3QUJtVUVnZEtrVnlVRGNRU2lZb2tMOGt4ZGlyR3ZPbmxXMjErT3ppbG1uaG1TYXR0DQpKQzhhK200amVycUpZcmxPWERrdGVGYUU3NHF4UlB5U3N1VHROZjNjN3lIOTYwajJGWFJsQ1BHM0hUbHFycXFodTlGRytHMVQ4ZVd4DQpwRnRiVzFxMXhGRExNSTFqaXZwSTFEeXNXWitLUkt0UzFXTkJ1Y1VJL3dEdzVlLzh0VjEvM0VKLytxZUtWQzgwZTd0WVZrYTR1MkRTDQp4UlVHb3pkWnBWakIvdSt4ZXVLcS93RGh5OS81YXJyL0FMaUUvd0QxVHhWRDZocEYzWjJrbHkxeGR1c2ZHcWpVWmdmaVlML3Z2M3hWDQpqLzhBakQ4dnVuK09kUHIvQU9CREhpclV2bS95T0kyK3IrY0xPNnVLSDBiV0hYMGFXVjZmREhHb0JMTXgyQUhmRkRLRzh2WG9JSDFtDQo3SlBocUUzYjV4NHBVNTlFdklZSnBUY1haOUpDNVg5SXpiZ0FuL2ZaOE1WWkRaMm90YlpJQkpKS0VyKzhtY3lPYWtuZGp1ZXVCVmJGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTB5cTZsVzNWZ1FSN0hBUllwSU5HMk95YzcxLzBVZEx1cmFJMjdCZFZab0JBek5HMFlVY0ptDQpuTDArUGVPZzhRMjJZMGNVYWh0K0tjZzVaWExmOFdsdmsveTBKUExOZzA5d0x1WXhrTmMzQ3lOSzlDdzVPVWxSYS9JRE5qa2tBYXI3DQp2MU9LOHYxNy9uSWI4czlCMTNVZEZ1OU8xR1M3MHU2bXM3aDRiWkdqTWx2STBibEMxK3BLa3J0VURJY1k3dnUvVXFVYWYvemxEK1hVDQppeWkrMGUvaEN1RmdhS0lURjA0cWVUMXZJdURjdVE0amw0MTNvQkdkQUQ5WDZreU5sTWZMdjU4L2wzNWs4NDZUcGRpdXFXNzM4Z3NvDQpyYVMwZ0ZxODA3QUpKSzR1NUoxNG5wd2FuaU1QR0wvcy9VcjFUem41WHA1WTFBd1hBdEorQytuY1FMS3NpTnlHNkZwWkY2LzVKeVVDDQpKR2lQdS9VdkpsVnZwOXRidGJ4aGVSaWhNZkp0eWVQRVZQdjc1aGVGRVRHM1Evb2J2RWtZbmZyK3RHNWUwdXhWRFhmOS9aZjhaai95DQpaa3hWRGFsYitaSkhjNlpmV2RxaFJCR3R6WnkzSkRoanpZbExtM3FDdEFCUVVPOVQwRVNEMC9IMnRzRGpIMUFuM0d2OTZWbXFpWVE2DQpZSm1WNWhkUUNSMFVvcGFocVZVbHlvSjdjajg4a0dzMWV5Qjh3NmhyMXBlQkxTOWloaG5DcEFuNkh2OEFVU3NoL2FrbHRaNDBDMFJ1DQpvV2xSVStNU0pkQ1B4OFcyRXNkZW9TSjhwQWY3MG8vVWpNZEt0ek82dktibXpMT3FORURXNmkvWVpuWmZrVGtoZlZxbFY3SzJvdzYzDQpJeWZvMjd0clpSL2VDNXRwTGduWS9aS1QyL0hlbmprWkE5R2NERCtJRSs0MStnb1hWMHZFOHVTTGVTeHpYUUNlckxERzBNYkgxQjltDQpObm1LL3dEQm5ERytyQ1ZYdHkvSHVlVmVhZnlXL3dDY2YvTFl0bnYvQUN4OEYyWFdLUTZzMXN2cUl2Sll4OWMxQzE1TS9SZUZmOHFnDQozd0UxMHRuanhpWE9Rajc3L1FDdjBmOEFKZjhBSmE2MEdiek5wUGxpU3h2TEUzRWxxMDE5UE15WEZrN0RsKzV1N21CK01zZjh4RzIrDQpHSnZwU01zQkUwQ0plNzl0UFhkVTB5RFVZMXQ1NUpVam8zTDBaR2pMQTBCVnVKRlJ2MHdzRU1OTmcwN1I3dTFnZVY0bGdiajYwalNFDQpBSVFGQlltaWdEcGlxYllxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGVU5IYlF5eFc3dUNXalVGQ0NSUWxhVkZENEhLWTR4SVJKDQo2QnRsTWdrRHFrZmtpNDR4NnBwekZpMWhmVFJKeTMvZGNxUjlQWWIvQUhucm1abWp0RStUVWt1cWZrTCtVbXFhbGRhbGYrWG81NzYrDQpta3VMcVl6WElMeXlzWGRpRmxBRldZbllaU3FHL3dDaGN2eVcvd0NwWmkvNlNMci9BS3E0cWpkRC9JdjhwOUMxZTIxZlMvTHNFR28yDQpiK3BiVHRKUEx3ZWhBWUxMSTZjaFdxbW14M0crS3ByNXp1RWViUjlNVTFtdXI2SnBFQklQb0thU0dvNmJOOU8rWDRSdEkrU3NpYi9lDQpoUDhBVWY4QVd1WXArb2U0L29aajZUN3grbFV5YkIyS29UVUc0Q0NibkducFNjdjNyK21wcWpMVGxSdC9pOE1WU3ErMUhXM3VZV3NiDQo3U29MWmEvV0lwekpOSSt4cHdkWGhDYjA2bzJLcWwvZUM1RnVmWHNrOUNkSnQ3bXRlSnB4K3dPdGNLcWN1cGEyWkNZcjNTbGlMR2l1DQowaFlMdlRjTUFUMDdERlhUWGQxUGFMRmRYZW5seFBES0pJNWpHdElwVWtDOFdEN3NVcFd2ZnBpcnRYbXU3NkJJclhWMDBwMWJrWjdTDQo0dG5kaFFqaVJkV3R5bE42N0tEdDFwZ1ZaZlhSYlJUYXozdHRLVVJCSmR5VHEwamxDQ1dLUlF4cVdhblJGRy9RWVZTUFYvekVuanU1DQpvYks0U0FSUXNTSk5KMWU4SHFFQW95eXd4eEk2aXU2THVmRVVPS3J0SDgwWDJyYVZkV1dwWDFwSk5lUVRlbGNDeDFEU280NHdQVGIxDQpGdlJMOFZYcW9McVdGU0FRQ2NWVFh6T0x6VjlKZXl0TDZ5c3BtZEc5YzNWeUtCVFVnRzBsc1poWDJscDRnNUV4dG5ESVlteFh4QVAzDQp0YVQra0xiUW0wKyt2dFB1cmx4S0RjeFRUb3RKQ1NQaHVKTHlYYmwzbFBnS0RiQ0JUR1VyTnNvVmxaUXlrRlNLZ2pjRUhGRGVLdXhWDQoyS3V4Vkt0Y3Z0Y3R6QW1qMnRwZHlNeGE2VzZ1bXRpa0s5WFFMRk56UGJmaVBmRFNxdWk2NXArcjJmMWl6bVdaVVlSeWxBNFVTRkZrDQpvcGRVTEtWY01yVW9WSUl5VThaaWFLQVV3eUNYWXE3RlhZcWhOUTB5Qys5UDFaYmlNeEdxL1Y3aWFDdTRKRGVreWN2czkvZnhPS3E5DQp0L3ZQRi9xTCtySVkvcEh1WjVQcVB2WWc2RFQvQURScVdveGN1VUxSR2FNQUV0YnlyVnlDZC9oa1ltZ0kzSXJzTXpZK3FBaitMYTJYDQp0YzI2MjV1VEl2b0JmVTlVR3E4YVZxQ091MlkxRzZTeFp2MHg5Yi94UnpmNnNGRVEwNEVjVGFFOGpMMTQ4eDEvSGx4b0JrK211RDdmDQpOREtWdUlHZ0Z3SFgwQ3ZQMUswWGpTdGFuTWFqZEpZamJoNy9BTTEyR29TQmh6OVpvNG4zQXQxVmxqSTY3bDFERWRLaW96Sm42WVVoDQpsVXNqcmUyNkNKM1YwazVTcVY0cFRpUnlxd2Jmb09JT1lSK29lNC9vWmo2VCtPOUVaTmk3RlVOZC93Qi9aZjhBR1kvOG1aTVZZaDUzDQpHdU5ydG5IcDkrYldGb0Fab3hQTEd4Q3lFTXlScHN4K0lWSk8yMmFUdEtVeGsybEtQcEhMM2xJTEp0Yi9BT1BEL21NaC9qbTdRcTNXDQp1NkphWERXMTFxRnRiM0txc2pRU3pSbzRSeVFyRldJTkNWTkQ3WkV6aU9aYkk0cGtXQVNQY29hbmMyOTFwZHRjMjBxVDI4MXhaU1F6DQpSc0hSMGE2aUtzckxVRUVkQ01rRGJDVVNEUjVxMnFhamVXVVJlMjB1NjFOZ0FSRmF0Ykt4cVFLZjZUTmJyc04rdjQ3WW9RMnN6U1RlDQpYSG1rZ2Uya2tTSjN0cFNoa2pMTXBLT1kya1RrdlE4V0k4Q2NWWDZqNW4wUFRaR2p2cm4wQ3JCQ1dTVGp5WmVRWGtGNGs4ZCt1Uk1nDQpPYktNRExrTFFsN3IyazZwNWUxQjdDZjE0NUxHYWFLUUk0UjR6R2FNak1BckQ0aDBPRUVIa2lVU0RSNXNDL05QekgrYjFsK1pIbFhTDQovS0tXNDBLNzR2ZitxMXN2ck1zanZjUnpOTDZrc2NTVzBQSU5HZ083VUxOUlFtUUczVkloSWl3Tm1VNkorWitoYXJxUWdOMXB0bGJ2DQpGV09PZlZiS1MrYWJacUxiV3IzTVJpOU1rOC9YNVZIMktmRmxjYzBUMSswT1RsME9XSFFrZC9ETDlJREt0TS80NXRwL3hoai9BT0lqDQpMWEVST0t1eFYyS3V4VjVKNS9mOHJaL09zMm5hOWRYMm42cEphYzd5NnQyOU8zOUFKeXJJOUdQU0plMU52RGxYUHc1TXNNZkVLNGJZDQpHaWE2czY4cnorVU5Xc2JlL3dCRHRsTnRieVNOYnp2YXkyN0xLeGFLWmg2OGNiK29lTEs1NitQWE1LVWlUWlpzaHlLdXhWMkt1eFYyDQpLcWR0L3ZQRi9xTCtySVkvcEh1WjVQcVB2U1dkSUI1dE1VeUF4WHRnWStOTm1aWEpZTjgwL1ZtUkUramJvV0NBdDRMKzZ2NVBMczZTDQpEVDlQWlpudTJOUFdqZGkwY2RRRjZVL1pvQjJweDN0SkFIR09aUXl3S29YaUFBb0ZBdmFuaFRNVkxFN3UyMUsydjA4djJ5c05PMUpwDQpKbHVBcEloalVocFl0Z1YrUGwrMVFlNVpzeWhLSkhFZnFIMnJTWTJxQSthNVZpWGhCWTJTMi9BZlpIcU9IWDViTFFmTEtwSDBqektvDQovVko3bUMybm50WXZYdW9yYVo0SWR6emtVQXF1Mis1MnlnRDFqOGR6SWZTZmgrbGdlamVlUHpkdWJTS1M0OGhxN1NlcjZqZlgwc3pHDQpVM1ZURE9yc2VmN0xLMUs5UUJ2bVJtaEdNcWliSGV3aVNSdTlCc0xxZTVoTWs5bkxaT0haZlJuTVRNUXBvSEJoa2xYaTNVVk5mRURLDQpVckwrV09LU3prbGRVUVRHck1RQUt3eURxY1ZlUitmdGF1ZFQ4ME8rbXNaTGFDRVdhVzl4YmF2NlVzNlNua3g0V1VscndQcWJTY21CDQpBclduVFg2dlM1TWtyaWEvSDQ2dCtPT0lqMUVnKzYvOThQdWVnSHpKcG1yV0duWGNNaGovQU5MaTlXT1pKcmRsSzFEZkJjeHdTOGE5DQpHS0N1YkFOQnJvalg4NCtYMHZKN1F6U21XMzRtUmhiWERSSGtLamhLSXpISjc4R05PK0txVjNyMms2aHBjTTl0UFJXdWJOZ3N5UEJJDQpBTGlKeVRITUVjVVhyVWJmUmlxbDVvMUR5UkxhR1BYcmFIVkxZQUgwRFp2cUlvWFgvZGNVVS83UVU5TzFlMkt0MzJxYUxjZVdBYkNSDQpJcmQ0NHZxOXVVTnU2b0hYaXZvU0NONDZEOWtxS1lWUUhtdnpUb2RqY3JGRllRNmxmeVJ0TEl6Mjk0OFhwcEc1SCtrV3RuZkp6NUlGDQo0R2pVTzFUeFZvbStqUEdJbjZpUjdoZjZRc3QvTVdnWDJpNnpEQmJwWTM4VnRPa3lyYjNNRWJBcTRUMHByaTN0Qk5XbFNFQnA3N0VzDQpiNnNaVmUzTDhlOUxmTVBsalNJSUpiK2J6dDVqaGhTNCtzZWxwOTBibHdXS29rYVF3d1R5dEd0UHM4U055V3IxeXJ3UE9YemMyR3VyDQovSjR6dC9OL2F5N3kvcVdpcm8xbkZiMzhzMFVNU1JyUHFEU0xkUHhVRG5OOVlFY2hkdXJFcU44dEFvVTRjNThVaWRoZmR5VERUQVJwDQoxcUQxOUdQL0FJaU1MRkU0cTdGWFlxN0ZWRTJWbWJrWFJnak4wbzRyT1VYMUFPbEExSzk4TnFyWUZkaXJzVmRpcnNWYVplU2xha1ZCDQpGUnNSWHd3RVdFZzBWS08zYU9OVVdaK0tBS3RlSk5CdDFLNUNPT2hRSit6OVRNenMzUVFPcTZGOWZhR1ZieWUzdXJjT0lKNCtBSzg2DQpjcWpqdlhpTXR4eU1mUDNzQ2I2SmZlSHpYREZKYlNJdW9RR09odUxkemIzQUoyRENsS1VJclJhbkpFeE5nYkg3RkE1VzNhZWFOTXRyDQpXS3ppZ3ZHdUlGRVMya29rbXVhSUFBN21Sbm1ldmRtcTNjNUx3dk1VaTFGYmZ6bGZhaTk0c3E2ZGJNZ1NLQ1lSUytudUN6eDBqV1FsDQo2ZkVIWWUyUU1ZaVFQUG15RXZTUW5HazZPdW50TkswOGx4Y1hIRDFwSkNUOWl2RUNwWnYydjJtT0dVN1lveG5UNjNHbkljL1RjOGE3DQowNUx2VEtDUnhnZVIvUXpBUENUNWo5S3JsakIyS29lOGFYbGJ4eHVZL1ZrS002aFNRQkc3N2NndzZyNFlxMTlVbi81Ylp2dWgvd0NxDQplS29UVVRlVzMxYjA3eVUrdmNSd3R5V0xaV3JXbEVHKzJLc0svTnp6enIza3kyMHg5TlkzTDN6eXJJOHlSdWtZaVVNS2hCR2ZpNVVyDQpYcjI4TVhVNXpDcXJmOW42MVRueXg1bnV2TVBrN1QvTVVMeTJwdnBraStyTVlaZUlONTlXYjR2U2pxYUFrYlpmaW54UnRVWEY1aGhiDQp6QmRhUzE4d1czalZsbUVsdXptU2tqU3h0Q0l1Y2ZwcEdyY20yUExiSmNRUmJSMWk5a3NydWVPYVNKb1hwQUdhM2s1eGgxVG0zQ09pDQo4cWtxT1IrR2hORFZRZ2dwVytkdGJsOHQ2TU5TazFKYmVFU3JITE5kK2lrU3F5c2QyNG9CdUIxT1krcXl6aEVHSXMyMjRvQ1IzUnJ6DQozNDh0dHFZdTVQckFzemNoZU1YRG42WE9sT0ZhVjk4dXhTNG9nbnFHRXhSSVc2cHJOdHBrdnAzTStxU05SVFcxMDZlN1g0K1ZQaXRyDQpXVmYyRFhmYmF2VVZKbUIvWVd6SGdsUGxYeGxFZmVRdFRWa3ViZVpyVzQxQkpJNEdtWDZ6WlNXNjdJSEFKbnRveFg0aDhOYTlSMUJvDQppWVA5aFl6eEdQT3ZtRDl4VC9KTmJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXFBMWJYdEYwaU9OOVR2WWJUMW00UUpJNER5DQp1ZGdrU2Zia2NuWUtnSko2REZWRmRPOHNIWGwxSVcxb05mbGdBVzZaSXhlR0RmWU1SNm9URlVScU4zcGRoRUo3emlvZHVLQVJtU1IzDQpOVHhSRURPN1VCTkZHUU9PSjZCbHhrZFZJNnI1Y1ZyTkd1clJaTlEyc1kyZU5YbUk2aUpTYXVSM3BqNFVlNEo4U1hlVVM1MDFKR2pjDQp3cklpZW82SGlHQ1ZweUkvbHFPdVBoeDdndmlTN3lxL1ZyYi9BSDBuL0FqSHc0OXdYeEpkNVZNbXdkaXFHdS83K3kvNHpIL2t6SmlxDQpGdnRKdjdtN2FlSFdyMnlpS0tvdFlFczJqQkRWTGd6Vzhzbkpoc2ZqcFRvQWQ4Z1lrOVQ5bjZtMk9TSUZHSVArbS9RVm1xUnZGQnBrDQpieXRPNlhVQ3ROSUZEdVFDQ3pCRlJLdDFQRlFQQVpNTmNqWmVmL245NVgxZlhOTDB4OU9zWnIvNnE4L09HM0RPNVoxUW9QVEJDbXZBDQo3djhBQ0RUTUxWeGxjVEg4ZnArVENRVGp5RG91b2FKK1YraGFacU1MUVhrRTl1WllucnlIcWFpSkY1Vlp5Q1ZjR2pHbzc3NWtZQVJIDQpmWktRUmFoNStsL01yVnJlWFNOUWk4dFFKZG0ydXVNVFc5dzZ3UHhRUi9WL1crTm4ySWwzS2pzMURBOCtYZjNzcVRUUjdxOHVZTlVlDQpXeHVyT05WRVpGemF5Mnk4MGxqSDdzeVJ4OHdhMURBa0hzY2NIRmU0VXNnODlTckxaUjI0UzUvY3pMSk0wTnJkVC9BWTNVY1BRdHJyDQptM0poc0tVNms3VU1OWERqalE1L0Z5Tk5FM2UzemlQdks2TzdzVDVRdk5PdElKNEYwL1RXaDRTMmx4YlJnSkU4WVdNelJRS3dIcG43DQpQYWhvQVJsK0Ura0E4d1BQOUxYbHhrRzl0L01IN2ltZXBhUGM2aWtTWEYweUNHUlpVTnM5MWFrc3ZRT2JlNGlMcjRvMVZQY1pZMUtCDQowdTYwL1FwN1NDNTlTR09HV2pYQnVMbVUxVW40cHJpZVdSdjlreHhWTzhWZGlyc1ZTL3pCcnRsb1drejZuZThqQkJ4QlZPSVptZGdxDQpxdk5rV3BKN3NNVllaYi9udDVGWVBIZUMrMCsrQ3lTUTZmUGF2SmNTeHhmYWRGdHZyQ2djcXJSMlZxZzFBeFZFWG41M2ZseGFKcUhQDQpVbmU0MHNoTHkyUzJ1R2RaRzVVUU1JK0RieHQ4UWJpS2JtbUt0MmY1eCtUVGJTUzZqZGZVeWc1cVZqdUpvNUk2UkhuR3doVXVCOVlqDQpEVVdnSnBVNGFWZDVWL09EeWY1cTh3RFNkRXVmWERRTlBETTZUeE5LRllxeGpSNGg4Q2tFTVhaVFg5a2dnNEZaeGlyc1ZkaXJzVmRpDQpySHRmL0wveWo1Z3V4ZDZ4WW03bkNoVkxUVHFxaFF5cVZWSFZWSTlScUVDdUtwRGQva3g1TlZMYTMwclQwc3JabVdMVUdFdHd6dmFLDQpqZ3d4aG5ZQXljK0ROMUNNd0c1eFZIeWZsRCtYa2wzRGR2cE5ibTNkNUlaZnJGeUdWcEhaM3BTWG96T2ZoNmR1bUtxVTM1TS9sdE1YDQpNbWtGdVVjY1FIMW03QVJJbFZJeEVCTFNMaXFBRGhURlZXWDhwUElNbG9MVTZkSUlWQTlNTGQzWUtNb1VCMVlTMURqMHg4WFd0ZjVqDQpWVmtPaGFEcFdoYWJIcHVsd21DempaM1ZHa2tsWXRJeGQyYVNWbmRpV1BWbU9Lby9GWFlxbGV0WFR4UENxMnQ1TXlreUxMYUpHM0U4DQpTbEc5UStEZUdLc1R0dENzN2NValR6VzN4ckorODFHNWwzU3RCKzh1VytIZmRlaDdnNFZUQ0lHS0dPRmJYWFhTT2Y2eXBtTVU3YzZrDQovYm1hUnVPK3kxb08xTVZTaldmSjNsYld0WWoxalU5QTFXZlVJdVBHVGpHaUVweTRNOFNPc2Jzdk04V1pTUjJ5Qnh4UFFOc2RSa2lLDQpFcEFlOUY2SG9tazZGcFkwdlNOSjFlMXMvV2p1Q25HR1Jta2hLY0Mwa3JPN1VFU0x1ZWdwaEVRT1RHZVNVemNpVDcxUFcvTGZsL1c3DQoxYjNVOUMxT2E3WDB5WmxqZ2laL1NZUEg2bnBzbnFjR1g0ZWRhYjA2bkFZUlBNQmxIUGtpS0VpQjcyOUQ4djZIb09qUG8ramFMcWxqDQpZeU56bFdPSzNaM2FvUEo1SEx5T2RxVlpqdHRoakVEa2pKbW5QZVJNdmViUW5tZnlWNVU4MFhzVjdyMmc2dGZYRVJRb1g0S3A5T3ZBDQpOR2tpb3lyeWFnWUVmRTM4eHJKclZORDhvZVY5QXQ1NE5GOHY2bHA2WE1CdHAyaGl0K1R4c0FEeVptWXMyMzJqdmlxYmF3cDFhM1NDDQo2dC9NRWFJL3FBMmNvc25xQVJ2SmF5UXV3Myt5VFRGWFdLbXkwNXRQaHQ5ZmtnY09DMXk2WGN2eDlmM3R5ODBueUhMYkZXVldkeWJtDQoyU1l3eVc1ZXY3bVlCWEZDUnVBV0c5SzljQ3EyS3V4VjJLcVY1YmZXYlNlMjlWNGZYamFQMW9pRmtUbXBYa2hJSURDdFJ0aXEyd3NiDQpUVDdLQ3hzNHhEYTIwYXhRUkwwVkVGRkgzWXFyNHE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3DQpGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcThaMW4vQUp5ZThxNlA1bzFMUmJuVHJtNGcwK1o3WVh0b3l0emtqb0hCam0rcjhlTDhsSjVIDQpvS1ZCcUppQklSWVZILzV5aDhsSkF0dytqYTBMWnp4anVQUXQvU1k3L1pmNnh4UDJUME9QaGxIRUUyL0xmODh0STg4Ni9McEZ2cDB0DQpneVc3WE1NbHhLaE1uQjFVcXFLUEJxOWVnd1NqU1FYcG1SU3hUOHpmekF0UElubGs2M2NXeHZHYWVPMnQ3WVA2WWVTU3JHcjhYNGdJDQpqTjlrOUtZWWl5Z2xnRnIvQU01VitSM1dNWGVtYWhDN3F0ZlRGdktvYW54ZkVab3pRTjBOTi9BWlB3eWppQ0lsL3dDY3B2eTVqWUQ2DQpycWJWQUpQcFFBQ3ZqeW5CL0RCNFpYaUQxK0NlRzRnam5nZFpJWlZEeFNLYXF5c0txd0k2Z2pJTWwrS3V4VjJLdXhWMkt1eFYyS3V4DQpWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3FHb1hrVmpZWE43TFgwcldKNXBLZGVNYWxqK0F4VitkTTkyOGtvDQp1V1p6SzNJdTVZazFlcGFuekxObDdXaHhNVkxnZnRIY2lnSTJyVUVlT0t2UnZ5RTFZMkg1bjZGT3RLejNCdFhVMUlwY0kwUTkvd0RkDQpnSTN5TXVTUnpmYitWTTN6L3dEODViWEpGaDVac3VSQ1R6WGNyRHNURXNTaXYvSTA1WmpZeWZPNmg2amt4Q3VRVlVrdHVEeVBjdCt6DQp2WExtRGxTa2FPcXNvY1Via1R2SlFjcUVVcGlyN0kvSWZXMzFiOHJkRmVWaTA5bWoyTXRlMzFkeWtZLzVGY014NWpkc0QwRElwZGlyDQpzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWWS84QW1KSVl2eS84elNBVkthVGZNQnYxDQpGczU3YjRSelV2ejdjRkNhSGJxUmw3V3BrTXFvNVdpbXZId1lEWTc1RkxLZnlza2RQeks4cW5zZFdzVjhOamN4akdYSlErLzhwWnZuDQpQL25MTkpSZmVXWExVajlPN0VkQzFlYXRDVzlodFRmTE1iR1R3RzRtbFJvQ05tWFpPRkFkanlydHVTS0VjdXVXbGdHMVlHSUJSdVFLDQpwUUViVi9hNjRxK3B2K2NXR2MvbDNlcXhydzFXWUFlQU52QWYxbktaODJjWHNlUVpPeFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWDQoyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMktzYy9Nbi93QWwzNW8vN1pOOS93QlF6NFJ6UVh3QkpGOFpJNmRRUjQ5alhMbURLUE5mDQpsNXJEeVA1TDFJS1F1cVEzN0ZqWHJGZHNnRmZzazhlSjI4Y0hWbDBVZnl1Mi9NZnlxZW4rNWl3SGJ2ZFI0SmNrQitnR1ZNM3pyL3psDQpxYTNQbFpCOXJoZmtlMjl2bG1QbXhrOG8vTExSSU5iOCs2RnAwdjd5R2U1VnBRRlAyVVZwSDdNSzhZemxrdVRHUE5qbHphUzJOekxhDQp5cXlUVzhoUmxJSUNsQ1ZOUHBYQ2d2cDMvbkZZcVBJZXB4aGdXWFZaR0svdEFOYlFVSkZlOU1wbnpaeGV6NUJrN0ZYWXE3RlhZcTdGDQpYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxeDM4eDZmOHE4ODBWNmZvaS9yVC9tR2t3eDVvTDRGWUhrDQphZEJ2dWZhdENjdmEzdGY1MWFISllmbEIrV2hJQU1GczRrb0FRSHU0WXB5S2cwNnFmbmtJOHl6UEo1aitXY1JINWtlVkRXbE5ZMCtvDQo2LzhBSDFHY011VEVGOS81UzJQbC93RDV5d3ZwNWZOK2k2ZUFQU3R0UGFkVDBQSzRtZEdxYTlLUURMTVlZeVNIL25HNjBhZjgwN0p3DQpLaXpndVptNmJjb2pGMC81NlpLZkpFVWsvT25UQnBYNW4rWWJWR0JFdHdicXRPTkJkS3R3YWZJeThjTVRzcGVxL3dET0pNNU1QbWVBDQo3QldzblhmYjRoT0Qvd0FSeXVhWXZvVElNbllxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYWXE3RlhZcTdGWFlxN0ZYDQpZcTdGV0EvbnplUzJuNVMrWVpZblpHYUtHRWxXS2twTmNSeE9LanN5T1FSM0cyU2p6UWVUNGhTSm5rUlVCTHVRQnQxSlA0NWUxdnF2DQovbkpqUjB0ZnlqMG0zVlZiOUdYbHBGenBVaEZ0NUl0aWQ2RThhNVRBN3M1Y256TDVRdm4wM3pabzJvS28vd0JEdjdhNENzYUNzY3l0DQp1ZTNUTER5WWgraGVVTmo1Qy81eWExRjdyOHo1NFBoSDFDMXQ3WmFEc3krdnZ2MXJQbGtHSlQzL0FKeFBzU2ZOdXMzbERTQ3c5Q3BODQpmNzJkR0E2LzhWWXpXS0UvNXlsMGhMZno5YVgwY2ZFYWhZUmwzM28wc01qeHQveVQ0REREa3NsLy9PTFY3SEI1K3ZiWm00ZlhOT2tFDQphL3pTUnl4dFFmN0RrY0UxaStxTXJaT3hWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMkt1eFYyS3V4VjJLdXhWMkt1eFYyDQpLdkxQK2NsdFRoc3Z5b3ZvSDVjOVF1TGEyaDQwKzBzb25OYW4rU0J1bVNnTjBTNVBsSHlQcHcxTHpsb2Vuay83MVg5dEExVHRSNWxVDQpuNkFhNWNXQWZYUC9BRGtUWnBjL2xIclRFVmUzTnROSFhzUmN4cVQvQU1DeHltSE5uTGsrS0ZjcTZ1UHRLd1lmUWRqbHpXL1I1V0RLDQpHWGNNS2creHpIYlh4ZCtkMTJ0LythUG1DWUx4NHpyQVJVdFg2dkVzRmEwSCsrNjB5MlBKZ2ViMVgvbkV2VDVJdE04eDNwUWlPYWEyDQpoUjZVRllra1pnRC9BTTlSa1psa0ZmOEE1eXQwYjF0QzBQVmxBLzBhNWx0SElIeEVYQ0NSZTNRZlZ6OStNRVNlVWZrTGVKWi9teG9UDQp6TUlrbWFhRnU5V2x0cEZSZS9WeXVTbHlSRjltNVV6ZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWZGlyc1ZkaXJzVmRpcnNWDQpkaXJzVmRpcnNWWVgrYS81Y256L0FPWElOSEdvZm8wd1hhWFluOUwxK1hDT1NQaHg1eDAvdmExcmtveXBCRnZLL3dBdlArY2JQTXZsDQpyejVwV3RhamVXTjVwZGpNOHpyRTh3bTVMRzNvc0VhTUx0SnhKK1A3OG5LWUlRSXZhL1Bta1hHc2VTZGUwdTFoVzR1N3l3dVlyV0ZpDQpvRFR0RTNwQ3IwVUhuU2hQVEt4elpGOHVhYi96aXgrWjk0dks1K29hZHhOQWx4Y0YyUHVQUVNZZmpscG1HSEMrdE5GdHJ1MTBleHRyDQp4eEplUVc4VWR6SXJGZzBpSUZkZ3pCU1FXSFVqS1did1B6aC96amo1dzFmekxxMnIyV3BXSHA2aGR6M0tKTTh5dXF6U0Z3cHBGSU51DQpWT3VURWtVOUovSlR5TnJIa3p5bGNhWnEvcGZYSjcyVzVwQTVkUWhTT05keUY2K2xXbnY5R1JrYlVCTXZ6UzhreWVjL0oxeG9rRWtjDQpONDBzTTFyY1RjaWtieHlEazFGM05ZaTZqNTRnMGtzSThtLzg0MDZEb09xV2VyWHVzM1Y5ZjJGekhkVzNvcEhiUTFpWU9xdWpldXpDDQpvM280eVJtZ0I3SmtFdXhWLzlrPTwveG1wR0ltZzppbWFnZT4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJPC9yZGY6QWx0Pg0KCQkJPC94bXA6VGh1bWJuYWlscz4NCgkJPC9yZGY6RGVzY3JpcHRpb24+DQoJCTxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1sbnM6c3RNZnM9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9NYW5pZmVzdEl0ZW0jIj4NCgkJCTx4bXBNTTpEb2N1bWVudElEPnhtcC5kaWQ6OUZCNzQ3QjA4QTIzRTQxMUE5QkFBN0Y0MjNCRUUyNzI8L3htcE1NOkRvY3VtZW50SUQ+DQoJCQk8eG1wTU06SW5zdGFuY2VJRD54bXAuaWlkOjlGQjc0N0IwOEEyM0U0MTFBOUJBQTdGNDIzQkVFMjcyPC94bXBNTTpJbnN0YW5jZUlEPg0KCQkJPHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD51dWlkOkRCQjE3RTU2Qzc5NUUxMTFBOUZBRjNBNDFFRjdBQkQxPC94bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ+DQoJCQk8eG1wTU06UmVuZGl0aW9uQ2xhc3M+cHJvb2Y6cGRmPC94bXBNTTpSZW5kaXRpb25DbGFzcz4NCgkJCTx4bXBNTTpEZXJpdmVkRnJvbSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJPHN0UmVmOmluc3RhbmNlSUQ+dXVpZDo5MGY5ZmZmYy0yNWZlLTRlNjItOWVhMy1lMzNlMDBkOGEwODE8L3N0UmVmOmluc3RhbmNlSUQ+DQoJCQkJPHN0UmVmOmRvY3VtZW50SUQ+eG1wLmRpZDo3NkU0REUxMkU1REJFMzExOEU2MTg1M0IyREFCMDNFMDwvc3RSZWY6ZG9jdW1lbnRJRD4NCgkJCQk8c3RSZWY6b3JpZ2luYWxEb2N1bWVudElEPnV1aWQ6REJCMTdFNTZDNzk1RTExMUE5RkFGM0E0MUVGN0FCRDE8L3N0UmVmOm9yaWdpbmFsRG9jdW1lbnRJRD4NCgkJCQk8c3RSZWY6cmVuZGl0aW9uQ2xhc3M+cHJvb2Y6cGRmPC9zdFJlZjpyZW5kaXRpb25DbGFzcz4NCgkJCTwveG1wTU06RGVyaXZlZEZyb20+DQoJCQk8eG1wTU06SGlzdG9yeT4NCgkJCQk8cmRmOlNlcT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjhFRDFCQkRDQzI5NEUxMTFBMTBCRTc1RDEzRjgyNDVBPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxMi0wNS0wM1QwOTo1NDowNCswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNS4xPC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo5MUQxQkJEQ0MyOTRFMTExQTEwQkU3NUQxM0Y4MjQ1QTwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTItMDUtMDNUMTU6MjY6MTUrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzUuMTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6Q0JDRDg2N0Y4NDk1RTExMUEwNDVBOEMwRDgxOEJDMkY8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDEyLTA1LTA0VDEwOjAyOjE3KzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1LjE8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOkE3REJFNjc1NzhEMEUxMTE5MjRCQzAwMTBDNzBDMDU1PC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxMi0wNy0xOFQwOTozNDo0MCswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNS4xPC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo2MjIzNTk5RUY3RjZFMTExQThEMThGQjAwRURGNDA1Njwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTItMDktMDVUMTQ6MzY6NTUrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzUuMTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6Q0RDRjQ4MjhCRkY3RTExMUIzNDhCQUY1NzFEN0RGRDQ8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDEyLTA5LTA2VDExOjQ5OjUzKzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1LjE8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjkxNUZCODkzMzEwMUUyMTE4NkMwRjc5QUJGQkUyOTRFPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxMi0wOS0xOFQwOTozODo0MSswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNS4xPC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo5MzVGQjg5MzMxMDFFMjExODZDMEY3OUFCRkJFMjk0RTwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTItMDktMThUMTQ6MTM6MTIrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzUuMTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6Q0IxMzA3OUY3NjVERTMxMTkxQTFFMkM2MzBERUUxNUI8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDEzLTEyLTA1VDE0OjQ5OjQ1KzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1PC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDozNjlGMkNEQjlDQTVFMzExQjFEMjlFMzM3NDE3QzZCQTwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTQtMDMtMDdUMTA6MDQ6NDYrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzU8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjM4OUYyQ0RCOUNBNUUzMTFCMUQyOUUzMzc0MTdDNkJBPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxNC0wMy0wN1QxMTo1NzoyOSswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdEV2dDphY3Rpb24+c2F2ZWQ8L3N0RXZ0OmFjdGlvbj4NCgkJCQkJCTxzdEV2dDppbnN0YW5jZUlEPnhtcC5paWQ6QjRFOEREQkYxQUQwRTMxMUIzNTJDMkZEQ0ZFRjAzNEY8L3N0RXZ0Omluc3RhbmNlSUQ+DQoJCQkJCQk8c3RFdnQ6d2hlbj4yMDE0LTA0LTMwVDE1OjA1OjA1KzA4OjAwPC9zdEV2dDp3aGVuPg0KCQkJCQkJPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgSWxsdXN0cmF0b3IgQ1M1PC9zdEV2dDpzb2Z0d2FyZUFnZW50Pg0KCQkJCQkJPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPg0KCQkJCQkJPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDo3NkU0REUxMkU1REJFMzExOEU2MTg1M0IyREFCMDNFMDwvc3RFdnQ6aW5zdGFuY2VJRD4NCgkJCQkJCTxzdEV2dDp3aGVuPjIwMTQtMDUtMTVUMTE6NTc6NDYrMDg6MDA8L3N0RXZ0OndoZW4+DQoJCQkJCQk8c3RFdnQ6c29mdHdhcmVBZ2VudD5BZG9iZSBJbGx1c3RyYXRvciBDUzU8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+DQoJCQkJCQk8c3RFdnQ6Y2hhbmdlZD4vPC9zdEV2dDpjaGFuZ2VkPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RFdnQ6YWN0aW9uPnNhdmVkPC9zdEV2dDphY3Rpb24+DQoJCQkJCQk8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjlGQjc0N0IwOEEyM0U0MTFBOUJBQTdGNDIzQkVFMjcyPC9zdEV2dDppbnN0YW5jZUlEPg0KCQkJCQkJPHN0RXZ0OndoZW4+MjAxNC0wOC0xNFQxNjoxMjowOSswODowMDwvc3RFdnQ6d2hlbj4NCgkJCQkJCTxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIElsbHVzdHJhdG9yIENTNTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4NCgkJCQkJCTxzdEV2dDpjaGFuZ2VkPi88L3N0RXZ0OmNoYW5nZWQ+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCTwvcmRmOlNlcT4NCgkJCTwveG1wTU06SGlzdG9yeT4NCgkJCTx4bXBNTTpNYW5pZmVzdD4NCgkJCQk8cmRmOlNlcT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RNZnM6bGlua0Zvcm0+RW1iZWRCeVJlZmVyZW5jZTwvc3RNZnM6bGlua0Zvcm0+DQoJCQkJCQk8c3RNZnM6cmVmZXJlbmNlIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCQk8c3RSZWY6ZmlsZVBhdGg+RDpcVXNlcnNca3Jpc3RpbmEuc29cRGVza3RvcFxOZXcgZm9sZGVyICgyKVxOZXcgZm9sZGVyXFAxMTUwMTY1LkpQRzwvc3RSZWY6ZmlsZVBhdGg+DQoJCQkJCQk8L3N0TWZzOnJlZmVyZW5jZT4NCgkJCQkJPC9yZGY6bGk+DQoJCQkJCTxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJPHN0TWZzOmxpbmtGb3JtPkVtYmVkQnlSZWZlcmVuY2U8L3N0TWZzOmxpbmtGb3JtPg0KCQkJCQkJPHN0TWZzOnJlZmVyZW5jZSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQkJPHN0UmVmOmZpbGVQYXRoPkQ6XFVzZXJzXGtyaXN0aW5hLnNvXERlc2t0b3BcTmV3IGZvbGRlciAoMilcTmV3IGZvbGRlclxQMTE1MDE2NC5KUEc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5XU1QtMTAxIChHKS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5SVlc4NUVTSS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5CVFdCQy5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD53YWlzdCB0aWNrZXQuanBnPC9zdFJlZjpmaWxlUGF0aD4NCgkJCQkJCTwvc3RNZnM6cmVmZXJlbmNlPg0KCQkJCQk8L3JkZjpsaT4NCgkJCQkJPHJkZjpsaSByZGY6cGFyc2VUeXBlPSJSZXNvdXJjZSI+DQoJCQkJCQk8c3RNZnM6bGlua0Zvcm0+RW1iZWRCeVJlZmVyZW5jZTwvc3RNZnM6bGlua0Zvcm0+DQoJCQkJCQk8c3RNZnM6cmVmZXJlbmNlIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCQk8c3RSZWY6ZmlsZVBhdGg+YWxsIGJvdHRvbXMgaGFuZ3RhZy5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5kOlxVc2Vyc1xNRFNHMTBcRGVza3RvcFxXUkFOR0xFUiBUUklNU1xIQU5HVEFHMi5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5kOlxVc2Vyc1xNRFNHMTBcRGVza3RvcFxXUkFOR0xFUiBUUklNU1xIQU5HVEFHMS5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9ybT4NCgkJCQkJCTxzdE1mczpyZWZlcmVuY2UgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPg0KCQkJCQkJCTxzdFJlZjpmaWxlUGF0aD5CVDE3TENFU0kgQS5CUkFTUy5qcGc8L3N0UmVmOmZpbGVQYXRoPg0KCQkJCQkJPC9zdE1mczpyZWZlcmVuY2U+DQoJCQkJCTwvcmRmOmxpPg0KCQkJCQk8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4NCgkJCQkJCTxzdE1mczpsaW5rRm9ybT5FbWJlZEJ5UmVmZXJlbmNlPC9zdE1mczpsaW5rRm9");
           //}
       }
    </script>
    <!--#endregion-->
</head>
<body style="height: 1150px">
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <form id="form1" runat="server" class="Entry">
           <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" ClientInstanceName="factbox" CloseAction="None" 
        EnableViewState="False" HeaderText="Item info" Height="207px" Width="245px" PopupHorizontalOffset="1085" PopupVerticalOffset="0"
         ShowCloseButton="False" ShowCollapseButton="True" ShowOnPageLoad="True" ShowPinButton="True" ShowShadow="True" Collapsed="true">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="FormLabel" runat="server" Text="Item Masterfile" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
        <dx:ASPxUploadControl ID="gvuploadimage" runat="server" AutoStartUpload="True"  Caption="dsada" ClientInstanceName="CINgvuploadimage" ClientVisible="false" CssClass="uploadControl" OnFileUploadComplete="gvuploadimage_FileUploadComplete" ShowProgressPanel="True" UploadMode="Auto" ShowTextBox="False" ClientSideEvents-FileUploadStart="function(s,e){cp.ShowLoadingPanel();}">
            <ValidationSettings AllowedFileExtensions=".jpg, .jpeg, .gif, .png" MaxFileSize="4194304" MaxFileSizeErrorText="File is too large!">
            <ErrorStyle CssClass="validationMessage" />
            </ValidationSettings>
                <ClientSideEvents 
                    FileUploadComplete="UploadImageComplete"  />
            <ProgressBarStyle CssClass="uploadControlProgressBar" />
        </dx:ASPxUploadControl>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="850px" Height="1000px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="1000px" Width="850px" style="margin-left: -3px" TabIndex="2">
                        <%--<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />--%>
                        <Items>

                          <%--<!--#region Region Header --> --%>
                            <%-- <!--#endregion --> --%>
                            
                          <%--<!--#region Region Details --> --%>
                            
                            <%-- <!--#endregion --> --%>
                            <dx:TabbedLayoutGroup Name="MainGroup">
                                <Items>
                                  <dx:LayoutGroup Caption="Generic Tab" ColCount="2">
                                        <Items>
<%--                                            <dx:LayoutItem Caption="Is Service">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" >
                                                        <dx:ASPxCheckBox ID="chkIsService" runat="server" CheckState="Unchecked">
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>--%>
                                            <dx:LayoutItem Caption="Item Code:" Name="txtitemCode">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtItemCode" runat="server" Width="170px" ReadOnly="False"
                                                           MaxLength="50" ClientInstanceName="txtitemcode">
                                                        <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorImage ToolTip="Supplier is required">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:EmptyLayoutItem>
                                            </dx:EmptyLayoutItem>
                                            <dx:LayoutItem Caption="Item Description" Name="txtitedesc">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtitemdesc" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                           <dx:LayoutItem Caption="Short Description" Name="txtdesc">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtshortdesc" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Item Category:" Name="txtitemcategory">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup Width="170px" ID="txtitemcat" ClientInstanceName="txtitemcat" runat="server" AutoGenerateColumns="False" DataSourceID="ItemCategory" KeyFieldName="ItemCategoryCode" OnLoad="LookupLoad" TextFormatString="{0}">
                                                            <ClientSideEvents ValueChanged="function(s,e){ cp.PerformCallback('itemcat'); e.processOnServer = false; txtprodsubcat.SetText(null);}" />
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                 <Settings ShowFilterRow="True" />
                                                                 </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="ItemCategoryCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                            <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorImage ToolTip="ItemCategory is required">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                              <dx:LayoutItem Caption="Product Category:" Name="txtProdCat">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup Width="170px" ID="txtprodcategory" runat="server" AutoGenerateColumns="False" DataSourceID="ProdCat" KeyFieldName="ProductCategoryCode"
                                                             OnLoad="LookupLoad" TextFormatString="{0}" ClientInstanceName="txtprodcategory">
                                                            <ClientSideEvents ValueChanged="function(s,e){ cp.PerformCallback('prodcat'); e.processOnServer = false; }" />
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                          <Settings ShowFilterRow="True" />
                                                                  </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="ProductCategoryCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="1">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                            <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorImage ToolTip="Supplier is required">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                        <dx:ASPxTextBox runat="server" ClientInstanceName="txtHavProdSub" ID="txtHavProdSub" ClientVisible="false">
                                                            <ClientSideEvents TextChanged="function(){ console.log('here')}" />
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:LayoutItem Caption="Product Sub Category:" Name="txtProdsubcat">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup Width="170px" ID="txtprodsubcat" runat="server" AutoGenerateColumns="False" DataSourceID="ProdSubCat" ClientInstanceName="txtprodsubcat" 
                                                            KeyFieldName="ProductSubCatCode" OnLoad="LookupLoad" TextFormatString="{0}">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                          <Settings ShowFilterRow="True" />
                                                                  </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="ProductSubCatCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="1">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                            <ClientSideEvents Validation="OnValidationProd" />
                                                            <ValidationSettings ErrorText="Product sub category is required">
                                                                <%--<ErrorImage ToolTip="Product sub category is required">
                                                                </ErrorImage>--%>
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Item Customer:" Name="txtitemcustomer">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup Width="170px" ID="txtitemcustomer" runat="server" AutoGenerateColumns="False" DataSourceID="customer" KeyFieldName="BizPartnerCode" OnLoad="LookupLoad" TextFormatString="{0}">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                           <Settings ShowFilterRow="True" />
                                                                 </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                          
                                             <dx:LayoutItem Caption="Item Supplier:" Name="txtitemsupplier">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup Width="170px" ID="txtitemsupplier" runat="server" AutoGenerateColumns="False" DataSourceID="supplier" KeyFieldName="SupplierCode" OnLoad="LookupLoad" TextFormatString="{0}">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                            <Settings ShowFilterRow="True" />
                                                                 </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="SupplierCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Reorder Level" Name="txtReorderlevel">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxSpinEdit ID="txtreorderlevel" runat="server" Width="170px" OnLoad="SpinEdit_Load">
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Unit Bulk" Name="txtunitbulk">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup Width="170px" ID= "txtunitbulk" ClientInstanceName="txtunitbulk" runat="server" AutoGenerateColumns="False" DataSourceID="unit" KeyFieldName="UnitCode" OnLoad="LookupLoad"  TextFormatString="{0}">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                                 <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="UnitCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="1">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                            <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorImage ToolTip="Unit for Bulk is required">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Base Unit" Name="txtbaseunit">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                     <dx:ASPxGridLookup Width="170px" ID= "txtbaseunit" runat="server" AutoGenerateColumns="False" DataSourceID="unit" 
                                                         KeyFieldName="UnitCode" OnLoad="LookupLoad"  TextFormatString="{0}" ClientInstanceName="txtbaseunit">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                            <Settings ShowFilterRow="True" />
                                                                  </GridViewProperties>
                                                                 <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="UnitCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="1">
                                                               <Settings AutoFilterCondition="Contains" />
                                                                     </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                         <ClientSideEvents Validation="OnValidation" />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorImage ToolTip="Base Qty Unit is required">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Item Type">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" >
                                                        <dx:ASPxComboBox ID="cboxItemType" runat="server" DataSourceID="ItemType" Width="170px" ClientInstanceName="cboxItemType" OnLoad="Comboboxload"
                                                                TextField="Description" ValueField="Code" ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True">
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Estimated Cost" Name="txtReorderlevel">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxSpinEdit ID="txtestcost" runat="server" Width="170px" OnLoad="SpinEdit_Load">
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tax Code" Name ="aglTaxCode">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup ID="aglTaxCode" ClientInstanceName="aglTaxCode" runat="server" DataSourceID="sdsTaxCode" 
                                                            Width="170px" KeyFieldName="TCode" OnLoad="LookupLoad" TextFormatString="{0}" AutoGenerateColumns="false">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" AllowSort="false"/>
                                                                <Settings ShowFilterRow="True" />
                                                            </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="TCode" ReadOnly="true" Caption="Tax Code">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="true">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Rate" ReadOnly="true">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Is Core">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" >
                                                        <dx:ASPxCheckBox ID="chkIsCore" runat="server" CheckState="Unchecked" OnLoad="Check_Load">
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Clone">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxSpinEdit ID="SpinClone" runat="server" Width="170px" ClientInstanceName="CINClone" SpinButtons-ShowIncrementButtons="false">
                                                           
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Is ByBulk">
                                             <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxCheckBox runat="server" CheckState="Unchecked" ID="chkIsBulk" OnLoad="Check_Load"></dx:ASPxCheckBox>
                                             </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                            <dx:LayoutItem Caption="Is Senior">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxCheckBox ID="chkIsSenior" runat="server" CheckState="Unchecked" >
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                            <dx:LayoutItem Caption="Is PWD">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxCheckBox ID="chkIsPWD" runat="server" CheckState="Unchecked" >
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                            <dx:LayoutItem Caption="Is BNPC">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxCheckBox ID="chkIsBNPC" runat="server" CheckState="Unchecked" >
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                             <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                            <dx:LayoutItem Caption="Is Inactive" ColSpan="1">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxCheckBox ID="chkIsInactive" runat="server" CheckState="Unchecked" ReadOnly="True">
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutGroup ShowCaption="False" Width="20%">
                                                <GroupBoxStyle>
                                                <Border BorderColor="Transparent"></Border>
                                                </GroupBoxStyle>
                                                        <Items>
                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <div id="embroiderDropZone" class="detailpicture">
                                                                            <dx:ASPxImage ID="ItemImage" runat="server" ClientInstanceName="CINItemImage" Height="200px" ShowLoadingImage="True" Width="300px">
                                                                            </dx:ASPxImage>
                                                                        </div>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                            <dx:LayoutGroup Caption="Item Detail" ColSpan="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv1" KeyFieldName="ItemCode;ColorCode;ClassCode;SizeCode" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gvitem_CommandButtonInitialize" Width="1077px">
                                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" Init="OnInitTrans" />
                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                    </SettingsPager>
                                                                    <SettingsEditing Mode="Batch">
                                                                    </SettingsEditing>
                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                                                                    <SettingsBehavior AllowSort="False" />
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="70px">
                                                                            <CustomButtons>
                                                                                 <dx:GridViewCommandColumnCustomButton ID="Delete">
                                                                        <Image IconID="actions_cancel_16x16"></Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                                                <dx:GridViewCommandColumnCustomButton ID="Details">
                                                                                    <Image IconID="support_info_16x16">
                                                                                    </Image>
                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                                 <dx:GridViewCommandColumnCustomButton ID="CloneButton" Text="Copy">
                                                                                <Image IconID="edit_copy_16x16" ToolTip="Clone"></Image>
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                            </CustomButtons>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="prevColorCode" FieldName="prevColorCode" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="4" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="prevSizeCode" FieldName="prevSizeCode" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="5" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="prevClassCode" FieldName="prevClassCode" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="6" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" Name="ColorCode" ShowInCustomizationForm="True" VisibleIndex="5" Width="80px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="ColorCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="gl2" DataSourceID="color" KeyFieldName="ColorCode" OnLoad="gvLookupLoad" TextFormatString="{0}" Width="80px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents DropDown="lookup" KeyDown="gridLookup_KeyDown" KeyPress="gridLookup_KeyPress" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <%--<dx:GridViewDataTextColumn Caption="Description" FieldName="Description" Name="Description" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                        </dx:GridViewDataTextColumn>--%>
                                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" Name="SizeCode" ShowInCustomizationForm="True" VisibleIndex="5" Width="80px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="SizeCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="gl4" DataSourceID="size" KeyFieldName="SizeCode" OnLoad="gvLookupLoad" TextFormatString="{0}" Width="80px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents DropDown="lookup" KeyDown="gridLookup_KeyDown" KeyPress="gridLookup_KeyPress" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" Name="ClassCode" ShowInCustomizationForm="True" VisibleIndex="5" Width="80px">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="ClassCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="gl3" DataSourceID="class" KeyFieldName="ClassCode" OnLoad="gvLookupLoad" TextFormatString="{0}" Width="80px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents DropDown="lookup" KeyDown="gridLookup_KeyDown" KeyPress="gridLookup_KeyPress" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Barcode" FieldName="Barcode" Name="Barcode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" ReadOnly="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="OnHand" FieldName="OnHand" Name="OnHand" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="8">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="OnOrder" FieldName="OnOrder" Name="OnOrder" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="9">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="OnAlloc" FieldName="OnAlloc" Name="OnAlloc" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="10">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="OnBulkQty" FieldName="OnBulkQty" Name="OnBulkQty" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="11">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="InTransit" FieldName="InTransit" Name="InTransit" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="12">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="StatusCode" FieldName="StatusCode" Name="StatusCode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="13">
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="SizeCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="glStat" DataSourceID="Statussql" KeyFieldName="StatusCode" OnLoad="gvLookupLoad" TextFormatString="{0}" Width="80px">
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="StatusCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents DropDown="lookup" KeyDown="gridLookup_KeyDown" KeyPress="gridLookup_KeyPress" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="BaseUnit" FieldName="BaseUnit" Name="BaseUnit" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="14">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataCheckColumn FieldName="IsInactive" ShowInCustomizationForm="True" VisibleIndex="17">
                                                                            <PropertiesCheckEdit>
                                                                                <ClientSideEvents CheckedChanged="function(s,e){ gv1.batchEditApi.EndEdit(); }" />
                                                                            </PropertiesCheckEdit>
                                                                        </dx:GridViewDataCheckColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field1" FieldName="Field1" Name="Field1" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="18">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field2" FieldName="Field2" Name="Field2" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="19">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field3" FieldName="Field3" Name="Field3" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="20">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field4" FieldName="Field4" Name="Field4" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="21">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field5" FieldName="Field5" Name="Field5" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="22">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field6" FieldName="Field6" Name="Field6" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="23">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field7" FieldName="Field7" Name="Field7" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="24">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field8" FieldName="Field8" Name="Field8" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="25">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Field9" FieldName="Field9" Name="Field9" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="26">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Picture" FieldName="ItemImage" Name="ItemImage" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="27">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataButtonEditColumn Caption="..." FieldName="UploadPhoto" Name="UploadPhoto" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="28">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <DataItemTemplate>
                                                                                <dx:ASPxButton ID="btnUploadPhoto" ClientInstanceName="CINUploadPhoto" runat="server" Width="100%" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" ClientVisible="true" Text=" " Theme="MetropolisBlue" >
                                                                                    <ClientSideEvents Click="UploadImageClick" />
                                                                                </dx:ASPxButton>
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataButtonEditColumn>
                                                                         <dx:GridViewDataTextColumn Caption="" Width="0px" FieldName="AddedDetail" Name="AddedDetail" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="26">
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
                                  <dx:LayoutGroup Name="FabricInfo" Paddings-Padding="0px" Caption="Fabric Information Tab" ColCount="2" ClientVisible="false">
<Paddings Padding="0px"></Paddings>
                                        <Items>
                                            <dx:LayoutGroup Paddings-Padding="0px" ShowCaption="False" GroupBoxStyle-Border-BorderStyle="None">
                                                <Border BorderStyle="None" />

<Paddings Padding="0px"></Paddings>

                                                <GroupBoxStyle>
                                                <Border BorderStyle="None"></Border>
                                                </GroupBoxStyle>
                                                <Items>
<%--                                                    <dx:LayoutItem Caption="Key Supplier">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridLookup ID="txtKeySupp" runat="server" DataSourceID="supplier" TextFormatString="{0}"
                                                                 KeyFieldName="SupplierCode"></dx:ASPxGridLookup>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>--%>
                                                    <dx:LayoutItem Paddings-Padding="0px" Caption="Retail Fabric Code">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtRetailFabCode" runat="server" Width="170px">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>

<Paddings Padding="0px"></Paddings>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Fabric Group">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox runat="server" ID="txtFabGroup" DataSourceID="FabricGroup" Width="170px"
                                                                TextField="Description" ValueField="Code" ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True">
                                                                    <ClientSideEvents ValueChanged="function(){cp.PerformCallback('fabgroup');}" />
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Fabric Design Category">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox runat="server" ID="txtFabDesCat" DataSourceID="FabDesign" Width="170px"
                                                                TextField="Description" ValueField="FabricDesignCode" ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True">
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Dyeing">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox runat="server" ID="txtDye" DataSourceID="Dye" Width="170px"
                                                                TextField="Description" ValueField="DyeingCode" ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True">
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Weave Type" Name="WeaveType">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox runat="server" ID="txtWeave" DataSourceID="Weave" Width="170px"
                                                                TextField="Description" ValueField="Code" ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True">
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Finishing">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxComboBox runat="server" ID="txtFinishing" DataSourceID="Finishing" Width="170px"
                                                                TextField="Description" ValueField="FinishingCode" ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True">
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                               <dx:ASPxGridView Caption="COMPOSITION" ID="gvFab" runat="server" AutoGenerateColumns="False" Width="295px" KeyFieldName="FabricCode;Type"
                                                                    OnCommandButtonInitialize="gvitem_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gvFabric" 
                                                                    OnBatchUpdate="gv1_BatchUpdate" >
                                                                   <SettingsPager Mode="ShowAllRecords"/> 
                                                                    <SettingsBehavior AllowSort ="false" />
                                                                        <Columns>
                                                                          <dx:GridViewCommandColumn ButtonType="Image" ShowDeleteButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="30px">  
                                                                          </dx:GridViewCommandColumn>
                                                                          <dx:GridViewDataSpinEditColumn FieldName="Percentage" VisibleIndex="1" Width="80px"  UnboundType="Decimal" PropertiesSpinEdit-AllowMouseWheel="false">
                                                                                <PropertiesSpinEdit NullDisplayText="0" ConvertEmptyStringToNull="True" NullText="0" DisplayFormatString="{0:N}"
                                                                                    MinValue="0">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                </PropertiesSpinEdit>
                                                                            </dx:GridViewDataSpinEditColumn>
                                                                          <dx:GridViewDataTextColumn FieldName="Description" Caption="Type" VisibleIndex="2" Width="170px" Name="Description" ReadOnly="true">
                                                                                 <EditItemTemplate>
                                                                                   <dx:ASPxGridLookup runat="server" DataSourceID="CompType" TextFormatString="{0}" AutoPostBack="false" AutoGenerateColumns="true"
                                                                                       ClientInstanceName="fabtype" KeyFieldName="Description">
                                                                                       <ClientSideEvents DropDown="lookup" KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown"
                                                                                         ValueChanged="function(){gvFabric.batchEditApi.EndEdit();}"/>
                                                                                   </dx:ASPxGridLookup>
                                                                                </EditItemTemplate>
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="100"/>
                                                                        <ClientSideEvents BatchEditConfirmShowing="OnConfirm"  
                                                                     BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                                                                    <SettingsEditing Mode="Batch" />
                                                                </dx:ASPxGridView>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>               
                                                </Items>
                                            </dx:LayoutGroup>          
                                            <dx:LayoutGroup Paddings-Padding="0px" ShowCaption="False" Height="110px"  GroupBoxStyle-Border-BorderStyle="None">
<Paddings Padding="0px"></Paddings>

                                            <GroupBoxStyle> 
                                            <Border BorderStyle="None"></Border>
                                            </GroupBoxStyle>
                                                <Items>
                                                    <dx:LayoutItem Paddings-Padding="0px" ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <table>
                                                            <tr>
                                                                <td></td>
                                                                <td style="padding-left: 8px">
                                                                    <dx:ASPxLabel runat="server" Text="Cuttable">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td></td>
                                                                <td style="padding-left: 15px">
                                                                    <dx:ASPxLabel runat="server" Text="Gross">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <dx:ASPxLabel runat="server" Text="(For Knits Only)">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:77px"><dx:ASPxLabel runat="server" Text="Width: "></dx:ASPxLabel></td>
                                                                <td><dx:ASPxTextBox runat="server" ID="txtCuttableWidth" Width="60px"></dx:ASPxTextBox></td>
                                                                <td><dx:ASPxLabel runat="server" Font-Size="Smaller" Text="inches"> </dx:ASPxLabel></td>
                                                                <td><dx:ASPxTextBox ID="txtGrossWidth" runat="server" Width="60px"></dx:ASPxTextBox></td>
                                                                <td style="width:30px"><dx:ASPxLabel runat="server" Font-Size="Smaller" Text="inches"></dx:ASPxLabel></td>
                                                                <td><dx:ASPxComboBox runat="server" ID="cbforknits" Width="80px">
                                                                    <Items>
                                                                        <dx:ListEditItem Text="OPEN" Value="OPEN" />
                                                                        <dx:ListEditItem Text="TUBE" Value="TUBE" />
                                                                    </Items>
                                                                </dx:ASPxComboBox></td>
                                                            </tr>
                                                            <tr><td style="height:5px"></td></tr>
                                                            <tr>
                                                                <td style="width:77px"><dx:ASPxLabel runat="server" Text="Weight BW: "></dx:ASPxLabel></td>
                                                                <td><dx:ASPxTextBox ID ="txtCuttableWeightBW" runat="server" Width="60px"></dx:ASPxTextBox></td>
                                                                <td></td>
                                                                <td><dx:ASPxTextBox ID="txtGrossWeightBW" runat="server" Width="60px"></dx:ASPxTextBox></td>
                                                                <td><dx:ASPxLabel runat="server" Text="Yield: "></dx:ASPxLabel></td>
                                                                <td><dx:ASPxTextBox ID="txtYield" runat="server" Width="45px"></dx:ASPxTextBox></td>
                                                            </tr>
                                                            <tr><td style="height:5px"></td></tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td style="width:77px"><dx:ASPxLabel runat="server" Text="Fabric Stretch: "></dx:ASPxLabel></td>
                                                                <td><dx:ASPxTextBox ID="txtFabricStretch" runat="server" Width="60px"></dx:ASPxTextBox></td>
                                                                <td style="width:27.83px; padding-left:2px"><dx:ASPxLabel runat="server" Text="%"> </dx:ASPxLabel></td>
                                                                <td><dx:ASPxLabel runat="server" Text="Use Pull-test w/ Rinse Wash"></dx:ASPxLabel></td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                                &nbsp;
                                                                &nbsp;
                                                                &nbsp;
                                                                &nbsp;
                                                        <table>
                                                            <tr>
                                                                        <td></td>
                                                                        <td style="text-align:center">
                                                                            <dx:ASPxLabel runat="server" Text="Warp">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="text-align:center">
                                                                            <dx:ASPxLabel runat="server" Text="Weft">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:77px"><dx:ASPxLabel runat="server" Text="Construction: "></dx:ASPxLabel></td>
                                                                        <td><dx:ASPxTextBox ID="txtWarpConstruction" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                        <td style="width:40px; text-align:center"><dx:ASPxLabel runat="server" Text="x"> </dx:ASPxLabel></td>
                                                                        <td><dx:ASPxTextBox ID="txtWeftConstruction" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr><td style="height:5px"></td></tr>
                                                                    <tr>
                                                                        <td style="width:77px"><dx:ASPxLabel runat="server" Text="Density: "></dx:ASPxLabel></td>
                                                                        <td><dx:ASPxTextBox ID="txtWarpDensity" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                        <td style="width:40px; text-align:center"><dx:ASPxLabel runat="server" Text="x"> </dx:ASPxLabel></td>
                                                                        <td><dx:ASPxTextBox ID="txtWeftDensity" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:77px"><dx:ASPxLabel runat="server" Text="Shrinkage (Rinse Watch): "></dx:ASPxLabel></td>
                                                                        <td><dx:ASPxTextBox Id="txtWarpShrinkage" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                        <td style="width:40px; text-align:left; padding-left:2px"><dx:ASPxLabel runat="server" Text="%&nbsp;&nbsp;x"> </dx:ASPxLabel></td>
                                                                        <td><dx:ASPxTextBox ID="txtWeftShrinkage" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td colspan="3"><dx:ASPxLabel runat="server" Text="Use 24&quot; x 24&quot; Method, 50 cm X 50 cm Marking"> </dx:ASPxLabel></td>
                                                                    </tr>
                                                             </table>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>

<Paddings Padding="0px"></Paddings>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                            
                                        </Items>
                                    </dx:LayoutGroup>  
                                  <dx:LayoutGroup Caption="Stock Master Info Tab" Name="Stock Master Info" ClientVisible ="false" >
                                        <Items>
                                            <dx:TabbedLayoutGroup ActiveTabIndex="0">
                                                <%--<SettingsTabPages EnableClientSideAPI="True">
                                                </SettingsTabPages>--%>
                                                <Items>
                                                    <dx:LayoutGroup Caption="Stock Info" ColCount="2" Width="1250px" Height="400px">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Brand">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox runat="server" ID="txtBrand" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Gender Code">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox runat="server" ID="txtGender" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Product Group">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox runat="server" ID="txtProductGroup" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Delivery Date">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <table>
                                                                    <tr>
                                                                        <td><dx:ASPxTextBox ID="txtDeliveryYear" runat="server" Width="75px" MaxLength="4"  ReadOnly="true">
                                                                            </dx:ASPxTextBox></td>
                                                                        <td style="padding-left:2px">
                                                                            <dx:ASPxComboBox ID="cboDeliveryMonth" runat="server" Width="93px" DataSourceID="Months"
                                                                                ValueField="Code" TextField="Description" ReadOnly="true">
                                                                            </dx:ASPxComboBox></td>
                                                                    </tr>
                                                                </table>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%--<dx:LayoutItem Caption="Collection Abbreviation">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtCollAbbreviation" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>--%>
                                                            <dx:LayoutItem Caption="PIS Number">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtPISNumber" runat="server" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Fit Code">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtFitCode" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%--<dx:LayoutItem Caption="Color">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtColor" runat="server" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>--%>
                                                            <dx:LayoutItem Caption="Product Design Category">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtProdDesignCat" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%--<dx:LayoutItem Caption="Color Name">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtColorName" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>--%>
                                                            <%--<dx:LayoutItem Caption="Retail Fabric Code">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtRetailFabric" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>--%>
                                                            <dx:LayoutItem Caption="Wash Code">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtWash" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Tint Code">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtTint" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Product Class">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxComboBox runat="server" ID="cbProdClass" ValueType="System.String" OnLoad="Comboboxload">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="New" Value="New" />
                                                                                <dx:ListEditItem Text="Repeat" Value="Repeat" />
                                                                                <dx:ListEditItem Text="Non-Repeat" Value="Non-Repeat" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Imported Item">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtImportedItem" runat="server" Width="170px" onload="TextboxLoad">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Product Sub-Class">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxComboBox runat="server" ID="cbProdSubClass" ValueType="System.String" OnLoad="Comboboxload">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="CORE" Value="CORE" />
                                                                                <dx:ListEditItem Text="IMAGE" Value="IMAGE" />
                                                                                 <dx:ListEditItem Text="REPEAT" Value="REPEAT" />
                                                                                <dx:ListEditItem Text="SEASON" Value="SEASON" />
                                                                               
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Product Alignment">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxComboBox runat="server" ID="cbProdAlign" ValueType="System.String" OnLoad="Comboboxload">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="ADOPT" Value="ADOPT" />
                                                                                <dx:ListEditItem Text="ADAPT" Value="ADAPT" />
                                                                                <dx:ListEditItem Text="CREATIVE" Value="CREATIVE" />
                                                                                <dx:ListEditItem Text="INITIATIVE" Value="INITIATIVE" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Season">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxGridLookup ID="cbSeason" runat="server" DataSourceID="Season" TextFormatString="{0}"
                                                                            AutoGenerateColumns="true" KeyFieldName="Season" OnLoad="LookupLoad">
                                                                            <GridViewProperties>
                                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                            </GridViewProperties>
                                                                        </dx:ASPxGridLookup>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Reco Allocation">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxComboBox runat="server" ID="cbRecon" ValueType="System.String" OnLoad="Comboboxload">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="A" Value="A" />
                                                                                <dx:ListEditItem Text="AB" Value="AB" />
                                                                                <dx:ListEditItem Text="ABC" Value="ABC" />
                                                                                <dx:ListEditItem Text="C-" Value="C-" />
                                                                                <dx:ListEditItem Text="RECONSO" Value="RECONSO" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="SRP">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxSpinEdit ID="txtSRP" runat="server" Number="0" OnLoad="SpinEdit_Load">
                                                                        </dx:ASPxSpinEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                                                            <dx:LayoutGroup Caption="Price History">
                                                                <Items>
                                                                    <dx:LayoutItem ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridView ID="gvStockPriceHistory" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPriceHistory" KeyFieldName="StockNumber;EffectivityDate;Price" OnBatchUpdate="gv1_BatchUpdate" 
                                                                                    OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gvitem_CommandButtonInitialize" Width="492px"
                                                                                    Settings-ShowStatusBar="Hidden">
                                                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" />
                                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="Batch">
                                                                                    </SettingsEditing>
                                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="100" VerticalScrollBarMode="Visible" />
                                                                                    <SettingsBehavior AllowSort="False" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="StockNumber" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0" Width="30px">
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="170px">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Price" FieldName="Price" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="170px">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataDateColumn FieldName="EffectivityDate" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                        </dx:GridViewDataDateColumn>
                                                                                    </Columns>
                                                                                </dx:ASPxGridView>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                              </Items>
                                                           </dx:LayoutGroup>
                                                            <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                                                            <dx:LayoutGroup Caption="Item Image" ColCount="2">
                                                            <Items>
                                                            <dx:LayoutItem Caption="">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxUploadControl Enabled="false" ID="btnFrontUpload" runat="server" AutoStartUpload="True"  Caption="dsada" ClientInstanceName="CINFrontUpload" CssClass="uploadControl" DialogTriggerID="externalDropZone" Name=" " ShowProgressPanel="True" UploadMode="Auto" ShowTextBox="False">
                                                                            <BrowseButton Text="FRONT"></BrowseButton>
                                                                            <BrowseButtonStyle Width="215px" CssClass="BrowseButton"></BrowseButtonStyle>
                                                                            <DropZoneStyle CssClass="uploadControlDropZone" />
                                                                        </dx:ASPxUploadControl>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxUploadControl Enabled="false" ID="btnBackUpload" runat="server" AutoStartUpload="True" Caption="dsada" ClientInstanceName="CINBackUpload" CssClass="uploadControl" DialogTriggerID="externalDropZoneBack" Name=" " ShowProgressPanel="True" UploadMode="Auto" ShowTextBox="False">
                                                                            <BrowseButton Text="BACK"></BrowseButton>
                                                                            <BrowseButtonStyle Width="215px" CssClass="BrowseButton"></BrowseButtonStyle>
                                                                            <DropZoneStyle CssClass="uploadControlDropZone" />
                                                                        </dx:ASPxUploadControl>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem ShowCaption="False" Paddings-PaddingLeft="1px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <div id="externalDropZone" class="dropZoneExternal">
                                                                            <div id="dragZone">
                                                                                <span class="dragZoneText"></span>
                                                                            </div>
                                                                            <dx:ASPxImage ID="FrontImage" runat="server" ClientInstanceName="CINFrontImage" Height="360px" ShowLoadingImage="True" Width="240px" CssClass="ImageRadius">
                                                                            </dx:ASPxImage>
                                                                            <div id="dropZone" class="hidden">
                                                                                <span class="dropZoneText"></span>
                                                                            </div>
                                                                        </div>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem ShowCaption="False" Paddings-PaddingLeft="2.5px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <div id="externalDropZoneBack" class="dropZoneExternal">
                                                                            <div id="dragZoneBack">
                                                                                <span class="dragZoneText"></span>
                                                                            </div>
                                                                            <dx:ASPxImage ID="BackImage" runat="server" ClientInstanceName="CINBackImage" Height="360px" ShowLoadingImage="True" Width="240px" CssClass="ImageRadius">
                                                                            </dx:ASPxImage>
                                                                            <div id="dropZoneBack" class="hidden">
                                                                                <span class="dropZoneText"></span>
                                                                            </div>
                                                                        </div>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            </Items>
                                                            </dx:LayoutGroup>
                                                           <%-- <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxTextBox ID="txtFrontImage64string" ClientInstanceName="CINFrontImage64string" runat="server" Width="250" ClientVisible="false"  ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                        <dx:ASPxTextBox ID="txtBackImage64string" ClientInstanceName="CINBackImage64string" runat="server" Width="250" ClientVisible="false"  ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>--%>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:LayoutGroup Caption="Sell Thru">
                                                        <Items>
                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxGridView ID="gvSellthru" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvSellthru" OnInitNewRow="gvSellthru_InitNewRow" 
                                                                    OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" Width="747px"
                                                                    KeyFieldName="StockNumber;LineNumber">
                                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing"  BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" />
                                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                                    <SettingsBehavior AllowSort ="false" />
                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="130" VerticalScrollBarMode="Auto" />
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="StockNumber" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowNewButtonInHeader="true" ShowInCustomizationForm="True" VisibleIndex="1" Width="60px" ShowDeleteButton="false">
                                                                            <%--<CustomButtons>
                                                                                <dx:GridViewCommandColumnCustomButton ID="RunningINVInfo">
                                                                                    <Image IconID="support_info_16x16">
                                                                                    </Image>
                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                            </CustomButtons>--%>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="LineNumber" FieldName="LineNumber" Name="LineNumber" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="2" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="DateEncoded" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="true" UnboundType="String">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="UserName" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Analysis" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataComboBoxColumn FieldName="ActionPlan" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                            <PropertiesComboBox>
                                                                                <Items>
                                                                                    <dx:ListEditItem Value="For Repeat" />
                                                                                    <dx:ListEditItem Value="For Reconso" />
                                                                                    <dx:ListEditItem Value="For Further Monitoring" />
                                                                                </Items>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                         <dx:GridViewDataComboBoxColumn FieldName="ProductAlignment" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                            <PropertiesComboBox>
                                                                                <Items>
                                                                                    <dx:ListEditItem Value="Repeat" />
                                                                                    <dx:ListEditItem Value="Damage" />
                                                                                    <dx:ListEditItem Value="Season" />
                                                                                    <dx:ListEditItem Value="Core" />
                                                                                    <dx:ListEditItem Value="Limited Fabric" />
                                                                                </Items>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                        <dx:GridViewDataComboBoxColumn FieldName="Remarks" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="8">
                                                                            <PropertiesComboBox>
                                                                                <Items>
                                                                                    <dx:ListEditItem Value="Fast" />
                                                                                    <dx:ListEditItem Value="Mid" />
                                                                                    <dx:ListEditItem Value="Slow" />
                                                                                    <dx:ListEditItem Value="No data to analyze yet" />
                                                                                </Items>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                    </Columns>
                                                                  <SettingsEditing Mode="Batch" />
                                                                </dx:ASPxGridView>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:LayoutGroup Caption="Price Matrix" ColCount="2">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Initial SRP">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtInitialSRP" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Outlet Inventory">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtOutletInventory" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Qty Sold">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtQTYSold" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Warehouse Inventory">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtWarehouseInventory" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Weeks Running">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtWeeksRunning" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Total Inventory">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtTotalInventory" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Updated SRP">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtUpdatedSRP" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Average Daily Sales">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtAverageDailySales" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Planned Buying Cost">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtPlannedBuyingCost" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Average Weekly Sales">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtAverageWeeklySales" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Updated Buying Markup">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtUpdatedBuyingMarkup" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Average Monthly Sales">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtAverageMonthlySales" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Index Price">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtIndexPrice" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="SSR (Days)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtSSRDays" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Average Buying Cost">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtAverageBuyingCost" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="SSR (Weeks)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtSSRWeeks" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Average Selling Markup">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtAverageSellingMarkup" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="SSR (Months)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtSSRMonths" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:EmptyLayoutItem ColSpan="2">
                                                            </dx:EmptyLayoutItem>
                                                            <dx:LayoutItem Caption="Total Sell Thru">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtTotalSellThru" runat="server" Width="170px" ReadOnly="true">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                                          <dx:LayoutGroup Caption="Per Size">
                                                                <Items>
                                                                    <dx:LayoutItem ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer Height="747px" >
                                                                             
                                                                                 <dx:ASPxGridView ID="gvPerSizeReport" runat="server" Settings-VerticalScrollBarMode="Auto" ClientInstanceName="gvPerSizeReport" AutoGenerateColumns="true" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gvitem_CommandButtonInitialize" BatchEditStartEditing="OnStartEditing" Width="747px">
                                                                                 <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" />
                                                                                     <SettingsEditing Mode="Batch" />
                                                                                 <SettingsPager Mode="ShowAllRecords" />
                                                                                 </dx:ASPxGridView>

                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                </Items>
                                                            </dx:LayoutGroup>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:LayoutGroup Caption="Outlet Sales">
                                                        <Items>
                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxGridView ID="gvOutletSales" runat="server" AutoGenerateColumns="True" ClientInstanceName="gvOutletSales" 
                                                                            OnCellEditorInitialize="gv1_CellEditorInitialize" KeyFieldName="OutletCode" OnCommandButtonInitialize="gv_CommandButtonInitialize" Width="905px"
                                                                            DataSourceID="OutletSalessql">
                                                                        <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" BatchEditStartEditing="OnStartEditingOutlet" CustomButtonClick="OnCustomClick"
                                                                            />
                                                                        <SettingsPager Mode="ShowAllRecords"/> 
                                                                        <SettingsBehavior AllowSort ="false" />
                                                                        <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="130" VerticalScrollBarMode="Auto" />
                                                                            <Settings ShowStatusBar="Hidden" />
                                                                        <%--<Columns>
                                                                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0" Width="60px" ShowEditButton="false" ShowDeleteButton="false">
                                                                                <CustomButtons>
                                                                                    <dx:GridViewCommandColumnCustomButton ID="RunningINVInfo">
                                                                                        <Image IconID="support_info_16x16">
                                                                                        </Image>
                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                </CustomButtons>
                                                                            </dx:GridViewCommandColumn>--%>
                                                                            <%--<dx:GridViewDataDateColumn FieldName="OutletCode" ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="OutletName" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="Weeks" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="DR" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="Sales" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="SellThru" ShowInCustomizationForm="True" VisibleIndex="5" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="EndingInv" ShowInCustomizationForm="True" VisibleIndex="6" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="SalesRate" ShowInCustomizationForm="True" VisibleIndex="7" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="StoreClass" ShowInCustomizationForm="True" VisibleIndex="8" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                        </Columns>--%>
                                                                      <SettingsEditing Mode="Batch" />
                                                                    </dx:ASPxGridView>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:LayoutGroup Caption="PO & Price History">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="PO History">
                                                            <Items>
                                                                <dx:LayoutItem ShowCaption="False">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxGridView ID="gvPOHistory" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPOHistory" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" Width="747px">
                                                                            <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" />
                                                                            <SettingsPager Mode="ShowAllRecords"/> 
                                                                            <SettingsBehavior AllowSort ="false" />
                                                                            <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="130" VerticalScrollBarMode="Auto" />
                                                                            <Columns>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="1" Width="60px" ShowEditButton="false" ShowDeleteButton="false">
                                                                                    <%--<CustomButtons>
                                                                                        <dx:GridViewCommandColumnCustomButton ID="RunningINVInfo">
                                                                                            <Image IconID="support_info_16x16">
                                                                                            </Image>
                                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                                    </CustomButtons>--%>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataDateColumn FieldName="PODate" ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn FieldName="AactualDelivery" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn FieldName="LeadTime" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn FieldName="Supplier" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn FieldName="FOBCost" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn FieldName="POQty" ShowInCustomizationForm="True" VisibleIndex="5" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn Caption="Deliv" FieldName="Delivered" ShowInCustomizationForm="True" VisibleIndex="6" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn Caption="+/- Shipment" FieldName="Shipment" ShowInCustomizationForm="True" VisibleIndex="7" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn Caption="%Achieve" FieldName="Achievement" ShowInCustomizationForm="True" VisibleIndex="8" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                                <dx:GridViewDataDateColumn FieldName="CreatedBy" ShowInCustomizationForm="True" VisibleIndex="9" ReadOnly="true">
                                                                                </dx:GridViewDataDateColumn>
                                                                            </Columns>
                                                                          <SettingsEditing Mode="Batch" />
                                                                        </dx:ASPxGridView>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                            </Items>
                                                            </dx:LayoutGroup>
                                                            <dx:LayoutGroup Caption="Price History">
                                                             <Items>
                                                               <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxGridView ID="gvPriceHistory2" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPriceHistory" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" Width="747px">
                                                                        <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" />
                                                                        <SettingsPager Mode="ShowAllRecords"/> 
                                                                        <SettingsBehavior AllowSort ="false" />
                                                                        <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="130" VerticalScrollBarMode="Auto" />
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="1" Width="60px" ShowEditButton="false" ShowDeleteButton="false">
                                                                                <%--<CustomButtons>
                                                                                    <dx:GridViewCommandColumnCustomButton ID="RunningINVInfo">
                                                                                        <Image IconID="support_info_16x16">
                                                                                        </Image>
                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                </CustomButtons>--%>
                                                                            </dx:GridViewCommandColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="EffectivityDate" ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="Status" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="NewSRP" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="PercentChange" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="Markup" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="Remarks" ShowInCustomizationForm="True" VisibleIndex="5" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataDateColumn FieldName="CreatedBy" ShowInCustomizationForm="True" VisibleIndex="6" ReadOnly="true">
                                                                            </dx:GridViewDataDateColumn>
                                                                        </Columns>
                                                                      <SettingsEditing Mode="Batch" />
                                                                    </dx:ASPxGridView>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                             </Items>
                                                            </dx:LayoutGroup>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:TabbedLayoutGroup>
                                        </Items>
                                  </dx:LayoutGroup>                 
                                     <dx:LayoutGroup Caption="WMS Tab" ColCount="2" Name="WMSInfo">
                                        <Items>
                                            <dx:LayoutItem Caption="Storage Type:" Name="txtstoragetype">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <%--<dx:ASPxTextBox ID="txtstoragetype" runat="server" Width="170px" ColCount="1" OnLoad="TextboxLoad">
                                                        
                                                        </dx:ASPxTextBox>--%>
                                                        <dx:ASPxGridLookup Width="170px" ID= "txtstoragetype" runat="server" AutoGenerateColumns="False" DataSourceID="StorageType" KeyFieldName="StorageType" OnLoad="LookupLoad"  TextFormatString="{0}">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                            </GridViewProperties>
                                                                 <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="StorageType" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="StorageDescription" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                               </Columns>
                                                                <ClientSideEvents Validation="function(){isValid=true;}" />
                                                            </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Picking Strategy:" Name="txtpickingStrategy" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxComboBox ID="txtstrategy" runat="server" ValueType="System.String" Width="170px" OnLoad="Comboboxload">
                                                            <Items>
                                                                <dx:ListEditItem Text="FIFO" Value="FIFO" />
                                                                <dx:ListEditItem Text="FEFO" Value="FEFO" />
                                                                <dx:ListEditItem Text="LIFO" Value="LIFO" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Max Level" Name="txtmaxlevel" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtmaxlevel" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Min Order Qty:" Name="txtminorderqty">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" >
                                                        <dx:ASPxTextBox ID="txtminorder" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                              <dx:LayoutItem Caption="Standard Qty:" Name="txtstandardqty">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" >
                                                        <dx:ASPxTextBox ID="txtstandardqty" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Use Count Sheet" Name="chkUseCountSheet" ClientVisible="false">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" >
                                                        <dx:ASPxCheckBox ID="chkcountsheet" runat="server" CheckState="Unchecked" OnLoad ="Check_Load" ClientVisible="false">
                                                        </dx:ASPxCheckBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                           
                                        </Items>
                                    </dx:LayoutGroup>
                                     <dx:LayoutGroup Caption="Running Inventory Information">
                                        <Items>
                                            <dx:LayoutGroup Caption="Lines">
                                                <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridView ID="agvRunningInv" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv4" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" Width="747px">
                                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" />
                                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                                    <SettingsBehavior AllowSort ="false" />
                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="130" VerticalScrollBarMode="Auto" />
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="1" Width="60px" ShowDeleteButton="false">
                                                                            <CustomButtons>
                                                                                <dx:GridViewCommandColumnCustomButton ID="RunningINVInfo">
                                                                                    <Image IconID="support_info_16x16">
                                                                                    </Image>
                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                            </CustomButtons>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="LineNumber" FieldName="LineNumber" Name="LineNumber" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="2" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="ItemCode" FieldName="ItemCode" Name="ItemCode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="3" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        
                                                                        <dx:GridViewDataTextColumn Caption="ColorCode" FieldName="ColorCode" Name="ColorCode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="4" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="SizeCode" FieldName="SizeCode" Name="SizeCode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="5" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="ClassCode" FieldName="ClassCode" Name="ClassCode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="6" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                       
                                                                        <dx:GridViewDataTextColumn Caption="Qty" FieldName="Qty" Name="Qty" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                         <dx:GridViewDataTextColumn Caption="BulkQty" FieldName="BulkQty" Name="BulkQty" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="8" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                         <dx:GridViewDataTextColumn Caption="BaseUnit" FieldName="BaseUnit" Name="BaseUnit" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="9" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="WarehouseCode" FieldName="WarehouseCode" Name="WarehouseCode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="10" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="StatusCode" FieldName="StatusCode" Name="StatusCode" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="11" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="LastMovementDate" FieldName="LastMovementDate" Name="LastMovementDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="12" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                         <dx:GridViewDataTextColumn Caption="FirstIn" FieldName="FirstIn" Name="FirstIn" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="LastIn" FieldName="LastIn" Name="LastIn" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="13" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="FirstOut" FieldName="FirstOut" Name="FirstOut" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="14" ReadOnly ="true">
                                                                        </dx:GridViewDataTextColumn>
                                                                           <dx:GridViewDataTextColumn Caption="LastOut" FieldName="LastOut" Name="LastOut" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="15" ReadOnly ="true">
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
                                     <dx:LayoutGroup Caption="Item Price History">
                                        <Items>
                                            <dx:LayoutGroup Caption="Lines" Width="650px">
                                                <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxGridView ID="agvItemCustomer" runat="server"  AutoGenerateColumns="False" ClientInstanceName="gv2" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gvitem_CommandButtonInitialize" Width="650px"
                                                                  KeyFieldName="ItemCode;ColorCode;ClassCode;SizeCode;Customer" OnInitNewRow="gv1_InitNewRow">
                                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditRowValidating="Grid_BatchEditRowValidating2" CustomButtonClick="OnCustomClick" 
                                                                        BatchEditStartEditing="OnStartEditing2" BatchEditEndEditing="OnEndEditing2"/>
                                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                                    <SettingsBehavior AllowSort ="false" />
                                                                    <Settings ColumnMinWidth="120" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="1" Width="60px" ShowDeleteButton="true">
                                                                            <%--<CustomButtons>
                                                                                <dx:GridViewCommandColumnCustomButton ID="Itempricehisto">
                                                                                    <Image IconID="support_info_16x16">
                                                                                    </Image>
                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                            </CustomButtons>--%>
                                                                        </dx:GridViewCommandColumn>
                                                                        
                                                                        <dx:GridViewDataTextColumn Caption="ItemCode" FieldName="ItemCode" Name="ItemCode" ShowInCustomizationForm="True" Width="0px" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" VisibleIndex="2" Width="150px" Name="ColorCode">
                                                                             <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="ColorCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                    DataSourceID="Color" KeyFieldName="ColorCode" ClientInstanceName="glColor" TextFormatString="{0}" Width="150px" OnLoad="gvLookupLoad" >
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                            AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                       <dx:GridViewDataTextColumn FieldName="SizeCode" VisibleIndex="3" Width="150px" Name="SizeCode">
                                                                         <EditItemTemplate>
                                                                            <dx:ASPxGridLookup ID="SizeCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                DataSourceID="size" KeyFieldName="SizeCode" ClientInstanceName="glSize" TextFormatString="{0}" Width="150px" OnLoad="gvLookupLoad" >
                                                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                        AllowSelectSingleRowOnly="True" />
                                                                                </GridViewProperties>
                                                                                <Columns>
                                                                                    <dx:GridViewDataTextColumn FieldName="SizeCode" ReadOnly="True" VisibleIndex="0" />
                                                                                    <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                </Columns>
                                                                                <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" ValueChanged="gridLookup_CloseUp" />
                                                                            </dx:ASPxGridLookup>
                                                                        </EditItemTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" VisibleIndex="4" Width="150px" Name="ClassCode">
                                                                             <EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="ClassCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                    DataSourceID="class" KeyFieldName="ClassCode" ClientInstanceName="glClass" TextFormatString="{0}" Width="150px" OnLoad="gvLookupLoad" >
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                            AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Customer" FieldName="Customer" Width="150px" Name="Customer" ShowInCustomizationForm="True" VisibleIndex="9">
                                                                        <EditItemTemplate>
                                                                        <dx:ASPxGridLookup ID="gvCustomer" runat="server" Width="150px" AutoGenerateColumns="False" DataSourceID="sdsBizPartner" KeyFieldName="BizPartnerCode" OnLoad="LookupLoad" TextFormatString="{0}"
                                                                         ClientInstanceName="gvCustomer">
                                                                            <GridViewProperties Settings-ShowFilterRow="true">
                                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                                <Settings ShowFilterRow="True"></Settings>  
                                                                            </GridViewProperties>
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <ClientSideEvents Validation="OnValidation" ValueChanged="gridLookup_CloseUp" />
                                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="Pink">
                                                                            </InvalidStyle>
                                                                        </dx:ASPxGridLookup>
                                                                        </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataSpinEditColumn FieldName="Price" ShowInCustomizationForm="True" VisibleIndex="10">
                                                                        <PropertiesSpinEdit NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0" DisplayFormatString="{0:N}" >
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                        </PropertiesSpinEdit>
                                                                        </dx:GridViewDataSpinEditColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SubstitutedItem" VisibleIndex="5" Width="150px" Name="glItemCode">
                                                                   <%-- <EditItemTemplate>
                                                                    <dx:ASPxGridLookup ID="glItemCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false"
                                                                        DataSourceID="Masterfileitem" KeyFieldName="ItemCode" ClientInstanceName="glItem" TextFormatString="{0}" Width="150px" OnLoad="gvLookupLoad">
                                                                        <GridViewProperties Settings-ShowFilterRow="true" SettingsBehavior-FilterRowMode="Auto" Settings-VerticalScrollableHeight="150" Settings-VerticalScrollBarMode="Visible"> 
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                            AllowSelectSingleRowOnly="True" AllowDragDrop="False"/>
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" ReadOnly="True" VisibleIndex="0" >
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                         </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="FullDesc" ReadOnly="True" VisibleIndex="1" >
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                         </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                        <ClientSideEvents DropDown="lookup" KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" 
                                                                        ValueChanged="gridLookup_CloseUp" />
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>--%>
                                                            </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SubstitutedColor" VisibleIndex="6" Width="150px" Name="ColorCode">
                                                                             <%--<EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="ColorCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                    DataSourceID="color" KeyFieldName="ColorCode" ClientInstanceName="glColor2" TextFormatString="{0}" Width="150px" OnLoad="gvLookupLoad" >
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                            AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>--%>
                                                                        </dx:GridViewDataTextColumn>
                                                                       <dx:GridViewDataTextColumn FieldName="SubstitutedSize" VisibleIndex="7" Width="150px" Name="SizeCode">
                                                                         <%--<EditItemTemplate>
                                                                            <dx:ASPxGridLookup ID="SizeCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                DataSourceID="size" KeyFieldName="SizeCode" ClientInstanceName="glSize2" TextFormatString="{0}" Width="150px" OnLoad="gvLookupLoad" >
                                                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                        AllowSelectSingleRowOnly="True" />
                                                                                </GridViewProperties>
                                                                                <Columns>
                                                                                    <dx:GridViewDataTextColumn FieldName="SizeCode" ReadOnly="True" VisibleIndex="0" />
                                                                                    <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                </Columns>
                                                                                <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" ValueChanged="gridLookup_CloseUp" />
                                                                            </dx:ASPxGridLookup>
                                                                        </EditItemTemplate>--%>
                                                                    </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SubstitutedClass" VisibleIndex="8" Width="150px" Name="ClassCode">
                                                                             <%--<EditItemTemplate>
                                                                                <dx:ASPxGridLookup ID="ClassCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                                    DataSourceID="class" KeyFieldName="ClassCode" ClientInstanceName="glClass2" TextFormatString="{0}" Width="150px" OnLoad="gvLookupLoad" >
                                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                            AllowSelectSingleRowOnly="True" />
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" ReadOnly="True" VisibleIndex="0" />
                                                                                        <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" ValueChanged="gridLookup_CloseUp" />
                                                                                </dx:ASPxGridLookup>
                                                                            </EditItemTemplate>--%>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="ColorCode" Name="ColorCode" ShowInCustomizationForm="True" VisibleIndex="11" Width="0px" FieldName="PrevColorCode" ReadOnly="true" >
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="SizeCode" Name="SizeCode" ShowInCustomizationForm="True" VisibleIndex="12" Width="0px" FieldName="PrevSizeCode" ReadOnly="true" >
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="ClassCode" Name="ClassCode" ShowInCustomizationForm="True" VisibleIndex="13" Width="0px" FieldName="PrevClassCode" ReadOnly="true" >
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <SettingsEditing Mode="Batch" />
                                                                </dx:ASPxGridView>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                            
                                             </Items>
                                    </dx:LayoutGroup>
                                     <dx:LayoutGroup Caption="Item Cost History">
                                        <Items>
                                            <dx:LayoutGroup Caption="Lines">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server"> 
                                               <dx:ASPxGridView ID="agvItemSupplier" runat="server" AutoGenerateColumns="False" Width="747px" 
                                                    OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCellEditorInitialize="gv1_CellEditorInitialize" ClientInstanceName="gv3" >
                                                    <ClientSideEvents Init="OnInitTrans" />
                                                   <SettingsPager Mode="ShowAllRecords"/> 
                                                                    <SettingsBehavior AllowSort ="false" />
                                                   <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="AccountCode" Visible="False"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="1" Width="60px">
                                                               <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="Itemcosthisto">
                                                               <Image IconID="support_info_16x16"></Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                             </dx:GridViewCommandColumn>
                                                         <dx:GridViewDataTextColumn Caption="LineNumber" Name="LineNumber" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="LineNumber" UnboundType="String" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ItemCode" Name="ItemCode" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="ItemCode" UnboundType="String" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ColorCode" Name="ColorCode" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="ColorCode" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SizeCode" Name="SizeCode" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="SizeCode" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ClassCode" Name="ClassCode" ShowInCustomizationForm="True" VisibleIndex="6" FieldName="ClassCode" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Supplier" Name="Supplier" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="Supplier" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn Caption="Unit" Name="Unit" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="Unit" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn Caption="Price" Name="Price" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="Price" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PriceCurrency" Name="PriceCurrency" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="PriceCurrency" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SupplierItemCode" Name="SupplierItemCode" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="SupplierItemCode" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LastPrice" Name="LastPrice" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="LastPrice" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LastUnit" Name="LastUnit" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="LastUnit" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn Caption="LastUpdate" Name="LastUpdate" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="LastUpdate" UnboundType="String" ReadOnly="true" 
                                                             PropertiesTextEdit-DisplayFormatString="{0:M/d/yyyy}">
            
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="QuotePrice" Name="QuotePrice" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="QuotePrice" UnboundType="String" ReadOnly="true" >
                                                        </dx:GridViewDataTextColumn>
                                                     </Columns>
                                                    <ClientSideEvents CustomButtonClick="OnCustomClick" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130"  /> 
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                                            </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="User Defined Tab" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Field 1:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField1" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                            <ClientSideEvents Validation="function(){isValid=true;}" />
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 2:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField2" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 3:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField3" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 4:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField4" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 5:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField5" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 6:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField6" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 7:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField7" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 8:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField8" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field 9:">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHField9" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Field10" Name="txth10">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txth10" runat="server" Width="170px" OnLoad="TextboxLoad">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Audit Trail Tab" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Added By:" Name="txtAddedBy">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedBy" runat="server" ColCount="1" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Added Date:" Name="txtAddedDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedDate" runat="server" ColCount="1" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited By" Name="txtLastEditedBy">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Edited Date" Name="txtLastEditedDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedDate" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Activated By:" Name="txtActivatedBy" >
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtActivatedBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Activated Date:" Name="txtActivatedDate">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtActivatedDate" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Deactivated By:" Name="txtDeactivatedBy">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDeactivatedBy" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Deactivated Date:" Name="txtDeactivatedDate">
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
                            
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

            <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Cloning......"
        ClientInstanceName="cloneloading" ContainerElementID="gv1" Modal="true" ImagePosition="Left">
        <LoadingDivStyle Opacity="0" ></LoadingDivStyle>
    </dx:ASPxLoadingPanel>
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
                                         </dx:ASPxButton>
                                     <td><dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false">
                                         <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                                         </dx:ASPxButton> 
                                    </tr>
                                </table>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
           <%--<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />--%>
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
        <dx:ASPxPopupControl ID="ExportCSheet" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="gvExtractpop" CloseAction="CloseButton" CloseOnEscape="true"
        EnableViewState="False" HeaderImage-Height="10px" Opacity="0" HeaderText="" Height="0px" ShowHeader="true" Width="0px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
         ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" 
            ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true">
            <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                                                <dx:ASPxGridView ID="gvExtract" runat="server" ClientInstanceName="gvExtract" align="center" Visible="true"
                                                     KeyFieldName="SizeCode" >
                                                    <%--<ClientSideEvents EndCallback="gvExtract_end" />--%>
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                  <SettingsEditing Mode="Batch" />
                                                </dx:ASPxGridView>
            </dx:PopupControlContentControl>
        </ContentCollection>
        </dx:ASPxPopupControl>
    <!--#region Region Datasource-->
    
    <%--<!--#region Region Header --> --%>
    <asp:ObjectDataSource ID="odsHeader" runat="server" SelectMethod="getdata" TypeName="Entity.ItemMasterfile" DataObjectTypeName="Entity.ItemMasterfile" DeleteMethod="DeleteData" InsertMethod="InsertData" UpdateMethod="UpdateData">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="ItemCode" QueryStringField="docnumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDetail" runat="server" SelectMethod="getdetail" TypeName="Entity.ItemMasterfile+ItemMasterDetail" DataObjectTypeName="Entity.ItemMasterfile+ItemMasterDetail" DeleteMethod="DeleteItemDetail" InsertMethod="AddItemDetail" UpdateMethod="UpdateItemDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="ItemCode" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsFab" runat="server" SelectMethod="getfabric" TypeName="Entity.ItemMasterfile+ItemMasterDetail" DataObjectTypeName="Entity.ItemMasterfile+ItemMasterDetail" DeleteMethod="DeleteFabricComp" InsertMethod="AddFabricComp" UpdateMethod="UpdateFabricComp">
        <SelectParameters>
            <asp:QueryStringParameter Name="ItemCode" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsWHDetail" runat="server" SelectMethod="getItemWHDetail" TypeName="Entity.ItemMasterfile+ItemMasterDetail" DataObjectTypeName="Entity.ItemMasterfile+ItemMasterDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="ItemCode" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsSuppDetail" runat="server" SelectMethod="getItemSupplierDetail" TypeName="Entity.ItemMasterfile+ItemMasterDetail" DataObjectTypeName="Entity.ItemMasterfile+ItemMasterDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="ItemCode" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsstockPrice" runat="server" SelectMethod="getdetail" TypeName="Entity.ItemMasterfile+ItemStock+StockMasterSellThru" DataObjectTypeName="Entity.ItemMasterfile+ItemStock+StockMasterSellThru" InsertMethod="AddItemDetail" UpdateMethod="UpdateStock">
        <SelectParameters>
            <asp:QueryStringParameter Name="ItemCode" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" /> 
        </SelectParameters>
    </asp:ObjectDataSource>
   <asp:SqlDataSource ID="sdsItemSupp" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM MasterFile.ItemCustomerPrice where ItemCode  is null " OnInit="Connection_Init"></asp:SqlDataSource>
   <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM  masterfile.itemdetail where ItemCode  is null "
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="sdsFabricComp" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM  masterfile.FabricCompositionDetail where FabricCode  is null "
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsstockPrice" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select StockNumber,LineNumber,CONVERT(varchar(20),DateEncoded) DateEncoded,UserName,Analysis,ActionPlan,ProductAlignment,Remarks from Retail.StockMasterSellThru where StockNumber  is null "
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="ProdCat" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ProductCategoryCode,Description from  Masterfile.ProductCategory where ISNULL(IsInactive,0)=0"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   
        <asp:SqlDataSource ID="ProdSubCat" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ProductSubCatCode,Description from  Masterfile.ProductCategorySub where ISNULL(IsInactive,0)=0"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
           <asp:ObjectDataSource ID="odsCusDetail" runat="server" DataObjectTypeName="Entity.ItemMasterfile+ItemCustomerPriceDetail" DeleteMethod="DeleteItemPriceDetail" InsertMethod="AddItemPriceDetail" SelectMethod="getdetail" TypeName="Entity.ItemMasterfile+ItemCustomerPriceDetail" UpdateMethod="UpdateItemPriceDetail">
               <SelectParameters>
                   <asp:QueryStringParameter Name="ItemCode" QueryStringField="docnumber" Type="String" />
                   <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
               </SelectParameters>
           </asp:ObjectDataSource>
   </form>

   <asp:SqlDataSource ID="supplier" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select SupplierCode,Name from  Masterfile.BPSupplierInfo"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="customer" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select BizPartnerCode,Name from  Masterfile.BPCustomerInfo"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="Unit" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select UnitCode,Description from Masterfile.Unit WHERE ISNULL(IsInactive,0) =0"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="Color" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ColorCode,Description from Masterfile.Color WHERE ISNULL(IsInactive,0) =0"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="Class" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ClassCode,Description from Masterfile.Class WHERE ISNULL(IsInactive,0) =0"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="Size" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select SizeCode,Description from Masterfile.Size WHERE ISNULL(IsInactive,0) =0"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>

   <asp:SqlDataSource ID="ItemCategory" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ItemCategoryCode,Description from Masterfile.ItemCategory WHERE ISNULL(IsInactive,0) =0 and isnull(IsAsset,0)=0 ORDER BY CONVERT(int, ItemCategoryCode) ASC"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
    <asp:SqlDataSource ID="Statussql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select StatusCode, Description from masterfile.StockStatus where ISNULL(IsInactive,0)=0"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
    <asp:SqlDataSource ID="StorageType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select StorageType, StorageDescription from masterfile.StorageType"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="FabricGroup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM IT.GenericLookup WHERE LookUpKey = 'FBGRP' AND ISNULL(ISINACTIVE,0)!=1"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="FabDesign" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM MasterFile.FabricDesignCategory ORDER BY 1"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="Dye" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select * from masterfile.dyeing"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="Weave" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="Finishing" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT FinishingCode,Description FROM MasterFile.Finishing WHERE ISNULL(IsInactive,'0') = '0'"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="ItemType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select Code,Description from it.GenericLookup where LookUpKey = 'ITMTYP' and ISNULL(IsInactive,'0') = '0'"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
   <asp:SqlDataSource ID="CompType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select Description from it.GenericLookup where LookUpKey = 'COMTP' and ISNULL(IsInactive,'0') = '0'"
       OnInit = "Connection_Init">
   </asp:SqlDataSource>
    <asp:SqlDataSource ID="Masterfileitem" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] where isnull(IsInactive,'')=0" OnInit ="Connection_Init"></asp:SqlDataSource>
   <asp:SqlDataSource ID="sdsBizPartner" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select BizPartnerCode,Name from masterfile.BPCustomerInfo where ISNULL(isinactive,0)!=1" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsVAT" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DISTINCT TCode AS Tax, Description, ISNULL(Rate,0) AS Rate FROM Masterfile.Tax WHERE ISNULL(IsInactive,0) = 0" OnInit = "Connection_Init"> </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsTaxCode" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT TCode, Description, Rate FROM Masterfile.Tax WHERE ISNULL(IsInactive,0) = 0" OnInit="Connection_Init"></asp:SqlDataSource>
    <asp:ObjectDataSource ID="Months" runat="server" SelectMethod="Months" TypeName="Common.Common"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="Season" runat="server" SelectMethod="Season" TypeName="Common.Common"></asp:ObjectDataSource>
   <asp:SqlDataSource ID="BrandLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM MasterFile.Brand WHERE ISNULL(IsInactive,0)=0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="OutletSalessql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>"
        SelectCommand="sp_getOutletSales" SelectCommandType="StoredProcedure"
        OnInit = "Connection_Init">
        <SelectParameters>
                   <asp:QueryStringParameter Name="ItemCode" QueryStringField="docnumber" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
        <!--#endregion-->
</body>
</html>

