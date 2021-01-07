using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;

namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculationCertificated
    {
        #region Internal Properties
            private Int64 _IdCalculation;    
            private Int64 _IdCertificated;
            private DateTime _StartDate;
            private DateTime _EndDate;
            private Decimal _Value;
            private Int64 _IdOrganizationDOE;
            private Credential _Credential;
        #endregion

        #region External Properties
            public Int64 IdCertificated
            {
                get { return _IdCertificated; }
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
            public Int64 IdOrganizationDOE
            {
                get { return _IdOrganizationDOE; }
           }

            #region Calculation
                public PA.Entities.Calculation Calculation
                {
                    get { return new PA.Collections.Calculations(_Credential).Item(_IdCalculation); }
                }
            #endregion

            #region Certification
                public Decimal CertificationDeviation(PA.Entities.CalculationScenarioType calculationScenarioType)
                {
                    Decimal _result = 0;
                    //Se trae todos los calculos estimados para el ScenarioType indicado.
                    List<PA.Entities.CalculationEstimated> _calculationEstimatedByScenarioType = new PA.Collections.CalculationEstimates(_Credential).ItemsByScenarioType(Calculation, calculationScenarioType);
                    //Recorre todas las estimaciones para el escenario indicado.
                    //Debe ir sumando todas las estimaciones.
                    foreach (PA.Entities.CalculationEstimated _calculationEstimated in _calculationEstimatedByScenarioType)
                    {
                        DateTime _startDateReal;
                        DateTime _endDateReal;
                        Int32 _daysCampaign;
                        Int32 _daysEstimated;
                        Decimal _estimatedByDay;

                        //Obtiene la cantidad de dias de la Estimacion.
                        _daysCampaign = _calculationEstimated.EndDate.Subtract(_calculationEstimated.StartDate).Days;
                        _estimatedByDay = _calculationEstimated.Value / _daysCampaign;

                        //Ahora va a obtener la cantidad de dias que realmente impactan en esta estimacion.
                        //Si la fecha de inicio de la estimacion es mas actual que la del periodo, tomo la fecha de la estimacion, sino la del periodo.
                        if (_calculationEstimated.StartDate >= _StartDate)
                            { _startDateReal = _calculationEstimated.StartDate; }
                        else
                            { _startDateReal = _StartDate; }

                        //Si la fecha fin de la estimacion es mas grande que la actual, tomo la fecha de hoy, sino la de la estimacion.
                        if (_calculationEstimated.EndDate >= _EndDate)
                            { _endDateReal = _EndDate; }
                        else
                            { _endDateReal = _calculationEstimated.EndDate; }

                        //Para obtener la cantidad de dias, uso el subtract y pregunto por la cantidad de dias. (el famoso TimeSpan...)
                        _daysEstimated = _endDateReal.Subtract(_startDateReal).Days;
                        //La diferencia en dias siempre tiene que ser positiva...sino es que no hay fechas a tener en cuenta.
                        if (_daysEstimated >= 0)
                        {
                            //Ahora que tengo cuantos dias realmente se deben aplicar, multiplico el valor dia con la cantidad de dias.
                            _result += _estimatedByDay * _daysEstimated;
                        }
                        else
                        {
                            _result += 0;
                        }
                    }
                    //Retorna el resultado.
                    return _result;
                }
            #endregion

        #endregion

        internal CalculationCertificated(Int64 idCalculation, Int64 idCertificated, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE, Credential credential)
        {
            _IdCalculation = idCalculation;
            _IdCertificated= idCertificated;
            _StartDate= startDate;
            _EndDate = endDate;
            _Value= value;
            _IdOrganizationDOE = idOrganizationDOE;
            _Credential = credential;
        }

        public void Modify(Calculation calculation, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE)
        {
            new Collections.CalculationCertificates(_Credential).Modify(calculation, _IdCertificated, startDate, endDate, value, idOrganizationDOE);
        }

    }
}
