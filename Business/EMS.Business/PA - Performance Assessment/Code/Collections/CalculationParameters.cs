using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class CalculationParameters
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

            internal CalculationParameters(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions       
            internal Dictionary<Int64, Entities.CalculationParameter> Items(Int64 idCalculation)
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.CalculationParameter> _oItems = new Dictionary<Int64, Entities.CalculationParameter>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationParameters_ReadAll(idCalculation);
            
            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {

                //Declara e instancia  
                Entities.CalculationParameter _calculationParameter = new Entities.CalculationParameter(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["PositionParameter"]), Convert.ToInt64(_dbRecord["idMeasurementParameter"]), Convert.ToString(_dbRecord["ParameterName"]), _Credential);
                //Lo agrego a la coleccion
                _oItems.Add(_calculationParameter.PositionParameter, _calculationParameter);
            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal void Add(Entities.Calculation calculation, Int64 positionParameter, Entities.Measurement measurementParameter, String parameterName)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Ejecuta el insert 
                    _dbPerformanceAssessments.CalculationParameters_Create(calculation.IdCalculation, positionParameter, measurementParameter.IdMeasurement,parameterName);
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
            internal void Remove(Int64 idCalculation, Int64 positionParameter)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                  
                    //Borrar de la base de datos
                    _dbPerformanceAssessments.CalculationParameters_Delete(idCalculation, positionParameter, _Credential.User.Person.IdPerson);
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
