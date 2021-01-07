using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.KC.Entities
{
    public class Resource_LG
    {
        #region Internal Properties
            private String _Title; //El nombre en la opción de idioma
            private String _Description;
            private String _IdLanguage; //El código ISO del lenguaje
        #endregion

        #region External Properties
            public String Title
            {
                get { return _Title; }
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

            internal Resource_LG(String idLanguage, String title, String description) 
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdLanguage = idLanguage;
            _Title= title;
            _Description = description;
        }
    }
}
