<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="UsersProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.DS.UsersProperties" %>

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
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblOrganizationValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Person -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPerson" runat="server" Text="Person:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upPerson" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phPerson" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phPersonValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- User Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblUserName" runat="server" Text="User:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtUserName" MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" SkinID="Validator" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Change Password -->
                    <tr>
                        <td class="ColTitle">
                            <asp:LinkButton ID="lbChangePassword" runat="server" Text="Change Password"></asp:LinkButton>
                        </td>
                        <td class="ColContent">
                            <ajaxToolkit:CollapsiblePanelExtender ID="cpeChangePassword" runat="server" TargetControlID="pnlPassword"
                                ExpandControlID="lbChangePassword" CollapseControlID="lbChangePassword" Collapsed="True"
                                ImageControlID="imbBuscar" SuppressPostBack="True" EnableViewState="False" Enabled="True" />
                            <asp:Panel ID="pnlPassword" runat="server">
                                <div id="contentformdivParPasswordConteiner">
                                    <div>
                                        <asp:Label CssClass="contentformspanTitle" ID="lblPassword" runat="server" Text="Password:" />
                                        <asp:TextBox CssClass="contentformInput" ID="txtPassword" runat="server" TextMode="Password"
                                            ValidationGroup="Password" CausesValidation="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" SkinID="Validator" ErrorMessage="Required Field"
                                            ID="rfv2" ControlToValidate="txtPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" />
                                        <asp:TextBox CssClass="contentformInput" ID="txtConfirmPassword" runat="server" TextMode="Password"
                                            ValidationGroup="Password" CausesValidation="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" SkinID="Validator" ErrorMessage="Required Field"
                                            ID="rfv3" ControlToValidate="txtConfirmPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToCompare="txtPassword"
                                            ControlToValidate="txtConfirmPassword" ErrorMessage="Password does not match."
                                            SetFocusOnError="True" ValidationGroup="Password" SkinID="Validator" />
                                    </div>
                                    <div id="Button">
                                        <asp:Button runat="server" Text="<%$ Resources:Common, btnSave %>" ID="btnSavePassword"
                                            ValidationGroup="Password" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Last Access -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLastAccess" runat="server" Text="Last Access:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblLastAccessValue" runat="server"></asp:Label>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Last IP -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLastIP" runat="server" Text="Last IP:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblLastIPValue" runat="server"></asp:Label>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Active -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblActive" runat="server" Text="Active:" />
                        </td>
                        <td class="ColContentCheck">
                            <asp:CheckBox CssClass="CheckBox" ID="chkActive" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Change Password on next login -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblChangePasswordOnNextLogin" runat="server" Text="Change Password On Next Login:" />
                        </td>
                        <td class="ColContentCheck">
                            <asp:CheckBox CssClass="CheckBox" ID="chkChangePasswordOnNextLogin" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Cannot change Password -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCannotChangePassword" runat="server" Text="Cannot Change Password:" />
                        </td>
                        <td class="ColContentCheck">
                            <asp:CheckBox CssClass="CheckBox" ID="chkCannotChangePassword" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Password Never Expires -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label CssClass="contentformspanTitle" ID="lblPasswordNeverExpires" runat="server"
                                Text="Password Never Expires:" />
                        </td>
                        <td class="ColContentCheck">
                            <asp:CheckBox CssClass="CheckBox" ID="chkPasswordNeverExpires" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- ViewGlobalMenu -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblViewGlobalMenu" runat="server" Text="Use Global Menu:" />
                        </td>
                        <td class="ColContentCheck">
                            <asp:CheckBox CssClass="CheckBox" ID="chkViewGlobalMenu" runat="server" />
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
