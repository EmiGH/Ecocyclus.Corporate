<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
 CodeBehind="GeographicTooltipNavigate.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.GeographicTooltipNavigate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<style type="text/css">
.Loading
{
	background-repeat: no-repeat;
	background-position: center center;
	-ms-background-position-x:center;
	-ms-background-position-y:center;
	background-color: #FFFFFF;
	height: 100%;
	width: 100%;
	filter:alpha(opacity=70);
	-ms-filter:alpha(opacity=70);
	-moz-opacity:.70;
	opacity:.70;
	position: fixed;
	visibility: visible;
	z-index: 999999;
	top: 0px;
	left: 0px;
	right: 0px;
	bottom: 0px;
	margin: 0px;
	overflow: visible;
}
</style>

    
    <div id="UpdateProgress" class="Loading">
    </div>

    <script type="text/javascript">
        //Event onload
        //window.attachEvent('onload', ShowUProgress);
        if (_BrowserName == _IEXPLORER) {   //IE and Opera
            window.attachEvent('onload', ShowUProgress);
        }
        else {   //FireFox
            document.addEventListener('DOMContentLoaded', ShowUProgress, false);
        }
        function ShowUProgress() {
            //Muestra el reloj.
            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
        }
    </script>

</asp:Content>
