﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    internal interface ICollectionItem
    {
        IEnumerable<System.Data.Common.DbDataRecord> getItem(Int64 Id);
    }
}
