<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_ghree.aspx.cs" Inherits="Condesus.EMS.WebUI.Login_ghree" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<meta http-equiv="X-UA-Compatible" content="IE=10; IE=9; IE=8;"/>--%>
    <title></title>
    <link type="text/css" href="~/Skins/Login.css" rel="stylesheet"/>
    <link href="~/Skins/RadControls/ComboBox.EMS.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="frmLogin" runat="server">
    <asp:ScriptManager ID="smLogin" runat="server" />
    <%--<asp:UpdatePanel ID="upLogin" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>

            <div class="divLoginBg">
            </div>
            <div class="divLoginTexture">
            </div>
            <div class="divLoginImage">
            </div>
            <div class="divLoginContainer">
                <div class="divLoginContent">
                    <div class="divLoginLogoGhree">
                    </div>
                    <!--Access-->
                    <asp:Panel runat="server" ID="logindivMessenger">
                        <asp:Label runat="server" ID="lblFailureText" />
                    </asp:Panel>
                    <div class="divLoginAccess">
                        <!--div pelsa-->
                        <div style="float: left; margin-right: 10px;">
                            <!--div pelsa-->
                            <asp:Label runat="server" ID="lblUser" CssClass="lblUser"></asp:Label>
                            <asp:TextBox runat="server" CssClass="input user" ID="txtUserName" />
                            <!--div pelsa-->
                        </div>
                        <!--div pelsa-->
                        <!--div pelsa-->
                        <div style="float: left;">
                            <!--div pelsa-->
                            <asp:Label runat="server" ID="lblPassword" CssClass="lblPassword"></asp:Label>
                            <asp:TextBox runat="server" CssClass="input pass" ID="txtPassword" TextMode="Password" />
                            <!--div pelsa-->
                        </div>
                        <div style="float: left;">
                            <asp:Button ID="btnLogin" runat="server" CssClass="button" OnClick="LoginButton_Click"
                                CommandName="Login" />
                        </div>
                        <!--div pelsa-->
                        <div class="divLoginAccessOptions">
                            <asp:CheckBox ID="ckRememberUser" runat="server" CssClass="checkbox" />
                            <asp:LinkButton ID="lnkForgetPassword" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <!--Register-->
                    <div class="divLoginBgRegister">
                        <div>
                            <asp:Label runat="server" ID="lblTitleRegister"></asp:Label>
                            <!--Full Name-->
                            <asp:TextBox runat="server" ID="txtFullNameh" CssClass="input" Style="display: none"
                                onblur="ShowFullName();" />
                            <asp:TextBox runat="server" CssClass="input" ID="txtFullName" onfocus="HideFullName();" />
                            <!--Email-->
                            <asp:TextBox runat="server" ID="txtEmailh" CssClass="input" Style="display: none"
                                onblur="ShowEmail();" />
                            <asp:TextBox runat="server" CssClass="input user" ID="txtEmail" onfocus="HideEmail();" />
                            <!--Password-->
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPasswordRegisterh" CssClass="input"
                                Style="display: none" onblur="ShowPasswordRegister();" />
                            <asp:TextBox runat="server" CssClass="input user" ID="txtPasswordRegister" onfocus="HidePasswordRegister();" />
                            <!--Button Register-->
                            <asp:Button runat="server" ID="btnRegister" CssClass="button" />
                        </div>
                    </div>
                    <!--Slogan-->
                    <div class="divContentSlogan">
                        <asp:Label runat="server" ID="lblSlogan" CssClass="lblSlogan"></asp:Label>
                    </div>
                    <asp:Panel runat="server" ID="pnlContentLanguege" CssClass="pnlContentLanguege">
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlContentFooter" CssClass="pnlContentFooter">
                        <asp:Label runat="server" ID="lblGhreeCopyRight"></asp:Label>
                        <div>
                            <asp:LinkButton runat="server" ID="lnkPrivacy"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkTerms"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkHelpdesk"></asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:Panel id="pnlErrorGeneric" runat="server" style="display:none;"></asp:Panel>
                </div>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--<asp:UpdateProgress runat="server" ID="uProgressLogin" AssociatedUpdatePanelID="upLogin">
            <ProgressTemplate>
                <div class="Loading">
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </form>
</body>

<script type="text/javascript">
    //real postback for login
    function realPostBack(eventTarget, eventArgument) {
        __doPostBack(eventTarget, eventArgument);
    }    
</script>
</html>
