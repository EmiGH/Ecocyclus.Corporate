using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Linq;

namespace Condesus.EMS.Business.DS.Entities
{
    public class Dashboard
    {
        #region Internal Properties
        private Credential _Credential;
        private Dictionary<Int64, PF.Entities.ProcessTaskExecution> _TasksPlanned;
        private Dictionary<Int64, PF.Entities.ProcessTaskExecution> _TasksWorking;
        private Dictionary<Int64, PF.Entities.ProcessTaskExecution> _TasksFinished;
        private Dictionary<Int64, PF.Entities.ProcessTaskExecution> _TasksOverdue;
        private List<PF.Entities.ProcessTask> _processTasks;
        #endregion

        #region External Properties            
        internal Credential Credential
        {
            get { return _Credential; }
        }

        public Dictionary<Int64, PF.Entities.ProcessTaskExecution> TasksPlanned()
            {
                if (_TasksPlanned == null)
                { _TasksPlanned = new PF.Collections.ProcessTaskExecutions(_Credential).ItemsPlanned(); }
                return _TasksPlanned;
            }
        public Dictionary<Int64, PF.Entities.ProcessTaskExecution> TasksPlanned(DateTime plannedDate)
            {
                if (_TasksPlanned == null)
                { _TasksPlanned = new PF.Collections.ProcessTaskExecutions(_Credential).ItemsPlanned(plannedDate); }
                return _TasksPlanned;
            }
        public Dictionary<Int64, PF.Entities.ProcessTaskExecution> TasksWorking
            {
                get
                {
                    if (_TasksWorking == null)
                    { _TasksWorking = new PF.Collections.ProcessTaskExecutions(_Credential).ItemsWorking(); }
                    return _TasksWorking;
                }
}
        public Dictionary<Int64, PF.Entities.ProcessTaskExecution> TasksFinished
            {
                get
                {
                    if (_TasksFinished == null)
                    { _TasksFinished = new PF.Collections.ProcessTaskExecutions(_Credential).ItemsFinished(); }
                    return _TasksFinished;
                }

            }
        public Dictionary<Int64, PF.Entities.ProcessTaskExecution> TaskOverdue
            {
                get
                {
                    if (_TasksOverdue == null)
                    { _TasksOverdue = new PF.Collections.ProcessTaskExecutions(_Credential).ItemsOverdue(); }
                    return _TasksOverdue;
                }

            }
            /// <summary>
            /// Retorna todas las tareas que la persona tiene permisos de Ejecucion. (Operar)
            /// </summary>
            public List<PF.Entities.ProcessTask> TaskOperator
            {
                get
                {
                    if (_processTasks == null)
                    {
                        _processTasks = new List<Condesus.EMS.Business.PF.Entities.ProcessTask>();
                        //Se trae todos los puestos de la persona en cuestion.
                        DS.Entities.Person _person = new DS.Collections.People(_Credential.User.Organization).Item(_Credential.User.Person.IdPerson);
                        List<DS.Entities.Post> _posts = ((DS.Entities.PersonwithUser)_person).Posts;
                        //Recorre todos los puestos
                        foreach (DS.Entities.Post _post in _posts)
                        {
                            //Para cada puesto, recorre sus tareas
                            foreach (PF.Entities.ProcessTask _processTask in _post.ProcessTaskOperator)
                            {
                                //Si no existe aun...lo inserta
                                if (!_processTasks.Contains(_processTask))
                                {
                                    //Me quedo con la tarea que le pertenece a esa persona.
                                    _processTasks.Add(_processTask);
                                }
                            }
                        }
                    }
                    //Devuelve la lista de tareas
                    return _processTasks;
                }
            }

            public Dictionary<Int64, PA.Entities.ConfigurationExcelFile> ConfigurationExcelFiles
            {
                get
                {
                   return new PA.Collections.ConfigurationExcelFiles(_Credential).Items(_Credential.User.Person.IdPerson);
                }
            }

