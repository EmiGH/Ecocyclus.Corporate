using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class CalculationCertificates
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal CalculationCertificates(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.CalculationCertificated Item(Int64 idCalculation, Int64 idCertificated)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();                

                Entities.CalculationCertificated _calculationCertificated = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationCertificates_ReadById(idCalculation, idCertificated);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_calculationCertificated == null)
                    {
                        _calculationCertificated = new Entities.CalculationCertificated(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdCertificated"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToDecimal(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["IdOrganizationDOE"]), _Credential);
                        return _calculationCertificated;
                    }
                }
                return _calculationCertificated;
            }
            internal List<Entities.CalculationCertificated> ItemsByCalculation(PA.Entities.Calculation calculation)
            {               
                //Coleccion para devolver los Indicator
                List<Entities.CalculationCertificated> _oItems = new List<Entities.CalculationCertificated>();

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();                

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationCertificates_ReadByCalculation(calculation.IdCalculation);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia  
                    Entities.CalculationCertificated _calculationCertificated = new Entities.CalculationCertificated(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdCertificated"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToDecimal(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["IdOrganizationDOE"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_calculationCertificated);
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.CalculationCertificated Add(Entities.Calculation calculation, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE)
            {              
                try
                {
                    DateTime _dateLastResult = DateTime.MinValue;
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();                    
                
                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idCertificated = _dbPerformanceAssessments.CalculationCertificates_Create(calculation.IdCalculation, startDate, endDate, value, idOrganizationDOE, _Credential.User.Person.IdPerson);

                    //Devuelvo el objeto creado
                    return new Entities.CalculationCertificated(calculation.IdCalculation, _idCertificated, startDate, endDate, value, idOrganizationDOE, _Credential);
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
            internal void Remove(Entities.Calculation calculation, Int64 idCertificated)
            {
               
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();                    
                    
                    //Borrar de la base de datos el certificado...
                    _dbPerformanceAssessments.CalculationCertificates_Delete(calculation.IdCalculation, idCertificated, _Credential.User.Person.IdPerson);
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
            internal void Modify(Entities.Calculation calculation, Int64 idCertificated, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE)
            {
            
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();                    

                    //Modifico los datos de la base
                    _dbPerformanceAssessments.CalculationCertificates_Update(calculation.IdCalculation, idCertificated, startDate, endDate, value, idOrganizationDOE, _Credential.User.Person.IdPerson);
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
        #endregion

    }
}
