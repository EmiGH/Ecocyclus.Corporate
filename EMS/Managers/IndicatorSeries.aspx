<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="IndicatorSeries.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Managers.IndicatorSeries" Title="Indicator Serires" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script src="../AppCode/JavaScriptCore/jquery-1.5.2.js" type="text/javascript"></script>

    <script src="../AppCode/JavaScriptCore/jquery.treeTable.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#ctl00_ContentMain_tblTreeTableReport").treeTable();
            $('#ctl00_ContentMain_tblTreeTableReport').expandAll();
        });
    </script>

    <script type="text/javascript">
        function ExportMeasurementSeries(e, idMeasurement, idTransformation) {
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;
//            StopEvent(e);

            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/Managers/ExportMeasurementSeries.aspx?IdMeasurement=" + idMeasurement + "&IdTransformation=" + idTransformation, 'Export', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else {   //FireFox
                var newWindow = window.parent.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/Managers/ExportMeasurementSeries.aspx?IdMeasurement=" + idMeasurement + "&IdTransformation=" + idTransformation, 'Export', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }

            newWindow.focus();
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            StopEvent(e);
        }
    </script>

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
                <br />
                <br />
                <!-- List -->
                <table class="tblTreeTableReport" id="tblTreeTableReport" runat="server" cellpadding="0"
                    cellspacing="0">
                    <thead>
                        <tr>
                            <th runat="server" id="title" class="rptColumnHeaderTitle">
                                Title
                            </th>
                            <th runat="server" id="Indicator" class="rptColumnHeaderResult">
                                Indicator
                            </th>
                            <th runat="server" id="Activity" class="rptColumnHeaderResult">
                                Activity
                            </th>
                            <th runat="server" id="Formula" class="rptColumnHeaderResult">
                                Formula
                            </th>
                            <th runat="server" id="Value" class="rptColumnHeaderResult">
                                Value
                            </th>
                            <th runat="server" id="Series" class="rptColumnHeaderResult">
                                Series
                            </th>
                            <th runat="server" id="View" class="rptColumnHeaderResult">
                                View
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbody">
                    </tbody>
                </table>
                <!-- FIN del Report -->
                <div class="ConteinerReport">
                    <br />
                    <br />
                    <asp:UpdatePanel ID="upGridSeries" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Label ID="lblEntitySeries" CssClass="Title" runat="server" />
                            <asp:LinkButton ID="lnkExport" runat="server" CssClass="lnkExport" Style="float: right;
                                padding-right: 13px; margin-bottom: 5px;" Text="Export"></asp:LinkButton>
                                <div style="clear:both;"></div>
                            <asp:PlaceHolder runat="server" ID="pchControls"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
    <!--Variables escondidas-->
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
    <input type="hidden" id="radMenuItemClicked" name="radMenuItemClicked" />
    <!--Variables escondidas-->
</asp:Content>
