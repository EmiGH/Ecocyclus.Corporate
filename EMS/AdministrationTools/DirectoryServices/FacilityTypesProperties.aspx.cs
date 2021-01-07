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
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.DirectoryServices
{
    public partial class FacilityTypesProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFacilityType") ? base.NavigatorGetTransferVar<Int64>("IdFacilityType") : 0;
                }
            }
            private FacilityType _Entity = null;
            private FacilityType Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            _Entity = EMSLibrary.User.GeographicInformationSystem.FacilityType(_IdEntity);
                        }

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
        #endregion

        #region PageLoad & Init
        protected override void InyectJavaScript()
        {
            base.InyectJavaScript();

            base.InjectCheckIndexesTags();
        }
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
            base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.FacilityType;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        }
        #endregion

        #region Private Methods
        private void LoadTextLabels()
        {
            Page.Title = Resources.CommonListManage.FacilityType;
            lblLanguage.Text = Resources.CommonListManage.Language;
            lblName.Text = Resources.CommonListManage.Name;
            lblDescription.Text = Resources.CommonListManage.Description;
            lblIconName.Text = Resources.CommonListManage.IconName;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtName.Text = String.Empty;
            txtDescription.Text = String.Empty;
            lblLanguageValue.Text = Global.DefaultLanguage.Name;
        }
        private void LoadData()
        {
            //carga los datos en pantalla
            txtName.Text = Entity.LanguageOption.Name;
            txtIconName.Text = Entity.IconName;
            //if (String.IsNullOrEmpty(Entity.IconName))
            //{
            //    rdcIconName.SelectedValue = "Unspecified";
            //}
            //else
            //{
            //    rdcIconName.SelectedValue = Entity.IconName;
            //}
            txtDescription.Text = Entity.LanguageOption.Description;
            lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
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
                    Entity = EMSLibrary.User.GeographicInformationSystem.FacilityTypeAdd(txtIconName.Text, txtName.Text, txtDescription.Text);
                }
                else
                {
                    //Modificacion
                    Entity.Modify(txtIconName.Text, txtName.Text, txtDescription.Text);
                }
                base.NavigatorAddTransferVar("IdFacilityType", Entity.IdFacilityType);

                String _pkValues = "IdFacilityType=" + Entity.IdFacilityType.ToString();

                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.FacilityType);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.FacilityType, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
