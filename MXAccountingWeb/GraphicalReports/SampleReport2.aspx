<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="SampleReport2.aspx.cs" Inherits="AccountingPlusWeb.GraphicalReports.SampleReport2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellReports();
        Meuselected("Reports");
    </script>
    <div>
        <asp:ScriptManager ID="smScriptManager" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Width="95%"
            Height="600px" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="GraphicalReports\Report1.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="MXAccountingWeb.GraphicalReports.DataSet1TableAdapters.T_JOB_LOGTableAdapter">
        </asp:ObjectDataSource>
    </div>
</asp:Content>
