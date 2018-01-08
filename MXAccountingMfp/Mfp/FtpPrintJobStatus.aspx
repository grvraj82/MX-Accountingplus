<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FtpPrintJobStatus.aspx.cs"
    Inherits="AccountingPlusDevice.Mfp.FtpPrintJobStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
        function RedirectToParent() {
            parent.location.href = "JobList.aspx?ID=Status";
        }

        function RedirectTo() {
            var isPSPModel = document.getElementById('HiddenFieldIsPSPModel').value;
            if (isPSPModel == "True") {
                parent.location.href = "../PSPModel/JobList.aspx?ID=Status";
            }
            else {
                parent.location.href = "../Mfp/JobList.aspx?ID=Status";
            }
        }

        function IsJobFinished() {
            setTimeout("RedirectTo()", 6000);
        }
    </script>
</head>
<style type="text/css">
     <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
</style>
<body id="pageBody" runat="server" leftmargin="0" topmargin="0" class="InsideBG">
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenFieldIsPSPModel" Value="0" runat="server" />
    <div id="WideVGA" runat="server">
        <table width="100%" border="0" cellpadding="0" height="250px" cellspacing="0">
            <tr>
                <td align="center" valign="middle" height="200">
                    <asp:Image ID="ImageMFP" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <div runat="server" id="Communicator">
                        <asp:Panel ID="PanelCommunicator" Visible="false" Width="100%" runat="server">
                            <table width="100%" align="center" border="0" class="Error_msgTable_Status" cellpadding="0"
                                cellspacing="0">
                                <tr class="Error_msgcenter">
                                    <td align="center" width="100%">
                                        <asp:Label ID="LabelCommunicatorMessage" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="100%">
                                        <asp:Table ID="TableButtons" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell ID="TableCellOk" Visible="false">
                                                    <asp:LinkButton ID="LinkButtonOk" runat="server">
                                                        <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="80%">
                                                            <asp:TableRow Width="70%">
                                                                <asp:TableCell Width="10%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="50%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_center">
                                                                    <div class="Login_TextFonts">
                                                                        <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="10%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                                    <asp:Image ID="ImageBlank" runat="server" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:LinkButton>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellCancel" Width="50%" HorizontalAlign="Right">
                                                    <asp:LinkButton ID="LinkButtonCancel" runat="server">
                                                        <table cellpadding="0" cellspacing="0" height="38" width="60%">
                                                            <tr width="70%">
                                                                <td width="10%" align="left" valign="top" class="Button_Left">
                                                                    &nbsp;
                                                                </td>
                                                                <td width="50%" align="left" valign="middle" class="Button_center">
                                                                    <div class="Login_TextFonts">
                                                                        <asp:Label ID="LabelMessageCancel" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="10%" align="left" valign="middle" class="Button_Right">
                                                                    <asp:Image ID="Image2" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:LinkButton>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellContinue" Width="50%" HorizontalAlign="Left">
                                                    <asp:LinkButton ID="LinkButtonContinue" OnClick="LinkButtonContinue_Click" runat="server">
                                                        <table cellpadding="0" cellspacing="0" border="0" height="38" width="60%">
                                                            <tr width="70%">
                                                                <td width="10%" align="left" valign="top" class="Button_Left">
                                                                    &nbsp;
                                                                </td>
                                                                <td width="50%" align="left" valign="middle" class="Button_center">
                                                                    <div class="Login_TextFonts">
                                                                        <asp:Label ID="LabelMessageContinue" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="10%" align="left" valign="middle" class="Button_Right">
                                                                    <asp:Image ID="Image3" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:LinkButton>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="PSP" runat="server" visible="false">
        <table width="100%" border="0" cellpadding="0" height="100px" cellspacing="0">
            <tr>
                <td align="center" valign="middle" height="100">
                    <asp:Image ID="PSPImageMFP" runat="server" Width="150" Height="100" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                    <div runat="server">
                        <asp:Panel ID="pspCommunicator" Visible="false" Width="100%" runat="server">
                            <table width="100%" align="center" border="0" class="Error_msgTable_Status" cellpadding="0"
                                cellspacing="0">
                                <tr class="Error_msgcenter">
                                    <td align="center" style="width: 100%;">
                                        <asp:Label ID="LabelPSPCommunicator" Font-Names="Verdana,Arial" Font-Bold="true"
                                            ForeColor="Red" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="100%">
                                        <asp:Table ID="TablePspButtons" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell ID="TableCellPSPOk" Visible="false">
                                                    <div style="width: 75%;">
                                                        <asp:LinkButton ID="LinkButtonPSPOk" runat="server" EnableViewState="false">
                                                            <table cellpadding="0" cellspacing="0" border="0" height="38" width="100%">
                                                                <tr>
                                                                    <td width="20%" align="left" valign="top" class="Button_Left">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="55%" align="center" valign="middle" class="Button_center">
                                                                        <div class="Login_TextFonts">
                                                                            <asp:Label ID="LabelPSPOk" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td width="25%" align="center" valign="middle" class="Button_Right">
                                                                        <asp:Image ID="Image5" SkinID="ImageBlank" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:LinkButton>
                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellPSPCancel" Width="50%" HorizontalAlign="Right">
                                                    <div style="width: 75%;">
                                                        <asp:LinkButton ID="LinkButtonPSPCancel" runat="server">
                                                            <table cellpadding="0" cellspacing="0" height="38" width="100%">
                                                                <tr>
                                                                    <td width="20%" align="left" valign="top" class="Button_Left">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="55%" align="center" valign="middle" class="Button_center">
                                                                        <div class="Login_TextFonts">
                                                                            <asp:Label ID="LabelPSPCancel" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td width="25%" align="left" valign="middle" class="Button_Right">
                                                                        <asp:Image ID="Image1" SkinID="ImageBlank" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:LinkButton>
                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellPSPContinue" Width="50%" HorizontalAlign="Left">
                                                    <div style="width: 75%;">
                                                        <asp:LinkButton ID="LinkButtonPSPContinue" OnClick="LinkButtonContinue_Click" runat="server">
                                                            <table cellpadding="0" cellspacing="0" border="0" height="38" width="100%">
                                                                <tr>
                                                                    <td width="20%" align="left" valign="top" class="Button_Left">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="55%" align="center" valign="middle" class="Button_center">
                                                                        <div class="Login_TextFonts">
                                                                            <asp:Label ID="LabelPSPContinue" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td width="25%" align="left" valign="middle" class="Button_Right">
                                                                        <asp:Image ID="Image4" SkinID="ImageBlank" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:LinkButton>
                                                    </div>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
