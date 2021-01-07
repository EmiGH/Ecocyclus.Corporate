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
    //TODO: CONTEXT NAVIGATOR
    public class BreadCrumbItem
    {
        private const String _INDENTATION_VALUE = "&nbsp;&nbsp;&nbsp;";
        private String _ItemIndentation = String.Empty;
        public String Indentation
        {
            get { return _ItemIndentation; }
        }

        private Int32 _ItemIndentationLevel = 0;
        public Int32 ItemIndentationLevel
        {
            get { return _ItemIndentationLevel; }
        }


        private Int32 _ItemIndex;
        public Int32 ItemIndex
        {
            get { return _ItemIndex; }
            set { _ItemIndex = value; }
        }

        private String _ItemArgs;
        public String ItemArgs
        {
            get { return _ItemArgs; }
            set { _ItemArgs = value; }
        }

        private String _ItemTitle;
        public String ItemTitle
        {
            get { return _ItemTitle; }
            set { _ItemTitle = value; }
        }

        private Boolean _IsCurrent;
        public Boolean IsCurrent
        {
            get { return _IsCurrent; }
        }

        private Boolean _IsContextInfoPathItem;
        public Boolean IsContextInfoPathItem
        {
            get { return _IsContextInfoPathItem; }
        }

        public BreadCrumbItem(Int32 itemIndex, String itemArgs, String itemTitle, Boolean isCurrent, Boolean isContextInfoPathItem)
        {
            _ItemIndex = itemIndex;
            _ItemArgs = itemArgs;
            _ItemTitle = itemTitle;
            _IsCurrent = isCurrent;
            _IsContextInfoPathItem = isContextInfoPathItem;
        }

        public void SetIndentation(Int32 indentationLevel)
        {
            _ItemIndentationLevel = indentationLevel;
            return;
            
            
            //OLD Para agregar &nbsp; - se reemplazo por paddings
            ////Si no esta Indentado, vuelve vacío
            //if (indentationLevel == 0)
            //    return;

            //for (var i = 0; i < indentationLevel; i++)
            //    _ItemIndentation = String.Concat(_ItemIndentation, _INDENTATION_VALUE);
        }
    }

    public class ContextInfoPathItem
    {
        private Int32 _ItemIndex;
        public Int32 ItemIndex
        {
            get { return _ItemIndex; }
            set { _ItemIndex = value; }
        }

        private String _ItemTitle;
        public String ItemTitle
        {
            get { return _ItemTitle; }
            set { _ItemTitle = value; }
        }

        private Boolean _IsCurrent;
        public Boolean IsCurrent
        {
            get { return _IsCurrent; }
        }

        //private String _ItemArgs;
        //public String ItemArgs
        //{
        //    get { return _ItemArgs; }
        //    set { _ItemArgs = value; }
        //}

        //public ContextInfoPathItem(Int32 itemIndex, String itemArgs, String itemTitle)
        public ContextInfoPathItem(Int32 itemIndex, String itemTitle, Boolean isCurrent)
        {
            _ItemIndex = itemIndex;
            //_ItemArgs = itemArgs;
            _ItemTitle = itemTitle;
            _IsCurrent = isCurrent;
        }
    }
}
