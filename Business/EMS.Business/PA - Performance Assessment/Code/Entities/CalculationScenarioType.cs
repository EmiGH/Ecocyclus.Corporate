using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculationScenarioType
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdScenarioType;
            private CalculationScenarioType_LG _LanguageOption;
            private Collections.CalculationScenarioTypes_LG _LanguagesOptions;           
        #endregion

        #region External Properties
            public Int64 IdScenarioType
            {
                get { return _IdScenarioType; }
            }
            public CalculationScenarioType_LG LanguageOption
            {
                get { return _LanguageOption; }             
            }
            public Collections.CalculationScenarioTypes_LG LanguagesOptions
            {
                get 
                {
                    if (_LanguagesOptions == null)
                    { _LanguagesOptions = new Collections.CalculationScenarioTypes_LG(_IdScenarioType, _Credential); }
                    return _LanguagesOptions; 
                }
            }
            public void Modify(String name, String description, Dictionary<Int64, PF.Entities.ProcessClassification> processClassifications)
            {

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //Hace el update sobre el Type
                    new PA.Collections.CalculationScenarioTypes(_Credential).Modify(_IdScenarioType, name, description);
                    //Elimina todos los Type de las clasificaciones....
                    new PA.Collections.CalculationScenarioTypeProcessClassifications(_Credential).RemoveRelatedClassification(_IdScenarioType);
                    //Inserta todas las clasificaciones
                    //Recorre para todas las clasificaciones e inserta una por una.
                    foreach (PF.Entities.ProcessClassification _processClassification in processClassifications.Values)
                    {
                        new PA.Collections.CalculationScenarioTypeProcessClassifications(_Credential).Add(_IdScenarioType, _processClassification.IdProcessClassification);
                    }
                    _transactionScope.Complete();
                }
            }
            public Dictionary<Int64, PF.Entities.ProcessClassification> ProcessClassification
            {
                get 
                {
                    Dictionary<Int64, PF.Entities.ProcessClassification> _processClassifications = new Dictionary<long, Condesus.EMS.Business.PF.Entities.ProcessClassification>();
                    List<Entities.CalculationScenarioTypeProcessClassification> _calculationScenarioTypeProcessClassification = new Collections.CalculationScenarioTypeProcessClassifications(_Credential).ItemsByType(_IdScenarioType);
                    foreach (CalculationScenarioTypeProcessClassification _CalculationScenarioTypeProcessClassification in _calculationScenarioTypeProcessClassification)
                    {
                        _processClassifications.Add(_CalculationScenarioTypeProcessClassification.IdProcessClassification.IdProcessClassification, _CalculationScenarioTypeProcessClassification.IdProcessClassification);
                    }
                    return _processClassifications;
                }
            }
        #endregion

        internal CalculationScenarioType(Int64 idScenarioType, String name, String description, Credential credential) 
        {
            _Credential = credential;
            _IdScenarioType = idScenarioType;
            _LanguageOption = new CalculationScenarioType_LG(_Credential.CurrentLanguage.IdLanguage, name, description);
        }

    }
}
