using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    public class Project 
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdProject;
        #endregion

        #region External Properties

        public Int64 IdProject
        {
            get { return _IdProject; }
        }
      
  
        #endregion

        internal Project(Int64 idProject, Credential credential)
        {
            _Credential = credential;
            _IdProject = idProject;
        }
     
    }
}
