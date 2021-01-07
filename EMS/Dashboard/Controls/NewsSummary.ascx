<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsSummary.ascx.cs"
    Inherits="Condesus.EMS.WebUI.Dashboard.Controls.NewsSummary" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="contentformDashboard">
    <rad:RadGrid ID="rgdMasterGrid" runat="server" AllowPaging="True" AllowSorting="True"
         Width="100%" AutoGenerateColumns="False" GridLines="None" PageSize="18"
        OnSortCommand="rgdMasterGrid_SortCommand" OnNeedDataSource="rgdMasterGrid_NeedDataSource"
        OnPageIndexChanged="rgdMasterGrid_PageIndexChanged"
        meta:resourcekey="rgdMasterGridResource1">
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
        <MasterTableView Width="100%" DataKeyNames="idProject" Name="gridMaster" CellPadding="0"
            GridLines="None">
            <Columns>
                <rad:GridTemplateColumn DataField="idProject" HeaderButtonType="TextButton" HeaderText="Id Project"
                    SortExpression="idProject" UniqueName="idProject" Visible="False" meta:resourcekey="GridTemplateColumnResource1">
                    <ItemTemplate>
                        <asp:Label Visible="False" ID="idProjectLabel" runat="server" Text='<%# Eval("idProject") %>'
                            meta:resourcekey="idProjectLabelResource1"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox Visible="False" ID="idProjectTextBox" runat="server" Text='<%# Bind("idProject") %>'
                            meta:resourcekey="idProjectTextBoxResource1"></asp:TextBox>
                    </EditItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridBoundColumn SortExpression="ProjectTitle" HeaderText="Project Title" DataField="ProjectTitle"
                    UniqueName="ProjectTitle" meta:resourcekey="GridBoundColumnResource1">
                </rad:GridBoundColumn>
                <rad:GridBoundColumn SortExpression="Date" HeaderText="Date" DataField="Date" UniqueName="Date"
                    meta:resourcekey="GridBoundColumnResource2">
                </rad:GridBoundColumn>
                <rad:GridBoundColumn SortExpression="Comment" HeaderText="Comment" DataField="Comment"
                    UniqueName="Comment" meta:resourcekey="GridBoundColumnResource3">
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
</div>
