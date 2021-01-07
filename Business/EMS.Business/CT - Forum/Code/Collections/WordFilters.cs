// Generated by Pnyx Generation tool at :06/04/2009 03:46:02 p.m.
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Collections
{
    internal class WordFilters
    {
        private Credential _Credential;

        internal WordFilters(Credential credential) 
        {
            _Credential = credential;
        }

        //#region Read Functions
        ///// <summary>
        ///// Retorna ForumWordFilters
        ///// </summary>
        ///// <returns></returns>
        //internal Dictionary<Decimal, Entities.WordFilter> Items()
        //{
        //    Dictionary<Decimal, Entities.WordFilter> _items = new Dictionary<Decimal, Entities.WordFilter>();
        //    DataAccess.CT.EntitiesModel.Entities Entities = new DataAccess.CT.EntitiesModel.Entities();
        //    ObjectQuery<DataAccess.CT.EntitiesModel.CT_ForumWordFilters> mainQuery = Entities.CT_ForumWordFilters;
             
        //     foreach (DataAccess.CT.EntitiesModel.CT_ForumWordFilters item in mainQuery.Execute(MergeOption.NoTracking))
        //     {
        //         CT.Entities.WordFilter _wordFilter = new Condesus.EMS.Business.CT.Entities.WordFilter(item);
        //         _items.Add(item.IdFilter ,_wordFilter);
        //     } 
        //    return _items;
        //}
        ///// <summary>
        ///// Retorna ForumWordFilters por ID
        ///// </summary>
        ///// <param name="IdMessage"></param>
        ///// <returns></returns>
        //internal Entities.WordFilter Item(decimal IdFilter)
        //{
        //    DataAccess.CT.EntitiesModel.Entities Entities = new DataAccess.CT.EntitiesModel.Entities();
        //    Dictionary<Decimal, Entities.WordFilter> _items = new Dictionary<Decimal, Entities.WordFilter>();
        //    ObjectQuery<DataAccess.CT.EntitiesModel.CT_ForumWordFilters> mainQuery = Entities.CT_ForumWordFilters;
        //    ObjectQuery<DataAccess.CT.EntitiesModel.CT_ForumWordFilters> queryByID = mainQuery.Where("it.IdFilter = " + IdFilter);

        //    foreach (DataAccess.CT.EntitiesModel.CT_ForumWordFilters item in queryByID.Execute(MergeOption.NoTracking))
        //    {
        //        CT.Entities.WordFilter _wordFilter = new Condesus.EMS.Business.CT.Entities.WordFilter(item);
        //        _items.Add(item.IdFilter, _wordFilter);
        //    }
        //    if (_items.Count != 0)
        //    {
        //      return (Entities.WordFilter)_items[0];  
        //    }
        //    else
        //    {
        //        return null;
        //    }
        

        //}

        //#endregion
        //#region Write Functions
        ////Crea ForumWordFilters
        //internal void Create(string Word,string ReplaceWord)
        //{
        //    DataAccess.CT.EntitiesModel.CT_ForumWordFilters itemResult = new DataAccess.CT.EntitiesModel.CT_ForumWordFilters();

        //    DataAccess.CT.EntitiesModel.Entities itemContext = new DataAccess.CT.EntitiesModel.Entities();
        //    itemContext.AddToCT_ForumWordFilters(itemResult);
        //    itemContext.SaveChanges();
        //}
        //internal void DeleteByID(decimal IdFilter)
        //{
        //    DataAccess.CT.EntitiesModel.Entities Entities = new DataAccess.CT.EntitiesModel.Entities();

        //    ObjectQuery<DataAccess.CT.EntitiesModel.CT_ForumWordFilters> mainQuery = Entities.CT_ForumWordFilters;
        //    ObjectQuery<DataAccess.CT.EntitiesModel.CT_ForumWordFilters> queryByID = mainQuery.Where("it.IdFilter = " + IdFilter);

        //    DataAccess.CT.EntitiesModel.Entities itemResultContext = new DataAccess.CT.EntitiesModel.Entities();
        //    ObjectResult itemResult = queryByID.Execute(MergeOption.NoTracking);

        //    itemResultContext.DeleteObject(itemResult);
        //    itemResultContext.SaveChanges();

        //}

        //internal void Update(decimal IdFilter,string Word,string ReplaceWord)
        //{

        //    DataAccess.CT.EntitiesModel.Entities Entities = new DataAccess.CT.EntitiesModel.Entities();

        //    ObjectQuery<DataAccess.CT.EntitiesModel.CT_ForumWordFilters> mainQuery = Entities.CT_ForumWordFilters;
        //    DataAccess.CT.EntitiesModel.Entities itemResultContext = new DataAccess.CT.EntitiesModel.Entities();
        //    ObjectQuery<DataAccess.CT.EntitiesModel.CT_ForumWordFilters> queryByID = mainQuery.Where("it.ReplaceWord = " + ReplaceWord);
        //    ObjectResult itemResult = queryByID.Execute(MergeOption.NoTracking);

        //    itemResultContext.DeleteObject(itemResult);
        //    itemResultContext.SaveChanges();

        //}
        //#endregion
    }
        
}

