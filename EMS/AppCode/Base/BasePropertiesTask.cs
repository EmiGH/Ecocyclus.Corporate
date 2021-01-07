using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Reflection;
using System.Linq;
using Condesus.EMS.WebUI.MasterControls;

namespace Condesus.EMS.WebUI
{
    public class BasePropertiesTask : BaseProperties
    {
        protected void LoadTreeViewPosts(ref RadTreeView _RtvPosts)
        {
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationsRoots, ManageEntityParams);
            //Con el tree ya armado, ahora hay que llenarlo con datos.
            base.LoadGenericTreeView(ref _RtvPosts, Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty, true, false);
            base.LoadGenericTreeView(ref _RtvPosts, Common.ConstantsEntitiesName.DS.OrganizationsRoots, Common.ConstantsEntitiesName.DS.Organization, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty, true, true);
        }
        //protected void AddTreeViewPosts(ref PlaceHolder phPosts, ref RadTreeView _RtvPosts, RadTreeViewEventHandler nodeExpand, RadTreeViewEventHandler nodeCreated, RadTreeViewEventHandler nodeCheck, CustomValidator cvTreeView, ServerValidateEventHandler serverValidate)
        protected void AddTreeViewPosts(ref PlaceHolder phPosts, ref RadTreeView _RtvPosts, RadTreeViewEventHandler nodeExpand, RadTreeViewEventHandler nodeCreated, RadTreeViewEventHandler nodeCheck)
        {
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            //Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //BuildGenericDataTable(Common.ConstantsEntitiesName.PA.Magnitudes, _params);

            //Arma tree con todos los roots.
            phPosts.Controls.Clear();

            _RtvPosts = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.DS.OrganizationClassifications, "Form");

            phPosts.Controls.Add(_RtvPosts);

            _RtvPosts.Nodes.Clear();

            //Ya tengo el Tree le attacho los Handlers
            _RtvPosts.NodeExpand += new RadTreeViewEventHandler(nodeExpand);
            _RtvPosts.NodeCreated += new RadTreeViewEventHandler(nodeCreated);
            _RtvPosts.NodeCheck += new RadTreeViewEventHandler(nodeCheck);

