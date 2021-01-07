using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class MeasurementUnits
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

            internal MeasurementUnits(Credential credential)
            {
                _Credential = credential;
            }

            #region Read Functions
            internal Entities.MeasurementUnit Item(Int64 idMeasurementUnit)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.MeasurementUnit _measurementUnit = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementUnits_ReadById(idMeasurementUnit, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_measurementUnit == null)
                {
                    _measurementUnit = new Entities.MeasurementUnit(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["Numerator"]),Convert.ToInt64(_dbRecord["Denominator"]), Convert.ToInt64(_dbRecord["Exponent"]), Convert.ToDecimal(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _measurementUnit;
                    }
                }
                else
                {
                    return new Entities.MeasurementUnit(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["Numerator"]), Convert.ToInt64(_dbRecord["Denominator"]), Convert.ToInt64(_dbRecord["Exponent"]), Convert.ToDecimal(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                }
            }
            return _measurementUnit;
        }
            internal Dictionary<Int64, Entities.MeasurementUnit> Items(Int64 idMagnitud)
        {
            //Coleccion para devolver los MeasurementUnit
            Dictionary<Int64, Entities.MeasurementUnit> _oItems = new Dictionary<Int64, Entities.MeasurementUnit>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementUnits_ReadAll(idMagnitud, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdMeasurementUnit"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]));
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
                        Entities.MeasurementUnit _measurementUnit = new Entities.MeasurementUnit(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["Numerator"]), Convert.ToInt64(_dbRecord["Denominator"]), Convert.ToInt64(_dbRecord["Exponent"]), Convert.ToDecimal(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_measurementUnit.IdMeasurementUnit, _measurementUnit);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.MeasurementUnit _measurementUnit = new Entities.MeasurementUnit(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["Numerator"]), Convert.ToInt64(_dbRecord["Denominator"]), Convert.ToInt64(_dbRecord["Exponent"]), Convert.ToDecimal(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_measurementUnit.IdMeasurementUnit, _measurementUnit);
                }

            }
            return _oItems;
        }
        
        #endregion

        #region Write Functions
            internal Entities.MeasurementUnit Add(Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, Int64 idMagnitud, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idMeasurementUnit = _dbPerformanceAssessments.MeasurementUnits_Create(numerator, denominator,exponent, constant, isPattern, idMagnitud, name, description, _Credential.DefaultLanguage.IdLanguage, _Credential.User.Person.IdPerson);
                //Devuelvo el objeto creado
                return new Entities.MeasurementUnit(_idMeasurementUnit, numerator, denominator,exponent, constant, isPattern, idMagnitud, _Credential.DefaultLanguage.IdLanguage,name, description,_Credential);
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
            internal void Remove(Int64 idMeasurementUnit)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                
                //Borrar de la base de datos
                _dbPerformanceAssessments.MeasurementUnits_Delete(idMeasurementUnit, _Credential.User.Person.IdPerson);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                }
                if (ex.Number == Common.Constants.ErrorDataBaseNotLastPatternMeasurementUnit)
                {
                    throw new Exception(Common.Resources.Errors.RemovePatternMeasurementUnit, ex);
                }
                throw ex;
            }
        }
            internal void Remove(Entities.Magnitud magnitud)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Borrar de la base de datos
                    _dbPerformanceAssessments.MeasurementUnits_Delete(magnitud.IdMagnitud);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                    {
                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                    }
                    if (ex.Number == Common.Constants.ErrorDataBaseNotLastPatternMeasurementUnit)
                    {
                        throw new Exception(Common.Resources.Errors.RemovePatternMeasurementUnit, ex);
                    }
                    throw ex;
                }
            }
            internal void Modify(Int64 idMeasurementUnit, Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, Int64 idMagnitud, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Modifico los datos de la base
                _dbPerformanceAssessments.MeasurementUnits_Update(idMeasurementUnit, numerator,denominator, exponent, constant, isPattern, idMagnitud, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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

        #region Measurement Device
            internal Dictionary<Int64, Entities.MeasurementUnit> ItemsByMeasurementDevice(Int64 idMeasurementDevice)
            {
                //Coleccion para devolver los MeasurementUnit
                Dictionary<Int64, Entities.MeasurementUnit> _oItems = new Dictionary<Int64, Entities.MeasurementUnit>();

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDeviceMeasurementUnits_ReadAll(idMeasurementDevice, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdMeasurementUnit"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]));
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
                            Entities.MeasurementUnit _measurementUnit = new Entities.MeasurementUnit(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["Numerator"]), Convert.ToInt64(_dbRecord["Denominator"]), Convert.ToInt64(_dbRecord["Exponent"]), Convert.ToDecimal(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_measurementUnit.IdMeasurementUnit, _measurementUnit);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.MeasurementUnit _measurementUnit = new Entities.MeasurementUnit(Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToInt64(_dbRecord["Numerator"]), Convert.ToInt64(_dbRecord["Denominator"]), Convert.ToInt64(_dbRecord["Exponent"]), Convert.ToDecimal(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_measurementUnit.IdMeasurementUnit, _measurementUnit);
                    }

                }
                return _oItems;
            }
            internal void AddByMeasurementDevice(Int64 idMeasurementDevice, Int64 idMeasurementUnit)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbPerformanceAssessments.MeasurementDeviceMeasurementUnits_Create(idMeasurementDevice, idMeasurementUnit, _Credential.User.Person.IdPerson);
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

            internal void RemoveByMeasurementDevice(Int64 idMeasurementDevice, Int64 idMeasurementUnit)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Borrar de la base de datos
                    _dbPerformanceAssessments.MeasurementDeviceMeasurementUnits_Delete(idMeasurementDevice, idMeasurementUnit, _Credential.User.Person.IdPerson);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                    {
                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                    }
                    if (ex.Number == Common.Constants.ErrorDataBaseNotLastPatternMeasurementUnit)
                    {
                        throw new Exception(Common.Resources.Errors.RemovePatternMeasurementUnit, ex);
                    }
                    throw ex;
                }
            }
            #endregion
    }
}
