using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class CalculateOfTransformationParameters
    {
        private Entities.CalculateOfTransformation _CalculateOfTransformation;
        private Credential _Credential;

        internal CalculateOfTransformationParameters(Entities.CalculateOfTransformation calculateOfTransformation)
        {
            _CalculateOfTransformation = calculateOfTransformation;
            _Credential = calculateOfTransformation.Credential;
        }

        #region Read Functions
        internal Entities.CalculateOfTransformationParameter Item(String idParameter)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Entities.CalculateOfTransformationParameter _item = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformationParameters_ReadById(idParameter, _CalculateOfTransformation.IdTransformation);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.CalculateOfTransformationParameter(Convert.ToInt64(_dbRecord["IdTransformation"]), Convert.ToString(_dbRecord["IdParameter"]), Convert.ToInt64(_dbRecord["IdObject"]), Convert.ToString(_dbRecord["ClassName"]), _Credential);
            }
            return _item;
        }
        internal Dictionary<String, Entities.CalculateOfTransformationParameter> Items()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.CalculateOfTransformationParameter> _items = new Dictionary<String, Entities.CalculateOfTransformationParameter>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformationParameters_ReadAll(_CalculateOfTransformation.IdTransformation);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.CalculateOfTransformationParameter _item = new Entities.CalculateOfTransformationParameter(Convert.ToInt64(_dbRecord["IdTransformation"]), Convert.ToString(_dbRecord["IdParameter"]), Convert.ToInt64(_dbRecord["IdObject"]), Convert.ToString(_dbRecord["ClassName"]), _Credential);
                _items.Add(_item.IdParameter, _item);
            }
            return _items;
        }

        #endregion

        #region Write Functions
        internal Entities.CalculateOfTransformationParameter Add(String idParameter, IOperand operand)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                switch (operand.ClassName)
                {
                    case "Measurement":
                        _dbPerformanceAssessments.CalculateOfTransformationParameterMeasurements_Create(idParameter, _CalculateOfTransformation.IdTransformation, operand.IdObject);
                        //Log
                        _dbLog.Create("PA_CalculateOfTransformationParameterMeasurements", "CalculateOfTransformationParameters", "Add", "IdParameter='" + idParameter + "' and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson); 
                        break;
                    case "CalculationOfTransformation":
                        _dbPerformanceAssessments.CalculateOfTransformationParameterTransformations_Create(idParameter, _CalculateOfTransformation.IdTransformation, operand.IdObject);
                        //Log
                        _dbLog.Create("PA_CalculateOfTransformationParameterTransformations", "CalculateOfTransformationParameters", "Add", "IdParameter='" + idParameter + "' and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson); 
                        break;
                    case "Constant":
                        _dbPerformanceAssessments.CalculateOfTransformationParameterConstants_Create(idParameter, _CalculateOfTransformation.IdTransformation, operand.IdObject);
                        //Log
                        _dbLog.Create("PA_CalculateOfTransformationParameterConstants", "CalculateOfTransformationParameters", "Add", "IdParameter='" + idParameter + "' and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson); 
                        break;
                    default:
                        break;
                }                
                
                
                //Devuelvo el objeto creado
                Entities.CalculateOfTransformationParameter _item = new Entities.CalculateOfTransformationParameter(_CalculateOfTransformation.IdTransformation, idParameter, operand.IdObject, operand.ClassName, _Credential);

                return _item;
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
        internal void Modify(Entities.CalculateOfTransformationParameter parameter, String idParameter, IOperand operand)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                switch (operand.ClassName)
                {
                    case "Measurement":
                        _dbPerformanceAssessments.CalculateOfTransformationParameterMeasurements_Update(parameter.IdParameter, _CalculateOfTransformation.IdTransformation, operand.IdObject);
                        //Log
                        _dbLog.Create("PA_CalculateOfTransformationParameterMeasurements", "CalculateOfTransformationParameters", "Modify", "IdParameter='" + idParameter + "' and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson);
                        break;
                    case "CalculationOfTransformation":
                        _dbPerformanceAssessments.CalculateOfTransformationParameterTransformations_Update(parameter.IdParameter, _CalculateOfTransformation.IdTransformation, operand.IdObject);
                        //Log
                        _dbLog.Create("PA_CalculateOfTransformationParameterTransformations", "CalculateOfTransformationParameters", "Modify", "IdParameter='" + idParameter + "' and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson);
                        break;
                    case "Constant":
                        _dbPerformanceAssessments.CalculateOfTransformationParameterConstants_Update(parameter.IdParameter, _CalculateOfTransformation.IdTransformation, operand.IdObject);
                        //Log
                        _dbLog.Create("PA_CalculateOfTransformationParameterConstants", "CalculateOfTransformationParameters", "Modify", "IdParameter='" + idParameter + "' and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson);
                        break;
                    default:
                        break;
                }

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
        internal void Remove(Entities.CalculateOfTransformationParameter parameter)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                

                _dbPerformanceAssessments.CalculateOfTransformationParameterMeasurements_Delete(parameter.IdTransformation);
                _dbPerformanceAssessments.CalculateOfTransformationParameterTransformations_Delete(parameter.IdTransformation);
                
                _dbPerformanceAssessments.CalculateOfTransformationParameterConstants_Delete(parameter.IdTransformation);

                //Log
                //DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                //_dbLog.Create("PA_CalculateOfTransformationParameters", "CalculateOfTransformationParameters", "Remove", "IdParameter=" + parameter.IdParameter + " and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson);


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
        /// <summary>
        /// Borra los parametros por id de transformacion
        /// </summary>
        internal void Remove()
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                
                _dbPerformanceAssessments.CalculateOfTransformationParameterMeasurements_Delete(_CalculateOfTransformation.IdTransformation);
                _dbPerformanceAssessments.CalculateOfTransformationParameterTransformations_Delete(_CalculateOfTransformation.IdTransformation);
                _dbPerformanceAssessments.CalculateOfTransformationParameterConstants_Delete(_CalculateOfTransformation.IdTransformation);

                //Log
                //DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                //_dbLog.Create("PA_CalculateOfTransformationParameters", "CalculateOfTransformationParameters", "Remove", "IdParameter=" + parameter.IdParameter + " and IdTransformation= '" + _CalculateOfTransformation.IdTransformation + "'", _Credential.User.IdPerson);


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
