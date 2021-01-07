<%@ Page Language="C#" MasterPageFile="~/EMSPopUp.Master" AutoEventWireup="true"
    CodeBehind="FilesViewer.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.KnowledgeCollaboration.FilesViewer"
    Title="Files Viewer" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Microsoft.Live.ServerControls" Namespace="Microsoft.Live.ServerControls"
    TagPrefix="live" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPopUp" runat="server">
    <asp:Panel ID="pnlFlashViewer" runat="server" Visible="false">
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
            width="100%" height="100%">
            <param name="movie" value="<% =swfFileName%>" />
            <param name="quality" value="high" />
            <embed src="<% =swfFileName%>" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                type="application/x-shockwave-flash" width="100%" height="100%"></embed>
        </object>
    </asp:Panel>
    <%--<live:SilverlightStreamingMediaPlayer ID="slStreamingMedia" runat="server" Visible="false"
        Height="100%" MediaSourceProvider="SilverlightStreaming" Width="100%">
    </live:SilverlightStreamingMediaPlayer>--%>
    <%--<object id="VIDEO" width="320" height="240" style="position: absolute; left: 0; top: 0;"
        classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" type="application/x-oleobject">
        <param name="src" value="./video.wma" />
        <param name="SendPlayStateChangeEvents" value="True" />
        <param name="AutoStart" value="True" />
        <param name="uiMode" value="none" />
        <param name="showcontrols" value="1" />
        <param name="showdisplay" value="1" />
        <param name="showstatusbar" value="1" />
        <param name="PlayCount" value="9999" />
    </object>--%>
    <asp:Panel ID="pnlMediaViewer" runat="server" Visible="false">
        <object data="<% =mediaFileName%>" type="<% =typeFile%>" width="100%" height="100%">
            <param name="src" value="<% =mediaFileName%>" />
            <param name="autostart" value="0" />
            <param name="volume" value="0" />
            <param name="showcontrols" value="1" />
            <param name="showdisplay" value="0" />
            <param name="showstatusbar" value="0" />
            <%--<param name="playcount" value="1" />--%>
        </object>
    </asp:Panel>
</asp:Content>
