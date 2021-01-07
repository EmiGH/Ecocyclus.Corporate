using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Microsoft.Live.ServerControls.VE;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.Managers
{
    public partial class GeographicManagerFacilities : BasePage
    {
        #region Internal Properties
            private String _EntityName = "Facility";
            public String _PointsLatLong = String.Empty;
            public String _MapType = "virtualearth";
            public String IdOrganization = String.Empty;
            //private Int64 _IdProcess
            //{
            //    get
            //    {
            //        return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
            //    }
            //}
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
            }
            private String _EntityNameGRC = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            //private DataTable _FacilityTypes
            //{
            //    get
            //    {
            //        object _o = Session["FacilityTypes"];
            //        if (_o != null)
            //            return (DataTable)_o;

            //        return new DataTable();
            //    }

            //    set { Session["FacilityTypes"] = value; }
            //}
            public String _FacilityTypes = String.Empty;

        #endregion

        #region Load & Init
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                InjectGoogleMapRegisterKey();
                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);

                //Carga el menu de opciones generales.
                LoadGeneralOptionMenu();

                BuildFilterFacilityTypes();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                LoadTextLabels();
                LoadParameters();

                if (!Page.IsPostBack)
                {
                    //BuildDTFacilityTypesSelected();
                    LoadFacilityTypeSelected();

                    GetAllGeographicData();
                }

                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
                //Carga el Menu de Context ElementMaps a la derecha.
                LoadContextElementMapsByEntity();


                //btnFilter.Click += new EventHandler(btnFilter_Click);
            }

            protected void btnFilter_Click(object sender, EventArgs e)
            {
                LoadFacilityTypeSelected();

                //Inicializo los puntos
                _PointsLatLong = String.Empty;
                //Vuelvo a cargar con el filtro...
                GetAllGeographicData();

                //Response.Write("<script>InitMap();</script>");
                //Response.Write("<script lengue>InitMap()</script>");

                //"<script type='text/javascript'>Funcion();</script>"; 
                //Oculta el popup.
                this.mpeFilter.Hide();
            }
            protected override void SetPagetitle()
            {
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                if (_organization != null)
                {
                    base.PageTitle = _organization.Name;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.LeyendGeographicManageFacility;
            }
        #endregion

        #region Event
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

                        String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                        NavigateEntity(_urlProperties, _EntityName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    default:
                        break;
                }
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

                lblTitleFilter.Text = Resources.Common.TitlePopFilter;
                btnCloseFilter.Text = Resources.Common.btnClosePopup;
                btnFilter.Text = Resources.Common.btnSearchApply;
                lblSubTitleFilter.Text = Resources.CommonListManage.FilterTitlePopUp;
                btnFilter.Text = Resources.CommonListManage.Filter;
                btnShowFilter.Text = Resources.CommonListManage.Filter;
            }
            //private DataTable BuildDTFacilityTypesSelected()
            //{
            //    _FacilityTypes = new DataTable();
            //    //Arma los Campos del Dt
            //    DataColumn _dtColum = new DataColumn();
            //    _dtColum.ColumnName = "IdFacilityType";
            //    _dtColum.AllowDBNull = false;
            //    _dtColum.DataType = System.Type.GetType("System.Int64");
            //    _dtColum.Unique = true;
            //    _FacilityTypes.Columns.Add(_dtColum);


            //    return _FacilityTypes;
            //}
            //private void LoadFacilityTypeSelected()
            //{
            //    _FacilityTypes.Rows.Clear();

            //    foreach (FacilityType _facilityType in EMSLibrary.User.GeographicInformationSystem.FacilityTypes().Values)
            //    {

            //        CheckBox _chk = (CheckBox)phFilter.FindControl("chkFacilityType" + _facilityType.IdFacilityType.ToString());
            //        if (_chk != null)
            //        {
            //            if (_chk.Checked)
            //            {
            //                _FacilityTypes.Rows.Add(_facilityType.IdFacilityType);
            //            }
            //        }
            //    }
            //}
            private void LoadFacilityTypeSelected()
            {
                _FacilityTypes = String.Empty;

                foreach (FacilityType _facilityType in EMSLibrary.User.GeographicInformationSystem.FacilityTypes().Values)
                {
                    CheckBox _chk = (CheckBox)phFilter.FindControl("chkFacilityType" + _facilityType.IdFacilityType.ToString());
                    if (_chk != null)
                    {
                        if (_chk.Checked)
                        {
                            Int64 _idFacilityType = _facilityType.IdFacilityType;
                            _FacilityTypes += _idFacilityType.ToString() + "|" + GetIconNameFacilityType(_idFacilityType) + ";";
                        }
                    }
                }
            }
            private void BuildFilterFacilityTypes()
            {
                var _lnqFacilityTypes = from facilityType in EMSLibrary.User.GeographicInformationSystem.FacilityTypes().Values
                                        orderby facilityType.LanguageOption.Name ascending
                                        select facilityType;
                //Trae los indicadores que no tienen clasificacion
                foreach (FacilityType _facilityType in _lnqFacilityTypes)
                {
                    CheckBox _chk = new CheckBox();
                    _chk.Text = _facilityType.LanguageOption.Name;
                    _chk.Checked = true;
                    _chk.ID = "chkFacilityType" + _facilityType.IdFacilityType;
                    _chk.CssClass = "Check";

                    divFilter.Controls.Add(_chk);
                    //phFilter.Controls.Add(_chk);
                }
            }
            private void GetAllGeographicData()
            {
                //Se muestran todos los Facilities del Process seleccionado
                //_PointsLatLong += GetCoordinatesFacilitiesByProcess();
                _PointsLatLong += GetCoordinatesFacilitiesByOrganization();
            }
            //private String GetCoordinatesFacilitiesByProcess()
            //{
            //    String _coords = String.Empty;
            //    Dictionary<Int64, Site> _sites = new Dictionary<Int64, Site>();

            //    //Tengo que obtener todos las tareas de un process, y de ahi sacar sus facilities.
            //    foreach (ProcessTask _processTask in ((ProcessGroup)EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess)).ChildrenTask.Values)
            //    {
            //        Site _site = _processTask.Site;

            //        if (_site != null)
            //        {
            //            //Si ya fue cargado no lo vuelve a meter, para no duplicar puntos en el mapa.
            //            if (!_sites.ContainsKey(_site.IdFacility))
            //            {
            //                //Agrega el site, para verificar si ya existe...
            //                _sites.Add(_site.IdFacility, _site);

            //                if (!String.IsNullOrEmpty(_site.Coordinate))
            //                {
            //                    String _facilityType = GetIconNameFacilityType(((Facility)_site).FacilityType.IdFacilityType);
            //                    //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
            //                    _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _site.IdFacility.ToString() + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
            //                }
            //            }
            //        }

            //    }
                

            //    return _coords;
            //}
            private String GetCoordinatesFacilitiesByOrganization()
            {
                String _coords = String.Empty;
                Dictionary<Int64, Site> _sites = new Dictionary<Int64, Site>();

                
                //Tengo que obtener todos las tareas de un process, y de ahi sacar sus facilities.
                foreach (Facility _facility in EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facilities.Values)
                {
                    if (!String.IsNullOrEmpty(_facility.Coordinate))
                    {
                        //DataRow[] _dr = _FacilityTypes.Select("IdFacilityType=" + _facility.FacilityType.IdFacilityType.ToString());
                        ////Si el type esta en el DT lo carga como punto en el mapa.
                        //if (_dr.Count() > 0)
                        //{
                            String _facilityType = GetIconNameFacilityType(_facility.FacilityType.IdFacilityType);
                            //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                            _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _facility.IdFacility.ToString() + "|" + _facility.Coordinate.Replace(";", "|").Substring(0, _facility.Coordinate.Length - 1) + ";";
                        //}
                    }
                }
                    //Site _site = _processTask.Site;

                    //if (_site != null)
                    //{
                    //    //Si ya fue cargado no lo vuelve a meter, para no duplicar puntos en el mapa.
                    //    if (!_sites.ContainsKey(_site.IdFacility))
                    //    {
                    //        //Agrega el site, para verificar si ya existe...
                    //        _sites.Add(_site.IdFacility, _site);

                    //        if (!String.IsNullOrEmpty(_site.Coordinate))
                    //        {
                    //            String _facilityType = GetIconNameFacilityType(((Facility)_site).FacilityType.IdFacilityType);
                    //            //Arma el string con el formato: ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                    //            _coords += Common.ConstantsEntitiesName.DS.Facility + "|" + _facilityType + "|" + _site.IdFacility.ToString() + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
                    //        }
                    //    }

                return _coords;
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


                //String _iconName = String.Empty;
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
            private void LoadGeneralOptionMenu()
            {
                //Para las ejecuciones, no es necesario tener meno de general options....
                //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity(_EntityName + "_MenuOption", ManageEntityParams, false));
                _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(rmnuGeneralOption_ItemClick);
                RadMenuItem _rmi = _rmnuGeneralOption.Items.FindItemByValue("rmiDelete");
                if (_rmi != null)
                {
                    _rmi.Visible = false;
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

                IdOrganization = _IdOrganization.ToString();
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
            private void LoadContextElementMapsByEntity()
            {
                if (BuildContextElementMapsModuleMenu("FacilityType", ManageEntityParams))
                {
                    base.BuildContextElementMapsShowMenuButton();
                }
            }

        #endregion

    }
}
