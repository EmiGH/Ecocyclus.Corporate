using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG.Entities
{
    public class Graphic_Pie
    {
        private Credential _Credential;
        private PF.Entities.ProcessGroupProcess _ProcessGroupProcess;

        internal Credential Credential
        {
            get
            {
                return _Credential;
            }
        }
        internal PF.Entities.ProcessGroupProcess ProcessGroupProcess
        {
            get
            {
                return _ProcessGroupProcess;
            }
        }

        internal Graphic_Pie(Credential credential, PF.Entities.ProcessGroupProcess processGroupProcess)
        {
            _Credential = credential;
            _ProcessGroupProcess = processGroupProcess;

        }

        public List<IGraphicPie> Pie_Scopes(Int64 idIndicatortCO2e, DateTime startDate, DateTime endDate)
        {
            //construye la coleccion
            Collections.Graphics _graphics = new Condesus.EMS.Business.RG.Collections.Graphics(this,startDate, endDate);

            return _graphics.PieScopesByIndicator(idIndicatortCO2e);

        }

        public List<IGraphicPie> Pie_ScopesByFacility(Int64 idIndicatortCO2e, Int64 idFacility, DateTime startDate, DateTime endDate)
        {
            //construye la coleccion
            Collections.Graphics _graphics = new Condesus.EMS.Business.RG.Collections.Graphics(this, startDate, endDate);

            return _graphics.PieScopesByFacility(idIndicatortCO2e, idFacility);

        }

       
    }
}
