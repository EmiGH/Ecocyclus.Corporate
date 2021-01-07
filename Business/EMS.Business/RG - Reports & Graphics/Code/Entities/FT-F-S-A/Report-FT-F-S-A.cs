using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG.Entities
{
    public class Report_FT_F_S_A
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

        internal Report_FT_F_S_A(Credential credential, PF.Entities.ProcessGroupProcess processGroupProcess)
        {
            _Credential = credential;
            _ProcessGroupProcess = processGroupProcess;

        }

        public List<IColumnsReport> FT(Int64 idIndicatortCO2e, Int64 idIndicatorCO2, Int64 idIndicatorCH4, Int64 idIndicatortnN2O,
            Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6, Int64 idIndicatorHCT, Int64 idIndicatorHCNM,
            Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10, Int64 idIndicatorCO, Int64 idIndicatorNOx,
            Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM, Int64 idIndicatorPM10, 
            DateTime startDate, DateTime endDate)
        {
            //construye la coleccion
            Collections.Report_FT_F_S_A_COL _report_FT_F_S_A_COL = new Condesus.EMS.Business.RG.Collections.Report_FT_F_S_A_COL(this, 
                idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, 
                idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, 
                idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, idIndicatorPM, idIndicatorPM10, startDate, endDate);

            return _report_FT_F_S_A_COL.FTs();
          
        }
    }
}
