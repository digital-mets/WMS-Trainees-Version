<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPurchasedOrder.aspx.cs" Inherits="GWL.frmPurchasedOrder" %>

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
        height: 770px; /*Change this whenever needed*/
        }

        .Entry {
         padding: 20px;
         margin: 10px auto;
         background: #FFF;
        }

        /*.dxeButtonEditSys input,
        .dxeTextBoxSys input{
            text-transform:uppercase;
        }*/

         .pnl-content
        {
            text-align: right;
        }

        .invalid{ border-left-color:deeppink;
                  border-right-color:deeppink;
                  border-bottom-color:deeppink;
                  border-top-color:deeppink;

                  border-width:medium;

        }

    </style>
    <!--#endregion-->
    
    <!--#region Region Javascript-->
    <script>
        var isValid = true;
        var counterror = 0;
        var isGross = false;

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
        //var changedValues = "";
        //function OnValueChanged(s, e) {
        //    if (gv1.batchEditApi.HasChanges()) {
        //        console.log(gv1.batchEditApi.HasChanges() + ' changes')
        //        var topRowIndex = gv1.GetTopVisibleIndex();
        //        for (var i = topRowIndex; i < topRowIndex + gv1.GetVisibleRowsOnPage() ; i++) {
        //            if (gv1.batchEditApi.HasChanges(i)) {
        //                for (var j = 0; j < gv1.GetColumnCount() ; j++) {
        //                    if (gv1.batchEditApi.HasChanges(i, j)) {
        //                        var fieldName = gv1.GetColumn(j).fieldName;
        //                        changedValues += "|" + fieldName + ":" + i + "=" + gv1.batchEditApi.GetCellValue(i, j);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    console.log(changedValues + ' changedValues')
        //   // grid.PerformCallback(changedValues);
        //}

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
                btn.SetEnabled(false);
                if (btnmode == "Add") {
                    gv1.PerformCallback("Add");
                }
                else if (btnmode == "Update") {
                    gv1.PerformCallback("Update");
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
            console.log(e);
            if (e.requestTriggerID === "MainForm_GridForm_gv1")//disables confirmation message upon saving.
                e.cancel = true;
        }

        var initgv = 'true';
        var VATRate = 0;
        var ATCRate = 0
        var VATCode = "";
        var vatdetail1 = 0;
        var iswithdetail = "false";
        function gridView_EndCallback(s, e) {//End callback function if (s.cp_success) {    
            
            if (s.cp_success) {
                gv1.CancelEdit();
                if (s.cp_valmsg != null) {
                    alert(s.cp_valmsg);
                }
                alert(s.cp_message);
                delete (s.cp_valmsg);
                delete (s.cp_success);//deletes cache variables' data
                delete (s.cp_message);

            }
            if (s.cp_close) {
                gv1.CancelEdit();
                if (s.cp_message != null) {
                    if (s.cp_message2 != null)
                    {
                        alert('Item: ' + s.cp_message2 + ' Ordered Quantity is below the Minimum Order Quantity of the item');
                    }
                    alert(s.cp_message);
                    delete (s.cp_message);
                    delete (s.cp_message2);
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
                autocalculate();
                //gv1.CancelEdit();
               // cp.PerformCallback('vat');
            }

            if (s.cp_unitcost) {
                delete (s.cp_unitcost);
            }

            if (s.cp_vatdetail != null) {
                totalvat = s.cp_vatdetail;
                delete (s.cp_vatdetail);
                CINGrossVATableAmount.SetValue(totalvat);
            }

            if (s.cp_nonvatdetail != null) {
                totalnonvat = s.cp_nonvatdetail;
                delete (s.cp_nonvatdetail);
                CINNonVATableAmount.SetValue(totalnonvat);
            }
            //if (s.cp_vatrate != null) {

            //    vatrate = s.cp_vatrate;
            //    delete (s.cp_vatrate);
            //    vatdetail1 = 1 + parseFloat(vatrate);
            //   // console.log(vatrate + "vatratetry");

                
            //    console.log(vatrate);
            //}
            //if (s.cp_atc != null) {
            //    //var ATCRate = 0.00;
            //    atc = s.cp_atc;
            //    //ATCRate = s.cp_atc;
            //    //txtATCRate.SetText(ATCRate.toFixed(2));
            //    delete (s.cp_atc);
                
            //    console.log('cp_atc');
            //}

            //console.log(s.cp_iswithpr + ' kuya era')
            if (s.cp_iswithpr == "1") {
                
                prnum.SetEnabled(true);
                CINGenerate.SetEnabled(true);
                delete (s.cp_iswithpr)
                //gv1.Columns["LineNumber"].setVisible(true);
            }
            if (s.cp_iswithpr == "0") {
                prnum.SetEnabled(false);
                prnum.SetText('');
                CINGenerate.SetEnabled(false);
                delete (s.cp_iswithpr)
                //gv1.Columns["LineNumber"].setVisible(false);
            }

            if (s.cp_VATAX == "True") {
                VATRate = s.cp_vatrate;
                delete (s.cp_vatrate);
                ATCRate = s.cp_atc;
                delete (s.cp_atc);
                VATCode = s.cp_VATCode;
                delete (s.cp_VATCode);
                setVATAX();
                autocalculate();
                delete (s.cp_VATAX);
                //gv1.Columns["LineNumber"].setVisible(false);
            }

            //if (s.cp_VATAX == "False") {
            //    console.log('Pasok din dito grabe?');
            //    //gv1.Columns["LineNumber"].setVisible(false);
            //}

            if (s.cp_iswithdetail == "True") {
                iswithdetail = s.cp_iswithdetail;
                delete (s.cp_iswithdetail);
                //gv1.Columns["LineNumber"].setVisible(false);
            }

            btn.SetEnabled(true);
        }

        var index;
        var closing;
        var itemc; //variable required for lookup
        var valchange = false;
        var valchange_VAT = false;
        var currentColumn = null;
        var isSetTextRequired = false;
        var linecount = 1;
        var editorobj;
        function OnStartEditing(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];
            itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode"); //needed var for all lookups; this is where the lookups vary for
            index = e.visibleIndex;
            //if (e.visibleIndex < 0) {//new row
            //    var linenumber = s.GetColumnByField("LineNumber");
            //    e.rowValues[linenumber.index].value = linecount++; // or any other default value
            //}
            if (s.batchEditApi.GetCellValue(e.visibleIndex, "IsAllowPartial") === null) {
                s.batchEditApi.SetCellValue(e.visibleIndex, "IsAllowPartial", true)
            }

            editorobj = e;

            var entry = getParameterByName('entry');
            if (entry == "V") {
                e.cancel = true; //this will made the gridview readonly
            }
            if (prnum.GetText() != "")
            {
                if (e.focusedColumn.fieldName === "ItemCode") { //Check the column name
                    e.cancel = true;
                }
                if (e.focusedColumn.fieldName === "ColorCode") {
                    e.cancel = true;
                }
                if (e.focusedColumn.fieldName === "ClassCode") {
                    e.cancel = true;
                }
                if (e.focusedColumn.fieldName === "SizeCode") {
                    e.cancel = true;
                }
            }

            if (entry != "V")
            {

                if (e.focusedColumn.fieldName === "VATCode") {
                    if (s.batchEditApi.GetCellValue(e.visibleIndex, "IsVat") == false) {
                        e.cancel = true;
                    }
                    else {
                        glVATCode.GetInputElement().value = cellInfo.value; //Gets the column value
                        isSetTextRequired = true;
                    }
                }

                if (e.focusedColumn.fieldName === "ItemCode") { //Check the column name
                    gl.GetInputElement().value = cellInfo.value; //Gets the column value
                    isSetTextRequired = true;
                    index = e.visibleIndex;
                    closing = true;
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
                if (e.focusedColumn.fieldName === "Unit") {
                    gl5.GetInputElement().value = cellInfo.value;
                }
                if (e.focusedColumn.fieldName === "FullDesc") {
                    gl6.GetInputElement().value = cellInfo.value;
                }
                if (e.focusedColumn.fieldName === "VATCode") {
                    glVATCode.GetInputElement().value = cellInfo.value;
                }
                if (e.focusedColumn.fieldName === "PRNumber") {
                    glPRNumber.GetInputElement().value = cellInfo.value;
                }
            }
       }

        function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
            var cellInfo = e.rowValues[currentColumn.index];

            var entry = getParameterByName('entry');

            //if (entry == "N") {
            //Pag ni click mo yung field hindi nawawala yung value. na seset pa din after mo mag click sa ibang field
                if (currentColumn.fieldName === "ItemCode") {
                    cellInfo.value = gl.GetValue();
                    cellInfo.text = gl.GetText().toUpperCase();
                }
                if (currentColumn.fieldName === "ColorCode") {
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
                if (currentColumn.fieldName === "Unit") {
                    cellInfo.value = gl5.GetValue();
                    cellInfo.text = gl5.GetText();
                }
                if (currentColumn.fieldName === "FullDesc") {
                    cellInfo.value = gl6.GetValue();
                    cellInfo.text = gl6.GetText();
                }
                if (currentColumn.fieldName === "VATCode") {
                    cellInfo.value = glVATCode.GetValue();
                    cellInfo.text = glVATCode.GetText();
                }
                if (currentColumn.fieldName === "PRNumber") {
                    cellInfo.value = glPRNumber.GetValue();
                    cellInfo.text = glPRNumber.GetText();
                }
        }

        function setVATAX(s,e)
        {
            setTimeout(function () { //New Rows
                var indicies = gv1.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++) {
                    if (gv1.batchEditHelper.IsNewRow(indicies[i])) {

                        gv1.batchEditApi.SetCellValue(indicies[i], 'VATCode', VATCode);
                        gv1.batchEditApi.SetCellValue(indicies[i], 'Rate', VATRate);
                        gv1.batchEditApi.SetCellValue(indicies[i], 'ATCRate', ATCRate);

                        if(VATCode != 'NONV')
                        {
                            gv1.batchEditApi.SetCellValue(indicies[i], 'IsVat', true);

                        }
                        else
                        {

                            gv1.batchEditApi.SetCellValue(indicies[i], 'IsVat', false);
                        }

                    }


                    else { //Existing Rows
                        var key = gv1.GetRowKey(indicies[i]);
                        if (gv1.batchEditHelper.IsDeletedRow(key)) {
                            console.log("deleted row " + indicies[i]);
                            //gv1.batchEditHelper.EndEdit();
                        }
                        else {
                            gv1.batchEditApi.SetCellValue(indicies[i], 'VATCode', VATCode);
                            gv1.batchEditApi.SetCellValue(indicies[i], 'Rate', VATRate);
                            gv1.batchEditApi.SetCellValue(indicies[i], 'ATCRate', ATCRate);


                            if (VATCode != 'NONV') {
                                if (iswithdetail == "True")
                                {
                                    gv1.batchEditApi.SetCellValue(indicies[i], 'IsVat', true);
                                }
                                else
                                {
                                    gv1.batchEditApi.SetCellValue(indicies[i], 'IsVat', 1);
                                }
                            }
                            else {
                                if (iswithdetail == "True") {
                                    gv1.batchEditApi.SetCellValue(indicies[i], 'IsVat', false);
                                }
                                else {
                                    gv1.batchEditApi.SetCellValue(indicies[i], 'IsVat', 0);
                                }
                            }
                        }

                    }

                }

            }, 500);

        }

        var Nanprocessor = function (entry) {
            if (isNaN(entry) == true) {
                console.log(entry +"entry")
                return 0;
            } else
                return entry;
        }
        function autocalculate(s, e) {
            console.log('inside autocalculate')
            OnInitTrans();
            //console.log(txtNewUnitCost.GetValue());
            var unitfrieght = 0.00;
            var receivedqty1 = 0.00;
            var unitcost1 = 0.00;
            var receivedqty2 = 0.00;
            var unitcost2 = 0.00;
            var freight = 0.00;
            
            var totalfreight = 0.00;
            var TotalQuantity = 0.00;
            var TotalAmount1 = 0.00;
            var TotalAmount2 = 0.00;
            var ForeignAmount = 0.00;
            

            var exchangerate = 0.00;
            var totalqty = 0.00
            var frieght = 0.00;
            var orderqty = 0.00;
            var orderqtyVAT = 0.00;
            var orderqtyNVAT = 0.00;
            var unitcost = 0.00;
            var unitcostVAT = 0.00;
            var unitcostNVAT = 0.00;
            var sumfreight = 0.00;
            var TotalAmount = 0.00;
            var TotalAmountVAT = 0.00;
            var TotalAmountNVAT = 0.00;
            var GrossVat = 0.00;
            var NonVat = 0.00;
            var VATAmount = 0.00;
            var WithHolding = 0.00;
            var PesoAmount = 0.00;
            var CPesoAmount = 0.00;

            var UnitExchangeR = 0.00;
            var Ratedetail = 0.00;
            var ATCDetail = 0.00;
            var TotalVatComputer = 0.00;



            var CosttimeExchange = 0.00;

            //Get and Set Value of Exhange Rate
            if (CINExchangeRate.GetValue() == null || CINExchangeRate.GetValue() == "") {
                exchangerate = 0;
            }
            else {
                exchangerate = CINExchangeRate.GetValue();
            }
            //Get and Set Value of Total Quantity
      
            //Get and Set Value of Total Freight
            if (CINTotalFreight.GetValue() == null || CINTotalFreight.GetValue() == "") {
                freight = 0;
            }
            else {
                freight = CINTotalFreight.GetValue();
            }

            setTimeout(function () { //New Rows
                var indicies = gv1.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++)
                {
                    if (gv1.batchEditHelper.IsNewRow(indicies[i]))
                    {
                         orderqty = gv1.batchEditApi.GetCellValue(indicies[i], "OrderQty");
                         unitcost = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                         unitfrieght = gv1.batchEditApi.GetCellValue(indicies[i], "UnitFreight");
                         PesoAmount = gv1.batchEditApi.SetCellValue(indicies[i], "PesoAmount");

                         
                         totalfreight += unitfrieght * orderqty;
                         TotalAmount += unitcost * orderqty;  //Total Amount of OrderQty
                         TotalQuantity += orderqty;          //Sum of all Quantity
                         CosttimeExchange += (unitcost * exchangerate) * orderqty;
                         CPesoAmount = CosttimeExchange;

                         var cb = gv1.batchEditApi.GetCellValue(indicies[i], "IsVat")
                         


                            //console.log(gv1.batchEditApi.GetCellValue(indicies[i], "IsVat"));
                         if (cb == true)
                         {
                             orderqtyVAT = gv1.batchEditApi.GetCellValue(indicies[i], "OrderQty");
                             unitcostVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                             Ratedetail = gv1.batchEditApi.GetCellValue(indicies[i], "Rate");
                             ATCDetail = gv1.batchEditApi.GetCellValue(indicies[i], "ATCRate");
                             TotalAmountVAT += unitcostVAT * exchangerate;
                             UnitExchangeR += (unitcostVAT * exchangerate) * orderqtyVAT;
                             TotalVatComputer += ((unitcostVAT * exchangerate) * orderqtyVAT) * Ratedetail;
                             GrossVat = TotalAmountVAT * orderqtyVAT;
                             //VATAmount = UnitExchangeR * Ratedetail;
                             VATAmount = TotalVatComputer;
                             //WithHolding = ((GrossVat - VATAmount) * ATCDetail);
                             WithHolding = (GrossVat * ATCDetail); // New Formula From Ate Nes 02-29-2016
                         }

                         else
                         {
                             orderqtyNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "OrderQty");
                             unitcostNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                             TotalAmountNVAT += unitcostNVAT * exchangerate;
                                NonVat = TotalAmountNVAT * orderqtyNVAT;
                         }
                    }




                    else
                    { //Existing Rows
                        var key = gv1.GetRowKey(indicies[i]);
                        if (gv1.batchEditHelper.IsDeletedRow(key))
                        {
                            console.log("deleted row " + indicies[i]);
                            //gv1.batchEditHelper.EndEdit();
                        }
                        else
                        {
                            console.log(i + 'EXISTINGSSSSSSSSSSSs')
                            orderqty = gv1.batchEditApi.GetCellValue(indicies[i], "OrderQty");
                            unitcost = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                            unitfrieght = gv1.batchEditApi.GetCellValue(indicies[i], "UnitFreight");
                            PesoAmount = gv1.batchEditApi.SetCellValue(indicies[i], "PesoAmount");


                            totalfreight += unitfrieght * orderqty;
                            TotalAmount += unitcost * orderqty;  //Total Amount of OrderQty
                            TotalQuantity += orderqty;          //Sum of all Quantity

                            CosttimeExchange += (unitcost * exchangerate) * orderqty;

                            CPesoAmount = CosttimeExchange;

                            var cb = gv1.batchEditApi.GetCellValue(indicies[i], "IsVat")
 
                                if (cb == true)
                                {
                                    orderqtyVAT = gv1.batchEditApi.GetCellValue(indicies[i], "OrderQty");
                                    unitcostVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                                    Ratedetail = gv1.batchEditApi.GetCellValue(indicies[i], "Rate");
                                    ATCDetail = gv1.batchEditApi.GetCellValue(indicies[i], "ATCRate");
                                    TotalAmountVAT += unitcostVAT * orderqtyVAT;
                                    UnitExchangeR += (unitcostVAT * exchangerate) * orderqtyVAT;
                                    TotalVatComputer += ((unitcostVAT * exchangerate) * orderqtyVAT) * Ratedetail;  // compute isa isa vatamount bago isalpak.
                                    GrossVat = TotalAmountVAT * exchangerate;
                                    //VATAmount = TotalAmountVAT * vatrate;
                                    VATAmount = TotalVatComputer;
                                    //WithHolding = ((GrossVat - VATAmount) * ATCDetail);
                                    WithHolding = (GrossVat * ATCDetail); // New Formula From Ate Nes 02-29-2016

                                }
                                else
                                {
                                    orderqtyNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "OrderQty");
                                    unitcostNVAT = gv1.batchEditApi.GetCellValue(indicies[i], "UnitCost");
                                    TotalAmountNVAT += unitcostNVAT * orderqtyNVAT;
                                    NonVat = TotalAmountNVAT * exchangerate;
                                }
                            }

                        }



                       
                    }

                    CINTotalFreight.SetValue(totalfreight.toFixed(2));
                    CINPesoAmount.SetValue(CPesoAmount.toFixed(2));
                    CINForeignAmount.SetValue(TotalAmount.toFixed(2));
                    CINTotalQty.SetValue(TotalQuantity.toFixed(2));
                    CINGrossVATableAmount.SetValue(GrossVat.toFixed(2));
                    CINNonVATableAmount.SetValue(NonVat.toFixed(2));
                    CINVATAmount.SetValue(VATAmount.toFixed(2));
                    CINWithholdingTax.SetValue(WithHolding.toFixed(2));

            }, 500);
        }

        function detailautocalculate(s, e) {
            OnInitTrans();
            var freight = 0.00;
            var totalqty = 0.00
            var totalfreight = 0.00;

            if (CINTotalFreight.GetValue() == null || CINTotalFreight.GetValue() == "")
                {
                    freight = 0;
                }
            else
                {
                    freight = CINTotalFreight.GetValue();
                }
            if (CINTotalQty.GetValue() == null || CINTotalQty.GetValue() == "")
                {
                    totalqty = 0;
                }
            else
                {
                    totalqty = CINTotalQty.GetValue();
                }

            setTimeout(function () {
                for (var i = 0; i < gv1.GetVisibleRowsOnPage() ; i++) {
                    gv1.batchEditApi.SetCellValue(i, "UnitFreight", (freight / totalqty).toFixed(2));
                }
            }, 500);
        }
        
        function negativecheck(s, e) {
            var unitfreight = CINUnitFreightDetail.GetValue();
            var orderqty = CINOrderQtyDetail.GetValue();
            var unitcost = CINUnitCostDetail.GetValue();

            unitfreight = unitfreight <= 0 ? 0 : unitfreight;
            CINUnitFreightDetail.SetValue(unitfreight);
        }

        function orderqtynegativecheck(s, e) {
            var orderqty = CINOrderQtyDetail.GetValue();

            orderqty = orderqty <= 0 ? 0 : orderqty;
            CINOrderQtyDetail.SetValue(orderqty);
        }

        function unitcostnegativecheck(s, e) {
            var unitcost = CINUnitCostDetail.GetValue();

            unitcost = unitcost <= 0 ? 0 : unitcost;
            CINUnitCostDetail.SetValue(unitcost);
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
            if (keyCode == 13) {
                gv1.batchEditApi.EndEdit();
            }
            //ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
        }

        function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
            //setTimeout(function () {
                gv1.batchEditApi.EndEdit();
            //}, 1000);
        }

        //validation
        //function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields/index 0 is from the commandcolumn)
        //    for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
        //        var column = s.GetColumn(i);
        //        if (column != s.GetColumn(1) && column != s.GetColumn(2) && column != s.GetColumn(3)
        //            && column != s.GetColumn(13) && column != s.GetColumn(14) && column != s.GetColumn(15)
        //            && column != s.GetColumn(16) && column != s.GetColumn(17) && column != s.GetColumn(18)
        //            && column != s.GetColumn(19) && column != s.GetColumn(20) && column != s.GetColumn(21)
        //            && column != s.GetColumn(22) && column != s.GetColumn(23)) {//Set to skip all unnecessary columns that doesn't need validation//Column index needed to set //Example for Qty
        //            var cellValidationInfo = e.validationInfo[column.index];
        //            if (!cellValidationInfo) continue;
        //            var value = cellValidationInfo.value;
        //            if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "") {
        //                cellValidationInfo.isValid = false;
        //                cellValidationInfo.errorText = column.fieldName + " is required";
        //                isValid = false;
        //                console.log(column);
        //            }
        //            else {
        //                isValid = true;
        //            }
        //        }
        //    }
        //}

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

                if (column.fieldName == "OrderQty") {
                    var cellValidationInfo = e.validationInfo[column.index];
                    if (!cellValidationInfo) continue;
                    var value = cellValidationInfo.value;
                    if ((!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "0" || ASPxClientUtils.Trim(value) == "0.00" || ASPxClientUtils.Trim(value) == null)) {
                        cellValidationInfo.isValid = false;
                        cellValidationInfo.errorText = column.fieldName + " is required!";
                        isValid = false;
                    }
                }

                if (column.fieldName == "UnitCost") {
                    var cellValidationInfo = e.validationInfo[column.index];
                    if (!cellValidationInfo) continue;
                    var value = cellValidationInfo.value;
                    if ((!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "0" || ASPxClientUtils.Trim(value) == "0.00" || ASPxClientUtils.Trim(value) == null)) {
                        cellValidationInfo.isValid = false;
                        cellValidationInfo.errorText = column.fieldName + " is required!";
                        isValid = false;
                    }
                }
            }
        }


        function OnCustomClick(s, e)
        {

            if (e.buttonID == "Details") {
                var itemcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ItemCode");
                var colorcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ColorCode");
                var classcode = s.batchEditApi.GetCellValue(e.visibleIndex, "ClassCode");
                var sizecode = s.batchEditApi.GetCellValue(e.visibleIndex, "SizeCode");
                var unitbase = s.batchEditApi.GetCellValue(e.visibleIndex, "Unit");
                var fulldesc = s.batchEditApi.GetCellValue(e.visibleIndex, "FullDesc");
                var Warehouse = CINReceivingWarehouse.GetText();
               
                factbox.SetContentUrl('../FactBox/fbItem.aspx?itemcode=' + itemcode
                + '&colorcode=' + colorcode + '&classcode=' + classcode + '&sizecode=' + sizecode + + '&Warehouse=' + Warehouse);
              


            }

            if (e.buttonID == "Delete") {
                gv1.DeleteRow(e.visibleIndex);
                //gv1.Refresh();
                autocalculate(s, e);
                CheckDetail();
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


            //if (e.buttonID == "CountSheet") {
            //    CSheet.Show();
            //    var linenum = s.batchEditApi.GetCellValue(e.visibleIndex, "LineNumber");
            //    var docnumber = getParameterByName('docnumber');
            //    var transtype = getParameterByName('transtype');
            //    var entry = getParameterByName('entry');
            //    CSheet.SetContentUrl('frmCountSheet.aspx?entry=' + entry + '&docnumber=' + docnumber + '&transtype=' + transtype +
            //        '&linenumber=' + linenum);
            //}
        }

        function Generate(s, e) {
            var generate = confirm("Are you sure you want to generate the Purchase Request Number(s)?");
            if (generate) {
                gv1.CancelEdit();
                gv1.PerformCallback('Generate');
                e.processOnServer = false;
            }
        }


        function checkedchanged(s, e) {
            var checkState = cbiswithpr.GetChecked();
            if (checkState == true) {
                gv1.PerformCallback('iswithprtrue');
                e.processOnServer = false;
            }
            else {
                gv1.PerformCallback('iswithprfalse');
                e.processOnServer = false;
            }

        }

        var identifier;
        var val_ALL;
        function GridEndChoice(s, e) {

            identifier = s.GetGridView().cp_identifier;
            val = s.GetGridView().cp_codes;
            val_ALL = s.GetGridView().cp_codes;
            

            val_VAT = s.GetGridView().cp_codes;
            temp_VAT = val_VAT.split(';');

            //console.log(identifier + " idetifier")
            //console.log(val + " VAL")
            //console.log(val_VAT + " VAL_VAT")

            console.log(identifier)
            console.log(val_ALL + " ito sila!")
            if (identifier == "ItemCode") {
                delete (s.GetGridView().cp_identifier);
                if (s.GetGridView().cp_valch) {
                    delete (s.GetGridView().cp_valch);
                    for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
                        //console.log('anoto')
                        var column = gv1.GetColumn(i);
                        if (column.visible == false || column.fieldName == undefined)
                            continue;
                        ProcessCells_ItemCode(0, editorobj, column, gv1);
                    }
                }
                console.log('1')
                gv1.batchEditApi.EndEdit();
            }

            if (identifier == "VAT") {
                GridEnd_VAT();
                console.log('2')
                gv1.batchEditApi.EndEdit();
            }


            loader.Hide();
        }


        function ProcessCells_ItemCode(selectedIndex, e, column, s) {
            var temp_ALL;
            if (temp_ALL == null) {
                temp_ALL = ";;;;;;;;;";
            }
            temp_ALL = val_ALL.split(';');
            if (temp_ALL[0] == null) {
                temp_ALL[0] = "";
            }
            if (temp_ALL[1] == null) {
                temp_ALL[1] = "";
            }
            if (temp_ALL[2] == null) {
                temp_ALL[2] = "";
            }
            if (temp_ALL[3] == null) {
                temp_ALL[3] = "";
            }
            if (temp_ALL[4] == null) {
                temp_ALL[4] = "";
            }
            if (temp_ALL[5] == null) {
                temp_ALL[5] = "";
            }
            if (temp_ALL[6] == null) {
                temp_ALL[6] = "";
            }
            if (temp_ALL[7] == null) {
                temp_ALL[7] = "";
            }
            if (temp_ALL[8] == null) {
                temp_ALL[8] = "";
            }
            if (temp_ALL[9] == null) {
                temp_ALL[9] = "";
            }
            if (selectedIndex == 0) {
                if (column.fieldName == "ColorCode") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[0]);
                }
                if (column.fieldName == "ClassCode") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[1]);
                }
                if (column.fieldName == "SizeCode") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[2]);
                }
                if (column.fieldName == "Unit") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[3]);
                }
                if (column.fieldName == "FullDesc") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[4]);
                }
                if (column.fieldName == "VATCode") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[5]);
                }
                if (column.fieldName == "IsVat") {
                    if (temp_ALL[6] == "True") {
                        s.batchEditApi.SetCellValue(index, column.fieldName, true);
                    }
                    else {
                        s.batchEditApi.SetCellValue(index, column.fieldName, false);
                    }
                }
                if (column.fieldName == "Rate") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[7]);
                }
                if (column.fieldName == "ATCRate") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[8]);
                }
                if (column.fieldName == "UnitCost") {
                    s.batchEditApi.SetCellValue(index, column.fieldName, temp_ALL[9]);
                }
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

        function OnInitTrans(s, e) {


            var BizPartnerCode = clBizPartnerCode.GetText();
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
            gvRef.SetWidth(width - 120);
        }

        function CheckDetail(s, e) {
            //if (prnum.GetText() != "") {
            //    if (gv1.GetVisibleRowsOnPage() == 0) {
            //        prnum.SetEnabled(true);
            //        cbiswithpr.SetEnabled(true);
            //        CINGenerate.SetEnabled(true);
                    
            //    }
            //}

            var checkState = cbiswithpr.GetChecked();
            if (checkState == true) {
                if (gv1.GetVisibleRowsOnPage() == 0) {
                    prnum.SetEnabled(true);
                    cbiswithpr.SetEnabled(true);
                    CINGenerate.SetEnabled(true);

                }
            }
            else {
                if (gv1.GetVisibleRowsOnPage() == 0) {
                    //prnum.SetEnabled(true);
                    cbiswithpr.SetEnabled(true);
                    CINGenerate.SetEnabled(true);

                }
            }
        }


        //var fck = 0
        //function DataSourceChecker(s, e) {
        //    console.log('Grid Init')
        //    if (fck == 0)
        //   { 
        //        console.log('DataSourceChecker')
        //        cp.PerformCallback('datasourceset');
        //        e.processOnServer = false;
        //        fck = 1;
        //    }

        //    autocalculate();
        //} 

        function SetDefaultCommitment(s, e) {
            console.log('Inside SetDefaultCommitment() Function.')
            var days = 7;
            var targetdeliverydate = CINTargetDeliveryDate.GetDate();

            var commitmentdate = new Date(targetdeliverydate);

            //cancellationdate.setTime(cancellationdate.getTime() + (days * 24 * 60 * 60 * 1000));;

            var commitmentmonth = commitmentdate.getMonth() + 1; //months from 1-12
            var commitmentday = commitmentdate.getDate();
            var commitmentyear = commitmentdate.getFullYear();

            var defaultdate = commitmentmonth + '/' + commitmentday + '/' + commitmentyear;
            CINCommitmentDate.SetText(defaultdate);


            var cancellationdate = new Date(targetdeliverydate);
            cancellationdate.setTime(cancellationdate.getTime() + (days * 24 * 60 * 60 * 1000));

            var cancellationmonth = cancellationdate.getMonth() + 1; //months from 1-12
            var cancellationday = cancellationdate.getDate();
            var cancellationyear = cancellationdate.getFullYear();

            var defaultdate = cancellationmonth + '/' + cancellationday + '/' + cancellationyear;
            CINCancellationDate.SetText(defaultdate);
        }

        function SetDefaultCancellation(s, e) {
            var days = 7;
            var commitmentdate = CINCommitmentDate.GetDate();
            var cancellationdate = new Date(commitmentdate);
                cancellationdate.setTime(cancellationdate.getTime() +(days * 24 * 60 * 60 * 1000));;

            var commitmentmonth = commitmentdate.getMonth() + 1; //months from 1-12
            var commitmentday = commitmentdate.getDate();
            var commitmentyear = commitmentdate.getFullYear();

            var cancellationmonth = cancellationdate.getMonth() + 1; //months from 1-12
            var cancellationday = cancellationdate.getDate();
            var cancellationyear = cancellationdate.getFullYear();


            //console.log(commitmentmonth + '/' + commitmentday + '/' + commitmentyear);
            //console.log(cancellationmonth + '/' + cancellationday + '/' + cancellationyear);

            var defaultdate = cancellationmonth + '/' + cancellationday + '/' + cancellationyear;
            CINCancellationDate.SetText(defaultdate);
        }


        function SetDefaultTargetDate(s, e) {
            var days = CINTerms.GetValue();
            if (days > 0)
            {
                var docdate = CINDocDate.GetDate();
                var targetdate = new Date(docdate);
                targetdate.setTime(targetdate.getTime() + (days * 24 * 60 * 60 * 1000));;

                var targetmonth = targetdate.getMonth() + 1; //months from 1-12
                var targetday = targetdate.getDate();
                var targetyear = targetdate.getFullYear();

                var defaultdate = targetmonth + '/' + targetday + '/' + targetyear;
                CINTargetDeliveryDate.SetText(defaultdate);
            }
        }
            


    </script>
    <!--#endregion-->
