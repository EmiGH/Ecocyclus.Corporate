using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.RG.Entities
{
    public class Graphic_Bar_A : IGraphicBar
    {

        #region Internal Properties
        private Int64 _Id;
        private String _Name;

        private Decimal _Result_tCO2e;
        private Decimal _Result_CO2;
        private Decimal _Result_CH4;
        private Decimal _Result_N2O;
        private Decimal _Result_PFC;
        private Decimal _Result_HFC;
        private Decimal _Result_SF6;
        private Decimal _Result_HCT;
        private Decimal _Result_HCNM;
        private Decimal _Result_C2H6;
        private Decimal _Result_C3H8;
        private Decimal _Result_C4H10;
        private Decimal _Result_CO;
        private Decimal _Result_NOx;
        private Decimal _Result_SOx;
        private Decimal _Result_SO2;
        private Decimal _Result_H2S;
        private Decimal _Result_PM;
        private Decimal _Result_PM10;
        #endregion

        #region Icolums
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        //nombrr de la entidad
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }     
        #endregion

        internal Graphic_Bar_A(Int64 id, String name, Decimal Result_tCO2e, Decimal Result_CO2, Decimal Result_CH4, Decimal Result_N2O,
            Decimal Result_PFC, Decimal Result_HFC, Decimal Result_SF6, Decimal Result_HCT, Decimal Result_HCNM, Decimal Result_C2H6,
            Decimal Result_C3H8, Decimal Result_C4H10, Decimal Result_CO, Decimal Result_NOx, Decimal Result_SOx,
            Decimal Result_SO2, Decimal Result_H2S, Decimal Result_PM, Decimal Result_PM10)
        {
            _Id = id;
            _Name = name;

            _Result_tCO2e = Result_tCO2e;
            _Result_CO2 = Result_CO2;
            _Result_CH4 = Result_CH4;
            _Result_N2O = Result_N2O;
            _Result_PFC = Result_PFC;
            _Result_HFC = Result_HFC;
            _Result_SF6 = Result_SF6;
            _Result_HCT = Result_HCT;
            _Result_HCNM = Result_HCNM;
            _Result_C2H6 = Result_C2H6;
            _Result_C3H8 = Result_C3H8;
            _Result_C4H10 = Result_C4H10;
            _Result_CO = Result_CO;
            _Result_NOx = Result_NOx;
            _Result_SOx = Result_SOx;
            _Result_SO2 = Result_SO2;
            _Result_H2S = Result_H2S;
            _Result_PM = Result_PM;
            _Result_PM10 = Result_PM10;
        }

        public Decimal Result_tCO2e
        {
            get { return _Result_tCO2e; }
            set { _Result_tCO2e = value; }
        }
        public Decimal Result_CO2
        {
            get { return _Result_CO2; }
            set { _Result_CO2 = value; }
        }
        public Decimal Result_CH4
        {
            get { return _Result_CH4; }
            set { _Result_CH4 = value; }
        }
        public Decimal Result_N2O
        {
            get { return _Result_N2O; }
            set { _Result_N2O = value; }
        }
        public Decimal Result_PFC
        {
            get { return _Result_PFC; }
            set { _Result_PFC = value; }
        }
        public Decimal Result_HFC
        {
            get { return _Result_HFC; }
            set { _Result_HFC = value; }
        }
        public Decimal Result_SF6
        {
            get { return _Result_SF6; }
            set { _Result_SF6 = value; }
        }
        public Decimal Result_HCT
        {
            get { return _Result_HCT; }
            set { _Result_HCT = value; }
        }
        public Decimal Result_HCNM
        {
            get { return _Result_HCNM; }
            set { _Result_HCNM = value; }
        }
        public Decimal Result_C2H6
        {
            get { return _Result_C2H6; }
            set { _Result_C2H6 = value; }
        }
        public Decimal Result_C3H8
        {
            get { return _Result_C3H8; }
            set { _Result_C3H8 = value; }
        }
        public Decimal Result_C4H10
        {
            get { return _Result_C4H10; }
            set { _Result_C4H10 = value; }
        }
        public Decimal Result_CO
        {
            get { return _Result_CO; }
            set { _Result_CO = value; }
        }
        public Decimal Result_NOx
        {
            get { return _Result_NOx; }
            set { _Result_NOx = value; }
        }
        public Decimal Result_SOx
        {
            get { return _Result_SOx; }
            set { _Result_SOx = value; }
        }

        public Decimal Result_SO2
        {
            get { return _Result_SO2; }
            set { _Result_SO2 = value; }
        }
        public Decimal Result_H2S
        {
            get { return _Result_H2S; }
            set { _Result_H2S = value; }
        }
        public Decimal Result_PM
        {
            get { return _Result_PM; }
            set { _Result_PM = value; }
        }
        public Decimal Result_PM10
        {
            get { return _Result_PM10; }
            set { _Result_PM10 = value; }
        }
    }
}
