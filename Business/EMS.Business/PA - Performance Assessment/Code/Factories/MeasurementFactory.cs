using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA
{
    internal class MeasurementFactory
    {
        internal MeasurementFactory() { }

        internal Entities.Measurement CreateMeasurement(Int64 idMeasurement, Int64 idDevice,  Int64 idIndicator, Int64 idTimeUnitFrequency, 
            Int32 frequency, Int64 idMeasurementUnit, String name, String description, Boolean isCumulative, Boolean isRegressive,
            Boolean isRelevant, String idLanguage, Credential credential, String source, String frequencyAtSource,
            Decimal uncertainty, Int64 idQuality, Int64 idMethodology)
        {

            if (isCumulative)
            {
                return new Entities.MeasurementExtensive(idMeasurement, idDevice, idIndicator, idTimeUnitFrequency, frequency,
                    idMeasurementUnit, name, description, isRegressive, isRelevant, idLanguage, credential, source, frequencyAtSource, uncertainty,
                    idQuality, idMethodology);
            }
            else
            {
                return new Entities.MeasurementIntensive(idMeasurement, idDevice, idIndicator, idTimeUnitFrequency, frequency,
                    idMeasurementUnit, name, description, isRegressive, isRelevant, idLanguage, credential, source, frequencyAtSource, uncertainty,
                    idQuality, idMethodology);
            }
        }
    }
}
