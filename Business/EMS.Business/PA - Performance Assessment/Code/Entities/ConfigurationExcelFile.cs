using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class ConfigurationExcelFile
    {
        #region Internal Region
        private Int64 _IdExcelFile;
        private String _Name;
        private String _StartIndexOfDataRows;
        private String _StartIndexOfDataCols;
        private Boolean _IsDataRows;
        private String _IndexStartDate;
        private String _IndexEndDate;
        private Credential _Credential;

        #endregion

        #region External Region
        public Int64 IdExcelFile
        {
            get { return _IdExcelFile; }
        }
        public String Name
        {
            get { return _Name; }
        }
        public String StartIndexOfDataRows
        {
            get { return _StartIndexOfDataRows; }
        }
        public String StartIndexOfDataCols
        {
            get { return _StartIndexOfDataCols; }
        }
        public Boolean ValuesInRows
        {
            get { return _IsDataRows; }
        }
        public String IndexStartDate
        {
            get { return _IndexStartDate; }
        }
        public String IndexEndDate
        {
            get { return _IndexEndDate; }
        }
        public Dictionary<String, ConfigurationAsociationMeasurementExcelFile> Measurements
        {
            get
            {
                Dictionary<String, ConfigurationAsociationMeasurementExcelFile> _oItems = new Dictionary<String, ConfigurationAsociationMeasurementExcelFile>();

                return new Collections.ConfigurationExcelFiles(_Credential).Items(this);
            }
        }
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
                if (ValuesInRows) //recorre por fila
                {
                    DataTable _dtValues = BuildDataTableValueParamMeasurements();
                    String _colum = StartIndexOfDataCols.Trim();
                    Int16 _rowDate = Convert.ToInt16(StartIndexOfDataRows);
                    Int16 _rowValue = Convert.ToInt16(StartIndexOfDataRows);
                    String _startDate = IndexStartDate.Trim();
                    String _endDate = IndexEndDate.Trim();

                    char _letra = Convert.ToChar(_colum);

                    int _starti = (int)_letra - 65;

                    foreach (ConfigurationAsociationMeasurementExcelFile _configExcelFile in Measurements.Values)
                    {
                        _rowDate = Convert.ToInt16(_configExcelFile.IndexDate);
                        _rowValue = Convert.ToInt16(_configExcelFile.IndexValue);

                        for (int i = _starti; i <= xlHoja1.UsedRange.Columns.Count; i++)
                        {
                            String _cellStartDate = ExcelColumnLetterFull(i) + _startDate;
                            string startdate = (string)xlHoja1.get_Range(_cellStartDate, _cellStartDate).Text;

                            String _cellEndDate = ExcelColumnLetterFull(i) + _endDate;
                            string enddate = (string)xlHoja1.get_Range(_cellEndDate, _cellEndDate).Text;

                            String _cellDate = ExcelColumnLetterFull(i) + _rowDate;
                            string date = (string)xlHoja1.get_Range(_cellDate, _cellDate).Text;

                            String _cell = ExcelColumnLetterFull(i) + _rowValue.ToString();
                            string value = (string)xlHoja1.get_Range(_cell, _cell).Text;

                            if (!String.IsNullOrEmpty(value))
                            {
                                _dtValues.Rows.Add(date, value, startdate, enddate);

                            }
                            if (_dtValues.Rows.Count > 0)
                            {
                                Byte[] _attachment = null;
                                Entities.MeasurementDevice _measurementDevice = null;
                                //hacer el add de la ejecucion
                                Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
                                ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAddTVP(
                                    ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(),
                                    "Carga desde el archivo exel",
                                    _attachment,
                                    _dtValues,
                                    _measurementDevice,
                                    _medicion.MeasurementUnit,
                                    false,
                                    chargeNotice);
                            }              
                        }
                    }
                }
                else //recorre por columna
                {
                    //Ruben, modifico aca, porque el DataTable, debe limpiarse en cada ciclo de la medicion...
                    DataTable _dtValues = new DataTable();  // BuildDataTableValueParamMeasurements();
                    String _columDate = StartIndexOfDataCols.Trim();
                    String _columValue = StartIndexOfDataCols.Trim();
                    Int16 _row = Convert.ToInt16(StartIndexOfDataRows);
                    String _startDate = IndexStartDate.Trim();
                    String _endDate = IndexEndDate.Trim();

                    foreach (ConfigurationAsociationMeasurementExcelFile _configExcelFile in Measurements.Values)
                    {
                        _dtValues = BuildDataTableValueParamMeasurements();
                        //StringBuilder _archivo = new StringBuilder();
                        _columDate = _configExcelFile.IndexDate.Trim();
                        _columValue = _configExcelFile.IndexValue.Trim();
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
                            Byte[] _attachment = null;
                            Entities.MeasurementDevice _measurementDevice = null;
                            //hacer el add de la ejecucion
                            Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
                            ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAddTVP(
                                ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(), 
                                "Carga desde el archivo exel", 
                                _attachment, 
                                _dtValues, 
                                _measurementDevice, 
                                _medicion.MeasurementUnit, 
                                false, 
                                chargeNotice);
                        }                   
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "System.Runtime.InteropServices.ExternalException")
                {
                    if (((System.Runtime.InteropServices.ExternalException)(ex)).ErrorCode == -2146827284)
                    {
                        throw new Exception(Common.Resources.Errors.ConfigurationExcelFileErrorConfig + " - " + ex.Message);
                    }
                }
                else
                {
                    throw new Exception(ex.Message);
                }
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
            _column.Caption = "MeasurementDate";
            _column.ColumnName = "MeasurementDate";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.Decimal");
            _column.Caption = "MeasurementValue";
            _column.ColumnName = "MeasurementValue";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.DateTime");
            _column.Caption = "StartDate";
            _column.ColumnName = "StartDate";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.DateTime");
            _column.Caption = "EndDate";
            _column.ColumnName = "EndDate";
            //Inserta la columna definida en el DAtaTable.
            _dtTVPMeasurements.Columns.Add(_column);

            return _dtTVPMeasurements;
        }
        #region Private metodhs

        public string ExcelColumnLetterFull(int intCol)
        {
            string _colum = "";

            if (intCol < 26)
            {
                int _numcol = intCol + 65;
                _colum = Char.ConvertFromUtf32(_numcol);

            }
            else
            {
                int _numcol = intCol - 26;
                _colum = ExcelColumnLetter(_numcol);
            }
            return _colum;
        }

        public string ExcelColumnLetter(int intCol)
        {

            if (intCol > 16384) { throw new Exception("Index exceeds maximum columns allowed."); }
            string strColumn;
            char letter1, letter2, FirstLetter;
            int InitialLetter = ((intCol) / 676);
            int intFirstLetter = ((intCol % 676) / 26);
            int intSecondLetter = (intCol % 26);
            InitialLetter = InitialLetter + 64;
            intFirstLetter = intFirstLetter + 65;
            intSecondLetter = intSecondLetter + 65;

            if (InitialLetter > 64)
            {
                FirstLetter = (char)InitialLetter;
            }
            else
            {
                FirstLetter = ' ';
            }

            if (intFirstLetter > 64)
            {
                letter1 = (char)intFirstLetter;
            }
            else
            {
                letter1 = ' ';
            }
            letter2 = (char)intSecondLetter;
            strColumn = FirstLetter + string.Concat(letter1, letter2);
            return strColumn.Trim();
        }

        #endregion

        #endregion

        internal ConfigurationExcelFile(Int64 IdExcelFile, String name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows,
            String IndexStartDate, String IndexEndDate, Credential credential)
        {
            _IdExcelFile = IdExcelFile;
            _Name = name;
            _StartIndexOfDataCols = StartIndexOfDataCols;
            _StartIndexOfDataRows = StartIndexOfDataRows;
            _IsDataRows = IsDataRows;
            _IndexStartDate = IndexStartDate;
            _IndexEndDate = IndexEndDate;
            _Credential = credential;
        }

        public void Modify(String name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows,
            String IndexStartDate, String IndexEndDate, Dictionary<String, Entities.ConfigurationAsociationMeasurementExcelFile> measurements)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.ConfigurationExcelFiles(_Credential).Modify(this, name, StartIndexOfDataRows, StartIndexOfDataCols, IsDataRows, IndexStartDate, IndexEndDate, measurements);
                _TransactionScope.Complete();
            }
        }
    }
}

