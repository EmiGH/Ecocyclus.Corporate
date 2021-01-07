<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessTaskExecutionMeasurementsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PM.ProcessTaskExecutionMeasurementsProperties" Title="EMS - Process Task Execution Measurements Properties"
    Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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

    <rad:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            var radGridId = "<%=rdgDataSeries.ClientID %>";
        </script>

    </rad:RadCodeBlock>
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <asp:UpdatePanel ID="uPanelView" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="contentListViewReportTable">
                            <tr>
                                <td class="Picture" id="tdPicture">
                                    <rad:RadChart Width="490px" Height="450px" ID="chartMeasurementSeries" runat="server"
                                        AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false" AutoTextWrap="false"
                                        OnItemDataBound="chartMeasurementSeries_ItemDataBound">
                                        <Appearance FillStyle-FillType="Solid" FillStyle-MainColor="#ffffff">
                                            <Border Color="Transparent" />
                                            <FillStyle MainColor="Transparent" SecondColor="Transparent" GammaCorrection="false"
                                                FillType="Solid">
                                            </FillStyle>
                                        </Appearance>
                                        <ChartTitle>
                                            <Appearance Border-Color="Transparent" FillStyle-MainColor="Transparent">
                                                <FillStyle MainColor="Transparent">
                                                </FillStyle>
                                                <Border Color="Transparent" />
                                            </Appearance>
                                            <TextBlock Appearance-TextProperties-Color="#4a5678">
                                                <Appearance TextProperties-Color="74, 86, 120" TextProperties-Font="Arial, 12px"
                                                    Position-AlignedPosition="TopLeft" AutoTextWrap="Auto">
                                                </Appearance>
                                            </TextBlock>
                                        </ChartTitle>
                                        <Legend>
                                            <Appearance Visible="false">
                                                <ItemTextAppearance TextProperties-Color="Black">
                                                </ItemTextAppearance>
                                                <ItemMarkerAppearance Figure="Square">
                                                </ItemMarkerAppearance>
                                                <Border Color="#cccccc" />
                                            </Appearance>
                                        </Legend>
                                        <Series>
                                            <rad:ChartSeries Name="Series 1">
                                                <Appearance LegendDisplayMode="Nothing" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                                    <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                                    </FillStyle>
                                                    <LabelAppearance RotationAngle="-90">
                                                    </LabelAppearance>
                                                    <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                                    </TextAppearance>
                                                    <Border Color="69, 115, 167" />
                                                </Appearance>
                                            </rad:ChartSeries>
                                        </Series>
                                        <PlotArea>
                                            <XAxis DataLabelsColumn="MeasurementDate">
                                                <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                                    <MajorGridLines Color="134, 134, 134" Width="0" />
                                                    <LabelAppearance RotationAngle="-90">
                                                    </LabelAppearance>
                                                    <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                                    </TextAppearance>
                                                </Appearance>
                                                <AxisLabel>
                                                    <TextBlock>
                                                        <Appearance TextProperties-Color="Black">
                                                        </Appearance>
                                                    </TextBlock>
                                                </AxisLabel>
                                                <Items>
                                                    <rad:ChartAxisItem>
                                                    </rad:ChartAxisItem>
                                                    <rad:ChartAxisItem Value="1">
                                                    </rad:ChartAxisItem>
                                                    <rad:ChartAxisItem Value="2">
                                                    </rad:ChartAxisItem>
                                                    <rad:ChartAxisItem Value="3">
                                                    </rad:ChartAxisItem>
                                                    <rad:ChartAxisItem Value="4">
                                                    </rad:ChartAxisItem>
                                                    <rad:ChartAxisItem Value="5">
                                                    </rad:ChartAxisItem>
                                                    <rad:ChartAxisItem Value="6">
                                                    </rad:ChartAxisItem>
                                                    <rad:ChartAxisItem Value="7">
                                                    </rad:ChartAxisItem>
                                                </Items>
                                            </XAxis>
                                            <YAxis>
                                                <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" MinorTick-Color="134, 134, 134">
                                                    <MajorGridLines Color="134, 134, 134" />
                                                    <MinorGridLines Color="134, 134, 134" />
                                                    <TextAppearance TextProperties-Color="Black">
                                                    </TextAppearance>
                                                </Appearance>
                                                <AxisLabel>
                                                    <TextBlock>
                                                        <Appearance TextProperties-Color="Black">
                                                        </Appearance>
                                                    </TextBlock>
                                                </AxisLabel>
                                            </YAxis>
                                            <Appearance>
                                                <FillStyle FillType="Solid" MainColor="">
                                                </FillStyle>
                                            </Appearance>
                                        </PlotArea>
                                    </rad:RadChart>
                                </td>
                                <td class="Space2">
                                    &nbsp;
                                </td>
                                <td class="MainData">
                                    <%--Va la grilla de viewer para el MainData--%>
                                    <asp:Label ID="lblTitleMainData" runat="server" Text="Main Data" CssClass="lblTitleMainData" />
                                    <asp:Panel ID="pnlListViewerMainData" runat="server">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <table cellpadding="0" cellspacing="0" class="tblStaticGrid">
                    <tr class="trHeader">
                        <td rowspan="2" style="width: 50%; vertical-align: middle;">
                            <p>
                                <asp:Label ID="lblLastValue" runat="server"></asp:Label></p>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td style="border: solid 1px #ccc; padding: 6px; border-right: solid 0px; border-bottom: solid 0px">
                            <asp:Label ID="lblValidityPeriod" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" class="tblStaticGrid">
                    <tr class="trHeaderLeft">
                        <td>
                            <asp:Label ID="lblLastMeasurementDate" runat="server" Text="Last Measurement Date:" />
                        </td>
                        <td>
                            <asp:Label ID="lblLastMeasurementValueTitle" runat="server" Text="Last Measurement Value:" />
                        </td>
                        <td>
                            <asp:Label ID="lblLastMeasurementStartDate" runat="server" Text="Last Measurement Start Date:" />
                        </td>
                        <td>
                            <asp:Label ID="lblLastMeasurementEndDate" runat="server" Text="Last Measurement End Date:"
                                Style="padding: 10px 0 20px 0" />
                        </td>
                    </tr>
                    <tr class="trBody">
                        <td>
                            <asp:Label ID="lblLastMeasurementDateValue" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblLastMeasurementValue" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblLastMeasurementStartDateValue" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblLastMeasurementEndDateValue" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:UpdatePanel ID="upGridSeries" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:PlaceHolder runat="server" ID="pchControls"></asp:PlaceHolder>
                        <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #66789b;
                            text-align: center; color: #fff; font-weight: bold; border: solid #ccc 1px;">
                            <tr>
                                <td rowspan="2" style="width: 50%; vertical-align: middle; padding: 6px;">
                                    <p>
                                        <asp:Label runat="server" ID="lblMeasureValue"></asp:Label></p>
                                </td>
                                <td style="padding: 6px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="border: solid 1px #ccc; padding: 6px; border-right: solid 0px; border-bottom: solid 0px">
                                    <asp:Label ID="lblValidityPeriod1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="divDataSeries">
                            <rad:RadGrid ID="rdgDataSeries" OnPreRender="rdgDataSeries_PreRender" OnDataBinding="rdgDataSeries_DataBinding"
                                OnNeedDataSource="rdgDataSeries_NeedDataSource" OnItemDataBound="rdgDataSeries_ItemDataBound"
                                Skin="EMS" runat="server">
                                <MasterTableView EditMode="InPlace" CommandItemDisplay="Top" AutoGenerateColumns="true">
                                    <CommandItemTemplate>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <ClientSettings>
                                </ClientSettings>
                            </rad:RadGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Comment -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblComment" runat="server" Text="Comment:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtComment" Rows="6" runat="server" MaxLength="8000" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Upload File Measurement Attach -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementAttach" runat="server" Text="Measurement Attach:" />
                        </td>
                        <td class="ColContent">
                            <asp:FileUpload ID="fuMeasurementAttach" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
