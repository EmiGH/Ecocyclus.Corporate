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
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.KC
{
    public partial class ResourceClassificationsProperties : BaseProperties
    {
        #region Internal Properties
            private ResourceClassification _Entity = null;
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdResourceClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResourceClassification")) : 0;
                }
            }
            private Int64 _IdParentResourceClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParentResourceClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParentResourceClassification")) : Convert.ToInt64(GetPKfromNavigator("IdResourceClassification"));
                }
            }
            private ResourceClassification Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdEntity);

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

                base.InjectCheckIndexesTags();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                ResourceClassification _resourceClass = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdParentResourceClassification);
                if (_resourceClass == null)
                { lblParentValue.Text = Resources.Common.ComboBoxNoDependency; }
                else
                { lblParentValue.Text = _resourceClass.LanguageOption.Name; }

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtName.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.ResourceClassification;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ResourceClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblParentResourceClassification.Text = Resources.CommonListManage.Parent;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
            }
            private void LoadData()
            {
                base.PageTitle = Entity.LanguageOption.Name;

                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Obtiene el key necesario.
                    //Object _obj = GetKeyValue(_RtvResourceClassification.SelectedNode.Value, "IdResourceClassification");   //Si lo saco del tree, funciona!!!.
                    ////Si el key obtenido no llega a exister devuelve null.
                    //Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    

                    if (Entity == null)
                    {
                        ResourceClassification _resourceClassificationParent = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdParentResourceClassification);
                        //Alta
                        Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassificationAdd(_resourceClassificationParent, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtName.Text, txtDescription.Text);
                        //Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdEntity);
                        //base.NavigatorAddTransferVar("IdResourceClassification", Entity.IdResourceClassification);
                        //base.NavigatorAddTransferVar("IdParentResourceClassification", Entity.IdParentResourceClassification);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }

                    base.NavigatorAddTransferVar("IdResourceClassification", Entity.IdResourceClassification);
                    base.NavigatorAddTransferVar("IdParentResourceClassification", Entity.IdParentResourceClassification);

                    String _pkValues = "IdResourceClassification=" + Entity.IdResourceClassification.ToString()
                            + "& IdParentResourceClassification=" + Entity.IdParentResourceClassification.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.KC.ResourceClassification);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.ResourceClassification);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ResourceClassification + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.KC.ResourceClassification, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (System.Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion
    }
}

