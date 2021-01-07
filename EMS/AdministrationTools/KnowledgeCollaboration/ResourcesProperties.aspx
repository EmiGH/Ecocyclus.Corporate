<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ResourcesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.KnowledgeCollaboration.ResourcesProperties"
    Title="EMS - Resources" Culture="auto" UICulture="auto" %>

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
                    <!-- Resource File Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceFileType" runat="server" Text="Resource Type:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadComboBox ID="ddlResourceFileType" runat="server" Skin="EMS">
                                <Items>
                                    <rad:RadComboBoxItem Value="ResourceVersion" Text="Versionable" />
                                    <rad:RadComboBoxItem Value="ResourceCatalog" Text="Catalog" />
                                </Items>
                            </rad:RadComboBox>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="cvResourceFileType" runat="server" ControlToValidate="ddlResourceFileType"
                                SkinID="EMS" ErrorMessage="Required Field" Display="Dynamic"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceType" runat="server" Text="Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phResourceType" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phResourceTypeValidator" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <!-- Title -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTitle" runat="server" Text="Title:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="rfv2" runat="server" ErrorMessage="Invalid Special Characters"
                                ControlToValidate="txtTitle" ClientValidationFunction="CheckIndexesTags"></asp:CustomValidator>
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
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFileName" runat="server" Text="Current File Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblFileNameValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Version -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFileVersion" runat="server" Text="Version:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblFileVersionValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Classifications -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblClassifications" runat="server" Text="Resource Classifications" />
                        </td>
                        <td class="ColContent">
                            <div class="Treeview">
                                <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:PlaceHolder ID="phResourceClassifications" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