</head>
<body style="height: 910px;">
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
<form id="form1" runat="server" class="Entry">
    <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel runat="server" Text="Purchase Order" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
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
        <ClientSideEvents CloseUp="function (s, e) { window.location.reload(); }" />
    </dx:ASPxPopupControl>


    <dx:ASPxFormLayout ID="MainForm" runat="server" Height="400px" Width="1280px" style="margin-left: -3px">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
        <Items>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="200px" ClientInstanceName="cp" OnCallback="cp_Callback">
                            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxFormLayout ID="HeaderForm" runat="server" Height="300px" Width="1280px" style="margin-left: -3px">
                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                        <Items>
                                            <dx:TabbedLayoutGroup>
                                                <Items>
                                                    <dx:LayoutGroup Caption="General">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Information" ColCount="2">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Document Number">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxTextBox ID="txtDocNumber" runat="server" Width="170px"  AutoCompleteType="Disabled" Enabled="False">
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Document Date">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxDateEdit ID="dtDocDate" ClientInstanceName="CINDocDate" runat="server" Width="170px" OnInit ="dtpDocDate_Init" >
                                                                                    <ClientSideEvents Validation="OnValidation" ValueChanged="SetDefaultTargetDate"/>
                                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                                        <RequiredField IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                    <InvalidStyle BackColor="Pink">
                                                                                    </InvalidStyle>
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Supplier Code">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridLookup ID="glSupplierCode" runat="server" Width="170px" ClientInstanceName="clBizPartnerCode" DataSourceID="SupplierCodelookup" KeyFieldName="SupplierCode" TextFormatString="{0}"
                                                                                    oninit="glSupplierCode_Init">
                                                                                    <ClientSideEvents Validation="OnValidation" DropDown="function(){
                                                                                        clBizPartnerCode.GetGridView().PerformCallback();
                                                                                        }"
                                                                                    ValueChanged="function (s, e){ console.log('clickedsupplier');  var g = clBizPartnerCode.GetGridView(); 
                                                                                             cp.PerformCallback('SupplierCodeCase|'+g.GetRowKey(g.GetFocusedRowIndex())); e.processOnServer = false; SetDefaultCommitment();}"/>
                                                                                    <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                                        <RequiredField IsRequired="True"/>
                                                                                    </ValidationSettings>
                                                                                    <InvalidStyle BackColor="Pink">
                                                                                    </InvalidStyle>
                                                                                    <GridViewProperties>
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                                        <Settings ShowFilterRow="True" />
                                                                                    </GridViewProperties>
                                                                                        <Columns>
					                                                                        <dx:GridViewDataTextColumn Caption="Supplier Code" FieldName="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="1" Width="5px">
					                                                                        </dx:GridViewDataTextColumn>
					                                                                        <dx:GridViewDataTextColumn Caption="Name" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2" Width="5px">
					                                                                        </dx:GridViewDataTextColumn>
				                                                                        </Columns>
                                                                                </dx:ASPxGridLookup>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Target Delivery Date">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxDateEdit ID="dtTargetDate" ClientInstanceName="CINTargetDeliveryDate" runat="server" Width="170px">
                                                                                    <ClientSideEvents Validation="OnValidation"  DateChanged="SetDefaultCommitment" ValueChanged="SetDefaultCommitment" />
                                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                                        <RequiredField IsRequired="True" />
                                                                                    </ValidationSettings>
                        <%----                                                           <InvalidStyle BackColor="Pink">
                                                                                    </InvalidStyle>--%>
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Status">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxTextBox ID="txtStatus" runat="server" ReadOnly="true" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Date Completed">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxDateEdit ID="dtDateCompleted" runat="server" Width="170px" ReadOnly="true" DropDownButton-Enabled="false">
                                                                                    <DropDownButton Enabled="False"></DropDownButton>
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Receiving Warehouse">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridLookup ID="glReceivingWarehouse" ClientInstanceName="CINReceivingWarehouse" runat="server" Width="170px" DataSourceID="ReceivingWarehouselookup" KeyFieldName="WarehouseCode" TextFormatString="{0}">
                                                                                    <GridViewProperties>
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                                        <Settings ShowFilterRow="True" />
                                                                                    </GridViewProperties>
                                                                                    <ClientSideEvents Validation="OnValidation" ValueChanged="OnInitTrans"/>
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
                                                                                <dx:ASPxDateEdit ID="dtCommitmentDate" ClientInstanceName="CINCommitmentDate" runat="server" Width="170px">
                                                                                    <ClientSideEvents Validation="OnValidation" DateChanged="SetDefaultCancellation"/>
                                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                                        <RequiredField IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                   <%--   <InvalidStyle BackColor="Pink">
                                                                                    </InvalidStyle>--%>
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Contact Person">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxTextBox ID="txtContactPerson" ClientInstanceName="CINContactPerson" runat="server" Width="170px">
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

                                                                     <dx:LayoutItem Caption="Cancellation Date">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxDateEdit ID="dtCancellationDate" ClientInstanceName="CINCancellationDate" runat="server" Width="170px">
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Quotation Number">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridLookup ID="glQuotationNumber" runat="server" Width="170px" DataSourceID="QuotationNumberlookup" KeyFieldName="DocNumber" TextFormatString="{0}">
                                                                                    <GridViewProperties>
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                                        <Settings ShowFilterRow="True" />
                                                                                    </GridViewProperties>
                                                                                        <%--<Columns>
                                                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="10px">
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Quotation Number" FieldName="DocNumber" ShowInCustomizationForm="True" VisibleIndex="1" Width="5px">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>--%>
                                                                                </dx:ASPxGridLookup>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                             
                                                                     <%--<dx:LayoutItem Caption="Remarks">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxTextBox ID="txtRemarks" runat="server"  Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>--%>

                                                                    <dx:LayoutItem Caption="Broker">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridLookup ID="glBroker" runat="server" ClientInstanceName="CINBroker"  Width="170px" 
                                                                                    DataSourceID="SupplierCodelookup" KeyFieldName="SupplierCode" TextFormatString="{0}" OnInit="glBroker_Init">
                                                                                    <GridViewProperties>
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                                        <Settings ShowFilterRow="True" />
                                                                                        <SettingsPager PageSize="5"></SettingsPager>
                                                                                    </GridViewProperties>
                                                                                    <ClientSideEvents DropDown="function(){
                                                                                        CINBroker.GetGridView().PerformCallback();
                                                                                        }" />
                                                                                        <Columns>
					                                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="1px">
					                                                                        </dx:GridViewCommandColumn>
					                                                                        <dx:GridViewDataTextColumn Caption="Broker" FieldName="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="1" Width="5px">
					                                                                        </dx:GridViewDataTextColumn>
					                                                                        <dx:GridViewDataTextColumn Caption="Name" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2" Width="5px">
					                                                                        </dx:GridViewDataTextColumn>
				                                                                        </Columns>
                                                                                </dx:ASPxGridLookup>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Remarks">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxMemo ID="memoremarks" runat="server" Width="170px" Height="50">
                                                                                </dx:ASPxMemo>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="PR Number:">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridLookup ID="glPRNumber" runat="server" Width="170px" DataSourceID="PRNumberlookup" AutoGenerateColumns="False" KeyFieldName="DocNumber" SelectionMode="Multiple" TextFormatString="{0}"
                                                                                     ClientInstanceName="prnum" OnInit="glPRNumber_Init">
                                                                                    <ClientSideEvents DropDown="function(){
                                                                                        prnum.GetGridView().PerformCallback();
                                                                                        }" />
                                                                                    <GridViewProperties>
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                        <Settings ShowFilterRow="True" />
                                                                                        <SettingsPager PageSize="5"></SettingsPager>
                                                                                    </GridViewProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="1">
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PR Number" FieldName="DocNumber" ShowInCustomizationForm="True" VisibleIndex="1" Width="50px">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                </dx:ASPxGridLookup>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem> 

                                                                    <dx:LayoutItem Caption="With PR">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxCheckBox ID="chkIsWithPR" runat="server" ClientInstanceName="cbiswithpr" CheckState="Unchecked">
                                                                                    <ClientSideEvents CheckedChanged="checkedchanged"/>
                                                                                </dx:ASPxCheckBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="With RefNumber">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxCheckBox ID="chkIsWithInvoice" runat="server" CheckState="Unchecked">
                                                                                </dx:ASPxCheckBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Printed">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxCheckBox ID="chkIsPrinted" runat="server" CheckState="Unchecked" ReadOnly="true">
                                                                                </dx:ASPxCheckBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Released">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxCheckBox ID="chkIsReleased" runat="server" CheckState="Unchecked" ReadOnly="true">
                                                                                </dx:ASPxCheckBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="" Name="Genereatebtn">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxButton ID="Generatebtn" ClientInstanceName="CINGenerate" runat="server" Width="170px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" onload="Generate_Btn" ClientVisible="true" Text="Generate" Theme="MetropolisBlue">
                                                                                    <ClientSideEvents Click="Generate" />
                                                                                </dx:ASPxButton>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                </Items>
                                                            </dx:LayoutGroup>   <%--END OF Information LayoutGroup--%>

                                                            <dx:LayoutGroup Caption="Amount" ColCount="2">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Currency">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridLookup ID="txtCurrency" runat="server" Width="170px" DataSourceID="Currencylookup" KeyFieldName="Currency"
                                                                                    AutoGenerateColumns="true" TextFormatString="{0}">
                                                                                    <GridViewProperties>                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                        <Settings ColumnMinWidth="50" ShowFilterRow="True" />
                                                                                    </GridViewProperties>
                                                                                </dx:ASPxGridLookup>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Terms">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinTerms" ClientInstanceName="CINTerms" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Exchange Rate">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinExchangeRate" ClientInstanceName="CINExchangeRate" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="autocalculate" />
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Total Freight">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinTotalFreight" ClientInstanceName="CINTotalFreight" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="detailautocalculate" />
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Withholding Tax">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinWithholdingTax" ClientInstanceName="CINWithholdingTax" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Total Quantity">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinTotalQty" ClientInstanceName="CINTotalQty" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                    
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="VAT Amount">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinVATAmount" ClientInstanceName="CINVATAmount" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                    <ClientSideEvents ValueChanged="autocalculate" /> 
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Peso Amount">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinPesoAmount" ClientInstanceName="CINPesoAmount" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                </dx:ASPxSpinEdit> 
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>



                                                                     <dx:LayoutItem Caption="Gross VATable Amount">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinGrossVATableAmount" ClientInstanceName="CINGrossVATableAmount" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                    <ClientSideEvents NumberChanged="autocalculate" />
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                     <dx:LayoutItem Caption="Foreign Amount">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinForeignAmount" ClientInstanceName="CINForeignAmount" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                </dx:ASPxSpinEdit> 
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="NonVATable Amount">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxSpinEdit ID="spinNonVATableAmount" ClientInstanceName="CINNonVATableAmount" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00"  DisplayFormatString="{0:N}">
                                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                                    <ClientSideEvents NumberChanged="autocalculate" />
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                </Items>
                                                            </dx:LayoutGroup>   <%--END OF Amount LayoutGroup--%>
                                                        </Items>
                                                    </dx:LayoutGroup>   <%--END OF General LayoutGroup--%>
                                                    <dx:LayoutGroup Caption="User Defined" ColCount="2">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Field1">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField1" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField2" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField3" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field4">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField4" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field5">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField5" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field6">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField6" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field7">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField7" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field8">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField8" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Field9">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHField9" runat="server">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>   <%--END OF User Defined LayoutGroup--%>
                                                    <dx:LayoutGroup Caption="Audit Trail" ColSpan="2" ColCount="2">
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
                                                            <dx:LayoutItem Caption="Submitted By">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHSubmittedBy" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Submitted Date">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtHSubmittedDate" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                             <dx:LayoutItem Caption="Cancelled By">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtCancelledBy" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Cancelled Date">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtCancelledDate" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Force Closed By">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtForceClosedBy" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Force Closed Date">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtForceClosedDate" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Released By">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtReleasedBy" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Released Date">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txtReleasedDate" runat="server" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>   <%--END OF Audit Trail LayoutGroup--%>

                                                    <dx:LayoutGroup Caption="Reference Transaction" Name="ReferenceTransaction">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Reference Detail">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                                <dx:ASPxGridView ID="gvRef" runat="server" AutoGenerateColumns="False" KeyFieldName="RTransType;REFDocNumber;TransType;DocNumber" Width="860px" ClientInstanceName="gvRef" SettingsBehavior-AllowSort="False">
                                                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" CustomButtonClick="OnCustomClick"  Init="OnInitTrans" />
                                                                                    <SettingsPager PageSize="5">
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="Batch">
                                                                                    </SettingsEditing>
                                                                                    <SettingsBehavior ColumnResizeMode="NextColumn" FilterRowMode="OnClick" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DocNumber" FieldName="DocNumber" Name="DocNumber" ShowInCustomizationForm="True" VisibleIndex="5" ReadOnly="True">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Reference TransType" FieldName="RTransType" Name="RTransType" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0" Width="90px" ShowUpdateButton="True" ShowCancelButton="False">
                                                                                            <CustomButtons>
                                                                                                <dx:GridViewCommandColumnCustomButton ID="ViewReferenceTransaction">
                                                                                                    <Image IconID="functionlibrary_lookupreference_16x16">
                                                                                                    </Image>
                                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                                                <dx:GridViewCommandColumnCustomButton ID="ViewTransaction">
                                                                                                    <Image IconID="find_find_16x16">
                                                                                                    </Image>
                                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                                            </CustomButtons>
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Reference DocNumber" FieldName="REFDocNumber" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="TransType" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly="True">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="RCommandString" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn FieldName="CommandString" ShowInCustomizationForm="True" VisibleIndex="6">
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
                                                </Items>
                                            </dx:TabbedLayoutGroup>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>    <%--END OF HEADER LAYOUTITEM--%>


            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxFormLayout ID="GridForm" runat="server" Height="400px" Width="1280px" style="margin-left: -3px">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                            <Items>
                                <dx:LayoutGroup Caption="Purchase Order Detail">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False"  Width="770px"
                                                        OnCommandButtonInitialize="gv_CommandButtonInitialize" ClientInstanceName="gv1"
                                                        OnRowValidating="grid_RowValidating" OnBatchUpdate="gv1_BatchUpdate"  OnCustomButtonInitialize="gv1_CustomButtonInitialize" 
                                                        OnInit="gv1_Init" SettingsBehavior-AllowSort="False" OnCustomCallback="gv1_CustomCallback">
                                                        <ClientSideEvents CustomButtonClick="OnCustomClick" Init="autocalculate" EndCallBack="gridView_EndCallback" />
                                                        <ClientSideEvents BatchEditConfirmShowing="OnConfirm" BatchEditStartEditing="OnStartEditing" 
                                                            BatchEditEndEditing="OnEndEditing" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                        <SettingsPager Mode="ShowAllRecords" /> 
                                                         <SettingsEditing Mode="Batch"/>
                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="200"  /> 

