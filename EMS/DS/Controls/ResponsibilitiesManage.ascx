<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResponsibilitiesManage.ascx.cs" Inherits="Condesus.EMS.WebUI.DS.Controls.ResponsibilitiesManage" %>
    
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

  
        <div class="contentmenu">
            <div class="contentmenudivBackground">
                <div class="contentmenudivPosition"> 
            <rad:RadMenu ID="rmnOptionResponsibilities" runat="server" CausesValidation="False" 
                         meta:resourcekey="rmnOptionResponsibilitiesResource1">
                <Items>
                    <rad:RadMenuItem ID="rmnGeneralOptionsResponsibilities" CssClass="GeneralOptions" Text="General Options" ></rad:RadMenuItem>
                </Items>
              <ExpandAnimation Type="None"></ExpandAnimation>
              <CollapseAnimation Type="None"></CollapseAnimation>
            </rad:RadMenu>
        </div>
    </div>
        <asp:UpdatePanel ID="upMasterGridResponsibilities" runat="server">
        <ContentTemplate>
            <!--Abro RadGrid-->
            <rad:RadGrid ID="rgdMasterGridResponsibilities" runat="server" 
                AllowPaging="True" AllowSorting="True"  Width="100%" 
            AutoGenerateColumns="False" GridLines="None" 
            EnableAJAXLoadingTemplate="True" LoadingTemplateTransparency="25" 
                meta:resourcekey="rgdMasterGridResponsibilitiesResource1">
                <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />

                <MasterTableView Width="100%" DataKeyNames="IdResponsibility" Name="gridMaster" CellPadding="0" GridLines="None">
                                            <RowIndicatorColumn Visible="False">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Resizable="False" Visible="False">
                             <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                    <Columns>
                        <rad:GridTemplateColumn UniqueName="TemplateColumn" 
                            meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <a onmouseover='this.style.cursor = "hand"'>
                                    <asp:CheckBox ToolTip="Check" ID="chkSelectResponsibility" runat="server" 
                                    meta:resourcekey="chkSelectResponsibilityResource1">
                                    </asp:CheckBox>
                                </a>
                            </ItemTemplate>
                            <HeaderStyle Width="21px"/>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn UniqueName="SelectCommand" Reorderable="False" 
                            Resizable="False" ShowSortIcon="False" 
                            meta:resourcekey="GridTemplateColumnResource2">
                            <HeaderTemplate>
                                <asp:Label ID="selectionHeader" runat="server" 
                                    meta:resourcekey="selectionHeaderResource1"  />
                            </HeaderTemplate> 
                            <ItemTemplate>
                                <a onmouseover='this.style.cursor = "hand"'>
                                    <img id="selButton"  src ="~/RadControls/Grid/Skins/EMS/SortMenuGrid.gif" 
                                    runat="server"/>  
                                </a>
                            </ItemTemplate>
                            <HeaderStyle Width="13px"/>
                        </rad:GridTemplateColumn>      
                        <rad:GridTemplateColumn DataField="IdResponsibility" 
                            HeaderButtonType="TextButton" HeaderText="Id." Display="False"
                            SortExpression="IdResponsibility" UniqueName="IdResponsibility" 
                            meta:resourcekey="GridTemplateColumnResource3">
                            <ItemTemplate>
                                <asp:Label ID="lblIdResponsibility" runat="server" 
                                    Text='<%# Eval("IdResponsibility") %>' 
                                    meta:resourcekey="lblIdResponsibilityResource1"></asp:Label>
                            </ItemTemplate>     
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIdResponsibility" runat="server" 
                                    Text='<%# Bind("IdResponsibility") %>' 
                                    meta:resourcekey="txtIdResponsibilityResource1"></asp:TextBox>
                            </EditItemTemplate>           
                        </rad:GridTemplateColumn>
                        <rad:GridBoundColumn SortExpression="Responsibility" HeaderText="Responsibilities"
                            DataField="Responsibility" UniqueName="Responsibility" 
                            meta:resourcekey="GridBoundColumnResource1">
                        </rad:GridBoundColumn>              
                    </Columns>           
                    </MasterTableView>
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
            </rad:RadGrid>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOkDeleteResponsibilities" EventName="Click" />
        </Triggers>            
    </asp:UpdatePanel>

