﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessClassification_LG
    {
        #region Internal Properties          
            private String _IdLanguage;
            private String _Name;
            private String _Description;
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


            internal ProcessClassification_LG(String name, String description, String idLanguage)
        {
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdLanguage = idLanguage;
            _Name = name;
            _Description = description;
        }
    }
}
