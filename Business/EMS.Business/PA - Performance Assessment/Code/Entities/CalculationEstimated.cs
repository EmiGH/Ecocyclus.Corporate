using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;

namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculationEstimated
    {
        #region Internal Properties
            private Int64 _IdCalculation;    
            private Int64 _IdEstimated;
            private DateTime _StartDate;
            private DateTime _EndDate;
            private Decimal _Value;
            private Int64 _IdScenarioType;
            private Credential _Credential;
        #endregion

        #region External Properties
            public Int64 IdEstimated
            {
                get { return _IdEstimated; }
            }
            public DateTime StartDate
            {
                get { return _StartDate; }
            }
            public DateTime EndDate
            {
                get { return _EndDate; }
            }
            public Decimal Value
            {
                get { return _Value; }
            }
            public PA.Entities.CalculationScenarioType CalculationScenarioType
            {
                get { return new PA.Collections.CalculationScenarioTypes(_Credential).Item(_IdScenarioType); }
            }

            #region Calculation
                public PA.Entities.Calculation Calculation
                {
                    get { return new PA.Collections.Calculations(_Credential).Item(_IdCalculation); }
                }
            #endregion
        #endregion

        internal CalculationEstimated(Int64 idCalculation, Int64 idEstimated, DateTime startDate, DateTime endDate, Int64 idScenarioType, Decimal value, Credential credential)
        {
            _IdCalculation = idCalculation;
            _IdEstimated= idEstimated;
            _StartDate= startDate;
            _EndDate = endDate;
            _IdScenarioType = idScenarioType;
            _Value= value;
            _Credential = credential;
        }

        public void Modify(Calculation calculation, DateTime startDate, DateTime endDate, Decimal value, PA.Entities.CalculationScenarioType calculationScenarioType)
        {
            new Collections.CalculationEstimates(_Credential).Modify(calculation, _IdEstimated, startDate, endDate, calculationScenarioType, value);
        }

    }
}
