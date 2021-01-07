using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Calculations
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal Calculations(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.Calculation Item(Int64 idCalculation)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Calculation _calculation = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Calculations_ReadById(idCalculation, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_calculation== null)
                {
                    _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _calculation;
                    }
                }
                else
                {
                    return new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                }
            }
            return _calculation;
        }
        internal Dictionary<Int64, Entities.Calculation> Items()
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.Calculation> _oItems = new Dictionary<Int64, Entities.Calculation>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Calculations_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdCalculation"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdCalculation"]));
                    }
                    else
                    {
                        //No debe insertar en la coleccion ya que existe el idioma correcto.
                        _oInsert = false;
                    }

                    //Solo inserta si es necesario.
                    if (_oInsert)
                    {
                        //Declara e instancia  
                        Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_calculation.IdCalculation, _calculation);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_calculation.IdCalculation, _calculation);
                }

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.Calculation> ItemsByProcess(PF.Entities.ProcessGroupProcess processGroupProcess)
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.Calculation> _oItems = new Dictionary<Int64, Entities.Calculation>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Calculations_ReadByProcess(processGroupProcess.IdProcess, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdCalculation"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdCalculation"]));
                    }
                    else
                    {
                        //No debe insertar en la coleccion ya que existe el idioma correcto.
                        _oInsert = false;
                    }

                    //Solo inserta si es necesario.
                    if (_oInsert)
                    {
                        //Declara e instancia  
                        Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_calculation.IdCalculation, _calculation);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_calculation.IdCalculation, _calculation);
                }

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.Calculation> ItemsByProcessIndicators(PF.Entities.ProcessGroupProcess processGroupProcess, Int64 idIndicator)
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.Calculation> _oItems = new Dictionary<Int64, Entities.Calculation>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();            

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Calculations_ReadByProcessIndicator(processGroupProcess.IdProcess, idIndicator, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdCalculation"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdCalculation"]));
                    }
                    else
                    {
                        //No debe insertar en la coleccion ya que existe el idioma correcto.
                        _oInsert = false;
                    }

                    //Solo inserta si es necesario.
                    if (_oInsert)
                    {
                        //Declara e instancia  
                        Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_calculation.IdCalculation, _calculation);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_calculation.IdCalculation, _calculation);
                }

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.Calculation> ItemsByFormula(Int64 idFormula)
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.Calculation> _oItems = new Dictionary<Int64, Entities.Calculation>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Calculations_ReadByFormula(idFormula, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdCalculation"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdCalculation"]));
                    }
                    else
                    {
                        //No debe insertar en la coleccion ya que existe el idioma correcto.
                        _oInsert = false;
                    }

                    //Solo inserta si es necesario.
                    if (_oInsert)
                    {
                        //Declara e instancia  
                        Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["LastResult"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["DateLastResult"], DateTime.MinValue)), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_calculation.IdCalculation, _calculation);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Calculation _calculation = new Entities.Calculation(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToInt64(_dbRecord["idTimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToDecimal(_dbRecord["LastResult"]), Convert.ToDateTime(_dbRecord["DateLastResult"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_calculation.IdCalculation, _calculation);
                }

            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal Entities.Calculation Add(Entities.Formula formula, Entities.MeasurementUnit measurementUnit, DateTime creationDate, String name, String description, Int16 frequency, PF.Entities.TimeUnit timeUnitFrequency, Dictionary<Int64, PF.Entities.ProcessGroupProcess> processGroupProcesses, DataTable parameters, Boolean isRelevant)
            {
                try
                {
                    //cargo variables que se que no van a venir en el add, como el result y date del result
                    Decimal _lastResult = 0;
                    DateTime _dateLastResult = DateTime.MinValue;
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                   
                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idCalculation = _dbPerformanceAssessments.Calculations_Create(formula.IdFormula, creationDate, measurementUnit.IdMeasurementUnit, frequency, timeUnitFrequency.IdTimeUnit, isRelevant, _Credential.User.Person.IdPerson);

                        _dbPerformanceAssessments.Calculations_LG_Create(_idCalculation, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

                        foreach (DataRow item in parameters.Rows)
                        {
                            _dbPerformanceAssessments.CalculationParameters_Create(_idCalculation, Convert.ToInt64(item["PositionParameter"]), Convert.ToInt64(item["IdMeasurementParameter"]), Convert.ToString(item["ParameterName"]));
                        }

                        foreach (PF.Entities.ProcessGroupProcess _process in processGroupProcesses.Values)
                        {
                            _dbPerformanceAssessments.Calculations_CreateCalculationProcessGroupPrjects(_idCalculation, _process.IdProcess);
                        }
                        // Completar la transacción
                        _transactionScope.Complete();
                        //Devuelvo el objeto creado
                        return new Entities.Calculation(_idCalculation, formula.IdFormula, creationDate, measurementUnit.IdMeasurementUnit, timeUnitFrequency.IdTimeUnit, frequency, _lastResult, _dateLastResult, name, description, isRelevant, _Credential);
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
        internal void Remove(Int64 idCalculation)
            {
                try
                {
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                        
                        //Borrar de la base de datos la relacion con projects
                        _dbPerformanceAssessments.Calculations_DeleteCalculationProcessGroupPrjects(idCalculation);
                        //borra todos los parametros para el calculo
                        _dbPerformanceAssessments.CalculationParameters_Delete(idCalculation);
                        //borra todas las opciones de lenguage para el calculo
                        _dbPerformanceAssessments.Calculations_LG_Delete(idCalculation);
                        //Borra todos los resultados historicos
                        _dbPerformanceAssessments.Calculations_DeleteHistoryResult(idCalculation);
                        //Borrar de la base de datos
                        _dbPerformanceAssessments.Calculations_Delete(idCalculation, _Credential.User.Person.IdPerson);
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
   
        //internal void AddResultHstory(Int64 idCalculation, DateTime timeStamp, Decimal result)
        //    {
        //        //Check for permission
        //        //Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "Write");
        //        try
        //        {      
        //                //Objeto de data layer para acceder a datos
        //                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
        //                DataAccess.PA.Entities.Calculations _dbCalculations = _dbPerformanceAssessments.Calculations;
                 
        //                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
        //                _dbCalculations.CreateHistoryResult(idCalculation, _IdOrganization, timeStamp , result, _Credential.User.Person.IdPerson);

        //        }
        //        catch (SqlException ex)
        //        {
        //            if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
        //            {
        //                throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
        //            }
        //            throw ex;
        //        }
        //    }
            //internal void Modify(Int64 idCalculation, Int64 idMeasurementUnit, String name, String description, Int16 Frequency, Int64 IdTimeUnitFrequency, Dictionary<Int64, PF.Entities.ProcessGroupProject> processGroupProjects)
            //{
            //    //Check for permission
            //    //Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "Write");
            //    try
            //    {
            //        using (TransactionScope _transactionScope = new TransactionScope())
            //        {
            //            //Objeto de data layer para acceder a datos
            //            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
            //            DataAccess.PA.Entities.Calculations _dbCalculations = _dbPerformanceAssessments.Calculations;
            //            DataAccess.PA.Entities.Calculations_LG _dbCalculations_LG = _dbPerformanceAssessments.Calculations_LG;
            //            DataAccess.PA.Entities.CalculationParameters _dbCalculationParameters = _dbPerformanceAssessments.CalculationParameters;

            //            //Modifico los datos de la base
            //            _dbCalculations.Update(idCalculation, _IdOrganization, idMeasurementUnit, Frequency, IdTimeUnitFrequency, _Credential.User.Person.IdPerson);

            //            _dbCalculations_LG.Update(idCalculation, _IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

            //            //borra todas las relacione
            //            _dbCalculations.DeleteCalculationProcessGroupPrjects(idCalculation);
            //            //inserta todas las relaciones
            //            foreach (PF.Entities.ProcessGroupProject _project in processGroupProjects.Values)
            //            {
            //                _dbCalculations.CreateCalculationProcessGroupPrjects(idCalculation, _IdOrganization, _project.IdProcess);
            //            }
                     
            //            // Completar la transacción
            //            _transactionScope.Complete();
            //        }
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

        internal void Modify(Int64 idCalculation, Entities.MeasurementUnit measurementUnit, String name, String description, Int16 frequency, Int64 idTimeUnitFrequency, Dictionary<Int64, PF.Entities.ProcessGroupProcess> processGroupProcesses, Boolean isRelevant)
            {
                try
                {
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                        //Modifico los datos de la base
                        _dbPerformanceAssessments.Calculations_Update(idCalculation, measurementUnit.IdMeasurementUnit, frequency, idTimeUnitFrequency, isRelevant, _Credential.User.Person.IdPerson);

                        _dbPerformanceAssessments.Calculations_LG_Update(idCalculation, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

                        _dbPerformanceAssessments.Calculations_DeleteCalculationProcessGroupPrjects(idCalculation);

                        foreach (PF.Entities.ProcessGroupProcess item in processGroupProcesses.Values)
                        {
                            _dbPerformanceAssessments.Calculations_CreateCalculationProcessGroupPrjects(idCalculation, item.IdProcess);
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


        #region process relationships
        /// <summary>
        /// Borra la relacion
        /// </summary>
        /// <param name="calculation"></param>
        /// <param name="processGroupProcess"></param>
        internal void Remove(Entities.Calculation calculation, PF.Entities.ProcessGroupProcess processGroupProcess)
        {
            try
            {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Borrar de la base de datos la relacion con projects
                    _dbPerformanceAssessments.Calculations_DeleteCalculationProcessGroupPrjects(calculation.IdCalculation, processGroupProcess.IdProcess);
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
