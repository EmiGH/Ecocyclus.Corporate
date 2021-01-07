using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    //internal class ProcessGroupNodes
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //    #endregion

    //    internal ProcessGroupNodes(Credential credential)
    //    {
    //        _Credential = credential;
    //    }

    //    #region Read Functions
    //    internal Entities.ProcessGroupNode Item(Int64 idProcess)
    //    {
    //        //Objeto de data layer para acceder a datos
    //        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

    //        IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupNodes_ReadById(idProcess, _Credential.CurrentLanguage.IdLanguage);
    //        foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //        {
    //            return new Entities.ProcessGroupNode(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToInt64(_dbRecord["IdParentProcess"]));                                                  
    //        }
    //        return null;
    //    }
    //    internal Dictionary<Int64, Entities.ProcessGroupNode> Items(Int64 idParentProcess)
    //    {
    //        //Coleccion para devolver los ExtendedProperty
    //        Dictionary<Int64, Entities.ProcessGroupNode> _oItems = new Dictionary<Int64, Entities.ProcessGroupNode>();

    //        //Objeto de data layer para acceder a datos
    //        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

    //        //Traigo los datos de la base
    //        IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupNodes_ReadByParent(idParentProcess, _Credential.CurrentLanguage.IdLanguage);

    //        //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
    //        Boolean _oInsert = true;

    //        //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //        foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //        {
    //            if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
    //            {
    //                ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
    //                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
    //                {
    //                    _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
    //                }
    //                else
    //                {
    //                    //No debe insertar en la coleccion ya que existe el idioma correcto.
    //                    _oInsert = false;
    //                }

    //                //Solo inserta si es necesario.
    //                if (_oInsert)
    //                {
    //                    //Declara e instancia  
    //                    Entities.ProcessGroupNode _processGroupNode = new Entities.ProcessGroupNode(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToInt64(_dbRecord["IdParentProcess"]));                                                  //Factories.NodesFactory.CreateNode(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToString(_dbRecord["Type"]));
    //                    //Lo agrego a la coleccion
    //                    _oItems.Add(_processGroupNode.IdProcess, _processGroupNode);
    //                }
    //                _oInsert = true;
    //            }
    //            else
    //            {   //Declara e instancia  
    //                Entities.ProcessGroupNode _processGroupNode = new Entities.ProcessGroupNode(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToInt64(_dbRecord["IdParentProcess"])); //Factories.NodesFactory.CreateNode(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToString(_dbRecord["Type"]));
    //                //Lo agrego a la coleccion
    //                _oItems.Add(_processGroupNode.IdProcess, _processGroupNode);
    //            }

    //        }
    //        return _oItems;
    //    }
    //    #endregion
                   
    //    #region Write Function
    //            internal Entities.ProcessGroupNode Add(Int16 weight, Int16 orderNumber, String title, String purpose, String description, 
    //                    Int16 threshold,PF.Entities.ProcessGroup parentProcess)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

    //                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                    Int64 _idProcess = _dbProcessesFramework.Processes_Create(weight, orderNumber);

    //                    _dbProcessesFramework.Processes_LG_Create(_idProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

    //                    _dbProcessesFramework.ProcessGroups_Create(_idProcess, threshold);

    //                    _dbProcessesFramework.ProcessGroupNodes_Create(_idProcess, parentProcess.IdProcess);

    //                    //Log
    //                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
    //                    _dbLog.Create("PF_ProcessGroupNodes", "ProcessGroupNodes", "Create", "IdProcess=" + _idProcess, _Credential.User.IdPerson);
                        
    //                    //Devuelvo el objeto creado
    //                    return new Entities.ProcessGroupNode(_idProcess, weight, orderNumber, _Credential.DefaultLanguage.IdLanguage, title, purpose, description, _Credential, threshold, parentProcess.IdProcess);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }

    //            internal void Modify(Entities.ProcessGroupNode processGroupNode, Int16 weight, Int16 orderNumber, String title, String purpose, String description, Int16 threshold, Entities.Process parentProcess)
    //            {

    //                //Objeto de data layer para acceder a datos
    //                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

    //                try
    //                {
    //                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                    _dbProcessesFramework.Processes_Update(processGroupNode.IdProcess, weight, orderNumber);

    //                    _dbProcessesFramework.Processes_LG_Update(processGroupNode.IdProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

    //                    _dbProcessesFramework.ProcessGroups_Update(processGroupNode.IdProcess, threshold);

    //                    _dbProcessesFramework.ProcessGroupNodes_Update(processGroupNode.IdProcess, parentProcess.IdProcess);

    //                    //Log
    //                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
    //                    _dbLog.Create("PF_ProcessGroupNodes", "ProcessGroupNodes", "Modify", "IdProcess=" + processGroupNode.IdProcess, _Credential.User.IdPerson);

    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }

    //            internal void Remove(Entities.ProcessGroupNode node)
    //            {
    //                try
    //                {
    //                    //Borra dependencias
    //                    node.Remove();
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
    //                    //Borrar el node
    //                    _dbProcessesFramework.ProcessGroupNodes_Delete(node.IdProcess);
    //                    //Borrar el process group
    //                    _dbProcessesFramework.ProcessGroups_Delete(node.IdProcess);
    //                    //Borrar el process 
    //                    _dbProcessesFramework.Processes_Delete(node.IdProcess);
    //                    //Log
    //                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
    //                    _dbLog.Create("PF_ProcessGroupNodes", "ProcessGroupNodes", "Delete", "IdProcess=" + node.IdProcess, _Credential.User.IdPerson);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }
    //        #endregion
        

    //}
}
