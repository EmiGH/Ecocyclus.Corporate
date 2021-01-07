using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.RM.Entities
{
    public class Risk 
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdRisk;
        private Int64 _IdMagnitud;
        private RiskClassification _Classification;
        private PA.Entities.Indicator_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private PA.Collections.Indicators_LG _LanguagesOptions;
        #endregion

        public Int64 IdRisk
        {
            get { return _IdRisk; }
        }
    }
}