<!--Abro Mesanje post accion -->
    <div class="formdivContainerValidation">
    <asp:UpdatePanel ID="upSaveMessageResponsibilities" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="vsTemplate" EnableViewState="False" runat="server" 
                DisplayMode="List"  
                meta:resourcekey="vsTemplateResource1" />
            <div class="contentdivMessange">
                <asp:Label ID="lblMessage" runat="server" EnableViewState="False" 
                    meta:resourcekey="lblMessageResource1"></asp:Label>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOkDeleteResponsibilities" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>                   
    <!--Cierro Mesanje post accion -->
</div>  
</div>

<asp:UpdateProgress runat="server" ID="uProgMasterGrid" AssociatedUpdatePanelID="upMasterGridResponsibilities">
    <ProgressTemplate>
        <div class="Loading">
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdateProgress runat="server" ID="uProgSaveMessageResponsibilitiesManage" AssociatedUpdatePanelID="upSaveMessageResponsibilities">
    <ProgressTemplate>
        <div class="Loading">
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

 <!--Abro RadMenu-->	 
            <rad:RadMenu id="rmnSelectionResponsibilities" 
                runat="server"               
                
                IsContext="True"
                ContextMenuElementID="None"
                CollapseDelay="0"        
                style="left: 0px; top: 0px" CausesValidation="False" 
    meta:resourcekey="rmnSelectionResponsibilitiesResource1">
            <CollapseAnimation Type="None" Duration="0"></CollapseAnimation>

            <ExpandAnimation Type="None" Duration="0"></ExpandAnimation>
            </rad:RadMenu>
            <!--Cierro RadMenu-->
            
        <asp:Panel ID="pnlDeleteResponsibilities" runat="server" 
    meta:resourcekey="pnlDeleteResponsibilitiesResource1">
    <ajaxToolkit:ConfirmButtonExtender ID="cbelbDeleteResponsibilities" runat="server" TargetControlID="btnHidden"
        OnClientCancel="cancelClick" DisplayModalPopupID="mpelbDeleteResponsibilities" 
                Enabled="False" ConfirmText="" />
    <ajaxToolkit:ModalPopupExtender ID="mpelbDeleteResponsibilities" runat="server" TargetControlID="btnHidden"
        BehaviorID="programmaticModalPopupBehaviorResponsibilitiesManage" PopupControlID="pnlConfirmDeleteResponsibilities"
        CancelControlID="btnCancel" BackgroundCssClass="ModalPopUp" DynamicServicePath="" 
                Enabled="True" />
    <div class="contentpopup">
        <asp:Panel ID="pnlConfirmDeleteResponsibilities" runat="server" 
            Style="display: none" 
            meta:resourcekey="pnlConfirmDeleteResponsibilitiesResource1">
            <span><asp:Literal ID="liMsgConfirmDelete" runat="server" 
                Text="<%$ Resources:Common, msgConfirmDelete %>" /></span>
            <asp:Button ID="btnOkDeleteResponsibilities" CssClass="contentformBotton" 
                runat="server" Text="<%$ Resources:Common, btnOk %>" CausesValidation="False" 
                meta:resourcekey="btnOkDeleteResponsibilitiesResource1" />
            <asp:Button ID="btnCancel" CssClass="contentformBotton" runat="server" 
                Text="<%$ Resources:Common, btnCancel %>" 
                meta:resourcekey="btnCancelResource1" />
        </asp:Panel>
    </div>
    <asp:Button ID="btnHidden" runat="server" Visible="False" CausesValidation="False" 
                meta:resourcekey="btnHiddenResource1" />
</asp:Panel>

   
<!--Variables escondidas-->	               
            <input type="hidden" id="radGridClickedRowIndexResponsibilitiesManage" name="radGridClickedRowIndexResponsibilitiesManage" /> 
            <input type="hidden" id="radGridClickedTableIdResponsibilitiesManage" name="radGridClickedTableIdResponsibilitiesManage" />
            <input type="hidden" id="radMenuClickedIdResponsibilitiesManage" name="radMenuClickedIdResponsibilitiesManage" />
<!--Variables escondidas-->
                     