<SettingsBehavior AllowSort="False"></SettingsBehavior>

                                                        <SettingsCommandButton>
                                                                        <NewButton>
                                                                        <Image IconID="actions_addfile_16x16"></Image>
                                                                        </NewButton>
                                                                        <EditButton>
                                                                        <Image IconID="actions_addfile_16x16"></Image>
                                                                        </EditButton>
                                                                        <DeleteButton>
                                                                        <Image IconID="actions_cancel_16x16"></Image>
                                                                        </DeleteButton>
                                                                    </SettingsCommandButton>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="DocNumber" Visible="False" Width="0px" VisibleIndex="1">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxTextBox ID="txtDocNumber" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                       TextFormatString="{0}" Width="0px" ReadOnly="true">
                                                                    </dx:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                        
                                                            <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Width="100px" Name="LineNumber" ReadOnly="True">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxTextBox ID="glpLineNumber" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                        KeyFieldName="LineNumber" ClientInstanceName="gl7" TextFormatString="{0}" Width="100px" ReadOnly="true">
                                                                    </dx:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>

                                                             <dx:GridViewDataTextColumn FieldName="PRNumber" Name="PRNumber" ShowInCustomizationForm="True" Visible="true" VisibleIndex="3" Width="0px" ReadOnly="True" Caption="PRNumber">
                                                               <EditItemTemplate>
                                                                    <dx:ASPxTextBox ID="PRNumber" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                        ClientInstanceName="glPRNumber" TextFormatString="{0}" Width="100px" ReadOnly="true">
                                                                    </dx:ASPxTextBox>
                                                                </EditItemTemplate>
                                                             </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataTextColumn FieldName="ItemCode" VisibleIndex="4" Width="150px" Name="glpItemCode" ReadOnly="True">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxGridLookup ID="glItemCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false"
                                                                        DataSourceID="sdsItem" KeyFieldName="ItemCode" ClientInstanceName="gl" TextFormatString="{0}" Width="150px" OnInit="glItemCode_Init">
                                                                        <GridViewProperties Settings-ShowFilterRow="true">
                                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                                                AllowSelectSingleRowOnly="True" />
                                                                        </GridViewProperties>
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="ItemCode" ReadOnly="True" VisibleIndex="0" Settings-AutoFilterCondition="Contains" />
                                                                            <dx:GridViewDataTextColumn FieldName="FullDesc" ReadOnly="True" VisibleIndex="1" Settings-AutoFilterCondition="Contains" />
                                                                        </Columns>
                                                                        <ClientSideEvents KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" DropDown="lookup" 
                                                                         EndCallback="GridEndChoice" />
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="ColorCode" VisibleIndex="5" Width="100px" Name="ColorCode" ReadOnly="True">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxGridLookup ID="glColorCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                        KeyFieldName="ColorCode" ClientInstanceName="gl2" TextFormatString="{0}" Width="100px" OnInit="lookup_Init">
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
                                                                            //e.processOnServer = false;
                                                                            }" ValueChanged="gridLookup_CloseUp"/>
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>


                                                            <dx:GridViewDataTextColumn FieldName="ClassCode" VisibleIndex="6" Width="100px" Name="ClassCode" ReadOnly="True">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxGridLookup ID="glClassCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" OnInit="lookup_Init"
                                                                    KeyFieldName="ClassCode" ClientInstanceName="gl3" TextFormatString="{0}" Width="100px">
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
                                                                            }" ValueChanged="gridLookup_CloseUp"/>
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                        
                                                            <dx:GridViewDataTextColumn FieldName="SizeCode" VisibleIndex="7" Width="100px" Name="SizeCode" ReadOnly="True">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxGridLookup ID="glSizeCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" OnInit="lookup_Init"
                                                                    KeyFieldName="SizeCode" ClientInstanceName="gl4" TextFormatString="{0}" Width="100px">
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
                                                                            }" ValueChanged="gridLookup_CloseUp"/>
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                       
                                                          <dx:GridViewDataTextColumn FieldName="FullDesc" VisibleIndex="8" Width="300px" Caption="Item Description">   
                                                                  <EditItemTemplate>
                                                                    <dx:ASPxTextBox ID="glFullDesc" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                        KeyFieldName="FullDesc" ClientInstanceName="gl6" TextFormatString="{0}" Width="300px" Readonly="true">
                                                                    </dx:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataSpinEditColumn FieldName="OrderQty" Name="OrderQty"  ShowInCustomizationForm="True" VisibleIndex="9" Width="80px" UnboundType="Bound">
                                                                <PropertiesSpinEdit ClientInstanceName="CINOrderQtyDetail" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                    <ClientSideEvents ValueChanged="autocalculate" NumberChanged="orderqtynegativecheck" />
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>


                                                            <dx:GridViewDataTextColumn FieldName="Unit" VisibleIndex="10" Width="80px" Caption="Unit">   
                                                                  <EditItemTemplate>
                                                                    <dx:ASPxGridLookup ID="glUnitBase" runat="server" AutoGenerateColumns="False" AutoPostBack="false" 
                                                                        KeyFieldName="Unit" DataSourceID="Unitlookup" ClientInstanceName="gl5" TextFormatString="{0}" Width="80px">
                                                                        <GridViewProperties Settings-ShowFilterRow="true">
                                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                        </GridViewProperties>
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="Unit" ReadOnly="True" VisibleIndex="0">
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1">
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataSpinEditColumn FieldName="UnitCost" Name="UnitCost" ShowInCustomizationForm="True" VisibleIndex="11" Width="80px">
                                                                <PropertiesSpinEdit ClientInstanceName="CINUnitCostDetail" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                    <ClientSideEvents ValueChanged="autocalculate" NumberChanged="unitcostnegativecheck" />
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>

                                                            <dx:GridViewDataSpinEditColumn FieldName="ReceivedQty" Name="ReceivedQty" ShowInCustomizationForm="True" VisibleIndex="12" Width="80px" ReadOnly="true">
                                                                <PropertiesSpinEdit ClientInstanceName="txtReceivedQty" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                    </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>

                                                            <dx:GridViewDataSpinEditColumn FieldName="UnitFreight" Name="glpUnitFreight" ShowInCustomizationForm="True" VisibleIndex="13" Width="80px">
                                                                <PropertiesSpinEdit ClientInstanceName="CINUnitFreightDetail" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                    <ClientSideEvents ValueChanged="autocalculate" NumberChanged="negativecheck" />
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>

                                                            <dx:GridViewDataCheckColumn Caption="VAT Liable" FieldName="IsVat" Name="glpIsVat" ShowInCustomizationForm="True" VisibleIndex="14">
                                                                <PropertiesCheckEdit ClientInstanceName="glIsVat" >
                                                                    <ClientSideEvents CheckedChanged="function(s,e){ 
                                                                        gv1.batchEditApi.EndEdit(); 
                                                                        if (s.GetChecked() == false) 
                                                                        {
                                                                            gv1.batchEditApi.SetCellValue(index, 'VATCode', 'NONV');
                                                                            gv1.batchEditApi.SetCellValue(index, 'Rate', '0');
                                                                        }
                                                                        autocalculate();
                                                                        }" />
                                                                </PropertiesCheckEdit>
              
                                                            </dx:GridViewDataCheckColumn>


                                                            <dx:GridViewDataTextColumn FieldName="VATCode" VisibleIndex="15" Width="80px" Caption="VATCode">   
                                                                  <EditItemTemplate>
                                                                      <dx:ASPxGridLookup ID="glVATCode" runat="server" DataSourceID="VatCodeLookup"  AutoGenerateColumns="False" AutoPostBack="false" 
                                                                        KeyFieldName="TCode" ClientInstanceName="glVATCode" TextFormatString="{0}" Width="80px">
                                                                        <GridViewProperties Settings-ShowFilterRow="true">
                                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                        </GridViewProperties>
                                                                       <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="TCode" ReadOnly="True" VisibleIndex="0" >
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                             </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Description" ReadOnly="True" VisibleIndex="1" >
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                             </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Rate" ReadOnly="True" VisibleIndex="2" >
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                             </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <ClientSideEvents DropDown="lookup" KeyDown="gridLookup_KeyDown" KeyPress="gridLookup_KeyPress" RowClick="function(s,e){
                                                                                console.log('rowclick');
                                                                                    setTimeout(function(){
                                                                                closing = true;
                                                                                gl2.GetGridView().PerformCallback('VATCode' + '|' + glVATCode.GetValue() + '|' + 'code');
                                                                                e.processOnServer = false;
                                                                                valchange_VAT = true
                                                                                }, 500);
                                                                              }" />
                                                                    </dx:ASPxGridLookup>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataCheckColumn Caption="Partial Delivery" FieldName="IsAllowPartial" Name="glpIsAllowPartial" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            </dx:GridViewDataCheckColumn>

                                                            <%--<dx:GridViewDataTextColumn FieldName="Rate" VisibleIndex="17" Width="100px">
                                                                <PropertiesTextEdit ClientInstanceName="txtRate"></PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>--%>

                                                            <dx:GridViewDataSpinEditColumn FieldName="Rate" Name="Rate" ShowInCustomizationForm="True" VisibleIndex="17" Width="0px">
                                                                <PropertiesSpinEdit ClientInstanceName="txtRate" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Width="0px" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>

                                                            <%--<dx:GridViewDataTextColumn FieldName="ATCRate" VisibleIndex="18" Width="100px">
                                                                <PropertiesTextEdit ClientInstanceName="txtATCRate"></PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>--%>

                                                            <dx:GridViewDataSpinEditColumn FieldName="ATCRate" Name="ATCRate" ShowInCustomizationForm="True" VisibleIndex="18" Width="0px">
                                                                <PropertiesSpinEdit ClientInstanceName="txtATCRate" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Width="0px" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>

                                                            <dx:GridViewDataTextColumn FieldName="Field1" Name="glpDField1" ShowInCustomizationForm="True" VisibleIndex="19" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field2" Name="glpDField2" ShowInCustomizationForm="True" VisibleIndex="20" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field3" Name="glpDField3" ShowInCustomizationForm="True" VisibleIndex="21" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field4" Name="glpDField4" ShowInCustomizationForm="True" VisibleIndex="22" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field5" Name="glpDField5" ShowInCustomizationForm="True" VisibleIndex="23" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field6" Name="glpDField6" ShowInCustomizationForm="True" VisibleIndex="24" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field7" Name="glpDField7" ShowInCustomizationForm="True" VisibleIndex="25" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field8" Name="glpDField8" ShowInCustomizationForm="True" VisibleIndex="26" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Field9" Name="glpDField9" ShowInCustomizationForm="True" VisibleIndex="27" Width="100px">
                                                            </dx:GridViewDataTextColumn>


                                                            <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True"  ShowNewButtonInHeader="True" VisibleIndex="0" Width="80px">
                                                                    <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="Details">
                                                                       <Image IconID="support_info_16x16" ToolTip="Countsheet"></Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                   <%-- <dx:GridViewCommandColumnCustomButton ID="CountSheet">
                                                                       <Image IconID="arrange_withtextwrapping_topleft_16x16" ToolTip="Countsheet"></Image>
                                                                    </dx:GridViewCommandColumnCustomButton> --%>
                                                                     <dx:GridViewCommandColumnCustomButton ID="Delete">
                                                                        <Image IconID="actions_cancel_16x16"> </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                </dx:GridViewCommandColumn>
                                                        </Columns>
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
            </dx:LayoutItem> <%--END OF GRID LAYOUTITEM--%>

 
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

    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Calculating..."
        ClientInstanceName="loader" ContainerElementID="gv1" Modal="true">
        <LoadingDivStyle Opacity="0"></LoadingDivStyle>
   </dx:ASPxLoadingPanel>
        



