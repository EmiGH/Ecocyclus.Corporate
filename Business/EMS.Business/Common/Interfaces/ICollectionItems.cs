using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    internal interface ICollectionItems
    {
        IEnumerable<System.Data.Common.DbDataRecord> getItems();
    }
}
