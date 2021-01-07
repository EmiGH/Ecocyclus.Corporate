using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    public interface ITransformer : IObjectIdentifier, ILanguageOption
    {
        /// <summary>
        /// Devuelve los valores que hay que transformar, si viene vacio es porque no hay nada para calcular
        /// </summary>
        /// <returns></returns>
        IEnumerable<System.Data.Common.DbDataRecord> TransformValues(PA.Entities.CalculateOfTransformation transformation);

        PA.Entities.CalculateOfTransformation Transformation(Int64 idTransformation);

        Dictionary<Int64, PA.Entities.CalculateOfTransformation> Transformations { get; }

        PA.Entities.CalculateOfTransformation TransformationAdd(PA.Entities.Indicator indicatorTransformation,
            PA.Entities.MeasurementUnit measurementUnit, String formula, String name, String description, PA.Entities.AccountingActivity activity, 
            Dictionary<String, IOperand> operands, List<NT.Entities.NotificationRecipient> notificationRecipients);

        void Remove(PA.Entities.CalculateOfTransformation calculationOfTransformation);

        //Valida q la formula este correcta
        void EvaluateFormula(String formula);

    }
}
