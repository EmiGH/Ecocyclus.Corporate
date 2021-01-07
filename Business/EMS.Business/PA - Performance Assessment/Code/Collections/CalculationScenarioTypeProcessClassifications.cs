using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class CalculationScenarioTypeProcessClassifications
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal CalculationScenarioTypeProcessClassifications(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            //Trae el tipo pedido
            internal Entities.CalculationScenarioTypeProcessClassification Item(Int64 idScenarioType, Int64 idProcessClassification)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypesProcessClassification_ReadById(idProcessClassification, idScenarioType);
                //si no trae nada retorno 0 para que no de error
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return new Entities.CalculationScenarioTypeProcessClassification(Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToInt64(_dbRecord["IdProcessClassification"]), _Credential);
                }
                return null;
            }
            //Trae todos los tipos
            internal List<Entities.CalculationScenarioTypeProcessClassification> ItemsByClassification(Int64 idProcessClassification)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Coleccion para devolver las areas funcionales
                List<Entities.CalculationScenarioTypeProcessClassification> _oItems = new List<Entities.CalculationScenarioTypeProcessClassification>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypesProcessClassification_ReadAll(idProcessClassification);

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {                    
                    //Declara e instancia una posicion
                    Entities.CalculationScenarioTypeProcessClassification _projectParticipation = new Entities.CalculationScenarioTypeProcessClassification(Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToInt64(_dbRecord["IdProcessClassification"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_projectParticipation);
                
                }
                return _oItems;
            }
            //Trae todos los tipos
            internal List<Entities.CalculationScenarioTypeProcessClassification> ItemsByType(Int64 idScenarioType)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Coleccion para devolver las areas funcionales
                List<Entities.CalculationScenarioTypeProcessClassification> _oItems = new List<Entities.CalculationScenarioTypeProcessClassification>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypesProcessClassification_ReadByType(idScenarioType);

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia una posicion
                    Entities.CalculationScenarioTypeProcessClassification _projectParticipation = new Entities.CalculationScenarioTypeProcessClassification(Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToInt64(_dbRecord["IdProcessClassification"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_projectParticipation);

                }
                return _oItems;
            }

        #endregion

        #region Write Functions
            internal Entities.CalculationScenarioTypeProcessClassification Add(Int64 idScenarioType, Int64 idProcessClassification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbPerformanceAssessments.CalculationScenarioTypesProcessClassification_Create(idProcessClassification, idScenarioType, _Credential.User.IdPerson);

                    return new Entities.CalculationScenarioTypeProcessClassification(idScenarioType, idProcessClassification, _Credential);
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
            internal void Remove(Int64 idScenarioType, Int64 idProcessClassification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.CalculationScenarioTypesProcessClassification_Delete(idProcessClassification, idScenarioType, _Credential.User.IdPerson);
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
            internal void Remove(Int64 idProcessClassification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.CalculationScenarioTypesProcessClassification_Delete(idProcessClassification, _Credential.User.IdPerson);
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
            internal void RemoveRelatedClassification(Int64 idScenarioType)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.CalculationScenarioTypesProcessClassification_DeleteRelatedClassification(idScenarioType, _Credential.User.IdPerson);
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