//public void ExecuteAttach(String path, String fileName, Boolean chargeNotice)
//        {
//            //Declaro las variables necesarias
//            Microsoft.Office.Interop.Excel.Application xlApp;
//            Microsoft.Office.Interop.Excel.Workbook xlLibro;
//            Microsoft.Office.Interop.Excel.Worksheet xlHoja1;
//            Microsoft.Office.Interop.Excel.Sheets xlHojas;
                        
//            //inicializo la variable xlApp (referente a la aplicación)
//            xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
//            //Muestra la aplicación Excel si está en true
//            xlApp.Visible = false;

//            //path = path.Replace("\\", @"\");

//            //Abrimos el libro a leer (documento excel)
//            xlLibro = xlApp.Workbooks.Open(path + fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
//            try
//            {
//                //Asignamos las hojas
//                xlHojas = xlLibro.Sheets;
//                //Asignamos la hoja con la que queremos trabajar: 
//                xlHoja1 = (Microsoft.Office.Interop.Excel.Worksheet)xlHojas.get_Item(1);
//                //recorremos las celdas que queremos y sacamos los datos 10 es el número de filas que queremos que lea
//                if (ValuesInRows) //recorre por fila
//                {
//                    DataTable _dtValues = BuildDataTableValueParamMeasurements();
//                    String _colum = StartIndexOfDataCols.Trim();
//                    Int16 _rowDate = Convert.ToInt16(StartIndexOfDataRows);
//                    Int16 _rowValue = Convert.ToInt16(StartIndexOfDataRows);
//                    String _startDate = IndexStartDate.Trim();
//                    String _endDate = IndexEndDate.Trim();

