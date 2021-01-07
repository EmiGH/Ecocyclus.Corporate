using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.RG
{
    public interface IGraphicPie
    {
        Int64 Id { get; set; }
        //nombrr de la entidad
        String Name { get; set; }
        
        //resultado del obj   
         Decimal Value{ get; set; }
         Decimal Percentage { get; set; }  
    }
}
