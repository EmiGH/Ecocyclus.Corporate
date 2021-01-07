using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG.Entities
{
    public class FacilitiesByIndicators
    {
        private PF.Entities.ProcessGroupProcess _Process;

        private DataTable _Dt;

        internal DataTable Dt
        {
            get
            {
                if (_Dt == null)
                { _Dt = BuildDt(); }
                return _Dt;
            }
        }

        internal DataTable BuildDt()
        {
            _Dt = new DataTable();

            DataColumn _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdIndicator";
            _Dt.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "NameIndicator";
            _Dt.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdMeasurement";
            _Dt.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "NameMeasurement";
            _Dt.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdFacility";
            _Dt.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "NameSite";
            _Dt.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.Int64");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "IdMeasurementUnit";
            _Dt.Columns.Add(_colum);

            _colum = new DataColumn();
            _colum.DataType = System.Type.GetType("System.String");
            _colum.AllowDBNull = true;
            _colum.ColumnName = "NameMeasurementUnit";
            _Dt.Columns.Add(_colum);

            return _Dt;
        }


        public FacilitiesByIndicators(PF.Entities.ProcessGroupProcess process)
        {
            _Process = process;
        }

        public DataTable Sites(DateTime startDate, DateTime endDate)
        {
                DataTable _dt = Dt;

                DataAccess.RG.ReportGraphic _reportGraphics = new Condesus.EMS.DataAccess.RG.ReportGraphic();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _reportGraphics.FacilitiesByIndicators(_Process.IdProcess, _Process.Credential.CurrentLanguage.IdLanguage, startDate, endDate);

                //Se modifica con los datos que realmente se tienen que usar...
                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMeasurement", _Process.Credential).Filter();

                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    _dt.Rows.Add(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["NameIndicator"]),
                                Convert.ToInt64(_dbRecord["IdMeasurement"]), Convert.ToString(_dbRecord["NameMeasurement"]),
                                Convert.ToInt64(_dbRecord["IdFacility"]), Convert.ToString(_dbRecord["NameSite"]),
                                Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToString(_dbRecord["NameMeasurementUnit"])
                                );
                }

                return _dt;
        }

        public PA.Entities.Measurement Mesurement(Int64 idMeasurement)
        {
            return new PA.Collections.Measurements(_Process.Credential).Item(idMeasurement);
        }



    }
}
