<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ExceptionsOpened.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.ExceptionsOpened" Title="Opened Exceptions" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

        <asp:UpdatePanel ID="upMasterGrid" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <rad:RadGrid ID="rgdMasterGrid" runat="server" AllowPaging="True" AllowSorting="True"
                     Width="100%" AutoGenerateColumns="False" GridLines="None" ShowStatusBar="False"
                    PageSize="18" OnSortCommand="rgdMasterGrid_SortCommand" OnNeedDataSource="rgdMasterGrid_NeedDataSource"
                    OnPageIndexChanged="rgdMasterGrid_PageIndexChanged">
                    <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                    <MasterTableView Width="100%" DataKeyNames="IdException" Name="gridMaster" CellPadding="0"
                        GridLines="None">
                        <Columns>
                            <rad:GridTemplateColumn UniqueName="TemplateColumn">
                    
                                <ItemTemplate>
                                    <a onmouseover='this.style.cursor = "hand"'>
                                        <asp:CheckBox ToolTip="Check" ID="chkSelect" runat="server" onClick="javascript:Check(this);"
                                            CausesValidation="false"></asp:CheckBox>
                                    </a>
                                </ItemTemplate>
                          
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


</asp:Content>
