<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="IndicatorsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PA.IndicatorsProperties" Title="EMS - Indicators"
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
                    <!-- Magnitud -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMagnitud" runat="server" Text="Magnitud:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phMagnitud" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phMagnitudValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Indicator Extensive -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIsCumulative" runat="server" Text="Indicator Extensive" />
                        </td>
                        <td class="ColContent">
                            <div class="Check">
                                <asp:CheckBox ID="chkIsCumulative" SkinID="EMS" runat="server" Checked="True" />
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
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
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="rfv2" runat="server" ErrorMessage="Invalid Special Characters"
                                ControlToValidate="txtName" ClientValidationFunction="CheckIndexesTags"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Scope -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblScope" runat="server" Text="Scope:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtScope" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Limitation -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLimitation" runat="server" Text="Limitation:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtLimitation" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Definition -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDefinition" runat="server" Text="Definition:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDefinition" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
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
                    <!-- Classifications -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblClassifications" runat="server" Text="Indicator Classifications" />
                        </td>
                        <td class="ColContent">
                            <div class="Treeview">
                                <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:PlaceHolder ID="phIndicatorClassifications" runat="server"></asp:PlaceHolder>
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
