using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.DataAccess.PF.Entities;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Condesus.EMS.DataAccess.RG
{
    public class ReportGraphic
    {
        #region Public Properties
        
        #region Report_GA_S_A_FT_F

        public IEnumerable<DbDataRecord> Read_All_GA(Int64 idProcess, String idLanguage)
        {
            return new Entities.Report().Read_All_GA(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> Read_All_O(Int64 idProcess)
        {
            return new Entities.Report().Read_All_O(idProcess);
        }
        public IEnumerable<DbDataRecord> Exist_GA()
        {
            return new Entities.Report().Exist_GA();
        }       
        public IEnumerable<DbDataRecord> Exist_S()
        {
            return new Entities.Report().Exist_S();
        }

        public IEnumerable<DbDataRecord> Read_All_A(String idLanguage)
        {
            return new Entities.Report().Read_All_A(idLanguage);
        }       
        public IEnumerable<DbDataRecord> Exist_A()
        {
            return new Entities.Report().Exist_A();
        }       
        public IEnumerable<DbDataRecord> Exist_FT()
        {
            return new Entities.Report().Exist_FT();
        }       
        public IEnumerable<DbDataRecord> Exist_F()
        {
            return new Entities.Report().Exist_F();
        }
        public IEnumerable<DbDataRecord> Read_All_F(String idLanguage)
        {
            return new Entities.Report().Read_All_F(idLanguage);
        }
        public IEnumerable<DbDataRecord> Read_All_I(String idLanguage)
        {
            return new Entities.Report().Read_All_I(idLanguage);
        }

        public IEnumerable<DbDataRecord> ReadTotals_by_Process(Int64 IdProcess, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, 
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, 
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, 
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, 
            Int64 idIndicatorPM,Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            return new Entities.Report().ReadTotals_by_Process(IdProcess, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, 
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, 
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, 
                idIndicatorPM, idIndicatorPM10, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> ReadTotals_Organization_by_Process(Int64 IdProcess, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
         Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
         Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
         Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S,
         Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            return new Entities.Report().ReadTotals_Organization_by_Process(IdProcess, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> ReadEmission_by_Facilities(Int64 IdProcess, String IdLanguage, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
          Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
          Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
          Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S,
          Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            return new Entities.Report().ReadEmission_by_Facilities(IdProcess, IdLanguage, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, startDate, endDate);
        }
        #endregion

        #region Evolutionary
        public IEnumerable<DbDataRecord> ReadEvolutionary_Organization_by_Process(Int64 IdProcess, Int64 IdScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
     Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
     Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
     Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S,
     Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            return new Entities.Report().ReadEvolutionary_Organization_by_Process(IdProcess, IdScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, startDate, endDate);
        }
        #endregion

        #region Graphics

        public IEnumerable<DbDataRecord> Read_Pie_TotalsScope_by_Indicator(Int64 IdProcess, Int64 result_Indicator, DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            return new Entities.Graphics().Read_Pie_TotalsScope_by_Indicator(IdProcess, result_Indicator, StartDate, EndDate, idLanguage);
        }
        public IEnumerable<DbDataRecord> Read_Pie_TotalsScope_by_Facility(Int64 IdProcess, Int64 result_Indicator, Int64 idFacility, DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            return new Entities.Graphics().Read_Pie_TotalsScope_by_Facility(IdProcess, result_Indicator, idFacility, StartDate, EndDate, idLanguage);
        }


        public IEnumerable<DbDataRecord> Read_Bar_TotalGasesFacilityType_by_Scope(Int64 IdProcess, Int64 IdScope,
        Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC,
        Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
        Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx,
        Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10,
        DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            return new Entities.Graphics().Read_Bar_TotalGasesFacilityType_by_Scope(IdProcess, IdScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, StartDate, EndDate, idLanguage);
        }

        public IEnumerable<DbDataRecord> Read_Bar_TotalGasesActivity_by_Scope(Int64 IdProcess, Int64 IdScope,
        Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC,
        Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
        Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx,
        Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10,
        DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            return new Entities.Graphics().Read_Bar_TotalGasesActivity_by_Scope(IdProcess, IdScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, StartDate, EndDate, idLanguage);
        }
        public IEnumerable<DbDataRecord> Read_Bar_TotalGasesActivity_by_ScopeAndFacility(Int64 IdProcess, Int64 IdScope, Int64 IdFacility,
        Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC,
        Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
        Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx,
        Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10,
        DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            return new Entities.Graphics().Read_Bar_TotalGasesActivity_by_ScopeAndFacility(IdProcess, IdScope, IdFacility, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, StartDate, EndDate, idLanguage);
        }
        public IEnumerable<DbDataRecord> Read_Bar_TotalGasesState_by_Scope(Int64 IdProcess, Int64 IdScope, Int64 IdState,
        Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC,
        Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
        Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx,
        Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10,
        DateTime StartDate, DateTime EndDate)
        {
            return new Entities.Graphics().Read_Bar_TotalGasesState_by_Scope(IdProcess, IdScope, IdState, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, StartDate, EndDate);
        }
        #endregion

        public IEnumerable<DbDataRecord> FacilitiesByIndicators(Int64 IdProcess, String IdLanguage, DateTime startDate, DateTime endDate)
        {
            return new Entities.Report().FacilitiesByIndicators(IdProcess, IdLanguage, startDate, endDate);
        }

        public IEnumerable<DbDataRecord> MultiObservatory(DataTable dtTVPProcessFilter, DateTime startDate, DateTime endDate)
        {
            return new Entities.Report().MultiObservatory(dtTVPProcessFilter, startDate, endDate);
        }

        #endregion
    }
}
