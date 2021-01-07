using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ciloci.FormulaEngine;
using System.Data.SqlTypes;
using System.Data.Common;
//using Condesus.EMS.Business.Common;
//using Condesus.EMS.Business.CT;
//using Condesus.EMS.Business.DS;
//using Condesus.EMS.Business.EP;
//using Condesus.EMS.Business.GIS;
//using Condesus.EMS.Business.IA;
//using Condesus.EMS.Business.KC;
//using Condesus.EMS.Business.NT;
//using Condesus.EMS.Business.PA;
//using Condesus.EMS.Business.PF;
//using Condesus.EMS.Business.RM;
using Condesus.EMS.Business.Security;
using Condesus.EMS.Business;
using Condesus.EMS.Business.Common;
using System.Data;


namespace TestBusiness
{


    public class PruebasDamian
    {
        public Condesus.EMS.Business.EMS _EMS;

        internal PruebasDamian()
        {


            //_EMS = new Condesus.EMS.Business.EMS("ems", "ems", "en-US", "127.0.0.1");
            //_EMS = new Condesus.EMS.Business.EMS("a.ternium", "12345", "en-US", "127.0.0.1");
            //_EMS = new Condesus.EMS.Business.EMS("a.condesus", "Admin.", "en-US", "127.0.0.1");
            //_EMS = new Condesus.EMS.Business.EMS("a.caba", "12345", "en-US", "127.0.0.1");
            _EMS = new Condesus.EMS.Business.EMS("admin", "admin", "en-US", "127.0.0.1");
            //_EMS = new Condesus.EMS.Business.EMS("operador", "operador", "en-US", "127.0.0.1");
            //_EMS = new Condesus.EMS.Business.EMS("famar", "famar", "en-US", "127.0.0.1");
        }

