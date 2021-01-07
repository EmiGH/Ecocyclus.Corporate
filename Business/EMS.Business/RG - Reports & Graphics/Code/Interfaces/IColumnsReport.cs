using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.RG
{
    public interface IColumnsReport
    {
        Int64 Id { get; set; }
        //nombrr de la entidad
        String Name { get; set; }
        //Retorna todos icolum que dependen de el
        List<IColumnsReport> Items();
        
        //Retorna todos icolum que dependen de el
        List<IColumnsReport> Child();
        
        //resultado del obj
         Double Result_tCO2e{ get; set; }
         Double Result_CO2{ get; set; }
         Double Result_CH4{ get; set; }
         Double Result_N2O{ get; set; }
         Double Result_PFC{ get; set; }
         Double Result_HFC{ get; set; }
         Double Result_SF6{ get; set; }
         Double Result_HCT{ get; set; }
         Double Result_HCNM{ get; set; }
         Double Result_C2H6{ get; set; }
         Double Result_C3H8{ get; set; }
         Double Result_C4H10{ get; set; }
         Double Result_CO{ get; set; }
         Double Result_NOx{ get; set; }
         Double Result_SOx{ get; set; }
         Double Result_SO2{ get; set; }
         Double Result_H2S{ get; set; }
         Double Result_PM{ get; set; }
         Double Result_PM10{ get; set; }
    }
}
