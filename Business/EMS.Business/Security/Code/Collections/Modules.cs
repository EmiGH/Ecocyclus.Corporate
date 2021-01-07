using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.Security.Collections
{
    //internal class Modules
    //{
    //    #region Internal Properties
    //    private Credential _Credential;   
    //    #endregion

    //    internal Modules(Credential credential) 
    //    {
    //        _Credential = credential;
    //    }

    //    #region Read Functions
    //        internal Entities.Module Item(String idModule)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //            Entities.Module _module = null;
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Modules_ReadById(idModule);
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                return new Entities.Module(Convert.ToString(_dbRecord["IdModule"]), Convert.ToString(_dbRecord["Name"]), _Credential);
    //            }
    //            return _module;
    //        }
    //        internal Dictionary<String, Entities.Module> Items()
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //            //Coleccion para devolver los modulos
    //            Dictionary<String, Entities.Module> _oItems = new Dictionary<String, Entities.Module>();

    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Modules_ReadAll();

    //            //busca si hay mas de un id modulo igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                    //Declara e instancia un modulo
    //                Entities.Module _oModule = new Entities.Module(Convert.ToString(_dbRecord["IdModule"]), Convert.ToString(_dbRecord["Name"]), _Credential);
    //                    //Lo agrego a la coleccion
    //                    _oItems.Add(_oModule.IdModule, _oModule);

    //            }
    //            return _oItems;
    //        }
    //        internal Dictionary<String, Entities.Module> Items(Entities.RoleType roleType)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //            //Coleccion para devolver los modulos
    //            Dictionary<String, Entities.Module> _oItems = new Dictionary<String, Entities.Module>();

    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypeModules_ReadById(roleType.IdRoleType);

    //            //busca si hay mas de un id modulo igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                //Declara e instancia un modulo
    //                Entities.Module _oModule = Item(Convert.ToString(_dbRecord["IdModule"]));
    //                //Lo agrego a la coleccion
    //                _oItems.Add(_oModule.IdModule, _oModule);

    //            }
    //            return _oItems;
    //        }

    //    #endregion
    //}
}
