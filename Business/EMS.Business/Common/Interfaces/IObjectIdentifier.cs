using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business
{
    public interface IObjectIdentifier
    {
         Int64 IdObject { get; }
         String ClassName { get; }         
    }
}
