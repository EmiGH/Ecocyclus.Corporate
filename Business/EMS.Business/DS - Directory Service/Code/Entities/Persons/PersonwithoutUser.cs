using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class PersonwithoutUser : Person
    {

         #region External Properties
   
        #region User       
        public Entities.User UsersAdd(String userName, String password, Boolean active, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Boolean ViewGlobalMenu)
        {
            Entities.User _user = new Condesus.EMS.Business.DS.Collections.Users(Organization, Credential).Add(IdPerson, userName, password, active, changePasswordOnNextLogin, cannotChangePassword, passwordNeverExpires, ViewGlobalMenu);
            return _user;
        }
       
        #endregion

        #endregion

        internal PersonwithoutUser(Int64 idPerson, String idLanguage, String firstName, String lastName, String nickName, String posName, Int64 idSalutationType, Organization organization, KC.Entities.ResourceCatalog resourcePicture, Credential credential) :
            base ( idPerson,  idLanguage,  firstName,  lastName,  nickName,  posName,  idSalutationType,  organization, resourcePicture, credential)
        {
            
        }

    }
}
