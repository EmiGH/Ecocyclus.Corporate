using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Condesus.EMS.WebUI.MasterControls
{
    public enum DockingZone
    {
        Top = 0,
        Right = 1,
        Bottom = 2,
        Left = 4
    }

    public enum DockingState
    {
        Docked = 0,
        UnDocked = 1
    }

    public class ToolbarMenuPanel : Panel
    {
        public DockingZone DockingZone
        {
            set { this.Attributes["docking-zone"] = value.ToString(); }
        }

        public Unit Top
        {
            set { this.Style["top"] = value.Value.ToString() + "px"; }
        }

        private Label _Title;
        public String Title
        {
            set 
            { 
                if(_Title != null)
                    _Title.Text = value; 
            }
        }

        #region Docking Functionality

        //private CheckBox _DockingNavigation;
        //public Boolean MenuDockNavigation
        //{
        //    get { return _DockingNavigation.Checked; }
        //    set { _DockingNavigation.Checked = value; }
        //}

        
        private ImageButton _DockingNavigation;
        public DockingState MenuDockNavigation
        {
            get { return (DockingState)Enum.Parse(typeof(DockingState), _DockingNavigation.Attributes["Docking"]); }
            set { 
                _DockingNavigation.Attributes["Docking"] = value.ToString();
                //_DockingNavigation.ImageUrl = Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/GlobalNavigator/" + value.ToString() + ".png";
                _DockingNavigation.CssClass = "globalNavigatorButton" + value.ToString();   //UnDocked";
            }
        }

        public String DockingNavigationOnClientClick
        {
            set { _DockingNavigation.Attributes["onclick"] = value; }
        }

        #endregion

        #region MenuContent

        private Panel _InnerMenuContainer;

        public void ClearMenuContent()
        {
            _InnerMenuContainer.Controls.Clear();
        }

        public void AddMenuContent(Control item)
        {
            if(item is WebControl)
                ((WebControl)item).Attributes["ParentMenuPanel"] = this.ID;
            
            _InnerMenuContainer.Controls.Add(item);
        }

        private Panel _InnerMenuHeaderToolBar;

        public void AddMenuHeaderToolbarContent(Control item)
        {
            _InnerMenuHeaderToolBar.Controls.Add(item);
        }
       
        #endregion

        

        public ToolbarMenuPanel(Boolean hasHeaderToolbar, Boolean dockNavigation)
        {
            this.Attributes["docking-zone"] = DockingZone.Left.ToString();
            this.Style["display"] = "none";
            this.Style["position"] = "absolute";
            this.CssClass = "GlobalNavigator";  // "ContentNavigator";

            if (hasHeaderToolbar)
            {
                Table _tbl = new Table();
                _tbl.CssClass = "Table";
                TableRow _tr = new TableRow();
                _tr.Style["display"] = "table-row";
                TableCell _tdDynamicContent = new TableCell();
                TableCell _tdDockingImage = new TableCell();

                _InnerMenuHeaderToolBar = new Panel();
                _InnerMenuHeaderToolBar.ID = "HeaderToolBar" + this.GetHashCode().ToString();
                _InnerMenuHeaderToolBar.CssClass = "headerToolBar";

                _tdDynamicContent.Style["width"] = "100%";
                _tdDynamicContent.Controls.Add(_InnerMenuHeaderToolBar);
                _tr.Cells.Add(_tdDynamicContent);

                //Docking Navigation
                _DockingNavigation = new ImageButton();
                _DockingNavigation.ID = "MenuDockingImage";
                _DockingNavigation.CausesValidation = false;
                _DockingNavigation.ImageUrl = "~/Skins/Images/Trans.gif";
                //_DockingNavigation.CssClass = "globalNavigatorButtonUnDocked";

                
                if (dockNavigation)
                {
                    _tdDockingImage.Controls.Add(_DockingNavigation);
                    _tr.Cells.Add(_tdDockingImage);
                }

                _tbl.Rows.Add(_tr);
                this.Controls.Add(_tbl);
            }
            else
            {
                _Title = new Label();
                _Title.ID = "title" + this.GetHashCode();
                _Title.CssClass = "Title";
                _Title.Text = string.Empty;

                this.Controls.Add(_Title);
            }

            _InnerMenuContainer = new Panel();
            _InnerMenuContainer.ID = this.GetHashCode().ToString();
            _InnerMenuContainer.CssClass = "innerContainer";
            
            this.Controls.Add(_InnerMenuContainer);

            ////Docking Navigation
            //_DockingNavigation = new CheckBox();
            //_DockingNavigation.ID = "dockingCheck";
            //_DockingNavigation.CausesValidation = false;

            //if (dockNavigation)
            //{
            //    //_DockingNavigation.Checked = dockNavigation.Value;
            //    this.Controls.Add(_DockingNavigation);
            //}
        }
    }
}
