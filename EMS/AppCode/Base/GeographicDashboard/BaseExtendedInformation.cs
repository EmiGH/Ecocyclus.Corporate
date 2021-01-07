using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Reflection;
using System.Linq;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.KC.Entities;
using Dundas.Charting.WebControl;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
            private RadTabStrip _RtsExtendedInformation = new RadTabStrip();
            private RadMultiPage _RmpExtendedInformation = new RadMultiPage();
            private Panel _PnlContentExtendedInfo = new Panel();
            private RadGrid _RgdMasterGridRelated = null;
        #endregion

        #region Private Methods
            //private RadTabStrip BuildTabStripExtendedInfo()
            //{
            //    _RtsExtendedInformation.ID = "rtsExtendedInformation";
            //    _RtsExtendedInformation.MultiPageID = "rmpExtendedInformation";
            //    _RtsExtendedInformation.Skin = "EMS";
            //    _RtsExtendedInformation.EnableEmbeddedSkins = false;
            //    _RtsExtendedInformation.CausesValidation = false;
            //    _RtsExtendedInformation.SelectedIndex = 0;
            //    _RmpExtendedInformation.Width = Unit.Pixel(200);


            //    return _RtsExtendedInformation;
            //}
            //private RadTab BuildTabExtendedInfo(String text)
            //{
            //    RadTab _radTab = new RadTab();
            //    _radTab.Text = text;

            //    return _radTab;
            //}
            //private RadMultiPage BuildMultiPageExtendedInfo(String controlID)
            //{
            //    RadMultiPage _radMultiPage = new RadMultiPage();
            //    _radMultiPage.ID = controlID;
            //    _radMultiPage.SelectedIndex = 0;

            //    return _radMultiPage;
            //}
            //private RadPageView BuildPageViewExtendedInfo(String controlID, Boolean selected)
            //{
            //    RadPageView _radPageView = new RadPageView();
            //    _radPageView.ID = controlID;
            //    _radPageView.Selected = selected;

            //    return _radPageView;
            //}
            ///// <summary>
            ///// Este metodo retorna los parametros armados, para cada registro de la grilla.
            ///// </summary>
            ///// <param name="rgdListManage"></param>
            ///// <returns>Un<c>String</c></returns>
            //private String GetPKCompostFromItem(RadGrid rgdListManage, Int32 _selectedIndex)
            //{
            //    String _params = String.Empty;
            //    for (int i = 0; i < rgdListManage.MasterTableView.DataKeyNames.Count(); i++)
            //    {
            //        //Obtiene el KeyName y su Valor del registo seleccionado en la grilla.
            //        String _keyName = rgdListManage.MasterTableView.DataKeyNames[i].Trim();
            //        String _keyValue = rgdListManage.MasterTableView.DataKeyValues[_selectedIndex][_keyName].ToString();
            //        //Finalmente concatena los key en un string de parametros.
            //        _params += _keyName + "=" + _keyValue + "&";
            //    }

            //    return _params.Substring(0, _params.Length - 1); ;
            //}

            #region Process
                //public RadGrid BuildProcessExtendedProperty(Int64 idEntity)
                //{
                //    //Construye el panel que contendra a la grilla..
                //    //Panel _pnlExtendedProperties = new Panel();
                //    //_pnlExtendedProperties.ID = "pnlExtendedProperties";
                //    //_pnlExtendedProperties.Controls.Add(BuildProcessExtendedProperties(idEntity));

                //    return BuildProcessExtendedProperties(idEntity);
                //}
                //public RadGrid BuildProcessForum(Int64 idEntity)
                //{
                //    //Construye el panel que contendra a la grilla con los topics.
                //    //Panel _pnlTopics = new Panel();
                //    //_pnlTopics.ID = "pnlTopics";
                //    //_pnlTopics.Controls.Add(BuildProcessForumTopics(idEntity));

                //    return BuildProcessForumTopics(idEntity);
                //}
                private void BuildProcessExtendedInformation(Int64 idEntity)
                {
                    String _multiPageID = "rmpExtendedInformation";
                    //Arma el TabStrip
                    _RtsExtendedInformation = BuildTabStrip( _multiPageID);  // BuildTabStripExtendedInfo();
                    //Construye los tabs a mostrar
                    _RtsExtendedInformation.Tabs.Add(BuildTab(Resources.CommonListManage.ExtendedProperties));
                    _RtsExtendedInformation.Tabs.Add(BuildTab(Resources.CommonListManage.Forums));

                    //Construye el MultiPage
                    _RmpExtendedInformation = BuildMultiPage(_multiPageID);

                    //Y ahora construye cada PageView con su contenido...
                    RadPageView _rpvExtendedProperties = BuildPageView("rpvExtendedProperties", true);
                    //Construye el panel que contendra a la grilla..
                    Panel _pnlExtendedProperties = new Panel();
                    _pnlExtendedProperties.ID = "pnlExtendedProperties";
                    _pnlExtendedProperties.Controls.Add(BuildProcessExtendedProperties(idEntity));
                    //Agrega el panel al pageview.
                    _rpvExtendedProperties.Controls.Add(_pnlExtendedProperties);

                    //Agrega otra Page view con los foros...
                    RadPageView _rpvForums = BuildPageView("rpvForums", false);
                    //Construye el panel que contendra a la grilla con los topics.
                    Panel _pnlTopics = new Panel();
                    _pnlTopics.ID = "pnlTopics";
                    _pnlTopics.Controls.Add(BuildProcessForumTopics(idEntity));
                    //Agrega el panel al pageview.
                    _rpvForums.Controls.Add(_pnlTopics);
                    

                    //Agrega los PAgeView al MultiPage
                    _RmpExtendedInformation.PageViews.Add(_rpvExtendedProperties);
                    _RmpExtendedInformation.PageViews.Add(_rpvForums);

                    //Agrega el Tab y el MultiPage al panel a retornar que es lo que se inyectara en la pagina.
                    _PnlContentExtendedInfo.Controls.Add(_RtsExtendedInformation);
                    _PnlContentExtendedInfo.Controls.Add(_RmpExtendedInformation);


                }
                public RadGrid BuildProcessExtendedProperties(Int64 idEntity)
                {
                    Boolean _showImgSelect = false;
                    Boolean _showCheck = false;
                    Boolean _allowSearchableGrid = false;
                    Boolean _showOpenFile = false;
                    Boolean _showOpenChart = false;
                    Boolean _showOpenSeries = false;

                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdProcess", idEntity);

                    BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessExtendedProperties, _params);
                    _RgdMasterGridRelated = BuildListManageContent(Common.ConstantsEntitiesName.PF.ProcessExtendedProperties, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
                    //Cambia el estilo de la paginacion solo para esta situacion
                    _RgdMasterGridRelated.PagerStyle.Mode = GridPagerMode.NumericPages;
                    _RgdMasterGridRelated.MasterTableView.PagerStyle.Mode = GridPagerMode.NumericPages;

                    //especializa el pagesize
                    _RgdMasterGridRelated.PageSize = 5;
                    PersistControlState(_RgdMasterGridRelated);

                    return _RgdMasterGridRelated;
                }
                public RadGrid BuildProcessForumTopics(Int64 idEntity)
                {
                    Boolean _showImgSelect = false;
                    Boolean _showCheck = false;
                    Boolean _allowSearchableGrid = false;
                    Boolean _showOpenFile = false;
                    Boolean _showOpenChart = false;
                    Boolean _showOpenSeries = false;

                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdProcess", idEntity);

                    BuildGenericDataTable(Common.ConstantsEntitiesName.CT.FullTopics, _params);
                    _RgdMasterGridRelated = BuildListManageContent(Common.ConstantsEntitiesName.CT.FullTopics, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
                    //Cambia el estilo de la paginacion solo para esta situacion
                    _RgdMasterGridRelated.PagerStyle.Mode = GridPagerMode.NumericPages;
                    _RgdMasterGridRelated.MasterTableView.PagerStyle.Mode = GridPagerMode.NumericPages;

                    //especializa el pagesize
                    _RgdMasterGridRelated.PageSize = 5;
                    PersistControlState(_RgdMasterGridRelated);

                    _RgdMasterGridRelated.ItemCreated += new GridItemEventHandler(RgdMasterGridRelated_ItemCreated);

                    return _RgdMasterGridRelated;

                }
                protected void RgdMasterGridRelated_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
                {
                    //ItemCreated(sender, e);
                    if (e.Item is Telerik.Web.UI.GridDataItem)
                    {
                        HtmlImage oimg = (HtmlImage)e.Item.FindControl("selButton");
                        if (!(oimg == null))
                        {
                            oimg.Attributes["onclick"] = string.Format("return ShowMenu(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                        }
                        foreach (GridColumn _col in ((RadGrid)sender).Columns)
                        {
                            if (_col.GetType().Name == "GridButtonColumn")
                            {
                                //Si la columna es de Tipo GridButtonColumn...
                                GridDataItem dataItem = (GridDataItem)e.Item;
                                LinkButton lbtNavigateView = (LinkButton)dataItem[_col.UniqueName].Controls[0];

                                RadGrid _rgdGridViewer = (RadGrid)sender;
                                //Agrega el evento Cliente JS, para que seleccione el registro y continue o no, con el PostBack.
                                //lbtNavigateView.Attributes["onclick"] = string.Format("return ClientSelectRow" + _rgdGridViewer.ClientID + "(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "', " + _execPostback.ToString().ToLower() + ");");
                                String _pkCompost = GetPKCompostFromItem((RadGrid)sender, e.Item.ItemIndex);
                                //Solo para este caso, que esta dentro de un iFrame, se reemplaza el & por | (pipe)
                                lbtNavigateView.Attributes.Add("PkCompost", _pkCompost.Replace("&", "|"));
                                lbtNavigateView.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Message);
                                lbtNavigateView.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.Messages);
                                lbtNavigateView.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Topic);
                                lbtNavigateView.Attributes.Add("EntityNameContextElement", String.Empty);
                                lbtNavigateView.Attributes.Add("onclick", "javascript:NavigateToContent(this, event);");

                            }
                            else
                            {
                                if (_col.UniqueName == "Value")
                                {
                                    GridDataItem dataItem = (GridDataItem)e.Item;
                                    if (dataItem[_col.UniqueName].Controls.Count > 0)
                                    {
                                        LinkButton lbtNavigateView = (LinkButton)dataItem[_col.UniqueName].Controls[0];
                                        //Saca el subrayado del texto.
                                        lbtNavigateView.Style.Add("text-decoration", "none");
                                    }
                                }
                            }
                        }
                    }
                }
            #endregion

            #region Measurement
                private void BuildMeasurementExtendedInformation(Int64 idEntity)
                {
                    String _multiPageID = "rmpExtendedInformation";
                    //Arma el TabStrip
                    _RtsExtendedInformation = BuildTabStrip(_multiPageID);  // BuildTabStripExtendedInfo();
                    //Construye los tabs a mostrar
                    _RtsExtendedInformation.Tabs.Add(BuildTab("Statistics"));
                    //_RtsExtendedInformation.Tabs.Add(BuildTabExtendedInfo("Conditions"));
                    //_RtsExtendedInformation.Tabs.Add(BuildTabExtendedInfo("Forums"));

                    //Construye el MultiPage
                    _RmpExtendedInformation = BuildMultiPage(_multiPageID);

                    //Y ahora construye cada PageView con su contenido...
                    RadPageView _rpvStatistics = BuildPageView("rpvStatistics", true);
                    //Construye el panel que contendra a la grilla..
                    Panel _pnlStatistics = new Panel();
                    _pnlStatistics.ID = "pnlStatistics";
                    _pnlStatistics.Controls.Add(BuildMeasurementStats(idEntity));
                    //Agrega el panel al pageview.
                    _rpvStatistics.Controls.Add(_pnlStatistics);

                    //Agrega otra Page view con los foros...
                    RadPageView _rpvConditions = BuildPageView("rpvConditions", false);
                    //Agrega la grilla de foros......
                    //por ahora no hay nada-...

                    //Agrega otra Page view con los foros...
                    RadPageView _rpvForums = BuildPageView("rpvForums", false);
                    //Agrega la grilla de foros......
                    //por ahora no hay nada-...


                    //Agrega los PAgeView al MultiPage
                    _RmpExtendedInformation.PageViews.Add(_rpvStatistics);
                    //_RmpExtendedInformation.PageViews.Add(_rpvConditions);
                    //_RmpExtendedInformation.PageViews.Add(_rpvForums);

                    //Agrega el Tab y el MultiPage al panel a retornar que es lo que se inyectara en la pagina.
                    _PnlContentExtendedInfo.Controls.Add(_RtsExtendedInformation);
                    _PnlContentExtendedInfo.Controls.Add(_RmpExtendedInformation);


                }
                public RadGrid BuildMeasurementStats(Int64 idEntity)
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdMeasurement", idEntity);
                    BuildGenericDataTable(Common.ConstantsEntitiesName.PA.MeasurementStatistical, _params);
                    _RgdMasterGridRelated = BuildListViewerContent(Common.ConstantsEntitiesName.PA.MeasurementStatistical);

                    InjectClientSelectRow(_RgdMasterGridRelated.ClientID);

                    return _RgdMasterGridRelated;
                }
                public RadGrid BuildMeasurements(Int64 idEntity, Int64 idFacility)
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdProcess", idEntity);
                    _params.Add("IdFacility", idFacility);

                    BuildGenericDataTable(Common.ConstantsEntitiesName.PA.AllMeasurementByFacility, _params);
                    _RgdMasterGridRelated = BuildListViewerContent(Common.ConstantsEntitiesName.PA.AllMeasurementByFacility);
                    _RgdMasterGridRelated.PagerStyle.Mode = GridPagerMode.NumericPages; 

                    _RgdMasterGridRelated.AllowPaging = true;
                    _RgdMasterGridRelated.PageSize = 10;
                    _RgdMasterGridRelated.Height = 210;
                    _RgdMasterGridRelated.ClientSettings.Scrolling.AllowScroll = true;
                    _RgdMasterGridRelated.ClientSettings.Scrolling.UseStaticHeaders = true;
                    _RgdMasterGridRelated.HeaderStyle.CssClass = "rgdHeaderFacility";
                    _RgdMasterGridRelated.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
                    _RgdMasterGridRelated.ItemDataBound += new GridItemEventHandler(_RgdMasterGridRelated_ItemDataBound);
                    _RgdMasterGridRelated.ClientSettings.Selecting.AllowRowSelect = true;
                    InjectClientSelectRow(_RgdMasterGridRelated.ClientID);

                    return _RgdMasterGridRelated;
                }
                protected void _RgdMasterGridRelated_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    if (e.Item is GridDataItem)
                    {
                        String _idMeasurement = ((DataRowView)e.Item.DataItem).Row["IdMeasurement"].ToString();
                        String _idProcess = ((DataRowView)e.Item.DataItem).Row["IdProcess"].ToString();
                        String _idTask = ((DataRowView)e.Item.DataItem).Row["IdTask"].ToString();
                        String _measurementName = ((DataRowView)e.Item.DataItem).Row["Measurement"].ToString();

                        String _pkCompost = "IdMeasurement=" + _idMeasurement
                            + "|IdProcess=" + _idProcess
                            + "|IdTask=" + _idTask;

                        e.Item.Attributes.Add("PkCompost", _pkCompost);
                        e.Item.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.KeyIndicator);
                        e.Item.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.AllKeyIndicators);
                        e.Item.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Measurement);
                        e.Item.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                        e.Item.Attributes.Add("Text", _measurementName);

                        e.Item.Style.Add("onmouseover", "this.style.cursor = 'pointer'");
                        e.Item.Attributes.Add("onmouseover", "this.style.cursor = 'pointer'");
                        e.Item.Attributes["onclick"] = string.Format("return NavigateToContent(this, event);");


                        String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                        if (_measurementStatus.ToLower() == "true")
                        {
                            e.Item.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                public RadGrid BuildSectors(Int64 idOrganization, Int64 idFacility)
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdOrganization", idOrganization);
                    _params.Add("IdFacility", idFacility);

                    BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Sectors, _params);
                    _RgdMasterGridRelated = BuildListViewerContent(Common.ConstantsEntitiesName.DS.Sectors);
                    _RgdMasterGridRelated.PagerStyle.Mode = GridPagerMode.NumericPages;

                    _RgdMasterGridRelated.AllowPaging = true;
                    _RgdMasterGridRelated.PageSize = 4;
                    _RgdMasterGridRelated.HeaderStyle.CssClass = "rgdHeaderFacility";
                    _RgdMasterGridRelated.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);

                    InjectClientSelectRow(_RgdMasterGridRelated.ClientID);

                    return _RgdMasterGridRelated;
                }
                public RadGrid BuildProcessAsociatedToFacility(Int64 idFacility)
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdFacility", idFacility);

                    BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessAsociatedToFacility, _params);
                    _RgdMasterGridRelated = BuildListViewerContent(Common.ConstantsEntitiesName.PF.ProcessAsociatedToFacility);
                    _RgdMasterGridRelated.PagerStyle.Mode = GridPagerMode.NumericPages;

                    _RgdMasterGridRelated.AllowPaging = true;
                    _RgdMasterGridRelated.PageSize = 10;
                    _RgdMasterGridRelated.Height = 210;
                    _RgdMasterGridRelated.ClientSettings.Scrolling.AllowScroll = true;
                    _RgdMasterGridRelated.ClientSettings.Scrolling.UseStaticHeaders = true;
                    _RgdMasterGridRelated.HeaderStyle.CssClass = "rgdHeaderFacility";
                    _RgdMasterGridRelated.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
                    _RgdMasterGridRelated.ItemDataBound += new GridItemEventHandler(_RgdMasterGridProcessAsociatedToFacility_ItemDataBound);
                    _RgdMasterGridRelated.ClientSettings.Selecting.AllowRowSelect = true;
                    InjectClientSelectRow(_RgdMasterGridRelated.ClientID);

                    return _RgdMasterGridRelated;
                }
                protected void _RgdMasterGridProcessAsociatedToFacility_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    if (e.Item is GridDataItem)
                    {
                        String _idProcess = ((DataRowView)e.Item.DataItem).Row["IdProcess"].ToString();
                        String _processName = ((DataRowView)e.Item.DataItem).Row["Name"].ToString();

                        String _pkCompost = "IdProcess=" + _idProcess;

                        e.Item.Attributes.Add("PkCompost", _pkCompost);
                        e.Item.Attributes.Add("ProcessName", _processName);
                        e.Item.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.FacilitiesByProcess);
                        e.Item.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.FacilitiesByProcess);
                        e.Item.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                        e.Item.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                        e.Item.Attributes.Add("onclick", "javascript:NavigateToContent(this, event);");

                        //Esto es para que funcione en Chrome y en IE....
                        e.Item.Style.Add("onmouseover", "this.style.cursor = 'pointer'");
                        e.Item.Attributes.Add("onmouseover", "this.style.cursor = 'pointer'");

                        //String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                        //if (_measurementStatus.ToLower() == "true")
                        //{
                        //    e.Item.ForeColor = System.Drawing.Color.Red;
                        //}
                    }
                }

            #endregion
        #endregion

        #region Public Methods
            public Panel BuildExtendedInformation(String entityObject, Int64 idEntity)
            {
                //Construye el panel que terminara retornando.
                _PnlContentExtendedInfo.ID = "pnlContentExtendedInfo";

                switch (entityObject)
                {
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.PF.Process:
                        //Construye todo el extended para el Process...
                        BuildProcessExtendedInformation(idEntity);
                        break;
                    case Common.ConstantsEntitiesName.PA.Measurement:
                        //Construye todo el extended para el measurement...
                        BuildMeasurementExtendedInformation(idEntity);
                        break;
                    default:
                        break;
                }
                //Retorna el panel con todos los controles metidos.
                return _PnlContentExtendedInfo;
            }
            public Panel BuildExtendedInformation(ref RadTabStrip rtsExtendedInfo, String entityObject, Int64 idEntity)
            {
                //Construye el panel que terminara retornando.
                _PnlContentExtendedInfo.ID = "pnlContentExtendedInfo";

                switch (entityObject)
                {
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.PF.Process:
                        //Construye todo el extended para el Process...
                        BuildProcessExtendedInformation(idEntity);
                        break;
                    case Common.ConstantsEntitiesName.PA.Measurement:
                        //Construye todo el extended para el measurement...
                        BuildMeasurementExtendedInformation(idEntity);
                        break;
                    default:
                        break;
                }
                //Retorna el panel con todos los controles metidos.
                return _PnlContentExtendedInfo;
            }
        #endregion

    }
}
