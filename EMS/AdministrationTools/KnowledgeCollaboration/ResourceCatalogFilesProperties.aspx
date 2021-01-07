<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ResourceCatalogFilesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.KnowledgeCollaboration.ResourceCatalogFilesProperties"
    Title="EMS - Resource Catalog File Property" Culture="auto" UICulture="auto" %>

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
                    <!-- Selected Resource -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSelectedResouce" runat="server" Text="Selected Resource:" />
                        </td>
                        <td class="ColContentList">
                            <asp:RadioButtonList ID="rbList" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="File" Value="rbFile" Selected="True">
                                </asp:ListItem>
                                <asp:ListItem Text="Url" Value="rbUrl">
                                </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- File Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceFile" runat="server" Text="File Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtResourceFile" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvTitle"
                                Display="Dynamic" ControlToValidate="txtResourceFile" SkinID="EMS"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                  
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="upTableUpload" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" style="width:100%;" id="contentHide">
                <tr>
                    <td>
                        <table id="tblContentFormUpload" runat="server" class="ContentForm">
                            <colgroup>
                                <col class="ColTitle" />
                                <col class="ColContent" />
                                <col class="ColValidator" />
                            </colgroup>
                            <!-- File Name -->
                            <tr>
                                <td class="ColTitle">
                                    <asp:Label ID="lblFileName" runat="server" Text="File Name:" />
                                </td>
                                <td class="ColContent">
                                    <asp:Label ID="lblFileNameValue" runat="server" />
                                </td>
                                <td class="ColValidator">
                                    &nbsp;
                                </td>
                            </tr>
                            <!-- Extension -->
                            <tr>
                                <td class="ColTitle">
                                    <asp:Label ID="lblExtension" runat="server" Text="Extension:" />
                                </td>
                                <td class="ColContent">
                                    <asp:Label ID="lblExtensionValue" runat="server" />
                                </td>
                                <td class="ColValidator">
                                    &nbsp;
                                </td>
                            </tr>
                            <!-- Lenght -->
                            <tr>
                                <td class="ColTitle">
                                    <asp:Label ID="lblLenght" runat="server" Text="Lenght:" />
                                </td>
                                <td class="ColContent">
                                    <asp:Label ID="lblLenghtValue" runat="server" />
                                </td>
                                <td class="ColValidator">
                                    &nbsp;
                                </td>
                            </tr>
                            <!-- Type -->
                            <tr>
                                <td class="ColTitle">
                                    <asp:Label ID="lblType" runat="server" Text="Type:" />
                                </td>
                                <td class="ColContent">
                                    <asp:Label ID="lblTypeValue" runat="server" />
                                </td>
                                <td class="ColValidator">
                                    &nbsp;
                                </td>
                            </tr>
                            <!-- File Upload -->
                            <tr>
                                <td class="ColTitle">
                                    <asp:Label ID="lblUpload" runat="server" Text="File Upload" />
                                </td>
                                <td class="ColContent">
                                    <asp:FileUpload ID="fileUploadCatalog" runat="server" Size="60" />
                                </td>
                                <td class="ColValidator">
                                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvFile"
                                        Display="Dynamic" ControlToValidate="fileUploadCatalog" SkinID="EMS"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