</form>
<form id="form2" runat="server" visible="false">
    <asp:ObjectDataSource ID="odsHeader" runat="server" DataObjectTypeName="Entity.PurchasedOrder" InsertMethod="InsertData" SelectMethod="getdata" TypeName="Entity.PurchasedOrder" UpdateMethod="UpdateData" DeleteMethod="Deletedata">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--Purchase Order ADD / UPDATE / EDIT Methods--%>
    <asp:ObjectDataSource ID="odsDetail" runat="server" DataObjectTypeName="Entity.PurchasedOrder+PurchasedOrderDetail" SelectMethod="getdetail" UpdateMethod="UpdatePurchasedOrderDetail" TypeName="Entity.PurchasedOrder+PurchasedOrderDetail" DeleteMethod="DeletePurchasedOrderDetail" InsertMethod="AddPurchasedOrderDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="odsReference" runat="server" SelectMethod="getreftransaction" TypeName="Entity.PurchasedOrder+RefTransaction" >
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select * from Procurement.PurchaseOrderDetail where DocNumber is null"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="Masterfileitemdetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
         OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsItem" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
         OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsItemDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsPicklistDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
      <%--Supplier Code Look Up--%>
    <asp:SqlDataSource ID="SupplierCodelookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <%--Contact Person Code Look Up--%>
    <%--<asp:SqlDataSource ID="ContactPersonlookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>--%>
    <%--Currency Look Up--%>
    <asp:SqlDataSource ID="Currencylookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select Currency,CurrencyName from masterfile.Currency where ISNULL(IsInactive,0)!=1"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <%--Contact Person Code Look Up--%>
    <asp:SqlDataSource ID="Termslookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT APTerms,SupplierCode FROM Masterfile.BPSupplierInfo WHERE ISNULL([IsInactive],0) = 0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <%--Receiving Warehouse Code Look Up--%>
    <asp:SqlDataSource ID="ReceivingWarehouselookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] WHERE ISNULL([IsInactive],0) = 0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <%--PR Number Look Up--%>
    <asp:SqlDataSource ID="PRNumberlookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <%--Quotation Number Look Up--%>
    <asp:SqlDataSource ID="QuotationNumberlookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DocNumber FROM Procurement.[Quotation] WHERE ISNULL(SubmittedBy,'')!='' AND Status = 'A'"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
        <%--Quotation Number Look Up--%>
    <asp:SqlDataSource ID="Unitlookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="Select DISTINCT UnitCode AS Unit, Description from masterfile.Unit where ISNULL(IsInactive, 0)=0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
   <asp:SqlDataSource ID="VatCodeLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT TCode,Description,Rate FROM Masterfile.Tax WHERE ISNULL(IsInactive,0) = 0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="Temporary" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ServiceLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="Select ServiceCode,Description,Type,IsVatable from Masterfile.Service where ISNULL(IsInactive, 0)=0"
        OnInit = "Connection_Init">
    </asp:SqlDataSource>
</form>
    <!--#region Region Datasource-->
</body>
</html>


