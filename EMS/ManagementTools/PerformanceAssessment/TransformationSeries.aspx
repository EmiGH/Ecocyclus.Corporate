<%@ Page Language="C#" MasterPageFile="~/EMSPopUp.Master" AutoEventWireup="true"
    CodeBehind="TransformationSeries.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.TransformationSeries"
    Title="Indicator Serires" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPopUp" runat="server">
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
                        <!-- Transformation -->
                        <tr>
                            <td class="ColTitle">
                                <asp:Label ID="lblTransformation" runat="server" Text="Transformation:" />
                            </td>
                            <td class="ColContent">
                                <asp:Label ID="lblTransformationValue" runat="server" />
                            </td>
                        </tr>
                        <!-- Base -->
                        <tr>
                            <td class="ColTitle">
                                <asp:Label ID="lblBase" runat="server" Text="Base:" />
                            </td>
                            <td class="ColContent">
                                <asp:Label ID="lblBaseValue" runat="server" />
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
                </td>
            </tr>
        </table>
        <div style="float: left">
        </div>
        <br />
        <br />
        <asp:PlaceHolder runat="server" ID="pchControls"></asp:PlaceHolder>
        <!--Variables escondidas-->
        <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
        <input type="hidden" id="radMenuItemClicked" name="radMenuItemClicked" />
        <!--Variables escondidas-->
    </div>
</asp:Content>
