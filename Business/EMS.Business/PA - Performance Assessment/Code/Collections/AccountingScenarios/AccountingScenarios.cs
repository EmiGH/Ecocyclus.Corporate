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
    public class AccountingScenarios
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal AccountingScenarios(Credential credential) 
        {
            _Credential = credential;
            _Datasource = new AccountingScenariosRead.AccountigScenariosRoot(credential);
        }        


        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.AccountingScenario> Items()
        {
            Dictionary<Int64, Entities.AccountingScenario> _items = new Dictionary<Int64, Entities.AccountingScenario>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdScenario", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.AccountingScenario _accountingScenario = new Entities.AccountingScenario(Convert.ToInt64(_dbRecord["IdScenario"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_accountingScenario.IdScenario, _accountingScenario);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.AccountingScenario Item(Int64 idScenario)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.AccountingScenario _accountingScenario = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.AccountingScenarios_ReadById(idScenario, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdScenario", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.AccountingScenario(Convert.ToInt64(_dbRecord["IdScenario"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _accountingScenario;
        }
        #endregion


        #region Write Functions
        //Crea ForumForums
        internal Entities.AccountingScenario Create(String name, String description)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idScenario = _dbPerformanceAssessments.AccountingScenarios_Create();
            //alta del lg
            _dbPerformanceAssessments.AccountingScenarios_LG_Create(_idScenario, _Credential.DefaultLanguage.IdLanguage, name, description);
            //crea el objeto 
            Entities.AccountingScenario _accountingScenario = new Entities.AccountingScenario(_idScenario, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingScenarios", "AccountingScenarios", "Add", "IdScenario=" + _idScenario, _Credential.User.IdPerson);

            return _accountingScenario;

        }

        internal void Delete(Entities.AccountingScenario scenario)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Borra dependencias 
            scenario.Remove();

            _dbPerformanceAssessments.AccountingScenarios_Delete(scenario.IdScenario);


            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingScenarios", "AccountingScenarios", "Delete", "IdScenario=" + scenario.IdScenario, _Credential.User.IdPerson);

        }


        internal void Update(Entities.AccountingScenario scenario, String name, String description)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            _dbPerformanceAssessments.AccountingScenarios_LG_Update(scenario.IdScenario, scenario.Credential.DefaultLanguage.IdLanguage, name, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingScenarios", "AccountingScenarios", "Update", "IdScenario=" + scenario.IdScenario, _Credential.User.IdPerson);

        }

        #endregion
    }
}
