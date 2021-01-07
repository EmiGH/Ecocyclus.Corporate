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
    public partial class MeasurementUnitsProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdMeasurementUnit") ? base.NavigatorGetTransferVar<Int64>("IdMeasurementUnit") : 0;
            }
        }
        private Int64 _IdMagnitud
        {
            get
            {
                object _o = ViewState["IdMagnitud"];
                if (_o != null)
                    return (Int64)ViewState["IdMagnitud"];

                return 0;
            }

            set
            {
                ViewState["IdMagnitud"] = value;
            }
        }

        private MeasurementUnit _Entity = null;
        private MeasurementUnit Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                        _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_IdMagnitud).MeasurementUnit(_IdEntity);

                    return _Entity;
                }
                catch
                {
                    return null;
                }
            }

            set { _Entity = value; }
        }

        RadComboBox _RdcMagnitud;
        CompareValidator _CvMagnitud;

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
                AddValidators();
                base.InjectCheckIndexesTags();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                if (!Page.IsPostBack)
                {
                    InitFkVars();

                    //Inicializo el Form
                    if (Entity == null)
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);                
                    this.txtName.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage).Name : Resources.CommonListManage.MeasurementUnit;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void InitFkVars()
            {
                _IdMagnitud = base.NavigatorContainsTransferVar("IdMagnitud") ? base.NavigatorGetTransferVar<Int64>("IdMagnitud") : 0;
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.Magnitudes, phMagnitudValidator, ref _CvMagnitud, _RdcMagnitud, Resources.ConstantMessage.SelectAMagnitud);
            }
            private void AddCombos()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phMagnitud, ref _RdcMagnitud, "Magnitudes", String.Empty, _params, false, true, false, false, false);
            }
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.MeasurementUnit;
                lblConstant.Text = Resources.CommonListManage.Constant;
                lblDenominator.Text = Resources.CommonListManage.Denominator;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblExponent.Text = Resources.CommonListManage.Exponent;
                lblIsPattern.Text = Resources.CommonListManage.IsPattern;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblMagnitud.Text = Resources.CommonListManage.Magnitud;
                lblName.Text = Resources.CommonListManage.Name;
                lblNumerator.Text = Resources.CommonListManage.Numerator;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv3.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv4.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv5.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                cv1.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
                cv2.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                revDenominator.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cv4.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                revConstant.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtConstant.Text = String.Empty;
                txtExponent.Text = String.Empty;
                txtNumerator.Text = String.Empty;
                txtDenominator.Text = String.Empty;
                chkIsPattern.Checked = false;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;

                _RdcMagnitud.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                txtConstant.Text = Entity.Constant.ToString().Replace(',', '.');
                txtExponent.Text = Entity.Exponent.ToString();
                txtNumerator.Text = Entity.Numerator.ToString();
                txtDenominator.Text = Entity.Denominator.ToString().Replace(',', '.');
                chkIsPattern.Checked = Entity.IsPattern;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;

                _RdcMagnitud.SelectedValue = "IdMagnitud=" + Entity.Magnitud.IdMagnitud.ToString();
                _RdcMagnitud.Enabled = false;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Int64 _idMagnitud = Convert.ToInt64(GetKeyValue(_RdcMagnitud.SelectedValue, "IdMagnitud"));

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnitAdd(Convert.ToInt64(txtNumerator.Text), Convert.ToInt64(txtDenominator.Text), Convert.ToInt64(txtExponent.Text), Convert.ToDecimal(txtConstant.Text), chkIsPattern.Checked, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_IdMagnitud).MeasurementUnitModify(_IdEntity, Convert.ToInt64(txtNumerator.Text), Convert.ToInt64(txtDenominator.Text), Convert.ToInt64(txtExponent.Text), Convert.ToDecimal(txtConstant.Text), chkIsPattern.Checked, txtName.Text, txtDescription.Text);
                    }
                    base.NavigatorAddTransferVar("IdMeasurementUnit", Entity.IdMeasurementUnit);
                    base.NavigatorAddTransferVar("IdMagnitud", Entity.Magnitud.IdMagnitud);

                    String _pkValues = "IdMeasurementUnit=" + Entity.IdMeasurementUnit.ToString()
                        + "& IdMagnitud=" + Entity.Magnitud.IdMagnitud.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.MeasurementUnit);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.MeasurementUnit + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.MeasurementUnit, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

