using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class Magnitudes
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

            internal Magnitudes(Credential credential)
            {
                _Credential = credential;
            }

        #region Read Functions
            internal Entities.Magnitud Item(Int64 idMagnitud)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Magnitud _Magnitud = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Magnitudes_ReadById(idMagnitud, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_Magnitud == null)
                {
                    _Magnitud = new Entities.Magnitud(Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _Magnitud;
                    }
                }
                else
                {
                    return new Entities.Magnitud(Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                }
            }
            return _Magnitud;
        }
            internal Dictionary<Int64, Entities.Magnitud> Items()
        {
            //Coleccion para devolver los Magnitud
            Dictionary<Int64, Entities.Magnitud> _oItems = new Dictionary<Int64, Entities.Magnitud>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Magnitudes_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdMagnitud"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdMagnitud"]));
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
                        Entities.Magnitud _Magnitud = new Entities.Magnitud(Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_Magnitud.IdMagnitud, _Magnitud);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Magnitud _Magnitud = new Entities.Magnitud(Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_Magnitud.IdMagnitud, _Magnitud);
                }

            }
            return _oItems;
        }
      
        #endregion

        #region Write Functions
            internal Entities.Magnitud Add(String name)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idMagnitud = _dbPerformanceAssessments.Magnitudes_Create(name, _Credential.DefaultLanguage.IdLanguage, _Credential.User.Person.IdPerson);
                //Devuelvo el objeto creado
                return new Entities.Magnitud(_idMagnitud, name, _Credential.DefaultLanguage.IdLanguage,_Credential);
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
            internal void Remove(Entities.Magnitud magnitud)
        {
            try
            {
                //Valida que no tenga dependencias de MeasurementUnits
                //if (magnitud.MeasurementUnits.Count > 0) { throw new Exception(Common.Resources.Errors.RemoveDependency); }
                magnitud.Remove();
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                
                //Borrar de la base de datos
                _dbPerformanceAssessments.Magnitudes_Delete(magnitud.IdMagnitud, _Credential.User.Person.IdPerson);
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
            internal void Modify(Int64 idMagnitud, String name)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Modifico los datos de la base
                _dbPerformanceAssessments.Magnitudes_Update(idMagnitud, _Credential.DefaultLanguage.IdLanguage, name, _Credential.User.Person.IdPerson);
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
