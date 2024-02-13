<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCapacityPlanning.aspx.cs" Inherits="GWL.frmCapacityPlanning" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Capacity Planning</title>
    
    <link rel="stylesheet" href="../css/form.css" /><%--Link to global stylesheet--%>
    <link rel="stylesheet" href="../css/bootstrap.min.css" /><%--Link to global stylesheet--%>
    <link rel="Stylesheet" type="text/css" href="../css/bootstrap.min.css" /><%--Link to global stylesheet--%>
    <link rel="Stylesheet" type="text/css" href="~/css/fontawesome.min.css" /><%--Link to global stylesheet--%>

    <script type='text/javascript' src="../js/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/moment-with-locales.js"></script>

    <script src="../js/CapacityPlanning.js" type="text/javascript"></script> <%-- Capacity Planning JS --%>

    <style>
        .dxflHALSys.dxflVATSys {
            padding-right: 0 !important;
        }
        .categoryTable {
            width: 100%;
        }
        .categoryTable .imageCell {
            padding: 2px;
        }
        .categoryTable .textCell {
            padding-left: 20px;
            width: 100%;
        }
        .textCell .label {
            color: #969696;
            text-transform: uppercase;
        }
        .textCell .description {
            font-size: 13px;
            width: 230px;
        }
        .captionCell {
            min-width: 120px !important;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <%-- Top Panel --%>
        <dx:ASPxPanel id="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel runat="server" Text="Capacity Planning" Font-Bold="true" ForeColor="White"  Font-Size="X-Large"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>

        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="820px" Height="641px" ClientInstanceName="cp" OnCallback="cp_Callback" SettingsLoadingPanel-ImagePosition="Left" SettingsLoadingPanel-Enabled="False">

            <ClientSideEvents EndCallback=""></ClientSideEvents>

            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True"> 
                    
                    <%-- Form Layout Start --%>
                    <dx:ASPxFormLayout ID="frmLayoutMachineCP" runat="server" Height="300px" Width="1280px" style="margin-left: -3px">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <%-- Machine Tab Start --%>
                                    <dx:LayoutGroup Caption="Machine">
                                        <Items>
                                            <dx:LayoutGroup Caption="Info">
                                                <Items>
                                                    <%-- Year Field --%>
                                                    <dx:LayoutItem Caption="Year:" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxDateEdit ID="MachineCPYear" runat="server" Width="170px" DisplayFormatString="yyyy" EditFormatString="yyyy" OnInit="de_Init" ClientInstanceName="MachineCPYear">
                                                                    <ClientSideEvents DropDown="OnDropDown" Init="OnInit" /> 
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <%-- Work Week Field --%>
                                                    <dx:LayoutItem Caption="Work Week" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="txtWorkWeek" ClientIDMode="Static" ClientInstanceName="txtWorkWeek" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" DisplayFormatString="G" MaxLength="2">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <%-- Day No. Field --%>
                                                    <dx:LayoutItem Caption="Day No." RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="txtDayNo" ClientIDMode="Static" ClientInstanceName="txtDayNo" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" DisplayFormatString="G">
                                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                            <%-- Details Field --%>
                                            <dx:LayoutGroup Caption="Details">
                                                <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <div class="container-fluid" id="ManchineCPTable">
                                                                    <dx:ASPxGridView ID="machineCP" runat="server" AutoGenerateColumns="False" KeyFieldName="DocNumber" Width="100%" ClientInstanceName="machineCP" OnBatchUpdate="BatchUpdate" 
                                                                        OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv_CustomButtonInitialize" DataSourceID="sdsmachineCP">
                                                                        <ClientSideEvents BatchEditConfirmShowing="OnConfirm" BatchEditStartEditing="MachineCP_OnStartEditing" CustomButtonClick="OnCustomClick"/>
                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                            </SettingsPager>
                                                                            <SettingsEditing Mode="Batch">
                                                                            </SettingsEditing>
                                                                            <SettingsBehavior AllowSort="False" />
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
                                                                    <Styles>
                                                                        <StatusBar CssClass="statusBar">
                                                                        </StatusBar>
                                                                    </Styles>
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn FieldName="DocNumber" Caption="SKU" />
                                                                            <dx:GridViewDataColumn FieldName="SupplierCode" Caption="No. of Batch" />
                                                                            <dx:GridViewDataColumn FieldName="SupplierName" Caption="Yielded  Batch Wt." />
                                                                            <dx:GridViewDataTextColumn FieldName="Type" Caption="Sequence for the Day" ShowInCustomizationForm="True" ReadOnly="false">
                                                                                <EditFormSettings Visible="true" />
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <Templates>
                                                                            <DetailRow>
                                                                                <dx:ASPxGridView ID="detailGrid" runat="server" DataSourceID="sdsmachineCP" KeyFieldName="DocNumber" Width="100%" EnablePagingGestures="False">
                                                                                    <Columns>
                                                                                        <dx:GridViewDataColumn FieldName="DocNumber" Caption="Process Sequence" VisibleIndex="1" />
                                                                                        <dx:GridViewDataColumn FieldName="SupplierName" Caption="Process Code" VisibleIndex="2" />
                                                                                        <dx:GridViewDataColumn FieldName="Type" Caption="Type of Machine" VisibleIndex="3" />
                                                                                        <dx:GridViewDataColumn FieldName="ReferenceDocnumber" Caption="Available Machine" VisibleIndex="4" Name="Quantity" />
                                                                                        <dx:GridViewDataColumn FieldName="Type" Caption="Machine Details" VisibleIndex="5" />
                                                                                        <dx:GridViewDataColumn FieldName="Type" Caption="Capacity/hr per Batch" VisibleIndex="5" />
                                                                                    </Columns>
                                                                                    <%--<Settings ShowFooter="True" />--%>
                                                                                    <SettingsPager EnableAdaptivity="true" />
                                                                                    <Styles Header-Wrap="True"/>
                                                                                </dx:ASPxGridView>
                                                                            </DetailRow>
                                                                        </Templates>
                                                                        <SettingsDetail ShowDetailRow="true" />
                                                                    </dx:ASPxGridView>
                                                                </div>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:LayoutGroup>  
                                    <%-- Machine Tab End --%>

                                    <%-- Manpower Tab Start --%>
                                    <dx:LayoutGroup Caption="Manpower">
                                        <Items>
                                            <%-- Specific Manpower Field --%>
                                            <dx:LayoutGroup Caption="Specific Manpower">
                                                <Items>
                                                    <%-- Header Field --%>
                                                    <dx:LayoutGroup Caption="Info" ColCount="2">
                                                        <Items>
                                                            <%-- Year Field --%>
                                                            <dx:LayoutItem Caption="Year:" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="170px" DisplayFormatString="yyyy" EditFormatString="yyyy" OnInit="de_Init" ClientInstanceName="MachineCPYear">
                                                                            <ClientSideEvents DropDown="OnDropDown" Init="OnInit" /> 
                                                                        </dx:ASPxDateEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%-- Specific Process Field --%>
                                                            <dx:LayoutItem Caption="Specific Process:">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit3" ClientIDMode="Static" ClientInstanceName="txtWorkWeek" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" DisplayFormatString="G" MaxLength="3" ReadOnly="true">
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                        </dx:ASPxSpinEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%-- Work Week Field --%>
                                                            <dx:LayoutItem Caption="Work Week" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit1" ClientIDMode="Static" ClientInstanceName="txtWorkWeek" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" DisplayFormatString="G" MaxLength="2">
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                        </dx:ASPxSpinEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%-- General Manpower Field --%>
                                                            <dx:LayoutItem Caption="General Manpower:">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit4" ClientIDMode="Static" ClientInstanceName="txtWorkWeek" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" DisplayFormatString="G" MaxLength="3" ReadOnly="true">
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                        </dx:ASPxSpinEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%-- Day No. Field --%>
                                                            <dx:LayoutItem Caption="Day No." RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit2" ClientIDMode="Static" ClientInstanceName="txtDayNo" runat="server" Width="170px" SpinButtons-ShowIncrementButtons="False" DisplayFormatString="G">
                                                                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                                                        </dx:ASPxSpinEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%-- SKU Field --%>
                                                            <dx:LayoutItem Caption="SKU">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="Sku" runat="server"  Width="170px" AutoCompleteType="Disabled" >
                                                                        </dx:ASPxTextBox>
                                                                        <dx:ASPxHiddenField runat="server" ID="hidcon"></dx:ASPxHiddenField>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <%-- Details Field --%>
                                                    <dx:LayoutGroup Caption="Details">
                                                        <Items>
                                                            <dx:LayoutItem Caption="">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <div class="container-fluid" id="SpecificManpowerCPTable">
                                                                            <%-- Detail 1 --%>
                                                                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="DocNumber" Width="100%" ClientInstanceName="machineCP" OnBatchUpdate="BatchUpdate" 
                                                                                OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv_CustomButtonInitialize" DataSourceID="sdsmachineCP">
                                                                                <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditRowValidating=""
                                                                                BatchEditEndEditing="" BatchEditStartEditing="OnCustomClick" CustomButtonClick="OnCustomClick" />
                                                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                                                <SettingsEditing Mode="Batch" ></SettingsEditing>
                                                                                <SettingsBehavior AllowSort="False" />
                                                                                <SettingsCommandButton>
                                                                                    <NewButton><Image IconID="actions_addfile_16x16"></Image></NewButton>
                                                                                    <EditButton><Image IconID="actions_addfile_16x16"></Image></EditButton>
                                                                                    <DeleteButton><Image IconID="actions_cancel_16x16"></Image></DeleteButton>
                                                                                </SettingsCommandButton>
                                                                                <Styles>
                                                                                    <StatusBar CssClass="statusBar"></StatusBar>
                                                                                </Styles>
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="DocNumber" Caption="SKU" />
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <DetailRow>
                                                                                        <%-- Detail 2 --%>
                                                                                        <dx:ASPxGridView ID="detailGrid" runat="server" DataSourceID="sdsmachineCP" KeyFieldName="DocNumber" Width="100%" EnablePagingGestures="False">
                                                                                            <Columns>
                                                                                                <dx:GridViewDataColumn FieldName="DocNumber" Caption="Process Code" VisibleIndex="1" />
                                                                                            </Columns>
                                                                                             <Templates>
                                                                                                <DetailRow>
                                                                                                    <%-- Detail 3 --%>
                                                                                                    <dx:ASPxGridView ID="detailGrid1" runat="server" DataSourceID="sdsmachineCP" KeyFieldName="DocNumber" Width="100%" EnablePagingGestures="False">
                                                                                                        <Columns>
                                                                                                            <dx:GridViewDataColumn FieldName="DocNumber" Caption="Position" VisibleIndex="1" />
                                                                                                            <dx:GridViewDataColumn FieldName="DocNumber" Caption="# of Manpower" VisibleIndex="1" />
                                                                                                        </Columns>
                                                                                                        <%--<Settings ShowFooter="True" />--%>
                                                                                                        <SettingsPager EnableAdaptivity="true" />
                                                                                                        <Styles Header-Wrap="True"/>
                                                                                                    </dx:ASPxGridView>
                                                                                                </DetailRow>
                                                                                            </Templates>
                                                                                            <SettingsDetail ShowDetailRow="true" />
                                                                                            <%--<Settings ShowFooter="True" />--%>
                                                                                            <SettingsPager EnableAdaptivity="true" />
                                                                                            <Styles Header-Wrap="True"/>
                                                                                        </dx:ASPxGridView>
                                                                                    </DetailRow>
                                                                                </Templates>
                                                                                <SettingsDetail ShowDetailRow="true" />
                                                                            </dx:ASPxGridView>
                                                                        </div>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:LayoutGroup>
                                            <%-- General Manpower Field --%>
                                            <dx:LayoutGroup Caption="General Manpower">
                                                <Items>
                                                    <%-- Details Field --%>
                                                    <dx:LayoutGroup Caption="Details">
                                                        <Items>
                                                            <dx:LayoutItem Caption="">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <div class="container-fluid" id="GeneralManpowerCPTable">
                                                                            <%-- Detail 1 --%>
                                                                            <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" KeyFieldName="DocNumber" Width="100%" ClientInstanceName="machineCP" OnBatchUpdate="BatchUpdate" 
                                                                                OnCommandButtonInitialize="gv_CommandButtonInitialize" OnCustomButtonInitialize="gv_CustomButtonInitialize" DataSourceID="sdsmachineCP">
                                                                                <ClientSideEvents Init="OnInitTrans" BatchEditConfirmShowing="OnConfirm" BatchEditRowValidating=""
                                                                                BatchEditEndEditing="" BatchEditStartEditing="OnCustomClick" CustomButtonClick="OnCustomClick" />
                                                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                                                <SettingsEditing Mode="Batch" ></SettingsEditing>
                                                                                <SettingsBehavior AllowSort="False" />
                                                                                <SettingsCommandButton>
                                                                                    <NewButton><Image IconID="actions_addfile_16x16"></Image></NewButton>
                                                                                    <EditButton><Image IconID="actions_addfile_16x16"></Image></EditButton>
                                                                                    <DeleteButton><Image IconID="actions_cancel_16x16"></Image></DeleteButton>
                                                                                </SettingsCommandButton>
                                                                                <Styles>
                                                                                    <StatusBar CssClass="statusBar"></StatusBar>
                                                                                </Styles>
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="DocNumber" Caption="Process" />
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <DetailRow>
                                                                                        <%-- Detail 2 --%>
                                                                                        <dx:ASPxGridView ID="detailGrid" runat="server" DataSourceID="sdsmachineCP" KeyFieldName="DocNumber" Width="100%" EnablePagingGestures="False">
                                                                                            <Columns>
                                                                                                <dx:GridViewDataColumn FieldName="DocNumber" Caption="Line Assignment" VisibleIndex="1" />
                                                                                            </Columns>
                                                                                             <Templates>
                                                                                                <DetailRow>
                                                                                                    <%-- Detail 3 --%>
                                                                                                    <dx:ASPxGridView ID="detailGrid1" runat="server" DataSourceID="sdsmachineCP" KeyFieldName="DocNumber" Width="100%" EnablePagingGestures="False">
                                                                                                        <Columns>
                                                                                                            <dx:GridViewDataColumn FieldName="DocNumber" Caption="Position" VisibleIndex="1" />
                                                                                                            <dx:GridViewDataColumn FieldName="DocNumber" Caption="# of Manpower" VisibleIndex="1" />
                                                                                                        </Columns>
                                                                                                        <%--<Settings ShowFooter="True" />--%>
                                                                                                        <SettingsPager EnableAdaptivity="true" />
                                                                                                        <Styles Header-Wrap="True"/>
                                                                                                    </dx:ASPxGridView>
                                                                                                </DetailRow>
                                                                                            </Templates>
                                                                                            <SettingsDetail ShowDetailRow="true" />
                                                                                            <%--<Settings ShowFooter="True" />--%>
                                                                                            <SettingsPager EnableAdaptivity="true" />
                                                                                            <Styles Header-Wrap="True"/>
                                                                                        </dx:ASPxGridView>
                                                                                    </DetailRow>
                                                                                </Templates>
                                                                                <SettingsDetail ShowDetailRow="true" />
                                                                            </dx:ASPxGridView>
                                                                        </div>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:LayoutGroup>  
                                    <%-- Manpower Tab End --%>

                                    <%-- Audit Trail Tab Start --%>
                                    <dx:LayoutGroup Caption="Audit Trail" ColCount="2">
                                        <Items>
                                            <%-- Added By Field --%>
                                            <dx:LayoutItem Caption="Added By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <%-- Added Date Field --%>
                                            <dx:LayoutItem Caption="Added Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtAddedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <%-- Last Edited By Field --%>
                                            <dx:LayoutItem Caption="Last Edited By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <%-- Last Edited Date Field --%>
                                            <dx:LayoutItem Caption="Last Edited Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtLastEditedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <%-- Submitted By Field --%>
                                            <dx:LayoutItem Caption="Submitted By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtSubmittedBy" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <%-- Submitted Date Field --%>
                                            <dx:LayoutItem Caption="Submitted Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtSubmittedDate" runat="server" Width="170px" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <%-- Audit Trail Tab END --%>
                                </Items>
                            </dx:TabbedLayoutGroup>
                        </Items>
                    </dx:ASPxFormLayout>
                    <%-- Form Layout END --%>

                    <%-- Bottom Panel --%>
                    <dx:ASPxPanel id="BottomPanel" runat="server" fixedposition="WindowBottom" backcolor="#FFFFFF">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <div class="pnl-content"">
                                <dx:ASPxCheckBox style="display: inline-block;" ID="glcheck" runat="server" ClientInstanceName="glcheck" TextAlign="Left" Text="Prevent auto-close upon update" Width="200px"></dx:ASPxCheckBox>
                                <dx:ASPxButton ID="updateBtn" runat="server" Text="Update" AutoPostBack="False" CssClass="btn btn-primary" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="" />
                                </dx:ASPxButton>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel> 

        <asp:SqlDataSource ID="sdsmachineCP" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" OnInit = "Connection_Init" SelectCommand="SELECT * FROM Accounting.APMemo">
        </asp:SqlDataSource>
    </form>
</body>
</html>
