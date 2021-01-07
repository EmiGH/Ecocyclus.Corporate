using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.TestBackEnd
{
    public partial class ListViewer : BasePage
    {
        #region Internal Properties
            RadGrid _rgdMasterGridListViewer;
            private String _EntityNameGRC = String.Empty;
            private String _EntityName = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            private String _PageTitleLocal = String.Empty;
        #endregion

        #region PageLoad & Init
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);
                
                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
            }
            protected override void OnUnload(EventArgs e)
            {
                base.OnUnload(e);

                ManageEntityParams = new Dictionary<String, Object>();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);

                //Inicializa la variable de parametros.
                ManageEntityParams = new Dictionary<String, Object>();
                //Debe recorrer las PK para saber si es un Manage de Lenguajes.
                String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                ManageEntityParams = GetKeyValues(_pkValues);

                ////Se guarda todos los parametros que recibe... si es que no vienen por PK
                foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                {
                    if (_item.Key != "EntityName")
                    {
                        if (!ManageEntityParams.ContainsKey(_item.Key))
                        {
                            ManageEntityParams.Add(_item.Key, _item.Value);
                        }
                    }
                }


                //Setea el nombre de la entidad que se va a mostrar
                _EntityName = base.NavigatorGetTransferVar<String>("EntityName");
                EntityNameGrid = base.NavigatorGetTransferVar<String>("EntityName");
                if (base.NavigatorContainsTransferVar("EntityNameContextInfo"))
                {
                    _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
                }
                if (base.NavigatorContainsTransferVar("EntityNameContextElement"))
                {
                    _EntityNameContextElement = base.NavigatorGetTransferVar<String>("EntityNameContextElement");
                }
                //Para el caso del Remove, debe ser el nombre de la entidad mas la palabra Remove!!!
                EntityNameToRemove = _EntityName + "Remove";

                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(_EntityName, ManageEntityParams);
                //Arma la grilla completa
                LoadListViewer();

                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
                //Carga el Menu de Context ElementMaps a la derecha.
                LoadContextElementMapsByEntity();
                //Carga el menu de opciones generales.
                LoadGeneralOptionMenu();
                //Carga el menu de Seguridad...
                LoadSecurityOptionMenu();
                //Inserta todos los manejadores de eventos que necesita la apgina
                InitializeHandlers();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                _PageTitleLocal = base.NavigatorGetTransferVar<String>("PageTitle");
                String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                if (String.IsNullOrEmpty(_PageTitleLocal))
                {
                    //el metodo GetPageTitleForViewer, obtiene el nombre del dato que se esta mostrando...
                    String _titleEntityName = GetGlobalResourceObject("CommonListManage", _EntityName) != null ? GetGlobalResourceObject("CommonListManage", _EntityName).ToString() : String.Empty;
                    _PageTitleLocal = _titleEntityName + ": " + GetPageTitleForViewer();
                    base.PageTitle = _PageTitleLocal;
                    base.PageTitleSubTitle = Resources.CommonListManage.lblSubtitle;
                }
                else
                {
                    base.PageTitle = _PageTitleLocal;
                    base.PageTitleSubTitle = _pageSubTitle;
                }
                
            }
            protected override void SetPagetitle()
            {
                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                _PageTitleLocal = base.NavigatorGetTransferVar<String>("PageTitle");
                if (String.IsNullOrEmpty(_PageTitleLocal))
                {
                    //el metodo GetPageTitleForViewer, obtiene el nombre del dato que se esta mostrando...
                    String _titleEntityName = GetGlobalResourceObject("CommonListManage", _EntityName) != null ? GetGlobalResourceObject("CommonListManage", _EntityName).ToString() : String.Empty;
                    _PageTitleLocal = _titleEntityName + ": " + GetPageTitleForViewer();
                    base.PageTitle = _PageTitleLocal;
                }
                else
                {
                    base.PageTitle = _PageTitleLocal;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                try
                {
                    String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                    if (String.IsNullOrEmpty(_pageSubTitle))
                    {
                        base.PageTitleSubTitle = Resources.Common.PageSubtitleView;
                    }
                    else
                    {
                        base.PageTitleSubTitle = _pageSubTitle;
                    }
                }
                catch { base.PageTitleSubTitle = String.Empty; }
            }
        #endregion

        #region Page Event
            protected void GridLinkButtonClick_Click(object sender, EventArgs e)
            {
                //Setea los parametros en el Navigate.
                BuildNavigateParamsFromListManageSelected(_rgdMasterGridListViewer);

                //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                    + "&" + BuildParamsFromListManageSelected(_rgdMasterGridListViewer);
                NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                String _paramsToNavigate = _pkCompost;
                String _entityName = Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"));
                NavigatorAddTransferVar("EntityName", _entityName);
                NavigatorAddTransferVar("EntityNameGrid", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameGrid")));
                NavigatorAddTransferVar("EntityNameContextInfo", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextInfo")));
                NavigatorAddTransferVar("EntityNameContextElement", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextElement")));

                //Navigate(GetPageViewerByEntity(Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"))), Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));
                //Navigate("~/MainInfo/ListViewer.aspx", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));

                String _url = GetPageViewerByEntity(_entityName);
                IButtonControl _lnkButton = (IButtonControl)sender;
                String _entityPropertyName = _lnkButton.Text;
                NavigateEntity(_url, _entityName, _entityPropertyName, NavigateMenuType.ListManagerMenu);
            }
            protected void rgdMasterGridListViewer_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                    {
                        if (column.UniqueName == "Value")
                        {
                            if (String.IsNullOrEmpty(((LinkButton)((GridDataItem)e.Item)["Value"].Controls[0]).Text))
                            {
                                ((LinkButton)((GridDataItem)e.Item)["Value"].Controls[0]).Text = "&nbsp;";
                            }
                        }
                    }
                }
            }
            protected void rmnuGeneralOption_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
            {
                String _urlProperties = String.Empty;
                String _actionTitleDecorator = GetActionTitleDecorator(e.Item);

                switch (e.Item.Value)
                {
                    case "rmiAdd": //ADD
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo.
                        base.NavigatorClearTransferVars();
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }
                        if (base.NavigatorContainsTransferVar("ParentEntity"))
                        {
                            base.NavigatorAddTransferVar("ParentEntity", NavigatorGetTransferVar<String>("ParentEntity"));
                        }
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);
                        
                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                        //Navigate(_urlProperties, _EntityName);
                        NavigateEntity(_urlProperties, _EntityName, _actionTitleDecorator, NavigateMenuAction.Add);
                        break;

                    case "rmiEdit":  //EDIT
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                        //Si quedaron variables que no estaban en el PKCompost, las leo del NavigatorTransferenceColl..
                        foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                        {
                            if (_item.Key.Substring(0, 2) == "Id")
                            {
                                if (String.IsNullOrEmpty(_pkValues))
                                //Si esta vacio, solo pone el key...
                                { _pkValues += _item.Key + "=" + _item.Value; }
                                else
                                //Si ya tiene un dato, entonces concatena el separador...
                                { _pkValues += "&" + _item.Key + "=" + _item.Value; }
                            }
                        }
                        BuildNavigateParamsFromSelectedValue(_pkValues);

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                        //Navigate(_urlProperties, EntityNameGrid + " " + e.Item.Text);
                        //NavigateEntity(_urlProperties, _actionTitleDecorator, NavigateMenuAction.Edit);
                        NavigateEntity(_urlProperties, _EntityName, GetPageTitleForViewer(), _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiLanguage":
                        String _entityName = _EntityName.Replace("_LG", String.Empty) + "_LG";
                        base.NavigatorAddTransferVar("EntityName", _entityName);
                        base.NavigatorAddTransferVar("EntityNameGrid", _EntityName.Replace("_LG", String.Empty) + "_LG");
                        base.NavigatorAddTransferVar("IsFilterHierarchy", false);

                        //Se concatenan las PK con el Key = Values, si hay mas, el separador es el "&"
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        //Navigate("~/Managers/ListManageAndView.aspx", _EntityName + " " + Resources.Common.Languages);
                        
                        NavigateEntity("~/Managers/ListManageAndView.aspx", _entityName, GetPageTitleForViewer(), _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
                        break;

                    default:
                        break;
                }
            }
            protected void rmnuSecuritySystem_ItemClick(object sender, RadMenuEventArgs e)
            {
                String _pkValues = String.Empty;
                //Igual para todos...
                if (NavigatorContainsPkTransferVar("PkCompost"))
                {
                    _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                    if (GetKeyValue(_pkValues, "ParentEntity") == null)
                    {
                        _pkValues += "&ParentEntity=" + _EntityName;
                        //base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    }
                }
                else
                {
                    _pkValues = "ParentEntity=" + _EntityName;
                    //base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                }
                //Si quedaron variables que no estaban en el PKCompost, las leo del NavigatorTransferenceColl..
                foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                {
                    if (_item.Key.Substring(0, 2) == "Id")
                    {
                        if (String.IsNullOrEmpty(_pkValues))
                        //Si esta vacio, solo pone el key...
                        { _pkValues += _item.Key + "=" + _item.Value; }
                        else
                        //Si ya tiene un dato, entonces concatena el separador...
                        { _pkValues += "&" + _item.Key + "=" + _item.Value; }
                    }
                }
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                switch (e.Item.Value)
                {
                    case "rmiSSJobTitles":
                        SecuritySystemJobTitlesNavigate();
                        break;

                    case "rmiSSPerson":
                        SecuritySystemPersonNavigate();
                        break;

                    default:
                        break;
                }//fin Switch
            }//fin evento
            private void SecuritySystemJobTitlesNavigate()
            {
                String _entityNameGrid = String.Empty;
                String _entityName = String.Empty;
                String _pageTitle = _PageTitleLocal + " - Right JobTitle";
                String _navigateEntity = String.Empty;
                String _actionTitleDecorator = _pageTitle;

                switch (_EntityName)
                {
                    case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassifications;
                        break;
                    case Common.ConstantsEntitiesName.DS.Organization:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizations;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleOrganization;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizations;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleProcesses;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleProcess;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleProcesses;
                        break;
                    case Common.ConstantsEntitiesName.PA.IndicatorClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PA.Indicator:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleIndicators;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleIndicator;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleIndicators;
                        break;
                    case Common.ConstantsEntitiesName.KC.ResourceClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassifications;
                        break;
                    case Common.ConstantsEntitiesName.KC.Resource:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleResources;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleResource;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleResources;
                        break;
                    case Common.ConstantsEntitiesName.IA.ProjectClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassifications;
                        break;
                    case Common.ConstantsEntitiesName.RM.RiskClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleRiskClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleRiskClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleRiskClassifications;
                        break;
                }

                base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
                base.NavigatorAddTransferVar("EntityName", _entityName);
                base.NavigatorAddTransferVar("PageTitle", _pageTitle);

                //base.Navigate("~/Managers/ListManageAndView.aspx", _navigateEntity);
                NavigateEntity("~/Managers/ListManageAndView.aspx",_entityName, _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
            }
            private void SecuritySystemPersonNavigate()
            {
                String _entityNameGrid = String.Empty;
                String _entityName = String.Empty;
                String _pageTitle = _PageTitleLocal + " - Right Post";
                String _navigateEntity = String.Empty;
                String _actionTitleDecorator = _pageTitle;

                switch (_EntityName)
                {
                    case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonOrganizationClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonOrganizationClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonOrganizationClassifications;
                        break;
                    case Common.ConstantsEntitiesName.DS.Organization:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonOrganizations;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonOrganization;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonOrganizations;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonProcessClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonProcessClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonProcessClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonProcesses;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonProcess;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonProcesses;
                        break;
                    case Common.ConstantsEntitiesName.PA.IndicatorClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonIndicatorClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonIndicatorClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonIndicatorClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PA.Indicator:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonIndicators;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonIndicator;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonIndicators;
                        break;
                    case Common.ConstantsEntitiesName.KC.ResourceClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonResourceClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonResourceClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonResourceClassifications;
                        break;
                    case Common.ConstantsEntitiesName.KC.Resource:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonResources;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonResource;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonResources;
                        break;
                    case Common.ConstantsEntitiesName.IA.ProjectClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonProjectClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonProjectClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonProjectClassifications;
                        break;
                    case Common.ConstantsEntitiesName.RM.RiskClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonRiskClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonRiskClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonRiskClassifications;
                        break;
                }

                base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
                base.NavigatorAddTransferVar("EntityName", _entityName);
                base.NavigatorAddTransferVar("PageTitle", _pageTitle);

                //base.Navigate("~/Managers/ListManageAndView.aspx", _navigateEntity);
                NavigateEntity("~/Managers/ListManageAndView.aspx", _entityName, _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
            }
            protected void btnOkDelete_Click(object sender, EventArgs e)
            {
                try
                {
                    //Se borra el elemento seleccionado.
                    ExecuteGenericMethodEntity(EntityNameToRemove, ManageEntityParams);

                    //Mostrar en el Status Bar
                    base.StatusBar.ShowMessage(Resources.Common.DeleteOK);

                    String _pkValues = String.Empty;
                    //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo.
                    if (NavigatorContainsPkTransferVar("PkCompost"))
                    {
                        _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost") + "&";
                    }
                    if (base.NavigatorContainsTransferVar("ParentEntity"))
                    {
                        _pkValues += "ParentEntity=" + NavigatorGetTransferVar<String>("ParentEntity");
                    }
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    //Como es un Delete, hay que limpiar el XML del tree global, si es una entidad mapa(se valida internamente)
                    ValidateClearXMLTreeViewGlobalMenu(_EntityName);
                    
                    //Navega al Manage.
                    //Ejecuta la Navegacion a la PAgina del MAnage que le corresponde segun la Entidad indicada, 
                    //TAmbien arma todos los PArametros necesarios para pasarle al Manage.
                    Navigate(GetParameterToManager(_EntityName, new Dictionary<String, Object>()), _EntityName, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                }
                catch (Exception ex)
                {
                    //Mostrar en el Status Bar....
                    base.StatusBar.ShowMessage(ex);
                }
                //oculta el popup.
                this.mpelbDelete.Hide();
            }
        #endregion

        #region Private Method
            private void InitializeHandlers()
            {
                btnOkDelete.Click += new EventHandler(btnOkDelete_Click);
                _rgdMasterGridListViewer.ItemDataBound += new GridItemEventHandler(rgdMasterGridListViewer_ItemDataBound);
                
                GridLinkButtonClick = new EventHandler(GridLinkButtonClick_Click);
                //_rgdMasterGridListViewer.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);
                base.InjectClientSelectRow(_rgdMasterGridListViewer.ClientID);
            }
            private void LoadListViewer()
            {
                _rgdMasterGridListViewer = base.BuildListViewerContent(_EntityName);
                pnlListViewer.Controls.Add(_rgdMasterGridListViewer);

                //pnlListViewer.Controls.Add(base.BuildListViewerContent(_EntityName));
                ////Busca el control grid y lo mantiene en esta pagina
                //_rgdMasterGridListViewer = (RadGrid)pnlListViewer.FindControl("rgdMasterGridListViewer");
            }
            private void LoadGeneralOptionMenu()
            {
                BuildPropertyGeneralOptionsMenu(_EntityName, new RadMenuEventHandler(rmnuGeneralOption_ItemClick), false);
            }
            private void LoadGRCByEntity()
            {
                //TODO: Por ahora lo preguntamos aca...pero despues lo deberiamos hacer en otro lado, me parece!!
                //Cuando es un Add, no debe cargar el GRC!!!
                if (!String.IsNullOrEmpty(_EntityNameGRC))
                {
                    //Dictionary<String, Object> _param = new Dictionary<String, Object>();
                    //_param.Add("IdOrganization", _IdOrganization);
                    //_param.Add("PageTitle", txtCorporateName.Text);
                    ManageEntityParams.Concat(GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost")));
                    if (BuildContextInfoModuleMenu(_EntityNameGRC, ManageEntityParams))
                    {
                        base.BuildContextInfoShowMenuButton();
                    }
                }
            }
            private void LoadContextElementMapsByEntity()
            {
                //Si tiene una entidad, entonces contruye el Context element Maps de la derecha
                if (!String.IsNullOrEmpty(_EntityNameContextElement))
                {
                    ManageEntityParams.Concat(GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost")));
                    if (BuildContextElementMapsModuleMenu(_EntityNameContextElement, ManageEntityParams))
                    {
                        base.BuildContextElementMapsShowMenuButton();
                    }
                }
            }
            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
            private void LoadSecurityOptionMenu()
            {
                //Menu de Seguridad
                //Si llega a ser un viewer de Ejecuciones, no debe tener menu de opciones generales.
                if (!_EntityName.Contains("ProcessTaskExecution"))
                {
                    base.BuildPropertySecuritySystemMenu(_EntityName, new RadMenuEventHandler(rmnuSecuritySystem_ItemClick));
                }
            }
        #endregion
    }
}
