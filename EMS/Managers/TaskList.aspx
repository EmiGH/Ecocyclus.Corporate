<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="TaskList.aspx.cs" Inherits="Condesus.EMS.WebUI.Managers.TaskList" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script type="text/javascript">
        function HideShowSearchContent(sender, e) {
            var senderClass = (sender.className == 'Link') ? 'LinkActive' : 'Link';
            sender.className = senderClass;

            var tdSearch = $get('ctl00_ContentMain_tblSearch');
            if (tdSearch.className == 'Header') {
                tdSearch.className = 'HeaderOpen';
            }
            else {
                tdSearch.className = 'Header';
            }

            var lnkSearch = $get('ctl00_ContentMain_lnkSearch');
            if (lnkSearch.style.display == 'none') {
                lnkSearch.style.display = 'block';
            }
            else {
                lnkSearch.style.display = 'none';
            }

            var trSearchContent = $get('ctl00_ContentMain_searchContent');
            if (trSearchContent.style.display == 'none') {
                trSearchContent.style.display = 'block';
            }
            else {
                trSearchContent.style.display = 'none';
            }

            StopEvent(e);   //window.event.returnValue = false;          
        }
    </script>

    <script type="text/javascript">
        function ShowLoading() {
            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
        }
    </script>

    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <%--<td style="float: left">
                <asp:Label ID="lblToday" runat="server"></asp:Label>
            </td>--%>
            <td style="padding-left: 5px">
                <asp:PlaceHolder ID="phTaskState" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlGeneralOptionMenu" runat="server">
                </asp:Panel>
                <asp:Panel ID="pnlSearch" runat="server">
                </asp:Panel>
                <asp:UpdatePanel ID="upListManage" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlListManage" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="padding-left: 5px">
                <asp:UpdatePanel ID="upCalendar" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <rad:RadCalendar ID="rcCalendar" runat="server" AutoPostBack="true" MultiViewColumns="1"
                            MultiViewRows="3" EnableMultiSelect="false" TitleFormat="MMMM yyyy" CultureInfo="English (United States)"
                            PresentationType="Interactive" EnableNavigation="true" EnableMonthYearFastNavigation="true"
                            Skin="EMS" ShowOtherMonthsDays="false" ShowRowHeaders="false" EnableRepeatableDaysOnClient="false"
                            EnableViewSelector="false" EnableNavigationAnimation="false" UseColumnHeadersAsSelectors="false">
                            <SpecialDays>
                                <rad:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                            </SpecialDays>
                        </rad:RadCalendar>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
    <!--Abro RadMenu Context-->
    <rad:RadContextMenu ID="rmnSelection" Skin="EMS" EnableEmbeddedSkins="false" runat="server"
        CollapseDelay="0" ExpandDelay="0" Style="left: 0px; top: 0px">
    </rad:RadContextMenu>
    <!--Cierro RadMenu Context-->
    <!--Abro Popup de Delete -->
    <asp:Panel ID="pnlPopUpConfirmDelete" runat="server">
        <asp:Button ID="btnHidden" runat="server" Visible="False" CausesValidation="False" />
        <ajaxToolkit:ConfirmButtonExtender ID="cbelbDelete" runat="server" TargetControlID="btnHidden"
            OnClientCancel="cancelClick" DisplayModalPopupID="mpelbDelete" Enabled="False"
            ConfirmText="" />
        <ajaxToolkit:ModalPopupExtender ID="mpelbDelete" runat="server" TargetControlID="btnHidden"
            BehaviorID="programmaticModalPopupBehavior" PopupControlID="pnlConfirmDelete"
            CancelControlID="btnCancel" BackgroundCssClass="ModalPopUp" DynamicServicePath=""
            Enabled="True" />
        <div class="contentpopup">
            <asp:Panel ID="pnlConfirmDelete" runat="server" Style="display: none">
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
    <!-- Fin de Popup de Delete -->
</asp:Content>
