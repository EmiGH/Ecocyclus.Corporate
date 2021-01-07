using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Factories
{
    internal class ExceptionFactory
    {
        internal ExceptionFactory() { }

        internal Entities.Exception CreateException(Int64 idException, Int64 idExceptionType, Int64 idExceptionState, String comment, DateTime exceptionDate, Credential credential)
        {
            switch (idExceptionType)
            {

                case Common.Constants.ExceptionTypeTaskOverdue:
                    return new Entities.ExceptionProcessTask(idException, idExceptionType, idExceptionState, comment, exceptionDate, credential);

                case Common.Constants.ExceptionTypeExecutionNotOK:
                    return new Entities.ExceptionProcessTask(idException, idExceptionType, idExceptionState, comment, exceptionDate, credential);

                case Common.Constants.ExceptionTypeMeasurementOutofRange:
                    return new Entities.ExceptionProcessTask(idException, idExceptionType, idExceptionState, comment, exceptionDate, credential);

                case Common.Constants.ExceptionTypeResourceExpiration:
                    return new Entities.ExceptionResourceVersion(idException, idExceptionType, idExceptionState, comment, exceptionDate, credential);

                default:
                    return null;
            }
        }
    }
}
