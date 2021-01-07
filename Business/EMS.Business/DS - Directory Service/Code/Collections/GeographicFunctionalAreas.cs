using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class GeographicFunctionalAreas
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Organization _Organization;
            private Entities.GeographicFunctionalArea _Parent; 

        #endregion

        internal GeographicFunctionalAreas(Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Organization = organization;
        }
        internal GeographicFunctionalAreas(Entities.GeographicFunctionalArea Parent, Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Organization = organization;
            _Parent = Parent;
        }
        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParentGeographicArea
        {
            get
            {
                if (_Parent == null) { return 0; } else { return _Parent.GeographicArea.IdGeographicArea; }
            }
        }
        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParentFunctionalArea
        {
            get
            {
                if (_Parent == null) { return 0; } else { return _Parent.FunctionalArea.IdFunctionalArea; }
            }
        }

        #region Read Functions
            internal List<Entities.GeographicFunctionalArea> Items()
            {
                //Coleccion a devolver (tiene 2 id y 2 objetos)
                List<Entities.GeographicFunctionalArea> _oItems = new List<Entities.GeographicFunctionalArea>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                
                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (IdParentFunctionalArea == 0 || IdParentGeographicArea == 0)
                {
                    _record = _dbDirectoryServices.GeographicFunctionalAreas_GetRoot(_Organization.IdOrganization);
                }
                else
                {
                    _record = _dbDirectoryServices.GeographicFunctionalAreas_GetByParent(IdParentFunctionalArea, IdParentGeographicArea, _Organization.IdOrganization); 
                }

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
                    Entities.FunctionalArea _functionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                    GIS.Entities.GeographicArea _parentGeographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentGeographicArea"], 0)));
                    Entities.FunctionalArea _parentFunctionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)));
                    Entities.GeographicFunctionalArea _parentGeographicFunctionalArea = new GeographicFunctionalAreas(_Organization).Item(_parentFunctionalArea,_parentGeographicArea);
                    Entities.GeographicFunctionalArea _oGeographicFunctionalArea = new Entities.GeographicFunctionalArea(_functionalArea, _geographicArea, _parentGeographicFunctionalArea, _Credential);
                    _oItems.Add(_oGeographicFunctionalArea);
                }
                return _oItems;
            }
            internal Entities.GeographicFunctionalArea Item(Entities.FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.GeographicFunctionalArea _item = null;
                if (functionalArea == null || geographicArea == null) { return _item; }
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.GeographicFunctionalAreas_ReadById(functionalArea.IdFunctionalArea, geographicArea.IdGeographicArea, _Organization.IdOrganization);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
                    Entities.FunctionalArea _functionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                    GIS.Entities.GeographicArea _parentGeographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentGeographicArea"], 0)));
                    Entities.FunctionalArea _parentFunctionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)));
                    Entities.GeographicFunctionalArea _parentGeographicFunctionalArea = new GeographicFunctionalAreas(_Organization).Item(_parentFunctionalArea,_parentGeographicArea);
                    _item = new Entities.GeographicFunctionalArea(_functionalArea, geographicArea, _parentGeographicFunctionalArea, _Credential);
                }
                return _item;
            }
        #endregion

        #region Write Function
            internal Entities.GeographicFunctionalArea Add(Entities.FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //alta del Functional position
                    _dbDirectoryServices.GeographicFunctionalAreas_Create(functionalArea.IdFunctionalArea, geographicArea.IdGeographicArea, _Organization.IdOrganization, IdParentFunctionalArea, IdParentGeographicArea, _Credential.User.Person.IdPerson);

                    return new Entities.GeographicFunctionalArea(functionalArea, geographicArea, _Parent, _Credential);
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
            internal void Remove(Entities.GeographicFunctionalArea geographicFunctionalArea)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.GeographicFunctionalAreas_Delete(geographicFunctionalArea.FunctionalArea.IdFunctionalArea, geographicFunctionalArea.GeographicArea.IdGeographicArea, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            internal void Modify(Entities.GeographicFunctionalArea geographicFunctionalArea)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //alta del Functional position
                    _dbDirectoryServices.GeographicFunctionalAreas_Update(geographicFunctionalArea.FunctionalArea.IdFunctionalArea, geographicFunctionalArea.GeographicArea.IdGeographicArea, _Organization.IdOrganization, IdParentFunctionalArea, IdParentGeographicArea, _Credential.User.Person.IdPerson);
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
