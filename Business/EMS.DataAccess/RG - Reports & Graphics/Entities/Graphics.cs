using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.RG.Entities
{
    internal class Graphics
    {
        internal Graphics() { }

        #region Read Functions

        internal IEnumerable<DbDataRecord> Read_Pie_TotalsScope_by_Indicator(Int64 IdProcess, Int64 result_Indicator, DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Chart_Pie_TotalsScope_by_Indicator");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "result_Indicator", DbType.Int64, result_Indicator);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }
        internal IEnumerable<DbDataRecord> Read_Pie_TotalsScope_by_Facility(Int64 IdProcess, Int64 result_Indicator, Int64 IdFacility, DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Chart_Pie_TotalsScope_by_Facility");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "result_Indicator", DbType.Int64, result_Indicator);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, IdFacility);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }


        internal IEnumerable<DbDataRecord> Read_Bar_TotalGasesFacilityType_by_Scope(Int64 IdProcess, Int64 IdScope,
            Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, 
            Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, 
            Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, 
            Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, 
            DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Chart_Bar_TotalGasesFacilityType_by_Scope");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, IdScope);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdIndicator_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "IdIndicator_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "IdIndicator_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "IdIndicator_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "IdIndicator_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "IdIndicator_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "IdIndicator_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);
            

            SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }

        internal IEnumerable<DbDataRecord> Read_Bar_TotalGasesActivity_by_Scope(Int64 IdProcess, Int64 IdScope,
           Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC,
           Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
           Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx,
           Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10,
           DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Chart_Bar_TotalGasesActivity_by_Scope");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, IdScope);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdIndicator_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "IdIndicator_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "IdIndicator_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "IdIndicator_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "IdIndicator_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "IdIndicator_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "IdIndicator_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);


            SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }

        internal IEnumerable<DbDataRecord> Read_Bar_TotalGasesActivity_by_ScopeAndFacility(Int64 IdProcess, Int64 IdScope, Int64 IdFacility,
          Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC,
          Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
          Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx,
          Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10,
          DateTime StartDate, DateTime EndDate, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Chart_Bar_TotalGasesActivity_by_ScopeAndFacility");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, IdScope);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, IdFacility);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdIndicator_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "IdIndicator_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "IdIndicator_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "IdIndicator_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "IdIndicator_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "IdIndicator_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "IdIndicator_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);


            SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }

        internal IEnumerable<DbDataRecord> Read_Bar_TotalGasesState_by_Scope(Int64 IdProcess, Int64 IdScope, Int64 IdState,
    Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC,
    Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
    Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx,
    Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10,
    DateTime StartDate, DateTime EndDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Chart_Bar_TotalGasesState_by_Scope");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, IdScope);
            _db.AddInParameter(_dbCommand, "IdState", DbType.Int64, IdState);
            _db.AddInParameter(_dbCommand, "IdIndicator_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "IdIndicator_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "IdIndicator_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "IdIndicator_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "IdIndicator_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "IdIndicator_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "IdIndicator_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "IdIndicator_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "IdIndicator_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "IdIndicator_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "IdIndicator_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "IdIndicator_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "IdIndicator_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);


            SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

            try
            {
                foreach (DbDataRecord _record in _reader)
                {
                    yield return _record;
                }
            }
            finally
            {
                _reader.Close();
            }
        }
        #endregion

        
    }
}
