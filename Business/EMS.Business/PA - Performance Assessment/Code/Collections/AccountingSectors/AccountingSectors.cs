using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class AccountingSectors
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal AccountingSectors(Credential credential) 
        {
            _Credential = credential;
            _Datasource = new AccountingSectorsRead.AccountingSectorRoot(credential);
        }

        internal AccountingSectors(Entities.AccountingSector accountingSector)
        {
            _Credential = accountingSector.Credential;
            _Datasource = new AccountingSectorsRead.AccountingSectorBySector(accountingSector);
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.AccountingSector> Items()
        {
            Dictionary<Int64, Entities.AccountingSector> _items = new Dictionary<Int64, Entities.AccountingSector>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdSector", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.AccountingSector _accountingSector = new Entities.AccountingSector(Convert.ToInt64(_dbRecord["IdSector"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentSector"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_accountingSector.IdSector, _accountingSector);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.AccountingSector Item(Int64 idSector)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.AccountingSector _accountingSector = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.AccountingSectors_ReadById(idSector, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdSector", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.AccountingSector(Convert.ToInt64(_dbRecord["IdSector"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentSector"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _accountingSector;
        }
        #endregion

        #region Write Functions
        //Crea ForumForums
        internal Entities.AccountingSector Create(Entities.AccountingSector parentSector, String name, String description)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Int64 _idParentSector = parentSector == null ? 0 : parentSector.IdSector;
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idSector = _dbPerformanceAssessments.AccountingSectors_Create(_idParentSector);
            //alta del lg
            _dbPerformanceAssessments.AccountingSectors_LG_Create(_idSector, _Credential.DefaultLanguage.IdLanguage, name, description);
            //crea el objeto 
            Entities.AccountingSector _accountingSector = new Entities.AccountingSector(_idSector, _idParentSector, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingSectors", "AccountingSectors", "Add", "IdSector=" + _idSector, _Credential.User.IdPerson);

            return _accountingSector;

        }

        internal void Delete(Entities.AccountingSector sector)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Borra dependencias 
            sector.Remove();

            _dbPerformanceAssessments.AccountingSectors_Delete(sector.IdSector);


            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingSectors", "AccountingSectors", "Delete", "IdSector=" + sector.IdSector, _Credential.User.IdPerson);

        }

        internal void Update(Entities.AccountingSector sector, Entities.AccountingSector parent, String name, String description)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Int64 _idParentSector = parent == null ? 0 : parent.IdSector;

            _dbPerformanceAssessments.AccountingSectors_Update(sector.IdSector, _idParentSector);

            _dbPerformanceAssessments.AccountingSectors_LG_Update(sector.IdSector, sector.Credential.DefaultLanguage.IdLanguage, name, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingSectors", "AccountingSectors", "Update", "IdSector=" + sector.IdSector, _Credential.User.IdPerson);

        }

        #endregion
    }
}
