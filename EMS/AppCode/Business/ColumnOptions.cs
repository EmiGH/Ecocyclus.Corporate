using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI.Business
{
    /// <summary>
    /// Permite configurar los datos de las columnas para armar el DataTable.
    /// </summary>
    public class ColumnOptions
    {
        #region Internal Properties
            private Type _ColumnDataType = System.Type.GetType("System.String");
            private String _ColumnCaption = String.Empty;
            private Boolean _IsPrimaryKey = false;
            private Boolean _DisplayInManage = true;
            private Boolean _DisplayInCombo = true;
            private Boolean _IsSearchable = true;
            private Boolean _AllowNull = true;
            private Boolean _IsContextMenuCaption = false;
            private Boolean _IsCellLink = false;
            private Boolean _IsSortedBy = false;
            private Boolean _IsBinaryImage = false;

            private String _EntityName = String.Empty;
            private String _EntityNameGrid = String.Empty;
            private String _EntityNameContextInfo = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            private GridSortOrder _SortOrder = GridSortOrder.Ascending;
        #endregion

        internal ColumnOptions()
        {
        }
        internal ColumnOptions(String columnCaption, Type columnDataType, Boolean isPrimaryKey, Boolean displayInManage, 
            Boolean displayInCombo, Boolean isSearchable, Boolean allowNull,
            Boolean isContextMenuCaption, 
            Boolean isCellLink, String entityName, String entityNameGrid, String entityNameContextInfo, String entityNameContextElement,
            Boolean isSortedBy, Boolean isBinaryImage, GridSortOrder sortOrderType)
        {
            _ColumnCaption = columnCaption;
            _ColumnDataType = columnDataType;
            _IsPrimaryKey = isPrimaryKey;
            _DisplayInManage = displayInManage;
            _DisplayInCombo = DisplayInCombo;
            _IsSearchable = isSearchable;
            _AllowNull = allowNull;
            _IsContextMenuCaption = isContextMenuCaption;
            _IsCellLink = isCellLink;
            _EntityName = entityName;
            _EntityNameGrid = entityNameGrid;
            _EntityNameContextInfo = entityNameContextInfo;
            _EntityNameContextElement = entityNameContextElement;
            _IsSortedBy = isSortedBy;
            _IsBinaryImage = isBinaryImage;
            _SortOrder = sortOrderType;
        }

        #region External Properties
            public String ColumnCaption
            {
                get { return _ColumnCaption; }
                set { _ColumnCaption = value; }
            }
            public Type ColumnDataType
            {
                get { return _ColumnDataType; }
                set { _ColumnDataType = value; }
            }
            public Boolean IsPrimaryKey
            {
                get { return _IsPrimaryKey; }
                set { _IsPrimaryKey = value; }
            }
            public Boolean DisplayInManage
            {
                get { return _DisplayInManage; }
                set { _DisplayInManage = value; }
            }
            public Boolean DisplayInCombo
            {
                get { return _DisplayInCombo; }
                set { _DisplayInCombo = value; }
            }
            public Boolean IsSearchable
            {
                get { return _IsSearchable; }
                set { _IsSearchable = value; }
            }
            public Boolean AllowNull
            {
                get { return _AllowNull; }
                set { _AllowNull = value; }
            }
            public Boolean IsContextMenuCaption
            {
                get { return _IsContextMenuCaption; }
                set { _IsContextMenuCaption = value; }
            }
            public Boolean IsCellLink
            {
                get { return _IsCellLink; }
                set { _IsCellLink = value; }
            }
            public String EntityName
            {
                get { return _EntityName; }
                set { _EntityName = value; }
            }
            public String EntityNameGrid
            {
                get { return _EntityNameGrid; }
                set { _EntityNameGrid = value; }
            }
            public String EntityNameContextInfo
            {
                get { return _EntityNameContextInfo; }
                set { _EntityNameContextInfo = value; }
            }
            public String EntityNameContextElement
            {
                get { return _EntityNameContextElement; }
                set { _EntityNameContextElement = value; }
            }
            public Boolean IsSortedBy
            {
                get { return _IsSortedBy; }
                set { _IsSortedBy = value; }
            }
            public Boolean IsBinaryImage
            {
                get { return _IsBinaryImage; }
                set { _IsBinaryImage = value; }
            }
            public GridSortOrder SortOrder
            {
                get { return _SortOrder; }
                set { _SortOrder = value; }
            }
        #endregion
    }

}
