using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Collections
{
    internal class ExtendedPropertyClassifications
    {
        #region Internal Properties    
        private Credential _Credential;
        #endregion

        internal ExtendedPropertyClassifications(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.ExtendedPropertyClassification Item(Int64 idExtendedPropertyClassification)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            Entities.ExtendedPropertyClassification _extendedPropertyClassification = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedPropertyClassifications_ReadById(idExtendedPropertyClassification,  _Credential.CurrentLanguage.IdLanguage );
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_extendedPropertyClassification == null)
                {
                    _extendedPropertyClassification = new Entities.ExtendedPropertyClassification(Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]) != _Credential.CurrentLanguage.IdLanguage.ToUpper())
                    {
                        return _extendedPropertyClassification;
                    }
                }
                else
                {
                    return new Entities.ExtendedPropertyClassification(Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                }
            }
            return _extendedPropertyClassification;
        }
        internal Dictionary<Int64, Entities.ExtendedPropertyClassification> Items()
        {
            //Coleccion para devolver los países
            Dictionary<Int64, Entities.ExtendedPropertyClassification> _oItems = new Dictionary<Int64, Entities.ExtendedPropertyClassification>();

            //Objeto de data layer para acceder a datos
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedPropertyClassifications_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]));
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
                        Entities.ExtendedPropertyClassification _extendedPropertyClassification = new Entities.ExtendedPropertyClassification(Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_extendedPropertyClassification.IdExtendedPropertyClassification , _extendedPropertyClassification);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.ExtendedPropertyClassification _extendedPropertyClassification = new Entities.ExtendedPropertyClassification(Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_extendedPropertyClassification.IdExtendedPropertyClassification, _extendedPropertyClassification);
                }

            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal Entities.ExtendedPropertyClassification Add(String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idExtendedPropertyClassification = _dbExtendedProperties.ExtendedPropertyClassifications_Create(_Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                //Devuelvo el objeto creado
                return new Entities.ExtendedPropertyClassification(_idExtendedPropertyClassification, name,description, _Credential.DefaultLanguage.IdLanguage, _Credential);
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
        internal void Remove(Int64 idExtendedPropertyClassification)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Borrar de la base de datos
                _dbExtendedProperties.ExtendedPropertyClassifications_Delete(idExtendedPropertyClassification, _Credential.User.Person.IdPerson);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                {
                    //throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                }
                throw ex;
            }
        }
        internal void Modify(Int64 idExtendedPropertyClassification, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Modifico los datos de la base
                _dbExtendedProperties.ExtendedPropertyClassifications_Update(idExtendedPropertyClassification, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