            /// <summary>
            /// retorna todas las organizaciones donde el usr tiene permisos
            /// </summary>
            public Dictionary<Int64, Entities.Organization> Organizations
            {
                get
                {
                    return new Collections.Organizations(_Credential).ReadAllByPerson();
                }
            }
            //#region Processs Roles
            /// <summary>
            /// Retorna todos los Processos en donde el usuario tiene algun ROL.
            /// </summary>
            public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcess
            {
                get
                {
                    return new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPost(_Credential.User.Person);
                    //List<PF.Entities.ProcessGroupProcess> _processGroupProcesses = new List<Condesus.EMS.Business.PF.Entities.ProcessGroupProcess>();
                    ////Se trae todos los puestos de la persona en cuestion.
                    //List<DS.Entities.Post> _posts = new DS.Collections.People(_Credential.User.Organization).Item(_Credential.User.Person.IdPerson).Posts;
                    ////Recorre todos los puestos
                    //foreach (DS.Entities.Post _post in _posts)
                    //{
                    //    Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processes = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPost(_post);
                    //    //Para cada puesto, recorre sus ProcessRoles
                    //        foreach (PF.Entities.ProcessGroupProcess _process in _processes.Values)
                    //        {
                    //            _processGroupProcesses.Add(_process);
                    //        }
                    //}
                    ////Devuelve la lista de ProcessRoles
                    //return _processGroupProcesses;
                }
            }
  
        //public List<PF.Entities.ProcessGroupProcess> ProcessRoleBuyers
        //    {
        //        get
        //        {
        //            Int64 idRoleBuyer = 1;//en la base el BUYER es 1

        //            Security.Entities.RoleType _roleType = new Security.Collections.RoleTypes(_Credential).Item(idRoleBuyer);

        //            List<PF.Entities.ProcessGroupProcess> _processGroupProcesses = new List<Condesus.EMS.Business.PF.Entities.ProcessGroupProcess>();
        //            //Se trae todos los puestos de la persona en cuestion.
        //            Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processes = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPersonAndRoleType(_Credential.User.Person, _roleType); 

        //            foreach (PF.Entities.ProcessGroupProcess _process in _processes.Values)
        //                {
        //                    _processGroupProcesses.Add(_process);
        //                }
        //            //Devuelve la lista de ProcessRoles
        //            return _processGroupProcesses;
        //        }
        //    }
        //public List<PF.Entities.ProcessGroupProcess> ProcessRoleBuyersAndTechnicalDirector
        //        {
        //            get
        //            {
        //                Int64 idRoleBuyer = 1;//en la base el BUYER es 1
        //                Int64 idRoleTechnicalDirector = 2;//en la base el TechnicalDirector es 2
        //                Security.Entities.RoleType _roleTypeBuyer = new Security.Collections.RoleTypes(_Credential).Item(idRoleBuyer);
        //                Security.Entities.RoleType _roleTypeTechnicalDirector = new Security.Collections.RoleTypes(_Credential).Item(idRoleTechnicalDirector);

        //                List<PF.Entities.ProcessGroupProcess> _processGroupProcesses = new List<Condesus.EMS.Business.PF.Entities.ProcessGroupProcess>();
        //                //Se trae todos los puestos de la persona en cuestion.
        //                Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processesBuyer = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPersonAndRoleType(_Credential.User.Person, _roleTypeBuyer);

        //                foreach (PF.Entities.ProcessGroupProcess _process in _processesBuyer.Values)
        //                {
        //                    _processGroupProcesses.Add(_process);
        //                }

        //                Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processesTechnicalDirector = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPersonAndRoleType(_Credential.User.Person, _roleTypeTechnicalDirector);

        //                foreach (PF.Entities.ProcessGroupProcess _process in _processesTechnicalDirector.Values)
        //                {
        //                    _processGroupProcesses.Add(_process);
        //                }
        //                //Devuelve la lista de ProcessRoles
        //                return _processGroupProcesses;
        //            }
        //        }
        //public List<PF.Entities.ProcessGroupProcess> ProcessRoleProcessLeader
        //        {
        //          get
        //            {
        //                Int64 idRoleProcessLeader = 3;//en la base el ProcessLeader es 3
        //                Security.Entities.RoleType _roleTypeProcessLeader = new Security.Collections.RoleTypes(_Credential).Item(idRoleProcessLeader);

