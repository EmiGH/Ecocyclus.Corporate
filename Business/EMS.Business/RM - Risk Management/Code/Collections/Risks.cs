using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.RM.Collections
{
    public class Risks
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal Risks(Credential credential)
        {
            _Credential = credential; 
        }

        #region Read Functions
        //internal Entities.RiskClassification Item(Int64 idRiskClassification)
        //{
        //    //Objeto de data layer para acceder a datos
        //    DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

        //    Entities.RiskClassification _RiskClassification = null;
        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbRiskManagement.RiskClassifications_ReadById(idRiskClassification, _Credential.CurrentLanguage.IdLanguage, Common.Security.ResourceClassification, _Credential.User.IdPerson);
        //    //si no trae nada retorno 0 para que no de error
        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
        //    {
        //        if ((_IdParent == 0) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdRiskClassification"], 0)) == _IdParent))
        //        {
        //            if (_RiskClassification == null)
        //            {
        //                _RiskClassification = new Entities.RiskClassification(Convert.ToInt64(_dbRecord["IdRiskClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentRiskClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
        //                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
        //                {
        //                    return _RiskClassification;
        //                }
        //            }
        //            else
        //            {
        //                return new Entities.RiskClassification(Convert.ToInt64(_dbRecord["IdRiskClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentRiskClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
        //            }
        //        }
        //        else
        //        {
        //            //no es hijo asi que no lo puede devolver......generar el error
        //            return null;
        //        }
        //    }
        //    return _RiskClassification;
        //}
        internal Dictionary<Int64, Entities.Risk> Items()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();
            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.Risk> _oItems = new Dictionary<Int64, Entities.Risk>();

            //TODO, sacar esta linea
            return _oItems;
            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            _record = _dbRiskManagement.RiskClassifications_GetRoot(_Credential.CurrentLanguage.IdLanguage, Common.Security.ResourceClassification, _Credential.User.IdPerson); 


            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {                
                    //Declara e instancia una posicion
                    Entities.Risk _risk = new Entities.Risk();

                    //Lo agrego a la coleccion
                    _oItems.Add(_risk.IdRisk, _risk);
            }
            return _oItems;
        }
        #endregion
    }
}
