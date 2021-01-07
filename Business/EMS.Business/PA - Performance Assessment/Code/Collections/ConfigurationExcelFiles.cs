using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class ConfigurationExcelFiles
    {

    #region Internal Properties
            private Credential _Credential;
    #endregion

    internal ConfigurationExcelFiles(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.ConfigurationExcelFile Item(Int64 IdExcelFile)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.ConfigurationExcelFile _configuration = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ConfigurationExcelFiles_ReadById(IdExcelFile);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {

                _configuration = new Entities.ConfigurationExcelFile(Convert.ToInt64(_dbRecord["IdExcelFile"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["StartIndexOfDataRows"]), Convert.ToString(_dbRecord["StartIndexOfDataCols"]), Convert.ToBoolean(_dbRecord["IsDataRows"]), Convert.ToString(_dbRecord["IndexStartDate"]), Convert.ToString(_dbRecord["IndexEndDate"]), _Credential);
                
            }
            return _configuration;
        }
        /// <summary>
        /// Para el dashboard
        /// </summary>
        /// <param name="IdPerson"></param>
        /// <returns></returns>
            internal Dictionary<Int64, Entities.ConfigurationExcelFile> Items(Int64 IdPerson)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Coleccion para devolver los Indicator
                Dictionary<Int64, Entities.ConfigurationExcelFile> _oItems = new Dictionary<Int64, Entities.ConfigurationExcelFile>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ConfigurationExcelFiles_ReadByPerson(IdPerson);
                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia  
                    Entities.ConfigurationExcelFile _configuration = new Entities.ConfigurationExcelFile(Convert.ToInt64(_dbRecord["IdExcelFile"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["StartIndexOfDataRows"]), Convert.ToString(_dbRecord["StartIndexOfDataCols"]), Convert.ToBoolean(_dbRecord["IsDataRows"]), Convert.ToString(_dbRecord["IndexStartDate"]), Convert.ToString(_dbRecord["IndexEndDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_configuration.IdExcelFile, _configuration);
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ConfigurationExcelFile> Items()
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.ConfigurationExcelFile> _oItems = new Dictionary<Int64, Entities.ConfigurationExcelFile>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ConfigurationExcelFiles_ReadAll();

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                   //Declara e instancia  
                Entities.ConfigurationExcelFile _configuration = new Entities.ConfigurationExcelFile(Convert.ToInt64(_dbRecord["IdExcelFile"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["StartIndexOfDataRows"]), Convert.ToString(_dbRecord["StartIndexOfDataCols"]), Convert.ToBoolean(_dbRecord["IsDataRows"]), Convert.ToString(_dbRecord["IndexStartDate"]), Convert.ToString(_dbRecord["IndexEndDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_configuration.IdExcelFile, _configuration);               
            }
            return _oItems;
        }
            internal Dictionary<String, Entities.ConfigurationAsociationMeasurementExcelFile> Items(Entities.ConfigurationExcelFile configurationExcelFile)
            {
                //Coleccion para devolver los Indicator
                Dictionary<String, Entities.ConfigurationAsociationMeasurementExcelFile> _oItems = new Dictionary<String, Entities.ConfigurationAsociationMeasurementExcelFile>();

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ConfigurationExcelFiles_ReadByFile(configurationExcelFile.IdExcelFile);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Measurement _measurement = new Measurements(_Credential).Item(Convert.ToInt64(_dbRecord["IdMeasurement"]));
                    //Declara e instancia  
                    Entities.ConfigurationAsociationMeasurementExcelFile _configuration = new Entities.ConfigurationAsociationMeasurementExcelFile(_measurement, Convert.ToString(_dbRecord["IndexValue"]), Convert.ToString(_dbRecord["IndexDate"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_configuration.IndexValue, _configuration);
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.ConfigurationExcelFile Add(String Name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows, String IndexStartDate, String IndexEndDate, Dictionary<String, Entities.ConfigurationAsociationMeasurementExcelFile> measurements)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                   
                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _IdExcelFile = _dbPerformanceAssessments.ConfigurationExcelFiles_Create(Name, StartIndexOfDataRows, StartIndexOfDataCols, IsDataRows, IndexStartDate, IndexEndDate);

                        //asocia las mediciones
                        foreach (Entities.ConfigurationAsociationMeasurementExcelFile _measurement in measurements.Values)
                        {
                            _dbPerformanceAssessments.ConfigurationExcelFiles_CreateRelationship(_IdExcelFile, _measurement.Measurement.IdMeasurement, _measurement.IndexValue, _measurement.IndexDate);
                        }

                        // Completar la transacción
                        _transactionScope.Complete();
                        //Devuelvo el objeto creado
                        return new Entities.ConfigurationExcelFile(_IdExcelFile, Name, StartIndexOfDataRows, StartIndexOfDataCols, IsDataRows, IndexStartDate, IndexEndDate, _Credential);
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
            internal void Remove(Int64 idExcelFile)
            {
                try
                {
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                        _dbPerformanceAssessments.ConfigurationExcelFiles_DeleteRelationship(idExcelFile);
                        //Borrar de la base de datos la relacion con projects
                        _dbPerformanceAssessments.ConfigurationExcelFiles_Delete(idExcelFile);
                        
                        // Completar la transacción
                        _transactionScope.Complete();
                    }
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


            internal void Modify(Entities.ConfigurationExcelFile configurationExcelFile, String Name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows, String IndexStartDate, String IndexEndDate, Dictionary<String, Entities.ConfigurationAsociationMeasurementExcelFile> measurements)
            {
                try
                {
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                        //Modifico los datos de la base
                        _dbPerformanceAssessments.ConfigurationExcelFiles_Update(configurationExcelFile.IdExcelFile, Name, StartIndexOfDataRows, StartIndexOfDataCols, IsDataRows, IndexStartDate, IndexEndDate);                        

                        //borra todas las mediciones
                        _dbPerformanceAssessments.ConfigurationExcelFiles_DeleteRelationship(configurationExcelFile.IdExcelFile);

                        //asocia las mediciones
                        foreach (Entities.ConfigurationAsociationMeasurementExcelFile _measurement in measurements.Values)
                        {
                            _dbPerformanceAssessments.ConfigurationExcelFiles_CreateRelationship(configurationExcelFile.IdExcelFile, _measurement.Measurement.IdMeasurement, _measurement.IndexValue, _measurement.IndexDate);
                        }

                        // Completar la transacción
                        _transactionScope.Complete();
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
        #endregion

    }
}
