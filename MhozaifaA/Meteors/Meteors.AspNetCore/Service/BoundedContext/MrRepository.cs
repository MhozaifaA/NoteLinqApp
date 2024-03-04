using Meteors.AspNetCore.Common.AuxiliaryImplemental.Interfaces;
using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
using Meteors.AspNetCore.Infrastructure.EntityFramework.Context;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
using Meteors.AspNetCore.Service.BoundedContext.General;
using Meteors.AspNetCore.Service.BoundedContext.Store;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Meteors.AspNetCore.Service.Options;
using Meteors.AspNetCore.Core.Shared;
using Meteors.AspNetCore.Common.Services.MimeKit;
using Meteors.AspNetCore.MVC.Filter.Options;
using Meteors.AspNetCore.MVC.Filter;
using Meteors.AspNetCore.Helper.ExtensionMethods.Exception;
using Meteors.AspNetCore.Helper.ExtensionMethods.Enumerable;
using Meteors.AspNetCore.Helper.ExtensionMethods.String;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Localization;
using Meteors.OperationContext;

namespace Meteors.AspNetCore.Service.BoundedContext
{


    public class MrRepository<TKey> : IMrRepository<TKey> , IDisposable where TKey : struct, IEquatable<TKey> 
    {
     
        /// <summary>
        /// Implement <see cref="IDisposable"/>.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Base layer of <see cref="Base.RepositoryBase"/> injected <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class MrRepository<TContext, TKey> : MrRepository<TKey>, IDisposable where TContext : IMrIdentityDbContext<TKey> where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// Main database sqlserver ef context.
        /// </summary>
        protected TContext Context;


        protected MrRepositoryOptions Options;

        /// <summary>
        /// Default constructure
        /// </summary>
        public MrRepository(TContext context)
        {
            this.Context = context;
            Options = context.GetService<IOptions<MrRepositoryOptions>>()?.Value??new();
        }


        #region -   IQueryable   -

        protected virtual IQueryable<TEntity> _query<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetAsNoTracking<TEntity>();
        protected virtual IQueryable<TEntity> _trackingQuery<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetOrder<TEntity>();


        protected virtual IQueryable<TEntity> _allQuery<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetUnValidAsNoTracking<TEntity>();
        protected virtual IQueryable<TEntity> _allTrackingQuery<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetUnValid<TEntity>();

      
        #endregion


        #region -   IEnumerable   -

        protected virtual IEnumerable<TEntity> AsEnumerable<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetAsNoTracking<TEntity>().AsEnumerable();
        protected IEnumerable<TEntity> TrackingAsEnumerable<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetOrder<TEntity>().AsEnumerable();


        protected virtual IEnumerable<TEntity> AllAsEnumerable<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetUnValidAsNoTracking<TEntity>().AsEnumerable();
        protected virtual IEnumerable<TEntity> AllTrackingAsEnumerable<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetUnValid<TEntity>().AsEnumerable();

        #endregion


        #region -   List   -

        protected virtual List<TEntity> _toList<TEntity>() where TEntity : class, IBaseEntity<TKey>
        => SetAsNoTracking<TEntity>().ToList();
        protected List<TEntity> _trackingToList<TEntity>() where TEntity : class, IBaseEntity<TKey>
        => SetOrder<TEntity>().ToList();

        protected virtual List<TEntity> _allToList<TEntity>() where TEntity : class, IBaseEntity<TKey>
        => SetUnValidAsNoTracking<TEntity>().ToList();
        protected List<TEntity> _allTrackingToList<TEntity>() where TEntity : class, IBaseEntity<TKey>
        => SetUnValid<TEntity>().ToList();



        protected virtual async Task<List<TEntity>> _toListAsync<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => await SetAsNoTracking<TEntity>().ToListAsync();
        protected async Task<List<TEntity>> _trackingToListAsync<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => await SetOrder<TEntity>().ToListAsync();

        protected virtual async Task<List<TEntity>> _allToListAsync<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => await SetUnValidAsNoTracking<TEntity>().ToListAsync();
        protected async Task<List<TEntity>> _allTrackingToListAsync<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => await SetUnValid<TEntity>().ToListAsync();

        #endregion


        #region -   Find   -

        protected virtual TEntity _findById<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>
            => Context.Find<TEntity>(key);

