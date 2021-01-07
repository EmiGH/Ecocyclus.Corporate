using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.KC.Entities
{
    public class ResourceType_LG
    {
        #region Internal Properties
            private String _Name; //El nombre en la opción de idioma
            private String _Description;
            private String _IdLanguage; //El código ISO del lenguaje
        #endregion

        #region External Properties
            public String Name 
            {
                get { return _Name; }
            }
            public String Description
            {
                get { return _Description; }
            }
            public DS.Entities.Language Language
            {
                get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
            }
        #endregion

            internal ResourceType_LG(String idLanguage, String name, String description) 
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdLanguage = idLanguage;
            _Name= name;
            _Description = description;
        }

    }
}
