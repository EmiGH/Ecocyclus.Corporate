using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI
{
    #region Template for Rad Combo Box with RadTreeView
        //Con este template, se termina insertando el treeView dentro del UpdatePanel.
        class ComboWithTreeUpdatePanelTemplate : ITemplate
        {
            private RadTreeView _rtvMapTreeView;

            public ComboWithTreeUpdatePanelTemplate(RadTreeView rtvMapTreeView)
            {
                _rtvMapTreeView = rtvMapTreeView;
            }
            public void InstantiateIn(Control container)
            {
                container.Controls.Add(_rtvMapTreeView);
            }
        }
        //Con este template, se contruye un updatePanel y adentro del mismo se coloca el treeView
        //Esta hecho para usar el Combo con TreeView.
        class ComboWithTreeTemplate : ITemplate
        {
            private RadTreeView _rtvMapTreeView;
            private UpdatePanelUpdateMode _UpanelUpdateMode;
            private String _UpanelID;
            private PlaceHolder _PhComboWithTreeView;

            public ComboWithTreeTemplate(UpdatePanelUpdateMode upanelUpdateMode, RadTreeView rtvMapTreeView, String upanelID, PlaceHolder phComboWithTreeView)
            {
                _UpanelUpdateMode = upanelUpdateMode;
                _rtvMapTreeView = rtvMapTreeView; 
                _UpanelID = upanelID;
                _PhComboWithTreeView = phComboWithTreeView;
            }
            public void InstantiateIn(Control container)
            {
                //El treeView que se inserta dentro del Combo, debe estar contenido por un UPDATE PANEL.
                UpdatePanel _upComboWithTreeView = new UpdatePanel();
                _upComboWithTreeView.ID = _UpanelID;
                _upComboWithTreeView.UpdateMode = _UpanelUpdateMode;
                _upComboWithTreeView.ContentTemplate = new ComboWithTreeUpdatePanelTemplate(_rtvMapTreeView);
                
                //Meto el updatepanel dentrol del panel(DIV) y luego todo dentro del contenedor inicial
                _PhComboWithTreeView.Controls.Add(_upComboWithTreeView);
                container.Controls.Add(_PhComboWithTreeView);
            }
        }
    #endregion

    #region Template ColumnsClass for RadGrid
        public class MyTemplateSelection : ITemplate
        {
            protected HtmlImage imgSelButton;
            private string colname;

            public MyTemplateSelection(string cName)
            {
                colname = cName;
            }
            public void InstantiateIn(System.Web.UI.Control container)
            {
                imgSelButton = new HtmlImage();
                imgSelButton.ID = "selButton";
                imgSelButton.Attributes["class"] = "MenuGrid";
                //imgSelButton.Attributes["class"] = "DocumentGrid";
                imgSelButton.Src = "~/Skins/Images/Trans.gif";
                imgSelButton.Alt = "";
                container.Controls.Add(imgSelButton);
            }
        }
        public class MyTemplateReport : ITemplate
        {
            protected HtmlImage imgSelButton;
            private string colname;

            public MyTemplateReport(string cName)
            {
                colname = cName;
            }
            public void InstantiateIn(System.Web.UI.Control container)
            {
                imgSelButton = new HtmlImage();
                imgSelButton.ID = "rptButton";
                imgSelButton.Src = "~/Skins/Images/Trans.gif";
                 imgSelButton.Alt = "";
                 imgSelButton.Attributes["class"] = "DocumentGrid";
                container.Controls.Add(imgSelButton);
            }
        }
        public class MyTemplateLinkButton : ITemplate
        {
            protected ImageButton imageButton;
            private string colname;

            public MyTemplateLinkButton(string cName)
            {
                colname = cName;
            }
            public void InstantiateIn(System.Web.UI.Control container)
            {
                imageButton = new ImageButton();
                imageButton.ID = "imageButton";
                imageButton.CssClass = "MenuGrid";
                imageButton.ImageUrl = "~/Skins/Images/Trans.gif";
                imageButton.CommandName = "Select";
                container.Controls.Add(imageButton);
            }
        }
        public class MyTemplateSelectionItemCheck : ITemplate
        {
            protected CheckBox chkSelectItem;
            private string colname;

            public MyTemplateSelectionItemCheck(string cName)
            {
                colname = cName;
            }

            public void InstantiateIn(System.Web.UI.Control container)
            {
                chkSelectItem = new CheckBox();
                chkSelectItem.ID = "chkSelectItem";
                chkSelectItem.ToolTip = "Check Item";
                chkSelectItem.Width = Unit.Pixel(15);
                chkSelectItem.CausesValidation = false;

                container.Controls.Add(chkSelectItem);
            }
        }
        public class DBImageButtonTemplate : ITemplate
        {
            protected ImageButton _LinkButton;

            private String _Id;
            private String _ImageUrl = "~/Skins/Images/Trans.gif";
          

            public DBImageButtonTemplate(String id)
                : this(id, null)
            { }

            public DBImageButtonTemplate(String id, String imageUrl)
            {
                _Id = id;
                if (!String.IsNullOrEmpty(imageUrl))
                    _ImageUrl = imageUrl;
            }

            public void InstantiateIn(System.Web.UI.Control container)
            {
                _LinkButton = new ImageButton();
                _LinkButton.ID = _Id;
                _LinkButton.ImageUrl = _ImageUrl;
                _LinkButton.CssClass = "DocumentGrid";

                //_LinkButton.DataBinding += new EventHandler(_LinkButton_DataBinding);

                container.Controls.Add(_LinkButton);
            }

            //void _LinkButton_DataBinding(object sender, EventArgs e)
            //{
            //    //throw new NotImplementedException();
            //}
        }

        public class MyTemplateBinaryImage : ITemplate
        {
            protected RadBinaryImage rbiImage;
            private string colname;

            public MyTemplateBinaryImage(string cName)
            {
                colname = cName;
            }
            public void InstantiateIn(System.Web.UI.Control container)
            {
                rbiImage = new RadBinaryImage();
                rbiImage.ID = "rbiImage";
                rbiImage.Height = Unit.Pixel(180);
                rbiImage.Width = Unit.Pixel(220);
                rbiImage.AutoAdjustImageControlSize = true;
                rbiImage.ImageAlign = ImageAlign.Middle;
                rbiImage.ResizeMode = BinaryImageResizeMode.Fit;
                container.Controls.Add(rbiImage);
            }
        }

    #endregion



}
