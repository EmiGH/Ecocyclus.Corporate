using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessResources
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal ProcessResources(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.ProcessResource Item(Int64 idProcess, Int64 idResource)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

            Entities.ProcessResource _processResource = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessResources_ReadById(idProcess, idResource);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_processResource == null)
                {
                    _processResource = new Condesus.EMS.Business.PF.Entities.ProcessResource(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdResource"]), Convert.ToString(_dbRecord["Comment"]), _Credential);
                }
                else
                {
                    return new Condesus.EMS.Business.PF.Entities.ProcessResource(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdResource"]), Convert.ToString(_dbRecord["Comment"]), _Credential);
                }
            }
            return _processResource;
        }
        internal List<Entities.ProcessResource> Items(PF.Entities.Process process)
        {
           //Objeto de data layer para acceder a datos
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

            //Coleccion para devolver las areas funcionales
            List<Entities.ProcessResource> _oItems = new List<Entities.ProcessResource>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessResources_ReadAll(process.IdProcess);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ProcessResource _processResource = new Condesus.EMS.Business.PF.Entities.ProcessResource(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdResource"]), Convert.ToString(_dbRecord["Comment"]), _Credential);
                //Lo agrego a la coleccion
                _oItems.Add(_processResource);
            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal Entities.ProcessResource Add(PF.Entities.Process process, KC.Entities.Resource resource, String value)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                _dbProcessesFramework.ProcessResources_Create(resource.IdResource, process.IdProcess, value, _Credential.User.Person.IdPerson);

                //Devuelvo el objeto FunctionalArea creado
                return new Entities.ProcessResource(process.IdProcess, resource.IdResource, value, _Credential);
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
        internal void Remove(PF.Entities.ProcessResource processResource)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Borrar de la base de datos
                _dbProcessesFramework.ProcessResources_Delete(processResource.Resource.IdResource,processResource.Process.IdProcess, _Credential.User.Person.IdPerson);
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
        #endregion


    }
}
