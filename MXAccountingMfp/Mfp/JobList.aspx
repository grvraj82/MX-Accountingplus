<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="AccountingPlusDevice.Browser.JobList" %>

<html>
<head runat="server">
    <meta name="Browser" content="NetFront" />
    <title></title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">

        var timerCloseCommunicator;
        function GetNextPage() {
            document.sound(0);
            //CloseCommunicator();
            //UnSelectAllJobs();
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
            var currentPage = parseInt(document.getElementById("currentPage").value);
            var recordsPerPage = parseInt(document.getElementById("HiddenRecordsPerPage").value);
            var totalPages = totalRecords / recordsPerPage;
            var strTotalPages = totalPages + "";
            var objPaginationInformation = document.getElementById("LabelPaginationInformation");

            if (strTotalPages.lastIndexOf('.') > 0) {
                totalPages = parseInt(totalPages);
                totalPages++;
            }

            if (currentPage > totalPages) {
                currentPage = parseInt(totalPages);
            }

            currentPage++

            if (currentPage > totalPages) {
                currentPage = currentPage - 1;
            }
            document.getElementById("currentPage").value = currentPage;

            var recordStartIndex = (currentPage * recordsPerPage) - recordsPerPage;
            var recordEndIndex = recordStartIndex + recordsPerPage;

            var lastRecordID = recordEndIndex;
            var firstRecordID = recordStartIndex;

            if (firstRecordID < 0) {
                firstRecordID = 1;
            }

            if (lastRecordID > totalRecords) {
                lastRecordID = totalRecords;
            }

            if (parseInt(currentPage) == parseInt(totalPages)) {
                var divEnabled = document.getElementById("NextDisabled");
                divEnabled.style.display = 'block';
                var divDisabled = document.getElementById("NextEnabled");
                divDisabled.style.display = 'none';

                var divEnabled = document.getElementById("PreviousDisabled");
                divEnabled.style.display = 'none';
                var divDisabled = document.getElementById("PreviousEnabled");
                divDisabled.style.display = 'block';
            }
            else {
                var divEnabled = document.getElementById("PreviousDisabled");
                divEnabled.style.display = 'none';
                var divDisabled = document.getElementById("PreviousEnabled");
                divDisabled.style.display = 'block';
            }

            objPaginationInformation.innerText = currentPage + "/" + totalPages;

            for (row = 0; row < totalRecords; row++) {
                var tr = document.getElementById("_row__" + row);
                var horizantalRow = document.getElementById("_horizantalRow__" + row);
                tr.style.display = 'none';
                horizantalRow.style.display = 'none';
            }

            for (row = recordStartIndex; row < recordEndIndex; row++) {
                var tr = document.getElementById("_row__" + row);
                var horizantalRow = document.getElementById("_horizantalRow__" + row);
                tr.style.display = '';
                horizantalRow.style.display = '';
            }
            ValidateSelectedCount();
        }

        function GetPreviousPage() {
            document.sound(0);
            //CloseCommunicator();
            //UnSelectAllJobs();
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
            var currentPage = parseInt(document.getElementById("currentPage").value);
            var recordsPerPage = parseInt(document.getElementById("HiddenRecordsPerPage").value);
            var totalPages = totalRecords / recordsPerPage;
            var objPaginationInformation = document.getElementById("LabelPaginationInformation");

            var strTotalPages = totalPages + "";

            if (strTotalPages.lastIndexOf('.') > 0) {
                totalPages = parseInt(totalPages);
                totalPages++;
            }

            currentPage--
            if (currentPage <= 0) {
                currentPage = 1;
            }

            document.getElementById("currentPage").value = currentPage;

            var recordStartIndex = (currentPage * recordsPerPage) - recordsPerPage;
            var recordEndIndex = recordStartIndex + recordsPerPage;

            var lastRecordID = recordEndIndex;
            var firstRecordID = recordStartIndex;

            if (firstRecordID < 0) {
                firstRecordID = 1;
            }

            if (lastRecordID > totalRecords) {
                lastRecordID = totalRecords;
            }

            if (parseInt(currentPage) == 1) {
                var divEnabled = document.getElementById("PreviousDisabled");
                divEnabled.style.display = 'block';
                var divDisabled = document.getElementById("PreviousEnabled");
                divDisabled.style.display = 'none';

                if (totalPages > 1) {
                    var divEnabled = document.getElementById("NextDisabled");
                    divEnabled.style.display = 'none';
                    var divDisabled = document.getElementById("NextEnabled");
                    divDisabled.style.display = 'block';
                }
                else {
                    var divEnabled = document.getElementById("NextDisabled");
                    divEnabled.style.display = 'block';
                    var divDisabled = document.getElementById("NextEnabled");
                    divDisabled.style.display = 'none';
                }
            }
            if (parseInt(currentPage) <= parseInt(totalPages)) {
                var divEnabled = document.getElementById("NextDisabled");
                divEnabled.style.display = 'none';
                var divDisabled = document.getElementById("NextEnabled");
                divDisabled.style.display = 'block';
            }

            objPaginationInformation.innerText = currentPage + "/" + totalPages;

            for (row = 0; row < totalRecords; row++) {
                var tr = document.getElementById("_row__" + row);
                var horizantalRow = document.getElementById("_horizantalRow__" + row);
                tr.style.display = 'none';
                horizantalRow.style.display = 'none';
            }

            for (row = recordStartIndex; row < recordEndIndex; row++) {
                var tr = document.getElementById("_row__" + row);
                var horizantalRow = document.getElementById("_horizantalRow__" + row);
                tr.style.display = '';
                horizantalRow.style.display = '';
            }
            ValidateSelectedCount();
            ResetTrackCounter();
        }

        function confirmDeleteJobs() {
            var selectedFileCount = GetSelectedItemCount();
            if (selectedFileCount == 0) {
                alert("Please selected the files to be deleted");
                return false;
            }
            if (confirm("Do you want to delete selected files?")) {

            }
            else {
                return false;
            }
        }

        function GetSelectedItemCount() {
            var selectedItemCount = 0;
            return selectedItemCount;
        }

        function CloseCommunicator() {
            try {
                document.sound(0);
                document.getElementById("PanelCommunicator").style.display = "none";
                document.getElementById("displayLimits").style.display = "none";
            }
            catch (Error) {
            }
            ResetTrackCounter();
        }

        function ShowPagination() {
            //alert(self.innerHeight);
            //alert(self.innerWidth);
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
            var currentPage = parseInt(document.getElementById("currentPage").value);
            var recordsPerPage = parseInt(document.getElementById("HiddenRecordsPerPage").value);
            var totalPages = totalRecords / recordsPerPage;
            var strTotalPages = totalPages + "";
            var objPaginationInformation = document.getElementById("LabelPaginationInformation");

            if (strTotalPages.lastIndexOf('.') > 0) {
                totalPages = parseInt(totalPages);
                totalPages++;
            }
            if (totalPages == 1) {
                var divEnabled = document.getElementById("PreviousDisabled");
                divEnabled.style.display = 'block';
                var divDisabled = document.getElementById("PreviousEnabled");
                divDisabled.style.display = 'none';

                var divEnabled = document.getElementById("NextDisabled");
                divEnabled.style.display = 'block';
                var divDisabled = document.getElementById("NextEnabled");
                divDisabled.style.display = 'none';
            }

            if (totalPages > 0) {
                objPaginationInformation.innerText = currentPage + "/" + totalPages;
            }
            else {
                objPaginationInformation.innerText = "0/0";
                var divEnabled = document.getElementById("NextDisabled");
                divEnabled.style.display = 'block';
                var divDisabled = document.getElementById("NextEnabled");
                divDisabled.style.display = 'none';
            }
            if (parseInt(totalRecords) <= 0) {
                document.getElementById("PanelPrintActive").style.display = "none";
            }
            //ResetTrackCounter();
        }

        function LoadPageImages() {
            try {
                ShowPagination();
                SelectCheckedJobs();
                SelectEmailCheckedJobs();
                pageImage = new Image(100, 25);
                timerCloseCommunicator = setTimeout('CloseCommunicator()', 10000);
                var currentPage = document.getElementById("currentPage").value;
                if (currentPage > 1) {
                    document.getElementById("currentPage").value = currentPage - 1;
                    GetNextPage();
                }
                ValidateSelectedCount();
            }
            catch (Error) {
            }
            ResetTrackCounter();
        }

        function SelectCheckedJobs() {
            var objCheckBoxes = document.forms["jobList"].elements["__SelectedFiles"];
            for (var i = 0; i < document.getElementById('jobList').elements.length; i++) {
                try {
                    if (objCheckBoxes[i].checked) {
                        objCheckBoxes[i].checked = true;
                        var rowId = "_row__" + i;
                    }
                    else {
                        objCheckBoxes[i].checked = false;
                    }
                }
                catch (Error) {

                }
            }
            //ResetTrackCounter();
        }

        function SelectEmailCheckedJobs() {
            var objCheckBoxes = document.forms["jobList"].elements["__SelectedEmailFiles"];
            for (var i = 0; i < document.getElementById('jobList').elements.length; i++) {
                try {
                    if (objCheckBoxes[i].checked) {
                        objCheckBoxes[i].checked = true;
                        var rowId = "_row__" + i;
                    }
                    else {
                        objCheckBoxes[i].checked = false;
                    }
                }
                catch (Error) {

                }
            }
            //ResetTrackCounter();
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var emailSelected = 0;
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
            var objCheckBoxes = document.forms["jobList"].elements["__SelectedFiles"];
            var objEmailCheckBoxes = document.forms["jobList"].elements["__SelectedEmailFiles"];
            for (var i = 0; i < document.getElementById('jobList').elements.length; i++) {
                try {
                    if (objCheckBoxes[i].checked) {
                        selectedCount++;
                    }
                    if (objEmailCheckBoxes[i].checked) {
                        emailSelected = 1;
                        selectedCount++;
                    }
                }
                catch (Error) {
                }
            }
            if (totalRecords == 0) {
                document.getElementById("PanelPrintActive").style.display = "none";
                document.getElementById("PanelPrintInActive").style.display = "none";
            }
            else if (totalRecords != 1) {
                if (selectedCount == 1) {
                    //alert(selectedCount + " " + totalRecords);
                    /*var originalPrintText = document.getElementById("HiddenFieldPrintText").value;
                    document.getElementById('LabelPrintInActive').innerText =originalPrintText;*/
                    document.getElementById("PanelPrintActive").style.display = "inline";
                    if (emailSelected == 0)
                    document.getElementById("PanelPrintInActive").style.display = "block";
                }
                else {
                    /*var originalPrintText = document.getElementById("HiddenFieldPrintText").value;
                    document.getElementById('LabelPrintInActive').innerText =originalPrintText + " +";*/
                    document.getElementById("PanelPrintActive").style.display = "inline";
                    document.getElementById("PanelPrintInActive").style.display = "none";
                }
            }

            // Check and Uncheck chkAll Checkbox
            if (totalRecords == selectedCount) {
                var checkBoxAll = document.getElementById("chkALL");
                checkBoxAll.checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL");
                checkBoxAll.checked = false;
            }

            if (totalRecords == 0) {
                var checkBoxAll = document.getElementById("chkALL");
                checkBoxAll.checked = false;
            }
            // Enable or Disable Fast Print, Print & Delete, Delete, Print Buttons
            //ResetTrackCounter();
        }

        function connectTo(sortOn, sortMode) {
            document.sound(0);
            var navigationUrl = "JobList.aspx?sortOn=" + sortOn + "&sortMode=" + sortMode;
            location.href = navigationUrl;
        }

        function ChkandUnchk() {
            CloseCommunicator();
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
            if (document.getElementById('chkALL').checked) {
                SelectAllJobs();
                ValidateSelectedCount();

                if (totalRecords == 1) {
                    
                    document.getElementById('chkALL').checked = true;
                    document.getElementById("PanelPrintActive").style.display = "inline";
                    document.getElementById("PanelPrintInActive").style.display = "block";
                }
            }
            else {
                UnSelectAllJobs();
                ValidateSelectedCount();

                if (totalRecords == 1) {
                    document.getElementById('chkALL').checked = false;
                    document.getElementById("PanelPrintActive").style.display = "inline";
                    document.getElementById("PanelPrintInActive").style.display = "none";
                }
            }
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
            if (parseInt(totalRecords) <= 0) {
                document.getElementById('chkALL').checked = false;
                document.getElementById("PanelPrintActive").style.display = "none";
            }
            ResetTrackCounter();
        }


        function SelectAllJobs() {
            try {
                var objCheckBoxes = document.forms["jobList"].elements["__SelectedFiles"];
                var objEmailCheckBoxes = document.forms["jobList"].elements["__SelectedEmailFiles"];
                var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
                var currentPage = parseInt(document.getElementById("currentPage").value);
                var startIndex = 0;
                var endIndex = startIndex + totalRecords;
                if (endIndex >= totalRecords) {
                    endIndex = totalRecords;
                }


                if (totalRecords == 1) {
                    try {

                        for (var i = startIndex; i < endIndex; i++) {
                            var jobID = objCheckBoxes.id;
                            jobID = jobID.replace("JobID_", "");
                            if (parseInt(jobID) == i) {
                                objCheckBoxes.checked = true;
                                var rowId = "_row__" + i;
                                //document.getElementById(rowId).style.backgroundColor = "#6d814b";
                                document.getElementById(rowId).className = 'Checkboxunselectrow';
                            }
                            else {
                                objCheckBoxes.checked = false;
                            }
                        }
                    }
                    catch (Error)
                    { }
                }
                else {
                    for (var i = startIndex; i < endIndex; i++) {
                        try {
                            var jobID = objCheckBoxes[i].id;
                            jobID = jobID.replace("JobID_", "");
                            if (parseInt(jobID) == i) {
                                objCheckBoxes[i].checked = true;
                                var rowId = "_row__" + i;
                                document.getElementById(rowId).className = 'Checkrowselect';
                            }
                            else {
                                objCheckBoxes[i].checked = false;
                            }
                        }
                        catch (Error) {
                        }
                    }
                }
                //Email
                if (totalRecords == 1) {

                    for (var i = startIndex; i < endIndex; i++) {
                        var jobID = objEmailCheckBoxes.id;
                        jobID = jobID.replace("JobID_", "");
                        objEmailCheckBoxes.checked = true;
                        var rowId = "_row__" + i;
                        document.getElementById(rowId).className = 'Checkboxunselectrow';

                    }
                }
                else {
                    for (var i = startIndex; i < endIndex; i++) {
                        try {
                            //alert("A");
                            var jobID = objEmailCheckBoxes[i].id;
                            //alert("B");
                            jobID = jobID.replace("JobID_", "");
                            objEmailCheckBoxes[i].checked = true;
                            var rowId = "_row__" + i;
                            document.getElementById(rowId).className = 'Checkrowselect';
                        }
                        catch (Error) {
                            //alert(Error.Message);
                        }
                    }
                }
            }
            catch (err) {
            }
            ResetTrackCounter();
        }

        function UnSelectAllJobs() {
            for (var i = 0; i < document.getElementById('jobList').elements.length; i++) {
                document.getElementById('jobList').elements[i].checked = false;
                var rowId = "_row__" + i;
                try {
                    //document.getElementById(rowId).style.backgroundColor = "#94ab6e";
                    document.getElementById(rowId).className = 'Checkboxunselectrow';
                }
                catch (Error) {
                }
            }
            ResetTrackCounter();
        }

        function SelectRow(rowID) {
            CloseCommunicator();
            document.sound(0);
            var ROWID = "_row__" + rowID;
            var CheckBoxID = "JobID_" + rowID;
            var checkBox = document.getElementById(ROWID);
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);
            ValidateSelectedCount();
            if (document.getElementById(CheckBoxID).checked) {
                //document.getElementById(ROWID).style.backgroundColor = "#6d814b";
                document.getElementById(ROWID).className = 'Checkrowselect';
                if (totalRecords == 1) {
                   
                    document.getElementById('chkALL').checked = true;
                    document.getElementById("PanelPrintActive").style.display = "inline";

                    document.getElementById("PanelPrintInActive").style.display = "block";
                }
            }
            else {
                //document.getElementById(ROWID).style.backgroundColor = "#94ab6e";
                document.getElementById(ROWID).className = 'Checkboxunselectrow';
                if (totalRecords == 1) {
                    document.getElementById('chkALL').checked = false;
                    document.getElementById("PanelPrintActive").style.display = "inline";

                    document.getElementById("PanelPrintInActive").style.display = "none";
                }
            }
            ResetTrackCounter();
        }

        function displayLimitsRemaining() {
            document.sound(0);
            document.getElementById("PanelCommunicator").style.display = "none";
            document.getElementById("displayLimits").style.display = "inline";
            clearTimeout(timerCloseCommunicator);
            timerCloseCommunicator = setTimeout('CloseCommunicator()', 10000);
            ResetTrackCounter();
        }

        function HighlightRow(controlID) {
            CloseCommunicator();
            document.sound(0);
            var rowID = controlID.replace("JobID_", "_row__");
            var checkBox = document.getElementById(controlID);
            var totalRecords = parseInt(document.getElementById("HiddenTotalJobs").value);

            if (checkBox.checked) {
                checkBox.checked = false;
                //document.getElementById(rowID).style.backgroundColor = "#94ab6e";
                document.getElementById(rowID).className = 'Checkboxunselectrow';
                if (totalRecords == 1) {
                    document.getElementById('chkALL').checked = false;
                    document.getElementById("PanelPrintActive").style.display = "inline";
                    document.getElementById("PanelPrintInActive").style.display = "none";
                }
                else {
                    ValidateSelectedCount();
                }
            }
            else {
                checkBox.checked = true;
                //document.getElementById(rowID).style.backgroundColor = "#6d814b";
                document.getElementById(rowID).className = 'Checkrowselect';
                if (totalRecords == 1) {
                    document.getElementById('chkALL').checked = true;
                    document.getElementById("PanelPrintActive").style.display = "inline";
                    document.getElementById("PanelPrintInActive").style.display = "block";
                }
                else {
                    ValidateSelectedCount();
                }
            }
            ResetTrackCounter();
        }

        function TrackUserInteraction() {
            var timerId = self.setInterval("StartTimer()", 1000);
            document.getElementById("TimerID").value = timerId;
        }

        function StartTimer() {
            var elapsedTime = parseInt(document.getElementById("ElapsedTime").value);
            elapsedTime = elapsedTime + 1;
            document.getElementById("ElapsedTime").value = elapsedTime;
            var timeOut = document.getElementById("HiddenFieldIntervalTime").value;
            if (elapsedTime >= timeOut) {
                ClearTimer();
                location.href = "../Mfp/LogOn.aspx";
            }
        }

        function ResetTrackCounter() {
            document.getElementById("ElapsedTime").value = "0";
        }

        function ClearTimer() {
            var timeId = document.getElementById("TimerID").value;
            if (timeId != "") {
                self.clearInterval(timeId);
            }
        }

    </script>
