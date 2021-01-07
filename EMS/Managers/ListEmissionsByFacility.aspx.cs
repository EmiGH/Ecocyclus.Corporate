using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Condesus.WebUI.Navigation;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.Managers
{
    public partial class ListEmissionsByFacility : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdProcess;
            private DateTime? _StartDate;
            private DateTime? _EndDate;

            private String _EntityName = String.Empty;
            private String _EntityNameGRC = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            private String _EntityNameMapClassification = String.Empty;
            private String _EntityNameMapClassificationChildren = String.Empty;
            private String _EntityNameMapElement = String.Empty;
            private String _EntityNameMapElementChildren = String.Empty;
        #endregion

        #region PageLoad & Init
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                LoadAllParameter();

                if (!Page.IsPostBack)
                {
                    //Arma la grilla completa
                    LoadTable();
                }
            }
            protected override void SetPagetitle()
            {
                try
                {
                    //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                    base.PageTitle = Resources.CommonMenu.mnuContextInfoEmissionsByFacility;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleList;
            }
        #endregion

        #region Private Method
            private void LoadListManage()
            {
                //rgdListEmissions.ExportSettings.ExportOnlyData = true;
                //rgdListEmissions.ExportSettings.IgnorePaging = true;
                //rgdListEmissions.ExportSettings.OpenInNewWindow = true;

                BuildGenericDataTable("EmissionByFacility", ManageEntityParams);
                //Si esta el DataTable lo carga en la grilla
                if (DataTableListManage.ContainsKey("EmissionByFacility"))
                {
                    //rgdListEmissions.DataSource = DataTableListManage["EmissionByFacility"];
                    //rgdListEmissions.Rebind();
                }
            }
            private void LoadAllParameter()
            {
                //Carga el DataTable.
                Int64 _idIndicator_tnCO2e;
                Int64 _idIndicator_CO2;
                Int64 _idIndicator_CH4;
                Int64 _idIndicator_N2O;
                Int64 _idIndicator_PFC;
                Int64 _idIndicator_HFC;
                Int64 _idIndicator_SF6;

                Int64 _idIndicator_HCNM;
                Int64 _idIndicator_HCT;
                Int64 _idIndicator_CO;
                Int64 _idIndicator_NOx;
                Int64 _idIndicator_SOx;
                Int64 _idIndicator_MP;
                Int64 _idIndicator_SO2;
                Int64 _idIndicator_H2S;
                Int64 _idIndicator_MP10;
                Int64 _idIndicator_C2H6;
                Int64 _idIndicator_C3H8;
                Int64 _idIndicator_C4H10;

                String _states;

                #region Parameter QueryString
                if (Request.QueryString["IdProcess"] != null)
                {
                    _IdProcess = Convert.ToInt64(Request.QueryString["IdProcess"]);
                }
                if (Request.QueryString["StartDate"] != null)
                {
                    if (Request.QueryString["StartDate"] != "")
                    {
                        _StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
                    }
                }
                if (Request.QueryString["EndDate"] != null)
                {
                    if (Request.QueryString["StartDate"] != "")
                    {
                        _EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
                    }
                }
                #endregion

                #region Indicator GAS Config
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_tnCO2e"], out _idIndicator_tnCO2e);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CO2"], out _idIndicator_CO2);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CH4"], out _idIndicator_CH4);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_N2O"], out _idIndicator_N2O);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PFC"], out _idIndicator_PFC);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HFC"], out _idIndicator_HFC);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SF6"], out _idIndicator_SF6);

                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HCNM"], out _idIndicator_HCNM);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HCT"], out _idIndicator_HCT);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CO"], out _idIndicator_CO);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_NOx"], out _idIndicator_NOx);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SOx"], out _idIndicator_SOx);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PM"], out _idIndicator_MP);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SO2"], out _idIndicator_SO2);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_H2S"], out _idIndicator_H2S);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PM10"], out _idIndicator_MP10);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C2H6"], out _idIndicator_C2H6);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C3H8"], out _idIndicator_C3H8);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C4H10"], out _idIndicator_C4H10);

                #endregion

                #region State of Geographic Area Config
                _states = ConfigurationManager.AppSettings["States"].ToString();
                #endregion

                #region Add Manage Entity Params
                if (ManageEntityParams.ContainsKey("States"))
                { ManageEntityParams.Remove("States"); }
                ManageEntityParams.Add("States", _states);

                if (ManageEntityParams.ContainsKey("IdProcess"))
                { ManageEntityParams.Remove("IdProcess"); }
                ManageEntityParams.Add("IdProcess", _IdProcess);

                if (ManageEntityParams.ContainsKey("IdIndicator_tnCO2e"))
                { ManageEntityParams.Remove("IdIndicator_tnCO2e"); }
                ManageEntityParams.Add("IdIndicator_tnCO2e", _idIndicator_tnCO2e);

                if (ManageEntityParams.ContainsKey("IdIndicator_CO2"))
                { ManageEntityParams.Remove("IdIndicator_CO2"); }
                ManageEntityParams.Add("IdIndicator_CO2", _idIndicator_CO2);

                if (ManageEntityParams.ContainsKey("IdIndicator_CH4"))
                { ManageEntityParams.Remove("IdIndicator_CH4"); }
                ManageEntityParams.Add("IdIndicator_CH4", _idIndicator_CH4);

                if (ManageEntityParams.ContainsKey("IdIndicator_N2O"))
                { ManageEntityParams.Remove("IdIndicator_N2O"); }
                ManageEntityParams.Add("IdIndicator_N2O", _idIndicator_N2O);

                if (ManageEntityParams.ContainsKey("IdIndicator_PFC"))
                { ManageEntityParams.Remove("IdIndicator_PFC"); }
                ManageEntityParams.Add("IdIndicator_PFC", _idIndicator_PFC);

                if (ManageEntityParams.ContainsKey("IdIndicator_HFC"))
                { ManageEntityParams.Remove("IdIndicator_HFC"); }
                ManageEntityParams.Add("IdIndicator_HFC", _idIndicator_HFC);

                if (ManageEntityParams.ContainsKey("IdIndicator_SF6"))
                { ManageEntityParams.Remove("IdIndicator_SF6"); }
                ManageEntityParams.Add("IdIndicator_SF6", _idIndicator_SF6);

                if (ManageEntityParams.ContainsKey("IdIndicator_HCNM"))
                { ManageEntityParams.Remove("IdIndicator_HCNM"); }
                ManageEntityParams.Add("IdIndicator_HCNM", _idIndicator_HCNM);

                if (ManageEntityParams.ContainsKey("IdIndicator_HCT"))
                { ManageEntityParams.Remove("IdIndicator_HCT"); }
                ManageEntityParams.Add("IdIndicator_HCT", _idIndicator_HCT);

                if (ManageEntityParams.ContainsKey("IdIndicator_CO"))
                { ManageEntityParams.Remove("IdIndicator_CO"); }
                ManageEntityParams.Add("IdIndicator_CO", _idIndicator_CO);

                if (ManageEntityParams.ContainsKey("IdIndicator_NOx"))
                { ManageEntityParams.Remove("IdIndicator_NOx"); }
                ManageEntityParams.Add("IdIndicator_NOx", _idIndicator_NOx);

                if (ManageEntityParams.ContainsKey("IdIndicator_SOx"))
                { ManageEntityParams.Remove("IdIndicator_SOx"); }
                ManageEntityParams.Add("IdIndicator_SOx", _idIndicator_SOx);

                if (ManageEntityParams.ContainsKey("IdIndicator_PM"))
                { ManageEntityParams.Remove("IdIndicator_PM"); }
                ManageEntityParams.Add("IdIndicator_PM", _idIndicator_MP);

                if (ManageEntityParams.ContainsKey("IdIndicator_SO2"))
                { ManageEntityParams.Remove("IdIndicator_SO2"); }
                ManageEntityParams.Add("IdIndicator_SO2", _idIndicator_SO2);

                if (ManageEntityParams.ContainsKey("IdIndicator_H2S"))
                { ManageEntityParams.Remove("IdIndicator_H2S"); }
                ManageEntityParams.Add("IdIndicator_H2S", _idIndicator_H2S);

                if (ManageEntityParams.ContainsKey("IdIndicator_PM10"))
                { ManageEntityParams.Remove("IdIndicator_PM10"); }
                ManageEntityParams.Add("IdIndicator_PM10", _idIndicator_MP10);

                if (ManageEntityParams.ContainsKey("IdIndicator_C2H6"))
                { ManageEntityParams.Remove("IdIndicator_C2H6"); }
                ManageEntityParams.Add("IdIndicator_C2H6", _idIndicator_C2H6);

                if (ManageEntityParams.ContainsKey("IdIndicator_C3H8"))
                { ManageEntityParams.Remove("IdIndicator_C3H8"); }
                ManageEntityParams.Add("IdIndicator_C3H8", _idIndicator_C3H8);

                if (ManageEntityParams.ContainsKey("IdIndicator_C4H10"))
                { ManageEntityParams.Remove("IdIndicator_C4H10"); }
                ManageEntityParams.Add("IdIndicator_C4H10", _idIndicator_C4H10);

                if (ManageEntityParams.ContainsKey("StartDate"))
                { ManageEntityParams.Remove("StartDate"); }
                ManageEntityParams.Add("StartDate", _StartDate);

                if (ManageEntityParams.ContainsKey("EndDate"))
                { ManageEntityParams.Remove("EndDate"); }
                ManageEntityParams.Add("EndDate", _EndDate);

                #endregion
            }
            //private void LoadParameters()
            //{
            //    ManageEntityParams = new Dictionary<String, Object>();
            //    //Debe recorrer las PK para saber si es un Manage de Lenguajes.
            //    String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
            //    //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
            //    ManageEntityParams = GetKeyValues(_pkValues);

            //    //Setea el nombre de la entidad que se va a mostrar 
            //    //y el resto de los parametros recibidos
            //    EntityNameGrid = base.NavigatorGetTransferVar<String>("EntityNameGrid");
            //    _EntityName = base.NavigatorGetTransferVar<String>("EntityName");
            //    _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
            //    if (base.NavigatorContainsTransferVar("EntityNameContextElement"))
            //    {
            //        _EntityNameContextElement = base.NavigatorGetTransferVar<String>("EntityNameContextElement");
            //    }
            //    //EntityNameToRemove = base.NavigatorGetTransferVar<String>("EntityNameToRemove");
            //    //Para el caso del Remove, debe ser el nombre de la entidad mas la palabra Remove!!!
            //    EntityNameToRemove = _EntityName + "Remove";

            //    IsFilterHierarchy = base.NavigatorGetTransferVar<Object>("IsFilterHierarchy") != null ? base.NavigatorGetTransferVar<Boolean>("IsFilterHierarchy") : false;
            //    EntityNameComboFilter = base.NavigatorGetTransferVar<String>("EntityNameComboFilter");
            //    EntityNameChildrenComboFilter = base.NavigatorGetTransferVar<String>("EntityNameChildrenComboFilter");
            //    _EntityNameMapClassification = base.NavigatorGetTransferVar<String>("EntityNameMapClassification");
            //    _EntityNameMapClassificationChildren = base.NavigatorGetTransferVar<String>("EntityNameMapClassificationChildren");
            //    _EntityNameMapElement = base.NavigatorGetTransferVar<String>("EntityNameMapElement");
            //    _EntityNameMapElementChildren = base.NavigatorGetTransferVar<String>("EntityNameMapElementChildren");
            //}
            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
            private void AddTableRow(Int64 id, String title,
                    String result_tCO2e, String result_CO2, String result_CH4, String result_N2O, String result_PFC,
                    String result_HFC, String result_SF6, String result_HCT, String result_HCNM, String result_C2H6,
                    String result_C3H8, String result_C4H10, String result_CO, String result_NOx, String result_SOx,
                    String result_SO2, String result_H2S, String result_PM, String result_PM10)
            {
                const String prefixNODE = "node_";
                const String prefixNODE_CHILD = "child-of-ctl00_ContentMain_node_";

                //Creamos 1 Registro en la tabla
                HtmlTableRow _tr = new HtmlTableRow();
                HtmlTableCell _td = new HtmlTableCell();
                Label _lblCaption = new Label();

                //Indica el Identificador del registro
                _tr.ID = prefixNODE + id.ToString();
                

                //Carga las columnas
                //Columna Titulo
                _lblCaption.Text = title;
                _td.Controls.Add(_lblCaption);
                _td.Attributes.Add("class", "Title");
                _tr.Cells.Add(_td);

                //Columna tCO2e
                _lblCaption = new Label();
                _lblCaption.Text = result_tCO2e == "0." ? "0" : result_tCO2e;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna CO2
                _lblCaption = new Label();
                _lblCaption.Text = result_CO2 == "0." ? "0" : result_CO2;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna CH4
                _lblCaption = new Label();
                _lblCaption.Text = result_CH4 == "0." ? "0" : result_CH4;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna N2O
                _lblCaption = new Label();
                _lblCaption.Text = result_N2O == "0." ? "0" : result_N2O;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna PFC
                _lblCaption = new Label();
                _lblCaption.Text = result_PFC == "0." ? "0" : result_PFC;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna HFC
                _lblCaption = new Label();
                _lblCaption.Text = result_HFC == "0." ? "0" : result_HFC;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna SF6
                _lblCaption = new Label();
                _lblCaption.Text = result_SF6 == "0." ? "0" : result_SF6;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna HCT
                _lblCaption = new Label();
                _lblCaption.Text = result_HCT == "0." ? "0" : result_HCT;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna HCNM
                _lblCaption = new Label();
                _lblCaption.Text = result_HCNM == "0." ? "0" : result_HCNM;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna C2H6
                _lblCaption = new Label();
                _lblCaption.Text = result_C2H6 == "0." ? "0" : result_C2H6;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna C3H8
                _lblCaption = new Label();
                _lblCaption.Text = result_C3H8 == "0." ? "0" : result_C3H8;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna C4H10
                _lblCaption = new Label();
                _lblCaption.Text = result_C4H10 == "0." ? "0" : result_C4H10;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna CO
                _lblCaption = new Label();
                _lblCaption.Text = result_CO == "0." ? "0" : result_CO;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna NOx
                _lblCaption = new Label();
                _lblCaption.Text = result_NOx == "0." ? "0" : result_NOx;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna SOx
                _lblCaption = new Label();
                _lblCaption.Text = result_SOx == "0." ? "0" : result_SOx;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna SO2
                _lblCaption = new Label();
                _lblCaption.Text = result_SO2 == "0." ? "0" : result_SO2;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna H2S
                _lblCaption = new Label();
                _lblCaption.Text = result_H2S == "0." ? "0" : result_H2S;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna PM
                _lblCaption = new Label();
                _lblCaption.Text = result_PM == "0." ? "0" : result_PM;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna PM10
                _lblCaption = new Label();
                _lblCaption.Text = result_PM10 == "0." ? "0" : result_PM10;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCCL");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);


                //Insertamos el Registro en la Tabla
                tblTreeTableReport.Controls.Add(_tr);
            }
            private void LoadTable()
            {
                DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                if (ManageEntityParams["StartDate"] != null)
                { _startDate = Convert.ToDateTime(ManageEntityParams["StartDate"]); }
                if (ManageEntityParams["EndDate"] != null)
                { _endDate = Convert.ToDateTime(ManageEntityParams["EndDate"]); }

                Int64 _idProcess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                Int64 _idIndicator_tnCO2e = Convert.ToInt64(ManageEntityParams["IdIndicator_tnCO2e"]);
                Int64 _idIndicator_CO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_CO2"]);
                Int64 _idIndicator_CH4 = Convert.ToInt64(ManageEntityParams["IdIndicator_CH4"]);
                Int64 _idIndicator_N2O = Convert.ToInt64(ManageEntityParams["IdIndicator_N2O"]);
                Int64 _idIndicator_PFC = Convert.ToInt64(ManageEntityParams["IdIndicator_PFC"]);
                Int64 _idIndicator_HFC = Convert.ToInt64(ManageEntityParams["IdIndicator_HFC"]);
                Int64 _idIndicator_SF6 = Convert.ToInt64(ManageEntityParams["IdIndicator_SF6"]);
                Int64 _idIndicator_HCNM = Convert.ToInt64(ManageEntityParams["IdIndicator_HCNM"]);
                Int64 _idIndicator_HCT = Convert.ToInt64(ManageEntityParams["IdIndicator_HCT"]);
                Int64 _idIndicator_CO = Convert.ToInt64(ManageEntityParams["IdIndicator_CO"]);
                Int64 _idIndicator_NOx = Convert.ToInt64(ManageEntityParams["IdIndicator_NOx"]);
                Int64 _idIndicator_SOx = Convert.ToInt64(ManageEntityParams["IdIndicator_SOx"]);
                Int64 _idIndicator_PM = Convert.ToInt64(ManageEntityParams["IdIndicator_PM"]);
                Int64 _idIndicator_SO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_SO2"]);
                Int64 _idIndicator_H2S = Convert.ToInt64(ManageEntityParams["IdIndicator_H2S"]);
                Int64 _idIndicator_PM10 = Convert.ToInt64(ManageEntityParams["IdIndicator_PM10"]);
                Int64 _idIndicator_C2H6 = Convert.ToInt64(ManageEntityParams["IdIndicator_C2H6"]);
                Int64 _idIndicator_C3H8 = Convert.ToInt64(ManageEntityParams["IdIndicator_C3H8"]);
                Int64 _idIndicator_C4H10 = Convert.ToInt64(ManageEntityParams["IdIndicator_C4H10"]);

                    

                ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                lblProcessValue.Text = _process.LanguageOption.Title;
                lblReportFilter.Text += "<br/>" + Resources.CommonListManage.FilterDate + " "
                    + Resources.CommonListManage.From + " "
                    + Convert.ToDateTime(_startDate).ToShortDateString() + " "
                    + Resources.CommonListManage.Through + " " + Convert.ToDateTime(_endDate).ToShortDateString();

                //Recorro todos los Scope que me devuelve el metodo y los agrega al DT
                foreach (DataRow _item in _process.Report_A_F_by_I_COL(_idIndicator_tnCO2e, _idIndicator_CO2,
                                    _idIndicator_CH4, _idIndicator_N2O, _idIndicator_PFC, _idIndicator_HFC, _idIndicator_SF6, _idIndicator_HCT,
                                    _idIndicator_HCNM, _idIndicator_C2H6, _idIndicator_C3H8, _idIndicator_C4H10, _idIndicator_CO, _idIndicator_NOx,
                                    _idIndicator_SOx, _idIndicator_SO2, _idIndicator_H2S, _idIndicator_PM, _idIndicator_PM10, _startDate, _endDate).Rows)
                {
                    //Inserta el total para cada columna
                    AddTableRow(Convert.ToInt64(_item["IdActivity"]),
                        _item["ActivityName"] + " - " +_item["FacilityName"],
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_tCO2e"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_CO2"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_CH4"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_N2O"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_PFC"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_HFC"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_SF6"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_HCT"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_HCNM"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_C2H6"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_C3H8"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_C4H10"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_CO"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_NOx"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_SOx"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_SO2"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_H2S"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_PM"])),
                        Common.Functions.CustomEMSRound(Convert.ToDouble(_item["Result_PM10"])));
                }

                SetCssInHtmlTable(tblTreeTableReport);
            }
            private void SetCssInHtmlTable(HtmlTable tblContentForm)
            {
                String _css = "trPar";

                foreach (HtmlTableRow _tr in tblContentForm.Rows)
                {
                    _tr.Attributes["class"] = _tr.Attributes["class"] + " " + _css;
                    AlternateRowClassHtmlTable(ref _css);
                }
            }
            private void AlternateRowClassHtmlTable(ref String cssClass)
            {
                cssClass = (cssClass == "trPar") ? "trImpar" : "trPar";
            }

        #endregion

        #region Page Event
            protected void lnkExportGridMeasurement_Click(object sender, EventArgs e)
            {   //Tengo que volver a construir la grilla plana.
                //LoadGridTransformationMeasurement();

                //ConfigureExport(_rgdMasterGridTransformationMeasurement);

                //LoadListManage();

                //rgdListEmissions.ExportSettings.ExportOnlyData = true;
                //rgdListEmissions.ExportSettings.IgnorePaging = true;
                //rgdListEmissions.ExportSettings.OpenInNewWindow = true;

                //rgdListEmissions.Rebind();
                //rgdListEmissions.MasterTableView.ExportToExcel();

                LoadTable();

                Response.Clear();
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=excelimage.xls");
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                tblTreeTableReport.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
            protected void rgdListEmissions_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
            {
                if (e.Column.IsBoundToFieldName("IdActivity") || e.Column.IsBoundToFieldName("IdFacility") || e.Column.IsBoundToFieldName("PermissionType"))
                {
                    //Las columnas con id no se muestra
                    e.Column.Visible = false;
                }
                //else if ((e.Column.IsBoundToFieldName(Resources.CommonListManage.AccountingActivity)) || (e.Column.IsBoundToFieldName(Resources.CommonListManage.Facility)))
                //{
                //    //Son las 3 columnas que tienen texto, etonces se setea esto
                //    e.OwnerTableView.HorizontalAlign = HorizontalAlign.Left;
                //    e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";
                //}
                //else if (e.Column is GridBoundColumn)
                //{
                //    //El resto de las columnas
                //    e.Column.HeaderStyle.Width = Unit.Pixel(95);
                //    e.Column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                //    e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //    e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";
                //}
            }
        #endregion


    }
}
