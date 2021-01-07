﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Formula_LG
    {
        #region Internal Properties
        private string _IdLanguage;
        private string _Name;
        private string _Desciption;
        #endregion

        #region External Properties
        public string Name
        {
            get { return _Name; }
        }

        public string Desciption
        {
            get { return _Desciption; }
        }
        public DS.Entities.Language Language
        {
            get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
        }
        #endregion

        internal Formula_LG(String idLanguage, String name, String description)
        {
            _IdLanguage = idLanguage;
            _Name = name;
            _Desciption = description;
        }
    }
}
