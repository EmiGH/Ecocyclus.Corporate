using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.KC.Collections
{
    internal class FileAttachs
    {
        #region Internal Properties
        #endregion

        internal FileAttachs(){}

        #region Read Common Functions
            internal Entities.FileAttach Item(Int64 idFile)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                Entities.FileAttach _FileAttach = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.FileAttaches_ReadById(idFile);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_FileAttach == null)
                    {
                        _FileAttach = new Condesus.EMS.Business.KC.Entities.FileAttach(Convert.ToInt64(_dbRecord["IdFile"]), Convert.ToString(_dbRecord["FileName"]));
                    }
                    else
                    {
                        return new Condesus.EMS.Business.KC.Entities.FileAttach(Convert.ToInt64(_dbRecord["IdFile"]), Convert.ToString(_dbRecord["FileName"]));
                    }
                }
                return _FileAttach;
            }
            internal Byte[] FileStream(Int64 idFile)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                Byte[] _FileStream = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.FileAttaches_ReadFileStream(idFile);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                     _FileStream = (Byte[])(_dbRecord["FileStream"]);
                }
                return _FileStream;
            }
        #endregion
    }
}
