using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;


namespace Condesus.EMS.Business.Security.Collections
{
    //public class RoleTypes
    //{
    //     #region Internal Properties
    //        private Credential _Credential;         
    //    #endregion

    //        internal RoleTypes(Credential credential)
    //    {
    //        _Credential = credential;            
    //    }

         

    //    #region Read Functions
    //        //Trae el tipo pedido
    //        internal Entities.RoleType Item(Int64 idRoleType)
    //    {
    //        //Objeto de data layer para acceder a datos
    //        DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //        IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypes_ReadById(idRoleType, _Credential.CurrentLanguage.IdLanguage);
    //        //Se modifica con los datos que realmente se tienen que usar...
    //        Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdRoleType", _Credential).Filter();
    //        //si no trae nada retorno 0 para que no de error
    //        foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
    //        {                
    //            return new Entities.RoleType(Convert.ToInt64(_dbRecord["IdRoleType"]), Convert.ToString(_dbRecord["Name"]), _Credential);
    //        }
    //        return null;
    //    }
    //        //Trae todos los tipos
    //        internal Dictionary<Int64, Entities.RoleType> Items()
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //            //Coleccion para devolver las areas funcionales
    //            Dictionary<Int64, Entities.RoleType> _oItems = new Dictionary<Int64, Entities.RoleType>();
    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);

    //            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdRoleType", _Credential).Filter();

    //            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
    //            {
    //                //Declara e instancia una posicion
    //                Entities.RoleType _roleType = new Entities.RoleType(Convert.ToInt64(_dbRecord["IdRoleType"]), Convert.ToString(_dbRecord["Name"]), _Credential);

    //                //Lo agrego a la coleccion
    //                _oItems.Add(_roleType.IdRoleType, _roleType);
    //            }
    //            return _oItems;
    //        }

    //        internal Dictionary<Int64, Entities.RoleType> Items(Entities.Module module)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //            //Coleccion para devolver las areas funcionales
    //            Dictionary<Int64, Entities.RoleType> _oItems = new Dictionary<Int64, Entities.RoleType>();
    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypeModules_ReadByModule(module.IdModule); 

    //            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                //Declara e instancia una posicion
    //                Entities.RoleType _roleType = Item(Convert.ToInt64(_dbRecord["IdRoleType"]));

    //                //Lo agrego a la coleccion
    //                _oItems.Add(_roleType.IdRoleType, _roleType);
    //            }
    //            return _oItems;
    //        }
    //        internal Dictionary<Int64, Entities.RoleType> Items(DS.Entities.Post post)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //            //Coleccion para devolver las areas funcionales
    //            Dictionary<Int64, Entities.RoleType> _oItems = new Dictionary<Int64, Entities.RoleType>();

    //            //Traigo los datos del post
    //            IEnumerable<System.Data.Common.DbDataRecord> _recordPost = _dbSecuritySystems.RoleTypes_ReadByPost(post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition, post.Person.IdPerson);
    //            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordPost)
    //            {
    //                //Declara e instancia una posicion
    //                Entities.RoleType _roleType = Item(Convert.ToInt64(_dbRecord["IdRoleType"]));

    //                //Lo agrego a la coleccion
    //                _oItems.Add(_roleType.IdRoleType, _roleType);
    //            }
    //            //Traigo los datos del post
    //            IEnumerable<System.Data.Common.DbDataRecord> _recordJobTitle = _dbSecuritySystems.RoleTypes_ReadByJobTitle(post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition);
    //            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordJobTitle)
    //            {
    //                //Declara e instancia una posicion
    //                Entities.RoleType _roleType = Item(Convert.ToInt64(_dbRecord["IdRoleType"]));

    //                //Lo agrego a la coleccion no existe
    //                if (!_oItems.ContainsKey(_roleType.IdRoleType))
    //                {_oItems.Add(_roleType.IdRoleType, _roleType);}
    //            }
    //            DataAccess.PF.ProcessesFramework _processesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
    //            //Traigo los datos de la base, valido que tenga seguridad dada en las tareas para saber si es operator
    //            IEnumerable<System.Data.Common.DbDataRecord> _recordTask = _processesFramework.ProcessTaskPermissions_ReadByPost(post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition, post.Person.IdPerson);
    //            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordTask)
    //            {
    //                //Declara e instancia una posicion
    //                Entities.RoleType _roleType = Item(Common.RolesTypes.Operator);

    //                //Lo agrego a la coleccion si no existe
    //                if (!_oItems.ContainsKey(_roleType.IdRoleType)) 
    //                { 
    //                    _oItems.Add(_roleType.IdRoleType, _roleType);                        
    //                }
    //                break;
    //            }
    //            return _oItems;
    //        }
    //        internal Dictionary<Int64, Entities.RoleType> Items(DS.Entities.JobTitle jobTitle)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //            //Coleccion para devolver las areas funcionales
    //            Dictionary<Int64, Entities.RoleType> _oItems = new Dictionary<Int64, Entities.RoleType>();
    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypes_ReadByJobTitle(jobTitle.Organization.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, jobTitle.FunctionalPositions.Position.IdPosition);

    //            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                //Declara e instancia una posicion
    //                Entities.RoleType _roleType = Item(Convert.ToInt64(_dbRecord["IdRoleType"]));

    //                //Lo agrego a la coleccion
    //                _oItems.Add(_roleType.IdRoleType, _roleType);
    //            }
    //            return _oItems;
    //        }


    //    #endregion

    //    #region Write Functions
    //        internal Entities.RoleType Add(String name, List<Entities.Module> modules, List<DS.Entities.Gadgets.Gadget> gadgets)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

    //                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                Int64 _idRoleTypes = _dbSecuritySystems.RoleTypes_Create();

    //                _dbSecuritySystems.RoleTypes_LG_Create(_idRoleTypes, _Credential.DefaultLanguage.IdLanguage, name);

    //                _dbLog.Create("SS_RoleTypes", "RoleTypes", "Add", "IdRoleType=" + _idRoleTypes, _Credential.User.IdPerson);

    //                foreach (Entities.Module _module in modules)
    //                {
    //                    _dbSecuritySystems.RoleTypeModules_Create(_idRoleTypes, _module.IdModule);                       
    //                }
    //                foreach (DS.Entities.Gadgets.Gadget _gadget in gadgets)
    //                {
    //                    _dbSecuritySystems.RoleTypeGadgets_Create(_idRoleTypes, _gadget.IdGadget);
    //                }
                   
    //                //Devuelvo el objeto FunctionalArea creado
    //                return new Entities.RoleType(_idRoleTypes, name, _Credential);

                    
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
    //        internal void Remove(Entities.RoleType roleTypes)
    //        {
    //             try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

    //                 _dbSecuritySystems.RoleTypes_LG_Delete(roleTypes.IdRoleType);
    //                    //Borrar de la base de datos
    //                 _dbSecuritySystems.RoleTypes_Delete(roleTypes.IdRoleType);

    //                 _dbLog.Create("SS_RoleTypes", "RoleTypes", "Delete", "IdRoleType=" + roleTypes.IdRoleType, _Credential.User.IdPerson);         

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
    //        internal void Modify(Int64 idRoleTypes, String name, List<Entities.Module> modules, List<DS.Entities.Gadgets.Gadget> gadgets)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

    //                    _dbSecuritySystems.RoleTypes_LG_Update(idRoleTypes, _Credential.DefaultLanguage.IdLanguage, name);

    //                    _dbLog.Create("SS_RoleTypes", "RoleTypes", "Modify", "IdRoleType=" + idRoleTypes, _Credential.User.IdPerson);

    //                    //borra los modulos
    //                    _dbSecuritySystems.RoleTypeModules_Delete(idRoleTypes);

    //                    foreach (Entities.Module _module in modules)
    //                    {
    //                        _dbSecuritySystems.RoleTypeModules_Create(idRoleTypes, _module.IdModule);
    //                    }
                           
    //                    //borra los gadgets
    //                    _dbSecuritySystems.RoleTypeGadgets_Delete(idRoleTypes);

    //                    foreach (DS.Entities.Gadgets.Gadget _gadget in gadgets)
    //                    {
    //                        _dbSecuritySystems.RoleTypeGadgets_Create(idRoleTypes, _gadget.IdGadget);
    //                    }


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
