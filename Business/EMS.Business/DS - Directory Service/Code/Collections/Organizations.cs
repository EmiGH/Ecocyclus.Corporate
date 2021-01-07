using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Linq;

namespace Condesus.EMS.Business.DS.Collections 
{
    internal class Organizations
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal Organizations(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.Organization Item(Int64 idOrganization)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.Organization _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Organizations_ReadById(idOrganization);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                    _item = new Entities.Organization(Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["CorporateName"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["FiscalIdentification"]), _resourcecatalog, _Credential);
                }
                return _item;
            }
            internal Dictionary<Int64, Entities.Organization> Items()
            {

                //Colección de Organizaciones a devolver
                Dictionary<Int64, Entities.Organization> _items = new Dictionary<Int64, Entities.Organization>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Organizations_ReadAll();
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    try
                    {
                        KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                        //Declara e instancia una organizacion
                        Entities.Organization _oOrganization = new Entities.Organization(
                            Convert.ToInt64(_dbRecord["IdOrganization"]),
                            Convert.ToString(_dbRecord["CorporateName"]),
                            Convert.ToString(_dbRecord["Name"]),
                            Convert.ToString(_dbRecord["FiscalIdentification"]),
                            _resourcecatalog,
                            _Credential);

                        _items.Add(_oOrganization.IdOrganization, _oOrganization);
                    }
                    catch (UnauthorizedAccessException)
                    { 
                        //No hago nada porque es solo para no utilizar las
                        //organizaciones sobre las que no tengo permisos
                    }
                                   
                }
                return _items;
            }

            internal List<PF.Entities.ProcessParticipation> ProcessParticipationsItems(Entities.Organization organization)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Coleccion para devolver las areas funcionales
                List<PF.Entities.ProcessParticipation> _oItems = new List<PF.Entities.ProcessParticipation>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessParticipations_ReadByOrganization(organization.IdOrganization);

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia una posicion
                    PF.Entities.ProcessParticipation _processParticipation = new PF.Entities.ProcessParticipation(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdParticipationType"]), Convert.ToString(_dbRecord["Comment"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_processParticipation);

                }
                return _oItems;
            }

            internal Dictionary<Int64, Entities.Organization> ReadRoot()
            {
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<Int64, Entities.Organization> _oItems = new Dictionary<Int64, Entities.Organization>();
                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Organizations_ReadRoot(Common.Security.Organization, _Credential.User.IdPerson);
                //Se modifica con los datos que realmente se tienen que usar...
                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganization", _Credential).Filter();

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                    //Declara e instancia una posicion
                    Entities.Organization _organization = new Entities.Organization(Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["CorporateName"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["FiscalIdentification"]), _resourcecatalog,_Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_organization.IdOrganization, _organization);

                }
                return _oItems;
            }
            /// <summary>
            /// Este metodo devuelve todas las organizaciones en las que tiene permiso el usr, se utiliza para poderlas ubicar en el mapa
            /// </summary>
            /// <returns></returns>
            internal Dictionary<Int64, Entities.Organization> ReadAllByPerson()
            {
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<Int64, Entities.Organization> _oItems = new Dictionary<Int64, Entities.Organization>();
                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Organizations_ReadByPerson(_Credential.User.IdPerson, Common.Security.Organization);
                ////Se modifica con los datos que realmente se tienen que usar...
                //Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganization", _Credential).Filter();

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                    //Declara e instancia una posicion
                    Entities.Organization _organization = new Entities.Organization(Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["CorporateName"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["FiscalIdentification"]), _resourcecatalog, _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_organization.IdOrganization, _organization);

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.Organization> Items(Entities.OrganizationClassification organizationClassification)
            {
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.Organization> _oItems = new Dictionary<Int64, Entities.Organization>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Organizations_ReadByClassification(organizationClassification.IdOrganizationClassification, Common.Security.Organization, _Credential.User.IdPerson);
                //Se modifica con los datos que realmente se tienen que usar...
                //Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganization", _Credential).Filter();

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                    //Declara e instancia una posicion
                    Entities.Organization _organization = new Entities.Organization(Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["CorporateName"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["FiscalIdentification"]), _resourcecatalog, _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_organization.IdOrganization, _organization);

                }
                return _oItems;
            }

        #endregion

        #region Write Functions
            internal Entities.Organization Add(String corporateName, String name, String fiscalIdentification, KC.Entities.ResourceCatalog resourcePicture, Dictionary<Int64, Entities.OrganizationClassification> organizationClassifications)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                    //validacio para los null
                    Int64 _IdResourceCatalog = 0; if (resourcePicture != null) { _IdResourceCatalog = resourcePicture.IdResource; }

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idOrganization = _dbDirectoryServices.Organizations_Create(name, corporateName, fiscalIdentification, _IdResourceCatalog);

                    Entities.Organization _organization = new Entities.Organization(_idOrganization, corporateName, name, fiscalIdentification, resourcePicture, _Credential);

                    
                    foreach (Entities.OrganizationClassification _classification in organizationClassifications.Values)
                    {
                        _dbDirectoryServices.Organizations_Create(_idOrganization, _classification.IdOrganizationClassification);

                    }

                    //Dar de alta seguridad al creador
                    Security.Entities.Permission _permision = new Security.Collections.Permissions(_Credential).Item(Common.Permissions.Manage);
                    new Security.Collections.Rights(_Credential).Add(_organization, _Credential.User.Person, _permision);

                    _dbLog.Create("DS_Organizations", "Organizations", "Add", "IdOrganization=" + _idOrganization, _Credential.User.IdPerson);

                    //Devuelvo el objeto Organization creado
                    return _organization;
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
            internal void Remove(Entities.Organization organization)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    //Borra sus dependencias
                    organization.Remove();
                    //Borrar de la base de datos
                    _dbDirectoryServices.Organizations_DeleteByOrganization(organization.IdOrganization);
                    _dbDirectoryServices.Organizations_Delete(organization.IdOrganization);
                    _dbLog.Create("DS_Organizations", "Organizations", "Delete", "IdOrganization=" + organization.IdOrganization, _Credential.User.IdPerson);

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
            internal void Modify(Entities.Organization organization, String corporateName, String name, String fiscalIdentification, KC.Entities.ResourceCatalog resourcePicture, Dictionary<Int64, Entities.OrganizationClassification> organizationClassifications)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                    //validacio para los null
                    Int64 _IdResourceCatalog = 0; if (resourcePicture != null) { _IdResourceCatalog = resourcePicture.IdResource; }

                    //Modifico los datos de la base
                    _dbDirectoryServices.Organizations_Update(organization.IdOrganization, name, corporateName, fiscalIdentification, _IdResourceCatalog);

                    //TODO:replicar esto para todas los modify de elementos
                    //Borra todas las relaciones con classifications que tengo permiso 
                    foreach (Entities.OrganizationClassification _classification in organization.Classifications.Values)
                    {
                        _dbDirectoryServices.Organizations_Delete(organization.IdOrganization, _classification.IdOrganizationClassification);
                    }                        
                    //da de alta las nuevas relaciones con classifications
                    foreach (Entities.OrganizationClassification _classification in organizationClassifications.Values)
                    {
                        _dbDirectoryServices.Organizations_Create(organization.IdOrganization, _classification.IdOrganizationClassification);
                    }
                    _dbLog.Create("DS_Organizations", "Organizations", "Modify", "IdOrganization=" + organization.IdOrganization, _Credential.User.IdPerson);
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

            #region Asociated Classifications
            internal void Remove(Entities.Organization organization, Entities.OrganizationClassification classification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.Organizations_Delete(organization.IdOrganization, classification.IdOrganizationClassification);
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
        #endregion
    }
}
