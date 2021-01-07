using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessGroups
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

            internal ProcessGroups(Credential credential)
        {
            _Credential = credential;
        }



        #region Read Common Functions
            internal Entities.ProcessGroup Item(Int64 idProcess)
            {
                 //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroups_ReadProjectType(idProcess);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return Factories.GroupsFactory.CreateGroup(idProcess, _Credential.CurrentLanguage.IdLanguage, Convert.ToString(_dbRecord["Type"]),_Credential);                   
                }
                return null;
            }
            #endregion
    }
}
