using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.DS.Collections
{
    public class DashBoards
    {
        public DashBoards() { }

        #region Write Functions
        //internal void Add(Entities.Person person, Dictionary<String, DS.Entities.Gadgets.Gadget> Gadgets)
        //{
        //    try
        //    {
        //        //Objeto de data layer para acceder a datos
        //        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
        //        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

        //        //borra todos los gadgets que tiene ese user
        //        Remove(person);

        //        //da de alta la nueva configuracion de gadgets
        //        foreach (Entities.Gadgets.Gadget _gadget in Gadgets.Values)
        //        {
        //            _dbDirectoryServices.DashBoard_Create(person.IdPerson, person.Organization.IdOrganization, _gadget.IdGadget, _gadget.Configuration);
        //        }

        //    }
        //    catch (SqlException ex)
        //    {
        //        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
        //        {
        //            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
        //        }
        //        throw ex;
        //    }
        //}
        internal void Remove(Entities.Person person)
        {
            try
            {
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                _dbDirectoryServices.DashBoard_Delete(person.IdPerson, person.Organization.IdOrganization);

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
