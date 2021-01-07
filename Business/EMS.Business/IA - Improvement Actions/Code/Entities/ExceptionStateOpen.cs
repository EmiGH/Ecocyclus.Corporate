using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    public class ExceptionStateOpen : ExceptionState
    {
        internal ExceptionStateOpen(Int64 idExceptionState, String idLanguage, String name, Credential credential)
            : base (idExceptionState, idLanguage, name, credential)
        { }

        internal override ExceptionState Treat(Int64 idException, String comment)
        {
            //cambia en la base a estado tratado (2)
            //Objeto de data layer para acceder a datos
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            _dbImprovementActions.ExceptionStates_ModifyState(idException,Common.Constants.ExceptionStateTreat, comment, DateTime.Now, Credential.User.IdPerson);

            return new Collections.ExceptionStates(Credential).Item(Common.Constants.ExceptionStateTreat);
        }

        internal override ExceptionState Close(Int64 idException, String comment)
        {
            //cambia en la base a estado Cerrado (3)
            //Objeto de data layer para acceder a datos
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            _dbImprovementActions.ExceptionStates_ModifyState(idException, Common.Constants.ExceptionStateClose, comment, DateTime.Now, Credential.User.IdPerson);

            return new Collections.ExceptionStates(Credential).Item(Common.Constants.ExceptionStateClose); 
        }
    }
}
