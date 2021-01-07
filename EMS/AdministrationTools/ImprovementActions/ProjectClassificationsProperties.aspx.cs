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
using Condesus.EMS.Business.IA.Entities;

namespace Condesus.EMS.WebUI.IA
{
    public partial class ProjectClassificationsProperties : BaseProperties
    {
        #region Internal Properties
            private ProjectClassification _Entity = null;
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProjectClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProjectClassification")) : 0;
                }
            }
            private ProjectClassification Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdEntity);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private Int64 _IdParentProjectClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParentProjectClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParentProjectClassification")) : Convert.ToInt64(GetPKfromNavigator("IdProjectClassification"));
                }
            }

        //private RadTreeView _RtvProjectClassificationVar;
        //private RadTreeView _RtvProjectClassification
        //{
        //    get
        //    {
        //        Session["rtvProjectClassification"] = _RtvProjectClassificationVar;
        //        return _RtvProjectClassificationVar;
        //    }
        //    set
        //    {
        //        Session["rtvProjectClassification"] = value;
        //        _RtvProjectClassificationVar = value;
        //    }
        //}
        //private RadComboBox _RdcProjectClassificationVar;
        //private RadComboBox _RdcProjectClassification
        //{
        //    get
        //    {
        //        Session["rdcProjectClassification"] = _RdcProjectClassificationVar;
        //        return _RdcProjectClassificationVar;
        //    }
        //    set
        //    {
        //        Session["rdcProjectClassification"] = value;
        //        _RdcProjectClassificationVar = value;
        //    }
        //}
        //private String _FilterExpressionProjectClassificationVar;
        //private String _FilterExpressionProjectClassification
        //{
        //    get
        //    {
        //        Session["FilterExpressionProjectClassification"] = _FilterExpressionProjectClassificationVar;
        //        return _FilterExpressionProjectClassificationVar;
        //    }
        //    set
        //    {
        //        Session["FilterExpressionProjectClassification"] = value;
        //        _FilterExpressionProjectClassificationVar = value;
        //    }
        //}

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
                ProjectClassification _projectClass = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdParentProjectClassification);
                if (_projectClass == null)
                    { lblParentValue.Text = Resources.Common.ComboBoxNoDependency; }
                else
                    { lblParentValue.Text = _projectClass.LanguageOption.Name; }

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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.ProjectClassification;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ProjectClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblParentProjectClassification.Text = Resources.CommonListManage.Parent;
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
                //Object _obj = GetKeyValue(_RtvProjectClassification.SelectedNode.Value, "IdProjectClassification");   //Si lo saco del tree, funciona!!!.
                ////Si el key obtenido no llega a exister devuelve null.
                //Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                

                if (Entity == null)
                {
                    ProjectClassification _projectClassificationParent = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdParentProjectClassification);
                    //Alta
                    Entity = EMSLibrary.User.ImprovementAction.Map.ProjectClassificationAdd(_projectClassificationParent, txtName.Text, txtDescription.Text);
                }
                else
                {
                    //Modificacion
                    EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdEntity).Modify(txtName.Text, txtDescription.Text);
                    //Entity = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdEntity);
                    //base.NavigatorAddTransferVar("IdProjectClassification", Entity.IdProjectClassification);
                    //base.NavigatorAddTransferVar("IdParentProjectClassification", Entity.IdParentProjectClassification);
                    //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                }
                base.NavigatorAddTransferVar("IdProjectClassification", Entity.IdProjectClassification);
                base.NavigatorAddTransferVar("IdParentProjectClassification", Entity.IdParentProjectClassification);
                String _pkValues = "IdProjectClassification=" + Entity.IdProjectClassification.ToString()
                        + "& IdParentProjectClassification=" + Entity.IdParentProjectClassification.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.IA.ProjectClassification);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ProjectClassification + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.IA.ProjectClassification, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

