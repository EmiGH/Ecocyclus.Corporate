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
    public class AccountingActivities
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal AccountingActivities(Credential credential) 
        {
            _Credential = credential;
            _Datasource = new AccountingActivitiesRead.AccountingActivityRoot(credential);
        }

        internal AccountingActivities(Entities.AccountingActivity accountingActivity)
        {
            _Credential = accountingActivity.Credential;
            _Datasource = new AccountingActivitiesRead.AccountingActivityByActivity(accountingActivity);
        }

        internal AccountingActivities(PF.Entities.ProcessTaskMeasurement processTaskMeasurement)
        {
            _Credential = processTaskMeasurement.Credential;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.AccountingActivity> Items()
        {
            Dictionary<Int64, Entities.AccountingActivity> _items = new Dictionary<Int64, Entities.AccountingActivity>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdActivity", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.AccountingActivity _accountingActivity = new Entities.AccountingActivity(Convert.ToInt64(_dbRecord["IdActivity"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentActivity"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_accountingActivity.IdActivity, _accountingActivity);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.AccountingActivity Item(Int64 idActivity)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.AccountingActivity _accountingActivity = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.AccountingActivities_ReadById(idActivity, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdActivity", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.AccountingActivity(Convert.ToInt64(_dbRecord["IdActivity"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentActivity"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _accountingActivity;
        }

        internal Decimal ReadTotalMeasurementResultByIndicator(Entities.AccountingScope scope, Entities.AccountingActivity activity, Entities.Indicator indicatorColumnGas, DateTime? startDate, DateTime? endDate)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Decimal _result = 0;

            DateTime _startdate = startDate == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue : (DateTime)startDate;
            DateTime _endDate = endDate == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue : (DateTime)endDate;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.AccountingActivities_ReadTotalMeasurementResultByIndicator(scope.IdScope, activity.IdActivity, indicatorColumnGas.IdIndicator, _startdate, _endDate);

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _result= Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["Result"],0.0));
            }
            return _result;
        }

        #endregion


        #region Write Functions
        //Crea ForumForums
        internal Entities.AccountingActivity Create(Entities.AccountingActivity parentActivity, String name, String description)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Int64 _idParentActivity = parentActivity == null ? 0 : parentActivity.IdActivity;
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idActivity = _dbPerformanceAssessments.AccountingActivities_Create(_idParentActivity);
            //alta del lg
            _dbPerformanceAssessments.AccountingActivities_LG_Create(_idActivity, _Credential.DefaultLanguage.IdLanguage, name, description);
            //crea el objeto 
            Entities.AccountingActivity _accountingActivity = new Entities.AccountingActivity(_idActivity, _idParentActivity, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingActivities", "AccountingActivities", "Add", "IdActivity=" + _idActivity, _Credential.User.IdPerson);

            return _accountingActivity;

        }
        internal void Delete(Entities.AccountingActivity activity)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Borra dependencias 
            activity.Remove();

            _dbPerformanceAssessments.AccountingActivities_Delete(activity.IdActivity);


            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingActivities", "AccountingActivities", "Delete", "IdActivity=" + activity.IdActivity, _Credential.User.IdPerson);

        }


        internal void Update(Entities.AccountingActivity activity, Entities.AccountingActivity parent, String name, String description)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Int64 _idParentActivity = parent == null ? 0 : parent.IdActivity;

            _dbPerformanceAssessments.AccountingActivities_Update(activity.IdActivity, _idParentActivity);

            _dbPerformanceAssessments.AccountingActivities_LG_Update(activity.IdActivity, activity.Credential.DefaultLanguage.IdLanguage, name, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_AccountingActivities", "AccountingActivities", "Update", "IdActivity=" + activity.IdActivity, _Credential.User.IdPerson);

        }

        #endregion

    }
}