        //                List<PF.Entities.ProcessGroupProcess> _processGroupProcesses = new List<Condesus.EMS.Business.PF.Entities.ProcessGroupProcess>();
        //                //Se trae todos los puestos de la persona en cuestion.
        //                Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processesProcessLeader = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPersonAndRoleType(_Credential.User.Person, _roleTypeProcessLeader);

        //                foreach (PF.Entities.ProcessGroupProcess _process in _processesProcessLeader.Values)
        //                {
        //                    _processGroupProcesses.Add(_process);
        //                }
        //                //Devuelve la lista de ProcessRoles
        //                return _processGroupProcesses;
        //            }
        //        }
        //public List<PF.Entities.ProcessGroupProcess> ProcessRoleTechnicalDirector
        //        {
        //             get
        //            {
        //                Int64 idRoleTechnicalDirector = 2;//en la base el TechnicalDirector es 2
        //                Security.Entities.RoleType _roleTypeTechnicalDirector = new Security.Collections.RoleTypes(_Credential).Item(idRoleTechnicalDirector);

        //                List<PF.Entities.ProcessGroupProcess> _processGroupProcesses = new List<Condesus.EMS.Business.PF.Entities.ProcessGroupProcess>();
        //                //Se trae todos los puestos de la persona en cuestion.
        //                Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processesTechnicalDirector = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPersonAndRoleType(_Credential.User.Person, _roleTypeTechnicalDirector);

        //                foreach (PF.Entities.ProcessGroupProcess _process in _processesTechnicalDirector.Values)
        //                {
        //                    _processGroupProcesses.Add(_process);
        //                }
        //                //Devuelve la lista de ProcessRoles
        //                return _processGroupProcesses;
        //            }
        //        }
        //public List<PF.Entities.ProcessGroupProcess> ProcessRoleProcessLeaderAndTechnicalDirector()
        //        {
        //            //get
        //            //{
        //                Int64 idRoleProcessLeader = 3;//en la base el ProcessLeader es 3
        //                Int64 idRoleTechnicalDirector = 2;//en la base el TechnicalDirector es 2
        //                Security.Entities.RoleType _roleTypeProcessLeader = new Security.Collections.RoleTypes(_Credential).Item(idRoleProcessLeader);
        //                Security.Entities.RoleType _roleTypeTechnicalDirector = new Security.Collections.RoleTypes(_Credential).Item(idRoleTechnicalDirector);


        //                List<PF.Entities.ProcessGroupProcess> _processGroupProcesses = new List<Condesus.EMS.Business.PF.Entities.ProcessGroupProcess>();
        //                //Se trae todos los puestos de la persona en cuestion.
        //                Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processesProcessLeader = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPersonAndRoleType(_Credential.User.Person, _roleTypeProcessLeader);

        //                foreach (PF.Entities.ProcessGroupProcess _process in _processesProcessLeader.Values)
        //                {
        //                    _processGroupProcesses.Add(_process);
        //                }

        //                Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processesTechnicalDirector = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPersonAndRoleType(_Credential.User.Person, _roleTypeTechnicalDirector);

        //                foreach (PF.Entities.ProcessGroupProcess _process in _processesTechnicalDirector.Values)
        //                {
        //                    _processGroupProcesses.Add(_process);
        //                }
        //                //Devuelve la lista de ProcessRoles
        //                return _processGroupProcesses;
        //            //}
        //        }

            //#endregion

           #region Processs Exceptions
                //public Dictionary<Int64, IA.Entities.Exception> ProcessExceptions
                //{
                //    get
                //    {
                //        Dictionary<Int64, IA.Entities.Exception> _exceptions = new Dictionary<Int64, Condesus.EMS.Business.IA.Entities.Exception>();

