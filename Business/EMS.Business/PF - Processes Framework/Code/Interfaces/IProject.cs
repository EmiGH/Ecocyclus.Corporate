using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PF.Interfaces
{
    interface IProject
    {
        PF.Entities.Process ProcessGroupProcess { get; }
    }
}