        protected virtual ValueTask<TEntity> _findByIdAsync<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>
          => Context.FindAsync<TEntity>(key);

        #endregion





        protected virtual async Task<OperationResult<T>> RepositoryHandler<T>(Func<OperationResult<T>, Task<OperationResult<T>>> action)
        {
            OperationResult<T> operation = new OperationResult<T>();

            try
            {
                return await action(operation);
            }
            catch (Exception e)
            {
                operation.SetException(e);

                if(Options.LoggingExceptionHandler)
                {
                    try
                    {
                        var filter = Context.GetService<IOptions<MrMvcFilterOptions>>()?.Value ?? new();

                        if(filter.ExceptionFilter?.EnableLoggingToEmail == true)
                        {
                            var mailServce = Context.GetService<IMailService>();
                            await mailServce.SendEmailAsync(new MailBody(
                                             toEmail: filter.ExceptionFilter.LoggingEmail.ToEmail,
                                             subject: filter.ExceptionFilter.LoggingEmail.Subject,
                                             body: operation.FullExceptionMessage));
                        }

                    }
                    catch (Exception ec)
                    {
                        throw new Exception($"From {nameof(MrExceptionFilter)}: {ec.ToFullException()}");
                    }
                }
            }
            return operation;
        }


        #region -   Private   -

        private DbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity<TKey> => this.Context.Set<TEntity>();

        private IQueryable<TEntity> SetOrder<TEntity>() where TEntity : class, IBaseEntity<TKey> => this.Set<TEntity>().SortOptionsOrder(Options.SortOptions);

        private IQueryable<TEntity> SetAsNoTracking<TEntity>() where TEntity : class, IBaseEntity<TKey> => SetOrder<TEntity>().AsNoTracking();

        private IQueryable<TEntity> SetUnValidAsNoTracking<TEntity>() where TEntity : class, IBaseEntity<TKey>
            => SetUnValid<TEntity>().AsNoTracking();
        //=> SetAsNoTracking<TEntity>().Where((Expression<Func<TEntity, bool>>)(entity => !entity.DateDeleted.HasValue));

        private IQueryable<TEntity> SetUnValid<TEntity>() where TEntity : class, IBaseEntity<TKey>
         => SetOrder<TEntity>().IgnoreQueryFilters();
        //=> Set<TEntity>().Where((Expression<Func<TEntity, bool>>)(entity => !entity.DateDeleted.HasValue));


        #endregion



        #region -   DbContext   -

