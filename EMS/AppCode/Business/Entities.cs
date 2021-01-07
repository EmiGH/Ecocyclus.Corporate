using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.IA.Entities;
using Condesus.EMS.Business.RM.Entities;
using Condesus.EMS.Business.CT.Entities;
using Condesus.EMS.Business.Security.Entities;

namespace Condesus.EMS.WebUI.Business
{
    public partial class Entities : Base
    {
        #region Internal Properties
        //protected Condesus.EMS.Business.EMS _EMSLibrary = null;
        #endregion

        //public Entities(Condesus.EMS.Business.EMS emsLibrary, String commandName)
        public Entities(String commandName)
        {
            //_EMSLibrary = emsLibrary;
            _CommandName = commandName;
        }
        //public Entities Create(Condesus.EMS.Business.EMS emsLibrary, String commandName)
        public Entities Create(String commandName)
        {
            //return new Entities(emsLibrary, commandName);
            return new Entities(commandName);
        }

        #region Private Methods
        private void SetParameters(String entityNameGrid, String entityName, String entityNameContextInfo, String entityNameContextElement, String entityNameHierarchical, String entityNameHierarchicalChildren,
                        String entityNameComboFilter, Boolean isFilterHierarchy, String entityNameChildrenComboFilter,
                        String entityNameMapClassification, String entityNameMapClassificationChildren, String entityNameMapElement,
                        String entityNameMapElementChildren, String selectedValueDefaultComboBox)
        {
            NavigatorAddTransferVar("EntityNameGrid", entityNameGrid);
            NavigatorAddTransferVar("EntityName", entityName);
            NavigatorAddTransferVar("EntityNameContextInfo", entityNameContextInfo);
            NavigatorAddTransferVar("EntityNameContextElement", entityNameContextElement);
            NavigatorAddTransferVar("EntityNameComboFilter", entityNameComboFilter);
            NavigatorAddTransferVar("EntityNameHierarchical", entityNameHierarchical);
            NavigatorAddTransferVar("EntityNameHierarchicalChildren", entityNameHierarchicalChildren);
            NavigatorAddTransferVar("IsFilterHierarchy", isFilterHierarchy);
            NavigatorAddTransferVar("EntityNameChildrenComboFilter", entityNameChildrenComboFilter);
            NavigatorAddTransferVar("EntityNameMapClassification", entityNameMapClassification);
            NavigatorAddTransferVar("EntityNameMapClassificationChildren", entityNameMapClassificationChildren);
            NavigatorAddTransferVar("EntityNameMapElement", entityNameMapElement);
            NavigatorAddTransferVar("EntityNameMapElementChildren", entityNameMapElementChildren);

            ////Finalmente hace el Navigate al Manage Correspondiente.
            //var argsColl = new Dictionary<String, String>();
            //argsColl.Add("EntityName", entityName);
            //argsColl.Add("EntityNameGrid", entityNameGrid);
            //argsColl.Add("EntityNameHierarchical", entityNameHierarchical);
            //argsColl.Add("EntityNameHierarchicalChildren", entityNameHierarchicalChildren);

            //Condesus.WebUI.Navigation.NavigateMenuEventArgs _menuArgs = new Condesus.WebUI.Navigation.NavigateMenuEventArgs("config", argsColl);
            //Navigate(url, entityName, _menuArgs);
            //Navigate(url, entityName);
        }
        #endregion

        #region Public Methods (Aca estan los metodos que via Reflection nos permite evitar el SWITCH)

        #region Directory Service

