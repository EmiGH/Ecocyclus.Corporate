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
    public partial class GeographicDashboardMonitoring : BaseProperties
    {
        #region Internal Properties
            public String _TwitterUser = String.Empty;
            public String _PointsLatLong = String.Empty;
            public String _PointLatLongProcess = String.Empty;
            public String _MapType = "virtualearth";
            private String _PageTitleLocal = String.Empty;
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

                LoadTextLabels();
                LoadParameters();

                if (!Page.IsPostBack)
                {
                    Dictionary<Int64, ProcessGroupProcess> _processesAsociated = new Dictionary<Int64, ProcessGroupProcess>();

                    GetAllGeographicData(ref _processesAsociated);
                    LoadAllProcessForFilter(_processesAsociated);
                }

                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
            }
            protected override void SetPagetitle()
            {
                _PageTitleLocal = Resources.Common.Dashboard;
                base.PageTitle = Resources.Common.Dashboard;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.GeographicDashboard + " " + Resources.ConstantMessage.LeyendGeographicDashboard;
            }
        #endregion

        #region Private Methods
            private void LoadAllProcessForFilter(Dictionary<Int64, ProcessGroupProcess> processesAsociated)
            {

                //foreach (ProcessGroupProcess _processGroupProcess in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values)

                foreach (ProcessGroupProcess _processGroupProcess in processesAsociated.Values)
                {
                    CheckBox _chkProcess = new CheckBox();
                    _chkProcess.Text = _processGroupProcess.LanguageOption.Title;
                    _chkProcess.Checked = true;
                    _chkProcess.CssClass = "Check";
                    _chkProcess.ID = "chkProcess_" + _processGroupProcess.IdProcess.ToString();
                    _chkProcess.Attributes.Add("IdProcess", _processGroupProcess.IdProcess.ToString());

                    //Se agrega el control al panel...
                    divFilter.Controls.Add(_chkProcess);
                }
            }
            private void LoadTextLabels()
            {
                btnCloseFilter.Text = Resources.Common.btnClosePopup;
                btnFilter.Text = Resources.Common.btnSearchApply;
                lblSubTitleFilter.Text = Resources.CommonListManage.FilterTitlePopUpProcess;
                btnFilter.Text = Resources.CommonListManage.Filter;
                btnShowFilter.Text = Resources.CommonListManage.Filter;
            }
            private void GetAllGeographicData(ref Dictionary<Int64, ProcessGroupProcess> processesAsociated)
            {
                //Se muestran todos los Facilities del Process seleccionado
                _PointsLatLong += GetCoordinatesFacilitiesByProcess(ref processesAsociated);
            }
            private String GetCoordinatesFacilitiesByProcess(ref Dictionary<Int64, ProcessGroupProcess> processesAsociated)
            {
                String _coords = String.Empty;

                foreach (Facility _facility in EMSLibrary.User.Dashboard.Facilities.Values)
                {
                    String _facilityType = GetIconNameFacilityType(_facility.IdFacilityType);
                    _facilityType = _facilityType.Replace("Red", String.Empty) + "Blue";

                    String _processAsociated = String.Empty;
                    foreach (ProcessGroupProcess _processGroupProcess in _facility.ProcessesAssociated.Values)
                    {
                        _processAsociated += _processGroupProcess.IdProcess + ",";
                        //Nos guardamos el process que esta relacionado, para poder agregarlo a los filtros....asi solo filtramos por los process que tienen algo!!
                        if (!processesAsociated.ContainsKey(_processGroupProcess.IdProcess))
                        {   //Como todavia no fue agregado, lo metemos en el dictionary.
                            processesAsociated.Add(_processGroupProcess.IdProcess, _processGroupProcess);
                        }
                    }
                    if (!String.IsNullOrEmpty(_processAsociated))
                    {   //Si no hay coordenada, no sirve, no lo ponemos!
                        if (!String.IsNullOrEmpty(_facility.Coordinate))
                        {
                            _processAsociated = _processAsociated.Substring(0, _processAsociated.Length - 1);
                            //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                            _coords += _processAsociated + "|" + Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _facility.IdFacility.ToString() + "|" + _facility.Coordinate.Replace(";", "|").Substring(0, _facility.Coordinate.Length - 1) + ";";
                        }
                    }
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
            private void LoadGRCByEntity()
            {
                //ManageEntityParams.Concat(GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost")));
                if (BuildContextInfoModuleMenu("DashboardMonitoring", ManageEntityParams))
                {
                    base.BuildContextInfoShowMenuButton();
                }
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
            }
        #endregion

        #region Page Event
            protected void btnFilter_Click(object sender, EventArgs e)
            {
                Dictionary<Int64, ProcessGroupProcess> _processesAsociated = new Dictionary<Int64, ProcessGroupProcess>();

                GetAllGeographicData(ref _processesAsociated);
                LoadAllProcessForFilter(_processesAsociated);
            }
        #endregion

    }
}
