<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ShowUploadAuditPlanProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PM.ShowUploadAuditPlanProperties" Title="EMS - Audit Plan Properties"
    Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

<script type="text/javascript">
    function ShowFileAudit(e, idProcess, fileName) {       
        var _IEXPLORER = 'Microsoft Internet Explorer';
        var _FIREFOX = 'Netscape';                     
        var _BrowserName = navigator.appName;

        if (_BrowserName == _IEXPLORER)
        {   //IE and Opera
            var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/AdministrationTools/ProcessesFramework/ShowFileAudit.aspx?IdProcess=" + idProcess + "&FileNameAudit=" + fileName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
        }
        else
        {   //FireFox
            var newWindow = window.parent.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/AdministrationTools/ProcessesFramework/ShowFileAudit.aspx?IdProcess=" + idProcess + "&FileNameAudit=" + fileName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
        }

        newWindow.focus();
        //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
        StopEvent(e);
    }
</script>

    <asp:UpdatePanel ID="upTableUploadFile" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contentHideFile" Style="margin-top: 10px">
                <table id="tblContentFormUploadFile" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Name Configuration Excel -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Excel Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblNameValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                            <asp:LinkButton runat="server" ID="lnkShowFile" SkinID="Form" CausesValidation="false"></asp:LinkButton>
                        </td>
                    </tr>
                    <!-- Upload File Measurements -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFile" runat="server" Text="Upload File:" />
                        </td>
                        <td class="ColContent">
                            <asp:FileUpload ID="fileUploadMeasurement" runat="server"/>
                            <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ErrorMessage="Tipo de archivo no permitido" ControlToValidate="fileUploadMeasurement" 
                                ValidationExpression= "(.*).(.xls|.XLS|.xlsx|.XLSX)$" ValidationGroup="Save"/>                            
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="fileUploadMeasurement"
                                Display="Dynamic" ErrorMessage="Please enter or browse for a file to upload."
                                SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
