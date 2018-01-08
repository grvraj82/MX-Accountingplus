<%@ Page Language="C#" MasterPageFile="~/MasterPages/Blank.Master" AutoEventWireup="true"
    CodeBehind="LicenceResponse.aspx.cs" Inherits="AccountingPlusWeb.Web.LicenceResponse"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <%--<table style="margin-top:10%" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td>
               
                <div style="margin-top:10px" class="MFPLogin_font">
                    <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label><br> 
                    <br />
                    <br />                
                    <asp:ImageButton ImageUrl="~/App_Themes/Blue/Images/key-icon.png" 
                        runat="server" ID="ImageButtonRegister" onclick="ImageButtonRegister_Click"  />
                </div>
            </td>
        </tr>
    </table>--%>
    <style type="text/css">
        body
        {
        	min-width:1024px;
        }
        .LoginTopBar
        {
            background-color: #171717;
            border-bottom: 1px solid #D6D7D9;
            height: 42px;	
        }

        .LoginTopBg
        {
	       /*background:url(Images/LoginTopBg.png) repeat-x center top #D4D7DB;*/
	       background:url(../Images/DisplaySprite/bodybg.jpg) repeat center top #07080d;
        }

        .LoginAreaWhiteBox{
        /*	border: 10px solid #FFFFFF;
	        -webkit-border-radius: 7px;
	        -moz-border-radius: 7px;
	        border-radius: 7px;	
            box-shadow: 0 0 5px #C7C7C7;	
	        background:#E0E1E3;*/
	        margin-top:2%;
	        width:100%;
	        height:600px;
	         
        }

        .LoginBox
        {
        	position: relative;
            z-index: 90;
	        background: #545b65; /* Old browsers */
	        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
	        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzU0NWI2NSIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMyNjJlMzUiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
	        background: -moz-linear-gradient(top,  #545b65 0%, #262e35 100%); /* FF3.6+ */
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#545b65), color-stop(100%,#262e35)); /* Chrome,Safari4+ */
	        background: -webkit-linear-gradient(top,  #545b65 0%,#262e35 100%); /* Chrome10+,Safari5.1+ */
	        background: -o-linear-gradient(top,  #545b65 0%,#262e35 100%); /* Opera 11.10+ */
	        background: -ms-linear-gradient(top,  #545b65 0%,#262e35 100%); /* IE10+ */
	        background: linear-gradient(to bottom,  #545b65 0%,#262e35 100%); /* W3C */
	        /*filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#545b65', endColorstr='#262e35',GradientType=0 ); /* IE6-8 */
	        border:8px solid #fd5767;
	        min-height:450px;
	        width:45%;
	        min-width:550px;
	        margin-top:7%;
	        -webkit-box-shadow: 0px 0px 4px #000000;
	        -moz-box-shadow:    0px 0px 4px #000000;
	        box-shadow:         0px 0px 4px #000000;	
	        
        }
        .LoginBox:hover
        {
	        background: #545b65; /* Old browsers */
	        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
	        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzU0NWI2NSIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMyNjJlMzUiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
	        background: -moz-linear-gradient(top,  #545b65 0%, #262e35 100%); /* FF3.6+ */
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#545b65), color-stop(100%,#262e35)); /* Chrome,Safari4+ */
	        background: -webkit-linear-gradient(top,  #545b65 0%,#262e35 100%); /* Chrome10+,Safari5.1+ */
	        background: -o-linear-gradient(top,  #545b65 0%,#262e35 100%); /* Opera 11.10+ */
	        background: -ms-linear-gradient(top,  #545b65 0%,#262e35 100%); /* IE10+ */
	        background: linear-gradient(to bottom,  #545b65 0%,#262e35 100%); /* W3C */
	        /*filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#545b65', endColorstr='#262e35',GradientType=0 ); /* IE6-8 */
	        border:8px solid #fd5767;
	        box-shadow:-3px -2px 29px 10px #fd5767;
	        transition:all 0.3s ease-in-out 0s;
	        transition-property: #51CBEE;
            transition-duration: 0.3s;
	        
	        min-height:450px;
	        width:45%;
	        min-width:550px;
	        margin-top:7%;
	        -webkit-box-shadow: 0px 0px 4px #000000;
	        -moz-box-shadow:    0px 0px 4px #000000;
	        
        }


        .LoginTitle{
	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        font-size:25px;
	        color:#ffffff;
	        text-shadow: 0 -1px 0 rgba(0, 0, 0, 0.25);
	        padding:10px 0px 0px 15px;
        }

        .LoginTitle span{
	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        font-size:12px;
	        color:#acacac;
	        float:left;
	        text-shadow: 0 -1px 0 rgba(0, 0, 0, 0.25);
	        padding:4px 0px 0px 0px;
        }

        .LoginTextBox{
	        height:30px;
	        width:200px;
	        margin-left:3px;
	        margin-right:3px;	
	        padding-left:4px;
	        color:#2f3944;
	        font-size:13px;
	        border: 1px solid #1b222b;
           background: #2d343d;
           -webkit-border-radius: 6px;
           -moz-border-radius: 6px;
           border-radius: 6px;
           color: #c9b7a2;
           -webkit-box-shadow: rgba(255,255,255,0.4) 0 1px 0, inset rgba(000,000,000,0.7) 0 0px 0px;
           -moz-box-shadow: rgba(255,255,255,0.4) 0 1px 0, inset rgba(000,000,000,0.7) 0 0px 0px;
           box-shadow: rgba(255,255,255,0.4) 0 1px 0, inset rgba(000,000,000,0.7) 0 0px 0px;
	        -webkit-border-radius: 3px;
	        -moz-border-radius: 3px;
	        border-radius: 3px;		
	        /*border:1px solid #28292d;*/
	        transition(all 0.30s ease-in-out);
           outline: none;	
        }

        .LoginTextBox:focus{
	        border: 1px solid #029dff;
	        box-shadow:0 0 5px #51CBEE;
	        transition:all 0.3s ease-in-out 0s;
        }

        .RememberPwd{
	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        font-size:11px;
	        color:#8f949a;	
        }
        .RememberPwd span{
	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        font-size:11px;
	        color:#8f949a;	
	        float:left;
	        padding-left:10px;
	        padding-top:3px;
        }

        .ForgotPwd{
	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        font-size:11px;
	        color:#8f949a;	
        }
        .ForgotPwd a{
	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        font-size:11px;
	        color:#8f949a;
	        text-decoration:none;	
        }
        .ForgotPwd a:hover{
	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        font-size:11px;
	        color:#ffffff;	
	        text-decoration:underline;	
        }

        .LoginBtn{
	        background: #34add6; /* Old browsers */
	        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
	        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzM0YWRkNiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMxYzk1ZGEiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
	        background: -moz-linear-gradient(top,  #34add6 0%, #1c95da 100%); /* FF3.6+ */
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#34add6), color-stop(100%,#1c95da)); /* Chrome,Safari4+ */
	        background: -webkit-linear-gradient(top,  #34add6 0%,#1c95da 100%); /* Chrome10+,Safari5.1+ */
	        background: -o-linear-gradient(top,  #34add6 0%,#1c95da 100%); /* Opera 11.10+ */
	        background: -ms-linear-gradient(top,  #34add6 0%,#1c95da 100%); /* IE10+ */
	        background: linear-gradient(to bottom,  #34add6 0%,#1c95da 100%); /* W3C */
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#34add6', endColorstr='#1c95da',GradientType=0 ); /* IE6-8 */

	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        text-shadow:0 -1px 0 rgba(0, 0, 0, 0.25);
	        cursor:pointer;
	        color:#ffffff;
	        font-size:13px;
	        font-weight:bold;
	        height:32px;
	        width:100px;
	        -webkit-border-radius: 3px;
	        -moz-border-radius: 3px;
	        border-radius: 3px;	
	        border:1px solid #4dc2ed;	
        }

        .LoginBtn:hover{
	        background: #1c95da; /* Old browsers */
	        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
	        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzFjOTVkYSIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiMzNGFkZDYiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
	        background: -moz-linear-gradient(top,  #1c95da 0%, #34add6 100%); /* FF3.6+ */
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#1c95da), color-stop(100%,#34add6)); /* Chrome,Safari4+ */
	        background: -webkit-linear-gradient(top,  #1c95da 0%,#34add6 100%); /* Chrome10+,Safari5.1+ */
	        background: -o-linear-gradient(top,  #1c95da 0%,#34add6 100%); /* Opera 11.10+ */
	        background: -ms-linear-gradient(top,  #1c95da 0%,#34add6 100%); /* IE10+ */
	        background: linear-gradient(to bottom,  #1c95da 0%,#34add6 100%); /* W3C */
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1c95da', endColorstr='#34add6',GradientType=0 ); /* IE6-8 */

	        font-family: Segoe UI,Arial, Helvetica, sans-serif;
	        text-shadow:0 -1px 0 rgba(0, 0, 0, 0.25);
	        cursor:pointer;
	        color:#ffffff;
	        font-size:13px;
	        font-weight:bold;
	        height:32px;
	        width:100px;
	        -webkit-border-radius: 3px;
	        -moz-border-radius: 3px;
	        border-radius: 3px;	
	        border:1px solid #4dc2ed;	
        }

        .CancelBtn{
	        background: #acacac; /* Old browsers */
	        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
	        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2FjYWNhYyIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM4Mjg1ODciIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
	        background: -moz-linear-gradient(top,  #acacac 0%, #828587 100%); /* FF3.6+ */
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#acacac), color-stop(100%,#828587)); /* Chrome,Safari4+ */
	        background: -webkit-linear-gradient(top,  #acacac 0%,#828587 100%); /* Chrome10+,Safari5.1+ */
	        background: -o-linear-gradient(top,  #acacac 0%,#828587 100%); /* Opera 11.10+ */
	        background: -ms-linear-gradient(top,  #acacac 0%,#828587 100%); /* IE10+ */
	        background: linear-gradient(to bottom,  #acacac 0%,#828587 100%); /* W3C */
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#acacac', endColorstr='#828587',GradientType=0 ); /* IE6-8 */

	        font-family: Segoe UI, Helvetica, sans-serif;
	        text-shadow:0 -1px 0 rgba(0, 0, 0, 0.25);
	        cursor:pointer;
	        color:#ffffff;
	        font-size:13px;
	        font-weight:bold;
	        height:32px;
	        width:100px;
	        -webkit-border-radius: 3px;
	        -moz-border-radius: 3px;
	        border-radius: 3px;	
	        border:1px solid #a8aaad;	
        }

        .CancelBtn:hover{
	        background: #828587; /* Old browsers */
	        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
	        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzgyODU4NyIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiNhY2FjYWMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
	        background: -moz-linear-gradient(top,  #828587 0%, #acacac 100%); /* FF3.6+ */
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#828587), color-stop(100%,#acacac)); /* Chrome,Safari4+ */
	        background: -webkit-linear-gradient(top,  #828587 0%,#acacac 100%); /* Chrome10+,Safari5.1+ */
	        background: -o-linear-gradient(top,  #828587 0%,#acacac 100%); /* Opera 11.10+ */
	        background: -ms-linear-gradient(top,  #828587 0%,#acacac 100%); /* IE10+ */
	        background: linear-gradient(to bottom,  #828587 0%,#acacac 100%); /* W3C */
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#828587', endColorstr='#acacac',GradientType=0 ); /* IE6-8 */

	        font-family:Arial, Helvetica, sans-serif;
	        text-shadow:0 -1px 0 rgba(0, 0, 0, 0.25);
	        cursor:pointer;
	        color:#ffffff;
	        font-size:13px;
	        font-weight:bold;
	        height:32px;
	        width:100px;
	        -webkit-border-radius: 3px;
	        -moz-border-radius: 3px;
	        border-radius: 3px;	
	        border:1px solid #a8aaad;	
        }
        
        .LoginFooterArea
        {
        	border:0;
        	z-index:1px;
        	position:absolute;
        	left:34%;
        	top:65%;
        	width:32%;
        	
        }
        
        .LoginFooterText
        {
            font-family:Segoe UI, Arial;
	        font-size:10px;
	        color:#999999;
        }
        
      
        .ribbon-wrapper-green 
        {
            overflow: hidden;    
            top: 0;
            right: 0;
        }

        .ribbon-green 
        {
            color: #333;
            text-align: center;
            text-shadow: rgba(255,255,255,0.5) 0px 1px 0px;
            -webkit-transform: rotate(45deg);
            -moz-transform:    rotate(45deg);
            -ms-transform:     rotate(45deg);
            -o-transform:      rotate(45deg);
            min-width:122px;            
            width:122px;
            left: 514px;
            padding: 7px 0;
            position: absolute;
            text-align: center;
            text-shadow: 0 1px 0 rgba(255, 255, 255, 0.5);
            top: 16px;
            background-color: #eb6835;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#BFDC7A), to(#8EBF45)); 
            background-image: -webkit-linear-gradient(top, #eb6835, #eb6835); 
            background-image:  -moz-linear-gradient(top, #eb6835, #eb6835); 
            background-image:  -ms-linear-gradient(top, #eb6835, #eb6835); 
            background-image:  -o-linear-gradient(top, #eb6835, #eb6835); 
            color: #FFF;
            -webkit-box-shadow: 0px 0px 3px rgba(0,0,0,0.3);
            -moz-box-shadow:    0px 0px 3px rgba(0,0,0,0.3);
            box-shadow:         0px 0px 3px rgba(0,0,0,0.3);
        }

        .ribbon-green:before, .ribbon-green:after {
          content: "";
          border-top:   3px solid #6e8900;   
          border-left:  3px solid transparent;
          border-right: 3px solid transparent;
          position:absolute;
          bottom: -3px;
        }

        .ribbon-green:before {
          left: 0;
        }
        .ribbon-green:after {
          right: 0;
        }
         .TableCellLabel
        {
            padding-top: 15px;
            padding-bottom: 15px;
            color:#FFF !important;
            font-family: Segoe UI, Arial;
            font-size: 16px; /*text-transform:uppercase;*/
            font-weight: bold;
            color: #464545;
            text-shadow: 0 1px #FFFFFF;
        }
         .TableCellLabelLeft
        {
            padding-top: 15px;
            padding-bottom: 15px;
            padding-left: 10px;
            color:#FFF !important;
            font-family:  Segoe UI, Arial;
            font-size: 18px; /*text-transform:uppercase;*/
            color: #464545;
            text-shadow: 0 1px #FFFFFF;
        }
        .TitleRow
        {
            background: url(../Images/DisplaySprite/Table_Top.png) repeat-x center;
            height: 34px;
            font-family: Segoe UI, Arial;
            font-size: 20px; /*text-transform:uppercase;*/
            font-weight: bold;
            color: #464545;
            text-shadow: 0 1px #FFFFFF;
            text-align: left;
            border-bottom: 1px solid #a3a3a3;
        }
        .TableCellLabel_Error
        {
            padding-top: 15px;
            padding-bottom: 15px;
            color:#fd7676 !important;
            font-family: Segoe UI, Arial;
            font-size: 20px; /*text-transform:uppercase;*/
            font-weight: bold;
            color:#fd7676;
            text-shadow: 0 1px #FFFFFF;
        }
         .TableCellLabel_Normal
        {
            padding-top: 15px;
            padding-bottom: 15px;
            color:#FFF !important;
            font-family: Segoe UI, Arial;
            font-size: 14px; /*text-transform:uppercase;*/
            font-weight:normal
            color: #464545;
            text-shadow: 0 1px #FFFFFF;
        }
        
        .Revalidation
        {
            color:#FFF !important;
            font-family: Segoe UI, Arial;
            font-size: 18px; /*text-transform:uppercase;*/
            font-weight:normal
            color: #464545;
            text-shadow: 0 1px #FFFFFF;
        }
        
    </style>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center" >
        <tr>
            <td align="center" valign="middle" width="100%" >
                <div class="LoginAreaWhiteBox">
                    <div class="LoginBox ribbon-wrapper-green" style="background-color:Black">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="" >
                                        <tr>
                                            <td colspan="2" align="center" class="TableCellLabel_Error">
                                                <div class="TableCellLabel_Error">
                                                    &nbsp;&nbsp;Error Occured!
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" border="0" width="96%" align="center" >
                                        <tr>
                                            <td width="25%" class="TableCellLabel" align="right">
                                                <asp:Label ID="LabelServerID" runat="server" Text="Server Id"></asp:Label>&nbsp;:
                                            </td>
                                            <td width="60%" align="left" class="TableCellLabelLeft">
                                                <asp:Label ID="LabelServerIDText" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" class="TableCellLabel" align="right">
                                                <asp:Label ID="LblMessage" runat="server" Text="Message ID"></asp:Label>&nbsp;:
                                            </td>
                                            <td width="60%" align="left" class="TableCellLabelLeft">
                                                <asp:Label ID="LabelMessageID" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="TableCellLabel">
                                                <asp:Label ID="Labelmessage" runat="server" Text="Message"></asp:Label>&nbsp;:
                                            </td>
                                            <td align="left" class="TableCellLabelLeft">
                                                <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="TableCellLabel">
                                                <asp:Label ID="LabelSuggestion" runat="server" Text="Suggestion"></asp:Label>&nbsp;:
                                            </td>
                                            <td align="left" class="TableCellLabelLeft">
                                                <asp:Label ID="LabelSuggestioinText" runat="server" Text=""></asp:Label>
                                                <asp:ImageButton ImageUrl="~/App_Themes/Blue/Images/key-icon.png" runat="server"
                                                    ID="ImageButtonRegister" OnClick="ImageButtonRegister_Click" Height="24" Width="24" Visible="false"/>
                                                <%-- <asp:ImageButton ID="ImageButtonRegister" runat="server" ImageUrl="~/Images/key-icon.png"
                                                    Visible="false" Height="24" Width="24" OnClick="ImageButtonRegister_Click" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="TableCellLabel">
                                                <asp:Label ID="LabelException" runat="server" Text="Exception"></asp:Label>&nbsp;:
                                            </td>
                                            <td align="left" class="TableCellLabelLeft">
                                                <asp:Label ID="LabelExceptionMessage" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div class="LoginFooterArea">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="top" class="LoginFooterText" width="75%">
                                <%--<asp:Label ID="LabelRights" runat="server" Text="2013 Sharp Software Development INDIA PVT LTD. , ALL RIGHTS RESERVED."></asp:Label>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
