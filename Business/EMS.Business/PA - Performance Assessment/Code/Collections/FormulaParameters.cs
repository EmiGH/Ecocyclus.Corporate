using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class FormulaParameters
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal FormulaParameters(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions       
        internal Dictionary<Int64, Entities.FormulaParameter> Items(Int64 idFormula)
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.FormulaParameter> _oItems = new Dictionary<Int64, Entities.FormulaParameter>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.FormulaParameters_ReadAll(idFormula);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            //Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {

                //Declara e instancia  
                Entities.FormulaParameter _formulaParameter = new Entities.FormulaParameter(Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToInt64(_dbRecord["PositionParameter"]), Convert.ToInt64(_dbRecord["idIndicator"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToString(_dbRecord["ParameterName"]), _Credential);
                //Lo agrego a la coleccion
                _oItems.Add(_formulaParameter.PositionParameter, _formulaParameter);
            }
            return _oItems;
        }
        #endregion

        #region Write Functions
            internal void Add(Entities.Formula formula, Int64 positionParameter, Entities.Indicator indicator, Entities.MeasurementUnit measurementUnit, String parameterName)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Ejecuta el insert 
                    _dbPerformanceAssessments.FormulaParameters_Create(formula.IdFormula, positionParameter, indicator.IdIndicator, measurementUnit.IdMeasurementUnit,parameterName);
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
            internal void Remove(Int64 idFormula, Int64 positionParameter)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                  
                    //Borrar de la base de datos
                    _dbPerformanceAssessments.FormulaParameters_Delete(idFormula,positionParameter, _Credential.User.Person.IdPerson);
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
            //internal void Modify(Int64 idFormula, Int64 positionParameter, Int64 idIndicator, Int64 idMeasurementUnit, String parameterName)
            //{
            //    //Check for permission
            //    //Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "Write");
            //    try
            //    {
            //        //Objeto de data layer para acceder a datos
            //        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
            //        DataAccess.PA.Entities.FormulaParameters _dbFormulaParameters = _dbPerformanceAssessments.FormulaParameters;

            //        //Modifico los datos de la base
            //        _dbFormulaParameters.Update(idFormula, positionParameter, idIndicator, idMeasurementUnit, parameterName, _Credential.User.Person.IdPerson);
            //    }
            //    catch (SqlException ex)
            //    {
            //        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
            //        {
            //            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
            //        }
            //        throw ex;
            //    }
            //}

        #endregion
    }
}
