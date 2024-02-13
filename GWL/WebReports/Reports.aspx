<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="GWL.Reports" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function cp_endcallback(s,e) {
            s.cp_connection
            window.open("./debugRepViewer.aspx?val=~" + s.cp_report + '&transtype=' + s.cp_transtype + '&docnumber='+ '' +'&connection=' + s.cp_connection, '_blank');
        }

        function cbvalchanged(s, e) {
            if (cb1.GetValue() != null) {
                cb2.SetValue(null);
            }
        }

        function cbvalchanged2(s, e) {
            if (cb2.GetValue() != null) {
                cb1.SetValue(null);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxCallbackPanel ID="cp" runat="server" Width="200px" OnCallback="cp_Callback">
            <ClientSideEvents EndCallback="cp_endcallback" />
            <PanelCollection>
                <dx:PanelContent>
                    <dx:ASPxLabel runat="server" Text ="Connection"></dx:ASPxLabel>
                    <dx:ASPxComboBox ID="connection" runat="server" ValueType="System.String" OnLoad="connection_Load"></dx:ASPxComboBox>
                    <dx:ASPxLabel runat="server" Text ="Reports"></dx:ASPxLabel>
                    <dx:ASPxComboBox ID="cb1" runat="server" ValueType="System.String" OnLoad="cb1_Load" ClientInstanceName="cb1">
                        <ClientSideEvents ValueChanged="cbvalchanged" />
                    </dx:ASPxComboBox>
                    <dx:ASPxLabel runat="server" Text ="Printout"></dx:ASPxLabel>
                    <dx:ASPxComboBox ID="cb2" runat="server" ValueType="System.String" OnLoad="cb2_Load" ClientInstanceName="cb2">
                        <ClientSideEvents ValueChanged="cbvalchanged2" />
                    </dx:ASPxComboBox>
                    <dx:ASPxButton ID="submit" runat="server" Text="Submit" UseSubmitBehavior="false" AutoPostBack="false">
                        <ClientSideEvents Click="function(){cp.PerformCallback();}" />
                    </dx:ASPxButton>
                    <dx:ASPxHyperLink runat="server" Text="click me" NavigateUrl="ReportViewer.aspx?val=~GEARS_Reports.R_RemainingInvAmt2&param=PView&rep=LCgWqnmvJEdr4weikw34/YnobXfMVs4ArKVXTxPDJdB1MMNDHnbiGxGwzKa7tWU2NmsiJ+E7j5v2WUZWg6tO1ZYkpLX8o7aOL02NCz/lndJ0Sj2D/29xiVLWNY4I0v86"></dx:ASPxHyperLink>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
    </form>
</body>
</html>
