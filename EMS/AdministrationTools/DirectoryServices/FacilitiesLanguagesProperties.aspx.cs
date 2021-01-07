using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.DirectoryServices
{
    public partial class FacilitiesLanguagesProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
            }
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFacility") ? base.NavigatorGetTransferVar<Int64>("IdFacility") : 0;
                }
            }
            private String _IdEntityLanguage
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
                }
            }
            private Facility _Entity = null;
            private Facility Entity
            {
                get
                {
                    if (_Entity == null)
                    {
                        _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdEntity);
                    }

                    return _Entity;
                }

                set { _Entity = value; }
            }
            private Site_LG _EntityLanguage = null;
            private Site_LG EntityLanguage
            {
                get
                {
                    if (_EntityLanguage == null)
                    {
                        _EntityLanguage = Entity.LanguagesOptions.Item(_IdEntityLanguage);
                    }

                    return _EntityLanguage;
                }

                set { _EntityLanguage = value; }
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
                    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    lblOrganizationValue.Text = _organization.CorporateName;

                    //Inicializo el Form
                    if (EntityLanguage == null)
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.Facility;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Facility_LG_Title;
                lblFacility.Text = Resources.CommonListManage.Facility;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblLanguage.Text = Resources.CommonListManage.Language;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtFacility.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtFacility.Text = EntityLanguage.Name;
                txtDescription.Text = EntityLanguage.Description;
                lblLanguageValue.Text = EntityLanguage.Language.Name;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (EntityLanguage == null)
                    {
                        //Alta
                        EntityLanguage = Entity.LanguagesOptions.Add(_IdEntityLanguage, txtFacility.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        EntityLanguage = Entity.LanguagesOptions.Modify(_IdEntityLanguage, txtFacility.Text, txtDescription.Text);
                    }
                    base.NavigateBack();

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
