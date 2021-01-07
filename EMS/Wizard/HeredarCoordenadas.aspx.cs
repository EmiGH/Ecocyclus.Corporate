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
using System.Transactions;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace Condesus.EMS.WebUI.Wizard
{
    public partial class HeredarCoordenadas : BasePropertiesTask
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
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                base.InjectCheckIndexesTags();

                if (!Page.IsPostBack)
                {
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
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
            private void modifysector(Condesus.EMS.Business.GIS.Entities.Sector sector)
            {
                sector.Modify(sector.Parent, sector.Coordinate, sector.LanguageOption.Name, sector.LanguageOption.Description, sector.FacilityType, sector.ResourcePicture,true);

                var _lnq = from s in sector.Sectors.Values
                           where s.Coordinate != String.Empty
                           select s;
                foreach (Condesus.EMS.Business.GIS.Entities.Sector _sector in _lnq)
                {
                    modifysector(_sector);
                }
            }
            public void Ejecutar()//Zona de pruebas
            {
                Condesus.EMS.Business.DS.Entities.Organization _org = EMSLibrary.User.DirectoryServices.Map.Organization(62);

                foreach (Condesus.EMS.Business.GIS.Entities.Facility _facility in _org.Facilities.Values)
                {
                    var _lnq = from s in _facility.Sectors.Values
                               where s.Coordinate != String.Empty
                               select s;
                    foreach (Condesus.EMS.Business.GIS.Entities.Sector _sector in _lnq)
                    {
                        modifysector(_sector);
                    }
                }
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Ejecutar();

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
