<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="OrganizationClassificationsLanguagesProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.DS.OrganizationClassificationsLanguagesProperties" %>

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
                    <!-- Geographic Area -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganizationClassification" runat="server" Text="Organization Classification Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtOrganizationClassification" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ControlToValidate="txtOrganizationClassification" SkinID="EMS"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganizationClassificationDescription" runat="server" Text="Organization Classification Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtOrganizationClassificationDescription" TextMode="MultiLine"
                                Rows="6" MaxLength="8000"></asp:TextBox>
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
