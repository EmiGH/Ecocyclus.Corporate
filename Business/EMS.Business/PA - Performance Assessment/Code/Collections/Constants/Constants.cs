using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Constants
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal Constants(Credential credential)
        {
            _Credential = credential;
        }

        internal Constants(Entities.Constant constant)
        {
            _Credential = constant.Credential;
        }
        internal Constants(Entities.Indicator indicator)
        {
            _Credential = indicator.Credential;
        }
        internal Constants(Entities.ConstantClassification constantClassification)
        {
            _Credential = constantClassification.Credential;
            _Datasource = new ConstantsRead.ConstantByConstantClassification(constantClassification);
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.Constant> Items()
        {
            Dictionary<Int64, Entities.Constant> _items = new Dictionary<Int64, Entities.Constant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdConstant", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Constant _constant = new Entities.Constant(Convert.ToInt64(_dbRecord["IdConstant"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["IdConstantClassification"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_constant.IdConstant, _constant);
            }
            return _items;
        }

        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.Constant Item(Int64 idConstant)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Constant _constant = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Constants_ReadById(idConstant, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdConstant", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.Constant(Convert.ToInt64(_dbRecord["IdConstant"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["IdConstantClassification"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _constant;
        }
        #endregion

        #region Write Functions
        //Crea ForumForums
        internal Entities.Constant Create(String symbol, Double value, Entities.MeasurementUnit measurementUnit, String name, String description, PA.Entities.ConstantClassification constantClassification)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Int64 _idConstant = _dbPerformanceAssessments.Constants_Create(symbol,value,measurementUnit.IdMeasurementUnit, constantClassification.IdConstantClassification);
            //alta del lg
            _dbPerformanceAssessments.Constants_LG_Create(_idConstant, _Credential.DefaultLanguage.IdLanguage, name, description);
            //crea el objeto 
            Entities.Constant _constant = new Entities.Constant(_idConstant, symbol, value, measurementUnit.IdMeasurementUnit, constantClassification.IdConstantClassification, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Constants", "Constants", "Add", "IdConstant=" + _idConstant, _Credential.User.IdPerson);

            return _constant;

        }

        internal void Delete(Entities.Constant constant)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {

                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Borra dependencias 
                constant.Remove();

                _dbPerformanceAssessments.Constants_Delete(constant.IdConstant);


                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                _dbLog.Create("PA_Constants", "Constants", "Delete", "IdConstant=" + constant.IdConstant, _Credential.User.IdPerson);
                _transactionScope.Complete();
            }
        }

        internal void Update(Entities.Constant constant, String symbol, Double value, Entities.MeasurementUnit measurementUnit, String name, String description, PA.Entities.ConstantClassification constantClassification)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            _dbPerformanceAssessments.Constants_Update(constant.IdConstant,symbol,value,measurementUnit.IdMeasurementUnit, constantClassification.IdConstantClassification);

            _dbPerformanceAssessments.Constants_LG_Update(constant.IdConstant, constant.Credential.DefaultLanguage.IdLanguage, name, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Constants", "Constants", "Update", "IdConstant=" + constant.IdConstant , _Credential.User.IdPerson);

        }

        #endregion
    }
}
