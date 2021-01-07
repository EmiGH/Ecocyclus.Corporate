using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.CT.Collections.ForumsRead
{
    
    internal class ForumByProcess : ICollectionItems
    {
        private PF.Entities.ProcessGroupProcess _Process;

        internal ForumByProcess(PF.Entities.ProcessGroupProcess process)
        {
            _Process = process;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

            return _dbProcessesFramework.ProcessGroupProcessForums_ReadByProcess(_Process.IdProcess, _Process.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
