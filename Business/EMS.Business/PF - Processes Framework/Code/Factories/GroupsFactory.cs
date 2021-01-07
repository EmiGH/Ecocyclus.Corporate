using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Factories
{
    internal class GroupsFactory
    {
        private GroupsFactory() { }

        static public PF.Entities.ProcessGroup CreateGroup(Int64 idProcess, String idLanguage, String type, Credential credential)
        {

            if (type == "Project") { return new PF.Collections.ProcessGroupProcesses(credential).Item(idProcess); }

            //if (type == "Node") { return new PF.Collections.ProcessGroupNodes(credential).Item(idProcess); }

            if (type == "Exception") { return new PF.Collections.ProcessGroupExceptions(credential).Item(idProcess); }

            return null;

        }
    }
}
