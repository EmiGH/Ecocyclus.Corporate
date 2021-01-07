using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.Security.Entities
{
    //public class RoleType
    //{
    //      #region Internal Properties
    //        private Credential _Credential;
    //        private Int64 _IdRoleType;            
    //        private RoleType_LG _LanguageOption;
    //        private Collections.RoleTypes_LG _LanguagesOptions;           
    //    #endregion

    //    #region External Properties
    //        public Int64 IdRoleType
    //        {
    //            get { return _IdRoleType; }
    //        }
            
    //        public RoleType_LG LanguageOption
    //        {
    //            get { return _LanguageOption; }             
    //        }
    //        public Collections.RoleTypes_LG LanguagesOptions
    //        {
    //            get 
    //            {
    //                if (_LanguagesOptions == null)
    //                { _LanguagesOptions = new Collections.RoleTypes_LG(_IdRoleType, _Credential); }
    //                return _LanguagesOptions; 
    //            }
    //        }
    //        #region Modules
    //        public Dictionary<String, Module> Modules
    //        {
    //            get { return new Collections.Modules(_Credential).Items(this); }
    //        }
    //        #endregion
    //        #region Gadgets
    //        public Dictionary<String, DS.Entities.Gadgets.Gadget> Gadgests
    //        {
    //            get { return new DS.Collections.Gadgets(_Credential).Items(this); }
    //        }

    //        #endregion
    //    #endregion

    //        internal RoleType(Int64 idRoleType, String name, Credential credential) 
    //    {
    //        _Credential = credential;
    //        _IdRoleType = idRoleType;
    //        _LanguageOption = new RoleType_LG(_Credential.CurrentLanguage.IdLanguage, name);
    //    }

    //        public void Modify(String name, List<Entities.Module> modules, List<DS.Entities.Gadgets.Gadget> gadgets)
    //        {
    //            new Collections.RoleTypes(_Credential).Modify(_IdRoleType, name, modules,gadgets);
    //        }

    //}
}
