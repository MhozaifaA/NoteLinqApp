using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Meteors.AspNetCore.Infrastructure.InfraExtensions
{
    public static class MrTransactionExtention
    {

        /// <summary>
        ///  Execute Action after transaction finished
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        public static TResult TranDone<TEntity, TResult>(this TEntity result, Func<TEntity, TResult> func) => func.Invoke(result);


        /// <summary>
        ///  Execute Action after transaction finished
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        public static void TranDone<TEntity>(this TEntity result, Action<TEntity> func) => func.Invoke(result);


        //public static void TranComplete(this TransactionScope tran)
        //   => tran.Complete(); 

        /// <summary>
        /// open transaction scope with acion executting
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">action parameter</param>
        /// <param name="action">action need executing  in transaction scope </param>
        /// <returns>return TransactionScope instance to execute other action inside it or complete it ...</returns>
        public static TransactionScope OpenTran<TEntity>(this TEntity entity , Action<TEntity> action)
        {
            TransactionScope scope = new TransactionScope();

            try
            {
                action.Invoke(entity);
            }
            catch
            {
                scope.Dispose();
                throw;
            }
         
            return scope;
        }


        /// <summary>
        /// open transaction scope with acion executting
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">action parameter</param>
        /// <param name="action">action need executing  in transaction scope </param>
        /// <returns>return TransactionScope instance and action result as tuple</returns>
        public static (TransactionScope , TResult) OpenTran<TEntity , TResult>(this TEntity entity , Func<TEntity , TResult> action)
        {
            TransactionScope scope = new TransactionScope();
            try
            {
                var res = action.Invoke(entity);
                return (scope, res);
            }
            catch
            {
                scope.Dispose();
                throw;
            }
            
        }

        //public static TransactionScope Then<TEntity>(this (TransactionScope  scope, TEntity entity) scope, Action<TEntity> tran)
        //{
        //    tran.Invoke(scope.entity);
        //    return scope.scope;
        //}

        /// <summary>
        /// open transaction scope with acion executting
        /// </summary>
        /// <typeparam name="TEntity">instance need to passed to action </typeparam>
        /// <param name="tranScope">tuple of Action parameter and TransactionScope within which the action will be executed</param>
        /// <param name="action">action need executing  in transaction scope </param>
        /// <returns>return TransactionScope instance and action result as tuple</returns>
        public static (TransactionScope scope, TEntity entity) Then<TEntity>(this (TransactionScope  scope, TEntity entity) tranScope, Action<TEntity> action)
        {
            try
            {
                action.Invoke(tranScope.entity);
            }
            catch
            {
                tranScope.scope.Dispose();
                throw;
            }
            return tranScope;
        }

        /// <summary>
        /// Execute action inside TransactionScope which opened in OpenTran method
        /// </summary>
        /// <param name="scope">TransactionScope whith execute action inside it </param>
        /// <param name="action">action need executing  in transaction scope </param>
        /// <returns>return TransactionScope instance to execute other action inside it or complete it ...</returns>
        public static TransactionScope Then(this TransactionScope scope, Action action)
        {
            try
            {
                action.Invoke();
                return scope;
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }



        /// <summary>
        /// close transactionScope which opened inside OpenTran method
        /// </summary>
        /// <param name="scope">TransactionScope with execute action inside it </param>
        public static void Complete(this TransactionScope scope)
        {
            scope.Complete();
            scope.Dispose();
        }

        /// <summary>
        /// close transactionScope which opened inside OpenTran method
        /// </summary>
        /// <param name="scope">TransactionScope with execute action inside it </param>
        public static void Complete<TEntity>(this (TransactionScope scope, TEntity entity) tranScope)
        {
            tranScope.scope.Complete();
            tranScope.scope.Dispose();                      
        }

    }
}