        protected virtual void AddThenSaveChanges<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        protected virtual async Task AddThenSaveChangesAsync<TEntity>(TEntity entity,CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>
        {
            await Context.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync();
        }

        protected virtual void AddRangeThenSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
        {
             Context.AddRange(entities);
             Context.SaveChanges();
        }

        protected virtual void AddRangeThenSaveChanges<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
        {
            Context.AddRange(entities);
            Context.SaveChanges();
        }
        
        protected virtual async Task AddRangeThenSaveChangesAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>
        {
            await Context.AddRangeAsync(entities, cancellationToken);
            await Context.SaveChangesAsync();
        }


        protected virtual void RemoveThenSaveChanges<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
        {
             Context.Remove(entity);
             Context.SaveChanges();
        }
        
        protected virtual void RemoveRangeThenSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
        {
             Context.RemoveRange(entities);
             Context.SaveChanges();
        }


        protected virtual void RemoveRangeThenSaveChanges<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
        {
            Context.RemoveRange(entities);
            Context.SaveChanges();
        }




        protected virtual void UpdateThenSaveChanges<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        protected virtual void UpdateRangeThenSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
        {
            Context.UpdateRange(entities);
            Context.SaveChanges();
        }


        protected virtual void UpdateRangeThenSaveChanges<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
        {
            Context.UpdateRange(entities);
            Context.SaveChanges();
        }




        protected virtual void SoftDeleteThenSaveChanges<TEntity>(TEntity entity,bool detach = false) where TEntity : class, IBaseEntity<TKey>
        {
            Context.SoftDelete(entity);
            SaveChangesDeletedOrNot(detach);
        }

        protected virtual void SoftDeleteThenSaveChanges<TEntity>(TKey key, bool detach = false) where TEntity : class, IBaseEntity<TKey>
        {
            Context.SoftDelete<TEntity>(key);
            SaveChangesDeletedOrNot(detach);
        }

        protected virtual async Task SoftDeleteThenSaveChangesAsync<TEntity>(TKey key, bool detach = false) where TEntity : class, IBaseEntity<TKey>
        {
            await Context.SoftDeleteAsync<TEntity>(key);
            await SaveChangesDeletedOrNotAsync(detach);
        }


        protected virtual void SoftDeleteThenSaveChanges<TEntity>(Expression<Func<TEntity, bool>> findEntity, bool detach = false) where TEntity : class, IBaseEntity<TKey>
        {
            Context.SoftDelete<TEntity>(findEntity);
            SaveChangesDeletedOrNot(detach);
        }

        protected virtual async Task SoftDeleteThenSaveChangesAsync<TEntity>(Expression<Func<TEntity, bool>> findEntity, bool detach = false) where TEntity : class, IBaseEntity<TKey>
        {
            await Context.SoftDeleteAsync<TEntity>(findEntity);
            await SaveChangesDeletedOrNotAsync(detach);
        }
        protected virtual async Task AddTransactionAsync(Action action) => await Context.TransactionAsync(action);
        protected virtual async Task AddTransactionAsync<TEntity>(Func<TEntity> action) where TEntity : class
        => await Context.TransactionAsync<TEntity>(action);

        protected virtual void AddTransaction(Action action) => Context.Transaction(action);
        protected virtual void AddTransaction<TEntity>(Func<TEntity> action) where TEntity : class
        =>  Context.Transaction<TEntity>(action);



        private int SaveChangesDeletedOrNot(bool detach)
        => detach ? Context.SaveChangesDeleted() : Context.SaveChanges();

        private async Task<int> SaveChangesDeletedOrNotAsync(bool detach)
        => detach ? await Context.SaveChangesDeletedAsync() : await Context.SaveChangesAsync();

        #endregion


        ~MrRepository()
        {
            Dispose(false);
        }
        private bool disposed = false;
        /// <summary>
        /// Implements the dipose pattern.
        /// </summary>
        /// <param name="disposing"><c>True</c> when disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    Context.Dispose();
            this.disposed = true;
        }

