﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.IA.Entities
{
    public class ExceptionState_LG
    {
        #region Internal Properties
            private String _Name; //El nombre en la opción de idioma
            private String _IdLanguage; //El código ISO del lenguaje
        #endregion

        #region External Properties
            public String Name 
            {
                get { return _Name; }
            }
            public DS.Entities.Language Language
            {
                get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
            }
        #endregion

            internal ExceptionState_LG(String idLanguage, String name) 
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdLanguage = idLanguage;
            _Name= name;
        }
    }
}