            //Inyecta el handler para validar la seleccion de al menos un post.
            //cvTreeView.ServerValidate += new ServerValidateEventHandler(serverValidate);
        }
        protected void LoadStructPostsAux(ref ArrayList postsAux, List<Condesus.EMS.Business.DS.Entities.Post> _posts)
        {
            ///Carga de forma inicial todos los id post que ya estan cargados en esta tarea....
            postsAux = new ArrayList();

            //Ahora recorre todas las clasificaciones que ya tiene asiganadas el indicador, y los guarda en la estructura interna (ArrayList).
            foreach (Condesus.EMS.Business.DS.Entities.Post _item in _posts)
            {
                //Obtiene el Id del nodo checkeado
                Int64 _idGeographicArea = _item.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea;
                Int64 _idPosition = _item.JobTitle.FunctionalPositions.Position.IdPosition;
                Int64 _idFunctionalArea = _item.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea;
                Int64 _idOrganization = _item.Organization.IdOrganization;
                Int64 _idPerson = _item.Person.IdPerson;

                String _keyValuePost = "IdGeographicArea=" + _idGeographicArea.ToString()
                    + "& IdPosition=" + _idPosition.ToString()
                    + "& IdFunctionalArea=" + _idFunctionalArea.ToString()
                    + "& IdOrganization=" + _idOrganization.ToString()
                    + "& IdPerson=" + _idPerson.ToString();

                postsAux.Add(_keyValuePost);
            }
        }

        protected void LoadTreeViewParameterGroups(ref RadTreeView _RtvParameterGroup, Dictionary<String, Object> enitiyParams)
        {
            BuildGenericDataTable(Common.ConstantsEntitiesName.PA.ParameterGroups, enitiyParams);
            //Con el tree ya armado, ahora hay que llenarlo con datos.
            base.LoadGenericTreeView(ref _RtvParameterGroup, Common.ConstantsEntitiesName.PA.ParameterGroups, Common.ConstantsEntitiesName.PA.ParameterGroup, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, true, false);
        }
        protected void AddTreeViewParameterGroup(ref PlaceHolder phParameterGroup, ref RadTreeView _RtvParameterGroup, RadTreeViewEventHandler nodeCreated, RadTreeViewEventHandler nodeCheck)
        {
            //Arma tree con todos los roots.
            phParameterGroup.Controls.Clear();

            _RtvParameterGroup = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.PA.ParameterGroups, "Form");
            _RtvParameterGroup.CheckBoxes = true;

            phParameterGroup.Controls.Add(_RtvParameterGroup);

            _RtvParameterGroup.Nodes.Clear();

            //Ya tengo el Tree le attacho los Handlers
            _RtvParameterGroup.NodeCreated += new RadTreeViewEventHandler(nodeCreated);
            _RtvParameterGroup.NodeCheck += new RadTreeViewEventHandler(nodeCheck);
        }
        protected void LoadStructParameterGroupAux(ref ArrayList parameterGroupsAux, Dictionary<Int64, Condesus.EMS.Business.PA.Entities.ParameterGroup> parameterGroups)
        {
            ///Carga de forma inicial todos los id post que ya estan cargados en esta tarea....
            parameterGroupsAux = new ArrayList();

            //Ahora recorre todas las clasificaciones que ya tiene asiganadas el indicador, y los guarda en la estructura interna (ArrayList).
            foreach (Condesus.EMS.Business.PA.Entities.ParameterGroup _item in parameterGroups.Values)
            {
                //Obtiene el Id del nodo checkeado
                Int64 _idIndicator = _item.Indicator.IdIndicator;
                Int64 _idParameterGroup = _item.IdParameterGroup;

                String _keyValuePost = "IdParameterGroup=" + _idParameterGroup.ToString()
                    + "& IdIndicator=" + _idIndicator.ToString();

                parameterGroupsAux.Add(_keyValuePost);
            }
        }

        #region JavaScript
        protected void InjectInitializeTaskValidators(RadioButtonList radioBtnList, String taskType)
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                //TODO: Desarollar Factory (reflection) para permitir n tipos

                //Name Exapmle: "ctl00_ContentMain_rblOptionTypeExecution_0"
                String _radioListButtonClientId = radioBtnList.ClientID;
                radioBtnList.CausesValidation = false;
                switch (taskType)
                {
                    case "Spontaneous":
                        //case "":
                        _radioListButtonClientId += "_0";
                        radioBtnList.Items[0].Selected = true;
                        break;
                    case "Repeatability":
                        _radioListButtonClientId += "_1";
                        radioBtnList.Items[1].Selected = true;
                        break;
                    case "Scheduler":
                        _radioListButtonClientId += "_2";
                        radioBtnList.Items[2].Selected = true;
                        break;
                    case "":
                        radioBtnList.Items[0].Selected = false;
                        radioBtnList.Items[1].Selected = false;
                        radioBtnList.Items[2].Selected = false;
                        break;
                }

                _sbBuffer.Append(OpenHtmlJavaScript());
                //Inicializacion de Variables
                _sbBuffer.Append("var radioListId = '" + _radioListButtonClientId + "';                                         \n");
                //_sbBuffer.Append("window.attachEvent('onload', InitializeTaskValidators);                                       \n");
                _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
                _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
                _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

                _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
                _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
                _sbBuffer.Append("      window.attachEvent('onload', InitializeTaskValidators);                                \n");
                _sbBuffer.Append("  }                                                                                           \n");
                _sbBuffer.Append("  else                                                                                        \n");
                _sbBuffer.Append("  {   //FireFox                                                                               \n");
                _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeTaskValidators, false);        \n");
                _sbBuffer.Append("  }                                                                                           \n");
                //Initialize
                _sbBuffer.Append("function InitializeTaskValidators()                                                           \n");
                _sbBuffer.Append("{                                                                                             \n");
                _sbBuffer.Append("  var oRadioList = $get(radioListId);                                                         \n");
                _sbBuffer.Append("  if(oRadioList != null)                                                                      \n");
                _sbBuffer.Append("    RadioButtonChange(oRadioList);                                                            \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_InitializeTaskValidators", _sbBuffer.ToString());
            }
            protected void InjectRadioButtonChangeScheduleTask(String comboTimeUnitInterval, String validatorComboTimeUnitInterval,
                String rfvInterval, String revInterval, String customvEndDate,
                String txtMaxNumberExecutions, String txtInterval, String rdtEndDate)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                _sbBuffer.Append("var selectedRBListItem = null;  //variable global para usar en el EndRequest.                             \n");
                _sbBuffer.Append("function RadioButtonChange(rbListTypeExec)                                                                \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("if (rbListTypeExec != null)                                                                               \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("  var _maxNumberExecutions = document.getElementById('" + txtMaxNumberExecutions + "');                   \n");
                _sbBuffer.Append("  var _interval = document.getElementById('" + txtInterval + "');                                         \n");
                _sbBuffer.Append("  var _timeUnitInterval = document.getElementById('" + comboTimeUnitInterval + "');                       \n");
                _sbBuffer.Append("  var _endDate = document.getElementById('" + rdtEndDate + "');                                           \n");
                _sbBuffer.Append("  var _rfvInterval = " + rfvInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revInterval = " + revInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _cvComboTimeUnitInterval =  " + validatorComboTimeUnitInterval + ";                                 \n");
                _sbBuffer.Append("  var _cvEndDate =  " + customvEndDate + ";                                                               \n");
                _sbBuffer.Append("  var _comboInterval = document.getElementById('" + comboTimeUnitInterval + "');                                                     \n");
                _sbBuffer.Append("  var _radCalendarEndDate = document.getElementById('" + rdtEndDate + "');                                \n");
                //Lo guardamos en una variable global a la pagina para que en el EndRequest podamos llamar a este metodo nuevamente
                _sbBuffer.Append("  selectedRBListItem = rbListTypeExec;                                                                    \n");
                //Cuando es scheduler, hay que bloquear el calendario de fecha fin...
                _sbBuffer.Append("  switch (rbListTypeExec.id)                                                                              \n");
                _sbBuffer.Append("  {                                                                                                       \n");
                _sbBuffer.Append("      case 'ctl00_ContentMain_rblOptionTypeExecution_0': //Spontaneous                                    \n");
                _sbBuffer.Append("          ValidatorEnable(_revInterval, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(_rfvInterval, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(_cvComboTimeUnitInterval, false);                                               \n");
                _sbBuffer.Append("          ValidatorEnable(_cvEndDate, true);                                                              \n");
                _sbBuffer.Append("          _comboInterval.disabled = true;                                                                 \n");
                _sbBuffer.Append("          //_comboInterval.Disable();                                                                     \n");
                _sbBuffer.Append("          _interval.readOnly = true;                                                                      \n");
                _sbBuffer.Append("          _interval.value = '0';                                                                          \n");
                _sbBuffer.Append("          _timeUnitInterval.disabled = true;                                                              \n");
                _sbBuffer.Append("          //_radCalendarEndDate.DateInput.Enable();                                                       \n");
                _sbBuffer.Append("          _radCalendarEndDate.disabled = false;                                                           \n");
                _sbBuffer.Append("          _endDate.disabled = false;                                                                      \n");
                _sbBuffer.Append("          _maxNumberExecutions.readOnly = false;                                                          \n");
                _sbBuffer.Append("          _maxNumberExecutions.value = '0';                                                               \n");
                _sbBuffer.Append("          document.getElementById('typeExecution').value = 'Spontaneous';                                 \n");
                _sbBuffer.Append("          break;                                                                                          \n");

                _sbBuffer.Append("      case 'ctl00_ContentMain_rblOptionTypeExecution_1': //Repeatability                                  \n");
                _sbBuffer.Append("          ValidatorEnable(_revInterval, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(_rfvInterval, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(_cvComboTimeUnitInterval, true);                                                \n");
                _sbBuffer.Append("          ValidatorEnable(_cvEndDate, true);                                                              \n");
                _sbBuffer.Append("          //_comboInterval.Enable();                                                                      \n");
                _sbBuffer.Append("          _comboInterval.disabled = false;                                                                \n");
                _sbBuffer.Append("          _maxNumberExecutions.readOnly = true;                                                           \n");
                _sbBuffer.Append("          _interval.readOnly = false;                                                                     \n");
                _sbBuffer.Append("          _timeUnitInterval.disabled = false;                                                             \n");
                _sbBuffer.Append("          //_radCalendarEndDate.DateInput.Enable();                                                       \n");
                _sbBuffer.Append("          _radCalendarEndDate.disabled = false;                                                           \n");
                _sbBuffer.Append("          _endDate.disabled = false;                                                                      \n");
                _sbBuffer.Append("          _maxNumberExecutions.value = '0';                                                               \n");
                _sbBuffer.Append("          document.getElementById('typeExecution').value = 'Repeatability';                               \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 'ctl00_ContentMain_rblOptionTypeExecution_2': //Scheduler                                      \n");
                _sbBuffer.Append("          ValidatorEnable(_revInterval, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(_rfvInterval, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(_cvComboTimeUnitInterval, false);                                               \n");
                _sbBuffer.Append("          ValidatorEnable(_cvEndDate, false);                                                             \n");
                _sbBuffer.Append("          //_comboInterval.Disable();                                                                     \n");
                _sbBuffer.Append("          _comboInterval.disabled = true;                                                                 \n");
                _sbBuffer.Append("          _maxNumberExecutions.readOnly = true;                                                           \n");
                _sbBuffer.Append("          _maxNumberExecutions.value = '1';                                                               \n");
                _sbBuffer.Append("          _interval.value = '0';                                                                          \n");
                _sbBuffer.Append("          _interval.readOnly = true;                                                                      \n");
                _sbBuffer.Append("          _timeUnitInterval.disabled = true;                                                              \n");
                _sbBuffer.Append("          //_radCalendarEndDate.DateInput.Disable();                                                      \n");
                _sbBuffer.Append("          _radCalendarEndDate.disabled = true;                                                            \n");
                _sbBuffer.Append("          _endDate.disabled = true;                                                                       \n");
                _sbBuffer.Append("          document.getElementById('typeExecution').value = 'Scheduler';                                   \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("  }                                                                                                       \n");
                _sbBuffer.Append("}                                                                                                         \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_RadioButtonChangeScheduleTask", _sbBuffer.ToString());
            }
            protected void InjectOnClientClickNextTabProcessTask(String rmpWizzardTask, String rtsWizzardTask)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function onClientClickNextTabProcessTask()                                                                \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("      //Ejecuta la validacion de los Validator en la pagina                                               \n");
                //_sbBuffer.Append("      if (!CheckClientValidatorPage())                                                                    \n");
                //_sbBuffer.Append("      {                                                                                                   \n");
                //_sbBuffer.Append("          //Si hay validaciones, entonces cancelo el evento                                               \n");
                //_sbBuffer.Append("          return false;                                                                                   \n");
                //_sbBuffer.Append("      }                                                                                                   \n");
                _sbBuffer.Append("      //No hay validaciones, sigo normalmente.                                                            \n");
                _sbBuffer.Append("      //Busca el MultiPage, y cual es el PageView que esta seleccionado                                   \n");
                _sbBuffer.Append("      var pageView = $find('" + rmpWizzardTask + "');                                                     \n");
                _sbBuffer.Append("      var pageViewIndex = pageView.get_selectedIndex() + 1;                                               \n");
                _sbBuffer.Append("      if (pageViewIndex > 4)                                                                              \n");
                _sbBuffer.Append("      {                                                                                                   \n");
                _sbBuffer.Append("          pageViewIndex = 4;                                                                              \n");
                _sbBuffer.Append("      }                                                                                                   \n");
                _sbBuffer.Append("      //Selecciona el page view                                                                           \n");
                _sbBuffer.Append("      pageView.get_pageViews().getPageView(pageViewIndex).set_selected(true);                             \n");
                _sbBuffer.Append("      //Busca el TabStrip, y cual es el tab seleccionado                                                  \n");
                _sbBuffer.Append("      var tabStrip = $find('" + rtsWizzardTask + "');                                                     \n");
                _sbBuffer.Append("      var tabSelectedIndex = tabStrip.get_selectedIndex() + 1;                                            \n");
                _sbBuffer.Append("      if (tabSelectedIndex > 4)                                                                           \n");
                _sbBuffer.Append("      {                                                                                                   \n");
                _sbBuffer.Append("          tabSelectedIndex = 4;                                                                           \n");
                _sbBuffer.Append("      }                                                                                                   \n");
                _sbBuffer.Append("      //Selecciona el Tab                                                                                 \n");
                _sbBuffer.Append("      tabStrip.set_selectedIndex(tabSelectedIndex);                                                       \n");
                _sbBuffer.Append("                                                                                                          \n");
                _sbBuffer.Append("      return false;                                                                                       \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onClientClickNextTabProcessTask", _sbBuffer.ToString());
            }

            protected void InjectonTabWizzardTaskCalibrationSelecting(String rfvOptionTypeExecution, String customvEndDate, String revDuration, String rfvDuration,
                String rfvInterval, String revInterval, String revNumberExec,
                String cvTimeUnitDuration, String cvTimeUnitInterval, String cvMeasurementDeviceType, String cvMeasurementDevice, String cvTreeView)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function onTabWizzardTaskCalibrationSelecting(sender, args)                                               \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("  var _rfvOptionTypeExecution = " + rfvOptionTypeExecution + ";                                           \n");
                _sbBuffer.Append("  var _customvEndDate = " + customvEndDate + ";                                                           \n");
                _sbBuffer.Append("  var _rfvDuration = " + rfvDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _revDuration = " + revDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvInterval = " + rfvInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revInterval = " + revInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revNumberExec = " + revNumberExec + ";                                                             \n");

                _sbBuffer.Append("  var _cvTimeUnitDuration = " + cvTimeUnitDuration + ";                                                   \n");
                _sbBuffer.Append("  var _cvTimeUnitInterval = " + cvTimeUnitInterval + ";                                                   \n");
                _sbBuffer.Append("  var _cvMeasurementDeviceType = " + cvMeasurementDeviceType + ";                                         \n");
                _sbBuffer.Append("  var _cvMeasurementDevice = " + cvMeasurementDevice + ";                                                 \n");
                _sbBuffer.Append("  var _cvTreeView = " + cvTreeView + ";                                                 \n");

                _sbBuffer.Append("  EnableValidatorsCalibration(args.get_tab().get_index(), _rfvOptionTypeExecution, _customvEndDate, _rfvDuration, _revDuration, _rfvInterval, _revInterval, _revNumberExec, _cvTimeUnitDuration, _cvTimeUnitInterval, _cvMeasurementDeviceType, _cvMeasurementDevice, _cvTreeView)    \n");
                _sbBuffer.Append("  //Esto lo hace para dejar seleccionado lo que corresponde despues de hacer click en un tab              \n");
                _sbBuffer.Append("  var _type = document.getElementById('typeExecution').value;                                             \n");
                _sbBuffer.Append("  var oRadioList = null;                                                                                  \n");
                _sbBuffer.Append("  switch (_type)                                                                                          \n");
                _sbBuffer.Append("  {                                                                                                       \n");
                _sbBuffer.Append("      case 'Spontaneous':                                                                                 \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_0');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 'Repeatability':                                                                               \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_1');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 'Scheduler':                                                                                   \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_2');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("  }                                                                                                       \n");
                _sbBuffer.Append("  if(oRadioList != null)                                                                                  \n");
                _sbBuffer.Append("    RadioButtonChange(oRadioList);                                                                        \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onTabWizzardTaskCalibrationSelecting", _sbBuffer.ToString());
            }
            protected void InjectonTabWizzardTaskCalibrationLoad(String rfvOptionTypeExecution, String customvEndDate, String revDuration, String rfvDuration,
                String rfvInterval, String revInterval, String revNumberExec,
                String cvTimeUnitDuration, String cvTimeUnitInterval, String cvMeasurementDeviceType, String cvMeasurementDevice, String cvTreeView)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function onTabWizzardTaskCalibrationLoad(sender, args)                                               \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("  var _rfvOptionTypeExecution = " + rfvOptionTypeExecution + ";                                           \n");
                _sbBuffer.Append("  var _customvEndDate = " + customvEndDate + ";                                                           \n");
                _sbBuffer.Append("  var _rfvDuration = " + rfvDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _revDuration = " + revDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvInterval = " + rfvInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revInterval = " + revInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revNumberExec = " + revNumberExec + ";                                                             \n");
                _sbBuffer.Append("  var _cvTimeUnitDuration = " + cvTimeUnitDuration + ";                                                   \n");
                _sbBuffer.Append("  var _cvTimeUnitInterval = " + cvTimeUnitInterval + ";                                                   \n");
                _sbBuffer.Append("  var _cvMeasurementDeviceType = " + cvMeasurementDeviceType + ";                                         \n");
                _sbBuffer.Append("  var _cvMeasurementDevice = " + cvMeasurementDevice + ";                                                 \n");
                _sbBuffer.Append("  var _cvTreeView = " + cvTreeView + ";                                                 \n");

                _sbBuffer.Append("  EnableValidatorsCalibration(sender.get_selectedIndex(), _rfvOptionTypeExecution, _customvEndDate, _rfvDuration, _revDuration, _rfvInterval, _revInterval, _revNumberExec, _cvTimeUnitDuration, _cvTimeUnitInterval, _cvMeasurementDeviceType, _cvMeasurementDevice, _cvTreeView)    \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onTabWizzardTaskCalibrationLoad", _sbBuffer.ToString());
            }
            protected void InjectEnableValidatorsCalibration()
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function EnableValidatorsCalibration(tabIndex, rfvOptionTypeExecution, customvEndDate, rfvDuration, revDuration, rfvInterval, revInterval, revNumberExec, cvTimeUnitDuration, cvTimeUnitInterval, cvMeasurementDeviceType, cvMeasurementDevice, cvTreeView)                                                   \n");
                _sbBuffer.Append("{                                                                                                         \n");
                //cuando se hace click en el scheduler, habilita los validator de ese tab.
                _sbBuffer.Append("  switch (tabIndex)                                                                                       \n");
                _sbBuffer.Append("  {                                                                                                       \n");
                _sbBuffer.Append("      case 0: //Main Data                                                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitDuration, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitInterval, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(customvEndDate, false);                                                         \n");
                _sbBuffer.Append("          ValidatorEnable(rfvOptionTypeExecution, false);                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(rfvDuration, false);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(revDuration, false);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(rfvInterval, false);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(revInterval, false);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(revNumberExec, false);                                                          \n");
                _sbBuffer.Append("          ValidatorEnable(cvMeasurementDeviceType, false);                                                \n");
                _sbBuffer.Append("          ValidatorEnable(cvMeasurementDevice, false);                                                    \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 1: //Scheduler                                                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(rfvOptionTypeExecution, true);                                                  \n");
                _sbBuffer.Append("          ValidatorEnable(customvEndDate, true);                                                          \n");
                _sbBuffer.Append("          ValidatorEnable(rfvDuration, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revDuration, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(rfvInterval, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revInterval, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revNumberExec, true);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitDuration, true);                                                      \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitInterval, true);                                                      \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 2: //Calibration                                                                               \n");
                _sbBuffer.Append("          ValidatorEnable(cvMeasurementDeviceType, true);                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(cvMeasurementDevice, true);                                                     \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 3: //Task Operators                                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(cvTreeView, true);                                                              \n");
                //_sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionSave').style.display = 'block';      \n");
                //_sbBuffer.Append("          //Oculta el next.                                                                               \n");
                //_sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'none';       \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 4: //Notification Recipients                                                                   \n");
                _sbBuffer.Append("          ValidatorEnable(cvTreeView, true);                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionSave').style.display = 'block';      \n");
                _sbBuffer.Append("          //Oculta el next.                                                                               \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'none';       \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("  }                                                                                                       \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_EnableValidatorsCalibration", _sbBuffer.ToString());
            }

            protected void InjectonTabWizzardTaskMeasurementSelecting(String rfvOptionTypeExecution, String customvEndDate, String revDuration, String rfvDuration,
                String rfvInterval, String revInterval, String revNumberExec,
                String cvTimeUnitDuration, String cvTimeUnitInterval,
                String cvIndicator, String cvTimeUnitFrequency, String cvMeasurementUnit,
                String revFrequency, String rfvFrequency, String rfvMeasurementName, String cvTreeView,
                String rfvMeasurementSource, String rfvMeasurementFrequencyAtSource)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function onTabWizzardTaskMeasurementSelecting(sender, args)                                               \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("  var _rfvOptionTypeExecution = " + rfvOptionTypeExecution + ";                                           \n");
                _sbBuffer.Append("  var _customvEndDate = " + customvEndDate + ";                                                           \n");
                _sbBuffer.Append("  var _rfvDuration = " + rfvDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _revDuration = " + revDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvInterval = " + rfvInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revInterval = " + revInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revNumberExec = " + revNumberExec + ";                                                             \n");
                _sbBuffer.Append("  var _cvTimeUnitDuration = " + cvTimeUnitDuration + ";                                                   \n");
                _sbBuffer.Append("  var _cvTimeUnitInterval = " + cvTimeUnitInterval + ";                                                   \n");

                _sbBuffer.Append("  var _cvIndicator = " + cvIndicator + ";                                                                 \n");
                _sbBuffer.Append("  var _cvTimeUnitFrequency = " + cvTimeUnitFrequency + ";                                                 \n");
                _sbBuffer.Append("  var _cvMeasurementUnit = " + cvMeasurementUnit + ";                                                     \n");
                _sbBuffer.Append("  var _revFrequency = " + revFrequency + ";                                                               \n");
                _sbBuffer.Append("  var _rfvFrequency = " + rfvFrequency + ";                                                               \n");
                _sbBuffer.Append("  var _rfvMeasurementName = " + rfvMeasurementName + ";                                                                 \n");
                _sbBuffer.Append("  var _cvTreeView = " + cvTreeView + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvMeasurementSource = " + rfvMeasurementSource + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvMeasurementFrequencyAtSource = " + rfvMeasurementFrequencyAtSource + ";                                                                 \n");

                _sbBuffer.Append("  EnableValidatorsMeasurement(args.get_tab().get_index(), _rfvOptionTypeExecution, _customvEndDate, _rfvDuration, _revDuration, _rfvInterval, _revInterval, _revNumberExec, _cvTimeUnitDuration, _cvTimeUnitInterval, _cvIndicator, _cvTimeUnitFrequency, _cvMeasurementUnit, _revFrequency, _rfvFrequency, _rfvMeasurementName, _cvTreeView, _rfvMeasurementSource, _rfvMeasurementFrequencyAtSource)  \n");
                _sbBuffer.Append("  //Esto lo hace para dejar seleccionado lo que corresponde despues de hacer click en un tab              \n");
                _sbBuffer.Append("  var _type = document.getElementById('typeExecution').value;                                             \n");
                _sbBuffer.Append("  var oRadioList = null;                                                                                  \n");
                _sbBuffer.Append("  switch (_type)                                                                                          \n");
                _sbBuffer.Append("  {                                                                                                       \n");
                _sbBuffer.Append("      case 'Spontaneous':                                                                                 \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_0');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 'Repeatability':                                                                               \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_1');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 'Scheduler':                                                                                   \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_2');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("  }                                                                                                       \n");
                _sbBuffer.Append("  if(oRadioList != null)                                                                                  \n");
                _sbBuffer.Append("    RadioButtonChange(oRadioList);                                                                        \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onTabWizzardTaskMeasurementSelecting", _sbBuffer.ToString());
            }
            protected void InjectonTabWizzardTaskMeasurementLoad(String rfvOptionTypeExecution, String customvEndDate, String revDuration, String rfvDuration,
                String rfvInterval, String revInterval, String revNumberExec,
                String cvTimeUnitDuration, String cvTimeUnitInterval,
                String cvIndicator, String cvTimeUnitFrequency, String cvMeasurementUnit,
                String revFrequency, String rfvFrequency, String rfvMeasurementName, String cvTreeView,
                String rfvMeasurementSource, String rfvMeasurementFrequencyAtSource)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function onTabWizzardTaskMeasurementLoad(sender, args)                                               \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("  var _rfvOptionTypeExecution = " + rfvOptionTypeExecution + ";                                           \n");
                _sbBuffer.Append("  var _customvEndDate = " + customvEndDate + ";                                                           \n");
                _sbBuffer.Append("  var _rfvDuration = " + rfvDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _revDuration = " + revDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvInterval = " + rfvInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revInterval = " + revInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revNumberExec = " + revNumberExec + ";                                                             \n");
                _sbBuffer.Append("  var _cvTimeUnitDuration = " + cvTimeUnitDuration + ";                                                   \n");
                _sbBuffer.Append("  var _cvTimeUnitInterval = " + cvTimeUnitInterval + ";                                                   \n");
                _sbBuffer.Append("  var _cvIndicator = " + cvIndicator + ";                                                                 \n");
                _sbBuffer.Append("  var _cvTimeUnitFrequency = " + cvTimeUnitFrequency + ";                                                 \n");
                _sbBuffer.Append("  var _cvMeasurementUnit = " + cvMeasurementUnit + ";                                                     \n");
                _sbBuffer.Append("  var _revFrequency = " + revFrequency + ";                                                               \n");
                _sbBuffer.Append("  var _rfvFrequency = " + rfvFrequency + ";                                                               \n");
                _sbBuffer.Append("  var _rfvMeasurementName = " + rfvMeasurementName + ";                                                                 \n");
                _sbBuffer.Append("  var _cvTreeView = " + cvTreeView + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvMeasurementSource = " + rfvMeasurementSource + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvMeasurementFrequencyAtSource = " + rfvMeasurementFrequencyAtSource + ";                                                                 \n");

                _sbBuffer.Append("  EnableValidatorsMeasurement(sender.get_selectedIndex(), _rfvOptionTypeExecution, _customvEndDate, _rfvDuration, _revDuration, _rfvInterval, _revInterval, _revNumberExec, _cvTimeUnitDuration, _cvTimeUnitInterval, _cvIndicator, _cvTimeUnitFrequency, _cvMeasurementUnit, _revFrequency, _rfvFrequency, _rfvMeasurementName, _cvTreeView, _rfvMeasurementSource, _rfvMeasurementFrequencyAtSource)                                                   \n");

                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onTabWizzardTaskMeasurementLoad", _sbBuffer.ToString());
            }
            protected void InjectEnableValidatorsMeasurement()
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function EnableValidatorsMeasurement(tabIndex, rfvOptionTypeExecution, customvEndDate, rfvDuration, revDuration, rfvInterval, revInterval, revNumberExec, cvTimeUnitDuration, cvTimeUnitInterval, cvIndicator, cvTimeUnitFrequency, cvMeasurementUnit, revFrequency, rfvFrequency, rfvMeasurementName, cvTreeView, rfvMeasurementSource, rfvMeasurementFrequencyAtSource)                                                   \n");
                _sbBuffer.Append("{                                                                                                         \n");
                //cuando se hace click en el scheduler, habilita los validator de ese tab.
                _sbBuffer.Append("  switch (tabIndex)                                                                                       \n");
                _sbBuffer.Append("  {                                                                                                       \n");
                _sbBuffer.Append("      case 0: //Main Data                                                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitDuration, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitInterval, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(customvEndDate, false);                                                              \n");
                _sbBuffer.Append("          ValidatorEnable(rfvOptionTypeExecution, false);                                                  \n");
                _sbBuffer.Append("          ValidatorEnable(rfvDuration, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revDuration, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(rfvInterval, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revInterval, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revNumberExec, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(cvIndicator, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitFrequency, false);                                                   \n");
                _sbBuffer.Append("          ValidatorEnable(cvMeasurementUnit, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(revFrequency, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(rfvFrequency, false);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(rfvMeasurementName, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(rfvMeasurementSource, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(rfvMeasurementFrequencyAtSource, false);                                                     \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 1: //Scheduler                                                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(rfvOptionTypeExecution, true);                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(customvEndDate, true);                                                         \n");
                _sbBuffer.Append("          ValidatorEnable(rfvDuration, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(revDuration, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(rfvInterval, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(revInterval, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(revNumberExec, true);                                                          \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitDuration, true);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitInterval, true);                                                     \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 2: //Measurement                                                                               \n");
                _sbBuffer.Append("          ValidatorEnable(cvIndicator, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitFrequency, true);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(cvMeasurementUnit, true);                                                       \n");
                _sbBuffer.Append("          ValidatorEnable(revFrequency, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(rfvFrequency, true);                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(rfvMeasurementName, true);                                                      \n");
                _sbBuffer.Append("          ValidatorEnable(rfvMeasurementSource, true);                                                    \n");
                _sbBuffer.Append("          ValidatorEnable(rfvMeasurementFrequencyAtSource, true);                                         \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 3: //Task Operators                                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(cvTreeView, true);                                                              \n");
                //_sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionSave').style.display = 'block';      \n");
                //_sbBuffer.Append("          //Oculta el next.                                                                               \n");
                //_sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'none';       \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 4: //Notification Recipients                                                                   \n");
                _sbBuffer.Append("          ValidatorEnable(cvTreeView, true);                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionSave').style.display = 'block';      \n");
                _sbBuffer.Append("          //Oculta el next.                                                                               \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'none';       \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("  }                                                                                                       \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_EnableValidatorsMeasurement", _sbBuffer.ToString());
            }

            protected void InjectonTabWizzardTaskOperationSelecting(String rfvOptionTypeExecution, String customvEndDate, String revDuration,
                String rfvDuration, String rfvInterval, String revInterval, String revNumberExec,
                String cvTimeUnitDuration, String cvTimeUnitInterval, String cvTreeView)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function onTabWizzardTaskOperationSelecting(sender, args)                                                 \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("  var _rfvOptionTypeExecution = " + rfvOptionTypeExecution + ";                                           \n");
                _sbBuffer.Append("  var _customvEndDate = " + customvEndDate + ";                                                           \n");
                _sbBuffer.Append("  var _rfvDuration = " + rfvDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _revDuration = " + revDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvInterval = " + rfvInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revInterval = " + revInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revNumberExec = " + revNumberExec + ";                                                             \n");
                _sbBuffer.Append("  var _cvTimeUnitDuration = " + cvTimeUnitDuration + ";                                                   \n");
                _sbBuffer.Append("  var _cvTimeUnitInterval = " + cvTimeUnitInterval + ";                                                   \n");
                _sbBuffer.Append("  var _cvTreeView = " + cvTreeView + ";                                                   \n");
                _sbBuffer.Append("  EnableValidatorsOperation(args.get_tab().get_index(), _rfvOptionTypeExecution, _customvEndDate, _rfvDuration, _revDuration, _rfvInterval, _revInterval, _revNumberExec, _cvTimeUnitDuration, _cvTimeUnitInterval, _cvTreeView)                                                   \n");

                _sbBuffer.Append("  //Esto lo hace para dejar seleccionado lo que corresponde despues de hacer click en un tab              \n");
                _sbBuffer.Append("  var _type = document.getElementById('typeExecution').value;                                             \n");
                _sbBuffer.Append("  var oRadioList = null;                                                                                  \n");
                _sbBuffer.Append("  switch (_type)                                                                                          \n");
                _sbBuffer.Append("  {                                                                                                       \n");
                _sbBuffer.Append("      case 'Spontaneous':                                                                                 \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_0');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 'Repeatability':                                                                               \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_1');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 'Scheduler':                                                                                   \n");
                _sbBuffer.Append("          oRadioList = document.getElementById('ctl00_ContentMain_rblOptionTypeExecution_2');                                  \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("  }                                                                                                       \n");
                _sbBuffer.Append("  if(oRadioList != null)                                                                                  \n");
                _sbBuffer.Append("    RadioButtonChange(oRadioList);                                                                        \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onTabWizzardTaskOperationSelecting", _sbBuffer.ToString());
            }
            protected void InjectonTabWizzardTaskOperationLoad(String rfvOptionTypeExecution, String customvEndDate, String revDuration,
                String rfvDuration, String rfvInterval, String revInterval, String revNumberExec,
                String cvTimeUnitDuration, String cvTimeUnitInterval, String cvTreeView)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function onTabWizzardTaskOperationLoad(sender, args)                                                      \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("  var _rfvOptionTypeExecution = " + rfvOptionTypeExecution + ";                                           \n");
                _sbBuffer.Append("  var _customvEndDate = " + customvEndDate + ";                                                           \n");
                _sbBuffer.Append("  var _rfvDuration = " + rfvDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _revDuration = " + revDuration + ";                                                                 \n");
                _sbBuffer.Append("  var _rfvInterval = " + rfvInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revInterval = " + revInterval + ";                                                                 \n");
                _sbBuffer.Append("  var _revNumberExec = " + revNumberExec + ";                                                             \n");
                _sbBuffer.Append("  var _cvTimeUnitDuration = " + cvTimeUnitDuration + ";                                                   \n");
                _sbBuffer.Append("  var _cvTimeUnitInterval = " + cvTimeUnitInterval + ";                                                   \n");
                _sbBuffer.Append("  var _cvTreeView = " + cvTreeView + ";                                                   \n");

                _sbBuffer.Append("  EnableValidatorsOperation(sender.get_selectedIndex(), _rfvOptionTypeExecution, _customvEndDate, _rfvDuration, _revDuration, _rfvInterval, _revInterval, _revNumberExec, _cvTimeUnitDuration, _cvTimeUnitInterval, _cvTreeView)                                                   \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onTabWizzardTaskOperationLoad", _sbBuffer.ToString());
            }
            protected void InjectEnableValidatorsOperation()
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function EnableValidatorsOperation(tabIndex, rfvOptionTypeExecution, customvEndDate, rfvDuration, revDuration, rfvInterval, revInterval, revNumberExec, cvTimeUnitDuration, cvTimeUnitInterval, cvTreeView)                                                   \n");
                _sbBuffer.Append("{                                                                                                         \n");
                //cuando se hace click en el scheduler, habilita los validator de ese tab.
                _sbBuffer.Append("  switch (tabIndex)                                                                                       \n");
                _sbBuffer.Append("  {                                                                                                       \n");
                _sbBuffer.Append("      case 0: //Main Data                                                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitDuration, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitInterval, false);                                                     \n");
                _sbBuffer.Append("          ValidatorEnable(customvEndDate, false);                                                              \n");
                _sbBuffer.Append("          ValidatorEnable(rfvOptionTypeExecution, false);                                                  \n");
                _sbBuffer.Append("          ValidatorEnable(rfvDuration, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revDuration, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(rfvInterval, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revInterval, false);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revNumberExec, false);                                                           \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 1: //Scheduler                                                                                 \n");
                _sbBuffer.Append("          ValidatorEnable(rfvOptionTypeExecution, true);                                                  \n");
                _sbBuffer.Append("          ValidatorEnable(customvEndDate, true);                                                          \n");
                _sbBuffer.Append("          ValidatorEnable(rfvDuration, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revDuration, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(rfvInterval, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revInterval, true);                                                             \n");
                _sbBuffer.Append("          ValidatorEnable(revNumberExec, true);                                                           \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitDuration, true);                                                      \n");
                _sbBuffer.Append("          ValidatorEnable(cvTimeUnitInterval, true);                                                      \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 2: //Operation (por ahora no tiene que hacer nada)                                             \n");
                _sbBuffer.Append("          //Muestra el next.                                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'block';      \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 3: //Task Operators                                                                            \n");
                _sbBuffer.Append("          ValidatorEnable(cvTreeView, true);                                                              \n");
                //_sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionSave').style.display = 'block';      \n");
                //_sbBuffer.Append("          //Oculta el next.                                                                               \n");
                //_sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'none';       \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("      case 4: //Notification Recipients                                                                   \n");
                _sbBuffer.Append("          ValidatorEnable(cvTreeView, true);                                                              \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionSave').style.display = 'block';      \n");
                _sbBuffer.Append("          //Oculta el next.                                                                               \n");
                _sbBuffer.Append("          document.getElementById('ctl00_MasterFWContentToolbarActionNext').style.display = 'none';       \n");
                _sbBuffer.Append("          break;                                                                                          \n");
                _sbBuffer.Append("  }                                                                                                       \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_EnableValidatorsOperation", _sbBuffer.ToString());
            }

        #endregion

    }
}
