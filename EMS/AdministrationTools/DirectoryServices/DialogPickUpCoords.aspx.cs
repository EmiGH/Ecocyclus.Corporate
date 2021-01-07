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
using System.IO;
using System.Linq;
using Condesus.EMS.WebUI.MasterControls;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Condesus.EMS.WebUI.DS
{
    public partial class DialogPickUpCoords : BasePage
    {
        #region Public Properties
            public String _PointsLatLong = String.Empty;
            public String InputPoints
            {
                get
                {
                    return inputPoints.Value;
                }
            }
            public String DrawModeType
            {
                get
                {
                    return drawModeType.Value;
                }
            }
        #endregion

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            InjectGoogleMapRegisterKey();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String _dataObjParams = String.Empty;
            if (Request.QueryString["ObjectQS"] != null)
            {
                _dataObjParams = Convert.ToString(Request.QueryString["ObjectQS"]);
            }
            //Si vienen datos, cargo los valores...
            if (!String.IsNullOrEmpty(_dataObjParams))
            {
                GetGeoDataObjects(_dataObjParams);
            }
            LoadTextLabels();

            //Form
            base.SetContentTableRowsCss(tblContentForm); 
        }

        #region Private Methods
            private void GetGeoDataObjects(String dataParams)
            {
                String _coordinates = String.Empty;
                String _objectName = dataParams.Split('|')[0];          //1° campo nombre de la entidad.
                String _objectParamIds = dataParams.Split('|')[1];      //2° campo los Ids de la entidad (formato key=value. ej.IdOrganization=2)
                Int64 _idGeographicArea = 0;
                Int64 _idOrganization = 0;
                Int64 _idFacility = 0;
                Int64 _idSector = 0;
                Int64 _idPerson = 0;
                Int64 _idAddress = 0;
                Int64 _idProcess=0;

                switch (_objectName)
                {
                    case Common.ConstantsEntitiesName.PF.Process:
                        _idProcess = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdProcess"));
                        _coordinates =((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Coordinate.Replace(";", "|");
                        break;

                    case Common.ConstantsEntitiesName.DS.GeographicArea:
                        _idGeographicArea = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdGeographicArea"));
                        _coordinates = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea).Coordinate.Replace(";", "|");
                        break;

                    case Common.ConstantsEntitiesName.DS.Facility:
                        _idFacility = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdFacility"));
                        _idOrganization = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdOrganization"));
                        _coordinates = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facility(_idFacility).Coordinate.Replace(";", "|");
                        break;

                    case Common.ConstantsEntitiesName.DS.Sector:
                        _idFacility = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdFacility"));
                        _idOrganization = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdOrganization"));
                        _idSector = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdSector"));
                        _coordinates = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facility(_idFacility).Sector(_idSector).Coordinate.Replace(";", "|");
                        break;

                    case Common.ConstantsEntitiesName.DS.Address:
                        _idAddress = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdAddress"));
                        _idFacility = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdFacility"));
                        _idOrganization = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdOrganization"));
                        _idSector = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdSector"));
                        _idPerson = Convert.ToInt64(GetKeyValue(_objectParamIds, "IdPerson"));
                        if (_idPerson > 0)
                        {
                            _coordinates = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Person(_idPerson).Address(_idAddress).Coordinate.Replace(";", "|");
                        }
                        else
                        {
                            if (_idSector > 0)
                            {
                                _coordinates = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facility(_idFacility).Sector(_idSector).Address(_idAddress).Coordinate.Replace(";", "|");
                            }
                            else
                            {
                                _coordinates = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facility(_idFacility).Address(_idAddress).Coordinate.Replace(";", "|");
                            }
                        } 
                        break;
                }
                //Si hay coordenadas, entonces las cargo.
                if (!String.IsNullOrEmpty(_coordinates))
                {
                    //se agregan los 2 primeros campos con el nombre de la entidad y un id ficticio, porque no se usa!!, despues van los puntos.
                    _PointsLatLong = _objectName + "|0|" + _coordinates + ";";
                }

            }

            private void LoadTextLabels()
            {
                lblSearchTitle.Text = Resources.CommonListManage.SearchLocationTitle;
                lblDrawMode.Text = Resources.CommonListManage.DrawModeType;
                lblPoint.Text = Resources.CommonListManage.Point;
                lblPolygon.Text = Resources.CommonListManage.Polygon;
                lblPolyline.Text = Resources.CommonListManage.Polyline;
                Page.Title = Resources.CommonListManage.PickUpCoordsTitle;
                btnClearMap.Text = Resources.CommonListManage.ClearMap;
                btnDeleteLastPoint.Text = Resources.CommonListManage.DeleteLastPoint;
                //btnSearch.Text = Resources.CommonListManage.Search;
                //btnCancel.Text = Resources.Common.btnCancel;
                //btnConfirm.Text = Resources.Common.btnOk;

                btnClearMap.ToolTip = Resources.CommonListManage.ClearMap;
                btnDeleteLastPoint.ToolTip = Resources.CommonListManage.DeleteLastPoint;
                btnSearch.ToolTip = Resources.CommonListManage.Search;
                btnCancel.ToolTip = Resources.Common.btnCancel;
                btnConfirm.ToolTip = Resources.Common.btnOk;
            }
        #endregion


    }
}
