using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class ContactMessengersProviders
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion
        internal ContactMessengersProviders(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Dictionary<String, Entities.ContactMessengerProvider> Items()
            {

                //Coleccion para devolver los MessengersProviders
                Dictionary<String, Entities.ContactMessengerProvider> _oItems = new Dictionary<String, Entities.ContactMessengerProvider>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactMessengersProviders_ReadAll();

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un mensajero
                    Entities.ContactMessengerProvider _oContactMessengerProvider = new Entities.ContactMessengerProvider(
                                Convert.ToString(_dbRecord["Provider"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_oContactMessengerProvider.Provider, _oContactMessengerProvider);
                }
                return _oItems;
            }
            internal Entities.ContactMessengerProvider Item(String provider)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.ContactMessengerProvider _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactMessengersProviders_ReadById(provider);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ContactMessengerProvider(_dbRecord["Provider"].ToString());
                }
                return _item;
            }
        #endregion

        #region Write Functions
            internal void Add(String provider)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert
                    _dbDirectoryServices.ContactMessengersProviders_Create(provider, _Credential.User.Person.IdPerson);
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
            internal void Remove(String provider)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.ContactMessengersProviders_Delete(provider, _Credential.User.Person.IdPerson);
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
