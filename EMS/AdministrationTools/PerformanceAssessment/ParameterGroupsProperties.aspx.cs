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
    public partial class ParameterGroupsProperties : BaseProperties
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
                    return base.NavigatorContainsTransferVar("IdParameterGroup") ? base.NavigatorGetTransferVar<Int64>("IdParameterGroup") : 0;
                }
            }
            private ParameterGroup _Entity = null;
            private ParameterGroup Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup);

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
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.ParameterGroup;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ParameterGroup;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void LoadData()
            {
                Condesus.EMS.Business.PA.Entities.ParameterGroup_LG _parameterGroup_LG = Entity.LanguageOption;

                txtName.Text = _parameterGroup_LG.Name;
                txtDescription.Text = _parameterGroup_LG.Description;
                lblLanguageValue.Text = _parameterGroup_LG.Language.Name;
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
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity == null)
                    {
                        //Alta
                        Entity =EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroupAdd(txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup).Modify(txtName.Text, txtDescription.Text);
                    }
                    base.NavigatorAddTransferVar("IdIndicator", _IdIndicator);
                    base.NavigatorAddTransferVar("IdParameterGroup", Entity.IdParameterGroup);
                    String _pkValues = "IdIndicator=" + _IdIndicator.ToString() +
                        "& IdParameterGroup=" + Entity.IdParameterGroup.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.ParameterGroup);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.ParameterGroup);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ParameterGroup + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.ParameterGroup, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

