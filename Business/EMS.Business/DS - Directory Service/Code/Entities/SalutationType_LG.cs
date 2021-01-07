using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class SalutationType_LG
    {
        #region Internal Properties
            private String _Name; //El nombre del addressing type en la opci�n de idioma
            private String _Description; //La descripcion del addressing type en la opci�n de idioma
            private String _IdLanguage; //El c�digo ISO del lenguaje
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

        internal SalutationType_LG(String idLanguage, String name, String description) 
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdLanguage = idLanguage;
            _Name= name;
            _Description = description;
        }
    }
}
