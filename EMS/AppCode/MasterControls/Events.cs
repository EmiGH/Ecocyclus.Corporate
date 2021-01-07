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
    public delegate void Navigate(object sender, MasterFwNavigationEventArgs e);
    public delegate void NodeClick(object sender);


    public enum MasterFwSender
    {
        BreadCrumb = 0,
        Header = 1,
        GlobalNavigator = 2,
        ContentNavigator = 4,
        StatusBar = 8,
        ContextInfoPath = 16,
        OperativeDash = 32
    }

    public enum MasterFwContentToolbarAction
    {
        New = 0,
        Save = 1,
        Delete = 2,
        Return = 3,
        Next = 4
    }

    public class MasterFwNavigationEventArgs : EventArgs
    {
        private MasterFwSender _Sender;
        public MasterFwSender Sender
        {
            get { return _Sender; }
        }

        private String _Title;
        public String Title
        {
            get { return _Title; }
        }

        private String _Args;
        public String Args
        {
            get { return _Args; }
        }

        private Int32? _NavigatorIndex = null;
        public Int32? NavigatorIndex
        {
            get { return _NavigatorIndex; }
        }

        public MasterFwNavigationEventArgs(MasterFwSender sender, String title, String args)
        {
            _Sender = sender;
            _Args = args;
            _Title = title;
        }

        public MasterFwNavigationEventArgs(MasterFwSender sender, String title, String args, Int32 navigatorIndex)
            : this(sender, title, args)
        {
            _NavigatorIndex = navigatorIndex;
        }
    }

    class MasterFrameWorkException : Exception
    {
        public MasterFrameWorkException()
            : base()
        { }

        public MasterFrameWorkException(string message)
            : base(message)
        {

        }
        public MasterFrameWorkException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    //public class StrategyMasterFwNavigationEventArgsBuilder()
    //{
        
    //}
}
