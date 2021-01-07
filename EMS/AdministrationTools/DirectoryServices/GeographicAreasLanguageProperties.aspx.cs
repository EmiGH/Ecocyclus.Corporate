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

namespace Condesus.EMS.WebUI.DS
{
    public partial class GeographicAreasLanguageProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdGeographicArea") ? base.NavigatorGetTransferVar<Int64>("IdGeographicArea") : 0;
                }
            }
            private String _IdEntityLanguage
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
                }
            }
            private GeographicArea _Entity = null;
            private GeographicArea Entity
            {
                get
                {
                    if (_Entity == null)
                    {
                        _Entity = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdEntity);
                    }

                    return _Entity;
                }

                set { _Entity = value; }
            }
            private GeographicArea_LG _EntityLanguage = null;
            private GeographicArea_LG EntityLanguage
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
                    //Inicializo el Form
                    if (EntityLanguage == null)
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtGeographicAreas.Focus();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.GeographicArea;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.GeographicArea_LG_Title;
                lblGeographicAreas.Text = Resources.CommonListManage.GeographicArea;
                lblGeographicAreasDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtGeographicAreas.Text = String.Empty;
                txtGeoAreaDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtGeographicAreas.Text = EntityLanguage.Name;
                txtGeoAreaDescription.Text = EntityLanguage.Description;
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
                        EntityLanguage = Entity.LanguagesOptions.Add(_IdEntityLanguage, txtGeographicAreas.Text, txtGeoAreaDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        EntityLanguage = Entity.LanguagesOptions.Modify(_IdEntityLanguage, txtGeographicAreas.Text, txtGeoAreaDescription.Text);
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
