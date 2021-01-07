using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG.Collections
{
    public class Report_A_F_by_I_COL
    {
        #region Internal Properties
        private Credential _Credential;
        
        private PF.Entities.ProcessGroupProcess _ProcessGroupProcess;

        private Int64 _idIndicatortCO2e;
        private Int64 _idIndicatorCO2;
        private Int64 _idIndicatorCH4;
        private Int64 _idIndicatortnN2O;
        private Int64 _idIndicatorPFC;
        private Int64 _idIndicatorHFC;
        private Int64 _idIndicatorSF6;
        private Int64 _idIndicatorHCT;
        private Int64 _idIndicatorHCNM;
        private Int64 _idIndicatorC2H6;
        private Int64 _idIndicatorC3H8;
        private Int64 _idIndicatorC4H10;
        private Int64 _idIndicatorCO;
        private Int64 _idIndicatorNOx;
        private Int64 _idIndicatorSOx;
        private Int64 _idIndicatorSO2;
        private Int64 _idIndicatorH2S;
        private Int64 _idIndicatorPM;
        private Int64 _idIndicatorPM10;
        private DateTime _StartDate;
        private DateTime _EndDate;
        #endregion

        internal Report_A_F_by_I_COL(PF.Entities.ProcessGroupProcess processGroupProcess, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O,
                  Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM,
                  Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx,
                  Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, 
                  DateTime startDate, DateTime endDate)
        {
            _idIndicatortCO2e = idIndicatortCO2e;
            _idIndicatorCO2 = idIndicatorCO2;
            _idIndicatorCH4 = idIndicatorCH4;
            _idIndicatortnN2O = idIndicatortnN2O;
            _idIndicatorPFC = idIndicatorPFC;
            _idIndicatorHFC = idIndicatorHFC;
            _idIndicatorSF6 = idIndicatorSF6;
            _idIndicatorHCT = idIndicatorHCT;
            _idIndicatorHCNM = idIndicatorHCNM;
            _idIndicatorC2H6 = idIndicatorC2H6;
            _idIndicatorC3H8 = idIndicatorC3H8;
            _idIndicatorC4H10 = idIndicatorC4H10;
            _idIndicatorCO = idIndicatorCO;
            _idIndicatorNOx=idIndicatorNOx;
            _idIndicatorSOx=idIndicatorSOx;
            _idIndicatorSO2=idIndicatorSO2;
            _idIndicatorH2S=idIndicatorH2S;
            _idIndicatorPM=idIndicatorPM;
            _idIndicatorPM10=idIndicatorPM10;
            _StartDate = startDate;
            _EndDate = endDate;
            _Credential = processGroupProcess.Credential;
            _ProcessGroupProcess = processGroupProcess;
        }
   
        #region Results
        private DataTable _DTResults;

        internal DataTable DTResults
        {
            get
            {
                if (_DTResults == null)
                { _DTResults = BuildDTResults(); }
                return _DTResults;
            }
        }

        internal DataTable BuildDTResults()
        {
            _DTResults = new DataTable();

            DataColumn _colum = new DataColumn();          
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdActivity";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdFacility";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "ActivityName";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "FacilityName";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_tCO2e";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_CO2";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_CH4";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_N2O";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_PFC";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_HFC";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_SF6";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_HCT";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_HCNM";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_C2H6";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_C3H8";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_C4H10";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_CO";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_NOx";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_SOx";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_SO2";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_H2S";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_PM";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Double");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Result_PM10";
            _DTResults.Columns.Add(_colum);

            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.ReadEmission_by_Facilities(
                _ProcessGroupProcess.IdProcess, _ProcessGroupProcess.Credential.CurrentLanguage.IdLanguage,
                _idIndicatortCO2e, _idIndicatorCO2, _idIndicatorCH4, _idIndicatortnN2O, _idIndicatorPFC, _idIndicatorHFC, _idIndicatorSF6, 
                _idIndicatorHCT, _idIndicatorHCNM, _idIndicatorC2H6, _idIndicatorC3H8, _idIndicatorC4H10, _idIndicatorCO, _idIndicatorNOx, 
                _idIndicatorSOx, _idIndicatorSO2, _idIndicatorH2S, _idIndicatorPM, _idIndicatorPM10, _StartDate, _EndDate);
        
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _DTResults.Rows.Add(Convert.ToInt64(_dbRecord["IdActivity"]),
                                    Convert.ToInt64(_dbRecord["IdFacility"]),
                                    Convert.ToString(_dbRecord["ActivityName"]),
                                    Convert.ToString(_dbRecord["FacilityName"]),
                                    Convert.ToDouble(_dbRecord["Result_tCO2e"]),
                                    Convert.ToDouble(_dbRecord["Result_CO2"]),
                                    Convert.ToDouble(_dbRecord["result_CH4"]),
                                    Convert.ToDouble(_dbRecord["result_N2O"]),
                                    Convert.ToDouble(_dbRecord["result_PFC"]),
                                    Convert.ToDouble(_dbRecord["result_HFC"]),
                                    Convert.ToDouble(_dbRecord["Result_SF6"]),
                                    Convert.ToDouble(_dbRecord["result_HCT"]),
                                    Convert.ToDouble(_dbRecord["result_HCNM"]),
                                    Convert.ToDouble(_dbRecord["result_C2H6"]),
                                    Convert.ToDouble(_dbRecord["result_C3H8"]),
                                    Convert.ToDouble(_dbRecord["result_C4H10"]),
                                    Convert.ToDouble(_dbRecord["result_CO"]),
                                    Convert.ToDouble(_dbRecord["result_NOx"]),
                                    Convert.ToDouble(_dbRecord["result_SOx"]),
                                    Convert.ToDouble(_dbRecord["result_SO2"]),
                                    Convert.ToDouble(_dbRecord["result_H2S"]),
                                    Convert.ToDouble(_dbRecord["result_PM"]),
                                    Convert.ToDouble(_dbRecord["result_PM10"]));
            }

            return _DTResults;
        }
        #endregion
    }
}
