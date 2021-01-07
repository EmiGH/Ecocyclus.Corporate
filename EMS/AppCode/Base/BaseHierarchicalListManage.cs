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

using EBPA = Condesus.EMS.Business.PA.Entities;


namespace Condesus.EMS.WebUI
{
    //public class BaseHierarchicalListManage : BasePage
    public partial class BasePage : System.Web.UI.Page
    {
        #region Hierarchical List Manage
            protected RadTreeView BuildHierarchicalListManageContent(String entityGenericHierarchical, String skin)
            {
                RadTreeView _MasterTreeView = new RadTreeView();
                //Prepara la grilla...
                InitHierarchicalListManageGrid(_MasterTreeView, entityGenericHierarchical, skin);

                return _MasterTreeView;
            }
            /// <summary>
            /// Este metodo arma el nodo para el tree
            /// </summary>
            /// <param name="drRecord">Indica el registro para insertar en el nodo</param>
            /// <returns>Un<c>RadTreeNode</c></returns>
            protected RadTreeNode SetNodeTreeViewManage(DataRow drRecord, String entityID)
            {
                RadTreeNode _node = new RadTreeNode();

                _node.Text = Common.Functions.ReplaceIndexesTags(GetTextDisplayInTreeView(drRecord, entityID, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage));
                _node.Value = GetKeyValueToDisplay(drRecord, entityID);
                _node.Checkable = true;
                _node.ContextMenuID = "rmnSelection";

                //Por si no tiene este campo...
                try
                {
                    //Agrego el atributo PermissionType...
                    _node.Attributes.Add("PermissionType", drRecord["PermissionType"].ToString());
                }
                catch { }

                SetExpandMode(_node, entityID, false, false);

                return _node;
            }

            #region Propiedades del Tree View
                /// <summary>
                /// Metodo que arma las caracteristicas principales del TreeView
                /// </summary>
                /// <param name="rtvMasterHierarchicalListManage">Indica el control del TreeView sobre el cual se realiza la configuracion de las caracteristicas</param>
                private void InitHierarchicalListManageGrid(RadTreeView rtvMasterHierarchicalListManage, String entityGenericHierarchical, String skin)
                {
                    rtvMasterHierarchicalListManage.ID = "rtvMasterHierarchicalListManage" + entityGenericHierarchical;
                    rtvMasterHierarchicalListManage.EnableTheming = true;
                    rtvMasterHierarchicalListManage.EnableViewState = true;
                    rtvMasterHierarchicalListManage.EnableEmbeddedSkins = false;
                    rtvMasterHierarchicalListManage.CheckBoxes = true;
                    rtvMasterHierarchicalListManage.ShowLineImages = false;
                    //rtvMasterHierarchicalListManage.Width = Unit.Percentage(100);
                    rtvMasterHierarchicalListManage.AllowNodeEditing = false;
                    rtvMasterHierarchicalListManage.CausesValidation = false;
                    rtvMasterHierarchicalListManage.Skin = skin;
                }
            #endregion
        #endregion
    }
}
