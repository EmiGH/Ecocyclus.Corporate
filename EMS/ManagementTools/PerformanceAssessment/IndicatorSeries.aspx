<%@ Page Language="C#" MasterPageFile="~/EMSPopUp.Master" AutoEventWireup="true"
    CodeBehind="IndicatorSeries.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.IndicatorSeries"
    Title="Indicator Serires" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPopUp" runat="server">

    <script src="../../AppCode/JavaScriptCore/jquery-1.5.2.js" type="text/javascript"></script>

    <script src="../../AppCode/JavaScriptCore/jquery.treeTable.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#ctl00_ContentPopUp_tblTreeTableReport").treeTable();
            $('#ctl00_ContentPopUp_tblTreeTableReport').expandAll();
        });


        function NavigateToContent(sender, e) {
            var _strRequestParam;
            //Mostrar el reloj.
            //ShowUProgress();
            _strRequestParam = "PkCompost=" + sender.attributes["PkCompost"].value; //El PK, viene separado en pipe |,  para que no falle el tranfer a esta pagina.
            _strRequestParam += "&EntityName=" + sender.attributes["EntityName"].value
            _strRequestParam += "&EntityNameGrid=" + sender.attributes["EntityNameGrid"].value;
            _strRequestParam += "&EntityNameContextInfo=" + sender.attributes["EntityNameContextInfo"].value;
            _strRequestParam += "&EntityNameContextElement=" + sender.attributes["EntityNameContextElement"].value;
            _strRequestParam += "&Text=" + sender.attributes["Text"].value;

            _strRequestParam += "&";

            //Pagina de transicion entre el link del tooltip del mapa y el Viewer.
            window.open('../PerformanceAssessment/IndicatorSeriesNavigate.aspx?' + _strRequestParam, '_parent');

            StopEvent(e);
        }
    </script>

    <div class="ConteinerReport">
        <asp:Label ID="lblTitle" CssClass="Title" runat="server" Text="Indicator Series" />
        <asp:Label CssClass="SubTitle" ID="lblDetail" runat="server" Text="Series of indicators of data uploaded." />
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
                    <br />
                    <br />
                    <asp:UpdatePanel ID="upGridSeries" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Label ID="lblEntitySeries" CssClass="Title" runat="server" />
                            <br />
                            <asp:PlaceHolder runat="server" ID="pchControls"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <!--Variables escondidas-->
        <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
        <input type="hidden" id="radMenuItemClicked" name="radMenuItemClicked" />
        <!--Variables escondidas-->
    </div>
</asp:Content>