</head>
<style type="text/css">
     <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
</style>
<body leftmargin="0" topmargin="0" scroll="NO" onload="LoadPageImages(),PageShowingEve(),TrackUserInteraction()"
    class="InsideBG">
    <div style="display: inline; width: 500px; left: 30px; z-index: 1; position: absolute"
        id="PageLoadingID">
        <table cellpadding="0" cellspacing="0" border="0" width="300" height="200">
            <tr>
                <td align="left" style="padding-left: 5px;" valign="middle">
                    <asp:Image ID="ImagePageLoading" runat="server" />
                </td>
                <td align="left" style="padding-left: 5px;" valign="middle" class="Login_TextFonts">
                    <asp:Label ID="LabelPageLoading" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none" id="PageShowingID">
        <form id="jobList" runat="server">
        <input type="hidden" size="4" id="ElapsedTime" value="0" />
        <input type="hidden" size="4" value="" id="TimerID" />
        <asp:HiddenField ID="HiddenFieldIntervalTime" runat="server" Value="0" />
        <input type="hidden" value="1" name="currentPage" id="currentPage" runat="server" />
        <asp:HiddenField ID="HiddenRecordsPerPage" Value="5" runat="server" />
        <asp:HiddenField ID="HiddenTotalJobs" Value="0" runat="server" />
        <asp:HiddenField ID="HiddenFieldPrintText" runat="server" />
        <div runat="server" id="Communicator">
            <asp:Panel ID="PanelCommunicator" Visible="false" CssClass="CommunicatorPannel" Width="100%"
                runat="server">
                <table width="100%" align="center" border="0" class="Error_msgTable" cellpadding="0"
                    cellspacing="0">
                    <tr class="Error_msgcenter">
                        <td align="center" style="width: 90%;">
                            <asp:Label ID="LabelCommunicatorMessage" Font-Names="Verdana,Arial" Font-Bold="true"
                                ForeColor="Red" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" valign="middle" width="10%">
                            <asp:Image ID="imageErrorClose" runat="server" onclick="CloseCommunicator()" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div runat="server" id="displayLimits" style="display: none;">
            <asp:Panel ID="PanelDisplayLimits" HorizontalAlign="Center" CssClass="CommunicatorPannel_Limits"
                Width="60%" runat="server">
                <table width="100%" align="center" border="0" class="Error_msgTable_Limits" cellpadding="2"
                    cellspacing="2">
                    <tr class="Login_TextFonts" height="10">
                        <td align="center" style="width: 40%">
                            <asp:Label ID="LabelJobType" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelLimitsAvailable" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelOverDraft" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="imageErrorClose2" runat="server" onclick="CloseCommunicator()" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 40%">
                            <asp:Label ID="LabelPrintColorText" runat="server" Text=""></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelPrintColor" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelPrintColorAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 40%">
                            <asp:Label ID="LabelPrintMonochromeText" runat="server" Text=""></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelPrintMonochrome" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelPrintMonochromeAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 40%">
                            <asp:Label ID="LabelSCanColorText" runat="server" Text=""></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelScanColor" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelScanColorAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 40%">
                            <asp:Label ID="LabelScanMonochromeText" runat="server" Text=""></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelScanMonochrome" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelScanMonochromeAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 40%">
                            <asp:Label ID="LabelCopyColorText" runat="server" Text=""></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelCopyColor" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelCopyColorAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 40%">
                            <asp:Label ID="LabelCopyMonochromeText" runat="server" Text=""></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelCopyMonochrome" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelCopyMonochromeAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 44%">
                            <asp:Label ID="LabelDocFilingPrintColorText" runat="server" Text="Doc Filing Print Color"></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelDocFilingPrintColor" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelDocFilingPrintColorAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 44%">
                            <asp:Label ID="LabelDocFilingPrintBWText" runat="server" Text="Doc Filing Print BW"></asp:Label>:
                        </td>
                        <td align="center" style="width: 28%">
                            <asp:Label ID="LabelDocFilingPrintBW" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 28%">
                            <asp:Label ID="LabelDocFilingPrintBWAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 44%">
                            <asp:Label ID="LabelDocFilingColorText" runat="server" Text="Doc Filing Scan Color"></asp:Label>:
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelDocFilingColor" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 30%">
                            <asp:Label ID="LabelDocFilingColorAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 44%">
                            <asp:Label ID="LabelDocFilingMonochromeText" runat="server" Text="Doc Filing Scan BW"></asp:Label>:
                        </td>
                        <td align="center" style="width: 28%">
                            <asp:Label ID="LabelDocFilingMonochrome" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 28%">
                            <asp:Label ID="LabelDocFilingMonochromeAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Login_TextFonts">
                        <td align="right" style="width: 44%">
                            <asp:Label ID="LabelFaxText" runat="server" Text="FAX"></asp:Label>:
                        </td>
                        <td align="center" style="width: 28%">
                            <asp:Label ID="LabelFax" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" style="width: 28%">
                            <asp:Label ID="LabelFaxAllowedOD" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div runat="server" id="JobDelete">
            <asp:Panel ID="PanelJobDelete" runat="server" Visible="false" CssClass="CommunicatorPannel"
                Width="100%">
                <table width="100%" align="center" border="0" class="Error_msgTable" cellpadding="0"
                    cellspacing="0">
                    <tr class="Error_msgcenter" align="center">
                        <td colspan="3" width="100%">
                            <asp:Label ID="LabelDeleteJob" Font-Names="Verdana,Arial" Font-Bold="true" ForeColor="Red"
                                runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="Error_msgcenter">
                        <td align="right" valign="middle" width="49%">
                            <asp:LinkButton ID="LinkButtonCancel" OnClick="LinkButtonCancel_Click" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" height="38" width="65%">
                                    <tr>
                                        <td width="2%" align="right" valign="top" class="Button_Left">
                                        </td>
                                        <td width="96%" align="center" valign="middle" class="Button_center">
                                            <div class="Login_TextFonts">
                                                <asp:Label ID="LabelCancel" runat="server" Text=""></asp:Label>
                                            </div>
                                        </td>
                                        <td width="2%" align="left" valign="middle" class="Button_Right">
                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </td>
                        <td width="2%">
                        </td>
                        <td align="left" width="49%">
                            <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" height="38" width="65%">
                                    <tr>
                                        <td width="2%" align="right" valign="top" class="Button_Left">
                                        </td>
                                        <td width="96%" align="center" valign="middle" class="Button_center">
                                            <div class="Login_TextFonts">
                                                <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                            </div>
                                        </td>
                                        <td width="2%" align="center" valign="middle" class="Button_Right">
                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonContinue" OnClick="LinkButtonContinue_Click" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" height="35" width="100%">
                                    <tr>
                                        <td width="2%" align="right" valign="top" class="Button_Left">
                                        </td>
                                        <td width="96%" align="center" valign="middle" class="Button_center">
                                            <div class="Login_TextFonts">
                                                <asp:Label ID="LabelContinue" runat="server" Text=""></asp:Label>
                                            </div>
                                        </td>
                                        <td width="2%" align="center" valign="middle" class="Button_Right">
                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" height="368" id="TablePage" runat="server">
            <tr>
                <td height="50" align="left" valign="top">
                    <!-- Title Bar and Logout and Delete button-->
                    <asp:Table Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0" Height="50"
                        border="0" runat="server">
                        <asp:TableRow>
                            <asp:TableCell Width="48%" CssClass="Inside_TOPTitleFontBold">
                                &nbsp;<asp:Label ID="LabelJobListPageTitle" runat="server" Text=""></asp:Label>
                                <span class="Inside_TOPTitleUserFontBold">
                                    <asp:Label ID="LabelUserName" runat="server" Text=""></asp:Label>
                                </span>
                            </asp:TableCell>
                            <asp:TableCell Width="18%">
                                <div id="PanelPrintActive" style="display: inline">
                                    <asp:LinkButton ID="LinkButtonPrint" OnClick="ImageButtonPrintNow_Click" runat="server">
                                        <table width="82%" border="0" cellpadding="0" cellspacing="0" height="38">
                                            <tr>
                                                <td width="4%" align="left" valign="middle" class="Button_Left">
                                                </td>
                                                <td width="75%" align="center" valign="middle" class="Button_center">
                                                    <div class="Login_TextFonts">
                                                        <asp:Label ID="LabelPrint" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                                <td width="3%" align="left" valign="middle" class="Button_Right">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:LinkButton>
                                </div>
                            </asp:TableCell>
                            <asp:TableCell Width="17%" HorizontalAlign="Center">
                                <asp:LinkButton ID="LinkButtonDeviceMode" OnClick="LinkButtonDeviceMode_Click" runat="server">
                                    <table width="82%" border="0" cellpadding="0" cellspacing="0" height="38">
                                        <tr>
                                            <td width="4%" align="left" valign="top" class="Button_Left">
                                            </td>
                                            <td width="75%" align="center" valign="middle" class="Button_center">
                                                <div runat="server" id="divMFPMode" class="Login_TextFonts">
                                                    <asp:Label ID="LabelMFPMode" runat="server" Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td width="3%" align="left" valign="middle" class="Button_Right">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </asp:TableCell>
                            <asp:TableCell Width="16%" ID="TableCellLogOff" HorizontalAlign="right" VerticalAlign="middle">
                                <asp:LinkButton ID="LinkButtonLogOff" OnClick="LinkButtonLogOff_Click" runat="server">
                                    <table width="83%" border="0" cellpadding="0" cellspacing="0" height="38">
                                        <tr>
                                            <td width="5%" align="left" valign="middle" class="Button_Left">
                                            </td>
                                            <td width="75%" align="center" valign="middle" class="Button_center">
                                                <div id="divLogOut" runat="server" class="Login_TextFonts">
                                                    <asp:Label ID="LabelLogOut" runat="server" Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td width="3%" align="left" valign="middle" class="Button_Right">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </asp:TableCell>
                            <asp:TableCell Width="1%">
                                &nbsp;
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td height="2" class="HR_Line">
                </td>
            </tr>
            <!--End of the Title Bar and Logout and Delete button-->
            <tr>
                <td align="left" valign="top">
                    <!-- Table for Data Loading...-->
                    <table id="TablePrintJobs" width="100%" border="0" cellpadding="0" cellspacing="0"
                        runat="server">
                        <tr>
                            <td width="89%" align="left" valign="top">
                                <asp:Table ID="TableJobList" runat="server" Height="42" BorderWidth="0" CellPadding="0"
                                    CellSpacing="0">
                                    <asp:TableRow CssClass="Title_bar_bg" Height="42" TabIndex="0">
                                        <asp:TableCell Width="30" VerticalAlign="Middle">
                                        <input id="chkALL" onclick="ChkandUnchk()" type="checkbox" />
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellCheckBox" Width="2" runat="server" class="Vr_Line_Insidepage"></asp:TableCell>
                                        <asp:TableCell VerticalAlign="Middle">
                                            <table width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LabelSellectAll" runat="server" Text="" CssClass="JoblistFont"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelJobName" runat="server" Text="" CssClass="JoblistFont"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Image ID="ImageJobNameSortMode" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellJobName" Width="2" runat="server" class="Vr_Line_Insidepage"></asp:TableCell>
                                        <asp:TableCell VerticalAlign="Middle">
                                            <table width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="LabelJobDate" runat="server" Text="" CssClass="JoblistFont"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Image ID="ImageJobDateSortMode" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellDate" Width="2" runat="server" class="Vr_Line_Insidepage"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                            <td width="10%" align="left" valign="top">
                                <table id="TablePageControls" width="100%" border="0" cellpadding="0" cellspacing="0"
                                    runat="server">
                                    <tr>
                                        <td align="center" class="Page_Nag_whiteFont">
                                            <asp:Label ID="LabelPage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle">
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <div id="PreviousEnabled" style="display: none">
                                                            <a href="javascript:GetPreviousPage()">
                                                                <asp:Image ID="ImageActiveUpArrow" runat="server" />
                                                            </a>
                                                        </div>
                                                        <div id="PreviousDisabled" style="display: block">
                                                            <asp:Image ID="ImageDisableUpArrow" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="height: 60" class="Page_Nag_whiteFont">
                                                        <asp:Label ID="LabelPaginationInformation" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table id="BottomNavigationControl">
                                                            <tr>
                                                                <td>
                                                                    <div id="NextEnabled" style="display: block">
                                                                        <a href="javascript:GetNextPage()">
                                                                            <asp:Image ID="ImageActiveDownArrow" runat="server" />
                                                                        </a>
                                                                    </div>
                                                                    <div id="NextDisabled" style="display: none">
                                                                        <asp:Image ID="ImageDisableDownArrow" runat="server" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <!-- End of the Table for Data Loading...-->
                </td>
            </tr>
            <tr>
                <td height="2" class="HR_Line">
                </td>
            </tr>
            <tr>
                <td height="40" align="left" valign="bottom">
                    <!-- Delete, Setting,Print Now and Paging button location-->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" height="50">
                        <tr>
                            <td width="48%" align="left">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" height="50">
                                    <tr>
                                        <td width="50%" align="center">
                                            <asp:LinkButton ID="LinkButtonDelete" OnClick="ImageButtonDeleteJobs_Click" runat="server">
                                                <table width="82%" border="0" cellpadding="0" cellspacing="0" height="38">
                                                    <tr>
                                                        <td width="4%" align="left" valign="middle" class="Button_Left">
                                                        </td>
                                                        <td width="75%" align="center" valign="middle" class="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelDelete" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td width="3%" align="left" valign="middle" class="Button_Right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:LinkButton>
                                        </td>
                                        <td width="50%" align="left">
                                            <asp:LinkButton ID="LinkButtonPrintAndDelete" OnClick="ImageButtonPrintAndDelete_Click"
                                                runat="server">
                                                <table width="82%" border="0" cellpadding="0" cellspacing="0" height="38">
                                                    <tr>
                                                        <td width="4%" align="left" valign="middle" class="Button_Left">
                                                        </td>
                                                        <td width="75%" align="center" valign="middle" class="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelPrintAndDelete" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td width="3%" align="left" valign="middle" class="Button_Right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%">
                            </td>
                            <td width="48%" align="right">
                                <table width="90%" border="0" cellpadding="0" cellspacing="0" height="50">
                                    <tr>
                                        <td width="50%" align="center">
                                            <div id="PanelPrintInActive" style="display: none">
                                                <asp:LinkButton ID="LinkButtonPrintOptions" OnClick="ImageButtonPrintOptions_Click"
                                                    runat="server">
                                                    <table width="82%" border="0" cellpadding="0" cellspacing="0" height="38">
                                                        <tr>
                                                            <td width="4%" align="left" valign="middle" class="Button_Left">
                                                            </td>
                                                            <td width="75%" align="center" valign="middle" class="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelPrintOptions" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td width="3%" align="left" valign="middle" class="Button_Right">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:LinkButton>
                                            </div>
                                        </td>
                                        <td width="50%" align="left">
                                            <asp:LinkButton ID="LinkButtonFastPrint" EnableViewState="false" OnClick="LinkButtonFastPrint_Click"
                                                runat="server">
                                                <table width="82%" border="0" cellpadding="0" cellspacing="0" height="38">
                                                    <tr>
                                                        <td width="4%" align="left" valign="middle" class="Button_Left">
                                                        </td>
                                                        <td width="75%" align="center" valign="middle" class="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelFastPrint" runat="server" Text=""></asp:Label></div>
                                                        </td>
                                                        <td width="3%" align="left" valign="middle" class="Button_Right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%">
                            </td>
                        </tr>
                    </table>
                    <!--End of the  Delete, Setting,Print Now and Paging button location-->
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        function PageShowingEve() {
            setTimeout(PageShowing(), 50000);
        }
        function PageShowing() {
            document.getElementById("PageLoadingID").style.display = "none";
            document.getElementById("PageShowingID").style.display = "inline";
        }
    
    </script>
</body>
</html>
