using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA
{
    internal class TransformerFactory
    {
        internal TransformerFactory() { }

        internal ITransformer CreateTransformer(Int64 idObject, String className, Credential credential)
        {
            switch (className)
            {
                case Common.ClassName.Measurement:
                    return new Collections.Measurements(credential).Item(idObject);
                case Common.ClassName.CalculateOfTransformation:
                    return new Collections.CalculateOfTransformations(credential).Item(idObject);
                default:
                    return null;
            }
        }

        internal ITransformer CreateTransformer(Int64 idMeasurementTransformer, Int64 idTransformationTransformer, Credential credential)
        {
            if (idMeasurementTransformer != 0)
            {
                return CreateTransformer(idMeasurementTransformer, Common.ClassName.Measurement, credential);
            }
            else
            {
                return CreateTransformer(idTransformationTransformer, Common.ClassName.CalculateOfTransformation, credential);
            }
        }
    }
}
