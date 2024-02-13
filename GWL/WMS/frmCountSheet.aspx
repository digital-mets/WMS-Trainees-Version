<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCountSheet.aspx.cs" Inherits="GWL.WMS.frmCountSheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <title></title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" />
    <%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
            height: 700px; /*Change this whenever needed*/
        }

        .Entry {
            width: 914px; /*Change this whenever needed*/
            padding: 10px;
            margin: 20px auto;
            background: #FFF;
            border-radius: 10px;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            -moz-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            -webkit-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
        }

        .pnl-content {
            text-align: right;
        }
    </style>
    <!--#endregion-->

    <!--#region Javascript-->
    <script>
        var isValid = false;
        var isValid2 = true;
        var counterror = 0;

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function calcuinit(s, e) {
            autocalculateALL();
        }

        function calcuinit2(s, e) {
            Bulk.SetVisible(false);
            autocalculateALL2();
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
            cp.PerformCallback("Update");
        }

        function OnConfirm(s, e) {//function upon saving entry
            if (e.requestTriggerID === "cp")//disables confirmation message upon saving.
                e.cancel = true;
        }

        function gridView_EndCallback(s, e) {
            if (s.cp_message != null) {
                alert(s.cp_message);
                delete (s.cp_message);
                countsheetheader.Refresh();
            }

            if (s.cp_error != null) {
                alert(s.cp_error);
                delete (s.cp_error);
            }

            if (s.cp_gensuccess) {
                if (!alert('Successfully Generated! Please wait while this countsheet reloads...')) {
                    txtFrom.SetText(null);
                    txtTo.SetText(null);
                    txtPallet.SetText(null);
                    txtExpDate.SetText(null);
                    txtMfgDate.SetText(null);
                    txtQty.SetText(null);
                    window.location.reload();
                }
            }
            
        }

        function OnStartEditing(s, e) {//On start edit grid function     
            currentColumn = e.focusedColumn;
            var cellInfo = e.rowValues[e.focusedColumn.index];

            if (s.batchEditApi.GetCellValue(e.visibleIndex, "PutawayDate") != null) {
                e.cancel = true;
            }

            if (e.focusedColumn.fieldName === "Location") { //Check the column name
                gl.GetInputElement().value = cellInfo.value; //Gets the column value
                isSetTextRequired = true;
            }
        }

        function OnEndEditing(s, e) {//end edit grid function, sets text after select/leaving the current lookup
            var cellInfo = e.rowValues[currentColumn.index];
            if (currentColumn.fieldName === "Location") {
                cellInfo.value = gl.GetValue();
                cellInfo.text = gl.GetText().toUpperCase();
            }
        }

        function gridLookup_CloseUp(s, e) { //Automatically leaves the current cell if an item is selected.
            countsheetsetup.batchEditApi.EndEdit();
        }

        function Clear(s, e) {
            txtFrom.SetText(null);
            txtTo.SetText(null);
            txtPallet.SetText(null);
            txtExpDate.SetText(null);
            txtMfgDate.SetText(null);
            txtQty.SetText(null);
        }

        function OnGenerate(s, e) {
            if (isValid && counterror < 1 && isValid2) {
              var generate = confirm('Are you sure you want to continue? Note: Changes are committed after generating');
              if (generate) {
                  cp.PerformCallback('Generate');
              }
           }
           else {
               counterror = 0;
               alert('Please check all the fields!');
           }
        }

        

        function onload() {
            var type = getParameterByName('type');
            var entry = getParameterByName('entry');
            var linenum = getParameterByName('linenumber');
            if (type != null) {
                if (type == "Putaway" || type == "PutawayM") {
                    var g = fl.GetItemByName('LG');
                    var g2 = fl.GetItemByName('Pallet');
                    //g2.SetVisible(!g.GetVisible());
                    g.SetVisible(!g.GetVisible());
                }
            }
            if (linenum == 'null') {
                var g = fl.GetItemByName('LG');
                var inf = fl.GetItemByName('Inf');
                g.SetVisible(!g.GetVisible());
                inf.SetVisible(!inf.GetVisible());
                btnCancel.SetVisible(false);
            }
            if (entry != "V") {
                alert('Please generate first, if you are going to change multiple values before making any changes to the grid.');
            }
            else {
                var g = fl.GetItemByName('LG');
                g.SetVisible(g.GetVisible());
            }
        }

        //function SetDifference() {
        //    var diff = CheckDifference();
        //    if (diff > 0) {
        //        clientResult.SetText(diff.toString());
        //    }
        //}

        function CheckDifference() {
            if (txtMfgDate.GetText() != "" && txtExpDate.GetText() != "") {
                console.log('test');
                var startDate = new Date();
                var endDate = new Date();
                var difference = -1;
                startDate = txtMfgDate.GetDate();
                if (startDate != null) {
                    endDate = txtExpDate.GetDate();
                    var startTime = startDate.getTime();
                    var endTime = endDate.getTime();
                    difference = (endTime - startTime) / 86400000;
                }
                if (difference >= 0) {
                    isValid2 = true;
                }
                else {
                    isValid2 = false;
                }
            }
        }

        function checkdate(s, e) {
            CheckDifference()
            e.isValid = isValid2;
        }

        function autocalculateALL(s, e) {
            var allnum = [];
            var allnum2 = [];

            var indicies = countsheetsetup.batchEditHelper.GetDataItemVisibleIndices();
            for (var i = 0; i < indicies.length; i++) {
                if (countsheetsetup.batchEditHelper.IsNewItem(indicies[i])) {
                    var getnum = parseFloat(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBaseQty"));
                    var getnum2 = (countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty") != null) ? parseInt(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty")) : 0;
                    allnum.push(getnum);
                    allnum2.push(getnum2);
                }
                else {
                    var key = countsheetsetup.GetRowKey(indicies[i]);
                    if (countsheetsetup.batchEditHelper.IsDeletedItem(key))
                        console.log("deleted row " + indicies[i]);
                    else {
                        var getnum = parseFloat(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBaseQty"));
                        var getnum2 = (countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty") != null) ? parseInt(countsheetsetup.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty")) : 0;
                        allnum.push(getnum);
                        allnum2.push(getnum2);
                    }
                }
            }

            if (allnum.length > 0 || allnum2.length > 0) {
                var sum = allnum.reduce(function (a, b) { return a + b; });
                var sum2 = allnum2.reduce(function (a, b) { return a + b; });

                Array.prototype.max = function () {
                    return Math.max.apply(null, this);
                };

                Array.prototype.min = function () {
                    return Math.min.apply(null, this);
                };

                var avg = sum / indicies.length;

                Min.SetText("Min: " + allnum.min());
                Max.SetText("Max: " + allnum.max());
                Sum.SetText("Sum: " + sum.toFixed(2));
                Average.SetText("Average: " + avg.toFixed(9));
                Bulk.SetText("Sum of BulkQty: " + sum2);
            }
        }

        function OnCancelClick(s, e) {
            if (countsheetsetup.GetVisible()) {
                countsheetsetup.CancelEdit();
                autocalculateALL();
            }
            if (countsheetsubsi.GetVisible()){
                countsheetsubsi.CancelEdit();
                autocalculateALL2();
            }
            
        }

        function autocalculateALL2(s, e) {
            var allnum = [];
            var allnum2 = [];

            var indicies = countsheetsubsi.batchEditHelper.GetDataItemVisibleIndices();
            for (var i = 0; i < indicies.length; i++) {
                if (countsheetsubsi.batchEditHelper.IsNewItem(indicies[i])) {
                    var getnum = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                    var getnum2 = (countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty") != null) ? parseInt(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty")) : 0;
                    allnum.push(getnum);
                    allnum2.push(getnum2);
                }
                else {
                    var key = countsheetsubsi.GetRowKey(indicies[i]);
                    if (countsheetsubsi.batchEditHelper.IsDeletedItem(key))
                        console.log("deleted row " + indicies[i]);
                    else {
                        var getnum = parseFloat(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "UsedQty"));
                        var getnum2 = (countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty") != null) ? parseInt(countsheetsubsi.batchEditApi.GetCellValue(indicies[i], "OriginalBulkQty")) : 0;
                        allnum.push(getnum);
                        allnum2.push(getnum2);
                    }
                }
            }

            if (allnum.length > 0 || allnum2.length > 0) {
                var sum = allnum.reduce(function (a, b) { return a + b; });
                var sum2 = allnum2.reduce(function (a, b) { return a + b; });

                Array.prototype.max = function () {
                    return Math.max.apply(null, this);
                };

                Array.prototype.min = function () {
                    return Math.min.apply(null, this);
                };

                var avg = sum / indicies.length;

                Min.SetText("Min: " + allnum.min());
                Max.SetText("Max: " + allnum.max());
                Sum.SetText("Sum: " + sum.toFixed(2));
                Average.SetText("Average: " + avg.toFixed(9));
                Bulk.SetText("Sum of BulkQty: " + sum2);
            }
        }
    </script>
    <!--#endregion-->
