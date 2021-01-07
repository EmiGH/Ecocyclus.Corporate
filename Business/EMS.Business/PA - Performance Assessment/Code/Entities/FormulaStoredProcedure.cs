using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class FormulaStoredProcedure
    {
        #region Internal Properties
            private Credential  _Credential;
            private String _Name;
            private List<Entities.FormulaStoredProcedureParameter> _StoredProcedureParameters;
        #endregion

        #region External Name
            public String Name
            {
                get { return _Name; }
            }
            public List<Entities.FormulaStoredProcedureParameter> StoredProcedureParameters
            {
                get
                {
                    if (_StoredProcedureParameters == null)
                    {
                        _StoredProcedureParameters = new Collections.FormulaStoredProcedureParameters(_Credential).Items(_Name);
                    }
                    return _StoredProcedureParameters;
                }
            }
        #endregion

        internal FormulaStoredProcedure(String name, Credential credential)
        {
            _Credential = credential;
            _Name = name;
        }


    }
}
