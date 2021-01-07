using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessResource
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdProcess;
        private Int64 _IdResource;
        private String _Comment;
        #endregion

        #region External Properties
        public String Comment
        {
            get { return _Comment; }
        }
        public Process Process
        {
            get
            {
                return new Collections.Processes(_Credential).Item(_IdProcess);
            }
        }
        public KC.Entities.Resource Resource
        {
            get
            {
                return new KC.Collections.Resources(_Credential).Item(_IdResource);
            }
        }
        #endregion

        internal ProcessResource(Int64 idProcess, Int64 idResource, String comment, Credential credential)
        {
            _Credential = credential;
            _IdProcess = idProcess;
            _IdResource = idResource;
            _Comment = comment;
        }

        public void Modify(String comment)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Modifico los datos de la base
                _dbProcessesFramework.ProcessResources_Update(_IdResource, _IdProcess, comment, _Credential.User.Person.IdPerson);
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

    }
}
