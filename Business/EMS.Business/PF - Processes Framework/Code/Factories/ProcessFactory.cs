using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Factories
{
    internal class ProcessFactory
    {
        private ProcessFactory() { }

        static public PF.Entities.Process CreateProcess(Int64 idProcess, String type, Credential credential)
        {

            if (type == "Group") { return new PF.Collections.ProcessGroups(credential).Item(idProcess); }

            if (type == "Task") { return new PF.Collections.ProcessTasks(credential).Item(idProcess);}

            return null;
        }
    }
}
