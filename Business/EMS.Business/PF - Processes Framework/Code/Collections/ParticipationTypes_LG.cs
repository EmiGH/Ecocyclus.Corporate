using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    public class ParticipationTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParticipationType; 
        #endregion

            internal ParticipationTypes_LG(Int64 idParticipationType, Credential credential)
        {
            _Credential = credential;
            _IdParticipationType = idParticipationType;
        }

        #region Read Functions
            public Entities.ParticipationType_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();
                //Condesus.EMS.DataAccess.PF.Entities.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG;

                Entities.ParticipationType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ParticipationTypes_LG_ReadById(_IdParticipationType, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ParticipationType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ParticipationType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //Condesus.EMS.DataAccess.PF.Entities.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG;
                //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();

                Dictionary<String, Entities.ParticipationType_LG> _oParticipationTypes_LG = new Dictionary<String, Entities.ParticipationType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ParticipationTypes_LG_ReadAll(_IdParticipationType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ParticipationType_LG _oParticipationType_LG = new Entities.ParticipationType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                    _oParticipationTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oParticipationType_LG);
                }

                return _oParticipationTypes_LG;
            }
            #endregion

        #region Write Functions
            public Entities.ParticipationType_LG Add(String idLanguage, String name)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //Condesus.EMS.DataAccess.PF.Entities.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG;
                    //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbProcessesFramework.ParticipationTypes_LG_Create(_IdParticipationType, idLanguage, name, _Credential.User.IdPerson);
                    return new Entities.ParticipationType_LG(idLanguage, name);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                    {
                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                    }
                    throw ex;
                }
            }
            public Entities.ParticipationType_LG Modify(String idLanguage, String name)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //Condesus.EMS.DataAccess.PF.Entities.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG;
                    //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();

                    _dbProcessesFramework.ParticipationTypes_LG_Update(_IdParticipationType, idLanguage, name, _Credential.User.IdPerson);
                    return new Entities.ParticipationType_LG(idLanguage, name);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                    {
                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                    }
                    throw ex;
                }
            }
            public void Remove(String idLanguage)
            {
                //Check to verify that the language option to be deleted is not default language
                if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //Condesus.EMS.DataAccess.PF.Entities.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG;
                    //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();

                    _dbProcessesFramework.ParticipationTypes_LG_Delete(_IdParticipationType, idLanguage, _Credential.User.IdPerson);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                    {
                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                    }
                    throw ex;
                }
        }
        #endregion
    }
}
