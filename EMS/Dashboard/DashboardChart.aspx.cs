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
using Dundas.Charting.WebControl;
using System.Data.OleDb;

using System.Collections.Generic;
using System.Web.Script.Serialization;

using Telerik.Web.UI;
//using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class DashboardChart : BasePage
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //TitleIconURL
            FwMasterPage.PageTitleIconURL = "/Skins/Images/Icons/Dashboard.png";
        }
        //Setea el Titulo de la Pagina
        protected override void SetPagetitle()
        {
            base.PageTitle = Resources.Common.Dashboard;
        }
        //Setea el Sub Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = "Statistics C02";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            // Cargo los greficos
            chart1();
            chart2();
            chart3();
            chart4();
        }
        #region Web Form Designer generated code

        
        // Armo los 4 chart
        
        void chart1()
        {

            // resolve the address to the Excel file
            string fileNameString = this.MapPath(".");
            fileNameString += "/DataChart/Chart1.xls";

            // Create connection object by using the preceding connection string.
            string sConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                fileNameString + ";Extended Properties=\"Excel 8.0;HDR=YES\"";
            OleDbConnection myConnection = new OleDbConnection(sConn);
            myConnection.Open();

            // The code to follow uses a SQL SELECT command to display the data from the worksheet.
            // Create new OleDbCommand to return data from worksheet.
            OleDbCommand myCommand = new OleDbCommand("Select * From [data1$A1:C4]", myConnection);

            // create a database reader    
            OleDbDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Populate the chart with data in the file
            Chart1.DataBindTable(myReader);
            // Set series chart type

            // close the reader and the connection
            myReader.Close();
            myConnection.Close();

            foreach (Series ser in Chart1.Series)
            {
                ser.ShadowOffset = 1;
                ser.BorderWidth = 3;
                ser.Type = SeriesChartType.Bar;
                ser.ShowLabelAsValue = true;
            }
            
        }

        void chart2()
        {

            // resolve the address to the Excel file
            string fileNameString = this.MapPath(".");
            fileNameString += "/DataChart/Chart2.xls";

            // Create connection object by using the preceding connection string.
            string sConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                fileNameString + ";Extended Properties=\"Excel 8.0;HDR=YES\"";
            OleDbConnection myConnection = new OleDbConnection(sConn);
            myConnection.Open();

            // The code to follow uses a SQL SELECT command to display the data from the worksheet.
            // Create new OleDbCommand to return data from worksheet.
            OleDbCommand myCommand = new OleDbCommand("Select * From [data1$A1:C4]", myConnection);

            // create a database reader    
            OleDbDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Populate the chart with data in the file
            Chart2.DataBindTable(myReader, "Prueba");
            // Set series chart type

            // close the reader and the connection
            myReader.Close();
            myConnection.Close();

            foreach (Series ser in Chart2.Series)
            {
                ser.ShadowOffset = 1;
                ser.BorderWidth = 3;
                ser.Type = SeriesChartType.Bar;
                ser.ShowLabelAsValue = true;
            }

        }

        void chart3()
        {

            // resolve the address to the Excel file
            string fileNameString = this.MapPath(".");
            fileNameString += "/DataChart/Chart3.xls";

            // Create connection object by using the preceding connection string.
            string sConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                fileNameString + ";Extended Properties=\"Excel 8.0;HDR=YES\"";
            OleDbConnection myConnection = new OleDbConnection(sConn);
            myConnection.Open();

            // The code to follow uses a SQL SELECT command to display the data from the worksheet.
            // Create new OleDbCommand to return data from worksheet.
            OleDbCommand myCommand = new OleDbCommand("Select * From [data1$A1:C4]", myConnection);

            // create a database reader    
            OleDbDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Populate the chart with data in the file
            Chart3.DataBindTable(myReader, "Title");
            // Set series chart type

            // close the reader and the connection
            myReader.Close();
            myConnection.Close();

            foreach (Series ser in Chart3.Series)
            {
                ser.ShadowOffset = 1;
                ser.BorderWidth = 3;
                ser.Type = SeriesChartType.Pie;                
                ser.ShowLabelAsValue = true;
                //ser.LegendText = "Prueba";
            
            }
        }

        void chart4()
        {

            // resolve the address to the Excel file
            string fileNameString = this.MapPath(".");
            fileNameString += "/DataChart/Chart4.xls";

            // Create connection object by using the preceding connection string.
            string sConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                fileNameString + ";Extended Properties=\"Excel 8.0;HDR=YES\"";
            OleDbConnection myConnection = new OleDbConnection(sConn);
            myConnection.Open();

            // The code to follow uses a SQL SELECT command to display the data from the worksheet.
            // Create new OleDbCommand to return data from worksheet.
            OleDbCommand myCommand = new OleDbCommand("Select * From [data1$A1:C4]", myConnection);

            // create a database reader    
            OleDbDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Populate the chart with data in the file
            Chart4.DataBindTable(myReader);
            // Set series chart type

            // close the reader and the connection
            myReader.Close();
            myConnection.Close();

            foreach (Series ser in Chart4.Series)
            {
                ser.ShadowOffset = 1;
                ser.BorderWidth = 3;
                ser.Type = SeriesChartType.StackedColumn;
                ser.ShowLabelAsValue = true;
            }

        }


        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboardOpen";

        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
