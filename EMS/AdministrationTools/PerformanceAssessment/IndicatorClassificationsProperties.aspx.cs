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

namespace Condesus.EMS.WebUI.PA
{
    public partial class IndicatorClassificationsProperties : BaseProperties
    {
        #region Internal Properties
            private IndicatorClassification _Entity = null;
            private Int64 _IdIndicatorClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdIndicatorClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicatorClassification")) : 0;
                }
            }
            private IndicatorClassification Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdIndicatorClassification);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private Int64 _IdParentIndicatorClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParentIndicatorClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParentIndicatorClassification")) : Convert.ToInt64(GetPKfromNavigator("IdIndicatorClassification"));
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
                base.InjectCheckIndexesTags();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                IndicatorClassification _indicatorClass = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdParentIndicatorClassification);
                if (_indicatorClass == null)
                    { lblParentValue.Text = Resources.Common.ComboBoxNoDependency; }
                else
                    { lblParentValue.Text = _indicatorClass.LanguageOption.Name; }

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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.IndicatorClassification;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.IndicatorClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblParentIndicatorClassification.Text = Resources.CommonListManage.Parent;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void LoadData()
            {
                base.PageTitle = Entity.LanguageOption.Name;

                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Obtiene el key necesario.
                    //Object _obj = GetKeyValue(_RtvIndicatorClassification.SelectedNode.Value, "IdIndicatorClassification");   //Si lo saco del tree, funciona!!!.
                    ////Si el key obtenido no llega a exister devuelve null.
                    //Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    

                    if (Entity == null)
                    {
                        IndicatorClassification _indicatorClassificationParent = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdParentIndicatorClassification);
                        //Alta
                        Entity = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassificationAdd(_indicatorClassificationParent, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtName.Text, txtDescription.Text);
                        //Entity = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdIndicatorClassification);
                        //base.NavigatorAddTransferVar("IdIndicatorClassification", Entity.IdIndicatorClassification);
                        //base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.IndicatorClassification);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }
                    //Ahora sea alta o modificacion va al Viewer de la entidad.
                    base.NavigatorAddTransferVar("IdIndicatorClassification", Entity.IdIndicatorClassification);
                    String _pkValues = "IdIndicatorClassification=" + Entity.IdIndicatorClassification.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.IndicatorClassification);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.IndicatorClassification);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.IndicatorClassification + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.IndicatorClassification, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
