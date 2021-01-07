using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.PA
{
    public partial class ParameterRangesProperties : BaseProperties
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
                    return base.NavigatorContainsTransferVar("IdParameter") ? base.NavigatorGetTransferVar<Int64>("IdParameter") : Convert.ToInt64(GetPKfromNavigator("IdParameter"));
                }
            }
            private Int64 _IdParameterRange
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdParameterRange") ? base.NavigatorGetTransferVar<Int64>("IdParameterRange") : 0;
                }
            }

            private Condesus.EMS.Business.PA.Entities.ParameterRange _Entity = null;
            private Condesus.EMS.Business.PA.Entities.ParameterRange Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup).Parameter(_IdParameter).ParameterRange(_IdParameterRange);

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
                try
                {
                    base.PageTitle = (Entity != null) ? Entity.LowValue.ToString() + " - " + Entity.HighValue.ToString() : Resources.CommonListManage.ParameterRange;
                }
                catch
                {
                    base.PageTitle = Resources.CommonListManage.ParameterRange;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ParameterRange;
                lblHighValue.Text = Resources.CommonListManage.HighValue;
                lblLowValue.Text = Resources.CommonListManage.LowValue;
                rfvHV.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvLV.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                cvHV.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cvHVHigherLV.ErrorMessage = Resources.ConstantMessage.ValidationHVHigherLV;
                cvLV.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cvLVLessHV.ErrorMessage = Resources.ConstantMessage.ValidationLVLessHV;
            }
            private void LoadData()
            {
                txtLowValue.Text = Entity.LowValue.ToString();
                txtHighValue.Text = Entity.HighValue.ToString();
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //limpio los textbox por si hay datos
                txtLowValue.Text = String.Empty;
                txtHighValue.Text = String.Empty;
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
                        Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup).Parameter(_IdParameter).ParameterRangeAdd(Convert.ToDouble(txtLowValue.Text), Convert.ToDouble(txtHighValue.Text));
                    }
                    else
                    {
                        //Modificacion
                        EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ParameterGroup(_IdParameterGroup).Parameter(_IdParameter).ParameterRange(_IdParameterRange).Modify(Convert.ToDouble(txtLowValue.Text), Convert.ToDouble(txtHighValue.Text));
                    }
                    base.NavigatorAddTransferVar("IdIndicator", _IdIndicator);
                    base.NavigatorAddTransferVar("IdParameterGroup", _IdParameterGroup);
                    base.NavigatorAddTransferVar("IdParameter", _IdParameter);
                    base.NavigatorAddTransferVar("IdParameterRange", Entity.IdParameterRange);

                    String _pkValues = "IdIndicator=" + _IdIndicator.ToString()
                        + "& IdParameterGroup=" + _IdParameterGroup.ToString()
                        + "& IdParameter=" + _IdParameter.ToString()
                        + "& IdParameterRange=" + Entity.IdParameterRange.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.ParameterRange);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ParameterRange + " " + Entity.LowValue.ToString() + " - " + Entity.HighValue.ToString(), Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LowValue.ToString(), " - ", Entity.HighValue.ToString());
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.ParameterRange, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
