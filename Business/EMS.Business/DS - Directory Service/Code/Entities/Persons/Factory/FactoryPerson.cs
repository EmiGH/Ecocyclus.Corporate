using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Factory
{
    internal class FactoryPerson
    {
        private FactoryPerson() { }

        public static Condesus.EMS.Business.DS.Entities.Person CreatePerson(Int64 idPerson, String idLanguage, String firstName, String lastName, String nickName, String posName, Int64 idSalutationType, Int64 idOrganization, KC.Entities.ResourceCatalog resourcePicture, Credential credential)
        {
            Entities.Organization _Organization = new Collections.Organizations(credential).Item(idOrganization);

            if (new Collections.Users(_Organization, credential).Item(idPerson) == null)
            {
                return new Condesus.EMS.Business.DS.Entities.PersonwithoutUser( idPerson,  idLanguage,  firstName,  lastName,  nickName,  posName,  idSalutationType,  _Organization, resourcePicture, credential);
            }
            else
            {
                return new Condesus.EMS.Business.DS.Entities.PersonwithUser(idPerson, idLanguage, firstName, lastName, nickName, posName, idSalutationType, _Organization, resourcePicture, credential);
            }
        }
    }
}
