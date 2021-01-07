using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;

namespace TestBusiness
{
    public partial class Form1 : Form
    {
        public Condesus.EMS.Business.EMS _EMS;

        public Form1()
        {
            InitializeComponent();
            
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //crea ems
        //    _EMS = new Condesus.EMS.Business.EMS("ems", "ems", "es-AR", "127.0.0.1");
        //    //Condesus.EMS.Business.EMS _EMS = new Condesus.EMS.Business.EMS("famar", "demo", "es-AR", "127.0.0.1");
        //    //Condesus.EMS.Business.EMS _EMS = new Condesus.EMS.Business.EMS("proeu13", "demo", "es-AR", "127.0.0.1");
        //    //si necesito crea una organizacion            
        //    //Condesus.EMS.Business.DS.Entities.Organization _Organization = _EMS.User.DirectoryServices.Map.Organization(1);


        //    //Llamada a ejecucion de tareas
        //    posts();
        //    //Notificaciones();



        //    String espero_aca = "";
        //}


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


//        public string ExcelColumnLetterFull(int intCol)
//        {
//            string _colum = "";

//            if (intCol < 26)
//            {
//                int _numcol = intCol + 65;
//                _colum = Char.ConvertFromUtf32(_numcol);

//            }
//            else
//            {
//                int _numcol = intCol - 26;
//                _colum = ExcelColumnLetter(_numcol);
//            }
//            return _colum;
//        }

//     public string ExcelColumnLetter(int intCol)
// {
 
//    if(intCol > 16384){throw new Exception("Index exceeds maximum columns allowed.");}
//    string strColumn;
//     char letter1, letter2, FirstLetter;
//     int InitialLetter = ((intCol) / 676);
//     int intFirstLetter = ((intCol % 676) / 26);
//     int intSecondLetter = (intCol % 26);
//     InitialLetter = InitialLetter + 64;
//     intFirstLetter = intFirstLetter + 65;
//     intSecondLetter = intSecondLetter + 65;
 
//if (InitialLetter > 64)
// {
// FirstLetter = (char)InitialLetter;
// }
// else
// {
// FirstLetter = ' ';
// }
 
//if (intFirstLetter > 64)
// {
// letter1 = (char)intFirstLetter;
// }
// else
// {
// letter1 = ' ';
// }
//letter2 = (char)intSecondLetter;
// strColumn = FirstLetter + string.Concat(letter1, letter2);
// return strColumn.Trim();
//     }


        private void buttonDamian_Click(object sender, EventArgs e)
        {

           
          

            //pruebas sin loguearse

            //Microsoft.Office.Interop.Excel.Application xlApp;
            //Microsoft.Office.Interop.Excel.Workbook xlLibro;
            //Microsoft.Office.Interop.Excel.Worksheet xlHoja1;
            //Microsoft.Office.Interop.Excel.Sheets xlHojas;
                        
            ////inicializo la variable xlApp (referente a la aplicación)
            //xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            ////Muestra la aplicación Excel si está en true
            //xlApp.Visible = false;
            ////Abrimos el libro a leer (documento excel)
            //xlLibro = xlApp.Workbooks.Open(@"C:\Users\damian.bonanno\Desktop\" + "arg.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
           
            //    //Asignamos las hojas
            //    xlHojas = xlLibro.Sheets;
            //    //Asignamos la hoja con la que queremos trabajar: 
            //    xlHoja1 = (Microsoft.Office.Interop.Excel.Worksheet)xlHojas.get_Item(1);
            //    //recorremos las celdas que queremos y sacamos los datos 10 es el número de filas que queremos que lea
            
            //        String _colum = "F";
            //        Int16 _row = 3;
            //        String _date = "1";

            //        char _letra = Convert.ToChar(_colum);
            //        //int _unicode = (int)_letra;// _colum;
            //        //char letra = (char)_unicode;

            //        int _starti = (int)_letra - 65;
            //            StringBuilder _archivo = new StringBuilder();
            //            //_row = Convert.ToInt16(_configExcelFile.IndexMesurement);

            //            for (int i = _starti; i <= xlHoja1.UsedRange.Columns.Count; i++)
            //            {
            //                String _cellDate = ExcelColumnLetterFull(i) + _date;
            //                string fecha = (string)xlHoja1.get_Range(_cellDate, _cellDate).Text;

            //                String _cell = ExcelColumnLetterFull(i) + _row.ToString();
            //                string med = (string)xlHoja1.get_Range(_cell, _cell).Text;

            //                //string med = (string)xlHoja1.get_Range(_row + xlHoja1.UsedRange.Columns.Next, _row + Char.ConvertFromUtf32(i)).Text;

            //                if (med != String.Empty)
            //                {
            //                    _archivo.Append(fecha + ";" + med + "\r");

            //                }
            //                else
            //                {
            //                    Byte[] _fileStreamBinary = System.Text.Encoding.Unicode.GetBytes(_archivo.ToString());
            //                    //hacer el add de la ejecucion
            //                    break;
            //                }
            //            }
                  

            //string _DB = System.Configuration.ConfigurationManager.ConnectionStrings["EMS"].ToString();

            //String _database = "EMS";
            //String _conectionString = "MultipleActiveResultSets=True;Database=" + _database + @";Server=192.168.1.2\SQLwebpublished;User ID=sa;Password=6719BDev;Min Pool Size=2;Max Pool Size=40;Connect Timeout=3600;" + "providerName=" + "System.Data.SqlClient";
            ////System.Configuration.Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            //System.Configuration.Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoaming);
            //System.Configuration.ConnectionStringsSection css = conf.ConnectionStrings;
            //css.ConnectionStrings["EMS"].ConnectionString = _conectionString;
            //conf.Save();
            //System.Configuration.ConfigurationManager.RefreshSection("EMS");

            //string _DB1 = System.Configuration.ConfigurationManager.ConnectionStrings["EMS"].ToString();

            //string _DB2 = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"].ToString();

            //System.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            //config.AppSettings.Settings["miParametro"].Value = "Mi valor";
            
            //config.Save(ConfigurationSaveMode.Modified);
            //Condesus.EMS.Business.EMS _EMS;
            //_EMS = new Condesus.EMS.Business.EMS("admin", "admin", "en-US", "127.0.0.1");

            //foreach (Condesus.EMS.Business.NT.Entities.NotificationMessage _noti in _EMS.EMSServices.Notifications.NotificationMessages)
            //{
            //    label1.Text = _noti.Body;
            //    //_noti.NotificationReported.ChangeStatusNotification(_noti.IdError);
            //}

           

            PruebasDamian _PruebasDamian = new PruebasDamian();
            _PruebasDamian.Ejecutar(this.DP_startdate.Value, this.DP_enddate.Value);

            
        }

        private void buttonFacundo_Click(object sender, EventArgs e)
        {
            PruebasFacu _PruebasFacu = new PruebasFacu();
            _PruebasFacu.Ejecutar();
        }

       
    }
}