//                    char _letra = Convert.ToChar(_colum);

//                    int _starti = (int)_letra - 65;

//                    foreach (ConfigurationAsociationMeasurementExcelFile _configExcelFile in Measurements.Values)
//                    {
//                        _rowDate = Convert.ToInt16(_configExcelFile.IndexDate);
//                        _rowValue = Convert.ToInt16(_configExcelFile.IndexValue);

//                        for (int i = _starti; i <= xlHoja1.UsedRange.Columns.Count; i++)
//                        {
//                            String _cellStartDate = ExcelColumnLetterFull(i) + _startDate;
//                            string startdate = (string)xlHoja1.get_Range(_cellStartDate, _cellStartDate).Text;

//                            String _cellEndDate = ExcelColumnLetterFull(i) + _endDate;
//                            string enddate = (string)xlHoja1.get_Range(_cellEndDate, _cellEndDate).Text;

//                            String _cellDate = ExcelColumnLetterFull(i) + _rowDate;
//                            string date = (string)xlHoja1.get_Range(_cellDate, _cellDate).Text;

//                            String _cell = ExcelColumnLetterFull(i) + _rowValue.ToString();
//                            string value = (string)xlHoja1.get_Range(_cell, _cell).Text;

//                            if (!String.IsNullOrEmpty(value))
//                            {
//                                _dtValues.Rows.Add(date, value, startdate, enddate);

//                            }
//                            if (_dtValues.Rows.Count > 0)
//                            {
//                                Byte[] _attachment = null;
//                                Entities.MeasurementDevice _measurementDevice = null;
//                                //hacer el add de la ejecucion
//                                Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
//                                ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAddTVP(
//                                    ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(),
//                                    "Carga desde el archivo exel",
//                                    _attachment,
//                                    _dtValues,
//                                    _measurementDevice,
//                                    _medicion.MeasurementUnit,
//                                    false,
//                                    chargeNotice);
//                            }
//                            //else
//                            //{
//                            //    Byte[] _fileStreamBinary = System.Text.Encoding.Unicode.GetBytes(_archivo.ToString());
//                            //    Byte[] _attachment = null;
//                            //    //hacer el add de la ejecucion
//                            //    Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
//                            //    ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAdd(
//                            //        ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(), 
//                            //        fileName, 
//                            //        _archivo.ToString(), 
//                            //        _fileStreamBinary, 
//                            //        "Carga desde el archivo exel, nombre: " + fileName + " fecha: " + DateTime.Now, 
//                            //        _attachment,
//                            //        _medicion.MeasurementUnit, 
//                            //        false, chargeNotice);
//                            //    break;
//                            //}
//                        }

