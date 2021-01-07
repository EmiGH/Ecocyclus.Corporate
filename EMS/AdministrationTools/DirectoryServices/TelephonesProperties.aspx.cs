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
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class TelephonesProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdFacility
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFacility") ? base.NavigatorGetTransferVar<Int64>("IdFacility") : Convert.ToInt64(GetPKfromNavigator("IdFacility"));
                }
            }
            private Int64 _IdSector
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdSector") ? base.NavigatorGetTransferVar<Int64>("IdSector") : Convert.ToInt64(GetPKfromNavigator("IdSector"));
                }
            }
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdTelephone") ? base.NavigatorGetTransferVar<Int64>("IdTelephone") : 0;
                }
            }
            private Int64 _IdOrganization
            {
                get
                {
                    object _o = ViewState["IdOrganization"];
                    if (_o != null)
                        return (Int64)ViewState["IdOrganization"];

                    return 0;
                }

                set
                {
                    ViewState["IdOrganization"] = value;
                }
            }
            private Int64 _IdPerson
            {
                get
                {
                    object _o = ViewState["IdPerson"];
                    if (_o != null)
                        return (Int64)ViewState["IdPerson"];

                    return 0;
                }

                set
                {
                    ViewState["IdPerson"] = value;
                }
            }
            private Telephone _Entity = null;
            private Telephone Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            if (_IdPerson > 0)
                            {
                                _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).Telephone(_IdEntity);
                            }
                            else
                            {
                                if (_IdSector > 0)
                                {
                                    _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector).Telephone(_IdEntity);
                                }
                                else
                                {
                                    _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Telephone(_IdEntity);
                                }
                            }
                        }

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    InitFkVars();
                    String _dataObjects = String.Empty;
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    {
                        LoadData(); //Edit.
                    }

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtAreaCode.Focus();
                }
            }
            private void InitFkVars()
            {
                //Aca intenta obtener el/los Id desde el TransferVar, si no esta ahi, entonces lo busca en las PKEntity.
                _IdOrganization = base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                _IdPerson = NavigatorContainsTransferVar("IdPerson") ? base.NavigatorGetTransferVar<Int64>("IdPerson") : Convert.ToInt64(GetPKfromNavigator("IdPerson"));
            }
            protected override void SetPagetitle()
            {
                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                String _pageTitle = Convert.ToString(GetPKfromNavigator("PageTitle"));
                if (_pageTitle != "0")
                {
                    base.PageTitle = _pageTitle;
                }
                else
                {
                    String _title = String.Empty;
                    if (Entity != null)
                    {
                        _title = Entity.AreaCode + ' ' + Entity.Number + " - " + Entity.Extension;
                    }
                    base.PageTitle = (Entity != null) ? _title : Resources.CommonListManage.Telephone;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Telephone;
                lblInternationalCode.Text = Resources.CommonListManage.InternationalCode;
                lblAreaCode.Text = Resources.CommonListManage.AreaCode;
                lblExtension.Text = Resources.CommonListManage.Extension;
                lblNumber.Text = Resources.CommonListManage.Number;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rev.ErrorMessage = Resources.ConstantMessage.ValidationInvalidData;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtInternationalCode.Text = String.Empty;
                txtAreaCode.Text = String.Empty;
                txtNumber.Text = String.Empty;
                txtExtension.Text = String.Empty;
                txtReason.Text = String.Empty;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtInternationalCode.Text = Entity.InternationalCode;
                txtAreaCode.Text = Entity.AreaCode;
                txtNumber.Text = Entity.Number;
                txtExtension.Text = Entity.Extension;
                txtReason.Text = Entity.Reason;
            }
            private Telephone AddEntity()
            {
                if (_IdPerson > 0)
                {
                    return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).TelephoneAdd(txtAreaCode.Text, txtNumber.Text, txtExtension.Text, txtInternationalCode.Text, txtReason.Text);
                }
                else
                {
                    if (_IdSector > 0)
                    {
                        return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector).TelephoneAdd(txtAreaCode.Text, txtNumber.Text, txtExtension.Text, txtInternationalCode.Text, txtReason.Text);
                    }
                    else
                    {
                        return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).TelephoneAdd(txtAreaCode.Text, txtNumber.Text, txtExtension.Text, txtInternationalCode.Text, txtReason.Text);
                    }
                }
            }
            private void ModifyEntity()
            {
                if (_IdPerson > 0)
                {
                    Entity.Modify(EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson), txtAreaCode.Text, txtNumber.Text, txtExtension.Text, txtInternationalCode.Text, txtReason.Text);
                }
                else
                {
                    if (_IdSector > 0)
                    {
                        Entity.Modify(EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector), txtAreaCode.Text, txtNumber.Text, txtExtension.Text, txtInternationalCode.Text, txtReason.Text);
                    }
                    else
                    {
                        Entity.Modify(EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility), txtAreaCode.Text, txtNumber.Text, txtExtension.Text, txtInternationalCode.Text, txtReason.Text);
                    }
                }
            }
            private void RemoveEntity()
            {
                if (_IdPerson > 0)
                {
                    EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).Remove(Entity);
                }
                else
                {
                    if (_IdSector > 0)
                    {
                        EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector).Remove(Entity);
                    }
                    else
                    {
                        EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Remove(Entity);
                    }
                }
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity == null)
                    {
                        //Alta
                        Entity = AddEntity();
                    }
                    else
                    {
                        //Modificacion
                        ModifyEntity();
                    }
                    base.NavigatorAddTransferVar("IdPerson", _IdPerson);
                    base.NavigatorAddTransferVar("IdTelephone", Entity.IdTelephone);
                    base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                    base.NavigatorAddTransferVar("IdFacility", _IdFacility);
                    base.NavigatorAddTransferVar("IdSector", _IdSector);

                    String _pkValues = "IdOrganization=" + _IdOrganization.ToString()
                            + "& IdPerson=" + _IdPerson.ToString()
                            + "& IdTelephone=" + Entity.IdTelephone.ToString()
                            + "& IdFacility=" + _IdFacility.ToString()
                            + "& IdSector=" + _IdSector.ToString();

                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Telephone);
                    if (Convert.ToString(GetPKfromNavigator("ParentEntity")) != "0")
                    {
                        base.NavigatorAddTransferVar("ParentEntity", Convert.ToString(GetPKfromNavigator("ParentEntity")));
                    }
                    else
                    {
                        if (base.NavigatorContainsTransferVar("ParentEntity"))
                        {
                            base.NavigatorAddTransferVar("ParentEntity", base.NavigatorGetTransferVar<String>("ParentEntity"));
                        }
                    }

                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                    String _entityPropertyName = String.Concat("(", Entity.AreaCode, ")", Entity.Number, " - ", Entity.Extension);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Telephone, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion       

    }
}

