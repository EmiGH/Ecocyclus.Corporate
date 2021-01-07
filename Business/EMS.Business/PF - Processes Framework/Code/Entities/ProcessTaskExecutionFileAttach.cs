using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessTaskExecutionFileAttach
    {
        #region Internal Properties
            private Int64 _IdProcess;
            private Int64 _IdExecution;
            private Int64 _IdFile;
        #endregion

        #region External Properties
            public Int64 IdProcess
            {
                get { return _IdProcess; }
            }
            public Int64 IdExecution
            {
                get { return _IdExecution; }
            }
            public Int64 IdFile
            {
                get { return _IdFile; }
            }

            public KC.Entities.FileAttach FileAttachs()
            {
                return new KC.Collections.FileAttachs().Item(_IdFile);
            }
        #endregion

        internal ProcessTaskExecutionFileAttach(Int64 idProcess, Int64 idExecution, Int64 idFile)
        {
            _IdProcess = idProcess;
            _IdExecution = idExecution;
            _IdFile = idFile;
        }
    }
}
