using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class MeasurementDeviceTypes
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal MeasurementDeviceTypes(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.MeasurementDeviceType Item(Int64 idMeasurementDeviceType)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.MeasurementDeviceType _measurementDeviceType = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDeviceTypes_ReadById(idMeasurementDeviceType, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_measurementDeviceType == null)
                {
                    _measurementDeviceType = new Entities.MeasurementDeviceType(Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _measurementDeviceType;
                    }
                }
                else
                {
                    return new Entities.MeasurementDeviceType(Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                }
            }
            return _measurementDeviceType;
        }
            internal Dictionary<Int64, Entities.MeasurementDeviceType> Items()
        {
            //Coleccion para devolver los MeasurementDeviceType
            Dictionary<Int64, Entities.MeasurementDeviceType> _oItems = new Dictionary<Int64, Entities.MeasurementDeviceType>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDeviceTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]));
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
                        Entities.MeasurementDeviceType _measurementDeviceType = new Entities.MeasurementDeviceType(Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_measurementDeviceType.IdMeasurementDeviceType, _measurementDeviceType);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.MeasurementDeviceType _measurementDeviceType = new Entities.MeasurementDeviceType(Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_measurementDeviceType.IdMeasurementDeviceType, _measurementDeviceType);
                }

            }
            return _oItems;
        }
      
        #endregion

        #region Write Functions
            internal Entities.MeasurementDeviceType Add(String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idMeasurementDeviceType = _dbPerformanceAssessments.MeasurementDeviceTypes_Create(name, description, _Credential.DefaultLanguage.IdLanguage, _Credential.User.Person.IdPerson);
                //Devuelvo el objeto creado
                return new Entities.MeasurementDeviceType(_idMeasurementDeviceType, name, description, _Credential.DefaultLanguage.IdLanguage,_Credential);
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
            internal void Remove(Entities.MeasurementDeviceType measurementDeviceType)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                measurementDeviceType.Remove();        
                //Borrar de la base de datos
                _dbPerformanceAssessments.MeasurementDeviceTypes_Delete(measurementDeviceType.IdMeasurementDeviceType, _Credential.User.Person.IdPerson);
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
            internal void Modify(Int64 idMeasurementDeviceType, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Modifico los datos de la base
                _dbPerformanceAssessments.MeasurementDeviceTypes_Update(idMeasurementDeviceType, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
