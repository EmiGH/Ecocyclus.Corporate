using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ContactType_LG
    {
        #region Internal Properties
            private String _Name; //El nombre del tipo de contacto en la opción de idioma
            private String _Description; //La descripcion del tipo de contacto en la opción de idioma
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
            public Entities.Language Language
            {
                get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
            }
        #endregion

        internal ContactType_LG(String idLanguage, String name, String description) 
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _Name = name;
            _Description = description;
            _IdLanguage = idLanguage;            
        }
    }
}
