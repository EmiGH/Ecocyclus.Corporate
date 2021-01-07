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
    public partial class MagnitudesProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdMagnitud") ? base.NavigatorGetTransferVar<Int64>("IdMagnitud") : 0;
            }
        }
        private Magnitud _Entity = null;
        private Magnitud Entity
        {
            get
            {
                if (_Entity == null)
                {
                    _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_IdEntity);
                }

                return _Entity;
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
                    this.txtName.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Magnitud;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Magnitud;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtName.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = Entity.LanguageOption.Name;
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
                    Entity = EMSLibrary.User.PerformanceAssessments.Configuration.MagnitudAdd(txtName.Text);
                }
                else
                {
                    //Modificacion
                    EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_IdEntity).Modify(txtName.Text);
                }
                base.NavigatorAddTransferVar("IdMagnitud", Entity.IdMagnitud);

                String _pkValues = "IdMagnitud=" + Entity.IdMagnitud.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Magnitud);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Magnitud + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Magnitud, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

