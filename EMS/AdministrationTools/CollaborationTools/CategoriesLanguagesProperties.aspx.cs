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
//using Condesus.EMS.Business.CT.Entities;

namespace Condesus.EMS.WebUI.CT
{
    public partial class CategoriesLanguagesProperties : BaseProperties
    {
        //#region Internal Properties

        //private Int64 _IdForum
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdForum") ? base.NavigatorGetTransferVar<Int64>("IdForum") : 0;
        //    }
        //}
        //private Int64 _IdEntity
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdCategory") ? base.NavigatorGetTransferVar<Int64>("IdCategory") : 0;
        //    }
        //}
        //private String _IdEntityLanguage
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
        //    }
        //}

        //private Category _Entity = null;
        //private Category Entity
        //{
        //    get
        //    {
        //        if (_Entity == null)
        //        {
        //            _Entity = EMSLibrary.User.CollaborationTools.Configuration.Forum(_IdForum).Category(_IdEntity);
        //        }

        //        return _Entity;
        //    }

        //    set { _Entity = value; }
        //}
        //private Category_LG _EntityLanguage = null;
        //private Category_LG EntityLanguage
        //{
        //    get
        //    {
        //        if (_EntityLanguage == null)
        //        {
        //            _EntityLanguage = Entity.LanguagesOptions.Item(_IdEntityLanguage);
        //        }

        //        return _EntityLanguage;
        //    }

        //    set { _EntityLanguage = value; }
        //}

        //#endregion

        //#region PageLoad & Init

        //protected override void InitializeHandlers()
        //{
        //    base.InitializeHandlers();

        //    //Le paso el Save a la MasterContentToolbar
        //    EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
        //    FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
        //}

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    if (!Page.IsPostBack)
        //    {
        //        //Inicializo el Form
        //        if (EntityLanguage == null)
        //            Add();
        //        else
        //            LoadData(); //Edit.

        //        //Form
        //        base.SetContentTableRowsCss(tblContentForm);
        //        this.txtCategory.Focus();
        //    }
        //}

        ////Setea el Titulo de la Pagina
        ////(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        //protected override void SetPagetitle()
        //{
        //    base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : GetLocalResourceObject("PageDescription").ToString();
        //}

        //#endregion

        //#region Private Methods

        //private void Add()
        //{
        //    base.StatusBar.Clear();

        //    //Inicializa el Formulario
        //    lblIdValue.Text = String.Empty;
        //    txtCategory.Text = String.Empty;
        //    txtDescription.Text = String.Empty;
        //    lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
        //}
        //private void LoadData()
        //{
        //    //carga los datos en pantalla
        //    lblIdValue.Text = Entity.IdSalutationType.ToString();
        //    txtCategory.Text = EntityLanguage.Name;
        //    txtDescription.Text = EntityLanguage.Description;
        //    lblLanguageValue.Text = EntityLanguage.Language.Name;
        //}

        //#endregion

        //#region Page Events

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (EntityLanguage == null)
        //        {
        //            //Alta
        //            EntityLanguage = Entity.LanguagesOptions.Add(_IdEntityLanguage, txtCategory.Text, txtDescription.Text);
        //        }
        //        else
        //        {
        //            //Modificacion
        //            EntityLanguage = Entity.LanguagesOptions.Modify(_IdEntityLanguage, txtCategory.Text, txtDescription.Text);
        //        }
        //        base.NavigateBack();

        //        base.StatusBar.ShowMessage(Resources.Common.SaveOK);
        //    }
        //    catch (Exception ex)
        //    {
        //        base.StatusBar.ShowMessage(ex);
        //    }
        //}

        //#endregion
    }
}
