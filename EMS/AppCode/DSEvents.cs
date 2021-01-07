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

namespace Condesus.EMS.WebUI.Controls
{
    public enum MenuType
    { 
        NaN = 0,
        GeneralOptions = 2,
        Selection = 4
    }

    public delegate void DSMenuItemClicked(object sender, DSMenuEventArgs e);

    public class DSMenuEventArgs : EventArgs
    {
        //private Int64 _Id;
        private String _Id;
        private Boolean _IsUpdate;
        private MenuType _MenuType;

        //public Int64 Id
        //{
        //    get { return _Id; }
        //}

        public String Id
        {
            get { return _Id; }
        }

        public Boolean IsUpdate
        {
            get { return _IsUpdate; }
        }

        public MenuType MenuType
        {
            get { return _MenuType; }
        }

        public DSMenuEventArgs(String id, Boolean isUpdate, MenuType menuType) : base()
        {
            _Id = id;
            _IsUpdate = isUpdate;
            _MenuType = menuType;
        }
    }
}
