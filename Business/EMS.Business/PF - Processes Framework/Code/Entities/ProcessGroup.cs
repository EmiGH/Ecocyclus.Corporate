using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Entities
{
    public abstract class ProcessGroup : Process
    {
        #region Internal Properties
        private Int32 _Threshold;
        private Dictionary<Int64, PF.Entities.ProcessTask> _ProcessTask;
        #endregion

        #region External Properties
        public override String State
        {
            get { return null; }
        }
        public override  DateTime StartDate
        {
            get { return DateTime.MinValue; }
        }
        public override DateTime EndDate
        {
            get { return DateTime.MinValue; }
        }      
        public override Int32 Completed
        {
            get { return 0; }
        }

        public Int32 Threshold
        {
            get { return _Threshold; }
        }          

        #endregion

        internal ProcessGroup(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential,
                          Int16 threshold)
            : base(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential)
        {

            _Threshold = threshold;         
        }
        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal virtual void Remove()
        {
            base.Remove();
        }

        #region Process Task
        #region Task Common
        public PF.Entities.ProcessTask ProcessTask(Int64 idProcess)
        {
            return new PF.Collections.ProcessTasks(Credential).Item(idProcess);
        }
        public Dictionary<Int64, ProcessTask> ChildrenTask
        {
            get
            {
                if (_ProcessTask == null)
                { _ProcessTask = new PF.Collections.ProcessTasks(Credential).Items(IdProcess); }
                return _ProcessTask;
            }
        }
        public Dictionary<Int64, ProcessTask> ChildrenTaskAdvanceNotice
        {
            get
            {
                if (_ProcessTask == null)
                { _ProcessTask = new PF.Collections.ProcessTasks(Credential).ItemsAdvanceNotice(IdProcess); }
                return _ProcessTask;
            }
        }
        #endregion      
        #endregion

        //#region Nodes
        //    public Dictionary<Int64, ProcessGroupNode> ChildrenNodes
        //    {
        //        get
        //        {
        //            Dictionary<Int64, ProcessGroupNode> _processGroupNodes = new PF.Collections.ProcessGroupNodes(Credential).Items(IdProcess);
        //            return _processGroupNodes;
        //        }
        //    }
        //    public virtual ProcessGroupNode AddNode(Int16 weight, Int16 orderNumber, String title, String purpose, String description,
        //        Int16 threshold)
        //    {
        //        ProcessGroupNode _processGroupNode = new Collections.ProcessGroupNodes(Credential).Add(weight, orderNumber, title, purpose, description, threshold, this);
        //        return (ProcessGroupNode)_processGroupNode;
        //    }
        //    public void Remove(ProcessGroupNode node)
        //    {
        //        using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
        //        {
        //            new PF.Collections.ProcessGroupNodes(Credential).Remove(node);
        //            _transactionScope.Complete();
        //        }

        //    }
        //#endregion

    }
}
