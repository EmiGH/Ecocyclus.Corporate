<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="OrganizationProperties.aspx.cs"
    EnableEventValidation="false" Inherits="Condesus.EMS.WebUI.DS.OrganizationProperties"
    Title="EMS - Organization Property" Culture="auto" UICulture="auto" %>

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
                    <!-- Corporate Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCorporateName" runat="server" Text="Corporate Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtCorporateName" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" Display="Dynamic"
                                ID="rfv1" ControlToValidate="txtCorporateName" SkinID="EMS"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtName" runat="server" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Fiscal Identification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFiscalIdentification" runat="server" Text="Fiscal Identification:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtFiscalId" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Resource Catalog -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceCatalog" runat="server" Text="Picture:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phResourceCatalog" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Classifications -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblClassifications" runat="server" Text="Organization Classifications" />
                        </td>
                        <td class="ColContent">
                            <div class="Treeview">
                                <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:PlaceHolder ID="phOrganizationClassifications" runat="server"></asp:PlaceHolder>
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
