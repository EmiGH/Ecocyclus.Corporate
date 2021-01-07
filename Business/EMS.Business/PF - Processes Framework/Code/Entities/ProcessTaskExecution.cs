using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessTaskExecution
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdProcess;
            private ProcessTask _ProcessTask;
            private Int64 _IdExecution;

            private Int64 _IdPerson;//Identificador de la persona      
            private Int64 _IdPosition;//Identificador de la posicion      
            private Int64 _IdGeographicArea;//Identificador del area geografica      
            private Int64 _IdFunctionalArea;//Identificador del area funcional      
            private Int64 _IdOrganization;            

            private DS.Entities.Post _Post;
            private DateTime _Date;
            private String _Comment;
            private Byte[] _Attachment;  
            private Dictionary<Int64, Entities.ProcessTaskExecutionFileAttach> _FileAttachs;
            private Boolean _Notify;
        #endregion

        #region External Properties
            internal Boolean Notify
            {
                get { return _Notify; }
            }
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public Int64 IdExecution
            {
                get { return _IdExecution; }
            }
            public DS.Entities.Post Post
            {
                get
                {
                    if (_Post == null)
                    { _Post = new DS.Collections.Posts(this).Item(_IdPerson, _IdOrganization, _IdGeographicArea, _IdFunctionalArea, _IdPosition); }
                    return _Post;
                }
            }
            public Entities.ProcessTask ProcessTask
            {
                get
                {
                    if (_ProcessTask == null)
                    { _ProcessTask = new Collections.ProcessTasks(_Credential).Item(_IdProcess); }
                    return _ProcessTask;
                }
            }
            public DateTime Date
            {
                get { return _Date; }
            }
            public String Comment
            {
                get { return _Comment; }
            }
            public Byte[] Attachment
            {
                get { return _Attachment;  }
            }
            public DateTime EndDate
            {
                get 
                {
                    //PF.Collections.ProcessTaskExecutions _processTaskExecutions = new Condesus.EMS.Business.PF.Collections.ProcessTaskExecutions(_Credential);

                    //Obtiene la duracion y en que unidad esta medida.
                    //Int32 _duration = _processTaskExecutions.Item(ProcessTask.IdProcess, IdExecution).ProcessTask.Duration;
                    //Int64 _timeUnitDuration = _processTaskExecutions.Item(ProcessTask.IdProcess, IdExecution).ProcessTask.TimeUnitDuration;
                    
                    Int32 _duration = this.ProcessTask.Duration;
                    Int64 _timeUnitDuration = this.ProcessTask.TimeUnitDuration;
                    //Obtiene la fecha Fin de la ejecucion.
                    return Common.Common.CalculateEndDate(_timeUnitDuration, _Date, _duration);
                }
            }
            public String State
            {
                get
                {
                    //Si la fecha de Ejecucion es mayor a la actual, quiere decir que por ahora esta Planeada.
                    if (DateTime.Now < _Date)
                        { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Planned; }

                    //Obtiene la duracion y en que unidad esta medida.
                    Int32 _duration = this.ProcessTask.Duration;
                    Int64 _timeUnitDuration = this.ProcessTask.TimeUnitDuration;
                    //Obtiene la fecha Fin de la ejecucion.
                    DateTime _endDate = Common.Common.CalculateEndDate(_timeUnitDuration, _Date, _duration);

                    //Si la fecha actual esta dentro de las fechas planeada de inicio y fin de la ejecucion
                    if ((DateTime.Now >= _Date) && (DateTime.Now <= _endDate))
                    {
                        //y si el idPerson ya esta cargado (que no es un valor 0), entonces la ejecucion ya esta realizada.
                        if (_Post != null)
                        {
                            return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Finished;
                        }
                        else
                        {
                            //sino esta trabajando.
                            return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Working;
                        }
                    }
                    //Si la fecha actual es mayor a la planificada como fecha final y aun no se ejecuto, quiere decir que esta vencida!!!
                    if ((DateTime.Now > _endDate) && (_Post == null))
                        { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_OverDue; }

                    //Como no entro en ninguna validacion anterior, quiere decir que esta terminada.
                    return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Finished;
                }
            }
            
            public Dictionary<Int64, Entities.ProcessTaskExecutionFileAttach> FileAttachs()
            {
                if (_FileAttachs == null)
                {
                    _FileAttachs = new PF.Collections.ProcessTaskExecutionFileAttachs(_Credential).Items(ProcessTask.IdProcess, IdExecution);
                }
                return _FileAttachs;
            }
            public Entities.ProcessTaskExecutionFileAttach FileAttach(Int64 idFile)
            {
                return new PF.Collections.ProcessTaskExecutionFileAttachs(_Credential).Item(ProcessTask.IdProcess, IdExecution, idFile);
            }
            public void Remove(Entities.ProcessTaskExecutionFileAttach FileAttach)
            {
                new PF.Collections.ProcessTaskExecutionFileAttachs(Credential).Remove(FileAttach);
            }
            
            #region Exceptions
            //Alta de extado de exception
            public IA.Entities.Exception CreateException(String comment)
            {
                return new Business.IA.Collections.Exceptions(_Credential).Add(ProcessTask.IdProcess, IdExecution, Common.Constants.ExceptionTypeExecutionNotOK, comment);
            }
            //mis exceptions
            public List<IA.Entities.Exception> Exception
            {
                get
                {
                    return new IA.Collections.Exceptions(_Credential).ItemByExecution(IdExecution, ProcessTask.IdProcess); 
                }
            }
            #endregion
        #endregion

        /// <summary>
        /// resetea la ejecucion
        /// </summary>
            public void ResetExecution()
            {
                new Collections.ProcessTaskExecutions(Credential).ResetExecution(this);
            }
        
        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal void Remove()
            {
                //borra los file atachh
                foreach (Entities.ProcessTaskExecutionFileAttach _file in FileAttachs().Values)
                {
                    Remove(_file);
                }
                //borra ecepciones, si tiene
                //if (this.Exception != null) 
                //{
                    foreach (IA.Entities.Exception _item in this.Exception)
                    {
                        new IA.Collections.Exceptions(Credential).Remove((IA.Entities.ExceptionProcessTask)_item);
                    }
                //}       
            }

        internal ProcessTaskExecution(Int64 idExecution, Int64 idProcess, Int64 idPerson, Int64 idOrganization, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, DateTime date, String comment, Byte[] attachment, Boolean notify, Credential credential)
        {
            _Credential = credential;
            _IdProcess = idProcess;
            _IdExecution = idExecution;
            _IdPerson = idPerson;
            _IdOrganization = idOrganization;
            _IdGeographicArea = idGeographicArea;
            _IdFunctionalArea = idFunctionalArea;
            _IdPosition = idPosition;
            _Date = date;
            _Comment = comment;
            _Attachment = attachment;
            _Notify = notify;
        }

        public void Modify(DS.Entities.Post post, String comment, Boolean result)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Modifico los datos de la base
                _dbProcessesFramework.ProcessTaskExecutions_UpdateExecution(ProcessTask.IdProcess, IdExecution, post.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, comment, result, Attachment, _Credential.User.Person.IdPerson);

                if (!result)
                {
                    new Business.IA.Collections.Exceptions(_Credential).Add(ProcessTask.IdProcess, IdExecution, Common.Constants.ExceptionTypeExecutionNotOK, Common.Resources.ConstantMessage.CommentExceptionNotOk);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
    }
}
