using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Microsoft.Live.ServerControls.VE;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.WebUI.Navigation;
using System.Text;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class GeographicDashboardFacilities : BaseProperties
    {
        #region Internal Properties
            public String _TwitterUser = String.Empty;
            public String _PointsLatLong = String.Empty;
            public String _PointLatLongProcess = String.Empty;
            public String _MapType = "virtualearth";
            private String _PageTitleLocal = String.Empty;
            public String IdProcess = String.Empty;
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private String _EntityName = String.Empty;
            private String _EntityNameGRC = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            private RadComboBox _RdcAccountingActivity;
            private Int64 _IdActivity = 0;
        #endregion

        #region Load & Init
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);
                
                //InjectGoogleMapRegisterKey();
                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                btnFilter.Click += new EventHandler(btnFilter_Click);
                btnOkDelete.Click += new EventHandler(btnOkDelete_Click);

                LoadTextLabels();
                LoadParameters();
                AddComboActivities();

                if (!Page.IsPostBack)
                {
                    GetAllGeographicData();
                }

                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
                //Carga el Menu de Context ElementMaps a la derecha.
                LoadContextElementMapsByEntity();
                //Carga el menu de opciones generales.
                LoadGeneralOptionMenu();
                //Carga el menu de Seguridad...
                LoadSecurityOptionMenu();

                LoadSocialNetworks();
                RDockFacebook.ContentContainer.Style.Add(HtmlTextWriterStyle.Overflow, "hidden");
                RDockTwitter.ContentContainer.Style.Add(HtmlTextWriterStyle.Overflow, "hidden"); 
            }

            protected override void SetPagetitle()
            {
                Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                if (_process != null)
                {
                    _PageTitleLocal = _process.LanguageOption.Title;
                    base.PageTitle = _process.LanguageOption.Title;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.LeyendGeographicDashboardFacility;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                lblTitleSearch.Text = Resources.Common.TitlePopSearch;
                lblSubTitleSearch.Text = Resources.CommonListManage.SearchLocationTitle;
                btnSearch.ToolTip = Resources.CommonListManage.Search;
                btnShowSearch.Text = Resources.CommonListManage.Search;
                btnClose.Text = Resources.Common.btnClosePopup;
                btnSearch.Text = Resources.Common.btnSearchApply;

                lblSubTitleFilter.Text = Resources.CommonListManage.FilterTitlePopUpProcess;
                btnFilter.Text = Resources.CommonListManage.Filter;
                btnShowFilter.Text = Resources.CommonListManage.Filter;
            }
            private void GetAllGeographicData()
            {
                //Se muestran todos los Facilities del Process seleccionado
                _PointsLatLong += GetCoordinatesFacilitiesByProcess();
            }
            private String GetCoordinatesFacilitiesByProcess()
            {
                String _coords = String.Empty;
                Dictionary<Int64, Site> _sites = new Dictionary<Int64, Site>();

                ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);

                //Tengo que obtener todos las tareas de un process, y de ahi sacar sus facilities.
                var _lnqSites = from s in _processGroupProcess.Sites.Values
                                where s.Coordinate != null && !String.IsNullOrEmpty(s.Coordinate)
                                //where s.GetType().Name == "Facility" && s.Coordinate != null
                                select s;
                foreach (Site _site in _lnqSites)
                {
                    if (_site.GetType().Name == "Facility")
                    {
                        //Si ya fue cargado no lo vuelve a meter, para no duplicar puntos en el mapa.
                        if (!_sites.ContainsKey(_site.IdFacility))
                        {
                            //Agrega el site, para verificar si ya existe...
                            _sites.Add(_site.IdFacility, _site);
                        }
                    }
                    else
                    {
                        Facility _facilityRoot = GetFacilityRoot(((Sector)_site).Parent.IdFacility);
                        if (!_sites.ContainsKey(_facilityRoot.IdFacility))
                        {
                            //Agrega el site, para verificar si ya existe...
                            _sites.Add(_facilityRoot.IdFacility, (Site)_facilityRoot);
                        }
                    }
                }
                
                foreach (Site _site in _sites.Values)
                {
                    String _facilityType = GetIconNameFacilityType(_site.IdFacilityType);
                    Dictionary<Int64, ProcessTask> _tasks = _site.TasksByProcess(_processGroupProcess);
                    if (_tasks.Count > 0)
                    {
                        //Si al menos una Tarea tiene excepciones, pongo el Icono en ROJO!
                        foreach (ProcessTask item in _tasks.Values)
                        {
                            ProcessTaskMeasurement _processTaskMeasurement = (ProcessTaskMeasurement)item;
                            if ((_processTaskMeasurement.ExecutionStatus) || (_processTaskMeasurement.MeasurementStatus))
                            {
                                _facilityType = _facilityType.Replace("Red", String.Empty) + "Red";
                            }
                        }
                        //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                        _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _site.IdFacility.ToString() + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
                    }
                    else
                    {
                        Facility _facility = (Facility)_site;
                        //Obtengo todos los hijos de este facility para recorrer las tareas y ver si va el ROJO!!!
                        Dictionary<Int64, Sector> _sectors = GetAllSectors(_facility);
                        foreach (Sector _item in _sectors.Values)
                        {
                            Dictionary<Int64, ProcessTask> _processTasks = _item.TasksByProcess(_processGroupProcess);
                            if (_processTasks.Count > 0)
                            {
                                //Si al menos una Tarea tiene excepciones, pongo el Icono en ROJO!
                                foreach (ProcessTask item in _processTasks.Values)
                                {
                                    ProcessTaskMeasurement _processTaskMeasurement = (ProcessTaskMeasurement)item;
                                    if ((_processTaskMeasurement.ExecutionStatus) || (_processTaskMeasurement.MeasurementStatus))
                                    {
                                        _facilityType = _facilityType.Replace("Red", String.Empty) + "Red";
                                    }
                                }
                                //Agrega el Punto geografico pero con la informacion del FacilityROOT (viene de arriba) solo cambia el icono...
                                //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                                _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _site.IdFacility.ToString() + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
                            }
                        }
                    }
                }

                //Como no hay Facilities cargado aun para este process... setea las coordenadas del Process para que el mapa salga bien.
                if (String.IsNullOrEmpty(_coords))
                {
                    _PointLatLongProcess = _processGroupProcess.Coordinate;
                }

                return _coords;
            }
            private Dictionary<Int64, Sector> GetAllSectors(Facility facility)
            {
                Dictionary<Int64, Sector> _children = new Dictionary<Int64, Sector>();

                foreach (Sector _item in facility.Sectors.Values)
                {
                    _children.Add(_item.IdFacility, _item);
                    GetChildrensSector(_item, _children);
                }

                return _children;
            }
            private void GetChildrensSector(Sector sector, Dictionary<Int64, Sector> childrens)
            {
                
                foreach (Sector _item in sector.Sectors.Values)
                {
                    childrens.Add(_item.IdFacility, _item);
                    GetChildrensSector(_item, childrens);
                }
            }
            private String GetCoordinatesFacilitiesByProcessOLD()
            {
                String _coords = String.Empty;
                Dictionary<Int64, Site> _sites = new Dictionary<Int64, Site>();

                //Tengo que obtener todos las tareas de un process, y de ahi sacar sus facilities.
                foreach (ProcessTask _processTask in ((ProcessGroup)EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess)).ChildrenTask.Values)
                {
                    Site _site = _processTask.Site;

                    if (_site != null)
                    {
                        //Si ya fue cargado no lo vuelve a meter, para no duplicar puntos en el mapa.
                        if (!_sites.ContainsKey(_site.IdFacility))
                        {
                            //Agrega el site, para verificar si ya existe...
                            _sites.Add(_site.IdFacility, _site);

                            if (_site.GetType().Name == "Facility")
                            {
                                if (!String.IsNullOrEmpty(_site.Coordinate))
                                {
                                    //String _facilityType = GetIconNameFacilityType(((Facility)_site).FacilityType.IdFacilityType);
                                    String _facilityType = GetIconNameFacilityType(_site.IdFacilityType);
                                    //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                                    _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _site.IdFacility.ToString() + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
                                }
                            }
                            else
                            {   //Al ser un Sector, entonces tengo que ir a buscar a que facility pertenece y mostrar el facility!!nunca se muestran los sectores
                                Facility _facilityRoot = GetFacilityRoot(((Sector)_site).Parent.IdFacility);
                                if (!_sites.ContainsKey(_facilityRoot.IdFacility))
                                {
                                    //Agrega el site, para verificar si ya existe...
                                    _sites.Add(_facilityRoot.IdFacility, (Site)_facilityRoot);

                                    //Si ese facility tiene coordenadas, entonces me meto...
                                    if (!String.IsNullOrEmpty(_facilityRoot.Coordinate))
                                    {
                                        String _facilityType = GetIconNameFacilityType(_facilityRoot.IdFacilityType);
                                        //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                                        _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _facilityRoot.IdFacility.ToString() + "|" + _facilityRoot.Coordinate.Replace(";", "|").Substring(0, _facilityRoot.Coordinate.Length - 1) + ";";
                                    }
                                }
                            }
                        }
                    }

                }


                return _coords;
            }
            private Facility GetFacilityRoot(Int64 idSite)
            {
                Site _site = EMSLibrary.User.GeographicInformationSystem.Site(idSite);
                if (_site.GetType().Name == "Facility")
                {
                    return (Facility)_site;
                }
                else
                {
                    return GetFacilityRoot(((Sector)_site).Parent.IdFacility);
                }
            }
            private String GetIconNameFacilityType(Int64 idFacilityType)
            {
                String _iconName = String.Empty;
                FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(idFacilityType);

                if (String.IsNullOrEmpty(_facilityType.IconName))
                {
                    _iconName = "Unspecified";
                }
                else
                {
                    _iconName = _facilityType.IconName;
                }

                return _iconName;

                //switch (idFacilityType)
                //{
                //    case Common.Constants.idPowerPlant: //1
                //        _iconName = "PowerPlant";
                //        break;
                //    case Common.Constants.idShopsAndServices: //2
                //        _iconName = "ShopsandServices";
                //        break;
                //    case Common.Constants.idHomes:  // 3
                //        _iconName = "Homes";
                //        break;
                //    case Common.Constants.idRefineries: // 4
                //        _iconName = "Refineries";
                //        break;
                //    case Common.Constants.idServiceStations:    // 5
                //        _iconName = "ServiceStations";
                //        break;
                //    case Common.Constants.idWaterTreatmentPlants:  //7
                //        _iconName = "WaterTreatmentPlants";
                //        break;
                //    case Common.Constants.idIndustries://8
                //        _iconName = "Industries";
                //        break;
                //    case Common.Constants.idFarms:  //  9
                //        _iconName = "Farms";
                //        break;
                //    case Common.Constants.idWasteTreatmentPlants:   // 10
                //        _iconName = "WasteTreatmentPlants";
                //        break;
                //    case Common.Constants.idLandfill:   // 11
                //        _iconName = "Landfill";
                //        break;
                //    case Common.Constants.idLand:   // 12
                //        _iconName = "Land";
                //        break;

                //    case Common.Constants.idOffice: // = 13
                //        _iconName = "Office";
                //        break;
                //    case Common.Constants.idUnspecified:  // = 14;
                //        _iconName = "Unspecified";
                //        break;
                //    case Common.Constants.idOilPipeline:    // = 15;
                //        _iconName = "OilPipeline";
                //        break;
                //    case Common.Constants.idGasPipeline:    // = 16;
                //        _iconName = "GasPipeline";
                //        break;
                //    case Common.Constants.idBatteries:  // = 17;
                //        _iconName = "BatteriesOil";
                //        break;
                //    case Common.Constants.idMotorCompressorStation: // = 19;
                //        _iconName = "MotorCompressorStation";
                //        break;
                //    case Common.Constants.idOilTreatmentPlant:  // = 20;
                //        _iconName = "OilTreatmentPlant";
                //        break;
                //    case Common.Constants.idConditioningPlantDewpoint:  // = 21;
                //        _iconName = "ConditioningPlantDewpoint";
                //        break;
                //    case Common.Constants.idSeparationPlantOfLiquefiedGases:    // = 22;
                //        _iconName = "SeparationPlantOfLiquefiedGases";
                //        break;
                //    case Common.Constants.idSaltWaterInjectionPlant:    // = 23;
                //        _iconName = "SaltWaterInjectionPlant";
                //        break;
                //    case Common.Constants.idFreshWaterInjectionPlant:   // = 24;
                //        _iconName = "FreshWaterInjectionPlant";
                //        break;
                //    case Common.Constants.idFreshWaterTransferPlant: // = 25;
                //        _iconName = "FreshWaterTransferPlant";
                //        break;
                //    case Common.Constants.idThermalPowerPlant:  // = 27;
                //        _iconName = "ThermalPowerPlant";
                //        break;
                //    case Common.Constants.idOilWell:    // = 28;
                //        _iconName = "OilWell";
                //        break;
                //    case Common.Constants.idFleetVehicles:  // = 29;
                //        _iconName = "FleetVehicles";
                //        break;
                //    case Common.Constants.idGlobal:  // = 30;
                //        _iconName = "Global";
                //        break;

                //    case Common.Constants.idMining:
                //        _iconName = "Mining";
                //        break;
                //    case Common.Constants.idServices:
                //        _iconName = "Services";
                //        break;

                //    default:
                //        _iconName = "Unspecified";
                //        break;
                //}

                //return _iconName;
            }

            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
            private void LoadParameters()
            {
                _MapType = ConfigurationManager.AppSettings["MAP_Type_Vendor"];

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

                if (base.NavigatorContainsTransferVar("EntityNameContextInfo"))
                {
                    _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
                }
                if (base.NavigatorContainsTransferVar("EntityNameContextElement"))
                {
                    _EntityNameContextElement = base.NavigatorGetTransferVar<String>("EntityNameContextElement");
                }
                if (base.NavigatorContainsTransferVar("EntityName"))
                {
                    _EntityName = base.NavigatorGetTransferVar<String>("EntityName");
                    if (_EntityName == Common.ConstantsEntitiesName.DS.FacilitiesByProcess)
                    {
                        _EntityName = Common.ConstantsEntitiesName.PF.ProcessGroupProcess;
                    }

                }

                //Para el caso del Remove, debe ser el nombre de la entidad mas la palabra Remove!!!
                EntityNameToRemove = _EntityName + "Remove";


                IdProcess = _IdProcess.ToString();
            }
            private void LoadGRCByEntity()
            {
                //TODO: Por ahora lo preguntamos aca...pero despues lo deberiamos hacer en otro lado, me parece!!
                //Cuando es un Add, no debe cargar el GRC!!!
                if (!String.IsNullOrEmpty(_EntityNameGRC))
                {
                    ManageEntityParams.Concat(GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost")));
                    if (BuildContextInfoModuleMenu(_EntityNameGRC, ManageEntityParams))
                    {
                        base.BuildContextInfoShowMenuButton();
                    }
                }
            }
            private void LoadSocialNetworks()
            {
                ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);

                if (!String.IsNullOrEmpty(_processGroupProcess.TwitterUser))
                {
                    _TwitterUser = _processGroupProcess.TwitterUser;
                }
                else
                {
                    RDockTwitter.Visible = false;
                }
                if (!String.IsNullOrEmpty(_processGroupProcess.FacebookUser))
                {
                    pnlFBComments.Attributes.Add("data-href", _processGroupProcess.FacebookUser);
                }
                else
                {
                    RDockFacebook.Visible = false;
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
            private void LoadGeneralOptionMenu()
            {
                BuildPropertyGeneralOptionsMenu(_EntityName, new RadMenuEventHandler(rmnuGeneralOption_ItemClick), false);
            }
            private void LoadSecurityOptionMenu()
            {
                //Menu de Seguridad
                base.BuildPropertySecuritySystemMenu(_EntityName, new RadMenuEventHandler(rmnuSecuritySystem_ItemClick));
            }

            private void AddComboActivities()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phActivities, ref _RdcAccountingActivity, Common.ConstantsEntitiesName.PA.AccountingActivities, String.Empty, _params, false, true, false, false, false);
                _RdcAccountingActivity.ZIndex = 10000000;
            }

        //private String GetCoordinatesForFacilities()
        //{
        //    String _coords = String.Empty;

        //    foreach (Organization _organization in EMSLibrary.User.Dashboard.Organizations.Values)
        //    {
        //        try
        //        {
        //            //Recorro todos los facility para cada organizacion, y por cada uno verifico si tiene coordenadas...entonces lo carga.
        //            foreach (Facility _facility in _organization.Facilities.Values)
        //            {
        //                if (!String.IsNullOrEmpty(_facility.Coordinate))
        //                {
        //                    _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facility.IdFacility + "|" + _facility.Coordinate.Replace(";", "|").Substring(0, _facility.Coordinate.Length - 1) + ";";
        //                }
        //            }
        //        }
        //        catch { }
        //    }

        //    return _coords;
        //}
        //private String GetCoordinatesForMeasurementDevices()
        //{
        //    String _coords = String.Empty;
        //    //Obtengo todos los Dispositivos!
        //    Dictionary<Int64, MeasurementDevice> _measurementDevices = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDevices();
        //    //Debe quedar asi:
        //    //MeasurementDevice|13|Pushpin|-47.27922900257082|-69.2578125;MeasurementDevice|14|Pushpin|-33.72433966174761|-67.14843750000001

        //    //Asi viene:
        //    //Polygon;-34.59951433005614|-58.39310646057129;-34.60453038355463|-58.39241981506348;-34.60368262114032|-58.38151931762695;-34.59894912193819|-58.382205963134766;
        //    foreach (MeasurementDevice _measurementDevice in _measurementDevices.Values)
        //    {
        //        //Recorro todos los dispositivos, y por cada uno verifico si tiene coordenadas...entonces lo carga.
        //        Site _site = _measurementDevice.Site;
        //        if (_site != null)
        //        {
        //            if (!String.IsNullOrEmpty(_measurementDevice.Site.Coordinate))
        //            {
        //                //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
        //                _coords += Common.ConstantsEntitiesName.PA.MeasurementDevice + "|" + _measurementDevice.IdMeasurementDevice.ToString() + "|" + _measurementDevice.Site.Coordinate.Replace(";", "|").Substring(0, _measurementDevice.Site.Coordinate.Length - 1) + ";";
        //            }
        //        }
        //    }
        //    return _coords;
        //}
        #endregion

        #region Page Event
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
                catch (System.Exception ex)
                {
                    //Mostrar en el Status Bar....
                    base.StatusBar.ShowMessage(ex);
                }
                //oculta el popup.
                this.mpelbDelete.Hide();
            }
            protected void btnFilter_Click(object sender, EventArgs e)
            {
                //Hace una recarga de los Facilities que correspondan al Activity...
                _IdActivity = Convert.ToInt64(GetKeyValue(_RdcAccountingActivity.SelectedValue, "IdActivity"));

                GetAllGeographicData();
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
                        NavigateEntity(_urlProperties, _EntityName, GetPageTitleForViewer(), _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiLanguage":
                        String _entityName = _EntityName.Replace("_LG", String.Empty) + "_LG";
                        base.NavigatorAddTransferVar("EntityName", _entityName);
                        base.NavigatorAddTransferVar("EntityNameGrid", _EntityName.Replace("_LG", String.Empty) + "_LG");
                        base.NavigatorAddTransferVar("IsFilterHierarchy", false);

                        //Se concatenan las PK con el Key = Values, si hay mas, el separador es el "&"
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
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

                NavigateEntity("~/Managers/ListManageAndView.aspx", _entityName, _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
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
        #endregion

    }
}
