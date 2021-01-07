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

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {

        #region External Methods
            public String GetPageViewerByEntity(String entityName)
            {
                switch (entityName)
                {
                    case Common.ConstantsEntitiesName.PF.Process:
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.DS.Organization:
                    case Common.ConstantsEntitiesName.DS.Person:
                    case Common.ConstantsEntitiesName.DS.Facility:
                    case Common.ConstantsEntitiesName.DS.Sector:
                    case Common.ConstantsEntitiesName.PA.Measurement:
                    case Common.ConstantsEntitiesName.PA.KeyIndicator:
                    case Common.ConstantsEntitiesName.PA.MeasurementDevice:
                    case Common.ConstantsEntitiesName.DS.GeographicArea:
                    case Common.ConstantsEntitiesName.PA.Transformation:
                    case Common.ConstantsEntitiesName.PA.TransformationByTransformation:
                        return "~/MainInfo/ListReportViewer.aspx";
                    
                    //Los mensajes del foro se deben mostrar en esta pagina...
                    case Common.ConstantsEntitiesName.CT.Message:
                        return "~/Managers/MessagesList.aspx";

                    //Desde el tooltip del dashboard para llegar a todos los facilities
                    case Common.ConstantsEntitiesName.DS.FacilitiesByProcess:
                        return "~/Dashboard/GeographicDashboardFacilities.aspx";

                    //Si no esta en la lista anterior, pasa al viewer Standard.
                    default:
                        return "~/MainInfo/ListViewer.aspx";
                }
            }
        #endregion

    }
}
