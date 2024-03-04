using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
using Meteors.AspNetCore.Helper.ExtensionMethods.String;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Securty;
using Meteors.AspNetCore.Service.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Service
{
    public static class ContextExtension
    {

        /// <summary>
        /// note not work with in-memory
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortOptions"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> SortOptionsOrder<TEntity>(this IQueryable<TEntity> query, SortOptions sortOptions)
        {

            return sortOptions.Sort switch
            {
                Sort.Non => query,
                Sort.Ascending when CheckOrderAndThenByColumn<TEntity>(sortOptions) =>
                query.OrderBy(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)).ThenBy(o => EF.Property<TEntity>(o, sortOptions.ThenByColumn)),

                Sort.Ascending when CheckOrderByColumn<TEntity>(sortOptions) =>
                    query.OrderBy(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)),

                Sort.Descending when CheckOrderAndThenByColumn<TEntity>(sortOptions) =>
                    query.OrderByDescending(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)).ThenByDescending(o => EF.Property<TEntity>(o, sortOptions.ThenByColumn)),

                Sort.Descending when CheckOrderByColumn<TEntity>(sortOptions) =>
                    query.OrderByDescending(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)),

                _ => query,
            };
        }

        public static IQueryable<TEntity> Valid<TEntity>(this IQueryable<TEntity> query) where TEntity : IMrIdentity, IDeletable
        {
            return query.Where(q => !q.DateDeleted.HasValue);
        }

        public static IQueryable<TEntity> Invalid<TEntity>(this IQueryable<TEntity> query) where TEntity : IMrIdentity, IDeletable
        {
            return query.Where(q => q.DateDeleted.HasValue);
        }

       



        private static bool CheckOrderByColumn<TEntity>(SortOptions sortOptions)
        {
            return sortOptions.OrderByColumn.IsNotNullOrEmpty() && ObjectExtension.HasProperty<TEntity>(sortOptions.OrderByColumn);
        }

        private static bool CheckThenByColumn<TEntity>(SortOptions sortOptions)
        {
            return sortOptions.ThenByColumn.IsNotNullOrEmpty() && ObjectExtension.HasProperty<TEntity>(sortOptions.ThenByColumn);
        }

        private static bool CheckOrderAndThenByColumn<TEntity>(SortOptions sortOptions)
        {
            return CheckOrderByColumn<TEntity>(sortOptions) && CheckThenByColumn<TEntity>(sortOptions);
        }

        //public static IQueryable<TEntity> SortOptionsOrder<TEntity>(this IQueryable<TEntity> query, SortOptions sortOptions)
        //{
        //    //old way
        //    //return sortOptions switch
        //    //{
        //    //    { Sort: var sort } when sort == Sort.Non => query,

        //    //    { Sort: var sort, OrderByColumn: var orderByColumn, ThenByColumn: var thenByColumn } when
        //    //    sort == Sort.Ascending && !orderByColumn.IsNullOrEmpty() && !thenByColumn.IsNullOrEmpty()
        //    //    => query.OrderBy(o => EF.Property<object>(o, orderByColumn)).ThenBy(o => EF.Property<object>(o, thenByColumn)),

        //    //    { Sort: var sort, OrderByColumn: var orderByColumn } when
        //    //   sort == Sort.Ascending && !orderByColumn.IsNullOrEmpty()
        //    //   => query.OrderBy(o => EF.Property<object>(o, orderByColumn)),

        //    //    { Sort: var sort, OrderByColumn: var orderByColumn, ThenByColumn: var thenByColumn } when
        //    //    sort == Sort.Descending && !orderByColumn.IsNullOrEmpty() && !thenByColumn.IsNullOrEmpty()
        //    //    => query.OrderByDescending(o => EF.Property<object>(o, orderByColumn)).ThenByDescending(o => EF.Property<object>(o, thenByColumn)),

        //    //    { Sort: var sort, OrderByColumn: var orderByColumn } when
        //    //       sort == Sort.Descending && !orderByColumn.IsNullOrEmpty()
        //    //       => query.OrderByDescending(o => EF.Property<object>(o, orderByColumn)),

        //    //    _ => query,
        //    //};
        //}

    }
}
