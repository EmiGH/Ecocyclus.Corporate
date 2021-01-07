<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/EMS.Master" CodeBehind="Map.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.DirectoryServices.Map" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script type="text/javascript">
   
//    window.attachEvent('onload', InitBackgroundImage);
    if (_BrowserName == _IEXPLORER) {   //IE and Opera
        window.attachEvent('onload', InitBackgroundImage);
    }
    else {   //FireFox
        document.addEventListener('DOMContentLoaded', InitBackgroundImage, false);
    }

function InitBackgroundImage() {    
    SetFwBounds();
    //La altura del Content (ya calculada)
    var _contentHeight = document.getElementById('divContent').style.height.replace('px', '');
    var _contentWidth = document.getElementById('divContent').style.width.replace('px', '');
    
    var _offersGrid = document.getElementById('divBackgroundImage');
    _offersGrid.style.height = (_contentHeight - 10) + 'px';
    _offersGrid.style.width = (_contentWidth - 60) + 'px' ;
   
    }
    </script>

    <div id="divBackgroundImage" class="divBackgroundImageDS">
        <asp:Label runat="server" ID="lblDescription" CssClass="contentConfigurationText"
            Text=""></asp:Label>
    </div>
</asp:Content>
