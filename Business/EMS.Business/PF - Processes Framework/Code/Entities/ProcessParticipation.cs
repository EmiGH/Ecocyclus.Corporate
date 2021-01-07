using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessParticipation
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParticipationType;
            private Int64 _IdProcess;
            private Int64 _IdOrganization;
            private String _Comment;
            private ProcessGroupProcess _ProcessGroupProcess;
            private ParticipationType _ParticipationType;
            private DS.Entities.Organization _Organization; 
        #endregion

        #region External Properties
            public Int64 IdProcess
            {
                get { return _IdProcess; }
            }
            public Int64 IdParticipationType
            {
                get { return _IdParticipationType; }
            }
            public String Comment
            {
                get {return _Comment;}
            }
            public ProcessGroupProcess ProcessGroupProcess
            {
                get 
                {
                    if (_ProcessGroupProcess == null)
                    { _ProcessGroupProcess = new Collections.ProcessGroupProcesses(_Credential).Item(_IdProcess); }
                    return _ProcessGroupProcess;
                }
            }
            public ParticipationType ParticipationType
            {
                get
                {
                    if (_ParticipationType == null)
                    { _ParticipationType = new Collections.ParticipationTypes(_Credential).Item(_IdParticipationType); }
                    return _ParticipationType;
                }
            }
            public DS.Entities.Organization Organization
            {
                get
                {
                    if (_Organization == null)
                    { _Organization = new DS.Collections.Organizations(_Credential).Item(_IdOrganization); }
                    return _Organization;
                }
            }
            
        #endregion

            internal ProcessParticipation(Int64 idProcess, Int64 idOrganization, Int64 idParticipationType, String comment, Credential credential) 
        {
            _Credential = credential;
            _IdParticipationType = idParticipationType;
            _IdProcess = idProcess;
            _IdOrganization = idOrganization;
            _Comment = comment;
        }

            public void Modify(String comment)
            {
                new Collections.ProcessParticipations(_Credential, ProcessGroupProcess).Modify(_IdOrganization, _IdParticipationType, comment);
            }
    }
}
