using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.RG.Entities
{
    internal class Report
    {
        internal Report() { }

        #region Read Functions

        internal IEnumerable<DbDataRecord> Read_All_GA(Int64 idProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_All_GA");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess); 
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

        internal IEnumerable<DbDataRecord> Read_All_O(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_O_S_A_FT_F_All_O");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

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

        internal IEnumerable<DbDataRecord> ReadTotals_GA_by_Process(Int64 idGeographicArea, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, 
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, 
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, 
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, 
            Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Totals_GA_by_Process");
            _db.AddInParameter(_dbCommand, "IdGeoArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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

        internal IEnumerable<DbDataRecord> Exist_GA()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Exist_GA");
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


        internal IEnumerable<DbDataRecord> ReadTotals_S_by_Process(Int64 idGeographicArea, Int64 idScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
            Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Totals_S_by_Process");
            _db.AddInParameter(_dbCommand, "IdGeoArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, idScope);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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

        internal IEnumerable<DbDataRecord> Exist_S()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Exist_S");
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

        internal IEnumerable<DbDataRecord> Read_All_A(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_All_A");
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
        internal IEnumerable<DbDataRecord> Read_All_I(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_All_I");
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
        internal IEnumerable<DbDataRecord> ReadTotals_A_by_Process(Int64 idGeographicArea, Int64 idScope, Int64 idActivity, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
            Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Totals_A_by_Process");
            _db.AddInParameter(_dbCommand, "IdGeoArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, idScope);
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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
        internal IEnumerable<DbDataRecord> Exist_A()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Exist_A");
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
        internal IEnumerable<DbDataRecord> ReadTotals_FT_by_Process(Int64 idGeographicArea, Int64 idScope, Int64 idActivity, Int64 idFacilityType,
            Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
           Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
           Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
           Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
           Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Totals_FT_by_Process");
            _db.AddInParameter(_dbCommand, "IdGeoArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, idScope);
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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
        internal IEnumerable<DbDataRecord> Exist_FT()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Exist_FT");
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
        internal IEnumerable<DbDataRecord> ReadTotals_F_by_Process(Int64 idGeographicArea, Int64 idScope, Int64 idActivity, Int64 idFacilityType, Int64 idFacility,
           Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
          Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
          Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
          Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
          Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Totals_F_by_Process");
            _db.AddInParameter(_dbCommand, "IdGeoArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, idScope);
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacilityType);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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
        internal IEnumerable<DbDataRecord> Exist_F()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Exist_F");
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
        internal IEnumerable<DbDataRecord> Read_All_F(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_All_F");
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
        #endregion

        #region Results By Process
        //internal IEnumerable<DbDataRecord> ReadTotals_by_Process(Int64 idGeographicArea, Int64 idScope, Int64 idActivity, Int64 idFacilityType, 
        //    Int64 idFacility, Int64 IdProcess, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O, 
        //    Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, 
        //    Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, 
        //    Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_GA_S_A_FT_F_Totals_by_Process");
        //    _db.AddInParameter(_dbCommand, "IdGeoArea", DbType.Int64, idGeographicArea);
        //    _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, idScope);
        //    _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
        //    _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);
        //    _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacilityType);
        //    _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
        //    _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
        //    _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
        //    _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
        //    _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
        //    _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
        //    _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
        //    _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
        //    _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
        //    _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
        //    _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
        //    _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
        //    _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
        //    _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
        //    _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
        //    _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
        //    _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
        //    _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
        //    _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
        //    _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
        //    _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
        //    _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

        //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

        //    try
        //    {
        //        foreach (DbDataRecord _record in _reader)
        //        {
        //            yield return _record;
        //        }
        //    }
        //    finally
        //    {
        //        _reader.Close();
        //    }
        //}

        internal IEnumerable<DbDataRecord> ReadTotals_by_Process(Int64 IdProcess, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O,
           Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
           Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2,
           Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_Totals_by_Process");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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

        internal IEnumerable<DbDataRecord> ReadTotals_Organization_by_Process(Int64 IdProcess, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O,
          Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
          Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2,
          Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_Totals_Organization_by_Process");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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



        internal IEnumerable<DbDataRecord> ReadEmission_by_Facilities(Int64 IdProcess, String IdLanguage, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O,
      Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
      Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2,
      Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_TotalsEmission_by_Facilities");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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


        #region Evolutionary
        internal IEnumerable<DbDataRecord> ReadEvolutionary_Organization_by_Process(Int64 IdProcess, Int64 IdScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O,
        Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6,
        Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2,
        Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_Evolutionary_Organization_by_Process");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, IdScope);
            _db.AddInParameter(_dbCommand, "result_tCO2e", DbType.Int64, idIndicatortCO2e);
            _db.AddInParameter(_dbCommand, "result_CO2", DbType.Int64, idIndicatorCO2);
            _db.AddInParameter(_dbCommand, "result_CH4", DbType.Int64, idIndicatorCH4);
            _db.AddInParameter(_dbCommand, "result_N2O", DbType.Int64, idIndicatortnN2O);
            _db.AddInParameter(_dbCommand, "result_PFC", DbType.Int64, idIndicatorPFC);
            _db.AddInParameter(_dbCommand, "result_HFC", DbType.Int64, idIndicatorHFC);
            _db.AddInParameter(_dbCommand, "result_SF6", DbType.Int64, idIndicatorSF6);
            _db.AddInParameter(_dbCommand, "result_HCT", DbType.Int64, idIndicatorHCT);
            _db.AddInParameter(_dbCommand, "result_HCNM", DbType.Int64, idIndicatorHCNM);
            _db.AddInParameter(_dbCommand, "result_C2H6", DbType.Int64, idIndicatorC2H6);
            _db.AddInParameter(_dbCommand, "result_C3H8", DbType.Int64, idIndicatorC3H8);
            _db.AddInParameter(_dbCommand, "result_C4H10", DbType.Int64, idIndicatorC4H10);
            _db.AddInParameter(_dbCommand, "result_CO", DbType.Int64, idIndicatorCO);
            _db.AddInParameter(_dbCommand, "result_NOx", DbType.Int64, idIndicatorNOx);
            _db.AddInParameter(_dbCommand, "result_SOx", DbType.Int64, idIndicatorSOx);
            _db.AddInParameter(_dbCommand, "result_SO2", DbType.Int64, idIndicatorSO2);
            _db.AddInParameter(_dbCommand, "result_H2S", DbType.Int64, idIndicatorH2S);
            _db.AddInParameter(_dbCommand, "result_PM", DbType.Int64, idIndicatorPM);
            _db.AddInParameter(_dbCommand, "result_PM10", DbType.Int64, idIndicatorPM10);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);

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


        internal IEnumerable<DbDataRecord> FacilitiesByIndicators(Int64 IdProcess, String IdLanguage, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("RG_Report_FacilitiesByIndicators");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

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

        #region MultiObservatory

        internal IEnumerable<DbDataRecord> MultiObservatory(DataTable dtTVPProcessFilter, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("RG_Report_MultiObservatory");
            //Arma el parametro de tipo tabla
            SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@dtIdProcess", dtTVPProcessFilter);
            //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
            _tvpParam.SqlDbType = SqlDbType.Structured;
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

            _dbCommand.CommandTimeout = 0;

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
