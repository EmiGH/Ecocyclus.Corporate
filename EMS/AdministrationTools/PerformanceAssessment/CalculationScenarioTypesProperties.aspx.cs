using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.PA
{
    public partial class CalculationScenarioTypesProperties : BaseProperties
    {
        #region Internal Properties

        private CalculationScenarioType _Entity = null;
        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdScenarioType") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdScenarioType")) : 0;
            }
        }
        private CalculationScenarioType Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                        _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(_IdEntity);

                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }
        private RadTreeView _RtvProcessClassification
        {
            get { return (RadTreeView)Session["rtvProcessClassification"]; }
            set { Session["rtvProcessClassification"] = value; }
        }
        private ArrayList _ProcessClassificationAux //Estructura interna para guardar los id de clasificacion que son seleccionados.
        {
            get
            {
                if (ViewState["ProcessClassificationAux"] == null)
                {
                    ViewState["ProcessClassificationAux"] = new ArrayList();
                }
                return (ArrayList)ViewState["ProcessClassificationAux"];
            }
            set { ViewState["ProcessClassificationAux"] = value; }
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
                AddTreeViewProcessClassifications();
                
                base.InjectCheckIndexesTags();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);                

                    //Realiza la carga de los datos de Clasificaciones, para que se puedan usar.
                    LoadDataProcessClassification();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.CalculationScenarioType;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.CalculationScenarioType;
                lblClassification.Text = Resources.CommonListManage.ProcessClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void ClearLocalSession()
            {
                _RtvProcessClassification = null;
            }
            private void AddTreeViewProcessClassifications()
            {
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessClassifications, _params);

                //Arma tree con todos los roots.
                phProcessClassification.Controls.Clear();
                _RtvProcessClassification = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.PF.ProcessClassifications, "Form");
                //Ya tengo el Tree le attacho los Handlers
                _RtvProcessClassification.NodeExpand += new RadTreeViewEventHandler(_RtvProcessClassification_NodeExpand);
                _RtvProcessClassification.NodeCreated += new RadTreeViewEventHandler(_RtvProcessClassification_NodeCreated);
                _RtvProcessClassification.NodeCheck += new RadTreeViewEventHandler(_RtvProcessClassification_NodeCheck);
                phProcessClassification.Controls.Add(_RtvProcessClassification);
            }
            /// <summary>
            /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
            /// </summary>
            private void LoadDataProcessClassification()
            {
                _RtvProcessClassification.Nodes.Clear();
                //Con el tree ya armado, ahora hay que llenarlo con datos.
                RadTreeView _rtvClass = _RtvProcessClassification;
                base.LoadGenericTreeView(ref _rtvClass, Common.ConstantsEntitiesName.PF.ProcessClassifications, Common.ConstantsEntitiesName.PF.ProcessClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
                _RtvProcessClassification = _rtvClass;
            }
            private void LoadStructProcessClassificationAux()
            {
                //Carga de forma inicial todos los id de clasificacion que ya estan cargados en este proyecto.
                _ProcessClassificationAux = new ArrayList();
                Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessClassification> _processClassifications = new Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessClassification>();
                if (_IdEntity != 0)
                {
                    _processClassifications = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(_IdEntity).ProcessClassification;
                    
                }
                //Ahora recorre todas las clasificaciones que ya tiene asiganadas, y los guarda en la estructura interna (ArrayList).
                foreach (Condesus.EMS.Business.PF.Entities.ProcessClassification _item in _processClassifications.Values)
                {
                    _ProcessClassificationAux.Add(_item.IdProcessClassification);
                }
            }
            private void LoadData()
            {
                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;

                //Carga la estructura paralela con las clasificaciones que tiene el indicador.
                LoadStructProcessClassificationAux();
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //limpio los textbox por si hay datos
                txtDescription.Text = String.Empty;
                txtName.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
            }
        #endregion

        #region Page Events
        void _RtvProcessClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(e.Node.Value);

            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, _params);
            foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PF.ProcessClassificationChildren].Rows)
            {
                RadTreeNode _node = SetNodeTreeViewManage(_drRecord, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren);
                e.Node.Nodes.Add(_node);
                SetExpandMode(_node, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, false, false);
            }
        }
        void _RtvProcessClassification_NodeCreated(object sender, RadTreeNodeEventArgs e)
        {
            Int64 _idClassification = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdProcessClassification"));
            if (_ProcessClassificationAux.Contains(_idClassification))
            {
                e.Node.Checked = true;
            }
            else
            {
                e.Node.Checked = false;
            }
        }
        void _RtvProcessClassification_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            RadTreeNode _node = e.Node;

            //Obtiene el Id del nodo checkeado
            Int64 _idClassification = Convert.ToInt64(GetKeyValue(_node.Value, "IdProcessClassification"));
            if (_ProcessClassificationAux.Contains(_idClassification))
            {
                if (!_node.Checked)
                {
                    _ProcessClassificationAux.Remove(_idClassification);
                }
            }
            else
            {
                _ProcessClassificationAux.Add(_idClassification);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Se deben insertar 1 o mas Clasificaciones
                Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessClassification> _processClassifications = new Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessClassification>();

                //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                foreach (Int64 _item in _ProcessClassificationAux)
                {
                    _processClassifications.Add(_item, EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_item));
                }

                //verifica si es un ADD o un Modify
                if (Entity == null)
                {
                    Entity = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioTypeAdd(txtName.Text, txtDescription.Text, _processClassifications);
                }
                else
                {
                    EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(_IdEntity).Modify(txtName.Text, txtDescription.Text, _processClassifications);
                    Entity = null;
                }
                base.NavigatorAddTransferVar("IdScenarioType", Entity.IdScenarioType);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.CalculationScenarioType);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                String _pkValues = "IdScenarioType=" + Entity.IdScenarioType.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.CalculationScenarioType + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.CalculationScenarioType, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
