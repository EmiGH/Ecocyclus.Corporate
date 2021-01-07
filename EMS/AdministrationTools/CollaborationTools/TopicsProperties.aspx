<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="TopicsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.CT.TopicsProperties" Title="EMS - Topic Property"
    Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Category -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCategory" runat="server" Text="Category:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phCategory" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phCategoryValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Topic -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtName" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" SkinID="EMS" Display="Dynamic" runat="server"
                                ErrorMessage="Required Field" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Language -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLanguage" runat="server" Text="Language:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblLanguageValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" MaxLength="8000" runat="server" TextMode="MultiLine"
                                Rows="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Is Locked -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIsLocked" runat="server" Text="Is Locked" />
                        </td>
                        <td class="ColContent">
                            <div class="Check">
                                <asp:CheckBox ID="chkIsLocked" runat="server" />
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Is Moderated -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIsModerated" runat="server" Text="Is Moderated" />
                        </td>
                        <td class="ColContent">
                            <div class="Check">
                                <asp:CheckBox ID="chkIsModerated" runat="server" />
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