                //        foreach (PF.Entities.ProcessRole _processRole in ProcessRoles)
                //        {
                //            foreach (IA.Entities.Exception _exception in _processRole.ProcessGroupProcess.Exceptions.Values)
                //            {
                //                if (!_exceptions.ContainsKey(_exception.IdException))
                //                { _exceptions.Add(_exception.IdException, _exception); }
                //            }
                //        }
                //        return _exceptions;
                //    }
                //}
            public Dictionary<Int64, IA.Entities.Exception> ProcessExceptionOpen
            {
                get
                {

                    Dictionary<Int64, IA.Entities.Exception> _exceptions = new Dictionary<Int64, Condesus.EMS.Business.IA.Entities.Exception>();

                    foreach (PF.Entities.ProcessTask _task in TaskOperator)
                    {
                        foreach (IA.Entities.Exception _exception in _task.Exceptions.Values)
                        {
                            if (_exception.ExceptionState.IdExceptionState == Common.Constants.ExceptionStateOpen)
                            {
                                if (!_exceptions.ContainsKey(_exception.IdException))
                                { _exceptions.Add(_exception.IdException, _exception); }
                            }
                        }
                    }                    
                    return _exceptions;
                }
            }
            public Dictionary<Int64, IA.Entities.Exception> ProcessExceptionClose
            {
                get
                {
                    Dictionary<Int64, IA.Entities.Exception> _exceptions = new Dictionary<Int64, Condesus.EMS.Business.IA.Entities.Exception>();

                    foreach (PF.Entities.ProcessTask _task in TaskOperator)
                    {
                        //foreach (IA.Entities.Exception _exception in _exceptions.Values)
                        foreach (IA.Entities.Exception _exception in _task.Exceptions.Values)
                        {
                            if (_exception.ExceptionState.IdExceptionState == Common.Constants.ExceptionStateClose)
                            {
                                if (!_exceptions.ContainsKey(_exception.IdException))
                                { _exceptions.Add(_exception.IdException, _exception); }
                            }
                        }
                    }
                    return _exceptions;
                }
            }
                public Dictionary<Int64, IA.Entities.Exception> ProcessExceptionTreat
                {
                    get
                    {
                        Dictionary<Int64, IA.Entities.Exception> _exceptions = new Dictionary<Int64, Condesus.EMS.Business.IA.Entities.Exception>();

                        foreach (IA.Entities.Exception _exception in _exceptions.Values) 
                        {
                            if (_exception.ExceptionState.IdExceptionState == Common.Constants.ExceptionStateTreat)
                            {
                                if (!_exceptions.ContainsKey(_exception.IdException))
                                { _exceptions.Add(_exception.IdException, _exception); }
                            }
                        }
                        return _exceptions;
                    }
                }
            #endregion


        #region for Map
                public Dictionary<Int64, GIS.Entities.Facility> Facilities
                {
                    get
                    {
                        Dictionary<Int64, GIS.Entities.Facility> _facilities = new Dictionary<long, Condesus.EMS.Business.GIS.Entities.Facility>();

                        Dictionary<Int64, GIS.Entities.Site> _sites = new GIS.Collections.Facilities(this).Items();

                        foreach (GIS.Entities.Site site in _sites.Values)
                        {
                            if (site.GetType().Name == "Sector")
                            {
                                GIS.Entities.Facility _facility;
                                GIS.Entities.Sector _sector = (GIS.Entities.Sector)site;

                                if (_sector.Parent.GetType().Name == "Sector")
                                {
                                    _facility = validateFacility((GIS.Entities.Sector)_sector.Parent);
                                }
                                else
                                {
                                    _facility = (GIS.Entities.Facility)_sector.Parent;
                                }
                                if(!_facilities.ContainsKey(_facility.IdFacility))
                                {
                                    _facilities.Add(_facility.IdFacility, _facility);
                                }

                            }
                            else
                            {
                                if (!_facilities.ContainsKey(site.IdFacility))
                                {
                                    _facilities.Add(site.IdFacility, (GIS.Entities.Facility)site);
                                }
                            }
                        }

                        return _facilities;
                    }
                }
        private GIS.Entities.Facility validateFacility (GIS.Entities.Sector sector)
        {
             GIS.Entities.Facility _facility;
             if (sector.Parent.GetType().Name == "Sector")
             {
                 _facility = validateFacility((GIS.Entities.Sector)sector.Parent);
             }
             else
             {
                 _facility = (GIS.Entities.Facility)sector.Parent;
             }
             return _facility;
        }
        #endregion
        
        #endregion

                internal Dashboard(Credential credential)
        {
            _Credential = credential;
        }


    }
}
