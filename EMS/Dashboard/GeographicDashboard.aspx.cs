using System;
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

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class GeographicDashboard : BasePage
    {
        #region Internal Properties
            public String _PointsLatLong = String.Empty;
            public String _MapType = "virtualearth";
            private String _PageTitleLocal = String.Empty;
            private String _LayerView
            {
                get
                {
                    return base.NavigatorContainsTransferVar("LayerView") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("LayerView")) : "Country";
                }
            }
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private String _NavigateType
            {
                get
                {
                    return base.NavigatorContainsTransferVar("NavigateType") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("NavigateType")) : "FORWARD";
                }
            }
            private String _OwnerNameSearch
            {
                get
                {
                    object _o = Session["OwnerNameSearch"];
                    if (_o != null)
                        return (String)Session["OwnerNameSearch"];

                    return String.Empty;
                }
                set
                {
                    Session["OwnerNameSearch"] = value;
                }
            }
        #endregion

        #region Load & Init
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                InjectGoogleMapRegisterKey();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                btnSearchName.Click += new EventHandler(btnSearchName_Click);
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                LoadTextLabels();

                GetAllGeographicData();

                _MapType = ConfigurationManager.AppSettings["MAP_Type_Vendor"];
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                _PageTitleLocal = Resources.Common.Dashboard;
                base.PageTitle = Resources.Common.Dashboard;
            }
            //Setea el Sub Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.GeographicDashboard + " " + Resources.ConstantMessage.LeyendGeographicDashboard;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                lblTitleSearch.Text = Resources.Common.TitlePopSearch;
                lblSubTitleSearch.Text = Resources.Common.SearchbyName; // Resources.CommonListManage.SearchLocationTitle;
                btnSearchName.ToolTip = Resources.CommonListManage.Search;
                btnShowSearch.Text = Resources.CommonListManage.Search;
                btnClose.Text = Resources.Common.btnClosePopup;
                btnSearchName.Text = Resources.Common.btnSearchApply;

                //btnSearch.ToolTip = Resources.CommonListManage.Search;
                //btnSearch.Text = Resources.Common.btnSearchApply;

                lblSubTitleFilter.Text = Resources.CommonListManage.FilterTitlePopUpProcess;
                btnFilter.Text = Resources.CommonListManage.Filter;
                btnShowFilter.Text = Resources.CommonListManage.Filter;
            }
            private void GetAllGeographicData()
            {
                //Solo se muestran los Process
                //_PointsLatLong += GetCoordinatesForMeasurementDevices();
                //_PointsLatLong += GetCoordinatesForFacilities();
                _PointsLatLong += GetCoordinatesForProcesses();
            }
            private String GetCoordinatesForProcesses()
            {
                String _coords = String.Empty;

                if (!String.IsNullOrEmpty(_OwnerNameSearch))
                {   //Hay filtro, entonces busca el nombre y muestra todo lo que resulta!
                    foreach (ProcessGroupProcess _processGroupProcess in EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcessesByOrganizationName(_OwnerNameSearch).Values)
                    {
                        //Solo necesito el Punto Geografico que tiene el process!!!
                        if (!String.IsNullOrEmpty(_processGroupProcess.Coordinate))
                        {
                            String _iconMap = GetIconName(_processGroupProcess);
                            //Arma el string con el formato: NombreOrganization|ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                            _coords += _processGroupProcess.Organization.Name + "|" + Common.ConstantsEntitiesName.PF.ProcessGroupProcess + "|" + _iconMap + "|" + _processGroupProcess.IdProcess.ToString() + "|" + _processGroupProcess.Coordinate.Replace(";", "|").Substring(0, _processGroupProcess.Coordinate.Length - 1) + ";";
                        }
                    }
                }
                else
                {
                    #region Arma Puntos Gegroaficos por Layer (Standar)
                    //EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcessesByLayerAndType(
                    Int64 _idGeoArea = 0;
                    if (_IdProcess > 0)
                    {
                        ProcessGroupProcess _processGroupProcessSelected = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                        if (_processGroupProcessSelected.GeographicArea != null)
                        {
                            if (_NavigateType == "FORWARD")
                            {
                                _idGeoArea = _processGroupProcessSelected.GeographicArea.IdGeographicArea;
                            }
                            else
                            {
                                _idGeoArea = _processGroupProcessSelected.GeographicArea.IdParentGeographicArea;
                            }
                        }
                    }

                    var _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                      select _processes;

                    if (_LayerView == "Country")
                    {
                        _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                      where (_processes.GeographicArea != null) && (_processes.GeographicArea.Layer == "Country")
                                      select _processes;

                        //Si no hay nada a nivel PAIS, tengo que ir por las Provincias...
                        if (_lnqProcess.Count() == 0)
                        {
                            _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                          where (_processes.GeographicArea != null) && (_processes.GeographicArea.Layer == "Province")
                                          && (_processes.GeographicArea.IdParentGeographicArea == _idGeoArea)
                                          select _processes;

                            //Si no hay Provincias, tengo que ir por Municipios
                            if (_lnqProcess.Count() == 0)
                            {
                                _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                              where (_processes.GeographicArea != null) && (_processes.GeographicArea.Layer == "Municipality")
                                                && (_processes.GeographicArea.IdParentGeographicArea == _idGeoArea)
                                              select _processes;

                                //Si no hay Municipios, traigo todo sin layer!
                                if (_lnqProcess.Count() == 0)
                                {
                                    _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                                  select _processes;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (_LayerView == "Province")
                        {
                            _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                          where (_processes.GeographicArea != null) && (_processes.GeographicArea.Layer == "Province")
                                                && (_processes.GeographicArea.IdParentGeographicArea == _idGeoArea)
                                          select _processes;

                            //Si no hay Provincias tengo que ir por Municipioas
                            if (_lnqProcess.Count() == 0)
                            {
                                _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                              where (_processes.GeographicArea != null) && (_processes.GeographicArea.Layer == "Municipality")
                                                    && (_processes.GeographicArea.IdParentGeographicArea == _idGeoArea)
                                              select _processes;
                            }
                        }
                        else
                        {
                            if (_LayerView == "Municipality")
                            {
                                _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                              where (_processes.GeographicArea != null) && (_processes.GeographicArea.Layer == "Municipality") && (_processes.IdOrganization == _processes.GeographicArea.IdOrganization)
                                                    && (_processes.GeographicArea.IdParentGeographicArea == _idGeoArea)
                                              select _processes;
                            }
                            else
                            {
                                if (_LayerView == "Organization")
                                {
                                    _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                                  where (_processes.GeographicArea != null) && (_processes.GeographicArea.Layer == "Municipality") && (_processes.IdOrganization != _processes.GeographicArea.IdOrganization)
                                                        && (_processes.GeographicArea.IdParentGeographicArea == _idGeoArea)
                                                  select _processes;
                                }
                                else
                                {
                                    if ((_IdProcess > 0) && (_LayerView == "ProcessAsocitatedToFacility"))
                                    {
                                        _lnqProcess = from _processes in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values
                                                  where (_processes.IdProcess == _IdProcess)
                                                  select _processes;
                                    }
                                }
                            }
                        }
                    }

                    foreach (ProcessGroupProcess _processGroupProcess in _lnqProcess)
                    {
                        //Solo necesito el Punto Geografico que tiene el process!!!
                        if (!String.IsNullOrEmpty(_processGroupProcess.Coordinate))
                        {
                            String _iconMap = GetIconName(_processGroupProcess);
                            //Arma el string con el formato: NombreOrganization|ObjectName|idObject|Lat|Long;  (si tiene muchos puntos se siguen repitiendo en ese orden separandolas con el |)
                            _coords += _processGroupProcess.Organization.Name + "|" + Common.ConstantsEntitiesName.PF.ProcessGroupProcess + "|" + _iconMap + "|" + _processGroupProcess.IdProcess.ToString() + "|" + _processGroupProcess.Coordinate.Replace(";", "|").Substring(0, _processGroupProcess.Coordinate.Length - 1) + ";";
                        }
                    }
                    #endregion
                }

                return _coords;
            }
            private String GetIconName(ProcessGroupProcess process)
            {
                String _iconName = "Process";

                Int16 _idProcessClass_Territorial = 1;
                Int16 _idProcessClass_Sectorial = 2;
                Int16 _idProcessClass_Organizational = 3;
                Int16 _idProcessClass_Individual = 4;
                Int16 _idProcessClass_Productive = 5;
                Int16.TryParse(ConfigurationManager.AppSettings["IdProcessClass_Territorial"], out _idProcessClass_Territorial);
                Int16.TryParse(ConfigurationManager.AppSettings["IdProcessClass_Sectorial"], out _idProcessClass_Sectorial);
                Int16.TryParse(ConfigurationManager.AppSettings["IdProcessClass_Organizational"], out _idProcessClass_Organizational);
                Int16.TryParse(ConfigurationManager.AppSettings["IdProcessClass_Individual"], out _idProcessClass_Individual);
                Int16.TryParse(ConfigurationManager.AppSettings["IdProcessClass_Productive"], out _idProcessClass_Productive);


                if (process.Classifications.Count > 0)
                {
                    ProcessClassification _processClass = process.Classifications.First().Value;

                    if (_processClass.IdProcessClassification == _idProcessClass_Territorial)
                    { _iconName = "ProcessTerritorial"; }
                    else
                    {
                        if (_processClass.IdProcessClassification == _idProcessClass_Sectorial)
                        { _iconName = "ProcessSectorial"; }
                        else
                        {
                            if (_processClass.IdProcessClassification == _idProcessClass_Organizational)
                            { _iconName = "ProcessOrganizacional"; }
                            else
                            {
                                if (_processClass.IdProcessClassification == _idProcessClass_Individual)
                                { _iconName = "ProcessPersonal"; }
                                else
                                {
                                    if (_processClass.IdProcessClassification == _idProcessClass_Productive)
                                    { _iconName = "ProcessProductive"; }
                                    else
                                    { _iconName = "Process"; }
                                }
                            }
                        }
                    }
                }

                return _iconName;
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

        #region Events
            protected void btnSearchName_Click(object sender, EventArgs e)
            {
                _OwnerNameSearch = ownerName.Value;
                Response.Redirect(Request.RawUrl);
            }
        #endregion

    }
}
