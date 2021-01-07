using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessClassification 
    {
        #region Internal Properties
        private Credential _Credential;    
            private Int64 _IdProcessClassification;
            private Int64 _IdParentProcessClassification;
            private ProcessClassification _Parent;       
            private Entities.ProcessClassification_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.ProcessClassifications_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
            private Dictionary<Int64, Entities.ProcessClassification> _Children; //Coleccion de hijas
            private Dictionary<Int64, Entities.ProcessGroupProcess> _ChildrenProcess; //Coleccion de Project hijos
        #endregion

        #region External Properties
            public Int64 IdProcessClassification
            {
                get { return _IdProcessClassification; }
            }
            public Int64 IdParentProcessClassification
            {
                get { return _IdParentProcessClassification; }
            }
            public ProcessClassification ParentProcessClassification
            {
                get
                {
                    if (_Parent == null)
                    { _Parent = new Collections.ProcessClassifications(_Credential).Item(_IdParentProcessClassification); }
                    return _Parent;
                }
            }

            public ProcessClassification_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.ProcessClassifications_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.ProcessClassifications_LG(this, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }

            public Dictionary<Int64, Entities.ProcessClassification> ChildrenClassifications
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.ProcessClassifications(this, _Credential).Items();
                    }

                    return _Children;
                }
            }
            public Dictionary<Int64, ProcessGroupProcess> ChildrenElements
            {
                get
                {
                    if (_ChildrenProcess == null)
                    {
                        _ChildrenProcess = new Collections.ProcessGroupProcesses(_Credential).Items(_IdProcessClassification);
                    }
                    return _ChildrenProcess;
                }
            }

            /// <summary>
            /// Borra las clasificaciones hijas 
            /// </summary>
            internal void Remove()
            {
                Collections.ProcessClassifications _processClassifications = new Collections.ProcessClassifications(_Credential);
                foreach (ProcessClassification _processClassification in ChildrenClassifications.Values)
                {
                    _processClassifications.Remove(_processClassification);
                }
            }

            internal void ProcessGroupProcessAdd(ProcessGroupProcess process)
            {
                new PF.Collections.ProcessGroupProcesses(_Credential).AddRelationship(process, this);
            }
            internal void Remove(ProcessGroupProcess process)
            {
                new PF.Collections.ProcessGroupProcesses(_Credential).RemoveRelationship(process, this);
            }


            #region CalculationScenarioType
                public Dictionary<Int64, PA.Entities.CalculationScenarioType> CalculationScenarioTypes()
                {
                    return new PA.Collections.CalculationScenarioTypes(_Credential).Items(_IdProcessClassification);
                }
            #endregion

        #endregion

            internal ProcessClassification(Int64 idProcessClassification, Int64 idParentProcessClassification, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdProcessClassification = idProcessClassification;
            _IdParentProcessClassification= idParentProcessClassification;     
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new ProcessClassification_LG(name, description, _Credential.CurrentLanguage.IdLanguage);
        }


            /// <summary>
            /// Modifica el parent (para el drug&drop)
            /// </summary>
            /// <param name="parent"></param>
            public void Move(ProcessClassification parent)
            {
                //Manejo de la transaccion
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Collections.ProcessClassifications(parent, _Credential).Modify(this);
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
                    new PF.Collections.ProcessClassifications(_Credential).Modify(this, name, description);
                    _transactionScope.Complete();
                }
            }

   }

}
