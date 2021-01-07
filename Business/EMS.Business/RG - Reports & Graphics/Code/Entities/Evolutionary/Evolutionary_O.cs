using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.RG.Entities
{
    public class Evolutionary_O 
    {
        #region Internal Properties
        private Int64 _Id;
        private String _Name;
        private int _TimeYear;
        private Double _Result_tCO2e;
        private Double _Result_CO2;
        private Double _Result_CH4;
        private Double _Result_N2O;
        private Double _Result_PFC;
        private Double _Result_HFC;
        private Double _Result_SF6;
        private Double _Result_HCT;
        private Double _Result_HCNM;
        private Double _Result_C2H6;
        private Double _Result_C3H8;
        private Double _Result_C4H10;
        private Double _Result_CO;
        private Double _Result_NOx;
        private Double _Result_SOx;
        private Double _Result_SO2;
        private Double _Result_H2S;
        private Double _Result_PM;
        private Double _Result_PM10;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Credential _Credential;
        #endregion

        #region Icolums
        public Int64 IdOrganization
        {
            get { return _Id; }
        }
        //nombrr de la entidad
        public DS.Entities.Organization Organization
        {
            get { return new DS.Collections.Organizations(_Credential).Item(_Id); }
        }
        //Retorna todos icolum que dependen de el
        public int TimeYear
        {
            get { return _TimeYear ;}
        }
         
        #endregion

        #region results//resultado del de los distintos gases        
        public Double Result_tCO2e
        {
            get { return _Result_tCO2e; }
        }
        public Double Result_CO2
        {
            get { return _Result_CO2; }
        }
        public Double Result_CH4
        {
            get { return _Result_CH4; }
        }
        public Double Result_N2O
        {
            get { return _Result_N2O; }
        }
        public Double Result_PFC
        {
            get { return _Result_PFC; }
        }
        public Double Result_HFC
        {
            get { return _Result_HFC; }
        }
        public Double Result_SF6
        {
            get { return _Result_SF6; }
        }
        public Double Result_HCT
        {
            get { return _Result_HCT; }
        }
        public Double Result_HCNM
        {
            get { return _Result_HCNM; }
        }
        public Double Result_C2H6
        {
            get { return _Result_C2H6; }
        }
        public Double Result_C3H8
        {
            get { return _Result_C3H8; }
        }
        public Double Result_C4H10
        {
            get { return _Result_C4H10; }
        }
        public Double Result_CO
        {
            get { return _Result_CO; }
        }
        public Double Result_NOx
        {
            get { return _Result_NOx; }
        }
        public Double Result_SOx
        {
            get { return _Result_SOx; }
        }
        public Double Result_SO2
        {
            get { return _Result_SO2; }
        }
        public Double Result_H2S
        {
            get { return _Result_H2S; }
        }
        public Double Result_PM
        {
            get { return _Result_PM; }
        }
        public Double Result_PM10
        {
            get { return _Result_PM10; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
        }
        #endregion

        internal Evolutionary_O(Int64 id, int timeYear, Double idIndicatortCO2e, Double idIndicatorCO2, Double idIndicatorCH4, Double idIndicatortnN2O,
                  Double idIndicatorPFC, Double idIndicatorHFC, Double idIndicatorSF6, Double idIndicatorHCT, Double idIndicatorHCNM,
                  Double idIndicatorC2H6, Double idIndicatorC3H8, Double idIndicatorC4H10, Double idIndicatorCO, Double idIndicatorNOx,
                  Double idIndicatorSOx, Double idIndicatorSO2, Double idIndicatorH2S, Double idIndicatorPM, Double idIndicatorPM10,
                  DateTime startDate, DateTime endDate, Credential credential)
        {
            _Id = id;
            _TimeYear = timeYear;
            _Result_tCO2e = idIndicatortCO2e;
            _Result_CO2 = idIndicatorCO2;
            _Result_CH4 = idIndicatorCH4;
            _Result_N2O = idIndicatortnN2O;
            _Result_PFC = idIndicatorPFC;
            _Result_HFC = idIndicatorHFC;
            _Result_SF6 = idIndicatorSF6;
            _Result_HCT = idIndicatorHCT;
            _Result_HCNM  = idIndicatorHCNM;
            _Result_C2H6 = idIndicatorC2H6;
            _Result_C3H8 = idIndicatorC3H8;
            _Result_C4H10 = idIndicatorC4H10;
            _Result_CO = idIndicatorCO;
            _Result_NOx  = idIndicatorNOx;
            _Result_SOx = idIndicatorSOx;
            _Result_SO2 = idIndicatorSO2;
            _Result_H2S = idIndicatorH2S;
            _Result_PM = idIndicatorPM;
            _Result_PM10 = idIndicatorPM10;
            _StartDate = startDate;
            _EndDate = endDate;
            _Credential = credential;
            
        }
    }
}
