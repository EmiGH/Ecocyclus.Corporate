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
using Condesus.EMS.Business.EP.Entities;

namespace Condesus.EMS.WebUI.PF
{
    public partial class ExtendedPropertiesProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdExtendedProperty") ? base.NavigatorGetTransferVar<Int64>("IdExtendedProperty") : 0;
            }
        }
        private Int64 _IdExtendedPropertyClassification
        {
            get
            {
                object _o = ViewState["IdExtendedPropertyClassification"];
                if (_o != null)
                    return (Int64)ViewState["IdExtendedPropertyClassification"];

                return 0;
            }

            set
            {
                ViewState["IdExtendedPropertyClassification"] = value;
            }
        }

        private ExtendedProperty _Entity = null;
        private ExtendedProperty Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                        _Entity = EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_IdExtendedPropertyClassification).ExtendedProperty(_IdEntity);

                    return _Entity;
                }
                catch
                {
                    return null;
                }
            }

            set { _Entity = value; }
        }

        RadComboBox _RdcExtendedPropertyClassification;

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
            private void InitFkVars()
            {
                _IdExtendedPropertyClassification = base.NavigatorContainsTransferVar("IdExtendedPropertyClassification") ? base.NavigatorGetTransferVar<Int64>("IdExtendedPropertyClassification") : 0;
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage).Name : Resources.CommonListManage.ExtendedProperty;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void AddCombos()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phExtendedPropertyClassification, ref _RdcExtendedPropertyClassification, "ExtendedPropertyClassifications", String.Empty, _params, false, true, false, false, false);
            }
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ExtendedProperty;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblExtendedPropertyClassification.Text = Resources.CommonListManage.ExtendedPropertyClassification;;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;

                _RdcExtendedPropertyClassification.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;

                _RdcExtendedPropertyClassification.SelectedValue = "IdExtendedPropertyClassification=" + Entity.ExtendedPropertyClassification.IdExtendedPropertyClassification.ToString();
            }
        #endregion

        #region Page Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _idExtendedPropertyClassification = Convert.ToInt64(GetKeyValue(_RdcExtendedPropertyClassification.SelectedValue, "IdExtendedPropertyClassification"));

                if (Entity == null)
                {
                    //Alta
                    Entity = EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClassification).ExtendedPropertiesAdd(txtName.Text, txtDescription.Text);
                }
                else
                {
                    //Modificacion
                    EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_IdExtendedPropertyClassification).ExtendedPropertiesModify(_IdEntity, txtName.Text, txtDescription.Text);
                    //Seteo mis Fk Nuevos
                    _IdExtendedPropertyClassification = Entity.ExtendedPropertyClassification.IdExtendedPropertyClassification;
                }
                base.NavigatorAddTransferVar("IdExtendedProperty", Entity.IdExtendedProperty);
                base.NavigatorAddTransferVar("IdExtendedPropertyClassification", Entity.ExtendedPropertyClassification.IdExtendedPropertyClassification);

                String _pkValues = "IdExtendedProperty=" + Entity.IdExtendedProperty.ToString()
                     + "& IdExtendedPropertyClassification=" + Entity.ExtendedPropertyClassification.IdExtendedPropertyClassification.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ExtendedProperty);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ExtendedProperty + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ExtendedProperty, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

