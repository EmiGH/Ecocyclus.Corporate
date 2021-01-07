using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    public abstract class Exception : INotificationReported
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdException;        
        private String _Comment;
        private DateTime _ExceptionDate;      
        private Int64 _IdExceptionType;
        private ExceptionType _ExceptionType;
        private Int64 _IdExceptionState;
        private ExceptionState _ExceptionState;        
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdException
        {
            get { return _IdException; }
        }
        public String Comment
        {
            get { return _Comment; }
        }
        public DateTime ExceptionDate
        {
            get { return _ExceptionDate; }
        }
        public ExceptionState ExceptionState
        { get
            {
                if (_ExceptionState == null)
                { _ExceptionState = new IA.Collections.ExceptionStates(_Credential).Item(_IdExceptionState); }
                return _ExceptionState;
            } 
        }
        public ExceptionType ExceptionType
        {
            get
            {
                if (_ExceptionType == null)
                { _ExceptionType = new Collections.ExceptionTypes(_Credential).Item(_IdExceptionType); }
                return _ExceptionType;
            }
        }

        #region Process Group Exception

        public PF.Entities.ProcessGroupException ProcessGroupException()
            {
                PF.Entities.ProcessGroupException _processGroupException = new PF.Collections.ProcessGroupExceptions(_Credential).ItemByException(_IdException);
                return _processGroupException;
            }

        public PF.Entities.ProcessGroupException ProcessGroupExceptionAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description,
                    Int16 threshold)
            {

                //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                //y luego se graban las fechas de medicion que se habilitan para recuperar.
                using (TransactionScope _transactionScope = new TransactionScope())
                {   
                    //crea el process
                    PF.Entities.ProcessGroupException _processGroupException = new PF.Collections.ProcessGroupExceptions(_Credential).Add(weight, orderNumber, title, purpose, description, threshold);

                    //crea la relacion
                    new IA.Collections.Exceptions(_Credential).AddExceptionProcessGroupException(_processGroupException.IdProcess, _IdException);

                    // Completar la transacción
                    _transactionScope.Complete();

                    return _processGroupException;
                }
                
            }

        public void ProcessGroupExceptionRemove(PF.Entities.ProcessGroupException process)
        {
            //Se borra el Group Exception y se encarga de borrar a todos sus hijos.
            new PF.Collections.ProcessGroupExceptions(_Credential).Remove(process);
        }
        #endregion

        #region INotificationReported
        public void ChangeStatusNotification(Int64 idError)
        {
            Modify(false);
        }
        
        public abstract List<NT.Entities.NotificationMessage> NotificationMessages { get; }

        #endregion

        #endregion

        internal virtual void Remove()//borra dependencias
        {
            //borra el IA_ExceptionHistories
            new IA.Collections.ExceptionHistories(_Credential).Remove(this);
        }


        internal Exception(Int64 idException, Int64 idExceptionType, Int64 idExceptionState, String comment, DateTime exceptionDate, Credential credential)
        {
            _Credential = credential;
            _IdException = idException;
            _IdExceptionType = idExceptionType;
            _IdExceptionState = idExceptionState;
            _Comment = comment;
            _ExceptionDate = exceptionDate;
        }

        public void Modify(Boolean notify)
        {
            new Collections.Exceptions(_Credential).Modify(this, notify);
        }

        public void Treat(String comment)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                _ExceptionState = ExceptionState.Treat(_IdException, comment);
                _Comment = comment;
                _IdExceptionState = _ExceptionState.IdExceptionState;
                Modify(true); //PAra que notifique
                _TransactionScope.Complete();
            }
        }

        public void Close(String comment)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                _ExceptionState = ExceptionState.Close(_IdException, comment);
                _Comment = comment;
                _IdExceptionState = _ExceptionState.IdExceptionState;
                Modify(true); //PAra que notifique
                _TransactionScope.Complete();
            }
        }

        
    }
}
