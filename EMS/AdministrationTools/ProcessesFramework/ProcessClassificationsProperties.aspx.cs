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
using Condesus.EMS.Business.PF.Entities;

namespace Condesus.EMS.WebUI.PF
{
    public partial class ProcessClassificationsProperties : BaseProperties
    {
        #region Internal Properties
            private ProcessClassification _Entity = null;
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcessClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcessClassification")) : 0;
                }
            }
            private ProcessClassification Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdEntity);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private Int64 _IdParentProcessClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParentProcessClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParentProcessClassification")) : Convert.ToInt64(GetPKfromNavigator("IdProcessClassification"));
                }
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
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                ProcessClassification _processClass = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdParentProcessClassification);
                if (_processClass == null)
                    { lblParentValue.Text = Resources.Common.ComboBoxNoDependency; }
                else
                    { lblParentValue.Text = _processClass.LanguageOption.Name; }

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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.ProcessClassification;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ProcessClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblParentProcessClassification.Text = Resources.CommonListManage.Parent;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
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
                    //Object _obj = GetKeyValue(_RtvProcessClassification.SelectedNode.Value, "IdProcessClassification");   //Si lo saco del tree, funciona!!!.
                    ////Si el key obtenido no llega a exister devuelve null.
                    //Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.

                    if (Entity == null)
                    {
                        //Alta
                        Condesus.EMS.Business.PF.Entities.ProcessClassification _parentProcessClassification = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdParentProcessClassification);
                        Entity = EMSLibrary.User.ProcessFramework.Map.ProcessClassificationAdd(_parentProcessClassification, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtName.Text, txtDescription.Text);//_IdParentProcessClassification,
                        //Entity = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdEntity);
                        //base.NavigatorAddTransferVar("IdProcessClassification", Entity.IdProcessClassification);
                        //base.NavigatorAddTransferVar("IdParentProcessClassification", Entity.IdParentProcessClassification);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }

                    base.NavigatorAddTransferVar("IdProcessClassification", Entity.IdProcessClassification);
                    base.NavigatorAddTransferVar("IdParentProcessClassification", Entity.IdParentProcessClassification);
                    String _pkValues = "IdProcessClassification=" + Entity.IdProcessClassification.ToString()
                            + "& IdParentProcessClassification=" + Entity.IdParentProcessClassification.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessClassification);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessClassification);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ProcessClassification + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessClassification, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

