using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.KC.Entities
{
    public class FileAttach
    {
        #region Internal Properties
            private Int64 _IdFile;
            private String _FileName;
            private Byte[] _FileStream;
        #endregion

        #region External Properties
            public Int64 IdFile
            {
                get { return _IdFile; }
            }
            public String FileName
            {
                get { return _FileName; }
            }
            public Byte[] FileStream
            {
                get 
                {
                    if (_FileStream == null)
                    { _FileStream = new Collections.FileAttachs().FileStream(_IdFile); }
                    return _FileStream; 
                }
            }
        #endregion

        internal FileAttach(Int64 idFile, String fileName)
        {
            _IdFile = idFile;
            _FileName = fileName;
            //_FileStream = fileStream;
        }
    }
}
