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
    public partial class TaskList : BaseProperties
    {
        #region Internal Properties
            private RadGrid _RgdMasterGridListManage;
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
            private RadComboBox _RdcTaskState;
        #endregion

        #region PageLoad & Init
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
                IsGridPageIndexChanged = false;
                IsReturnFromDelete = false;
            }
            protected override void OnLoadComplete(EventArgs e)
            {
                base.OnLoadComplete(e);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                //Carga todos los paramtros que recibe la pagina...
                //LoadParameters();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                //lblToday.Text = "Today" + " - " + DateTime.Now.ToLongDateString();
                AddComboTaskState();
                if (!IsPostBack)
                {
                    rcCalendar.FocusedDate = DateTime.Now.AddMonths(-1);
                }
                //Por defecto cargamos los OverDue.
                //EntityNameGrid = Common.ConstantsEntitiesName.DB.OverDueTasks;
                switch (GetKeyValue(_RdcTaskState.SelectedValue, "IdState").ToString())
                {
                    case "Finished":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.FinishedTasks;
                        break;
                    case "OverDue":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.OverDueTasks;
                        break;
                    case "Planned":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.PlannedTasks;
                        break;
                    case "Working":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.ActiveTasks;
                        break;
                }

                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                //Arma la grilla completa
                LoadListManage();

                SetAllDatesTaskByState();

                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
                //Carga el menu de opciones generales.
                LoadGeneralOptionMenu();
                //Inyecta los javascript que necesita la pagina
                InyectJavaScripts();
                //Inserta todos los manejadores de eventos que necesita la apgina
                InitializeLocalHandlers();
                //Arma el Menu de Seleccion.
                LoadMenuSelection();

                LoadMenuSearch();
            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = Resources.Common.OperativeDashboard;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                try
                {
                    base.PageTitleSubTitle = Resources.Common.TasksByDate;
                }
                catch { base.PageTitleSubTitle = String.Empty; }
            }
        #endregion

        #region Page Event
            protected void GridLinkButtonClick_Click(object sender, EventArgs e)
            {
                IButtonControl _lnkButton = (IButtonControl)sender;
                //Debo acceder a la grilla...
                //Como puede haber muchas grillas, entonces la busco como padre el linkButton.
                RadGrid _rgdViewers = (RadGrid)((LinkButton)sender).Parent.Parent.Parent.Parent.Parent;

                //Setea los parametros en el Navigate.
                BuildNavigateParamsFromListManageSelected(_rgdViewers);

                //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                    + "&" + BuildParamsFromListManageSelected(_rgdViewers);
                NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                String _paramsToNavigate = ((LinkButton)sender).CommandArgument; //_pkCompost;
                if (String.IsNullOrEmpty(_paramsToNavigate))
                {
                    //Si viene vacio, entonces uso lo que tiene cargado pkCompost...
                    _paramsToNavigate = _pkCompost;
                }
                String _entityName = Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"));
                NavigatorAddTransferVar("EntityName", _entityName);
                NavigatorAddTransferVar("EntityNameGrid", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameGrid")));
                NavigatorAddTransferVar("EntityNameContextInfo", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextInfo")));
                NavigatorAddTransferVar("EntityNameContextElement", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextElement")));

                //Navigate(GetPageViewerByEntity(Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"))), Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));

                String _url = GetPageViewerByEntity(Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));
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

                        switch (EntityNameGrid)
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
                        catch (Exception ex)
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
                //Cuando estoy en un Listado de Lenguajes y alguien pide add, lo debo llevar al add de la entidad completa, no del Lenguaje. por eso todos los replace del "_LG".
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
                        base.NavigatorAddTransferVar("EntityName", _EntityName.Replace("_LG", String.Empty));
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);

                        String _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName.Replace("_LG", String.Empty)).ToString();
                        String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                        NavigateEntity(_urlProperties, _EntityName.Replace("_LG", String.Empty), _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    default:
                        break;
                }
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

                    //Setea que viene de un delete
                    IsReturnFromDelete = true;

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
                    base.NavigatorAddTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));

                    NavigateEntity(HttpContext.Current.Request.Url.AbsolutePath, EntityNameGrid, Condesus.WebUI.Navigation.NavigateMenuAction.Delete);
                    IsGridPageIndexChanged = true;

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
            protected void RgdMasterGridListManage_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    #region rptButton
                    HtmlImage oimg2 = (HtmlImage)e.Item.FindControl("rptButton");
                    if (!(oimg2 == null))
                    {
                        try
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
                        catch
                        {
                            //Como sale por error, no quiero que salga el icono.
                            oimg2.Attributes["class"] = String.Empty;
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
                }
            }
            protected void _RdcTaskState_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                //Cambio el tipo de estado
                String _selectedValue = ((RadComboBox)sender).SelectedValue;
                //volvemos a poner el foco en el dia actual!!
                rcCalendar.FocusedDate = DateTime.Now.AddMonths(-1);

                switch (GetKeyValue(_selectedValue, "IdState").ToString())
                {
                    case "Finished":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.FinishedTasks;
                        break;
                    case "OverDue":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.OverDueTasks;
                        break;
                    case "Planned":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.PlannedTasks;
                        break;
                    case "Working":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        EntityNameGrid = Common.ConstantsEntitiesName.DB.ActiveTasks;
                        break;
                }
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                
                _RgdMasterGridListManage.DataSource = DataTableListManage[EntityNameGrid];
                //Rebind de la grilla.
                _RgdMasterGridListManage.Rebind();
                
                //Finalmente como cambio el combo del estado de tareas, vuelve a cargar las fechas
                SetAllDatesTaskByState();
            }
            protected void rcCalendar_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
            {
                String _filterDate = "StartDate >= '" + e.SelectedDates[e.SelectedDates.Count - 1].Date + "' AND StartDate <= '" + e.SelectedDates[e.SelectedDates.Count - 1].Date.AddHours(12) + "'";

                if (EntityNameGrid != "PlannedTasks")
                {
                    DataRow[] _dataRow = DataTableListManage[EntityNameGrid].Select(_filterDate);
                    DataTable _dt = DataTableListManage[EntityNameGrid].Clone();

                    for (Int64 i = 0; i < _dataRow.Length; i++)
                    {
                        _dt.ImportRow(_dataRow[i]);
                    }

                    _RgdMasterGridListManage.DataSource = _dt;
                    //Rebind de la grilla.
                    _RgdMasterGridListManage.Rebind();
                }
                else
                {
                    if (ManageEntityParams.ContainsKey("StartDate"))
                    {
                        ManageEntityParams.Remove("StartDate");
                    }
                    ManageEntityParams.Add("StartDate", e.SelectedDates[e.SelectedDates.Count - 1].Date);
                    BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                    _RgdMasterGridListManage.DataSource = DataTableListManage[EntityNameGrid];
                    //Rebind de la grilla.
                    _RgdMasterGridListManage.Rebind();
                }
            }
        #endregion

        #region Private Method
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
            private void AddComboTaskState()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();

                AddCombo(phTaskState, ref _RdcTaskState, Common.ConstantsEntitiesName.PF.TaskState, String.Empty, _params, false, false, false, true, false);

                _RdcTaskState.SelectedValue = "IdState=OverDue";
                _RdcTaskState.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcTaskState_SelectedIndexChanged);
            }
            private void InyectJavaScripts()
            {
                base.InjectRowContextMenu(rmnSelection.ClientID, String.Empty);
                base.InjectShowMenu(rmnSelection.ClientID, _RgdMasterGridListManage.ClientID);
                base.InjectClientSelectRow(_RgdMasterGridListManage.ClientID);
                base.InjectRmnSelectionItemClickHandler(String.Empty, String.Empty, false);
                base.InjectValidateItemChecked(_RgdMasterGridListManage.ClientID);
            }
            private void InitializeLocalHandlers()
            {
                btnOkDelete.Click += new EventHandler(btnOkDelete_Click);
                GridLinkButtonClick = new EventHandler(GridLinkButtonClick_Click);
                _RgdMasterGridListManage.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);
                _RgdMasterGridListManage.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu";

                rcCalendar.SelectionChanged += new Telerik.Web.UI.Calendar.SelectedDatesEventHandler(rcCalendar_SelectionChanged);
            }
            private void LoadListManage()
            {
                Boolean _showImgSelect = true;
                Boolean _showCheck = false;
                Boolean _showOpenFile = false;
                Boolean _showOpenChart = false;
                Boolean _showOpenSeries = false;
                Boolean _allowSearchableGrid = true; //En el manage si se muestra el search.

                _RgdMasterGridListManage = base.BuildListManageContent(EntityNameGrid, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
                _RgdMasterGridListManage.GroupingEnabled = true;
                _RgdMasterGridListManage.ShowGroupPanel = false;
                _RgdMasterGridListManage.MasterTableView.GroupLoadMode = GridGroupLoadMode.Client;
                _RgdMasterGridListManage.MasterTableView.GroupsDefaultExpanded = false;
                _RgdMasterGridListManage.ClientSettings.AllowDragToGroup = false;
                _RgdMasterGridListManage.MasterTableView.UseAllDataFields = true;
                _RgdMasterGridListManage.MasterTableView.RetrieveAllDataFields = true;

                String _expression = "Project [" + Resources.CommonListManage.Project + "], Title [" + Resources.CommonListManage.Task + "], count(Title) Items [" + Resources.CommonListManage.Count + "] Group By Project, Title";
                GridGroupByExpression _gridGroupByExpression = GridGroupByExpression.Parse(_expression);

                _RgdMasterGridListManage.MasterTableView.GroupByExpressions.Add(_gridGroupByExpression);

                pnlListManage.Controls.Add(_RgdMasterGridListManage);
                base.PersistControlState(_RgdMasterGridListManage);
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
                Int64 _idprocess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                Int64 _idTask = Convert.ToInt64(ManageEntityParams["IdTask"]);

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
                switch (EntityNameGrid)
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
                    case Common.ConstantsEntitiesName.PA.KeyIndicator:
                    case Common.ConstantsEntitiesName.PA.Measurement:
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
            private void SetAllDatesTaskByState()
            {
                String _entitiyNameListDatesOfTasks = String.Empty;
                switch (GetKeyValue(_RdcTaskState.SelectedValue, "IdState").ToString())
                {
                    case "Finished":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        _entitiyNameListDatesOfTasks = Common.ConstantsEntitiesName.DB.DatesOfFinishedTasks;
                        break;
                    case "OverDue":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        _entitiyNameListDatesOfTasks = Common.ConstantsEntitiesName.DB.DatesOfOverdueTasks;
                        break;
                    case "Planned":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        _entitiyNameListDatesOfTasks = Common.ConstantsEntitiesName.DB.DatesOfPlannedTasks;
                        break;
                    case "Working":
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        _entitiyNameListDatesOfTasks = Common.ConstantsEntitiesName.DB.DatesOfWorkingTasks;
                        break;
                }

                BuildGenericDataTable(_entitiyNameListDatesOfTasks, ManageEntityParams);
                DataTable _dt = DataTableListManage[_entitiyNameListDatesOfTasks];
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    DateTime _date = Convert.ToDateTime(_dt.Rows[i]["StartDate"]);

                    RadCalendarDay _rcdDay = new RadCalendarDay();
                    _rcdDay.Date = _date;
                    _rcdDay.ItemStyle.BackColor = System.Drawing.Color.Aquamarine;
                    _rcdDay.ToolTip = Resources.Common.OperativeDashboardDayTooltip;
                    rcCalendar.SpecialDays.Add(_rcdDay);
                }

            }
        #endregion
    }
}
