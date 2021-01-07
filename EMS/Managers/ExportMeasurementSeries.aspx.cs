using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using Condesus.EMS.Business.PA.Entities;
using System.Text;


namespace Condesus.EMS.WebUI
{
    public partial class ExportMeasurementSeries : BasePage
    {
        #region Internal Properties
            private Int64 _IdMeasurement
            {
                get { return Convert.ToInt64(ViewState["IdMeasurement"]); }
                set { ViewState["IdMeasurement"] = value; }
            }
            private Int64 _IdTransformation
            {
                get { return Convert.ToInt64(ViewState["IdTransformation"]); }
                set { ViewState["IdTransformation"] = value; }
            }
            private DateTime? _StartDate
            {
                get { return Convert.ToDateTime(ViewState["StartDate"]); }
                set { ViewState["StartDate"] = value; }
            }
            private DateTime? _EndDate
            {
                get { return Convert.ToDateTime(ViewState["EndDate"]); }
                set { ViewState["EndDate"] = value; }
            }
        #endregion

            private DataTable ReturnDataGrid()
            {
                //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("MeasureDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("MeasureValue", System.Type.GetType("System.String"));
                _dt.Columns.Add("StartDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("EndDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("Status", System.Type.GetType("System.String"));

                //Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                //List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementPoints = _measurement.Series();
                //foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint in _measurementPoints)
                //{
                //    _dt.Rows.Add(_measurementPoint.MeasureDate,
                //        Common.Functions.CustomEMSRound(_measurementPoint.MeasureValue),
                //        _measurementPoint.StartDate,
                //        _measurementPoint.EndDate,
                //        _measurementPoint.Sing);
                //}



                //Si no hay id de transformacion quiere decir que es una medicion...
                if (_IdTransformation == 0)
                {
                    Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                    List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementPoints;    // = _measurement.Series();

                    if ((_StartDate != null) && (_StartDate != DateTime.MinValue))
                    {
                        _measurementPoints = _measurement.Series(_StartDate, _EndDate);
                    }
                    else
                    {
                        _measurementPoints = _measurement.Series();
                    }
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint in _measurementPoints)
                    {
                        _dt.Rows.Add(_measurementPoint.MeasureDate,
                            Common.Functions.CustomEMSRound(_measurementPoint.MeasureValue),
                            _measurementPoint.StartDate,
                            _measurementPoint.EndDate,
                            _measurementPoint.Sing);
                    }
                }
                else
                {
                    CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement).Transformation(_IdTransformation);
                    List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementTransformationPoints;      // = _calculateOfTransformation.Series();

                    if ((_StartDate != null) && (_StartDate != DateTime.MinValue))
                    {
                        _measurementTransformationPoints = _calculateOfTransformation.Series(_StartDate, _EndDate);
                    }
                    else
                    {
                        _measurementTransformationPoints = _calculateOfTransformation.Series();
                    }
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementTransformationPoint in _measurementTransformationPoints)
                    {
                        _dt.Rows.Add(_measurementTransformationPoint.MeasureDate,
                            Common.Functions.CustomEMSRound(_measurementTransformationPoint.MeasureValue),
                            _measurementTransformationPoint.StartDate,
                            _measurementTransformationPoint.EndDate,
                            false);
                    }
                }

                return _dt;
            }

            public static void ExportarExcelDataTable(DataTable dt)
            {
                const string FIELDSEPARATOR = "\t";
                const string ROWSEPARATOR = "\n";
                StringBuilder output = new StringBuilder();
                // Escribir encabezados    
                foreach (DataColumn dc in dt.Columns)
                {
                    output.Append(dc.ColumnName);
                    output.Append(FIELDSEPARATOR);
                }
                output.Append(ROWSEPARATOR);
                foreach (DataRow item in dt.Rows)
                {
                    foreach (object value in item.ItemArray)
                    {
                        output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace('.', ','));
                        output.Append(FIELDSEPARATOR);
                    }
                    // Escribir una línea de registro        
                    output.Append(ROWSEPARATOR);
                }
                HttpContext context = HttpContext.Current;
                //Agrega el contenido...
                context.Response.Write(output.ToString());
                // Agregamos los headers HTTP para que el archivo sea descargado
                context.Response.ContentType = "application/excel";
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=MeasurementSeries.xls");
                context.Response.End();
            }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Title = Resources.CommonListManage.Measurement;
            
            if (!IsPostBack)
            {
                if (Request.QueryString["IdMeasurement"] != null)
                {
                    _IdMeasurement = Convert.ToInt64(Request.QueryString["IdMeasurement"]);
                }
                if (Request.QueryString["IdTransformation"] != null)
                {
                    _IdTransformation = Convert.ToInt64(Request.QueryString["IdTransformation"]);
                }
                if (Request.QueryString["StartDate"] != null)
                {
                    _StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
                }
                else
                {
                    _StartDate = DateTime.MinValue;
                }
                if (Request.QueryString["EndDate"] != null)
                {
                    _EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
                }
                else
                {
                    _EndDate = DateTime.MaxValue;
                }


                ExportarExcelDataTable(ReturnDataGrid());
            }
        }
    }
}
