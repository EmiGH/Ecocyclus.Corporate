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


namespace Condesus.EMS.WebUI.DS
{
    public partial class UsersProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
            }
            private Int64 _IdPerson
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdPerson") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPerson")) : Convert.ToInt64(GetPKfromNavigator("IdPerson"));
                }
            }
            private User _Entity = null;
            private User Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            _Entity = ((PersonwithUser)EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson)).User;
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }
                set { _Entity = value; }
            }

            private RadComboBox _RdcPerson;
            CompareValidator _CvPerson;
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
                btnSavePassword.Click+=new EventHandler(btnSavePassword_Click);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboPeople();

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);

                    //Locales, solo para esto...
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);


                    lblOrganizationValue.Text = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).CorporateName;
                    if (_IdPerson > 0)
                    {
                        _RdcPerson.Enabled = false;

                    }
                    else
                    {
                        _RdcPerson.Enabled = true;
                        FilterComboPerson();
                    }
                }
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
                    if (Entity != null)
                    {
                        String _title = Entity.Person.FirstName + ' ' + Entity.Person.LastName;
                        base.PageTitle = _title;
                    }
                    else
                    {
                        base.PageTitle = Resources.CommonListManage.User;
                    }
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
                Page.Title = Resources.CommonListManage.User;
                lbChangePassword.Text = Resources.CommonListManage.ChangePassword;
                lblActive.Text = Resources.CommonListManage.Active;
                lblCannotChangePassword.Text = Resources.CommonListManage.CannotChangePassword;
                lblChangePasswordOnNextLogin.Text = Resources.CommonListManage.ChangePasswordOnNextLogin;
                lblConfirmPassword.Text = Resources.CommonListManage.ConfirmPassword;
                lblLastAccess.Text = Resources.CommonListManage.LastAccess;
                lblLastIP.Text = Resources.CommonListManage.LastIP;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblPassword.Text = Resources.CommonListManage.Password;
                lblPasswordNeverExpires.Text = Resources.CommonListManage.PasswordNeverExpires;
                lblPerson.Text = Resources.CommonListManage.Person;
                lblUserName.Text = Resources.CommonListManage.UserName;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv3.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                cvConfirmPassword.ErrorMessage = Resources.ConstantMessage.ValidationPassword;
                lblViewGlobalMenu.Text = Resources.CommonListManage.UseGlobalMenu;
            }
            private void FilterComboPerson()
            {
                RadComboBox _combo = new RadComboBox();

                foreach (Person _item in EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).People.Values)
                {
                    if ((_item != null) && (_item.GetType().Name == "PersonwithUser"))
                    {
                        String _selectedValue = "IdPerson=" + _item.IdPerson.ToString() + "& " + "IdOrganization=" + _IdOrganization.ToString() + "& " + "IdSalutationType=" + _item.SalutationType.IdSalutationType.ToString();

                        RadComboBoxItem _rdcItem = _RdcPerson.FindItemByValue(_selectedValue);
                        //RadComboBoxItem _rdcItem = new RadComboBoxItem(_item.FullName, _selectedValue);

                        _RdcPerson.Items.Remove(_rdcItem);
                    }
                }
            }
            private void AddComboPeople()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                String _selectedValue = String.Empty;
                if (_IdPerson > 0)
                {
                    Person _person = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson);
                    _selectedValue = "IdPerson=" + _IdPerson.ToString() + "& " + "IdOrganization=" + _IdOrganization.ToString() + "& " + "IdSalutationType=" + _person.SalutationType.IdSalutationType.ToString();
                }
                AddCombo(phPerson, ref _RdcPerson, Common.ConstantsEntitiesName.DS.People, _selectedValue, _params, false, true, false, false, false);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.People, phPersonValidator, ref _CvPerson, _RdcPerson, Resources.ConstantMessage.SelectAPerson);

                if (_IdPerson > 0)
                {
                    _RdcPerson.SelectedValue = _selectedValue;
                }
            }
            private void Add()
            {
                base.StatusBar.Clear();

                txtUserName.Text = String.Empty;
                lblLastIPValue.Text = String.Empty;
                lblLastAccessValue.Text = String.Empty;
                chkActive.Checked = false;
                chkCannotChangePassword.Checked = false;
                chkChangePasswordOnNextLogin.Checked = false;
                chkPasswordNeverExpires.Checked = false;
                chkViewGlobalMenu.Checked = true;

                btnSavePassword.Style.Add("display", "none");
                cpeChangePassword.Collapsed = false;
            }
            private void LoadData()
            {
                txtUserName.Text = Entity.Username;
                lblLastIPValue.Text = Entity.LastIP;
                if (Entity.LastLogin != DateTime.MinValue)
                {
                    lblLastAccessValue.Text = Entity.LastLogin.ToString();
                }
                chkActive.Checked = Entity.Active;
                chkCannotChangePassword.Checked = Entity.CannotChangePassword;
                chkChangePasswordOnNextLogin.Checked = Entity.ChangePasswordOnNextLogin;
                chkPasswordNeverExpires.Checked = Entity.PasswordNeverExpires;
                chkViewGlobalMenu.Checked = Entity.ViewGlobalMenu;

                btnSavePassword.Style.Add("display", "block");
                cpeChangePassword.Collapsed = true;

                rfv2.EnableClientScript = false;
                rfv3.EnableClientScript = false;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Int64 _idPerson = Convert.ToInt64(GetKeyValue(_RdcPerson.SelectedValue, "IdPerson"));

                    if (Entity == null)
                    {
                        //Alta
                        Entity = ((Condesus.EMS.Business.DS.Entities.PersonwithoutUser)EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_idPerson)).UsersAdd(txtUserName.Text, txtPassword.Text, chkActive.Checked, chkChangePasswordOnNextLogin.Checked, chkCannotChangePassword.Checked, chkPasswordNeverExpires.Checked, chkViewGlobalMenu.Checked);
                    }
                    else
                    {
                        //Modificacion
                        ((Condesus.EMS.Business.DS.Entities.PersonwithUser)EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_idPerson)).UsersModify(txtUserName.Text, chkActive.Checked, chkChangePasswordOnNextLogin.Checked, chkCannotChangePassword.Checked, chkPasswordNeverExpires.Checked, chkViewGlobalMenu.Checked);
                    }
                    base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                    base.NavigatorAddTransferVar("IdPerson", _idPerson);

                    String _pkValues = "IdOrganization=" + _IdOrganization.ToString()
                        + "& IdPerson=" + _idPerson.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.User);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.User + " " + Entity.Username, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.Username);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.User, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
            protected void btnSavePassword_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity != null)
                    {
                        ((PersonwithUser)EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson)).UsersModify(txtUserName.Text, txtPassword.Text);
                        base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                        base.NavigatorAddTransferVar("IdPerson", _IdPerson);

                        String _pkValues = "IdOrganization=" + _IdOrganization.ToString()
                            + "& IdPerson=" + _IdPerson.ToString();
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.User);
                        base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                        //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.User + " " + Entity.Username);
                        String _entityPropertyName = String.Concat(Entity.Username);
                        NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.User, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                        base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                    }
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion

    }
}
