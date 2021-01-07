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
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.Managers
{
    public partial class ListManage : BasePage
    {
        #region Internal Properties
            private RadGrid _RgdMasterGridListManage;
            private RadTreeView _rtvComboHierarchicalFilter;
            private RadComboBox _rdcComboFilter;
            private String _EntityName = String.Empty;
            private String _EntityNameGRC = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            private String _EntityNameMapClassification = String.Empty;
            private String _EntityNameMapClassificationChildren = String.Empty;
            private String _EntityNameMapElement = String.Empty;
            private String _EntityNameMapElementChildren = String.Empty;
            private Dictionary<Int64, CatalogDoc> _CatalogDoc
            {
                get 
                {
                    return GetPicturesByObject(_EntityName + "Pictures", ManageEntityParams);
                }
            }
        #endregion

        #region PageLoad & Init
            private void LoadParameters()
            {
                ManageEntityParams = new Dictionary<String, Object>();
                //Debe recorrer las PK para saber si es un Manage de Lenguajes.
                String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                ManageEntityParams = GetKeyValues(_pkValues);

                //Setea el nombre de la entidad que se va a mostrar 
                //y el resto de los parametros recibidos
                EntityNameGrid = base.NavigatorGetTransferVar<String>("EntityNameGrid");
                _EntityName = base.NavigatorGetTransferVar<String>("EntityName");
                _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
                if (base.NavigatorContainsTransferVar("EntityNameContextElement"))
                {
                    _EntityNameContextElement = base.NavigatorGetTransferVar<String>("EntityNameContextElement");
                }
                //EntityNameToRemove = base.NavigatorGetTransferVar<String>("EntityNameToRemove");
                //Para el caso del Remove, debe ser el nombre de la entidad mas la palabra Remove!!!
                EntityNameToRemove = _EntityName + "Remove";

                IsFilterHierarchy = base.NavigatorGetTransferVar<Object>("IsFilterHierarchy") != null ? base.NavigatorGetTransferVar<Boolean>("IsFilterHierarchy") : false;
                EntityNameComboFilter = base.NavigatorGetTransferVar<String>("EntityNameComboFilter");
                EntityNameChildrenComboFilter = base.NavigatorGetTransferVar<String>("EntityNameChildrenComboFilter");
                _EntityNameMapClassification = base.NavigatorGetTransferVar<String>("EntityNameMapClassification");
                _EntityNameMapClassificationChildren = base.NavigatorGetTransferVar<String>("EntityNameMapClassificationChildren");
                _EntityNameMapElement = base.NavigatorGetTransferVar<String>("EntityNameMapElement");
                _EntityNameMapElementChildren = base.NavigatorGetTransferVar<String>("EntityNameMapElementChildren");
            }
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                //Carga todos los paramtros que recibe la pagina...
                LoadParameters();

                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                //Arma la grilla completa
                LoadListManage();
                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
                //Carga el menu de opciones generales.
                LoadGeneralOptionMenu();
                //Inyecta los javascript que necesita la pagina
                InyectJavaScript();
                //Inserta todos los manejadores de eventos que necesita la apgina
                InitializeHandlers();
                //Arma el Menu de Seleccion.
                LoadMenuSelection();

                LoadMenuSearch();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);


                //Esto es para limpiar la variable de session para las properties
                Session["IdOrganization_local"] = null;

                //Si no hay entidad para el filtro, no lo inyecta.

                if (!String.IsNullOrEmpty(EntityNameComboFilter) || !String.IsNullOrEmpty(_EntityNameMapClassification))
                {
                    LoadFilter();
                }
                else
                {
                    if (_EntityName == Common.ConstantsEntitiesName.DB.PlannedTasks)
                    {
                        RadDatePicker _rdp = new RadDatePicker();
                        _rdp.AutoPostBack = true;
                        _rdp.SelectedDateChanged += new Telerik.Web.UI.Calendar.SelectedDateChangedEventHandler(_rdp_SelectedDateChanged);

                        pnlFilter.Controls.Add(BuildFilterTable(_rdp));

                        PersistControlState(_rdp);
                    }
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                try
                {
                    //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                    String _pageTitle = base.NavigatorGetTransferVar<String>("PageTitle");
                    if (String.IsNullOrEmpty(_pageTitle))
                    {
                        base.PageTitle = GetGlobalResourceObject("CommonListManage", _EntityName).ToString();
                    }
                    else
                    {
                        base.PageTitle = _pageTitle;
                    }
                }
                catch { base.PageTitle = String.Empty; }
            }
            //Setea el Sub Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPageTileSubTitle()
            {
                String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                if (String.IsNullOrEmpty(_pageSubTitle))
                {
                    base.PageTitleSubTitle = Resources.Common.PageSubtitleList;
                }
                else
                {
                    base.PageTitleSubTitle = _pageSubTitle;
                }
            }
        #endregion

        #region Page Event
            protected void GridLinkButtonClick_Click(object sender, EventArgs e)
            {
                //Setea los parametros en el Navigate.
                BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);

                //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                    + "&" + BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                IButtonControl _lnkButton = (IButtonControl)sender;
                String _paramsToNavigate = _lnkButton.CommandArgument;
                String _entityName = Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"));
                NavigatorAddTransferVar("EntityName", _entityName);
                NavigatorAddTransferVar("EntityNameGrid", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameGrid")));
                NavigatorAddTransferVar("EntityNameContextInfo", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextInfo")));
                NavigatorAddTransferVar("EntityNameContextElement", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextElement")));

                //Navigate(GetPageViewerByEntity(Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"))), Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));
                //String _url = GetPageViewerByEntity(Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));
                //NavigateEntity(_url, NavigateMenuAction.View);

                String _url = GetPageViewerByEntity(_entityName);
                String _entityPropertyName = _lnkButton.Text;
                NavigateEntity(_url, _entityName, _entityPropertyName, NavigateMenuType.ListManagerMenu);

            }  
            protected void rmnSelection_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
            {
                String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                String _entityPropertyName = GetContextInfoCaption(EntityNameGrid, _RgdMasterGridListManage);
                String _urlProperties = String.Empty;

                switch (e.Item.Value)
                {
                    case "rmiView":  //VIEW
                        FilterExpressionGrid = String.Empty;
                        //String _params = BuildQueryStringParamsFromListManageSelected(_RgdMasterGridListManage);
                        //Setea los parametros en el Navigate.
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        
                        //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                        //NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        //NavigatorAddPkEntityIdTransferVar("PkCompost", BuildParamsFromListManageSelected(_RgdMasterGridListManage));
                        String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                            + "&" + BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                        NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                        String _entityName = _EntityName.Replace("_LG", String.Empty);
                        base.NavigatorAddTransferVar("EntityName", _entityName);
                        base.NavigatorAddTransferVar("EntityNameGrid", EntityNameGrid.Replace("_LG", String.Empty));
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);

                        //Navigate(GetPageViewerByEntity(_EntityName.Replace("_LG", String.Empty)), EntityNameGrid + " " + e.Item.Text);

                        //NavigateEntity(GetPageViewerByEntity(_entityName), , NavigateMenuAction.View);
                        NavigateEntity(GetPageViewerByEntity(_entityName), _entityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.View);

                    //Navigate("~/MainInfo/ListViewer.aspx", EntityNameGrid + " " + e.Item.Text);
                        break;

                    case "rmiEdit":  //EDIT
                        FilterExpressionGrid = String.Empty;
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        //Context.Items.Add("IdValue", _RgdMasterGridListManage.SelectedValue);
                        //Server.Transfer("~/TestBackEnd/CountriesProperties.aspx");
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }
                        
                        switch (_EntityName)
                        {
                            case Common.ConstantsEntitiesName.DB.PlannedTasks:
                            case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                            case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                            case Common.ConstantsEntitiesName.DB.ActiveTasks:
                            case Common.ConstantsEntitiesName.DB.OverDueTasks:
                            case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                            case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                                String _type = _RgdMasterGridListManage.MasterTableView.DataKeyValues[_RgdMasterGridListManage.SelectedItems[0].ItemIndex]["TypeExecution"].ToString();
                                //Si ya contiene la palabra execution...lo dejo, sino reemplazo el processtask por processtaskexecution...
                                if (_type.Contains("Execution"))
                                {                                
                                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _type).ToString();
                                }
                                else
                                {
                                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _type.Replace("ProcessTask", "ProcessTaskExecution")).ToString();
                                }
                                break;
                            default:
                                _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                                break;
                        }

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);

                        //Navigate(_urlProperties, EntityNameGrid + " " + e.Item.Text);
                        break;

                        //Para el caso de estar viendo Calculos, se puede ejecutar!!!
                    case "rmiCompute":
                        try
                        {
                            Int64 _idCalculation = Convert.ToInt64(GetKeyValue(BuildParamsFromListManageSelected(_RgdMasterGridListManage), "IdCalculation"));
                            Condesus.EMS.Business.PA.Entities.Calculation _calculation = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation);
                            Decimal _yValueCalculated = _calculation.Calculate();

                            FilterExpressionGrid = String.Empty;
                            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                            BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                            //Recarga la grilla
                            _RgdMasterGridListManage.Rebind();
                        }
                        catch(Exception ex)
                        {
                            //Mostrar en el Status Bar....
                            base.StatusBar.ShowMessage(ex);
                        }
                        break;

                    case "rmiCreateException":
                        FilterExpressionGrid = String.Empty;
                        
                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateCreateName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiCloseException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateCloseName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiTreatException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateTreatName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    default:  //DELETE se ejecuta en el btnOkDelete_Click()
                        break;
                }
            }
            protected void rmnuGeneralOption_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
            {
                switch (e.Item.Value)
                {
                    case "rmiAdd": //ADD
                        FilterExpressionGrid = String.Empty;
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo.
                        base.NavigatorClearTransferVars();
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }
                        base.NavigatorAddTransferVar("EntityName", _EntityName);
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);
                        
                        String _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                        //Navigate(_urlProperties, _EntityName + " " + e.Item.Text);

                        String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                        NavigateEntity(_urlProperties, _EntityName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        //NavigateEntity(_urlProperties, _actionTitleDecorator, NavigateMenuAction.Add);
                        break;

                    default:
                        break;
                }
            }
            protected void _rdcComboFilter_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
                String _selectedValue = ((RadComboBox)sender).SelectedValue;
                //Obtiene las Key y vuelve a armar el DataTable.
                ManageEntityParams = GetKeyValues(_selectedValue);
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                //Rebind de la grilla.
                _RgdMasterGridListManage.Rebind();
            }
            protected void btnOkDelete_Click(object sender, EventArgs e)
            {
                String _radMenuClickedId = Convert.ToString(Request.Form["radMenuClickedId"]);

                try
                {
                    Dictionary<String, Object> _paramForDelete = new Dictionary<String, Object>();
                    String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                    //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                    _paramForDelete = GetKeyValues(_pkValues);
                    //Se guarda todos los parametros que recibe... si es que no vienen por PK
                    foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                    {
                        if (!_paramForDelete.ContainsKey(_item.Key))
                        {
                            _paramForDelete.Add(_item.Key, _item.Value);
                        }
                        else
                        {
                            _paramForDelete.Remove(_item.Key);
                            _paramForDelete.Add(_item.Key, _item.Value);
                        }
                    }

                    #region Massive Delete
                        if (_radMenuClickedId == "Option")
                        {
                            //Cuando se ejecuta desde option, recorre toda la grilla quien esta chequeado
                            //De esta forma recorremos todas las filas de la grilla ( de la pagina en la que estamos )
                            foreach (Telerik.Web.UI.GridDataItem _row in _RgdMasterGridListManage.Items)
                            {
                                //Buscamos el Checkbox para ver el valor que tiene y saber si lo eliminamos o no
                                CheckBox _chkSelect = (CheckBox)(_row.FindControl("chkSelectItem"));
                                //Si esta clickeado entramos a la rutina para borrar la fila.
                                if (_chkSelect.Checked)
                                {
                                    //Con esto Arma el string con los parametros en base a los DataKeyName de la grilla.
                                    String _params = BuildParamsFromListManageChecked(_RgdMasterGridListManage, _row);
                                    foreach (KeyValuePair<String, Object> _item in GetKeyValues(_params))
                                    {
                                        if (!_paramForDelete.ContainsKey(_item.Key))
                                        {
                                            _paramForDelete.Add(_item.Key, _item.Value);
                                        }
                                        else
                                        {
                                            _paramForDelete.Remove(_item.Key);
                                            _paramForDelete.Add(_item.Key, _item.Value);
                                        }
                                    }
                                    //Cuando se ejecuta el menu Opciones generales, se borran sobre todos los que estan chequeados.
                                    ExecuteGenericMethodEntity(EntityNameToRemove, _paramForDelete);
                                }
                            }
                        }
                    #endregion
                    #region Simple Delete
                        else
                        {
                            //Cuando se ejecuta el menu Selection, solo borra un registro.(el seleccionado)
                            String _params = BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                            foreach (KeyValuePair<String, Object> _item in GetKeyValues(_params))
                            {
                                //si ya existe el dato, lo borra y lo vuelve a meter(actualiza, por las dudas)
                                if (_paramForDelete.ContainsKey(_item.Key))
                                {
                                    _paramForDelete.Remove(_item.Key);
                                    _paramForDelete.Add(_item.Key, _item.Value);
                                }
                                else
                                {
                                    //Si no existe, solamanete lo inserta
                                    _paramForDelete.Add(_item.Key, _item.Value);
                                }
                            }
                            //Se borra el elemento seleccionado.
                            ExecuteGenericMethodEntity(EntityNameToRemove, _paramForDelete);
                        }
                    #endregion

                    //Mostrar en el Status Bar
                    base.StatusBar.ShowMessage(Resources.Common.DeleteOK);

                    //Revisa si hay algo en el filtro jeraquico y lo agrega a los parametros para volver a cargar la grilla
                    if (_rtvComboHierarchicalFilter != null)
                    {
                        if (_rtvComboHierarchicalFilter.SelectedNode != null)
                        {
                            foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rtvComboHierarchicalFilter.SelectedNode.Value))
                            {
                                if (!ManageEntityParams.ContainsKey(_item.Key))
                                {
                                    ManageEntityParams.Add(_item.Key, _item.Value);
                                }
                            }
                        }
                    }
                    else
                    {
                        //si no esta en el combotree, revisa el combo simple.
                        if (_rdcComboFilter != null)
                        {
                            foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rdcComboFilter.SelectedValue))
                            {
                                if (!ManageEntityParams.ContainsKey(_item.Key))
                                {
                                    ManageEntityParams.Add(_item.Key, _item.Value);
                                }
                            }
                        }
                    }

                    //Pasa las variables otra vez...para poder navegar a si misma
                    base.NavigatorAddTransferVar("EntityName", _EntityName);
                    base.NavigatorAddTransferVar("EntityNameGrid", EntityNameGrid);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                    base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);
                    base.NavigatorAddTransferVar("IsFilterHierarchy", IsFilterHierarchy);
                    base.NavigatorAddTransferVar("EntityNameComboFilter", EntityNameComboFilter);
                    base.NavigatorAddTransferVar("EntityNameChildrenComboFilter", EntityNameChildrenComboFilter);
                    base.NavigatorAddTransferVar("EntityNameMapClassification", _EntityNameMapClassification);
                    base.NavigatorAddTransferVar("EntityNameMapClassificationChildren", _EntityNameMapClassificationChildren);
                    base.NavigatorAddTransferVar("EntityNameMapElement", _EntityNameMapElement);
                    base.NavigatorAddTransferVar("EntityNameMapElementChildren", _EntityNameMapElementChildren);

                    NavigateEntity(HttpContext.Current.Request.Url.AbsolutePath, EntityNameGrid, Condesus.WebUI.Navigation.NavigateMenuAction.Delete);

                    //FilterExpressionGrid = String.Empty;
                    ////Setea los datos en el DataTable de base, para que luego se carge la grilla.
                    //BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                    ////Recarga la grilla sin el/los registros borrados
                    //_RgdMasterGridListManage.Rebind();
                }
                catch (Exception ex)
                {
                    //Mostrar en el Status Bar....
                    base.StatusBar.ShowMessage(ex);
                }
                //oculta el popup.
                this.mpelbDelete.Hide();
            }
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(EntityNameChildrenComboFilter, _params);
                foreach (DataRow _drRecord in DataTableListManage[EntityNameChildrenComboFilter].Rows)
                {
                    RadTreeNode _node = SetGenericNodeTreeView(_drRecord, EntityNameChildrenComboFilter, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, false, false);
                    e.Node.Nodes.Add(_node);
                    SetExpandMode(_node, EntityNameChildrenComboFilter, false, false);
                }
            }
            //Evento para el Expand del Combo con Tree pero ElementMaps
            protected void rtvElementMaps_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(_EntityNameMapClassificationChildren, _params);
                if (DataTableListManage.ContainsKey(_EntityNameMapClassificationChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[_EntityNameMapClassificationChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, _EntityNameMapClassificationChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(_EntityNameMapElementChildren, _params);
                if (DataTableListManage.ContainsKey(_EntityNameMapElementChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[_EntityNameMapElementChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapElementChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Los elementos no tienen hijos
                        //SetExpandMode(_node, _EntityNameMapElementChildren);
                    }
                }
            }
            protected void rtvElementMaps_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                _rdcComboFilter.Text = e.Node.Text;
                _rdcComboFilter.SelectedValue = e.Node.Value;

                //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
                String _selectedValue = ((RadTreeView)sender).SelectedValue;
                //Obtiene las Key y vuelve a armar el DataTable.
                ManageEntityParams = GetKeyValues(_selectedValue);
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                //Rebind de la grilla.
                _RgdMasterGridListManage.Rebind();
            }
            protected void rtvHierarchicalTreeViewInCombo_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                _rdcComboFilter.Text = e.Node.Text;
                _rdcComboFilter.SelectedValue = e.Node.Value;

                //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
                String _selectedValue = ((RadTreeView)sender).SelectedValue;
                //Obtiene las Key y vuelve a armar el DataTable.
                ManageEntityParams = GetKeyValues(_selectedValue);
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                //Rebind de la grilla.
                _RgdMasterGridListManage.Rebind();
            }
            protected void _rdp_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
            {
                Dictionary<String, Object> _param=new Dictionary<String,Object>();
                _param.Add("StartDate", e.NewDate);

                BuildGenericDataTable(EntityNameGrid, _param);

                _RgdMasterGridListManage.Rebind();
            }
            protected void RgdMasterGridListManage_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    #region rptButton
                        HtmlImage oimg2 = (HtmlImage)e.Item.FindControl("rptButton");
                        if (!(oimg2 == null))
                        {
                            String _resourceType = ((DataRowView)e.Item.DataItem).Row["ResourceType"].ToString();
                            String _urlName = ((DataRowView)e.Item.DataItem).Row["Name"].ToString();
                            Int64 _idResource = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResource"]);
                            Int64 _idResourceFile = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResourceFile"]);

                            if (_idResourceFile > 0)
                            {
                                oimg2.Attributes["onclick"] = string.Format("return ShowFile(event, '" + _resourceType + "','" + _urlName + "'," + _idResource + ", " + _idResourceFile + ");");
                                oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                            }
                            else
                            {
                                //No quiero que salga el icono, pero que no pierda el estilo la celda.
                                oimg2.Attributes["class"] = String.Empty;
                                //oimg2.Visible = false;
                            }
                        }
                    #endregion

                    #region Image
                        Byte[] _fileStream = null;
                        RadBinaryImage _rbiImage = (RadBinaryImage)e.Item.FindControl("rbiImage");
                        if (_rbiImage != null)
                        {
                            String _pkValuesForRow = GetPKCompostFromItem((RadGrid)sender, e.Item.ItemIndex);
                            //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                            ManageEntityParams = GetKeyValues(_pkValuesForRow);

                            if ((_CatalogDoc != null) && (_CatalogDoc.Count > 0))
                            {
                                _fileStream = _CatalogDoc.First().Value.FileAttach.FileStream;
                                _rbiImage.DataValue = _fileStream;
                            }
                            else
                            {
                                //_rbiImage.Visible = false;
                                _rbiImage.ImageUrl = "~/Skins/Images/NoImagesAvailable.gif";
                            }
                        }
                    #endregion

                    #region Exception Measurement Out Of Range (Color Row)
                        try
                        {
                            String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                            if (_measurementStatus.ToLower() == "true")
                            {
                                e.Item.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch { }
                    #endregion

                }
            }
            protected void RgdMasterGridListManageKeyIndicator_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    String _idMeasurement = ((DataRowView)e.Item.DataItem).Row["IdMeasurement"].ToString();

                    ImageButton oimg = (ImageButton)e.Item.FindControl("btnChartLink");
                    if (!(oimg == null))
                    {
                        oimg.Attributes["onclick"] = string.Format("return ShowChart(event, " + _idMeasurement + ");");
                        oimg.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }

                    ImageButton oimg2 = (ImageButton)e.Item.FindControl("btnSeriesLink");
                    if (!(oimg2 == null))
                    {
                        oimg2.Attributes["onclick"] = string.Format("return ShowSeries(event,  " + _idMeasurement + ");");
                        oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }

                    #region Exception Measurement Out Of Range (Color Row)
                    try
                    {
                        String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                        if (_measurementStatus.ToLower() == "true")
                        {
                            e.Item.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch { }
                    #endregion

                }
            }
            //protected void RgdMasterGridListManageMeasurementDevice_ItemDataBound(object sender, GridItemEventArgs e)
            //{
            //    if (e.Item is GridDataItem)
            //    {
            //        Byte[] _fileStream = null;
            //        RadBinaryImage _rbiImage = (RadBinaryImage)e.Item.FindControl("rbiImage");
            //        if (_rbiImage != null)
            //        {
            //            String _pkValuesForRow = GetPKCompostFromItem((RadGrid)sender, e.Item.ItemIndex);
            //            //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
            //            ManageEntityParams = GetKeyValues(_pkValuesForRow);

            //            if ((_CatalogDoc != null) && (_CatalogDoc.Count > 0))
            //            {
            //                _fileStream = _CatalogDoc.First().Value.FileAttach.FileStream;
            //                _rbiImage.DataValue = _fileStream;
            //            }
            //            else
            //            {
            //                _rbiImage.ImageUrl = "~/Skins/Images/NoImagesAvailable.gif";
            //            }
            //        }
            //    }
            //}
        #endregion

        #region Private Method
            private void InyectJavaScript()
            {
                base.InjectRowContextMenu(rmnSelection.ClientID, String.Empty);
                base.InjectShowMenu(rmnSelection.ClientID, _RgdMasterGridListManage.ClientID);
                base.InjectClientSelectRow(_RgdMasterGridListManage.ClientID);
                base.InjectRmnSelectionItemClickHandler(String.Empty, String.Empty, false);
                base.InjectValidateItemChecked(_RgdMasterGridListManage.ClientID);
            }
            private void InitializeHandlers()
            {
                btnOkDelete.Click += new EventHandler(btnOkDelete_Click);
                GridLinkButtonClick = new EventHandler(GridLinkButtonClick_Click);
                _RgdMasterGridListManage.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);
                //_RgdMasterGridListManage.SortCommand += new GridSortCommandEventHandler(rgdMasterGrid_SortCommand);
                //_RgdMasterGridListManage.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
                _RgdMasterGridListManage.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu";                
            }
            private void LoadListManage()
            {
                Boolean _showImgSelect = true;
                Boolean _showCheck = true;
                Boolean _showOpenFile = false;
                Boolean _showOpenChart = false;
                Boolean _showOpenSeries = false;
                Boolean _allowSearchableGrid = true; //En el manage si se muestra el search.
                Boolean _showBinaryImage = false;

                //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                _showCheck = ShowCheck(_EntityName);
                //Si se tiene que mostrar un file, entonces agrega la columna de openfile.
                if ((_EntityName.Contains(Common.ConstantsEntitiesName.KC.ResourceVersion)) || (_EntityName.Contains(Common.ConstantsEntitiesName.PF.ProcessResource)) || (_EntityName.Contains(Common.ConstantsEntitiesName.PA.Formula)))
                    { _showOpenFile = true; }
                //Si se esta visualizando un key indicator, entonces agrega las columnas showchart y showseries
                if (_EntityName.Contains(Common.ConstantsEntitiesName.PA.KeyIndicator))
                { 
                    _showOpenChart = true;
                    _showOpenSeries = true;
                }
                if (_EntityName.Contains(Common.ConstantsEntitiesName.PA.MeasurementDevice))
                {
                    _showBinaryImage = true;
                }

                _RgdMasterGridListManage = base.BuildListManageContent(EntityNameGrid, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
                if (_showBinaryImage)
                {
                    _RgdMasterGridListManage.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManage_ItemDataBound);
                }
                //Si tiene el open file, agrego el evento e inyecto el JS.
                if (_showOpenFile)
                {
                    _RgdMasterGridListManage.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManage_ItemDataBound);
                    InjectShowFile(_RgdMasterGridListManage.ClientID);
                }
                //Si tiene el ShowChart o ShowSeries, agrego el evento e inyecto el JS.
                if ((_showOpenChart) || (_showOpenSeries))
                {
                    _RgdMasterGridListManage.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManageKeyIndicator_ItemDataBound);
                    InjectShowChart();
                    InjectShowSeries();
                }
                base.PersistControlState(_RgdMasterGridListManage);
                pnlListManage.Controls.Add(_RgdMasterGridListManage);
            }

            private Boolean ShowCheck(String entity)
            {
                switch (entity)
                {
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecution:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement:
                    case Common.ConstantsEntitiesName.DB.ActiveTasks:
                    case Common.ConstantsEntitiesName.DB.PlannedTasks:
                    case Common.ConstantsEntitiesName.DB.FinishedTasks:
                    case Common.ConstantsEntitiesName.DB.OverDueTasks:
                        return false;

                    default: return true;
                }
            }
            private void LoadMenuSearch()
            {
                pnlSearch.Controls.Add(base.BuildSearchContent(pnlSearch));

                 //Busca el control Button del Search y si lo encuentra, entonces lo regristra como async
                Button _btnSearch = (Button)pnlSearch.FindControl("btnSearch");
                if (_btnSearch != null)
                {
                    //Registra como Async
                    FwMasterPage.RegisterContentAsyncPostBackTrigger(_btnSearch, "Click");
                }
            }
            private void LoadFilter()
            {
                PlaceHolder _phComboWithTreeView = new PlaceHolder();
                _phComboWithTreeView.ID = "phComboWithTreeView" + _EntityNameMapClassification;
                //Arma el texto para mostrar en combo por defecto.
                String _selectedValue = String.Empty;

                //Si el filtro es jerarquico, entonces verifica si es de tipo Mapa o Tree Simple.
                if (IsFilterHierarchy)
                {
                    //Verifica si el filtro a cargar es uno de Mapa o un combo Simple.
                    if (_EntityNameMapClassification != String.Empty)
                    {
                        #region Armar un Combo con Tree de ElementMaps

                        
                            //Como se carga un tree con los ElementMaps, se setea este parametro
                            if (!ManageEntityParams.ContainsKey("IsLoadElementMap"))
                                { ManageEntityParams.Add("IsLoadElementMap", true); }
                            //Esto es para hacer combo con tree de ElementMap....
                            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                            BuildGenericDataTable(_EntityNameMapClassification, ManageEntityParams);
                            BuildGenericDataTable(_EntityNameMapElement, ManageEntityParams);
                            //Construye el TreeView
                            _rtvComboHierarchicalFilter = BuildElementMapsContent(_EntityNameMapClassification);
                            //Asocia el Handler del Expand y click
                            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(rtvElementMaps_NodeExpand);
                            _rtvComboHierarchicalFilter.NodeClick += new RadTreeViewEventHandler(rtvElementMaps_NodeClick); //+= new RadTreeViewEventHandler(_rtvComboHierarchicalFilter_NodeClick);
                            //Carga los registros en el Tree
                            base.LoadGenericTreeViewElementMap(ref _rtvComboHierarchicalFilter, _EntityNameMapClassification, _EntityNameMapElement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
                            _selectedValue = GetInitialTextComboWithTreeView(_rdcComboFilter, _EntityNameMapClassification);
                            //Contruye el Combo de Filtro.
                            _rdcComboFilter = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, _EntityNameMapClassification, false, true, false, _selectedValue, _phComboWithTreeView, false);

                            //Agrego el combo al panel con su tabla
                            pnlFilter.Controls.Add(BuildFilterTable(_rdcComboFilter));

                            PersistControlState(_rtvComboHierarchicalFilter);
                            PersistControlState(_rdcComboFilter);
                        //Ojo
                            //Evento Cliente para mostrar el texto en el combo
                            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;
                            //Inyecta el JavaScript del evento cliente para mostrar el texto en el combo
                            base.InjectOnClientNodeClicking(_rdcComboFilter.ClientID, _rtvComboHierarchicalFilter.ClientID, _EntityNameMapClassification, _rtvComboHierarchicalFilter.ClientID);
                        #endregion
                    }
                    else
                    {
                        #region Armar un Combo con Tree (Generico)
                            //Aca cargo un combo con TreeView extra en la pagina(combotree generico)
                            _rtvComboHierarchicalFilter = BuildHierarchicalInComboContent(EntityNameComboFilter);

                            //#region esto para las pruebas....
                            //if (!ManageEntityParams.ContainsKey("IdOrganization"))
                            //{ ManageEntityParams.Add("IdOrganization", 2); }
                            //#endregion

                            //Arma el DataTable con la entidad indicada
                            BuildGenericDataTable(EntityNameComboFilter, ManageEntityParams);
                            //Asocia los Handlers del Tree.
                            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand);
                            _rtvComboHierarchicalFilter.NodeClick += new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeClick);
                            //Carga los datos en el Tree.
                            base.LoadGenericTreeView(ref _rtvComboHierarchicalFilter, EntityNameComboFilter, EntityNameComboFilter, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty, false, false);
                            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
                            _selectedValue = GetInitialTextComboWithTreeView(_rdcComboFilter, EntityNameComboFilter);
                            //Contruye el combo con el treeView adentro
                            _rdcComboFilter = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, EntityNameComboFilter, false, true, false, _selectedValue, _phComboWithTreeView, false);
                    
                            //Agrego el combo al panel con su tabla
                            pnlFilter.Controls.Add(BuildFilterTable(_rdcComboFilter));

                            PersistControlState(_rtvComboHierarchicalFilter);
                            PersistControlState(_rdcComboFilter);
                        //Ojo
                            //Asocia el Evento de cliente, para mostrar el texto en el combo.
                            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;
                            //Inyecta el javascript para mostrar el texto seleccionado en el combo.
                            base.InjectOnClientNodeClicking(_rdcComboFilter.ClientID, _rtvComboHierarchicalFilter.ClientID, EntityNameComboFilter, _rtvComboHierarchicalFilter.ClientID);
                        #endregion
                    }
                }
                else
                {
                    //Carga el DataTable del combo
                    BuildGenericDataTable(EntityNameComboFilter, ManageEntityParams);
                    _rdcComboFilter = BuildComboBox(EntityNameComboFilter, false, true, false, SelectedValueDefaultComboBox, true, false);
                    _rdcComboFilter.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_rdcComboFilter_SelectedIndexChanged);
                    //pnlFilter.Controls.Add(base.BuildFilterContent(pnlFilter, _rdcComboFilter));
                    
                    //Agrego el combo al panel con su tabla
                    pnlFilter.Controls.Add(BuildFilterTable(_rdcComboFilter));
                    
                    PersistControlState(_rdcComboFilter);

                    //Registra como Async
                    FwMasterPage.RegisterContentAsyncPostBackTrigger(_rdcComboFilter, "SelectedIndexChanged");
                }
            }
            private Table BuildFilterTable(Control filter)
            {

                //************************ Armo la tabla del filtro ********************

                String _imagePath = "~/App_Themes/" + this.Page.Theme + "/Images/";

                Table _tblContentForm = new Table();
                _tblContentForm.CssClass = "ContentFilter";
                _tblContentForm.CellSpacing = 0;
                _tblContentForm.CellPadding = 0;

                TableRow _currentRow = new TableRow();
                TableCell _tblCell = new TableCell();
                _tblCell.CssClass = "Header";
                //Arreglar problema al hacer click sobre el Search (linkbutton).
                //_tblCell.Attributes.Add("onclick", "javascript:HideShowSearchContent(this);");
                Label _lblCaption = new Label();
                _lblCaption.ID = "lblFilterTitle";
                _lblCaption.Text = Resources.Common.lblFilter;
                _tblCell.Controls.Add(_lblCaption);
                _currentRow.Cells.Add(_tblCell);

                //_tblCell = new TableCell();
                //ImageButton _imgControlFilter = new ImageButton();
                //_imgControlFilter.ID = "imgFilterControl";
                //_imgControlFilter.ImageUrl = _imagePath + "tras.gif";
                //_imgControlFilter.CssClass = "ImagesControlFilter";
                //_imgControlFilter.OnClientClick = "HideShowFilterContent(this)";

                //_tblCell.Controls.Add(_imgControlFilter);
                _currentRow.Cells.Add(_tblCell);

                _tblContentForm.Controls.Add(_currentRow);

                TableRow _FilterContent = new TableRow();
                _FilterContent.ID = "filterContent";
                _FilterContent.Style.Add("display", "block");


                _currentRow = new TableRow();
                _tblContentForm.Rows.Add(_currentRow);

                _tblCell = new TableCell();

                Panel _pnlContentFormFilter = new Panel();
                _pnlContentFormFilter.CssClass = "Form";

                //Agrego el combo a la celda
                _pnlContentFormFilter.Controls.Add(filter);
                _tblCell.Controls.Add(_pnlContentFormFilter);
                _currentRow.Cells.Add(_tblCell);

                return _tblContentForm;

            }
            private void LoadGeneralOptionMenu()
            {
                //Para las ejecuciones, no es necesario tener meno de general options....
                //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                if (!_EntityName.Contains(Common.ConstantsEntitiesName.PF.ProcessTaskExecution))
                {
                    //RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity(_EntityName + "_MenuOption", ManageEntityParams, false));
                    RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity(_EntityName + "_MenuOption", ManageEntityParams, false));
                    _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(rmnuGeneralOption_ItemClick);
                }
                else
                {
                    //Si es una ejecucion, debo mirar si es de tipo espontanea, para meter el menu.
                    if (CheckTaskTypeExecutionSpontaneous())
                    {
                        //RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity("ProcessTaskExecution_MenuOption", ManageEntityParams, false));
                        RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity("ProcessTaskExecution_MenuOption", ManageEntityParams, false));
                        _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(rmnuGeneralOption_ItemClick);
                    }
                }
            }
            private Boolean CheckTaskTypeExecutionSpontaneous()
            {
                String _typeExecution = String.Empty;
                Int64 _idprocess=Convert.ToInt64(ManageEntityParams["IdProcess"]);
                Int64 _idTask=Convert.ToInt64(ManageEntityParams["IdTask"]);

                _typeExecution = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).TypeExecution;
                if (_typeExecution == "Spontaneous")
                    { return true; }

                return false;
            }
            private void LoadMenuSelection()
            {
                //Arma los items del menu.
                Boolean _showDelete = true;
                Boolean _showEdit = true;
                Boolean _showView = true;
                Boolean _showExecution = false;
                Boolean _showCompute = false;
                Boolean _showCloseException = false;
                Boolean _showTreatException = false;
                Boolean _showCreateException = false;

                //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                switch (_EntityName)
                {
                    case Common.ConstantsEntitiesName.IA.Exception:
                        _showCloseException = true;
                        _showTreatException = true;
                        _showEdit = false;
                        _showDelete = false;
                        break;

                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecution:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement:
                        _showDelete = false;
                        _showEdit = false;
                        //Como son ejecuciones, puedo ejecutar o crear excepcion, pero para crear una excepcion debe ya estar ejecutada.
                        _showExecution = true;
                        _showCreateException = true;
                        break;

                    case Common.ConstantsEntitiesName.DB.FinishedTasks:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        _showCreateException = true;
                        break;
                    case Common.ConstantsEntitiesName.DB.ClosedExceptions:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        break;

                    case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                    case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        _showExecution = true;
                        _showCloseException = true;
                        _showTreatException = true;
                        break;

                    case Common.ConstantsEntitiesName.DB.PlannedTasks:
                    case Common.ConstantsEntitiesName.DB.ActiveTasks:
                    case Common.ConstantsEntitiesName.DB.OverDueTasks:
                    case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                    case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        _showExecution = true;
                        break;

                    //Todas las entidades que no tienen EDIT
                    case Common.ConstantsEntitiesName.DS.Applicability:
                    case Common.ConstantsEntitiesName.PF.TimeUnit:
                        _showEdit = false;
                        _showDelete = false;
                        break;

                    case Common.ConstantsEntitiesName.PA.Calculations:
                    case Common.ConstantsEntitiesName.PA.Calculation:
                        _showCompute = true;
                        break;
                }
                BuildManageContextMenu(ref rmnSelection, new RadMenuEventHandler(rmnSelection_ItemClick), _showDelete, _showEdit, _showView, _showExecution, _showCompute, _showCloseException, _showTreatException, _showCreateException);
                InjectContextMenuSelectionOnClientShowing(_RgdMasterGridListManage.ClientID, true);
            }
            private void LoadGRCByEntity()
            {
                //Cuando es un Add, no debe cargar el GRC!!!
                if (!String.IsNullOrEmpty(_EntityNameGRC))
                {
                    //Dictionary<String, Object> _param = new Dictionary<String, Object>();
                    //_param.Add("IdOrganization", _IdOrganization);
                    //_param.Add("PageTitle", txtCorporateName.Text);
                    if (BuildContextInfoModuleMenu(_EntityNameGRC, ManageEntityParams))
                    {
                        base.BuildContextInfoShowMenuButton();
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
        #endregion

        #region Entity Navigators
            //private void NavigateEntity(String url, String actionTitleDecorator, NavigateMenuAction menuAction)
            //{
            //    String _entityClassName = base.GetValueFromGlobalResource("CommonListManage", _EntityName);
            //    String _entityPropertyName = (menuAction == NavigateMenuAction.Add) ? String.Empty : GetContextInfoCaption(EntityNameGrid, _RgdMasterGridListManage);
            //    String _title = String.Concat(_entityPropertyName, " [", _entityClassName, "]", actionTitleDecorator);

            //    NavigateMenuEventArgs _navArgs = BuildMenuEventArgs(_entityClassName, _entityPropertyName, NavigateMenuType.ListManagerMenu, menuAction);

            //    Navigate(url, _title, _navArgs);
            //}
            //private void NavigateEntity(String url, NavigateMenuAction menuAction)
            //{
            //    NavigateEntity(url, String.Empty, menuAction);
            //}
        #endregion

    }
}
