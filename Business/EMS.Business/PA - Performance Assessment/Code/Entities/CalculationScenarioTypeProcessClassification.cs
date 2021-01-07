using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculationScenarioTypeProcessClassification
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdScenarioType;
            private Int64 _IdProcessClassification;
        #endregion

        #region External Properties
            public PF.Entities.ProcessClassification IdProcessClassification
            {
                get { return new PF.Collections.ProcessClassifications(_Credential).Item(_IdProcessClassification); }
            }
            public PA.Entities.CalculationScenarioType CalculationScenarioType
            {
                get { return new PA.Collections.CalculationScenarioTypes(_Credential).Item(_IdScenarioType); }
            }
        #endregion

        internal CalculationScenarioTypeProcessClassification(Int64 idScenarioType, Int64 idProcessClassification, Credential credential) 
        {
            _Credential = credential;
            _IdScenarioType = idScenarioType;
            _IdProcessClassification = idProcessClassification;
        }
    }
}
