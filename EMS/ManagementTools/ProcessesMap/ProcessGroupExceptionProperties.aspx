<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessGroupExceptionProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.ProcessesMap.ProcessGroupExceptionProperties"
    Title="EMS - Process Group Exception Properties" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

<div class="contentmenu">
            <div id="contentmenudivBackground">
                <div id="contentmenudivPosition">
                    <!--Opciones Generales -->
                    <asp:UpdatePanel ID="upMenuOption" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <rad:RadMenu ID="rmnOption" CausesValidation="False" runat="server" OnClientItemClicked="rmnOption_OnClientItemClickedHandler"
                                OnItemClick="rmnOption_ItemClick"  
                                meta:resourcekey="rmnOptionResource1">
                                <Items>
                                    <rad:RadMenuItem ID="rmnGeneralOptions" CssClass="GeneralOptions" 
                                        Text="General Options" runat="server" 
                                        meta:resourcekey="rmnGeneralOptionsResource1">
                                    </rad:RadMenuItem>
                                </Items>
                                <ExpandAnimation Type="None" Duration="0"></ExpandAnimation>
                                <CollapseAnimation Type="None" Duration="0"></CollapseAnimation>
                            </rad:RadMenu>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rmnOption" EventName="ItemClick" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!-- Fin Opciones Generales -->
                </div>
            </div>
        </div>
        
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Exception -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblIdParent" runat="server" 
                            Text="Exception:" meta:resourcekey="lblIdParentResource1" />
                       
                        </td>
                        <td class="ColContent">
                             <asp:Label  ID="lblIdExceptionValue" 
                            runat="server" meta:resourcekey="lblIdExceptionValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Id Group Exception -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblID" runat="server" 
                            Text="Id Group Exception:" meta:resourcekey="lblIDResource1" />
                        
                        </td>
                        <td class="ColContent">
                            <asp:Label  ID="lblIdValue" runat="server" 
                            meta:resourcekey="lblIdValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Language -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblLanguage" runat="server" 
                            Text="Language:" meta:resourcekey="lblLanguageResource1" />
                        
                        </td>
                        <td class="ColContent">
                            <asp:Label  ID="lblLanguageValue" runat="server" 
                            meta:resourcekey="lblLanguageValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Title -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblTitle" runat="server" 
                            Text="Title:" meta:resourcekey="lblTitleResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" SkinID="EMS" ID="txtTitle" 
                            MaxLength="150" meta:resourcekey="txtTitleResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                            ID="rfv1" ControlToValidate="txtTitle" ValidationGroup="Task" Display="Dynamic"
                            meta:resourcekey="rfv1Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Purpose -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblPurpose" runat="server" 
                            Text="Purpose:" meta:resourcekey="lblPurposeResource1" />
                        
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtPurpose" runat="server" SkinID="EMS"
                            meta:resourcekey="txtPurposeResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblDescription" runat="server" 
                            Text="Description:" meta:resourcekey="lblDescriptionResource1" />
                       
                        </td>
                        <td class="ColContent">
                             <asp:TextBox ID="txtDescription" runat="server" MaxLength="8000" SkinID="EMS"
                            TextMode="MultiLine" Rows="6"  meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Weight -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblWeight" runat="server" 
                            Text="Weight:" meta:resourcekey="lblWeightResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtWeight" runat="server" SkinID="EMS"
                            meta:resourcekey="txtWeightResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Threshold -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label  ID="lblThreshold" runat="server" 
                            Text="Threshold:" meta:resourcekey="lblThresholdResource1" />
                        
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="TxtThreshold" runat="server" SkinID="EMS"
                            meta:resourcekey="TxtThresholdResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
             </td>
         </tr>
    </table> 
    
        
            
            <div class="formdivContainerBotton">
                <asp:Button runat="server" Text="<%$ Resources:Common, btnSave %>" ID="btnSave" OnClick="btnSave_Click"
                    ValidationGroup="FuntionalArea" meta:resourcekey="btnSaveResource1" />
            </div>
            <!--Tab Strip-->
            <rad:RadTabStrip ID="rtsMainTab" runat="server"  AutoPostBack="True" 
                OnClientTabSelected="SetSelectedCssClass" 
                meta:resourcekey="rtsMainTabResource1">
                <Tabs>
                    <rad:RadTab Value="ExtendedProperty" runat="server" Text="Extended Properties">
                    </rad:RadTab>
                </Tabs>
            </rad:RadTabStrip>
            <div class="contentmenu">
                <div class="contentmenudivBackground">
                    <div class="contentmenudivPosition">
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="udpMainTab" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <asp:Panel runat="server" ID="pnlTabContainer" 
                            meta:resourcekey="pnlTabContainerResource1">
                        </asp:Panel>
                        <div class="formdivContainerBotton">
                            <asp:Button ID="btnMainTabSave" runat="server" 
                                Text="<%$ Resources:Common, btnSave %>" 
                                meta:resourcekey="btnMainTabSaveResource1" />
                        </div>
                        <asp:HiddenField ID="hdnMainTabState" runat="server" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rtsMainTab" EventName="TabClick" />
                    <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="rmnOption" EventName="ItemClick" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <!-- End Tab Strip -->
            
    <!--Variables escondidas-->
    <asp:Button ID="btnTransferAdd" runat="server" OnClick="btnTransferAdd_Click" 
        Style="display: none" meta:resourcekey="btnTransferAddResource1" />
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
    <asp:Button ID="btnHidden" runat="server" Visible="False" 
        CausesValidation="False" meta:resourcekey="btnHiddenResource1" />
    <input type="hidden" id="radMenuItemClicked" name="radMenuItemClicked" />
    
    <%--Cierra menu opciones generales--%>
    <asp:Panel ID="pnlDelete" runat="server" meta:resourcekey="pnlDeleteResource1">
        <ajaxToolkit:ConfirmButtonExtender ID="cbelbDelete" runat="server" TargetControlID="btnHidden"
            OnClientCancel="cancelClick" DisplayModalPopupID="mpelbDelete" 
            Enabled="False" ConfirmText="" />
        <ajaxToolkit:ModalPopupExtender ID="mpelbDelete" runat="server" TargetControlID="btnHidden"
            BehaviorID="programmaticModalPopupBehavior" PopupControlID="pnlConfirmDelete"
            CancelControlID="btnCancel" BackgroundCssClass="ModalPopUp" 
            DynamicServicePath="" Enabled="True" />
        <div class="contentpopup">
            <asp:Panel ID="pnlConfirmDelete" runat="server" Style="display: none" 
                meta:resourcekey="pnlConfirmDeleteResource1">
                <span>
                    <asp:Literal ID="liMsgConfirmDelete" runat="server" 
                    Text="<%$ Resources:Common, msgConfirmDelete %>" /></span>
                <asp:Button ID="btnOk" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnOk %>"
                    CausesValidation="False" OnClick="btnOkDelete_Click" BorderStyle="None" 
                    meta:resourcekey="btnOkResource1" />
                <asp:Button ID="btnCancel" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnCancel %>"
                    BorderStyle="None" meta:resourcekey="btnCancelResource1" />
            </asp:Panel>
        </div>
    </asp:Panel>

    <script type="text/javascript">
     //Inicializacion de Variables PageRequestManager
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    var postBackElement;

    prm.add_initializeRequest(InitializeRequest);
    prm.add_endRequest(EndRequest);
    
    //Handlers          
    function InitializeRequest(sender, args)
    { 
        postBackElement = args.get_postBackElement();
        
        //ExtProps DropDown Functions
        InitDdSelIndex();
        ResetDllChangedValue(postBackElement);
        
        //Si es un LinkButton Attacheado a la Grilla (sino puede ser cualquier evento de la Grilla (paginador etc)
        if (document.getElementById("radMenuItemClicked").value== "Language")
        {
            args.set_cancel(true);
            //Dispara el OnClick del btnHidden emulando un post back normal por sobre el async que dispara el Boton de la Grilla
            DoNormalPostBack(document.getElementById("<%= btnTransferAdd.ClientID %>"));
        }
    }
    
    function EndRequest(sender, args)
    { 
        var allTabs = <%= rtsMainTab.ClientID %>.AllTabs;
        var tabState = $get('ctl00$ContentMain$hdnMainTabState').value;
        
        for (var i = 0; i < allTabs.length; i++)
        {
            if(tabState == "enabled")
                allTabs[i].Enable();
            
            if(tabState == "disabled")
                allTabs[i].Disable();
        }
    }

    //El Handler que Cancela el PostBack "Asyncronic" del UpdatePanel, termina haciendo un PostBack Normal
    function DoNormalPostBack(btnPostBack)
    {
        btnPostBack.click();
    }
    
    //Parche para que cambie el Estilo del Tab Seleccionado del lado del cliente cuando el TabStrip es Trigger de un UpdatePanel
    function SetSelectedCssClass()
    {
        //document.getElementById('ctl00_ContentMain_rtsMainTab_tab' + currentTab.Value).className = "selected";
        var tabStrip = <%= rtsMainTab.ClientID%>; 
        var allTabs = tabStrip.AllTabs;
                
        for (var i = 0; i < allTabs.length; i++)
        {
            var cssSelected = "";
            if(allTabs[i] == tabStrip.SelectedTab)
                cssSelected = "selected";
            
            allTabs[i].DomElement.className = cssSelected;
        }
    }
    
   

    //******************************************************

    /////////////////////////////////////////////////////////////////////
    ///
    ///     Funciones para mostrar el popup
    ///
    /////////////////////////////////////////////////////////////////////

    //Esta funcion se ejecuta al hacer click sobre un item del menuRad
    function rmnOption_OnClientItemClickedHandler(sender, eventArgs)
    {
        //cargo las constantes
        var mioptAdd = "m0"; //menu item Add
        var mioptLanguage = "m1"; //menu item Language
        var mioptDelete = "m2";  //menu item Delete

       if (eventArgs.Item.ID=="ctl00_ContentMain_rmnOption_m0")
        {
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            StopEvent(eventArgs);     //window.event.returnValue = false;
        }
        else
        {
            //Se guarda el id del item que fue seleccionado del menu
            var strMenuItemId = eventArgs.Item.ID;

            //Aca se guarda si se ejecuto el menu Option o el Selection
            document.getElementById("radMenuClickedId").value = "Option"

            //solo muestra el popup cuando piden borrar
            if (strMenuItemId.substring(strMenuItemId.lastIndexOf("_") + 1) == mioptDelete)
            {
               //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
               StopEvent(eventArgs);     //window.event.returnValue = false;
               //Busca el popup
               var modalPopupBehavior = $find('programmaticModalPopupBehavior');
               //muestra el popup
               modalPopupBehavior.show();
            }
            else
            {
                //al item que hacer server.transfer, lo identifico.
                if (strMenuItemId.substring(strMenuItemId.lastIndexOf("_") + 1) == mioptLanguage)
                {
                    $get("<%= rmnOption.ClientID %>").style.display = "none";
                    $get("<%= uProgProperties.ClientID %>").style.display = "block";
                    document.getElementById("radMenuItemClicked").value = "Language";
                }
            }
        }
    }

