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

namespace Condesus.EMS.WebUI.PF
{
    public partial class ProcessGroupNodeStandardProperties : BaseProperties
    {
        //#region Internal Properties
        //    private Int64 _IdParentProcess
        //    {
        //        get
        //        {
        //            return base.NavigatorContainsTransferVar("IdParentProcess") ? base.NavigatorGetTransferVar<Int64>("IdParentProcess") : Convert.ToInt64(GetPKfromNavigator("IdProcess")); //En el PK, lo busca como IdProcess, ya que viene con ese nombre en las variables...(para el ADD)
        //        }
        //    }
        //    private Int64 _IdEntity
        //    {
        //        get
        //        {
        //            return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : 0;
        //        }
        //    }
        //    private ProcessGroupNode _Entity = null;
        //    private ProcessGroupNode Entity
        //    {
        //        get
        //        {
        //            try
        //            {
        //                if (_Entity == null)
        //                    _Entity = (ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(_IdEntity);

        //                return _Entity;
        //            }
        //            catch
        //            {
        //                return null;
        //            }
        //        }
        //        set { _Entity = value; }
        //    }
        //#endregion

        //#region PageLoad & Init
        //    protected override void InitializeHandlers()
        //    {
        //        base.InitializeHandlers();
        //        //Le paso el Save a la MasterContentToolbar
        //        EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
        //        FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
        //    }
        //    protected override void OnInit(EventArgs e)
        //    {
        //        base.OnInit(e);
        //        LoadTextLabels();
        //    }
        //    protected override void OnLoad(EventArgs e)
        //    {
        //        base.OnLoad(e);
        //        //Setea el titulo del padre, puede ser un process o un nodo.
        //        lblIdParentValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(_IdParentProcess).LanguageOption.Title;
                
        //        if (!Page.IsPostBack)
        //        {
        //            //Inicializo el Form
        //            if (Entity == null)
        //                Add();
        //            else
        //                LoadData(); //Edit.

        //            //Form
        //            base.SetContentTableRowsCss(tblContentForm);
        //            lblLanguageValue.Text = Global.DefaultLanguage.Name;
        //            this.txtTitle.Focus();
        //        }
        //    }
        //    protected override void SetPagetitle()
        //    {
        //        base.PageTitle = (Entity != null) ? Entity.LanguageOption.Title : Resources.CommonListManage.ProcessGroupNode;
        //    }
        //    protected override void SetPageTileSubTitle()
        //    {
        //        base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        //    }
        //#endregion

        //#region Private Methods
        //    private void LoadTextLabels()
        //    {
        //        Page.Title = Resources.CommonListManage.ProcessGroupNode;
        //        lblDescription.Text = Resources.CommonListManage.Description;
        //        lblIdParent.Text = Resources.CommonListManage.Processes;
        //        lblLanguage.Text = Resources.CommonListManage.Language;
        //        lblOrder.Text = Resources.CommonListManage.OrderNumber;
        //        lblPurpose.Text = Resources.CommonListManage.Purpose;
        //        lblThreshold.Text = Resources.CommonListManage.Threshold;
        //        lblTitle.Text = Resources.CommonListManage.Title;
        //        lblWeight.Text = Resources.CommonListManage.Weight;
        //        rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
        //        rfvOrder.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
        //    }
        //    private void Add()
        //    {
        //        base.StatusBar.Clear();

        //        txtTitle.Text = String.Empty;
        //        txtOrder.Text = "0";
        //        txtPurpose.Text = String.Empty;
        //        txtDescription.Text = String.Empty;
        //        txtWeight.Text = String.Empty;
        //        txtThreshold.Text = String.Empty;
        //    }
        //    private void LoadData()
        //    {
        //        txtTitle.Text = _Entity.LanguageOption.Title;
        //        txtOrder.Text = _Entity.OrderNumber.ToString();
        //        txtPurpose.Text = _Entity.LanguageOption.Purpose;
        //        txtDescription.Text = _Entity.LanguageOption.Description;
        //        txtWeight.Text = _Entity.Weight.ToString();
        //        txtThreshold.Text = _Entity.Threshold.ToString();
        //    }
        //#endregion

        //#region Page Events
        //    protected void btnSave_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            if (Entity == null)
        //            {
        //                //Alta
        //                Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroup)EMSLibrary.User.ProcessFramework.Map.Process(_IdParentProcess)).AddNode(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text), txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToInt16(txtThreshold.Text));
        //            }
        //            else
        //            {
        //                //Modificacion
        //                Entity.Modify(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text), txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToInt16(txtThreshold.Text), _Entity.Parent);
        //                //base.NavigatorAddTransferVar("IdProcess", Entity.IdProcess);
        //                //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Title + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
        //            }

        //            base.NavigatorAddTransferVar("IdProcess", Entity.IdProcess);
        //            String _pkValues = "IdProcess=" + Entity.IdProcess.ToString();
        //            base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
        //            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
        //            base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
        //            base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
        //            //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ProcessGroupNode + " " + Entity.LanguageOption.Title, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
        //            String _entityPropertyName = String.Concat(Entity.LanguageOption.Title);
        //            NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessGroupNode, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

        //            base.StatusBar.ShowMessage(Resources.Common.SaveOK);
        //        }
        //        catch (Exception ex)
        //        {
        //            base.StatusBar.ShowMessage(ex);
        //        }
        //    }
        //#endregion
    }
}
