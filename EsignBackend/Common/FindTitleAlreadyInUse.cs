using DocumentFormat.OpenXml.Bibliography;
using EsignBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EsignBackend.Common
{
    public static class FindTitleAlreadyInUse
    {
        //For future use
        //public static bool IsNameAlreadyInUse<T>(this IEnumerable<T> dbSet, AppDbContext dbContext, string title)
        //{
        //    // case-insensitive (collation "Hebrew_CI_AS" case-insensitive) IQueryable<T>
        //    //return dbContext.Issplaces.Where(item => item.Title.Equals(title)).Count() != 0;

        //    if (Nullable.GetUnderlyingType(typeof(T)) != null)
        //    {
        //        dbSet = dbSet.Where(item => item.Title.Equals(title));
        //    }
        //}
    }
}
