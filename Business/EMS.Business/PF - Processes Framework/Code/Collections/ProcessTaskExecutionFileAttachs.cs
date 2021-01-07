using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessTaskExecutionFileAttachs
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal ProcessTaskExecutionFileAttachs(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Common Functions
            internal Entities.ProcessTaskExecutionFileAttach Item(Int64 idProcess, Int64 idExecution, Int64 idFile)
            {
                 //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutionFileAttachs _dbProcessTaskExecutionFileAttachs = _dbProcessesFramework.ProcessTaskExecutionFileAttachs;

                Entities.ProcessTaskExecutionFileAttach _processTaskExecutionFileAttach = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutionFileAttachs_ReadById(idProcess, idExecution, idFile);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTaskExecutionFileAttach == null)
                    {
                        _processTaskExecutionFileAttach = new Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionFileAttach(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(_dbRecord["IdFile"]));
                    }
                    else
                    {
                        return new Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionFileAttach(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(_dbRecord["IdFile"]));
                    }
                }
                return _processTaskExecutionFileAttach;
            }
            internal Dictionary<Int64, Entities.ProcessTaskExecutionFileAttach> Items(Int64 idProcess, Int64 idExecution)
            {
                 //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutionFileAttachs _dbProcessTaskExecutionFileAttachs = _dbProcessesFramework.ProcessTaskExecutionFileAttachs;

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecutionFileAttach> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecutionFileAttach>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutionFileAttachs_ReadAll(idProcess, idExecution);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecutionFileAttach _processTaskExecutionFileAttach = new Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionFileAttach(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(_dbRecord["IdFile"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processTaskExecutionFileAttach.IdFile, _processTaskExecutionFileAttach);
                }
                return _oItems;
            }
        #endregion

        #region write Functions
            internal void Remove(Entities.ProcessTaskExecutionFileAttach fileAttach)
            {
                   //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                _dbProcessesFramework.ProcessTaskExecutionFileAttachs_DeleteExecution(fileAttach.IdProcess, fileAttach.IdExecution, fileAttach.IdFile, _Credential.User.Person.IdPerson);
            }

        #endregion
    }
}
