using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    interface IExtendedProperty
    {
        List<EP.Entities.ExtendedPropertyValue> ExtendedPropertyValues { get; }

        EP.Entities.ExtendedPropertyValue ExtendedPropertyValue(Int64 idExtendedProperty);

        void ExtendedPropertyValueAdd(EP.Entities.ExtendedProperty extendedProperty, String value);

        void Remove(EP.Entities.ExtendedPropertyValue extendedPropertyValue);

        void ExtendedPropertyValueModify(EP.Entities.ExtendedPropertyValue extendedPropertyValue, String value);
    }
}
