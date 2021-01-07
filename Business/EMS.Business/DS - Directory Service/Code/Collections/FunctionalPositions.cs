using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class FunctionalPositions
    {
        #region Internal Properties   
            private Credential _Credential;
            private Entities.Organization _Organization ;
            private Entities.Position _ParentPosition ;
            private Entities.FunctionalArea _ParentFunctionalArea;
            private Entities.FunctionalPosition _ParentFunctionalPosition;
            //encapsula la toma de decision de cual es el idparent
            private Int64 IdParentPosition
            {
                get
                {
                    if (_ParentPosition == null) { return 0; } else { return _ParentPosition.IdPosition; }
                }
            }
            //encapsula la toma de decision de cual es el idparent
            private Int64 IdParentFunctionalArea
            {
                get
                {
                    if (_ParentFunctionalArea == null) { return 0; } else { return _ParentFunctionalArea.IdFunctionalArea; }
                }
            }
        #endregion

            internal FunctionalPositions(Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Organization = organization;
        }
            internal FunctionalPositions(Entities.FunctionalPosition parentFunctionalPosition, Entities.Organization organization)
        {
            _Organization = organization;
            _ParentPosition = parentFunctionalPosition.Position;
            _ParentFunctionalArea = parentFunctionalPosition.FunctionalArea;
            _ParentFunctionalPosition = parentFunctionalPosition;
            _Credential = organization.Credential;
        }

        #region Read Functions
            internal List<Entities.FunctionalPosition> Items()
            {
                 //Coleccion a devolver (tiene 2 id y 2 objetos)
                List<Entities.FunctionalPosition> _oItems = new List<Entities.FunctionalPosition>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                
                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (IdParentFunctionalArea == 0 || IdParentPosition == 0)
                    { _record = _dbDirectoryServices.FunctionalPositions_GetRoot(_Organization.IdOrganization); }
                else
                { _record = _dbDirectoryServices.FunctionalPositions_GetByParent(IdParentPosition, IdParentFunctionalArea, _Organization.IdOrganization); }
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Position _position = new Positions(_Organization).Item(Convert.ToInt64(_dbRecord["IdPosition"]));
                    Entities.FunctionalArea _functionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                    Entities.Position _parentPosition = new Positions(_Organization).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentPosition"], 0)));
                    Entities.FunctionalArea _parentFunctionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)));
                    Entities.FunctionalPosition _parentFunctionalPosition = new FunctionalPositions(_Organization).Item(_parentPosition ,_parentFunctionalArea);
                    Entities.FunctionalPosition _oFunctionalPosition = new Entities.FunctionalPosition(_position, _functionalArea, _Organization,_parentFunctionalPosition, _Credential);
                    _oItems.Add(_oFunctionalPosition);
                }
                return _oItems;
            }
            internal Entities.FunctionalPosition Item(Entities.Position position, Entities.FunctionalArea functionalArea)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.FunctionalPosition _item = null;
                if (position == null || functionalArea == null) { return _item; }
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.FunctionalPositions_ReadById(position.IdPosition, functionalArea.IdFunctionalArea, _Organization.IdOrganization);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Position _parentPosition = new Positions(_Organization).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentPosition"], 0)));
                    Entities.FunctionalArea _parentFunctionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)));
                    Entities.FunctionalPosition _parentFunctionalPosition = new FunctionalPositions(_Organization).Item(_parentPosition, _parentFunctionalArea);
                    _item = new Entities.FunctionalPosition(position, functionalArea, _Organization, _parentFunctionalPosition, _Credential);
                }
                return _item;
            }
        #endregion

        #region Write Functions
            internal Entities.FunctionalPosition Add(Entities.Position position, Entities.FunctionalArea functionalArea)
            {   
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
      
                    //alta del Functional position
                    _dbDirectoryServices.FunctionalPositions_Create(position.IdPosition, functionalArea.IdFunctionalArea, _Organization.IdOrganization, IdParentPosition, IdParentFunctionalArea, _Credential.User.Person.IdPerson);

                    return new Entities.FunctionalPosition(position,functionalArea, _Organization, _ParentFunctionalPosition, _Credential);
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
            internal void Modify(Entities.FunctionalPosition functionalPosition)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
      
                    //alta del Functional position
                    _dbDirectoryServices.FunctionalPositions_Update(functionalPosition.Position.IdPosition, functionalPosition.FunctionalArea.IdFunctionalArea, _Organization.IdOrganization, IdParentPosition, IdParentFunctionalArea, _Credential.User.Person.IdPerson);
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
            internal void Remove(Entities.FunctionalPosition functionalPosition)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //Borrar de la base de datos
                    _dbDirectoryServices.FunctionalPositions_Delete(functionalPosition.Position.IdPosition, functionalPosition.FunctionalArea.IdFunctionalArea, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
