using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class ContactMessengersApplications
    {
        #region Internal Properties
            private Credential _Credential;
            private String _Provider;
        #endregion

        internal ContactMessengersApplications(String provider, Credential credential)
        {
            _Credential = credential;
            _Provider = provider;
        }

        #region Read Functions
            internal Dictionary<String, Entities.ContactMessengerApplication> Items()
            {
                //Coleccion para devolver los MessengerApplication
                Dictionary<String, Entities.ContactMessengerApplication> _oItems = new Dictionary<String, Entities.ContactMessengerApplication>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                //DataAccess.DS.ContactMessengersApplications _dboContactMessengersApplications = _dbDirectoryServices.ContactMessengersApplications;

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactMessengersApplications_ReadAll(_Provider);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un mensajero
                    Entities.ContactMessengerApplication _oContactMessengerApplication = new Entities.ContactMessengerApplication(
                                Convert.ToString(_dbRecord["Provider"]),
                                Convert.ToString(_dbRecord["Application"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_oContactMessengerApplication.Provider + _oContactMessengerApplication.Application, _oContactMessengerApplication);
                }
                return _oItems;
            }
            internal Entities.ContactMessengerApplication Item(String application)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                //DataAccess.DS.ContactMessengersApplications _dboContactMessengersApplications = _dbDirectoryServices.ContactMessengersApplications;

                Entities.ContactMessengerApplication _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactMessengersApplications_ReadById(_Provider, application);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ContactMessengerApplication(
                        _dbRecord["Provider"].ToString(),
                        _dbRecord["Application"].ToString());
                }
                return _item;
            }
        #endregion

        #region Write Functions
            internal void Add(String application)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //DataAccess.DS.ContactMessengersApplications _dboContactMessengersApplications = _dbDirectoryServices.ContactMessengersApplications;

                    //ejecuta el insert
                    _dbDirectoryServices.ContactMessengersApplications_Create(_Provider, application, _Credential.User.Person.IdPerson);
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
            internal void Remove(String application)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //DataAccess.DS.ContactMessengersApplications _dboContactMessengersApplications = _dbDirectoryServices.ContactMessengersApplications;

                    //Borrar de la base de datos
                    _dbDirectoryServices.ContactMessengersApplications_Delete(_Provider, application, _Credential.User.Person.IdPerson);
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
