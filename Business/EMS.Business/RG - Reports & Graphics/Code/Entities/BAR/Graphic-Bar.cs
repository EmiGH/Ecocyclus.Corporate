using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG.Entities
{
    public class Graphic_Bar
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

        internal Graphic_Bar(Credential credential, PF.Entities.ProcessGroupProcess processGroupProcess)
        {
            _Credential = credential;
            _ProcessGroupProcess = processGroupProcess;

        }
      

        public List<IGraphicBar> Bar_FacilityTypeByScope(Int64 idScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
            Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            //construye la coleccion
            Collections.Graphics _graphics = new Condesus.EMS.Business.RG.Collections.Graphics(this, startDate, endDate);

            return _graphics.BarTotalGasesFacilityType_by_Scope(idScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, 
                idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, 
                idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,idIndicatorPM, idIndicatorPM10);
        }
        public List<IGraphicBar> Bar_ActivityByScope(Int64 idScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
          Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
          Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
          Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
          Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            //construye la coleccion
            Collections.Graphics _graphics = new Condesus.EMS.Business.RG.Collections.Graphics(this, startDate, endDate);

            return _graphics.BarTotalGasessActivity_by_Scope(idScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC,
                idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO,
                idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, idIndicatorPM, idIndicatorPM10);
        }

        public List<IGraphicBar> Bar_ActivityByScopeAndFacility(Int64 idScope, Int64 idFacility, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
         Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
         Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
         Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
         Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            //construye la coleccion
            Collections.Graphics _graphics = new Condesus.EMS.Business.RG.Collections.Graphics(this, startDate, endDate);

            return _graphics.BarTotalGasessActivity_by_ScopeAndFacility(idScope, idFacility, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC,
                idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO,
                idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, idIndicatorPM, idIndicatorPM10);
        }

        public List<IGraphicBar> Bar_TotalGasesState_by_Scope(List<Int64> geoAreas, Int64 idScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
        Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
        Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
        Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S, Int64 idIndicatorPM,
        Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            //construye la coleccion
            Collections.Graphics _graphics = new Condesus.EMS.Business.RG.Collections.Graphics(this, startDate, endDate);

            return _graphics.BarTotalGasesState_by_Scope(geoAreas, idScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4, idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC,
                idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6, idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO,
                idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S, idIndicatorPM, idIndicatorPM10);
        }
    }
}
