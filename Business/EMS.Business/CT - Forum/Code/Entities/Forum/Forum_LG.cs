// Generated by Pnyx Generation tool at :05/04/2009 11:10:21 p.m.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Entities
{
    public class Forum_LG
    {
        #region Internal Region
        private Int64 _IdForum;
        private String _IdLanguage;
        private String _Name;
        private String _Description;
        #endregion

        #region External Region

        public Int64 IdForum
        {
            get { return _IdForum; }
        }
        public String IdLanguage
        {
            get { return _IdLanguage; }
        }
        public String Name
        {
            get { return _Name; }
        }
        public String Description
        {
            get { return _Description; }
        }
        #endregion
        #region Constructor
        internal Forum_LG(Int64 idforum, String idlanguage, String name, String description)
        {
            _IdForum = idforum;
            _IdLanguage = idlanguage;
            _Name = name;
            _Description = description;
        }

       
        #endregion
    }
}
