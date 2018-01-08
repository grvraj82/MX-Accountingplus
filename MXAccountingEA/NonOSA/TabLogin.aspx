<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TabLogin.aspx.cs" Inherits="AccountingPlusEA.NonOSA.TabLogin" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>AccountingPlus Campus - Login</title>
   
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <link rel="stylesheet" href="css/Login_style.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenFieldAuthSource" runat="server" Value="AD" />
   <header class="top-header"><!-- Logo Area Starts -->
        <div class="container">
            <img src="Images/Logo_Big.png" alt="" class="img-responsive logo">
        </div>
    </header><!-- Logo Area Ends -->

    <section><!-- Login Section Starts -->
        <div class="container-fluid">
            <div class="row">

                <div class="carousel-inner LoginOuterBox" role="listbox">

                    <div class="col-lg-6 col-md-6 col-sm-6 Login_ImgDiv">
                        <img src="Images/Login_Img.png" alt="" class="img-responsive" border="0" />
                    </div>

                    <div class="col-lg-5 col-md-5 col-sm-5">

                        <div class="LoginBox_Bg"><!--Loginbox Starts-->

                            <div id="ManualLogin_Thumb_Cont" class="Dummy"><!--ManualLogin Content Starts-->

                                <div class="LoginBox_Header">
                                    Manual Login
                                </div>

                                <div class="Toggle">
                                    <div class="ToggleSub_Select" id="ActiveDirectory_Tab">Active Directory</div>
                                    <div class="ToggleSub" id="Database_Tab">Database</div>
                                </div>

                                <div class="Logintype_Outerdiv" id="ActiveDirectory_Tab_Cont">
                                    <ul>
                                        <li>
                                        <asp:DropDownList ID="DropDownListDomainList" runat="server" CssClass="Login_Dropdown"></asp:DropDownList>
                                        
                                        </li>
                                        <li>
                                        <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="Login_TextBox_Username" onfocus="if (this.value=='Enter your username') this.value = ''" onblur="if (this.value=='') this.value = 'Enter your username'" value="Enter your username" ></asp:TextBox>
                                           <%-- <input type="text" name="TextBox" class="Login_TextBox_Username" onfocus="if (this.value=='Enter your username') this.value = ''" onblur="if (this.value=='') this.value = 'Enter your username'" value="Enter your username" />--%>
                                        </li>
                                        <li>
                                        <asp:TextBox ID="TextBoxPassword" runat="server" CssClass="Login_TextBox_PWD" TextMode="Password" onfocus="if (this.value=='Password') this.value = ''" onblur="if (this.value=='') this.value = 'Password'" value="Password"></asp:TextBox>
                                            <%--<input type="password" name="Password" class="Login_TextBox_PWD" onfocus="if (this.value=='Password') this.value = ''" onblur="if (this.value=='') this.value = 'Password'" value="Password" />--%>
                                        </li>
                                        <li>
                                        <asp:Button ID="ButtonLogOn" runat="server" Text="Button"  CssClass="Login_Btn" OnClick="ButtonLogOn_Click"></asp:Button>
                                           <%-- <input type="button" name="Button" value="Login" class="Login_Btn" />--%>
                                        </li>
                                    </ul>
                                </div>

                                <div class="Logintype_Outerdiv" id="Database_Tab_Cont" style="display:none;">
                                    <ul>
                                      <li>
                                        <asp:TextBox ID="TextBoxUserNameDB" runat="server" CssClass="Login_TextBox_Username" onfocus="if (this.value=='Enter your username') this.value = ''" onblur="if (this.value=='') this.value = 'Enter your username'" value="Enter your username" ></asp:TextBox>
                                           <%-- <input type="text" name="TextBox" class="Login_TextBox_Username" onfocus="if (this.value=='Enter your username') this.value = ''" onblur="if (this.value=='') this.value = 'Enter your username'" value="Enter your username" />--%>
                                        </li>
                                        <li>
                                        <asp:TextBox ID="TextBoxPasswordDB" runat="server" CssClass="Login_TextBox_PWD" TextMode="Password" onfocus="if (this.value=='Password') this.value = ''" onblur="if (this.value=='') this.value = 'Password'" value="Password"></asp:TextBox>
                                            <%--<input type="password" name="Password" class="Login_TextBox_PWD" onfocus="if (this.value=='Password') this.value = ''" onblur="if (this.value=='') this.value = 'Password'" value="Password" />--%>
                                        </li>
                                        <li>
                                        <asp:Button ID="Button1" runat="server" Text="Button"  CssClass="Login_Btn" OnClick="ButtonLogOn_Click"></asp:Button>
                                           <%-- <input type="button" name="Button" value="Login" class="Login_Btn" />--%>
                                        </li>
                                    </ul>
                                </div>

                            </div><!--ManualLogin Content Ends-->

                            <div id="CardLogin_Thumb_Cont" class="Dummy MarginTop_CardLogin" style="display:none;"><!--CardLogin Content Starts-->
                            
                                <div class="LoginBox_Header">
                                    Card Login
                                </div>
                            
                                <div class="Logintype_Outerdiv_New">
                                    <ul>
                                        <li>
                                            <asp:TextBox ID="TextBoxCardId" TextMode="Password" style="margin-top:0 !important;" runat="server" CssClass="Login_TextBox_CardId" onfocus="if (this.value=='Enter Your Card Id') this.value = ''" onblur="if (this.value=='') this.value = 'Enter Your Card Id'" value="Enter Your Card Id" ></asp:TextBox>
                                            <%--<input type="text" name="TextBox" style="margin-top:0 !important;" class="Login_TextBox_CardId" onfocus="if (this.value=='Enter Your Card Id') this.value = ''" onblur="if (this.value=='') this.value = 'Enter Your Card Id'" value="Enter Your Card Id" />--%>
                                        </li>
                                        <li>
                                        <asp:Button ID="ButtonCard" runat="server" Text="Button" CssClass="Login_Btn" OnClick="ButtonCard_Click"></asp:Button>
                                            <%--<input type="button" name="Button" value="Login" class="Login_Btn" />--%>
                                        </li>
                                    </ul>
                                </div>  

                            </div><!--CardLogin Content Ends--> 

                            <div id="PinLogin_Thumb_Cont" class="Dummy MarginTop_CardLogin" style="display:none;"><!--PinLogin Content Starts-->
                            
                                <div class="LoginBox_Header">
                                    Pin Login
                                </div>

                                <div class="Logintype_Outerdiv_New">
                                    <ul>
                                        <li>
                                            <input type="text" name="TextBox" style="margin-top:0 !important;" class="Login_TextBox_Pin" onfocus="if (this.value=='Enter Your Pin') this.value = ''" onblur="if (this.value=='') this.value = 'Enter Your Pin'" value="'Enter Your Pin" />
                                        </li>
                                        <li>
                                            <input type="button" name="Button" value="Login" class="Login_Btn" />
                                        </li>
                                    </ul>
                                </div>    

                            </div><!--PinLogin Content Ends-->

                        </div><!--Loginbox Ends-->

                    </div>

                </div>

            </div>
        </div>
    </section><!-- Login Section Ends -->

    <section class="BottomMenu_Thumbs">
        <!-- About & How it works Starts -->
        <div class="container">
            <div class="row">
                
                <div>
                    <ul class="BottThumbs_Bg">
                        <li class="Selected" id="ManualLogin_Thumb">
                            <img src="Images/1.gif" alt="" class="ManualLogin_Icon img-responsive" title="Manual Login" border="0" />
                        </li>
                        <li id="CardLogin_Thumb">
                            <img src="Images/1.gif" alt="" class="CardLogin_Icon img-responsive" title="Card Login" border="0" />
                        </li>
                        <li id="PinLogin_Thumb">
                            <img src="Images/1.gif" alt="" class="PinLogin_Icon img-responsive" title="Pin Login" border="0" />
                        </li>
                    </ul>
                </div>

            </div>
        </div>
    </section><!-- About & How it works Ends -->

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


                var tabDB = document.getElementById("Database_Tab_Cont").offsetLeft;

                if (tabDB <= 0) {
                    document.getElementById('HiddenFieldAuthSource').value = "AD";
                }
                else {
                    document.getElementById('HiddenFieldAuthSource').value = "DB";
                }
            });
        });

    </script>

    </form>
</body>
</html>
