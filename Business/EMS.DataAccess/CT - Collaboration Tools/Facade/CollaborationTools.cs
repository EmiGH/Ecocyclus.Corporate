using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.CT
{
    public class CollaborationTools
    {
        # region Forum
        
        # region Facade_ForumCategories
            #region Read Functions
        public IEnumerable<DbDataRecord> ForumCategories_ReadAll(String IdLanguage)
            {
                return new Entities.ForumCategories().ReadAll(IdLanguage);
            }
        public IEnumerable<DbDataRecord> ForumCategories_ReadById(Int64 IdCategory, String IdLanguage)
            {
                return new Entities.ForumCategories().ReadById(IdCategory,IdLanguage);
            }
            #endregion
            #region FK's
            #endregion
            #region Write Functions
            #endregion
        #endregion

        # region Facade_ForumCategories_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> ForumCategories_LG_ReadAll(Int64 IdCategory)
        {
            return new Entities.ForumCategories_LG().ReadAll(IdCategory);
        }
        public IEnumerable<DbDataRecord> ForumCategories_LG_ReadById(Int64 IdCategory, String IdLanguage)
        {
            return new Entities.ForumCategories_LG().ReadById(IdCategory,IdLanguage);
        }
        #endregion
        #region FK's
        #endregion
        #region Write Functions
        public void ForumCategories_LG_Create(Int64 IdCategory,String IdLanguage,String Name,String Desciption)
        {
            new Entities.ForumCategories_LG().Create(IdCategory,IdLanguage,Name,Desciption);
        }

        public void ForumCategories_LG_Delete(Int64 IdCategory,String IdLanguage)
        {
             new Entities.ForumCategories_LG().Delete(IdCategory,IdLanguage);
        }

        public void ForumCategories_LG_Update(Int64 IdCategory,String IdLanguage,String Name,String Desciption)
        {
            new Entities.ForumCategories_LG().Update(IdCategory,IdLanguage,Name,Desciption);
        }
        #endregion
        #endregion

        # region Facade_ForumForums

        #region Read Functions
        public IEnumerable<DbDataRecord> ForumForums_ReadAll(String IdLanguage)
        {
            return new Entities.ForumForums().ReadAll(IdLanguage);
        }
        public IEnumerable<DbDataRecord> ForumForums_ReadById(Int64 IdForum, String IdLanguage)
        {
            return new Entities.ForumForums().ReadById(IdForum,IdLanguage);
        }
        #endregion
        #region FK's
        #endregion
        #region Write Functions
        public Int64 ForumForums_Create(Boolean IsActive)
        {
            return new Entities.ForumForums().Create(IsActive);
        }

        public void ForumForums_Delete(Int64 IdForum)
        {
             new Entities.ForumForums().Delete(IdForum);
        }

        public void ForumForums_Update(Int64 IdForum,Boolean IsActive)
        {
            new Entities.ForumForums().Update(IdForum,IsActive);
        }
        #endregion

        #endregion

        # region Facade_ForumForums_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> ForumForums_LG_ReadAll(Int64 IdForum)
        {
            return new Entities.ForumForums_LG().ReadAll(IdForum);
        }
        public IEnumerable<DbDataRecord> ForumForums_LG_ReadById(Int64 IdForum, String IdLanguage)
        {
            return new Entities.ForumForums_LG().ReadById(IdForum,IdLanguage);
        }
        #endregion
        #region FK's
        #endregion
        #region Write Functions
        public void ForumForums_LG_Create(Int64 IdForum,String IdLanguage,String Name,String Description)
        {
            new Entities.ForumForums_LG().Create(IdForum,IdLanguage,Name,Description);
        }

        public void ForumForums_LG_Delete(Int64 IdForum, String IdLanguage)
        {
             new Entities.ForumForums_LG().Delete(IdForum,IdLanguage);
        }

        public void ForumForums_LG_Delete(Int64 IdForum)
        {
            new Entities.ForumForums_LG().Delete(IdForum);
        }

        public void ForumForums_LG_Update(Int64 IdForum, String IdLanguage, String Name, String Description)
        {
            new Entities.ForumForums_LG().Update(IdForum,IdLanguage,Name,Description);
        }
        #endregion
        #endregion

        # region Facade_ForumMessages
        #region Read Functions
        public IEnumerable<DbDataRecord> ForumMessages_ReadAll()
        {
            return new Entities.ForumMessages().ReadAll();
        }
        public IEnumerable<DbDataRecord> ForumMessages_ReadById(Int64 IdMessage)
        {
            return new Entities.ForumMessages().ReadById(IdMessage);
        }
        public IEnumerable<DbDataRecord> ForumMessages_ReadByPerson(Int64 IdPerson)
        {
            return new Entities.ForumMessages().ReadByPerson(IdPerson);
        }
        public IEnumerable<DbDataRecord> ForumMessages_ReadByTopic(Int64 IdTopic)
        {
            return new Entities.ForumMessages().ReadByTopic(IdTopic);
        }
        #endregion
        #region FK's
        #endregion
        #region Write Functions
        public Int64 ForumMessages_Create(Int64 IdTopic, Int64 IdPerson, DateTime PostedDate, DateTime LastEditedDate, String Text, Boolean IsNormal)
        {
            return new Entities.ForumMessages().Create(IdTopic, IdPerson, PostedDate, LastEditedDate, Text, IsNormal);
        }

        public void ForumMessages_Delete(Int64 IdMessage)
        {
            new Entities.ForumMessages().Delete(IdMessage);
        }

        public void ForumMessages_Update(Int64 IdMessage, DateTime LastEditedDate, String Text)
        {
            new Entities.ForumMessages().Update(IdMessage, LastEditedDate, Text);
        }
        public void ForumMessages_Update(Int64 IdMessage, Boolean isNormal)
        {
            new Entities.ForumMessages().ChangeType(IdMessage, isNormal);
        }
        #endregion
        #endregion

        # region Facade_ForumPolls
        #region Read Functions
        public IEnumerable<DbDataRecord> ForumPolls_ReadAll()
        {
            return new Entities.ForumPolls().ReadAll();
        }
        public IEnumerable<DbDataRecord> ForumPolls_ReadById(Int64 IdPoll)
        {
            return new Entities.ForumPolls().ReadById(IdPoll);
        }
        public IEnumerable<DbDataRecord> ForumPolls_ReadByMessage(Int64 idMessage)
        {
            return new Entities.ForumPolls().ReadByMessage(idMessage);
        }
        #endregion
        #region FK's
        #endregion
        #region Write Functions
        public Int64 ForumPolls_Create(Int64 IdPerson, Int64 IdMessage, DateTime PollDate, Int16 Value)
        {
            return new Entities.ForumPolls().Create(IdPerson, IdMessage, PollDate, Value);
        }

        public void ForumPolls_Delete(Int64 IdPoll)
        {
            new Entities.ForumPolls().Delete(IdPoll);
        }

        public void ForumPolls_Update(Int64 IdPoll, Int16 Value)
        {
            new Entities.ForumPolls().Update(IdPoll,Value);
        }
        #endregion
        #endregion

        # region Facade_ForumTopics
        #region Read Functions
        public IEnumerable<DbDataRecord> ForumTopics_ReadAll()
        {
            return new Entities.ForumTopics().ReadAll();
        }
        public IEnumerable<DbDataRecord> ForumTopics_ReadById(Int64 IdTopic)
        {
            return new Entities.ForumTopics().ReadById(IdTopic);
        }
        public IEnumerable<DbDataRecord> ForumTopics_ReadByForum(Int64 IdForum)
        {
            return new Entities.ForumTopics().ReadByForum(IdForum);
        }
        public IEnumerable<DbDataRecord> ForumTopics_ReadByPerson(Int64 IdPerson)
        {
            return new Entities.ForumTopics().ReadByPerson(IdPerson);
        }
        public IEnumerable<DbDataRecord> ForumTopics_ReadByCategory(Int64 IdCategory)
        {
            return new Entities.ForumTopics().ReadByCategory(IdCategory);
        }
        #endregion
        #region FK's
        #endregion
        #region Write Functions
        public Int64 ForumTopics_Create(Int64 IdForum, Int64 IdCategory, Int64 IdPerson, DateTime PostedDate, String Title, Int64 MaxAttachmentSize, Boolean AllowResponses, Boolean IsModerated, Boolean IsActive)
        {
            return new Entities.ForumTopics().Create(IdForum, IdCategory, IdPerson, PostedDate, Title, MaxAttachmentSize, AllowResponses, IsModerated, IsActive);
        }

        public void ForumTopics_Delete(Int64 IdTopic)
        {
            new Entities.ForumTopics().Delete(IdTopic);
        }

        public void ForumTopics_Update(Int64 IdTopic, Int64 IdCategory, String Title, Int64 MaxAttachmentSize, Boolean AllowResponses, Boolean IsModerated)
        {
            new Entities.ForumTopics().Update(IdTopic, IdCategory, Title, MaxAttachmentSize, AllowResponses, IsModerated);
        }
        public void ForumTopics_Update(Int64 IdTopic, Boolean IsActive)
        {
            new Entities.ForumTopics().Update(IdTopic, IsActive);
        }
        #endregion
        #endregion

        # region Facade_ForumWordFilters
        #region Read Functions
        public IEnumerable<DbDataRecord> ForumWordFilters_ReadAll()
        {
            return new Entities.ForumWordFilters().ReadAll();
        }
        public IEnumerable<DbDataRecord> ForumWordFilters_ReadById(Int64 IdFilter)
        {
            return new Entities.ForumWordFilters().ReadById(IdFilter);
        }
        #endregion
        #region FK's
        #endregion
        #region Write Functions
        public Int64 ForumWordFilters_Create(String Word, String ReplaceWord)
        {
            return new Entities.ForumWordFilters().Create(Word, ReplaceWord);
        }

        public void ForumWordFilters_Delete(Int64 IdFilter)
        {
            new Entities.ForumWordFilters().Delete(IdFilter);
        }

        public void ForumWordFilters_Update(Int64 IdFilter, String Word, String ReplaceWord)
        {
            new Entities.ForumWordFilters().Update(IdFilter, Word, ReplaceWord);
        }
        #endregion
        #endregion

       
        #endregion
    }
}
