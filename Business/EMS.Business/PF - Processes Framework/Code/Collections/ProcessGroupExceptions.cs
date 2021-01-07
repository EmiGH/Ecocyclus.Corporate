using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessGroupExceptions
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

            internal ProcessGroupExceptions(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions

            internal Entities.ProcessGroupException Item(Int64 idProcess)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupExceptions_ReadById(idProcess, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                return new Entities.ProcessGroupException(idProcess, Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), _Credential);                          
            } 
            return null;
        }

            internal Entities.ProcessTask AssociatedTask(Int64 idProcess)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupExceptions_ReadAssociatedTask(idProcess);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return new Collections.ProcessTasks(_Credential).Item(Convert.ToInt16(_dbRecord["ProcessTask"]));
                }
                return null;
            }

            internal Entities.ProcessGroupException ItemByException(Int64 idException)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupExceptions_ReadByException(idException, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return new Entities.ProcessGroupException(Convert.ToInt64(_dbRecord["idProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), _Credential);
                }
                return null;
            }
        #endregion

      
        #region Write Function
            internal Entities.ProcessGroupException Add(Int16 weight, Int16 orderNumber, String title, String purpose, String description,
                    Int16 threshold)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();


                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idProcess = _dbProcessesFramework.Processes_Create(weight, orderNumber);

                    _dbProcessesFramework.Processes_LG_Create(_idProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                    _dbProcessesFramework.ProcessGroups_Create(_idProcess, threshold);

                    _dbProcessesFramework.ProcessGroupExceptions_Create(_idProcess);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PF_ProcessGroupExceptions", "ProcessGroupExceptions", "Create", "IdProcess=" + _idProcess, _Credential.User.IdPerson);
                        
                    //Devuelvo el objeto creado
                    return new Entities.ProcessGroupException(_idProcess, weight, orderNumber, _Credential.DefaultLanguage.IdLanguage, title, purpose, description, threshold, _Credential);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                    {
                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                    }
                    throw ex;
                }
            }
        #endregion

            internal void Remove(Entities.ProcessGroupException process)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Borra dependencias
                    process.Remove();

                    //Borrar de la base de datos
                    _dbProcessesFramework.ProcessGroupExceptions_Delete(process.IdProcess);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PF_ProcessGroupExceptions", "ProcessGroupExceptions", "Delete", "IdProcess=" + process.IdProcess, _Credential.User.IdPerson);
      
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                    {
                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                    }
                    throw ex;
                }
            }
            public void Modify(Entities.ProcessGroupException process, Int16 weight, Int16 orderNumber, String title, String purpose, String description, Int16 threshold)
            {

                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbProcessesFramework.Processes_Update(process.IdProcess, weight, orderNumber);

                    _dbProcessesFramework.Processes_LG_Update(process.IdProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                    _dbProcessesFramework.ProcessGroups_Update(process.IdProcess, threshold);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PF_ProcessGroupExceptions", "ProcessGroupExceptions", "Modify", "IdProcess=" + process.IdProcess, _Credential.User.IdPerson);                       

                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                    {
                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                    }
                    throw ex;
                }
            }

    }
}
