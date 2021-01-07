<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ExceptionsWorking.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.ExceptionsWorking" Title="Working Exceptions" %>
    
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="cntContent" ContentPlaceHolderID="ContentMain" runat="server">
<div class="content">
        <div id="contentdivPadBackground">
            <!--Abro SiteMap-->
            <asp:SiteMapPath ID="smpEMS" runat="server"></asp:SiteMapPath>
            <!--Cierro SiteMap-->
        </div>
        <div id="contentdivContainerIconsTitle">
            <div id="contentdivContainerTitleIconExceptionsWorking"></div>
            <div id="contentdivContainerTitleTitle">
                <asp:Label ID="lblTitle" CssClass="contentdivContainerTitleTitle" runat="server" Text="Working Exceptions" />
                <asp:Label ID="lblSubTitle" CssClass="contentdivContainerSubTitle" runat="server" Text="<%$ Resources:CommonProperties, lblSubtitle %>"  Height="13px"/>
            </div>
            <asp:Label CssClass="contentdivContainerDetail" ID="lblDetail" runat="server" Text="Working Exceptions details"/>
        </div>
        
        <div class="contentmenu">
            <div id="contentmenudivBackground">
                <div id="contentmenudivPosition">                 
                 </div>
            </div>
        </div>
        <asp:UpdatePanel ID="upMasterGrid" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <rad:RadGrid ID="rgdMasterGrid" runat="server" AllowPaging="True" AllowSorting="True"
                     Width="100%" AutoGenerateColumns="False" GridLines="None" ShowStatusBar="False"
                    PageSize="18" OnSortCommand="rgdMasterGrid_SortCommand" OnNeedDataSource="rgdMasterGrid_NeedDataSource"
                    OnPageIndexChanged="rgdMasterGrid_PageIndexChanged" EnableAJAXLoadingTemplate="True"
                    LoadingTemplateTransparency="25">
                    <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                    <MasterTableView Width="100%" DataKeyNames="IdException" Name="gridMaster" CellPadding="0"
                        GridLines="None">
                        <Columns>
                            <rad:GridTemplateColumn UniqueName="TemplateColumn">
                                <HeaderTemplate>
                                    <a onmouseover='this.style.cursor = "hand"'>
                                        <asp:CheckBox ToolTip="Check All" ID="chkSelectHeader" onClick="javascript:Check(this);"
                                            runat="server" Enabled="true" />
                                    </a>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a onmouseover='this.style.cursor = "hand"'>
                                        <asp:CheckBox ToolTip="Check" ID="chkSelect" runat="server" onClick="javascript:Check(this);"
                                            CausesValidation="false"></asp:CheckBox>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Width="21px" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn UniqueName="SelectCommand" Reorderable="False" Resizable="False"
                                ShowSortIcon="False">
                                <HeaderTemplate>
                                    <asp:Label ID="selectionHeader" runat="server" Enabled="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a onmouseover='this.style.cursor = "hand"'>
                                        <img id="selButton" src="~/RadControls/Grid/Skins/EMS/SortMenuGrid.gif" runat="server" alt="" />
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Width="13px" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="IdException" HeaderButtonType="TextButton" HeaderText="Id Exception"
                                SortExpression="IdException" UniqueName="IdException" Visible="False">
                                <ItemTemplate>
                                    <asp:Label Visible="false" ID="IdExceptionLabel" runat="server" Text='<%# Eval("IdException") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox Visible="false" ID="IdExceptionTextBox" runat="server" Text='<%# Bind("IdException") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridBoundColumn SortExpression="Date" HeaderText="Date" DataField="Date" UniqueName="Date">
                            </rad:GridBoundColumn>
                            <rad:GridBoundColumn SortExpression="Source" HeaderText="Source" HeaderButtonType="TextButton"
                                DataField="Source" UniqueName="Source">
                            </rad:GridBoundColumn>
                            <rad:GridBoundColumn SortExpression="Status" HeaderText="Status"
                                DataField="Status" UniqueName="Status">
                            </rad:GridBoundColumn>
                        </Columns>
                        <RowIndicatorColumn Visible="False">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Resizable="False" Visible="False">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                    </MasterTableView>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="True" />
                    <ExportSettings>
                        <Pdf PageBottomMargin="" PageFooterMargin="" PageHeaderMargin="" PageHeight="11in"
                            PageLeftMargin="" PageRightMargin="" PageTopMargin="" PageWidth="8.5in" />
                    </ExportSettings>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>            

</div>
</asp:Content>

