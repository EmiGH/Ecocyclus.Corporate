using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    //internal class Countries
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //    #endregion

    //    internal Countries(Credential credential)
    //    {
    //        _Credential = credential;
    //    }

    //    #region Read Functions
    //        internal Entities.Country Item(Int64 idCountry)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            Entities.Country _country = null;
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Countries_ReadById(idCountry, _Credential.CurrentLanguage.IdLanguage);
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                if (_country == null)
    //                {
    //                    _country = new Entities.Country(Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Alpha"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["InternationalCode"]),_Credential);
    //                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
    //                    {
    //                        return _country;
    //                    }
    //                }
    //                else
    //                {
    //                    return new Entities.Country(Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Alpha"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["InternationalCode"]),_Credential);
    //                }
    //            }
    //            return _country;
    //        }
    //        internal Dictionary<Int64, Entities.Country> Items()
    //        {
    //            //Coleccion para devolver los países
    //            Dictionary<Int64, Entities.Country> _oItems = new Dictionary<Int64, Entities.Country>();

    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Countries_ReadAll(_Credential.CurrentLanguage.IdLanguage);

    //            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
    //            Boolean _oInsert = true;

    //            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdCountry"])))
    //                {
    //                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
    //                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
    //                    {
    //                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdCountry"]));
    //                    }
    //                    else
    //                    {
    //                        //No debe insertar en la coleccion ya que existe el idioma correcto.
    //                        _oInsert = false;
    //                    }

    //                    //Solo inserta si es necesario.
    //                    if (_oInsert)
    //                    {
    //                        //Declara e instancia un pais 
    //                        Entities.Country _oCountry = new Entities.Country(Convert.ToInt64(_dbRecord["IdCountry"]), _Credential.CurrentLanguage.IdLanguage, Convert.ToString(_dbRecord["Alpha"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["InternationalCode"]), _Credential);
    //                        //Lo agrego a la coleccion
    //                        _oItems.Add(_oCountry.IdCountry, _oCountry);
    //                    }
    //                    _oInsert = true;
    //                }
    //                else
    //                {   //Declara e instancia un pais 
    //                    Entities.Country _oCountry = new Entities.Country(Convert.ToInt64(_dbRecord["IdCountry"]), _Credential.CurrentLanguage.IdLanguage, Convert.ToString(_dbRecord["Alpha"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["InternationalCode"]), _Credential);
    //                    //Lo agrego a la coleccion
    //                    _oItems.Add(_oCountry.IdCountry, _oCountry);
    //                }

    //            }
    //            return _oItems;
    //        }
         
    //    #endregion

    //    #region Write Functions
    //        internal Entities.Country Add(String alpha, String name, String internationalCode)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                Int64 _oIdCountry = _dbDirectoryServices.Countries_Create(_Credential.DefaultLanguage.IdLanguage, alpha, name, internationalCode, _Credential.User.Person.IdPerson);
    //                //Devuelvo el objeto país creado
    //                return new Entities.Country(_oIdCountry, _Credential.DefaultLanguage.IdLanguage, alpha, name, internationalCode, _Credential);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        internal void Remove(Int64 idCountry)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                //Borrar de la base de datos
    //                _dbDirectoryServices.Countries_Delete(idCountry, _Credential.User.Person.IdPerson);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
    //                {
    //                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        internal void Modify(Int64 idCountry, String alpha, String name, String internationalCode)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                //Modifico los datos de la base
    //                _dbDirectoryServices.Countries_Update(idCountry, _Credential.DefaultLanguage.IdLanguage, alpha, name, internationalCode, _Credential.User.Person.IdPerson);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //    #endregion
    //}
}
