using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{   
    //public class Gadgets
    //{
    //   #region Internal Properties
    //        private Credential _Credential;
    //    #endregion

    //    internal Gadgets(Credential credential) 
    //    {
    //        _Credential =credential;
    //    }
        
    //    #region Read Functions
    //        internal Entities.Gadgets.Gadget Item(String idGadget)
    //        {
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Gadgets_ReadById(idGadget, _Credential.CurrentLanguage.IdLanguage);
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                return Factory.GadgetsFactory.CreateGadget(Convert.ToString(_dbRecord["IdGadget"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), "");
    //            }
    //            return null;
    //        }
    //        internal Dictionary<String, Entities.Gadgets.Gadget> Items()
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
    //            //Coleccion para devolver las clases
    //            Dictionary<String, Entities.Gadgets.Gadget> _oItems = new Dictionary<String, Entities.Gadgets.Gadget>();

    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Gadgets_ReadAll(_Credential.CurrentLanguage.IdLanguage);
    //            //busca si hay mas de un id modulo igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                    //Declara e instancia un modulo
    //                Entities.Gadgets.Gadget _oItem = Factory.GadgetsFactory.CreateGadget(Convert.ToString(_dbRecord["IdGadget"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), "");
    //                    //Lo agrego a la coleccion
    //                    _oItems.Add(_oItem.IdGadget, _oItem);
    //            }
    //            return _oItems;
    //        }
    //        internal Dictionary<String, Entities.Gadgets.Gadget> Items(Security.Entities.RoleType roleType)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
    //            //Coleccion para devolver las clases
    //            Dictionary<String, Entities.Gadgets.Gadget> _oItems = new Dictionary<String, Entities.Gadgets.Gadget>();

    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypeGadgets_ReadByRoleType(roleType.IdRoleType, _Credential.CurrentLanguage.IdLanguage);
    //            //busca si hay mas de un id modulo igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                //Declara e instancia un modulo
    //                Entities.Gadgets.Gadget _oItem = Factory.GadgetsFactory.CreateGadget(Convert.ToString(_dbRecord["IdGadget"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), "");
    //                //Lo agrego a la coleccion
    //                _oItems.Add(_oItem.IdGadget, _oItem);
    //            }
    //            return _oItems;
    //        }
    //        internal Dictionary<String, Entities.Gadgets.Gadget> Items(Entities.Person person)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
    //            //Coleccion para devolver las clases
    //            Dictionary<String, Entities.Gadgets.Gadget> _oItems = new Dictionary<String, Entities.Gadgets.Gadget>();

    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.DashBoard_ReadByPerson(person.IdPerson, person.Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
    //            //busca si hay mas de un id modulo igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                //Declara e instancia un modulo
    //                Entities.Gadgets.Gadget _oItem = Factory.GadgetsFactory.CreateGadget(Convert.ToString(_dbRecord["IdGadget"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Configuration"]));
    //                //Lo agrego a la coleccion
    //                _oItems.Add(_oItem.IdGadget, _oItem);
    //            }
    //            return _oItems;
    //        }
    //    #endregion
    //}
}


