<%@ Page Title="Home" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="HierarchicalListManage.aspx.cs" Inherits="Condesus.EMS.WebUI.Managers.HierarchicalListManage" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <script src="../AppCode/JavaScriptCore/QueryData.js" type="text/javascript"></script>

    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <asp:Panel ID="pnlFilter" runat="server">
                </asp:Panel>
                <asp:PlaceHolder ID="phHeaderTable" runat="server"></asp:PlaceHolder>
                <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlHirarchicalListManage" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
    <!--Abro RadMenu-->
    <rad:RadContextMenu ID="rmnSelection" runat="server" CollapseDelay="0" ExpandDelay="0"
        Style="left: 0px; top: 0px">
    </rad:RadContextMenu>
    <!--Abro RadMenu-->
    <!--Abro Popup de Delete -->
    <div style="z-index:9000">
    <asp:Panel ID="pnlPopUpConfirmDelete" runat="server" style="z-index:9000">
        <asp:Button ID="btnHidden" runat="server" Visible="False" CausesValidation="False" />
        <ajaxToolkit:ConfirmButtonExtender ID="cbelbDelete" runat="server" TargetControlID="btnHidden"
            OnClientCancel="cancelClick" DisplayModalPopupID="mpelbDelete" Enabled="False"
            ConfirmText=""  />
        <ajaxToolkit:ModalPopupExtender ID="mpelbDelete" runat="server" TargetControlID="btnHidden"
            BehaviorID="programmaticModalPopupBehavior" PopupControlID="pnlConfirmDelete"
            CancelControlID="btnCancel" BackgroundCssClass="ModalPopUp" DynamicServicePath=""
            Enabled="True" />
        <div class="contentpopup">
            <asp:Panel ID="pnlConfirmDelete" runat="server" Style="display: none; z-index:1000;">
                <span>
                    <asp:Literal ID="liMsgConfirmDelete" runat="server" Text="<%$ Resources:Common, msgConfirmDelete %>" />
                </span>
                <asp:Button ID="btnOkDelete" runat="server" Text="<%$ Resources:Common, btnOk %>"
                    CausesValidation="False" meta:resourcekey="btnOkResource1" />
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Common, btnCancel %>"
                    meta:resourcekey="btnCancelResource1" />
            </asp:Panel>
        </div>
    </asp:Panel>
    </div>
    <!-- Fin de Popup de Delete -->
</asp:Content>
