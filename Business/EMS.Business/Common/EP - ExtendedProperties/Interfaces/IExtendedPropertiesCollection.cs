using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.EP
{
    interface IExtendedPropertiesCollection : ICollectionItems
    {
        IEnumerable<System.Data.Common.DbDataRecord> getItem(Int64 idExtendedProperty);

        Entities.ExtendedPropertyValue Add(Entities.ExtendedProperty extendedProperty, String value);
        void Remove(Entities.ExtendedPropertyValue extendedPropertyValue);
        void Modify(Entities.ExtendedPropertyValue extendedPropertyValue, String value);

    }
}
