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

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap
{
    public partial class ProcessGroupExceptionProperties : BasePM
    {
        #region Internal Properties
            private Int64 IdException
            {
                get { return Convert.ToInt64(ViewState["IdException"]); }
                set { ViewState["IdException"] = value.ToString(); }
            }
            private Int64 IdProcess
            {
                get { return Convert.ToInt64(ViewState["IdProcess"]); }
                set { ViewState["IdProcess"] = value.ToString(); }
            }
            private String ItemOptionSelected
            {
                get { return Convert.ToString(ViewState["ItemOptionSelected"]); }
                set { ViewState["ItemOptionSelected"] = value.ToString(); }
            }
        #endregion

        #region Load Information
        protected void Page_Init(object sender, EventArgs e)
        {
            InitializeHandlers();
            CheckSecurity();
        }
        private void InitializeHandlers()
        {
            rtsMainTab.TabClick += new RadTabStripEventHandler(rtsMainTab_TabClick);
            btnMainTabSave.Click += new EventHandler(btnMainTabSave_Click);
        }

        #region TabStrip
        void rtsMainTab_TabClick(object sender, RadTabStripEventArgs e)
        {
            _ActiveTab = e.Tab.Value;

            BuildTabStrip();
        }
        private void BuildTabStrip()
        {
            pnlTabContainer.Controls.Clear();
            _Process = EMSLibrary.User.ProcessFramework.Map.Process(IdProcess);

            if (SetTabStripState(rtsMainTab, btnMainTabSave, hdnMainTabState))
            {
                switch (_ActiveTab)
                {
                    case "ExtendedProperty":
                        BuildExtendedDataTabContent(pnlTabContainer);
                        break;
                    default:
                        BuildEmptyTab(pnlTabContainer);
                        break;
                }
            }
        }
        void btnMainTabSave_Click(object sender, EventArgs e)
        {
            switch (_ActiveTab)
            {
                case "ExtendedProperty":
                    SaveExecutionPropertyRels();
                    break;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnTransferAdd);

            if (!Page.IsPostBack)
            {
                base.SetNavigator();

                this.Title = "EMS - " + Resources.CommonListManage.ProcessTask;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                ItemOptionSelected = Convert.ToString(this.Context.Items["ItemOptionSelected"]);

                if (this.Context.Items["IdProcess"] != null)
                {
                    IdProcess = Convert.ToInt64(this.Context.Items["IdProcess"]);
                }

                IdException = Convert.ToInt64(this.Context.Items["IdException"]);

                Navigator();

                //Inicializa TabStrip
                _ActiveTab = "ExtendedProperty";
                rtsMainTab.SelectedIndex = 0;

                //LoadComboParentProcess();
                lblIdExceptionValue.Text = IdException.ToString();

                if (IdProcess == 0)
                {
                    //Esto es un add,
                    Add();
                }
                else if (ItemOptionSelected == "VIEW")
                { View(); }
                else
                { Edit(); }
            }

            BuildTabStrip();

            this.txtTitle.Focus();
        }
        private void CheckSecurity()
        {
            rmnGeneralOptions.Items.Clear();

            //if (!EMSLibrary.User.Security.Authorize("DirectoryServices", "GeographicAreas", "Add")) { throw new UnauthorizedAccessException(Resources.Common.PageNotAlowed); }

            //Boolean _deleteItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Remove");
            //Boolean _viewItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Item");
            //Boolean _editItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Modify");
            //Boolean _addItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Add");

            //Carga los menu en el inicio con el chequeo de seguridad
            //Menu de Opciones
            RadMenuItem ItemAdd = new RadMenuItem(Resources.Common.mnuAdd);
            ItemAdd.Value = "ItemAdd";
            Common.Functions.DoRadItemSecurity(ItemAdd, true);
            rmnGeneralOptions.Items.Add(ItemAdd);

            RadMenuItem ItemLanguage = new RadMenuItem(Resources.Common.mnuLanguage);
            ItemLanguage.Value = "ItemLanguage";
            Common.Functions.DoRadItemSecurity(ItemLanguage, true);
            rmnGeneralOptions.Items.Add(ItemLanguage);

            RadMenuItem ItemDelete = new RadMenuItem(Resources.Common.mnuDelete);
            ItemDelete.Value = "ItemDelete";
            Common.Functions.DoRadItemSecurity(ItemDelete, true);
            rmnGeneralOptions.Items.Add(ItemDelete);
        }
        private void LoadProcess()
        {
            Condesus.EMS.Business.PF.Entities.ProcessGroup _process = (Condesus.EMS.Business.PF.Entities.ProcessGroup)EMSLibrary.User.ProcessFramework.Map.Process(IdProcess);

            base.PageTitle = _process.LanguageOption.Title;

            txtTitle.Text = _process.LanguageOption.Title;
            txtPurpose.Text = _process.LanguageOption.Purpose;
            txtDescription.Text = _process.LanguageOption.Description;
            txtWeight.Text = _process.Weight.ToString();
            TxtThreshold.Text = _process.Threshold.ToString();
            lblIdValue.Text = IdProcess.ToString();
        }

        //private void LoadComboParentProcess()
        //{
        //    Condesus.EMS.Business.PF.Entities.ProcessGroupRootClassification _processRootClasif = (Condesus.EMS.Business.PF.Entities.ProcessGroupRootClassification)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess);

        //    ddlIdParentValue.Items.Add(new RadComboBoxItem(Common.Functions.RemoveIndexesTags(_processRootClasif.LanguageOption.Title), _processRootClasif.IdProcess.ToString()));

        //    foreach (var _childRoot in _processRootClasif.ChildrenRoot().Values)
        //        LoadComboChildProcessRoot(_childRoot, Convert.ToChar(160).ToString());
        //    foreach (var _childRoot in _processRootClasif.ChildrenNodes().Values)
        //        LoadComboChildProcessNode(_childRoot, Convert.ToChar(160).ToString());
        //}
        //private void LoadComboChildProcessRoot(Condesus.EMS.Business.PF.Entities.ProcessGroupRoot root, String tab)
        //{
        //    ddlIdParentValue.Items.Add(new RadComboBoxItem(tab + Common.Functions.RemoveIndexesTags(root.LanguageOption.Title), root.IdProcess.ToString()));

        //    foreach (var _childRoot in root.ChildrenRoot().Values)
        //        LoadComboChildProcessRoot(_childRoot, tab + tab);

        //    foreach (var _childNode in root.ChildrenNodes().Values)
        //        LoadComboChildProcessNode(_childNode, tab + tab);
        //}
        //private void LoadComboChildProcessNode(Condesus.EMS.Business.PF.Entities.ProcessGroupNode node, String tab)
        //{
        //    ddlIdParentValue.Items.Add(new RadComboBoxItem(tab + Common.Functions.RemoveIndexesTags(node.LanguageOption.Title), node.IdProcess.ToString()));

        //    foreach (var _childNode in node.ChildrenNodes().Values)
        //        LoadComboChildProcessNode(_childNode, tab + tab);
        //}
       
        private void Navigator()
        {
            String[,] _param = new String[,] { { "IdProcess", IdProcess.ToString() }, { "ItemOptionSelected", ItemOptionSelected.ToString() }, { "IdException", IdException.ToString() } };
            base.SetNavigator(_param);
        }
        #endregion

        #region Controls Action
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //Condesus.EMS.Business.PF.Entities.ProcessClassification _processClassification = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).ProcessClassification(IdProcessClassification);

                if (IdProcess == 0)
                {
                    //Condesus.EMS.Business.IA.Entities.Exception _exception = EMSLibrary.User.ImprovementAction.Configuration.Exception(IdException);
                    Condesus.EMS.Business.PF.Entities.ProcessGroupException _processGroupException = EMSLibrary.User.ImprovementAction.Configuration.Exception(IdException).ProcessGroupExceptionAdd(Convert.ToInt16(txtWeight.Text), 6666, txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToInt16(TxtThreshold.Text));

                    lblIdValue.Text = _processGroupException.IdProcess.ToString();
                    IdProcess = _processGroupException.IdProcess;
                }
                else
                {
                    Condesus.EMS.Business.PF.Entities.ProcessGroupException _processGroupException = ((Condesus.EMS.Business.PF.Entities.ProcessGroupException)EMSLibrary.User.ProcessFramework.Map.Process(IdProcess));
                    _processGroupException.Modify(Convert.ToInt16(txtWeight.Text), 6666, txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToInt16(TxtThreshold.Text));
                    //((Condesus.EMS.Business.PF.Entities.ProcessGroupException)EMSLibrary.User.ProcessFramework.Map.Process(IdProcess)).Modify(Convert.ToInt16(txtWeight.Text), txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToInt16(TxtThreshold.Text), _geographicFunctionalArea);

                }

                CheckSecurity();
                BuildTabStrip();
                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
            }
            catch (Exception ex)
            {
                base.StatusBar.ShowMessage(ex);
            }

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            base.Back();
        }

        protected void btnOkDelete_Click(object sender, EventArgs e)
        {
            if (IdProcess != 0)
            {
                try
                {
                    Condesus.EMS.Business.IA.Entities.Exception _exception = EMSLibrary.User.ImprovementAction.Configuration.Exception(IdException);
                    Condesus.EMS.Business.PF.Entities.ProcessGroupException _processGroupException = (Condesus.EMS.Business.PF.Entities.ProcessGroupException)EMSLibrary.User.ProcessFramework.Map.Process(IdProcess);
                    _exception.ProcessGroupExceptionRemove(_processGroupException);

                    //limpia los campos
                    lblIdValue.Text = String.Empty;
                    IdProcess = 0;

                    Add();

                    base.StatusBar.ShowMessage(Resources.Common.DeleteOK);

                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
                //oculta el popup de confirmacion del delete.
                this.mpelbDelete.Hide();

                BuildTabStrip();
            }
        }
        protected void btnTransferAdd_Click(object sender, EventArgs e)
        {
            Navigator();

            Context.Items.Add("IdProcess", IdProcess);
            Server.Transfer("~/ManagementTools/ProcessesMap/ProcessesLanguages.aspx");
        }
        private void DisableInsertData(Boolean bAction)
        {
            txtTitle.ReadOnly = bAction;
            txtPurpose.ReadOnly = bAction;
            txtDescription.ReadOnly = bAction;
            txtWeight.ReadOnly = bAction;

            if (bAction)
            { btnSave.Style.Add("display", "none"); }    //Esta todo en read only no se puede grabar
            else
            { btnSave.Style.Add("display", "block"); }   //esta habilitado para grabar.
        }



        private void Add()
        {

            txtTitle.Text = String.Empty;
            txtPurpose.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtWeight.Text = String.Empty;
            TxtThreshold.Text = String.Empty;

            DisableInsertData(false);

            ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = false; //Add
            ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = false; //Language
            ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = false; //Delete
        }
        private void Edit()
        {
            LoadProcess();
            DisableInsertData(false);


            ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = true; //Add
            ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = true; //Language
            ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = true; //Delete

        }
        private void View()
        {
            LoadProcess();
            DisableInsertData(true);

            ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = true; //Add
            ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = true; //Language
            ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = true; //Delete

        }
        #endregion

        #region Rad Menu
        protected void rmnOption_ItemClick(object sender, RadMenuEventArgs e)
        {
            switch (e.Item.ID)
            {
                case "m0": //ADD
                    IdProcess = 0;
                    lblLanguageValue.Text = Global.DefaultLanguage.Name;
                    ItemOptionSelected = "ADD";
                    txtTitle.Text = String.Empty;
                    txtPurpose.Text = String.Empty;
                    txtDescription.Text = String.Empty;
                    txtWeight.Text = String.Empty;
                    TxtThreshold.Text = String.Empty;

                    base.StatusBar.Clear();

                    ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = false; //Add
                    ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = false; //Language
                    ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = false; //Delete

                    BuildTabStrip();
                    break;

                case "m1":  //LANGUAGE  llama a la pagina de lenguajes
                    //Context.Items.Add("IdProcess", IdProcess);
                    ////Server.Transfer("~/ManagementTools/ProcessesMap/ProcessGroupRootClassificationLanguages.aspx");
                    //Server.Transfer("~/ManagementTools/ProcessesMap/ProcessesLanguages.aspx");
                    break;

                case "m2":  //DELETE    Esta opcion no debe hacer nada aca. Lo hace en el btnOKDelete_Click()
                    break;

            }
        }
        #endregion
    }
}
