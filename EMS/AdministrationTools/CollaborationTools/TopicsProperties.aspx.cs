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
using Condesus.EMS.Business.CT.Entities;

namespace Condesus.EMS.WebUI.CT
{
    public partial class TopicsProperties : BaseProperties
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
        //private Int64 _IdTopic
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdTopic") ? base.NavigatorGetTransferVar<Int64>("IdTopic") : 0;
        //    }
        //}
        //private Topic _Entity = null;
        //private Topic Entity
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (_Entity == null)
        //                _Entity = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum).Category(_IdCategory).Topic(_IdTopic);

        //            return _Entity;
        //        }
        //        catch { return null; }
        //    }

        //    set { _Entity = value; }
        //}

        //private CompareValidator _CvCategory;
        //private RadComboBox _RdcCategory;

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
        //    AddComboCategory();
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
        //    _RdcCategory.SelectedValue = "IdCategory=" + _Entity.Category.IdCategory.ToString();
        //    chkIsLocked.Value = Entity.IsLocked;
        //    chkIsModerated.Value = Entity.IsModerated;
        //}
        //private void AddComboCategory()
        //{
        //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
        //    _params.Add("IdForum", _IdForum);
        //    String _selectedValue = "IdCategory=" + _IdIdCategory.ToString();// + "& IdForum=" + _IdForum.ToString();
        //    AddCombo(phCategory, ref _RdcCategory, Common.ConstantsEntitiesName.CT.Category, _selectedValue, _params, false, true, false, true);
        //    _RdcCategory.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcCategory_SelectedIndexChanged);

        //    ValidatorRequiredField(Common.ConstantsEntitiesName.CT.Categories, phCategoryValidator, ref _CvCategory, _RdcCategory, Resources.ConstantMessage.SelectACategory);
        //}

        //#endregion

        //#region Page Events
        //void _RdcCategory_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    _IdCategory = Convert.ToInt64(GetKeyValue(e.Value, "IdCategory"));
        //}
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //      Int64 _idCategory = Convert.ToInt64(GetKeyValue(_RdcCategory.SelectedValue, "IdCategory"));
        //        if (Entity == null)
        //        {
        //            //Alta
        //            Entity = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum).Category(_IdCategory).TopicsAdd(Global.DefaultLanguage.IdLanguage, txtName.Text, txtDescription.Text, chkIsLocked.Value, chkIsModerated.Value);
        //        }
        //        else
        //        {
        //            //Modificacion
        //            Entity = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).Forum(_IdForum).Category(_IdCategory).TopicsModify(Entity.IdTopic, Global.DefaultLanguage.IdLanguage, txtName.Text, txtDescription.Text, chkIsLocked.Value, chkIsModerated.Value);
        //        }

        //        base.NavigatorAddTransferVar("IdProcess", _IdProcess);
        //        base.NavigatorAddTransferVar("IdForum", _IdForum);
        //        base.NavigatorAddTransferVar("IdCategory", _IdCategory);
        //        base.NavigatorAddTransferVar("IdTopic", Entity.IdTopic);

        //        String _pkValues = "IdForum=" + _IdForum.ToString() + '&' + "IdCategory=" + Entity.IdCategory.ToString() + '&' + "IdTopic=" + Entity.IdTopic.ToString() + '&' + "IdProcess=" + _IdProcess.ToString();
        //        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

        //        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit);
        //        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.CT.Topic);
        //        base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
        //        Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Topic + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);

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
