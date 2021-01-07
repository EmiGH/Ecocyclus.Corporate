using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class Languages
    {

        #region Internal Properties
        private Credential _Credential;        
        #endregion

        internal Languages(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.Language Item(String idLanguage)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.Language _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Languages_ReadById(idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["DefaultLanguage"]), Convert.ToBoolean(_dbRecord["Enabled"]));
                }
                return _item;
            }
            internal Dictionary<String, Entities.Language> Items()
            {
                //Coleccion para devolver los lenguajes
                Dictionary<String, Entities.Language> _oItems = new Dictionary<String, Entities.Language>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Languages_GetEnabled();
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un lenguaje
                    Entities.Language _oLanguage = new Entities.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["DefaultLanguage"]), Convert.ToBoolean(_dbRecord["Enabled"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_oLanguage.IdLanguage, _oLanguage);
                }
                return _oItems;
            }
            internal Dictionary<String, Entities.Language> AllItems()
            {
                //Coleccion para devolver los lenguajes
                Dictionary<String, Entities.Language> _oItems = new Dictionary<String, Entities.Language>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Languages_ReadAll();
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un lenguaje
                    Entities.Language _oLanguage = new Entities.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["DefaultLanguage"]), Convert.ToBoolean(_dbRecord["Enabled"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_oLanguage.IdLanguage, _oLanguage);
                }
                return _oItems;
            }
            public static Dictionary<String, Entities.Language> Options()
            {
                //No necesita verificar permisos porque esta información está expuesta
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Coleccion para devolver los lenguajes
                Dictionary<String, Entities.Language> _oItems = new Dictionary<String, Entities.Language>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Languages_GetEnabled();
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un lenguaje
                    Entities.Language _oLanguage = new Entities.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["DefaultLanguage"]), Convert.ToBoolean(_dbRecord["Enabled"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_oLanguage.IdLanguage, _oLanguage);
                }
                return _oItems;
            }
            internal Entities.Language DefaultLanguage()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.Language _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Languages_GetDefault();
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.Language(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToBoolean(_dbRecord["DefaultLanguage"]), Convert.ToBoolean(_dbRecord["Enabled"]));
                }
                return _item;
            }
        #endregion

        #region Write Functions
            internal Entities.Language Add(String idLanguage, String name, Boolean enable)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert
                    _dbDirectoryServices.Languages_Create(idLanguage, name, enable, _Credential.User.Person.IdPerson);
                    //Devuelvo el objeto lenguaje creado
                    return Item(idLanguage);
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
            internal void Remove(String idLanguage)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.Languages_Delete(idLanguage, _Credential.User.Person.IdPerson);
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
            internal void Modify(String idLanguage, String name, Boolean enable)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.Languages_Update(idLanguage, name, enable, _Credential.User.Person.IdPerson);
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