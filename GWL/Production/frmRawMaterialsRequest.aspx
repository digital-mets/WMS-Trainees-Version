<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRawMaterialsRequest.aspx.cs" Inherits="GWL.frmRawMaterialsRequest" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <script src="../js/Production/RawMaterialsRequest.js" type="text/javascript"></script>
    <script src="../js/keyboardNavi.js" type="text/javascript"></script>
    <title>Raw Materials Request</title>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" /><%--Link to global stylesheet--%>
    <!--#region Region CSS-->
    <style type="text/css">
        /*Stylesheet for separate forms (All those which has comments are the ones that can only be changed)*/
        #form1 {
        height: 400px; /*Change this whenever needed*/
        }

        .formLayout {
            max-width: 1300px;
            margin: auto;
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
<body style="height: 600px;" onload="onload()">
    
    <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
    </dx:ASPxGlobalEvents>    
    
    <form id="form1" runat="server" class="entry">

    <dx:ASPxPanel ID="toppanel" runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxLabel runat="server" ID="FormTitle" Text="Raw Materials Request" Font-Bold="true" ForeColor="White" Font-Size="X-Large"></dx:ASPxLabel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
    
    <dx:ASPxCallbackPanel ID="cp" runat="server" Width="820px" Height="641px" ClientInstanceName="cp" OnCallback="cp_Callback" SettingsLoadingPanel-Enabled="true" SettingsLoadingPanel-Delay="3000" Images-LoadingPanel-Url="..\images\loadinggear.gif" Images-LoadingPanel-Height="30px" Styles-LoadingPanel-BackColor="Transparent" Styles-LoadingPanel-Border-BorderStyle="None" SettingsLoadingPanel-Text="" SettingsLoadingPanel-ShowImage="true" LoadingPanelDelay="3000" LoadingPanelText="" >
        <SettingsLoadingPanel Delay="3000" Text=""></SettingsLoadingPanel>

        <ClientSideEvents EndCallback="gridView_EndCallback"></ClientSideEvents>

        <Images>
        <LoadingPanel Height="30px" Url="..\images\loadinggear.gif"></LoadingPanel>
        </Images>

        <LoadingPanelImage Height="30px" Url="..\images\loadinggear.gif"></LoadingPanelImage>

        <Styles>
        <LoadingPanel Border-BorderStyle="None" BackColor="Transparent"></LoadingPanel>
        </Styles>

        <LoadingPanelStyle Border-BorderStyle="None" BackColor="Transparent"></LoadingPanelStyle>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="formLayout" runat="server" Height="400px" Width="800px" style="margin-left: -20px" SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsAdaptivity-SwitchToSingleColumnAtWindowInnerWidth="800">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                            <dx:TabbedLayoutGroup Width="100%">
                                <Items>
                                    <dx:LayoutGroup Caption="General" ColCount="2" Width="100%">
                                        <Items>

                                            <dx:LayoutItem Caption="Document Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxDateEdit ID="dtpDocDate" runat="server" OnLoad="Date_Load" OnInit ="dtpDocDate_Init"  Width="170px">
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
                                            
                                            <dx:LayoutItem Caption="Raw Materials Request Number">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHRMRNumber" ClientInstanceName="txtRMRNumber" runat="server" Width="170px"  AutoCompleteType="Disabled" ClientEnabled="False">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Raw Materials Request Reference Details">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHRMRRDetails" ClientInstanceName="txtSRNumber" runat="server" Width="170px"  AutoCompleteType="Disabled">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Requesting Department Company">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHRequestingDeptCompany" ClientInstanceName="txtRequestingDeptCompany" runat="server" Width="170px"  AutoCompleteType="Disabled" ClientEnabled="False">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Warehouse Code">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup runat="server" DataSourceID="sdsWarehouse" Width="170px" ID="glHWarehouseCode" >
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />

                                                                <SettingsLoadingPanel Delay="3000" Text=""></SettingsLoadingPanel>
                                                            </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="WarehouseCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>

                                                            <GridViewImages>
                                                            <LoadingPanel Height="30px" Url="..\images\loadinggear.gif"></LoadingPanel>
                                                            </GridViewImages>

                                                            <GridViewStyles>
                                                            <LoadingPanel Border-BorderColor="Transparent" BackColor="Transparent"></LoadingPanel>
                                                            </GridViewStyles>

                                                            <ClientSideEvents  Validation="OnValidation" ValueChanged="function(s, e) {
                                                            cp.PerformCallback('Supplier');
                                                            e.processOnServer = false;
                                                            }"
                                                            />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Customer Code">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridLookup runat="server" DataSourceID="sdsCustomer" Width="170px" ID="glHCustomerCode">
                                                            <GridViewProperties>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                                <Settings ShowFilterRow="True" />

                                                                <SettingsLoadingPanel Delay="3000" Text=""></SettingsLoadingPanel>
                                                            </GridViewProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="BizPartnerCode" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>

                                                            <GridViewImages>
                                                            <LoadingPanel Height="30px" Url="..\images\loadinggear.gif"></LoadingPanel>
                                                            </GridViewImages>

                                                            <GridViewStyles>
                                                            <LoadingPanel Border-BorderColor="Transparent" BackColor="Transparent"></LoadingPanel>
                                                            </GridViewStyles>

                                                            <ClientSideEvents  Validation="OnValidation" ValueChanged="function(s, e) {
                                                            cp.PerformCallback('Supplier');
                                                            e.processOnServer = false;
                                                            }"
                                                            />
                                                            <ValidationSettings Display="None" ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="Pink">
                                                            </InvalidStyle>
                                                        </dx:ASPxGridLookup>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Consignee">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHConsignee" runat="server" OnLoad="TextboxLoad" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Consignee Address">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHConsigneeAddress" runat="server" OnLoad="TextboxLoad" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Required Loading Time">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHRequiredLoadingTime" runat="server" OnLoad="TextboxLoad" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Type of Shipment">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHShipmentType" runat="server" OnLoad="TextboxLoad" Width="170px" ClientEnabled="false">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:EmptyLayoutItem>
                                            </dx:EmptyLayoutItem>
                                            <dx:EmptyLayoutItem>
                                            </dx:EmptyLayoutItem>
                                            <dx:LayoutGroup Caption="Material Request Detail" Name="RMRDetail">
                                                <Items>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                
                                                                <dx:ASPxGridView ID="gridRMRDetail" runat="server" Width="770px" >
                                                                    
                                                                    
                                                                    <ClientSideEvents BatchEditRowValidating="Grid_BatchEditRowValidating" Init="OnInitTrans" />
                                                                    <SettingsPager PageSize="5" Visible="False">
                                                                    </SettingsPager>
                                                                    <Settings HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" VerticalScrollBarMode="Visible" />

                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="2" Width="60px">
                                                                            
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

                                                                        <dx:GridViewDataTextColumn FieldName="OCN Number" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="120px">
                                                                            <PropertiesTextEdit ConvertEmptyStringToNull="False">
                                                                            </PropertiesTextEdit>
                                                                            
                                                                        </dx:GridViewDataTextColumn>

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

                                                                        <dx:GridViewDataTextColumn FieldName="Remarks" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="180px">
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


                                    <dx:LayoutGroup Caption="Audit Trail" ColCount="2" Width="100%">
                                        <Items>
                                            <dx:LayoutItem Caption="Added By">
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
                                                        <dx:ASPxTextBox ID="txtHLastEditedBy" runat="server" Width="170px"  ReadOnly="true" >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem> 

                                            <dx:LayoutItem Caption="Last Edited Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHLastEditedDate" runat="server" Width="170px" ReadOnly="True" >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Cancelled By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHCancelledBy" runat="server" Width="170px"  ReadOnly="true" >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem> 

                                            <dx:LayoutItem Caption="Cancelled Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHCancelledDate" runat="server" Width="170px" ReadOnly="True" >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Submitted By">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedBy" ClientInstanceName="txtHSubmittedBy" runat="server" Width="170px"  ReadOnly="true"  >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            
                                            <dx:LayoutItem Caption="Submitted Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtHSubmittedDate" runat="server" Width="170px"  ReadOnly="true">
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
                                <dx:ASPxButton ID="updateBtn" runat="server" Text="Update" AutoPostBack="False" CssClass="btn" ClientInstanceName="btn"
                                    UseSubmitBehavior="false" CausesValidation="true">
                                    <ClientSideEvents Click="OnUpdateClick" />
                                </dx:ASPxButton>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>

                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Cloning..." Modal="true"
                        ClientInstanceName="loader" ContainerElementID="gv1">
                        <LoadingDivStyle Opacity="0"></LoadingDivStyle>
                    </dx:ASPxLoadingPanel>

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

    <asp:ObjectDataSource ID="odsHeader" runat="server" DataObjectTypeName="Entity.Collection" InsertMethod="InsertData" SelectMethod="getdata" TypeName="Entity.Collection" UpdateMethod="UpdateData" DeleteMethod="DeleteData">
        <SelectParameters>
            <asp:QueryStringParameter Name="DocNumber" QueryStringField="docnumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="odsDetail" runat="server" DataObjectTypeName="Entity.RawMaterialsRequest+RMRDetail" SelectMethod="getdetail" UpdateMethod="UpdateRMRDetail" TypeName="Entity.RawMaterialsRequest+UpdateRMRDetail" DeleteMethod="DeleteUpdateRMRDetail" InsertMethod="AddUpdateRMRDetail">
    <SelectParameters>
        <asp:QueryStringParameter Name="MRNumber" QueryStringField="mrnumber" Type="String"/>
        <asp:SessionParameter Name="Conn" SessionField="ConnString" Type="String"/>
    </SelectParameters>
    </asp:ObjectDataSource>

    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="select * from Production.UpdateRMRDetail where SRNumber is null"   OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsWarehouse" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT WarehouseCode, [Description] FROM Masterfile.[Warehouse] WHERE ISNULL([IsInactive],0) = 0" OnInit = "Connection_Init">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:GEARS-METSITConnectionString %>" SelectCommand="SELECT BizPartnerCode, [Name] FROM Masterfile.[BPCustomerInfo] WHERE ISNULL([IsInactive],0) = 0" OnInit = "Connection_Init">
    </asp:SqlDataSource>


</body>
</html>