        #region Applicabilities
        public void Applicability_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idApplicabilityContactType = Convert.ToInt64(param["IdApplicabilityContactType"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.ApplicabilityContactType(_idApplicabilityContactType).LanguagesOptions.Remove(_idLanguage);
        }
        public String Applicability(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Applicabilities_LG, Common.ConstantsEntitiesName.DS.Applicability_LG, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Applicability_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            if (isViewer)
            {
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }

            return _itemsMenu;
        }
        #endregion

        #region Contact Addresses
        public void AddressRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idContactAddress = Convert.ToInt64(param["IdAddress"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            String _parentEntity = param["ParentEntity"].ToString();
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            Int64 _idFacility = 0;

            //Ejecuta el metodo para eliminar el registro.
            switch (_parentEntity)
            {
                case "Facility":
                    _idFacility = Convert.ToInt64(param["IdFacility"]);
                    Facility _facility = _organization.Facility(_idFacility);
                    _facility.Remove(_facility.Address(_idContactAddress));
                    break;
                case "Sector":
                    _idFacility = Convert.ToInt64(param["IdFacility"]);
                    Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                    Sector _sector = _organization.Facility(_idFacility).Sector(_idSector);
                    _sector.Remove(_sector.Address(_idContactAddress));
                    break;
                case "Person":
                    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
                    Person _person = _organization.Person(_idPerson);
                    _person.Remove(_person.Address(_idContactAddress));
                    break;
            }
        }
        public String Address(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Addresses, Common.ConstantsEntitiesName.DS.Address, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Address_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Contact Emails
        public void ContactEmailRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idContactEmail = Convert.ToInt64(param["IdContactEmail"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            String _parentEntity = param["ParentEntity"].ToString();
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            ContactEmail _contactEmail = _organization.ContactEmail(_idContactEmail);

            //Ejecuta el metodo para eliminar el registro.
            switch (_parentEntity)
            {
                case "Organization":
                    _organization.Remove(_contactEmail);
                    break;
                case "Person":
                    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
                    _organization.Person(_idPerson).Remove(_contactEmail);
                    break;
            }
        }
        public String ContactEmail(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.ContactEmails, Common.ConstantsEntitiesName.DS.ContactEmail, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ContactEmail_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Contact Messenger Applications
        public void ContactMessengerApplicationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            String _provider = param["Provider"].ToString();
            String _application = param["Application"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.ContactMessengersApplicationsRemove(_provider, _application);
        }
        public String ContactMessengerApplication(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.ContactMessengerApplications, Common.ConstantsEntitiesName.DS.ContactMessengerApplication, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ContactMessengerApplication_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Contact Messenger Providers
        public void ContactMessengerProviderRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            String _provider = param["Provider"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.ContactMessengersProvidersRemove(_provider);
        }
        public String ContactMessengerProvider(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.ContactMessengerProviders, Common.ConstantsEntitiesName.DS.ContactMessengerProvider, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ContactMessengerProvider_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Contact Messengers
        public void ContactMessengerRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idContactMessenger = Convert.ToInt64(param["IdContactMessenger"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            String _parentEntity = param["ParentEntity"].ToString();
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            ContactMessenger _contactMessenger = _organization.ContactMessenger(_idContactMessenger);

            //Ejecuta el metodo para eliminar el registro.
            switch (_parentEntity)
            {
                case "Organization":
                    _organization.Remove(_contactMessenger);
                    break;
                case "Person":
                    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
                    _organization.Person(_idPerson).Remove(_contactMessenger);
                    break;
            }
        }
        public String ContactMessenger(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.ContactMessengers, Common.ConstantsEntitiesName.DS.ContactMessenger, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ContactMessenger_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Contact Types
        public void ContactTypeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idContactType = Convert.ToInt64(param["IdContactType"]);
            Int64 _idApplicability = Convert.ToInt64(param["IdApplicability"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.ContactTypesRemove(_idApplicability, _idContactType);
        }
        public void ContactType_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idApplicability = Convert.ToInt64(param["IdApplicability"]);
            Int64 _idContactType = Convert.ToInt64(param["IdContactType"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.ContactType(_idApplicability, _idContactType).LanguagesOptions.Remove(_idLanguage);
        }
        public String ContactType(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.ContactTypes, Common.ConstantsEntitiesName.DS.ContactType, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.Applicabilities, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ContactType_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Contact Telephones
        public void TelephoneRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idContactTelephone = Convert.ToInt64(param["IdTelephone"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            String _parentEntity = param["ParentEntity"].ToString();
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            Int64 _idFacility = 0;

            //Ejecuta el metodo para eliminar el registro.
            switch (_parentEntity)
            {
                case "Facility":
                    _idFacility = Convert.ToInt64(param["IdFacility"]);
                    Facility _facility = _organization.Facility(_idFacility);
                    _facility.Remove(_facility.Telephone(_idContactTelephone));
                    break;
                case "Sector":
                    _idFacility = Convert.ToInt64(param["IdFacility"]);
                    Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                    Sector _sector = _organization.Facility(_idFacility).Sector(_idSector);
                    _sector.Remove(_sector.Telephone(_idContactTelephone));
                    break;
                case "Person":
                    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
                    Person _person = _organization.Person(_idPerson);
                    _person.Remove(_person.Telephone(_idContactTelephone));
                    break;
            }
        }
        public String Telephone(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Telephones, Common.ConstantsEntitiesName.DS.Telephone, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Telephone_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Contact Urls
        public void ContactUrlRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idContactUrl = Convert.ToInt64(param["IdContactURL"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            String _parentEntity = param["ParentEntity"].ToString();

            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            ContactURL _contactURL = _organization.ContactURL(_idContactUrl);
            //Ejecuta el metodo para eliminar el registro.
            switch (_parentEntity)
            {
                case "Organization":
                    _organization.Remove(_contactURL);
                    break;
                case "Person":
                    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
                    _organization.Person(_idPerson).Remove(_contactURL);
                    break;
            }
        }
        public void ContactUrl_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            Int64 _idContactUrl = Convert.ToInt64(param["IdContactURL"]);
            String _idLanguage = param["IdLanguage"].ToString();
            String _parentEntity = param["ParentEntity"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            switch (_parentEntity)
            {
                case "Person":
                    EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Person(_idPerson).ContactURL(_idContactUrl).LanguagesOptions.Remove(_idLanguage);
                    break;
                case "Organization":
                    EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).ContactURL(_idContactUrl).LanguagesOptions.Remove(_idLanguage);
                    break;
            }
        }
        public String ContactUrl(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.ContactURLs, Common.ConstantsEntitiesName.DS.ContactURL, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ContactUrl_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion
        
        #region Functional Areas
        public void FunctionalAreaRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);

            _organization.Remove(_funArea);
        }
        public void FunctionalArea_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idFacility = Convert.ToInt64(param["IdFunctionalArea"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFacility);

            _funArea.LanguagesOptions.Remove(_idLanguage);
        }

        public String FunctionalArea(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.FunctionalArea, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.FunctionalAreas, Common.ConstantsEntitiesName.DS.FunctionalAreaChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> FunctionalArea_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Functional Positions
        public void FunctionalPositionRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);

            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            Position _position = _organization.Position(_idPosition);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);

            _organization.Remove(_funPos);
        }
        public String FunctionalPosition(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.FunctionalPosition, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.FunctionalPositions, Common.ConstantsEntitiesName.DS.FunctionalPositionChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> FunctionalPosition_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Geographic Areas
        public void GeographicAreaRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            //Ejecuta el metodo para eliminar el registro.
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            
            EMSLibrary.User.GeographicInformationSystem.Remove(_geoArea);
        }
        public void GeographicArea_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea).LanguagesOptions.Remove(_idLanguage);
        }
        public String GeographicArea(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.GeographicArea, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.GeographicAreas, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> GeographicArea_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Facilities
            public void FacilityRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                //Ejecuta el metodo para eliminar el registro.
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                Facility _facility = _organization.Facility(_idFacility);
             
                _organization.Remove(_facility);
            }
            public void Facility_LGRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                String _idLanguage = param["IdLanguage"].ToString();
                //Ejecuta el metodo para eliminar el registro.
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                Facility _facility = _organization.Facility(_idFacility);

                _facility.LanguagesOptions.Remove(_idLanguage);
            }
            public String Facility(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> Facility_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (param.ContainsKey("IdOrganization"))
                {
                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }
                else
                {   //Como no viene el ID, entonces directamente miro el mapa.
                    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                    _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> Facility_LG_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (param.ContainsKey("IdOrganization"))
                {
                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }
                else
                {   //Como no viene el ID, entonces directamente miro el mapa.
                    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
        #endregion

        #region Sectors
            public void SectorRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                //Ejecuta el metodo para eliminar el registro.
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                Facility _facility = _organization.Facility(_idFacility);
             
                _facility.Remove(_facility.Sector(_idSector));
            }
            public void Sector_LGRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                String _idLanguage = param["IdLanguage"].ToString();
                //Ejecuta el metodo para eliminar el registro.
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                Facility _facility = _organization.Facility(_idFacility);

                _facility.Sector(_idSector).LanguagesOptions.Remove(_idLanguage);
            }
            public String Sector(Dictionary<String, Object> param)
            {
                SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.Sector, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.Sectors, Common.ConstantsEntitiesName.DS.SectorsChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/HierarchicalListManage.aspx";
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> Sector_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (param.ContainsKey("IdOrganization"))
                {
                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }
                else
                {   //Como no viene el ID, entonces directamente miro el mapa.
                    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                    _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> Sector_LG_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (param.ContainsKey("IdOrganization"))
                {
                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }
                else
                {   //Como no viene el ID, entonces directamente miro el mapa.
                    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
        #endregion

        #region Geographic Functional Areas
        public void GeographicFunctionalAreaRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);

            _organization.Remove(_geoFunArea);
        }
        public String GeographicFunctionalArea(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.GeographicFunctionalArea, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.GeographicFunctionalAreas, Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> GeographicFunctionalArea_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Job Titles
        public void JobTitleRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);

            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            Position _position = _organization.Position(_idPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            //if (param.ContainsKey("IdOrganizationalChart"))
            //{
            //    //Quiere decir que tengo que borrar el JT que esta en ese Organigrama.
            //    Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);
            //    _organization.OrganizationalChart(_idOrganizationalChart).Remove(_jobTitle);
            //}
            //else
            //{
                //Borra el JT de toda la organizacion.
                _organization.Remove(_jobTitle);
            //}
        }
        public String JobTitle(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.JobTitle, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.JobTitles, Common.ConstantsEntitiesName.DS.JobTitleChildren, Common.ConstantsEntitiesName.DS.OrganizationalCharts, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> JobTitle_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Languages
        public void LanguageRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.LanguagesRemove(_idLanguage);
        }
        public String Language(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Languages, Common.ConstantsEntitiesName.DS.Language, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Language_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Organization Classification
        public void OrganizationClassificationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
            if (!param.ContainsKey("IdOrganization"))
            {
                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.DirectoryServices.Map.Remove(EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification));
            }
            else
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                //Ejecuta el metodo para eliminar el registro.
                OrganizationClassification _organizationClassification = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification);
                EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Remove(_organizationClassification);
            }
        }
        public void OrganizationClassification_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).LanguagesOptions.Remove(_idLanguage);
        }
        public String OrganizationClassification(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassification, String.Empty, Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            //Navega al default de mapa de DS- no hay manage de org.
            return "~/ManagementTools/DirectoryServices/Map.aspx";  //return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> OrganizationClassification_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Organization Relationship Types
        public void OrganizationRelationshipTypeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganizationRelationshipType = Convert.ToInt64(param["IdOrganizationRelationshipType"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.OrganizationsRelationshipsTypesRemove(_idOrganizationRelationshipType);
        }
        public void OrganizationRelationshipType_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganizationRelationshipType = Convert.ToInt64(param["IdOrganizationRelationshipType"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.OrganizationRelationshipType(_idOrganizationRelationshipType).LanguagesOptions.Remove(_idLanguage);
        }
        public String OrganizationRelationshipType(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassification, String.Empty, Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> OrganizationRelationshipType_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)                    
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Organization Relationships
        public void OrganizationRelationshipRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganizationRelationshipType = Convert.ToInt64(param["IdOrganizationRelationshipType"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idOrganization2 = Convert.ToInt64(param["IdOrganization2"]);
            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            OrganizationRelationship _organizationRelationship = _organization.OrganizationRelationship(_idOrganization, _idOrganization2, _idOrganizationRelationshipType);

            _organization.Remove(_organizationRelationship);
        }
        public String OrganizationRelationship(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.OrganizationRelationshipTypes, Common.ConstantsEntitiesName.DS.OrganizationRelationshipType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> OrganizationRelationship_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Organizations
        public void OrganizationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Map.Remove(EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization));
        }
        public String Organization(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Organizations, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organization, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.OrganizationClassifications, true, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            //Navega al default de mapa de DS- no hay manage de org.
            return "~/ManagementTools/DirectoryServices/Map.aspx";  //"~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Organization_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Organization_MenuSecurity(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            _itemsMenu.Add("rmiSSJobTitles", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSJobTitle, _isEnabled));
            _itemsMenu.Add("rmiSSPerson", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSPerson, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Organizationalchart
        public void OrganizationalChartRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idOrganizationChart = Convert.ToInt64(param["IdOrganizationalChart"]);
            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            _organization.Remove(_organization.OrganizationalChart(_idOrganizationChart));
        }
        public void OrganizationalChart_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idOrganizationChart = Convert.ToInt64(param["IdOrganizationalChart"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            _organization.OrganizationalChart(_idOrganizationChart).LanguagesOptions.Remove(Global.Languages[_idLanguage]);
        }
        public String OrganizationalChart(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.OrganizationalCharts, Common.ConstantsEntitiesName.DS.OrganizationalChart, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> OrganizationalChart_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region People
        public void PersonRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            _organization.Remove(_organization.Person(_idPerson));
        }
        public String Person(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.People, Common.ConstantsEntitiesName.DS.Person, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Person_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Positions
        public void PositionRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            Position _position = _organization.Position(_idPosition);

            EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Remove(_position);
        }
        public void Position_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Position(_idPosition).LanguagesOptions.Remove(_idLanguage);
        }
        public String Position(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Positions, Common.ConstantsEntitiesName.DS.Position, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Position_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Posts
        public void PostRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            Position _position = _organization.Position(_idPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            PersonwithUser _person = (Condesus.EMS.Business.DS.Entities.PersonwithUser)EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Person(_idPerson);
            _person.Remove(_person.Post(_jobTitle));
        }
        public String Post(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Posts, Common.ConstantsEntitiesName.DS.Post, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Post_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Presences
        public void PresenceRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);

            //Ejecuta el metodo para eliminar el registro.
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            Position _position = _organization.Position(_idPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            Post _post = ((Condesus.EMS.Business.DS.Entities.PersonwithUser)_organization.Person(_idPerson)).Post(_jobTitle);
            Facility _facility = _organization.Facility(_idFacility);
            Presence _presence = _post.Presence(_facility);

            _post.Remove(_presence);
        }
        public String Presence(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Presence, Common.ConstantsEntitiesName.DS.Presence, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Presence_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)                    
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Responsibilities
        public void ResponsibilityRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            Int64 _idGeographicAreaResponsibility = Convert.ToInt64(param["IdGeographicAreaResponsibility"]);
            Int64 _idFunctionalAreaResponsibility = Convert.ToInt64(param["IdFunctionalAreaResponsibility"]);

            //FunctionalArea _funArea = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).FunctionalArea(_idFunctionalAreaResponsibility);
            //GeographicArea _geoArea = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).GeographicArea(_idGeographicAreaResponsibility);

            //Ejecuta el metodo para eliminar el registro.
            //EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea).ResponsibilitiesRemove(_idFunctionalAreaResponsibility, _idGeographicAreaResponsibility);
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicAreaResponsibility);
            Position _position = _organization.Position(_idPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalAreaResponsibility);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
            _jobTitle.Remove(_jobTitle.Responsibility(_funArea, _geoArea));
        }
        public String Responsibility(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Responsibilities, Common.ConstantsEntitiesName.DS.Responsibility, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Responsibility_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Salutation Types
        public void SalutationTypeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idSalutationType = Convert.ToInt64(param["IdSalutationType"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.SalutationTypesRemove(_idSalutationType);
        }
        public void SalutationType_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idSalutationType = Convert.ToInt64(param["IdSalutationType"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.SalutationType(_idSalutationType).LanguagesOptions.Remove(_idLanguage);
        }
        public String SalutationType(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.SalutationTypes, Common.ConstantsEntitiesName.DS.SalutationType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> SalutationType_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)                    
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Users
        public void UserRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            //Ejecuta el metodo para eliminar el registro.
            PersonwithUser _personWithUser = (PersonwithUser)EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Person(_idPerson);
            User _user = _personWithUser.User;
            _personWithUser.Remove(_user);
        }
        public String User(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.DS.Users, Common.ConstantsEntitiesName.DS.User, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> User_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            //Solo sale el add, si viene con el idPerson.
            if (param.ContainsKey("IdPerson"))
            {
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            }
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Organization Extended Property
            public void OrganizationExtendedPropertyRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
                //Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                //Ejecuta el metodo para eliminar el registro.
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                _organization.Remove(_organization.ExtendedPropertyValue(_idExtendedProperty));
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> OrganizationExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (param.ContainsKey("IdOrganization"))
                {
                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }
                else
                {   //Como no viene el ID, entonces directamente miro el mapa.
                    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public String OrganizationExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.DS.OrganizationExtendedProperties, Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty, Common.ConstantsEntitiesName.DS.Organization, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
        #endregion

            #region FacilityTypes
                public void FacilityTypeRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idFacilityType = Convert.ToInt64(param["IdFacilityType"]);
                    //Ejecuta el metodo para eliminar el registro.
                    FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType);

                    EMSLibrary.User.GeographicInformationSystem.Remove(_facilityType);
                }
                public void FacilityType_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idFacilityType = Convert.ToInt64(param["IdFacilityType"]);
                    String _idLanguage = param["IdLanguage"].ToString();

                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String FacilityType(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.DS.FacilityTypes, Common.ConstantsEntitiesName.DS.FacilityType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> FacilityType_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    //Como no viene el ID, entonces directamente miro el mapa.
                    if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

        #endregion

        #region Improvement Actions

        #region Project Classifications
        public void ProjectClassificationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ImprovementAction.Map.Remove(EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification));
        }
        public void ProjectClassification_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification).LanguagesOptions.Remove(_idLanguage);
        }
        public String ProjectClassification(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.IA.ProjectClassification, String.Empty, String.Empty, Common.ConstantsEntitiesName.IA.ProjectClassifications, Common.ConstantsEntitiesName.IA.ProjectClassificationChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/ManagementTools/PerformanceAssessment/Map.aspx";  //return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ProjectClassification_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }
        
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #endregion

        #region Knowledge Collaboration

        #region Resource Classifications
        public void ResourceClassificationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.Remove(EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification));
        }
        public void ResourceClassification_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification).LanguagesOptions.Remove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
        }
        public String ResourceClassification(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassification, String.Empty, Common.ConstantsEntitiesName.KC.ResourceClassifications, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/ManagementTools/KnowledgeCollaboration/Map.aspx";  //return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ResourceClassification_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }
        
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Resource File States
        public void ResourceFileStateRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResourceFileState = Convert.ToInt64(param["IdResourceFileState"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceHistoryStateRemove(_idResourceFileState);
        }
        public void ResourceFileState_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResourceFileState = Convert.ToInt64(param["IdResourceFileState"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceHistoryState(_idResourceFileState).LanguagesOptions.Remove(_idLanguage);
        }
        public String ResourceHistoryState(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.KC.ResourceFileStates, Common.ConstantsEntitiesName.KC.ResourceHistoryState, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ResourceFileState_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Resource Files = Resource Catalogues
        public void ResourceVersionRemove(Dictionary<String, Object> param)
        {
            ResourceFileRemove(param);
        }
        public void ResourceFileRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResource = Convert.ToInt64(param["IdResource"]);
            Int64 _idResourceFile = Convert.ToInt64(param["IdResourceFile"]);
            //Ejecuta el metodo para eliminar el registro.
            Condesus.EMS.Business.KC.Entities.ResourceVersion _resourceFile = ((Condesus.EMS.Business.KC.Entities.ResourceVersion)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource));
            Condesus.EMS.Business.KC.Entities.Version _versionFile = _resourceFile.Version(_idResourceFile);
            _resourceFile.Remove(_versionFile);

            //((Condesus.EMS.Business.KC.Entities.ResourceVersion)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource)).ResourceFileRemove(_idResourceFile);
        }
        public String ResourceFile(Dictionary<String, Object> param)
        {
            return ResourceVersion(param);
        }
        public String ResourceVersion(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.KC.ResourceFiles, Common.ConstantsEntitiesName.KC.ResourceVersion, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ResourceFile_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }
        
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ResourceVersion_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }
        
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Resource Types
        public void ResourceTypeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResourceType = Convert.ToInt64(param["IdResourceType"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceTypeRemove(EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_idResourceType));
        }
        public void ResourceType_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResourceType = Convert.ToInt64(param["IdResourceType"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_idResourceType).LanguagesOptions.Remove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
        }
        public String ResourceType(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.KC.ResourceType, String.Empty, String.Empty, Common.ConstantsEntitiesName.KC.ResourceTypes, Common.ConstantsEntitiesName.KC.ResourceTypeChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ResourceType_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Resources
        public void ResourceRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResource = Convert.ToInt64(param["IdResource"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.Remove(EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource));
        }
        public void Resource_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResource = Convert.ToInt64(param["IdResource"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).LanguagesOptions.Remove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
        }
        public void KCResourceRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResource = Convert.ToInt64(param["IdResource"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.Remove(EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource));
        }
        public void KCResource_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResource = Convert.ToInt64(param["IdResource"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).LanguagesOptions.Remove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
        }
        public String KCResource(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.KC.Resources, Common.ConstantsEntitiesName.KC.Resource, Common.ConstantsEntitiesName.KC.Resource, Common.ConstantsEntitiesName.KC.Resource, String.Empty, String.Empty, Common.ConstantsEntitiesName.KC.ResourceClassifications, true, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/ManagementTools/KnowledgeCollaboration/Map.aspx";  //return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> KCResource_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region ResourceCatalog
        public void ResourceCatalogRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idResource = Convert.ToInt64(param["IdResource"]);
            Int64 _idResourceFile = Convert.ToInt64(param["IdResourceFile"]);
            //Ejecuta el metodo para eliminar el registro.
            Condesus.EMS.Business.KC.Entities.ResourceCatalog _resourceCatalog = ((Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource));
            Condesus.EMS.Business.KC.Entities.Catalog _catalog = _resourceCatalog.Catalog(_idResourceFile);
            _resourceCatalog.Remove(_catalog);
        }
        public String ResourceCatalog(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.KC.ResourceCatalogues, Common.ConstantsEntitiesName.KC.ResourceCatalog, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ResourceCatalog_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }
        
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Resource Extended Property
            public void ResourceExtendedPropertyRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
                //Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);
                Int64 _idResource = Convert.ToInt64(param["IdResource"]);

                //Ejecuta el metodo para eliminar el registro.
                Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);
                _resource.Remove(_resource.ExtendedPropertyValue(_idExtendedProperty));
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ResourceExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public String ResourceExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.KC.ResourceExtendedProperties, Common.ConstantsEntitiesName.KC.ResourceExtendedProperty, Common.ConstantsEntitiesName.KC.Resource, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
        #endregion

        #endregion

        #region Performance Assessment

        #region Calculation Scenario Types
        public void CalculationScenarioTypeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idCalculationScenarioType = Convert.ToInt64(param["IdScenarioType"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioTypeRemove(_idCalculationScenarioType);
        }
        public void CalculationScenarioType_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idCalculationScenarioType = Convert.ToInt64(param["IdScenarioType"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(_idCalculationScenarioType).LanguagesOptions.Remove(_idLanguage);
        }
        public String CalculationScenarioType(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.CalculationScenarioTypes, Common.ConstantsEntitiesName.PA.CalculationScenarioType, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> CalculationScenarioType_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Calculations
        public void CalculationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idCalculation = Convert.ToInt64(param["IdCalculation"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.CalculationRemove(_idCalculation);
        }
        public void Calculation_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idCalculation = Convert.ToInt64(param["IdCalculation"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation).LanguagesOptions.Remove(_idLanguage);
        }
        public String Calculation(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.Calculations, Common.ConstantsEntitiesName.PA.Calculation, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Calculation_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Calculation Estimate
            public void CalculationEstimateRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idCalculation = Convert.ToInt64(param["IdCalculation"]);
                Int64 _idCalculationEstimated = Convert.ToInt64(param["IdEstimated"]);

                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation).CalculationEstimatesRemove(_idCalculationEstimated);
            }
            public String CalculationEstimate(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PA.Calculations, Common.ConstantsEntitiesName.PA.Calculation, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> CalculationEstimate_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
        #endregion

        #region Calculation Verificated
            public void CalculationCertificateRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idCalculation = Convert.ToInt64(param["IdCalculation"]);
                Int64 _idCertificated = Convert.ToInt64(param["IdCertificated"]);

                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation).CalculationCertificatesRemove(_idCertificated);
            }
            public String CalculationCertificate(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PA.Calculations, Common.ConstantsEntitiesName.PA.Calculation, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> CalculationCertificate_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
        #endregion

        #region Formulas
        public void FormulaRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idFormula = Convert.ToInt64(param["IdFormula"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.FormulaRemove(_idFormula);
        }
        public void Formula_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idFormula = Convert.ToInt64(param["IdFormula"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_idFormula).LanguageOptions.Remove(_idLanguage);
        }
        public String Formula(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.Formulas, Common.ConstantsEntitiesName.PA.Formula, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, Common.ConstantsEntitiesName.PA.IndicatorsRoots, Common.ConstantsEntitiesName.PA.Indicators, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Formula_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Indicator Classifications
        public void IndicatorClassificationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.Remove(EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification));
        }
        public void IndicatorClassification_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification).LanguagesOptions.Remove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
        }
        public String IndicatorClassification(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.PA.IndicatorClassification, Common.ConstantsEntitiesName.PA.IndicatorClassification, String.Empty, Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/ManagementTools/PerformanceAssessment/Map.aspx";  //return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> IndicatorClassification_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Indicators
        public void IndicatorRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.Remove(EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator));
        }
        public void Indicator_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).LanguagesOptions.Remove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
        }
        public String Indicator(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.Indicators, Common.ConstantsEntitiesName.PA.Indicator, Common.ConstantsEntitiesName.PA.Indicator, Common.ConstantsEntitiesName.PA.Indicator, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.IndicatorClassifications, true, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/ManagementTools/PerformanceAssessment/Map.aspx";  //return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Indicator_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Magnitudes
        public void MagnitudRemove(Dictionary<String, Object> param)
        {
            try
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idMagnitud = Convert.ToInt64(param["IdMagnitud"]);
                //Ejecuta el metodo para eliminar el registro.
                Magnitud _magnitud = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud);
                EMSLibrary.User.PerformanceAssessments.Configuration.Remove(_magnitud);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public void Magnitud_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idMagnitud = Convert.ToInt64(param["IdMagnitud"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).LanguagesOptions.Remove(_idLanguage);
        }
        public String Magnitud(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.Magnitudes, Common.ConstantsEntitiesName.PA.Magnitud, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Magnitud_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Measurement Devices
        public void MeasurementDeviceRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idMeasurementDeviceType = Convert.ToInt64(param["IdMeasurementDeviceType"]);
            Int64 _idMeasurementDevice = Convert.ToInt64(param["IdMeasurementDevice"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_idMeasurementDeviceType).MeasurementDeviceRemove(_idMeasurementDevice);
        }
        public String MeasurementDevice(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.MeasurementDevices, Common.ConstantsEntitiesName.PA.MeasurementDevice, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> MeasurementDevice_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Measurement Device Types
        public void MeasurementDeviceTypeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idMeasurementDeviceType = Convert.ToInt64(param["IdMeasurementDeviceType"]);
            MeasurementDeviceType _measurementDeviceType = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_idMeasurementDeviceType);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceTypeRemove(_measurementDeviceType);
        }
        public void MeasurementDeviceType_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idMeasurementDeviceType = Convert.ToInt64(param["IdMeasurementDeviceType"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_idMeasurementDeviceType).LanguagesOptions.Remove(_idLanguage);
        }
        public String MeasurementDeviceType(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, Common.ConstantsEntitiesName.PA.MeasurementDeviceType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> MeasurementDeviceType_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Measurements
            public void MeasurementRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                
                Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementRemove(_measurement);
            }
            public void Measurement_LGRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                String _idLanguage = param["IdLanguage"].ToString();

                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).LanguagesOptions.Remove(_idLanguage);
            }
            public String Measurement(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PA.Measurements, Common.ConstantsEntitiesName.PA.Measurement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> Measurement_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                //_itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    //_itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                    _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                }
                //_itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
        #endregion

        #region Measurement Units
        public void MeasurementUnitRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idMagnitud = Convert.ToInt64(param["IdMagnitud"]);
            Int64 _idMeasurementUnit = Convert.ToInt64(param["IdMeasurementUnit"]);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnitRemove(_idMeasurementUnit);
        }
        public void MeasurementUnit_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idMagnitud = Convert.ToInt64(param["IdMagnitud"]);
            Int64 _idMeasurementUnit = Convert.ToInt64(param["IdMeasurementUnit"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnit(_idMeasurementUnit).LanguagesOptions.Remove(_idLanguage);
        }
        public String MeasurementUnit(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.MeasurementUnits, Common.ConstantsEntitiesName.PA.MeasurementUnit, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.Magnitudes, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> MeasurementUnit_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Parameter Groups
        public void ParameterGroupRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
            //Ejecuta el metodo para eliminar el registro.
            Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
            _indicator.Remove(_indicator.ParameterGroup(_idParameterGroup));
        }
        public void ParameterGroup_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup).LanguagesOptions.Remove(_idLanguage);
        }
        public String ParameterGroup(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.ParameterGroups, Common.ConstantsEntitiesName.PA.ParameterGroup, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ParameterGroup_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Parameters
        public void ParameterRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
            Int64 _idParameter = Convert.ToInt64(param["IdParameter"]);
            //Ejecuta el metodo para eliminar el registro.
            ParameterGroup _parameterGroup = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup);
            _parameterGroup.Remove(_parameterGroup.Parameter(_idParameter));
        }
        public void Parameter_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
            Int64 _idParameter = Convert.ToInt64(param["IdParameter"]);
            String _idLanguage = param["IdLanguage"].ToString();
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup).Parameter(_idParameter).LanguagesOptions.Remove(_idLanguage);
        }
        public String Parameter(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.Parameters, Common.ConstantsEntitiesName.PA.Parameter, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> Parameter_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Parameter Ranges
        public void ParameterRangeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
            Int64 _idParameter = Convert.ToInt64(param["IdParameter"]);
            Int64 _idParameterRange = Convert.ToInt64(param["IdParameterRange"]);
            //Ejecuta el metodo para eliminar el registro.
            Parameter _parameter = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup).Parameter(_idParameter);
            _parameter.Remove(_parameter.ParameterRange(_idParameterRange));
        }
        public String ParameterRange(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PA.ParameterRanges, Common.ConstantsEntitiesName.PA.ParameterRange, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ParameterRange_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Indicator Extended Property
            public void IndicatorExtendedPropertyRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
                Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);

                //Ejecuta el metodo para eliminar el registro.
                Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                _indicator.Remove(_indicator.ExtendedPropertyValue(_idExtendedProperty));
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> IndicatorExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public String IndicatorExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PA.IndicatorExtendedProperties, Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty, Common.ConstantsEntitiesName.PA.Indicator, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
        #endregion

        #region Formula Extended Property
            //public void FormulaExtendedPropertyRemove(Dictionary<String, Object> param)
            //{
            //    //Ya esta el DataTable armado, ahora se trae el item.
            //    Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
            //    Int64 _idFormula = Convert.ToInt64(param["IdFormula"]);

            //    //Ejecuta el metodo para eliminar el registro.
            //    Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_idFormula);
            //    _formula.Remove(_formula.ExtendedPropertyValue(_idExtendedProperty));
            //}
            public Dictionary<String, KeyValuePair<String, Boolean>> FormulaExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
           
                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public String FormulaExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PA.FormulaExtendedProperties, Common.ConstantsEntitiesName.PA.FormulaExtendedProperty, Common.ConstantsEntitiesName.PA.Formula, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
        #endregion

        #region Calculation Extended Property
            //public void CalculationExtendedPropertyRemove(Dictionary<String, Object> param)
            //{
            //    //Ya esta el DataTable armado, ahora se trae el item.
            //    Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
            //    Int64 _idCalculation = Convert.ToInt64(param["IdCalculation"]);

            //    //Ejecuta el metodo para eliminar el registro.
            //    Calculation _calculation = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation);
            //    _calculation.Remove(_calculation.ExtendedPropertyValue(_idExtendedProperty));
            //}
            public Dictionary<String, KeyValuePair<String, Boolean>> CalculationExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
                
                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public String CalculationExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PA.CalculationExtendedProperties, Common.ConstantsEntitiesName.PA.CalculationExtendedProperty, Common.ConstantsEntitiesName.PA.Calculation, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
        #endregion

        #region Parameter Group Extended Property
            //public void ParameterGroupExtendedPropertyRemove(Dictionary<String, Object> param)
            //{
            //    //Ya esta el DataTable armado, ahora se trae el item.
            //    Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
            //    Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
            //    Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);

            //    //Ejecuta el metodo para eliminar el registro.
            //    ParameterGroup _parameterGroup = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup);
            //    _parameterGroup.Remove(_parameterGroup.ExtendedPropertyValue(_idExtendedProperty));
            //}
            public Dictionary<String, KeyValuePair<String, Boolean>> ParameterGroupExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
                
                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public String ParameterGroupExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperties, Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty, Common.ConstantsEntitiesName.PA.ParameterGroup, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
        #endregion

            #region Constant Classification
                public void ConstantClassificationRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);

                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification));
                }
                public void ConstantClassification_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String ConstantClassification(Dictionary<String, Object> param)
                {
                    SetParameters(String.Empty, Common.ConstantsEntitiesName.PA.ConstantClassification, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.ConstantClassifications, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/HierarchicalListManage.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> ConstantClassification_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Accounting Activity
                public void AccountingActivityRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idActivity = Convert.ToInt64(param["IdActivity"]);

                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity));
                }
                public void AccountingActivity_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idActivity = Convert.ToInt64(param["IdActivity"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String AccountingActivity(Dictionary<String, Object> param)
                {
                    SetParameters(String.Empty, Common.ConstantsEntitiesName.PA.AccountingActivity, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.AccountingActivities, Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/HierarchicalListManage.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> AccountingActivity_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Accounting Sector
                public void AccountingSectorRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idSector = Convert.ToInt64(param["IdSector"]);

                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_idSector));
                }
                public void AccountingSector_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_idSector).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String AccountingSector(Dictionary<String, Object> param)
                {
                    SetParameters(String.Empty, Common.ConstantsEntitiesName.PA.AccountingSector, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.AccountingSectors, Common.ConstantsEntitiesName.PA.AccountingSectorsChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/HierarchicalListManage.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> AccountingSector_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Constant
                public void ConstantRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);
                    Int64 _idConstant = Convert.ToInt64(param["IdConstant"]);
                    //Ejecuta el metodo para eliminar el registro.
                    ConstantClassification _constantClassification = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification);
                    _constantClassification.Remove(_constantClassification.Constant(_idConstant));
                }
                public void Constant_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);
                    Int64 _idConstant = Convert.ToInt64(param["IdConstant"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    Constant _constant = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification).Constant(_idConstant);
                    _constant.LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String Constant(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PA.Constants, Common.ConstantsEntitiesName.PA.Constant, String.Empty, Common.ConstantsEntitiesName.PA.Constant, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.ConstantClassifications, true, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> Constant_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Accounting Scenario
                public void AccountingScenarioRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idScenario = Convert.ToInt64(param["IdScenario"]);
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScenario(_idScenario));
                }
                public void AccountingScenario_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idScenario = Convert.ToInt64(param["IdScenario"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScenario(_idScenario).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String AccountingScenario(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PA.AccountingScenarios, Common.ConstantsEntitiesName.PA.AccountingScenario, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> AccountingScenario_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Accounting Scope
                public void AccountingScopeRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idScope = Convert.ToInt64(param["IdScope"]);
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(_idScope));
                }
                public void AccountingScope_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idScope = Convert.ToInt64(param["IdScope"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(_idScope).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String AccountingScope(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PA.AccountingScopes, Common.ConstantsEntitiesName.PA.AccountingScope, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> AccountingScope_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> AccountingScope_LG_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Methodology
                public void MethodologyRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idMethodology = Convert.ToInt64(param["IdMethodology"]);
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(EMSLibrary.User.PerformanceAssessments.Configuration.Methodology(_idMethodology));
                }
                public void Methodology_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idMethodology = Convert.ToInt64(param["IdMethodology"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Methodology(_idMethodology).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String Methodology(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PA.Methodologies, Common.ConstantsEntitiesName.PA.Methodology, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> Methodology_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Quality
                public void QualityRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idQuality = Convert.ToInt64(param["IdQuality"]);
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(EMSLibrary.User.PerformanceAssessments.Configuration.Quality(_idQuality));
                }
                public void Quality_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idQuality = Convert.ToInt64(param["IdQuality"]);
                    String _idLanguage = param["IdLanguage"].ToString();
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Quality(_idQuality).LanguageRemove(EMSLibrary.User.DirectoryServices.Configuration.Language(_idLanguage));
                }
                public String Quality(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PA.Qualities, Common.ConstantsEntitiesName.PA.Quality, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> Quality_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region Transformations
                public void TransformationByTransformationRemove(Dictionary<String, Object> param)
                {
                    TransformationRemove(param);
                }
                public void TransformationRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                    Int64 _idTransformation = Convert.ToInt64(param["IdTransformation"]);

                    Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                    //Ejecuta el metodo para eliminar el registro.
                    _measurement.Remove(_measurement.Transformation(_idTransformation));
                }
                public void TransformationByTransformation_LGRemove(Dictionary<String, Object> param)
                {
                    Transformation_LGRemove(param);
                }
                public void Transformation_LGRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                    Int64 _idTransformation = Convert.ToInt64(param["IdTransformation"]);
                    String _idLanguage = param["IdLanguage"].ToString();

                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).Transformation(_idTransformation).LanguagesOptions.Remove(_idLanguage);
                }
                public String TransformationByTransformation(Dictionary<String, Object> param)
                {
                    return Transformation(param);
                }
                public String Transformation(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PA.Transformations, Common.ConstantsEntitiesName.PA.Transformation, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> Transformation_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> TransformationByTransformation_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

            #region ConfigurationExcelFile
                public void ConfigurationExcelFileRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idExcelFile = Convert.ToInt64(param["IdExcelFile"]);
                    //Ejecuta el metodo para eliminar el registro.
                    EMSLibrary.User.PerformanceAssessments.Configuration.Remove(_idExcelFile);
                }
                public String ConfigurationExcelFile(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PA.ConfigurationExcelFiles, Common.ConstantsEntitiesName.PA.ConfigurationExcelFile, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Managers/ListManageAndView.aspx";
                }
                public Dictionary<String, KeyValuePair<String, Boolean>> ConfigurationExcelFile_MenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
            #endregion

        #endregion

        #region Process Framework

        #region Process Extended Properties
            public void ProcessExtendedPropertyRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
                Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);

                //Ejecuta el metodo para eliminar el registro.
                Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                _process.Remove(_process.ExtendedPropertyValue(_idExtendedProperty));
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (param.ContainsKey("IdProcess"))
                {
                    Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                    ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                    if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }
                else
                {   //Como no viene el ID, entonces directamente miro el mapa.
                    if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _isEnabled = true; }
                }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                    //_itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
            public String ProcessExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PF.ProcessExtendedProperties, Common.ConstantsEntitiesName.PF.ProcessExtendedProperty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
        #endregion

        #region Extended Properties
            public void ExtendedPropertyRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
                Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);

                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClassification).ExtendedPropertiesRemove(_idExtendedProperty);
            }
            public void ExtendedProperty_LGRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
                Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);
                String _idLanguage = param["IdLanguage"].ToString();

                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClassification).ExtendedProperty(_idExtendedProperty).LanguagesOptions.Remove(_idLanguage);
            }
            public String ExtendedProperty(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PF.ExtendedProperties, Common.ConstantsEntitiesName.PF.ExtendedProperty, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ExtendedPropertyClassifications, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ExtendedProperty_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }

                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                    _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
        #endregion

        #region Extended Property Classifications
        public void ExtendedPropertyClassificationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassificationRemove(_idExtendedPropertyClassification);
        }
        public void ExtendedPropertyClassification_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClassification).LanguagesOptions.Remove(_idLanguage);
        }
        public String ExtendedPropertyClassification(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PF.ExtendedPropertyClassifications, Common.ConstantsEntitiesName.PF.ExtendedPropertyClassification, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ExtendedPropertyClassification_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Participation Types
        public void ParticipationTypeRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idParticipationType = Convert.ToInt64(param["IdParticipationType"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Configuration.ParticipationTypeRemove(_idParticipationType);
        }
        public void ParticipationType_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idParticipationType = Convert.ToInt64(param["IdParticipationType"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Configuration.ParticipationType(_idParticipationType).LanguagesOptions.Remove(_idLanguage);
        }
        public String ParticipationType(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PF.ParticipationTypes, Common.ConstantsEntitiesName.PF.ParticipationType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ParticipationType_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Process Classifications
        public void ProcessClassificationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Map.Remove(EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification));
        }
        public void ProcessClassification_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification).LanguagesOptions.Remove(_idLanguage);
        }
        public String ProcessClassification(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassification, Common.ConstantsEntitiesName.PF.ProcessClassification, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/ManagementTools/ProcessesMap/Map.aspx";  //return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ProcessClassification_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }
        
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #region Processes
            public void ProcessRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);

                //Ejecuta el metodo para eliminar el registro.
                ProcessGroupProcess _processGroupProcess = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess);
                EMSLibrary.User.ProcessFramework.Map.Remove(_processGroupProcess);
            }
            public void ProcessGroupProcessRemove(Dictionary<String, Object> param)
            {
                ProcessRemove(param);
            }
            public void Process_LGRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                String _idLanguage = param["IdLanguage"].ToString();

                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).LanguagesOptions.Remove(_idLanguage);
            }
            public String Process(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/ManagementTools/ProcessesMap/Map.aspx";  //return "~/Managers/ListManageAndView.aspx";
            }
            public String ProcessGroupProcess(Dictionary<String, Object> param)
            {
                return Process(param);
            }
            public void ProcessGroupProcess_LGRemove(Dictionary<String, Object> param)
            {
                Process_LGRemove(param);
            }

            public Dictionary<String, KeyValuePair<String, Boolean>> Process_MenuOption(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuOption(param);
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> Process_MenuSecurity(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuSecurity(param);
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessGroupProcess_MenuOption(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuOption(param);
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessGroupProcess_MenuSecurity(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuSecurity(param);
            }
            //public Dictionary<String, KeyValuePair<String, Boolean>> ProcessGroupNode_MenuOption(Dictionary<String, Object> param)
            //{   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
            //    return BuildProcessMenuOption(param);
            //}
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessTaskCalibration_MenuOption(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuOption(param);
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessTaskMeasurement_MenuOption(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuOption(param);
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessTaskOperation_MenuOption(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuOption(param);
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessTask_MenuOption(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuOption(param);
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessResource_MenuOption(Dictionary<String, Object> param)
            {   //Este metodo lo replico tantas veces como process distintos tenga...(Process/Node/etc)
                return BuildProcessMenuOption(param);
            }

            #region Private Method for Process
                private Dictionary<String, KeyValuePair<String, Boolean>> BuildProcessMenuOption(Dictionary<String, Object> param)
                {
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;
                    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                    if (param.ContainsKey("IdProcess"))
                    {
                        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                        ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                        if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _isEnabled = true; }
                    }
                    else
                    {   //Como no viene el ID, entonces directamente miro el mapa.
                        if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _isEnabled = true; }
                    }

                    //Esto lo pongo de esta forma, para mantener el orden del menu.-
                    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                    if (isViewer)
                    {
                        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                    }
                    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                    return _itemsMenu;
                }
                private Dictionary<String, KeyValuePair<String, Boolean>> BuildProcessMenuSecurity(Dictionary<String, Object> param)
                {
                    //Este menuSecurity es solo para las entidades que llevan seguridad directa asociada.(Mapas)
                    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    Boolean _isEnabled = false;

                    if (param.ContainsKey("IdProcess"))
                    {
                        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                        ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                        if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _isEnabled = true; }
                    }
                    else
                    {   //Como no viene el ID, entonces directamente miro el mapa.
                        if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _isEnabled = true; }
                    }

                    _itemsMenu.Add("rmiSSJobTitles", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSJobTitle, _isEnabled));
                    _itemsMenu.Add("rmiSSPerson", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSPerson, _isEnabled));

                    return _itemsMenu;
                }
            #endregion
        #endregion
        
        //#region Process Node
        //    public void ProcessGroupNodeRemove(Dictionary<String, Object> param)
        //    {
        //        //Ya esta el DataTable armado, ahora se trae el item.
        //        Int64 _idProcess = Convert.ToInt64(param["IdParentProcess"]);
        //        Int64 _idProcessNode = Convert.ToInt64(param["IdProcess"]);

        //        //Ejecuta el metodo para eliminar el registro.
        //        ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
        //        ProcessGroupNode _processGroupNode = (ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(_idProcessNode);
        //        _processGroupProcess.Remove(_processGroupNode);
        //    }
        //    public String ProcessGroupNode(Dictionary<String, Object> param)
        //    {
        //        SetParameters(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        //        return "~/ManagementTools/ProcessesMap/Map.aspx";
        //    }
        //#endregion

                public void OverDueTasksRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                    Int64 _idExecution = Convert.ToInt64(param["IdExecution"]);
                    //ProcessGroupProcess _processGroupProcess = null;

                    ProcessTask _processTask = (ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                    _processTask.Remove(_processTask.ProcessTaskExecution(_idExecution));


                    //switch (EMSLibrary.User.ProcessFramework.Map.Process(_idTask).GetType().Name )
                    //{
                    //    case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                    //        //Ejecuta el metodo para eliminar el registro.
                    //        ProcessTaskMeasurement _processTaskMeasurement = (ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                    //        _processGroupProcess = _processTaskMeasurement.Parent;

                    //        _processGroupProcess.ProcessTasksRemove(_processTaskMeasurement);
                    //        break;

                    //    case Common.ConstantsEntitiesName.PF.ProcessTaskCalibration:
                    //        //Ejecuta el metodo para eliminar el registro.
                    //        ProcessTaskCalibration _processTaskCalibration = (ProcessTaskCalibration)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                    //        _processGroupProcess = _processTaskCalibration.Parent;

                    //        _processGroupProcess.ProcessTasksRemove(_processTaskCalibration);
                    //        break;

                    //    case Common.ConstantsEntitiesName.PF.ProcessTaskOperation:
                    //        //Ejecuta el metodo para eliminar el registro.
                    //        ProcessTaskOperation _processTaskOperation = (ProcessTaskOperation)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                    //        _processGroupProcess = _processTaskOperation.Parent;

                    //        _processGroupProcess.ProcessTasksRemove(_processTaskOperation);
                    //        break;
                    //}

                }

        #region Process Task
            #region Task Measurement
                public void ProcessTaskMeasurementRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idTask = Convert.ToInt64(param["IdTask"]);

                    //Ejecuta el metodo para eliminar el registro.
                    ProcessTaskMeasurement _processTaskMeasurement = (ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                    ProcessGroupProcess _processGroupProcess = _processTaskMeasurement.Parent;

                    _processGroupProcess.ProcessTasksRemove(_processTaskMeasurement);
                }
                public String ProcessTaskMeasurement(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/Dashboard/GeographicDashboardFacilities.aspx";
                    //return "~/ManagementTools/ProcessesMap/Map.aspx";
                }
            #endregion
            #region Task Calibration
                public void ProcessTaskCalibrationRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idTask = Convert.ToInt64(param["IdTask"]);

                    //Ejecuta el metodo para eliminar el registro.
                    ProcessTaskCalibration _processTaskCalibration = (ProcessTaskCalibration)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                    ProcessGroupProcess _processGroupProcess = _processTaskCalibration.Parent;

                    _processGroupProcess.ProcessTasksRemove(_processTaskCalibration);
                }
                public String ProcessTaskCalibration(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    //return "~/ManagementTools/ProcessesMap/Map.aspx";
                    return "~/Dashboard/GeographicDashboardFacilities.aspx";
                }
            #endregion
            #region Task Operation
                public void ProcessTaskOperationRemove(Dictionary<String, Object> param)
                {
                    //Ya esta el DataTable armado, ahora se trae el item.
                    Int64 _idTask = Convert.ToInt64(param["IdTask"]);

                    //Ejecuta el metodo para eliminar el registro.
                    ProcessTaskOperation _processTaskOperation = (ProcessTaskOperation)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                    ProcessGroupProcess _processGroupProcess = _processTaskOperation.Parent;

                    _processGroupProcess.ProcessTasksRemove(_processTaskOperation);
                }
                public String ProcessTaskOperation(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    //return "~/ManagementTools/ProcessesMap/Map.aspx";
                    return "~/Dashboard/GeographicDashboardFacilities.aspx";
                }
            #endregion
            #region Task Data Recovery
                //public void ProcessTaskDataRecoveryRemove(Dictionary<String, Object> param)
                //{
                //    //Ya esta el DataTable armado, ahora se trae el item.
                //    Int64 _idTask = Convert.ToInt64(param["IdTask"]);

                //    //Ejecuta el metodo para eliminar el registro.
                //    ProcessTaskDataRecovery _processTaskDataRecovery = (ProcessTaskDataRecovery)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                //    ProcessGroupProcess _processGroupProcess = _processTaskDataRecovery.ProcessGroupProcess;

                //    _processGroupProcess.ProcessTasksRemove(_processTaskDataRecovery);
                //}
                public String ProcessTaskDataRecovery(Dictionary<String, Object> param)
                {
                    SetParameters(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, Common.ConstantsEntitiesName.PF.Process, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    return "~/ManagementTools/ProcessesMap/Map.aspx";
                }
            #endregion
        #endregion

        #region Process Task Execution
            public Dictionary<String, KeyValuePair<String, Boolean>> ProcessTaskExecution_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdProcess"))
            {
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuExecution, _isEnabled));
            }

            return _itemsMenu;
        }
            public void ProcessTaskExecutionExecutedRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                Int64 _idExecution = Convert.ToInt64(param["IdExecution"]);

                //Ejecuta el metodo para eliminar el registro.
                ProcessTask _processTask = (ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_idTask);
                _processTask.ProcessTaskExecution(_idExecution).ResetExecution();
            }
        #endregion

        #region Process Participations
        public void ProcessParticipationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idParticipationType = Convert.ToInt64(param["IdParticipationType"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);

            //Ejecuta el metodo para eliminar el registro.
            ProcessGroupProcess _processGroupProcess = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess));
            ProcessParticipation _processParticipation = _processGroupProcess.ProcessParticipation(_idOrganization, _idParticipationType);
            _processGroupProcess.Remove(_processParticipation);
        }
        public String ProcessParticipation(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.PF.ProcessParticipations, Common.ConstantsEntitiesName.PF.ProcessParticipation, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> ProcessParticipation_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdProcess"))
            {
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        //#region Role Types
        //public void RoleTypeRemove(Dictionary<String, Object> param)
        //{
        //    //Ya esta el DataTable armado, ahora se trae el item.
        //    Int64 _idRoleType = Convert.ToInt64(param["IdRoleType"]);

        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.Security.Remove(EMSLibrary.User.Security.RoleType(_idRoleType));
        //}
        //public void RoleType_LGRemove(Dictionary<String, Object> param)
        //{
        //    //Ya esta el DataTable armado, ahora se trae el item.
        //    Int64 _idRoleType = Convert.ToInt64(param["IdRoleType"]);
        //    String _idLanguage = param["IdLanguage"].ToString();

        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.Security.RoleType(_idRoleType).LanguagesOptions.Remove(_idLanguage);
        //}
        //public String RoleType(Dictionary<String, Object> param)
        //{
        //    SetParameters(Common.ConstantsEntitiesName.SS.RoleTypes, Common.ConstantsEntitiesName.SS.RoleType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        //    return "~/Managers/ListManageAndView.aspx";
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RoleType_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        #region Time Units
        public void TimeUnit_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idTimeUnit = Convert.ToInt64(param["IdTimeUnit"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_idTimeUnit).LanguagesOptions.Remove(_idLanguage);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> TimeUnit_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            if (isViewer)
            {
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }

            return _itemsMenu;
        }
        #endregion

        #endregion

        #region Risk Management

        #region Risk Classifications
        public void RiskClassificationRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.RiskManagement.Map.Remove(EMSLibrary.User.RiskManagement.Map.RiskClassification(_idRiskClassification));
        }
        public void RiskClassification_LGRemove(Dictionary<String, Object> param)
        {
            //Ya esta el DataTable armado, ahora se trae el item.
            Int64 _idRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);
            String _idLanguage = param["IdLanguage"].ToString();

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.RiskManagement.Map.RiskClassification(_idRiskClassification).LanguagesOptions.Remove(_idLanguage);
        }
        public String RiskClassification(Dictionary<String, Object> param)
        {
            SetParameters(String.Empty, Common.ConstantsEntitiesName.RM.RiskClassification, String.Empty, String.Empty, Common.ConstantsEntitiesName.RM.RiskClassifications, Common.ConstantsEntitiesName.RM.RiskClassificationChildren, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/ManagementTools/PerformanceAssessment/Map.aspx";  //return "~/Managers/HierarchicalListManage.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RiskClassification_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }
        
            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        #endregion

        #region Collaboration Tools

        #region Forums
            public void ForumRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idForum = Convert.ToInt64(param["IdForum"]);
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                Forum _forum = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum);

                //Primero desasocia el foro del process.
                EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess).DisAssociateForum(_forum);
                //Segundo Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.CollaborationTools.Map.Remove(_forum);
            }
            public void Forum_LGRemove(Dictionary<String, Object> param)
            {
                //Ya esta el DataTable armado, ahora se trae el item.
                Int64 _idForum = Convert.ToInt64(param["IdForum"]);
                String _idLanguage = param["IdLanguage"].ToString();

                //Ejecuta el metodo para eliminar el registro.
                EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).LanguagesOptions.Delete(_idLanguage);
            }
            public String Forum(Dictionary<String, Object> param)
            {
                SetParameters(Common.ConstantsEntitiesName.CT.Forums, Common.ConstantsEntitiesName.CT.Forum, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                return "~/Managers/ListManageAndView.aspx";
            }
            public Dictionary<String, KeyValuePair<String, Boolean>> Forum_MenuOption(Dictionary<String, Object> param)
            {
                Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                Boolean _isEnabled = false;
                Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

                //if (param.ContainsKey("IdForum"))
                //{
                //    Int64 _idForum = Convert.ToInt64(param["IdForum"]);
                //    Forum _forum = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum);
                //    if (_forum.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                //    { _isEnabled = true; }
                //}
                //else
                //{   //Como no viene el ID, entonces directamente miro el mapa.
                //    if (EMSLibrary.User.CollaborationTools.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                //    { _isEnabled = true; }
                //}

                _isEnabled = true; 
                //Esto lo pongo de esta forma, para mantener el orden del menu.-
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
                if (isViewer)
                {
                    _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
                    _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
                }
                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                return _itemsMenu;
            }
        #endregion

        #region Categories

        //public void CategoryRemove(Dictionary<String, Object> param)
        //{
        //    //Ya esta el DataTable armado, ahora se trae el item.
        //    Int64 _idForum = Convert.ToInt64(param["IdForum"]);
        //    Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);

        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Remove(EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory));
        //}
        //public void Category_LGRemove(Dictionary<String, Object> param)
        //{
        //    //Ya esta el DataTable armado, ahora se trae el item.
        //    Int64 _idForum = Convert.ToInt64(param["IdForum"]);
        //    Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);
        //    String _idLanguage = param["IdLanguage"].ToString();

        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory).LanguagesOptions.Remove(_idLanguage);
        //}
        //public String Category(Dictionary<String, Object> param)
        //{
        //    SetParameters(Common.ConstantsEntitiesName.CT.Categories, Common.ConstantsEntitiesName.CT.Category, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        //    return "~/Managers/ListManageAndView.aspx";
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> Category_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    if ((param.ContainsKey("IdForum")) && (param.ContainsKey("IdCategory")))
        //    {
        //        Int64 _idForum = Convert.ToInt64(param["IdForum"]);
        //        Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);
        //        Category _category = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory);
        //        if (_category.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //        { _isEnabled = true; }
        //    }
        //    else
        //    {   //Como no viene el ID, entonces directamente miro el mapa.
        //        if (EMSLibrary.User.CollaborationTools.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //        { _isEnabled = true; }
        //    }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}

        #endregion

        #region Topics

        //public void TopicRemove(Dictionary<String, Object> param)
        //{
        //    //Ya esta el DataTable armado, ahora se trae el item.
        //    Int64 _idForum = Convert.ToInt64(param["IdForum"]);
        //    Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);
        //    Int64 _idTopic = Convert.ToInt64(param["IdTopic"]);

        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory).Remove(EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory).Topic(_idTopic));
        //}
        //public void Topic_LGRemove(Dictionary<String, Object> param)
        //{
        //    //Ya esta el DataTable armado, ahora se trae el item.
        //    Int64 _idForum = Convert.ToInt64(param["IdForum"]);
        //    Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);
        //    Int64 _idTopic = Convert.ToInt64(param["IdTopic"]);
        //    String _idLanguage = param["IdLanguage"].ToString();

        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory).Topic(_idTopic).LanguagesOptions.Remove(_idLanguage);
        //}
        //public String Topic(Dictionary<String, Object> param)
        //{
        //    SetParameters(Common.ConstantsEntitiesName.CT.Topics, Common.ConstantsEntitiesName.CT.Topic, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        //    return "~/Managers/ListManageAndView.aspx";
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> Topic_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    if ((param.ContainsKey("IdForum")) && (param.ContainsKey("IdCategory")) && (param.ContainsKey("IdTopic")))
        //    {
        //        Int64 _idForum = Convert.ToInt64(param["IdForum"]);
        //        Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);
        //        Int64 _idTopic = Convert.ToInt64(param["IdTopic"]);
        //        Topic _topic = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory).Topic(_idTopic);
        //        if (_topic.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //        { _isEnabled = true; }
        //    }
        //    else
        //    {   //Como no viene el ID, entonces directamente miro el mapa.
        //        if (EMSLibrary.User.CollaborationTools.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //        { _isEnabled = true; }
        //    }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //        _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true)); //El lenguage si el objeto tiene, se muestra siempre. (no lleva seguridad)
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}

        #endregion

        #endregion

        #region Security
        private Dictionary<String, KeyValuePair<String, Boolean>> BuildSecurityRigthMenuOption(Boolean isEnabled, Boolean isViewer)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, isEnabled));

            return _itemsMenu;
        }

        #region Security Map And Configuration
        private void GetPersonAndPermissionForDelete(Dictionary<String, Object> param, ref Person person, ref Permission permission)
        {
            //POST
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            //Permission
            Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);

            //Contruye estos 2 objetos que necesita para borrar
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            person = _organization.Person(_idPerson);
            
            permission = EMSLibrary.User.Security.Permission(_idPermission);
        }
        private void GetJobTitleAndPermissionForDelete(Dictionary<String, Object> param, ref JobTitle jobTitle, ref Permission permission)
        {
            //JT
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            //Permission
            Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
            //Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
            //String _className = Convert.ToString(param["OwnerClassName"]);

            //Contruye estos 2 objetos que necesita para borrar
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            Position _position = _organization.Position(_idPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);

            jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
            permission = EMSLibrary.User.Security.Permission(_idPermission);

        }

        #region Map DS
        public void RightPersonMapDSRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Map.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleMapDSRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Map.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonMapDS(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonMapsDS, Common.ConstantsEntitiesName.SS.RightPersonMapDS, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleMapDS(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleMapsDS, Common.ConstantsEntitiesName.SS.RightJobTitleMapDS, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonMapDS_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleMapDS_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Configuration DS
        public void RightPersonConfigurationDSRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleConfigurationDSRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Configuration.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonConfigurationDS(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonConfigurationsDS, Common.ConstantsEntitiesName.SS.RightPersonConfigurationDS, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleConfigurationDS(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsDS, Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationDS, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonConfigurationDS_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleConfigurationDS_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Map PA
        public void RightPersonMapPARemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleMapPARemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Map.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonMapPA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonMapsPA, Common.ConstantsEntitiesName.SS.RightPersonMapPA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleMapPA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleMapsPA, Common.ConstantsEntitiesName.SS.RightJobTitleMapPA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonMapPA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleMapPA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Configuration PA
        public void RightPersonConfigurationPARemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleConfigurationPARemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.PerformanceAssessments.Configuration.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonConfigurationPA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonConfigurationsPA, Common.ConstantsEntitiesName.SS.RightPersonConfigurationPA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleConfigurationPA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsPA, Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonConfigurationPA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleConfigurationPA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Map PF
        public void RightPersonMapPFRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Map.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleMapPFRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Map.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonMapPF(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonMapsPF, Common.ConstantsEntitiesName.SS.RightPersonMapPF, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleMapPF(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleMapsPF, Common.ConstantsEntitiesName.SS.RightJobTitleMapPF, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonMapPF_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleMapPF_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Configuration PF
        public void RightPersonConfigurationPFRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Configuration.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleConfigurationPFRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Configuration.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonConfigurationPF(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonConfigurationsPF, Common.ConstantsEntitiesName.SS.RightPersonConfigurationPF, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleConfigurationPF(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsPF, Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPF, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonConfigurationPF_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleConfigurationPF_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Map KC
        public void RightPersonMapKCRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleMapKCRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Map.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonMapKC(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonMapsKC, Common.ConstantsEntitiesName.SS.RightPersonMapKC, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleMapKC(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleMapsKC, Common.ConstantsEntitiesName.SS.RightJobTitleMapKC, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonMapKC_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleMapKC_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Configuration KC
        public void RightPersonConfigurationKCRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleConfigurationKCRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonConfigurationKC(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonConfigurationsKC, Common.ConstantsEntitiesName.SS.RightPersonConfigurationKC, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleConfigurationKC(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsKC, Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationKC, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonConfigurationKC_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleConfigurationKC_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Map IA
        public void RightPersonMapIARemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ImprovementAction.Map.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleMapIARemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ImprovementAction.Map.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonMapIA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonMapsIA, Common.ConstantsEntitiesName.SS.RightPersonMapIA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleMapIA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleMapsIA, Common.ConstantsEntitiesName.SS.RightJobTitleMapIA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonMapIA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleMapIA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Configuration IA
        public void RightPersonConfigurationIARemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ImprovementAction.Configuration.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleConfigurationIARemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ImprovementAction.Configuration.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonConfigurationIA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonConfigurationsIA, Common.ConstantsEntitiesName.SS.RightPersonConfigurationIA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleConfigurationIA(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsIA, Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationIA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonConfigurationIA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ImprovementAction.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleConfigurationIA_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.ImprovementAction.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Map RM
        public void RightPersonMapRMRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.RiskManagement.Map.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleMapRMRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.RiskManagement.Map.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonMapRM(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonMapsRM, Common.ConstantsEntitiesName.SS.RightPersonMapRM, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleMapRM(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleMapsRM, Common.ConstantsEntitiesName.SS.RightJobTitleMapRM, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonMapRM_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleMapRM_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #region Configuration RM
        public void RightPersonConfigurationRMRemove(Dictionary<String, Object> param)
        {
            Person _person = null;
            Permission _permission = null;
            GetPersonAndPermissionForDelete(param, ref _person, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.RiskManagement.Configuration.SecurityPersonRemove(_person, _permission);
        }
        public void RightJobTitleConfigurationRMRemove(Dictionary<String, Object> param)
        {
            JobTitle _jobTitle = null;
            Permission _permission = null;
            GetJobTitleAndPermissionForDelete(param, ref _jobTitle, ref _permission);

            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.RiskManagement.Configuration.SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public String RightPersonConfigurationRM(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonConfigurationsRM, Common.ConstantsEntitiesName.SS.RightPersonConfigurationRM, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleConfigurationRM(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsRM, Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationRM, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonConfigurationRM_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.RiskManagement.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleConfigurationRM_MenuOption(Dictionary<String, Object> param)
        {
            Boolean _isEnabled = false;
            Boolean _isViewer = Convert.ToBoolean(param["IsViewer"]);

            //Como no viene el ID, entonces directamente miro el mapa.
            if (EMSLibrary.User.RiskManagement.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
            { _isEnabled = true; }

            return BuildSecurityRigthMenuOption(_isEnabled, _isViewer);
        }
        #endregion

        #endregion

        #region Security By Entities

        #region Organization
        public void RightJobTitleOrganizationRemove(Dictionary<String, Object> param)
        {
            //JT
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            //Permission
            Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
            //Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
            //String _className = Convert.ToString(param["OwnerClassName"]);
            Int64 _idOrganizationOrg = Convert.ToInt64(param["IdOrganizationOrg"]);

            //Contruye estos 2 objetos que necesita para borrar
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            Position _position = _organization.Position(_idPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
            Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganizationOrg).SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public void RightPersonOrganizationRemove(Dictionary<String, Object> param)
        {
            //POST
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            //Permission
            Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);

            Int64 _idOrganizationOrg = Convert.ToInt64(param["IdOrganizationOrg"]);

            //Contruye estos 2 objetos que necesita para borrar
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            Person _person = _organization.Person(_idPerson);
            Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganizationOrg).SecurityPersonRemove(_person, _permission);
        }
        public String RightPersonOrganization(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonOrganizations, Common.ConstantsEntitiesName.SS.RightPersonOrganization, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleOrganization(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleOrganizations, Common.ConstantsEntitiesName.SS.RightJobTitleOrganization, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonOrganization_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleOrganization_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdOrganization"))
            {
                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        //#region Organization Classification
        //public void RightJobTitleOrganizationClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonOrganizationClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonOrganizationClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleOrganizationClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        //#region Process Classification
        //public void RightJobTitleProcessClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonProcessClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonProcessClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleProcessClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        #region Process
        public void RightJobTitleProcessRemove(Dictionary<String, Object> param)
        {
            //JT
            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
            Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
            Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
            Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            //Permission
            Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
            //Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
            //String _className = Convert.ToString(param["OwnerClassName"]);

            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
            Position _position = _organization.Position(_idPosition);
            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
            JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

            //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
            Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess).SecurityJobTitleRemove(_jobTitle, _permission);
        }
        public void RightPersonProcessRemove(Dictionary<String, Object> param)
        {
            //POST
            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
            //Permission
            Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);

            //Contruye estos 2 objetos que necesita para borrar
            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            Person _person = _organization.Person(_idPerson);

            Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission);
            //Ejecuta el metodo para eliminar el registro.
            EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess).SecurityPersonRemove(_person, _permission);
        }
        public String RightPersonProcess(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightPersonProcesses, Common.ConstantsEntitiesName.SS.RightPersonProcess, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public String RightJobTitleProcess(Dictionary<String, Object> param)
        {
            SetParameters(Common.ConstantsEntitiesName.SS.RightJobTitleProcesses, Common.ConstantsEntitiesName.SS.RightJobTitleProcess, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            return "~/Managers/ListManageAndView.aspx";
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonProcess_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdProcess"))
            {
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleProcess_MenuOption(Dictionary<String, Object> param)
        {
            Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            Boolean _isEnabled = false;
            Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

            if (param.ContainsKey("IdProcess"))
            {
                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }
            else
            {   //Como no viene el ID, entonces directamente miro el mapa.
                if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                { _isEnabled = true; }
            }

            //Esto lo pongo de esta forma, para mantener el orden del menu.-
            _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
            if (isViewer)
            {
                _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
            }
            _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

            return _itemsMenu;
        }
        #endregion

        //#region Indicator Classification
        //public void RightJobTitleIndicatorClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonIndicatorClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonIndicatorClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleIndicatorClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        //#region Indicator
        //public void RightJobTitleIndicatorRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonIndicatorRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonIndicator_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleIndicator_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }

        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        //#region Resource Classification
        //public void RightJobTitleResourceClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonResourceClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonResourceClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleResourceClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        //#region Resource
        //public void RightJobTitleResourceRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idResource = Convert.ToInt64(param["IdResource"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonResourceRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idResource = Convert.ToInt64(param["IdResource"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonResource_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleResource_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        //#region Project Classification
        //public void RightJobTitleProjectClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonProjectClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonProjectClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //   //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleProjectClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        //#region Risk Classification
        //public void RightJobTitleRiskClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //JT
        //    Int64 _idRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.RiskManagement.Map.RiskClassification(_idRiskClassification).SecurityJobTitleRemove(_jobTitle, _permission);
        //}
        //public void RightPersonRiskClassificationRemove(Dictionary<String, Object> param)
        //{
        //    //POST
        //    Int64 _idRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);
        //    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
        //    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
        //    Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
        //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
        //    //Permission
        //    Int64 _idPermission = Convert.ToInt64(param["IdPermission"]);
        //    Int64 _idObject = Convert.ToInt64(param["IdOwnerObject"]);
        //    String _className = Convert.ToString(param["OwnerClassName"]);

        //    //Contruye estos 2 objetos que necesita para borrar
        //    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
        //    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
        //    Position _position = _organization.Position(_idPosition);
        //    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
        //    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
        //    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
        //    JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

        //    Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
        //    Condesus.EMS.Business.Security.Entities.Permission _permission = EMSLibrary.User.Security.Permission(_idPermission, _className, _idObject);
        //    //Ejecuta el metodo para eliminar el registro.
        //    EMSLibrary.User.RiskManagement.Map.RiskClassification(_idRiskClassification).SecurityPostRemove(_post, _permission);
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightPersonRiskClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //public Dictionary<String, KeyValuePair<String, Boolean>> RightJobTitleRiskClassification_MenuOption(Dictionary<String, Object> param)
        //{
        //    Dictionary<String, KeyValuePair<String, Boolean>> _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //    Boolean _isEnabled = false;
        //    Boolean isViewer = Convert.ToBoolean(param["IsViewer"]);

        //    //Como no viene el ID, entonces directamente miro el mapa.
        //    if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //    { _isEnabled = true; }
        
        //    //Esto lo pongo de esta forma, para mantener el orden del menu.-
        //    _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));
        //    if (isViewer)
        //    {
        //        _itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, _isEnabled));
        //    }
        //    _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

        //    return _itemsMenu;
        //}
        //#endregion

        #endregion

        #endregion

        #endregion
    }
}
