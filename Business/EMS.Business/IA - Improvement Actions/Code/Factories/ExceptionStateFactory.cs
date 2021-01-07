using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Factories
{
    internal class ExceptionStateFactory
    {
        private ExceptionStateFactory() { }

        static public IA.Entities.ExceptionState CreateExceptionState(Int64 idExceptionState, String idLanguage, String name, Credential credential)
        {                       
            if (idExceptionState == 1) { return new IA.Entities.ExceptionStateOpen(idExceptionState, idLanguage, name, credential); }

            if (idExceptionState == 2) { return new IA.Entities.ExceptionStateTreat(idExceptionState, idLanguage, name, credential); }

            if (idExceptionState == 3) { return new IA.Entities.ExceptionStateClose(idExceptionState, idLanguage, name, credential); }

            return null;
        }
    }
}
