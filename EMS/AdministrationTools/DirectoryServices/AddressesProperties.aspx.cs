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

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class AddressesProperties : BaseProperties
    {
        #region Internal Properties
            CompareValidator _CvGeographicArea;
            private RadComboBox _RdcGeographicArea;
            private RadTreeView _RtvGeographicArea;
            private Int64 _IdAddress
            {
                get 
                {
                    return base.NavigatorContainsTransferVar("IdAddress") ? base.NavigatorGetTransferVar<Int64>("IdAddress") : 0;
                }
            }
            private Int64 _IdFacility
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFacility") ? base.NavigatorGetTransferVar<Int64>("IdFacility") : Convert.ToInt64(GetPKfromNavigator("IdFacility"));
                }
            }
            private Int64 _IdSector
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdSector") ? base.NavigatorGetTransferVar<Int64>("IdSector") : Convert.ToInt64(GetPKfromNavigator("IdSector"));
                }
            }
            private Int64 _IdOrganization
            {
                get
                {
                    object _o = ViewState["IdOrganization"];
                    if (_o != null)
                        return (Int64)ViewState["IdOrganization"];

                    return 0;
                }

                set
                {
                    ViewState["IdOrganization"] = value;
                }
            }
            private Int64 _IdPerson
            {
                get
                {
                    object _o = ViewState["IdPerson"];
                    if (_o != null)
                        return (Int64)ViewState["IdPerson"];

                    return 0;
                }

                set
                {
                    ViewState["IdPerson"] = value;
                }
            }
            private Address _Entity = null;
            private Address Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            if (_IdPerson > 0)
                            {   //Para personas
                                _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).Address(_IdAddress);
                            }
                            else
                            {
                                if (_IdSector > 0)
                                {   //Para sectores
                                    _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector).Address(_IdAddress);
                                }
                                else
                                {   //Para Facility
                                    _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Address(_IdAddress);
                                }
                            }
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);

                LoadTextLabels();
                AddCombos();
                AddValidators();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    String _dataObjects = String.Empty;
                    InitFkVars();
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                    {
                        LoadData(); //Edit.
                        if (_IdPerson > 0)
                        {
                            _dataObjects = Common.ConstantsEntitiesName.DS.Address + "|IdOrganization=" + _IdOrganization.ToString() + ",IdPerson=" + _IdPerson.ToString() + ",IdAddress=" + Entity.IdAddress.ToString();
                        }
                        else
                        {
                            if (_IdSector > 0)
                            {
                                _dataObjects = Common.ConstantsEntitiesName.DS.Address + "|IdOrganization=" + _IdOrganization.ToString() + ",IdFacility=" + _IdFacility.ToString() + ",IdSector=" + _IdSector.ToString() + ",IdAddress=" + Entity.IdAddress.ToString();
                            }
                            else
                            {
                                _dataObjects = Common.ConstantsEntitiesName.DS.Address + "|IdOrganization=" + _IdOrganization.ToString() + ",IdFacility=" + _IdFacility.ToString() + ",IdAddress=" + Entity.IdAddress.ToString();
                            }
                        } 
                    }
                    //Inyecta los JS que permiten abrir la ventana con el mapa.
                    InjectOpenWindowDialogPickUpCoords(inputPoints.ClientID, drawModeType.ClientID, pnlCoords.ClientID, _dataObjects);

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                String _pageTitle = Convert.ToString(GetPKfromNavigator("PageTitle"));
                if (_pageTitle != "0")
                {
                    base.PageTitle = _pageTitle;
                }
                else
                {
                    if (Entity != null)
                    {
                        String _title = Entity.Street + ' ' + Entity.Number + " - " + Entity.GeographicArea.LanguageOption.Name;
                        base.PageTitle = _title;
                    }
                    else
                    {
                        base.PageTitle = Resources.CommonListManage.Address;
                    }
                }
            }
        #endregion

        #region Private Methods
            private void SetGeographicArea()
            {
                //si es un root, no debe hacer nada de esto.
                if (Entity.GeographicArea.IdGeographicArea != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdGeographicArea=" + Entity.GeographicArea.IdGeographicArea.ToString();
                    RadTreeView _rtvGeoArea = _RtvGeographicArea;
                    RadComboBox _rcbGeoArea = _RdcGeographicArea;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvGeoArea, ref _rcbGeoArea, Common.ConstantsEntitiesName.DS.GeographicArea, Common.ConstantsEntitiesName.DS.GeographicAreaChildren);
                    _RdcGeographicArea = _rcbGeoArea;
                    _RtvGeographicArea = _rtvGeoArea;
                }
            }
            private void AddComboGeographicAreas()
            {
                String _filterExpression = String.Empty;
                //Combo de GeographicArea Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phGeographicArea, ref _RdcGeographicArea, ref _RtvGeographicArea,
                    Common.ConstantsEntitiesName.DS.GeographicAreas, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboGeoArea_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix, false);
            }
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Address;
                lblApartment.Text = Resources.CommonListManage.Apartment;
                lblFloor.Text = Resources.CommonListManage.Floor;
                lblNumber.Text = Resources.CommonListManage.Number;
                lblStreet.Text = Resources.CommonListManage.Street;
                lblZipCode.Text = Resources.CommonListManage.ZipCode;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                lblCoordenates.Text = Resources.CommonListManage.Coordenates;
                pnlCoords.InnerHtml = Resources.ConstantMessage.GeoCodeNotSelected;
            }
            private void InitFkVars()
            {
                //Aca intenta obtener el/los Id desde el TransferVar, si no esta ahi, entonces lo busca en las PKEntity.
                _IdOrganization = base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                _IdPerson = NavigatorContainsTransferVar("IdPerson") ? base.NavigatorGetTransferVar<Int64>("IdPerson") : Convert.ToInt64(GetPKfromNavigator("IdPerson"));
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtStreet.Text = String.Empty;
                txtNumber.Text = String.Empty;
                txtFloor.Text = String.Empty;
                txtApartment.Text = String.Empty;
                txtZipCode.Text = String.Empty;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtStreet.Text = Entity.Street;
                txtNumber.Text = Entity.Number;
                txtFloor.Text = Entity.Floor;
                txtApartment.Text = Entity.Department;
                txtZipCode.Text = Entity.PostCode;

                if (!String.IsNullOrEmpty(Entity.Coordinate))
                {
                    pnlCoords.InnerHtml = Resources.ConstantMessage.SelectedCoords + " <br />" + Entity.Coordinate;
                }
                SetGeographicArea();
            }
            private Address AddEntity()
            {
                Int64 _idGeographicArea = Convert.ToInt64(GetKeyValue(_RtvGeographicArea.SelectedNode.Value, "IdGeographicArea"));
                GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                String _coordinate = String.Empty;
                //Si no hay nada selecionado, entonces las coordendas quedan vacias
                if (!String.IsNullOrEmpty(drawModeType.Value) && !String.IsNullOrEmpty(inputPoints.Value))
                {
                    _coordinate = drawModeType.Value + ";" + inputPoints.Value;
                }
                else
                {
                    if (pnlCoords.InnerHtml != Resources.ConstantMessage.GeoCodeNotSelected)
                    {
                        _coordinate = pnlCoords.InnerHtml.ToString().Replace(Resources.ConstantMessage.SelectedCoords + " <br />", String.Empty);
                    }
                }

                if (_IdPerson > 0)
                {
                    return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).AddressesAdd(_geoArea, _coordinate, txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text);
                }
                else
                {
                    if (_IdSector > 0)
                    {
                        return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector).AddressesAdd(_geoArea, _coordinate, txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text);
                    }
                    else
                    {
                        return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).AddressesAdd(_geoArea, _coordinate, txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text);
                    }
                }
                        
            }
            private void ModifyEntity()
            {
                Int64 _idGeographicArea = Convert.ToInt64(GetKeyValue(_RtvGeographicArea.SelectedNode.Value, "IdGeographicArea"));
                GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                String _coordinate = String.Empty;
                //Si no hay nada selecionado, entonces las coordendas quedan vacias
                if (!String.IsNullOrEmpty(drawModeType.Value) && !String.IsNullOrEmpty(inputPoints.Value))
                {
                    _coordinate = drawModeType.Value + ";" + inputPoints.Value;
                }
                else
                {
                    if (pnlCoords.InnerHtml != Resources.ConstantMessage.GeoCodeNotSelected)
                    {
                        _coordinate = pnlCoords.InnerHtml.ToString().Replace(Resources.ConstantMessage.SelectedCoords + " <br />", String.Empty);
                    }
                }
                if (_IdPerson > 0)
                {
                    ((AddressPerson)Entity).Modify(EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson), _geoArea, _coordinate, txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text);
                }
                else
                {
                    if (_IdSector > 0)
                    {
                        ((AddressFacility)Entity).Modify(EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector), _geoArea, _coordinate, txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text); 
                    }
                    else
                    {
                        ((AddressFacility)Entity).Modify(EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility), _geoArea, _coordinate, txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text); 
                    }
                }
            }
            private void RemoveEntity()
            {
                if (_IdPerson > 0)
                {
                    EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).Remove(Entity);
                }
                else
                {
                    if (_IdSector > 0)
                    {
                        EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector).Remove(Entity);
                    }
                    else
                    {
                        EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Remove(Entity);
                    }
                }
            }
            private void AddCombos()
            {
                AddComboGeographicAreas();
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.GeographicAreas, phGeographicAreaValidator, ref _CvGeographicArea, _RdcGeographicArea, Resources.ConstantMessage.SelectAGeographicArea);
            }
        #endregion

        #region Page Events
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboGeoArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity == null)
                    {
                        //Alta
                        Entity = AddEntity();
                    }
                    else
                    {
                        //Modificacion
                        ModifyEntity();
                    }
                    base.NavigatorAddTransferVar("IdPerson", _IdPerson);
                    base.NavigatorAddTransferVar("IdAddress", Entity.IdAddress);
                    base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                    base.NavigatorAddTransferVar("IdFacility", _IdFacility);
                    base.NavigatorAddTransferVar("IdSector", _IdSector);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Address);

                    String _parentEntity = String.Empty;
                    if (Convert.ToString(GetPKfromNavigator("ParentEntity")) != "0")
                    {
                        _parentEntity = Convert.ToString(GetPKfromNavigator("ParentEntity"));
                        base.NavigatorAddTransferVar("ParentEntity", _parentEntity);
                    }
                    else
                    {
                        if (base.NavigatorContainsTransferVar("ParentEntity"))
                        {
                            _parentEntity = base.NavigatorGetTransferVar<String>("ParentEntity");
                            base.NavigatorAddTransferVar("ParentEntity", _parentEntity);
                        }
                        else
                        {
                            if (_IdPerson == 0)
                            {
                                _parentEntity = Common.ConstantsEntitiesName.DS.Person;
                                base.NavigatorAddTransferVar("ParentEntity", _parentEntity);
                            }
                            else
                            {
                                _parentEntity = Common.ConstantsEntitiesName.DS.Organization;
                                base.NavigatorAddTransferVar("ParentEntity", _parentEntity);
                            }
                        }
                    }
                    String _pkValues = "IdPerson=" + _IdPerson.ToString() +
                        "& IdAddress=" + Entity.IdAddress.ToString() +
                        "& IdOrganization=" + _IdOrganization.ToString() +
                        "& IdFacility=" + _IdFacility.ToString() +
                        "& IdSector=" + _IdSector.ToString() +
                        "& ParentEntity=" + _parentEntity;
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));

                    String _entityPropertyName = String.Concat(Entity.Street, ", ", Entity.Number);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Address, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);
                    //Navigate("~/MainInfo/ListViewer.aspx", Convert.ToString(GetPKfromNavigator("PageTitle")), Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion
    }
}
