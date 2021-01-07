using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    
    public class ExceptionStateClose : ExceptionState
    {   

        internal ExceptionStateClose(Int64 idExceptionState, String idLanguage, String name, Credential credential)
            : base (idExceptionState, idLanguage, name, credential)
        { }

        internal override ExceptionState Treat(Int64 idException, String comment)
        {
            //no hace nada
            throw new ApplicationException ("You can not Treat a Closed Exception");
        }

        internal override ExceptionState Close(Int64 idException, String comment)
        {
            //no hace nada
            throw new ApplicationException("You can not Close a Closed Exception");
        }
    }
}
