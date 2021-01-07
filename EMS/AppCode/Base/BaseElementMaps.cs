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
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
            //private RadTreeView _ElementMapTreeView = null;
            //private String _EntityNameMapClassification;
            //private String _EntityNameMapClassificationChildren;
            //private String _EntityNameMapElement;
            //private String _EntityNameMapElementChildren;
        #endregion

        #region External Properties
            //public String EntityNameMapClassification
            //{
            //    get { return _EntityNameMapClassification; }
            //    set { _EntityNameMapClassification = value; }
            //}
            //public String EntityNameMapClassificationChildren
            //{
            //    get { return _EntityNameMapClassificationChildren; }
            //    set { _EntityNameMapClassificationChildren = value; }
            //}
            //public String EntityNameMapElement
            //{
            //    get { return _EntityNameMapElement; }
            //    set { _EntityNameMapElement = value; }
            //}
            //public String EntityNameMapElementChildren
            //{
            //    get { return _EntityNameMapElementChildren; }
            //    set { _EntityNameMapElementChildren = value; }
            //}
            //public RadTreeView ElementMapTreeView
            //{
            //    get { return _ElementMapTreeView; }
            //    set { _ElementMapTreeView = value; }
            //    //get 
            //    //{
            //    //    if (Session["ElementMapTreeView"] == null)
            //    //    {
            //    //        Session["ElementMapTreeView"] = new RadTreeView();
            //    //    }
            //    //    return (RadTreeView)Session["ElementMapTreeView"]; 
            //    //}
            //    //set
            //    //{ Session["ElementMapTreeView"] = value; }

            //}
        #endregion

        #region Hierarchical Element Maps
            //protected RadTreeView BuildElementMapsContent(String entityNameMapClassification)
            protected RadTreeView BuildElementMapsContent(String entityNameMapClassification)
            {
                //RadTreeView _ElementMapTreeView = new RadTreeView();
                RadTreeView _ElementMapTreeView = new RadTreeView();
                //Prepara la grilla...
                InitElementMaps(_ElementMapTreeView, entityNameMapClassification);

                return _ElementMapTreeView;
            }
            /// <summary>
            /// Metodo publico que realiza la carga de un TreeView, con todo un Mapa (Clasificacion y Elemento), 
            /// utilizando los DataTables ya cargados.
            /// </summary>
            /// <param name="rtvTreeView">Indica el control del TreeView sobre el cual se realiza la carga</param>
            protected void LoadGenericTreeViewElementMap(ref RadTreeView rtvTreeView, String entityNameMapClassification, String entityNameMapElement, String singleEntityClassificationName, String singleEntityElementName, String contextInfoEntityClassificationName, String contextInfoEntityElementName, String contextElementMapEntityName)
            {
                rtvTreeView.Nodes.Clear();
                //Carga todas las clasificaciones, Inicialmente ROOTs
                if (DataTableListManage.ContainsKey(entityNameMapClassification))
                {
                    foreach (DataRow _drRecord in DataTableListManage[entityNameMapClassification].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, entityNameMapClassification, singleEntityClassificationName, Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, contextInfoEntityClassificationName, contextElementMapEntityName, String.Empty);
                        rtvTreeView.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        if (entityNameMapElement == Common.ConstantsEntitiesName.KC.ResourceCatalogues)
                            { SetExpandMode(_node, entityNameMapClassification, true, true); }
                        else
                        { SetExpandMode(_node, entityNameMapClassification, true, false); }
                    }
                }
                //Ahora carga todos los Elementos que se tengan que ver como ROOTs.
                if (DataTableListManage.ContainsKey(entityNameMapElement))
                {
                    foreach (DataRow _drRecord in DataTableListManage[entityNameMapElement].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, entityNameMapElement, singleEntityElementName, Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, contextInfoEntityElementName, contextElementMapEntityName, String.Empty);
                        rtvTreeView.Nodes.Add(_node);
                        //Los elementos no tienen hijos
                    }
                }
            }
            /// <summary>
            /// Metodo publico que realiza la carga de un TreeView, con todo un Mapa (Clasificacion y Elemento), 
            /// utilizando los DataTables ya cargados.
            /// </summary>
            /// <param name="rtvTreeView">Indica el control del TreeView sobre el cual se realiza la carga</param>
            protected void LoadGenericTreeViewElementMap(ref RadTreeNode rtvTreeViewNode, String entityNameMapClassification, String entityNameMapElement, String singleEntityClassificationName, String singleEntityElementName, String contextInfoEntityClassificationName, String contextInfoEntityElementName, String contextElementMapEntityName, Boolean allowElementChildren)
            {
                //rtvTreeView.Nodes.Clear();
                //Carga todas las clasificaciones, Inicialmente ROOTs
                if (DataTableListManage.ContainsKey(entityNameMapClassification))
                {
                    foreach (DataRow _drRecord in DataTableListManage[entityNameMapClassification].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, entityNameMapClassification, singleEntityClassificationName, Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, contextInfoEntityClassificationName, contextElementMapEntityName, String.Empty);
                        _node.Attributes["id"] = _node.ClientID;
                        _node.Attributes.Add("EntityType", "Classification");
                        rtvTreeViewNode.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, entityNameMapClassification, true, false);
                    }
                }
                //Ahora carga todos los Elementos que se tengan que ver como ROOTs.
                if (DataTableListManage.ContainsKey(entityNameMapElement))
                {
                    foreach (DataRow _drRecord in DataTableListManage[entityNameMapElement].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, entityNameMapElement, singleEntityElementName, Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, contextInfoEntityElementName, contextElementMapEntityName, String.Empty);
                        _node.Attributes["id"] = _node.ClientID;
                        _node.Attributes.Add("EntityType", "Element");
                        //Los elementos no tienen hijos
                        //Pero si la entidad que esta cargando es una org root con facility, entonces si debe cargar los hijos
                        //Por eso preguntamos por esta variable.
                        if (allowElementChildren)
                        {
                            SetExpandMode(_node, entityNameMapElement, true, false);
                        }
                        rtvTreeViewNode.Nodes.Add(_node);
                    }
                }
            }

            /// <summary>
            /// Este metodo arma el nodo para el tree para ElementMaps
            /// </summary>
            /// <param name="drRecord">Indica el registro para insertar en el nodo</param>
            /// <returns>Un<c>RadTreeNode</c></returns>
            internal RadTreeNode SetElementMapsNodeTreeView(DataRow drRecord, String entityID, String singleEntityName, Common.Constants.ExtendedPropertiesColumnDataTable displayIn, String contextInfoEntityName, String entityNameContextElement, String alternateEntityIDforIcon)
            {
                RadTreeNode _node = new RadTreeNode();

                _node.Text = Common.Functions.ReplaceIndexesTags(GetTextDisplayInTreeView(drRecord, entityID, displayIn));
                _node.Value = GetKeyValueToDisplay(drRecord, entityID);
                _node.Checkable = false;
                _node.PostBack = true;
                
                //Obtiene el EntityName limpio para buscarlo en el resource...
                String _entityNameforIcons;
                _entityNameforIcons = entityID.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                //Si viene el alternativo, uso ese....
                if (!String.IsNullOrEmpty(alternateEntityIDforIcon))
                {
                    _entityNameforIcons = alternateEntityIDforIcon.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                }
                _node.CssClass = (GetGlobalResourceObject("IconsByEntity", _entityNameforIcons) != null) ? GetGlobalResourceObject("IconsByEntity", _entityNameforIcons).ToString() : String.Empty;

                //Ruben, agregar la validacion del Estado que tiene el Resource....
                if (singleEntityName == Common.ConstantsEntitiesName.KC.Resource)
                {
                    try
                    {
                        if (drRecord["ResourceStatus"].ToString() == "Overdue")
                        {
                            _node.CssClass = "KCResourceOverdue";
                        }
                        else
                        {
                            if (drRecord["ResourceStatus"].ToString() == "OK")
                            {
                                _node.CssClass = "KCResourceWorking";
                            }
                        }
                    }
                    catch { }
                }

                //Setea el nombre de la entidad para poder llegar con un click al viewer de la entidad de ese registro.
                _node.Attributes.Add("SingleEntityName", singleEntityName);
                //Setea el nombre de la entidad ContextInformation, para este nodo
                _node.Attributes.Add("EntityNameContextInfo", contextInfoEntityName);
                _node.Attributes.Add("EntityNameContextElement", entityNameContextElement);

                try
                {
                    if (drRecord["PermissionType"] != null)
                    {
                        _node.Attributes.Add("PermissionType", drRecord["PermissionType"].ToString());
                    }
                }
                catch 
                {
                    //Si no existe la columna, asume que es view...no podra dar altas...
                    _node.Attributes.Add("PermissionType", Common.Constants.PermissionViewName);
                } 

                //_node.ContextMenuID = "rmnSelection";
                //SetExpandMode(_node, entityID);

                return _node;
            }
            internal RadTreeNode SetElementMapsNodeTreeView(String text, String value, String entityID, String singleEntityName, Common.Constants.ExtendedPropertiesColumnDataTable displayIn, String contextInfoEntityName, String entityNameContextElement, String alternateEntityIDforIcon, String permissionType)
            {
                RadTreeNode _node = new RadTreeNode();

                _node.Text = Common.Functions.ReplaceIndexesTags(text);
                _node.Value = value;
                _node.Checkable = false;
                _node.PostBack = true;

                //Obtiene el EntityName limpio para buscarlo en el resource...
                String _entityNameforIcons;
                _entityNameforIcons = entityID.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                //Si viene el alternativo, uso ese....
                if (!String.IsNullOrEmpty(alternateEntityIDforIcon))
                {
                    _entityNameforIcons = alternateEntityIDforIcon.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                }
                _node.CssClass = (GetGlobalResourceObject("IconsByEntity", _entityNameforIcons) != null) ? GetGlobalResourceObject("IconsByEntity", _entityNameforIcons).ToString() : String.Empty;

                //Setea el nombre de la entidad para poder llegar con un click al viewer de la entidad de ese registro.
                _node.Attributes.Add("SingleEntityName", singleEntityName);
                //Setea el nombre de la entidad ContextInformation, para este nodo
                _node.Attributes.Add("EntityNameContextInfo", contextInfoEntityName);
                _node.Attributes.Add("EntityNameContextElement", entityNameContextElement);
                _node.Attributes.Add("PermissionType", permissionType);

                return _node;
            }

            #region Propiedades del Tree View
                /// <summary>
                /// Metodo que arma las caracteristicas principales del TreeView
                /// </summary>
                /// <param name="rtvMasterHierarchicalListManage">Indica el control del TreeView sobre el cual se realiza la configuracion de las caracteristicas</param>
                private void InitElementMaps(RadTreeView rtvElementMaps, String entityNameMapClassification)
                {
                    rtvElementMaps.ID = "rtvElementMaps" + entityNameMapClassification;
                    //rtvElementMaps.EnableTheming = true;
                    rtvElementMaps.EnableViewState = true;
                    rtvElementMaps.CheckBoxes = false;
                    rtvElementMaps.ShowLineImages = true;
                    //rtvElementMaps.Width = Unit.Percentage(100);
                    rtvElementMaps.AllowNodeEditing = false;
                    rtvElementMaps.CausesValidation = false;
                    rtvElementMaps.Skin = "EMS";
                    rtvElementMaps.EnableEmbeddedSkins = false;
                    
                    //Crea los metodos del TreeView (Server).
                    //rtvElementMaps.NodeExpand += new RadTreeViewEventHandler(rtvElementMaps_NodeExpand);
                }
                ///// <summary>
                ///// Este metodo verifica y arma el Expand para el nodo.
                ///// </summary>
                ///// <param name="rtvNode">Indica el nodo a verificar y para asociarle mas hijos</param>
                //private void SetExpandMode(RadTreeNode rtvNode, String entityIDHasChildren)
                //{
                //    //Busca todos los KeyValues del nodo actual y verifica si tiene hijos.
                //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                //    _params = GetKeyValues(rtvNode.Value);

                //    if (entityIDHasChildren.Contains(Common.Constants.SubFixMethodHierarchicalChildren))
                //    {
                //        entityIDHasChildren = entityIDHasChildren.Replace(Common.Constants.SubFixMethodHierarchicalChildren, Common.Constants.SubFixMethodHierarchicalHasChildren);
                //    }
                //    else
                //    {
                //        entityIDHasChildren = entityIDHasChildren + Common.Constants.SubFixMethodHierarchicalHasChildren;
                //    }
                //    //Aca verifica si este nodo tiene hijos para asociarle en el arbol.
                //    if (HasChildren(entityIDHasChildren, _params))
                //    { rtvNode.ExpandMode = TreeNodeExpandMode.ServerSide; }
                //}
            #endregion

            #region Events
                //protected void rtvElementMaps_NodeExpand(object sender, RadTreeNodeEventArgs e)
                //{
                //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                //    _params = GetKeyValues(e.Node.Value);

                //    //Primero lo hace sobre las Clasificaciones Hijas...
                //    //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                //    BuildGenericDataTable(_EntityNameMapClassificationChildren, _params);
                //    if (DataTableListManage.ContainsKey(_EntityNameMapClassificationChildren))
                //    {
                //        foreach (DataRow _drRecord in DataTableListManage[_EntityNameMapClassificationChildren].Rows)
                //        {
                //            RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapClassificationChildren, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo);
                //            e.Node.Nodes.Add(_node);
                //            SetExpandMode(_node, _EntityNameMapClassificationChildren);
                //        }
                //    }
                    
                //    //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                //    BuildGenericDataTable(_EntityNameMapElementChildren, _params);
                //    if (DataTableListManage.ContainsKey(_EntityNameMapElementChildren))
                //    {
                //        foreach (DataRow _drRecord in DataTableListManage[_EntityNameMapElementChildren].Rows)
                //        {
                //            RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapElementChildren, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo);
                //            e.Node.Nodes.Add(_node);
                //            //Los elementos no tienen hijos
                //            //SetExpandMode(_node, _EntityNameMapElementChildren);
                //        }
                //    }
                //}
            #endregion

        #endregion

    }
}
