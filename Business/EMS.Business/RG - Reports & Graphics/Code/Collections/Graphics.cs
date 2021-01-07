using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG.Collections
{

    public class Graphics
    {
        #region Internal Properties
        private Credential _Credential;

        private PF.Entities.ProcessGroupProcess _ProcessGroupProcess;
    
        private DateTime _StartDate;
        private DateTime _EndDate;
        #endregion

        internal Graphics(Entities.Graphic_Pie graphic_Pie, DateTime startDate, DateTime endDate)
        {
            _Credential = graphic_Pie.Credential;
            _ProcessGroupProcess = graphic_Pie.ProcessGroupProcess;
            _StartDate = startDate;
            _EndDate = endDate;
        }
        internal Graphics(Entities.Graphic_Bar graphic_Bar, DateTime startDate, DateTime endDate)
        {
            _Credential = graphic_Bar.Credential;
            _ProcessGroupProcess = graphic_Bar.ProcessGroupProcess;
            _StartDate = startDate;
            _EndDate = endDate;
        }

        internal List<IGraphicPie> PieScopesByIndicator(Int64 idIndicatortCO2e)
        {
            List<IGraphicPie> _items = new List<IGraphicPie>();
            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_Pie_TotalsScope_by_Indicator(_ProcessGroupProcess.IdProcess,
                idIndicatortCO2e, _StartDate, _EndDate, _Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdScope", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Graphic_Pie_S _s = new Condesus.EMS.Business.RG.Entities.Graphic_Pie_S(
                                                Convert.ToInt64(_dbRecord["IdScope"]),
                                                Convert.ToString(_dbRecord["Name"]),
                                                Convert.ToDecimal(_dbRecord["Result"]),
                                                Convert.ToDecimal(_dbRecord["Percentage"]));    
                _items.Add(_s);
            }

            return _items;
        }
        internal List<IGraphicPie> PieScopesByFacility(Int64 idIndicatortCO2e, Int64 idFacility)
        {
            List<IGraphicPie> _items = new List<IGraphicPie>();
            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_Pie_TotalsScope_by_Facility(_ProcessGroupProcess.IdProcess,
                idIndicatortCO2e, idFacility, _StartDate, _EndDate, _Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdScope", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Graphic_Pie_S _s = new Condesus.EMS.Business.RG.Entities.Graphic_Pie_S(
                                                Convert.ToInt64(_dbRecord["IdScope"]),
                                                Convert.ToString(_dbRecord["Name"]),
                                                Convert.ToDecimal(_dbRecord["Result"]),
                                                Convert.ToDecimal(_dbRecord["Percentage"]));
                _items.Add(_s);
            }

            return _items;
        }

        internal List<IGraphicBar> BarTotalGasesFacilityType_by_Scope(Int64 idScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
            Int64 idIndicatorPM10)
        {
            List<IGraphicBar> _items = new List<IGraphicBar>();
            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_Bar_TotalGasesFacilityType_by_Scope(_ProcessGroupProcess.IdProcess,
                idScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, 
                idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, 
                idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,idIndicatorPM, idIndicatorPM10, 
                _StartDate, _EndDate, _Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdFacilityType", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Graphic_Bar_FT _bFT = new Condesus.EMS.Business.RG.Entities.Graphic_Bar_FT(
                                                Convert.ToInt64(_dbRecord["IdFacilityType"]),
                                                Convert.ToString(_dbRecord["FacilityTypeName"]),
                                                Convert.ToDecimal(_dbRecord["Result_tCO2e"]),
                                                Convert.ToDecimal(_dbRecord["Result_CO2"]),
                                                Convert.ToDecimal(_dbRecord["result_CH4"]),
                                                Convert.ToDecimal(_dbRecord["result_N2O"]),
                                                Convert.ToDecimal(_dbRecord["result_PFC"]),
                                                Convert.ToDecimal(_dbRecord["result_HFC"]),
                                                Convert.ToDecimal(_dbRecord["Result_SF6"]),
                                                Convert.ToDecimal(_dbRecord["result_HCT"]),
                                                Convert.ToDecimal(_dbRecord["result_HCNM"]),
                                                Convert.ToDecimal(_dbRecord["result_C2H6"]),
                                                Convert.ToDecimal(_dbRecord["result_C3H8"]),
                                                Convert.ToDecimal(_dbRecord["result_C4H10"]),
                                                Convert.ToDecimal(_dbRecord["result_CO"]),
                                                Convert.ToDecimal(_dbRecord["result_NOx"]),
                                                Convert.ToDecimal(_dbRecord["result_SOx"]),
                                                Convert.ToDecimal(_dbRecord["result_SO2"]),
                                                Convert.ToDecimal(_dbRecord["result_H2S"]),
                                                Convert.ToDecimal(_dbRecord["result_PM"]),
                                                Convert.ToDecimal(_dbRecord["result_PM10"]));
                                                
                _items.Add(_bFT);
            }

            return _items;
        }

        internal List<IGraphicBar> BarTotalGasessActivity_by_Scope(Int64 idScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
         Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
         Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
         Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
         Int64 idIndicatorPM10)
        {
            List<IGraphicBar> _items = new List<IGraphicBar>();
            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_Bar_TotalGasesActivity_by_Scope(_ProcessGroupProcess.IdProcess,
                idScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC,
                idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO,
                idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, idIndicatorPM, idIndicatorPM10,
                _StartDate, _EndDate, _Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdActivity", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Graphic_Bar_A _bA = new Condesus.EMS.Business.RG.Entities.Graphic_Bar_A(
                                                Convert.ToInt64(_dbRecord["IdActivity"]),
                                                Convert.ToString(_dbRecord["Name"]),
                                                Convert.ToDecimal(_dbRecord["Result_tCO2e"]),
                                                Convert.ToDecimal(_dbRecord["Result_CO2"]),
                                                Convert.ToDecimal(_dbRecord["result_CH4"]),
                                                Convert.ToDecimal(_dbRecord["result_N2O"]),
                                                Convert.ToDecimal(_dbRecord["result_PFC"]),
                                                Convert.ToDecimal(_dbRecord["result_HFC"]),
                                                Convert.ToDecimal(_dbRecord["Result_SF6"]),
                                                Convert.ToDecimal(_dbRecord["result_HCT"]),
                                                Convert.ToDecimal(_dbRecord["result_HCNM"]),
                                                Convert.ToDecimal(_dbRecord["result_C2H6"]),
                                                Convert.ToDecimal(_dbRecord["result_C3H8"]),
                                                Convert.ToDecimal(_dbRecord["result_C4H10"]),
                                                Convert.ToDecimal(_dbRecord["result_CO"]),
                                                Convert.ToDecimal(_dbRecord["result_NOx"]),
                                                Convert.ToDecimal(_dbRecord["result_SOx"]),
                                                Convert.ToDecimal(_dbRecord["result_SO2"]),
                                                Convert.ToDecimal(_dbRecord["result_H2S"]),
                                                Convert.ToDecimal(_dbRecord["result_PM"]),
                                                Convert.ToDecimal(_dbRecord["result_PM10"]));

                _items.Add(_bA);
            }

            return _items;
        }


            internal List<IGraphicBar> BarTotalGasessActivity_by_ScopeAndFacility(Int64 idScope, Int64 idFacility, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
         Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
         Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
         Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
         Int64 idIndicatorPM10)
        {
            List<IGraphicBar> _items = new List<IGraphicBar>();
            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_Bar_TotalGasesActivity_by_ScopeAndFacility(_ProcessGroupProcess.IdProcess,
                idScope, idFacility, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC,
                idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO,
                idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, idIndicatorPM, idIndicatorPM10,
                _StartDate, _EndDate, _Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdActivity", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Graphic_Bar_A _bA = new Condesus.EMS.Business.RG.Entities.Graphic_Bar_A(
                                                Convert.ToInt64(_dbRecord["IdActivity"]),
                                                Convert.ToString(_dbRecord["Name"]),
                                                Convert.ToDecimal(_dbRecord["Result_tCO2e"]),
                                                Convert.ToDecimal(_dbRecord["Result_CO2"]),
                                                Convert.ToDecimal(_dbRecord["result_CH4"]),
                                                Convert.ToDecimal(_dbRecord["result_N2O"]),
                                                Convert.ToDecimal(_dbRecord["result_PFC"]),
                                                Convert.ToDecimal(_dbRecord["result_HFC"]),
                                                Convert.ToDecimal(_dbRecord["Result_SF6"]),
                                                Convert.ToDecimal(_dbRecord["result_HCT"]),
                                                Convert.ToDecimal(_dbRecord["result_HCNM"]),
                                                Convert.ToDecimal(_dbRecord["result_C2H6"]),
                                                Convert.ToDecimal(_dbRecord["result_C3H8"]),
                                                Convert.ToDecimal(_dbRecord["result_C4H10"]),
                                                Convert.ToDecimal(_dbRecord["result_CO"]),
                                                Convert.ToDecimal(_dbRecord["result_NOx"]),
                                                Convert.ToDecimal(_dbRecord["result_SOx"]),
                                                Convert.ToDecimal(_dbRecord["result_SO2"]),
                                                Convert.ToDecimal(_dbRecord["result_H2S"]),
                                                Convert.ToDecimal(_dbRecord["result_PM"]),
                                                Convert.ToDecimal(_dbRecord["result_PM10"]));

                _items.Add(_bA);
            }

            return _items;
        }

            internal List<IGraphicBar> BarTotalGasesState_by_Scope(List<Int64> geoAreas, Int64 idScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
        Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
        Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
        Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
        Int64 idIndicatorPM10)
            {
                List<IGraphicBar> _items = new List<IGraphicBar>();

                foreach (Int64 _idGeoArea in geoAreas)
                {

                    GIS.Entities.GeographicArea _geoagraphicArea = new GIS.Collections.GeographicAreas(_Credential).Item(_idGeoArea);

                    //Objeto de data layer para acceder a datos
                    DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

                    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_Bar_TotalGasesState_by_Scope(_ProcessGroupProcess.IdProcess,
                        idScope, _idGeoArea, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC,
                        idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO,
                        idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, idIndicatorPM, idIndicatorPM10,
                        _StartDate, _EndDate);

                    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                    {
                        Entities.Graphic_Bar_A _bA = new Condesus.EMS.Business.RG.Entities.Graphic_Bar_A(
                                                        _geoagraphicArea.IdGeographicArea,
                                                        _geoagraphicArea.LanguageOption.Name,
                                                        Convert.ToDecimal(_dbRecord["Result_tCO2e"]),
                                                        Convert.ToDecimal(_dbRecord["Result_CO2"]),
                                                        Convert.ToDecimal(_dbRecord["result_CH4"]),
                                                        Convert.ToDecimal(_dbRecord["result_N2O"]),
                                                        Convert.ToDecimal(_dbRecord["result_PFC"]),
                                                        Convert.ToDecimal(_dbRecord["result_HFC"]),
                                                        Convert.ToDecimal(_dbRecord["Result_SF6"]),
                                                        Convert.ToDecimal(_dbRecord["result_HCT"]),
                                                        Convert.ToDecimal(_dbRecord["result_HCNM"]),
                                                        Convert.ToDecimal(_dbRecord["result_C2H6"]),
                                                        Convert.ToDecimal(_dbRecord["result_C3H8"]),
                                                        Convert.ToDecimal(_dbRecord["result_C4H10"]),
                                                        Convert.ToDecimal(_dbRecord["result_CO"]),
                                                        Convert.ToDecimal(_dbRecord["result_NOx"]),
                                                        Convert.ToDecimal(_dbRecord["result_SOx"]),
                                                        Convert.ToDecimal(_dbRecord["result_SO2"]),
                                                        Convert.ToDecimal(_dbRecord["result_H2S"]),
                                                        Convert.ToDecimal(_dbRecord["result_PM"]),
                                                        Convert.ToDecimal(_dbRecord["result_PM10"]));

                        _items.Add(_bA);
                    }
                }
                    
                return _items;
                
            }
    }


    }

