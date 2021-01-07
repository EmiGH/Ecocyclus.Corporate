<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImprovementActions.ascx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.ProcessesMap.Controls.ImprovementActions" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<rad:RadTabStrip ID="rtsMainTab" runat="server"  AutoPostBack="true">
    <Tabs>
        <rad:RadTab Value="Exception" runat="server" Text="Exception">
        </rad:RadTab>
    </Tabs>
</rad:RadTabStrip>
<div class="contentmenu">
    <div id="contentmenudivBackground">
        <div id="contentmenudivPosition">
            <!--Opciones Generales --  >
            <!-- Fin Opciones Generales -->
        </div>
    </div>
</div>
<div>
    <asp:Panel runat="server" ID="pnlTabContainer">
    </asp:Panel>
</div>
<!-- IA.Entities.Exception -->
<!--Abro RadMenu-->
<rad:RadMenu ID="rmnSelectionIAException" runat="server" OnClientItemClicked="rmnSelectionIAException_OnClientItemClickedHandler"
     CollapseDelay="0" Style="left: 0px; top: 0px;">
    <CollapseAnimation Type="None" Duration="0"></CollapseAnimation>
    <ExpandAnimation Type="None" Duration="0"></ExpandAnimation>
</rad:RadMenu>
<!--Abro RadMenu-->
<!--Variables escondidas PMExecution-->
<input type="hidden" id="radMenuClickedIdIAException" name="radMenuClickedIdIAException" />
<input type="hidden" id="radMenuItemClickedIAException" name="radMenuItemClickedIAException" />
<input type="hidden" id="radGridClickedRowIndexIAException" name="radGridClickedRowIndexIAException" />
<input type="hidden" id="radGridClickedTableIdIAException" name="radGridClickedTableIdIAException" />
<!--Variables escondidas-->
