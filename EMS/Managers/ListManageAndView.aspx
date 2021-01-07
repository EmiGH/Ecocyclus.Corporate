<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ListManageAndView.aspx.cs" Inherits="Condesus.EMS.WebUI.Managers.ListManageAndView" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script type="text/javascript">

        if (_BrowserName == _IEXPLORER) {   //IE and Opera
            window.attachEvent('onload', Initimg);
        }
        else {   //FireFox
            document.addEventListener('DOMContentLoaded', Initimg, false);
        }
        function Initimg() {
            //La altura del Content (ya calculada)
            var _tdimgHeight = document.getElementById('tdPicture').clientHeight;  //document.getElementById('tdPicture').style.height.replace('px', '');
            var _img = document.getElementById('ctl00_ContentMain_imgShowSlide');
            //Si no hay imagen...no hace nada
            if (_img != null) {
                var _imgHeigth = _img.clientHeight; //Le resto la altura para que quede centrado?...
                _img.style.marginTop = (parseInt(_tdimgHeight - _imgHeigth, 10) / 2) + 'px';
            }
        }

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

        function HideShowFilterContent(sender, e) {
            var senderClass = (sender.className == 'Link') ? 'LinkActive' : 'Link';
            sender.className = senderClass;

//            var tdFilter = $get('ctl00_ContentMain_tblFilter');
//            if (tdFilter.className == 'Header') {
//                tdFilter.className = 'HeaderOpen';
//            }
//            else {
//                tdFilter.className = 'Header';
//            }

            var trFilterContent = $get('ctl00_ContentMain_filterContent');
            if (trFilterContent.style.display == 'none') {
                trFilterContent.style.display = 'block';
            }
            else {
                trFilterContent.style.display = 'none';
            }

            StopEvent(e);   //window.event.returnValue = false;
        }
        
      
    </script>

    <style>
        .ContentFilter .Header
        {
            padding: 0px 0px 0px 0px;
            line-height: 10px;
        }
    </style>

    <script type="text/javascript">
        function ShowLoading() {
            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
        }
    </script>

    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <asp:LinkButton ID="lnkExport" runat="server" CssClass="lnkPrint" Text="Export" Style="display: none;"
                    OnClick="lnkExportGridMeasurement_Click"></asp:LinkButton>
                <asp:Panel ID="pnlGeneralOptionMenu" runat="server">
                </asp:Panel>
                <asp:Panel ID="pnlFilterForExecutionTask" runat="server" Visible="false">
                    <table id="tblContentFilter" runat="server" class="ContentFilter">
                        <tr>
                            <td class="Header">
                                <asp:LinkButton ID="lnkFilterOpenForm" runat="server" CssClass="Link" OnClientClick="HideShowFilterContent(this, event);" > 
                                    <asp:Label runat="server" ID="lblFilterForExecutionTask"></asp:Label>
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="filterContent" runat="server" style="display: none;">
                            <td>
                                <div class="Form">
                                    <div style="float: left; margin-bottom: 3px;">
                                        <div style="float: left; margin-right: 5px;">
                                            <asp:PlaceHolder ID="phAccountingActivity" runat="server" />
                                        </div>
                                        <div style="float: left;">
                                            <asp:PlaceHolder ID="phMethodology" runat="server" />
                                        </div>
                                    </div>
                                    <div style="clear: both; float: left;">
                                        <div style="float: left; margin-right: 5px;">
                                            <asp:PlaceHolder ID="phFacilityType" runat="server" />
                                        </div>
                                        <div style="float: left;">
                                            <asp:PlaceHolder ID="phFacility" runat="server" />
                                        </div>
                                    </div>
                                    <div style="float: right;">
                                        <asp:Button CssClass="contentButton" ID="btnFilter" runat="server" Text="Filter"
                                            CausesValidation="False" Visible="false" />
                                        <asp:Button CssClass="contentButton" ID="btnClearFilter" runat="server" Text="Clear Filter"
                                            CausesValidation="False" Visible="false" Style="margin-right: 5px;" />&nbsp;
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlFilter" runat="server">
                </asp:Panel>
                <asp:Panel ID="pnlSearch" runat="server">
                </asp:Panel>
                <asp:UpdatePanel ID="upListManage" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlListManage" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upViewer" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlViewer" runat="server" Style="display: none;">
                            <table border="0" cellpadding="0" cellspacing="0" class="contentListViewReportTable">
                                <tr>
                                    <td class="PictureSmall" id="tdPicture">
                                        <asp:UpdatePanel ID="upCounter" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:Image ID="imgShowSlide" runat="server" ImageUrl="~/Skins/Images/Trans.gif" CssClass="imgShowSlide"
                                                    Style="display: inline-block;" />
                                                <br />
                                                <asp:HiddenField ID="hdn_ImagePosition" runat="server" Value="0" />
                                                <asp:ImageButton ID="btnPrevPicture" CommandArgument="-1" runat="server" CssClass="Back"
                                                    ImageUrl="~/Skins/Images/Trans.gif" />
                                                <asp:ImageButton ID="btnNextPicture" CommandArgument="1" runat="server" CssClass="Next"
                                                    ImageUrl="~/Skins/Images/Trans.gif" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="Space2">
                                        &nbsp;
                                    </td>
                                    <td class="MainData">
                                        <%--Va la Tabla que contrendra a las grillas de viewer para el Related Data--%>
                                        <asp:UpdatePanel ID="upListViewerRelatedData" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div class="divContentTabStrip">
                                                    <asp:Label ID="lblSubTitle" runat="server" Text="Información Relacionada" CssClass="lblSubTitle" />
                                                    <asp:Panel ID="pnlTabStrip" runat="server" CssClass="pnlTabStrip">
                                                    </asp:Panel>
                                                </div>
                                                <asp:Panel ID="pnlListViewerMainData" runat="server">
                                                </asp:Panel>
                                                <asp:Panel ID="pnlListViewerRelatedData" runat="server">
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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
    <asp:UpdatePanel ID="upMenuSelection" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <rad:RadContextMenu ID="rmnSelectionOnRelated" Skin="EMS" EnableEmbeddedSkins="false"
                runat="server" CollapseDelay="0" ExpandDelay="0" Style="left: 0px; top: 0px">
            </rad:RadContextMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
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
