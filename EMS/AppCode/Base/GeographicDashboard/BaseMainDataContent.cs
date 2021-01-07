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
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
        #endregion

        #region Internal Methods
            private void SetAttributesToLinkButton(ref LinkButton lbtnValue, String pkCompost, String entityName, String entityNameGrid,
                String entityNameContextInfo, String entityNameContextElement)
            {
                lbtnValue.Attributes.Add("PkCompost", pkCompost);
                lbtnValue.Attributes.Add("EntityName", entityName);
                lbtnValue.Attributes.Add("EntityNameGrid", entityNameGrid);
                lbtnValue.Attributes.Add("EntityNameContextInfo", entityNameContextInfo);
                lbtnValue.Attributes.Add("EntityNameContextElement", entityNameContextElement);
                lbtnValue.Attributes.Add("onclick", "javascript:NavigateToContent(this, event);");
            }
            //private TableRow BuildLabelTitle(TableRow currentRow, TableCell tblCell, String text)
            //{
            //    Label _lblTitle = new Label();
            //    //_lblTitle.ID = "lblTitle" + text;
            //    _lblTitle.Text = text;
            //    tblCell.CssClass = "Title";
            //    tblCell.VerticalAlign = VerticalAlign.Top;
            //    tblCell.Controls.Add(_lblTitle);
            //    currentRow.Cells.Add(tblCell);

            //    return currentRow;
            //}
            private Label BuildLabelValue(String text, String className)
            {
                Label _lblValue = new Label();
                _lblValue.Text = text;
                _lblValue.CssClass = className;

                return _lblValue;
            }
            //private TableRow BuildLabelValue(TableRow currentRow, TableCell tblCell, String text)
            //{
            //    Label _lblValue = new Label();
            //    _lblValue.Text = text;
            //    tblCell.CssClass = "Text";
            //    tblCell.VerticalAlign = VerticalAlign.Top;
            //    tblCell.Controls.Add(_lblValue);
            //    currentRow.Cells.Add(tblCell);

            //    return currentRow;
            //}
            //private TableRow BuildLinkButtonValue(TableRow currentRow, TableCell tblCell, String text, String pkCompost, String entityName, String entityNameGrid,
            //    String entityNameContextInfo, String entityNameContextElement)
            //{
            //    LinkButton _lbtnValue = new LinkButton();
            //    //_lblTitle.ID = "lblValue" + text;
            //    SetAttributesToLinkButton(ref _lbtnValue, pkCompost, entityName, entityNameGrid, entityNameContextInfo, entityNameContextElement);
            //    _lbtnValue.Text = text;

            //    tblCell.CssClass = "Text";
            //    tblCell.VerticalAlign = VerticalAlign.Top;
            //    tblCell.Controls.Add(_lbtnValue);
            //    currentRow.Cells.Add(tblCell);

            //    return currentRow;
            //}
            private LinkButton BuildLinkButtonValue(String text, String pkCompost, String entityName, String entityNameGrid,
                    String entityNameContextInfo, String entityNameContextElement, String className)
            {
                LinkButton _lbtnValue = new LinkButton();
                SetAttributesToLinkButton(ref _lbtnValue, pkCompost, entityName, entityNameGrid, entityNameContextInfo, entityNameContextElement);
                _lbtnValue.Text = text;
                _lbtnValue.CssClass = className;

                return _lbtnValue;
            }
            //private TableRow BuildRow(String title, String value, String pkCompost, String entityName, String entityNameGrid,
            //    String entityNameContextInfo, String entityNameContextElement)
            //{
            //    //Comienza con los registros de Informacion Principal
            //    //1° Registro
            //    TableRow _currentRow = new TableRow();
            //    TableCell _tblCell = new TableCell();
            //    //Titulo
            //    _currentRow = BuildLabelTitle(_currentRow, _tblCell, title);
            //    //Valor
            //    _tblCell = new TableCell();
            //    _currentRow = BuildLinkButtonValue(_currentRow, _tblCell, value, pkCompost, entityName, entityNameGrid, entityNameContextInfo, entityNameContextElement);

            //    return _currentRow;
            //}
            //private TableRow BuildRow(String title, String value)
            //{
            //    //Comienza con los registros de Informacion Principal
            //    //1° Registro
            //    TableRow _currentRow = new TableRow();
            //    TableCell _tblCell = new TableCell();
            //    //Titulo
            //    _currentRow = BuildLabelTitle(_currentRow, _tblCell, title);
            //    //Valor
            //    _tblCell = new TableCell();
            //    _currentRow = BuildLabelValue(_currentRow, _tblCell, value);

            //    return _currentRow;
            //}
            //private Table BuildProcessMainData(Int64 idEntity, ref String title)
            //{
            //    //Definiciones
            //    //La tabla
            //    Table _tblContentRelatedData = new Table();
            //    String pkCompost = String.Empty;
            //    _tblContentRelatedData.CellPadding = 0;
            //    _tblContentRelatedData.CellSpacing = 0;
            //    _tblContentRelatedData.CssClass = "contentGeographicDashboardtable";

            //    //Construye el Objeto principal
            //    ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(idEntity);

            //    //Setea el titulo para retornar.
            //    title = _processGroupProcess.LanguageOption.Title;

            //    //1° Registro Nombre del proceso
            //    pkCompost = "IdProcess=" + _processGroupProcess.IdProcess.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Process, _processGroupProcess.LanguageOption.Title, pkCompost, Common.ConstantsEntitiesName.PF.ProcessGroupProcess,
            //        Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess));

            //    //2° Registro Descripcion
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Description, _processGroupProcess.LanguageOption.Description));

            //    //3° Registro Purpose
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Purpose, _processGroupProcess.LanguageOption.Purpose));

            //    //4° Registro Current Campaing
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.CurrentCampaignStartDate, _processGroupProcess.CurrentCampaignStartDate.ToShortDateString() + " - " + _processGroupProcess.EndDate.ToShortDateString()));

            //    //5° Registro un linkbutton para llegar a los facilities
            //    pkCompost = "IdProcess=" + _processGroupProcess.IdProcess.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(String.Empty, Resources.CommonListManage.Facilities, pkCompost, Common.ConstantsEntitiesName.DS.FacilitiesByProcess,
            //        Common.ConstantsEntitiesName.DS.FacilitiesByProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess));

            //    //Finalmente retorna la tabla
            //    return _tblContentRelatedData;
            //}
            private String GetIconName(ProcessGroupProcess processClass)
            {
                String _iconName = "pnlIconProcess";

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

                if (processClass.Classifications.Count > 0)
                {
                    ProcessClassification _processClass = processClass.Classifications.First().Value;

                    if (_processClass.IdProcessClassification == _idProcessClass_Territorial)
                    { _iconName = "pnlIconProcessTerritorial"; }
                    else
                    {
                        if (_processClass.IdProcessClassification == _idProcessClass_Sectorial)
                        { _iconName = "pnlIconProcessSectorial"; }
                        else
                        {
                            if (_processClass.IdProcessClassification == _idProcessClass_Organizational)
                            { _iconName = "pnlIconProcessOrganizacional"; }
                            else
                            {
                                if (_processClass.IdProcessClassification == _idProcessClass_Individual)
                                { _iconName = "pnlIconProcessPersonal"; }
                                else
                                {
                                    if (_processClass.IdProcessClassification == _idProcessClass_Productive)
                                    { _iconName = "pnlIconProcessProductive"; }
                                    else
                                    { _iconName = "pnlIconProcess"; }
                                }
                            }
                        }
                    }
                }

                return _iconName;
            }
            private PlaceHolder BuildProcessMainData(Int64 idEntity, ref String title, ref String classNameIcon, ref Dictionary<Int64, CatalogDoc> catalogDoc)
            {
                //Definiciones
                PlaceHolder _phContentRelatedData = new PlaceHolder();
                String pkCompost = String.Empty;

                //Construye el Objeto principal
                ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(idEntity);
                //Setea el dictionary de pictures para devolverlo
                catalogDoc = _processGroupProcess.Pictures;
                //Setea el nombre del class para el icono a mostrar!. En este caso solo Process, siempre el mismo.
                classNameIcon = GetIconName(_processGroupProcess);      // "pnlIconProcess";

                //Setea el titulo para retornar.
                title = _processGroupProcess.LanguageOption.Title;

                //1° Registro Nombre del proceso
                pkCompost = "IdProcess=" + _processGroupProcess.IdProcess.ToString();
                _phContentRelatedData.Controls.Add(BuildLinkButtonValue(_processGroupProcess.LanguageOption.Title, pkCompost, Common.ConstantsEntitiesName.DS.FacilitiesByProcess,
                    Common.ConstantsEntitiesName.DS.FacilitiesByProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, "lblProcessGroupProcess"));
                
                //2° Registro Descripcion
                _phContentRelatedData.Controls.Add(BuildLabelValue(_processGroupProcess.LanguageOption.Description, "lblDescription"));

                //3° Registro Purpose
                _phContentRelatedData.Controls.Add(BuildLabelValue(_processGroupProcess.LanguageOption.Purpose, "lblPurpose"));

                //4° Registro Current Campaing
                _phContentRelatedData.Controls.Add(BuildLabelValue(Resources.CommonListManage.CurrentCampaignStartDate, "lblCurrentCampaignStartDate"));
                _phContentRelatedData.Controls.Add(BuildLabelValue(_processGroupProcess.CurrentCampaignStartDate.ToShortDateString() + " - " + _processGroupProcess.EndDate.ToShortDateString(), "lblToShortDateString"));

                //5° Agrego un Link Button para acceder a los hijos (Si es Pais-Provincia-Municipio)
                if (_processGroupProcess.GeographicArea != null)
                {
                    String _layerName = String.Empty;
                    String _layerValue = String.Empty;
                    switch (_processGroupProcess.GeographicArea.Layer)
                    {
                        case "Country":
                            _layerName = Resources.CommonListManage.CommunityByProvincials;
                            _layerValue = "Province";
                            break;
                        case "Province":
                            _layerName = Resources.CommonListManage.CommunityByMunicipalities;
                            _layerValue = "Municipality";
                            break;
                        case "Municipality":
                            _layerName = Resources.CommonListManage.CommunityByOrganizations;
                            _layerValue = "Organization";
                            break;
                    }
                    LinkButton _lbtnValue = new LinkButton();
                    //SetAttributesToLinkButton(ref _lbtnValue, pkCompost, entityName, entityNameGrid, entityNameContextInfo, entityNameContextElement);
                    _lbtnValue.Attributes.Add("LayerView", _layerValue);
                    _lbtnValue.Attributes.Add("PkCompost", pkCompost);
                    _lbtnValue.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _lbtnValue.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                    _lbtnValue.Attributes.Add("EntityNameContextInfo", String.Empty);
                    _lbtnValue.Attributes.Add("EntityNameContextElement", String.Empty);
                    _lbtnValue.Attributes.Add("NavigateType", "FORWARD");
                    _lbtnValue.Attributes.Add("onclick", "javascript:NavigateToContentLayers(this, event);");

                    _lbtnValue.Text = _layerName;
                    _lbtnValue.CssClass = "lblProcessGroupProcess";

                    Boolean _showButtonLayer = true;
                    if (_processGroupProcess.GeographicArea.Layer == "Municipality")
                    {
                        if (_processGroupProcess.IdOrganization != _processGroupProcess.GeographicArea.IdOrganization)
                        {
                            //No debe mostrar el LinkButton
                            _showButtonLayer = false;
                        }
                    }
                    if (_showButtonLayer)
                    {
                        _phContentRelatedData.Controls.Add(_lbtnValue);
                    }
                }

                //6° Agrego un Link Button para acceder a los Padres (Si es Pais-Provincia-Municipio) (va para atras...)
                if (_processGroupProcess.GeographicArea != null)
                {
                    if (_processGroupProcess.GeographicArea.Layer != "Country")
                    {
                        String _layerName = String.Empty;
                        String _layerValue = String.Empty;
                        switch (_processGroupProcess.GeographicArea.Layer)
                        {
                            case "Province":
                                _layerName = Resources.CommonListManage.CommunityByCountriesBack;
                                _layerValue = "Country";
                                break;
                            case "Municipality":
                                _layerName = Resources.CommonListManage.CommunityByProvincialsBack;
                                _layerValue = "Province";
                                break;
                            case "Organization":
                                _layerName = Resources.CommonListManage.CommunityByMunicipalitiesBack;
                                _layerValue = "Municipality";
                                break;
                        }
                        LinkButton _lbtnValue = new LinkButton();
                        _lbtnValue.Attributes.Add("LayerView", _layerValue);
                        _lbtnValue.Attributes.Add("PkCompost", pkCompost);
                        _lbtnValue.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                        _lbtnValue.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                        _lbtnValue.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _lbtnValue.Attributes.Add("EntityNameContextElement", String.Empty);
                        _lbtnValue.Attributes.Add("NavigateType", "BACK"); 
                        _lbtnValue.Attributes.Add("onclick", "javascript:NavigateToContentLayers(this, event);");

                        _lbtnValue.Text = _layerName;
                        _lbtnValue.CssClass = "lblProcessGroupProcess";

                        _phContentRelatedData.Controls.Add(_lbtnValue);
                    }
                }


                //5° Registro un linkbutton para llegar a los facilities
                //pkCompost = "IdProcess=" + _processGroupProcess.IdProcess.ToString();
                //_phContentRelatedData.Controls.Add(BuildLinkButtonValue(Resources.CommonListManage.Facilities, pkCompost, Common.ConstantsEntitiesName.DS.FacilitiesByProcess,
                //    Common.ConstantsEntitiesName.DS.FacilitiesByProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, "lblFacilitiesByProcess"));

                //Finalmente retorna la tabla
                return _phContentRelatedData;
            }
            //private Table BuildFacilityMainData(Int64 idEntity, ref String title)
            //{
            //    //Definiciones
            //    //La tabla
            //    Table _tblContentRelatedData = new Table();
            //    String pkCompost = String.Empty;
            //    _tblContentRelatedData.CellPadding = 0;
            //    _tblContentRelatedData.CellSpacing = 0;
            //    _tblContentRelatedData.CssClass = "contentGeographicDashboardtable";

            //    //Construye el Objeto principal
            //    Site _facility = EMSLibrary.User.GeographicInformationSystem.Site(idEntity);

            //    //Setea el titulo para retornar.
            //    title = _facility.LanguageOption.Name;

            //    //1° Registro Nombre del facility
            //    //El separador de los pk, le pongo pipe |, porque sino trae problemas al redirect.
            //    pkCompost = "IdOrganization=" + _facility.Organization.IdOrganization.ToString() + "|IdFacility=" + _facility.IdFacility.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Facility, _facility.LanguageOption.Name, pkCompost, Common.ConstantsEntitiesName.DS.Facility,
            //        Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organization));

            //    //2° Registro Descripcion
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Description, _facility.LanguageOption.Description));

            //    //3° Registro Facility Type
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.FacilityType, ((Facility)_facility).FacilityType.LanguageOption.Name));

            //    //4° Registro Address.
            //    String _addr = String.Empty;
            //    if (_facility.Addresses.Count > 0)
            //    {
            //        Address _contactAddress = _facility.Addresses.First().Value;
            //        _addr = _contactAddress.Street + " " + _contactAddress.Number + " " + _contactAddress.Floor + " " + _contactAddress.Department;
            //    }
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Addresses, _addr));

            //    //5° Registro Organization
            //    pkCompost = "IdOrganization=" + _facility.Organization.IdOrganization.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Organization, _facility.Organization.CorporateName, pkCompost, Common.ConstantsEntitiesName.DS.Organization,
            //        Common.ConstantsEntitiesName.DS.Organizations, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organization));

            //    //Finalmente retorna la tabla
            //    return _tblContentRelatedData;
            //}
            private PlaceHolder BuildFacilityMainData(Int64 idProcess, Int64 idEntity, ref String title, ref String subTitle, ref String classNameIcon, ref Dictionary<Int64, CatalogDoc> catalogDoc)
            {
                //Definiciones
                PlaceHolder _phContentRelatedData = new PlaceHolder();
                String pkCompost = String.Empty;

                //Construye el Objeto principal
                Site _facility = EMSLibrary.User.GeographicInformationSystem.Site(idEntity);
                //Setea el dictionary de pictures para devolverlo.
                catalogDoc = _facility.Pictures;

                //Setea el titulo para retornar.
                title = _facility.LanguageOption.Name;
             
                FacilityType _facilityType = ((Facility)_facility).FacilityType;
                subTitle = _facilityType.LanguageOption.Name;
                //Setea el nombre del class para el icono a mostrar!.
                classNameIcon = GetIconFacilityType(_facilityType.IdFacilityType);

                if (idProcess > 0)  //Solo para cuando estoy navegando un process!!
                {
                    ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(idProcess);
                    Dictionary<Int64, ProcessTask> _tasks = _facility.TasksByProcess(_processGroupProcess);
                    if (_tasks.Count > 0)
                    {
                        //Si al menos una Tarea tiene excepciones, pongo el Icono en ROJO!
                        foreach (ProcessTask item in _facility.TasksByProcess(_processGroupProcess).Values)
                        {
                            ProcessTaskMeasurement _processTaskMeasurement = (ProcessTaskMeasurement)item;
                            if ((_processTaskMeasurement.ExecutionStatus) || (_processTaskMeasurement.MeasurementStatus))
                            {
                                classNameIcon = classNameIcon.Replace("Red", String.Empty) + "Red";
                            }
                        }
                    }
                    else
                    {
                        Facility _facilityRoot = (Facility)_facility;
                        //Obtengo todos los hijos de este facility para recorrer las tareas y ver si va el ROJO!!!
                        Dictionary<Int64, Sector> _sectors = GetAllSectors(_facilityRoot);
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
                                        classNameIcon = classNameIcon.Replace("Red", String.Empty) + "Red";
                                    }
                                }
                            }
                        }
                    }
                }

                //1° Registro Nombre del facility
                //El separador de los pk, le pongo pipe |, porque sino trae problemas al redirect.
                pkCompost = "IdOrganization=" + _facility.Organization.IdOrganization.ToString() + "|IdFacility=" + _facility.IdFacility.ToString();
                //Pone el valor de tipo link
                //_phContentRelatedData.Controls.Add(BuildLinkButtonValue(_facility.LanguageOption.Name, pkCompost, Common.ConstantsEntitiesName.DS.Facility,
                //    Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organization, "lblFacility"));
                _phContentRelatedData.Controls.Add(BuildLinkButtonValue(_facility.LanguageOption.Name, pkCompost, Common.ConstantsEntitiesName.DS.Facility,
                    Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Facility, "lblFacility"));

                //2° Registro Descripcion
                //Pone valor
                _phContentRelatedData.Controls.Add(BuildLabelValue(_facility.LanguageOption.Description, "lblDescription"));

                //3° Registro Facility Type
                //Titulo
                _phContentRelatedData.Controls.Add(BuildLabelValue(Resources.CommonListManage.FacilityType, "lblFacilityType"));
                //Pone valor
                _phContentRelatedData.Controls.Add(BuildLabelValue(subTitle, "subTitle"));

                //4° Registro Address.
                String _addr = String.Empty;
                if (_facility.Addresses.Count > 0)
                {
                    Address _contactAddress = _facility.Addresses.First().Value;
                    _addr = _contactAddress.Street + " " + _contactAddress.Number + " " + _contactAddress.Floor + " " + _contactAddress.Department;
                }
                //Titulo
                _phContentRelatedData.Controls.Add(BuildLabelValue(Resources.CommonListManage.Addresses, "lblAddresses"));
                //Pone valor
                _phContentRelatedData.Controls.Add(BuildLabelValue(_addr, "lblValueAddresses"));

                //5° Registro Organization
                pkCompost = "IdOrganization=" + _facility.Organization.IdOrganization.ToString();
                //Titulo
                _phContentRelatedData.Controls.Add(BuildLabelValue(Resources.CommonListManage.Organization, "lblOrganization"));
                //Pone valor
                _phContentRelatedData.Controls.Add(BuildLinkButtonValue(_facility.Organization.CorporateName, pkCompost, Common.ConstantsEntitiesName.DS.Organization,
                    Common.ConstantsEntitiesName.DS.Organizations, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organization, "lblValueOrganization"));

                //Finalmente retorna la tabla
                return _phContentRelatedData;
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
            private String GetIconFacilityType(Int64 idFacilityType)
            {
                String _iconName = String.Empty;
                FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(idFacilityType);

                if (String.IsNullOrEmpty(_facilityType.IconName))
                {
                    _iconName = "pnlIconUnspecified";
                }
                else
                {
                    _iconName = "pnlIcon" + _facilityType.IconName;
                }

                return _iconName;


                //String _iconName = String.Empty;
                ////Una vez que tengamos todos los classname definidos para cada type se agregan en este switch.
                //switch (idFacilityType)
                //{
                //    case Common.Constants.idPowerPlant: //1
                //        _iconName = "pnlIconPowerPlant";
                //        break;
                //    case Common.Constants.idShopsAndServices: //2
                //        _iconName = "pnlIconShopsandServices";
                //        break;
                //    case Common.Constants.idHomes:  // 3
                //        _iconName = "pnlIconHomes";
                //        break;
                //    case Common.Constants.idRefineries: // 4
                //        _iconName = "pnlIconRefineries";
                //        break;
                //    case Common.Constants.idServiceStations:    // 5
                //        _iconName = "pnlIconServiceStations";
                //        break;
                //    case Common.Constants.idWaterTreatmentPlants:  //7
                //        _iconName = "pnlIconWaterTreatmentPlants";
                //        break;
                //    case Common.Constants.idIndustries://8
                //        _iconName = "pnlIconIndustries";
                //        break;
                //    case Common.Constants.idFarms:  //  9
                //        _iconName = "pnlIconFarms";
                //        break;
                //    case Common.Constants.idWasteTreatmentPlants:   // 10
                //        _iconName = "pnlIconWasteTreatmentPlants";
                //        break;
                //    case Common.Constants.idLandfill:   // 11
                //        _iconName = "pnlIconLandfill";
                //        break;
                //    case Common.Constants.idLand:   // 12
                //        _iconName = "pnlIconLand";
                //        break;

                //    case Common.Constants.idOffice: // = 13
                //        _iconName = "pnlIconOffice";
                //        break;
                //    case Common.Constants.idUnspecified:  // = 14;
                //        _iconName = "pnlIconUnspecified";
                //        break;
                //    case Common.Constants.idOilPipeline:    // = 15;
                //        _iconName = "pnlIconOilPipeline";
                //        break;
                //    case Common.Constants.idGasPipeline:    // = 16;
                //        _iconName = "pnlIconGasPipeline";
                //        break;
                //    case Common.Constants.idBatteries:  // = 17;
                //        _iconName = "pnlIconBatteriesOil";
                //        break;
                //    case Common.Constants.idMotorCompressorStation: // = 19;
                //        _iconName = "pnlIconMotorCompressorStation";
                //        break;
                //    case Common.Constants.idOilTreatmentPlant:  // = 20;
                //        _iconName = "pnlIconOilTreatmentPlant";
                //        break;
                //    case Common.Constants.idConditioningPlantDewpoint:  // = 21;
                //        _iconName = "pnlIconConditioningPlantDewpoint";
                //        break;
                //    case Common.Constants.idSeparationPlantOfLiquefiedGases:    // = 22;
                //        _iconName = "pnlIconSeparationPlantOfLiquefiedGases";
                //        break;
                //    case Common.Constants.idSaltWaterInjectionPlant:    // = 23;
                //        _iconName = "pnlIconSaltWaterInjectionPlant";
                //        break;
                //    case Common.Constants.idFreshWaterInjectionPlant:   // = 24;
                //        _iconName = "pnlIconFreshWaterInjectionPlant";
                //        break;
                //    case Common.Constants.idFreshWaterTransferPlant: // = 25;
                //        _iconName = "pnlIconFreshWaterTransferPlant";
                //        break;
                //    case Common.Constants.idThermalPowerPlant:  // = 27;
                //        _iconName = "pnlIconThermalPowerPlant";
                //        break;
                //    case Common.Constants.idOilWell:    // = 28;
                //        _iconName = "pnlIconOilWell";
                //        break;
                //    case Common.Constants.idFleetVehicles:  // = 29;
                //        _iconName = "pnlIconFleetVehicles";
                //        break;
                //    case Common.Constants.idGlobal:  // = 30;
                //        _iconName = "pnlIconGlobal";
                //        break;

                //    case Common.Constants.idMining:
                //        _iconName = "pnlIconMining";
                //        break;
                //    case Common.Constants.idServices:
                //        _iconName = "pnlIconServices";
                //        break;

                //    default:
                //        _iconName = "pnlIconUnspecified"; // "pnlIconServiceStations";
                //        break;
                //}

                //return _iconName;
            }
            //private Table BuildDeviceMainData(Int64 idEntity, ref String title)
            //{
            //    //Definiciones
            //    //La tabla
            //    Table _tblContentRelatedData = new Table();
            //    String pkCompost = String.Empty;
            //    _tblContentRelatedData.CellPadding = 0;
            //    _tblContentRelatedData.CellSpacing = 0;
            //    _tblContentRelatedData.CssClass = "contentGeographicDashboardtable";

            //    //Construye el Objeto principal
            //    MeasurementDevice _measurementDevice = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDevice(idEntity);
            //    MeasurementDeviceType _measurementDeviceType = _measurementDevice.DeviceType;

            //    //Setea el titulo para retornar.
            //    title = _measurementDevice.FullName;

            //    //Inyecta el primer registro Nombre del device
            //    //El separador de los pk, le pongo pipe |, porque sino trae problemas al redirect.
            //    pkCompost = "IdMeasurementDeviceType=" + _measurementDeviceType.IdMeasurementDeviceType.ToString() + "|IdMeasurementDevice=" + _measurementDevice.IdMeasurementDevice.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Device, _measurementDevice.FullName, pkCompost, Common.ConstantsEntitiesName.PA.MeasurementDevice,
            //        Common.ConstantsEntitiesName.PA.MeasurementDevices, String.Empty, String.Empty));
                
            //    //2° Registro Organization
            //    pkCompost = "IdMeasurementDeviceType=" + _measurementDeviceType.IdMeasurementDeviceType.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.DeviceType, Common.Functions.ReplaceIndexesTags( _measurementDeviceType.LanguageOption.Name), pkCompost, Common.ConstantsEntitiesName.PA.MeasurementDeviceType,
            //        Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, String.Empty, String.Empty));

            //    String _calibrationState = String.Empty;
            //    if ((_measurementDevice.CalibrationStartDate() == null) || (!((DateTime.Now >= _measurementDevice.CalibrationStartDate()) && (DateTime.Now <= _measurementDevice.CalibrationEndDate()))))
            //    {
            //        _calibrationState = Resources.Common.NotCalibration;
            //    }
            //    else
            //    {
            //        _calibrationState = Resources.Common.Calibrated;
            //    }
            //    //3° Registro estado del dispositivo.
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.CalibrationState, _calibrationState));

            //    //4° Registro frecuencia de calibracion
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.CalibrationPeriodicity, _measurementDevice.CalibrationPeriodicity));

            //    //5° Registro fecha de instalacion
            //    String _instalationDate = String.Empty;
            //    if (_measurementDevice.InstallationDate.HasValue)
            //    {
            //        _instalationDate = _measurementDevice.InstallationDate.Value.ToShortDateString();
            //    }
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.InstallationDate, _instalationDate));

            //    //Finalmente retorna la tabla
            //    return _tblContentRelatedData;
            //}
            //private Table BuildMeasurementMainData(Int64 idEntity, ref String title)
            //{
            //    //Definiciones
            //    //La tabla
            //    Table _tblContentRelatedData = new Table();
            //    String pkCompost = String.Empty;
            //    _tblContentRelatedData.CellPadding = 0;
            //    _tblContentRelatedData.CellSpacing = 0;
            //    _tblContentRelatedData.CssClass = "contentGeographicDashboardtable";

            //    //Construye el Objeto principal
            //    Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(idEntity);
            //    Indicator _indicator = _measurement.Indicator;
            //    ProcessTask _processTask = _measurement.ProcessTask;
            //    ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)_processTask.ProcessGroupProcess;

            //    //Setea el titulo para retornar.
            //    title = _measurement.LanguageOption.Name;

            //    //Inyecta el primer registro Nombre de la medicion
            //    //El separador de los pk, le pongo pipe |, porque sino trae problemas al redirect.
            //    pkCompost = "IdMeasurement=" + _measurement.IdMeasurement.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Measurement, Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name), pkCompost, Common.ConstantsEntitiesName.PA.Measurement,
            //        Common.ConstantsEntitiesName.PA.Measurements, String.Empty, String.Empty));

            //    //2° Registro Indicator
            //    pkCompost = "IdIndicator=" + _indicator.IdIndicator.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Indicator, Common.Functions.ReplaceIndexesTags(_indicator.LanguageOption.Name), pkCompost, Common.ConstantsEntitiesName.PA.Indicator,
            //        Common.ConstantsEntitiesName.PA.Indicators, Common.ConstantsEntitiesName.PA.Indicator, Common.ConstantsEntitiesName.PA.Indicator));

            //    //3° Registro Process
            //    pkCompost = "IdProcess=" + _processGroupProcess.IdProcess.ToString();
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Process, _processGroupProcess.LanguageOption.Title, pkCompost, Common.ConstantsEntitiesName.PF.ProcessGroupProcess,
            //        Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess));

            //    //4° Registro Task Measurement
            //    pkCompost = "IdProcess=" + _processTask.Parent.IdProcess.ToString() + "|IdTask=" + _processTask.IdProcess.ToString(); ;
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.ProcessTaskMeasurement, _processTask.LanguageOption.Title, pkCompost, Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement,
            //        Common.ConstantsEntitiesName.PF.ProcessTaskMeasurements, Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement, Common.ConstantsEntitiesName.PF.ProcessGroupProcess));
                
            //    //5° Registro estado del dispositivo.
            //    TimeUnit _timeUnit = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_measurement.TimeUnitFrequency);
            //    String _measurementPlan = Resources.Common.WordFrequency + " " + _measurement.Frequency + " " + _timeUnit.LanguageOption.Name;
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.MeasurementPlan, _measurementPlan));

            //    //6° Registro Estado de ejecucion
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.ExecutionState, _processTask.State));

            //    //7° Registro Condicion de la medicion (en que rango pega)
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.Status, "Normal Operation"));

            //    //8° Registro indice de percepcion
            //    _tblContentRelatedData.Controls.Add(BuildRow(Resources.CommonListManage.PerceptionsIndex, "Good"));

            //    //Finalmente retorna la tabla
            //    return _tblContentRelatedData;
            //}
        #endregion

        #region External Methods
            protected Panel BuildMainData(String entityObject, Int64 idEntity, Int64 idProcess, ref String title, ref String subTitle, ref String classNameTitle, ref String classNameIcon, ref Dictionary<Int64, CatalogDoc> catalogDoc)
            {
                Panel _pnlContentMainData = new Panel();
                _pnlContentMainData.ID = "pnlContentMainData";

                switch (entityObject)
                {
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.PF.Process:
                        //Finalmente inyecta la tabla en el panel...
                        _pnlContentMainData.Controls.Add(BuildProcessMainData(idEntity, ref title, ref classNameIcon, ref catalogDoc));
                        classNameTitle = "lblTitleTooltips";
                        break;

                    case Common.ConstantsEntitiesName.DS.Facility:
                        //Finalmente inyecta la tabla en el panel...
                        _pnlContentMainData.Controls.Add(BuildFacilityMainData(idProcess, idEntity, ref title, ref subTitle, ref classNameIcon, ref catalogDoc));
                        classNameTitle = "lblTitleFacilityTooltips"; 
                        break;

                    default:
                        break;
                }

                return _pnlContentMainData;
            }
        #endregion

    }
}