        /// <summary>
        /// Implement <see cref="IDisposable"/>.
        /// </summary>
        public new void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
            //base.Dispose();
        }
    }


    public class MrRepository<TContext, TKey, TEntity> : MrRepository<TContext, TKey>, IDisposable where TEntity : class, IBaseEntity<TKey> where TKey : struct, IEquatable<TKey> where TContext : IMrIdentityDbContext<TKey>
    {

        public MrRepository(TContext context) : base(context) { }


        protected IQueryable<TEntity> Query
        {
            get => _query<TEntity>();
        }

        protected IQueryable<TEntity> AllQuery
        {
            get => _allQuery<TEntity>();
        }

        protected IQueryable<TEntity> TrackingQuery
        {
            get => _trackingQuery<TEntity>();
        }

        protected IQueryable<TEntity> AllTrackingQuery
        {
            get => _allTrackingQuery<TEntity>();
        }


        protected List<TEntity> ToList
        {
            get => _toList<TEntity>();
        }
        protected Task<List<TEntity>> ToListAsync
        {
            get => _toListAsync<TEntity>();
        }

        protected TEntity Find(TKey key)
            => _findById<TEntity>(key);
        protected ValueTask<TEntity> FindAsync(TKey key)
            => _findByIdAsync<TEntity>(key);


    }



    public class MrRepository<TContext, TKey, TEntity, TStore> : MrRepository<TContext, TKey, TEntity>,
        IDisposable where TEntity : class, IBaseEntity<TKey> where TKey : struct, IEquatable<TKey>
        where TContext : IMrIdentityDbContext<TKey> where TStore : _IStore, new()
    {
        protected TStore Store;

        public MrRepository(TContext context) : base(context)
        {
            if (Store is not _FakeStore _) { Store = new TStore(); }
        }
    }


    public class MrRepository<TContext, TKey, TEntity, TStore, TDto> : MrRepository<TContext, TKey, TEntity, TStore>, IMrRepositoryGeneral<TKey, TEntity, TDto>, IDisposable
      where TEntity : class, IBaseEntity<TKey> where TKey : struct, IEquatable<TKey>
      where TContext : IMrIdentityDbContext<TKey> where TStore : _IStore, new()
      where TDto : ISelector<TEntity, TDto>
    {

        public MrRepository(TContext context) : base(context) { }


        public virtual async Task<OperationResult<IEnumerable<TDto>>> FetchAsync() => await RepositoryHandler(_fetch);
        public virtual async Task<OperationResult<TDto>> GetByIdAsync(TKey id) => await RepositoryHandler(_get(id));
        public virtual async Task<OperationResult<TDto>> AddAsync(TDto dto) => await RepositoryHandler(_add(dto));
        public virtual async Task<OperationResult<TDto>> AddWithTranslateAsync(TDto dto) => await RepositoryHandler(_addwithtranslate(dto));
        public virtual async Task<OperationResult<TDto>> UpdateAsync(TDto dto) => await RepositoryHandler(_update(dto));
        public virtual async Task<OperationResult<TDto>> ModifyAsync(TDto dto) => await RepositoryHandler(_modeify(dto));
        public virtual async Task<OperationResult<TDto>> DeleteAsync(TKey id) => await RepositoryHandler(_delete(id));


        public virtual async Task<OperationResult<IEnumerable<TDto>>> AddMultiAsync(IEnumerable<TDto> dto) => await RepositoryHandler(_addMulti(dto));
        public virtual async Task<OperationResult<IEnumerable<TDto>>> UpdateMultiAsync(IEnumerable<TDto> dtos) => await RepositoryHandler(_updateMulti(dtos));
        public virtual async Task<OperationResult<IEnumerable<TDto>>> ModifyMultiAsync(IEnumerable<TDto> dtos) => await RepositoryHandler(_modifyMulti(dtos));
        public virtual async Task<OperationResult<IEnumerable<TDto>>> DeleteMultiAsync(IEnumerable<TKey> ids) => await RepositoryHandler(_deleteMulti(ids));




        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _fetch
            => async operation =>
            {
                Expression<Func<TEntity, TDto>> selector = Selector.GetProfSelector<TEntity, TDto>();
                IEnumerable<TDto> entities = await Query.Select(selector).ToListAsync();
                return operation.SetSuccess(entities);
            };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _get(TKey id)
          => async operation =>
          {
              TEntity entity = await FindAsync(id);
              if (entity is null)
                  return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
              return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
          };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _add(TDto dto)
            => async operation =>
            {
                TEntity entity = Selector.GetProfInverseSelector<TDto, TEntity>().CompileInverseSelector(dto);
                await Context.AddAsync(entity);
                await Context.SaveChangesAsync();
                return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
            };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _addwithtranslate(TDto dto)
         => async operation =>
         {
             TEntity entity = Selector.GetProfInverseSelector<TDto, TEntity>().CompileInverseSelector(dto);
             if (Options.Translator)
                 await Context.AddWithTranslateAsync(entity);
             else
                 await Context.AddAsync(entity);
             await Context.SaveChangesAsync();
             return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
         };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _update(TDto dto)
            => async operation =>
            {
                TKey id = dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id);
                TEntity entity = await Query.FirstOrDefaultAsync(ent => ent.Id.Equals(id));
                if (entity is null)
                    return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
                IBaseEntity<TKey> baseEntity = entity;
                entity = Selector.GetProfInverseSelector<TDto, TEntity>().CompileInverseSelector(dto);
                entity.DateCreated = baseEntity.DateCreated;
                entity.DateUpdated = baseEntity.DateUpdated;
                entity.DateDeleted = baseEntity.DateDeleted;
                entity.CreatedBy = baseEntity.CreatedBy;
                entity.UpdatedBy = baseEntity.UpdatedBy;
                entity.DeletedBy = baseEntity.DeletedBy;
                Context.Update(entity);
                await Context.SaveChangesAsync();
                return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
            };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _modeify(TDto dto)
          => async operation => {
              TKey id = dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id);
              TEntity entity = await TrackingQuery.FirstOrDefaultAsync(ent => ent.Id.Equals(id));
              if (entity is null)
                  return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
              Selector.GetProfAssignSelector<TDto, TEntity>().CompileAssignSelector(dto, entity);
              await Context.SaveChangesAsync();
              return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
          };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _delete(TKey id)
            => async operation =>
            {
                TEntity entity = await FindAsync(id);
                if (entity is null)
                    return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
                Context.SoftDelete(entity);
                await Context.SaveChangesAsync();
                return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity), $"Soft deleted {typeof(TEntity).Name} success");
            };



        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _addMulti(IEnumerable<TDto> dtos)
            => async operation =>
            {
                if (dtos.IsNullOrEmpty())
                    return operation.SetFailed($"{typeof(IEnumerable<TDto>).FullName}: can't be null or empty");

                Func<TDto, TEntity> compileInverseSelector = Selector.GetProfInverseSelector<TDto, TEntity>().Compile();
                TEntity[] entities = dtos.Select(compileInverseSelector).ToArray();
                await AddRangeThenSaveChangesAsync(entities);
                Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
                return operation.SetSuccess(entities.Select(compileSelector));
            };

        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _deleteMulti(IEnumerable<TKey> ids)
            => async operation =>
            {
                if (ids.IsNullOrEmpty())
                    return operation.SetFailed($"{typeof(IEnumerable<TKey>).FullName}: can't be null or empty");

                IEnumerable<TEntity> entities = await TrackingQuery.Where(e => ids.Contains(e.Id)).ToListAsync();
                if (entities.IsNullOrEmpty())
                    return (Statuses.NotExist, $"{typeof(IEnumerable<TKey>).FullName}: {StringExtension.IntoParentheses(ids.Select(i => i.ToString()).ToArray())} not exist");

                foreach (TEntity entity in entities)
                    Context.SoftDelete(entity);

                await Context.SaveChangesDeletedAsync();

                Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
                return operation.SetSuccess(entities.Select(compileSelector));
            };

        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _updateMulti(IEnumerable<TDto> dtos)
            => async operation =>
            {
                if (dtos.IsNullOrEmpty())
                    return operation.SetFailed($"{typeof(IEnumerable<TDto>).FullName}: can't be null or empty");

                IEnumerable<(TKey key, TDto obj)> full_ids = dtos.Select(dto => (dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id), dto));
                IEnumerable<TKey> ids = full_ids.Select(i => i.key);

                IEnumerable<TEntity> entities = await Query.Where(entity => ids.Contains(entity.Id)).ToListAsync();
                if (entities.IsNullOrEmpty())
                    return (Statuses.NotExist, $"{typeof(IEnumerable<TDto>).FullName}: {StringExtension.IntoParentheses(ids.Select(i => i.ToString()).ToArray())} not exist");

                Func<TDto, TEntity> compileInverseSelector = Selector.GetProfInverseSelector<TDto, TEntity>().Compile();

                entities = entities.Select(entity =>
                {
                    IBaseEntity<TKey> baseEntity = entity;
                    entity = compileInverseSelector(full_ids.First(i => i.key.Equals(baseEntity.Id)).obj);
                    entity.DateCreated = baseEntity.DateCreated;
                    entity.DateUpdated = baseEntity.DateUpdated;
                    entity.DateDeleted = baseEntity.DateDeleted;
                    entity.CreatedBy = baseEntity.CreatedBy;
                    entity.UpdatedBy = baseEntity.UpdatedBy;
                    entity.DeletedBy = baseEntity.DeletedBy;
                    return entity;
                });

                Context.UpdateRange(entities);
                await Context.SaveChangesAsync();
                Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
                return operation.SetSuccess(entities.Select(compileSelector));
            };

        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _modifyMulti(IEnumerable<TDto> dtos)
           => async operation =>
           {
               if (dtos.IsNullOrEmpty())
                   return operation.SetFailed($"{typeof(IEnumerable<TDto>).FullName}: can't be null or empty");

               IEnumerable<(TKey key, TDto obj)> full_ids = dtos.Select(dto => (dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id), dto));
               IEnumerable<TKey> ids = full_ids.Select(i => i.key);

               IEnumerable<TEntity> entities = await TrackingQuery.Where(entity => ids.Contains(entity.Id)).ToListAsync();
               if (entities.IsNullOrEmpty())
                   return (Statuses.NotExist, $"{typeof(IEnumerable<TDto>).FullName}: {StringExtension.IntoParentheses(ids.Select(i => i.ToString()).ToArray())} not exist");

               foreach (var entity in entities)
                   Selector.GetProfAssignSelector<TDto, TEntity>().CompileAssignSelector(full_ids.First(i => i.key.Equals(entity.Id)).obj, entity);

               await Context.SaveChangesAsync();
               Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
               return operation.SetSuccess(entities.Select(compileSelector));
           };

            
    }

}
