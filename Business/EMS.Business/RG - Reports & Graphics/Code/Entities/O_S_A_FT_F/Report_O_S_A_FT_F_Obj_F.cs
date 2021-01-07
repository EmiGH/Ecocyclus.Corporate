using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.RG.Entities
{
    public class Report_O_S_A_FT_F_Obj_F : IColumnsReport
    {
        #region Internal Properties
        private Int64 _Id;
        private String _Name;
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
        private Report_O_S_A_FT_F_Obj_FT _FT;
        private List<IColumnsReport> _Child;
        private List<IColumnsReport> _Items;
        private Boolean _Show;
        #endregion

       
        internal Collections.Report_O_S_A_FT_F_COL COL
        {
            get { return FT.COL; }
        }
        
        internal Report_O_S_A_FT_F_Obj_FT FT
        {
            get { return _FT; }
        }

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
        //Retorna todos icolum que dependen de el
        public List<IColumnsReport> Items()
        {
            if (_Items == null)
            {
                _Items = new List<IColumnsReport>();

                _Items = COL.Is(this);

            }

            return _Items;
        }
        //Retorna todos icolum que dependen de el
        public List<IColumnsReport> Child()
        {
            return new List<IColumnsReport>();
            //if (_Child == null)
            //{
            //    _Child = new List<IColumnsReport>();

            //    _Child = COL.Fs(this);
                
            //}
            //return _Child;
        }
        #endregion

        #region results//resultado del de los distintos gases
        private void LoadingResults()
        {
            foreach (IColumnsReport _obj in Items())
            {
                _Result_C2H6 = _Result_C2H6 + _obj.Result_C2H6;
                _Result_C3H8 = _Result_C3H8 + _obj.Result_C3H8;
                _Result_C4H10 = _Result_C4H10 + _obj.Result_C4H10;
                _Result_CH4 = _Result_CH4 + _obj.Result_CH4;
                _Result_CO = _Result_CO + _obj.Result_CO;
                _Result_CO2 = _Result_CO2 + _obj.Result_CO2;
                _Result_H2S = _Result_H2S + _obj.Result_H2S;
                _Result_HCNM = _Result_HCNM + _obj.Result_HCNM;
                _Result_HCT = _Result_HCT + _obj.Result_HCT;
                _Result_HFC = _Result_HFC + _obj.Result_HFC;
                _Result_N2O = _Result_N2O + _obj.Result_N2O;
                _Result_NOx = _Result_NOx + _obj.Result_NOx;
                _Result_PFC = _Result_PFC + _obj.Result_PFC;
                _Result_PM = _Result_PM + _obj.Result_PM;
                _Result_PM10 = _Result_PM10 + _obj.Result_PM10;
                _Result_SF6 = _Result_SF6 + _obj.Result_SF6;
                _Result_SO2 = _Result_SO2 + _obj.Result_SO2;
                _Result_SOx = _Result_SOx + _obj.Result_SOx;
                _Result_tCO2e = _Result_tCO2e + _obj.Result_tCO2e;
            }
        }
        public Double Result_tCO2e
        {
            get { return _Result_tCO2e; }
            set { _Result_tCO2e = value; }
        }
        public Double Result_CO2
        {
            get { return _Result_CO2; }
            set { _Result_CO2 = value; }
        }
        public Double Result_CH4
        {
            get { return _Result_CH4; }
            set { _Result_CH4 = value; }
        }
        public Double Result_N2O
        {
            get { return _Result_N2O; }
            set { _Result_N2O = value; }
        }
        public Double Result_PFC
        {
            get { return _Result_PFC; }
            set { _Result_PFC = value; }
        }
        public Double Result_HFC
        {
            get { return _Result_HFC; }
            set { _Result_HFC = value; }
        }
        public Double Result_SF6
        {
            get { return _Result_SF6; }
            set { _Result_SF6 = value; }
        }
        public Double Result_HCT
        {
            get { return _Result_HCT; }
            set { _Result_HCT = value; }
        }
        public Double Result_HCNM
        {
            get { return _Result_HCNM; }
            set { _Result_HCNM = value; }
        }
        public Double Result_C2H6
        {
            get { return _Result_C2H6; }
            set { _Result_C2H6 = value; }
        }
        public Double Result_C3H8
        {
            get { return _Result_C3H8; }
            set { _Result_C3H8 = value; }
        }
        public Double Result_C4H10
        {
            get { return _Result_C4H10; }
            set { _Result_C4H10 = value; }
        }
        public Double Result_CO
        {
            get { return _Result_CO; }
            set { _Result_CO = value; }
        }
        public Double Result_NOx
        {
            get { return _Result_NOx; }
            set { _Result_NOx = value; }
        }
        public Double Result_SOx
        {
            get { return _Result_SOx; }
            set { _Result_SOx = value; }
        }

        public Double Result_SO2
        {
            get { return _Result_SO2; }
            set { _Result_SO2 = value; }
        }
        public Double Result_H2S
        {
            get { return _Result_H2S; }
            set { _Result_H2S = value; }
        }
        public Double Result_PM
        {
            get { return _Result_PM; }
            set { _Result_PM = value; }
        }
        public Double Result_PM10
        {
            get { return _Result_PM10; }
            set { _Result_PM10 = value; }
        }
        public Boolean Show
        {
            get { return _Show; }
            set { _Show = value; }
        }
        #endregion

        internal Report_O_S_A_FT_F_Obj_F(Int64 id, String name, Report_O_S_A_FT_F_Obj_FT ft)
        {
            _Id = id;
            _Name = name;           
            _FT = ft;

            //Se cargan los resultados

            LoadingResults();
        }    
    }
}
