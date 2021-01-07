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
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class ContactTypesProperties : BaseProperties
    {
        #region Internal Properties
            private ContactType _Entity = null;
            RadComboBox _RdcApplicabilityContactType;
            CompareValidator _CvApplicability;
            private Int64 _IdContactType
            {
                //TODO: Cuando se guarda este valor al TransferCollection desde la ListManage, 
                //se lo esta guardando como un String. Hay que guardarlo como Int64 (o en realidad como el Tipo de dato que es)
                //get { return base.NavigatorContainsTransferVar("IdContactType")?base.NavigatorGetTransferVar<Int64>("IdContactType"):0; }
                get
                {
                    return base.NavigatorContainsTransferVar("IdContactType") ? base.NavigatorGetTransferVar<Int64>("IdContactType") : 0;
                }
            }
            private Int64 _IdApplicability
            {
                get
                {
                    object _o = ViewState["IdApplicability"];
                    if(_o != null)
                        return (Int64)ViewState["IdApplicability"];

                    return 0;
                }

                set
                {
                    ViewState["IdApplicability"] = value;
                }
            }
            private ContactType Entity
            {
                get
                {
                    if (_Entity == null)
                        _Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactType(_IdApplicability,_IdContactType);

                    return _Entity;
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
                AddCombos();
                AddValidators();
                
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
                _IdApplicability = base.NavigatorContainsTransferVar("IdApplicability") ? base.NavigatorGetTransferVar<Int64>("IdApplicability") : 0;
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage).Name : Resources.CommonListManage.ContactType;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ContactType;
                lblApplicability.Text = Resources.CommonListManage.Applicability;
                lblDescription.Text = Resources.CommonListManage.Description;
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
                _RdcApplicabilityContactType.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
                _RdcApplicabilityContactType.SelectedValue = "IdApplicability=" + Entity.Applicability.ToString();
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.Applicabilities, phApplicabilityValidator, ref _CvApplicability, _RdcApplicabilityContactType, Resources.ConstantMessage.SelectAnApplicability);
            }
            private void AddCombos()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phApplicability, ref _RdcApplicabilityContactType, Common.ConstantsEntitiesName.DS.Applicabilities, String.Empty, _params, false, true, false, false, false);
            }
        #endregion
       
        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Int64 _idApplicability = Convert.ToInt64(GetKeyValue(_RdcApplicabilityContactType.SelectedValue, "IdApplicability"));

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactTypesAdd(_idApplicability, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactTypesModify(_idApplicability, Entity.IdContactType, txtName.Text, txtDescription.Text);
                    }
                    base.NavigatorAddTransferVar("IdContactType", Entity.IdContactType);
                    base.NavigatorAddTransferVar("IdApplicability", Entity.Applicability);

                    String _pkValues = "IdContactType=" + Entity.IdContactType.ToString() +
                        "& IdApplicability=" + Entity.Applicability.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.ContactType);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ContactType + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.ContactType, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
