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
    public partial class ParametersProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdIndicator
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdIndicator") ? base.NavigatorGetTransferVar<Int64>("IdIndicator") : Convert.ToInt64(GetPKfromNavigator("IdIndicator"));
                }
            }
            private Int64 _IdParameterGroup
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParameterGroup") ? base.NavigatorGetTransferVar<Int64>("IdParameterGroup") : Convert.ToInt64(GetPKfromNavigator("IdParameterGroup"));
                }
            }
            private Int64 _IdParameter
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParameter") ? base.NavigatorGetTransferVar<Int64>("IdParameter") : 0;
                }
            }
            private Condesus.EMS.Business.PA.Entities.Parameter _Entity = null;
            private Condesus.EMS.Business.PA.Entities.Parameter Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup).Parameter(_IdParameter);

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
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
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
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Description : Resources.CommonListManage.Parameter;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Parameter;
                lblDescription.Text = Resources.CommonListManage.Name;
                lblException.Text = Resources.CommonListManage.RaiseException;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblSign.Text = Resources.CommonListManage.Sign;
                ddlSign.Items[0].Text = Resources.CommonListManage.Positive;
                ddlSign.Items[1].Text = Resources.CommonListManage.Neutral;
                ddlSign.Items[2].Text = Resources.CommonListManage.Negative;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void LoadData()
            {
                txtDescription.Text = Entity.LanguageOption.Description;
                ddlSign.SelectedValue = Entity.Sign;
                chkRaiseException.Checked = Entity.RaiseException;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //limpio los textbox por si hay datos
                txtDescription.Text = String.Empty;
                chkRaiseException.Enabled = true;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup).ParameterAdd(txtDescription.Text, ddlSign.SelectedValue.ToString(), chkRaiseException.Checked); 
                    }
                    else
                    {
                        //Modificacion
                        EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup).Parameter(_IdParameter).Modify(txtDescription.Text, ddlSign.SelectedValue.ToString(), chkRaiseException.Checked);
                    }
                    base.NavigatorAddTransferVar("IdIndicator", _IdIndicator);
                    base.NavigatorAddTransferVar("IdParameterGroup", _IdParameterGroup);
                    base.NavigatorAddTransferVar("IdParameter", Entity.IdParameter);

                    String _pkValues = "IdIndicator=" + _IdIndicator.ToString()
                        + "& IdParameterGroup=" + _IdParameterGroup.ToString()
                        + "& IdParameter=" + Entity.IdParameter.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Parameter);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Parameter + " " + Entity.LanguageOption.Description, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Description);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Parameter, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

