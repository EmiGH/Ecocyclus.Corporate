using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class OrganizationClassifications
    {
        #region Internal Properties
        private Credential _Credential;
        private Entities.OrganizationClassification _Parent; 
        #endregion

        internal OrganizationClassifications(Credential credential)
        {
            _Credential = credential;            
            _Parent = null;
        }
        internal OrganizationClassifications(Entities.OrganizationClassification parent, Credential credential)
        {
            _Credential = credential;
            _Parent = parent;
        }
        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParent
        {
            get
            {
                if (_Parent == null) { return 0; } else { return _Parent.IdOrganizationClassification; }
            }
        }

        #region Read Functions
        internal Entities.OrganizationClassification Item(Int64 idOrganizationClassification)
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationClassifications_ReadById(idOrganizationClassification, _Credential.CurrentLanguage.IdLanguage);
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganizationClassification", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                if ((IdParent == 0) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganizationClassification"], 0)) == IdParent))
                {
                    return new Entities.OrganizationClassification(Convert.ToInt64(_dbRecord["IdOrganizationClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentOrganizationClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                }
            }
            return null;
        }
        internal Dictionary<Int64, Entities.OrganizationClassification> Items()
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Dictionary<Int64, Entities.OrganizationClassification> _oItems = new Dictionary<Int64, Entities.OrganizationClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            if (IdParent == 0)
            { _record = _dbDirectoryServices.OrganizationClassifications_ReadRoot(_Credential.CurrentLanguage.IdLanguage); }
            else
            { _record = _dbDirectoryServices.OrganizationClassifications_ReadByParent(IdParent, _Credential.CurrentLanguage.IdLanguage); }
            //Filtro de lenguaje
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganizationClassification", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.OrganizationClassification _OrganizationClassification = new Entities.OrganizationClassification(Convert.ToInt64(_dbRecord["IdOrganizationClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentOrganizationClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_OrganizationClassification.IdOrganizationClassification, _OrganizationClassification);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.OrganizationClassification> Items(Entities.Organization organization)
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Dictionary<Int64, Entities.OrganizationClassification> _oItems = new Dictionary<Int64, Entities.OrganizationClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationClassifications_ReadByOrganization(organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganizationClassification", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.OrganizationClassification _OrganizationClassification = new Entities.OrganizationClassification(Convert.ToInt64(_dbRecord["IdOrganizationClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentOrganizationClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_OrganizationClassification.IdOrganizationClassification, _OrganizationClassification);
            }
            return _oItems;
        }

        /// <summary>
        /// Devuelve todos los clasification sin filtro de seguridad, se usa para dar de alta la seguridad
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.OrganizationClassification> ItemsSecurity()
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Dictionary<Int64, Entities.OrganizationClassification> _oItems = new Dictionary<Int64, Entities.OrganizationClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationClassifications_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganizationClassification", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.OrganizationClassification _OrganizationClassification = new Entities.OrganizationClassification(Convert.ToInt64(_dbRecord["IdOrganizationClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentOrganizationClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_OrganizationClassification.IdOrganizationClassification, _OrganizationClassification);
            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal Entities.OrganizationClassification Add(String name, String description)
        {
            try
            {
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idOrganizationClassification = _dbDirectoryServices.OrganizationClassifications_Create(IdParent);

                    _dbDirectoryServices.OrganizationClassifications_LG_Create(_idOrganizationClassification, _Credential.DefaultLanguage.IdLanguage, name, description);

                    _log.Create("DS_OrganizationClassifications", "OrganizationClassifications", "Add", "IdOrganizationClassification = " + _idOrganizationClassification, _Credential.User.IdPerson);

                    Entities.OrganizationClassification _organizationClassification = new Entities.OrganizationClassification(_idOrganizationClassification, IdParent, name, description, _Credential);
                    
                    return _organizationClassification;
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
        internal void Remove(Entities.OrganizationClassification organizationClassification)
        {           
            try
            {
                    //Borrado cascada
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();
                    //Borra las hijas
                    organizationClassification.Remove();
              
                    //Borra la relacion con los elementos
                    _dbDirectoryServices.Organizations_DeleteByClassification(organizationClassification.IdOrganizationClassification);
                    //Borra los LG
                    _dbDirectoryServices.OrganizationClassifications_LG_Delete(organizationClassification.IdOrganizationClassification);
                    //Borrar de la base de datos
                    _dbDirectoryServices.OrganizationClassifications_Delete(organizationClassification.IdOrganizationClassification);
                    //Log
                    _log.Create("DS_OrganizationClassifications", "OrganizationClassifications", "Remove", "IdOrganizationClassification = " + organizationClassification.IdOrganizationClassification, _Credential.User.IdPerson);
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
        internal void Modify(Entities.OrganizationClassification organizationClassification, String name, String description)
        {
            try
            {               
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();

                    //Modifico los datos de la base                    
                    _dbDirectoryServices.OrganizationClassifications_LG_Update(organizationClassification.IdOrganizationClassification, _Credential.DefaultLanguage.IdLanguage, name, description);

                    _log.Create("DS_OrganizationClassifications", "OrganizationClassifications", "Modify", "IdOrganizationClassification = " + organizationClassification.IdOrganizationClassification, _Credential.User.IdPerson);
                
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
        internal void Modify(Entities.OrganizationClassification organizationClassification)
        {
            try
            {
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();

                //Seguridad: hereda de la del padre, si no tiene padre hereda del mapa
                if (_Parent == null)
                {
                    _dbDirectoryServices.OrganizationClassifications_Update(organizationClassification.IdOrganizationClassification, 0);
                }
                else
                {
                    _dbDirectoryServices.OrganizationClassifications_Update(organizationClassification.IdOrganizationClassification, _Parent.IdOrganizationClassification);
                }
                
                _log.Create("DS_OrganizationClassifications", "OrganizationClassifications", "Modify", "IdOrganizationClassification = " + organizationClassification.IdOrganizationClassification, _Credential.User.IdPerson);

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
