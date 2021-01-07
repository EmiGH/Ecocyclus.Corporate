using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Factories
{
    internal class ResourceFactory
    {
        private ResourceFactory() { }

        //el factory contiene un metodo publico y estatico que puede ser utilizado por sin necesidad de instanciar la clase
        static public KC.Entities.Resource CreateResource(Int64 idResource, Entities.ResourceType resourceType, String title, String description, Int64 currentFile, String type, Credential credential)
        {
            if (type == Common.Constants.TypeResourceFile)
                //type ResourceFile
            { return new KC.Entities.ResourceVersion(idResource, resourceType, title, description, credential, currentFile); }
            else
                //Type ResourceCatalog
            { return new KC.Entities.ResourceCatalog(idResource, resourceType, title, description, credential); }                        
        }

    }
}
