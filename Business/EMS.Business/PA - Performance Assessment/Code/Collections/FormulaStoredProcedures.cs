using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class FormulaStoredProcedures
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal FormulaStoredProcedures(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal List<Entities.FormulaStoredProcedure> Items(String storedProcedureName)
            {
                //Coleccion para devolver los Stored Procedures.
                List<Entities.FormulaStoredProcedure> _oItems = new List<Entities.FormulaStoredProcedure>();

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.FormulaStoredProcedures_ReadAll(storedProcedureName);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia  
                    Entities.FormulaStoredProcedure _formulaStoredProcedure = new Entities.FormulaStoredProcedure(Convert.ToString(_dbRecord["SPName"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_formulaStoredProcedure);
                }
                return _oItems;
            }
        #endregion

    }
}
