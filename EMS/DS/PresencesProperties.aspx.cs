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
    public partial class PresencesProperties : BaseProperties
    {
        #region Internal Properties

        private JobTitle _JobTitle = null;
        private Person _Person = null;
        private Int64 _IdFunctionalArea
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdFunctionalArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFunctionalArea")) : 0;
            }
        }
        private Int64 _IdGeographicArea
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdGeographicArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdGeographicArea")) : 0;
            }
        }
        private Int64 _IdPosition
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPosition") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPosition")) : 0;
            }
        }
        private Int64 _IdOrganization
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : 0;
            }
        }
        private Int64 _IdPerson
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPerson") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPerson")) : 0;
            }
        }
        private JobTitle JobTitle
        {
            get
            {
                try
                {
                    if (_JobTitle == null)
                    {
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
                        Position _position = _organization.Position(_IdPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);

                        _JobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                    }
                    return _JobTitle;
                }
                catch { return null; }
            }

            set { _JobTitle = value; }
        }
        private Person Person
        {
            get
            {
                try
                {
                    if (_Person == null)
                        _Person = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson);

                    return _Person;
                }
                catch { return null; }
            }

            set { _Person = value; }
        }

        private RadTreeView _RtvGeographicArea
        {
            get { return (RadTreeView)Session["rtvGeographicArea"]; }
            set { Session["rtvGeographicArea"] = value; }
        }
        private ArrayList _AssignedPresences //Estructura interna para guardar los id de presencias asignadas.
        {
            get
            {
                if (ViewState["AssignedPresences"] == null)
                {
                    ViewState["AssignedPresences"] = new ArrayList();
                }
                return (ArrayList)ViewState["AssignedPresences"];
            }
            set { ViewState["AssignedPresences"] = value; }
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

            AddTreeViewGeographicArea();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Inicializo el Form
                if (JobTitle != null)
                { LoadData(); } //Edit.

                //Form
                base.SetContentTableRowsCss(tblContentForm);

                //Realiza la carga de los datos de Geographic Area, para que se puedan usar.
                LoadDataGeographicArea();
            }
        }
        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        protected override void SetPagetitle()
        {
            base.PageTitle = (JobTitle != null) ? Person.FirstName + ' ' + Person.LastName + " - " + JobTitle.Name() : Resources.CommonListManage.Presence;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        }

        #endregion

        #region Private Methods

        private void ClearLocalSession()
        {
            _RtvGeographicArea = null;
        }
        private void AddTreeViewGeographicArea()
        {
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.GeographicArea, _params);

            //Arma tree con todos los roots.
            phGeographicArea.Controls.Clear();
            _RtvGeographicArea = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.DS.GeographicArea, "Form");
            //Ya tengo el Tree le attacho los Handlers
            _RtvGeographicArea.NodeExpand += new RadTreeViewEventHandler(_RtvGeographicArea_NodeExpand);
            _RtvGeographicArea.NodeCreated += new RadTreeViewEventHandler(_RtvGeographicArea_NodeCreated);
            _RtvGeographicArea.NodeCheck += new RadTreeViewEventHandler(_RtvGeographicArea_NodeCheck);
            phGeographicArea.Controls.Add(_RtvGeographicArea);
        }
        /// <summary>
        /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
        /// </summary>
        private void LoadDataGeographicArea()
        {
            _RtvGeographicArea.Nodes.Clear();
            //Con el tree ya armado, ahora hay que llenarlo con datos.
            RadTreeView _rtvClass = _RtvGeographicArea;
            base.LoadGenericTreeView(ref _rtvClass, Common.ConstantsEntitiesName.DS.GeographicArea, Common.ConstantsEntitiesName.DS.GeographicArea, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            _RtvGeographicArea = _rtvClass;
        }
        private void LoadStructAssignedPresences()
        {
            //Carga de forma inicial todos los id de presences que ya estan asignados.
            _AssignedPresences = new ArrayList();
            Dictionary<Int64, Condesus.EMS.Business.DS.Entities.Presence> _presences = new Dictionary<Int64, Condesus.EMS.Business.DS.Entities.Presence>();
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
            Position _position = _organization.Position(_IdPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            foreach (Condesus.EMS.Business.DS.Entities.Presence _assignedPresences in ((Condesus.EMS.Business.DS.Entities.PersonwithUser)_organization.Person(_IdPerson)).Post(_jobTitle).Presences())
            {
                Int64 _id = _assignedPresences.Facility.IdFacility;

                _AssignedPresences.Add(_id);
            }
        }
        private void LoadData()
        {
            lblJobTitleValue.Text = _JobTitle.Name();
            lblPersonValue.Text = _Person.FirstName + ' ' + _Person.LastName;

            //Carga la estructura paralela con las areas geograficas.
            LoadStructAssignedPresences();
        }

        #endregion

        #region Page Events

        void _RtvGeographicArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(e.Node.Value);

            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.GeographicAreaChildren, _params);
            foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.GeographicAreaChildren].Rows)
            {
                RadTreeNode _node = SetNodeTreeViewManage(_drRecord, Common.ConstantsEntitiesName.DS.GeographicAreaChildren);
                e.Node.Nodes.Add(_node);
                SetExpandMode(_node, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, false, false);
            }
        }
        void _RtvGeographicArea_NodeCreated(object sender, RadTreeNodeEventArgs e)
        {
            Int64 _id = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdGeographicArea"));
            if (_AssignedPresences.Contains(_id))
            {
                e.Node.Checked = true;
                e.Node.Checkable = false;
            }
            else
            {
                e.Node.Checked = false;
            }
        }
        void _RtvGeographicArea_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            RadTreeNode _node = e.Node;

            //Obtiene el Id del nodo checkeado
            Int64 _id = Convert.ToInt64(GetKeyValue(_node.Value, "IdGeographicArea"));
            if (_AssignedPresences.Contains(_id))
            {
                if (!_node.Checked)
                {
                    _AssignedPresences.Remove(_id);
                }
            }
            else
            {
                _AssignedPresences.Add(_id);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ////Se deben insertar 1 o mas Clasificaciones
                //Dictionary<Int64, Condesus.EMS.Business.DS.Entities.GeographicArea> _geographicAreas = new Dictionary<Int64, Condesus.EMS.Business.DS.Entities.GeographicArea>();

                //foreach (Int64 _item in _AssignedPresences)
                //{
                //    _geographicAreas.Add(_item, EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).GeographicArea(_item);
                //}

                foreach (RadTreeNode _node in _RtvGeographicArea.CheckedNodes)
                {
                    if ((_node.Checked) && (_node.Checkable))
                    {
                        Int64 _idFacility = Convert.ToInt64(_node.Value);

                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                        Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
                        Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_IdPosition);
                        Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);

                        Condesus.EMS.Business.DS.Entities.Post _post = ((Condesus.EMS.Business.DS.Entities.PersonwithUser)_organization.Person(_IdPerson)).Post(_JobTitle);
                        Facility _geoAreaFacility = _organization.Facility(_idFacility);

                        _post.PresencesAdd(_geoAreaFacility);
                    }
                }
                base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                base.NavigatorAddTransferVar("IdPerson", _IdPerson);
                base.NavigatorAddTransferVar("IdGeographicArea", _IdGeographicArea);
                base.NavigatorAddTransferVar("IdPosition", _IdPosition);
                base.NavigatorAddTransferVar("IdFunctionalArea", _IdFunctionalArea);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Presence);
                base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                String _pkValues = "IdOrganization=" + _IdOrganization.ToString() + 
                    "& IdPerson=" + _IdPerson.ToString() +
                    "& IdGeographicArea=" + _IdGeographicArea.ToString() + 
                    "& IdPosition=" + _IdPosition.ToString() +
                    "& IdFunctionalArea=" + _IdFunctionalArea.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Presence + " " + JobTitle.Name());
                String _entityPropertyName = String.Concat(JobTitle.Name());
                NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Presence, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
