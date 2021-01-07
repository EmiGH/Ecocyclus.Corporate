using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    public interface ISecurityEntity : ISecurity
    {

        #region Write region

        //Se usan para el delete de la entidad
        void SecurityPersonRemove();
        void SecurityJobTitleRemove();

        #endregion

        #region Versions Security Element

        #region ISecurityEntity 15-02-2010
        ////Security Remove
        //public void SecurityPersonRemove()
        //{//se usa cuando la baja se hace de este elemento
        //    new Security.Collections.Rights(Credential).Remove(this);
        //}
        //public void SecurityJobTitleRemove()
        //{//se usa cuando la baja se hace de este elemento
        //    new Security.Collections.Rights(Credential).Remove(this);
        //}
        #endregion

        #endregion
    }
}
