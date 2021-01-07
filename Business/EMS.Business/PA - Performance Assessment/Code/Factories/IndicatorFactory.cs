using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA
{
    internal class IndicatorFactory
    {
        internal IndicatorFactory() { }

        internal Entities.Indicator CreateIndicator(Int64 idIndicator, Int64 idMagnitud, Boolean IsCumulative, String name, String description, String scope, String limitation, String definition, String idLanguage, Credential credential)
        {
            return new Entities.Indicator(idIndicator, idMagnitud, IsCumulative, name, description, scope, limitation, definition, idLanguage, credential);            
        }
    }
}