        #region EXCELL
        public void ExecuteAttach(String path, String fileName, Boolean chargeNotice)
        {
            //Declaro las variables necesarias
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlLibro;
            Microsoft.Office.Interop.Excel.Worksheet xlHoja1;
            Microsoft.Office.Interop.Excel.Sheets xlHojas;

            //inicializo la variable xlApp (referente a la aplicación)
            xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            //Muestra la aplicación Excel si está en true
            xlApp.Visible = false;

            //Abrimos el libro a leer (documento excel)
            xlLibro = xlApp.Workbooks.Open(path + fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            try
            {
                //Asignamos las hojas
                xlHojas = xlLibro.Sheets;
                //Asignamos la hoja con la que queremos trabajar: 
                xlHoja1 = (Microsoft.Office.Interop.Excel.Worksheet)xlHojas.get_Item(1);
                //recorremos las celdas que queremos y sacamos los datos 10 es el número de filas que queremos que lea
                if (false) //recorre por fila
                {
                    //DataTable _dtValues = BuildDataTableValueParamMeasurements();
                    //String _colum = StartIndexOfDataCols.Trim();
                    //Int16 _rowDate = Convert.ToInt16(StartIndexOfDataRows);
                    //Int16 _rowValue = Convert.ToInt16(StartIndexOfDataRows);
                    //String _startDate = IndexStartDate.Trim();
                    //String _endDate = IndexEndDate.Trim();

                    //char _letra = Convert.ToChar(_colum);

                    //int _starti = (int)_letra - 65;

                    //foreach (ConfigurationAsociationMeasurementExcelFile _configExcelFile in Measurements.Values)
                    //{
                    //    _rowDate = Convert.ToInt16(_configExcelFile.IndexDate);
                    //    _rowValue = Convert.ToInt16(_configExcelFile.IndexValue);

                    //    for (int i = _starti; i <= xlHoja1.UsedRange.Columns.Count; i++)
                    //    {
                    //        String _cellStartDate = ExcelColumnLetterFull(i) + _startDate;
                    //        string startdate = (string)xlHoja1.get_Range(_cellStartDate, _cellStartDate).Text;

                    //        String _cellEndDate = ExcelColumnLetterFull(i) + _endDate;
                    //        string enddate = (string)xlHoja1.get_Range(_cellEndDate, _cellEndDate).Text;

                    //        String _cellDate = ExcelColumnLetterFull(i) + _rowDate;
                    //        string date = (string)xlHoja1.get_Range(_cellDate, _cellDate).Text;

                    //        String _cell = ExcelColumnLetterFull(i) + _rowValue.ToString();
                    //        string value = (string)xlHoja1.get_Range(_cell, _cell).Text;

                    //        if (!String.IsNullOrEmpty(value))
                    //        {
                    //            _dtValues.Rows.Add(date, value, startdate, enddate);

                    //        }
                    //        if (_dtValues.Rows.Count > 0)
                    //        {
                    //            Byte[] _attachment = null;
                    //            Entities.MeasurementDevice _measurementDevice = null;
                    //            //hacer el add de la ejecucion
                    //            Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
                    //            ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAddTVP(
                    //                ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(),
                    //                "Carga desde el archivo exel",
                    //                _attachment,
                    //                _dtValues,
                    //                _measurementDevice,
                    //                _medicion.MeasurementUnit,
                    //                false,
                    //                chargeNotice);
                    //        }
                    //    }
                    //}
                }
                else //recorre por columna
                {
                    DataTable _dtValues = BuildDataTableValueParamMeasurements();
                    String _columDate = "a";
                    String _columValue = "a";
                    Int16 _row = Convert.ToInt16(2);
                    String _startDate = "A";
                    String _endDate = "A";

                    //foreach (ConfigurationAsociationMeasurementExcelFile _configExcelFile in Measurements.Values)
                    //{
                        //StringBuilder _archivo = new StringBuilder();
                        _columDate = "D";
                        _columValue = "C";
                        for (int i = _row; i <= xlHoja1.UsedRange.Rows.Count; i++)
                        {
                            string startdate = (string)xlHoja1.get_Range(_startDate + i, _startDate + i).Text;
                            string enddate = (string)xlHoja1.get_Range(_endDate + i, _endDate + i).Text;
                            string date = (string)xlHoja1.get_Range(_columDate + i, _columDate + i).Text;
                            string value = (string)xlHoja1.get_Range(_columValue + i, _columValue + i).Text;

                            if (!String.IsNullOrEmpty(value))
                            {
                                _dtValues.Rows.Add(date, value, startdate, enddate);

                            }
                        }
                        //Una vez finalizada la recorrida del
                        if (_dtValues.Rows.Count > 0)
                        {
                            //Byte[] _attachment = null;
                            //Entities.MeasurementDevice _measurementDevice = null;
                            ////hacer el add de la ejecucion
                            //Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
                            //((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAddTVP(
                            //    ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(),
                            //    "Carga desde el archivo exel",
                            //    _attachment,
                            //    _dtValues,
                            //    _measurementDevice,
                            //    _medicion.MeasurementUnit,
                            //    false,
                            //    chargeNotice);
                        }
                    //}
                }

            }
            catch (Exception ex)
            {
                //if (ex.GetType().Name == "System.Runtime.InteropServices.ExternalException")
                //{
                //    if (((System.Runtime.InteropServices.ExternalException)(ex)).ErrorCode == -2146827284)
                //    {
                //        throw new Exception(Common.Resources.Errors.ConfigurationExcelFileErrorConfig + " - " + ex.Message);
                //    }
                //}
                //else
                //{
                //    throw new Exception(ex.Message);
                //}
            }
            finally
            {
                //Cerrar el Libro
                xlLibro.Close(false, null, null);
                //Cerrar la Aplicación
                xlApp.Quit();
            }
        }
        private DataTable BuildDataTableValueParamMeasurements()
        {
            DataTable _dtTVPMeasurements = new DataTable();
            _dtTVPMeasurements.TableName = "dtValues";
            DataColumn _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.DateTime");
            _column.Caption = "Date";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.Decimal");
            _column.Caption = "Value";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.DateTime");
            _column.Caption = "StartDate";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.DateTime");
            _column.Caption = "EndDate";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            return _dtTVPMeasurements;
        }

        public void DATOSEXCEL()
        {
            //Microsoft.Office.Interop.Excel.Application xlApp;
            //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;

            //xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            //xlWorkBook = xlApp.Workbooks.Open(@"C:\tmpPrueba.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();
        }
        //public void RecogerDatosExcel(string ruta)
        //{
        //    //List<string> lista= new List<string> ();

        //    //Declaro las variables necesarias
        //    Microsoft.Office.Interop.Excel.Application xlApp;
        //    Microsoft.Office.Interop.Excel.Workbook xlLibro;
        //    Microsoft.Office.Interop.Excel.Worksheet xlHoja1;
        //    Microsoft.Office.Interop.Excel.Sheets xlHojas;
        //    //asigno la ruta dónde se encuentra el archivo    
        //    string RutaName = ruta; //+"\"" + name;
        //    //inicializo la variable xlApp (referente a la aplicación)
        //    xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    //Muestra la aplicación Excel si está en true
        //    xlApp.Visible = false;
        //    //Abrimos el libro a leer (documento excel)
        //    xlLibro = xlApp.Workbooks.Open(RutaName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //    try
        //    {
        //        //Asignamos las hojas
        //        xlHojas = xlLibro.Sheets;
        //        //Asignamos la hoja con la que queremos trabajar: 
        //        xlHoja1 = (Microsoft.Office.Interop.Excel.Worksheet)xlHojas.get_Item(1);
        //        Condesus.EMS.Business.PA.Entities.ConfigurationExcelFile _bulkLoadExcelFile = _EMS.User.PerformanceAssessments.Configuration.ConfigurationExcelFile(14);
        //        //recorremos las celdas que queremos y sacamos los datos 10 es el número de filas que queremos que lea
        //        if (_bulkLoadExcelFile.IsDataRows) //recorre por fila
        //        {
        //            int _colum = Convert.ToInt16(_bulkLoadExcelFile.StartIndexOfDataCols);
        //            String _row = _bulkLoadExcelFile.StartIndexOfDataRows;
        //            String _date = _bulkLoadExcelFile.DatesIndex;

        //            foreach (String _Index in _bulkLoadExcelFile.Measurements.Keys)
        //            {
        //                StringBuilder _archivo = new StringBuilder();
        //                _row = _Index;
        //                for (int i = _colum; i < xlHoja1.UsedRange.Columns.Count; i++)
        //                {
        //                    string fecha = (string)xlHoja1.get_Range(_date + Char.ConvertFromUtf32(i), _date + Char.ConvertFromUtf32(i)).Text;
        //                    string med = (string)xlHoja1.get_Range(_row + Char.ConvertFromUtf32(i), _row + Char.ConvertFromUtf32(i)).Text;


        //                    if (med != String.Empty)
        //                    {
        //                        _archivo.Append(fecha + ";" + med + "\r");

        //                    }
        //                    else
        //                    {
        //                        Byte[] _fileStreamBinary = System.Text.Encoding.Unicode.GetBytes(_archivo.ToString());
        //                        //hacer el add de la ejecucion
        //                        Condesus.EMS.Business.PA.Entities.Measurement _medicion = null;// _bulkLoadExcelFile.Measurements[_Index];
        //                        //((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAdd(
        //                        //    _EMS.User.Person.Posts.First(), name, _archivo.ToString(), _fileStreamBinary, "", true);
        //                    }
        //                }
        //            }
        //        }
        //        else //recorre por columna
        //        {
        //            string _colum = _bulkLoadExcelFile.StartIndexOfDataCols;
        //            int _row = Convert.ToInt16(_bulkLoadExcelFile.StartIndexOfDataRows);
        //            string _date = _bulkLoadExcelFile.DatesIndex;

        //            foreach (String _Index in _bulkLoadExcelFile.Measurements.Keys)
        //            {
        //                StringBuilder _archivo = new StringBuilder();
        //                _colum = _Index;
        //                for (int i = _row; i < xlHoja1.UsedRange.Rows.Count; i++)
        //                {
        //                    string fecha = (string)xlHoja1.get_Range(_date + i, _date + i).Text;
        //                    string med1 = (string)xlHoja1.get_Range(_colum + i, _colum + i).Text;

        //                    if (med1 != String.Empty)
        //                    {
        //                        _archivo.Append(fecha + ";" + med1 + "\r");

        //                    }
        //                    else
        //                    {
        //                        Byte[] _fileStreamBinary = System.Text.Encoding.Unicode.GetBytes(_archivo.ToString());
        //                        //hacer el add de la ejecucion
        //                        Condesus.EMS.Business.PA.Entities.Measurement _medicion = null;// _bulkLoadExcelFile.Measurements[_Index];
        //                        //((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAdd(
        //                        //    _EMS.User.Person.Posts.First(), name, _archivo.ToString(), _fileStreamBinary, "", true);
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch 
        //    {
        //    }
        //    finally
        //    {
        //        //Cerrar el Libro
        //        xlLibro.Close(false, null, null);
        //        //Cerrar la Aplicación
        //        xlApp.Quit();
        //    }
        //}
        #endregion

        #region Calculo
        public void Executecalculo()
        {
            try
            {

                //falta crear variables independientes y validar la existencia de los valores
                //Valida la formula
                Condesus.EMS.Business.Common.Formulator _formulator = new Condesus.EMS.Business.Common.Formulator();

                //String _formul = "WASTE(base, 0.9, 0, 0.1, 0.5, 0.5, 1, 0.35, 0.1, 0.90484)";

                //Double _value = Convert.ToDouble("439");
                //DateTime _date = Convert.ToDateTime("04/01/2011 12:00:00");
                //DateTime _startDate = Convert.ToDateTime("01/01/2011 12:00:00");
                //DateTime _endDate = Convert.ToDateTime("04/01/2011 12:00:00");


                ////Carga variables
                //String _formula = _formul.ToLower();

                //if (_formula.Contains("waste"))
                //{
                //    int _MeasureMes = _date.Month;
                //    int _MeasureDia = _date.Day; ;
                //    int _MeasureAno = _date.Year;
                //    int _MeasureHora = _date.Hour;
                //    int _MeasureMin = _date.Minute;
                //    int _MeasureSeg = _date.Second;
                //    int _StartMes = _startDate.Month;
                //    int _StartDia = _startDate.Day;
                //    int _StartAno = _startDate.Year;
                //    int _StartHora = _startDate.Hour;
                //    int _StartMin = _startDate.Minute;
                //    int _StartSeg = _startDate.Second;
                //    int _EndMes = _endDate.Month;
                //    int _EndDia = _endDate.Day;
                //    int _EndAno = _endDate.Year;
                //    int _EndHora = _endDate.Hour;
                //    int _EndMin = _endDate.Minute;
                //    int _EndSeg = _endDate.Second;

                //    _formula = _formula.Replace(")", "," + "439" + "," +
                //        _MeasureMes + "," + _MeasureDia + "," + _MeasureAno + "," + _MeasureHora + "," + _MeasureMin + "," + _MeasureSeg + "," +
                //        _StartMes + "," + _StartDia + "," + _StartAno + "," + _StartHora + "," + _StartMin + "," + _StartSeg + "," +
                //        _EndMes + "," + _EndDia + "," + _EndAno + "," + _EndHora + "," + _EndMin + "," + _EndSeg + ")");
                //}


                string _formula = "48550.44 / 298374.237417582";
                //_formula = "48550.44/582871.6035";

                Double _result = _formulator.Execute(_formula);
                
                //_formulator.EvaluateFormula(_formula);

                ////Carga la variable a transformar, la "base" es el baseTransformer
                //_formula = _formula.Replace("base", _value.ToString());

                //_formula = ReplaceIntrincicFuctions(_formula);

                //Carga el resto de los parametros
                //foreach (CalculateOfTransformationParameter _parameter in this.Parameters.Values)
                //{
                //    Double _operateValue = _parameter.Operand.OperateValue(this, _startDate, _endDate);
                //    //si viene minValue no tiene que ejecutar
                //    if (_operateValue == Double.MinValue) { _calculate = false; }

                //    _formula = _formula.Replace(_parameter.IdParameter.ToLower(), Convert.ToString(_operateValue));
                //}

                //Valida que despues de los reemplazos a la formula no le hayan quedado letras y sean solo numeros
                //for (int i = 97; i < 123; i++)
                //{
                //    String _letter = Char.ConvertFromUtf32(i);
                //HashSeth
                    
                //    //Si la formula contiene una letra genera una exepcion
                //    if (_formula.Contains(_letter))
                //    {
                //        throw new Exception(": " + _letter);
                //    }
                //}

             
                    //Ejecuta la formula
                Double _result1 = _formulator.Execute(_formula);

                    //Graba el resultado
                    // new Collections.CalculateOfTransformationResults(BaseTransformer, this).Add(this, this.BaseTransformer, _result, _date, _startDate, _endDate);
              
            }
            //}

            catch (Exception ex)
            { }

        }

        private String ReplaceIntrincicFuctions(String formula)
        {
            //reemplaza todas las funciones intrinsecas, las pasa a mayuscula para que no se reemplazen cuando reemplazo los operadores
            formula = formula.Replace("abs", "ABS");
            formula = formula.Replace("acos", "ACOS");
            formula = formula.Replace("asin", "ASIN");
            formula = formula.Replace("atan", "ATAN");
            formula = formula.Replace("atan2", "ATAN2");
            formula = formula.Replace("bigmull", "BIGMULL");
            formula = formula.Replace("ceiling", "CEILING");
            formula = formula.Replace("cos", "COS");
            formula = formula.Replace("cosh", "COSH");
            formula = formula.Replace("divrem", "DIVREM");
            formula = formula.Replace("exp", "EXP");
            formula = formula.Replace("floor", "FLOOR");
            formula = formula.Replace("ieeeremainder", "IEEEREMAINDER");
            formula = formula.Replace("log", "LOG");
            formula = formula.Replace("log10", "LOG10");
            formula = formula.Replace("max", "MAX");
            formula = formula.Replace("min", "MIN");
            formula = formula.Replace("pow", "POW");
            formula = formula.Replace("round", "ROUND");
            formula = formula.Replace("sing", "SIGN");
            formula = formula.Replace("sin", "SIN");
            formula = formula.Replace("sinh", "SINH");
            formula = formula.Replace("sqrt", "SQRT");
            formula = formula.Replace("tan", "TAN");
            formula = formula.Replace("tanh", "TANH");
            formula = formula.Replace("truncate", "TRUNCATE");
            formula = formula.Replace("if", "IF");

            //CUSTOM FUNCTIONS
            formula = formula.Replace("ef_drillingch4", "EF_DRILLINGCH4");
            formula = formula.Replace("ef_drillingc2h6", "EF_DRILLINGC2H6");
            formula = formula.Replace("ef_drillingc3h8", "EF_DRILLINGC3H8");
            formula = formula.Replace("ef_drillingc4h10", "EF_DRILLINGC4H10");
            formula = formula.Replace("ef_drillingh2s", "EF_DRILLINGH2S");
            formula = formula.Replace("ef_drillingco2", "EF_DRILLINGCO2");
            formula = formula.Replace("waste", "WASTE");
            return formula;
        }
        #endregion

        #region multiobser
        //private DataTable _DTResults;
        //private DataTable _DTColumName;

        //private DataTable multiobser()
        //{


        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _EMS.User.Result();
        //    //construye el dt
        //    DataTable _dtResults = BuildDTResult(_record);

            IEnumerable<System.Data.Common.DbDataRecord> _record = null;// = _EMS.User.Result();
            //construye el dt
            //DataTable _dtResults = BuildDTResult(_record);

        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
        //    {

        //        DataRow _row = _dtResults.NewRow();

        //        _row["IdProcess"] = Convert.ToInt64(_dbRecord["IdProcess"]);
        //        _row["ProcessName"] = _EMS.User.ProcessFramework.Map.ProcessTitle(Convert.ToInt64(_dbRecord["IdProcess"]));

                
        //        foreach(DataRow _dr in _DTColumName.Rows)
        //        {
        //            String _columName = _dr["ColumName"].ToString();

        //            if(_dbRecord[_columName] == null)
        //            {
        //                _row[_columName] = 0;
        //            }
        //            else 
        //            {
        //            _row[_columName] = _dbRecord[_columName];
        //            }
        //        }

        //        _dtResults.Rows.Add(_row);
        //        //_dt.Rows.Add(Convert.ToInt64(_dbRecord["IdProcess"]),
        //        //    Convert.ToString(_dbRecord["ProcessName"]),
        //        //    );
        //    }

        //    return _dtResults;

        //}
        //private DataTable BuildDTResult(IEnumerable<System.Data.Common.DbDataRecord> record)
        //{
        //    try
        //    {
        //        BuildDTColumName();

        //        _DTResults = new DataTable();

        //        DataColumn _colum = new DataColumn();
        //        _colum.DataType = System.Type.GetType("System.Int64");
        //        _colum.AllowDBNull = true;
        //        _colum.ColumnName = "IdProcess";
        //        _DTResults.Columns.Add(_colum);

        //        _colum = new DataColumn();
        //        _colum.DataType = System.Type.GetType("System.String");
        //        _colum.AllowDBNull = true;
        //        _colum.ColumnName = "ProcessName";
        //        _DTResults.Columns.Add(_colum);

        //        foreach (System.Data.Common.DbDataRecord _dbRecord in record)
        //        {
        //            for (int i = 1; i < int.MaxValue; i++)
        //            {
        //                String _columName = _dbRecord.GetName(i);

        //                _colum = new DataColumn();
        //                _colum.DataType = System.Type.GetType("System.String");
        //                _colum.AllowDBNull = true;
        //                _colum.ColumnName = _columName;
        //                _DTResults.Columns.Add(_colum);

        //                _DTColumName.Rows.Add(_columName);
        //            }
                    
        //        }
        //        return _DTResults;
        //    }
        //    catch(Exception ex)
        //    {
        //        return _DTResults;
        //    }
        //}

        //internal void BuildDTColumName()
        //{
        //    _DTColumName = new DataTable();

        //    DataColumn _colum = new DataColumn();          
        //    _colum.DataType = System.Type.GetType("System.String");
        //    _colum.AllowDBNull = true;
        //    _colum.ColumnName = "ColumName";
        //    _DTColumName.Columns.Add(_colum);
                              
        //}

        #endregion

            private void PerformTasks(String userEMS, String passwordEMS, String culture)
            {

                //Construye la credencial para usar la dll
                Condesus.EMS.Business.EMS _EMS = new Condesus.EMS.Business.EMS(userEMS, passwordEMS, culture, "127.0.0.1");

                //Cambia los flag de tarea y measurement
                _EMS.EMSServices.OverdueInTaskAndMeasurements();

                //Genera las excepciones de over due en las tareas
                _EMS.EMSServices.ExceptionAutomaticAlert();

                //hace las notificaciones de avance
                _EMS.EMSServices.Notifications.AdvanceNotifications();

                
                //Borra todos lo errores antes de que se vuelven a ejecutar
                _EMS.EMSServices.DeleteAllErrors();

                
                //Calculos
                Calculation(_EMS);
                
                //Metodo que se encarga del envio de mails
                Notifications(_EMS);

                ///
                /// Fin zona
                /// 

            }

            private void Calculation(Condesus.EMS.Business.EMS EMS)
            {
                //pide todas las transformaciones
                foreach (Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _calculateOfTransformation in EMS.EMSServices.Transformations.CalculateOfTransformations.Values)
                {
                    //Ejecuta el calculo
                    _calculateOfTransformation.Execute();
                }

            }

            private void Notifications(Condesus.EMS.Business.EMS EMS)
            {

                //Pide las notificaciones q tiene q hacer
                List<Condesus.EMS.Business.NT.Entities.NotificationMessage> _NotificationMessages = EMS.EMSServices.Notifications.NotificationMessages;

            }


        public void Ejecutar(DateTime startdate, DateTime enddate)//Zona de pruebas
        {          

            try
            {
                //PerformTasks("Admin", "admin", "en-US");

                Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _calculate = _EMS.EMSServices.Transformations.CalculateOfTransformation(454);
                _calculate.Execute();


                //Condesus.EMS.Business.PA.Entities.ConfigurationExcelFile _calculate = _EMS.User.PerformanceAssessments.Configuration.ConfigurationExcelFile(37);
                //_calculate.ExecuteAttach(@"C:\Users\damian.bonanno\Desktop\", "cantidad de coches.xlsx", false);
                //Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _pgp = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_EMS.User.ProcessFramework.Map.Process(1842);
                //_EMS.User.ProcessFramework.Map.Remove(_pgp);
                //foreach (Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _calculateOfTransformation in _EMS.EMSServices.Transformations.CalculateOfTransformations.Values)
                //{
                //    if (_calculateOfTransformation.IdTransformation == 284)
                //    {
                //        Int16 x = 10;
                //    }
                //    //Ejecuta el calculo
                //    _calculateOfTransformation.Execute();
                //}
            }
            catch (Exception ex)
            {

            }


            Condesus.EMS.Business.PF.Entities.ProcessTaskExecution _pte = ((Condesus.EMS.Business.PF.Entities.ProcessTask)_EMS.User.ProcessFramework.Map.Process(338)).ProcessTaskExecutionMeasurementLast();

            if (_pte.GetType().Name == "ProcessTaskExecutionMeasurement")
            {
                Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionMeasurement _ptem = (Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionMeasurement)_pte;
            }

           // ExecuteAttach(@"C:\Users\damian.bonanno\Desktop\", "Libro1.xlsx", false);

            //Report();

            //Executecalculo();

            //Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _calculate = _EMS.EMSServices.Transformations.CalculateOfTransformation(293);
            //_calculate.Execute();


            #region calculo de transformacion
            foreach (Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _calculateOfTransformation in _EMS.EMSServices.Transformations.CalculateOfTransformations.Values)
            {
                //Ejecuta el calculo
                //if (_calculateOfTransformation.IdTransformation == 295)
                //{
                    _calculateOfTransformation.Execute();       
                //}
            }
            #endregion               

            #region Formulas


            //string _formula = "(0.0010*68.43*(4.3381*2.71828182845904^(0.013*(0.44*((530.25+503.149999999)/2)+0.56*(((530.25+503.149999999)/2)+6*0.68*1389.7)+0.079*0.68*1389.7)))*512265*1*IF(((180+((5.614*512265)/5650.3466752))/(6*((5.614*512265)/5650.3466752)))>36,  ((180+((5.614*512265)/5650.3466752))/(6*((5.614*512265)/5650.3466752))),1))/0.000453592";

            //Condesus.EMS.Business.Common.Formulator _formulator = new Condesus.EMS.Business.Common.Formulator();

            //_formulator.EvaluateFormula(_formula);

            //Double _res = _formulator.Execute(_formula);

            //////instancia la dll
            //FormulaEngine FE = new FormulaEngine();

            ////FE.FunctionLibrary.AddFunction(Sumade2valores);

            ////ejecuta el metodo de creacion de la formula
            //Formula F = FE.CreateFormula(_formula);
            ////devuelve el resultado del tipo object
            //object _Result = F.Evaluate();

            //decimal _re = Convert.ToDecimal(_Result);


            ////_formula = _formula.ToLower();

            //_formula = _formula.Replace("A", "22");
            //_formula = _formula.Replace("o", "25");

            //for (int i = 97; i < 123; i++)
            //{
            //    String _letter = Char.ConvertFromUtf32(i);
            //    if (_formula.Contains(_letter))
            //    {
            //        string _msg = "al ejecutar se encontro el caracter: " + _letter;
            //    }

            //}
            #endregion


            String espero_aca = "";

        }
        #region Metodos
        #region report

        private void Report()
        {

            //Condesus.EMS.Business.RG.Entities.Report_GA_S_A_FT_F _rep = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(377).Report_GA_S_A_FT_F;
            Condesus.EMS.Business.RG.Entities.Report_S_GA_A_FT_F _rep = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(231).Report_S_GA_A_FT_F;            

            List<Condesus.EMS.Business.RG.IColumnsReport> _gas = _rep.S(37, 67, 68, 70, 71, 69, 72, 182, 183, 257, 258, 262, 259, 256, 281, 260, 268, 269, 270, Convert.ToDateTime("2001/01/01"), Convert.ToDateTime("2012/01/01"));
            foreach (Condesus.EMS.Business.RG.IColumnsReport _ga in _gas)
            {
                double _tc = _ga.Result_tCO2e;
            }


            //Condesus.EMS.Business.RG.Entities.Graphic_Bar _gra = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(231).Graphic_Bar;
            //foreach (Condesus.EMS.Business.RG.IGraphicBar _ga in _gra.Bar_FacilityTypeByScope(1, 37, 31, 32, 33, 34, 35, 36, 182, 183, 257, 258, 262, 36, 125, 139, 121, 206, 207, 208, Convert.ToDateTime("2001/01/01"), Convert.ToDateTime("2012/01/01")))
            //{
            //    String _tc = _ga.Name;
            //}

            //////Condesus.EMS.Business.RG.ReportGraphic _rep = _EMS.User.ReportGraphic;
            //Condesus.EMS.Business.RG.Entities.Report_S_GA_A_FT_F _rep = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(231).Report_S_GA_A_FT_F;
            //Condesus.EMS.Business.RG.Entities.Report_S_A_FT_F _rep = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(231).Report_S_A_FT_F;
            //Condesus.EMS.Business.RG.Entities.Report_GA_S_A_FT_F _rep = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(231).Report_GA_S_A_FT_F;
            //Condesus.EMS.Business.RG.Entities.Report_GA_FT_F_S_A _rep = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(231).Report_GA_FT_F_S_A;            
            //Condesus.EMS.Business.RG.Entities.Report_FT_F_S_A _rep = _EMS.User.ProcessFramework.Map.ProcessGroupProcess(231).Report_FT_F_S_A;
            //List<Condesus.EMS.Business.RG.IColumnsReport> _gas = _rep.FT(37, 31, 32, 33, 34, 35, 36, 182, 183, 257, 258, 262, 36, 125, 139, 121, 206, 207, 208, Convert.ToDateTime("2001/01/01"), Convert.ToDateTime("2012/01/01"));
            //foreach (Condesus.EMS.Business.RG.IColumnsReport _ga in _gas)
            //{
            //    double _tc = _ga.Result_tCO2e;
            //}

        }
        #endregion
        private void TiemposdeTareasOverdue()
        {
            foreach (Condesus.EMS.Business.PF.Entities.ProcessTaskExecution _processTaskExecution in _EMS.User.Dashboard.TaskOverdue.Values)
            {
                DateTime _date = _processTaskExecution.Date;
                Int64 _idex = _processTaskExecution.IdExecution;
                Int64 _id = _processTaskExecution.ProcessTask.IdProcess;
                //string _tastietl = _processTaskExecution.ProcessTask.LanguageOption.Title;
                //string _dam = _processTaskExecution.ProcessTask.ProcessGroupProcess.LanguageOption.Title;
                //DateTime _enddate = _processTaskExecution.EndDate;
                //string _state = _processTaskExecution.State;
            }
        }

        private void ProcessClasification()
        {
            Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessClassification> ProcessClassifications = _EMS.User.ProcessFramework.Map.ProcessClassifications();
        }
        private void Tareas()
        {
            //Alta de una tarea de medicion            
            Condesus.EMS.Business.DS.Entities.Organization _Organization = _EMS.User.DirectoryServices.Map.Organization(1);
            Condesus.EMS.Business.DS.Entities.Person _Person = _Organization.Person(1);
            //Condesus.EMS.Business.PF.Entities.ProcessGroupNode _node = (Condesus.EMS.Business.PF.Entities.ProcessGroupNode)_EMS.User.ProcessFramework.Map.Process(434);
            Condesus.EMS.Business.PF.Entities.TimeUnit _TimeUnit = _EMS.User.ProcessFramework.Configuration.TimeUnit(1);
            Condesus.EMS.Business.PA.Entities.Indicator _Indicator = _EMS.User.PerformanceAssessments.Map.Indicator(80);
            Condesus.EMS.Business.PA.Entities.MeasurementDevice _device = null;

            Condesus.EMS.Business.PA.Entities.MeasurementUnit _MeasurementUnit = _EMS.User.PerformanceAssessments.Configuration.Magnitud(24).MeasurementUnit(2);
            Condesus.EMS.Business.PA.Entities.ParameterGroup _pargrup = _Indicator.ParameterGroup(8);
            List<Condesus.EMS.Business.DS.Entities.Post> _posts = new List<Condesus.EMS.Business.DS.Entities.Post>();
            Condesus.EMS.Business.GIS.Entities.Site _site = null;
            Condesus.EMS.Business.KC.Entities.Resource _res = null;
            Condesus.EMS.Business.PA.Entities.Quality _Quality = null;
            Condesus.EMS.Business.PA.Entities.Methodology _methodolo = null;
            Condesus.EMS.Business.PA.Entities.AccountingActivity _AccountingActivity = null;
            Condesus.EMS.Business.PA.Entities.AccountingScope _AccountingScope = null;
            Condesus.EMS.Business.NT.Entities.NotificationRecipientEmail _NotificationRecipientEmail = new Condesus.EMS.Business.NT.Entities.NotificationRecipientEmail("damian@hjotmail.com");
            Condesus.EMS.Business.NT.Entities.NotificationRecipientEmail _NotificationRecipientEmail1 = new Condesus.EMS.Business.NT.Entities.NotificationRecipientEmail("damian@hotmail.com");
            Condesus.EMS.Business.NT.Entities.NotificationRecipientEmail _NotificationRecipientEmail2 = new Condesus.EMS.Business.NT.Entities.NotificationRecipientEmail("damian@hjotmail.com");
            Condesus.EMS.Business.DS.Entities.ContactEmail _ContactEmail = _Person.ContactEmail(82);
            Condesus.EMS.Business.NT.Entities.NotificationRecipientPerson _NotificationRecipientEmail3 = new Condesus.EMS.Business.NT.Entities.NotificationRecipientPerson(_Person, _ContactEmail);
            List<Condesus.EMS.Business.NT.Entities.NotificationRecipient> _NotificationRecipients = new List<Condesus.EMS.Business.NT.Entities.NotificationRecipient>();
            _NotificationRecipients.Add(_NotificationRecipientEmail);
            _NotificationRecipients.Add(_NotificationRecipientEmail1);
            _NotificationRecipients.Add(_NotificationRecipientEmail2);
            _NotificationRecipients.Add(_NotificationRecipientEmail3);
            //_node.ProcessTasksAdd(_device, _pargrup, _Indicator, "medicion de prueba", "med prueba", _TimeUnit, 1, _MeasurementUnit, true, true, true, 1, 1, "Tarea de medicion de prueba", "probar", "solo hacer la prueba de alta", _node, DateTime.Now, DateTime.Now, 1, 1, 1, true, 1, _TimeUnit, _TimeUnit, "", _posts, _site, _res, "lo da la imaginacion", "ups", 1, _Quality, _methodolo, _AccountingScope, _AccountingActivity, "referencia", _NotificationRecipients);



        }
        private void Transformations()
        {
            foreach (Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _calculateOfTransformation in _EMS.EMSServices.Transformations.CalculateOfTransformations.Values)
            {
                //Ejecuta el calculo
                _calculateOfTransformation.Execute();
            }




            //Condesus.EMS.Business.PA.Entities.MeasurementUnit _MeasurementUnit = _EMS.User.PerformanceAssessments.Configuration.Magnitud(1).MeasurementUnit(159);
            //Condesus.EMS.Business.PA.Entities.Indicator _Indicator = _EMS.User.PerformanceAssessments.Map.Indicator(40);
            //Condesus.EMS.Business.PA.Entities.Measurement _measurement34 = _EMS.User.PerformanceAssessments.Configuration.Measurement(34);
            ////_measurement34.TransformationAdd(_Indicator, _MeasurementUnit, "x*a","trans prueba","1 prueba");

            ////Condesus.EMS.Business.PA.Entities.Measurement _measurement2 = _EMS.User.PerformanceAssessments.Configuration.Measurement(2);

            ////Condesus.EMS.Business.PA.Entities.CalculateOfTransformationParameter _CalculateOfTransformationParameter = _measurement34.Transformation(3).Parameter("b");

            ////_measurement34.Transformation(3).Modify(_Indicator, _MeasurementUnit, "a*x", "Transformacion modify", "prueba 1");
            ////_measurement34.Transformation(3).ParameterAdd("b", _measurement2);

            //_measurement34.Transformation(3).Execute();


        }
        private void posts()
        {
           // _EMS.User.DirectoryServices.Map.Organization(2).Posts();

            Condesus.EMS.Business.DS.Entities.PersonwithUser _person = (Condesus.EMS.Business.DS.Entities.PersonwithUser)_EMS.User.DirectoryServices.Map.Organization(62).Person(12);
            
            Condesus.EMS.Business.GIS.Entities.GeographicArea _G = _EMS.User.GeographicInformationSystem.GeographicArea(1765);
            Condesus.EMS.Business.DS.Entities.FunctionalArea _FA = _EMS.User.DirectoryServices.Map.Organization(62).FunctionalArea(3);
            Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _GFA = _EMS.User.DirectoryServices.Map.Organization(62).GeographicFunctionalArea(_FA,_G);
            Condesus.EMS.Business.DS.Entities.Position _P = _EMS.User.DirectoryServices.Map.Organization(62).Position(3);
            Condesus.EMS.Business.DS.Entities.FunctionalPosition _FP = _EMS.User.DirectoryServices.Map.Organization(62).FunctionalPosition(_P, _FA);
            Condesus.EMS.Business.DS.Entities.JobTitle _job = _EMS.User.DirectoryServices.Map.Organization(62).JobTitle(_GFA, _FP);

            _person.Remove(_person.Post(_job));

        }
        private void processlg()
        {
            Condesus.EMS.Business.PF.Entities.Process _process = _EMS.User.ProcessFramework.Map.Process(442);
            Condesus.EMS.Business.PF.Entities.Process_LG _Process_LG = _process.LanguagesOptions.Add("en-US", "dami", "dami", "dami");
        }
        private void Notificaciones()
        {

            Condesus.EMS.Business.NT.Entities.NotificationConfiguration _con = _EMS.EMSServices.Notifications.NotificationConfiguration();
            _EMS.EMSServices.Notifications.NotificationConfigurationAdd("damian.bonanno@condesus.com.ar", "srv-dev.condesus.local", "damian.bonanno@condesus.com.ar", "damian12", 2000);


            List<Condesus.EMS.Business.NT.Entities.NotificationMessage> _NotificationMessages = _EMS.EMSServices.Notifications.NotificationMessages;

            foreach (Condesus.EMS.Business.NT.Entities.NotificationMessage _msg in _NotificationMessages)
            {
                String _to = _msg.To;
                String _From = _msg.From;
                String _Subject = _msg.Subject;
                String _Body = _msg.Body;
            }
        }

        private void telephone()
        {
            Condesus.EMS.Business.DS.Entities.Telephone _tel = _EMS.User.DirectoryServices.Map.Organization(1).Facility(3).Telephone(2);
        }
        private void Facility()
        {
            //Alta
            Condesus.EMS.Business.DS.Entities.Organization _Organization = _EMS.User.DirectoryServices.Map.Organization(1);
            //Condesus.EMS.Business.KC.Entities.ResourceCatalog _respict = (Condesus.EMS.Business.KC.Entities.ResourceCatalog)_EMS.User.KnowledgeCollaboration.Map.Resource(26);
            //Condesus.EMS.Business.KC.Entities.ResourceCatalog _respictsec = null;
            //Condesus.EMS.Business.GIS.Entities.Facility _Facili = _Organization.FacilityAdd("23,43", "fabrica1", "central", _respict);
            Condesus.EMS.Business.GIS.Entities.Facility _Facili = _Organization.Facility(3);
            //Dictionary<Int64, Condesus.EMS.Business.GIS.Entities.Facility> _colFacili = _Organization.Facilities;
            //_Facili.SectorAdd("nana", "ofi 1", "gern", _respictsec);
            Condesus.EMS.Business.GIS.Entities.Sector _sec = _Facili.Sector(5);
            Condesus.EMS.Business.GIS.Entities.GeographicArea _geoa1 = _EMS.User.GeographicInformationSystem.GeographicArea(9);
            //Condesus.EMS.Business.GIS.Entities.Address _direc = _Facili.AddressesAdd(_geoa1,"23,54","pola","123","-", "-", "1144");
            Condesus.EMS.Business.GIS.Entities.AddressFacility _add = _Facili.Address(1);
            //Dictionary<Int64, Condesus.EMS.Business.GIS.Entities.Address> _adds = _Facili.Addresses;
            //((Condesus.EMS.Business.GIS.Entities.AddressFacility)_add).Modify(_sec, _geoa1, "-", "na", "na", "sa", "sa", "sa");
            Condesus.EMS.Business.DS.Entities.Telephone _tel = _Facili.TelephoneAdd("", "12344356", "", "", "");
        }
        private void Geographicareas()
        {
            //Alta
            Dictionary<Int64, Condesus.EMS.Business.GIS.Entities.GeographicArea> _colgeoa = _EMS.User.GeographicInformationSystem.GeographicArea(10).Children;
            Condesus.EMS.Business.GIS.Entities.GeographicArea _parentgeoa = null;
            //Condesus.EMS.Business.GIS.Entities.GeographicArea _geoa = _EMS.User.GeographicInformationSystem.GeographicAreasAdd(_parentgeoa, "", "", "america", "");
            Condesus.EMS.Business.GIS.Entities.GeographicArea _geoa1 = _EMS.User.GeographicInformationSystem.GeographicArea(9);
            //_geoa.Modify(_parentgeoa, "22,322,324,423", "AM", "America", "Continente");
            //Condesus.EMS.Business.GIS.Entities.GeographicArea _geoa1 = _EMS.User.GeographicInformationSystem.GeographicAreasAdd(_geoa, "", "AR", "Argentina", "Pais");
            //Condesus.EMS.Business.GIS.Entities.GeographicArea _geoa2 = _EMS.User.GeographicInformationSystem.GeographicAreasAdd(_geoa, "", "AR", "bolivia", "Pais");
            //Dictionary<Int64, Condesus.EMS.Business.GIS.Entities.GeographicArea> _colgeoa = _geoa.Children;
            //_EMS.User.GeographicInformationSystem.Remove(_geoa);

        }
        private void ExtendedProperties()
        {
            Condesus.EMS.Business.EP.Entities.ExtendedProperty _extprop = _EMS.User.ExtendedProperties.ExtendedPropertyClassification(39).ExtendedProperty(48);
            _EMS.User.ProcessFramework.Map.Process(2).ExtendedPropertyValueAdd(_extprop, "sasaza");
        }
        //private DataTable ObtenerDataTableParaProbarSPTableValue()
        //{
        //    DataTable _dtTVP = new DataTable();
        //    _dtTVP.TableName = "tvp";
        //    DataColumn _column = new DataColumn();
        //    _column.DataType = System.Type.GetType("System.DateTime");
        //    _column.Caption = "MeasurementDate";
        //    //Inserta la columna definida en el DAtaTable.
        //    _dtTVP.Columns.Add(_column);

        //    _column = new DataColumn();
        //    _column.DataType = System.Type.GetType("System.Decimal");
        //    _column.Caption = "MeasurementValue";
        //    //Inserta la columna definida en el DAtaTable.
        //    _dtTVP.Columns.Add(_column);

        //    DateTime _dateTime = new DateTime(2007, 1, 1, 0, 0, 0);
        //    Random _random = new Random(8);
        //    for (int i = 0; i < 17000; i++)
        //    {
        //        //Quiero un registro por hora.
        //        _dateTime = _dateTime.AddHours(1);
        //        Decimal _value = 12;
        //        _value = (_value * _random.Next()) / 10000;
        //        _dtTVP.Rows.Add(_dateTime, _value);
        //    }

        //    return _dtTVP;
        //}


        //private void EjecutarTareaMedicion()
        //{
        //    //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
        //    CultureInfo _cultureUSA = new CultureInfo("en-US");
        //    //Me guarda la actual, para luego volver a esta...
        //    CultureInfo _currentCulture = CultureInfo.CurrentCulture;
        //    //Seta la cultura estandard
        //    Thread.CurrentThread.CurrentCulture = _cultureUSA;

        //    FileStream _fileStream = new FileStream(@"C:\Users\ruben.short\Documents\Trabajo\monitoreo\Landfill\1trim 2007.txt", FileMode.Open);

        //    //String _fileNameMeasurement = fileUploadMeasurement.FileName;
        //    //_fileStream = fileUploadMeasurement.FileContent;

        //    //Obtiene el texto
        //    StreamReader _streamReaderFileMeasurement = new StreamReader(_fileStream);
        //    String _fileMeasurement = _streamReaderFileMeasurement.ReadToEnd();

        //    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //    Byte[] _fileMeasurementBinary = encoding.GetBytes(_fileMeasurement);

        //    Int64 _idFile = new long();
        //    Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice = null;
        //    Condesus.EMS.Business.PA.Entities.MeasurementUnit _measurementUnit = null;
        //    if (_EMS.User.ProcessFramework.Map.Process(441).GetType().Name == "ProcessTaskDataRecovery")
        //    {
        //        _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)_EMS.User.ProcessFramework.Map.Process(441)).ProcessTaskMeasurementToRecovery.Measurement.Device;
        //        _measurementUnit = ((Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)_EMS.User.ProcessFramework.Map.Process(441)).ProcessTaskMeasurementToRecovery.Measurement.MeasurementUnit;
        //    }

        //    else
        //    {
        //        _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_EMS.User.ProcessFramework.Map.Process(441)).Measurement.Device;

        //        _measurementUnit = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_EMS.User.ProcessFramework.Map.Process(441)).Measurement.MeasurementUnit;
        //    }
        //    Condesus.EMS.Business.PF.Entities.ProcessTaskExecution _Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTask)_EMS.User.ProcessFramework.Map.Process(441)).ProcessTaskExecution(303039);
        //    decimal _measurementValue = new decimal();
        //    DateTime _measurementDate = new DateTime();
        //    try
        //    {
        //        ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_EMS.User.ProcessFramework.Map.Process(441)).ProcessTaskExecutionsAdd(((Condesus.EMS.Business.DS.Entities.PersonwithUser)_EMS.User.Person).Posts.First(), _Entity, ref _idFile, "prueba add file", _fileMeasurement, _fileMeasurementBinary, "nada", ref _measurementValue, ref _measurementDate, _measurementDevice, _measurementUnit, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //    //Vuelve a la cultura original...
        //    Thread.CurrentThread.CurrentCulture = _currentCulture;

        //}
        #endregion

    }
}
