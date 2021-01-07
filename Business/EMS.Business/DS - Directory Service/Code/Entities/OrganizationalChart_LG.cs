﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class OrganizationalChart_LG
    {
        #region Internal Properties
            private String _Name; //El nombre del país en la opción de idioma
            private String _Description; //Descripcion del area geografica
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

        internal OrganizationalChart_LG(String name, String description, String idLanguage)
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdLanguage = idLanguage;
            _Name = name;
            _Description = description; 

        }
    }
}
