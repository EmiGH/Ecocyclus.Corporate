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
using Condesus.EMS.WebUI.Controls;

namespace Condesus.EMS.WebUI.DS.Controls
{
    public partial class ResponsibilitiesManage : DSBaseControls
    {
        #region Internal Properties

        //public Int64 _IdOrganization
        //{
        //    get { return Convert.ToInt64(ViewState["_IdOrganization"]); }
        //    set { ViewState["_IdOrganization"] = value.ToString(); }
        //}
        public long _IdFunctionalArea
        {
            get { return Convert.ToInt64(ViewState["IdFunctionalArea"]); }
            set { ViewState["IdFunctionalArea"] = value.ToString(); }
        }
        public long _IdGeographicArea
        {
            get { return Convert.ToInt64(ViewState["IdGeographicArea"]); }
            set { ViewState["IdGeographicArea"] = value.ToString(); }
        }
        public long _IdPosition
        {
            get { return Convert.ToInt64(ViewState["IdPosition"]); }
            set { ViewState["IdPosition"] = value.ToString(); }
        }
        private String _Entity;

        #endregion

        protected void Page_Init(object source, System.EventArgs e)
        {
            _Entity = this.GetType().BaseType.Name;
            
            rgdMasterGridResponsibilities.MasterTableView.DataKeyNames = new string[] { "IdResponsibility" };
            rgdMasterGridResponsibilities.ClientSettings.AllowExpandCollapse = false;
            rgdMasterGridResponsibilities.ClientSettings.Selecting.AllowRowSelect = true;
            rgdMasterGridResponsibilities.AllowMultiRowSelection = true;
            rgdMasterGridResponsibilities.ClientSettings.Selecting.EnableDragToSelectRows = false;

            InjectJavaScript();
            InitializeHandlers();

            base.CheckSecurity(ref rmnGeneralOptionsResponsibilities, "DirectoryServices", "Responsibilities", true, false, false, true, false);
            base.CheckSecurity(ref rmnSelectionResponsibilities, "DirectoryServices", "Responsibilities", false, false, false, true, false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
                //ScriptManager _sm = (ScriptManager)(this.Page.Master.FindControl("smEMSMaster"));
                //_sm.RegisterAsyncPostBackControl(rgdMasterGridResponsibilities);
            }
        }

        #region Generic Methods

        public void ReloadGrid()
        {
            rgdMasterGridResponsibilities.DataSource = LoadGrid();
            rgdMasterGridResponsibilities.Rebind();
        }
        private DataTable LoadGrid()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdResponsibility");
            _dt.Columns.Add("Responsibility");

            Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
            Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
            Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_IdPosition);
            Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
            Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            foreach (Condesus.EMS.Business.DS.Entities.Responsibility _responsibility in _jobTitle.Responsibilities())
            {
                String _responsibilityName = _responsibility.GeographicFunctionalAreaResponsibility.Name();

                String _id = _IdGeographicArea.ToString() + ',' + _IdFunctionalArea.ToString() + ',' + _IdPosition.ToString() + ',' + _responsibility.GeographicFunctionalAreaResponsibility.GeographicArea.IdGeographicArea.ToString() + ',' + _responsibility.GeographicFunctionalAreaResponsibility.FunctionalArea.IdFunctionalArea.ToString();

                _dt.Rows.Add(_id, _responsibilityName);
            }