var valueHasChanged = false;

//Para persisitir el Indice del DropDown
var ddSelIndex;
//window.onload = InitDdSelIndex;
if (_BrowserName == _IEXPLORER) {   //IE and Opera
    window.attachEvent('onload', InitDdSelIndex);
}
else {   //FireFox
    document.addEventListener('DOMContentLoaded', InitDdSelIndex, false);
}

function InitDdSelIndex()
{
    var ddExtProp = $get('ctl00$ContentMain$DropDownEPClasification');
    if(ddExtProp != null)
        ddSelIndex = ddExtProp.selectedIndex;
}

function ResetDdSelIndexes()
{
    var ddExtProp = $get('ctl00$ContentMain$DropDownEPClasification');
    if(ddExtProp != null)
        ddExtProp.selectedIndex = ddSelIndex;
} 

function ResetDllChangedValue(postBackElement)
{
    try
    {
        if(postBackElement.id == "ctl00_ContentMain_btnMainTabSave")
        {
            valueHasChanged = false;
            $get("<%= uProgProperties.ClientID %>").style.display = "block";
        }
    }
    catch(ex) {}
}

function CheckExtendedProperty(chkBox, textBoxId)
{
    valueHasChanged = true;
    
    var _chkBox = chkBox;
    var _txtBox = document.getElementById(textBoxId);
    
    if(chkBox.checked)
    {
        _txtBox.disabled = false;
        //_txtBox.style.backgroundColor = '';
        _txtBox.focus();
    }
    else
    {
        //_txtBox.style.backgroundColor = '#e1e0e0';
        _txtBox.value = '';
        _txtBox.disabled = true;
        _txtBox.blur();
    }
    
    //changeColor(_chkBox);
}

function ValidateTextBox(txtBox, chkBoxId)
{
    var _txtBox = txtBox;
    var _chkBox = document.getElementById(chkBoxId);
    
    valueHasChanged = true;
    
    if(_txtBox.value == '')
    {
//        alert('A value is required.');
//        _txtBox.focus();
    }
}

function CheckSaveStatus()
{
    if(valueHasChanged)
    {
        if(confirm('There has been changes made on the Extended Properties.\nDo you want to continue without saving? (changes will be lost)'))
        {
            valueHasChanged = false;
            DoPostBackDropDown();
        }
        else
        {
            ResetDdSelIndexes();
        }
    }
    else
    {
        DoPostBackDropDown();
    }
}

function DoPostBackDropDown()
{
    setTimeout('__doPostBack(\'ctl00$ContentMain$DropDownEPClasification\',\'\')', 0)
}
    </script>

</asp:Content>
