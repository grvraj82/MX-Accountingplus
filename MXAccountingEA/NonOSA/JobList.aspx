<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="AccountingPlusEA.NonOSA.JobList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<head>
    <meta charset="UTF-8">
   
    <title></title>
     <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <link rel="stylesheet" href="css/Login_Index.css"/>
    <style type="text/css">
        .Hide
        {
            display: none;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
    <header class="top-header"><!-- Header Area Starts -->
        <div class="container-fluid">
            <div class="row">

                <div class="col-xs-4 header-logo">
                    <a href=""><img src="Images/Logo_Small.png" alt="" class="img-responsive logo" border="0" /></a>
                </div>

                <div class="col-md-8" style="padding:0 !important;">
                    <nav class="navbar navbar-default">
                        <div class="container-fluid nav-bar" style="padding:0 !important;">
                            <!-- Brand and toggle get grouped for better mobile display -->
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                                    <span class="sr-only">Toggle navigation</span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>
                            </div>

                            <!-- Collect the nav links, forms, and other content for toggling -->
                            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                                <ul class="nav navbar-nav navbar-right">
                                    <li>
                                        <a class="menu" href="#">
                                            <img src="Images/1.gif" alt="" class="img-responsive Admin_Img" border="0" />
                                            Welcome <asp:Label ID="LabelUserHeader" runat="server" Text=""></asp:Label>
                                        </a>
                                    </li>
                                    <li>
                                        <a class="menu LogoutOuter" href="#"><asp:ImageButton BorderWidth="0" ID="ImageButtonLogout" ImageUrl="Images/1.gif" CssClass="img-responsive Logout_Icon" OnClick="ImageButtonLogout_Click" runat="server"></asp:ImageButton>
                                          <%--  <img src="Images/1.gif" alt="" class="img-responsive Logout_Icon" border="0" />--%>
                                            Logout
                                        </a>
                                    </li>
                                </ul>
                            </div><!-- /navbar-collapse -->
                        </div><!-- / .container-fluid -->
                    </nav>
                </div>

            </div>
        </div>
    </header>
    <!-- Header Area Ends -->
    <section><!-- Login Section Starts -->
        <div class="container-fluid" style="padding:0 !important;">
            <div class="row" style="margin:0 !important;">

                <div class="carousel-inner InnerPadd" role="listbox">

                    <div class="TitleFont">
                        Documents for <asp:Label ID="LabelUser" runat="server" Text=""></asp:Label>  
                    </div>
                  
                    <div class="col-md-9 col-sm-9 col-xs-12 GridOuterDiv">
                        
                        <asp:GridView ID="GridViewJobs" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            EnableViewState="False" CssClass="DataGrid" CellPadding="3" DataKeyNames="JOBID" Width="100%" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="File Name" SortExpression="Name" />
                <asp:BoundField DataField="Date" HeaderText="Date" />
                <asp:BoundField DataField="JOBID" HeaderText="JOBID"  ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
            </Columns>
            <%--<FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
        </asp:GridView>

                       <%-- <table width="100%" cellpadding="0" cellspacing="0" border="0" class="DataGrid">
                            <tr>
                                <th>
                                    <input type="checkbox" name="Checkbox" />
                                </th>
                                <th>
                                    Job details
                                </th>
                                <th>
                                    Last Updated
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="checkbox" name="Checkbox" />
                                </td>
                                <td>
                                    Filename_123
                                </td>
                                <td>
                                    12/07/2014  10:05:52
                                </td>
                            </tr>
                        </table>--%>

                    </div>

                    <div class="col-md-3 col-sm-3 col-xs-12 ButtonsOuterDiv">
                        
                        <ul>
                        <li>
                        <asp:DropDownList ID="DropDownListMFPs" Width="150px" height="30px" runat="server"></asp:DropDownList>
                         </li>
                            <li>
                                <asp:ImageButton ID="ImageButtonPrint" CssClass="img-responsive Print_Icon" runat="server" OnClick="ImageButtonPrint_Click" border="0"></asp:ImageButton>
                                <span>Print</span>
                            </li>
                            <li>
                            <asp:ImageButton ID="ImageButtonPrintDelete" runat="server" Cssclass="img-responsive PrintandDelete_Icon" OnClick="ImageButtonPrintDelete_Click" border="0"></asp:ImageButton>
                            
                                <span>Print & Delete</span>
                            </li>
                            <li>
                               
                                <asp:ImageButton ID="ImageButtonFastPrint" runat="server" Cssclass="img-responsive FastPrint_Icon" OnClick="ImageButtonFastPrint_Click" border="0"></asp:ImageButton>
                                <span>Fast Print</span>
                            </li>
                            <li>
                            <asp:ImageButton ID="ImageButtonDelete" runat="server" Cssclass="img-responsive Delete_Icon" OnClick="ImageButtonDelete_Click" border="0"></asp:ImageButton>
                             
                                <span>Delete</span>
                            </li>
                        </ul>

                    </div>

                </div>

            </div>
        </div>
    </section>
    <!-- Login Section Ends -->
    <!-- Script tags -->
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript">

        //Js for Thumbs & Tabs
        $(document).ready(function () {

            $('.BottThumbs_Bg li').click(function (e) {
                e.stopPropagation();
                $('.BottThumbs_Bg li').removeClass("Selected");
                $('.Dummy').hide();
                $(this).addClass("Selected");
                $('#' + $(this).attr('id') + "_Cont").show();
            });

            $('.Toggle div').click(function (e) {
                e.stopPropagation();
                $('.Toggle div').removeClass("ToggleSub_Select").addClass("ToggleSub");
                $('.Logintype_Outerdiv').hide();
                $(this).addClass("ToggleSub_Select");
                $('#' + $(this).attr('id') + "_Cont").show();
            });

        });

    </script>
    <%--   <div>
        <asp:Button ID="CheckAll" runat="server" Text="Check All"  OnClick="CheckAll_Click"/>
        <asp:Button ID="UncheckAll" runat="server" Text="Uncheck All" OnClick="UncheckAll_Click"/>

         <asp:Button ID="ButtonPrint" runat="server" Text="Print" OnClick="ButtonPrint_Click"/>
          <asp:Button ID="ButtonPrintandDelete" runat="server" Text="Print&Delete" OnClick="ButtonPrintDelete_Click"/>
        <asp:GridView ID="GridViewJobs" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            EnableViewState="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="1px" CellPadding="3" DataKeyNames="JOBID" >
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="File Name" SortExpression="Name" />
                <asp:BoundField DataField="Date" HeaderText="Date" />
                <asp:BoundField DataField="JOBID" HeaderText="JOBID"  ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>--%>
    </form>
</body>
</html>
