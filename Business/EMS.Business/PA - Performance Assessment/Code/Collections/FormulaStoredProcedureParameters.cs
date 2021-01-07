using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class FormulaStoredProcedureParameters
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal FormulaStoredProcedureParameters(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal List<Entities.FormulaStoredProcedureParameter> Items(String storedProcedureName)
            {
                //Coleccion para devolver los Stored Procedures.
                List<Entities.FormulaStoredProcedureParameter> _oItems = new List<Entities.FormulaStoredProcedureParameter>();

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.FormulaStoredProcedureParameters_ReadAll(storedProcedureName);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia  
                    Entities.FormulaStoredProcedureParameter _formulaStoredProcedureParameter = new Entities.FormulaStoredProcedureParameter(Convert.ToInt16(_dbRecord["ParameterOrder"]), Convert.ToString(_dbRecord["ParameterMode"]), Convert.ToString(_dbRecord["ParameterName"]), Convert.ToString(_dbRecord["DataType"]), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["CharacterLength"],-1)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["NumericPrecision"],-1)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["NumericScale"],-1)), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_formulaStoredProcedureParameter);
                }
                return _oItems;
            }
        #endregion
    }
}
