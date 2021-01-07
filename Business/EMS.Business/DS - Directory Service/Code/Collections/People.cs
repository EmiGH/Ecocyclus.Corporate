using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class People
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Organization _Organization;  //Identificador de la organizacion administrada            
        #endregion

        internal People(Credential credential)
        {
            _Credential = credential;
        }

        internal People(Entities.Organization organization)
        {                      
            _Organization = organization;
            _Credential = organization.Credential;
        }

        #region Read Functions
            internal Entities.Person Item(Int64 idPerson)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.Person _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.People_ReadById(idPerson);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                    _item = Factory.FactoryPerson.CreatePerson(Convert.ToInt64(_dbRecord["IdPerson"]), _Credential.DefaultLanguage.IdLanguage, Convert.ToString(_dbRecord["FirstName"]), Convert.ToString(_dbRecord["LastName"]), Convert.ToString(_dbRecord["NickName"]), Convert.ToString(_dbRecord["PosName"]), Convert.ToInt64(_dbRecord["IdSalutationType"]), Convert.ToInt64(_dbRecord["IdOrganization"]), _resourcecatalog, _Credential);
                }
                return _item;
            }
            internal Dictionary<Int64, Entities.Person> Items()
            {
                //Colección de personas a devolver
                Dictionary<Int64, Entities.Person> _oItems = new Dictionary<Int64, Entities.Person>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.People_ReadAll(_Organization.IdOrganization);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                    //Declara e instancia una persona
                    Entities.Person _oPerson = Factory.FactoryPerson.CreatePerson(Convert.ToInt64(_dbRecord["IdPerson"]), _Credential.DefaultLanguage.IdLanguage, Convert.ToString(_dbRecord["FirstName"]), Convert.ToString(_dbRecord["LastName"]), Convert.ToString(_dbRecord["NickName"]), Convert.ToString(_dbRecord["PosName"]), Convert.ToInt64(_dbRecord["IdSalutationType"]), Convert.ToInt64(_dbRecord["IdOrganization"]), _resourcecatalog, _Credential);
                    _oItems.Add(_oPerson.IdPerson, _oPerson);
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.Person Add(Entities.SalutationType salutationType, String lastName, String firstName, String posName, String nickName, KC.Entities.ResourceCatalog resourceCatalog)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    
                    //validacio para los null
                    Int64 _IdResourceCatalog = 0; if (resourceCatalog != null) { _IdResourceCatalog = resourceCatalog.IdResource; }
                    Int64 _IdSalutationType = 0; if (salutationType != null) { _IdSalutationType = salutationType.IdSalutationType; }
                    
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idPerson = _dbDirectoryServices.People_Create(_IdSalutationType, _Organization.IdOrganization, lastName, firstName, posName, nickName, _IdResourceCatalog);
                    //Devuelvo el objeto Persona creado

                    return Factory.FactoryPerson.CreatePerson(_idPerson, _Credential.DefaultLanguage.IdLanguage, firstName, lastName, nickName, posName, _IdSalutationType, _Organization.IdOrganization, resourceCatalog, _Credential);
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
            internal void Remove(Entities.Person person)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    person.Remove();
                    //Borrar de la base de datos
                    _dbDirectoryServices.People_Delete(person.IdPerson, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            internal void Modify(Entities.Person person, Entities.SalutationType salutationType, String lastName, String firstName, String posName, String nickName, KC.Entities.ResourceCatalog resourceCatalog)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //validacio para los null
                    Int64 _IdResourceCatalog = 0; if (resourceCatalog != null) { _IdResourceCatalog = resourceCatalog.IdResource; }

                    //Modifico los datos de la base
                    _dbDirectoryServices.People_Update(person.IdPerson, salutationType.IdSalutationType, _Organization.IdOrganization, lastName, firstName, posName, nickName, _IdResourceCatalog);
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
