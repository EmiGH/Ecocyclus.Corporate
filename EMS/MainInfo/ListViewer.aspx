<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ListViewer.aspx.cs" Inherits="Condesus.EMS.WebUI.TestBackEnd.ListViewer" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <asp:Panel ID="pnlListViewer" runat="server">
                </asp:Panel>
            </td>
        </tr>
    </table>
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
    <asp:Button ID="btnHidden" runat="server" Visible="False" CausesValidation="False" />
    <ajaxToolkit:ConfirmButtonExtender ID="cbelbDelete" runat="server" TargetControlID="btnHidden"
        OnClientCancel="cancelClick" DisplayModalPopupID="mpelbDelete" Enabled="False"
        ConfirmText="" />
    <ajaxToolkit:ModalPopupExtender ID="mpelbDelete" runat="server" TargetControlID="btnHidden"
        BehaviorID="programmaticModalPopupBehavior" PopupControlID="pnlConfirmDelete"
        CancelControlID="btnCancel" BackgroundCssClass="ModalPopUp" DynamicServicePath=""
        Enabled="True" />
    <div class="contentpopup">
        <asp:Panel ID="pnlConfirmDelete" runat="server" Style="display: none" meta:resourcekey="pnlConfirmDeleteResource1">
            <span>
                <asp:Literal ID="liMsgConfirmDelete" runat="server" Text="<%$ Resources:Common, msgConfirmDelete %>" /></span>
            <asp:Button ID="btnOkDelete" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnOk %>"
                CausesValidation="False" BorderStyle="None" meta:resourcekey="btnOkResource1" />
            <asp:Button ID="btnCancel" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnCancel %>"
                BorderStyle="None" meta:resourcekey="btnCancelResource1" />
        </asp:Panel>
    </div>

    <script type="text/javascript">

        function GetPopupInfo(node, img) 
        {
            var text = "<div style='vertical-align: middle; height:25px;'><span style='color:#307DB3;font-family: Verdana; font-size: 11px;'>" + node._attributes.getAttribute('TitleNode') + "</span></div>";
            text += "<table border='0' cellpadding='0' cellspacing='0'>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Start Date</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('StartDate') + "</span></td></tr>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>End Date</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('EndDate') + "</span></td></tr>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Duration</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Duration') + "</span></td></tr>";

            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Status</span></td>";
            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('State') + "</span></td></tr>";

//            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Completed</span></td>";
//            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Completed') + " %</span></td></tr>";

//            text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Result</span></td>";
//            text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Result') + "</span></td></tr>";

            //Los Custom Attributes "especiales"
            if (node._attributes.getAttribute('ProcessType') == "ProcessTaskMeasurement" || node._attributes.getAttribute('ProcessType') == "ProcessTaskMeasurementDataRecovery") {
                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Frequency</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Frequency') + "</span></td></tr>";

                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Indicator</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Indicator') + "</span></td></tr>";

                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Measurement Unit</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('MeasurementUnit') + "</span></td></tr>";

                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Measurement</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('Measurement') + "</span></td></tr>";
            }
            if (node._attributes.getAttribute('ProcessType') == "ProcessTaskCalibration") {
                text += "<tr style='padding-bottom: 3px;'><td><span style='color:#307DB3;font-family: Verdana; font-size: 10px;'>Device</span></td>";
                text += "<td><span style='font-family: Verdana; font-size: 10px;padding-left: 5px;'> " + node._attributes.getAttribute('MeasurementDevice') + "</span></td></tr>";
            }

            text += "</table>";

            return text;
        }
    
    </script>

</asp:Content>

