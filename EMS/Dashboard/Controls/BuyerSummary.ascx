<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuyerSummary.ascx.cs"
    Inherits="Condesus.EMS.WebUI.BuyerSummary" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div class="contentformDashboard">
    <rad:RadGrid ID="rgdMasterGrid" runat="server" AllowSorting="True" 
        Width="100%" AutoGenerateColumns="False" GridLines="None" ShowStatusBar="True"
        PageSize="18" OnSortCommand="rgdMasterGrid_SortCommand" OnNeedDataSource="rgdMasterGrid_NeedDataSource"
        OnItemCreated="rgdMasterGrid_ItemCreated" EnableAJAXLoadingTemplate="True" LoadingTemplateTransparency="25"
        meta:resourcekey="rgdMasterGridResource1">
        <PagerStyle AlwaysVisible="false" Mode="NextPrevAndNumeric" />
        <ClientSettings Selecting-AllowRowSelect="true">
            <Selecting AllowRowSelect="True"></Selecting>
            <ClientEvents OnRowContextMenu="RowContextMenu" />
        </ClientSettings>
        <MasterTableView Width="100%" DataKeyNames="idProject" Name="gridMaster" CellPadding="0"
            GridLines="None">
            <Columns>
                <rad:GridTemplateColumn DataField="idProject" HeaderButtonType="TextButton" HeaderText="Id Project"
                    SortExpression="idProject" UniqueName="idProject" Display="false" meta:resourcekey="GridTemplateColumnResource1">
                    <ItemTemplate>
                        <asp:Label ID="idProjectLabel" runat="server" Text='<%# Eval("idProject") %>' meta:resourcekey="idProjectLabelResource1"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="idProjectTextBox" runat="server" Text='<%# Bind("idProject") %>'
                            meta:resourcekey="idProjectTextBoxResource1"></asp:TextBox>
                    </EditItemTemplate>
                </rad:GridTemplateColumn>
                <rad:GridTemplateColumn UniqueName="SelectCommand" Reorderable="False" Resizable="False"
                    ShowSortIcon="False" meta:resourcekey="GridTemplateColumnResource2">
                    <HeaderTemplate>
                        <asp:Label ID="selectionHeader" runat="server" meta:resourcekey="selectionHeaderResource1" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <a onmouseover='this.style.cursor = "hand"'>
                            <img id="selButton" src="~/RadControls/Grid/Skins/EMS/SortMenuGrid.gif" runat="server" />
                        </a>
                    </ItemTemplate>
                    <HeaderStyle Width="13px" />
                </rad:GridTemplateColumn>
                <rad:GridBoundColumn SortExpression="ProjectTitle" HeaderText="Project Title" DataField="ProjectTitle"
                    UniqueName="ProjectTitle" meta:resourcekey="GridBoundColumnResource1">
                </rad:GridBoundColumn>
                <rad:GridBoundColumn SortExpression="Estimated" HeaderText="Estimated PDD ERs" DataField="Estimated"
                    UniqueName="Estimated" meta:resourcekey="GridBoundColumnResource2">
                </rad:GridBoundColumn>
                <rad:GridBoundColumn SortExpression="Actual" HeaderText="Actual ERs" DataField="Actual"
                    UniqueName="Actual" meta:resourcekey="GridBoundColumnResource3">
                </rad:GridBoundColumn>
                <rad:GridBoundColumn SortExpression="Issued" HeaderText="Issued ERs" DataField="Issued"
                    UniqueName="Issued" meta:resourcekey="GridBoundColumnResource4">
                </rad:GridBoundColumn>
                <rad:GridBoundColumn SortExpression="Status" HeaderText="Status" DataField="Status"
                    UniqueName="Status" meta:resourcekey="GridBoundColumnResource5">
                </rad:GridBoundColumn>
                <rad:GridBoundColumn SortExpression="News" HeaderText="News" DataField="News" UniqueName="News"
                    meta:resourcekey="GridBoundColumnResource6">
                </rad:GridBoundColumn>
                <rad:GridTemplateColumn UniqueName="reportCommand" Reorderable="False" Resizable="False"
                    HeaderText="Report" ShowSortIcon="False" meta:resourcekey="GridTemplateColumnResource3">
                    <ItemTemplate>
                        <a onmouseover='this.style.cursor = "hand"'>
                            <img id="rptButton" class="DocumentGrid" src="~/Skins/Images/Trans.gif" runat="server" />
                        </a>
                    </ItemTemplate>
                    <HeaderStyle Width="13px" />
                </rad:GridTemplateColumn>
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
<!--Abro Loading-->
<asp:UpdateProgress runat="server" ID="uProgMasterGrid">
    <ProgressTemplate>
        <div class="Loading">
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<!--Cierro Loading-->
<!--Variables escondidas-->
<input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
<input type="hidden" id="radGridClickedTableId" name="radGridClickedTableId" />
<!--Abro RadMenu-->
<rad:RadMenu ID="rmnSelection" runat="server"  CollapseDelay="0" OnClientItemClicked="rmnSelection_OnClientItemClickedHandler"
    OnItemClick="rmnSelection_ItemClick" Style="left: 0px; top: 0px" meta:resourcekey="rmnSelectionResource1">
    <CollapseAnimation Type="None" Duration="0"></CollapseAnimation>
    <ExpandAnimation Type="None" Duration="0"></ExpandAnimation>
</rad:RadMenu>
<!--Abro RadMenu-->

<script type="text/javascript">
//Esta funcion es la encargada de mostrar el menu de opciones cuando se selecciona el campo de seleccion en la grilla
//Parametros    <e> event
//          <idGridRow> el indice del row en donde esta parado
//          <idGridTable> el id de la tabla donde esta parado(por si es jerarquica)
function ShowReport(e, idGridRow, idGridTable)
{
    //Obtiene el control de la grilla
    var grid = <%= rgdMasterGrid.ClientID %>;
    //Realiza la seleccion del registro en el que se hace click
    grid.MasterTableView.SelectRow(grid.MasterTableView.Rows[idGridRow].Control, true);
    //Se obtiene el id con el KeyValue
    var idProject = grid.MasterTableView.GetCellByColumnUniqueName(grid.MasterTableView.Rows[idGridRow],"idProject").innerText;
    //Abre una nueva ventana con el reporte.
    var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/Reports/ProjectReports.aspx?IdProyect=" + idProject, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes'); 
    newWindow.focus(); 
    //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
    StopEvent(e);     //window.event.returnValue = false;
}

//Esta funcion es la encargada de mostrar el menu de opciones cuando se realiza click derecho sobre un registro en la grilla.
//Parametros, <index> es el indice del registro donde esta parado
//        <e> es el evento
function RowContextMenu(index, e)
{
    if(this.Rows[index].ItemType != "NestedView")
    { 
        //Accede al menu
        var MyMenu = <%= rmnSelection.ClientID %>;
        //Se guarda el indice del row en donde esta parado y el id de la tabla
        document.getElementById("radGridClickedRowIndex").value = this.Rows[index].RealIndex;
        document.getElementById("radGridClickedTableId").value = this.UID;

        //Muestra el menu
        MyMenu.Show(e);

        e.cancelBubble = true;
        e.returnValue = false;

        if (e.stopPropagation)
        {
            e.stopPropagation();
            e.preventDefault();
        }
        //Pone al registro en selected.
        this.SelectRow(this.Rows[index].Control, true);
    }
}
function rmnSelection_OnClientItemClickedHandler(sender, eventArgs)
{
    $get("<%= uProgMasterGrid.ClientID %>").style.display = "block";
}
</script>

