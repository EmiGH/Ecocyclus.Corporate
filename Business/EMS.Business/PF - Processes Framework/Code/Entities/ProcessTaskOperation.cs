using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessTaskOperation : ProcessTask
    {
        #region Internal Properties
            private String _Comment;
        #endregion

        #region External Properties
            public String Comment
            {
                get { return _Comment; }
            }
            public override ProcessGroupProcess Parent
            {
                get
                {
                    return new PF.Collections.ProcessGroupProcesses(Credential).Item(_IdProcessParent);
                }
            }
        #endregion

            internal ProcessTaskOperation(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential, Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration,Int64 timeUnitInterval, String typeExecution, Int64 idFacility,Int64 idTaskInstruction, Boolean ExecutionStatus,
                            String comment, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
                : base(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, ExecutionStatus, timeUnitAdvanceNotice, advanceNotice)
        {
            _Comment = comment;
        }

            public void Modify(Int16 weight, Int16 orderNumber, String title, String purpose, String description, PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String comment, String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Collections.ProcessTasks(Credential).ModifyTask(this, weight, orderNumber, title, purpose, description, process, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, comment, typeExecution, post, site, taskInstruction, notificationRecipients, timeUnitAdvanceNotice, advanceNotice);
                    _transactionScope.Complete();
                }
            }

            /// <summary>
            /// Borra sus dependencias
            /// </summary>
            internal virtual void Remove()
            {
                //borra ejecuciones
                foreach (ProcessTaskExecution _processTaskExecution in this.ProcessTaskExecutions.Values)
                {
                    this.Remove(_processTaskExecution);
                }

                base.Remove();
            }
    }
}
