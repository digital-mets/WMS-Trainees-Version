<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCheckUnRelease.aspx.cs" Inherits="GWL.frmCheckUnRelease" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../js/PerfSender.js" type="text/javascript"></script>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="Stylesheet" type="text/css" href="~/css/styles.css" />
    <title>Unrelease Checks</title>
    <!--#region Region CSS-->
    <style type="text/css">
        
        #form1 {
            height: 800px;
        }

        .Entry {
            padding: 20px;
            margin: 10px auto;
            background: #FFF;
        }

        .uppercase .dxeEditAreaSys {
            text-transform: uppercase;
        }

        .pnl-content {
            text-align: right;
        }
    </style>
    <!--#endregion-->
    <script>

        function OnUpdateClick(s, e) {
            cp.PerformCallback("Unrelease");
        }

        function OnConfirm(s, e) {
            if (e.requestTriggerID === "cp")
                e.cancel = true;
        }

        function OnEndCallback(s, e) {
            if (s.cp_success) {
                alert(s.cp_message);
                delete (s.cp_success);
                delete (s.cp_message);
            }
            else if (s.cp_message != null) {
                alert(s.cp_message);
                delete (s.cp_message);
            }
            if (s.cp_close) {
                delete (cp_close);
                window.close();
            }
        }

        function OnStartEditing (s, e)
        {
            if (e.focusedColumn.fieldName == "SelectCB") {
                e.cancel = false;
            }
            else
                e.cancel = true;
        }
    </script>
    <!--#endregion-->
</head>

<body style="height: 565px">
    <form id="form1" runat="server" class="Entry">
        <dx:ASPxPanel runat="server" FixedPositionOverlap="true" fixedposition="WindowTop" backcolor="#2A88AD" Height="30px">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="FormTitle" runat="server" Text="Unrelease Checks" Font-Bold="true" ForeColor="White" Font-Size="X-Large"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="850px" Height="565px" ClientInstanceName="cp" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="_FormLayout" runat="server" Height="565px" Width="850px" style="margin-left: -3px" ClientInstanceName="frmlayout">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        <Items>
                          <%--<!--#region Region Header --> --%>
                            <dx:TabbedLayoutGroup>
                                <Items>
                                    <dx:LayoutGroup Caption="General" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Voucher No.">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDocNumber" runat="server" ClientEnabled="false"
                                                                        Width="170px" CssClass="uppercase" >
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Voucher Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtDocDate" runat="server" ClientEnabled="false"
                                                                        Width="170px" CssClass="uppercase">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Supplier Code">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtSupplierCode" runat="server" ClientEnabled="false"
                                                                        Width="170px" CssClass="uppercase">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Supplier Name">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtSupplierName" runat="server" ClientEnabled="false"
                                                                        Width="170px" CssClass="uppercase">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="OR Number">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtORNumber" runat="server" ClientEnabled="false" 
                                                                        Width="170px" CssClass="uppercase">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="OR Date">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtORDate" runat="server" ClientEnabled="false" 
                                                                        Width="170px" CssClass="uppercase">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Payment Type">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtPaymentType" runat="server" ClientEnabled="false" 
                                                                        Width="170px" CssClass="uppercase">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Memo">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxMemo ID="txtMemo" runat="server" ClientEnabled="false" Width="170" Height="50px" />
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:EmptyLayoutItem>
                                            </dx:EmptyLayoutItem>
                                            <dx:LayoutItem Caption="Remarks">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxMemo ID="txtRemarks" runat="server" Width="170" Height="50px" />
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:EmptyLayoutItem Height="30px">
                                            </dx:EmptyLayoutItem>

                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxGridView ID="gv1" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv1" DataSourceID="sdsCheckDetail" KeyFieldName="RecordID" 
                                                            Width="832px" OnCellEditorInitialize="gv1_CellEditorInitialize" OnCommandButtonInitialize="gv1_CommandButtonInitialize" OnDataBound="gv1_DataBound">
                                                            <ClientSideEvents BatchEditStartEditing="OnStartEditing" BatchEditConfirmShowing="OnConfirm"/>
                                                            <SettingsPager Mode="ShowAllRecords" /><SettingsEditing Mode="Batch" />
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="RecordID" Visible="false" VisibleIndex="1" Width="0px" ReadOnly="true" />
                                                                <dx:GridViewDataTextColumn FieldName="Bank" Visible="true" VisibleIndex="4" Width="60px" />
                                                                <dx:GridViewDataTextColumn FieldName="Branch" Visible="true" VisibleIndex="7" Width="120px" />
                                                                <dx:GridViewDataTextColumn FieldName="PayeeName" Visible="true" VisibleIndex="10" Width="120px" />
                                                                <dx:GridViewDataDateColumn FieldName="CheckDate" Visible="true" VisibleIndex="13" Width="100px" />
                                                                <dx:GridViewDataTextColumn FieldName="CheckNumber" Visible="true" VisibleIndex="16" Width="120px" />
                                                                <dx:GridViewDataTextColumn FieldName="CheckAmount" Visible="true" VisibleIndex="19" Width="120px" >
                                                                    <CellStyle HorizontalAlign="Right" />
                                                                    <PropertiesTextEdit DisplayFormatString="#,0.00" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn FieldName="ReleasedDate" Visible="true" VisibleIndex="22" Width="100px" ReadOnly="true" />
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                           </dx:TabbedLayoutGroup>
                           <%-- <!--#endregion --> --%>
                        </Items>
                    </dx:ASPxFormLayout>
                    <dx:ASPxPanel id="BottomPanel" runat="server" fixedposition="WindowBottom" backcolor="#FFFFFF" Height="30px">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <div class="pnl-content">
                                <dx:ASPxCheckBox style="display: inline-block;" ID="glcheck" runat="server" ClientInstanceName="glcheck" TextAlign="Left" Text="Prevent auto-close upon update" Width="200px"></dx:ASPxCheckBox>
                                <dx:ASPxButton ID="updateBtn" runat="server" Text="Unrelease" AutoPostBack="False" CssClass="btn" ClientInstanceName="btn"
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

    <!--#region Region Datasource-->
    <asp:SqlDataSource ID="sdsCheckDetail" runat="server" 
        SelectCommand="SELECT 1 AS SelectCB, DocNumber, LineNumber, Bank, Branch, PayeeName, CheckDate, CheckNumber, CheckAmount, ReleasedDate, RecordID FROM Accounting.CheckVoucherDetail WHERE ISNULL(ClearedDate,'') = '' AND ISNULL(ReleasedDate,'') != '' AND DocNumber=@DocNumber" 
        OnInit="Connection_Init">
        <SelectParameters>
            <asp:Parameter Name="DocNumber" Type="String" DefaultValue="???"/>
        </SelectParameters>
    </asp:SqlDataSource>
    <!--#endregion-->
</body>
</html>


