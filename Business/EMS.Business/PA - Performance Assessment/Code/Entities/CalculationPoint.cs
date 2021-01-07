using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    
namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculationPoint
    {
        private DateTime _CalculationDate;
        private Decimal _CalculationValue;

        public DateTime CalculationDate
        {
            get { return _CalculationDate; }
        }
        public Decimal CalculationValue
        {
            get { return _CalculationValue; }
        }

        internal CalculationPoint(DateTime calculationDate, Decimal calculationValue)
        {
            _CalculationDate = calculationDate;
            _CalculationValue = calculationValue;
        }
    }

    //public class CalculationPointPeriod
    //{
    //    private DateTime[] _CalculationPeriodDate;
    //    private Decimal _CalculationValue;

    //    public DateTime[] CalculationPeriodDate
    //    {
    //      get { return _CalculationPeriodDate; }
    //      set { _CalculationPeriodDate = value; }
    //    }
    //    public Decimal CalculationValue
    //    {
    //        get { return _CalculationValue; }
    //    }

    //    internal CalculationPointPeriod(DateTime[] calculationPeriodDate, Decimal calculationValue)
    //    {
    //        _CalculationPeriodDate = calculationPeriodDate;
    //        _CalculationValue = calculationValue;
    //    }
    //}
}