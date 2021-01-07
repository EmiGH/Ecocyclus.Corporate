using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class Responsibility
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.JobTitle _JobTitle;
            private Entities.GeographicFunctionalArea _GeographicFunctionalArea;
        #endregion

        #region External Properties

        public Entities.GeographicFunctionalArea GeographicFunctionalAreaResponsibility
        { 
            get
            {               
                return _GeographicFunctionalArea; 
            }
        }
        public Entities.JobTitle JobTitle
        {
            get
            {
                return _JobTitle;
            }
        }
        #endregion

        internal Responsibility(JobTitle jobTitle, GeographicFunctionalArea GeographicFunctionalAreaResponsibility, Credential credential) 
        {
            _Credential = credential;
            _JobTitle = jobTitle;
            _GeographicFunctionalArea = GeographicFunctionalAreaResponsibility;
            
        }
    }
}
