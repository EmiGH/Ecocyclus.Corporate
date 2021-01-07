using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;


namespace Condesus.EMS.Business.RG.Collections
{
    public class Report_S_GA_A_FT_F_COL
    {
        #region Internal Properties
        private Credential _Credential;
        
        private PF.Entities.ProcessGroupProcess _ProcessGroupProcess;

        //private Dictionary<Int64, Exist_GA> _Exist_GAs;
        //private Dictionary<Int64, Exist_S> _Exist_Ss;
        //private Dictionary<Int64, Exist_A> _Exist_As;
        //private Dictionary<Int64, Exist_FT> _Exist_FTs;
        //private Dictionary<Int64, Exist_F> _Exist_Fs;

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

        internal Report_S_GA_A_FT_F_COL(Entities.Report_S_GA_A_FT_F report_S_GA_A_FT_F, Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O,
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
            _Credential = report_S_GA_A_FT_F.Credential;
            _ProcessGroupProcess = report_S_GA_A_FT_F.ProcessGroupProcess;
        }

        #region GA
        private DataTable _AllGA;

        internal DataTable AllGA
        {
            get
            {
                if (_AllGA == null)
                { _AllGA = BuildAllGA(); }
                return _AllGA;
            }
        }

        internal DataTable BuildAllGA()
        {
            _AllGA = new DataTable();

            DataColumn _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Id";
            _AllGA.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdParent";
            _AllGA.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Name";
            _AllGA.Columns.Add(_colum);

             //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_All_GA(_ProcessGroupProcess.IdProcess, _Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdGeographicArea", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                _AllGA.Rows.Add(Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentGeographicArea"], 0)), Convert.ToString(_dbRecord["Name"]));
            }

            return _AllGA;
        }

        internal List<IColumnsReport> GAs(Entities.Report_S_GA_A_FT_F_Obj_S s)
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();

            DataTable _allGA = AllGA;

            DataRow[] _dataRow = _allGA.Select("IdParent = 0"); //Quiero los ROOTs

            for (int i = 0; i < _dataRow.Length; i++)
            {

                Entities.Report_S_GA_A_FT_F_Obj_GA _ga = new Entities.Report_S_GA_A_FT_F_Obj_GA
                  (Convert.ToInt64(_dataRow[i]["Id"]), _dataRow[i]["Name"].ToString(), s);

                if (_ga.Items().Count != 0 || _ga.Child().Count != 0)
                {
                    _oItems.Add(_ga);
                }
            }        

