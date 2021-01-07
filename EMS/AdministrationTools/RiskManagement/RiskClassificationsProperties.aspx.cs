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
using Condesus.EMS.Business.RM.Entities;

namespace Condesus.EMS.WebUI.RM
{
    public partial class RiskClassificationsProperties : BaseProperties
    {
        #region Internal Properties
            private RiskClassification _Entity = null;
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdRiskClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdRiskClassification")) : 0;
                }
            }
            private Int64 _IdParentRiskClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParentRiskClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParentRiskClassification")) : Convert.ToInt64(GetPKfromNavigator("IdRiskClassification"));
                }
            }
            private RiskClassification Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.RiskManagement.Map.RiskClassification(_IdEntity);

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
                RiskClassification _riskClass = EMSLibrary.User.RiskManagement.Map.RiskClassification(_IdParentRiskClassification);
                if (_riskClass == null)
                { lblParentValue.Text = Resources.Common.ComboBoxNoDependency; }
                else
                { lblParentValue.Text = _riskClass.LanguageOption.Name; }

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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.RiskClassification;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.RiskClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblParentRiskClassification.Text = Resources.CommonListManage.Parent;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
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
                    //Object _obj = GetKeyValue(_RtvRiskClassification.SelectedNode.Value, "IdRiskClassification");   //Si lo saco del tree, funciona!!!.
                    ////Si el key obtenido no llega a exister devuelve null.
                    //Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    RiskClassification _riskClassificationParent = EMSLibrary.User.RiskManagement.Map.RiskClassification(_IdParentRiskClassification);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.RiskManagement.Map.RiskClassificationAdd(_riskClassificationParent, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtName.Text, txtDescription.Text);
                        //Entity = EMSLibrary.User.RiskManagement.Map.RiskClassification(_IdEntity);
                        //base.NavigatorAddTransferVar("IdRiskClassification", Entity.IdRiskClassification);
                        //base.NavigatorAddTransferVar("IdParentRiskClassification", Entity.IdParentRiskClassification);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }

                    base.NavigatorAddTransferVar("IdRiskClassification", Entity.IdRiskClassification);
                    base.NavigatorAddTransferVar("IdParentRiskClassification", Entity.IdParentRiskClassification);
                    String _pkValues = "IdRiskClassification=" + Entity.IdRiskClassification.ToString()
                            + "& IdParentRiskClassification=" + Entity.IdParentRiskClassification.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.RM.RiskClassification);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.RiskClassification + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.RM.RiskClassification, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

