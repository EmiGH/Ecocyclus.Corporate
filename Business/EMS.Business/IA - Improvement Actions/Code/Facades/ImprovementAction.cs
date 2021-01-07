using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA
{
    public class ImprovementAction : IModule
    {

        #region Internal Properties
        private Credential _Credential;
        #endregion

        #region External Properties
        public String ModuleName
        {
            get { return "IA"; }
        }

        #region MAP
        public Entities.MapIA Map
        {
            get { return new Entities.MapIA(_Credential); }
        }
        #endregion

        #region CONFIGURATION
        public Entities.ConfigurationIA Configuration
        {
            get { return new Entities.ConfigurationIA(_Credential); }
        }
        #endregion

        internal void ExceptionAutomaticAlert()
        {
            new IA.Collections.Exceptions(_Credential).ExceptionAutomaticAlert();
        }
        #endregion

        internal ImprovementAction(Credential credential)
        {
            _Credential = credential;
        }
    }
}
