<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmScrapRequest.aspx.cs" Inherits="GWL.frmScrapRequest" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <script src="../js/Production/ScrapRequest.js" type="text/javascript"></script>
    <script src="../js/keyboardNavi.js" type="text/javascript"></script>
    <title>Scrap Request</title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
        height: 400px; /*Change this whenever needed*/
        }

        .Entry {
        padding: 20px;
        margin: 10px auto;
        background: #FFF;
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
</head>

<body>
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>

    <form id="form1" runat="server">

        <dx:ASPxPanel ID="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel runat="server" ID="FormTitle" Text="Scrap Request" Font-Bold="true" ForeColor="White" Font-Size="X-Large"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>

        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="100%" Height="641px" ClientInstanceName="cp" OnCallback="cp_Callback" SettingsLoadingPanel-Enabled="true" SettingsLoadingPanel-Delay="3000" Images-LoadingPanel-Url="..\images\loadinggear.gif" Images-LoadingPanel-Height="30px" Styles-LoadingPanel-BackColor="Transparent" Styles-LoadingPanel-Border-BorderStyle="None" SettingsLoadingPanel-Text="" SettingsLoadingPanel-ShowImage="true" LoadingPanelDelay="3000" LoadingPanelText="" >
            <SettingsLoadingPanel Delay="3000" Text="" />
            <ClientSideEvents EndCallback="gridView_EndCallback" />

            <Images>
                <LoadingPanel Height="30px" Url="..\images\loadinggear.gif"></LoadingPanel>
            </Images>

            <LoadingPanelImage Height="30px" Url="..\images\loadinggear.gif"></LoadingPanelImage>

            <Styles>
                <LoadingPanel Border-BorderStyle="None" BackColor="Transparent"></LoadingPanel>
            </Styles>

            <LoadingPanelStyle Border-BorderStyle="None" BackColor="Transparent"></LoadingPanelStyle>

            <PanelCollection>
                <dx:PanelContent>
                    <dx:ASPxFormLayout ID="formlayout1" runat="server" Width="100%" ClientInstanceName="formlayout1" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="800">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600"/>
                        <Items>
                            <dx:TabbedLayoutGroup Width="100%">
                                <Items>
                                    <dx:LayoutGroup Width="100%" Caption="General" ColCount="2">
                                        <Items>
                                            <dx:LayoutGroup Width="100%" Caption="" ColCount="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="Document Number" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxTextBox runat="server" ID="txtDocnumber"></dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Document Date" ColSpan="1" VerticalAlign="Middle" Width="400px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxDateEdit runat="server" ID="dtpDocDate" OnLoad="Date_Load" OnInit ="dtpDocDate_Init" Width="100%">
                                                                    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="true" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Scrap Request Number" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxTextBox runat="server" ID="txtHSCRNumber"></dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Scrap Code Reference" ColSpan="1" VerticalAlign="Middle" Width="400px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxComboBox runat="server" ID="comboHScrapCodeRef" DataSourceID="sdsSCR" TextField="ScrapCode"></dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="OCN Request Number" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxTextBox runat="server" ID="txtHOCNRNumber"></dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Requesting Department Company" ColSpan="1" VerticalAlign="Middle" Width="400px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxComboBox runat="server" ID="comboHRDC" DataSourceID="sdsRDC" TextField="DepartmentCode"></dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Transaction Date" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxDateEdit runat="server" ID="dtpTransDate" OnLoad="Date_Load" Width="100%">
                                                                    <ClientSideEvents Validation="OnValidation" />
                                                                    <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="true" />
                                                                    </ValidationSettings>
                                                                    <InvalidStyle BackColor="Pink">
                                                                    </InvalidStyle>
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Warehouse Code" ColSpan="1" VerticalAlign="Middle" Width="400px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxComboBox runat="server" ID="comboHWarehouseCode" DataSourceID="sdsWHC" TextField="WarehouseCode"></dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Required Loading Time" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxTextBox runat="server" ID="txtHRLT"></dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    
                                                    <dx:LayoutItem Caption="Type of Shipment" ColSpan="1" VerticalAlign="Middle" Width="400px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxTextBox runat="server" ID="txtHTOS" ReadOnly="true"></dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                </Items>
                                            </dx:LayoutGroup>

                                            <dx:LayoutGroup Width="100%" Caption="Details" ColCount="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="" Width="100%" ColSpan="2">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer>
                                                                <dx:ASPxGridView runat="server" ID="gv1" KeyFieldName="DocNumber" DataSourceID="odsDetail">
                                                                    <ClientSideEvents BatchEditRowValidating="Grid_BatchEditRowValidating" Init="OnInitTrans" />
                                                                    

                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="60px">
                                                                        </dx:GridViewCommandColumn>

                                                                        <dx:GridViewDataComboBoxColumn Caption="Item Code" FieldName="ItemCode" ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                                                                            <PropertiesComboBox DataSourceID="ItemCodeDataSource" TextField="ItemCode" ValueField="ItemCode" ValueType="System.Int32">
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="Item Description" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="120px">
                                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="Batch Number" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="120px">
                                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataComboBoxColumn Caption="Process Steps" FieldName="ProcessSteps" ShowInCustomizationForm="True" VisibleIndex="2" Width="110px">
                                                                            <PropertiesComboBox DataSourceID="ProcessStepsDataSource" TextField="ProcessSteps" ValueField="ProcessSteps" ValueType="System.Int32">
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="Quantity" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataComboBoxColumn Caption="UOM" FieldName="UOM" ShowInCustomizationForm="True" VisibleIndex="2" Width="70px">
                                                                            <PropertiesComboBox DataSourceID="UOMDataSource" TextField="UOM" ValueField="UOM" ValueType="System.Int32">
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="Special Handling Instruction" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="180px">
                                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="Total Quantity" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="Remarks" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                                            </PropertiesTextEdit>
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

                                    <dx:LayoutGroup Width="100%" Caption="Audit Trail" ColCount="2">
                                        <Items>
                                            <dx:LayoutGroup Width="100%" Caption="" ColCount="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="Added By" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHAddedBy" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Added Date" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHAddedDate" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Last Edited By" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHLastEditedBy" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Last Edited Date" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHLastEditedDate" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Cancelled By" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHCancelledBy" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Cancelled Date" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHCancelledDate" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Submitted By" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHSubmittedBy" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                    <dx:LayoutItem Caption="Submitted Date" ColSpan="1" VerticalAlign="Middle" Width="350px">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxTextBox ID="txtHSubmittedDate" runat="server" ReadOnly="True" Width="170px">
                                                                <ClientSideEvents Validation="function (s,e)
                                                                    {
                                                                     OnValidation = true;
                                                                    }" />
                                                                </dx:ASPxTextBox>
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

    </form>

    <!-- Detail Data Source -->
    <asp:ObjectDataSource ID="odsDetail" runat="server" DataObjectTypeName="Entity.ScrapRequest+ScrapRequestDetail" SelectMethod="getdetail" UpdateMethod="UpdateScrapRequestDetail" TypeName="Entity.ScrapRequest+ScrapRequestDetail" DeleteMethod="DeleteScrapRequestDetail" InsertMethod="AddScrapRequestDetail">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String"/>
            <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String"/>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select * from Production.ScrapRequestDetail where DocNumber is null"   OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <!-- Scrap Code Reference Combobox Data Source -->
    <asp:SqlDataSource ID="sdsSCR" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select ScrapCode from Masterfile.Scrap"   OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <!-- Requesting Department Company Combobox Data Source -->
    <asp:SqlDataSource ID="sdsRDC" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select DepartmentCode from Masterfile.Department"   OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <!-- Warehouse Combobox Data Source -->
    <asp:SqlDataSource ID="sdsWHC" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select WarehouseCode from Masterfile.TollWarehouse"   OnInit = "Connection_Init">
    </asp:SqlDataSource>

</body>
</html>
