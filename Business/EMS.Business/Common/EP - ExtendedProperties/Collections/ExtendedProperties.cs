using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Collections
{
    internal class ExtendedProperties
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal ExtendedProperties(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.ExtendedProperty Item(Int64 idExtendedProperty)
        {

            //Objeto de data layer para acceder a datos
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            Entities.ExtendedProperty _extendedProperty = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedProperties_ReadById(idExtendedProperty, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdExtendedProperty", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                if (_extendedProperty == null)
                {
                    _extendedProperty = new Entities.ExtendedProperty(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.CurrentLanguage.IdLanguage.ToUpper())
                    {
                        return _extendedProperty;
                    }
                }
                else
                {
                    return new Entities.ExtendedProperty(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                }
            }
            return _extendedProperty;
        }
        internal Dictionary<Int64, Entities.ExtendedProperty> Items()
        {
            //Coleccion para devolver los ExtendedProperty
            Dictionary<Int64, Entities.ExtendedProperty> _oItems = new Dictionary<Int64, Entities.ExtendedProperty>();

            //Objeto de data layer para acceder a datos
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedProperties_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdExtendedProperty"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.CurrentLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdExtendedProperty"]));
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
                        Entities.ExtendedProperty _extendedProperty = new Entities.ExtendedProperty(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_extendedProperty.IdExtendedProperty, _extendedProperty);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.ExtendedProperty _extendedProperty = new Entities.ExtendedProperty(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_extendedProperty.IdExtendedProperty, _extendedProperty);
                }

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.ExtendedProperty> ItemsByClassification(Int64 idExtendedPropertyClassification)
        {

            //Coleccion para devolver los ExtendedProperty
            Dictionary<Int64, Entities.ExtendedProperty> _oItems = new Dictionary<Int64, Entities.ExtendedProperty>();

            //Objeto de data layer para acceder a datos
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedProperties_ReadByClassification(idExtendedPropertyClassification,  _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdExtendedProperty"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.CurrentLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdExtendedProperty"]));
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
                        Entities.ExtendedProperty _extendedProperty = new Entities.ExtendedProperty(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_extendedProperty.IdExtendedProperty, _extendedProperty);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.ExtendedProperty _extendedProperty = new Entities.ExtendedProperty(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToInt64(_dbRecord["IdExtendedPropertyClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_extendedProperty.IdExtendedProperty, _extendedProperty);
                }

            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal Entities.ExtendedProperty Add(Int64 extendedPropertyClasification, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idExtendedProperty = _dbExtendedProperties.ExtendedProperties_Create(extendedPropertyClasification, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                //Devuelvo el objeto creado
                return new Entities.ExtendedProperty(_idExtendedProperty, extendedPropertyClasification, name, description, _Credential.CurrentLanguage.IdLanguage,_Credential);
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
        internal void Remove(Int64 idExtendedProperty)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Borrar de la base de datos
                _dbExtendedProperties.ExtendedProperties_Delete(idExtendedProperty, _Credential.User.Person.IdPerson);
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
        internal void Modify(Int64 idExtendedProperty, Int64 idExtendedPropertyClassification, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Modifico los datos de la base
                _dbExtendedProperties.ExtendedProperties_Update(idExtendedProperty, idExtendedPropertyClassification, _Credential.CurrentLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
