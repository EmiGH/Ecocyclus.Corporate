using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public class Post
    {
        #region Internal Properties
            private Credential _Credential;
            private DateTime _StartDate;//Fecha de inicio del puesto
            private DateTime _EndDate;//Fecha de finalizacion del puesto
            private Int64 _IdPerson;//Identificador de la persona      
            private Int64 _IdPosition;//Identificador de la posicion      
            private Int64 _IdGeographicArea;//Identificador del area geografica      
            private Int64 _IdFunctionalArea;//Identificador del area funcional      
            private Int64 _IdOrganization;            
            private List<Entities.Presence> _Presences;
            private Entities.Person _Person;
            private Entities.Position _Position;
            private Entities.FunctionalArea _FunctionalArea;
            private GIS.Entities.GeographicArea _GeographicArea;
            private Entities.JobTitle _JobTitle;
            private List<PF.Entities.ProcessTask> _ProcessTaskOperator;
        #endregion

        #region External Properties
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public Organization Organization
            {
                get { return new Collections.Organizations(_Credential).Item(_IdOrganization); }
            }
            public DateTime StartDate
            {
                get { return _StartDate; }
            }
            public DateTime EndDate
            {
                get { return _EndDate; }
            }            
            public Entities.Person Person
            {
                get
                {
                    return new Collections.People(_Credential).Item(_IdPerson);
                }
            }
            public Entities.JobTitle JobTitle
            {
                get
                {
                    return new Collections.JobTitles(_Credential).Item(_IdOrganization, _IdGeographicArea, _IdFunctionalArea, _IdPosition);
                }
            }

            #region Precences
                public List<Entities.Presence> Presences()
                {
                    if (_Presences == null)
                    {
                        _Presences = new Collections.Presences(this).Items();
                    }
                    return _Presences;
                }
                public Entities.Presence Presence(GIS.Entities.Facility facility)
                {
                    return new Condesus.EMS.Business.DS.Collections.Presences(this).Item(facility);
                }
                public void PresencesAdd(GIS.Entities.Facility facility)
                {
                    Collections.Presences _presences = new Condesus.EMS.Business.DS.Collections.Presences(this);
                    _presences.Add(facility);
                    
                }
                public void Remove(Presence presence)
                {
                    Collections.Presences _presences = new Condesus.EMS.Business.DS.Collections.Presences(this);
                    _presences.Remove(presence);
                }
            #endregion

            #region ProcessTaskOperator
                /// <summary>
                /// devuelve todas las tareas en las que tiene permiso de operacion
                /// </summary>
                public List<PF.Entities.ProcessTask> ProcessTaskOperator
                {
                    get
                    {
                        if (_ProcessTaskOperator == null)
                        { _ProcessTaskOperator = new PF.Collections.ProcessTasks(Credential).ReadExecutionPermissions(this); }
                        return _ProcessTaskOperator;
                    }
                }
            #endregion

            #region ResourceFileHistories

            #endregion
 
            #region Post
                /// <summary>
                /// Borra sus dependencias           
                /// </summary>
                internal void Remove()
                {
                    //Borra permisos de ejecucion de tareas
                    new PF.Collections.ProcessTasks(_Credential).RemoveExecutionPermission(this);
                    //Borra Precencias
                    foreach (Presence _presence in this.Presences())
                    {
                        this.Remove(_presence);
                    }
                    //Borra ProcessTaskExecution

                    //Borra ProcessTaskPermissions
                    //Borra resource filehistotries
                    // new KC.Collections.ResourceFileHistories(_Credential).Remove(this); no se borran los historicos
                }
            #endregion
        #endregion


                internal Post(Int64 idPerson, Int64 idOrganization, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, DateTime startDate, DateTime endDate, Credential credential)
                {
                    _Credential = credential;
                    _IdPerson = idPerson;
                    _IdOrganization = idOrganization;
                    _IdGeographicArea = idGeographicArea;
                    _IdFunctionalArea = idFunctionalArea;
                    _IdPosition = idPosition;
                    _StartDate = startDate;
                    _EndDate = endDate;
                }

        public void Modify(DateTime startDate, DateTime endDate)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                 new Collections.Posts(this.Organization).Modify(this, startDate, endDate);
                _transactionScope.Complete();
            }
        }

        
    }
}
