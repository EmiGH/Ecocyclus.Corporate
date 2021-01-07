<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Dashboard.Dashboard" Title="Dashboard" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:UpdatePanel ID="upDashboard" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <rad:RadContextMenu ID="rmnContextMenu" runat="server" Skin="EMS">
                <Targets>
                    <rad:ContextMenuControlTarget ControlID="rdzLeftTop" />
                    <rad:ContextMenuControlTarget ControlID="rdProyectBuyerPluggIn" />
                </Targets>
                <Items>
                </Items>
            </rad:RadContextMenu>
            <asp:PlaceHolder ID="phGadgetToolbar" runat="server" />
            <rad:RadDockLayout ID="rdlDashboard" runat="server" Skin="EMS" EnableEmbeddedSkins="false">
                <table cellpadding="0" cellspacing="0" style="width:100%">
                    <tr>
                        <td valign="top">
                            <rad:RadDockZone ID="rdzLeftTop" runat="server" FixedByWidth="true" BorderWidth="0px"
                                Orientation="Vertical">
                            </rad:RadDockZone>
                        </td>
                        <td style="width:10px;">
                        </td>
                        <td valign="top" style="width:200px;">
                            <rad:RadDockZone ID="rdProyectBuyerPluggIn" runat="server" FixedByWidth="true" >
                            </rad:RadDockZone>
                        </td>
                    </tr>
                </table>
            </rad:RadDockLayout>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--Variables escondidas-->
    <input type="hidden" id="radGridClickedRowIndexBuyer" name="radGridClickedRowIndexBuyer" />
    <input type="hidden" id="radGridClickedTableIdBuyer" name="radGridClickedTableIdBuyer" />
    <input type="hidden" id="radMenuClickedIdBuyer" name="radMenuClickedIdBuyer" />
</asp:Content>
