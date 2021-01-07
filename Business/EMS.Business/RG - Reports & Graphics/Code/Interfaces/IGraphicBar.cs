using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.RG
{
    public interface IGraphicBar
    {
        Int64 Id { get; set; }
        //nombrr de la entidad
        String Name { get; set; }

        //resultado del obj
        Decimal Result_tCO2e { get; set; }
        Decimal Result_CO2 { get; set; }
        Decimal Result_CH4 { get; set; }
        Decimal Result_N2O { get; set; }
        Decimal Result_PFC { get; set; }
        Decimal Result_HFC { get; set; }
        Decimal Result_SF6 { get; set; }
        Decimal Result_HCT { get; set; }
        Decimal Result_HCNM { get; set; }
        Decimal Result_C2H6 { get; set; }
        Decimal Result_C3H8 { get; set; }
        Decimal Result_C4H10 { get; set; }
        Decimal Result_CO { get; set; }
        Decimal Result_NOx { get; set; }
        Decimal Result_SOx { get; set; }
        Decimal Result_SO2 { get; set; }
        Decimal Result_H2S { get; set; }
        Decimal Result_PM { get; set; }
        Decimal Result_PM10 { get; set; }
    }
}
