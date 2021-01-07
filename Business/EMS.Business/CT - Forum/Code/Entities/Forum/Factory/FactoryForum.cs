using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Factory
{
    internal class FactoryForum
    {
        internal FactoryForum() { }

        internal Entities.Forum CreateForum(Int64 idforum, Boolean isActive, String idLanguage, String name, String description, Credential credential)
        {
            if (isActive)
            {
                return new Entities.ActiveForum(idforum, idLanguage, name, description, credential);
            }
            else
            {
                return new Entities.InactiveForum(idforum, idLanguage, name, description, credential);        
            }
            
        }

    }
}
