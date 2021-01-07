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

namespace Condesus.EMS.WebUI.DS
{
    public partial class OrganizationClassificationsProperties : BaseProperties
    {
        #region Internal Properties
            private OrganizationClassification _Entity = null;
            private Int64 _IdOrganizationClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganizationClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganizationClassification")) : 0;
                }
            }
            private OrganizationClassification Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            if (!_IsParent)
                                { _Entity = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdOrganizationClassification); }
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private Int64 _IdParentOrganizationClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParentOrganizationClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParentOrganizationClassification")) : Convert.ToInt64(GetPKfromNavigator("IdOrganizationClassification"));
                }
            }
            private Boolean _IsParent
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IsParent") ? Convert.ToBoolean(base.NavigatorGetTransferVar<Object>("IsParent")) : false;
                }
            }
            //RadComboBox _RdcOrganizationClassification;
            //private RadTreeView _RtvOrganizationClassification;
            //private String _FilterExpression;
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
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                OrganizationClassification _organizationClass = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdParentOrganizationClassification);
                if (_organizationClass == null)
                { lblParentValue.Text = Resources.Common.ComboBoxNoDependency; }
                else
                { lblParentValue.Text = _organizationClass.LanguageOption.Name; }

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtOrganizationClassification.Focus();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.OrganizationClassification;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.OrganizationClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblIdParent.Text = Resources.CommonListManage.Parent;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblOrganizationClassification.Text = Resources.CommonListManage.OrganizationClassification;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                txtOrganizationClassification.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
            }
            private void LoadData()
            {
                Condesus.EMS.Business.DS.Entities.OrganizationClassification_LG _organizationClassification_LG = _Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage);

                txtOrganizationClassification.Text = _organizationClassification_LG.Name;
                txtDescription.Text = _organizationClassification_LG.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
            }
            private void AddCombos()
            {
                //AddComboOrganizationClassifications();
            }
            //private void AddComboOrganizationClassifications()
            //{
            //    if (Entity != null)
            //    {
            //        _FilterExpression = "IdOrganizationClassification<>" + Entity.IdOrganizationClassification.ToString();
            //    }
            //    //Combo de GeographicArea Parent
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    AddComboWithTree(phOrganizationClassification, ref _RdcOrganizationClassification, ref _RtvOrganizationClassification,
            //        Common.ConstantsEntitiesName.DS.OrganizationClassifications, _params, false, false, true, ref _FilterExpression,
            //        new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand),
            //        Resources.Common.ComboBoxNoDependency);
            //}
        #endregion

        #region Page Events
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            //protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            //{
            //    if (Entity != null)
            //    {
            //        _FilterExpression = "IdOrganizationClassification<>" + Entity.IdOrganizationClassification.ToString();
            //    }
            //    NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _FilterExpression, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            //}
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    ////Obtiene el key necesario.
                    //Object _obj = GetKeyValue(_RtvOrganizationClassification.SelectedNode.Value, "IdOrganizationClassification");   //Si lo saco del tree, funciona!!!.
                    ////Si el key obtenido no llega a exister devuelve null.
                    //Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    //OrganizationClassification _organizationClassificationParent = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_parentValue);
                    OrganizationClassification _organizationClassificationParent = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdParentOrganizationClassification);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.DirectoryServices.Map.OrganizationClassificationAdd(_organizationClassificationParent, txtOrganizationClassification.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtOrganizationClassification.Text, txtDescription.Text);
                        //base.NavigatorAddTransferVar("IdOrganizationClassification", _IdOrganizationClassification);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }

                    base.NavigatorAddTransferVar("IdOrganizationClassification", Entity.IdOrganizationClassification);
                    String _pkValues = "IdOrganizationClassification=" + Entity.IdOrganizationClassification.ToString()
                        + "& IdParentOrganizationClassification=" + Entity.IdParentOrganizationClassification.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.OrganizationClassification);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.OrganizationClassification);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.OrganizationClassification + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.OrganizationClassification, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
