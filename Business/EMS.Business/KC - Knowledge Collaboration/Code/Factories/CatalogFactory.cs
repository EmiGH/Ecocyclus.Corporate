using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Factories
{
    internal class CatalogFactory
    {
        private CatalogFactory() { }

        //el factory contiene un metodo publico y estatico que puede ser utilizado por sin necesidad de instanciar la clase
        static public KC.Entities.Catalog CreateCatalog(Int64 idResourceFile, Entities.ResourceCatalog resourceCatalog, DateTime timeStamp, Int64 idPerson, Credential credential, String url, String docType, String docSize, Int64 idFile)
        {
            if (idFile == 0)
            { return new KC.Entities.CatalogURL(idResourceFile, resourceCatalog, timeStamp, idPerson, credential, url); }
            else
            { return new KC.Entities.CatalogDoc(idResourceFile, resourceCatalog, timeStamp, idPerson, credential, docType, docSize, idFile); }                        
        }

    }
}
