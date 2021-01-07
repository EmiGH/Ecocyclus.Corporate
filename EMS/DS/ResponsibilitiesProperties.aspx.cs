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
    public partial class ResponsibilitiesProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdOrganization
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : 0;
            }
        }
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
        private JobTitle _JobTitle = null;
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
                        GeographicFunctionalArea _geoFunArea=_organization.GeographicFunctionalArea(_funArea, _geoArea);
                        _JobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                    }
                    return _JobTitle;
                }
                catch { return null; }
            }

            set { _JobTitle = value; }
        }
        private RadTreeView _RtvGeographicFunctionalArea
        {
            get { return (RadTreeView)Session["rtvGeographicFunctionalArea"]; }
            set { Session["rtvGeographicFunctionalArea"] = value; }
        }
        private ArrayList _AssignedGeoFuncAreas //Estructura interna para guardar los id de areas geo funcionales asignadas.
        {
            get
            {
                if (ViewState["AssignedGeoFuncAreas"] == null)
                {
                    ViewState["AssignedGeoFuncAreas"] = new ArrayList();
                }
                return (ArrayList)ViewState["AssignedGeoFuncAreas"];
            }
            set { ViewState["AssignedGeoFuncAreas"] = value; }
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

            AddTreeViewGeographicFunctionalArea();
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

                //Realiza la carga de los datos de Geographic Functional Area, para que se puedan usar.
                LoadDataGeographicFunctionalArea();
            }
        }
        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        protected override void SetPagetitle()
        {
            base.PageTitle = (JobTitle != null) ? JobTitle.Name(): Resources.CommonListManage.Responsibility;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        }

        #endregion

        #region Private Methods

        private void ClearLocalSession()
        {
            _RtvGeographicFunctionalArea = null;
        }
        private void AddTreeViewGeographicFunctionalArea()
        {
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.GeographicArea, _params);

            //Arma tree con todos los roots.
            phGeographicFunctionalArea.Controls.Clear();
            _RtvGeographicFunctionalArea = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.DS.GeographicFunctionalArea, "Form");
            //Ya tengo el Tree le attacho los Handlers
            _RtvGeographicFunctionalArea.NodeExpand += new RadTreeViewEventHandler(_RtvGeographicFunctionalArea_NodeExpand);
            _RtvGeographicFunctionalArea.NodeCreated += new RadTreeViewEventHandler(_RtvGeographicFunctionalArea_NodeCreated);
            _RtvGeographicFunctionalArea.NodeCheck += new RadTreeViewEventHandler(_RtvGeographicFunctionalArea_NodeCheck);
            phGeographicFunctionalArea.Controls.Add(_RtvGeographicFunctionalArea);
        }
        /// <summary>
        /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
        /// </summary>
        private void LoadDataGeographicFunctionalArea()
        {
            _RtvGeographicFunctionalArea.Nodes.Clear();
            //Con el tree ya armado, ahora hay que llenarlo con datos.
            RadTreeView _rtvClass = _RtvGeographicFunctionalArea;
            base.LoadGenericTreeView(ref _rtvClass, Common.ConstantsEntitiesName.DS.GeographicFunctionalArea, Common.ConstantsEntitiesName.DS.GeographicFunctionalArea, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            _RtvGeographicFunctionalArea = _rtvClass;
        }
        private void LoadStructAssignedGeographicFuinctionalAreas()
        {
            //Carga de forma inicial todos los id de presences que ya estan asignados.
            _AssignedGeoFuncAreas = new ArrayList();
            Dictionary<Int64, Condesus.EMS.Business.DS.Entities.Responsibility> _responsibilities = new Dictionary<Int64, Condesus.EMS.Business.DS.Entities.Responsibility>();
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
            Position _position = _organization.Position(_IdPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            foreach (Condesus.EMS.Business.DS.Entities.Responsibility _assignedGeoFuncAreas in _jobTitle.Responsibilities())
            {
                String _id = _assignedGeoFuncAreas.GeographicFunctionalAreaResponsibility.FunctionalArea.IdFunctionalArea.ToString() + ',' + _assignedGeoFuncAreas.GeographicFunctionalAreaResponsibility.GeographicArea.IdGeographicArea.ToString();

                _AssignedGeoFuncAreas.Add(_id);
            }
        }
        private void LoadData()
        {
            lblJobTitleValue.Text = _JobTitle.Name();

            //Carga la estructura paralela con las areas geograficas.
            LoadStructAssignedGeographicFuinctionalAreas();
        }

        #endregion

        #region Page Events

        void _RtvGeographicFunctionalArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(e.Node.Value);

            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren, _params);
            foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren].Rows)
            {
                RadTreeNode _node = SetNodeTreeViewManage(_drRecord, Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren);
                e.Node.Nodes.Add(_node);
                SetExpandMode(_node, Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren, false, false);
            }
        }
        void _RtvGeographicFunctionalArea_NodeCreated(object sender, RadTreeNodeEventArgs e)
        {
            Int64 _id = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdGeographicArea"));
            if (_AssignedGeoFuncAreas.Contains(_id))
            {
                e.Node.Checked = true;
                e.Node.Checkable = false;
            }
            else
            {
                e.Node.Checked = false;
            }
        }
        void _RtvGeographicFunctionalArea_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            RadTreeNode _node = e.Node;

            //Obtiene el Id del nodo checkeado
            Int64 _id = Convert.ToInt64(GetKeyValue(_node.Value, "IdGeographicArea"));
            if (_AssignedGeoFuncAreas.Contains(_id))
            {
                if (!_node.Checked)
                {
                    _AssignedGeoFuncAreas.Remove(_id);
                }
            }
            else
            {
                _AssignedGeoFuncAreas.Add(_id);
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

                foreach (RadTreeNode _node in _RtvGeographicFunctionalArea.CheckedNodes)
                {
                    if ((_node.Checked) && (_node.Checkable))
                    {
                        String _idGeographicFunctionalArea = _node.Value.ToString();
                        String[] _id = _idGeographicFunctionalArea.Split(',');
                        Int64 _idFunctionalArea_Res = Convert.ToInt64(_id[0]);
                        Int64 _idGeographicArea_Res = Convert.ToInt64(_id[1]);

                        //De esta forma obtenemos el ID ( la PK ) de la Fila en la que estamos para 
                        //luego utilizar en nuestro objeto para eliminar el registro
                        //EMSLibrary.User.DirectoryServices.Configuration.Responsibilities.Remove(Convert.ToInt32(IdResponsibility.Text));

                        //Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).FunctionalArea(_idFunctionalArea_Res);
                        //Condesus.EMS.Business.DS.Entities.GeographicArea _geoArea = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).GeographicArea(_idGeographicArea_Res);

                        //EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).JobTitle(_IdGeographicArea, _IdPosition, _IdFunctionalArea).ResponsibilitiesAdd(_idFunctionalArea_Res, _idGeographicArea_Res);
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea_Res);
                        Position _position = _organization.Position(_IdPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea_Res);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                        _jobTitle.ResponsibilitiesAdd(_funArea, _geoArea);
                    }
                }
                base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                base.NavigatorAddTransferVar("IdGeographicArea", _IdGeographicArea);
                base.NavigatorAddTransferVar("IdPosition", _IdPosition);
                base.NavigatorAddTransferVar("IdFunctionalArea", _IdFunctionalArea);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Responsibility);
                base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                String _pkValues = "IdOrganization=" + _IdOrganization.ToString() +
                    "& IdGeographicArea=" + _IdGeographicArea.ToString() +
                    "& IdPosition=" + _IdPosition.ToString() +
                    "& IdFunctionalArea=" + _IdFunctionalArea.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Responsibility + " " + JobTitle.Name());
                String _entityPropertyName = String.Concat(JobTitle.Name());
                NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Responsibility, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
            }
            catch (Exception ex)
            {
                base.StatusBar.ShowMessage(ex);
            }
        }
        #endregion
       

        #region Generic Methods
      
        private void CheckNodes()
        {
            foreach (Condesus.EMS.Business.DS.Entities.Responsibility _responsibility in _AssignedGeoFuncAreas)
            {
                String _value = _responsibility.GeographicFunctionalAreaResponsibility.FunctionalArea.IdFunctionalArea.ToString() + "," + _responsibility.GeographicFunctionalAreaResponsibility.GeographicArea.IdGeographicArea.ToString();
                RadTreeNode _node = _RtvGeographicFunctionalArea.FindNodeByValue(_value);
                if (_node != null)
                {
                    _node.Checked = true;
                    _node.Checkable = false;
                }
            }
        }
       
        #endregion
    }
}
