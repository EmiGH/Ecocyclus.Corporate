using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Transactions;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.Security.Entities;
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.Wizard
{
    public partial class CreateProcessForAllOrganizationCommunity : BasePropertiesTask
    {
        #region Internal Properties
        #endregion

        #region PageLoad & Init
            protected override void InyectJavaScript()
            {
                base.InjectCheckIndexesTags();
            }
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

                base.InjectCheckIndexesTags();

                if (!Page.IsPostBack)
                {
                    //Form
                    //base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.ScriptEngine;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Quality;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                Int64 _idorganizationGlobal = 0;
                try
                {
                    //Construye el Scope de la transaccion (todo lo que este dentro va en transaccion)
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        DateTime _campaignDate = Convert.ToDateTime("01/01/2011");
                        ResourceCatalog _resourceCatalog = null;
                        String _coordinate = String.Empty;
                        Dictionary<Int64, ProcessClassification> _processClassifications = new Dictionary<Int64, ProcessClassification>();

                        //Dictionary<Int64, Organization> _organizations = EMSLibrary.User.DirectoryServices.Map.Organizations();
                        var _lnqOrganizations = from o in EMSLibrary.User.DirectoryServices.Map.Organizations().Values
                                                where o.IdOrganization > Convert.ToInt64(txtIdOrgDesde.Text) && o.IdOrganization <= Convert.ToInt64(txtIdOrgHasta.Text) //&& o.IdOrganization <= 3476
                                                select o;
                        foreach (Organization _organization in _lnqOrganizations)
                        {   //La Organizacion 1 Es Condesus esa no Necesita un Process
                            if (_organization.IdOrganization != 1)
                            {
                                _idorganizationGlobal = _organization.IdOrganization;
                                //por cada Organizacion, Dar de ALTA 1 Proceso
                                String _processTitle = "Observatory of " + _organization.CorporateName;
                                //Esto es para dar de alta los usuario para cada provincia y municipio
                                GeographicArea _geoArea;
                                _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(Convert.ToInt64(_organization.FiscalIdentification));

                                //if (_organization.IdOrganization <= 156)
                                //{   //Son las Provincias y Municipios
                                //    _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(Convert.ToInt64(_organization.FiscalIdentification));
                                //}
                                //else
                                //{   //Son las Escuelas, y estan en CABA....
                                //    _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(6431);
                                //}
                                //Da el ALTA del Proceso
                                ProcessGroupProcess _process = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcessAdd(100, 0, _processTitle, String.Empty, String.Empty, 100,
                                    String.Empty, _campaignDate, _resourceCatalog, _coordinate, _geoArea, _processClassifications, _organization, String.Empty, String.Empty);


                                //Aca da la Seguridad para los usuarios de esa Organizacion!....
                                Permission _permissionManager = EMSLibrary.User.Security.Permission(Common.Constants.PermissionManageKey);
                                Permission _permissionReader = EMSLibrary.User.Security.Permission(Common.Constants.PermissionViewKey);
                                //Da Seguridad a los Usuarios de la organizacion....
                                foreach (Person _person in _organization.People.Values)
                                {
                                    if (_person.NickName == "Admin")
                                    {   //Si es ADMIN, es MANAGER
                                        _process.SecurityPersonAdd(_person, _permissionManager);
                                    }
                                    else
                                    {   //Cualquier otra cosa, es READER
                                        _process.SecurityPersonAdd(_person, _permissionReader);
                                    }
                                }
                                //Aqui finaliza y pasa a la proxima organizacion...

                            }
                        }

                        //Finaliza la transaccion
                        _transactionScope.Complete();
                    }

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
