<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Translist.aspx.cs" Inherits="GWL.Translist" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register TagPrefix="wc" TagName="UpldDocsPopup" Src="~/WebControl/Accounting/wc_UpldDocsPopup.ascx" %>
<%@ Register TagPrefix="wc" TagName="DocRmkPopup" Src="~/WebControl/Accounting/wc_DocRmkPopup.ascx" %>
<!DOCTYPE html>
<meta name="viewport" content="width=device-width, user-scalable=no, maximum-scale=1.0, minimum-scale=1.0" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="~/Content/sweetalert-style.css" />
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="../js/Notification_popup.js" type="text/javascript"></script>
    <script src="../../Content/sweetalert.min.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        .dxpnlControl_Moderno {
            font: 14px 'Segoe UI','Helvetica Neue','Droid Sans',Arial,Tahoma,Geneva,Sans-serif;
            color: #2b2b2b;
            border: 0px solid #d1d1d1;
        }

        #form1 {
            height: 410px;
        }

        .dxgv /*cell class*/ {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        ::-moz-selection {
            background-color: Transparent;
            color: #000;
        }

        ::selection {
            background-color: Transparent;
            color: #000;
        }

        .myclass::-moz-selection,
        .myclass::selection {
            ...;
        }

        .d-none {
            display: none;
        }

        /*Carla add this styel start region*/
        .swal-icon, .swal-title {
            float: left;
        }

        .swal-title {
            font-size: 25px !important;
            font-weight: 600 !important;
            padding: 0 !important;
        }

        .swal-title, .swal-text {
            font: 12px 'Segoe UI', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif;
            font-style: normal;
            font-variant-ligatures: normal;
            font-variant-caps: normal;
            font-variant-numeric: normal;
            font-variant-east-asian: normal;
            font-weight: normal;
            font-stretch: normal;
            font-size: 15px;
            line-height: normal;
            font-family: "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif;
            color: #333333;
        }

        .swal-text {
            height: 200px;
            overflow-y: scroll;
        }

        #btnRight {
            float: left;
            margin-right: 320px;
            background-color: #78cbf2;
            color: #fff;
        }
    </style>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript">
        var Postback = true;
        function OnInitTrans(s, e) {
            //gv2.Refresh();
            AdjustSize();
            //console.log('here');
        }

        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }

        var gvhyt = 0;
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight);
            if (Postback) {
                Postback = false;
                gv2.SetHeight(height - 238);
                gvhyt = gv2.GetHeight();
                //console.log(gv2.GetHeight() + ' hehes')
            }
            else {
                //console.log(gv2.GetHeight() + ' - ' + gvhyt)
                if (gvhyt == gv2.GetHeight() + 16) {
                    gvhyt = gv2.GetHeight();
                    gv2.SetHeight(height - 237);
                    //console.log('ssss')
                }
                else if (gvhyt != gv2.GetHeight()) {
                    if (gv2.GetHeight() - 1 == gvhyt) {
                        gv2.SetHeight(height - 236.5);
                        //console.log('aaaa')
                    }
                    else {
                        gv2.SetHeight(height - 222);
                        if (gvhyt + 32 == gv2.GetHeight())
                            gv2.SetHeight(height - 238);
                        //console.log(gv2.GetHeight() + ' - ' + gvhyt)
                        //console.log('xxxx')
                    }
                }
                else
                    gv2.SetHeight(height - 237);
            }

            var width = Math.max(0, document.documentElement.clientWidth);
            gwlribbon.SetWidth(width - 10);
        }

                     <%--<//RA 2016-05-14 > --%>
        function gridcpendcallback(s, e) {
            if (s.cp_extract2) {
                delete (s.cp_extract2);
                document.getElementById("btnXlsxExport2").click();
            }
        }

        function gridView_EndCallback(s, e) {

            if (s.cp_refresh) {
                location.reload();
            }

            if (s.cp_docnum) {
                delete (s.cp_docnum);
                DocSettings.Show();
                isBusy = false;
            }


            var ventry = "";
            var vshow = false;
            switch (s.cp_redirect) {
                case "view":
                    ventry = 'V'
                    vshow = true
                    break;
                case "edit":
                    ventry = 'E'
                    vshow = true
                    break;
                case "delete":
                    ventry = 'D'
                    vshow = true
                    break;
            }

            if (s.cp_masterfile) {
                delete (cp_masterfile);
                ventry = 'N';
                vshow = true;
            }

            if (vshow) {
                window.open(s.cp_frm + '?entry=' + ventry + '&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters +
                    '&iswithdetail=' + s.cp_iswithdetail + '&docnumber=' + encodeURIComponent(s.cp_docnumber), '_blank');
                delete (s.cp_redirect);
                delete (s.cp_masterfile);
                RefreshNotif.Show();
            }

            //2015-06-16    JDSD    Comment old Code
            //if (s.cp_redirect == "view") {
            //    window.open(s.cp_frm + '?entry=V&transtype=' + s.cp_transtype, '_blank');//view
            //    delete (s.cp_redirect);
            //}
            //if (s.cp_redirect == "edit") {
            //    window.open(s.cp_frm + '?entry=E&transtype=' + s.cp_transtype, '_blank');//edit
            //    delete (s.cp_redirect);
            //}
            //if (s.cp_redirect == "delete") {
            //    window.open(s.cp_frm + '?entry=D&transtype=' + s.cp_transtype, '_blank');//edit
            //    delete (s.cp_redirect);
            //}

            if (s.cp_redirect == "error") {
                errorlabel.SetText(s.cp_errormes);
                errorpop.Show();
                delete (s.cp_errormes);
                delete (s.cp_redirect);
            }

            if (!s.cp_docnum) {
                isBusy = false;
                //scheduleGridRefresh(s);
            }

            if (s.cp_message != null) {
                alertMesage("", "", s.cp_message);
                delete (s.cp_message);
            }

            AdjustSize();
        }

        function OnContextMenuItemClick(sender, args) {
            if (args.item.name != "Unfreeze Columns" && args.item.name != "Freeze Columns" && args.item.name != "Hide Columns") {
                args.processOnServer = true;
            }
            else if (args.item.name == "Unfreeze Columns") {
                gv2.Refresh();
            }
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function OnContextMenu(s, e) {
            var itemViewRow = e.menu.GetItemByName('View');
            var itemEditRow = e.menu.GetItemByName('Edit');
            var itemDelRow = e.menu.GetItemByName('Delete');
            var itemNewRow = e.menu.GetItemByName('New');

            itemViewRow.SetEnabled(false);
            itemEditRow.SetEnabled(false);
            itemDelRow.SetEnabled(false);
            itemNewRow.SetEnabled(false);

            var access = getParameterByName('access');

            if (access.indexOf('A') >= 0) {
                itemNewRow.SetEnabled(true);
            }
            if (access.indexOf('V') >= 0) {
                itemViewRow.SetEnabled(true);
                itemViewRow.SetEnabled(s.GetSelectedRowCount() <= 1);
            }
            if (access.indexOf('E') >= 0) {
                itemEditRow.SetEnabled(true);
                itemEditRow.SetEnabled(s.GetSelectedRowCount() <= 1);
            }
            if (access.indexOf('D') >= 0) {
                itemDelRow.SetEnabled(true);
                itemDelRow.SetEnabled(s.GetSelectedRowCount() <= 1);
            }
            s.SetFocusedRowIndex(e.index);
            e.showBrowserMenu = false;
        }

        var transclick = "";
        function cp_EndCallback(s, e) {
            if (s.cp_import) {
                delete (s.cp_import);
                ImportCSheet.Show();
            }

            if (s.cp_errorimport != null) {
                alertMesage("error", "", s.cp_errorimport);
                delete (s.cp_errorimport);
            }
           

            if (s.cp_docnum) {
                delete (s.cp_docnum);
                DocSettings.Show();
            }

            if (s.cp_print) {
                delete (s.cp_print);
                Print.Show();
            }

            if (s.cp_HRprint) {
                delete (s.cp_HRprint);
                HRPrintPop.Show();
            }

            if (s.cp_HRPrintSI) {
                delete (s.cp_HRPrintSI);
                HRPrintSI.Show();
            }
            if (s.cp_ShowReturn) {
                //show the afterblast detail if there's no error
                delete (s.cp_ShowReturn);
                delete (s.cp_ReturnValidate);
                //SubmitAlert.Hide();
                ReturnPop.Show();
                conf.Hide();

            }
            if (s.cp_ShowAB) {
                //show the afterblast detail if there's no error
                delete (s.cp_ShowAB);
                delete (s.cp_AfterBlastValidate);
                //SubmitAlert.Hide();
                AfterBlastPop.Show();
                conf.Hide();

            }
            if (s.cp_Charges) {
                let alertType = "";

                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    alertType = "error";
                    alertMesage(alertType, "No select rows in table", "Please select on table");
                }
                delete (s.cp_Charges);
                if (alertType == "") {
                    ChargesPop.Show();
                }




            }
            if (s.cp_Comi) {
                delete (s.cp_Comi);
                ComiPop.Show();

            }
            if (s.cp_Replenish) {
                delete (s.cp_Replenish);
                ReplenishPop.Show();

            }

            //RCD 2023-09-15
            if (s.cp_Assign) {
                //let alertType = "";

                //if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                // alertType = "error";
                //alertMesage(alertType, "No select rows in table", "Please select on table");
                //}
                delete (s.cp_Assign);
                //if (alertType == "") {
                AssignPop.Show();
                // }

            }
            if (s.cp_MultiAssign) {
                delete (s.cp_MultiAssign);
                delete (s.cp_RAssignProcess);
                MultiAssignPop.Show();
                conf.Hide();
            }
            if (s.cp_SingleAssign) {
                delete (s.cp_SingleAssign);
                delete (s.cp_RAssignProcess);
                AssignPop.Show();
                conf.Hide();
            }
            //RCD  2023-09-18
            if (s.cp_templateextract) {
                window.open('../Template/' + getParameterByName('prompt') + '.xlsx', '_blank');
                //alertMesage("success", getParameterByName('prompt'), "Export of import template completed.");
                delete (s.cp_templateextract);
                RefreshNotif.Show();
            }

            //----START-------inclusion 1/26/2024 from mli
            // 2023-07-07   TL  Download Softcopy
            if (s.cp_dwnlddocs) {
                console.log(s.cp_dwnlddocs);
                var FileList = s.cp_dwnlddocs.split('|');
                for (var i = 0; i < FileList.length; i++) {
                    //window.open('file://192.168.181.32/IT/GWL-MLI-Documents/' + FileList[i], '_blank');
                    window.open('./TempDocuments/' + FileList[i], '_blank');
                }
                alert('Download of document soft copy completed');
                delete (s.cp_dwnlddocs);
            }

          
            if (s.cp_uploaddocs) {
                delete (s.cp_uploaddocs);
                UpldDocsTranType.SetText(s.cp_uploadtrntyp);
                UpldDocsDocNum.SetText(s.cp_uploaddocnum);
                delete (s.cp_uploadtrntyp);
                delete (s.cp_uploaddocnum);
                if (s.cp_appenddocs) {
                    delete (s.cp_appenddocs);
                    UpldDocsMode.SetText('APPEND');
                }
                else {
                    UpldDocsMode.SetText('UPLOAD');
                    UpldDocsDocDate.SetValue(s.cp_uploaddocdte);
                    delete (s.cp_uploaddocdte);
                }
                // 2023-12-04   TL  Auxiliary Docs
                if (s.cp_auxiliarydocs) {
                    delete (s.cp_auxiliarydocs)
                    UpldDocsPopup.SetHeaderText('Upload Auxiliary Docs');
                    UpldDocsDocType.SetText('AUX');
                    UpldDocs_TF.SetText('');
                    UpldDocs_TF.SetEnabled(false);
                    UpldDocs_btnTF.SetEnabled(false);
                }
                else {
                    UpldDocsPopup.SetHeaderText('Upload Document');
                    UpldDocsDocType.SetText('MAIN');
                    UpldDocs_TF.SetText('');
                    UpldDocs_TF.SetEnabled(true);
                    UpldDocs_btnTF.SetEnabled(true);
                    cp_UpldDocs.PerformCallback('GetDir:');
                }
                // 2023-12-04   TL  (End)
                UpldDocsPopup.Show();
            }

            if (s.cp_docremarks) {
                delete (s.cp_docremarks);
                DocRmk_SetRmkType(s.cp_drmkrmktyp, s.cp_drmkdoclst, s.cp_drmkrmrks);
                DocRmkTranType.SetText(s.cp_drmktrntyp);
                DocRmkDocNum.SetText(s.cp_drmkdocnum);
                delete (s.cp_drmkrmktyp);
                delete (s.cp_drmkdoclst);
                delete (s.cp_drmktrntyp);
                delete (s.cp_drmkdocnum);
                delete (s.cp_drmkrmrks);
                DocRmkPopup.Show();
            }
            //----END------- inclusion 1/26/2024 from mli

            if (s.cp_JOprint) {
                delete (s.cp_JOprint);
                cpprint.PerformCallback("JOPrint");
            }

            if (s.cp_PrintBilling) {
                delete (s.cp_PrintBilling);
                cpprint.PerformCallback("PrintBilling");
            }

            if (s.cp_copy) {
                delete (s.cp_copy);
                Copy.Show();
                cpcopy.PerformCallback('databind');
            }

            //Genrev 01/18/2016 Added export
            if (s.cp_customexport) {
                exportdocnum = s.cp_customexportitem;
                exporttrans = s.cp_customexporttrans;
                delete (s.cp_customexportitem);
                delete (s.cp_customexporttrans);
                CustomExportFunc();
            }

            if (s.cp_onexport) {
                delete (s.cp_onexport);
                CSheet.Show();
                var docnumber = s.cp_expdocnum;
                var transtype = s.cp_transtype;
                CSheet.SetContentUrl('WMS/frmCountSheet.aspx?entry=V&docnumber=' + docnumber + '&transtype=' + transtype +
                    '&linenumber=null&status=null');
                delete (s.cp_expdocnum);
                delete (s.cp_transtype);
            }

            if (s.cp_exporting) {
                delete (s.cp_exporting);
                document.getElementById("btnXlsxExport").click();
            }

            if (s.cp_invcor) {
                delete (s.cp_invcor);
                INVpop.Show();
            }
            //RA 2016-05-14
            if (s.cp_extract) {
                delete (s.cp_extract);
                document.getElementById("btnXlsxExport1").click();
            }
            if (s.cp_forsubmission) {
                delete (s.cp_forsubmission);

                var key = "renats";
                for (var i = 0; i < gv2.GetColumnsCount(); i++)
                    if (gv2.GetAutoFilterEditor(i) != null) {

                        var editor = gv2.GetColumn(i).fieldName;
                        console.log(editor)
                        if (editor == "CancelledBy") {

                            key = "jayr";
                        }
                    }

                if (key == "jayr") {
                    var filterCondition = "[SubmittedBy] = " + null + " AND [CancelledBy]  = " + null + " OR [SubmittedBy] = '' AND [CancelledBy]  = '' ";

                }
                else {
                    var filterCondition = "SubmittedBy =  " + null + " OR SubmittedBy = ''  ";


                }

                gv2.ApplyFilter(filterCondition);

            }
            if (s.cp_forcost) {
                delete (s.cp_forcost);

                var key1 = "renato";
                for (var i = 0; i < gv2.GetColumnsCount(); i++)
                    if (gv2.GetAutoFilterEditor(i) != null) {

                        var editor = gv2.GetColumn(i).fieldName;
                        console.log(editor)
                        if (editor == "CancelledBy") {

                            key1 = "xile";
                        }
                    }

                if (key1 == "xile") {
                    var filterCondition = "[SubmittedBy]!='' AND SubmittedBy!=  " + null + " AND ([CancelledBy]=" + null + "  OR    [CancelledBy]='')   AND ([CostingSubmittedBy]  = " + null + "  OR [CostingSubmittedBy]  = '') ";

                }
                else {
                    var filterCondition = "SubmittedBy <>  " + null + "  AND [CostingSubmittedBy]  = " + null + " OR SubmittedBy  <>  ''   AND [CostingSubmittedBy]  = '' ";


                }
                console.log(filterCondition);
                gv2.ApplyFilter(filterCondition);

            }
            if (s.cp_forautosi) {
                delete (s.cp_forautosi);

                var key1 = "renato";
                for (var i = 0; i < gv2.GetColumnsCount(); i++)
                    if (gv2.GetAutoFilterEditor(i) != null) {

                        var editor = gv2.GetColumn(i).fieldName;
                        console.log(editor)
                        if (editor == "CancelledBy") {

                            key1 = "xile";
                        }
                    }

                if (key1 == "xile") {
                    var filterCondition = "[SubmittedBy]!='' AND SubmittedBy!=  " + null + " AND ([CancelledBy]=" + null + "  OR    [CancelledBy]='')   AND ([AutoSI]  = " + null + "  OR [AutoSI]  = '') ";

                }
                else {
                    var filterCondition = "[SubmittedBy]!=  " + null + "  AND [AutoSI]  = " + null + " OR [SubmittedBy]!= ''   AND [AutoSI]  = '' ";


                }
                console.log(filterCondition);
                gv2.ApplyFilter(filterCondition);

            }

            //GC Added code 6/2/106
            if (s.cp_ForSubmitColRep) {
                delete (s.cp_ForSubmitColRep);

                var key = "";
                for (var i = 0; i < gv2.GetColumnsCount(); i++)
                    if (gv2.GetAutoFilterEditor(i) != null) {

                        var editor = gv2.GetColumn(i).fieldName;
                        console.log(editor)
                        if (editor == "CancelledBy") {
                            key = "1";
                        }
                    }

                if (key == "1") {
                    var filterCondition = "([SubmittedBy] ='' OR SubmittedBy =  " + null + ") AND ([CancelledBy]=" + null + "  OR    [CancelledBy]='')   AND ([IsForDeposit]  = " + null + "  OR [IsForDeposit]  = 'FALSE') ";
                }

                gv2.ApplyFilter(filterCondition);
            }
            //end

            //#region checkConfirmation
            if (s.cp_onsubmit) {
                delete (s.cp_onsubmit);
                transclick = "submit";
                conf.Show();
            }

            //9/16/16   GC  Added code
            if (s.cp_onUnsubmit) {
                delete (s.cp_onUnsubmit);
                transclick = "Unsubmit";
                conf.Show();
            }
            //end

            if (s.cp_onapprove) {
                delete (s.cp_onapprove);
                transclick = "approve";
                conf.Show();
            }


            if (s.cp_voidDR) {
                delete (s.cp_voidDR);
                transclick = "voidDR";
                conf.Show();
            }

            if (s.cp_onapproveJODueDate) {
                delete (s.cp_onapproveJODueDate)
                transclick = "approve";
                conf.Show;
            }

            if (s.cp_AutoCounter) {
                delete (s.cp_AutoCounter);
                CounterRangeWindow.Show();
            }

            if (s.cp_onapproveJODueDatePrompt) {
                delete (s.cp_onapproveJODueDatePrompt)
                JODueDate.Show();
            }

            if (s.cp_onunapprove) {
                delete (s.cp_onunapprove);
                transclick = "unapprove";
                conf.Show();
            }

            if (s.cp_onauthorize) {
                delete (s.cp_onauthorize);
                transclick = "authorize";
                confclick(); // TLAV 03-08-2016
                //conf.Show();
            }

            if (s.cp_funcauthorize) {
                delete (s.cp_funcauthorize);
                transclick = "Funcgroup";
                confclick(); // KATE 

            }

            if (s.cp_onauthorizeinit) { // TLAV 03-08-2016
                authpw.SetText("");
                delete (s.cp_onauthorizeinit);
                transclick = "authorizeinit";
                conf.Show();
            }

            if (s.cp_InputPass) {
                delete (s.cp_InputPass);
                authpw.SetText("");
                authorizationwindow.Show();
            }

            if (s.cp_onstartdis) {
                delete (s.cp_onstartdis);
                transclick = "startdis";
                conf.Show();
            } //TLAV added 2016-04-18

            if (s.cp_unprint) {
                delete (s.cp_unprint);
                transclick = "unprint";
                conf.Show();
            }

            if (s.cp_multiprint2) {
                delete (s.cp_multiprint2);
                if (s.cp_report != null) {
                    console.log(s.cp_reprint);
                    window.open("./WebReports/ReportViewer.aspx?val=~" + s.cp_report + '&transtype=' + s.cp_transtype + '&docnumber=' + s.cp_docnumber + '&reprinted=' + s.cp_reprint, '_blank');
                    gv2.Refresh();
                    delete (s.cp_docnumber);
                    delete (s.cp_report);
                    delete (s.cp_transtype);
                    delete (s.cp_reprint);
                }
                else {
                    alertMesage("", "", "No report for this transaction!");
                }
            }

            if (s.cp_multiprint) {
                delete (s.cp_multiprint);
                transclick = "multiprint";
                conf.Show();
            }

            if (s.cp_onpartialsubmit) {
                delete (s.cp_onpartialsubmit);
                transclick = "partialsubmit";
                conf.Show();
            }

            if (s.cp_RProdSched) {
                delete (s.cp_RProdSched);
                window.open('./Production/frmProdScheduling.aspx?entry=N&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters + '&docnumber=' + s.cp_docnumber, '_blank');
                RefreshNotif.Show();
            }

            if (s.cp_pcount) {
                delete (s.cp_pcount);
                transclick = "pcount";
                conf.Show();
            }

            if (s.cp_onFC) {
                delete (s.cp_onFC);
                transclick = "forceclose";
                conf.Show();
            }

            //genrev 01/23/2016
            if (s.cp_onCancel) {
                delete (s.cp_onCancel);
                transclick = "cancellation";
                conf.Show();
            }

            if (s.cp_onPRJO) {
                delete (s.cp_onPRJO);
                transclick = "PRJO";
                conf.Show();
            }

            if (s.cp_onGenDepreciation) {
                delete (s.cp_onGenDepreciation);
                transclick = "GenDepreciation";
                conf.Show();
            }

            if (s.cp_onracctgsubmit) {
                delete (s.cp_onracctgsubmit);
                transclick = "racctgsub";
                conf.Show();
            }
            //end

            if (s.cp_onPCVar) {
                delete (s.cp_onPCVar);
                transclick = "PCVar";
                conf.Show();
            }

            if (s.cp_onIsFinal) {
                delete (s.cp_onIsFinal);
                cp.PerformCallback('RIsFinalInitiate');
            }

            if (s.cp_onPCIsFinal) {
                delete (s.cp_onPCIsFinal);
                transclick = "PCIsFinal";
                conf.Show();
            }

            if (s.cp_onIncExc) {
                delete (s.cp_onIncExc);
                transclick = "ExcludeBillingInitiate";
                conf.Show();
            }

            //genrev 01/30/2016
            if (s.cp_DFPerform) {
                DFDocNum.SetText(s.cp_DFDocNum);
                delete (s.cp_DFPerform);
                delete (s.cp_DFDocNum);
                DFWindow.Show();
            }
            //end

            if (s.cp_onrwaybill) {
                delete (s.cp_onrwaybill);
                WayBillText.SetText(null);
                waybillpop.Show();
            }
            //#endregion

            //GC Added code 2-27-2016
            if (s.cp_issexec != null) {
                cp.PerformCallback("RSubmission");
                delete (s.cp_issexec);
                IssWindow.Hide();
                IssPass.SetText("");
            }

            if (s.cp_masterfile) {
                delete (cp_masterfile);
                window.open(s.cp_frm + '?entry=N&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters + '&docnumber=' + s.cp_docnumber, '_blank');
                RefreshNotif.Show();
            }
            function alertMesage(alertType, alertTitle, alertContent) {
                console.log('alertType:' + alertType + 'alertTitle:' + alertTitle + 'alertContent:' + alertContent);
                swal({
                    title: alertTitle,
                    text: alertContent,
                    icon: alertType,
                    buttons: "",
                    closeOnEsc: null,
                    closeOnClickOutside: null
                });

                document.getElementsByClassName("swal-button--confirm").item(0).style.display = "none";
                const bodyModal = document.getElementsByClassName("swal-modal")[0];
                const cellInput = document.createElement("textarea");
                cellInput.setAttribute("id", "msgError");
                cellInput.style.opacity = 0;
                cellInput.innerHTML = s.cp_submitalert;
                bodyModal.appendChild(cellInput);
                cellInput.style.height = "1px";

                const bodyAlert = document.getElementsByClassName("swal-button-container")[0];
                const cellButton = document.createElement("button");
                cellButton.setAttribute("type", "submit");
                cellButton.setAttribute("id", "btnRight")
                cellButton.setAttribute("class", "swal-button swal-button--cancel")
                cellButton.innerText = "Copy to Clipboard";
                cellButton.setAttribute("onclick", "var copyText = document.getElementById('msgError');copyText.select();document.execCommand('copy');");
                bodyAlert.appendChild(cellButton);

                const bodyAlert2 = document.getElementsByClassName("swal-button-container")[0];
                const cellButton2 = document.createElement("button");
                cellButton2.setAttribute("type", "submit");
                cellButton2.setAttribute("id", "btnsub")
                cellButton2.setAttribute("class", "swal-button swal-button--ok")
                cellButton2.innerText = "OK";
                cellButton2.setAttribute("onclick", "gv2.Refresh(); swal.close();");
                bodyAlert2.appendChild(cellButton2);
            }
            if (s.cp_submit != null) {

                //SubmitAlert.Show();
                //delete (s.cp_submit);
                //conf.Hide();
                console.log('countsubmited:' + s.cp_countsubmited + 'counttotal:' + s.cp_counttotal + 'submitalert:' + s.cp_submitalert + ' cpsuccess: ' + s.cp_success);
                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }

                else if (s.cp_submitalert.includes('Successfully Charged:')) {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_submit);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_ReturnValidate != null) {

                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table"); cp_ShowReturn
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_ReturnValidate);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
                ReturnPop.Hide();
            }
            if (s.cp_AfterBlastValidate != null) {
                //show the error of afterblast if the submittedby column is null or putaway column is not null 
                //SubmitAlert.Show();
                //delete (s.cp_AfterBlastValidate);
                //conf.Hide();
                //AfterBlast.Hide();

                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_AfterBlastValidate);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
                AfterBlast.Hide();


            }
            if (s.cp_RAssignProcess != null) {

                //SubmitAlert.Show();
                //delete (s.cp_Truck);
                //conf.Hide();
                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_RAssignProcess);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_Truck != null) {

                //SubmitAlert.Show();
                //delete (s.cp_Truck);
                //conf.Hide();
                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_Truck);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_RRStartProcess != null) {
                //SubmitAlert.Show();
                //delete (s.cp_RRStartProcess);
                //conf.Hide();
                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_RRStartProcess);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_RREndProcess != null) {
                //SubmitAlert.Show();
                //delete (s.cp_RREndProcess);
                //conf.Hide();

                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_RREndProcess);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_RDepartureProcessInbound != null) {
                //SubmitAlert.Show();
                //delete (s.cp_RDepartureProcessInbound);
                //conf.Hide();
                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_RDepartureProcessInbound);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_RWRStartProcess != null) {
                //SubmitAlert.Show();
                //delete (s.cp_RWRStartProcess);
                //conf.Hide();
                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_RWRStartProcess);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_RWREndProcess != null) {
                //SubmitAlert.Show();
                //delete (s.cp_RWREndProcess);
                //conf.Hide();

                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_RWREndProcess);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_RDepartureProcess != null) {
                //SubmitAlert.Show();
                //delete (s.cp_RDepartureProcess);
                //conf.Hide();

                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_RDepartureProcess);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();
            }
            if (s.cp_TruckArrivalOutboundSubmit != null) {
                //SubmitAlert.Show();
                //delete (s.cp_TruckArrivalOutboundSubmit);
                //conf.Hide();

                if (gv2.GetSelectedRowCount() == 0 && s.cp_selectdata != "selectData") {
                    let alertType = "error";
                    alertMesage(alertType, "No selected row/s in table", "Please select on table");
                }
                else if (s.cp_countsubmited == s.cp_counttotal || s.cp_success == "success") {

                    let alertType = "success";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else if (s.cp_countsubmited == 0 || s.cp_counttotal == 0) {
                    let alertType = "error";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                else {
                    let alertType = "warning";
                    alertMesage(alertType, s.cp_modaltitle, s.cp_submitalert);
                }
                delete (s.cp_TruckArrivalOutboundSubmit);
                // 2020-09-28   TL  reset flags
                if (s.cp_selectdata != null) {
                    delete (s.cp_selectdata);
                }
                if (s.cp_success != null) {
                    delete (s.cp_success);
                }
                // 2020-09-28   TL  (End)
                conf.Hide();

            }
            if (s.cp_genbillinginfo) {
                delete (s.cp_genbillinginfo);
                transclick = "GenBillingInfo";
                conf.Show();
            }

            var ventry = "";
            var vshow = false;
            switch (s.cp_redirect) {
                case "edit":
                    ventry = 'E'
                    vshow = true
                    break;
            }

            if (vshow) {
                window.open(s.cp_frm + '?entry=' + ventry + '&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters +
                    '&iswithdetail=' + s.cp_iswithdetail + '&docnumber=' + s.cp_docnumber, '_blank');
                delete (s.cp_redirect);
                RefreshNotif.Show();
            }

            if (s.cp_redirect == "error") {
                errorlabel.SetText(s.cp_errormes);
                errorpop.Show();
                delete (s.cp_errormes);
                delete (s.cp_redirect);
            }
            if (s.cp_redirectcosting != null) {
                console.log('test');
                window.open(s.cp_frm + '?entry=' + s.cp_redirectcosting + '&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters +
                    '&iswithdetail=' + s.cp_iswithdetail + '&docnumber=' + s.cp_docnumber + '&editcosting=true', '_blank');
                delete (s.cp_redirect);
                delete (s.cp_masterfile);
                delete (s.cp_redirectcosting);
                RefreshNotif.Show();
            }

            //GC Added code 2-27-2016
            if (s.cp_issuance != null) {
                IssIssuedTo.SetText(s.cp_issuanceitem);
                IssIssuedName.SetText(s.cp_issuancename);
                IssWindow.Show();
                delete (s.cp_issuance);
            }

            if (s.cp_RView) {
                delete (s.cp_RView);
                window.open('./Retail/frmIMS.aspx?entry=V&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters + '&docnumber=' + s.cp_docnumber, '_blank');

            }

            if (s.cp_open) {
                delete (s.cp_open);
                window.open(s.cp_frm + '?entry=' + 'N' + '&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters +
                    '&docnumber=' + s.cp_docnumber, '_blank');
                gv2.Refresh();
                delete (s.cp_docnumber);
                delete (s.cp_parameters);
                delete (s.cp_transtype);
                delete (s.cp_frm);
            }
            if (s.cp_tropen) {
                delete (s.cp_tropen);
                window.open(s.cp_frm + '?entry=' + 'V' + '&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters +
                    '&docnumber=' + s.cp_docnumber, '_blank');
                gv2.Refresh();
                delete (s.cp_docnumber);
                delete (s.cp_parameters);
                delete (s.cp_transtype);
                delete (s.cp_frm);
            }

            if (s.cp_storagetype != null) {

                StorageType.Show();
                delete (s.cp_storagetype);
            }

            if (s.cp_storagetypedone != null) {

                alertMesage("", "", s.cp_locmessage);
                StorageType.Hide();
                gv2.Refresh();
                delete (s.cp_storagetypedone);
                delete (s.cp_locmessage);
            }
            if (s.cp_SubmitCharges) {
                delete (s.cp_SubmitCharges);
                transclick = "charges";
                conf.Show();
            }
            if (s.cp_RAssign) {
                delete (s.cp_RAssign);
                transclick = "Assign";
                conf.Show();
            }
            if (s.cp_Return) {
                delete (s.cp_Return);
                transclick = "Return";
                conf.Show();
            }
            if (s.cp_AfterBlast) {
                delete (s.cp_AfterBlast);
                transclick = "Blast";
                conf.Show();
            }
            if (s.cp_TruckArrival) {
                delete (s.cp_TruckArrival);
                transclick = "Arrival";
                conf.Show();
            }
            if (s.cp_RRStart) {
                delete (s.cp_RRStart);
                transclick = "RRStart";
                conf.Show();
            }
            if (s.cp_RREnd) {
                delete (s.cp_RREnd);
                transclick = "RREnd";
                conf.Show();
            }
            if (s.cp_departureInbound) {
                delete (s.cp_departureInbound);
                transclick = "departureInbound";
                conf.Show();
            }
            if (s.cp_wrstart) {
                delete (s.cp_wrstart);
                transclick = "WRStart";
                conf.Show();
            }
            if (s.cp_wrend) {
                delete (s.cp_wrend);
                transclick = "WREnd";
                conf.Show();
            }
            if (s.cp_departure) {
                delete (s.cp_departure);
                transclick = "Departure";
                conf.Show();
            }
            if (s.cp_TruckArrivalOutbound) {
                delete (s.cp_TruckArrivalOutbound);
                transclick = "TruckArrivalOutbound";
                conf.Show();
            }

        }

        //Genrev
        var splitexport = "";
        var exportdocnum = "";
        var exporttrans = "";
        function CustomExportFunc(s, e) {
            var eDoc = "";
            var eTrans = "";

            eDoc = exportdocnum;
            eTrans = exporttrans;
            CustomExportSheet.Show();
            CustomExportSheet.SetContentUrl('../ReferenceFile/frmExport.aspx?entry=V&docnumber=' + eDoc + '&transtype=' + eTrans);

        }

        //Genrev 01/30/2016
        function DFUpdateInfo(s, e) {
            e.processOnServer = false;
            DFWindow.Hide();
            cp.PerformCallback("RDFUpdate");
        }

        function JODueYes(s, e) {
            e.processOnServer = false;
            JODueDate.Hide();
            transclick = "approve";
            conf.Show();
        }

        function JODueNo(s, e) {
            e.processOnServer = false;
            JODueDate.Hide();
        }
        //end
        //GC Added code 06/06/2016
        function CounterSlipGen(s, e) {
            e.processOnServer = false;
            CounterRangeWindow.Hide();
            cp.PerformCallback("RExecAutoCounter");
            CtrDateFr.SetText('');
            CtrDateTo.SetText('');
        }

        //Genrev Added s.cp_isprinted 02/12/2016
        function cp2_EndCallback(s, e) {
            if (s.cp_print) {
                delete (s.cp_print);
                if (s.cp_report != null) {
                    window.open("./WebReports/ReportViewer.aspx?val=~" + s.cp_report + '&transtype=' + s.cp_transtype + '&docnumber=' + s.cp_docnumber + '&tag=' + s.cp_isprinted + '&reprinted=' + s.cp_reprint, '_blank');
                    gv2.Refresh();
                    delete (s.cp_docnumber);
                    delete (s.cp_report);
                    delete (s.cp_transtype);
                    delete (s.cp_isprinted);
                    delete (s.cp_reprint);
                    Print.Hide();
                    RefreshNotif.Show();
                }
                else {
                    alertMesage("warning", "Report Unavailable", "No report for this transaction!");
                }
            }

            if (s.cp_exist) {
                delete (s.cp_exist);
                alertMesage("warning", "", "DocNumber already exist!");
                OnDocNumberChanged(cmbDocNumber); e.processOnServer = false;
            }
            else if (s.cp_exist == false) {
                delete (s.cp_exist);
                window.open(s.cp_frm + '?entry=N&transtype=' + s.cp_transtype + '&parameters=' + s.cp_parameters
                    + '&iswithdetail=false' + '&docnumber=' + s.cp_docnumber, '_blank');
                gv2.Refresh();
                if (cmbDocNumber.GetText() != '') {
                    OnDocNumberChanged(cmbDocNumber); e.processOnServer = false;
                }
                DocSettings.Hide();
                RefreshNotif.Show();
            }

            if (s.cp_execmes != null) {
                if (s.cp_execmes == "Successfully imported") {
                    alertMesage("success", "Imported", s.cp_execmes);
                } else {
                    alertMesage("", "", s.cp_execmes);
                }


                delete (s.cp_execmes);
                gv2.Refresh();
                e.processOnServer = false;
            }

            if (s.cp_copy) {
                delete (s.cp_copy);
                alertMesage("Success", "", "Successfully Copied!");
                Copycont.Hide();
                Copy.Hide();
                gv2.Refresh();
                e.processOnServer = false;
            }
        }

        function OnDocNumberChanged(s, e) {
            cp2.PerformCallback(s.GetValue().toString());
        }

        function AddDocNumber(s, e) {
            cp2.PerformCallback("Add");
            e.processOnServer = false;
        }

        function Printing(s, e) {
            cpprint.PerformCallback("Print");
            e.processOnServer = false;
        }

        function HRPrintCallbackCounter(s, e) {
            cpHRprint.PerformCallback("HRPrintCallbackCounter");
            e.processOnServer = false;
            HRPrintPop.Hide();
        }

        function HRPrintCallbackInvoice(s, e) {
            cpHRPrintSI.PerformCallback("HRPrintCallbackInvoice");
            e.processOnServer = false;
            HRPrintSI.Hide();
        }


        function CopyTrans(s, e) {
            if (copyfrom.GetText() == copyto.GetText()) {
                alertMesage("error", "Unable to Copy!", 'Cannot copy the transaction to its own!');
            }
            else {
                Copycont.Show();
            }
        }
        function OnValidation(s, e) { //Validation function for header controls (Set this for each header controls)
            if (glStorerKey.GetText() == "" || glWarehousecode.GetText() == "") {

                isValid = false;
            }
            else {
                isValid = true;
            }
        }

        //GC Added code 2-27-2016
        function IssExecIssue(s, e) {
            cp.PerformCallback("RSubmitIssuanceExec");
        }


        function Close(s, e) {
            isBusy = false;
            //scheduleGridRefresh(s);
            cp2.PerformCallback("Close");
            e.processOnServer = false;
            DocSettings.Hide();
        }

        function Oncheckboxchanged(s, e) {
            var checkState = checkBox.GetChecked();
            if (checkState == true) {
                txtDocNumber.GetInputElement().readOnly = true;
                OnDocNumberChanged(cmbDocNumber);
                e.processOnServer = false;
            }
            else {
                txtDocNumber.GetInputElement().readOnly = false;
                txtDocNumber.SetText(null);
            }
        }
        function onCheckboxChanged(s, e) {
            // Get the key of the row where the checkbox changed
            var key = s.GetRowKey(e.visibleIndex);

            // Get the state of the checkbox (true if checked, false if unchecked)
            var isChecked = s.GetRowValues(e.visibleIndex, "Refresh");

            // Perform any custom logic based on the checkbox state or row key
            if (isChecked) {
                console.log("Checkbox in row with key " + key + " is checked.");
                // Add your logic here when the checkbox is checked
            } else {
                console.log("Checkbox in row with key " + key + " is unchecked.");
                // Add your logic here when the checkbox is unchecked
            }
        }
        function confclick(s, e) {
            if (transclick == "submit") {
                cp.PerformCallback('RSubmission');
                e.processOnServer = false;
            }
            if (transclick == "charges") {
                cp.PerformCallback('RChargesSubmission');
                e.processOnServer = false;
            }
            if (transclick == "Assign") {
                cp.PerformCallback('RAssignProcess');
                e.processOnServer = false;
            }
            if (transclick == "Return") {
                cp.PerformCallback('RReturnValidate');
                e.processOnServer = false;
            }
            if (transclick == "Blast") {
                cp.PerformCallback('RAfterBlastValidate');
                e.processOnServer = false;
            }
            if (transclick == "Arrival") {
                cp.PerformCallback('RTruckArrivalSubmit');
                e.processOnServer = false;
            }
            if (transclick == "RRStart") {
                cp.PerformCallback('RRRStartProcess');
                e.processOnServer = false;
            }
            if (transclick == "RREnd") {
                cp.PerformCallback('RRREndProcess');
                e.processOnServer = false;
            }
            if (transclick == "departureInbound") {
                cp.PerformCallback('RDepartureProcessInbound');
                e.processOnServer = false;
            }
            if (transclick == "WRStart") {
                cp.PerformCallback('RWRStartProcess');
                e.processOnServer = false;
            }
            if (transclick == "WREnd") {
                cp.PerformCallback('RWREndProcess');
                e.processOnServer = false;
            }
            if (transclick == "Departure") {
                cp.PerformCallback('RDepartureProcess');
                e.processOnServer = false;
            }
            if (transclick == "TruckArrivalOutbound") {
                cp.PerformCallback('RTruckArrivalOutboundSubmit');
                e.processOnServer = false;
            }
            // 9/16/16  GC  Added code
            if (transclick == "Unsubmit") {
                cp.PerformCallback('ExecRLUnsubmitted');
                e.processOnServer = false;
            }
            //end

            if (transclick == "approve") {
                cp.PerformCallback('ROnApproved');
                e.processOnServer = false;
            }
            if (transclick == "unapprove") {
                cp.PerformCallback('ROnUnapproved');
                e.processOnServer = false;
            }
            if (transclick == "authorize") {
                cp.PerformCallback('ROnAuthorize');
                e.processOnServer = false;
            }
            if (transclick == "authorizeinit") { // TLAV 03-08-2016
                conf.Hide();
                cp.PerformCallback('RAuthorizeInit');
                e.processOnServer = false;
            }
            if (transclick == "startdis") { // TLAV 04-11-2016
                conf.Hide();
                cp.PerformCallback('ROnStartDIS');
                e.processOnServer = false;
            }
            if (transclick == "partialsubmit") {
                cp.PerformCallback('RPartialSubmission');
                e.processOnServer = false;
            }
            if (transclick == "pcount") {
                cp.PerformCallback('RGeneratingPCount');
                e.processOnServer = false;
            }
            if (transclick == "forceclose") {
                cp.PerformCallback('RForceClosing');
                e.processOnServer = false;
            }
            //genrev 01/23/2016
            if (transclick == "cancellation") {
                cp.PerformCallback('RCancelExec');
                e.processOnServer = false;
            }
            if (transclick == "PRJO") {
                cp.PerformCallback('RGeneratePRJOExec');
                e.processOnServer = false;
            }
            //end
            //GC
            if (transclick == "PCVar") {
                cp.PerformCallback('RGeneratePCVarExec');
                e.processOnServer = false;
            }

            if (transclick == "PCIsFinal") {
                cp.PerformCallback('RGeneratePCIsFinalExec');
                e.processOnServer = false;
            }


            if (transclick == "GenBillingInfo") {
                cp.PerformCallback('RGenerateBillingInfoExec');
                e.processOnServer = false;
            }

            //END

            //9/5/2016 GC Added code
            if (transclick == "GenDepreciation") {
                cp.PerformCallback('RExecGenDepreciation');
                e.processOnServer = false;
            }
            //end

            if (transclick == "unprint") {
                cp.PerformCallback('ROnUnprint');
                e.processOnServer = false;
            }
            if (transclick == "multiprint") {
                conf.Hide();
                cp.PerformCallback('ROnMultiPrint');
                e.processOnServer = false;
            }
            if (transclick == "racctgsub") {
                conf.Hide();
                cp.PerformCallback('RACCTGSubmission');
                e.processOnServer = false;
            }
            if (transclick == "Funcgroup") { //KATE
                cp.PerformCallback('RFuncClose');
                e.processOnServer = false;
            }
            if (transclick == "voidDR") { //GC
                cp.PerformCallback('ExecVoidDR');
                e.processOnServer = false;
            }

            if (transclick == "ExcludeBillingInitiate") {
                cp.PerformCallback('RExcludeBillingInitiate');
                e.processOnServer = false;
            }

        }

        function OnFileUploadComplete(s, e) {//Loads the excel file into the grid
            import_cp.PerformCallback();
        }

        //#region grid_refresh
        var timeout;
        var busy;
        var count = 0;
        var isBusy = false;
        //function grid_ColumnResizing(s, e) {
        //    isBusy = true;
        //}

        //function grid_ColumnResized(s, e) {
        //    isBusy = false;
        //    scheduleGridRefresh(s);
        //}

        //// Start dragging a column header to the grouping box.
        //function grid_ColumnMoving(s, e) {
        //    scheduleGridRefresh(s);
        //}

        // Start typing in the filter edit box for a column.
        //function grid_FilterTextBoxGotFocus() {
        //    isBusy = true;
        //}

        //function grid_FilterTextBoxLostFocus(s, e) {
        //    isBusy = false;
        //    scheduleGridRefresh(s);
        //}

        function OnFocusedRow(s, e) {
            //s.SetFocusedRowIndex(0);
            //e.showBrowserMenu = false; 
            //alert(gv.GetFocusedRowIndex());
        }

        function StorageTypeReloc(s, e) {
            cp.PerformCallback("RStorageReloc");
        }



        function scheduleGridRefresh(grid) {
            //console.log(isBusy);
            window.clearTimeout(timeout);
            timeout = window.setTimeout(gridPerformCallback, 3000);
        }

        function gridPerformCallback() {
            if (!gv2.InCallback() && !isBusy) {
                gv2.PerformCallback("Refresh");
            }
        }

        function gridcp_Init(s, e) {
            //scheduleGridRefresh(s);
        }

        function gridcp_BeginCallback(s, e) {
            window.clearTimeout(timeout);
            //console.log(e.command);
            //if (e.command == "SHOWFILTERCONTROL") {
            //    isBusy = true;
            //}
            //if (e.command == "CLOSEFILTERCONTROL") {
            //    isBusy = false;
            //}
        }


        function rowclick(s, e) {
            isBusy = true;
            //console.log("rowclick");
            window.clearTimeout(timeout);
            timeout = window.setTimeout(setisbusycallback, 5000);
        }

        function setisbusycallback(s, e) {
            isBusy = false;
            //console.log("setisbusycallback");
            // window.clearTimeout(timeout);
            // timeout = window.setTimeout(gridPerformCallback, 500);
            //gridcp.PerformCallback("Refresh");
        }



        //#endregion
        //setInterval(function refresh() { gv2.Refresh(); e.processOnServer = false; }, 2000);

        // 2016-02-19  Tony
        function OnRibbonItemClick(s, e) {
            if (e.item.name == "RImportNewTicket" || e.item.name == "RImportResolvedTicket" || e.item.name == "RImportClosedTicket") {
                switch (e.item.name) {
                    case "RImportNewTicket":
                        lblParam.SetText("N");
                        break;
                    case "RImportResolvedTicket":
                        lblParam.SetText("R");
                        break;
                    case "RImportClosedTicket":
                        lblParam.SetText("C");
                        break;
                }
                ImportTicketPopup.Show();
            }
            // 2016-03-18  Tony
            else if (e.item.name == "RNewISProj") {
                NewISProjPopup.Show();
            }
            else if (e.item.name == "RImportISProj") {
                ISProjVersionPopup.Show();
            }
            else if (e.item.name == "RGenKPI") {
                GenKPIPopup.Show();
            }
            // 2016-03-18  Tony  End
            else {
                cp.PerformCallback(e.item.name);
                e.processOnServer = false;
            }
        }

        function cpTicket_EndCallback(s, e) {
            alertMesage("", "", s.cp_message);
            delete (s.cp_message);
            ImportTicketPopup.Hide();
        }
        function ShowRowsCount() {
            labelInfo.SetText('Selected Rows: ' + gv2.GetSelectedRowCount());
        }

        // 2016-02-19  Tony  End

        // 2016-06-07  Era invcor
        function autocalcinv(s, e) {
            var amount = 0.00;
            var totalamount = 0.00;
            //New Rows
            var indicies = invcorgrid.batchEditHelper.GetDataItemVisibleIndices();
            for (var i = 0; i < indicies.length; i++) {
                if (invcorgrid.batchEditHelper.IsNewItem(indicies[i])) {
                    //console.log("new row " + indicies[i]);
                }
                else { //Existing Rows
                    var key = invcorgrid.GetRowKey(indicies[i]);
                    if (invcorgrid.batchEditHelper.IsDeletedItem(key)) { }
                    //console.log("deleted row " + indicies[i]);
                    else {
                        amount = parseFloat(invcorgrid.batchEditApi.GetCellValue(indicies[i], "Amount"));
                        amount = parseFloat(amount.toFixed(2));
                        totalamount += amount;
                    }
                }
            } ss
            //var a = ;
            invcortxt.SetText('Year: ' + s.cp_invcoryr);
            invcormon.SetText('Month: ' + s.cp_invcormon);
            delete (s.cp_invcoryr);
            delete (s.cp_invcormon);
            invtxt.SetText('Total Amount: ' + totalamount.toFixed(2));

            if (s.cp_result) {
                alertMesage("", "", s.cp_result);
                delete (s.cp_result)
            }
        }
        //StatusBar Charges
        function StatusBar(s, e) {
            CustomerCode = s.cp_chargesCusCode;
            delete (s.cp_chargesCusCode);
            if (s.cp_result) {
                alertMesage("", "", s.cp_result);
                delete (s.cp_result)
            }
        }
        function StatusBarNotify(s, e) {

            CustomerCode = s.cp_NotifyUserName;
            delete (s.cp_notifyme);
            if (s.cp_result) {
                alertMesage("", "", s.cp_result);
                delete (s.cp_result)
            }
        }

        function SubmitTrans(s, e) {
            var key;
            var recordID;
            var chargeflag;
            var chargesqty;
            var info = [];
            var iMO = Chargesgrid.batchEditHelper.GetDataItemVisibleIndices();

            for (var h = 0; h < iMO.length; h++) {
                var key = Chargesgrid.GetRowKey(iMO[h]);
                recordID = Chargesgrid.batchEditApi.GetCellValue(iMO[h], "RecordID");
                chargeflag = Chargesgrid.batchEditApi.GetCellValue(iMO[h], "ChargeFlag");
                chargesqty = parseFloat(Chargesgrid.batchEditApi.GetCellValue(iMO[h], "ChargesQty"));

                const jsonData = {
                    "RecordID": recordID,
                    "ChargeFlag": chargeflag,
                    "ChargesQty": chargesqty,
                };
                info.push(jsonData);
            }

            $.ajax({
                type: 'POST',
                url: "Translist.aspx/ChargesSubmit",
                contentType: "application/json",
                data: '{Charges: ' + JSON.stringify(info) + '}',
                dataType: "json",
                success: function (data) {

                    if (data.d.search("Successfully Charged:") != -1) {
                        console.log('success');
                        ChargesPop.Hide();
                        alertMesage("error", "", data.d);
                    }
                    else if (data.d != '') {
                        console.log('eee');
                        ChargesPop.Hide();
                        alertMesage("error", "", data.d);
                    }

                    else {
                        console.log('wwww');
                        ChargesPop.Hide();
                        alertMesage("success", "", "Saved Successfully")
                    }
                    Chargesgrid.PerformCallback('Show');
                }
            });
        }

        function ReturnSubmit(s, e) {
            var key;
            var DocNumber;
            var LineNumber;
            var ItemReturn;
            var info = [];
            var iMO = Returngrid.batchEditHelper.GetDataItemVisibleIndices();

            for (var h = 0; h < iMO.length; h++) {
                var key = Returngrid.GetRowKey(iMO[h]);
                DocNumber = Returngrid.batchEditApi.GetCellValue(iMO[h], "DocNumber");
                LineNumber = Returngrid.batchEditApi.GetCellValue(iMO[h], "LineNumber");
                ItemReturn = parseFloat(Returngrid.batchEditApi.GetCellValue(iMO[h], "ItemReturn"));

                const jsonData = {
                    "DocNumber": DocNumber,
                    "LineNumber": LineNumber,
                    "ItemReturn": ItemReturn,
                };
                info.push(jsonData);
            }

            $.ajax({
                type: 'POST',
                url: "Translist.aspx/ReturnSubmit",
                contentType: "application/json",
                data: '{Returns: ' + JSON.stringify(info) + '}',
                dataType: "json",
                success: function (data) {

                    if (data.d != '') {
                        alertMesage("error", "", data.d);
                    }

                    else {
                        alertMesage("success", "", "Saved Successfully")
                        ReturnPop.Hide();
                    }
                    //Returngrid.PerformCallback('Show');
                }
            });
        }

        function alertMesage(alertType, alertTitle, alertContent) {
            //console.log('alertType:' + alertType + 'alertTitle:' + alertTitle + 'alertContent:' + alertContent);
            swal({
                title: alertTitle,
                text: alertContent,
                icon: alertType,
                buttons: "",
                closeOnEsc: null,
                closeOnClickOutside: null
            });

            document.getElementsByClassName("swal-button--confirm").item(0).style.display = "none";
            const bodyModal = document.getElementsByClassName("swal-modal")[0];
            const cellInput = document.createElement("textarea");
            cellInput.setAttribute("id", "msgError");
            cellInput.style.opacity = 0;
            cellInput.innerHTML = alertContent;
            bodyModal.appendChild(cellInput);
            cellInput.style.height = "1px";

            const bodyAlert = document.getElementsByClassName("swal-button-container")[0];
            const cellButton = document.createElement("button");
            cellButton.setAttribute("type", "submit");
            cellButton.setAttribute("id", "btnRight")
            cellButton.setAttribute("class", "swal-button swal-button--cancel")
            cellButton.innerText = "Copy to Clipboard";
            cellButton.setAttribute("onclick", "var copyText = document.getElementById('msgError');copyText.select();document.execCommand('copy');");
            bodyAlert.appendChild(cellButton);

            const bodyAlert2 = document.getElementsByClassName("swal-button-container")[0];
            const cellButton2 = document.createElement("button");
            cellButton2.setAttribute("type", "submit");
            cellButton2.setAttribute("id", "btnsub")
            cellButton2.setAttribute("class", "swal-button swal-button--ok")
            cellButton2.innerText = "OK";
            cellButton2.setAttribute("onclick", "gv2.Refresh(); swal.close();");
            bodyAlert2.appendChild(cellButton2);
        }
        function SubmitAfterBlast(s, e) {
            var columnValue = AfterBlastgrid.batchEditHelper.GetDataItemVisibleIndices();

            var values = []

            for (var i = 0; i < columnValue.length; i++) {
                var ItemCodeValue = AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "ItemCode");
                const jsonData = {
                    "DocNumber": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "DocNumber"),
                    "LineNumber": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "LineNumber"),
                    "ItemCode": Array.isArray(ItemCodeValue) ? ItemCodeValue[0] : ItemCodeValue,
                    "ReceivedQty": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "ReceivedQty"),
                    "PalletID": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "PalletID"),
                };
                values.push(jsonData);
            }

            $.ajax({
                type: 'POST',
                url: "Translist.aspx/SubmitAfterBlast",
                contentType: "application/json",
                data: '{_changes: ' + JSON.stringify(values) + '}',
                dataType: "json",
                success: function (data) {

                    if (data.d == "") {
                        AfterBlastPop.Hide();

                        let alertType = "success";
                        alertMesage(alertType, "After Blast Submission", "Successfully Submitted");
                        AfterBlastgrid.PerformCallback('Shown');
                    }
                    else {
                        AfterBlastPop.Hide();
                        let alertType = "error";
                        alertMesage(alertType, "After Blast Submission", data.d);
                    }

                }
            });

        }

        function UpdateComi(s, e) {
            var columnValue = Comigrid.batchEditHelper.GetDataItemVisibleIndices();
            let batchVal = document.getElementById('ComiPop_comi_cp_comiCon_txtBatch_I').value;
            let consigneeVal = document.getElementById('ComiPop_comi_cp_comiCon_txtConsignee_I').value;
            let doc = "";


            var values = []
            for (var i = 0; i < columnValue.length; i++) {
                const jsonData = {
                    "DocNumber": Comigrid.batchEditApi.GetCellValue(columnValue[i], "InbdocNumber"),
                    "LineNumber": Comigrid.batchEditApi.GetCellValue(columnValue[i], "InblineNumber"),
                    "ItemCode": Comigrid.batchEditApi.GetCellValue(columnValue[i], "InbitemCode"),
                    "Brand": Comigrid.batchEditApi.GetCellValue(columnValue[i], "Brand"),
                    "Origin": Comigrid.batchEditApi.GetCellValue(columnValue[i], "Origin"),
                    "MfgName": Comigrid.batchEditApi.GetCellValue(columnValue[i], "MfgName"),
                    "EstablishNo": Comigrid.batchEditApi.GetCellValue(columnValue[i], "EstablishNo"),
                    "VQMLICNo": Comigrid.batchEditApi.GetCellValue(columnValue[i], "VQMLICNo"),
                };
                values.push(jsonData);
                doc = Comigrid.batchEditApi.GetCellValue(columnValue[i], "InbDocNumber");
            }
            const jsonHeader = {
                "Docnum": doc,
                "Batch": batchVal,
                "Consignee": consigneeVal,
            }
            var values2 = []
            values2.push(jsonHeader);
            console.log(values);
            console.log(values2);
            $.ajax({
                type: 'POST',
                url: "Translist.aspx/UpdateComi",
                contentType: "application/json",
                data: '{_update: ' + JSON.stringify(values) + ',_updatehead: ' + JSON.stringify(values2) + '}',
                dataType: "json",
                success: function (data) {

                    if (data.d.match(/No Changes*/)) {
                        alertMesage("", "", data.d)
                    }
                    else {
                        //alert("Saved Successfully")

                        //swal.fire({
                        //    title: "",
                        //    text: "Saved Successfully",
                        //    icon: "success",
                        //    confirmButtonText: 'OK',
                        //    willClose: function () {
                        //        $('.scroll-box').fadeIn('fast');
                        //        //SLoadCload();
                        //    }
                        //})
                        ComiPop.Hide();

                        alertMesage("success", "COMI", "Saved Successfully");
                    }

                }
            });
        }
        function UpdateReplenish(s, e) {
            var columnValue = replenishGrid.batchEditHelper.GetDataItemVisibleIndices();

            var values = []

            for (var i = 0; i < columnValue.length; i++) {

                const jsonData = {
                    "DocNumber": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "DocNumber"),
                    "ReferenceRecordID": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "ReferenceRecordID"),
                    "LineNumber": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "LineNumber"),
                    "Refresh": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "Refresh"),
                    "WarehouseCode": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "WarehouseCode"),
                    "CustomerCode": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "CustomerCode"),
                    "Location": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "Location"),
                    "MinimumWeight": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "MinimumWeight"),
                    "CurrentWeight": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "CurrentWeight"),
                    "RemainingWeight": replenishGrid.batchEditApi.GetCellValue(columnValue[i], "RemainingWeight"),
                };
                values.push(jsonData);
            }
            console.log(values);
            $.ajax({
                type: 'POST',
                url: "Translist.aspx/UpdateReplenish",
                contentType: "application/json",
                data: '{_replenish: ' + JSON.stringify(values) + '}',
                dataType: "json",
                success: function (data) {

                    if (data.d.match(/No Changes*/)) {
                        alertMesage("", "", data.d)
                    }
                    else {
                        //alert("Saved Successfully")

                        //swal.fire({
                        //    title: "",
                        //    text: "Saved Successfully",
                        //    icon: "success",
                        //    confirmButtonText: 'OK',
                        //    willClose: function () {
                        //        $('.scroll-box').fadeIn('fast');
                        //        //SLoadCload();
                        //    }
                        //})
                        //replenishGrid.PerformCallback('Shown');
                        ReplenishPop.Hide();
                        alertMesage("success", "Replenished", "Saved Successfully")
                    }

                }
            });
        }
        function UpdateAfterBlast(s, e) {
            //adds the info of afterblast the data will be passed to tranlist.aspx.cs
            var columnValue = AfterBlastgrid.batchEditHelper.GetDataItemVisibleIndices();

            var values = []

            for (var i = 0; i < columnValue.length; i++) {
                var ItemCodeValue = AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "ItemCode");
                const jsonData = {
                    "DocNumber": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "DocNumber"),
                    "LineNumber": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "LineNumber"),
                    "ItemCode": Array.isArray(ItemCodeValue) ? ItemCodeValue[0] : ItemCodeValue,
                    "ReceivedQty": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "ReceivedQty"),
                    "PalletID": AfterBlastgrid.batchEditApi.GetCellValue(columnValue[i], "PalletID"),
                };
                values.push(jsonData);
            }

            $.ajax({
                type: 'POST',
                url: "Translist.aspx/UpdateAfterBlast",
                contentType: "application/json",
                data: '{_changes: ' + JSON.stringify(values) + '}',
                dataType: "json",
                success: function (data) {

                    if (data.d.match(/No Changes*/)) {
                        AfterBlastPop.Hide();
                        alertMesage("", "", data.d)
                    }
                    else {
                        //alert("Saved Successfully")

                        //swal.fire({
                        //    title: "",
                        //    text: "Saved Successfully",
                        //    icon: "success",
                        //    confirmButtonText: 'OK',
                        //    willClose: function () {
                        //        $('.scroll-box').fadeIn('fast');
                        //        //SLoadCload();
                        //    }
                        //})
                        //AfterBlastgrid.PerformCallback('Shown');
                        AfterBlastPop.Hide();
                        alertMesage("success", "After Blast", "Saved Successfully")
                    }

                }
            });

        }

        function MultiAssignUpdate(s, e) {
            MultiAssigngrid.batchEditApi.EndEdit();
            var indicies = MultiAssigngrid.batchEditHelper.GetDataItemVisibleIndices();
            var WarehouseCheck = [];
            var Assign = [];

            for (var i = 0; i < indicies.length; i++) {
                var warehouseCheckerValue = MultiAssigngrid.batchEditApi.GetCellValue(indicies[i], "WarehouseChecker");
                var DocNumber = MultiAssigngrid.batchEditApi.GetCellValue(indicies[i], "DocNumber");
                var CheckerChanged = MultiAssigngrid.batchEditApi.GetCellValue(indicies[i], "CheckerChanged");

                getConcatenatedValues(warehouseCheckerValue)

                WarehouseCheck.push(getConcatenatedValues(warehouseCheckerValue));
            }

            if (hasDuplicates(WarehouseCheck)) {
                MultiAssignPop.Hide();
                alertMesage("error", "", "Duplicate Checker found in WarehouseChecker.");
                return; // Abort further processing if there are duplicates.
            }

            var WarehouseC = WarehouseCheck.toString();

            const jsonData = {
                "DocNumber": DocNumber,
                "WarehouseC": WarehouseC,
                "CheckerChanged": CheckerChanged,
            };

            Assign.push(jsonData);

            console.log(jsonData);

            $.ajax({
                type: 'POST',
                url: "Translist.aspx/UpdateAssign",
                contentType: "application/json",
                data: '{_infos: ' + JSON.stringify(Assign) + '}',
                dataType: "json",
                success: function (data) {
                    if (data.d.match(/No Changes*/)) {
                        MultiAssignPop.Hide();
                        alertMesage("error", "", data.d);
                    }
                    else if (data.d.match("Accepted Cannot be Reassigned")) {
                        MultiAssignPop.Hide();
                        alertMesage("error", "", data.d)
                    }
                    else {
                        MultiAssignPop.Hide();
                        alertMesage("success", "", "Saved Successfully");
                        MultiAssigngrid.PerformCallback('Shown');
                    }
                }
            });
        }

        function getConcatenatedValues(warehouseCheckerValue) {
            if (Array.isArray(warehouseCheckerValue)) {
                return warehouseCheckerValue.join(',');
            } else {
                return warehouseCheckerValue;
            }
        }

        function hasDuplicates(array) {
            var valuesSoFar = Object.create(null);
            for (var i = 0; i < array.length; ++i) {
                var value = array[i];
                if (value in valuesSoFar) {
                    return true;
                }
                valuesSoFar[value] = true;
            }
            return false;
        }

        //var preventEndEditOnLostFocus = false;
        function gridLookup_KeyDown1(s, e) { //Allows tabbing between gridlookup on details
            isSetTextRequired = false;
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode !== ASPxKey.Tab) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (MultiAssigngrid.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }

        }

        function gridLookup_KeyPress1(s, e) { //Prevents grid refresh when a user press enter key for every column
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode == ASPxKey.Enter) {
                MultiAssigngrid.batchEditApi.EndEdit();
            }

            //ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
        }

        function gridLookup_CloseUp1(s, e) {
                MultiAssigngrid.batchEditApi.EndEdit();
        }

        function AssignUpdate(s, e) {
            Assigngrid.batchEditApi.EndEdit();
            var indicies = Assigngrid.batchEditHelper.GetDataItemVisibleIndices();
            var WarehouseCheck = [];
            var Assign = [];

            for (var i = 0; i < indicies.length; i++) {
                var warehouseCheckerValue = Assigngrid.batchEditApi.GetCellValue(indicies[i], "WarehouseChecker");
                var DocNumber = Assigngrid.batchEditApi.GetCellValue(indicies[i], "DocNumber");
                var CheckerChanged = Assigngrid.batchEditApi.GetCellValue(indicies[i], "CheckerChanged");

                getConcatenatedValues(warehouseCheckerValue)

                WarehouseCheck.push(getConcatenatedValues(warehouseCheckerValue));
            }
            if (hasDuplicates(WarehouseCheck)) {
                AssignPop.Hide();
                alertMesage("error", "", "Duplicate Checker found in WarehouseCheck.");
                return; // Abort further processing if there are duplicates.
            }

            var WarehouseC = WarehouseCheck.toString();

            const jsonData = {
                "DocNumber": DocNumber,
                "WarehouseC": WarehouseC,
                "CheckerChanged": CheckerChanged,
            };

            Assign.push(jsonData);

            console.log(jsonData);

            $.ajax({
                type: 'POST',
                url: "Translist.aspx/UpdateAssign",
                contentType: "application/json",
                data: '{_infos: ' + JSON.stringify(Assign) + '}',
                dataType: "json",
                success: function (data) {

                    if (data.d.match(/No Changes*/)) {
                        AssignPop.Hide();
                        alertMesage("error", "", data.d)
                    }
                    else if (data.d.match("Accepted Cannot be Reassigned")) {
                        AssignPop.Hide();
                        alertMesage("error", "", data.d)
                    }
                    else {
                        AssignPop.Hide();
                        alertMesage("success", "", "Saved Successfully")
                        Assigngrid.PerformCallback('Shown');
                    }

                }
            });
        }

        //var preventEndEditOnLostFocus = false;
        function gridLookup_KeyDown(s, e) { //Allows tabbing between gridlookup on details
            isSetTextRequired = false;
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode !== ASPxKey.Tab) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (Assigngrid.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            }

        }

        function gridLookup_KeyPress(s, e) { //Prevents grid refresh when a user press enter key for every column
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode == ASPxKey.Enter) {
                Assigngrid.batchEditApi.EndEdit();
            }

            //ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
        }

        function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
            setTimeout(function () {
                Assigngrid.batchEditApi.EndEdit();

            }, 500);
        }

        function lookup(s, e) {
            if (isSetTextRequired) {//Sets the text during lookup for item code
                s.SetText(s.GetInputElement().value);
                isSetTextRequired = false;
            }
        }


        var index;
        var itemc; //variable required for lookup
        var warehouseC;
        var currentColumn = null;
        var isSetTextRequired = false;
        var linecount = 1;
        var valchange = false;
        var valchange1 = false;
        function OnStartEditing(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            index = e.visibleIndex;
            warehouseC = s.batchEditApi.GetCellValue(e.visibleIndex, "WarehouseChecker");
            //console.log(warehouseC);

            var cellInfo = e.rowValues[e.focusedColumn.index];


            if (e.focusedColumn.fieldName === "WarehouseChecker") {
                glWarehouseChecker.GetInputElement().value = cellInfo.value;
            }
            if (e.focusedColumn.fieldName === "ItemCode") {
                glItemCode.GetInputElement().value = cellInfo.value;
            }


        }

        function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
            var cellInfo = e.rowValues[currentColumn.index];
            //console.log(s);
            if (currentColumn.fieldName === "WarehouseChecker") {
                cellInfo.value = glWarehouseChecker.GetValue();
                cellInfo.text = glWarehouseChecker.GetText().toUpperCase();
            }
            if (currentColumn.fieldName === "ItemCode") {
                //console.log(currentColumn.fieldName);
                cellInfo.value = glItemCode.GetValue();
                cellInfo.text = glItemCode.GetText().toUpperCase();
            }

        }
        function OnConfirmUpload(s, e) {//function upon saving entry
            e.cancel = true;
        }

        function ItemCodeChanged(value) {
            AfterBlastgrid.batchEditApi.SetCellValue(index, "CheckerChanged", '1');
        }
        function UpdateCheckerChanged(values) {
            Assigngrid.batchEditApi.SetCellValue(index, "CheckerChanged", '1');
            console.log(index);
            //MultiAssigngrid.batchEditApi.SetCellValue(index, "CheckerChanged", '1');
            //console.log(index);
        }
        function UpdateCheckerChangedMulti(values) {
            MultiAssigngrid.batchEditApi.SetCellValue(index, "CheckerChanged", '1');
            console.log(index);
            //MultiAssigngrid.batchEditApi.SetCellValue(index, "CheckerChanged", '1');
            //console.log(index);
        }
        //function autocalculate(s, e) {
        //    //console.log(txtNewUnitCost.GetValue());
        //    OnInitTrans();

        //    var TotalQuantity = 0.00;
        //    var qty = 0.00;
        //    setTimeout(function () {
        //        var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
        //        for (var i = 0; i < indicies.length; i++) {
        //            if (gv1.batchEditHelper.IsNewItem(indicies[i])) {

        //                qty = parseFloat(gv1.batchEditApi.GetCellValue(indicies[i], "ReceivedQty"));

        //                TotalQuantity += qty;          //Sum of all Quantity
        //                //console.log(TotalQuantity);
        //            }
        //            else {
        //                var key = gv1.GetRowKey(indicies[i]);
        //                if (gv1.batchEditHelper.IsDeletedItem(key))
        //                    console.log("deleted row " + indicies[i]);
        //                else {
        //                    qty = parseFloat(gv1.batchEditApi.GetCellValue(indicies[i], "ReceivedQty"));
        //                    TotalQuantity += qty;
        //                    //console.log(TotalQuantity);
        //                }
        //            }
        //        }
        //        //txtTotalAmount.SetText(TotalAmount.toFixed(2))
        //        txtField2.SetText(TotalQuantity.toFixed(2));

        //    }, 1000);
        //}

        //function ChangedPalCount(s, e) {
        //    if (s.cp_palcharcount != null) {
        //        PalCharCount = s.cp_palcharcount;
        //        PaltwoCustomer = s.cp_paltwocustomer;
        //        delete (s.cp_palcharcount);
        //        delete (s.cp_paltwocustomer);
        //        gv1.CancelEdit();

        //    }
        //}

        if (e.buttonID == "Delete") {
            if (s.batchEditApi.GetCellValue(e.visibleIndex, "Status") == 'S') {
                e.cancel = true;
            }
            else {
                gv1.DeleteRow(e.visibleIndex);
            }
        }

        function ViewRejectData(s, e) {
            MultiAssignPop.Hide();
            RejectPop.Show();
        }

        function ReturnToAssign(s, e) {
            MultiAssignPop.Show();
            RejectPop.Hide();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="1025px" ClientInstanceName="cp" OnCallback="ASPxCallbackPanel1_Callback">
            <ClientSideEvents EndCallback="cp_EndCallback"></ClientSideEvents>
            <%--<!--#region Region Ribbon --> --%>
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <dx:ASPxPopupControl ID="Confirm" runat="server" Width="250px" Height="100px" HeaderText="Warning!"
                        CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="conf"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Are you sure that you want to proceed with this action?" />
                                <table>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="Ok2" runat="server" Text="Ok" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                                <ClientSideEvents Click="confclick" />
                                            </dx:ASPxButton>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton5" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                                    <ClientSideEvents Click="function (s,e){ conf.Hide(); }" />
                                                </dx:ASPxButton>
                                    </tr>
                                </table>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>

                    <dx:ASPxPopupControl Text="" ID="SubmitAlert" runat="server" Width="350px" Height="400px" HeaderText="Submission Alert!"
                        CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="SubmitAlert" EncodeHtml="true"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ContentStyle-HorizontalAlign="Center" ScrollBars="Vertical">
                        <ClientSideEvents CloseButtonClick="function(s,e) { 
                                   //gv2.UnselectAllRowsOnPage(); 
                                   gv2.Refresh();  e.processOnServer = false;}" />
                        <ContentStyle HorizontalAlign="Center"></ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxRibbon ID="GWLRibbon" runat="server" Theme="Office2010Blue" EnableTheming="True" ShowFileTab="False" ClientInstanceName="gwlribbon">
                        <%--2016-02-19  Tony--%>
                        <%--                                    <ClientSideEvents CommandExecuted="function(s, e) 
                                                        {
                                                        
                                                        cp.PerformCallback(e.item.name);
                                                        e.processOnServer = false;
                                                        }"/>--%>
                        <ClientSideEvents CommandExecuted="OnRibbonItemClick" />
                        <%--2016-02-19  Tony  End--%>
                        <Tabs>
                            <dx:RibbonTab Name="File" Text="File" Visible="false">
                                <Groups>
                                    <dx:RibbonGroup Name="New" Text="New" Visible="false">
                                        <Items>
                                             
                                            <dx:RibbonButtonItem Name="RNew" Size="Large" Text="New" Visible="false">
                                                <LargeImage IconID="actions_addfile_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_addfile_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="ROpen" Size="Large" Text="Open" Visible="false">
                                                <LargeImage IconID="actions_loadfrom_32x32office2013">
                                                </LargeImage>
                                                <SmallImage IconID="actions_loadfrom_32x32office2013">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RTranOpen" Size="Large" Text="Open" Visible="false">
                                                <LargeImage IconID="actions_loadfrom_32x32office2013">
                                                </LargeImage>
                                                <SmallImage IconID="actions_loadfrom_32x32office2013">
                                                </SmallImage>

                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RPutaway" Size="Large" Text="PutAway" Visible="false">
                                                <LargeImage IconID="actions_newsales_32x32devav">
                                                </LargeImage>
                                                <SmallImage IconID="actions_newsales_16x16devav">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RView" Size="Large" Text="View" Visible="false">
                                                <LargeImage IconID="actions_show_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_show_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RUpdateStorage" Size="Large" Text="Update StorageType" Visible="false">
                                                <LargeImage IconID="edit_edit_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="edit_edit_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                        </Items>
                                    </dx:RibbonGroup>
                                    <dx:RibbonGroup Name="Activate" Text="Inactive / Deactivate" Visible="False">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RActivate" Size="Large" Text="Activate" ToolTip="Activate" Visible="False">
                                                <LargeImage IconID="actions_apply_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_apply_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RDeactivate" Size="Large" Text="Deactivate" ToolTip="Deactivate" Visible="False">
                                                <LargeImage IconID="actions_cancel_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_cancel_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                        </Items>
                                    </dx:RibbonGroup>
                                </Groups>
                            </dx:RibbonTab>
                            <dx:RibbonTab Name="Actions" Text="Actions" Visible="false">
                                <Groups>
                                    <dx:RibbonGroup Name="Submit" Text="Submit" Visible="false">
                                        <Items>
                                            
                                            <dx:RibbonButtonItem Name="RForSubmission" Size="Large" Text="For Submission" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <LargeImage IconID="tasks_edittask_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_edittask_32x32">
                                                </SmallImage>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </dx:RibbonButtonItem>


                                            <dx:RibbonButtonItem Name="RSubmit" Size="Large" Text="Submit" Visible="false">
                                                <LargeImage IconID="tasks_task_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RPartialSubmit" Size="Large" Text="Partial Submit" Visible="false">
                                                <LargeImage IconID="tasks_task_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RCancel" Size="Large" Text="Tag as Cancel" Visible="false">
                                                <LargeImage IconID="programming_forcetesting_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="miscellaneous_design_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RLUnsubmitted" Size="Large" Text="Unsubmit" Visible="false">
                                                <LargeImage IconID="actions_deletelist2_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_deletelist2_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RForceClose" Size="Large" Text="Manual Close" Visible="False">
                                                <LargeImage IconID="analysis_nonelines_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="analysis_nonelines_32x32">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RCopyTrans" Size="Large" Text="Copy Transaction" Visible="false">
                                                <LargeImage IconID="actions_down_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_fill_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RPost" Size="Large" Text="Posting" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <LargeImage IconID="edit_paste_32x32office2013">
                                                </LargeImage>
                                                <SmallImage IconID="edit_paste_32x32office2013">
                                                </SmallImage>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RComi" Text="COMI" Size="Large" Visible="false">
                                                <LargeImage IconID="toolboxitems_report2_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="toolboxitems_report2_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RReplenish" Text="Replenish" Size="Large" Visible="false">
                                                <LargeImage IconID="businessobjects_boorderitem_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="businessobjects_boorderitem_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RAssign" Size="Large" Text="Assign" Visible="false">
                                                <LargeImage IconID="businessobjects_bodepartment_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="businessobjects_bodepartment_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                        </Items>
                                    </dx:RibbonGroup>

                                    <dx:RibbonGroup Name="Generate" Text="Generate" Visible="false">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RGeneratePCount" Size="Large" Text="Generate" Visible="false">
                                                <LargeImage IconID="programming_technology_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="programming_technology_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RGeneratePicklistDetail" Size="Large" Text="Generate Detail" Visible="false">
                                                <LargeImage IconID="actions_converttorange_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RGenerateCountsheet" Size="Large" Text="Generate" Visible="false">
                                                <LargeImage IconID="actions_converttorange_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RGenerateCountsheetSubsi" Size="Large" Text="Generate CountsheetSubsi" Visible="false">
                                                <LargeImage IconID="actions_converttorange_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RGenerateLocation" Size="Large" Text="Generate Location" Visible="false">
                                                <LargeImage IconID="actions_switchrowcolumn_32x32">
                                                </LargeImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RGenerateBillingInfo" Size="Large" Text="Generate BillingInfo" Visible="false">
                                                <LargeImage IconID="actions_converttorange_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RCopy" Size="Large" Text="Copy Transaction" Visible="False">
                                                <LargeImage IconID="edit_copy_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="edit_copy_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RRoomCascade" Size="Large" Text="Room Cascade" Visible="False">
                                                <LargeImage IconID="maps_geopointmap_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="maps_geopointmap_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RConnstring" Size="Large" Text="Check CString" Visible="False">
                                                <LargeImage IconID="miscellaneous_publish_32x32office2013">
                                                </LargeImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RGeneratePickData" Size="Large" Text="Get Picklist Data" Visible="false">
                                                <LargeImage IconID="actions_converttorange_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RCharges" Text="Charges" Size="Large" Visible="false">
                                                <LargeImage IconID="tasks_edittask_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RSubmitCharges" Text="Submit Charges" Size="Large" Visible="false">
                                                <LargeImage IconID="tasks_task_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RAfterBlast" Text="After Blast" Size="Large" Visible="false">
                                                <LargeImage IconID="grid_wordwrap_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="grid_wordwrap_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RReturn" Text="Return" Size="Large" Visible="false">
                                                <LargeImage IconID="arrows_uturnright_32x32devav">
                                                </LargeImage>
                                                <SmallImage IconID="arrows_uturnright_32x32devav">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <%-- 2016-07-12  Terence  End --%>
                                        </Items>
                                    </dx:RibbonGroup>
                                    <dx:RibbonGroup Name="NonStorage" Text="NonStorage" Visible="false">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RNonStorage" Size="Large" Text="NonStorage Service" Visible="false">
                                                <LargeImage IconID="reports_addheader_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="reports_addheader_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                        </Items>
                                    </dx:RibbonGroup>

                                    <dx:RibbonGroup Name="TruckTransaction" Text="Truck Transaction" Visible="false">
                                        <Items>
                                            
                                            <dx:RibbonButtonItem Name="RTruckArrival" Size="Large" Text="Truck Arrival" Visible="false">
                                                <LargeImage IconID="maps_transit_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="maps_transit_32x32">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RRRStart" Size="Large" Text="Start RR Process" Visible="false">
                                                <LargeImage IconID="sales_salesperiodlifetime_32x32devav">
                                                </LargeImage>
                                                <SmallImage IconID="sales_salesperiodlifetime_16x16devav">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RRREnd" Size="Large" Text="End RR Process" Visible="false">
                                                <LargeImage IconID="sales_salesanalysis_32x32devav">
                                                </LargeImage>
                                                <SmallImage IconID="sales_salesanalysis_16x16devav">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RDepartureInbound" Size="Large" Text="Truck Departure" Visible="false">
                                                <LargeImage IconID="maps_transit_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="maps_transit_32x32">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RTruckArrivalOutbound" Size="Large" Text="Truck Arrival" Visible="false">
                                                <LargeImage IconID="maps_transit_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="maps_transit_32x32">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RWRStart" Size="Large" Text="Start WR Process" Visible="false">
                                                <LargeImage IconID="sales_salesperiodlifetime_32x32devav">
                                                </LargeImage>
                                                <SmallImage IconID="sales_salesperiodlifetime_16x16devav">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RWREnd" Size="Large" Text="End WR Process" Visible="false">
                                                <LargeImage IconID="sales_salesanalysis_32x32devav">
                                                </LargeImage>
                                                <SmallImage IconID="sales_salesanalysis_16x16devav">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RDeparture" Size="Large" Text="Departure" Visible="false">
                                                <LargeImage IconID="maps_transit_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="maps_transit_32x32">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                        </Items>
                                    </dx:RibbonGroup>

                                    <dx:RibbonGroup Name="Approved" Text="Approved" Visible="False">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RApproved" Text="Approved" Visible="False" Size="Large">
                                                <LargeImage IconID="tasks_task_32x32office2013">
                                                </LargeImage>
                                                <SmallImage IconID="tasks_task_32x32office2013">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RUnapproved" Size="Large" Text="Unapprove" Visible="false">
                                                <LargeImage IconID="actions_deletelist2_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_deletelist2_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                        </Items>
                                    </dx:RibbonGroup>
                                    <dx:RibbonGroup Name="Authorize" Text="Authorize" Visible="False">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RAuthorize" Text="Authorize" Visible="False" Size="Large">
                                                <LargeImage IconID="businessobjects_bouser_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="businessobjects_bouser_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                        </Items>
                                    </dx:RibbonGroup>


                                    <%--Tony 2016-02-18--%>

                                    <%--Tony 2016-02-18  End--%>
                                    <dx:RibbonGroup Name="FuncGroup" Text="FuncGroup" Visible="False">
                                        <Items>


                                            <dx:RibbonButtonItem Name="RFuncGroupClose" Size="Large" Visible="False" Text="Close">
                                                <LargeImage IconID="reports_deletegroupheader_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="reports_deletegroupheader_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RMonthEndClosing" Size="Large" Text="Month End Closing" Visible="false">
                                                <LargeImage IconID="analysis_nonelines_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="analysis_nonelines_32x32">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                        </Items>


                                    </dx:RibbonGroup>

                                    <dx:RibbonGroup Name="Tagging" Text="Tagging" Visible="False">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RGeneratePCIsFinal" Text="Tag as Final" Visible="False" Size="Large">
                                                <LargeImage IconID="actions_apply_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_apply_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RRevertPCIsFinal" Text="Untag as Final" Visible="False" Size="Large">
                                                <LargeImage IconID="actions_cancel_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_cancel_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RIncludeBilling" Text="Include in Billing" Visible="False" Size="Large">
                                                <LargeImage IconID="actions_apply_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_apply_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RExcludeBilling" Text="Exclude in Billing" Visible="False" Size="Large">
                                                <LargeImage IconID="actions_cancel_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_cancel_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                        </Items>
                                    </dx:RibbonGroup>
                                            <dx:RibbonGroup Name="Document" Text="Document" Visible="false">
                                              <Items>
                                                    <%-- <dx:RibbonButtonItem Name="RDocForUpld" Size="Large" Text="For Upload" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="zoom_zoom_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="zoom_zoom_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>--%>
                                                   <dx:RibbonButtonItem Name="RAppnDocs" Size="Large" Text="Append" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="actions_additem_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="actions_additem_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>
                                                        <dx:RibbonButtonItem Name="RUpldDocs" Size="Large" Text="Upload" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="actions_up2_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="actions_up2_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>
                                                        <dx:RibbonButtonItem Name="RDwnldDocs" Size="Large" Text="Download" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="actions_down_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="actions_down_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>
                                                  <%--<dx:RibbonButtonItem Name="RDocTrnsmtl" Size="Large" Text="Received" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="content_checkbox_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="content_checkbox_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>
                                                        <dx:RibbonButtonItem Name="RDocRmk" Size="Large" Text="Missing Document" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="dashboards_parameters2_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="dashboards_parameters2_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>
                                                        <dx:RibbonButtonItem Name="RDocComplete" Size="Large" Text="Complete" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="content_checkbox_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="content_checkbox_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>
                                                        <dx:RibbonButtonItem Name="RDocError" Size="Large" Text="Incorrect Document" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="actions_deletelist2_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="actions_deletelist2_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>
                                                        <dx:RibbonButtonItem Name="RDocVerify" Size="Large" Text="Verified" Visible="False" ItemStyle-HorizontalAlign="Center">
                                                            <LargeImage IconID="content_checkbox_32x32">
                                                            </LargeImage>
                                                            <SmallImage IconID="content_checkbox_16x16">
                                                            </SmallImage>
                                                        </dx:RibbonButtonItem>--%>
                                                    </Items>
                                    </dx:RibbonGroup>
                                </Groups>
                            </dx:RibbonTab>
                            <dx:RibbonTab Name="Print" Text="Print" Visible="false">
                                <Groups>
                                    <dx:RibbonGroup Name="Print" Text="Print " Visible="false">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RPrint" Size="Large" Text="Print" Visible="false">
                                                <LargeImage IconID="print_print_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="print_print_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RMultiPrint" Size="Large" Text="Multiple Print" Visible="false">
                                                <LargeImage IconID="actions_printquick_32x32devav">
                                                </LargeImage>
                                                <SmallImage IconID="actions_printquick_16x16devav">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RUnprint" Size="Large" Text="Unprint" Visible="false">
                                                <LargeImage IconID="print_printarea_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="print_printarea_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RHRPrint" Size="Large" Text="Multi-Print" Visible="False">
                                                <LargeImage IconID="print_print_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="print_print_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RHRPrintSI" Size="Large" Text="Multi-Print" Visible="False">
                                                <LargeImage IconID="print_print_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="print_print_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                        </Items>
                                    </dx:RibbonGroup>
                                </Groups>
                            </dx:RibbonTab>
                            <dx:RibbonTab Name="Import" Text="Import/Export" Visible="false">
                                <Groups>
                                    <dx:RibbonGroup Name="Import" Text="Import/Export" Visible="false">
                                        <Items>
                                            <dx:RibbonButtonItem Name="RImportCSheet" Size="Large" Text="Import CSheet" Visible="false">
                                                <LargeImage IconID="actions_left_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_left_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RImport" Size="Large" Text="Import" Visible="false">
                                                <LargeImage IconID="actions_left_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_left_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RExportCSheet" Size="Large" Text="Export CSheet" Visible="false">
                                                <LargeImage IconID="actions_right_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_right_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RExport" Size="Large" Text="Export Translist" Visible="false">
                                                <LargeImage IconID="export_exporttoxlsx_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="export_exporttoxlsx_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RCustomExport" Size="Large" Text="Export" Visible="False">
                                                <LargeImage IconID="actions_right_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="actions_right_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                            <dx:RibbonButtonItem Name="RExtract" Size="Large" Text="Extract Data" Visible="false">
                                                <LargeImage IconID="export_exporttoxlsx_32x32">
                                                </LargeImage>
                                                <SmallImage IconID="export_exporttoxlsx_16x16">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>
                                            <dx:RibbonButtonItem Name="RExtractTemplate" Size="Large" Text="Export Template" Visible="False">
                                                <LargeImage IconID="support_template_32x32office2013">
                                                </LargeImage>
                                                <SmallImage IconID="support_template_16x16office2013">
                                                </SmallImage>
                                            </dx:RibbonButtonItem>

                                        </Items>
                                    </dx:RibbonGroup>


                                    <%--Tony 2016-03-08  End--%>
                                </Groups>
                            </dx:RibbonTab>
                        </Tabs>
                    </dx:ASPxRibbon>
                </dx:PanelContent>
            </PanelCollection>
            <%--<!--#endregion --> --%>
        </dx:ASPxCallbackPanel>
        <dx:ASPxLabel Font-Size="Large" ID="title" runat="server"></dx:ASPxLabel>
        <br />
        <dx:ASPxButton ID="Calendar" runat="server" AutoPostBack="False" HorizontalAlign="Left">
            <ClientSideEvents Click="function(s, e) {
                                daterangewindow.Show();
                                }" />
            <Image Url="~/icons/Calendar.png"></Image>
        </dx:ASPxButton>
        <%--2016-02-16  Tony--%>


        <%--2016-02-16  Tony  End--%>
        <dx:ASPxButton ID="Refresh" runat="server" AutoPostBack="False" HorizontalAlign="Left">
            <ClientSideEvents Click="function(s, e) {
                                  gv2.Refresh();  e.processOnServer = false;

                                 }" />
            <Image Url="~/icons/Refresh.png"></Image>
        </dx:ASPxButton>
        <dx:ASPxLabel ID="labelInfo" runat="server" Text="" ClientInstanceName="labelInfo">
            <ClientSideEvents Init="function(s, e) { ShowRowsCount(); }" />
        </dx:ASPxLabel>

        <dx:ASPxLabel Font-Size="X-Small" ID="caltext" runat="server" EncodeHtml="true" ClientInstanceName="caltext"></dx:ASPxLabel>
        <dx:ASPxComboBox ID="selectAllMode" ClientInstanceName="selectAllMode" Caption="Select all checkbox mode:" runat="server">
            <ClientSideEvents SelectedIndexChanged="function(s,e){gridcp.PerformCallback(s.GetText());}" />
            <RootStyle CssClass="OptionsBottomMargin"></RootStyle>
        </dx:ASPxComboBox>

        <dx:ASPxButton ID="btnXlsxExport" runat="server" ClientVisible="false" Text="Export to Excel" UseSubmitBehavior="False"
            OnClick="btnXlsxExport_Click" ToolTip="Select rows then click to export into an excel file." HorizontalAlign="Left" />
        <dx:ASPxButton ID="btnXlsxExport1" runat="server" ClientVisible="false" AutoPostBack="false" Text="Export to Excel" UseSubmitBehavior="False"
            ToolTip="Select rows then click to export into an excel file." HorizontalAlign="Left">

            <ClientSideEvents Click="function (s,e) { gridcp.PerformCallback()} " />
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnForSubmit" runat="server" ClientVisible="false" AutoPostBack="false" Text="Export to Excel" UseSubmitBehavior="False"
            ToolTip="Select rows then click to export into an excel file." HorizontalAlign="Left">

            <ClientSideEvents Click="function (s,e) { gridcp.PerformCallback()} " />
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnXlsxExport2" runat="server" ClientVisible="false" AutoPostBack="false" Text="Extract Data" UseSubmitBehavior="False"
            HorizontalAlign="Left" OnClick="btnXlsxExport1_Click">
        </dx:ASPxButton>
        <%--<!--#region Translist Gridview --> --%>
        <dx:ASPxCallbackPanel ID="gridcp" runat="server" Width="100%" Height="100%" ClientInstanceName="gridcp"
            OnCallback="gridcp_Callback">
            <%--<//RA 2016-05-14 > --%>
            <ClientSideEvents EndCallback="gridcpendcallback" />
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <dx:ASPxGridView ID="Translistgrid" runat="server" Width="100%" ClientInstanceName="gv2" DataSourceID="POmain" EnableTheming="True" KeyFieldName="DocNumber"
                        OnContextMenuItemClick="ASPxGridView1_ContextMenuItemClick" OnDataBound="ASPxGridView1_DataBound" OnFillContextMenuItems="ASPxGridView1_FillContextMenuItems" Theme="Office2010Blue">
                        <ClientSideEvents Init="OnInitTrans" ContextMenu="OnContextMenu" ContextMenuItemClick="function(s,e) { OnContextMenuItemClick(s, e); }" EndCallback="gridView_EndCallback" SelectionChanged="function(s, e) { ShowRowsCount(); }" />
                        <SettingsBehavior FilterRowMode="OnClick" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="NextColumn" />
                        <SettingsPager PageSize="20">
                        </SettingsPager>
                        <Settings HorizontalScrollBarMode="Visible" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="true" ShowFilterRow="True" VerticalScrollableHeight="0" VerticalScrollBarMode="Auto" />
                        <SettingsContextMenu Enabled="True">
                            <RowMenuItemVisibility DeleteRow="False" EditRow="False" NewRow="False" />
                        </SettingsContextMenu>
                        <Paddings Padding="0px" />
                        <Styles>
                            <AlternatingRow Enabled="True">
                            </AlternatingRow>
                            <Cell Font-Size="8pt">
                                <Paddings Padding="0px" />
                            </Cell>
                        </Styles>
                        <Border BorderWidth="0px" />
                    </dx:ASPxGridView>
                    <%--<//RA 2016-05-14 > --%>
                    <dx:ASPxGridView ID="ExtractedData" runat="server" ClientInstanceName="gvExtract" align="center" Visible="false">
                    </dx:ASPxGridView>

                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <%--<!--#endregion --> --%>

        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="Translistgrid" ExportedRowType="Selected" />
        <%--<//RA 2016-05-14 > --%>
        <dx:ASPxGridViewExporter ID="gridExtract" runat="server" GridViewID="ExtractedData" ExportedRowType="All" />

        <dx:ASPxPopupControl ID="DocSettings" runat="server" Width="290px" Height="100px" HeaderText="DocNumber"
            CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="DocSettings"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="cp2" runat="server" Width="200px" ClientInstanceName="cp2"
                        OnCallback="cp2_Callback" ClientSideEvents-EndCallback="cp2_EndCallback">
                        <PanelCollection>
                            <dx:PanelContent>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxComboBox ID="cmbDocNumber" runat="server" DataSourceID="Docnumsettings"
                                                TextField="Prefix" ValueField="Prefix" Width="50px"
                                                ClientInstanceName="cmbDocNumber">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDocNumberChanged(s); }" />
                                            </dx:ASPxComboBox>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="  " />
                                                <td>
                                                    <dx:ASPxTextBox ID="txtDocNumber" runat="server" Width="90px" ClientInstanceName="txtDocNumber" />
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="  " />
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton3" runat="server">
                                                                <ClientSideEvents Click="function(s, e) { OnDocNumberChanged(cmbDocNumber); e.processOnServer = false; }" />
                                                                <Image IconID="actions_refresh2_16x16"></Image>
                                                            </dx:ASPxButton>
                                    </tr>
                                    <tr>
                                        <td />
                                        <td colspan="3">
                                            <dx:ASPxCheckBox ID="cbReadOnly" runat="server" Checked="false" ClientInstanceName="checkBox" Text="ReadOnly">
                                                <ClientSideEvents ValueChanged="Oncheckboxchanged" />
                                            </dx:ASPxCheckBox>
                                    </tr>
                                    <tr>
                                        <td colspan="4" />
                                        <td>
                                            <dx:ASPxButton ID="Add" runat="server" Text="Add">
                                                <ClientSideEvents Click="AddDocNumber" />
                                            </dx:ASPxButton>
                                            <td>
                                                <dx:ASPxButton ID="Cancel" runat="server" Text="Cancel">
                                                    <ClientSideEvents Click="Close" />
                                                </dx:ASPxButton>

                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>



        <dx:ASPxPopupControl ID="DateRange" runat="server" ClientInstanceName="daterangewindow"
            CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            HeaderText="Date Range" AllowDragging="True" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="Panel1" runat="server">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td />
                                        <dx:ASPxLabel runat="server" Text="From: "></dx:ASPxLabel>
                                        <td />
                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="170px" ClientInstanceName="datefrom">
                                        </dx:ASPxDateEdit>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td />
                                        <dx:ASPxLabel runat="server" Text="To: "></dx:ASPxLabel>
                                        <td />
                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="170px" ClientInstanceName="dateto">
                                        </dx:ASPxDateEdit>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td />
                                        <td />
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Requery" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
                                                                gv2.Refresh(); e.processOnServer = false;
                                                                daterangewindow.Hide();
                                                                caltext.SetText('DateRange: ' + datefrom.GetText() + ' - ' + dateto.GetText());
                                                                }" />
                                        </dx:ASPxButton>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="Authorization" runat="server" ClientInstanceName="authorizationwindow"
            CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            HeaderText="Authorize Transaction" AllowDragging="True" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="PanelTer" runat="server">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td />
                                        <dx:ASPxLabel runat="server" Text="Input Password: "></dx:ASPxLabel>
                                    </tr>
                                    <tr>
                                        <td />
                                        <dx:ASPxTextBox ID="PW" runat="server" Width="170px" ClientSideEvents-KeyPress="function(s, e) {
                                         var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
                                        if (keyCode == ASPx.Key.Enter) {
                                        cp.PerformCallback('RAuthorizeCheck');
                                        gv2.Refresh(); e.processOnServer = false;
                                        authorizationwindow.Hide();
                                        }
                                        }"
                                            Password="true" ClientInstanceName="authpw" ClientEnabled="true">
                                        </dx:ASPxTextBox>
                                        <td />
                                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="OK" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
                                        cp.PerformCallback('RAuthorizeCheck');
                                        gv2.Refresh(); e.processOnServer = false;
                                        authorizationwindow.Hide();
                                        }" />
                                        </dx:ASPxButton>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <%--2016-02-16  Tony--%>



        <dx:ASPxPopupControl ID="StorageType" runat="server" ClientInstanceName="StorageType"
            CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            HeaderText="Update Storage Type" AllowDragging="True" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="ASPxPanel7" runat="server">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td />
                                        <dx:ASPxLabel runat="server" Text="Storage Type : "></dx:ASPxLabel>
                                        <td />
                                        <dx:ASPxGridLookup ID="glStorageType" runat="server" Width="170px"
                                            DataSourceID="sdsStorageType" KeyFieldName="StorageType" TextFormatString="{0}">
                                            <ClearButton Visibility="True"></ClearButton>
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="StorageType" Width="100px" />
                                                <dx:GridViewDataTextColumn FieldName="StorageDescription" Width="250px" />
                                            </Columns>
                                            <GridViewProperties Settings-ShowFilterRow="true">
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />

                                                <Settings ShowFilterRow="True"></Settings>

                                            </GridViewProperties>
                                        </dx:ASPxGridLookup>

                                    </tr>
                                    <tr>

                                        <td>&nbsp; </td>
                                        <tr>
                                            <td />
                                            <dx:ASPxLabel runat="server" Text=" "></dx:ASPxLabel>
                                            <td />

                                            <dx:ASPxButton ID="ASPxButton9" runat="server" Text="Update" AutoPostBack="False">
                                                <ClientSideEvents Click="StorageTypeReloc" />
                                            </dx:ASPxButton>
                                        </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


        <%--2016-02-16  Tony  End--%>
        <dx:ASPxPopupControl ID="Error" Modal="true" ShowCloseButton="true" ContentStyle-HorizontalAlign="Center" runat="server" ClientInstanceName="errorpop" CloseAction="CloseButton" Theme="Aqua" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseOnEscape="True" HeaderText="Error Message">
            <ContentStyle HorizontalAlign="Center"></ContentStyle>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="Red" Text="" ClientInstanceName="errorlabel"></dx:ASPxLabel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxPopupControl ID="Print" runat="server" Width="200px" Height="100px" HeaderText="Printout"
            CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="Print"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ContentStyle-HorizontalAlign="Center">
            <ContentStyle HorizontalAlign="Center"></ContentStyle>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="print_callback" runat="server" Width="200px" ClientInstanceName="cpprint"
                        OnCallback="print_Callback" ClientSideEvents-EndCallback="cp2_EndCallback">
                        <PanelCollection>
                            <dx:PanelContent>
                                <dx:ASPxComboBox ID="cmbprintsel" runat="server" DataSourceID="printsel"
                                    TextField="Text" ValueField="Text" Width="200px"
                                    ClientInstanceName="cmbPrinter">
                                    <%--<ClientSideEvents TextChanged="function(s,e){cpprint.PerformCallback(cmbPrinter.GetText()); e.processOnServer = false;}"/>--%>
                                </dx:ASPxComboBox>
                                <br />
                                <dx:ASPxButton ID="Printbtn" runat="server" Text="Print">
                                    <ClientSideEvents Click="Printing" />
                                </dx:ASPxButton>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


        <dx:ASPxPopupControl ID="CustomExportSheet" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="CustomExportSheet" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" HeaderText="" Height="400px" ShowHeader="true" Width="950px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="Copy" runat="server" Width="200px" Height="100px" HeaderText="Copy Transaction"
            CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="Copy"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ContentStyle-HorizontalAlign="Center">
            <ContentStyle HorizontalAlign="Center"></ContentStyle>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="copy_callback" runat="server" Width="200px" ClientInstanceName="cpcopy"
                        OnCallback="copy_Callback">
                        <ClientSideEvents EndCallback="cp2_EndCallback"></ClientSideEvents>
                        <PanelCollection>
                            <dx:PanelContent>
                                <dx:ASPxComboBox ID="copyfrom" runat="server" DataSourceID="Copysel"
                                    TextField="DocNumber" ValueField="DocNumber" Width="200px" OnLoad="Copy_Load"
                                    ClientInstanceName="copyfrom">
                                </dx:ASPxComboBox>
                                <br />
                                <dx:ASPxComboBox ID="copyto" runat="server" DataSourceID="Copysel"
                                    TextField="DocNumber" ValueField="DocNumber" Width="200px" OnLoad="Copy_Load"
                                    ClientInstanceName="copyto">
                                </dx:ASPxComboBox>
                                <br />
                                <dx:ASPxButton ID="Copybut" runat="server" Text="Copy" AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="CopyTrans" />
                                </dx:ASPxButton>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxPopupControl ID="CopyControl" runat="server" Width="250px" Height="100px" HeaderText="Warning!"
            CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="Copycont"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Copying of transaction into another document will replace its current data. Are you sure you want to copy the transaction?" />
                    <table>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxButton ID="Ok" runat="server" Text="Ok" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                    <ClientSideEvents Click="function (s, e){ cpcopy.PerformCallback('Copy'); e.processOnServer = false;}" />
                                </dx:ASPxButton>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                        <ClientSideEvents Click="function (s,e){ Copycont.Hide(); }" />
                                    </dx:ASPxButton>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxPopupControl ID="RefreshNotif" runat="server" Width="250px" Height="100px" HeaderText="Notice"
            Modal="True" ClientInstanceName="RefreshNotif" ShowCloseButton="false" CloseAction="None"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ContentStyle-HorizontalAlign="Center">
            <ContentStyle HorizontalAlign="Center"></ContentStyle>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Kindly click the refresh button to reload the translist."></dx:ASPxLabel>
                    <dx:ASPxButton ID="RefreshBtn2" runat="server" AutoPostBack="False" Height="20px" Width="20px">
                        <ClientSideEvents Click="function(s, e) {
                                  gv2.Refresh(); e.processOnServer = false;
                                  RefreshNotif.Hide();
                                 }" />
                        <Image Url="~/icons/Refresh.png"></Image>
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxPopupControl ID="ImportCSheet" runat="server" Width="290px" Height="100px" HeaderText="Import Excel"
            CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="ImportCSheet"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="import_cp" runat="server" Width="200px" ClientInstanceName="import_cp"
                        OnCallback="import_cp_Callback" ClientSideEvents-EndCallback="cp2_EndCallback">
                        <ClientSideEvents EndCallback="cp2_EndCallback"></ClientSideEvents>
                        <PanelCollection>
                            <dx:PanelContent>
                                <dx:ASPxUploadControl ID="Upload" runat="server" ShowUploadButton="True" OnFileUploadComplete="Upload_FileUploadComplete" ShowProgressPanel="True" UploadMode="Auto"
                                    Width="200px" Visible="true">
                                    <ValidationSettings AllowedFileExtensions=".xls,.xlsx">
                                    </ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                                    <AdvancedModeSettings EnableDragAndDrop="True">
                                    </AdvancedModeSettings>
                                </dx:ASPxUploadControl>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxPopupControl ID="ExportCSheet" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="CSheet" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="0" HeaderText="" Height="1px" ShowHeader="true" Width="1px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true">
            <ClientSideEvents Shown="function(){ setTimeout(function () {CSheet.Hide();  alertMesage('success','Exported','\t Successfully Exported!\nWait until your browser finished downloading the file.');},200);}" />
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--Charges Popup--%>
        <dx:ASPxPopupControl ID="ChargesPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="ChargesPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="500px" ShowHeader="true" Width="800px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true">
            <ClientSideEvents Shown="function(){Chargesgrid.PerformCallback('Shown');}" />

            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxGridView ID="Chargesgrid" runat="server" Width="100%" ClientInstanceName="Chargesgrid" EnableTheming="True" KeyFieldName="RecordID;LineNumber;ServiceType;Description;ServiceRate;UnitOfMeasure;UnitOfMeasure;BillingType;StorageCode;ContractNumber;BizPartnerCode;WarehouseCode;ProfitCenterCode;Period"
                        Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="Chargesgrid_CustomCallback" SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="false" SettingsEditing-Mode="Batch" OnEndCallback="Chargesgrid_EndCallback">

                        <ClientSideEvents EndCallback="StatusBar" />
                        <SettingsBehavior FilterRowMode="OnClick" AllowFocusedRow="True" AllowSelectByRowClick="False" ColumnResizeMode="NextColumn" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />

                        <Columns>

                            <dx:GridViewDataTextColumn FieldName="DocNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="LineNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="ServiceType" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="RecordID" ShowInCustomizationForm="True" VisibleIndex="0" Width="50px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="Description" Caption="ServiceType" ShowInCustomizationForm="True" VisibleIndex="1" Width="160px" ReadOnly="true" />
                            <dx:GridViewDataCheckColumn FieldName="ChargeFlag" ShowInCustomizationForm="True" VisibleIndex="2" Width="85px" />
                            <dx:GridViewDataTextColumn FieldName="UnitOfMeasure" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="ServiceRate" ShowInCustomizationForm="True" VisibleIndex="4" Width="90px" ReadOnly="true">
                                <PropertiesTextEdit DisplayFormatString="n2" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataSpinEditColumn FieldName="ChargesQty" ShowInCustomizationForm="false" VisibleIndex="5" Width="100px" ReadOnly="false">
                                <PropertiesSpinEdit DisplayFormatString="n2" />
                            </dx:GridViewDataSpinEditColumn>
                            <dx:GridViewDataTextColumn FieldName="BillingType" ShowInCustomizationForm="True" VisibleIndex="5" Width="80px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="StorageCode" ShowInCustomizationForm="True" VisibleIndex="6" Width="140px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="ContractNumber" ShowInCustomizationForm="True" VisibleIndex="7" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="WarehouseCode" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="ProfitCenterCode" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="Period" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                        </Columns>

                    </dx:ASPxGridView>

                    <table>
                        <tr>
                            <td style="padding-left: 700px">

                                <dx:ASPxButton ID="ASPxButton111" runat="server" Text="Post" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="SubmitTrans" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--After Blast Popup--%>
        <dx:ASPxPopupControl ID="AfterBlastPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="AfterBlastPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="500px" ShowHeader="true" Width="800px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true" OnRowUpdating="gvStepProcess_RowUpdating">
            <ClientSideEvents Shown=" function(){AfterBlastgrid.PerformCallback('Shown');} " />

            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxGridView ID="AfterBlastgrid" runat="server" Width="100%" ClientInstanceName="AfterBlastgrid" EnableTheming="True" KeyFieldName="DocNumber;LineNumber"
                        Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="AfterBlast_CustomCallback"
                        SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="true" SettingsEditing-Mode="Batch">

                        <%--  <SettingsBehavior FilterRowMode="OnClick" AllowFocusedRow="True" AllowSelectByRowClick="False" ColumnResizeMode="NextColumn" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />--%>


                        <SettingsBehavior FilterRowMode="OnClick" ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />

                        <Columns>

                            <dx:GridViewDataTextColumn FieldName="DocNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="LineNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />

                            <dx:GridViewDataTextColumn FieldName="ItemCode" VisibleIndex="1" Width="100px" Name="glItemCode" ReadOnly="True" Caption="ItemCode">
                                <EditItemTemplate>
                                    <dx:ASPxGridLookup ID="glItemCode" runat="server" AutoGenerateColumns="False" AutoPostBack="false" OnInit="glItemCode_Init"
                                        DataSourceID="Masterfileitem" KeyFieldName="ItemCode" ClientInstanceName="glItemCode" TextFormatString="{0}" Width="100px">
                                        <GridViewProperties Settings-ShowFilterRow="true">
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                AllowSelectSingleRowOnly="True" AllowDragDrop="False" />
                                        </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="ItemCode" ReadOnly="True" VisibleIndex="0" Settings-AutoFilterCondition="Contains" />
                                            <dx:GridViewDataTextColumn FieldName="FullDesc" ReadOnly="True" VisibleIndex="1" Settings-AutoFilterCondition="Contains" />
                                        </Columns>
                                        <ClientSideEvents DropDown="function(s,e){glItemCode.GetGridView().PerformCallback('ItemCodeDropDown');}" KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" />

                                    </dx:ASPxGridLookup>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataSpinEditColumn Caption="ReceivedWgt" FieldName="ReceivedQty" ShowInCustomizationForm="false" VisibleIndex="2" Width="100px" ReadOnly="false">
                                <PropertiesSpinEdit DisplayFormatString="n2" MinValue ="0" MaxValue="99999999"/>
                            </dx:GridViewDataSpinEditColumn>

                            <dx:GridViewDataTextColumn FieldName="BulkUnit" Caption="Unit" ShowInCustomizationForm="True" VisibleIndex="3" Width="50px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="BulkQty" Caption="Qty" ShowInCustomizationForm="True" VisibleIndex="4" Width="70px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="PalletID" Caption="Pallet" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px" />
                            <dx:GridViewDataDateColumn FieldName="ManufacturingDate" Caption="MfgDate" ShowInCustomizationForm="True" VisibleIndex="6" Width="107px" Name="MfgDate" PropertiesDateEdit-DropDownButton-Enabled="false" ReadOnly="true">
                                <PropertiesDateEdit DisplayFormatString="MM/dd/yy HH:mm">
                                    <DropDownButton Enabled="False"></DropDownButton>
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataDateColumn FieldName="ExpiryDate" Caption="ExpDate" ShowInCustomizationForm="True" VisibleIndex="7" Width="107px" Name="ExpDate" PropertiesDateEdit-DropDownButton-Enabled="false" ReadOnly="true">
                                <PropertiesDateEdit DisplayFormatString="MM/dd/yy HH:mm">
                                    <DropDownButton Enabled="False"></DropDownButton>
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>


                            <dx:GridViewDataTextColumn FieldName="LotID" Caption="Lot" ShowInCustomizationForm="True" VisibleIndex="8" Width="100px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="BatchNumber" Caption="Batch#" ShowInCustomizationForm="True" VisibleIndex="9" Width="100px" ReadOnly="true" />

                            <%--    <dx:GridViewDataTextColumn ReadOnly="true" FieldName="ItemCode" VisibleIndex="4" Width="100px" Caption="ItemCode">

                                                                <PropertiesTextEdit>
                                                                    <Style Border-BorderWidth="0">
                                                                     </Style>
                                                                </PropertiesTextEdit>
                                      </dx:GridViewDataTextColumn>--%>
                        </Columns>
                        <ClientSideEvents BatchEditConfirmShowing="OnConfirmUpload"
                            BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                    </dx:ASPxGridView>

                    <table>
                        <tr>
                            <td style="padding: 10px 0px 0px 680px">

                                <dx:ASPxButton ID="ASPxButton7" runat="server" Text="Submit" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="SubmitAfterBlast" />
                                </dx:ASPxButton>
                            </td>
                            <td style="padding: 10px 0px 0px 10px">

                                <dx:ASPxButton ID="ASPxButton6" runat="server" Text="Update" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="UpdateAfterBlast" />
                                </dx:ASPxButton>
                            </td>

                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


        <%--Return Popup--%>
        <dx:ASPxPopupControl ID="ReturnPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="ReturnPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="400px" ShowHeader="true" Width="800px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true" OnRowUpdating="gvStepProcess_RowUpdating">
            <ClientSideEvents Shown=" function(){Returngrid.PerformCallback('Shown');} " />

            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxGridView ID="Returngrid" runat="server" Width="100%" ClientInstanceName="Returngrid" EnableTheming="True" KeyFieldName="DocNumber;LineNumber"
                        Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="Return_CustomCallback"
                        SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="true" SettingsEditing-Mode="Batch" AutoGenerateSelectButton="True">

                        <SettingsBehavior FilterRowMode="OnClick" ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />

                        <Columns>

                            <dx:GridViewDataTextColumn FieldName="DocNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="LineNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataCheckColumn FieldName="ItemReturn" ShowInCustomizationForm="True" VisibleIndex="1" Width="75px" Caption="Select" />
                            <dx:GridViewDataTextColumn FieldName="ItemCode" Caption="Item Code" VisibleIndex="2" Width="150px" Name="glItemCode" ReadOnly="True" />
                            <dx:GridViewDataTextColumn FieldName="FullDesc" Caption="Item Description" VisibleIndex="3" Width="300px" Name="glItemCode" ReadOnly="True" />
                            <dx:GridViewDataTextColumn FieldName="BulkQty" Caption="Qty" ShowInCustomizationForm="True" VisibleIndex="4" Width="75px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="BulkUnit" Caption="Bulk Unit" ShowInCustomizationForm="True" VisibleIndex="5" Width="75px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="BaseQty" Caption="Kilos" ShowInCustomizationForm="True" VisibleIndex="6" Width="75px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="Unit" Caption="Unit" ShowInCustomizationForm="True" VisibleIndex="7" Width="75px" ReadOnly="true" />

                        </Columns>
                        <ClientSideEvents BatchEditConfirmShowing="OnConfirmUpload"
                            BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                    </dx:ASPxGridView>

                    <table>
                        <tr>
                            <td style="padding: 10px 0px 0px 680px">

                                <dx:ASPxButton ID="ASPxButton8" runat="server" Text="Return" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="ReturnSubmit" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <%--COMI Popup--%>
        <dx:ASPxPopupControl ID="ComiPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="ComiPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="500px" ShowHeader="true" Width="800px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true" OnRowUpdating="gvStepProcess_RowUpdating">
            <ClientSideEvents Shown=" function(){
                comi_cp.PerformCallback('Shown');
                Comigrid.PerformCallback('Shown');} " />

            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="comi_cp" runat="server" ClientInstanceName="comi_cp" OnCallback="comi_Callback">
                        <%--                        <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>--%>

                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="comiCon" ClientInstanceName="comiCon" runat="server" Width="800px" Style="margin-left: -3px">
                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />

                                    <Items>
                                        <dx:LayoutGroup Caption="" ColCount="3">
                                            <Items>
                                                <dx:LayoutItem Caption="Batch:">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox ID="txtBatch" runat="server" Width="170px" Enabled="False" ColCount="1" ReadOnly="true">
                                                                <DisabledStyle BackColor="#F9F9F9" />
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Consignee:">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox ID="txtConsignee" runat="server" Width="170px" ColCount="1" OnLoad="TextboxLoad">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="">
                                            <Items>
                                                <dx:LayoutItem Caption="">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridView ID="Comigrid" runat="server" Width="100%" ClientInstanceName="Comigrid" EnableTheming="True" KeyFieldName="InbdocNumber;InblineNumber"
                                                                Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="Comi_CustomCallback"
                                                                SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="true" SettingsEditing-Mode="Batch">

                                                                <SettingsBehavior FilterRowMode="OnClick" ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                                <Paddings Padding="0px" />
                                                                <Border BorderWidth="0px" />

                                                                <Columns>

                                                                    <dx:GridViewDataTextColumn FieldName="InbdocNumber" Caption="DocNumber" ShowInCustomizationForm="True" VisibleIndex="1" Width="100px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="InblineNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="InbitemCode" Caption="ItemCode" ShowInCustomizationForm="True" VisibleIndex="1" Width="100px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="Inbdesc" Caption="Description" ShowInCustomizationForm="True" VisibleIndex="2" Width="100px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="Brand" Caption="Brand" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px" />
                                                                    <dx:GridViewDataTextColumn FieldName="Origin" Caption="Origin" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px" />
                                                                    <dx:GridViewDataTextColumn FieldName="MfgName" Caption="Manufacturer’s Name" ShowInCustomizationForm="True" VisibleIndex="5" Width="200px" />
                                                                    <dx:GridViewDataTextColumn FieldName="EstablishNo" Caption="Establish #" ShowInCustomizationForm="True" VisibleIndex="6" Width="100px" />

                                                                    <dx:GridViewDataTextColumn FieldName="VQMLICNo" Caption="VQMLICNo" ShowInCustomizationForm="True" VisibleIndex="8" Width="100px" />


                                                                </Columns>
                                                                <ClientSideEvents BatchEditConfirmShowing="OnConfirmUpload"
                                                                    BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                                                            </dx:ASPxGridView>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                            </Items>
                                        </dx:LayoutGroup>

                                    </Items>
                                    <%-- <ServerSideEvents CustomCallback="comiheader_CustomCallback" />--%>
                                </dx:ASPxFormLayout>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                    <table>
                        <tr>

                            <td style="padding: 10px 0px 0px 700px">

                                <dx:ASPxButton ID="ASPxButton10" runat="server" Text="Update" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="UpdateComi" />
                                </dx:ASPxButton>

                            </td>


                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--Replenish Popup--%>
        <dx:ASPxPopupControl ID="ReplenishPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="ReplenishPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="500px" ShowHeader="true" Width="800px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true" OnRowUpdating="gvStepProcess_RowUpdating">
            <ClientSideEvents Shown=" function(){replenishGrid.PerformCallback('Shown');} " />

            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="rep_cp" runat="server" ClientInstanceName="rep_cp">


                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="Replenish_Form" ClientInstanceName="Replenish_Form" runat="server" Width="800px" Style="margin-left: -3px">
                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />

                                    <Items>
                                        <dx:LayoutGroup Caption="" ColCount="4">
                                            <Items>
                                                <dx:LayoutItem Caption="Warehousecode:" Name="WarehouseCode">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridLookup ID="glWarehousecode" runat="server" Width="170px" ClientInstanceName="glWarehousecode" DataSourceID="Warehouse" OnLoad="LookupLoad" KeyFieldName="WarehouseCode" TextFormatString="{0}">
                                                                <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField IsRequired="True" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="Pink">
                                                                </InvalidStyle>
                                                                <GridViewProperties Settings-ShowFilterRow="true">
                                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />

                                                                    <Settings ShowFilterRow="True"></Settings>
                                                                </GridViewProperties>
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn FieldName="WarehouseCode" ReadOnly="True" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>

                                                                </Columns>
                                                                <ClientSideEvents Validation="OnValidation" KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" />
                                                            </dx:ASPxGridLookup>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Customer:">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridLookup Width="80px" ID="glStorerKey" runat="server" AutoGenerateColumns="False" ClientInstanceName="glStorerKey"
                                                                DataSourceID="customer" KeyFieldName="CustomerCode" OnLoad="LookupLoad" TextFormatString="{0}">
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
                                                                    <dx:GridViewDataTextColumn FieldName="CustomerCode" ReadOnly="True" Settings-AutoFilterCondition="Contains" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>

                                                                </Columns>
                                                                <ClientSideEvents Validation="OnValidation" KeyPress="gridLookup_KeyPress" KeyDown="gridLookup_KeyDown" />

                                                            </dx:ASPxGridLookup>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxButton ID="btnSearch" Width="50px" runat="server" Text="Search" AutoPostBack="false" UseSubmitBehavior="false" BackColor="CornflowerBlue" ForeColor="White">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                                       //loader.Show(); 
                                                                                       //loader.SetText('Searching...'); 
                                                                                       //gvExtract.PerformCallback('Pal');
                                                                                       replenishGrid.PerformCallback('Search');
                                                                                       }" />

                                                            </dx:ASPxButton>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="">
                                            <Items>
                                                <dx:LayoutItem Caption="">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxGridView ID="replenishGrid" runat="server" Width="100%" ClientInstanceName="replenishGrid" EnableTheming="True" KeyFieldName="ReferenceRecordID;LineNumber"
                                                                Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="Replenish_CustomCallback"
                                                                SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="true" SettingsEditing-Mode="Batch">

                                                                <SettingsBehavior FilterRowMode="OnClick" ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                                <Paddings Padding="0px" />
                                                                <Border BorderWidth="0px" />

                                                                <Columns>

                                                                    <dx:GridViewDataTextColumn FieldName="ReferenceRecordID" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="LineNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" ReadOnly="true" />

                                                                    <dx:GridViewDataCheckColumn FieldName="Refresh" Caption=" " Width="40px" VisibleIndex="1">
                                                                        <PropertiesCheckEdit>
                                                                            <ClientSideEvents CheckedChanged="function(s, e) { onCheckboxChanged(s, e); }" />
                                                                        </PropertiesCheckEdit>
                                                                    </dx:GridViewDataCheckColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="DocNumber" Caption="DocNumber" ShowInCustomizationForm="True" VisibleIndex="2" Width="100px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="WarehouseCode" Caption="WarehouseCode" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="CustomerCode" Caption="Customer" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px" ReadOnly="true" />
                                                                    <dx:GridViewDataTextColumn FieldName="Location" Caption="Location" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px" ReadOnly="true" />

                                                                    <dx:GridViewDataSpinEditColumn FieldName="MinimumWeight" ShowInCustomizationForm="false" VisibleIndex="6" Width="100px" ReadOnly="true">
                                                                        <PropertiesSpinEdit DisplayFormatString="n2" MinValue ="0" MaxValue="99999999" />
                                                                    </dx:GridViewDataSpinEditColumn>
                                                                    <dx:GridViewDataSpinEditColumn FieldName="CurrentWeight" ShowInCustomizationForm="false" VisibleIndex="7" Width="100px" ReadOnly="true">
                                                                        <PropertiesSpinEdit DisplayFormatString="n2" MinValue ="0" MaxValue="99999999" />
                                                                    </dx:GridViewDataSpinEditColumn>
                                                                    <dx:GridViewDataSpinEditColumn Caption="ExcessWeight" FieldName="RemainingWeight" ShowInCustomizationForm="false" VisibleIndex="8" Width="120px" ReadOnly="true">
                                                                        <PropertiesSpinEdit DisplayFormatString="n2" />
                                                                    </dx:GridViewDataSpinEditColumn>
                                                                    <dx:GridViewDataSpinEditColumn Caption="MaxWeight" FieldName="MaxWeight" ShowInCustomizationForm="false" VisibleIndex="8" Width="120px" ReadOnly="true">
                                                                        <PropertiesSpinEdit DisplayFormatString="n2" />
                                                                    </dx:GridViewDataSpinEditColumn>
                                                                </Columns>
                                                                <ClientSideEvents BatchEditConfirmShowing="OnConfirmUpload"
                                                                    BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
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

                    <table>
                        <tr>

                            <td style="padding: 10px 0px 0px 700px">

                                <dx:ASPxButton ID="ASPxButton12" runat="server" Text="Replenish" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="UpdateReplenish" />
                                </dx:ASPxButton>

                            </td>


                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--Assign Popup--%>
        <dx:ASPxPopupControl ID="AssignPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="AssignPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="300px" ShowHeader="true" Width="350px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true" OnRowUpdating="gvStepProcess_RowUpdating">
            <ClientSideEvents Shown="function(){Assigngrid.PerformCallback('Shown');}" />
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxGridView ID="Assigngrid" runat="server" Width="100%" ClientInstanceName="Assigngrid" EnableTheming="True" KeyFieldName="DocNumber"
                        Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="Assigngrid_CustomCallback" SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="true">

                        <SettingsBehavior AllowSort="False" FilterRowMode="OnClick" ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />
                        <Columns>
                            <%--    DocNumber--%>
                            <dx:GridViewDataTextColumn FieldName="DocNumber" ShowInCustomizationForm="True" VisibleIndex="0" Width="150px" />
                            <%--    DocDate--%>
                            <dx:GridViewDataTextColumn FieldName="DocDate" ShowInCustomizationForm="True" VisibleIndex="0" Width="120px">
                                <PropertiesTextEdit DisplayFormatString="MM/dd/yy" />
                            </dx:GridViewDataTextColumn>
                            <%--    WarehouseChecker--%>
                            <dx:GridViewDataTextColumn FieldName="WarehouseChecker" VisibleIndex="0" Width="120px" Name="glpItemCode" ReadOnly="True" Caption="WarehouseChecker">
                                <EditItemTemplate>
                                    <dx:ASPxGridLookup ID="glWarehouseChecker" runat="server" AutoGenerateColumns="False" AutoPostBack="false"
                                        DataSourceID="sdsWarehouseChecker" KeyFieldName="UserId" ClientInstanceName="glWarehouseChecker" TextFormatString="{0}" Width="120px" ReadOnly="False" Enabled="true">
                                        <GridViewProperties Settings-ShowFilterRow="true">
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="False"
                                                AllowSelectSingleRowOnly="False" />
                                        </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" />
                                            <dx:GridViewDataColumn FieldName="UserId" ReadOnly="True" VisibleIndex="1" Settings-AutoFilterCondition="Contains" />
                                            <dx:GridViewDataColumn FieldName="Name" ReadOnly="True" VisibleIndex="2" Settings-AutoFilterCondition="Contains" />
                                        </Columns>
                                        <ClientSideEvents KeyPress="gridLookup_KeyPress"
                                            KeyDown="gridLookup_KeyDown"
                                            DropDown="lookup"
                                            CloseUp="gridLookup_CloseUp"
                                            ValueChanged="function(s, e) { 
                                               var g = s.GetGridView();
                                               g.GetRowValues(g.GetFocusedRowIndex(), 'UserId', UpdateCheckerChanged);
                                           }" />
                                    </dx:ASPxGridLookup>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <%--    AssignedDate--%>
                            <dx:GridViewDataDateColumn FieldName="AssignedDate" ShowInCustomizationForm="True" VisibleIndex="18" Width="150px" Name="dtpAssignedDate" PropertiesDateEdit-DropDownButton-Enabled="false" ReadOnly="true">
                                <PropertiesDateEdit DisplayFormatString="MM/dd/yy HH:mm">
                                    <DropDownButton Enabled="False"></DropDownButton>
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <%--    Status--%>
                            <dx:GridViewDataTextColumn FieldName="Status" ShowInCustomizationForm="True" VisibleIndex="0" Width="100px" ReadOnly="true" />


                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <ClientSideEvents BatchEditConfirmShowing="OnConfirmUpload"
                            BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                    </dx:ASPxGridView>
                    <table>
                        <tr>
                            <td style="padding: 5px 0px 5px 570px">
                                <dx:ASPxButton ID="ASPxButton11" runat="server" Text="Assign" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="AssignUpdate" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>

                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <%--Assign Popup--%>

        <%--MultiAssign Popup--%>
        <dx:ASPxPopupControl ID="MultiAssignPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="MultiAssignPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="300px" ShowHeader="true" Width="350px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true" OnRowUpdating="gvStepProcess_RowUpdating">
            <ClientSideEvents Shown="function(){MultiAssigngrid.PerformCallback('Shown');}"/>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxGridView ID="MultiAssigngrid" runat="server" Width="100%" ClientInstanceName="MultiAssigngrid" EnableTheming="True" KeyFieldName="DocNumber;WarehouseChecker"
                        Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="MultiAssigngrid_CustomCallback" SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="true">

                        <SettingsBehavior AllowSort="False" FilterRowMode="OnClick" ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Auto"/>
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />
                        <Columns>
                            <%--    DocNumber--%>
                            <dx:GridViewDataTextColumn FieldName="DocNumber" ShowInCustomizationForm="False" VisibleIndex="2" Width="150px" ReadOnly="True" />
                            <%--    DocDate--%>
                            <dx:GridViewDataTextColumn FieldName="DocDate" ShowInCustomizationForm="True" VisibleIndex="3" Width="120px" ReadOnly="True">
                                <PropertiesTextEdit DisplayFormatString="MM/dd/yy" />
                            </dx:GridViewDataTextColumn>
                            <%--    WarehouseChecker--%>
                            <dx:GridViewDataTextColumn FieldName="WarehouseChecker" VisibleIndex="4" Width="120px" Name="glpItemCode" ReadOnly="True" Caption="WarehouseChecker">
                                <EditItemTemplate>
                                    <dx:ASPxGridLookup ID="glWarehouseChecker" runat="server" AutoGenerateColumns="False" AutoPostBack="false"
                                        DataSourceID="sdsWarehouseChecker" KeyFieldName="UserId" ClientInstanceName="glWarehouseChecker" TextFormatString="{0}" Width="120px" ReadOnly="False" Enabled="true">
                                        <GridViewProperties Settings-ShowFilterRow="true">
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"
                                                AllowSelectSingleRowOnly="True" />
                                        </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="UserId" ReadOnly="True" VisibleIndex="1" Settings-AutoFilterCondition="Contains" />
                                            <dx:GridViewDataTextColumn FieldName="Name" ReadOnly="True" VisibleIndex="2" Settings-AutoFilterCondition="Contains" />
                                        </Columns>
                                        <ClientSideEvents KeyPress="gridLookup_KeyPress1"
                                            KeyDown="gridLookup_KeyDown1"
                                            DropDown="lookup"
                                            CloseUp="gridLookup_CloseUp1"
                                            ValueChanged="function(s, e) { 
                                               var g = s.GetGridView();
                                               g.GetRowValues(g.GetFocusedRowIndex(), 'UserId', UpdateCheckerChangedMulti);
                                           }" />
                                    </dx:ASPxGridLookup>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <%--    AssignedDate--%>
                            <dx:GridViewDataDateColumn FieldName="AssignedDate" ShowInCustomizationForm="True" VisibleIndex="6" Width="150px" Name="dtpAssignedDate" PropertiesDateEdit-DropDownButton-Enabled="false" ReadOnly="true">
                                <PropertiesDateEdit DisplayFormatString="MM/dd/yy HH:mm">
                                    <DropDownButton Enabled="False"></DropDownButton>
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <%--    Status--%>
                            <dx:GridViewDataTextColumn FieldName="Status" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px" ReadOnly="true" />
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <ClientSideEvents BatchEditConfirmShowing="OnConfirmUpload"
                            BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                    </dx:ASPxGridView>
                    <table>
                        <tr>
                            <td style="padding: 5px 0px 5px 0px">
                                <dx:ASPxButton ID="ASPxButton13" runat="server" Text="Rejected Records" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="function(s, e) { ViewRejectData(); }" />
                                </dx:ASPxButton>
                            </td>
                            <td style="padding: 5px 0px 5px 470px">
                                <dx:ASPxButton ID="ASPxButton14" runat="server" Text="Update" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="function(s, e) { MultiAssignUpdate('MultiAssigngrid'); }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>

                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <%--MultiAssign Popup--%>

        <%--RejectDetails Popup--%>
        <dx:ASPxPopupControl ID="RejectPop" Theme="Aqua" runat="server" AllowDragging="True" ClientInstanceName="RejectPop" CloseAction="CloseButton" CloseOnEscape="true"
            EnableViewState="False" HeaderImage-Height="10px" Opacity="100" HeaderText="" Height="300px" ShowHeader="true" Width="500px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="true" ShowOnPageLoad="false" ShowShadow="True" Modal="true" ContentStyle-HorizontalAlign="Center" SettingsLoadingPanel-Enabled="true" OnRowUpdating="gvStepProcess_RowUpdating">
            <ClientSideEvents Shown=" function(){Rejectgrid.PerformCallback('Shown');} " />

            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxGridView ID="Rejectgrid" runat="server" Width="100%" ClientInstanceName="Rejectgrid" EnableTheming="True" KeyFieldName="DocNumber;WarehouseChecker"
                        Theme="Office2010Blue" AutoGenerateColumns="true" Settings-ShowStatusBar="Hidden" OnCustomCallback="Rejectgrid_CustomCallback"
                        SettingsEditing-BatchEditSettings-ShowConfirmOnLosingChanges="true" SettingsEditing-Mode="Batch" AutoGenerateSelectButton="True">

                        <SettingsBehavior AllowSort="False" FilterRowMode="OnClick" ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />

                        <Columns>

                            <dx:GridViewDataTextColumn FieldName="DocNumber" ShowInCustomizationForm="True" VisibleIndex="1" Width="0px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="WarehouseChecker" ShowInCustomizationForm="True" Caption="WarehouseChecker" VisibleIndex ="2" Width="120px" ReadOnly="true" />
                            <dx:GridViewDataTextColumn FieldName="RejectedDate" ShowInCustomizationForm="True" Caption="Reject Date" VisibleIndex ="4" Width="100px" ReadOnly="True">
                                <PropertiesTextEdit DisplayFormatString="MM/dd/yy" />
                                </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="AuthorizedBy" Caption="Authorized" VisibleIndex="5" Width="100px" ReadOnly="True" />
                            <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" VisibleIndex="5" Width="400px" ReadOnly="True" />

                        </Columns>
                        <ClientSideEvents BatchEditConfirmShowing="OnConfirmUpload"
                            BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />
                    </dx:ASPxGridView>
                    <table>
                        <tr>
                            <td style="padding: 5px 0px 5px 410px">
                                <dx:ASPxButton ID="ASPxButton16" runat="server" Text="Back" Height="33px" AutoPostBack="False" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="function(s, e) { ReturnToAssign(); }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
        </dx:ASPxGlobalEvents>
        <%--RejectDetails Popup--%>

        <%--<!--#region Region SqlDataSource --> --%>
        <wc:UpldDocsPopup ID="UpldDocsPopup" runat="server" Visible="true" />
        <wc:UpldDocsPopup ID="AppnDocsPopup" runat="server" Visible="true" />
		<%--2023-07-07  TL  End --%>
        <%--2023-12-11  TL  Incomplete Document Popup--%>
        <wc:DocRmkPopup ID="DocRmkPopup" runat="server" Visible="true" />
        <wc:DocRmkPopup ID="DocErrorPopup" runat="server" Visible="true" />
        <asp:SqlDataSource ID="POmain" OnInit="Connection_Init" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=""
            OnSelecting="POmain_Selecting">
            <DeleteParameters>
                <asp:Parameter Name="DocNumber" Type="String" />
            </DeleteParameters>
            <SelectParameters>
                <asp:Parameter Name="date1" Type="String" />
                <asp:Parameter Name="date2" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="Chargessql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="EXEC sp_Generate_Charges @Docn,@CusCode,@Trans"
            UpdateCommand="UPDATE WMS.Charges SET Chargeflag=@CFlag, ChargesQty=@CQty  WHERE RecordID=@ParamURecordID">
            <UpdateParameters>
                <asp:Parameter Name="ParamURecordID" Type="String" />
                <asp:Parameter Name="CFlag" Type="String" />
                <asp:Parameter Name="CQty" Type="String" />
            </UpdateParameters>

            <SelectParameters>
                <asp:Parameter Name="CusCode" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Docn" Type="String" DefaultValue="0" />
                <asp:Parameter Name="Trans" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="Docnumsettings" OnInit="Connection_Init" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT [Module], [Counter], [Prefix], [SeriesWidth], [SeriesNumber], [IsDefault] FROM it.[DocNumberSettings] where module = @moduleid">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="moduleid" Name="moduleid" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="Copysel" OnInit="Connection_Init" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="">
            <SelectParameters>
                <asp:Parameter Name="date1" Type="String" />
                <asp:Parameter Name="date2" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="printsel" runat="server" OnInit="Connection_Init" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT text FROM masterfile.formlayout where formname = @form">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="transtype" Name="form" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <%-- 2016-03-18  Tony --%>
        <%--        SelectCommand="SELECT CompanyCode, CompanyDescription AS CompanyName FROM KPI.Company WHERE ISNULL(IsInactive,0) = 0" --%>

        <asp:SqlDataSource ID="sdsStorageType" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>"
            SelectCommand="SELECT StorageType,StorageDescription FROM Masterfile.StorageType"
            OnInit="Connection_Init"></asp:SqlDataSource>

        <%-- 2016-03-18  Tony  End --%>
        <%--<!--#endregion --> --%>
        <asp:SqlDataSource ID="GetColumnSql" OnInit="Connection_Init" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="">
            <SelectParameters>
                <asp:Parameter Name="date1" Type="String" />
                <asp:Parameter Name="date2" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsWarehouseChecker" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT UserId, FullName AS Name FROM IT.Users WHERE UserType = 'RF'" OnInit="Connection_Init"></asp:SqlDataSource>
        <asp:SqlDataSource ID="Masterfileitem" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand=" " OnInit="Connection_Init"></asp:SqlDataSource>
        
        <asp:SqlDataSource ID="Warehouse" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT [WarehouseCode], [Description] FROM Masterfile.[Warehouse] WHERE ISNULL([IsInactive],0) = 0" OnInit="Connection_Init"></asp:SqlDataSource>
        <asp:SqlDataSource ID="customer" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT [BizPartnerCode] as CustomerCode FROM Masterfile.[BizPartner] WHERE ISNULL([IsInactive],0) = 0" OnInit="Connection_Init"></asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="MultiAssignsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="EXEC sp_MULTI_ASSIGN 'MULTI',@DocNumber,@DateF,@DateT">
            <SelectParameters>
                <asp:Parameter Name="User" Type="String" DefaultValue="0" />
                <asp:Parameter Name="DocNumber" Type="String" DefaultValue="0" />
                <asp:Parameter Name="DateF" Type="String" DefaultValue="0" />
                <asp:Parameter Name="DateT" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="Assignsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="EXEC sp_MULTI_ASSIGN 'SINGLE',@DocNumber,@DateF,@DateT">
            <SelectParameters>
                <asp:Parameter Name="User" Type="String" DefaultValue="0" />
                <asp:Parameter Name="DocNumber" Type="String" DefaultValue="0" />
                <asp:Parameter Name="DateF" Type="String" DefaultValue="0" />
                <asp:Parameter Name="DateT" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="AfterBlastsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select [DocNumber],[LineNumber],A.ItemCode, CASE WHEN ISNULL(A.[ReceivedQty],0) = 0 OR A.[ReceivedQty] < 0 THEN 0 ELSE A.[ReceivedQty] END as ReceivedQty,CASE WHEN ISNULL(A.[BulkQty],0) = 0 OR A.[BulkQty] < 0 THEN 0 ELSE A.[BulkQty] END as BulkQty,[BulkUnit],[ManufacturingDate],[ExpiryDate],[BatchNumber],[PalletID],[LotID] from wms.InboundDetail A LEFT JOIN  Masterfile.Item B on A.ItemCode  = B.ItemCode where DocNumber = @DocNumber and ISNULL(B.Blast,0) !=0">
            <SelectParameters>
                <asp:Parameter Name="DocNumber" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="Replenishsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ISNULL(c.DocNumber,'') as DocNumber,ROW_NUMBER() OVER(order by a.Minweight) as LineNumber,ISNULL(c.Refresh,0) as Refresh,a.ReferenceRecordId as ReferenceRecordID,ISNULL(a.MinWeight,0) as MinimumWeight,ISNULL(b.CurrentWeight,0) as CurrentWeight,CASE WHEN ISNULL(a.MinWeight,0) > ISNULL(b.CurrentWeight,0) THEN ISNULL(a.MinWeight,0) - ISNULL(b.CurrentWeight,0) ELSE ISNULL(b.CurrentWeight,0) - ISNULL(a.MinWeight,0) END AS RemainingWeight,ISNULL(a.MaxBaseQty,0) as MaxWeight,b.Location,a.WarehouseCode,a.CustomerCode from masterfile.Location a left join (select sum(RemainingBaseQty) as CurrentWeight,Location from wms.countsheetsetup group by Location) b on a.LocationCode = b.Location left join (select Refresh,ReferenceRecordID,DocNumber from wms.replenishment) c on CONVERT(varchar, c.ReferenceRecordID) = a.ReferenceRecordID where ISNULL(a.CustomerCode,'') != '' and ISNULL(a.WarehouseCode,'') != '' and ISNULL(b.Location,'') != '' group by b.Location,a.Minweight,b.CurrentWeight,a.WarehouseCode,a.CustomerCode,a.ReferenceRecordId,a.MaxBaseQty,a.replenish,c.DocNumber,c.Refresh"></asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="Returnsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DocNumber,LineNumber,A.ItemCode,B.FullDesc,BulkQty,BulkUnit,BaseQty,Unit,CASE WHEN ISNULL(ItemReturn, '') = '' THEN 0 ELSE ItemReturn END AS ItemReturn FROM wms.OutboundDetail A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE DocNumber = @DocNumber">
            <SelectParameters>
                <asp:Parameter Name="DocNumber" Type="String" DefaultValue="0" />
                <asp:Parameter Name="LineNumber" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="Rejectsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT DocNumber, WarehouseChecker, RejectedDate, AuthorizedBy, Remarks FROM WMS.RejectDetails WHERE DocNumber = @DocNumber">
            <SelectParameters>
                <asp:Parameter Name="DocNumber" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="Comisql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select a.ItemCode as InbitemCode,a.LineNumber as InblineNumber,@DocNumber as InbdocNumber,b.FullDesc as Inbdesc,c.* from WMS.InboundDetail a left join masterfile.item b on a.ItemCode = b.ItemCode left join wms.[COMIDetail] c on a.DocNumber = c.DocNumber and a.LineNumber = c.LineNumber where a.DocNumber = @DocNumber">
            <SelectParameters>
                <asp:Parameter Name="DocNumber" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource OnInit="Connection_Init" ID="ComiHSql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select Batch,Consignee from wms.comi where DocNumber = @DocNumber">
            <SelectParameters>
                <asp:Parameter Name="DocNumber" Type="String" DefaultValue="0" />

            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
