using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class User
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdPerson;
            private Int64 _IdOrganization;
            private String _Username;
            private String _Password;
            private String _LastIP;
            private DateTime _LastLogin;
            private Boolean _Active;
            private Boolean _ChangePasswordOnNextLogin;
            private Boolean _CannotChangePassword;
            private Boolean _PasswordNeverExpires;
            private Boolean _ViewGlobalMenu;
            private Condesus.EMS.Business.DS.Entities.PersonwithUser _Person;   //Persona
        #endregion

        #region External Properties
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public String LastIP
            {
                get { return _LastIP; }
            }
            public DateTime LastLogin
            {
                get { return _LastLogin; }
            }
            public String  Username
            {
                get { return _Username; }
            }
            public Boolean Active
            {
                get { return _Active; }
            }
            public String Password
            {
                get { return _Password; }
            }
            public Boolean ViewGlobalMenu
            {
                get { return _ViewGlobalMenu; }
            }
            public Boolean IsAdministrator
            {
                get 
                {
                    DataAccess.Security.SecuritySystems _SecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                    foreach(Entities.Post _post in _Person.Posts)
                    {
                        IEnumerable<System.Data.Common.DbDataRecord> _record = _SecuritySystems.IsAdmin(_Person.Organization.IdOrganization, _post.JobTitle.GeographicArea.IdGeographicArea, _post.JobTitle.FunctionalArea.IdFunctionalArea, _post.JobTitle.Position.IdPosition, _Person.IdPerson, Common.Permissions.Manage);
            
                        foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                        {
                            if (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)) == 0)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    return false;
                }
            }
            //borrar esto
            public Int64 IdPerson
            {
                get { return _IdPerson; }
            }
            public Boolean ChangePasswordOnNextLogin 
            {
                get { return _ChangePasswordOnNextLogin; }
            }
            public Boolean CannotChangePassword
            {
                get { return _CannotChangePassword; }
            }
            public Boolean PasswordNeverExpires
            {
                get { return _PasswordNeverExpires; }
            }
            public Organization Organization
            {
                get { return new Collections.Organizations(_Credential).Item(_IdOrganization);}
            }
            public Condesus.EMS.Business.DS.Entities.PersonwithUser Person
            {
                get
                {
                    if (_Person == null)
                    {
                        Condesus.EMS.Business.DS.Collections.People oPeople = new Condesus.EMS.Business.DS.Collections.People(Organization);
                        _Person = (PersonwithUser)oPeople.Item(_IdPerson);
                    }
                    return _Person;
                }
            }
        #endregion

            internal User(Int64 idPerson, Int64 idOrganization, String username, String password, String lastIP, DateTime lastLogin, Boolean active, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Credential credential, Boolean ViewGlobalMenu)
        {
            _Credential = credential;
            _IdPerson = idPerson;
            _IdOrganization = idOrganization;
            _Password = password;
            _LastIP = lastIP;
            _LastLogin = lastLogin;
            _Username = username;
            _Active = active;
            _ChangePasswordOnNextLogin = changePasswordOnNextLogin;
            _CannotChangePassword = cannotChangePassword;
            _PasswordNeverExpires = passwordNeverExpires;
            _ViewGlobalMenu = ViewGlobalMenu;
        }

        #region Public Properties (Facades of Modules)
            public Dashboard Dashboard
            {
                get
                {
                    return new Dashboard(_Credential);
                }
            }
        public Condesus.EMS.Business.DS.DirectoryServices DirectoryServices
        {
            get
            {
                return new Condesus.EMS.Business.DS.DirectoryServices(_Credential);                
            }
        }
        public Condesus.EMS.Business.PA.PerformanceAssessments PerformanceAssessments
        {
            get
            {
                return new Condesus.EMS.Business.PA.PerformanceAssessments(this);                
            }
        }
        public Condesus.EMS.Business.PF.ProcessFramework ProcessFramework
        {
            get
            {
                return new Condesus.EMS.Business.PF.ProcessFramework(_Credential);
            }
        }       
        public Condesus.EMS.Business.KC.KnowledgeCollaboration KnowledgeCollaboration
        {
            get
            {
                return new Condesus.EMS.Business.KC.KnowledgeCollaboration(_Credential);                
            }
        }
        public Condesus.EMS.Business.IA.ImprovementAction ImprovementAction
        {
            get
            {
                return new Condesus.EMS.Business.IA.ImprovementAction(_Credential);                
            }
        }
        public Condesus.EMS.Business.RM.RiskManagement RiskManagement
        {
            get
            {
                return new Condesus.EMS.Business.RM.RiskManagement(_Credential);
            }
        }
        public Condesus.EMS.Business.Security.Security Security
        {
            get
            {
                return new Condesus.EMS.Business.Security.Security(_Credential);
            }
        }

        public Condesus.EMS.Business.CT.CollaborationTools CollaborationTools
        {
            get
            {
                return new Condesus.EMS.Business.CT.CollaborationTools(_Credential);
               
            }
        }

        public Condesus.EMS.Business.EP.ExtendedProperties ExtendedProperties
        {
            get
            {
                return new Condesus.EMS.Business.EP.ExtendedProperties(_Credential);
                
            }
        }
        public Condesus.EMS.Business.GIS.GeographicInformationSystem GeographicInformationSystem
        {
            get
            {
                return new Condesus.EMS.Business.GIS.GeographicInformationSystem(_Credential);

            }
        }
        //public Condesus.EMS.Business.RG.ReportGraphic ReportGraphic
        //{
        //    get
        //    {
        //        return new Condesus.EMS.Business.RG.ReportGraphic(_Credential);

        //    }
        //}
        #endregion

        #region Prueba SP con tabla de parametro
        public void ExecuteSPWithTable(DataTable dtTableValueParam)
        {
            Condesus.EMS.DataAccess.Temporal.ExecWithTableParams2(dtTableValueParam);
        }
        #endregion


        public DataTable Report_MultiObservatory(DataTable dtTVPProcessFilter, DateTime starDate, DateTime endDate)
        {
            return new RG.ReportGraphic(this).MultiObservatory(dtTVPProcessFilter, starDate, endDate);
        }
    }
}
