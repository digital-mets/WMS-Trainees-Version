﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSalesForecast.aspx.cs" Inherits="GWL.Sales.frmSalesForecast" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
            height: 800px; /*Change this whenever needed*/
        }

        .Entry {
        padding: 20px;
        margin: 10px auto;
        background: #FFF;
        }

        

        .gvInline {
        display: inline-table;
        }

        .myclass::-moz-selection,
        .myclass::selection { ... }
    </style>
    <!--#endregion-->
    
    <!--#region Region Javascript-->
    <script>
        var isValid = false;
        var counterror = 0;
        var locked = false;

        function OnValidation(s, e) { //Validation function for header controls (Set this for each header controls)
            if (s.GetText() == "" || e.value == "" || e.value == null || s.GetValue() == null) {
                counterror++;
                isValid = false
            }
            else {
                isValid = true;
            }
        }

        function OnUpdateClickbtn(s, e) { //Add/Edit/Close button function

            var btnmode = btn.GetText(); //gets text of button
                if (btnmode == "Update") {
                    cp.PerformCallback("Update");
                }
        }

        function OnUpdateClick(s, e) { //Add/Edit/Close button function
            if (isValid && counterror < 1) { //check if there's no error then proceed to callback
                if (locked == false) 
                    alert('Parameter tab must be locked!');
                else
                    cp.PerformCallback('generate');
            }
            else {
                counterror = 0;
                alert('Please check all the fields!');
            }
        }

        function OnUpdateClick2(s, e) { //Add/Edit/Close button function
            if (isValid && counterror < 1) { //check if there's no error then proceed to callback
                if (locked == false)
                    alert('Parameter tab must be locked!');
                else
                cp.PerformCallback('retrieve');
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

        var fieldID;
        var onlyone = false;
        function gridView_EndCallback(s, e) {//End callback function if (s.cp_success) {
            if (s.cp_success) {
                alert(s.cp_message);
                delete (s.cp_success);//deletes cache variables' data
                delete (s.cp_message);
            }
            if (s.cp_close) {
                window.close();//close window if callback successful
            }
            if (s.cp_reload) {
                alert('Successfully saved! The page will reload...');
                location.reload();
            }
            if (s.cp_message) {
                alert(s.cp_message);
                delete (s.cp_message);
            }

            if (s.cp_field != null) {
                if (applbl.GetText() != '') {
                    btnref.SetEnabled(false);
                    btnsave.SetEnabled(false);
                    //btnsavever.SetEnabled(false);
                    buttonImport.SetEnabled(false);
                }
                fieldID = s.cp_field;
                delete (s.cp_field);
                if (s.cp_onlyone) {
                    onlyone = true;
                    delete (s.cp_onlyone);
                }
            }

            if (s.cp_successimp) {
                ImportCSheet.Hide();
                alert("Successfully Imported!");
                delete (s.cp_successimp);
            }
        }

        function OnLock(s, e) {
            if (lockbtn.GetText() == "Lock") {
                year.SetEnabled(false);
                startmonth.SetEnabled(false);
                itemcat.SetEnabled(false);
                txtagent.SetEnabled(false);
                vpick.SetEnabled(false);
                lockbtn.SetText("Unlock");
                locked = true;
            }
            else {
                year.SetEnabled(true);
                startmonth.SetEnabled(true);
                itemcat.SetEnabled(true);
                txtagent.SetEnabled(true);
                vpick.SetEnabled(true);
                lockbtn.SetText("Lock");
                locked = false;
            }
        }

        function CheckLock(s, e) {
            if (locked == true) {
                year.SetEnabled(false);
                startmonth.SetEnabled(false);
                itemcat.SetEnabled(false);
                txtagent.SetEnabled(false);
                vpick.SetEnabled(false);
                lockbtn.SetText("Unlock");
            }
            else {
                year.SetEnabled(true);
                startmonth.SetEnabled(true);
                itemcat.SetEnabled(true);
                txtagent.SetEnabled(true);
                vpick.SetEnabled(true);
                lockbtn.SetText("Lock");
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
            gv2.SetWidth(width - 550);
            gv3.SetWidth(width - 1100);
            //pivot.SetHeight(height - 200);
        }

        function pivotEnd(s,e){
            if (s.cp_callback) {
                //cp.PerformCallback('refreshgrid');
            }
        }

        function OnFileUploadComplete(s, e) {//Loads the excel file into the grid
            import_cp.PerformCallback();
        }

        function importEndCB(s,e) {
            if (s.cp_execmes == "Successfully imported") {
                cp.PerformCallback("import");
                delete (s.cp_execmes)
            }
        }
        function autocalculate() {
            var newterm = 0.00;
            var total = 0.00;
            var projsrp = 0.00;
            var forecast = 0.00;
            setTimeout(function () {
                var indicies = gv1.batchEditHelper.GetDataItemVisibleIndices();
                for (var i = 0; i < indicies.length; i++) {
                    if (gv1.batchEditHelper.IsNewItem(indicies[i])) {
                        var g;
                    }
                    else {
                        var key = gv1.GetRowKey(indicies[i]);
                        if (gv1.batchEditHelper.IsDeletedItem(key))
                            var q;
                        else {
                            console.log(indicies[i])
                            forecast = gv1.batchEditApi.GetCellValue(indicies[i], 'ForecastQty');
                            projsrp = gv1.batchEditApi.GetCellValue(indicies[i], 'ProjectedSRP');
                            newterm = gv1.batchEditApi.GetCellValue(indicies[i], 'NewTerm');
                            total = (forecast / 12) * projsrp * (newterm / 30);
                            gv1.batchEditApi.SetCellValue(indicies[i], 'NewCreditLimit', total.toFixed(2));
                        }
                    }
                }
            }, 500);
        }
    </script>
    <!--#endregion-->
</head>
<body style="height: 910px">
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <form id="form1" runat="server" class="Entry">
    <dx:ASPxPopupControl ID="ImportCSheet" runat="server" Width="290px" Height="100px" HeaderText="Import Excel"
        CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="ImportCSheet"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
           <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                  <dx:ASPxCallbackPanel ID="import_cp" runat="server" Width="200px" ClientInstanceName="import_cp"
                  OnCallback="import_cp_Callback">
                      <ClientSideEvents EndCallback="importEndCB" />
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
        <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel runat="server" Text="Sales Forecast" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="806px" Height="950px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="frmlayout1" runat="server" Height="565px" Width="1000px" style="margin-left: -3px" ColCount="3">
                        
                        <ClientSideEvents Init="CheckLock" />
                        <Items>
                            <dx:LayoutGroup Caption="Parameters" ColCount="2" ColSpan="2" Width="840">
                                <Items>
                                    <dx:LayoutItem Caption="Year">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <table>
                                                    <tr>
                                                        <td><dx:ASPxComboBox ID="year" runat="server" Width="60px" ClientInstanceName="year">
                                                            <Items>
                                                                <dx:ListEditItem Text="2020" Value="2020" />
                                                                <dx:ListEditItem Text="2019" Value="2019" />
                                                                <dx:ListEditItem Text="2018" Value="2018" />
                                                                <dx:ListEditItem Text="2017" Value="2017" />
                                                                <dx:ListEditItem Text="2016" Value="2016" Selected="true"/>
                                                                <dx:ListEditItem Text="2015" Value="2015"/>
                                                                <dx:ListEditItem Text="2014" Value="2014" />
                                                                <dx:ListEditItem Text="2013" Value="2013" />

                                                            </Items>
                                                            <ClientSideEvents TextChanged="function(){cp.PerformCallback('itemcat');}" />
                                                        </dx:ASPxComboBox></td><td>&nbsp;&nbsp;</td>
                                                        <td><dx:ASPxComboBox Caption="StartMonth" ID="startm" runat="server" Width="35px" ClientInstanceName="startmonth">
                                                            <Items>
                                                        <dx:ListEditItem Text="1" Value="1" Selected="true"/>
                                                        <dx:ListEditItem Text="2" Value="2" />
                                                        <dx:ListEditItem Text="3" Value="3" />
                                                        <dx:ListEditItem Text="4" Value="4" />
                                                        <dx:ListEditItem Text="5" Value="5" />
                                                        <dx:ListEditItem Text="6" Value="6" />
                                                        <dx:ListEditItem Text="7" Value="7" />
                                                        <dx:ListEditItem Text="8" Value="8" />
                                                        <dx:ListEditItem Text="9" Value="9" />
                                                        <dx:ListEditItem Text="10" Value="10" />
                                                        <dx:ListEditItem Text="11" Value="11" />
                                                        <dx:ListEditItem Text="12" Value="12" />
                                                    </Items>
                                                            
                                                        </dx:ASPxComboBox></td>
                                                    </tr>
                                                </table>
                                                
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Version">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridLookup Width="100px" ID="txtver" runat="server" AutoGenerateColumns="True" DataSourceID="versionsql" KeyFieldName="version" TextFormatString="{0}" ClientInstanceName="vpick">
                                                            <ClientSideEvents ValueChanged="function(){cp.PerformCallback();}" />
                                                                <%--<ClientSideEvents Validation="OnValidation"/>
                                                            <ValidationSettings Display="None" ValidateOnLeave="true" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True"/>
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>--%>
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                            </GridViewProperties>
                                                        </dx:ASPxGridLookup>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Item Category">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxGridLookup ID="txtitemcat" runat="server" ClientInstanceName="itemcat" DataSourceID="itemcat" KeyFieldName="ItemCategoryCode" TextFormatString="{0}" Width="170px">
                                                                <GridViewProperties>
                                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                </GridViewProperties>
                                                                <ClientSideEvents Validation="OnValidation" ValueChanged="function(){cp.PerformCallback('itemcat');}" />
                                                                <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField IsRequired="True" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="Pink">
                                                                </InvalidStyle>
                                                            </dx:ASPxGridLookup>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Agent">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridLookup ID="txtagent" runat="server" ClientInstanceName="txtagent" DataSourceID="employee" KeyFieldName="EmployeeID" TextFormatString="{0}" Width="170px">
                                                    <GridViewProperties>
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                    </GridViewProperties>
                                                </dx:ASPxGridLookup>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>

                            <dx:LayoutGroup Caption="" Height="150px" RowSpan="2" Width="240px"> 
                                <GroupBoxStyle>
                                    <Border BorderStyle="None" />
                                </GroupBoxStyle>
                                <Items>
                                    <dx:LayoutItem Caption="" ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxButton ID="locker" runat="server" Text="Lock" ClientInstanceName="lockbtn" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                                                    <ClientSideEvents Click="OnLock" />
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxLabel ID="applabel" runat="server" Text="" ClientInstanceName="applbl">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>

                            <dx:LayoutGroup ColCount="1" Caption="Retrieve Current Year">
                                <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="padding-left: 10px" >
                                                            <dx:ASPxButton ID="frmlayout1_E1" runat="server" Text="Retrieve" Width="200px" AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="OnUpdateClick2" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                                <SettingsItems HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Caption="Generate Year Sales" ColCount="2" Width="412px">
                                <Items>
                                    <dx:LayoutItem Caption="Forecast Methods">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxComboBox ID="frmlayout1_E2" runat="server">
                                                    <Items>
                                                        <dx:ListEditItem Text="Historical Average" Value="HisAve" Selected="true"/>
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem>
                                    </dx:EmptyLayoutItem>
                                    <dx:LayoutItem Caption="" ShowCaption="False" Width="120px">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxComboBox ID="yearfrom" runat="server" Caption="Year From" Width="60px">
                                                                <Items>
                                                                <dx:ListEditItem Text="2016" Value="2016"/>
                                                                <dx:ListEditItem Text="2015" Value="2015" />
                                                                <dx:ListEditItem Text="2014" Value="2014" />
                                                                <dx:ListEditItem Text="2013" Value="2013" Selected="true" />
                                                            </Items>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;</td>
                                                        <td>
                                                            <dx:ASPxComboBox Caption="To" ID="yearto" runat="server" Width="60px">
                                                            <Items>
                                                                <dx:ListEditItem Text="2016" Value="2016"/>
                                                                <dx:ListEditItem Text="2015" Value="2015"/>
                                                                <dx:ListEditItem Text="2014" Value="2014" Selected="true" />
                                                                <dx:ListEditItem Text="2013" Value="2013" />
                                                            </Items>
                                                </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem>
                                    </dx:EmptyLayoutItem>
                                    <dx:LayoutItem Caption="Percentage Inc/Dec">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="percentage" runat="server" Width="80px">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="%" Width="5px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                                        <td>
                                                            <dx:ASPxButton ID="frmlayout1_E13" runat="server" AutoPostBack="False" Text="Generate" UseSubmitBehavior="False" Width="120px">
                                                                <ClientSideEvents Click="OnUpdateClick" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            
                            <dx:TabbedLayoutGroup ColSpan="3">
                                <Items>
                                    <dx:LayoutGroup Caption="Forecast" ShowCaption="False">
                                                                                <Items>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <table>
                                                            <tr>
                                                                <td><dx:ASPxButton ID="btnrefresh" runat="server" Text="Refresh" UseSubmitBehavior="false" AutoPostBack="false" ClientInstanceName="btnref"
                                                                     ClientEnabled="false">
                                                                    <ClientSideEvents Click="function(){cp.PerformCallback('refresh')}" />
                                                                </dx:ASPxButton></td>
                                                                <td>&nbsp;&nbsp;<dx:ASPxButton ID="btnsave" runat="server" Text="Save" UseSubmitBehavior="false" AutoPostBack="false"
                                                                     ClientInstanceName="btnsave" ClientEnabled="false">
                                                                    <ClientSideEvents Click="function(){cp.PerformCallback('save')}"/>
                                                                </dx:ASPxButton></td>
                                                               <td style="padding-left: 10px" ><dx:ASPxButton ID="btnsavever" runat="server" Text="Save Version" UseSubmitBehavior="false" AutoPostBack="false"
                                                                    ClientEnabled="false" ClientInstanceName="btnsavever">
                                                                    <ClientSideEvents Click="function(){cp.PerformCallback('savever'); e.processOnServer = false;}" />
                                                                </dx:ASPxButton></td>
                                                                <td style="padding-left: 10px" >
                                                                    <dx:ASPxButton ID="ASPxButton3" ClientInstanceName="buttonImport" runat="server" ToolTip="Import and use forecast"
                                                                        Text="Import" UseSubmitBehavior="false" ClientEnabled="true" AutoPostBack="false">
                                                                        <ClientSideEvents Click="function(){ImportCSheet.Show();}" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td style="padding-left: 10px" >
                                                                    <dx:ASPxButton ID="btnexp" ClientInstanceName="buttonSaveAs" runat="server" ToolTip="Export and save"
                                                                        OnClick="buttonSaveAs_Click" Text="Export" UseSubmitBehavior="false" ClientEnabled="false"/>
                                                                </td>
                                                                <td style="padding-left: 10px" >
                                                                    <dx:ASPxCheckBox ID="cbgroup" runat="server" Text="Group By Biz. Account"></dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
       <dx:ASPxPivotGrid runat="server" ClientInstanceName="pivot" Width="1250px" Height="650px" ClientIDMode="AutoID" 
       ID="ASPxPivotGrid1" OnCustomCallback="pivotGrid_CustomCallback">
<ClientSideEvents CellClick="function(s, e) {
                        var rowname = e.RowFieldName;
	                    if ((!rowname.includes(fieldID) && !onlyone )||e.ColumnValueType != &#39;Value&#39; || e.RowValueType != &#39;Value&#39;  || e.Value == &#39;&#39;) return;     
                        if(e.DataIndex == 0){
                        if(applbl.GetText() == ''){
                        //console.log(e);
                        //console.log(s);
                        colIndex = e.ColumnIndex;
                        rowIndex = e.RowIndex;
                        editor.SetText(e.Value);
                        editPopup.ShowAtPos(e.HtmlEvent.pageX, e.HtmlEvent.pageY);
                            }
                        }
                    }" EndCallback="pivotEnd"></ClientSideEvents>

<OptionsView HorizontalScrollBarMode="Auto" HorizontalScrollingMode="Standard" VerticalScrollBarMode="Visible"></OptionsView>

<OptionsPager Visible="False"></OptionsPager>
</dx:ASPxPivotGrid>

        <dx:ASPxPopupControl runat="server" ClientInstanceName="editPopup" ShowHeader="False" ID="ASPxPopupControl1">
    <ContentCollection>
<dx:PopupControlContentControl runat="server"><dx:ASPxTextBox runat="server" Width="170px" ClientInstanceName="editor" ID="ASPxTextBox1"></dx:ASPxTextBox>

         <dx:ASPxButton runat="server" AutoPostBack="False" EnableClientSideAPI="True" Text="OK" ID="ASPxButton1">
<ClientSideEvents Click="function(s, e) {
	        pivot.PerformCallback('D|' + colIndex + '|' + rowIndex + '|' + editor.GetText());
	        editPopup.Hide();
        }"></ClientSideEvents>
</dx:ASPxButton>

        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ID="confirm" runat="server" Width="250px" Height="100px" HeaderText="Notice:"
        CloseAction="CloseButton" CloseOnEscape="True" Modal="True" ClientInstanceName="conf"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Saving a version of this forecast will make this current window to reload. Are you sure you want to continue?" />
                    <table>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                         <td><dx:ASPxButton ID="Ok" runat="server" Text="Ok" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                             <ClientSideEvents Click="function (s,e){ }" />
                             </dx:ASPxButton>
                         <td><dx:ASPxButton ID="ASPxButton2" runat="server" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" CausesValidation="false">
                             <ClientSideEvents Click="function (s,e){ conf.Hide(); }" />
                             </dx:ASPxButton> 
                        </tr>
                    </table> 
                </dx:PopupControlContentControl>
            </ContentCollection>
</dx:ASPxPopupControl>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>                          
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Customer-Item Category Worksheet" ShowCaption="False">
                                        <Items>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False"  Width="850px" KeyFieldName="Year;Customer;ItemCategoryCode"
                                                    ClientInstanceName="gv1" DataSourceID="ENT_FWorksheet" >
                                                    <ClientSideEvents BatchEditConfirmShowing="OnConfirm" />
                                                    <%--BatchEditRowValidating="Grid_BatchEditRowValidating"
                                                        BatchEditStartEditing="OnStartEditing" BatchEditEndEditing="OnEndEditing" />--%>
                                                    <ClientSideEvents Init="OnInitTrans" />
                                                    <SettingsPager Mode="ShowAllRecords">
                                                                                    </SettingsPager>
                                                            <SettingsEditing Mode="Batch" />
                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="300"  /> 
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="Year"
                                                            VisibleIndex="0" ReadOnly="true">
                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Customer" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CustomerName" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ItemCategoryCode" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="AverageSOQty" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:#,0.0000;(#,0.0000);}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="ForecastQty" ShowInCustomizationForm="True" VisibleIndex="5" ReadOnly="true">
                                                       <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:#,0.0000;(#,0.0000);}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="ForecastAmount" ShowInCustomizationForm="True" VisibleIndex="6" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="AverageSRP" ShowInCustomizationForm="True" VisibleIndex="7" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="ProjectedSRP" ShowInCustomizationForm="True" VisibleIndex="8">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                            <ClientSideEvents LostFocus="autocalculate" />
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TargetMarkup" ShowInCustomizationForm="True" VisibleIndex="9" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="ActualMarkup" ShowInCustomizationForm="True" VisibleIndex="10" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="NewMarkup" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TargetTerm" ShowInCustomizationForm="True" VisibleIndex="12" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="ActualTerm" ShowInCustomizationForm="True" VisibleIndex="13" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="NewTerm" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                            <ClientSideEvents LostFocus="autocalculate" />
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="CurrentCreditLimit" ShowInCustomizationForm="True" VisibleIndex="15" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="CurrentAR" ShowInCustomizationForm="True" VisibleIndex="16" ReadOnly="true">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="NewCreditLimit" ShowInCustomizationForm="True" VisibleIndex="17">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Note" ShowInCustomizationForm="True" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="PDC" ShowInCustomizationForm="True" VisibleIndex="19">
                                                        <PropertiesSpinEdit DisplayFormatString="g"
                                                                 SpinButtons-ShowIncrementButtons="false">
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TotalAR_PDC" ShowInCustomizationForm="True" VisibleIndex="20">
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NullDisplayText="0" ConvertEmptyStringToNull="False" NullText="0"  DisplayFormatString="{0:N}" Increment="0"
                                                                 >
                                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                        </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                                    </dx:LayoutGroup>

                                    <dx:LayoutGroup Caption="Summary" ShowCaption="False">
                                               <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <div class="gvInline">
                                                                <dx:ASPxGridView ID="gv2" runat="server" AutoGenerateColumns="False" Width="700" KeyFieldName="Row"
                                                                    SettingsBehavior-AllowSort="false" ClientInstanceName="gv2" Settings-ShowStatusBar="Hidden"
                                                                    ><ClientSideEvents Init="OnInitTrans" />
                                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="MName" Caption="Month" ReadOnly="true" ShowInCustomizationForm="True" VisibleIndex="11" UnboundType="Bound">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="DescName" Caption="Agent" ReadOnly="true"  ShowInCustomizationForm="True" VisibleIndex="12" UnboundType="Bound">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SFqty" PropertiesTextEdit-DisplayFormatString="{0:#,0.0000;(#,0.0000);}" Caption="Forecast" ReadOnly="true" ShowInCustomizationForm="True" VisibleIndex="13" UnboundType="Bound">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="CDqty" Caption="Inc(+)/Dec(-)" ReadOnly="true" ShowInCustomizationForm="True" VisibleIndex="14" UnboundType="Bound">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="CSqty" Caption="Current Sales Qty" PropertiesTextEdit-DisplayFormatString="{0:#,0.0000;(#,0.0000);}"  ReadOnly="true" ShowInCustomizationForm="True" VisibleIndex="15" UnboundType="Bound">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SFamt" Caption="FS Amt" ReadOnly="true" ShowInCustomizationForm="True" VisibleIndex="16" UnboundType="Bound">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Width="150px" FieldName="CSamt" ReadOnly="true" Caption="Current Sales Amount" ShowInCustomizationForm="True" VisibleIndex="17" UnboundType="Bound">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="300"  /> 
                                                                            <SettingsEditing Mode="Batch"/>
                                                                </dx:ASPxGridView>
                                                                </div>
                                                                <div class="gvInline">
                                                                <dx:ASPxGridView ID="gv3" runat="server" AutoGenerateColumns="True" Width="250" KeyFieldName="What" Settings-ShowStatusBar="Hidden"
                                                                    SettingsBehavior-AllowSort="false" ClientInstanceName="gv3" ><ClientSideEvents Init="OnInitTrans" />
                                                                    <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                                    <Columns>
                                                                    </Columns>
                                                                    <SettingsPager Mode="ShowAllRecords"/> 
                                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Auto" ColumnMinWidth="120" VerticalScrollableHeight="300"  /> 
                                                                            <SettingsEditing Mode="Batch" />
                                                                </dx:ASPxGridView>
                                                                </div>
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
    </form>

    <!--#region Region Datasource-->
    <%-- put all datasource codeblock here --%>
    <asp:ObjectDataSource ID="ENT_FWorksheet" runat="server" DataObjectTypeName="Entity.SalesForecast+ForecastWorksheet" SelectMethod="getdetail" UpdateMethod="UpdateForecastWorksheet" TypeName="Entity.SalesForecast+ForecastWorksheet">
        <SelectParameters>
            <asp:Parameter Name="yr" Type="String" />
            <asp:Parameter Name="itemcat" Type="String" />
            <asp:Parameter Name="version" Type="String" />
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource OnInit="Connection_Init" ID="itemcat" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ItemCategoryCode,Description from Masterfile.ItemCategory where ISNULL(IsInactive,'') != 1">
    </asp:SqlDataSource>
    <asp:SqlDataSource OnInit="Connection_Init" ID="versionsql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="">  
    </asp:SqlDataSource>
    <asp:SqlDataSource OnInit="Connection_Init" ID="employee" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select [EmployeeID],[EmployeeCode],[FirstName],[MiddleName],[LastName] from masterfile.BPEmployeeInfo where ISNULL(IsInactive,'') != 1"/>
    <asp:SqlDataSource OnInit="Connection_Init" ID="Masterfilebiz" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT * FROM Masterfile.[BPSupplierInfo]"></asp:SqlDataSource>
    <asp:SqlDataSource OnInit="Connection_Init" ID="sql" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="sp_temp_Forecast"
         SelectCommandType="StoredProcedure" UpdateCommand="UPDATE sales.forecast_temp_Save SET forecast = @Forecast WHERE Year = @Year and Month = @Month and Customer = @Customer and Col1 = @Col1">
        <SelectParameters>
            <asp:Parameter Name="year" Type="String"/>
            <asp:Parameter Name="itemcategory" Type="String" />
            <asp:SessionParameter Name="Session" SessionField="userid" Type="String" />
        </SelectParameters>    
    </asp:SqlDataSource>

    <asp:SqlDataSource OnInit="Connection_Init" ID="sql2" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="sp_Forecast"
         SelectCommandType="StoredProcedure" UpdateCommand="">
        <SelectParameters>
            <asp:Parameter Name="year" Type="String" />
            <asp:Parameter Name="itemcategory" Type="String" />
            <asp:Parameter Name="version" Type="String" />
            <asp:Parameter Name="agent" Type="String" />
            <asp:Parameter Name="startmonth" Type="String" />
            <asp:Parameter Name="groupby" Type="String" />
            <asp:SessionParameter Name="userid" SessionField="userid" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource OnInit="Connection_Init" ID="sql3" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="sp_temp_Forecast"
         SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="year" Type="String" />
            <asp:Parameter Name="itemcategory" Type="String" />
            <asp:SessionParameter Name="Session" SessionField="userid" Type="String" />
        </SelectParameters>    
    </asp:SqlDataSource>
    <asp:SqlDataSource OnInit="Connection_Init" ID="forecastsumm" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="usp_FS_summ"
         SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="useyear" Type="String" />
            <asp:Parameter Name="ItemCategory" Type="String" />
            <asp:Parameter Name="version" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    <asp:SqlDataSource OnInit="Connection_Init" ID="forecastsumm2" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="usp_FS_summ2"
         SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="useyear" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    <!--#endregion-->
    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="ASPxPivotGrid1" Visible="False" />
</body>
</html>

