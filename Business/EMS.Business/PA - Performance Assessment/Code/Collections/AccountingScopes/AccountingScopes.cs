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
    public class AccountingScopes
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal AccountingScopes(Credential credential) 
        {
            _Credential = credential;
            _Datasource = new AccountingScopesRead.AccountingScopesRoot(credential);
        }

        internal AccountingScopes(PF.Entities.ProcessTaskMeasurement processTaskMeasurement)
        {
            _Credential = processTaskMeasurement.Credential;
        }     

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.AccountingScope> Items()
        {
            Dictionary<Int64, Entities.AccountingScope> _items = new Dictionary<Int64, Entities.AccountingScope>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdScope", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.AccountingScope _accountingScope = new Entities.AccountingScope(Convert.ToInt64(_dbRecord["IdScope"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_accountingScope.IdScope, _accountingScope);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.AccountingScope Item(Int64 idScope)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.AccountingScope _accountingScope = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.AccountingScopes_ReadById(idScope, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdScope", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.AccountingScope(Convert.ToInt64(_dbRecord["IdScope"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _accountingScope;
        }
        #endregion

        #region Write Functions
        //Crea ForumForums
        internal Entities.AccountingScope Create(String name, String description)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idScope = _dbPerformanceAssessments.AccountingScopes_Create();
            //alta del lg
            _dbPerformanceAssessments.AccountingScopes_LG_Create(_idScope, _Credential.DefaultLanguage.IdLanguage, name, description);
            //crea el objeto 
            Entities.AccountingScope _accountingScope = new Entities.AccountingScope(_idScope, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingScopes", "AccountingScopes", "Add", "IdScope=" + _idScope, _Credential.User.IdPerson);

            return _accountingScope;

        }

        internal void Delete(Entities.AccountingScope scope)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Borra dependencias 
            scope.Remove();

            _dbPerformanceAssessments.AccountingScopes_Delete(scope.IdScope);


            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingScopes", "AccountingScopes", "Delete", "IdScope=" + scope.IdScope, _Credential.User.IdPerson);

        }


        internal void Update(Entities.AccountingScope scope, String name, String description)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            _dbPerformanceAssessments.AccountingScopes_LG_Update(scope.IdScope, scope.Credential.DefaultLanguage.IdLanguage, name, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingScopes", "AccountingScopes", "Update", "IdScope=" + scope.IdScope, _Credential.User.IdPerson);

        }

        #endregion
    }
}