            return _oItems;
        }

        internal List<IColumnsReport> GAs(Entities.Report_S_GA_A_FT_F_Obj_GA ga)
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();
        
                DataTable _allGA = AllGA;
               
                DataRow[] _dataRow = _allGA.Select("IdParent = " + ga.Id); //Quiero los hijos
                for (int i = 0; i < _dataRow.Length; i++)
                {

                    Entities.Report_S_GA_A_FT_F_Obj_GA _ga = new Entities.Report_S_GA_A_FT_F_Obj_GA
                      (Convert.ToInt64(_dataRow[i]["Id"]), _dataRow[i]["Name"].ToString(), ga.S);

                    if (_ga.Items().Count != 0 || _ga.Child().Count != 0)
                    {
                        _oItems.Add(_ga);
                    }
                }            

            return _oItems;
        }    
        #endregion

        #region S
        private Dictionary<Int64, PA.Entities.AccountingScope> _Scopes;
        private Dictionary<Int64, PA.Entities.AccountingScope> Scopes
        {
            get
            {
                if (_Scopes == null) { _Scopes = new PA.Collections.AccountingScopes(_Credential).Items(); }
                return _Scopes;
            }
        }      
        internal List<IColumnsReport> Ss()
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();

            foreach (PA.Entities.AccountingScope _scope in Scopes.Values)
            {
                //Declara e instancia una posicion
                Entities.Report_S_GA_A_FT_F_Obj_S _s = new Entities.Report_S_GA_A_FT_F_Obj_S(_scope.IdScope, _scope.LanguageOption.Name, this);

                if (_s.Items().Count != 0 || _s.Child().Count != 0)
                {
                    _oItems.Add(_s);
                }

            }
        
            return _oItems;
        }  
        #endregion

        #region A
        private DataTable _AllA;

        internal DataTable AllA
        {
            get
            {
                if (_AllA == null)
                { _AllA = BuildAllA(); }
                return _AllA;
            }
        }

        internal DataTable BuildAllA()
        {
            _AllA = new DataTable();

            DataColumn _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Id";
            _AllA.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdParent";
            _AllA.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Name";
            _AllA.Columns.Add(_colum);

            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_All_A(_Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdActivity", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                _AllA.Rows.Add(Convert.ToInt64(_dbRecord["IdActivity"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentActivity"], 0)), Convert.ToString(_dbRecord["Name"]));
            }

            return _AllA;
        }

        internal List<IColumnsReport> As(Entities.Report_S_GA_A_FT_F_Obj_GA ga)
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();

       
                // traee todos los facilityes
                DataTable _allA = AllA;
                //filtra el datatable
                DataRow[] _dataRow = _allA.Select("IdParent = 0");
                // por cada facility
                for (int i = 0; i < _dataRow.Length; i++)
                {
                    //Declara e instancia una posicion
                    Entities.Report_S_GA_A_FT_F_Obj_A _a = new Entities.Report_S_GA_A_FT_F_Obj_A
                        (Convert.ToInt64(_dataRow[i]["Id"]), _dataRow[i]["Name"].ToString(),ga);

                    if (_a.Items().Count != 0 || _a.Child().Count != 0)
                    {
                        _oItems.Add(_a);
                    }
                    
                }

            return _oItems;
        }

        internal List<IColumnsReport> As(Entities.Report_S_GA_A_FT_F_Obj_A a)
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();
         
                // traee todos los facilityes
                DataTable _allA = AllA;
                //filtra el datatable
                DataRow[] _dataRow = _allA.Select("IdParent = " + a.Id.ToString());
                // por cada facility
                for (int i = 0; i < _dataRow.Length; i++)
                {
                    //Declara e instancia una posicion
                    Entities.Report_S_GA_A_FT_F_Obj_A _a = new Entities.Report_S_GA_A_FT_F_Obj_A
                        (Convert.ToInt64(_dataRow[i]["Id"]), _dataRow[i]["Name"].ToString(), a.GA);

                    if (_a.Items().Count != 0 || _a.Child().Count != 0)
                    {
                        _oItems.Add(_a);
                    }

                }            

            return _oItems;
        }

 
        #endregion

        #region FT
        private Dictionary<Int64, GIS.Entities.FacilityType> _FacilityTypes;
        private Dictionary<Int64, GIS.Entities.FacilityType> FacilityTypes
        {
            get
            {
                if (_FacilityTypes == null) { _FacilityTypes = new GIS.Collections.FacilityTypes(_Credential).Items(); }
                return _FacilityTypes;
            }
        }

        internal List<IColumnsReport> FTs(Entities.Report_S_GA_A_FT_F_Obj_A a)
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();
      
    
                foreach (GIS.Entities.FacilityType _facilitytype in FacilityTypes.Values)
                {                    
                    //Declara e instancia una posicion
                    Entities.Report_S_GA_A_FT_F_Obj_FT _ft = new Entities.Report_S_GA_A_FT_F_Obj_FT
                    (_facilitytype.IdFacilityType, _facilitytype.LanguageOption.Name,a);

                    if (_ft.Items().Count != 0 || _ft.Child().Count != 0)
                    {
                        _oItems.Add(_ft);
                    }
                 
                }

            return _oItems;
        }
   
        #endregion

        #region F
        private DataTable _AllF;

        internal DataTable AllF
        {
            get
            {
                if (_AllF == null)
                { _AllF = BuildAllF(); }
                return _AllF;
            }
        }

        internal DataTable BuildAllF()
        {
            _AllF = new DataTable();

            DataColumn _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Id";
            _AllF.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdParent";
            _AllF.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdFacilityType";
            _AllF.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Name";
            _AllF.Columns.Add(_colum);

            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_All_F(_Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdFacility", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                _AllF.Rows.Add(Convert.ToInt64(_dbRecord["IdFacility"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacilityType"],0)), Convert.ToString(_dbRecord["Name"]));
            }

            return _AllF;
        }

   
        internal List<IColumnsReport> Fs(Entities.Report_S_GA_A_FT_F_Obj_FT ft)
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();
            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();
     
                // traee todos los facilityes
                DataTable _allF = AllF;
                //filtra el datatable
                DataRow[] _dataRow = DTResults.Select("IdFacilityType = " + ft.Id.ToString() + " and IdActivity= " + ft.A.Id.ToString() + " and IdGeographicArea= " + ft.A.GA.Id.ToString() + " and IdScope= " + ft.A.GA.S.Id.ToString());

                var _listFacilities = (from r in _dataRow.AsEnumerable()
                         select r["IdFacility"]).Distinct().ToList();

                foreach (Int64 _idFacility in _listFacilities)
                {
                    DataRow[] _dataRowName = _allF.Select("Id = " + _idFacility); 
                
                    Entities.Report_S_GA_A_FT_F_Obj_F _f = new Entities.Report_S_GA_A_FT_F_Obj_F
                                                        (_idFacility,
                                                        _dataRowName[0]["Name"].ToString(), ft);

                    if (_f.Items().Count != 0)
                    {
                        _oItems.Add(_f);
                    }
                }
           

            return _oItems;
        }

 

   
        #endregion

        #region I
        private DataTable _AllI;

        internal DataTable AllI
        {
            get
            {
                if (_AllI == null)
                { _AllI = BuildAllI(); }
                return _AllI;
            }
        }

        internal DataTable BuildAllI()
        {
            _AllI = new DataTable();

            DataColumn _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Id";
            _AllI.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "Name";
            _AllI.Columns.Add(_colum);

            //Objeto de data layer para acceder a datos
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.Read_All_I(_Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdIndicator", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                _AllI.Rows.Add(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["Name"]));
            }

            return _AllI;
        }

        internal List<IColumnsReport> Is(Entities.Report_S_GA_A_FT_F_Obj_F f)
        {
            List<IColumnsReport> _oItems = new List<IColumnsReport>();


            // traee todos los facilityes
            DataTable _allI = AllI;
            //filtra el datatable
            DataRow[] _dataRow = _allI.Select("Id > 0");
            // por cada facility
            for (int i = 0; i < _dataRow.Length; i++)
            {
                //Declara e instancia una posicion
                Entities.Report_S_GA_A_FT_F_Obj_I _i = new Entities.Report_S_GA_A_FT_F_Obj_I
                    (Convert.ToInt64(_dataRow[i]["Id"]), _dataRow[i]["Name"].ToString(), f);

                if (_i.Show)
                {
                    _oItems.Add(_i);
                }

            }
            return _oItems;
        }
        #endregion

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
            _colum.ColumnName = "IdGeographicArea";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdScope";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdActivity";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdFacilityType";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdFacility";
            _DTResults.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdIndicator";
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

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.ReadTotals_by_Process(_ProcessGroupProcess.IdProcess, 
                _idIndicatortCO2e, _idIndicatorCO2, _idIndicatorCH4, _idIndicatortnN2O, _idIndicatorPFC, _idIndicatorHFC, _idIndicatorSF6, 
                _idIndicatorHCT, _idIndicatorHCNM, _idIndicatorC2H6, _idIndicatorC3H8, _idIndicatorC4H10, _idIndicatorCO, _idIndicatorNOx, 
                _idIndicatorSOx, _idIndicatorSO2, _idIndicatorH2S, _idIndicatorPM, _idIndicatorPM10, _StartDate, _EndDate);
        
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _DTResults.Rows.Add(Convert.ToInt64(_dbRecord["IdGeographicArea"]),
                                    Convert.ToInt64(_dbRecord["IdScope"]),
                                    Convert.ToInt64(_dbRecord["IdActivity"]),
                                    Convert.ToInt64(_dbRecord["IdFacilityType"]),
                                    Convert.ToInt64(_dbRecord["IdFacility"]),
                                    Convert.ToInt64(_dbRecord["IdIndicator"]), 
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

        internal void Results_I(Entities.Report_S_GA_A_FT_F_Obj_I i)
        {
            DataTable _result = DTResults;

            DataRow[] _dataRowResult = _result.Select("IdIndicator= " + i.Id.ToString() + " and IdGeographicArea = " + i.F.FT.A.GA.Id.ToString() + " and IdScope= " + i.F.FT.A.GA.S.Id.ToString() +
                                                    " and IdActivity= " + i.F.FT.A.Id.ToString() + " and IdFacilityType = " + i.F.FT.Id.ToString() +
                                                    " and IdFacility= " + i.F.Id.ToString());

            i.Show = false;
            for (int j = 0; j < _dataRowResult.Length; j++)
            {
                i.Result_tCO2e = Convert.ToDouble(_dataRowResult[j]["Result_tCO2e"]);
                i.Result_CO2 = Convert.ToDouble(_dataRowResult[j]["Result_CO2"]);
                i.Result_CH4 = Convert.ToDouble(_dataRowResult[j]["result_CH4"]);
                i.Result_N2O = Convert.ToDouble(_dataRowResult[j]["result_N2O"]);
                i.Result_PFC = Convert.ToDouble(_dataRowResult[j]["result_PFC"]);
                i.Result_HFC = Convert.ToDouble(_dataRowResult[j]["result_HFC"]);
                i.Result_SF6 = Convert.ToDouble(_dataRowResult[j]["Result_SF6"]);
                i.Result_HCT = Convert.ToDouble(_dataRowResult[j]["result_HCT"]);
                i.Result_HCNM = Convert.ToDouble(_dataRowResult[j]["result_HCNM"]);
                i.Result_C2H6 = Convert.ToDouble(_dataRowResult[j]["result_C2H6"]);
                i.Result_C3H8 = Convert.ToDouble(_dataRowResult[j]["result_C3H8"]);
                i.Result_C4H10 = Convert.ToDouble(_dataRowResult[j]["result_C4H10"]);
                i.Result_CO = Convert.ToDouble(_dataRowResult[j]["result_CO"]);
                i.Result_NOx = Convert.ToDouble(_dataRowResult[j]["result_NOx"]);
                i.Result_SOx = Convert.ToDouble(_dataRowResult[j]["result_SOx"]);
                i.Result_SO2 = Convert.ToDouble(_dataRowResult[j]["result_SO2"]);
                i.Result_H2S = Convert.ToDouble(_dataRowResult[j]["result_H2S"]);
                i.Result_PM = Convert.ToDouble(_dataRowResult[j]["result_PM"]);
                i.Result_PM10 = Convert.ToDouble(_dataRowResult[j]["result_PM10"]);

                i.Show = true;
            }


        }       
        #endregion
    }

   

}
