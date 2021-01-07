using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Condesus.EMS.WebUI.Business
{
    public partial class Collections : Base
    {
        #region Internal Properties
            protected DataColumn[] _DataColumnKeys = new DataColumn[] { };
        #endregion

        //public Collections(Condesus.EMS.Business.EMS emsLibrary, String commandName)
        public Collections(String commandName)
        {
            //_EMSLibrary = emsLibrary;
            _CommandName = commandName;
        }
        //public Collections Create(Condesus.EMS.Business.EMS emsLibrary, String commandName)
        public Collections Create(String commandName)
        {
            //return new Collections(emsLibrary, commandName);
            return new Collections(commandName);
        }

        #region Private Methods
            /// <summary>
            /// Crea el DataTable con el nombre de la entidad que se va a utilizar.
            /// </summary>
            /// <param name="tableName"></param>
            /// <returns>Un <c>DataTable</c></returns>
            protected DataTable BuildDataTable(String tableName)
            {
                //Crea la tabla principal.
                DataTable _dt = new DataTable();
                _dt.TableName = tableName;
                
                return _dt;
            }
            /// <summary>
            /// Crea la columna en el DataTable con las opciones indicadas
            /// </summary>
            /// <param name="dtMain">DataTable a utilizar</param>
            /// <param name="columnName">Nombre de la columna</param>
            /// <param name="columnOptions">Opciones de configuracion de la columna</param>
            protected void BuildColumnsDataTable(ref DataTable dtMain, String columnName, ColumnOptions columnOptions)
            {
                // Create a DataColumn and set various properties. 
                DataColumn _column = new DataColumn();
                _column.DataType = System.Type.GetType(columnOptions.ColumnDataType.ToString());
                _column.AllowDBNull = columnOptions.AllowNull;
                _column.ColumnName = columnName;
                _column.Caption = Common.Functions.ReplaceIndexesTags(columnOptions.ColumnCaption);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, columnOptions.DisplayInManage);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, columnOptions.DisplayInCombo);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.IsSearchable, columnOptions.IsSearchable);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.IsContextMenuCaption, columnOptions.IsContextMenuCaption);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.IsCellLink, columnOptions.IsCellLink);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.EntityName, columnOptions.EntityName);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.EntityNameGrid, columnOptions.EntityNameGrid);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.EntityNameContextInfo, columnOptions.EntityNameContextInfo);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.EntityNameContextElement, columnOptions.EntityNameContextElement);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.IsSortedBy, columnOptions.IsSortedBy);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.IsBinaryImage, columnOptions.IsBinaryImage);
                _column.ExtendedProperties.Add(Common.Constants.ExtendedPropertiesColumnDataTable.SortOrderType, columnOptions.SortOrder);

                //Inserta la columna definida en el DAtaTable.
                dtMain.Columns.Add(_column);
                
                //Como es primaryKey, entonces setea eso.
                if (columnOptions.IsPrimaryKey)
                {
                    Int64 _colPK = dtMain.PrimaryKey.Count();
                    // Add the _column to the table. 
                    _DataColumnKeys = (DataColumn[])RedimArray(_DataColumnKeys, _DataColumnKeys.Length + 1);
                    _DataColumnKeys[_colPK] = _column;
                    dtMain.PrimaryKey = _DataColumnKeys;
                }
            }
            /// <summary>
            /// Verifica que si dentro de los parametros no vengan los items por defecto del combo (selectitem, all, nodependency)
            /// </summary>
            /// <param name="parameters"></param>
            /// <returns>Un<c>Boolean</c></returns>
            protected Boolean ValidateSelectedItemComboBox(Dictionary<String, Object> parameters, ref Boolean showAll)
            {
                //Se pone en falso
                showAll = false;
                //Si no hay parametros, no se hace nada y retorna true.
                if ((parameters != null) && (parameters.Count == 1))
                {
                    //Si viene el NoDependency, retorna false...
                    if (parameters.Contains(Common.Constants.ComboBoxNoDependencyKeyValue))
                    {
                        return false;
                    }
                    //Si viene el SelectItem, retorna false...
                    if (parameters.Contains(Common.Constants.ComboBoxSelectItemKeyValue))
                    {
                        return false;
                    }
                    //Si viene el ShowAll, retorna true, pero con algo mas...
                    if (parameters.Contains(Common.Constants.ComboBoxShowAllKeyValue))
                    {
                        //Como el combo indica mostrar todo, lo pongo en true, y lo retorno, para que la coleccion haga lo suyo.
                        showAll = true;
                        return false;
                    }
                }
                return true;
            }
            /// <summary>
            /// Seteo de las columnas de propiedad y valor del viewer
            /// </summary>
            /// <param name="caption"></param>
            /// <param name="allowNull"></param>
            /// <returns></returns>
            protected ColumnOptions SetColumnViewer(String caption, Boolean allowNull)
            {
                ColumnOptions _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = caption;
                _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                _columnOptions.IsPrimaryKey = false;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = true;
                _columnOptions.IsSearchable = false;
                _columnOptions.AllowNull = allowNull;

                return _columnOptions;
            }
            /// <summary>
            /// Seteo de las columnas de propiedad y valor del viewer para link
            /// </summary>
            /// <param name="caption"></param>
            /// <param name="allowNull"></param>
            /// <returns></returns>
            protected ColumnOptions SetColumnLinkViewer()
            {
                ColumnOptions _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = "KeyValueLink";
                _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                _columnOptions.IsPrimaryKey = true;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = false;
                _columnOptions.IsSearchable = false;
                _columnOptions.AllowNull = false;
                
                return _columnOptions;
            }
            /// <summary>
            /// Seteo de las columnas de propiedad y valor del viewer para link
            /// </summary>
            /// <param name="caption"></param>
            /// <param name="allowNull"></param>
            /// <returns></returns>
            protected ColumnOptions SetColumnOrderViewer()
            {
                ColumnOptions _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = "ColumnOrderViewer";
                _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                _columnOptions.IsPrimaryKey = false;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = false;
                _columnOptions.IsSearchable = false;
                _columnOptions.AllowNull = false;
                _columnOptions.IsSortedBy = true;
                _columnOptions.SortOrder = Telerik.Web.UI.GridSortOrder.Ascending;

                return _columnOptions;
            }
            
            /// <summary>
            /// Seteo de la columna para que el viewer tenga un orden especifico.
            /// </summary>
            /// <param name="caption"></param>
            /// <param name="allowNull"></param>
            /// <returns></returns>
            protected ColumnOptions SetColumnInternalOrder()
            {
                ColumnOptions _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = "InternalOrder";
                _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                _columnOptions.IsPrimaryKey = true;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = false;
                _columnOptions.IsSearchable = false;
                _columnOptions.AllowNull = false;

                return _columnOptions;
            }

           
            /// <summary>
            /// Seteo de las columnas de propiedad y valor de lenguaje
            /// </summary>
            /// <param name="dt"></param>
            /// <param name="idEntity"></param>
            /// <param name="resourceEntity"></param>
            protected void SetColumnsLanguage(ref DataTable dt, String idEntity, String resourceEntity, Boolean description)
            {
                //Contruye las columnas y sus atributos.
                ColumnOptions _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = resourceEntity;
                _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                _columnOptions.IsPrimaryKey = true;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = false;
                _columnOptions.IsSearchable = false;
                _columnOptions.AllowNull = false;
                BuildColumnsDataTable(ref dt, idEntity, _columnOptions);

                _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = Resources.CommonListManage.IdLanguage;
                _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                _columnOptions.IsPrimaryKey = true;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = true;
                _columnOptions.IsSearchable = true;
                _columnOptions.AllowNull = false;
                BuildColumnsDataTable(ref dt, "IdLanguage", _columnOptions);

                _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = Resources.CommonListManage.LanguageName;
                _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                _columnOptions.IsPrimaryKey = false;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = true;
                _columnOptions.IsSearchable = true;
                _columnOptions.AllowNull = false;
                _columnOptions.IsContextMenuCaption = true;
                BuildColumnsDataTable(ref dt, "LanguageName", _columnOptions);

                _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = Resources.CommonListManage.Name;
                _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                _columnOptions.IsPrimaryKey = false;
                _columnOptions.DisplayInCombo = true;
                _columnOptions.DisplayInManage = true;
                _columnOptions.IsSearchable = true;
                _columnOptions.AllowNull = true;
                _columnOptions.IsContextMenuCaption = true; 
                BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                if (description)
                {
                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref dt, "Description", _columnOptions);
                }

                _columnOptions = new ColumnOptions();
                _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                _columnOptions.IsPrimaryKey = false;
                _columnOptions.DisplayInCombo = false;
                _columnOptions.DisplayInManage = false;
                _columnOptions.IsSearchable = false;
                _columnOptions.AllowNull = false;
                BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
            }

        #endregion
        
    }
}