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
    public partial class PresencesManage : DSBaseControls
    {
        #region Internal Properties

        public long _IdPerson
        {
            get { return Convert.ToInt64(ViewState["IdPerson"]); }
            set { ViewState["IdPerson"] = value.ToString(); }
        }
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

            rgdMasterGridPresences.MasterTableView.DataKeyNames = new string[] { "IdPresence" };
            rgdMasterGridPresences.ClientSettings.AllowExpandCollapse = false;
            rgdMasterGridPresences.ClientSettings.Selecting.AllowRowSelect = true;
            rgdMasterGridPresences.AllowMultiRowSelection = true;
            rgdMasterGridPresences.ClientSettings.Selecting.EnableDragToSelectRows = false;

            InjectJavaScript();
            InitializeHandlers();
            base.CheckSecurity(ref rmnGeneralOptions, "DirectoryServices", "Presences", true, false, false, true, false);
            base.CheckSecurity(ref rmnSelectionPresences, "DirectoryServices", "Presences", false, false, false, true, false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnTransferAdd);
        }

        #region Generic Methods

        public void ReloadGrid()
        {
            rgdMasterGridPresences.DataSource = LoadGrid();
            rgdMasterGridPresences.Rebind();
        }
        private DataTable LoadGrid()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdPresence");
            _dt.Columns.Add("Presence");

            if (_IdGeographicArea != 0)
            {
                Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
                Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_IdPosition);
                Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                foreach (Condesus.EMS.Business.DS.Entities.Presence _presence in ((Condesus.EMS.Business.DS.Entities.PersonwithUser)_organization.Person(_IdPerson)).Post(_jobTitle).Presences())
                {
                    String _presenceName = _presence.Facility.LanguageOption.Name;

                    String _id = _IdGeographicArea.ToString() + ',' + _IdFunctionalArea.ToString() + ',' + _IdPosition.ToString() + ',' + _presence.Facility.IdFacility.ToString() + ',' + _IdPerson.ToString();

                    _dt.Rows.Add(_id, _presenceName);
                }
            }
            return _dt;
        }
     
        #endregion

        #region Page Events

        private void InjectJavaScript()
        {
            base.InjectShowMenu(rmnSelectionPresences.ClientID, _Entity);
            base.InjectRowContextMenu(rmnSelectionPresences.ClientID, _Entity);
            base.InjectRmnSelectionItemClickHandler(uProgMasterGridPresences.ClientID, _Entity);
            base.InjectRmnOptionItemClickHandler(rmnOptionPresences.ClientID, uProgMasterGridPresences.ClientID, _Entity);
            base.InjectValidateItemChecked(rgdMasterGridPresences.ClientID);
        }
        private void InitializeHandlers()
        {
            rmnOptionPresences.ItemClick += new RadMenuEventHandler(rmnOptionPresences_ItemClick);
            rmnOptionPresences.OnClientItemClicked = "rmnOption" + _Entity + "_OnClientItemClickedHandler";

            rmnSelectionPresences.ItemClick += new RadMenuEventHandler(rmnSelectionPresences_ItemClick);
            rmnSelectionPresences.OnClientItemClicked = "rmnSelection" + _Entity + "_OnClientItemClickedHandler";

            rgdMasterGridPresences.ItemCreated += new GridItemEventHandler(rgdMasterGridPresences_ItemCreated);
            rgdMasterGridPresences.SortCommand += new GridSortCommandEventHandler(rgdMasterGridPresences_SortCommand);
            rgdMasterGridPresences.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGridPresences_PageIndexChanged);
            rgdMasterGridPresences.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu" + _Entity;

            btnOk.Click += new EventHandler(btnOkDeletePresence_Click);
        }
        protected void rmnOptionPresences_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            String _id = _IdPerson.ToString() + ',' + _IdGeographicArea.ToString() + ',' + _IdFunctionalArea.ToString() + ',' + _IdPosition.ToString();

            //Solo entro en el Add (El delete lo hace Client Side y cancela el PostBack)
            MenuClick(_id, false, MenuType.GeneralOptions);

        }
        protected void rmnSelectionPresences_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            //carga la grilla seleccionada
            int _radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex" + _Entity]);
            //carga la tabla seleccionada
            string _uId = Request.Form["radGridClickedTableId" + _Entity];

             GridTableView _tableView = this.Page.FindControl(_uId) as GridTableView;
             ((_tableView.Controls[0] as Table).Rows[_radGridClickedRowIndex] as GridItem).Selected = true;

            switch (e.Item.Value)
            {
                case "rmiDelete":  //Delete
                    {
                        mpelbDeletePresence.Show();
                    }
                    break;
            }//fin Switch
        }//fin evento
        protected void rgdMasterGridPresences_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HtmlImage oimg = (HtmlImage)e.Item.FindControl("selButton");
                if (!(oimg == null))
                {
                    oimg.Attributes["onclick"] = string.Format("return ShowMenu"+_Entity+"(event, " + e.Item.RowIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                }
            }
        }
        protected void rgdMasterGridPresences_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            rgdMasterGridPresences.DataSource = LoadGrid();
            rgdMasterGridPresences.MasterTableView.Rebind();
        }
        protected void rgdMasterGridPresences_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            rgdMasterGridPresences.DataSource = LoadGrid();
            rgdMasterGridPresences.MasterTableView.Rebind();
        }
        protected void btnOkDeletePresence_Click(object sender, EventArgs e)
        {
            try
            {
                //identifica si ejecuto el menu Option o Selection
                string _radMenuClickedId = Convert.ToString(Request.Form["radMenuClickedId" + _Entity]);

                if (_radMenuClickedId == "Option")
                {
                    //De esta forma recorremos todas las filas de la grilla ( de la pagina en la que estamos )
                    foreach (GridDataItem _row in rgdMasterGridPresences.Items)
                    {
                        //Buscamos el Checkbox para ver el valor que tiene y saber si lo eliminamos o no
                        CheckBox _chkSelect = (CheckBox)(_row.FindControl("chkSelectPresence"));
                        if (_chkSelect != null)
                        {
                            //Si esta clickeado entramos a la rutina para borrar la fila.
                            if (_chkSelect.Checked)
                            {
                                //De esta forma obtenemos el ID ( la PK ) de la Fila en la que estamos para 
                                //luego utilizar en nuestro objeto para eliminar el registro
                                Label _lblIdPresence = (Label)(_row.FindControl("lblIdPresence"));
                                String[] _id = ((String)_lblIdPresence.Text).Split(',');
                                Int64 _idGeographicArea = Convert.ToInt64(_id[0]);
                                Int64 _idFunctionalArea = Convert.ToInt64(_id[1]);
                                Int64 _idPosition = Convert.ToInt64(_id[2]);
                                Int64 _idFacility = Convert.ToInt64(_id[3]);
                                Int64 _idPerson = Convert.ToInt64(_id[4]);

                                Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                                Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                                Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                                Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                                Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                                Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                                Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                                
                                Condesus.EMS.Business.DS.Entities.Post _post = ((Condesus.EMS.Business.DS.Entities.PersonwithUser)_organization.Person(_idPerson)).Post(_jobTitle);
                                Condesus.EMS.Business.GIS.Entities.Facility _facility = _organization.Facility(_idFacility);
                                Condesus.EMS.Business.DS.Entities.Presence _presence = _post.Presence(_facility);
                                _post.Remove(_presence);

                                //EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_idPerson).Post(_idGeographicArea, _idPosition, _idFunctionalArea).PresencesRemove(_idFacility);
                            }
                        }
                    }
                }
                else
                {
                    //carga la grilla seleccionada
                    int _radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex" + _Entity]);
                    //carga la tabla seleccionada
                    string _uId = Request.Form["radGridClickedTableId"+_Entity];

                    //Obtiene el item de la grilla que se selecciono.
                    GridTableView _tableView = this.Page.FindControl(_uId) as GridTableView;
                    String _strIdPresence = ((GridDataItem)((_tableView.Controls[0] as Table).Rows[_radGridClickedRowIndex] as GridItem)).GetDataKeyValue("IdPresence").ToString();

                    String[] _id = _strIdPresence.Split(',');
                    Int64 _idGeographicArea = Convert.ToInt64(_id[0]);
                    Int64 _idFunctionalArea = Convert.ToInt64(_id[1]);
                    Int64 _idPosition = Convert.ToInt64(_id[2]);
                    Int64 _idFacility = Convert.ToInt64(_id[3]);
                    Int64 _idPerson = Convert.ToInt64(_id[4]);

                    //De esta forma obtenemos el ID ( la PK ) de la Fila en la que estamos para 
                    //luego utilizar en nuestro objeto para eliminar el registro
                    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                    Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                    Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                    Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                    Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                    Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                    Condesus.EMS.Business.DS.Entities.Post _post = ((Condesus.EMS.Business.DS.Entities.PersonwithUser)_organization.Person(_idPerson)).Post(_jobTitle);
                    Condesus.EMS.Business.GIS.Entities.Facility _facility = _organization.Facility(_idFacility);
                    Condesus.EMS.Business.DS.Entities.Presence _presence = _post.Presence(_facility);

                    _post.Remove(_presence);
                }

                //Recarga la grilla sin el/los registros borrados
                rgdMasterGridPresences.DataSource = LoadGrid();
                rgdMasterGridPresences.MasterTableView.Rebind();
                //oculta el popup.
                this.mpelbDeletePresence.Hide();
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
