using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.RM.Entities
{
    public class RiskClassification 
    {
     #region Internal Properties
            private Credential _Credential;
            private Int64 _IdRiskClassification;
            private Int64 _IdParentRiskClassification;   
            private Entities.RiskClassification_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.RiskClassifications_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
            private Dictionary<Int64, Entities.RiskClassification> _Children; //Coleccion de hijas
            private Dictionary<Int64, Entities.Risk> _ChildrenElemnts; //Coleccion de elementos
        #endregion

        #region External Properties
            public Int64 IdRiskClassification
            {
                get { return _IdRiskClassification; }
            }            
            public Int64 IdParentRiskClassification
            {
                get { return _IdParentRiskClassification; }
            }

            public RiskClassification_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.RiskClassifications_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.RiskClassifications_LG(IdRiskClassification, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }
            public Dictionary<Int64, Entities.RiskClassification> ChildrenClassifications
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.RiskClassifications(this, _Credential).Items();
                    }

                    return _Children;
                }
            }
            public Dictionary<Int64, Entities.Risk> ChildrenElements
            {
                get
                {
                    if (_ChildrenElemnts == null)
                    {
                        _ChildrenElemnts = new Collections.Risks(_Credential).Items();
                    }
                    return _ChildrenElemnts;
                }
            }
        #endregion

            internal RiskClassification(Int64 idRiskClassification, Int64 idParentRiskClassification, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdRiskClassification = idRiskClassification;
            _IdParentRiskClassification = idParentRiskClassification;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new RiskClassification_LG(name, description, _Credential.CurrentLanguage.IdLanguage); 
        }

            /// <summary>
            /// Modifica el parent (para el drug&drop)
            /// </summary>
            /// <param name="parent"></param>
            public void Move(RiskClassification parent)
            {

                //Manejo de la transaccion
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Collections.RiskClassifications(parent, _Credential).Modify(this);
                    _transactionScope.Complete();
                }
            }
            /// <summary>
            /// modifica solo el nombre y description
            /// </summary>
            /// <param name="name"></param>
            /// <param name="description"></param>
            public void Modify(String name, String description)
            {
                //Manejo de la transaccion
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Collections.RiskClassifications(_Credential).Modify(this, name, description);
                    _transactionScope.Complete();
                }
            }
      

            
    }
}
