using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class FormulaStoredProcedureParameter
    {
        #region Internal Properties
            private Credential  _Credential;
		    private Int16 _Order;
		    private String _Mode;
		    private String _Name;
		    private String _DataType;
		    private Int16 _CharacterLength;
		    private Int16 _NumericPrecision;
            private Int16 _NumericScale;
        #endregion

        #region External Name
            public Int16 Order
            {
                get { return _Order; }
            }
            public String Mode
            {
                get { return _Mode; }
            }
            public String Name
            {
                get { return _Name; }
            }
            public String DataType
            {
                get { return _DataType; }
            }
            public Int16 CharacterLength
            {
                get { return _CharacterLength; }
            }
            public Int16 NumericPrecision
            {
                get { return _NumericPrecision; }
            }
            public Int16 NumericScale
            {
                get { return _NumericScale; }
            }         
        #endregion

        internal FormulaStoredProcedureParameter(Int16 order, String mode, String name, String dataType, Int16 characterLength, Int16 numericPrecision, Int16 numericScale, Credential credential)
        {
            _Credential = credential;
            _Order = order;
            _Mode = mode;
            _Name = name;
            _DataType = dataType;
            _CharacterLength = characterLength;
            _NumericPrecision = numericPrecision;
            _NumericScale = numericScale;
        }


    }
}
