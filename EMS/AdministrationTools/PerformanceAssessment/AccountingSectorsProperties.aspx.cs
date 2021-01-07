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
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment
{
    public partial class AccountingSectorsProperties : BaseProperties
    {
        #region Internal Properties
            private AccountingSector _Entity = null;
            private Int64 _IdSector
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdSector") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdSector")) : 0;
                }
            }
            private AccountingSector Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_IdSector);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadComboBox _RdcAccountingSector;
            private RadTreeView _RtvAccountingSector;
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
                base.InjectCheckIndexesTags();
                LoadTextLabels();
                AddCombos();
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
                    {
                        //Edit.
                        LoadData();
                    }
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.AccountingSector;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.AccountingSector;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblParentAccountingSector.Text = Resources.CommonListManage.Parent;
                lblLanguage.Text = Resources.CommonListManage.Language;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void ClearLocalSession()
            {
                _RdcAccountingSector = null;
                _RtvAccountingSector = null;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtName.ReadOnly = false;
                txtDescription.ReadOnly = false;
            }
            private void LoadData()
            {
                Condesus.EMS.Business.PA.Entities.AccountingSector_LG _accountingSector_LG = _Entity.LanguagesOptions[Global.DefaultLanguage.IdLanguage];

                base.PageTitle = _accountingSector_LG.Name;

                txtName.Text = _accountingSector_LG.Name;
                txtDescription.Text = _accountingSector_LG.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
                txtName.ReadOnly = false;
                txtDescription.ReadOnly = false;

                //si es un root, no debe hacer nada de esto.
                if (_Entity.IdParentSector != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdSector=" + _Entity.IdParentSector.ToString();
                    RadTreeView _rtvConstantClass = _RtvAccountingSector;
                    RadComboBox _rcbConstantClass = _RdcAccountingSector;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvConstantClass, ref _rcbConstantClass, Common.ConstantsEntitiesName.PA.AccountingSector, Common.ConstantsEntitiesName.PA.AccountingSectorsChildren);
                    _RdcAccountingSector = _rcbConstantClass;
                    _RtvAccountingSector = _rtvConstantClass;
                }
            }
            private void AddCombos()
            {
                AddComboAccountingSectors();
            }
            private void AddComboAccountingSectors()
            {
                String _filterExpression = String.Empty;
                //Combo de AccountingSector Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phAccountingSector, ref _RdcAccountingSector, ref _RtvAccountingSector,
                    Common.ConstantsEntitiesName.PA.AccountingSectors, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand),
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeClick),
                    Resources.Common.ComboBoxNoDependency, false);
            }
        #endregion

        #region Page Events
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.AccountingSectorsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            protected void rtvHierarchicalTreeViewInCombo_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                _RdcAccountingSector.Text = e.Node.Text;
                _RdcAccountingSector.SelectedValue = e.Node.Value;
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                AccountingSectorsPropertiesSave();
            }
            private void AccountingSectorsPropertiesSave()
            {
                try
                {
                    //Obtiene el key necesario.
                    Object _obj = GetKeyValue(_RtvAccountingSector.SelectedNode.Value, "IdSector");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    AccountingSector _constantClassificationParent = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_parentValue);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSectorAdd(_constantClassificationParent, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(_constantClassificationParent, txtName.Text, txtDescription.Text);
                    }

                    base.NavigatorAddTransferVar("IdSector", Entity.IdSector);
                    base.NavigatorAddTransferVar("IdParentSector", Entity.IdParentSector);
                    String _pkValues = "IdSector=" + Entity.IdSector.ToString()
                        + "& IdParentSector=" + Entity.IdParentSector.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.AccountingSector);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);

                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Entity.GetType().Name, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
