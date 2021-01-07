<%@ Page Title="" Language="C#" MasterPageFile="~/EMSPopUpReport.Master" AutoEventWireup="true"
    CodeBehind="ListEmissionsByFacility.aspx.cs" Inherits="Condesus.EMS.WebUI.Managers.ListEmissionsByFacility" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPopUp" runat="server">
    <script type="text/javascript">
        function ShowLoading() {
            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
        }
    </script>

    <style type="text/css">
.tblTreeTableReport
        {
            width: 100%;
            margin-top: 20px;
            border: solid 1px #ccc;
        }
        .tblTreeTableReport th
        {
            background-color: #66789b; /*line-height: 28px;*/
            padding-bottom: 8px;
            padding-top: 8px;
            color: #fff;
            font-weight: bold;
            padding-left: 20px;
            padding-right: 20px;
        }
        .tblTreeTableReport tr
        {
            /*border: solid 1px #ccc;*/
        }
        .tblTreeTableReport .trImpar
        {
            background-color: #eee;
        }
        .tblTreeTableReport .trPar
        {
            background-color: #fff;
        }
        .tblTreeTableReport td
        {
            padding-left: 20px;
            padding-right: 20px;
            padding-bottom: 8px;
            padding-top: 8px;
        }
        .tblTreeTableReport tr:hover
        {
            background-color: #ffd671;
            color: #000;
        }
        .rptColumnHeaderTitle
        {
            text-align: left;
        }
        .DCGEI
        {
            text-align: right;
        }
        .DCCL
        {
            text-align: right;
        }
        .rptColumnHeaderResult
        {
            text-align: right;
        }        
        .Title
        {
            text-align: left;
            width:180px;
        }
    </style>
    
<div class="divContentReport">
    <div style="float: left;">
        <asp:Label ID="lblProcessValue" runat="server" CssClass="lblOrganizationValue"></asp:Label>
        <br />
        <asp:Label ID="lblReportFilter" runat="server" CssClass="ReportFilterSubTitle"></asp:Label>
        <br />
    </div>
    <asp:LinkButton ID="lnkExport" runat="server" CssClass="lnkPrint" Text="Export" OnClick="lnkExportGridMeasurement_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="lnkPrint" Text="Print" OnClientClick="javaScript:window.print();return false;"></asp:LinkButton>
    <asp:UpdatePanel ID="upReportGrid" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <!-- Report -->
            <table class="tblTreeTableReport" id="tblTreeTableReport" runat="server" cellpadding="0"
                cellspacing="0">
                <thead>
                    <tr>
                        <th runat="server" id="title" class="rptColumnHeaderTitle">
                            Title
                        </th>
                        <th runat="server" id="tCO2e" class="rptColumnHeaderResult">
                            CO<sub>2e</sub> [tn]
                        </th>
                        <th runat="server" id="CO2" class="rptColumnHeaderResult">
                            CO<sub>2</sub> [tCO<sub>2e</sub>]
                        </th>
                        <th runat="server" id="CH4" class="rptColumnHeaderResult">
                            CH<sub>4</sub> [tCO<sub>2e</sub>]
                        </th>
                        <th runat="server" id="N20" class="rptColumnHeaderResult">
                            N<sub>2</sub>O [tCO<sub>2e</sub>]
                        </th>
                        <th runat="server" id="PFC" class="rptColumnHeaderResult">
                            PFC [tCO<sub>2e</sub>]
                        </th>
                        <th runat="server" id="HFC" class="rptColumnHeaderResult">
                            HFC [tCO<sub>2e</sub>]
                        </th>
                        <th runat="server" id="SF6" class="rptColumnHeaderResult">
                            SF<sub>6</sub> [tCO<sub>2e</sub>]
                        </th>
                        <th runat="server" id="HCT" class="rptColumnHeaderResult">
                            HCT [Mg]
                        </th>
                        <th runat="server" id="HCNM" class="rptColumnHeaderResult">
                            HCNM [Mg]
                        </th>
                        <th runat="server" id="C2H6" class="rptColumnHeaderResult">
                            C<sub>2</sub>H<sub>6</sub> [Mg]
                        </th>
                        <th runat="server" id="C3H8" class="rptColumnHeaderResult">
                            C<sub>3</sub>H<sub>8</sub> [Mg]
                        </th>
                        <th runat="server" id="C4H10" class="rptColumnHeaderResult">
                            C<sub>4</sub>H<sub>10</sub> [Mg]
                        </th>
                        <th runat="server" id="CO" class="rptColumnHeaderResult">
                            CO [Mg]
                        </th>
                        <th runat="server" id="NOx" class="rptColumnHeaderResult">
                            NO<sub>x</sub> [Mg]
                        </th>
                        <th runat="server" id="SOx" class="rptColumnHeaderResult">
                            SO<sub>x</sub> [Mg]
                        </th>
                        <th runat="server" id="SO2" class="rptColumnHeaderResult">
                            SO<sub>2</sub> [Mg]
                        </th>
                        <th runat="server" id="H2S" class="rptColumnHeaderResult">
                            H<sub>2</sub>S [Mg]
                        </th>
                        <th runat="server" id="PM" class="rptColumnHeaderResult">
                            PM [Mg]
                        </th>
                        <th runat="server" id="PM10" class="rptColumnHeaderResult">
                            PM<sub>10</sub> [Mg]
                        </th>
                    </tr>
                </thead>
                <tbody id="tbody">
                </tbody>
            </table>
            <!-- FIN del Report -->
        </ContentTemplate>
    </asp:UpdatePanel>
</div>    
</asp:Content>