</head>
<body style="height: 700px" onload="onload()">
    <form id="form1" runat="server" class="Entry">
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="641px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" ClientInstanceName="fl" runat="server" DataSourceID="" Height="565px" Width="910px" Style="margin-left: -3px" ColCount="2">
                        <Items>
                            <dx:LayoutItem Caption="Transaction Type">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtTransType" runat="server" Width="170px" ReadOnly="true">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Doc No.">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server">
                                        <dx:ASPxTextBox ID="txtDocNumber" runat="server" Width="170px" ReadOnly="true">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutGroup Caption="Generate details" ColSpan="2" ColCount="7" Name="LG">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxTextBox ID="txtFrom" ClientInstanceName="txtFrom" runat="server" Width="50px">
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
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxLabel ID="frmlayout1_E9" runat="server" Text="~" Width="10px">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxTextBox ID="txtTo" ClientInstanceName="txtTo" runat="server" Width="50px">
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
                                    <dx:LayoutItem Caption="Pallet ID" Name="Pallet">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxTextBox ID="txtPallet" ClientInstanceName="txtPallet" runat="server" Width="90px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Mfg Date">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxDateEdit ID="txtMfgDate" ClientInstanceName="txtMfgDate" runat="server" Width="80px">
                                                </dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Exp. Date">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxDateEdit ID="txtExpDate" ClientInstanceName="txtExpDate" runat="server" Width="80px">
                                                    <ClientSideEvents Validation="checkdate"/>
                                                </dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Orig. Base Qty">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxTextBox ID="txtQty" ClientInstanceName="txtQty" runat="server" Width="50px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem>
                                    </dx:EmptyLayoutItem>
                                    <dx:EmptyLayoutItem>
                                    </dx:EmptyLayoutItem>
                                    <dx:EmptyLayoutItem>
                                    </dx:EmptyLayoutItem>
                                    <dx:EmptyLayoutItem>
                                    </dx:EmptyLayoutItem>
                                    <dx:EmptyLayoutItem>
                                    </dx:EmptyLayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxButton ID="frmlayout1_E6" runat="server" Text="Clear" AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="Clear" />
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxButton ID="genbtn" runat="server" Text="Generate" AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="OnGenerate" />
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Caption="Information" ColSpan="2" Name="Inf">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="header" runat="server" DataSourceID="countsheetheader"
                                                    ClientInstanceName="countsheetheader" KeyFieldName="TransDoc;TransLine" Width="742px" OnCellEditorInitialize="headerline_CellEditorInitialize">
                                                    <Settings HorizontalScrollBarMode="Visible" />
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Caption="Details" ColSpan="2">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <div id="loadingcont">
                                                <dx:ASPxGridView ID="countsheetsetup" runat="server" AutoGenerateColumns="False" ClientInstanceName="countsheetsetup" DataSourceID="countsheetdetailsetup" KeyFieldName="TransDoc;TransLine;LineNumber" OnCellEditorInitialize="countsheetsetup_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" Visible="False" Width="742px">
                                                    <ClientSideEvents Init="calcuinit" BatchEditConfirmShowing="OnConfirm" BatchEditEndEditing="OnEndEditing" BatchEditStartEditing="OnStartEditing" />
                                                    <SettingsEditing Mode="Batch">
                                                    </SettingsEditing>
                                                    <Settings ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="TransType" ShowInCustomizationForm="True" Visible="True" VisibleIndex="0" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransDoc" ShowInCustomizationForm="True" UnboundType="String" Visible="False" VisibleIndex="1" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransLine" ShowInCustomizationForm="True" UnboundType="String" Visible="False" VisibleIndex="2" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="3" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PalletID" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" Width="80px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="BatchNumber" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" Width="80px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Location" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" Width="80px" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                     
                                                        <%-- <dx:GridViewDataTextColumn FieldName="Location" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="8" Width="80px">
                                                            <EditItemTemplate>
                                                                <dx:ASPxGridLookup ID="glLocation" runat="server" AutoGenerateColumns="False" AutoPostBack="false" ClientInstanceName="gl" DataSourceID="locationsql" Enabled="false" KeyFieldName="LocationCode" OnLoad="glLocation_Load" TextFormatString="{0}" Width="80px">
                                                                    <GridViewProperties Settings-ShowFilterRow="true">
                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                    </GridViewProperties>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="LocationCode" ReadOnly="True" VisibleIndex="0" />
                                                                        <dx:GridViewDataTextColumn FieldName="WarehouseCode" ReadOnly="True" VisibleIndex="1" />
                                                                        <dx:GridViewDataTextColumn FieldName="RoomCode" ReadOnly="True" VisibleIndex="2" />
                                                                    </Columns>
                                                                    <ClientSideEvents Valuechanged="gridLookup_CloseUp" />
                                                                </dx:ASPxGridLookup>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>--%>
                                                        <dx:GridViewDataTextColumn FieldName="OriginalBulkQty" Caption="Original Qty" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="14" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="OriginalBaseQty" Caption="Original Kilos" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="15" Width="80px">
                                                                <PropertiesTextEdit>
                                                                    <ClientSideEvents TextChanged="function(){setTimeout(function(){autocalculateALL();},500);}" />
                                                                </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn FieldName="ExpirationDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="36" Width="80px">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="MfgDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="35" Width="80px">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="RRdate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="37" Width="80px" ReadOnly="true">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="PutawayDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="38" Width="80px" ReadOnly="true">
                                                        </dx:GridViewDataDateColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                    <SettingsBehavior AllowSort ="false" />
                                                </dx:ASPxGridView>
                                                
                                                <dx:ASPxGridView ID="countsheetsubsi" runat="server" AutoGenerateColumns="False" ClientInstanceName="countsheetsubsi" DataSourceID="countsheetdetailsubsi" KeyFieldName="TransDoc;TransLine;LineNumber" OnCellEditorInitialize="countsheetsubsi_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" Visible="False" Width="742px">
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="calcuinit2" />
                                                    <SettingsEditing Mode="Batch">
                                                    </SettingsEditing>
                                                    <Settings ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="TransType" ShowInCustomizationForm="True" Visible="True" VisibleIndex="0" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransDoc" ShowInCustomizationForm="True" UnboundType="String" Visible="False" VisibleIndex="1" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransLine" ShowInCustomizationForm="True" UnboundType="String" Visible="False" VisibleIndex="2" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="3" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PalletID" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="4" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                          <dx:GridViewDataTextColumn FieldName="ToPalletID" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="4" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataSpinEditColumn FieldName="DocBulkQty" Caption="Qty" ShowInCustomizationForm="True" VisibleIndex="4" Width="80px" >
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataTextColumn FieldName="UsedQty" Caption="Kilos" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SystemQty" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="8" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="VarianceQty" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="9" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Location" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="10" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ToLoc" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="16" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn FieldName="ExpirationDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="14" Width="90px">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="MfgDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="13" Width="90px">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="RRdate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="16" Width="80px">
                                                        </dx:GridViewDataDateColumn>
                                                        
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                    <SettingsBehavior AllowSort ="false" />
                                                </dx:ASPxGridView>

                                                <dx:ASPxGridView OnDataBound="countsheetsubsi_DataBound" ID="countsheetsubsi2" runat="server" AutoGenerateColumns="False" ClientInstanceName="countsheetsubsi" DataSourceID="countsheetdetailsubsi" KeyFieldName="TransDoc;TransLine;LineNumber" OnCellEditorInitialize="countsheetsubsi_CellEditorInitialize" OnCommandButtonInitialize="gv_CommandButtonInitialize" Visible="False" Width="742px">
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" Init="calcuinit2" />
                                                    <SettingsEditing Mode="Batch">
                                                    </SettingsEditing>
                                                    <Settings ShowStatusBar="Hidden" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="TransType" Caption="TransType" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="0" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransDoc" Caption="TransDoc" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="1" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TransLine" Caption="TransLine" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="2" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LineNumber" Caption="LineNumber" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="3" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ItemCode" Caption="ItemCode" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="4" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" Caption="ColorCode" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="5" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" Caption="ClassCode" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="6" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" Caption="SizeCode" ReadOnly="True" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="7" Width="0px">
                                                        </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataSpinEditColumn FieldName="DocBulkQty" Caption="Qty" ShowInCustomizationForm="True" VisibleIndex="7" Width="80px" >
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataTextColumn FieldName="UsedQty" Caption="Kilos" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="8" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                       
                                                        <dx:GridViewDataTextColumn FieldName="Location" Caption="Location" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="10" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PalletID" Caption="PalletID" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="11" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn FieldName="ExpirationDate" Caption="ExpirationDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="12" Width="90px">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="MfgDate" Caption="MfgDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="13" Width="90px">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="RRdate" Caption="RRdate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="14" Width="80px">
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ToLoc" Caption="ToLoc" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="15" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="DocDate" Caption="DocDate" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="16" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="BatchNo" Caption="BatchNo" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="17" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LotID" Caption="LotID" ShowInCustomizationForm="True" UnboundType="String" VisibleIndex="18" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                        
                                                        
                                                        
                                                        
                                                         
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                    <SettingsBehavior AllowSort ="false" />
                                                </dx:ASPxGridView>
                                                </div>
                                                <dx:ASPxLabel runat="server" Text="Min: " Width="100px" ClientInstanceName="Min"></dx:ASPxLabel>
                                                <dx:ASPxLabel runat="server" Text="Max: " Width="100px" ClientInstanceName="Max"></dx:ASPxLabel>
                                                <dx:ASPxLabel runat="server" Text="Average: " Width="150px" ClientInstanceName="Average"></dx:ASPxLabel>
                                                <dx:ASPxLabel runat="server" Text="Sum: " Width="100px" ClientInstanceName="Sum"></dx:ASPxLabel>
                                                <dx:ASPxLabel runat="server" Text="Sum of BulkQty: " Width="177px" ClientInstanceName="Bulk"></dx:ASPxLabel>
                                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel Changes" ClientInstanceName="btnCancel" CausesValidation="false" AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="OnCancelClick" />
                                                </dx:ASPxButton>
                                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" ExportedRowType="All" />
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:ASPxFormLayout>
                    <dx:ASPxPanel ID="BottomPanel" runat="server" FixedPosition="WindowBottom" BackColor="#FFFFFF" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <div class="pnl-content">
                                    <dx:ASPxButton ID="updateBtn" runat="server" Text="Update" AutoPostBack="False" CssClass="btn" ClientInstanceName="btn"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="OnUpdateClick" />
                                    </dx:ASPxButton>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Calculating..."
            ClientInstanceName="loader" ContainerElementID="loadingcont">
        </dx:ASPxLoadingPanel>
        
    </form>

    <!--#region Region Datasource-->
    <%--    <dx:GridViewDataTextColumn FieldName="ItemCode" VisibleIndex="5" Width="80px" UnboundType="String" />
                                                        <dx:GridViewDataTextColumn FieldName="ColorCode" VisibleIndex="6" Width="80px" UnboundType="String" />
                                                        <dx:GridViewDataTextColumn FieldName="ClassCode" VisibleIndex="7" Width="80px" UnboundType="String" />
                                                        <dx:GridViewDataTextColumn FieldName="SizeCode" VisibleIndex="8" Width="80px" UnboundType="String" />--%>
    <asp:SqlDataSource ID="countsheetheader" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="" OnInit ="Connection_Init"></asp:SqlDataSource>
    <asp:ObjectDataSource ID="countsheetdetailsubsi" runat="server" DataObjectTypeName="Entity.CountSheetSubsi" SelectMethod="getdetail" TypeName="Entity.CountSheetSubsi" UpdateMethod="UpdateCountSheetSubsi">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:QueryStringParameter Name="LineNumber" QueryStringField="linenumber" Type="String" />
            <asp:QueryStringParameter Name="TransType" QueryStringField="transtype" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="countsheetdetailsetup" runat="server" DataObjectTypeName="Entity.CountSheetSetup" SelectMethod="getdetail" TypeName="Entity.CountSheetSetup" UpdateMethod="UpdateCountSheetSetup">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
            <asp:QueryStringParameter Name="LineNumber" QueryStringField="linenumber" Type="String" />
            <asp:QueryStringParameter Name="TransType" QueryStringField="transtype" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="locationsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>"
        SelectCommand="Select LocationCode,WarehouseCode,RoomCode from masterfile.location where PlantCode = @Plant" OnInit ="Connection_Init">
        <SelectParameters>
            <asp:Parameter Name="Plant" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</body>
</html>