//                        //Una vez finalizada la recorrida del
//                        //if (_archivo.Length > 0)
//                        //{
//                        //    Byte[] _fileStreamBinary = System.Text.Encoding.Unicode.GetBytes(_archivo.ToString());
//                        //    Byte[] _attachment = null;
//                        //    //hacer el add de la ejecucion
//                        //    Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
//                        //    ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAdd(
//                        //        ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(),
//                        //        fileName,
//                        //        _archivo.ToString(),
//                        //        _fileStreamBinary,
//                        //        "Carga desde el archivo exel, nombre: " + fileName + " fecha: " + DateTime.Now,
//                        //        _attachment,
//                        //        _medicion.MeasurementUnit,
//                        //        false, chargeNotice);
//                        //}
//                    }
//                }
//                else //recorre por columna
//                {
//                    DataTable _dtValues = BuildDataTableValueParamMeasurements();
//                    String _columDate = StartIndexOfDataCols.Trim();
//                    String _columValue = StartIndexOfDataCols.Trim();
//                    Int16 _row = Convert.ToInt16(StartIndexOfDataRows);
//                    String _startDate = IndexStartDate.Trim();
//                    String _endDate = IndexEndDate.Trim();

//                    foreach (ConfigurationAsociationMeasurementExcelFile _configExcelFile in Measurements.Values)
//                    {
//                        //StringBuilder _archivo = new StringBuilder();
//                        _columDate = _configExcelFile.IndexDate.Trim();
//                        _columValue = _configExcelFile.IndexValue.Trim();
//                        for (int i = _row; i <= xlHoja1.UsedRange.Rows.Count; i++)
//                        {
//                            string startdate = (string)xlHoja1.get_Range(_startDate + i, _startDate + i).Text;
//                            string enddate = (string)xlHoja1.get_Range(_endDate + i, _endDate + i).Text;
//                            string date = (string)xlHoja1.get_Range(_columDate + i, _columDate + i).Text;
//                            string value = (string)xlHoja1.get_Range(_columValue + i, _columValue + i).Text;

//                            if (!String.IsNullOrEmpty(value))
//                            {
//                                _dtValues.Rows.Add(date, value, startdate, enddate);

//                            }
//                            //else
//                            //{
//                            //    Byte[] _fileStreamBinary = System.Text.Encoding.Unicode.GetBytes(_archivo.ToString());
//                            //    Byte[] _attachment = null;
//                            //    //hacer el add de la ejecucion
//                            //    Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
//                            //    ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAdd(
//                            //        ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(), fileName, _archivo.ToString(), _fileStreamBinary, "Carga desde el archivo exel, nombre: " + fileName + " fecha: " + DateTime.Now, _attachment, _medicion.MeasurementUnit, false, chargeNotice);
//                            //    break;
//                            //}
//                        }
//                        //Una vez finalizada la recorrida del
//                        if (_dtValues.Rows.Count > 0)
//                        {
//                            Byte[] _attachment = null;
//                            Entities.MeasurementDevice _measurementDevice = null;
//                            //hacer el add de la ejecucion
//                            Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
//                            ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAddTVP(
//                                ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(), 
//                                "Carga desde el archivo exel", 
//                                _attachment, 
//                                _dtValues, 
//                                _measurementDevice, 
//                                _medicion.MeasurementUnit, 
//                                false, 
//                                chargeNotice);
//                        }
//                        //if (_archivo.Length > 0)
//                        //{
//                        //    Byte[] _fileStreamBinary = System.Text.Encoding.Unicode.GetBytes(_archivo.ToString());
//                        //    Byte[] _attachment = null;
//                        //    //hacer el add de la ejecucion
//                        //    Condesus.EMS.Business.PA.Entities.Measurement _medicion = _configExcelFile.Measurement;
//                        //    ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_medicion.ProcessTask).ProcessTaskExecutionsAdd(
//                        //        ((DS.Entities.PersonwithUser)_Credential.User.Person).Posts.First(), fileName, _archivo.ToString(), _fileStreamBinary, "Carga desde el archivo exel, nombre: " + fileName + " fecha: " + DateTime.Now, _attachment, _medicion.MeasurementUnit, false, chargeNotice);
//                        //}

//                    }
//                }

//            }
//            catch (Exception ex)
//            {
//                if (ex.GetType().Name == "System.Runtime.InteropServices.ExternalException")
//                {
//                    if (((System.Runtime.InteropServices.ExternalException)(ex)).ErrorCode == -2146827284)
//                    {
//                        throw new Exception(Common.Resources.Errors.ConfigurationExcelFileErrorConfig + " - " + ex.Message);
//                    }
//                }
//                else
//                {
//                    throw new Exception(ex.Message);
//                }
//            }
//            finally
//            {
//                //Cerrar el Libro
//                xlLibro.Close(false, null, null);
//                //Cerrar la Aplicación
//                xlApp.Quit();
//            }
//        }