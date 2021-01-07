using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA
{
    internal class OperandFactory
    {
        internal OperandFactory() { }

        internal IOperand CreateOperand(Int64 idObject, String className, Credential credential)
        {
            switch (className)
            {
                case Common.ClassName.Measurement:
                    return new Collections.Measurements(credential).Item(idObject);
                case Common.ClassName.Constant:
                    return new Collections.Constants(credential).Item(idObject);
                case Common.ClassName.CalculateOfTransformation:
                    return new Collections.CalculateOfTransformations(credential).Item(idObject);
                default:
                    return null;
            }
        }
    }
}