            return _dt;
        }

        #endregion

        #region Page Events

        private void InjectJavaScript()
        {
            base.InjectShowMenu(rmnSelectionResponsibilities.ClientID, _Entity);
            base.InjectRowContextMenu(rmnSelectionResponsibilities.ClientID, _Entity);
            base.InjectRmnSelectionItemClickHandler(uProgMasterGrid.ClientID, _Entity);
            //base.InjectRmnOptionItemClickHandler(rmnOptionResponsibilities.ClientID, uProgMasterGrid.ClientID, _Entity);
            base.InjectValidateItemChecked(rgdMasterGridResponsibilities.ClientID);
            base.InjectRmnOptionItemClickHandler(rmnOptionResponsibilities.ClientID, uProgMasterGrid.ClientID, _Entity);
        }
        private void InitializeHandlers()
        {
            rmnOptionResponsibilities.ItemClick += new RadMenuEventHandler(rmnOptionResponsibilities_ItemClick);
            rmnOptionResponsibilities.OnClientItemClicked = "rmnOption" + _Entity + "_OnClientItemClickedHandler";

            rmnSelectionResponsibilities.ItemClick += new RadMenuEventHandler(rmnSelectionResponsibilities_ItemClick);
            rmnSelectionResponsibilities.OnClientItemClicked = "rmnSelection" + _Entity + "_OnClientItemClickedHandler";

            rgdMasterGridResponsibilities.ItemCreated += new GridItemEventHandler(rgdMasterGridResponsibilities_ItemCreated);
            rgdMasterGridResponsibilities.SortCommand += new GridSortCommandEventHandler(rgdMasterGridResponsibilities_SortCommand);
            rgdMasterGridResponsibilities.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGridResponsibilities_PageIndexChanged);
            //rgdMasterGridResponsibilities.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu" + _Entity;

            btnOkDeleteResponsibilities.Click += new EventHandler(btnOkDeleteResponsibility_Click);
        }
        protected void rmnSelectionResponsibilities_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            int _radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex" + _Entity]);
            string _uId= Request.Form["radGridClickedTableId" + _Entity];

            GridTableView _tableView = this.Page.FindControl(_uId) as GridTableView;
            ((_tableView.Controls[0] as Table).Rows[_radGridClickedRowIndex] as GridItem).Selected = true;

            switch (e.Item.Value)
            {
                case "rmiDelete":  //Delete
                    {
                        mpelbDeleteResponsibilities.Show();
                    }
                    break;
            }//fin Switch
        }//fin evento
        protected void rmnOptionResponsibilities_ItemClick(object sender, RadMenuEventArgs e)
        {
            //Solo entro en el Add (El delete lo hace Client Side y cancela el PostBack)
            base.MenuClick("0", false, MenuType.GeneralOptions);
        }
        protected void rgdMasterGridResponsibilities_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            base.ItemCreated(sender,e,_Entity);
        }
        protected void rgdMasterGridResponsibilities_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            rgdMasterGridResponsibilities.DataSource = LoadGrid();
            rgdMasterGridResponsibilities.MasterTableView.Rebind();
        }
        protected void rgdMasterGridResponsibilities_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            rgdMasterGridResponsibilities.DataSource = LoadGrid();
            rgdMasterGridResponsibilities.MasterTableView.Rebind();
        }
        protected void btnOkDeleteResponsibility_Click(object sender, EventArgs e)
        {
            try
            {
                //identifica si ejecuto el menu Option o Selection
                string _radMenuClickedId = Convert.ToString(Request.Form["radMenuClickedId" + _Entity]);

                if (_radMenuClickedId == "Option")
                {
                    //De esta forma recorremos todas las filas de la grilla ( de la pagina en la que estamos )
                    foreach (GridDataItem _row in rgdMasterGridResponsibilities.Items)
                    {
                        //Buscamos el Checkbox para ver el valor que tiene y saber si lo eliminamos o no
                        CheckBox _chkSelect = (CheckBox)(_row.FindControl("chkSelectResponsibility"));
                        if (_chkSelect != null)
                        {
                            //Si esta clickeado entramos a la rutina para borrar la fila.
                            if (_chkSelect.Checked)
                            {
                                //De esta forma obtenemos el ID ( la PK ) de la Fila en la que estamos para 
                                //luego utilizar en nuestro objeto para eliminar el registro
                                Label _lblIdResponsibility = (Label)(_row.FindControl("lblIdResponsibility"));
                                String[] _id = ((String)_lblIdResponsibility.Text).Split(',');
                                Int64 _idGeographicArea = Convert.ToInt64(_id[0]);
                                Int64 _idFunctionalArea = Convert.ToInt64(_id[1]);
                                Int64 _idPosition = Convert.ToInt64(_id[2]);
                                Int64 _idGeographicArea_Rel = Convert.ToInt64(_id[3]);
                                Int64 _idFunctionalArea_Rel = Convert.ToInt64(_id[4]);

                                //Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).FunctionalArea(_idFunctionalArea_Rel);
                                //Condesus.EMS.Business.DS.Entities.GeographicArea _geoArea = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).GeographicArea(_idGeographicArea_Rel);
                                //EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea).ResponsibilitiesRemove(_idFunctionalArea_Rel, _idGeographicArea_Rel);
                                Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                                Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea_Rel);
                                Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_IdPosition);
                                Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea_Rel);
                                Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                                Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                                Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                                Condesus.EMS.Business.DS.Entities.Responsibility _responsibility = _jobTitle.Responsibility(_funArea, _geoArea);
                                _jobTitle.Remove(_responsibility);
                            }
                        }
                    }
                }
                else
                {
                    //carga la grilla seleccionada
                    int _radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex" + _Entity]);
                    //carga la tabla seleccionada
                    string _uId = Request.Form["radGridClickedTableId" + _Entity];

                    //Obtiene el item de la grilla que se selecciono.
                    GridTableView _tableView = this.Page.FindControl(_uId) as GridTableView;
                    String _strIdResponsibility = ((GridDataItem)((_tableView.Controls[0] as Table).Rows[_radGridClickedRowIndex] as GridItem)).GetDataKeyValue("IdResponsibility").ToString();

                    String[] _id = _strIdResponsibility.Split(',');
                    Int64 _idGeographicArea = Convert.ToInt64(_id[0]);
                    Int64 _idFunctionalArea = Convert.ToInt64(_id[1]);
                    Int64 _idPosition = Convert.ToInt64(_id[2]);
                    Int64 _idGeographicArea_Rel = Convert.ToInt64(_id[3]);
                    Int64 _idFunctionalArea_Rel = Convert.ToInt64(_id[4]);

                    //Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).FunctionalArea(_idFunctionalArea_Rel);
                    //Condesus.EMS.Business.DS.Entities.GeographicArea _geoArea = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).GeographicArea(_idGeographicArea_Rel);

                    //De esta forma obtenemos el ID ( la PK ) de la Fila en la que estamos para 
                    //luego utilizar en nuestro objeto para eliminar el registro
                    //EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea).ResponsibilitiesRemove(_idFunctionalArea_Rel, _idGeographicArea_Rel);
                    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea_Rel);
                    Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_IdPosition);
                    Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea_Rel);
                    Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                    Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                    Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                    Condesus.EMS.Business.DS.Entities.Responsibility _responsibility = _jobTitle.Responsibility(_funArea, _geoArea);
                    _jobTitle.Remove(_responsibility);
                }

                //Recarga la grilla sin el/los registros borrados
                rgdMasterGridResponsibilities.DataSource = LoadGrid();
                rgdMasterGridResponsibilities.MasterTableView.Rebind();
                //oculta el popup.
                this.mpelbDeleteResponsibilities.Hide();
                lblMessage.CssClass = "contentformLoadAccept";
                lblMessage.Text = String.Format(Resources.Common.DeleteOK);
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "contentformLoadError";
                lblMessage.Text = String.Format(Resources.Common.DeleteFailed, ex.Message);
            }
        }

        #endregion
    }
}
