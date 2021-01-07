using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Web.Script.Serialization;

using Telerik.Web.UI;
//using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class Dashboard : BaseDashboard
    {
//        #region Internal Properties

//        //RadMenu _RmnuGadgetToolbar;
//        RadMenu _RmnuGadgetToolbar;
//        //Store the info about the added docks in the session.
//        private List<DockState> CurrentDockStates
//        {
//            get
//            {
//                List<DockState> _currentDockStates = (List<DockState>)Session["CurrentDockStates"];
//                if (Object.Equals(_currentDockStates, null))
//                {
//                    _currentDockStates = new List<DockState>();
//                    Session["CurrentDockStates"] = _currentDockStates;
//                }
//                return _currentDockStates;

//            }
//            set
//            {
//                Session["CurrentDockStates"] = value;
//            }
//        }

//        #endregion

//        protected override void OnInit(EventArgs e)
//        {
//            base.OnInit(e);

//            ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboardOpen";

//            rdlDashboard.SaveDockLayout += new DockLayoutEventHandler(rdlDashboard_SaveDockLayout);
//            rdlDashboard.LoadDockLayout += new DockLayoutEventHandler(rdlDashboard_LoadDockLayout);

//            BuildGadgetMenuHeaderToolbar();
//            BuildContextMenu();
//            //RecreateMenu();
//            RecreateDocks();
//        }
//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);

//            //TitleIconURL
//            FwMasterPage.PageTitleIconURL = "/Skins/Images/Icons/Dashboard.png";
//            //LoadMenu();
//            if (!Page.IsPostBack)
//            {                
//                //LoadMenu();
//                LoadContextMenu();
//            }
//        }
//        protected override void SetPagetitle()

//        {
//            base.PageTitle = GetLocalResourceObject("PageTitle").ToString();
           
//        }
//        protected override void SetPageTileSubTitle()
//        {
//            base.PageTitleSubTitle = GetLocalResourceObject("PageTitleSubTitle").ToString();
//        }

//        #region Methods

//        private List<DockState> GetDocks()
//        {
//            List<DockState> _currentDockStates = new List<DockState>();

//            JavaScriptSerializer _serializer = new JavaScriptSerializer();
//            //'Get saved state string from the database
//            Dictionary<String,Condesus.EMS.Business.DS.Entities.Gadgets.Gadget> _gadgets = EMSLibrary.User.Dashboard.UserGadgetsSelected;

//            foreach (Condesus.EMS.Business.DS.Entities.Gadgets.Gadget _gadget in _gadgets.Values)
//            {
//                //if (_gadget.Configuration.Trim() != String.Empty)
//                //{
//                    DockState _state = _serializer.Deserialize<DockState>(_gadget.Configuration.Trim());
//                    //e.Positions[_state.UniqueName] = _state.DockZoneID;
//                    //e.Indices[_state.UniqueName] = _state.Index;
//                    _state.Left = Unit.Pixel(10);
//                    _state.Top = Unit.Pixel(10);
//                    _currentDockStates.Add(_state);
//                //}
//            }

//            return _currentDockStates;
//        }
//        private void RecreateDocks()
//        {
//            CurrentDockStates.Clear();
//            CurrentDockStates = GetDocks();
//            //rdlDashboard.Controls.Clear();
//            //Recreate the docks in order to ensure their proper operation
//            foreach (DockState _state in CurrentDockStates)
//            {
//                RadDock _dock = CreateRadDockFromState(_state);
//                //We will just add the RadDock control to the RadDockLayout.
//                // You could use any other control for that purpose, just ensure
//                // that it is inside the RadDockLayout control.
//                // The RadDockLayout control will automatically move the RadDock
//                // controls to their corresponding zone in the LoadDockLayout
//                // event (see below).
//                rdlDashboard.Controls.Add(_dock);
//                //We want to save the dock state every time a dock is moved.
//                CreateSaveStateTrigger(_dock);

//                LoadDock(_dock);
//            }
//        }
//        private void LoadDock(RadDock dock)
//        {
//            if (string.IsNullOrEmpty(dock.Tag))
//                return;

//            switch (dock.Tag)
//            {
//                case "TasksSummary":
//                    //if (EMSLibrary.User.Dashboard.TaskOperator.Count > 0)
//                    //{
//                        dock.ContentContainer.Controls.Add(base.BuildTasksSummary());
//                        dock.ForbiddenZones = new String[] { "rdzLeftTop" };
//                    //}
//                    break;
//                case "ExceptionsSummary":
//                    //if (EMSLibrary.User.Dashboard.ProcessRoleProcessLeader.Count>0)
//                    //{
//                        dock.ContentContainer.Controls.Add(base.BuildExceptionsSummary());
//                        dock.ForbiddenZones = new String[] { "rdzLeftTop" };
//                    //}
//                    break;
//                case "ProjectSummary":
//                    //if (EMSLibrary.User.Dashboard.ProcessRoleProcessLeader.Count > 0)
//                    //{
//                        dock.ContentContainer.Controls.Add(base.BuildProjectSummary());
//                        dock.ForbiddenZones = new String[] { "rdProyectBuyerPluggIn" };
//                    //}
//                    break;
//                case "NewsSummary":
//                        dock.ContentContainer.Controls.Add(base.BuildNewsSummary());
//                        dock.ForbiddenZones = new String[] { "rdzLeftTop" };
//                    break;
//                case "BuyerProjectSummary":
//                    //if (EMSLibrary.User.Dashboard.ProcessRoleBuyers.Count > 0)
//                    //{ BuildBuyerProjectSummaryContent
//                    dock.ContentContainer.Controls.Add(base.BuildProjectBuyerSummary());
//                    dock.ForbiddenZones = new String[] { "rdProyectBuyerPluggIn" };
//                    //dock.ContentContainer.Controls.Add(base.BuildBuyerProjectSummaryContent());
//                    //}
//                    break;
//            }
//        }
//        private RadDock CreateRadDockFromState(DockState state)
//        {
//            RadDock _dock = SetDock(state.UniqueName);
            
//            _dock.ApplyState(state);

//            return _dock;
//        }
//        private RadDock CreateRadDock(String value, String name)
//        {
//            String _uniqueName = Guid.NewGuid().ToString();
//            RadDock _dock = SetDock(_uniqueName);
//            _dock.UniqueName = _uniqueName;
//            _dock.Tag = value;
//            _dock.Title = name;
            
//            return _dock;
//        }
//        private RadDock SetDock(String uniqueName)
//        {
//            RadDock _dock = new RadDock();
//            _dock.DockMode = DockMode.Docked;
//            _dock.ID = string.Format("RadDock{0}", uniqueName);
//            _dock.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;

//            //_dock.Commands.Add(new DockCloseCommand());
//            //_dock.Commands.Add(new DockExpandCollapseCommand());
//            //_dock.Command += new DockCommandEventHandler(dock_Command);

//            return _dock;
//        }
//        private void CreateSaveStateTrigger(RadDock dock)
//        {
//            //Ensure that the RadDock control will initiate postback
//            // when its position changes on the client or any of the commands is clicked.
//            //Using the trigger we will "ajaxify" that postback.
//            dock.AutoPostBack = true;
//            dock.CommandsAutoPostBack = true;

//            AsyncPostBackTrigger saveStateTrigger = new AsyncPostBackTrigger();
//            saveStateTrigger.ControlID = dock.ID;
//            saveStateTrigger.EventName = "DockPositionChanged";
//            upDashboard.Triggers.Add(saveStateTrigger);

//            saveStateTrigger = new AsyncPostBackTrigger();
//            saveStateTrigger.ControlID = dock.ID;
//            saveStateTrigger.EventName = "Command";
//            upDashboard.Triggers.Add(saveStateTrigger);
//        }
//        private void RecreateMenu()
//        {
//            if (Page.IsPostBack)
//            {
//                LoadMenu();
//            }
//        }
//        //private void CreateSaveStateTriggerMenu(RadMenu menu)
//        private void CreateSaveStateTriggerMenu(RadMenu menu)
//        {
            
//            AsyncPostBackTrigger saveStateTrigger = new AsyncPostBackTrigger();
//            saveStateTrigger.ControlID = menu.ID;
//            saveStateTrigger.EventName = "ItemClick";
//            upDashboard.Triggers.Add(saveStateTrigger);
//        }
//        private void CreateSaveStateTriggerContextMenu(RadContextMenu menu)
//        {

//            AsyncPostBackTrigger saveStateTrigger = new AsyncPostBackTrigger();
//            saveStateTrigger.ControlID = menu.ID;
//            saveStateTrigger.EventName = "ItemClick";
//            upDashboard.Triggers.Add(saveStateTrigger);
//        }
//        #endregion
        
//        #region Page Events

//        protected void dock_Command(object sender, DockCommandEventArgs e)
//        {

//            if (e.Command.Name == "Close")
//            {
//                ScriptManager.RegisterStartupScript(
//                upDashboard,
//                this.GetType(),
//                "RemoveDock",
//                string.Format(@"function _removeDock() {{
//                        Sys.Application.remove_load(_removeDock);
//                        $find('{0}').undock();
//                        $get('{1}').appendChild($get('{0}'));
//                        $find('{0}').doPostBack('DockPositionChanged');
//                        }};
//                        Sys.Application.add_load(_removeDock);", ((RadDock)sender).ClientID, upDashboard.ClientID),
//                        true);

//            }
//        }
//        protected void _RmnuGadgetToolbar_ItemClick(object sender, RadMenuEventArgs e)
//        {
//            RadDock dock = CreateRadDock(e.Item.Value, e.Item.Text);

//            LoadDock(dock);
//            rdzLeftTop.Controls.Add(dock);

//            //In order to optimize the execution speed we are adding the dock to a 
//            // hidden update panel and then register a script which will move it
//            // to RadDockZone1 after the AJAX request completes. If you want to 
//            // dock the control in other zone, modify the script according your needs.
////            upDashboard.ContentTemplateContainer.Controls.Add(dock);

////            ScriptManager.RegisterStartupScript(
////                dock,
////                this.GetType(),
////                "AddDock",
////                string.Format(@"function _addDock() {{ 
////	            Sys.Application.remove_load(_addDock);
////	            $find('{1}').dock($find('{0}'));
////	            $find('{0}').doPostBack('DockPositionChanged');
////                }};
////                Sys.Application.add_load(_addDock);", dock.ClientID, rdzLeftTop.ClientID),
////                true);

//            CreateSaveStateTrigger(dock);
//            //RemoveMenuItem(e.Item.Value);
//        }
//        void rmnContextMenu_ItemClick(object sender, RadMenuEventArgs e)
//        {
//            RadDock dock = CreateRadDock(e.Item.Value, e.Item.Text);

//            LoadDock(dock);            
//            switch(dock.Tag)
//            {
//                case "ExceptionsSummary":
//                case "TasksSummary":
//                case "NewsSummary":
//                    dock.ForbiddenZones = new String[] { "rdzLeftTop" };
//                    rdProyectBuyerPluggIn.Controls.Add(dock);
//                    break;
//                case "ProjectSummary":
//                case "BuyerProjectSummary":
//                    dock.ForbiddenZones = new String[] { "rdProyectBuyerPluggIn" };
//                    rdzLeftTop.Controls.Add(dock);
//                    break;
//            }
//            //In order to optimize the execution speed we are adding the dock to a 
//            // hidden update panel and then register a script which will move it
//            // to RadDockZone1 after the AJAX request completes. If you want to 
//            // dock the control in other zone, modify the script according your needs.
//            //            upDashboard.ContentTemplateContainer.Controls.Add(dock);

//            //            ScriptManager.RegisterStartupScript(
//            //                dock,
//            //                this.GetType(),
//            //                "AddDock",
//            //                string.Format(@"function _addDock() {{ 
//            //	            Sys.Application.remove_load(_addDock);
//            //	            $find('{1}').dock($find('{0}'));
//            //	            $find('{0}').doPostBack('DockPositionChanged');
//            //                }};
//            //                Sys.Application.add_load(_addDock);", dock.ClientID, rdzLeftTop.ClientID),
//            //                true);

//            CreateSaveStateTrigger(dock);
//            //RemoveMenuItem(e.Item.Value);
//        }
//        protected void rdlDashboard_LoadDockLayout(object sender, DockLayoutEventArgs e)
//        {
//            CurrentDockStates.Clear();
//            CurrentDockStates = GetDocks();

//            foreach (DockState _state in CurrentDockStates)
//            {
//                e.Positions[_state.UniqueName] = _state.DockZoneID;
//                e.Indices[_state.UniqueName] = _state.Index;
//            }

            
//        }
//        protected void rdlDashboard_SaveDockLayout(object sender, DockLayoutEventArgs e)
//        {
//            SaveDocks();
//            CurrentDockStates = rdlDashboard.GetRegisteredDocksState(true);
//            //RemoveClosedDocks();
//            //LoadMenu();
//            LoadContextMenu();
            
//        }
//        private void SaveDocks()
//        {
//            String _dockState = String.Empty;
//            JavaScriptSerializer _serializer = new JavaScriptSerializer();
//            Dictionary<String,Condesus.EMS.Business.DS.Entities.Gadgets.Gadget> _gadgets = EMSLibrary.User.Dashboard.UserGadgetsAvailable;
//            Dictionary<String, Condesus.EMS.Business.DS.Entities.Gadgets.Gadget> _gadgetsSelected = new Dictionary<string,Condesus.EMS.Business.DS.Entities.Gadgets.Gadget>();

//            foreach (DockState _state in rdlDashboard.GetRegisteredDocksState(true))
//            {
//                foreach (Condesus.EMS.Business.DS.Entities.Gadgets.Gadget _gadget in _gadgets.Values)
//                {
//                    if (_state.Tag == _gadget.IdGadget)
//                    {
//                        //if (_state.Closed)
//                        //{
//                        //    _gadget.Configuration = String.Empty;
//                        //}
//                        //else
//                        //{
//                            _gadget.Configuration = _serializer.Serialize(_state);
//                            _gadgetsSelected.Add(_gadget.IdGadget, _gadget);
//                        //}
//                    }
//                }
//            }

//            EMSLibrary.User.Dashboard.SaveConfiguration(_gadgetsSelected);
//        }
//        private void RemoveClosedDocks()
//        {
//            List<DockState> _auxDockStates = CurrentDockStates;

//            for (int i = _auxDockStates.Count - 1; i >= 0; i--)
//            {
//                if (_auxDockStates[i].Closed)
//                {
//                    DockState _state = CurrentDockStates[i];
//                    CurrentDockStates.Remove(_state);
//                }
//            }
//        }
//        private void RemoveMenuItem(String item)
//        {
//            RadMenuItem _item = (RadMenuItem)(_RmnuGadgetToolbar.Items.FindItemByValue(item));
//            _RmnuGadgetToolbar.Items.Remove(_item);
//        }

//        #endregion

//        #region Menu

//        private void BuildGadgetMenuHeaderToolbar()
//        {
//            phGadgetToolbar.Controls.Clear();

//            //_RmnuGadgetToolbar = new RadMenu();
//            _RmnuGadgetToolbar = new RadMenu();
//            _RmnuGadgetToolbar.ID = "rmnuGadgetToolbar";
//            _RmnuGadgetToolbar.CausesValidation = false;
//            _RmnuGadgetToolbar.Skin = "EMS";
//            _RmnuGadgetToolbar.EnableEmbeddedSkins = false;
//            _RmnuGadgetToolbar.CollapseDelay = 0;
//            _RmnuGadgetToolbar.ExpandDelay = 0;
//            _RmnuGadgetToolbar.ItemClick += new RadMenuEventHandler(_RmnuGadgetToolbar_ItemClick);

//            CreateSaveStateTriggerMenu(_RmnuGadgetToolbar);

//            phGadgetToolbar.Controls.Add(_RmnuGadgetToolbar);

//            //FwMasterPage.RegisterContentAsyncPostBackTrigger(_RmnuGadgetToolbar, "ItemClick");
//        }
//        private void LoadMenu()
//        {
//            _RmnuGadgetToolbar.Items.Clear();

//            #region RootItem
//            RadMenuItem _radmnuItemRoot = new RadMenuItem();

//            _radmnuItemRoot.CssClass = "MnuItemRoot";
//            _radmnuItemRoot.ExpandedCssClass = "MnuItemRootOver";
//            _radmnuItemRoot.PostBack = false;

//            _radmnuItemRoot.Text = "Gadgets";
//            _RmnuGadgetToolbar.Items.Add(_radmnuItemRoot);
//            #endregion

//            LoadItems(_radmnuItemRoot);

//        }
//        private void BuildContextMenu()
//        {
//            rmnContextMenu.CausesValidation = false;
//            rmnContextMenu.Skin = "EMS";
//            rmnContextMenu.EnableEmbeddedSkins = false;
//            rmnContextMenu.CollapseDelay = 0;
//            rmnContextMenu.ExpandDelay = 0;
//            rmnContextMenu.ItemClick += new RadMenuEventHandler(rmnContextMenu_ItemClick);
//            CreateSaveStateTriggerContextMenu(rmnContextMenu);

//        }
//        //private void LoadContextMenu()
//        //{
//        //    rmnContextMenu.Items.Clear();
            
//        //    RadMenuItem _radmnuItem = new RadMenuItem();
//        //    _radmnuItem.PostBack = false;

//        //    _radmnuItem.Text = "Gadgets";
//        //    LoadItems(_radmnuItem);
//        //    rmnContextMenu.Items.Add(_radmnuItem);
//        //}
//        private void LoadContextMenu()
//        {
//            rmnContextMenu.Items.Clear();

//            RadMenuItem _radmnuItem = new RadMenuItem();
//            _radmnuItem.PostBack = false;

//            _radmnuItem.Text = "Gadgets";
//            LoadItems(_radmnuItem);
//            rmnContextMenu.Items.Add(_radmnuItem);
//        }
//        private void LoadItems(RadMenuItem radmnuItemRoot)
//        {
//            var _items = new Dictionary<String, String>();

//            Dictionary<String, Condesus.EMS.Business.DS.Entities.Gadgets.Gadget> _gadgetsAvailable = EMSLibrary.User.Dashboard.UserGadgetsAvailable;
//            Dictionary<String, Condesus.EMS.Business.DS.Entities.Gadgets.Gadget> _gadgetsSelected = EMSLibrary.User.Dashboard.UserGadgetsSelected;

//            try
//            {
//                //filtrar gadget ya agregados
//                foreach (Condesus.EMS.Business.DS.Entities.Gadgets.Gadget _gadget in _gadgetsAvailable.Values)
//                {
//                    if (!_gadgetsSelected.ContainsKey(_gadget.IdGadget))
//                    {
//                        _items.Add(_gadget.IdGadget, _gadget.Name);
//                    }
//                }

//                //foreach (DockState _state in CurrentDockStates)
//                //{
//                //    if (_items.ContainsKey(_state.Tag))
//                //    {
//                //        _items.Remove(_state.Tag);
//                //    }
//                //}
//            }
//            catch
//            {
//                //Falta el Recurso
//            }

//            RadMenuItem _radmnuItem;
//            foreach (var _item in _items)
//            {
//                _radmnuItem = new RadMenuItem();
//                _radmnuItem.Value = _item.Key;
//                _radmnuItem.Text = _item.Value;

//                //El Seleccionado no deberia hacer PostBack
//                //_radmnuItem.PostBack = (Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"] != _itemModule);
//                //Lo mismo para "marcar x Css" el selecionado
//                //TODO: Marcar el Modulo con Css de Seleccionado

//                radmnuItemRoot.Items.Add(_radmnuItem);
//            }
//        }

//        #endregion
    }
}

