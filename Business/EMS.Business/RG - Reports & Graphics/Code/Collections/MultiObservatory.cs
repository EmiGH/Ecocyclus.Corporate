using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG.Collections
{
    public class MultiObservatory
    {
        private DataTable _DTResults;
        private DataTable _DTColumName;
        private Credential _Credential;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credential"></param>
        internal MultiObservatory(Credential credential)
        { 
            _Credential = credential; 
        }

        /// <summary>
        /// Devuelve un DT con todo armado
        /// </summary>
        /// <returns></returns>
        public DataTable Result(DataTable dtTVPProcessFilter, DateTime startDate, DateTime endDate)
        {
            DateTime _startDate = new DateTime();
            if (startDate < Convert.ToDateTime("01/01/1800"))
            { _startDate = Convert.ToDateTime("01/01/1800"); }
            else
            { _startDate = startDate; }
            DateTime _endDate = new DateTime();
            if (endDate > Convert.ToDateTime("01/01/2070"))
            { _endDate = Convert.ToDateTime("01/01/2070"); }
            else
            { _endDate = endDate; }


            //construye el dataaccess
            DataAccess.RG.ReportGraphic _dbReportGraphic = new Condesus.EMS.DataAccess.RG.ReportGraphic();
            //trae los datos
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbReportGraphic.MultiObservatory(dtTVPProcessFilter,_startDate,_endDate);
       
            //construye el dt
            DataTable _dtResults = BuildDTResult(_record);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {

                DataRow _row = _dtResults.NewRow();

                if (Convert.ToInt64(_dbRecord["IdProcess"]) != 0)
                {
                    _row["IdProcess"] = Convert.ToInt64(_dbRecord["IdProcess"]);
                    _row["ProcessName"] = new PF.Collections.Processes_LG(Convert.ToInt64(_dbRecord["IdProcess"]), _Credential).Item(_Credential.CurrentLanguage.IdLanguage).Title;


                    foreach (DataRow _dr in _DTColumName.Rows)
                    {
                        String _columName = _dr["ColumName"].ToString();

                        if (_dbRecord[_columName] == null)
                        {
                            _row[_columName] = 0;
                        }
                        else
                        {
                            _row[_columName] = _dbRecord[_columName];
                        }
                    }

                    _dtResults.Rows.Add(_row);
                }
            }

            return _dtResults;

        }
        
        private DataTable BuildDTResult(IEnumerable<System.Data.Common.DbDataRecord> record)
        {
            try
            {
                BuildDTColumName();

                _DTResults = new DataTable();

                DataColumn _colum = new DataColumn();
                _colum.DataType = System.Type.GetType("System.Int64");
                _colum.AllowDBNull = true;
                _colum.ColumnName = "IdProcess";
                _DTResults.Columns.Add(_colum);

                _colum = new DataColumn();
                _colum.DataType = System.Type.GetType("System.String");
                _colum.AllowDBNull = true;
                _colum.ColumnName = "ProcessName";
                _DTResults.Columns.Add(_colum);

                foreach (System.Data.Common.DbDataRecord _dbRecord in record)
                {
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        String _columName = _dbRecord.GetName(i);

                        _colum = new DataColumn();
                        _colum.DataType = System.Type.GetType("System.String");
                        _colum.AllowDBNull = true;
                        _colum.ColumnName = _columName;
                        _DTResults.Columns.Add(_colum);

                        _DTColumName.Rows.Add(_columName);
                    }
                    
                }
                return _DTResults;
            }
            catch(Exception ex)
            {
                return _DTResults;
            }
        }

        private void BuildDTColumName()
        {
            _DTColumName = new DataTable();

            DataColumn _colum = new DataColumn();          
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "ColumName";
            _DTColumName.Columns.Add(_colum);
                              
        }

    }
}
