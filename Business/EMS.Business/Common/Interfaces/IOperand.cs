using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    public interface IOperand : IObjectIdentifier, ILanguageOption
    {      
     
        /// <summary>
        /// Devuleve el valor para hacer la operacion en para la fecha solocitada
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Double OperateValue(PA.Entities.CalculateOfTransformation transformation, DateTime startDate, DateTime endDate);
        
    }
}
