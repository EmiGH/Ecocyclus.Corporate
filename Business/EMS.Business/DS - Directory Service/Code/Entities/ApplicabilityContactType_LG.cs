using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ApplicabilityContactType_LG
    {
        #region Internal Properties
            private String _Name; //El nombre del addressing type en la opción de idioma
            private String _IdLanguage; //El código ISO del lenguaje
        #endregion

        #region External Properties
            public String Name
            {
                get { return _Name; }
            }
            public Entities.Language Language
            {
                get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
            }
        #endregion

        internal ApplicabilityContactType_LG(String idLanguage, String name) 
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdLanguage = idLanguage;
            _Name= name;
        }
    }
}
