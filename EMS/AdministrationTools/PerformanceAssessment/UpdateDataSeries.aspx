<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="UpdateDataSeries.aspx.cs"
    Inherits="Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment.UpdateDataSeries"
    Title="Update Data Series" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script type="text/javascript">
        var cellSingleClick = false;
        var cellDoubleClick = false;
        var selectedCellId;
        var doubleSlectedId;
        var rows;
        var colls;
        var enterPressed = false;

        // Selects the cell in double selection mode.
        function cellDoubleClickFunction(id) {
            cellDoubleClick = true;
            cellSingleClick = false;
            unselectAllCells();
            var textBox = $get(id);
            if (textBox != null) {
                textBox.parentNode.className = "doubleSelect";
                textBox.focus();
                textBox.select();
                doubleSlectedId = id;
            }
        }

        // Selects the cell in single selection mode.
        function cellClick(id) {
            if (id != doubleSlectedId) {
                doubleSlectedId = null;
                cellDoubleClick = false;
                cellSingleClick = true;
                selectedCellId = id;
                unselectAllCells();
                var textBox = $get(id);

                if (textBox != null) {
                    textBox.parentNode.className = "doubleSelect";   //"singleSelect";
                    var radGrid = $find(radGridId).get_element();
                    radGrid.focus();
                    textBox.select();
                }
            }
        }

        function fireCommand() {
            $find(radGridId).get_masterTableView().fireCommand("InsertNewRow", selectedCellId);
        }

        function onKeyDown(sender, eventArgs) {
            var keyCode = eventArgs.keyCode;
            //    debugger;
            switch (keyCode) {
                case 8:
                    {
                        var textBox = $get(selectedCellId);
                        if (!textBox.readOnly && cellSingleClick) {
                            textBox.value = "";
                        }
                        if (window.event.stopPropagation) {
                            window.event.stopPropagation();
                            window.event.preventDefault();
                        }
                    }
                    break;
                case 9:
                    {
                        //TabKeyPressed();
                        //Esto es porque solo tenemos 1 Columna editable, entonces lo que hacemos es ir para abajo!
                        //                if (cellSingleClick) {
                        //                    MoveDown();
                        //                }

                        doubleSlectedId = null;
                        cellClick(selectedCellId);
                        setTimeout(function() {
                            MoveDown();
                        }, 0); ;
                    }
                    break;
                case 13:
                    {
                        if (eventArgs.shiftKey) {
                            MoveUp();
                        }
                        else {

                            doubleSlectedId = null;
                            cellClick(selectedCellId);
                            setTimeout(function() {
                                MoveDown();
                            }, 0); ;
                        }
                    }
                    break;
                case 37:
                    {
                        if (cellSingleClick) {
                            //MoveLeft();
                            //Esto es porque solo tenemos 1 Columna editable, entonces lo que hacemos es ir para abajo!
                            MoveUp();
                        }
                    }
                    break;
                case 38:
                    {
                        if (cellSingleClick) {
                            MoveUp();
                        }
                    }
                    break;
                case 39:
                    {
                        if (cellSingleClick) {
                            //MoveRight();
                            //Esto es porque solo tenemos 1 Columna editable, entonces lo que hacemos es ir para abajo!
                            MoveDown();
                        }
                    }
                    break;
                case 40:
                    {
                        if (cellSingleClick) {
                            MoveDown();
                        }
                    }
                    break;
                default:
                    {
                        if (cellSingleClick) {
                            cellDoubleClickFunction(selectedCellId);
                        }
                    }
                    break;
            }
        }
        // Unselects all selected cells.
        function unselectAllCells() {
            var grid = $find(radGridId).get_element();
            var oldSelectedElementClick = $telerik.getElementByClassName(grid, "doubleSelect", "td");
            if (oldSelectedElementClick != null && oldSelectedElementClick != undefined) {
                oldSelectedElementClick.className = "";
            }
            var oldSelectedElementDoubleClick = $telerik.getElementByClassName(grid, "singleSelect", "td");
            if (oldSelectedElementDoubleClick != null && oldSelectedElementDoubleClick != undefined) {
                oldSelectedElementDoubleClick.className = "";
            }
        }

        var tmpI;
        var tmpJ;
        var indexI;
        var indexJ;
        var firstPart;
        var secondPart;
        var firstRowPartId = "04";
        var firtsCollPartId = "00";
        var leadingZero = "0";

        // Selects the cell above.
        function MoveUp() {
            SetIdParts();
            var newSelectedCellId;
            if (tmpI == 4) {
                newSelectedCellId = firstPart + firstRowPartId + secondPart + indexJ;
                cellClick(newSelectedCellId);
                return;
            }
            else {
                tmpI = tmpI - 1;
            }
            CheckParts();
            newSelectedCellId = firstPart + indexI + secondPart + indexJ;
            cellClick(newSelectedCellId);

            var textBox = $get(selectedCellId);
            var row = textBox.parentNode.parentNode;
            MoveScrollPosition(row);
        }
        // Selects the cell down bellow.
        function MoveDown() {

            SetIdParts();
            if (tmpI == (rows + 3)) {
                //fireCommand();
                tmpI = 4;
                //        return;
            }
            else {
                tmpI = tmpI + 1;
            }
            CheckParts();
            var newSelectedCellId = firstPart + indexI + secondPart + indexJ;
            cellClick(newSelectedCellId);


            var textBox = $get(selectedCellId);
            var row = textBox.parentNode.parentNode;
            MoveScrollPosition(row);
        }
        // Selects left cell.
        function MoveLeft() {
            SetIdParts();
            var newSelectedCellId;
            // If selected cell is the first cell of the row then selects the last cell of the row above. 
            if ((tmpI == 4) && (tmpJ == 0)) {
                newSelectedCellId = firstPart + firstRowPartId + secondPart + firtsCollPartId;
                cellClick(newSelectedCellId);
                return;
            }
            else {
                if (tmpJ == 0) {
                    tmpJ = tmpJ + colls - 1;
                    tmpI = tmpI - 1;
                }
                else {
                    tmpJ = tmpJ - 1;
                }
            }
            CheckParts();
            newSelectedCellId = firstPart + indexI + secondPart + indexJ;
            cellClick(newSelectedCellId);
        }
        // Selects right cell.
        function MoveRight() {
            SetIdParts();
            if ((tmpI == (rows + 3)) && (tmpJ == (colls - 1))) {
                selectedCellId = firstPart + tmpI + secondPart + firtsCollPartId;
                MoveDown();
                return;
            }
            else {
                if (tmpJ == (colls - 1)) {
                    tmpI = tmpI + 1;
                    tmpJ = 0;
                }
                else {
                    tmpJ = tmpJ + 1;
                }
                CheckParts();
                var newSelectedCellId = firstPart + indexI + secondPart + indexJ;
                cellClick(newSelectedCellId);
            }
        }

        // Parse the temporary ID's and assaign them to the variables.
        function SetIdParts() {
            var indexOfLastUnderscore = selectedCellId.lastIndexOf("_");
            var subStr = selectedCellId.substr(0, indexOfLastUnderscore);
            var indexOfLastLetter = subStr.lastIndexOf("l");
            var subStrTempI = selectedCellId.substr(indexOfLastLetter + 1, subStr.length - 1 - indexOfLastLetter);
            var subStrTempJ = selectedCellId.substr(indexOfLastUnderscore + 4, selectedCellId.length - 1 - indexOfLastUnderscore);

            // If the row is of the top 6 rows, the id starts with leading 0(zero) symbol.
            // Perform checks to avoid wrong results from parse() function.
            if (subStrTempI[0] == leadingZero) {
                tmpI = parseInt(subStrTempI.substr(1, subStrTempI.length - 1));
            }
            else {
                tmpI = parseInt(subStrTempI);
            }
            // If the column is of the first 10 columns, the id starts with leading 0(zero) symbol.
            // Perform checks to avoid wrong results from parse() function.
            if (subStrTempJ[0] == leadingZero) {
                tmpJ = parseInt(subStrTempJ.substr(1, subStrTempJ.length - 1));
            }
            else {
                tmpJ = parseInt(subStrTempJ);
            }

            firstPart = selectedCellId.substr(0, indexOfLastLetter + 1);
            secondPart = selectedCellId.substr(indexOfLastUnderscore, selectedCellId.length - indexOfLastUnderscore - 2);
        }

        // Check temporary IDs. If they are less 10 appends the leading zeros.
        function CheckParts() {
            if (tmpI < 10) {
                indexI = leadingZero + tmpI.toString();
            }
            else {
                indexI = tmpI.toString();
            }
            if (tmpJ < 10) {
                indexJ = leadingZero + tmpJ.toString();
            }
            else {
                indexJ = tmpJ.toString();
            }
        }

        function TabKeyPressed() {
            MoveRight();
        }

        function MoveScrollPosition(rowElement) {
            //    setTimeout(function() {
            //        //get the main table scrollArea HTML element
            //        var scrollArea = $find(radGridId).GridDataDiv;

            //        //check if the selected row is below the viewable grid area
            //        if ((rowElement.offsetTop - scrollArea.scrollTop) +
            //       rowElement.offsetHeight + 20 > scrollArea.offsetHeight) {
            //            //scroll down to selected row
            //            scrollArea.scrollTop = scrollArea.scrollTop +
            //        (rowElement.offsetTop - scrollArea.scrollTop) +
            //        (rowElement.offsetHeight - scrollArea.offsetHeight - 20) +
            //        rowElement.offsetHeight;
            //        }
            //        //check if the selected row is above the viewable grid area
            //        else if ((rowElement.offsetTop - scrollArea.scrollTop) < 0) {
            //            //scroll the selected row to the top
            //            scrollArea.scrollTop = rowElement.offsetTop;
            //        }
            //    }, 10);
        }    
    </script>

    <style type="text/css">
        .RadGrid
        {
            border: none !important;
        }
        .RadGrid .rgCommandRow td
        {
        	border:0px;
        }
        .Calendar
        {
        	display:none;
        }
        .RadGrid:active, .RadGrid:focus
        {
            outline-style: none !important;
            -moz-outline-style: none !important;
        }
        .rgEditRow
        {
            background: none !important;
        }
        .rgRow td td td, .rgAltRow td td td, .rgEditRow td td td
        {
            border: 1px solid #CFD9E7 !important;
            padding-left: 0 !important;
            padding-right: 2px !important;
            padding-bottom: 0px !important;
            padding-top: 0px !important;
        }
       .rgRow td td, .rgAltRow td td, .rgEditRow td td
        {
            border: 0px solid #CFD9E7 !important;
        }
        .rgRow td, .rgAltRow td, .rgEditRow td
        {
        	border-bottom: 1px solid #CFD9E7 !important;
        	border-right: 1px solid #CFD9E7 !important;
            padding-left: 5 !important;
            padding-right: 2px !important;
            padding-bottom: 1px !important;
            padding-top: 3px !important;
        }
        .rgRow .singleSelect, .rgAltRow .singleSelect, .rgEditRow .singleSelect, .rgRow .doubleSelect, .rgAltRow .doubleSelect, .rgEditRow .doubleSelect
        {
            border: 1px solid #01144E !important;
        }
        .rgRow .doubleSelect, .rgAltRow .doubleSelect, .rgEditRow .doubleSelect
        {
            background: #CDFFB1 !important;
        }
        .rgRow input, .rgAltRow input, .rgEditRow input
        {
            background: none transparent;
            /*width: 100%;*/
            border: 1px solid #ccc;
            height: 100%;
            border: 0;
        }
    </style>
    <rad:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            var radGridId = "<%=rdgDataSeries.ClientID %>";
        </script>

    </rad:RadCodeBlock>

    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentFormReport">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                    </colgroup>
                    <!-- Activity -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblActivity" runat="server" Text="Accounting Activity:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblActivityValue" runat="server" />
                        </td>
                    </tr>
                    <!-- Measurement -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurement" runat="server" Text="Measurement:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblMeasurementValue" runat="server" />
                        </td>
                    </tr>
                    <!-- Indicator -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicator" runat="server" Text="Indicator:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIndicatorValue" runat="server" />
                        </td>
                    </tr>
                    <!-- Parameter -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameter" runat="server" Text="Parameter:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblParameterValue" runat="server" />
                        </td>
                    </tr>
                    <!-- Device -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDevice" runat="server" Text="Device:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblDeviceValue" runat="server" />
                        </td>
                    </tr>
                    <!-- Measurement Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnit" runat="server" Text="Measurement Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblMeasurementUnitValue" runat="server" />
                        </td>
                    </tr>
                </table>
                <div style="float: left">
                </div>
   
                <div class="ConteinerReport">
                    <br />
                    <br />
                    <asp:UpdatePanel ID="upGridSeries" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Label ID="lblEntitySeries" CssClass="Title" runat="server" />
                            <asp:PlaceHolder runat="server" ID="pchControls"></asp:PlaceHolder>
                            <rad:RadGrid ID="rdgDataSeries" OnPreRender="rdgDataSeries_PreRender" OnDataBinding="rdgDataSeries_DataBinding"
                                OnNeedDataSource="rdgDataSeries_NeedDataSource" OnItemDataBound="rdgDataSeries_ItemDataBound" Skin="EMS" runat="server">
                                <MasterTableView EditMode="InPlace" CommandItemDisplay="Top" AutoGenerateColumns="true">
                                    <CommandItemTemplate>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <ClientSettings>
                                </ClientSettings>
                            </rad:RadGrid>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
