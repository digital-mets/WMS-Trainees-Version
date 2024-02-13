﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSAM.aspx.cs" Inherits="GWL.frmSAM" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Standard Allowable Minute</title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /> 
        <script src="../js/jquery-1.6.1.min.js" type="text/javascript"></script><%--NEWADD--%>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script><%--NEWADD--%><%--Link to global stylesheet--%>
    <script src="../js/keyboardNavi.js" type="text/javascript"></script><%--Need Add Reference RA--%>
    <script src="../js/lookupGrid.js" type="text/javascript"></script><%--Need Add Reference RA--%>
         <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
       
            #form1 {
            height: 300px; /*Change this whenever needed*/
            }

            .itemimage > div,
            .itemimage > img
            {
                position: absolute;
            }
            .itemimage
            {
                height: 40px;
                width: 80px;
                position: relative;
                border: 2px dashed #808080!important;
                border-radius: 10px;
                cursor: pointer;
            }
            .dragZoneTextItemImage
            {
                height: 50px;
                width: 100px;
                display: table-cell;
                vertical-align: middle;
                text-align: center;
                font-size: 20pt;
            }
              .tabs {
            position: relative;   
            min-height: 500px; /* This part sucks */
            clear: both;
            margin: 25px 0;
        }
        .tab {
            float: left;
        }
        .tab label {
            background: #eee; 
            padding: 10px; 
            border: 1px solid #ccc; 
            margin-left: -1px; 
            position: relative;
            left: 1px; 
        }
        .tab [type=radio] {
            display: none;   
        }
        .content {
            position: absolute;
            top: 28px;
            left: 0;
            background: white;
            right: 0;
            bottom: 0;
            padding: 20px;
            border: 1px solid #ccc; 
        }
        [type=radio]:checked ~ label {
            background: white;
            border-bottom: 1px solid white;
            z-index: 2;
        }
        [type=radio]:checked ~ label ~ .content {
            z-index: 1;
        }
            .embroiderprint > div,
            .embroiderprint > img
            {
                position: absolute;
            }
            .embroiderprint
            {
                height: 150px;
                width: 200px;
                position: relative;
                border: 2px dashed #808080!important;
                border-radius: 10px;
                cursor: pointer;
            }
            .dragZoneTextEmbroiderPrint
            {
                width: 200px;
                height: 150px;
                display: table-cell;
                vertical-align: middle;
                text-align: center;
                font-size: 20pt;
            }
            .dropZoneTextEmbroiderPrint
            {
                width: 200px;
                height: 150px;
                color: #d5d2cc;
                background-color: #2A88AD;
                border-radius: 10px;
            }
            .dropZoneTextEmbroiderPrint,
            .dragZoneTextEmbroiderPrint
            {
                display: table-cell;
                vertical-align: middle;
                text-align: center;
                font-size: 20pt;
            }

            .luisgeneledpao /*cell class*/
        {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
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
                background-color: #2A88AD;
                border-radius: 10px;
            }
            .uploadControlDropZone,
            .hidden
            {
                display: none;
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
            .dxucInlineDropZoneSys span
            {
                color: #fff!important;
                font-size: 10pt;
                font-weight: normal!important;
            }
            .uploadControlProgressBar
            {
                width: 250px!important;
            }
            .validationMessage
            {
                padding: 0 20px;
                text-align: center;
            }
            .uploadControl
            {
             
            }
            .Note
            {
                width: 500px;
            }

            .roundedPopup
             {
                border-radius: 10px;
                box-shadow: 0 0 0 1px rgba(255,255,255,0.25), 0 8px 16px 0px rgba(0,0,0,0.35);
            }
            
    .BrowseButton
        {
            border-radius: 100px;
            text-align: center;
        }

.Entry {
 /*Change this whenever needed
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

        .txtboxInLine {

        float: left;

        }
    </style>

    <script>
        var isValid = true;
        var counterror = 0;

        var module = getParameterByName("transtype");
        var id = getParameterByName("docnumber");
        var entry = getParameterByName("entry");

        $(document).ready(function () {
            PerfStart(module, entry, id);
        });



        function OnValidation(s, e) {
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

        function onload() {
            var type = getParameterByName('type');
            var entry = getParameterByName('entry');

        }


        function OnUpdateClick(s, e) {
            console.log(counterror);
            console.log(isValid);
            var btnmode = btn.GetText();
            if (isValid && counterror < 1 || btnmode == "Close") {
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

        function OnConfirm(s, e) {
            if (e.requestTriggerID === "cp")
                e.cancel = true;
        }

        function gridView_EndCallback(s, e) {
            if (s.cp_success) {
                if (s.cp_valmsg != null) {
                    alert(s.cp_valmsg);
                }
                alert(s.cp_message);
                delete (s.cp_valmsg);
                delete (s.cp_success);
                delete (s.cp_message);
                gv1.CancelEdit();
                gvS.CancelEdit();
                gvF.CancelEdit();
                gvW.CancelEdit();
                gvE.CancelEdit();
                gvP.CancelEdit();
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
                    if (getParameterByName('entry') === 'N') {
                        window.open('../Production/frmSAM.aspx?entry=E&transtype=PRODSAM&parameters=' +
                       '&iswithdetail=0&docnumber=' + txtDocNumber.GetText(), '_blank');
                        window.close();
                    }
                    else {
                        window.location.reload();
                    }
                }
                else {
                    delete (s.cp_close);
                    window.close();
                }
            }
            if (s.cp_delete) {
                delete (cp_delete);
                DeleteControl.Show();
            }

            if (s.cp_generated) {

                gv1.CancelEdit();
                var _indices = gv1.batchEditHelper.GetDataRowIndices();
                for (var i = 0; i < _indices.length; i++) {
                    gv1.DeleteRow(_indices[i]);
                }

                gv1.AddNewRow();

                // **-- EMBRRO --** //
                gvE.CancelEdit();

                var _indicesE = gvE.batchEditHelper.GetDataRowIndices();
                for (var i = 0; i < _indicesE.length; i++) {
                    gvE.DeleteRow(_indicesE[i]);
                }

                gvE.AddNewRow();


                // **-- FINISHING --** //
                gvF.CancelEdit();

                var _indicesF = gvF.batchEditHelper.GetDataRowIndices();
                for (var i = 0; i < _indicesF.length; i++) {
                    gvF.DeleteRow(_indicesF[i]);
                }

                gvF.AddNewRow();

                // **-- PRINTING --** //
                gvP.CancelEdit();

                var _indicesP = gvP.batchEditHelper.GetDataRowIndices();
                for (var i = 0; i < _indicesP.length; i++) {
                    gvP.DeleteRow(_indicesP[i]);
                }

                gvP.AddNewRow();

                //// **-- SEWING --** //
                gvS.CancelEdit();

                var _indicesS = gvS.batchEditHelper.GetDataRowIndices();
                for (var i = 0; i < _indicesS.length; i++) {
                    gvS.DeleteRow(_indicesS[i]);
                }

                gvS.AddNewRow();







                // **-- WASHING --** //
                gvW.CancelEdit();

                var _indicesW = gvW.batchEditHelper.GetDataRowIndices();
                for (var i = 0; i < _indicesW.length; i++) {
                    gvW.DeleteRow(_indicesW[i]);
                }

                gvW.AddNewRow();





                var _refindices = gvRefC.batchEditHelper.GetDataRowIndices();
                for (var i = 0; i < _refindices.length; i++) {

                    if (gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OperationType') == "Cutting") {

                        gv1.AddNewRow();
                        _indices = gv1.batchEditHelper.GetDataRowIndices();

                        var sam = 0.00;

                        sam = parseFloat(gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime')) * (1 + (parseFloat(speAllowanceC.GetValue() || 0) / 100.0));

                        gv1.batchEditApi.SetCellValue(_indices[0], 'OperationCode', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OBCode'));
                        gv1.batchEditApi.SetCellValue(_indices[0], 'Steps', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Step'));
                        gv1.batchEditApi.SetCellValue(_indices[0], 'Parts', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Parts'));
                        gv1.batchEditApi.SetCellValue(_indices[0], 'OpsBreakdown', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OpsDescription'));
                        gv1.batchEditApi.SetCellValue(_indices[0], 'MachineType', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'MachineType'));
                        gv1.batchEditApi.SetCellValue(_indices[0], 'ObservedTime', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime'));
                        gv1.batchEditApi.SetCellValue(_indices[0], 'Video', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Video'));
                        gv1.batchEditApi.SetCellValue(_indices[0], 'SAM', sam.toFixed(2));
                    }

                    else if (gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OperationType') == "Embroidery") {

                        gvE.AddNewRow();
                        _indicesE = gvE.batchEditHelper.GetDataRowIndices();

                        var sam = 0.00;

                        sam = parseFloat(gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime')) * (1 + (parseFloat(speAllowanceE.GetValue() || 0) / 100.0));

                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'OperationCode', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OBCode'));
                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'Steps', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Step'));
                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'Parts', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Parts'));
                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'OpsBreakdown', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OpsDescription'));
                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'MachineType', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'MachineType'));
                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'ObservedTime', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime'));
                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'Video', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Video'));
                        gvE.batchEditApi.SetCellValue(_indicesE[0], 'SAM', sam.toFixed(2));


                    }
                    else if (gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OperationType') == "Finishing") {


                        gvF.AddNewRow();
                        _indicesF = gvF.batchEditHelper.GetDataRowIndices();

                        sam = parseFloat(gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime')) * (1 + (parseFloat(speAllowanceF.GetValue() || 0) / 100.0));

                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'OperationCode', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OBCode'));
                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'Steps', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Step'));
                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'Parts', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Parts'));
                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'OpsBreakdown', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OpsDescription'));
                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'MachineType', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'MachineType'));
                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'ObservedTime', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime'));
                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'Video', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Video'));
                        gvF.batchEditApi.SetCellValue(_indicesF[0], 'SAM', sam.toFixed(2));


                    }
                    else if (gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OperationType') == "Printing") {

                        gvP.AddNewRow();
                        _indicesP = gvP.batchEditHelper.GetDataRowIndices();

                        sam = parseFloat(gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime')) * (1 + (parseFloat(speAllowanceP.GetValue() || 0) / 100.0));

                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'OperationCode', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OBCode'));
                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'Steps', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Step'));
                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'Parts', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Parts'));
                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'OpsBreakdown', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OpsDescription'));
                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'MachineType', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'MachineType'));
                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'ObservedTime', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime'));
                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'Video', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Video'));
                        gvP.batchEditApi.SetCellValue(_indicesP[0], 'SAM', sam.toFixed(2));


                    }
                    else if (gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OperationType') == "Sewing") {


                        gvS.AddNewRow();
                        _indicesS = gvS.batchEditHelper.GetDataRowIndices();


                        sam = parseFloat(gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime')) * (1 + (parseFloat(speAllowanceS.GetValue() || 0) / 100.0));

                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'OperationCode', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OBCode'));
                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'Steps', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Step'));
                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'Parts', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Parts'));
                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'OpsBreakdown', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OpsDescription'));
                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'MachineType', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'MachineType'));
                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'ObservedTime', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime'));
                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'Video', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Video'));
                        gvS.batchEditApi.SetCellValue(_indicesS[0], 'SAM', sam.toFixed(2));


                    }

                    else if (gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OperationType') == "Washing") {

                        gvW.AddNewRow();
                        _indicesW = gvW.batchEditHelper.GetDataRowIndices();

                        sam = parseFloat(gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime')) * (1 + (parseFloat(speAllowanceW.GetValue() || 0) / 100.0));

                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'OperationCode', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OBCode'));
                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'Steps', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Step'));
                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'Parts', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Parts'));
                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'OpsBreakdown', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'OpsDescription'));
                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'MachineType', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'MachineType'));
                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'ObservedTime', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'ObservedTime'));
                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'Video', gvRefC.batchEditApi.GetCellValue(_refindices[i], 'Video'));
                        gvW.batchEditApi.SetCellValue(_indicesW[0], 'SAM', sam.toFixed(2));


                    }



                }
                gv1.batchEditApi.EndEdit();
                gv1.DeleteRow(-1);     // First added dummy record
                gvE.batchEditApi.EndEdit();
                gvE.DeleteRow(-1);     // First added dummy record
                gvF.batchEditApi.EndEdit();
                gvF.DeleteRow(-1);     // First added dummy record
                gvP.batchEditApi.EndEdit();
                gvP.DeleteRow(-1);     // First added dummy record
                gvS.batchEditApi.EndEdit();
                gvS.DeleteRow(-1);     // First added dummy record
                gvW.batchEditApi.EndEdit();
                gvW.DeleteRow(-1);     // First added dummy record



                autocalculate();
                autocalculateE();
                autocalculateS();
                autocalculateF();
                autocalculateW();

                autocalculateP();

                delete (s.cp_generated);



            }
        }

        //Image upload
        function FrontImageUploadComplete(s, e) {
            if (e.isValid)

                CINFrontImage.SetImageProperties(e.callbackData, e.callbackData, '', '');
            var imagebinary = e.callbackData.replace('data:image/jpg;base64,', '');
            CINFrontImage64string.SetText(imagebinary);
            //UploadControl.SetText(e.callbackData);
            //UploadControl.
            //setElementVisible("uploadedImage", e.isValid);
        }

        function BackImageUploadComplete(s, e) {
            if (e.isValid)
                // CINBackImage.SetImageUrl(e.callbackData);
                CINBackImage.SetImageProperties(e.callbackData, e.callbackData, '', '');
            var imagebinary = e.callbackData.replace('data:image/jpg;base64,', '');
            CINBackImage64string.SetText(imagebinary);
        }

        function setElementVisible(elementId, visible) {
            document.getElementById(elementId).className = visible ? "" : "hidden";
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function OnBtnSplitClick(s, e) {
            popupMenu.ShowAtElement(btn.GetMainElement());
        }

        function OnItemClick(s, e) {
            btn.SetText(e.item.GetText());
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
            //document.getElementById("badtrip").click();
            //gvJournal.SetWidth(width - 100);
            gv2.SetWidth(width - 100);

            var height = Math.max(0, document.documentElement.clientHeight);

            gv2.SetHeight(-100);


            //CINgvEmbroiderDetail.SetWidth(width - 120);
        }
        var isBusy = false;
        var index;
        var closing;
        var itemc; //variable required for lookup
        var valchange = false;
        var valchange2 = false;
        var valchange3 = false;
        var gridcheck;
        var currentColumn = null;
        var isSetTextRequired = false;
        var linecount = 1;
        var foclum; //variable required for lookup/keyboard plugin
        function OnStartEditing(s, e) {//On start edit grid function     
            //RA Start need sa lookup

            if (!isError) {
                lookGrid = s;
                keybGrid = s;
                foclum = e.focusedColumn.fieldName;
            }
            //RA END

            //RA START COMMENT OUT CODE NOT RELATED
            //var cellInfo = e.rowValues[e.focusedColumn.index];

            //console.log('onstart');
            //var entry = getParameterByName('entry');
            //RA END
            index = e.visibleIndex;

            if (entry != "V" && entry != "D") {
                currentColumn = e.focusedColumn;
                var cellInfo = e.rowValues[e.focusedColumn.index];

                itemc = s.batchEditApi.GetCellValue(e.visibleIndex, "OperationCode"); //needed var for all lookups; this is where the lookups vary for

                index = e.visibleIndex;

                // Need to check nakakaaffect sa cascading RA
                //if (e.focusedColumn.fieldName == 'DocNumber') {
                //    e.cancel = true;
                //    s.batchEditApi.StartEdit(e.visibleIndex, 3); //Edit this to always start on the cell that will be first edited
                //}


                // RA START
                if (isError) {//Prevents editing of other rows other than the row with error
                    if (((errcol != e.focusedColumn.fieldName && errindex != e.visibleIndex) ||
                       (errcol == e.focusedColumn.fieldName && errindex != e.visibleIndex)) ||
                       (errGrid != null && s != errGrid)) {
                        e.cancel = true;
                    }
                }

            }
            else
                e.cancel = true;

            keyboardOnStart(e); //lookupGrid reference func
        }

        function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup

            if (entry != "V") {

                // RA START Lookup
                var cellInfo = e.rowValues[currentColumn.index];
                foclum = currentColumn.fieldName;
                index2 = index;
                if (currentColumn.fieldName === "OperationCode") {
                    OnTextChanged(glOBcode, s);
                    if (cback && !isError) {//Allow auto detail if no Error found
                        valchange2 = true;
                        CallbackFunc.PerformCallback('OBCode' + '|' + cellInfo.value + '|' + 'code');
                        keyOff = true;
                    }
                }
            }
            keyboardOnEnd(); //lookupGrid reference func //RA 
        }


        var val;
        var temp;
        function ProcessCells(selectedIndex, e, column, s) {
            if (val == null) {
                val = ";;;;";
                temp = val.split(';');
            }

            if (selectedIndex == 0) {
                if (gridcheck == 1) {
                    if (column.fieldName == "Steps") {
                        s.batchEditApi.SetCellValue(index, column.fieldName, temp[0]);
                    }
                    if (column.fieldName == "Parts") {
                        s.batchEditApi.SetCellValue(index, column.fieldName, temp[1]);
                    }
                    if (column.fieldName == "OpsBreakdown") {
                        s.batchEditApi.SetCellValue(index, column.fieldName, temp[2]);
                    }
                    if (column.fieldName == "MachineType") {
                        s.batchEditApi.SetCellValue(index, column.fieldName, temp[3]);
                    }
                    if (column.fieldName == "ObservedTime") {
                        s.batchEditApi.SetCellValue(index, column.fieldName, temp[4]);
                    }
                    if (column.fieldName == "SAM") {

                        var sam = 0.00
                        sam = parseFloat(temp[4]) * (1 + (parseFloat(speAllowanceC.GetValue() || 0) / 100.0));

                        s.batchEditApi.SetCellValue(index, column.fieldName, sam.toFixed(2));
                    }
                    if (column.fieldName == "Video") {
                        s.batchEditApi.SetCellValue(index, column.fieldName, temp[5]);
                    }

                    autocalculate();
                    autocalculateE();
                    autocalculateS();
                    autocalculateF();
                    autocalculateW();

                    autocalculateP();
                }

            }
        }


        function GridEnd(s, e) {
            val = s.cp_codes;

            temp = val.split(';');
            // RA START
            if (valchange2) {
                valchange2 = false;
                closing = false;
                if (index2 == index) {
                    for (var i = 0; i < lookGrid.GetColumnsCount() ; i++) {
                        var column = lookGrid.GetColumn(i);
                        if (column.visible == false || column.fieldName == undefined)
                            continue;
                        gridcheck = 1;
                        ProcessCells(0, e, column, lookGrid);
                    }
                }
                cback = false;
                keyOff = false;
                lookGrid.batchEditApi.EndEdit();
                lookGrid.batchEditApi.StartEdit(index, lookGrid.GetColumnByField(foclum).index);
            }

            // RA END

            loader.Hide();
        }



        function Grid_BatchEditRowValidating(s, e) {//Client side validation. Check empty fields. (only visible fields)
            for (var i = 0; i < gv1.GetColumnsCount() ; i++) {
                var column = s.GetColumn(i);
                var chckd;
                var chckd2;

                //if (column.fieldName == "IsByBulk") {
                //var cellValidationInfo = e.validationInfo[column.index];
                //if (!cellValidationInfo) continue;
                //var value = cellValidationInfo.value;
                ////ASPxClientUtils.Trim(value)                    
                //if (value == true) {
                //chckd2 = true;
                //}
                //}
                //if (column.fieldName == "BulkQty") {
                //var cellValidationInfo = e.validationInfo[column.index];
                //if (!cellValidationInfo) continue;
                //var value = cellValidationInfo.value;
                //if ((!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "" || ASPxClientUtils.Trim(value) == "0" || ASPxClientUtils.Trim(value) == null) && chckd2 == true) {
                //if ((!ASPxClientUtils.IsExists(value) || value == "" || value == "0" || value == "0.00" || value == null) && chckd2 == true) {
                //cellValidationInfo.isValid = false;
                //      cellValidationInfo.errorText = column.fieldName + " is required";
                //      isValid = false;
                //  }
                //}
                //if (column.fieldName == "BulkUnit") {
                // var cellValidationInfo = e.validationInfo[column.index];
                //  if (!cellValidationInfo) continue;
                //  var value = cellValidationInfo.value;
                //  if ((!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) == "" || ASPxClientUtils.Trim(value) == null) && chckd2 == true) {
                //      cellValidationInfo.isValid = false;
                //       cellValidationInfo.errorText = column.fieldName + " is required";
                //       isValid = false;
                //    }
                //}
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
            if (gv1.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }
        }

        function gridLookup_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode == ASPxKey.Enter) {
                gv1.batchEditApi.EndEdit();
            }
        }

        function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
            gv1.batchEditApi.EndEdit();
        }

        function CascadeSName(Value) {

            txtSName.SetText(Value);

        }

        function OnCustomClick(s, e) {

            if (e.buttonID == "Delete") {
                gv1.DeleteRow(e.visibleIndex);
                autocalculate();

            }
            if (e.buttonID == "DeleteS") {
                gvS.DeleteRow(e.visibleIndex);
                autocalculate();

            }
            if (e.buttonID == "DeleteF") {
                gvF.DeleteRow(e.visibleIndex);
                autocalculate();

            }
            if (e.buttonID == "DeleteW") {
                gvW.DeleteRow(e.visibleIndex);
                autocalculate();

            }
            if (e.buttonID == "DeleteE") {
                gvE.DeleteRow(e.visibleIndex);
                autocalculate();

            }
            if (e.buttonID == "DeleteP") {
                gvP.DeleteRow(e.visibleIndex);
                autocalculate();

            }


            if (e.buttonID == "ViewTransaction") {

                var OperationCode = s.batchEditApi.GetCellValue(e.visibleIndex, "OperationCode");

                factbox.SetContentUrl('../FactBox/fbOBPic.aspx?OBCode=' + OperationCode);


            }
            if (e.buttonID == "ViewTransactionS") {

                var OperationCode = s.batchEditApi.GetCellValue(e.visibleIndex, "OperationCode");
                factbox.SetContentUrl('../FactBox/fbOBPic.aspx?OBCode=' + OperationCode);

            }
            if (e.buttonID == "ViewTransactionF") {

                var OperationCode = s.batchEditApi.GetCellValue(e.visibleIndex, "OperationCode");
                factbox.SetContentUrl('../FactBox/fbOBPic.aspx?OBCode=' + OperationCode);

            }
            if (e.buttonID == "ViewTransactionW") {

                var OperationCode = s.batchEditApi.GetCellValue(e.visibleIndex, "OperationCode");
                factbox.SetContentUrl('../FactBox/fbOBPic.aspx?OBCode=' + OperationCode);

            }
            if (e.buttonID == "ViewTransactionE") {

                var OperationCode = s.batchEditApi.GetCellValue(e.visibleIndex, "OperationCode");
                factbox.SetContentUrl('../FactBox/fbOBPic.aspx?OBCode=' + OperationCode);

            }
            if (e.buttonID == "ViewTransactionP") {

                var OperationCode = s.batchEditApi.GetCellValue(e.visibleIndex, "OperationCode");
                factbox.SetContentUrl('../FactBox/fbOBPic.aspx?OBCode=' + OperationCode);

            }



        }


        function Generate(s, e) {

            cp.PerformCallback('Generate');
            e.processOnServer = false;

        }

        function autocalculate(s, e) {


            var observedtime = 0.00;
            var TotalObservedTime = 0.00;
            setTimeout(function () {
                var indicies = gv1.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++) {
                    if (gv1.batchEditHelper.IsNewRow(indicies[i])) {


                        observedtime = parseFloat(gv1.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                        TotalObservedTime += observedtime;

                        var sam = 0.00
                        sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceC.GetValue() || 0) / 100.0));
                        gv1.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                    }
                    else {
                        var key = gv1.GetRowKey(indicies[i]);
                        if (gv1.batchEditHelper.IsDeletedRow(key))
                            console.log("deleted row " + indicies[i]);
                        else {
                            observedtime = parseFloat(gv1.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                            TotalObservedTime += observedtime;


                            var sam = 0.00
                            sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceC.GetValue() || 0) / 100.0));
                            gv1.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                        }
                    }
                }

                speTotalObservedTimeC.SetValue(TotalObservedTime.toFixed(2));
                speBasicMinutesC.SetValue(TotalObservedTime * (parseFloat(speEfficiencyC.GetValue() || 0) / 100.0).toFixed(2));
                speSAMC.SetValue((speBasicMinutesC.GetValue() * (1 + (parseFloat(speAllowanceC.GetValue() || 0) / 100.0))).toFixed(2));
                speLaborCostC.SetValue((speSAMC.GetValue() * (speMinimumWageC.GetValue() / 450)).toFixed(2))
                speCostC.SetValue(speLaborCostC.GetValue() * (parseFloat(speMarkupC.GetValue() || 0) / 100.0).toFixed(2))
                speCuttingCost.SetValue(speLaborCostC.GetValue() * (parseFloat(speMarkupC.GetValue() || 0) / 100.0))
                speCMPCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0))
                speTotalCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0)
                    + parseFloat(speWashing.GetValue() || 0) + parseFloat(speEmbro.GetValue() || 0) + parseFloat(spePrintingCost.GetValue() || 0))



            }, 500);
        }

        function autocalculateS(s, e) {


            var observedtime = 0.00;
            var TotalObservedTime = 0.00;
            setTimeout(function () {
                var indicies = gvS.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++) {
                    if (gvS.batchEditHelper.IsNewRow(indicies[i])) {


                        observedtime = parseFloat(gvS.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                        TotalObservedTime += observedtime;

                        var sam = 0.00
                        sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceS.GetValue() || 0) / 100.0));
                        gvS.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                    }
                    else {
                        var key = gvS.GetRowKey(indicies[i]);
                        if (gvS.batchEditHelper.IsDeletedRow(key))
                            console.log("deleted row " + indicies[i]);
                        else {
                            observedtime = parseFloat(gvS.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                            TotalObservedTime += observedtime;
                            var sam = 0.00
                            sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceS.GetValue() || 0) / 100.0));
                            gvS.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                        }
                    }
                }

                speTotalObservedTimeS.SetValue(TotalObservedTime.toFixed(2));
                speBasicMinutesS.SetValue(TotalObservedTime * (parseFloat(speEfficiencyS.GetValue() || 0) / 100.0).toFixed(2));
                speSAMS.SetValue((speBasicMinutesS.GetValue() * (1 + (parseFloat(speAllowanceS.GetValue() || 0) / 100.0))).toFixed(2));
                speLaborCostS.SetValue((speSAMS.GetValue() * (speMinimumWageS.GetValue() / 450)).toFixed(2))
                speCostS.SetValue(speLaborCostS.GetValue() * (parseFloat(speMarkupS.GetValue() || 0) / 100.0).toFixed(2))
                speSewingCost.SetValue(speLaborCostS.GetValue() * (parseFloat(speMarkupS.GetValue() || 0) / 100.0))
                speCMPCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0))
                speTotalCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0)
                    + parseFloat(speWashing.GetValue() || 0) + parseFloat(speEmbro.GetValue() || 0) + parseFloat(spePrintingCost.GetValue() || 0))

            }, 500);
        }

        function autocalculateF(s, e) {


            var observedtime = 0.00;
            var TotalObservedTime = 0.00;
            setTimeout(function () {
                var indicies = gvF.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++) {
                    if (gvF.batchEditHelper.IsNewRow(indicies[i])) {


                        observedtime = parseFloat(gvF.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                        TotalObservedTime += observedtime;
                        var sam = 0.00
                        sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceF.GetValue() || 0) / 100.0));
                        gvF.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                    }
                    else {
                        var key = gvF.GetRowKey(indicies[i]);
                        if (gvF.batchEditHelper.IsDeletedRow(key))
                            console.log("deleted row " + indicies[i]);
                        else {
                            observedtime = parseFloat(gvF.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                            TotalObservedTime += observedtime;
                            var sam = 0.00
                            sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceF.GetValue() || 0) / 100.0));
                            gvF.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                        }
                    }
                }

                speTotalObservedTimeF.SetValue(TotalObservedTime.toFixed(2));
                speBasicMinutesF.SetValue(TotalObservedTime * (parseFloat(speEfficiencyF.GetValue() || 0) / 100.0).toFixed(2));
                speSAMF.SetValue((speBasicMinutesF.GetValue() * (1 + (parseFloat(speAllowanceF.GetValue() || 0) / 100.0))).toFixed(2));
                speLaborCostF.SetValue((speSAMF.GetValue() * (speMinimumWageF.GetValue() / 450)).toFixed(2))
                speCostF.SetValue(speLaborCostF.GetValue() * (parseFloat(speMarkupF.GetValue() || 0) / 100.0).toFixed(2))
                speFinishingCost.SetValue(speLaborCostF.GetValue() * (parseFloat(speMarkupF.GetValue() || 0) / 100.0))
                speCMPCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0))
                speTotalCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0)
                    + parseFloat(speWashing.GetValue() || 0) + parseFloat(speEmbro.GetValue() || 0) + parseFloat(spePrintingCost.GetValue() || 0))

            }, 500);
        }

        function autocalculateW(s, e) {


            var observedtime = 0.00;
            var TotalObservedTime = 0.00;
            setTimeout(function () {
                var indicies = gvW.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++) {
                    if (gvW.batchEditHelper.IsNewRow(indicies[i])) {


                        observedtime = parseFloat(gvW.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                        TotalObservedTime += observedtime;
                        var sam = 0.00
                        sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceW.GetValue() || 0) / 100.0));
                        gvW.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                    }
                    else {
                        var key = gvW.GetRowKey(indicies[i]);
                        if (gvW.batchEditHelper.IsDeletedRow(key))
                            console.log("deleted row " + indicies[i]);
                        else {
                            observedtime = parseFloat(gvW.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                            TotalObservedTime += observedtime;
                            var sam = 0.00
                            sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceW.GetValue() || 0) / 100.0));
                            gvW.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));
                        }
                    }
                }

                speTotalObservedTimeW.SetValue(TotalObservedTime.toFixed(2));
                speBasicMinutesW.SetValue(TotalObservedTime * (parseFloat(speEfficiencyW.GetValue() || 0) / 100.0).toFixed(2));
                speSAMW.SetValue((speBasicMinutesW.GetValue() * (1 + (parseFloat(speAllowanceW.GetValue() || 0) / 100.0))).toFixed(2));
                speLaborCostW.SetValue((speSAMW.GetValue() * (speMinimumWageW.GetValue() / 450)).toFixed(2))
                speCostW.SetValue(speLaborCostW.GetValue() * (parseFloat(speMarkupW.GetValue() || 0) / 100.0).toFixed(2))
                speWashing.SetValue(speLaborCostW.GetValue() * (parseFloat(speMarkupW.GetValue() || 0) / 100.0))
                speTotalCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0)
                  + parseFloat(speWashing.GetValue() || 0) + parseFloat(speEmbro.GetValue() || 0) + parseFloat(spePrintingCost.GetValue() || 0))
            }, 500);
        }

        function autocalculateE(s, e) {


            var observedtime = 0.00;
            var TotalObservedTime = 0.00;
            setTimeout(function () {
                var indicies = gvE.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++) {
                    if (gvE.batchEditHelper.IsNewRow(indicies[i])) {


                        observedtime = parseFloat(gvE.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                        TotalObservedTime += observedtime;
                        var sam = 0.00
                        sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceE.GetValue() || 0) / 100.0));
                        gvE.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                    }
                    else {
                        var key = gvE.GetRowKey(indicies[i]);
                        if (gvE.batchEditHelper.IsDeletedRow(key))
                            console.log("deleted row " + indicies[i]);
                        else {
                            observedtime = parseFloat(gvE.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                            TotalObservedTime += observedtime;
                            var sam = 0.00
                            sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceE.GetValue() || 0) / 100.0));
                            gvE.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                        }
                    }
                }

                speTotalObservedTimeE.SetValue(TotalObservedTime.toFixed(2));
                speBasicMinutesE.SetValue(TotalObservedTime * (parseFloat(speEfficiencyE.GetValue() || 0) / 100.0).toFixed(2));
                speSAME.SetValue((speBasicMinutesE.GetValue() * (1 + (parseFloat(speAllowanceE.GetValue() || 0) / 100.0))).toFixed(2));
                speLaborCostE.SetValue((speSAME.GetValue() * (speMinimumWageE.GetValue() / 450)).toFixed(2))
                speCostE.SetValue(speLaborCostE.GetValue() * (parseFloat(speMarkupE.GetValue() || 0) / 100.0).toFixed(2))
                speEmbro.SetValue(speLaborCostE.GetValue() * (parseFloat(speMarkupE.GetValue() || 0) / 100.0))
                speTotalCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0)
                  + parseFloat(speWashing.GetValue() || 0) + parseFloat(speEmbro.GetValue() || 0) + parseFloat(spePrintingCost.GetValue() || 0))


            }, 500);
        }

        function autocalculateP(s, e) {


            var observedtime = 0.00;
            var TotalObservedTime = 0.00;
            setTimeout(function () {
                var indicies = gvP.batchEditHelper.GetDataRowIndices();

                for (var i = 0; i < indicies.length; i++) {
                    if (gvP.batchEditHelper.IsNewRow(indicies[i])) {


                        observedtime = parseFloat(gvP.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                        TotalObservedTime += observedtime;
                        var sam = 0.00
                        sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceP.GetValue() || 0) / 100.0));
                        gvP.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                    }
                    else {
                        var key = gvP.GetRowKey(indicies[i]);
                        if (gvP.batchEditHelper.IsDeletedRow(key))
                            console.log("deleted row " + indicies[i]);
                        else {
                            observedtime = parseFloat(gvP.batchEditApi.GetCellValue(indicies[i], "ObservedTime"));

                            TotalObservedTime += observedtime;
                            var sam = 0.00
                            sam = parseFloat(observedtime) * (1 + (parseFloat(speAllowanceP.GetValue() || 0) / 100.0));
                            gvP.batchEditApi.SetCellValue(indicies[i], "SAM", sam.toFixed(2));

                        }
                    }
                }

                speTotalObservedTimeP.SetValue(TotalObservedTime.toFixed(2));
                speBasicMinutesP.SetValue(TotalObservedTime * (parseFloat(speEfficiencyP.GetValue() || 0) / 100.0).toFixed(2));
                speSAMP.SetValue((speBasicMinutesP.GetValue() * (1 + (parseFloat(speAllowanceP.GetValue() || 0) / 100.0))).toFixed(2));
                speLaborCostP.SetValue((speSAMP.GetValue() * (speMinimumWageP.GetValue() / 450)).toFixed(2))
                speCostP.SetValue(speLaborCostP.GetValue() * (parseFloat(speMarkupP.GetValue() || 0) / 100.0).toFixed(2))
                spePrintingCost.SetValue(speLaborCostP.GetValue() * (parseFloat(speMarkupP.GetValue() || 0) / 100.0))
                speTotalCost.SetValue(parseFloat(speCuttingCost.GetValue() || 0) + parseFloat(speSewingCost.GetValue() || 0) + parseFloat(speFinishingCost.GetValue() || 0)
              + parseFloat(speWashing.GetValue() || 0) + parseFloat(speEmbro.GetValue() || 0) + parseFloat(spePrintingCost.GetValue() || 0))

            }, 500);
        }



        function CascadeCode(Value) {

            speMinimumWageC.SetValue(Value);
            autocalculate();

        }


        function CascadeCodeS(Value) {

            speMinimumWageS.SetValue(Value);
            autocalculateS();
        }

        function CascadeCodeF(Value) {

            speMinimumWageF.SetValue(Value);
            autocalculateF();

        }

        function CascadeCodeW(Value) {

            speMinimumWageW.SetValue(Value);
            autocalculateW();
        }

        function CascadeCodeE(Value) {

            speMinimumWageE.SetValue(Value);
            autocalculateE();

        }

        function CascadeCodeP(Value) {

            speMinimumWageP.SetValue(Value);
            autocalculateP();

        }





    </script> 
</head>
<body style="height: 300px" onload="onload()" onkeypress="OnBodyKeyPress(event)">
  <dx:ASPxGlobalEvents ID="ge" runat="server">
    <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
  </dx:ASPxGlobalEvents>
  <form id="form1" runat="server" class="Entry">
    <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
      <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
          <dx:ASPxLabel runat="server" Text="Standard Allowable Minute" Font-Bold="true" ForeColor="White" Font-Size="X-Large"></dx:ASPxLabel>
        </dx:PanelContent>
      </PanelCollection>
    </dx:ASPxPanel>
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" ClientInstanceName="factbox" CloseAction="None" EnableViewState="False" HeaderText="Picture" Height="300px" Width="245px" PopupHorizontalOffset="1000" PopupVerticalOffset="920"
    ShowCloseButton="False" ShowCollapseButton="True" ShowOnPageLoad="True" ShowPinButton="True" ShowShadow="True">
      <ContentCollection>
        <dx:PopupControlContentControl runat="server">
        </dx:PopupControlContentControl>
      </ContentCollection>
    </dx:ASPxPopupControl>


    <dx:ASPxPopupControl ID="GridPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="GridPop" CloseAction="CloseButton" CloseOnEscape="true" HeaderImage-Height="10px" HeaderText="" Height="400px" ShowHeader="true" Width="950px" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" OnWindowCallback="GridPop_WindowCallback">
      <ClientSideEvents CloseUp="popUpClose" EndCallback="GridPop_EndCallback" />
      <ContentCollection>
        <dx:PopupControlContentControl runat="server">
          <dx:ASPxGridView ID="gvData" runat="server" AutoGenerateColumns="true" Width="900px" ClientInstanceName="gvData" EnableRowsCache="false" Settings-ShowFilterRowMenuLikeItem="true" Settings-ShowFilterRowMenu="true" Settings-ShowFilterRow="true" OnAutoFilterCellEditorInitialize="gvData_AutoFilterCellEditorInitialize"
          KeyboardSupport="true" EnableViewState="false" SettingsBehavior-AutoFilterRowInputDelay="500" OnDataBinding="gvData_DataBinding" OnDataBound="gvData_DataBound">
            <ClientSideEvents RowDblClick="function(){gvData.GetRowValues(gvData.GetFocusedRowIndex(), foclum, PutVal);}" />
            <SettingsPager Mode="ShowPager" PageSize="20" />
            <Settings VerticalScrollableHeight="250" VerticalScrollBarMode="Auto" ShowStatusBar="Hidden" />
            <SettingsBehavior AllowSelectSingleRowOnly="true" AllowFocusedRow="true" />

          </dx:ASPxGridView>
          <%--<dx:ASPxButton ID="btn" ClientInstanceName="POPUPGetJODetailbtn" runat="server" Width="170px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" onload="ButtonLoad" ClientVisible="true" Text="Get JO Details" Theme="MetropolisBlue">
                                            <ClientSideEvents Click="POPUPGetJODetail" />
                                        </dx:ASPxButton>--%>
        </dx:PopupControlContentControl>
      </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxCallback runat="server" ID="CallbackFunc" OnCallback="CallbackFunc_Callback" ClientInstanceName="CallbackFunc">
      <ClientSideEvents EndCallback="GridEnd" />
    </dx:ASPxCallback>
    <dx:ASPxCallback ID="Errorchecker" runat="server" ClientInstanceName="Errorchecker" OnCallback="Errorchecker_Callback">
      <ClientSideEvents EndCallback="OnCallbackComplete" />
    </dx:ASPxCallback>

    <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="400px" ClientInstanceName="cp" OnCallback="cp_Callback">
      <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
      <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
          <dx:ASPxFormLayout ID="frmlayout1" ClientInstanceName="frmlayout1" runat="server" Height="200px" Width="850px" Style="margin-left: -3px; margin-right: 0px;">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
            <Items>
              <dx:TabbedLayoutGroup ActiveTabIndex="0">
                <Items>
                  <dx:LayoutGroup Caption="General">
                    <Items>
                      <dx:LayoutGroup Caption="SAM Information" ColCount="2" Width="100%">
                        <Items>
                          <dx:LayoutGroup Caption="" UseDefaultPaddings="False" Width="37%" Paddings-PaddingLeft="25">
                            <Items>

                              <dx:LayoutItem Caption="Document Number">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="txtDocNumber" runat="server" Width="220px" OnLoad="TextboxLoad" AutoCompleteType="Disabled" Enabled="False">
                                    </dx:ASPxTextBox>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>
                              <dx:LayoutItem Caption="Document Date">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxDateEdit ID="dtpDocDate" runat="server" OnInit="dtpDocDate_Init" Width="220px">
                                      <ClientSideEvents Validation="OnValidation" />
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
                                    <dx:ASPxGridLookup ID="glSupplierCodeH" runat="server" Width="220px" ClientInstanceName="clBizPartnerCode" DataSourceID="SupplierCodelookup" KeyFieldName="SupplierCode" TextFormatString="{0}">
                                      <ClientSideEvents Validation="OnValidation" ValueChanged="function (s, e){  var g = clBizPartnerCode.GetGridView(); g.GetRowValues(g.GetFocusedRowIndex(),'Name',CascadeSName);  }" />
                                      <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField IsRequired="True" />
                                      </ValidationSettings>
                                      <InvalidStyle BackColor="Pink">
                                      </InvalidStyle>
                                      <GridViewProperties>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                        <Settings ShowFilterRow="True" />
                                      </GridViewProperties>
                                      <Columns>
                                        <dx:GridViewDataTextColumn Caption="Supplier Code" FieldName="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="1">
                                          <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Name" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2">
                                          <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                      </Columns>
                                    </dx:ASPxGridLookup>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>



                              <dx:LayoutItem Caption="Supplier Name">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="txtSName" ClientInstanceName="txtSName" runat="server" Width="220px" ClientEnabled="false">
                                    </dx:ASPxTextBox>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>

                              <dx:LayoutItem Caption="DISNumber" Name="DISNumber">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxMemo ID="txtDISNumber" Width="215px" Height="50px" SkinID="None" Native="True" runat="server" MaxLength="30" ClientInstanceName="txtDISNumber">
                                      <ClientSideEvents Validation="OnValidation" />
                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField IsRequired="True" />
                                      </ValidationSettings>
                                      <InvalidStyle BackColor="Pink">
                                      </InvalidStyle>
                                    </dx:ASPxMemo>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>

                              <dx:LayoutItem Caption="Brand">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridLookup ID="cbBrand" runat="server" OnLoad="LookupLoad" ClientInstanceName="CINBrand" DataSourceID="BrandLookup" KeyFieldName="Code" TextFormatString="{1}" Width="220px" ValueField="Code" TextField="Description">
                                      <GridViewProperties Settings-ShowFilterRow="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                        <Settings ShowFilterRow="True"></Settings>
                                        <SettingsPager PageSize="10"></SettingsPager>
                                      </GridViewProperties>
                                      <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Code" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                        </dx:GridViewDataTextColumn>
                                      </Columns>

                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField IsRequired="True" />
                                      </ValidationSettings>
                                      <InvalidStyle BackColor="Pink">
                                      </InvalidStyle>
                                    </dx:ASPxGridLookup>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>
                              <dx:LayoutItem Caption="Gender">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridLookup ID="cbGender" runat="server" OnLoad="LookupLoad" ClientInstanceName="CINGender" DataSourceID="GenderLookup" KeyFieldName="Code" TextFormatString="{1}" Width="220px" ValueField="Code" TextField="Description">
                                      <ClientSideEvents ValueChanged="function(s,e){cp.PerformCallback('gendercodefiltercase'); e.processOnServer = false;}" />
                                      <GridViewProperties Settings-ShowFilterRow="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                        <Settings ShowFilterRow="True"></Settings>
                                        <SettingsPager PageSize="10"></SettingsPager>
                                      </GridViewProperties>
                                      <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Code" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                        </dx:GridViewDataTextColumn>
                                      </Columns>

                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField IsRequired="True" />
                                      </ValidationSettings>
                                      <InvalidStyle BackColor="Pink">
                                      </InvalidStyle>
                                    </dx:ASPxGridLookup>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>

                              <dx:LayoutItem Caption="Product Category">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridLookup ID="cbProductCategory" runat="server" OnLoad="LookupLoad" ClientInstanceName="CINProductCategory" DataSourceID="ProductCategoryLookup" KeyFieldName="Code" TextFormatString="{1}" Width="220px" ValueField="Code" TextField="Description">
                                      <ClientSideEvents ValueChanged="function(s,e){cp.PerformCallback('productcatergoryfiltercase'); e.processOnServer = false;}" />

                                      <GridViewProperties Settings-ShowFilterRow="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                        <Settings ShowFilterRow="True"></Settings>
                                        <SettingsPager PageSize="10"></SettingsPager>
                                      </GridViewProperties>
                                      <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Code" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                        </dx:GridViewDataTextColumn>
                                      </Columns>
                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField IsRequired="True" />
                                      </ValidationSettings>
                                      <InvalidStyle BackColor="Pink">
                                      </InvalidStyle>
                                    </dx:ASPxGridLookup>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>


                              <dx:LayoutItem Caption="Product Sub Category">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridLookup ID="glProductSubCategory" runat="server" ClientInstanceName="CINProductSubCategory" DataSourceID="ProductSubCategoryLookup" KeyFieldName="ProductSubCatCode" OnLoad="LookupLoad" TextFormatString="{1}" Width="220px" HelpText="ProductSubCategory: Filtered By Product Category And Gender."
                                    HelpTextSettings-DisplayMode="Popup" ValueField="Code" TextField="Description">
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

                              <dx:LayoutItem Caption="Product Group">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridLookup ID="glProductGroup" runat="server" ClientInstanceName="CINProductGroup" DataSourceID="ProductGroupLookup" KeyFieldName="ProductGroupCode" OnLoad="LookupLoad" TextFormatString="{1}" Width="220px" HelpText="ProductGroup: Filtered By Product Category ."
                                    HelpTextSettings-DisplayMode="Popup" ValueField="Code" TextField="Description">
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


                              <%--                                              Subcat and subgroup      ///--%>

                                <dx:LayoutItem Caption="Fit Code">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxGridLookup ID="cbFitCode" runat="server" OnLoad="LookupLoad" ClientInstanceName="CINFit" DataSourceID="FitLookup" KeyFieldName="Code" TextFormatString="{1}" Width="220px" ValueField="Code" TextField="Description">
                                        <GridViewProperties Settings-ShowFilterRow="true">
                                          <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                          <Settings ShowFilterRow="True"></Settings>
                                          <SettingsPager PageSize="10"></SettingsPager>
                                        </GridViewProperties>
                                        <Columns>
                                          <dx:GridViewDataTextColumn FieldName="Code" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                          </dx:GridViewDataTextColumn>
                                          <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                          </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                          <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <InvalidStyle BackColor="Pink">
                                        </InvalidStyle>
                                      </dx:ASPxGridLookup>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="Product Class">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxGridLookup ID="cbProductClass" runat="server" ClientInstanceName="CINProductClass" OnLoad="LookupLoad" DataSourceID="ProductClassLookup" KeyFieldName="Code" TextFormatString="{1}" Width="220px" ValueField="Code" TextField="Description">
                                        <GridViewProperties Settings-ShowFilterRow="true">
                                          <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                          <Settings ShowFilterRow="True"></Settings>
                                          <SettingsPager PageSize="10"></SettingsPager>
                                        </GridViewProperties>
                                        <Columns>
                                          <dx:GridViewDataTextColumn FieldName="Code" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                          </dx:GridViewDataTextColumn>
                                          <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                          </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                          <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <InvalidStyle BackColor="Pink">
                                        </InvalidStyle>
                                      </dx:ASPxGridLookup>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Design Description" Name="DesignDesc">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxMemo ID="txtDesignDesc" Width="215px" Height="50px" SkinID="None" Native="True" runat="server" MaxLength="30" ClientInstanceName="txtDISNumber">
                                        <ClientSideEvents Validation="OnValidation" />
                                        <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                          <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <InvalidStyle BackColor="Pink">
                                        </InvalidStyle>
                                      </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Remarks" Name="Remarks">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxMemo ID="txtRemarks" Width="215px" Height="50px" SkinID="None" Native="True" runat="server" MaxLength="30" ClientInstanceName="txtRemarks">
                                        <ClientSideEvents Validation="OnValidation" />
                                        <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                          <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <InvalidStyle BackColor="Pink">
                                        </InvalidStyle>
                                      </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="" Name="Genereatebtn">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                      <dx:ASPxButton ID="Generatebtn" runat="server" Width="70px" ValidateInvisibleEditors="false" CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="False" Text="Generate" Theme="MetropolisBlue">
                                        <ClientSideEvents Click="Generate" />
                                      </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>



                            </Items>
                          </dx:LayoutGroup>



                          <dx:LayoutGroup Caption="" ColCount="2" UseDefaultPaddings="False" Width="63%" Paddings-PaddingLeft="70" Paddings-PaddingRight="50" ParentContainerStyle-Paddings-PaddingLeft="">
                            <Paddings PaddingLeft="70px" PaddingRight="50px"></Paddings>
                            <Items>




                              <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxLabel Text="*Note: Recommended picture size 600pxl X 900pxl and less than 500 KB." runat="server" Width="400" ForeColor="Red"> </dx:ASPxLabel>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>

                              <dx:EmptyLayoutItem></dx:EmptyLayoutItem>




                              <dx:LayoutItem Caption="">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxUploadControl ID="btnFrontUpload" runat="server" AutoStartUpload="True" Caption="dsada" ClientInstanceName="CINFrontUpload" CssClass="uploadControl" DialogTriggerID="externalDropZone" Name=" " OnFileUploadComplete="btnFrontUpload_FileUploadComplete"
                                    ShowProgressPanel="True" UploadMode="Auto" ShowTextBox="False">
                                      <ValidationSettings AllowedFileExtensions=".jpg, .jpeg, .gif, .png" MaxFileSize="510462" MaxFileSizeErrorText="File is too large!">
                                        <ErrorStyle CssClass="validationMessage" />
                                      </ValidationSettings>
                                      <ClientSideEvents DropZoneEnter="function(s, e) { if(e.dropZone.id == 'externalDropZone') setElementVisible('dropZone', true); }" DropZoneLeave="function(s, e) { if(e.dropZone.id == 'externalDropZone') setElementVisible('dropZone', false); }" FileUploadComplete="FrontImageUploadComplete"
                                      />
                                      <BrowseButton Text="FRONT"></BrowseButton>
                                      <BrowseButtonStyle Width="215px" CssClass="BrowseButton"></BrowseButtonStyle>
                                      <DropZoneStyle CssClass="uploadControlDropZone" />
                                      <ProgressBarStyle CssClass="uploadControlProgressBar" />
                                      <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False" ExternalDropZoneID="externalDropZone">
                                      </AdvancedModeSettings>
                                    </dx:ASPxUploadControl>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>

                              <dx:LayoutItem Caption="">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxUploadControl ID="btnBackUpload" runat="server" AutoStartUpload="True" Caption="dsada" ClientInstanceName="CINBackUpload" CssClass="uploadControl" DialogTriggerID="externalDropZoneBack" Name=" " OnFileUploadComplete="btnBackUpload_FileUploadComplete"
                                    ShowProgressPanel="True" UploadMode="Auto" ShowTextBox="False" OnLoad="UploadControlLoad">
                                      <ValidationSettings AllowedFileExtensions=".jpg, .jpeg, .gif, .png" MaxFileSize="510462" MaxFileSizeErrorText="File is too large!">
                                        <ErrorStyle CssClass="validationMessage" />
                                      </ValidationSettings>
                                      <ClientSideEvents DropZoneEnter="function(s, e) { if(e.dropZone.id == 'externalDropZoneBack') setElementVisible('dropZoneBack', true); }" DropZoneLeave="function(s, e) { if(e.dropZone.id == 'externalDropZoneBack') setElementVisible('dropZoneBack', false); }"
                                      FileUploadComplete="BackImageUploadComplete" />
                                      <BrowseButton Text="BACK"></BrowseButton>
                                      <BrowseButtonStyle Width="215px" CssClass="BrowseButton"></BrowseButtonStyle>
                                      <DropZoneStyle CssClass="uploadControlDropZone" />
                                      <ProgressBarStyle CssClass="uploadControlProgressBar" />
                                      <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False" ExternalDropZoneID="externalDropZoneBack">
                                      </AdvancedModeSettings>
                                    </dx:ASPxUploadControl>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>
                              <%-- RA TEST remove muna id para sa drag --%>
                                <dx:LayoutItem ShowCaption="False">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                      <div id="externalDropZone" class="dropZoneExternal">
                                        <div id="dragZone">
                                          <span class="dragZoneText">DRAG IMAGE HERE</span>
                                        </div>
                                        <dx:ASPxImageZoom ID="dxFrontImage" ExpandWindowText="Press Escape to Exit Full Screen Mode" ImagesExpandWindow-CloseButton-Width="0px" runat="server" LargeImageUrl="~\IT\Initial.png" ImageUrl="~\IT\Initial.png" EnableExpandMode="true" ClientInstanceName="CINFrontImage"
                                        Height="354px" ShowLoadingImage="True" Width="234px" CssClass="ImageRadius">
                                          <SettingsZoomMode ZoomWindowPosition="Right" />
                                        </dx:ASPxImageZoom>

                                        <div id="dropZone" class="hidden">
                                          <span class="dropZoneText">DROP IMAGE HERE</span>
                                        </div>
                                      </div>
                                      <table>
                                        <tr>
                                          <dx:ASPxLabel Text="" runat="server" Height="2px"> </dx:ASPxLabel>
                                        </tr>
                                        <tr>
                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="50"> </dx:ASPxLabel>
                                          </td>
                                          <td>
                                            <dx:ASPxButton ID="ASPxButton1" ClientInstanceName="CINGetStepDetail" runat="server" Width="150px" CssClass="BrowseButton" AutoPostBack="False" ClientVisible="true" Text="View Full Size">
                                              <ClientSideEvents Click="function(s,e){ CINFrontImage.expandWindow.Show();
                                                                                         
                                                                                             }" />
                                            </dx:ASPxButton>
                                          </td>
                                        </tr>
                                      </table>


                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem ShowCaption="False">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                      <div id="externalDropZoneBack" class="dropZoneExternal">
                                        <div id="dragZoneBack">
                                          <span class="dragZoneText">DRAG IMAGE HERE</span>
                                        </div>
                                        <dx:ASPxImageZoom ID="dxBackImage" ExpandWindowText="Press Escape to Exit Full Screen Mode" ImagesExpandWindow-CloseButton-Width="0px" runat="server" LargeImageUrl="~\IT\Initial.png" ImageUrl="~\IT\Initial.png" ClientInstanceName="CINBackImage" Height="354px"
                                        ShowLoadingImage="True" Width="234px" CssClass="ImageRadius">
                                        </dx:ASPxImageZoom>
                                        <div id="dropZoneBack" class="hidden">
                                          <span class="dropZoneText">DROP IMAGE HERE</span>
                                        </div>
                                      </div>
                                      <table>
                                        <tr>
                                          <dx:ASPxLabel Text="" runat="server" Height="2px"> </dx:ASPxLabel>
                                        </tr>
                                        <tr>
                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="50"> </dx:ASPxLabel>
                                          </td>
                                          <td>
                                            <dx:ASPxButton ID="ASPxButton2" ClientInstanceName="CINGetStepDetail" runat="server" Width="150px" CssClass="BrowseButton" AutoPostBack="False" ClientVisible="true" Text="View Full Size">
                                              <ClientSideEvents Click="function(s,e){ CINBackImage.expandWindow.Show();
                                                                                         
                                                                                             }" />
                                            </dx:ASPxButton>
                                          </td>
                                        </tr>
                                      </table>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem ShowCaption="False">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                      <dx:ASPxTextBox ID="txtFrontImage64string" ClientInstanceName="CINFrontImage64string" runat="server" Width="250" ClientVisible="false" ReadOnly="true">
                                      </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem ShowCaption="False">
                                  <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                      <dx:ASPxTextBox ID="txtBackImage64string" ClientInstanceName="CINBackImage64string" runat="server" Width="250" ClientVisible="false" ReadOnly="true">
                                      </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                  </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>



                            </Items>
                          </dx:LayoutGroup>
                        </Items>
                      </dx:LayoutGroup>

                      <dx:LayoutGroup Caption="Costing">
                        <Items>
                          <dx:LayoutGroup ColCount="2" Caption="Costing Information">
                            <Items>

                              <dx:LayoutGroup Width="100%" Caption="CMP Cost" ColCount="2" Name="Costing">
                                <Items>
                                  <dx:LayoutItem Caption="CMP Cost" Name="CMPCost">
                                    <LayoutItemNestedControlCollection>
                                      <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxSpinEdit ID="speCMPCost" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="speCMPCost" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                        AllowMouseWheel="false" HorizontalAlign="Right">

                                        </dx:ASPxSpinEdit>
                                      </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                  </dx:LayoutItem>
                                  <dx:LayoutItem Caption="Cutting Cost" Name="CuttingCost">
                                    <LayoutItemNestedControlCollection>
                                      <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxSpinEdit ID="speCuttingCost" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="speCuttingCost" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                        AllowMouseWheel="false" HorizontalAlign="Right">

                                        </dx:ASPxSpinEdit>
                                      </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                  </dx:LayoutItem>
                                  <dx:LayoutItem Caption="Sewing Cost" Name="SewingCost">
                                    <LayoutItemNestedControlCollection>
                                      <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxSpinEdit ID="speSewingCost" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="speSewingCost" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                        AllowMouseWheel="false" HorizontalAlign="Right">

                                        </dx:ASPxSpinEdit>
                                      </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                  </dx:LayoutItem>

                                  <dx:LayoutItem Caption="Finishing Cost" Name="FinishingCost">
                                    <LayoutItemNestedControlCollection>
                                      <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxSpinEdit ID="speFinishingCost" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="speFinishingCost" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                        AllowMouseWheel="false" HorizontalAlign="Right">

                                        </dx:ASPxSpinEdit>
                                      </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                  </dx:LayoutItem>
                                </Items>
                              </dx:LayoutGroup>

                              <dx:LayoutItem Caption="Washing Cost" Name="WashingCost">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxSpinEdit ID="speWashing" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="speWashing" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                    AllowMouseWheel="false" HorizontalAlign="Right">

                                    </dx:ASPxSpinEdit>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>
                              <dx:LayoutItem Caption="Embroidery Cost" Name="EmbroCost">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxSpinEdit ID="speEmbro" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="speEmbro" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                    HorizontalAlign="Right">

                                    </dx:ASPxSpinEdit>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>

                              <dx:LayoutItem Caption="Printing Cost" Name="PrintingCost">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxSpinEdit ID="spePrintingCost" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="spePrintingCost" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                    AllowMouseWheel="false" HorizontalAlign="Right">

                                    </dx:ASPxSpinEdit>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>
                              <dx:LayoutItem Caption="Total Cost" Name="TotalCost">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxSpinEdit ID="speTotalCost" runat="server" Width="170px" ReadOnly="true" ClientInstanceName="speTotalCost" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                    AllowMouseWheel="false" HorizontalAlign="Right">

                                    </dx:ASPxSpinEdit>
                                  </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>



                              <dx:LayoutItem ClientVisible="false">
                                <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxGridView ID="gvRefC" runat="server" ClientInstanceName="gvRefC" AutoGenerateColumns="true" BatchEditStartEditing="OnStartEditing">
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



                      <dx:LayoutGroup Caption="Steps">
                        <Items>

                          <dx:LayoutItem ShowCaption="false">
                            <LayoutItemNestedControlCollection>
                              <dx:LayoutItemNestedControlContainer>

                                <div class="tabs">
                                  <div class="tab">
                                    <input type="radio" id="tab-1" name="tab-group-1" checked="checked" />
                                    <label for="tab-1">Cutting</label>
                                    <div class="content">
                                      <table>
                                        <tr>


                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Effciency" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speEfficiencyC" runat="server" Width="170px" ClientInstanceName="speEfficiencyC" SpinButtons-ShowIncrementButtons="false" MinValue="0" MaxValue="999" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculate" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Allowance" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Height="5px"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speAllowanceC" runat="server" Width="170px" ClientInstanceName="speAllowanceC" MinValue="0" MaxValue="999" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculate" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>




                                          <tr>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Total Observed Time" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speTotalObservedTimeC" runat="server" Width="170px" ClientInstanceName="speTotalObservedTimeC" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%--    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Basic Minutes" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speBasicMinutesC" runat="server" Width="170px" ClientInstanceName="speBasicMinutesC" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%-- <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>
                                          </tr>
                                          <tr>


                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="SAM" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speSAMC" runat="server" Width="170px" ClientInstanceName="speSAMC" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ReadOnly="true" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                              HorizontalAlign="Right">
                                                <ClientSideEvents Validation="OnValidation" />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Minimum Wage" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxGridLookup ID="cbRegionC" runat="server" ClientInstanceName="CINRegion" DataSourceID="RegionLookup" KeyFieldName="Region" TextFormatString="{0}" Width="170px" ValueField="Code" TextField="Description">
                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                  <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                  <Settings ShowFilterRow="True"></Settings>
                                                  <SettingsPager PageSize="3"></SettingsPager>
                                                </GridViewProperties>
                                                <Columns>
                                                  <dx:GridViewDataTextColumn FieldName="Region" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="NonCompliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Compliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents ValueChanged=" function (s, e){  var g = CINRegion.GetGridView();  if (CINBrand.GetText() == 'WRANGLER') {g.GetRowValues(g.GetFocusedRowIndex(),'Compliance',CascadeCode);} else {g.GetRowValues(g.GetFocusedRowIndex(),'NonCompliance',CascadeCode);}  }"
                                                />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxGridLookup>

                                            </td>



                                            <td>

                                            </td>
                                          </tr>


                                          <tr>
                                            <tr>


                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Labor Cost" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                <dx:ASPxSpinEdit ID="speLaborCostC" runat="server" Width="170px" ClientInstanceName="speLaborCostC" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                                AllowMouseWheel="false" HorizontalAlign="Right">
                                                  <ClientSideEvents Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>
                                                <dx:ASPxSpinEdit ID="speMinimumWageC" ReadOnly="true" runat="server" Width="170px" ClientInstanceName="speMinimumWageC" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                HorizontalAlign="Right">
                                                  <ClientSideEvents ValueChanged="autocalculate" Validation="OnValidation" />
                                                  <ValidationSettings Display="None"  ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                              </td>
                                            </tr>
                                            <tr>

                                              <tr>
                                                <tr>


                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Cost" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                    <dx:ASPxSpinEdit ID="speCostC" runat="server" Width="170px" ClientInstanceName="speCostC" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                    HorizontalAlign="Right">
                                                      <ClientSideEvents Validation="OnValidation" />
                                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" />
                                                      </ValidationSettings>
                                                      <InvalidStyle BackColor="Pink">
                                                      </InvalidStyle>
                                                    </dx:ASPxSpinEdit>
                                                  </td>


                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel56" runat="server" Text="Mark-up" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                    <dx:ASPxSpinEdit ID="speMarkupC" runat="server" Width="170px" ClientInstanceName="speMarkupC" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                    HorizontalAlign="Right">
                                                      <ClientSideEvents ValueChanged="autocalculate" Validation="OnValidation" />
                                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" />
                                                      </ValidationSettings>
                                                      <InvalidStyle BackColor="Pink">
                                                      </InvalidStyle>
                                                    </dx:ASPxSpinEdit>
                                                  </td>
                                                </tr>











                                                <tr>
                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                </tr>

                                                <tr>




                                                  <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" Width="1200px" ClientInstanceName="gv1" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                  OnRowValidating="grid_RowValidating" KeyFieldName="LineNumber" EnableRowsCache="false">
                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" />
                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                    <Columns>
                                                      <dx:GridViewDataTextColumn FieldName="DocNumber" Width="0px" VisibleIndex="0">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                        </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="90px">
                                                        <CustomButtons>

                                                          <dx:GridViewCommandColumnCustomButton ID="Delete">
                                                            <Image IconID="actions_cancel_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                          <dx:GridViewCommandColumnCustomButton ID="ViewTransaction">
                                                            <Image IconID="find_find_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                      </dx:GridViewCommandColumn>
                                                      <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Visible="true" UnboundType="String" Width="0px" ReadOnly="true">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False"></PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <%--< RA START Lookup  --%>
                                                        <dx:GridViewDataTextColumn FieldName="OperationCode" VisibleIndex="3" Width="100px" Name="glOBcode">
                                                          <PropertiesTextEdit ClientInstanceName="glOBcode">
                                                            <ClientSideEvents KeyDown="function(s,e){FieldKeyDown(s,e,'OBCode',s.GetText(),'CUTTING');}" KeyUp="function(s,e){FieldKeyup(e,'OBCode',s.GetText(),'')}" KeyPress="fieldKeyPress" />
                                                          </PropertiesTextEdit>

                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Steps" VisibleIndex="4" Width="120px" Caption="Steps" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Parts" VisibleIndex="4" Width="120px" Caption="Parts" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="OpsBreakdown" VisibleIndex="4" Width="120px" Caption="OpsBreakdown" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="MachineType" VisibleIndex="4" Width="120px" Caption="MachineType" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="ObservedTime" VisibleIndex="4" Width="120px" Caption="ObservedTime" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="SAM" VisibleIndex="4" Width="120px" Caption="SAM" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Video" VisibleIndex="4" Width="120px" Caption="Video" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn FieldName="Field1" Name="glpDField1" ShowInCustomizationForm="True" VisibleIndex="20" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field2" Name="glpDField2" ShowInCustomizationForm="True" VisibleIndex="21" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field3" Name="glpDField3" ShowInCustomizationForm="True" VisibleIndex="22" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field4" Name="glpDField4" ShowInCustomizationForm="True" VisibleIndex="23" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field5" Name="glpDField5" ShowInCustomizationForm="True" VisibleIndex="24" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field6" Name="glpDField6" ShowInCustomizationForm="True" VisibleIndex="25" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field7" Name="glpDField7" ShowInCustomizationForm="True" VisibleIndex="26" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field8" Name="glpDField8" ShowInCustomizationForm="True" VisibleIndex="27" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field9" Name="glpDField9" ShowInCustomizationForm="True" VisibleIndex="28" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                    </Columns>
                                                  </dx:ASPxGridView>
                                                </tr>

                                      </table>
                                      <table>
                                        <tr>

                                        </tr>
                                      </table>
                                    </div>
                                  </div>

                                  <div class="tab">
                                    <input type="radio" id="tab-2" name="tab-group-1" />
                                    <label for="tab-2">Sewing</label>
                                    <div class="content">
                                      <table>
                                        <tr>


                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Effciency" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speEfficiencyS" runat="server" Width="170px" ClientInstanceName="speEfficiencyS" SpinButtons-ShowIncrementButtons="false" MinValue="0" MaxValue="999" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateS" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Allowance" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Height="5px"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speAllowanceS" runat="server" Width="170px" ClientInstanceName="speAllowanceS" MinValue="0" MaxValue="999" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateS" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>




                                          <tr>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Total Observed Time" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speTotalObservedTimeS" runat="server" Width="170px" ClientInstanceName="speTotalObservedTimeS" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%--    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Basic Minutes" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speBasicMinutesS" runat="server" Width="170px" ClientInstanceName="speBasicMinutesS" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%-- <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>
                                          </tr>
                                          <tr>


                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="SAM" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speSAMS" runat="server" Width="170px" ClientInstanceName="speSAMS" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ReadOnly="true" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                              HorizontalAlign="Right">
                                                <ClientSideEvents Validation="OnValidation" />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Minimum Wage" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxGridLookup ID="cbRegionS" runat="server" ClientInstanceName="CINRegionS" DataSourceID="RegionLookup" KeyFieldName="Region" TextFormatString="{0}" Width="170px" ValueField="Code" TextField="Description">
                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                  <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                  <Settings ShowFilterRow="True"></Settings>
                                                  <SettingsPager PageSize="3"></SettingsPager>
                                                </GridViewProperties>
                                                <Columns>
                                                  <dx:GridViewDataTextColumn FieldName="Region" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="NonCompliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Compliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents ValueChanged=" function (s, e){  var g = CINRegionS.GetGridView();  if (CINBrand.GetText() == 'WRANGLER') {g.GetRowValues(g.GetFocusedRowIndex(),'Compliance',CascadeCodeS);} else {g.GetRowValues(g.GetFocusedRowIndex(),'NonCompliance',CascadeCodeS);}  }"
                                                />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxGridLookup>

                                            </td>
                                            
                                                <td>

                                              
                                                </td>
                                          </tr>


                                          <tr>
                                            <tr>


                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Labor Cost" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                <dx:ASPxSpinEdit ID="speLaborCostS" runat="server" Width="170px" ClientInstanceName="speLaborCostS" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                                AllowMouseWheel="false" HorizontalAlign="Right">
                                                  <ClientSideEvents Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>
                                                      <dx:ASPxSpinEdit ID="speMinimumWageS" ReadOnly="true"  runat="server" Width="170px" ClientInstanceName="speMinimumWageS" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                  HorizontalAlign="Right">
                                                    <ClientSideEvents ValueChanged="autocalculateS" Validation="OnValidation" />
                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                      <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                    <InvalidStyle BackColor="Pink">
                                                    </InvalidStyle>
                                                  </dx:ASPxSpinEdit>


                                            
                                              </td>
                                            </tr>
                                            <tr>

                                              <tr>
                                                <tr>


                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="Cost" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                    <dx:ASPxSpinEdit ID="speCostS" runat="server" Width="170px" ClientInstanceName="speCostS" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                    HorizontalAlign="Right">
                                                      <ClientSideEvents Validation="OnValidation" />
                                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" />
                                                      </ValidationSettings>
                                                      <InvalidStyle BackColor="Pink">
                                                      </InvalidStyle>
                                                    </dx:ASPxSpinEdit>
                                                  </td>

                                                      <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel57" runat="server" Text="Mark-up" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                     <dx:ASPxSpinEdit ID="speMarkupS" runat="server" Width="170px" ClientInstanceName="speMarkupS" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                HorizontalAlign="Right">
                                                  <ClientSideEvents ValueChanged="autocalculateS" Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                                  </td>


                                                </tr>

                                                <tr>
                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                </tr>

                                                <tr>




                                                  <dx:ASPxGridView ID="gvS" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="gvS" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                  OnRowValidating="grid_RowValidating" KeyFieldName="LineNumber" EnableRowsCache="false">
                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" />
                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                    <Columns>
                                                      <dx:GridViewDataTextColumn FieldName="DocNumber" Width="0px" VisibleIndex="0">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                        </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="90px">
                                                        <CustomButtons>

                                                          <dx:GridViewCommandColumnCustomButton ID="DeleteS">
                                                            <Image IconID="actions_cancel_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                          <dx:GridViewCommandColumnCustomButton ID="ViewTransactionS">
                                                            <Image IconID="find_find_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                      </dx:GridViewCommandColumn>
                                                      <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Visible="true" UnboundType="String" Width="0px" ReadOnly="true">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False"></PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <%--< RA START Lookup  --%>
                                                        <dx:GridViewDataTextColumn FieldName="OperationCode" VisibleIndex="3" Width="100px" Name="glOBcode">
                                                          <PropertiesTextEdit ClientInstanceName="glOBcode">
                                                            <ClientSideEvents KeyDown="function(s,e){FieldKeyDown(s,e,'OBCode',s.GetText(),'SEWING');}" KeyUp="function(s,e){FieldKeyup(e,'OBCode',s.GetText(),'')}" KeyPress="fieldKeyPress" />
                                                          </PropertiesTextEdit>

                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Steps" VisibleIndex="4" Width="120px" Caption="Steps" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Parts" VisibleIndex="4" Width="120px" Caption="Parts" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="OpsBreakdown" VisibleIndex="4" Width="120px" Caption="OpsBreakdown" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="MachineType" VisibleIndex="4" Width="120px" Caption="MachineType" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="ObservedTime" VisibleIndex="4" Width="120px" Caption="ObservedTime" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="SAM" VisibleIndex="4" Width="120px" Caption="SAM" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Video" VisibleIndex="4" Width="120px" Caption="Video" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn FieldName="Field1" Name="glpDField1" ShowInCustomizationForm="True" VisibleIndex="20" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field2" Name="glpDField2" ShowInCustomizationForm="True" VisibleIndex="21" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field3" Name="glpDField3" ShowInCustomizationForm="True" VisibleIndex="22" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field4" Name="glpDField4" ShowInCustomizationForm="True" VisibleIndex="23" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field5" Name="glpDField5" ShowInCustomizationForm="True" VisibleIndex="24" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field6" Name="glpDField6" ShowInCustomizationForm="True" VisibleIndex="25" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field7" Name="glpDField7" ShowInCustomizationForm="True" VisibleIndex="26" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field8" Name="glpDField8" ShowInCustomizationForm="True" VisibleIndex="27" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field9" Name="glpDField9" ShowInCustomizationForm="True" VisibleIndex="28" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                    </Columns>
                                                  </dx:ASPxGridView>
                                                </tr>

                                      </table>
                                      <table>
                                        <tr>

                                        </tr>
                                      </table>
                                    </div>
                                  </div>
                                  <div class="tab">
                                    <input type="radio" id="tab-3" name="tab-group-1" />
                                    <label for="tab-3">Finishing</label>
                                    <div class="content">
                                      <table>
                                        <tr>


                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Effciency" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speEfficiencyF" runat="server" Width="170px" ClientInstanceName="speEfficiencyF" SpinButtons-ShowIncrementButtons="false" MinValue="0" MaxValue="999" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateF" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="Allowance" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Height="5px"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speAllowanceF" runat="server" Width="170px" ClientInstanceName="speAllowanceF" MinValue="0" MaxValue="999" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateF" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>




                                          <tr>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="Total Observed Time" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speTotalObservedTimeF" runat="server" Width="170px" ClientInstanceName="speTotalObservedTimeF" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%--    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Basic Minutes" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speBasicMinutesF" runat="server" Width="170px" ClientInstanceName="speBasicMinutesF" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%-- <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>
                                          </tr>
                                          <tr>


                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="SAM" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speSAMF" runat="server" Width="170px" ClientInstanceName="speSAMF" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ReadOnly="true" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                              HorizontalAlign="Right">
                                                <ClientSideEvents Validation="OnValidation" />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="Minimum Wage" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxGridLookup ID="cbRegionF" runat="server" ClientInstanceName="CINRegionF" DataSourceID="RegionLookup" KeyFieldName="Region" TextFormatString="{0}" Width="170px" ValueField="Code" TextField="Description">
                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                  <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                  <Settings ShowFilterRow="True"></Settings>
                                                  <SettingsPager PageSize="3"></SettingsPager>
                                                </GridViewProperties>
                                                <Columns>
                                                  <dx:GridViewDataTextColumn FieldName="Region" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="NonCompliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Compliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents ValueChanged=" function (s, e){  var g = CINRegionF.GetGridView();  if (CINBrand.GetText() == 'WRANGLER') {g.GetRowValues(g.GetFocusedRowIndex(),'Compliance',CascadeCodeF);} else {g.GetRowValues(g.GetFocusedRowIndex(),'NonCompliance',CascadeCodeF);}  }"
                                                />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxGridLookup>

                                            </td>
                                     
                                                <td>

                                           
                                                </td>
                                          </tr>


                                          <tr>
                                            <tr>


                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="Labor Cost" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                <dx:ASPxSpinEdit ID="speLaborCostF" runat="server" Width="170px" ClientInstanceName="speLaborCostF" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                                AllowMouseWheel="false" HorizontalAlign="Right">
                                                  <ClientSideEvents Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>
                                            <dx:ASPxSpinEdit ID="speMinimumWageF" runat="server" ReadOnly="true"  Width="170px" ClientInstanceName="speMinimumWageF" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                  HorizontalAlign="Right">
                                                    <ClientSideEvents ValueChanged="autocalculateF" Validation="OnValidation" />
                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                      <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                    <InvalidStyle BackColor="Pink">
                                                    </InvalidStyle>
                                                  </dx:ASPxSpinEdit>

                                              </td>
                                            </tr>
                                            <tr>

                                              <tr>
                                                <tr>


                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="Cost" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                    <dx:ASPxSpinEdit ID="speCostF" runat="server" Width="170px" ClientInstanceName="speCostF" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                    HorizontalAlign="Right">
                                                      <ClientSideEvents Validation="OnValidation" />
                                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" />
                                                      </ValidationSettings>
                                                      <InvalidStyle BackColor="Pink">
                                                      </InvalidStyle>
                                                    </dx:ASPxSpinEdit>
                                                  </td>


                                                      <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel58" runat="server" Text="Mark-up" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                 




                                                <dx:ASPxSpinEdit ID="speMarkupF" runat="server" Width="170px" ClientInstanceName="speMarkupF" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                HorizontalAlign="Right">
                                                  <ClientSideEvents ValueChanged="autocalculateF" Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                                  </td>


                                                </tr>

                                                <tr>
                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                </tr>

                                                <tr>




                                                  <dx:ASPxGridView ID="gvF" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="gvF" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                  OnRowValidating="grid_RowValidating" KeyFieldName="LineNumber" EnableRowsCache="false">
                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" />
                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                    <Columns>
                                                      <dx:GridViewDataTextColumn FieldName="DocNumber" Width="0px" VisibleIndex="0">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                        </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="90px">
                                                        <CustomButtons>

                                                          <dx:GridViewCommandColumnCustomButton ID="DeleteF">
                                                            <Image IconID="actions_cancel_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                          <dx:GridViewCommandColumnCustomButton ID="ViewTransactionF">
                                                            <Image IconID="find_find_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                      </dx:GridViewCommandColumn>
                                                      <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Visible="true" UnboundType="String" Width="0px" ReadOnly="true">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False"></PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <%--< RA START Lookup  --%>
                                                        <dx:GridViewDataTextColumn FieldName="OperationCode" VisibleIndex="3" Width="100px" Name="glOBcode">
                                                          <PropertiesTextEdit ClientInstanceName="glOBcode">
                                                            <ClientSideEvents KeyDown="function(s,e){FieldKeyDown(s,e,'OBCode',s.GetText(),'FINISHING');}" KeyUp="function(s,e){FieldKeyup(e,'OBCode',s.GetText(),'')}" KeyPress="fieldKeyPress" />
                                                          </PropertiesTextEdit>

                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Steps" VisibleIndex="4" Width="120px" Caption="Steps" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Parts" VisibleIndex="4" Width="120px" Caption="Parts" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="OpsBreakdown" VisibleIndex="4" Width="120px" Caption="OpsBreakdown" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="MachineType" VisibleIndex="4" Width="120px" Caption="MachineType" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="ObservedTime" VisibleIndex="4" Width="120px" Caption="ObservedTime" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="SAM" VisibleIndex="4" Width="120px" Caption="SAM" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Video" VisibleIndex="4" Width="120px" Caption="Video" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn FieldName="Field1" Name="glpDField1" ShowInCustomizationForm="True" VisibleIndex="20" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field2" Name="glpDField2" ShowInCustomizationForm="True" VisibleIndex="21" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field3" Name="glpDField3" ShowInCustomizationForm="True" VisibleIndex="22" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field4" Name="glpDField4" ShowInCustomizationForm="True" VisibleIndex="23" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field5" Name="glpDField5" ShowInCustomizationForm="True" VisibleIndex="24" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field6" Name="glpDField6" ShowInCustomizationForm="True" VisibleIndex="25" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field7" Name="glpDField7" ShowInCustomizationForm="True" VisibleIndex="26" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field8" Name="glpDField8" ShowInCustomizationForm="True" VisibleIndex="27" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field9" Name="glpDField9" ShowInCustomizationForm="True" VisibleIndex="28" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                    </Columns>
                                                  </dx:ASPxGridView>
                                                </tr>

                                      </table>
                                      <table>
                                        <tr>

                                        </tr>
                                      </table>
                                    </div>
                                  </div>
                                  <div class="tab">
                                    <input type="radio" id="tab-4" name="tab-group-1" />
                                    <label for="tab-4">Washing</label>
                                    <div class="content">
                                      <table>
                                        <tr>


                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text="Effciency" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speEfficiencyW" runat="server" Width="170px" ClientInstanceName="speEfficiencyW" SpinButtons-ShowIncrementButtons="false" MinValue="0" MaxValue="999" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateW" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel30" runat="server" Text="Allowance" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Height="5px"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speAllowanceW" runat="server" Width="170px" ClientInstanceName="speAllowanceW" MinValue="0" MaxValue="999" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateW" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>




                                          <tr>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="Total Observed Time" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speTotalObservedTimeW" runat="server" Width="170px" ClientInstanceName="speTotalObservedTimeW" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%--    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="Basic Minutes" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speBasicMinutesW" runat="server" Width="170px" ClientInstanceName="speBasicMinutesW" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%-- <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>
                                          </tr>
                                          <tr>


                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="SAM" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speSAMW" runat="server" Width="170px" ClientInstanceName="speSAMW" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ReadOnly="true" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                              HorizontalAlign="Right">
                                                <ClientSideEvents Validation="OnValidation" />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel34" runat="server" Text="Minimum Wage" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxGridLookup ID="cbRegionW" runat="server" ClientInstanceName="CINRegionW" DataSourceID="RegionLookup" KeyFieldName="Region" TextFormatString="{0}" Width="170px" ValueField="Code" TextField="Description">
                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                  <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                  <Settings ShowFilterRow="True"></Settings>
                                                  <SettingsPager PageSize="3"></SettingsPager>
                                                </GridViewProperties>
                                                <Columns>
                                                  <dx:GridViewDataTextColumn FieldName="Region" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="NonCompliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Compliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents ValueChanged=" function (s, e){  var g = CINRegionW.GetGridView();  if (CINBrand.GetText() == 'WRANGLER') {g.GetRowValues(g.GetFocusedRowIndex(),'Compliance',CascadeCodeW);} else {g.GetRowValues(g.GetFocusedRowIndex(),'NonCompliance',CascadeCodeW);}  }"
                                                />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxGridLookup>

                                            </td>
                                        
                                                <td>

                                                 
                                                </td>
                                          </tr>


                                          <tr>
                                            <tr>


                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="Labor Cost" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                <dx:ASPxSpinEdit ID="speLaborCostW" runat="server" Width="170px" ClientInstanceName="speLaborCostW" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                                AllowMouseWheel="false" HorizontalAlign="Right">
                                                  <ClientSideEvents Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel36" runat="server" Text="" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                   <dx:ASPxSpinEdit ID="speMinimumWageW" ReadOnly="true"  runat="server" Width="170px" ClientInstanceName="speMinimumWageW" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                  HorizontalAlign="Right">
                                                    <ClientSideEvents ValueChanged="autocalculateW" Validation="OnValidation" />
                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                      <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                    <InvalidStyle BackColor="Pink">
                                                    </InvalidStyle>
                                                  </dx:ASPxSpinEdit>

                                                
                                              </td>
                                            </tr>
                                            <tr>

                                              <tr>
                                                <tr>


                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="Cost" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                    <dx:ASPxSpinEdit ID="speCostW" runat="server" Width="170px" ClientInstanceName="speCostW" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                    HorizontalAlign="Right">
                                                      <ClientSideEvents Validation="OnValidation" />
                                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" />
                                                      </ValidationSettings>
                                                      <InvalidStyle BackColor="Pink">
                                                      </InvalidStyle>
                                                    </dx:ASPxSpinEdit>
                                                  </td>

                                                     <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel59" runat="server" Text="Mark-up" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                  <dx:ASPxSpinEdit ID="speMarkupW" runat="server" Width="170px" ClientInstanceName="speMarkupW" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                HorizontalAlign="Right">
                                                  <ClientSideEvents ValueChanged="autocalculateW" Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                                  </td>

                                                </tr>

                                                <tr>
                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                </tr>

                                                <tr>




                                                  <dx:ASPxGridView ID="gvW" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="gvW" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                  OnRowValidating="grid_RowValidating" KeyFieldName="LineNumber" EnableRowsCache="false">
                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" />
                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                    <Columns>
                                                      <dx:GridViewDataTextColumn FieldName="DocNumber" Width="0px" VisibleIndex="0">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                        </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="90px">
                                                        <CustomButtons>

                                                          <dx:GridViewCommandColumnCustomButton ID="DeleteW">
                                                            <Image IconID="actions_cancel_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                          <dx:GridViewCommandColumnCustomButton ID="ViewTransactionW">
                                                            <Image IconID="find_find_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                      </dx:GridViewCommandColumn>
                                                      <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Visible="true" UnboundType="String" Width="0px" ReadOnly="true">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False"></PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <%--< RA START Lookup  --%>
                                                        <dx:GridViewDataTextColumn FieldName="OperationCode" VisibleIndex="3" Width="100px" Name="glOBcode">
                                                          <PropertiesTextEdit ClientInstanceName="glOBcode">
                                                            <ClientSideEvents KeyDown="function(s,e){FieldKeyDown(s,e,'OBCode',s.GetText(),'WASHING');}" KeyUp="function(s,e){FieldKeyup(e,'OBCode',s.GetText(),'')}" KeyPress="fieldKeyPress" />
                                                          </PropertiesTextEdit>

                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Steps" VisibleIndex="4" Width="120px" Caption="Steps" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Parts" VisibleIndex="4" Width="120px" Caption="Parts" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="OpsBreakdown" VisibleIndex="4" Width="120px" Caption="OpsBreakdown" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="MachineType" VisibleIndex="4" Width="120px" Caption="MachineType" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="ObservedTime" VisibleIndex="4" Width="120px" Caption="ObservedTime" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="SAM" VisibleIndex="4" Width="120px" Caption="SAM" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Video" VisibleIndex="4" Width="120px" Caption="Video" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn FieldName="Field1" Name="glpDField1" ShowInCustomizationForm="True" VisibleIndex="20" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field2" Name="glpDField2" ShowInCustomizationForm="True" VisibleIndex="21" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field3" Name="glpDField3" ShowInCustomizationForm="True" VisibleIndex="22" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field4" Name="glpDField4" ShowInCustomizationForm="True" VisibleIndex="23" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field5" Name="glpDField5" ShowInCustomizationForm="True" VisibleIndex="24" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field6" Name="glpDField6" ShowInCustomizationForm="True" VisibleIndex="25" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field7" Name="glpDField7" ShowInCustomizationForm="True" VisibleIndex="26" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field8" Name="glpDField8" ShowInCustomizationForm="True" VisibleIndex="27" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field9" Name="glpDField9" ShowInCustomizationForm="True" VisibleIndex="28" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                    </Columns>
                                                  </dx:ASPxGridView>
                                                </tr>

                                      </table>
                                      <table>
                                        <tr>

                                        </tr>
                                      </table>
                                    </div>
                                  </div>
                                  <div class="tab">
                                    <input type="radio" id="tab-5" name="tab-group-1" />
                                    <label for="tab-5">Embroidery</label>
                                    <div class="content">
                                      <table>
                                        <tr>


                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="Effciency" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speEfficiencyE" runat="server" Width="170px" ClientInstanceName="speEfficiencyE" SpinButtons-ShowIncrementButtons="false" MinValue="0" MaxValue="999" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateE" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="Allowance" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Height="5px"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speAllowanceE" runat="server" Width="170px" ClientInstanceName="speAllowanceE" MinValue="0" MaxValue="999" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateE" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>




                                          <tr>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="Total Observed Time" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speTotalObservedTimeE" runat="server" Width="170px" ClientInstanceName="speTotalObservedTimeE" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%--    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="Basic Minutes" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speBasicMinutesE" runat="server" Width="170px" ClientInstanceName="speBasicMinutesE" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%-- <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>
                                          </tr>
                                          <tr>


                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel42" runat="server" Text="SAM" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speSAME" runat="server" Width="170px" ClientInstanceName="speSAME" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ReadOnly="true" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                              HorizontalAlign="Right">
                                                <ClientSideEvents Validation="OnValidation" />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="Minimum Wage" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxGridLookup ID="cbRegionE" runat="server" ClientInstanceName="CINRegionE" DataSourceID="RegionLookup" KeyFieldName="Region" TextFormatString="{0}" Width="170px" ValueField="Code" TextField="Description">
                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                  <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                  <Settings ShowFilterRow="True"></Settings>
                                                  <SettingsPager PageSize="3"></SettingsPager>
                                                </GridViewProperties>
                                                <Columns>
                                                  <dx:GridViewDataTextColumn FieldName="Region" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="NonCompliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Compliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents ValueChanged=" function (s, e){  var g = CINRegionE.GetGridView();  if (CINBrand.GetText() == 'WRANGLER') {g.GetRowValues(g.GetFocusedRowIndex(),'Compliance',CascadeCodeE);} else {g.GetRowValues(g.GetFocusedRowIndex(),'NonCompliance',CascadeCodeE);}  }"
                                                />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxGridLookup>

                                            </td>
                                            
                                                <td>

                                                
                                                </td>
                                          </tr>


                                          <tr>
                                            <tr>


                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel44" runat="server" Text="Labor Cost" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                <dx:ASPxSpinEdit ID="speLaborCostE" runat="server" Width="170px" ClientInstanceName="speLaborCostE" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                                AllowMouseWheel="false" HorizontalAlign="Right">
                                                  <ClientSideEvents Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                    <dx:ASPxSpinEdit ID="speMinimumWageE" runat="server" ReadOnly="true"  Width="170px" ClientInstanceName="speMinimumWageE" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                  HorizontalAlign="Right">
                                                    <ClientSideEvents ValueChanged="autocalculateE" Validation="OnValidation" />
                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                      <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                    <InvalidStyle BackColor="Pink">
                                                    </InvalidStyle>
                                                  </dx:ASPxSpinEdit>

                                              </td>
                                            </tr>
                                            <tr>

                                              <tr>
                                                <tr>


                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel46" runat="server" Text="Cost" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                    <dx:ASPxSpinEdit ID="speCostE" runat="server" Width="170px" ClientInstanceName="speCostE" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                    HorizontalAlign="Right">
                                                      <ClientSideEvents Validation="OnValidation" />
                                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" />
                                                      </ValidationSettings>
                                                      <InvalidStyle BackColor="Pink">
                                                      </InvalidStyle>
                                                    </dx:ASPxSpinEdit>
                                                  </td>

                                                     <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel60" runat="server" Text="Mark-up" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                <dx:ASPxSpinEdit ID="speMarkupE" runat="server" Width="170px" ClientInstanceName="speMarkupE" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                HorizontalAlign="Right">
                                                  <ClientSideEvents ValueChanged="autocalculateE" Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                                  </td>

                                                </tr>

                                                <tr>
                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                </tr>

                                                <tr>




                                                  <dx:ASPxGridView ID="gvE" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="gvE" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                  OnRowValidating="grid_RowValidating" KeyFieldName="LineNumber" EnableRowsCache="false">
                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" />
                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                    <Columns>
                                                      <dx:GridViewDataTextColumn FieldName="DocNumber" Width="0px" VisibleIndex="0">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                        </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="90px">
                                                        <CustomButtons>

                                                          <dx:GridViewCommandColumnCustomButton ID="DeleteE">
                                                            <Image IconID="actions_cancel_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                          <dx:GridViewCommandColumnCustomButton ID="ViewTransactionE">
                                                            <Image IconID="find_find_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                      </dx:GridViewCommandColumn>
                                                      <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Visible="true" UnboundType="String" Width="0px" ReadOnly="true">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False"></PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <%--< RA START Lookup  --%>
                                                        <dx:GridViewDataTextColumn FieldName="OperationCode" VisibleIndex="3" Width="100px" Name="glOBcode">
                                                          <PropertiesTextEdit ClientInstanceName="glOBcode">
                                                            <ClientSideEvents KeyDown="function(s,e){FieldKeyDown(s,e,'OBCode',s.GetText(),'EMBROIDERY');}" KeyUp="function(s,e){FieldKeyup(e,'OBCode',s.GetText(),'')}" KeyPress="fieldKeyPress" />
                                                          </PropertiesTextEdit>

                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Steps" VisibleIndex="4" Width="120px" Caption="Steps" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Parts" VisibleIndex="4" Width="120px" Caption="Parts" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="OpsBreakdown" VisibleIndex="4" Width="120px" Caption="OpsBreakdown" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="MachineType" VisibleIndex="4" Width="120px" Caption="MachineType" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="ObservedTime" VisibleIndex="4" Width="120px" Caption="ObservedTime" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="SAM" VisibleIndex="4" Width="120px" Caption="SAM" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Video" VisibleIndex="4" Width="120px" Caption="Video" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn FieldName="Field1" Name="glpDField1" ShowInCustomizationForm="True" VisibleIndex="20" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field2" Name="glpDField2" ShowInCustomizationForm="True" VisibleIndex="21" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field3" Name="glpDField3" ShowInCustomizationForm="True" VisibleIndex="22" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field4" Name="glpDField4" ShowInCustomizationForm="True" VisibleIndex="23" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field5" Name="glpDField5" ShowInCustomizationForm="True" VisibleIndex="24" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field6" Name="glpDField6" ShowInCustomizationForm="True" VisibleIndex="25" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field7" Name="glpDField7" ShowInCustomizationForm="True" VisibleIndex="26" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field8" Name="glpDField8" ShowInCustomizationForm="True" VisibleIndex="27" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field9" Name="glpDField9" ShowInCustomizationForm="True" VisibleIndex="28" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                    </Columns>
                                                  </dx:ASPxGridView>
                                                </tr>

                                      </table>
                                      <table>
                                        <tr>

                                        </tr>
                                      </table>
                                    </div>
                                  </div>
                                  <div class="tab">
                                    <input type="radio" id="tab-6" name="tab-group-1" />
                                    <label for="tab-6">Printing</label>
                                    <div class="content">
                                      <table>
                                        <tr>


                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel47" runat="server" Text="Effciency" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speEfficiencyP" runat="server" Width="170px" ClientInstanceName="speEfficiencyP" SpinButtons-ShowIncrementButtons="false" MinValue="0" MaxValue="999" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateP" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                          </td>

                                          <td>
                                            <dx:ASPxLabel ID="ASPxLabel48" runat="server" Text="Allowance" />
                                          </td>

                                          <td>
                                            <dx:ASPxLabel Text="" runat="server" Height="5px"> </dx:ASPxLabel>
                                          </td>
                                          <td>

                                            <dx:ASPxSpinEdit ID="speAllowanceP" runat="server" Width="170px" ClientInstanceName="speAllowanceP" MinValue="0" MaxValue="999" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                            AllowMouseWheel="false" HorizontalAlign="Right">
                                              <ClientSideEvents ValueChanged="autocalculateP" Validation="OnValidation" />
                                              <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                              </ValidationSettings>
                                              <InvalidStyle BackColor="Pink">
                                              </InvalidStyle>
                                            </dx:ASPxSpinEdit>
                                          </td>




                                          <tr>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel49" runat="server" Text="Total Observed Time" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speTotalObservedTimeP" runat="server" Width="170px" ClientInstanceName="speTotalObservedTimeP" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%--    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel50" runat="server" Text="Basic Minutes" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speBasicMinutesP" runat="server" Width="170px" ClientInstanceName="speBasicMinutesP" SpinButtons-ShowIncrementButtons="false" ReadOnly="true" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                              AllowMouseWheel="false" HorizontalAlign="Right">
                                                <%-- <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>--%>
                                              </dx:ASPxSpinEdit>
                                            </td>
                                          </tr>
                                          <tr>


                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel51" runat="server" Text="SAM" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxSpinEdit ID="speSAMP" runat="server" Width="170px" ClientInstanceName="speSAMP" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ReadOnly="true" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                              HorizontalAlign="Right">
                                                <ClientSideEvents Validation="OnValidation" />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxSpinEdit>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                            </td>

                                            <td>
                                              <dx:ASPxLabel ID="ASPxLabel52" runat="server" Text="Minimum Wage" />
                                            </td>

                                            <td>
                                              <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                            </td>
                                            <td>

                                              <dx:ASPxGridLookup ID="cbRegionP" runat="server" ClientInstanceName="CINRegionP" DataSourceID="RegionLookup" KeyFieldName="Region" TextFormatString="{0}" Width="170px" ValueField="Code" TextField="Description">
                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                  <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                  <Settings ShowFilterRow="True"></Settings>
                                                  <SettingsPager PageSize="3"></SettingsPager>
                                                </GridViewProperties>
                                                <Columns>
                                                  <dx:GridViewDataTextColumn FieldName="Region" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="NonCompliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                  <dx:GridViewDataTextColumn FieldName="Compliance" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                  </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents ValueChanged=" function (s, e){  var g = CINRegionP.GetGridView();  if (CINBrand.GetText() == 'WRANGLER') {g.GetRowValues(g.GetFocusedRowIndex(),'Compliance',CascadeCodeP);} else {g.GetRowValues(g.GetFocusedRowIndex(),'NonCompliance',CascadeCodeP);}  }"
                                                />
                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                  <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="Pink">
                                                </InvalidStyle>
                                              </dx:ASPxGridLookup>

                                            </td>
                                          
                                                <td>

                                                 
                                                </td>
                                          </tr>


                                          <tr>
                                            <tr>


                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel53" runat="server" Text="Labor Cost" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                <dx:ASPxSpinEdit ID="speLaborCostP" runat="server" Width="170px" ClientInstanceName="speLaborCostP" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}"
                                                AllowMouseWheel="false" HorizontalAlign="Right">
                                                  <ClientSideEvents Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                              </td>

                                              <td>
                                                <dx:ASPxLabel ID="ASPxLabel54" runat="server" Text="" />
                                              </td>

                                              <td>
                                                <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                              </td>
                                              <td>

                                                   <dx:ASPxSpinEdit ID="speMinimumWageP" runat="server" ReadOnly="true"  Width="170px" ClientInstanceName="speMinimumWageP" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                  HorizontalAlign="Right">
                                                    <ClientSideEvents ValueChanged="autocalculateP" Validation="OnValidation" />
                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                      <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                    <InvalidStyle BackColor="Pink">
                                                    </InvalidStyle>
                                                  </dx:ASPxSpinEdit>

                                          
                                              </td>
                                            </tr>
                                            <tr>

                                              <tr>
                                                <tr>


                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel55" runat="server" Text="Cost" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                    <dx:ASPxSpinEdit ID="speCostP" runat="server" Width="170px" ClientInstanceName="speCostP" ReadOnly="true" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                    HorizontalAlign="Right">
                                                      <ClientSideEvents Validation="OnValidation" />
                                                      <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" />
                                                      </ValidationSettings>
                                                      <InvalidStyle BackColor="Pink">
                                                      </InvalidStyle>
                                                    </dx:ASPxSpinEdit>
                                                  </td>

                                                     <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="100"> </dx:ASPxLabel>
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel ID="ASPxLabel61" runat="server" Text="Mark-up" />
                                                  </td>

                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                  <td>

                                                        <dx:ASPxSpinEdit ID="speMarkupP" runat="server" Width="170px" ClientInstanceName="speMarkupP" SpinButtons-ShowIncrementButtons="false" NullDisplayText="0.00" ConvertEmptyStringToNull="False" NullText="0.00" DisplayFormatString="{0:N}" AllowMouseWheel="false"
                                                HorizontalAlign="Right">
                                                  <ClientSideEvents ValueChanged="autocalculateP" Validation="OnValidation" />
                                                  <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" />
                                                  </ValidationSettings>
                                                  <InvalidStyle BackColor="Pink">
                                                  </InvalidStyle>
                                                </dx:ASPxSpinEdit>
                                                  </td>


                                                </tr>

                                                <tr>
                                                  <td>
                                                    <dx:ASPxLabel Text="" runat="server" Width="5"> </dx:ASPxLabel>
                                                  </td>
                                                </tr>

                                                <tr>




                                                  <dx:ASPxGridView ID="gvP" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="gvP" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize"
                                                  OnRowValidating="grid_RowValidating" KeyFieldName="LineNumber" EnableRowsCache="false">
                                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" />
                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                    <Columns>
                                                      <dx:GridViewDataTextColumn FieldName="DocNumber" Width="0px" VisibleIndex="0">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                        </PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="90px">
                                                        <CustomButtons>

                                                          <dx:GridViewCommandColumnCustomButton ID="DeleteP">
                                                            <Image IconID="actions_cancel_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                          <dx:GridViewCommandColumnCustomButton ID="ViewTransactionP">
                                                            <Image IconID="find_find_16x16"></Image>
                                                          </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                      </dx:GridViewCommandColumn>
                                                      <dx:GridViewDataTextColumn FieldName="LineNumber" VisibleIndex="2" Visible="true" UnboundType="String" Width="0px" ReadOnly="true">
                                                        <PropertiesTextEdit ConvertEmptyStringToNull="False"></PropertiesTextEdit>
                                                      </dx:GridViewDataTextColumn>
                                                      <%--< RA START Lookup  --%>
                                                        <dx:GridViewDataTextColumn FieldName="OperationCode" VisibleIndex="3" Width="100px" Name="glOBcode">
                                                          <PropertiesTextEdit ClientInstanceName="glOBcode">
                                                            <ClientSideEvents KeyDown="function(s,e){FieldKeyDown(s,e,'OBCode',s.GetText(),'PRINTING');}" KeyUp="function(s,e){FieldKeyup(e,'OBCode',s.GetText(),'')}" KeyPress="fieldKeyPress" />
                                                          </PropertiesTextEdit>

                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Steps" VisibleIndex="4" Width="120px" Caption="Steps" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Parts" VisibleIndex="4" Width="120px" Caption="Parts" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="OpsBreakdown" VisibleIndex="4" Width="120px" Caption="OpsBreakdown" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="MachineType" VisibleIndex="4" Width="120px" Caption="MachineType" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="ObservedTime" VisibleIndex="4" Width="120px" Caption="ObservedTime" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="SAM" VisibleIndex="4" Width="120px" Caption="SAM" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ReadOnly="true" FieldName="Video" VisibleIndex="4" Width="120px" Caption="Video" PropertiesTextEdit-EncodeHtml="false">
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn FieldName="Field1" Name="glpDField1" ShowInCustomizationForm="True" VisibleIndex="20" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field2" Name="glpDField2" ShowInCustomizationForm="True" VisibleIndex="21" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field3" Name="glpDField3" ShowInCustomizationForm="True" VisibleIndex="22" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field4" Name="glpDField4" ShowInCustomizationForm="True" VisibleIndex="23" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field5" Name="glpDField5" ShowInCustomizationForm="True" VisibleIndex="24" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field6" Name="glpDField6" ShowInCustomizationForm="True" VisibleIndex="25" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field7" Name="glpDField7" ShowInCustomizationForm="True" VisibleIndex="26" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field8" Name="glpDField8" ShowInCustomizationForm="True" VisibleIndex="27" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Field9" Name="glpDField9" ShowInCustomizationForm="True" VisibleIndex="28" UnboundType="String">
                                                        </dx:GridViewDataTextColumn>

                                                    </Columns>
                                                  </dx:ASPxGridView>
                                                </tr>

                                      </table>
                                      <table>
                                        <tr>

                                        </tr>
                                      </table>
                                    </div>
                                  </div>
                                </div>
                              </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>

                          <dx:LayoutGroup Visible="false">
                            <Items>
                              <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>

                                </LayoutItemNestedControlCollection>
                              </dx:LayoutItem>
                            </Items>
                          </dx:LayoutGroup>
                        </Items>
                      </dx:LayoutGroup>


                      <dx:LayoutGroup GroupBoxDecoration="None" Caption="Detail">
                        <Items>
                          <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                              <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:LayoutItemNestedControlContainer>
                                  <dx:ASPxGridView ID="gv2" SettingsPopup-EditForm-ShowHeader="false" runat="server" Settings-ShowGroupPanel="false" AutoGenerateColumns="False" Width="0px" ClientInstanceName="gv2" OnBatchUpdate="gv1_BatchUpdate" OnCellEditorInitialize="gv1_CellEditorInitialize"
                                  OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv1_CustomButtonInitialize" OnRowValidating="grid_RowValidating" KeyFieldName="LineNumber" EnableRowsCache="false">
                                    <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" CustomButtonClick="OnCustomClick" BatchEditRowValidating="Grid_BatchEditRowValidating" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <SettingsText EmptyDataRow="No records have been entered" EmptyHeaders=" " />
                                    <SettingsEditing Mode="Batch" />
                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="130" />
                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                    <Columns>




                                    </Columns>
                                  </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                              </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>
                        </Items>
                      </dx:LayoutGroup>




                    </Items>
                  </dx:LayoutGroup>
                  <dx:LayoutGroup Caption="User Defined" ColCount="2" Name="udf">
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
                            <dx:ASPxTextBox ID="txtHField5" runat="server" OnLoad="TextboxLoad">
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
                  <dx:ASPxButton ID="updateBtn" runat="server" Text="Save" AutoPostBack="False" CssClass="btn" ClientInstanceName="btn" UseSubmitBehavior="false" CausesValidation="true">
                    <ClientSideEvents Click="OnUpdateClick" />
                  </dx:ASPxButton>

                </div>
              </dx:PanelContent>
            </PanelCollection>
          </dx:ASPxPanel>
        </dx:PanelContent>
      </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxPopupControl ID="DeleteControl" runat="server" Width="250px" Height="100px" HeaderText="Warning!" CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="DeleteControl" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
      <ContentCollection>
        <dx:PopupControlContentControl runat="server">
          <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Are you sure you want to delete this specific document?" />
          <table>
            <tr>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td>
                <dx:ASPxButton ID="Ok" runat="server" Text="Ok" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                  <ClientSideEvents Click="function (s, e){ cp.PerformCallback('ConfDelete');  e.processOnServer = false;}" />
                </dx:ASPxButton>
              </td>
              <td>
                <dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                  <ClientSideEvents Click="function (s,e){ DeleteControl.Hide(); }" />
                </dx:ASPxButton>
              </td>
            </tr>
          </table>
        </dx:PopupControlContentControl>
      </ContentCollection>
    </dx:ASPxPopupControl>

  </form>




  <asp:ObjectDataSource ID="odsCutting" runat="server" DataObjectTypeName="Entity.SAM+SAMCutting" SelectMethod="getdetail" UpdateMethod="UpdateSAMCutting" TypeName="Entity.SAM+SAMCutting" DeleteMethod="DeleteSAMCutting" InsertMethod="AddSAMCutting">
    <SelectParameters>
      <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
      <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:SqlDataSource ID="sdsCutting" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.SAMdetail WHERE DocNumber IS NULL" OnInit="Connection_Init"></asp:SqlDataSource>


  <asp:ObjectDataSource ID="odsSewing" runat="server" DataObjectTypeName="Entity.SAM+SAMSewing" SelectMethod="getdetail" UpdateMethod="UpdateSAMSewing" TypeName="Entity.SAM+SAMSewing" DeleteMethod="DeleteSAMSewing" InsertMethod="AddSAMSewing">
    <SelectParameters>
      <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
      <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:SqlDataSource ID="sdsSewing" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.SAMdetail WHERE DocNumber IS NULL" OnInit="Connection_Init"></asp:SqlDataSource>


  <asp:ObjectDataSource ID="odsFinishing" runat="server" DataObjectTypeName="Entity.SAM+SAMFinishing" SelectMethod="getdetail" UpdateMethod="UpdateSAMFinishing" TypeName="Entity.SAM+SAMFinishing" DeleteMethod="DeleteSAMFinishing" InsertMethod="AddSAMFinishing">
    <SelectParameters>
      <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
      <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:SqlDataSource ID="sdsFinishing" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.SAMdetail WHERE DocNumber IS NULL" OnInit="Connection_Init"></asp:SqlDataSource>


  <asp:ObjectDataSource ID="odsWashing" runat="server" DataObjectTypeName="Entity.SAM+SAMWashing" SelectMethod="getdetail" UpdateMethod="UpdateSAMWashing" TypeName="Entity.SAM+SAMWashing" DeleteMethod="DeleteSAMWashing" InsertMethod="AddSAMWashing">
    <SelectParameters>
      <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
      <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:SqlDataSource ID="sdsWashing" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.SAMdetail WHERE DocNumber IS NULL" OnInit="Connection_Init"></asp:SqlDataSource>


  <asp:ObjectDataSource ID="odsEmbroidery" runat="server" DataObjectTypeName="Entity.SAM+SAMEmbroidery" SelectMethod="getdetail" UpdateMethod="UpdateSAMEmbroidery" TypeName="Entity.SAM+SAMEmbroidery" DeleteMethod="DeleteSAMEmbroidery" InsertMethod="AddSAMEmbroidery">
    <SelectParameters>
      <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
      <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:SqlDataSource ID="sdsEmbroidery" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.SAMdetail WHERE DocNumber IS NULL" OnInit="Connection_Init"></asp:SqlDataSource>


  <asp:ObjectDataSource ID="odsPrinting" runat="server" DataObjectTypeName="Entity.SAM+SAMPrinting" SelectMethod="getdetail" UpdateMethod="UpdateSAMPrinting" TypeName="Entity.SAM+SAMPrinting" DeleteMethod="DeleteSAMPrinting" InsertMethod="AddSAMPrinting">
    <SelectParameters>
      <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
      <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:SqlDataSource ID="sdsPrinting" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Production.SAMdetail WHERE DocNumber IS NULL" OnInit="Connection_Init"></asp:SqlDataSource>


  <asp:SqlDataSource ID="OperationTypeLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
SELECT Description FROM It.GenericLookup WHERE LookUpKey='OPSType'" OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="ProductClassLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
             SELECT ProductClassCode as Code,Description FROM Masterfile.ProductClass WHERE ISNULL(IsInactive,0)=0" OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="ProductCategoryLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
      SELECT ProductCategoryCode as Code,Description FROM Masterfile.ProductCategory WHERE ISNULL(IsInactive,0)=0 and ISNULL(ItemCategoryCode,0)=1" OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="BrandLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
      SELECT BrandCode as Code,BrandName as Description FROM Masterfile.Brand WHERE ISNULL(IsInactive,0)=0" OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="GenderLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
      SELECT GenderCode as Code,Description FROM Masterfile.Gender WHERE ISNULL(IsInactive,0)=0" OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="FitLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
      SELECT FitCode as Code,FitName as Description FROM Masterfile.Fit WHERE ISNULL(IsInactive,0) =0 " OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="PartsLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
     SELECT PartsCode as Code,Description FROM Masterfile.Parts WHERE ISNULL(IsInactive,0) =0 " OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="MachineLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="
    SELECT MachineCode as Code,Description FROM Masterfile.Machine WHERE ISNULL(IsInactive,0) =0 " OnInit="Connection_Init"></asp:SqlDataSource>

  <asp:SqlDataSource ID="SupplierCodelookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT SupplierCode,Name FROM Masterfile.[BPSupplierInfo] WHERE ISNULL([IsInactive],0) = 0" OnInit="Connection_Init">

  </asp:SqlDataSource>

  <asp:SqlDataSource ID="sdsRefCutting" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Masterfile.OpsBreakdown WHERE OBCode IS NULL" OnInit="Connection_Init">

  </asp:SqlDataSource>


  <asp:SqlDataSource ID="ProductGroupLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="" OnInit="Connection_Init"> </asp:SqlDataSource>
  <asp:SqlDataSource ID="ProductSubCategoryLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="" OnInit="Connection_Init"> </asp:SqlDataSource>


  <asp:SqlDataSource ID="RegionLookup" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT Region,Description,NonCompliance,Compliance FROM Masterfile.[minimumwage]" OnInit="Connection_Init">

  </asp:SqlDataSource>
</body>

</html>

