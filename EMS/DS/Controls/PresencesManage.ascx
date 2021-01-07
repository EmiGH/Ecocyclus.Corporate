<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PresencesManage.ascx.cs" Inherits="Condesus.EMS.WebUI.DS.Controls.PresencesManage" %>
    
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

 <div class="contentmenu">
    <div class="contentmenudivBackground">
        <div class="contentmenudivPosition">  
            <rad:RadMenu ID="rmnOptionPresences" runat="server" CausesValidation="False"
                OnItemClick="rmnOptionPresences_ItemClick"
                OnClientItemClicked="rmnOptionPresences_OnClientItemClickedHandler" 
                 meta:resourcekey="rmnOptionPresencesResource1">
                <Items>
                    <rad:RadMenuItem ID="rmnGeneralOptions" CssClass="GeneralOptions" Text="General Options" ></rad:RadMenuItem>
                </Items>
              <ExpandAnimation Type="None"></ExpandAnimation>
              <CollapseAnimation Type="None"></CollapseAnimation>
            </rad:RadMenu>
        </div>
    </div>            
    <asp:UpdatePanel ID="upMasterGridPresences" runat="server">
        <ContentTemplate>
            <!--Abro RadGrid-->
            <rad:RadGrid 
                ID="rgdMasterGridPresences"   
                runat="server" 
                AllowPaging="True" 
                AllowSorting="True" 
                
                Width="100%" 
                AutoGenerateColumns="False" 
                GridLines="None"
                PageSize="20"
                EnableAJAXLoadingTemplate="True" 
                LoadingTemplateTransparency="25" 
                meta:resourcekey="rgdMasterGridPresencesResource1" >
             
                <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />

                <MasterTableView Width="100%" DataKeyNames="IdPresence" Name="gridMaster" CellPadding="0" GridLines="None">
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
                                    <asp:CheckBox ToolTip="Check" ID="chkSelectPresence" runat="server" 
                                    meta:resourcekey="chkSelectPresenceResource1">
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
                        <rad:GridTemplateColumn DataField="IdPresence" HeaderButtonType="TextButton" 
                            HeaderText="Id." Display="False"
                            SortExpression="IdPresence" UniqueName="IdPresence" 
                            meta:resourcekey="GridTemplateColumnResource3">
                            <ItemTemplate>
                                <asp:Label ID="lblIdPresence" runat="server" Text='<%# Eval("IdPresence") %>' 
                                    meta:resourcekey="lblIdPresenceResource1"></asp:Label>
                            </ItemTemplate>     
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIdPresence" runat="server" Text='<%# Bind("IdPresence") %>' 
                                    meta:resourcekey="txtIdPresenceResource1"></asp:TextBox>
                            </EditItemTemplate>           
                        </rad:GridTemplateColumn>
                        <rad:GridBoundColumn SortExpression="Presence" HeaderText="Presences"
                            DataField="Presence" UniqueName="Presence" 
                            meta:resourcekey="GridBoundColumnResource1">
                        </rad:GridBoundColumn>              
                    </Columns>           
                </MasterTableView>
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="True" />
            </rad:RadGrid>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>	
    <!--Abro Mesanje post accion -->
    
    <asp:UpdatePanel ID="upSaveMessagePresences" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="vsTemplate" EnableViewState="False" runat="server" 
                CssClass="contentExclamation" DisplayMode="List"  
                meta:resourcekey="vsTemplateResource1" />
                  <div class="contentdivMessange">   
                <asp:Label ID="lblMessage" runat="server" EnableViewState="False" 
                          meta:resourcekey="lblMessageResource1"></asp:Label>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
                
    <!--Cierro Mesanje post accion -->
</div>

<!--Variables escondidas-->	               

<input type="hidden" id="radGridClickedRowIndexPresencesManage" name="radGridClickedRowIndexPresencesManage"/> 
<input type="hidden" id="radGridClickedTableIdPresencesManage" name="radGridClickedTableIdPresencesManage"/>
<input type="hidden" id="radMenuClickedIdPresencesManage" name="radMenuClickedIdPresencesManage"/>
<input type="hidden" id="radMenuItemClickedPresencesManage" name="radMenuItemClickedPresencesManage"/>
<!--Variables escondidas-->

<!--Abro RadMenu-->	 
<rad:RadMenu id="rmnSelectionPresences" 
    runat="server"               
    
    IsContext="True"
    ContextMenuElementID="None"
    CollapseDelay="0"        
    style="left: 0px; top: 0px" CausesValidation="False" 
    meta:resourcekey="rmnSelectionPresencesResource1">
<CollapseAnimation Type="None" Duration="0"></CollapseAnimation>
<ExpandAnimation Type="None" Duration="0"></ExpandAnimation>
</rad:RadMenu>
<!--Cierro RadMenu-->

<asp:Panel ID="pnlDeletePresence" runat="server" 
    meta:resourcekey="pnlDeletePresenceResource1">
    <ajaxToolkit:ConfirmButtonExtender ID="cbelbDeletePresence" runat="server" 
        TargetControlID="btnHidden"
        OnClientCancel="cancelClick" 
        DisplayModalPopupID="mpelbDeletePresence" Enabled="False" ConfirmText="" />
    <ajaxToolkit:ModalPopupExtender ID="mpelbDeletePresence" runat="server" 
    TargetControlID="btnHidden" 
    BehaviorID="programmaticModalPopupBehaviorPresencesManage"
    PopupControlID="pnlConfirmDeletePresence" 
    CancelControlID="btnCancel" 
    BackgroundCssClass="ModalPopUp" DynamicServicePath="" Enabled="True" />
    
    <div class="contentpopup"> 
    <asp:Panel ID="pnlConfirmDeletePresence" runat="server" style="display:none" 
            meta:resourcekey="pnlConfirmDeletePresenceResource1">
        <span><asp:Literal ID="liMsgConfirmDelete" runat="server" 
            Text="<%$ Resources:Common, msgConfirmDelete %>" /></span>
            <asp:Button ID="btnOk" CssClass="contentformBotton" runat="server" 
            Text="<%$ Resources:Common, btnOk %>" CausesValidation="False" 
            meta:resourcekey="btnOkResource1" />
            <asp:Button ID="btnCancel" CssClass="contentformBotton" runat="server" 
            Text="<%$ Resources:Common, btnCancel %>" 
            meta:resourcekey="btnCancelResource1" />
    </asp:Panel>
    </div>
    <asp:Button ID="btnHidden" runat="server" Visible="False" 
        CausesValidation="False" meta:resourcekey="btnHiddenResource1" />     
</asp:Panel>

            
<asp:UpdateProgress runat="server" ID="uProgMasterGridPresences" AssociatedUpdatePanelID="upMasterGridPresences" >
    <ProgressTemplate>
        <div class="Loading"></div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdateProgress runat="server" ID="uProgSaveMessagePresenceManage" AssociatedUpdatePanelID="upSaveMessagePresences">
    <ProgressTemplate>
        <div class="Loading">
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

