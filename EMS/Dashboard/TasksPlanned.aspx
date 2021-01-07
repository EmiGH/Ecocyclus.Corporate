<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="TasksPlanned.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Dashboard.TasksPlanned" Title="Planned Tasks" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="contentmenu">
        <div id="contentmenudivBackground">
            <div id="contentmenudivPosition">
                <div style="margin: 2px 10px 0 0">
                    <asp:Panel ID="pnlSearch" runat="server">
                    <asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Always">
                       <ContentTemplate>
                        <rad:RadDatePicker Width="350px" OnSelectedDateChanged="rcSelectDay_SelectedDateChanged"
                            AutoPostBack="true" ID="rcSelectDay" runat="server" CssClass="Calendar">
                            <DatePopupButton Visible="false" />
                            <DateInput BackColor="Transparent" ForeColor="#ffffff" BorderColor="Transparent"
                                Font-Bold="true" runat="server" ID="dtinSelectDay" DateFormat="D" CssClass="DateInput" >
                            </DateInput>
                        </rad:RadDatePicker>
                       </ContentTemplate>
                      </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <!--Abro Filtro-->
        <asp:Panel ID="pnlTasks" runat="server">
        </asp:Panel>

        <script type="text/javascript">
    function PopupAbove(e, pickerID)
    {
    var datePicker = <%= rcSelectDay.ClientID %>;
    var textBox = datePicker.GetTextBox()
    var position = datePicker.GetElementPosition(textBox);

    datePicker.ShowPopup(position.x - 0, position.y - 0);
    
    //datePicker.ShowPopup(0 , 0);

    
    //var popupElement = datePicker.get_popupContainer();
    //var Magnitudes = datePicker.getElementMagnitudes(popupElement);
    

    //datePicker.showPopup(position.x, position.y - Magnitudes.height);
    //datePicker.ShowPopup();
}

        </script>
</asp:Content>
