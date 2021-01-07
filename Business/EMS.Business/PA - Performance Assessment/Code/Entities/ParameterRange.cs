using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class ParameterRange
    {
        #region Internal Properties
            private Int64 _IdParameterRange;
            private Int64 _IdParameter;
            private Parameter _Parameter;
            private Double _LowValue;
            private Double _HighValue;
            private Credential _Credential;
        #endregion

        #region External Properties
            public Int64 IdParameterRange
            {
                get { return _IdParameterRange; }
            }
            public Double LowValue
            {
                get { return _LowValue; }
            }
            public Double HighValue
            {
                get { return _HighValue; }
            }
            internal Parameter Parameter
            {
                get
                {
                    if (_Parameter == null)
                    { _Parameter = new Collections.Parameters(_Credential).Item(_IdParameter); }
                    return _Parameter;
                }
            }
        #endregion

            internal ParameterRange(Int64 idParameterRange, Int64 idParameter, Double lowValue, Double highValue, Credential credential)
        {
            _Credential = credential;
            _IdParameterRange = idParameterRange;
            _IdParameter = idParameter;
            _LowValue = lowValue;
            _HighValue = highValue;
        }

        /// <summary>
        /// modificacion de un parameterrange
        /// </summary>
        /// <param name="lowValue"></param>
        /// <param name="HighValue"></param>
            public void Modify(Double lowValue, Double HighValue)
        {
            new Collections.ParameterRanges(Parameter, _Credential).Modify(this, lowValue, HighValue);
        }
    }
}
