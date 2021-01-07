using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Factories
{
    internal  class VersionFactory
    {
        private VersionFactory() { }

        //el factory contiene un metodo publico y estatico que puede ser utilizado por sin necesidad de instanciar la clase
        static public KC.Entities.Version CreateVersion(Int64 idResourceFile, Entities.ResourceVersion resourceVersion, DateTime timeStamp, Int64 idPerson, DateTime validForm, DateTime validThrough, String version, Credential credential, String url, String docType, String docSize, Int64 idFile)
        {
            if (idFile == 0)
            { return new KC.Entities.VersionURL(idResourceFile, resourceVersion, timeStamp, idPerson, validForm, validThrough, version, credential, url); }
            else
            { return new KC.Entities.VersionDoc(idResourceFile, resourceVersion, timeStamp, idPerson, validForm, validThrough, version, credential, docType, docSize, idFile); }                        
        }
    }
}
