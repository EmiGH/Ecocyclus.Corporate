using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap.Controls
{
    public delegate void GRCTabChanged (object sender, GRCTabEventArgs e);
    public delegate void GRCButtonClick (object sender, GRCButtonEventArgs e);
    public delegate void GRCGridMenuClick (object sender, GRCGridMenuEventArgs e);
    public delegate void GRCDropDownChange(object sender, GRCDropDownEventArgs e);

    public class GRCTabEventArgs : EventArgs
    {
        private String _SelectedTab;
        public String SelectedTab
        {
            get { return _SelectedTab; }
        }

        public GRCTabEventArgs(String selectedTab) : base ()
        {
            _SelectedTab = selectedTab;
        }
    }

    public class GRCButtonEventArgs : EventArgs
    {
        private String _Action;
        public String Action
        {
            get { return _Action; }
        }

        private String _BtnCommand;
        public String BtnCommand
        {
            get { return _BtnCommand; }
        }

        private Int64 _IdProcess;
        public Int64 IdProcess
        {
            get { return _IdProcess; }
        }

        public GRCButtonEventArgs(String action, String btnCommand, Int64 idProcess) : this(btnCommand, idProcess)
        {
            _Action = action;
        }

        public GRCButtonEventArgs(String btnCommand, Int64 idProcess) : base()
        {
            _BtnCommand = btnCommand;
            _IdProcess = idProcess;
        }
    }

    public class GRCGridMenuEventArgs : EventArgs
    {
        private String _Action;
        public String Action
        {
            get { return _Action; }
        }

        private Int64 _IdValue;
        public Int64 IdValue
        {
            get { return _IdValue; }
        }

        private String _ItemOptionSelected;
        public String ItemOptionSelected
        {
            get { return _ItemOptionSelected; }
        }

        private Int64 _IdTask;
        public Int64 IdTask
        {
            get { return _IdTask; }
        }

        private Int64 _IdExtendedProperty;
        public Int64 IdExtendedProperty
        {
            get { return _IdExtendedProperty; }
        }

        private Int64 _IdExtendedPropertyClassification;
        public Int64 IdExtendedPropertyClassification
        {
            get { return _IdExtendedPropertyClassification; }
        }
        private Int64 _IdResource;
        public Int64 IdResource
        {
            get { return _IdResource; }
        }
        private Int64 _IdResourceType;
        public Int64 IdResourceType
        {
            get { return _IdResourceType; }
        }

        public GRCGridMenuEventArgs(String action, Int64 idValue, String itemOptionSelected, Int64 idTask) : base()
        {
            _Action = action;
            _IdValue = idValue;
            _ItemOptionSelected = itemOptionSelected;
            _IdTask = idTask;
       }

        public GRCGridMenuEventArgs(String action, Int64 idExtendedProperty, Int64 idExtendedPropertyClassification) : base()
        {
            _Action = action;
            _IdExtendedProperty = idExtendedProperty;
            _IdExtendedPropertyClassification = idExtendedPropertyClassification;
        }
        //ProcessResources
        public GRCGridMenuEventArgs(Int64 idResource, Int64 idResourceType, String action)
            : base()
        {
            _Action = action;
            _IdResource = idResource;
            _IdResourceType = idResourceType;
        }
    }

    public class GRCDropDownEventArgs : EventArgs
    {
        private String _SelectedValue;
        public String SelectedValue
        {
            get { return _SelectedValue; }
        }

        public GRCDropDownEventArgs(String selectedValue) : base()
        {
            _SelectedValue = selectedValue;
        }
    }
}
