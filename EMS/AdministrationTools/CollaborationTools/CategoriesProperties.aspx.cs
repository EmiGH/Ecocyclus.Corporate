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
using Telerik.Web.UI;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.CT.Entities;

namespace Condesus.EMS.WebUI.CT
{
    public partial class CategoriesProperties : BaseProperties
    {
        //#region Internal Properties

        //private Int64 _IdProcess
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : 0;
        //    }
        //}
        //private Int64 _IdForum
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdForum") ? base.NavigatorGetTransferVar<Int64>("IdForum") : 0;
        //    }
        //}
        //private Int64 _IdCategory
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdCategory") ? base.NavigatorGetTransferVar<Int64>("IdCategory") : 0;
        //    }
        //}
        //private Category _Entity = null;
        //private Category Entity
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (_Entity == null)
        //                _Entity = ((ActiveForum)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum)).Category(_IdCategory);

        //            return _Entity;
        //        }
        //        catch { return null; }
        //    }

        //    set { _Entity = value; }
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
        //        if (Entity == null)
        //            Add();
        //        else
        //            LoadData(); //Edit.

        //        //Form
        //        base.SetContentTableRowsCss(tblContentForm);
        //        this.txtName.Focus();
        //    }
        //}

        ////Setea el Titulo de la Pagina
        ////(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        //protected override void SetPagetitle()
        //{
        //    base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : GetLocalResourceObject("PageDescription").ToString();
        //}

        //#endregion

        //#region Private Methods

        //private void Add()
        //{
        //    base.StatusBar.Clear();

        //    //Inicializa el Formulario
        //    lblIdValue.Text = String.Empty;
        //    txtName.Text = String.Empty;
        //    txtDescription.Text = String.Empty;
        //    lblLanguageValue.Text = Global.DefaultLanguage.Name;
        //}
        //private void LoadData()
        //{
        //    //carga los datos en pantalla
        //    lblIdValue.Text = Entity.IdCategory.ToString();
        //    txtName.Text = Entity.LanguageOption.Name;
        //    txtDescription.Text = Entity.LanguageOption.Description;
        //    lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
        //}

        //#endregion

        //#region Page Events
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Entity == null)
        //        {
        //            //Alta
        //            Entity = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum).CategoriesAdd(Global.DefaultLanguage.IdLanguage, txtName.Text, txtDescription.Text);
        //        }
        //        else
        //        {
        //            //Modificacion
        //            Entity = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum).CategoriesModify(Entity.IdCategory, Global.DefaultLanguage.IdLanguage, txtName.Text, txtDescription.Text);
        //        }

        //        base.NavigatorAddTransferVar("IdProcess", _IdProcess);
        //        base.NavigatorAddTransferVar("IdForum", _IdForum);
        //        base.NavigatorAddTransferVar("IdCategory", Entity.IdCategory);

        //        String _pkValues = "IdForum=" + _IdForum.ToString() + '&' + "IdCategory=" + Entity.IdCategory.ToString() + '&' + "IdProcess=" + _IdProcess.ToString();
        //        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

        //        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit);
        //        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.CT.Category);
        //        base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
        //        Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Category + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);

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
