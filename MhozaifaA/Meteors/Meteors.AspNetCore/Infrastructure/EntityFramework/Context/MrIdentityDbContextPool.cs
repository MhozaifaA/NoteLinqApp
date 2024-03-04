//using Meteors.AspNetCore.Helper.ExtensionMethods.EntityEntry;
//using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
//using Meteors.AspNetCore.Domain.Util;
//using Meteors.AspNetCore.Infrastructure.EntityFramework.Util;
//using Meteors.AspNetCore.Infrastructure.ModelEntity.Base;
//using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore.Query;
//using Microsoft.EntityFrameworkCore.Metadata;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Options;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.Extensions.DependencyInjection;
//using System.Diagnostics;
//using Meteors.AspNetCore.Security.Claims;
//using Meteors.AspNetCore.Core.DataStructures;
//using Meteors.AspNetCore.Infrastructure.InfraExtensions;
//using Meteors.AspNetCore.Infrastructure.ModelEntity.Localization;
//using Meteors.AspNetCore.Localization.Translation;

//namespace Meteors.AspNetCore.Infrastructure.EntityFramework.Context
//{
//    public class MrIdentityDbContextPool<TKey> : MrIdentityDbContextPool<IdentityUser<TKey>, IdentityRole<TKey>, TKey>
//    where TKey : struct, IEquatable<TKey>
//    {
//        public MrIdentityDbContextPool(DbContextOptions options) : base(options) { }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);
//        }
//    }

//    public class MrIdentityDbContextPool<TUser, TRole, TKey> : MrIdentityDbContextPool<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
//    where TUser : IdentityUser<TKey>
//    where TRole : IdentityRole<TKey>
//    where TKey : struct, IEquatable<TKey>
//    {
//        public MrIdentityDbContextPool(DbContextOptions options) : base(options) { }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);
//        }
//    }


//    public class MrIdentityDbContextPool<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>,
//        IMrIdentityDbContext<TKey> where TUser : IdentityUser<TKey>
//    where TRole : IdentityRole<TKey>
//    where TKey : struct, IEquatable<TKey>
//    where TUserClaim : IdentityUserClaim<TKey>
//    where TUserRole : IdentityUserRole<TKey>
//    where TUserLogin : IdentityUserLogin<TKey>
//    where TRoleClaim : IdentityRoleClaim<TKey>
//    where TUserToken : IdentityUserToken<TKey>
//    {
//        readonly IHttpResolverService HttpResolverService;
//        protected readonly IMrTranslate MrTranslate;

//        public MrIdentityDbContextPool(DbContextOptions options) : base(options)
//        {
//            HttpResolverService = this.GetService<IHttpResolverService>();
//            MrTranslate = this.GetService<IMrTranslate>();
//        }
//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            foreach (var entityType in builder.Model.GetEntityTypes().Where(x => x.ClrType.BaseType == typeof(BaseEntity<TKey>)))
//            {
//                AddIndex(builder, entityType, nameof(IBaseEntity<TKey>.DateCreated));
//                AddQueryFilter(builder, f => !f.DateDeleted.HasValue, entityType);
//            }
//        }

//        protected TKey? CurrentUserId
//        {
//            get { return HttpResolverService?.GetCurrentUserId<TKey>(); }
//        }

//        protected TransferredProp? CurrentTransferredProps
//        {
//            get { return HttpResolverService?.GetCurrentTransferredProps(); }
//        }

//        protected IHttpResolverService CurrentHttpResolverService
//        {
//            get { return HttpResolverService; }
//        }

//        public virtual T GetService<T>() => AccessorExtensions.GetService<T>(this);


//        public virtual event BeforeSaveChangesSignature BeforeSaveChangesSignature
//        {
//            add => _beforeSaveChangesSignature += value;
//            remove => _beforeSaveChangesSignature -= value;
//        }

//        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity<TKey>
//               => base.Set<TEntity>();


//        public virtual new TEntity Find<TEntity>([NotNull] params TKey[] keyValues) where TEntity : class, IBaseEntity<TKey>
//            => base.Find<TEntity>(keyValues.Cast<object>().ToArray());


//        public virtual new ValueTask<TEntity> FindAsync<TEntity>([NotNull] params TKey[] keyValues) where TEntity : class, IBaseEntity<TKey>
//            => base.FindAsync<TEntity>(keyValues.Cast<object>().ToArray());




//        public virtual new EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//            => base.Add(entity);

//        public virtual new ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>
//            => base.AddAsync(entity, cancellationToken);

//        public virtual new void AddRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
//            => base.AddRange(entities);

//        public virtual new void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
//          => base.AddRange(entities);

//        public virtual new Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>
//            => base.AddRangeAsync(entities, cancellationToken);




//        public virtual new EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//            => base.Update(entity);

//        public virtual new void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
//            => base.UpdateRange(entities);

//        public virtual new void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
//            => base.UpdateRange(entities);



//        public virtual new EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//            => base.Remove(entity);

//        public virtual new void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
//            => base.RemoveRange(entities);

//        public virtual new void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
//         => base.RemoveRange(entities);




//        public virtual EntityEntry<TEntity> SoftDelete<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//            => !ModifyDeletable(ref entity) ? default : Entry(entity);

//        public virtual EntityEntry<TEntity> SoftDelete<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = Find<TEntity>(key);
//            return !ModifyDeletable(ref entity) ? default : Entry(entity);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteAsync<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await FindAsync<TEntity>(key);
//            return !ModifyDeletable(ref entity) ? default : Entry(entity);
//        }

//        public virtual EntityEntry<TEntity> SoftDelete<TEntity>(Expression<Func<TEntity, bool>> findEntity) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = this.Set<TEntity>().FirstOrDefault(findEntity);
//            return !ModifyDeletable(ref entity) ? default : Entry(entity);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> findEntity) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await this.Set<TEntity>().FirstOrDefaultAsync(findEntity);
//            return !ModifyDeletable(ref entity) ? default : Entry(entity);
//        }


//        public virtual EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//            => BuildSoftDeleteTree(ref entity);


//        //public virtual EntityEntry<TEntity> SoftDeleteTraversal<TEntity, TProperty>(TEntity entity, params Func<TEntity, TProperty>[] without) where TEntity : class, IBaseEntity<TKey>
//        //    => !ModifyDeletable(ref entity) ? default : SoftDeleteTraversal(base.Entry<TEntity>(entity), without.Select(x => x.Method.ReturnType));


//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity>(Expression<Func<TEntity, bool>> findEntity, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty, TPreviousPreviousPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, Expression<Func<TPreviousPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousPreviousProperty>>> thenNextNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).ThenInclude(thenNextNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType, thenNextNextNextInclude.Compile().Method.ReturnType);
//        }


//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity>(TKey key, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType);
//        }

//        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty, TPreviousPreviousPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, Expression<Func<TPreviousPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousPreviousProperty>>> thenNextNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
//        {
//            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).ThenInclude(thenNextNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
//            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType, thenNextNextNextInclude.Compile().Method.ReturnType);
//        }

//        public virtual EntityEntry<TEntity> Translate<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//        {
//            if (MrTranslate is null || !MrTranslate.IsEnabled())
//                return Entry(entity);
//            var entityProp = entity.GetType().GetProperty(nameof(ILanguages.Languages));
//            if (entityProp is null) return Entry(entity);
//            if (entityProp.GetMethod.Invoke(entity, null) is not null) return Entry(entity);

//            Dictionary<string, string> list = entity.GetTranslateProperties();
//            CultureDictionary languages = new CultureDictionary();
//            Dictionary<string, IEnumerable<string>> result = MrTranslate.Translate(list.Values);
//            foreach (KeyValuePair<string, IEnumerable<string>> item in result)
//                languages.AddOrUpdate(item.Key, new PropertyTranslate(list.Keys.Zip(item.Value).ToDictionary(x => x.First, x => x.Second)));
//            entityProp.SetMethod.Invoke(entity, new object[] { languages });
//            return Entry(entity);
//        }

//        public virtual async Task<EntityEntry<TEntity>> TranslateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//        {
//            if (MrTranslate is null || !MrTranslate.IsEnabled())
//                return Entry(entity);
//            var entityProp = entity.GetType().GetProperty(nameof(ILanguages.Languages));
//            if (entityProp is null) return Entry(entity);
//            if (entityProp.GetMethod.Invoke(entity, null) is not null) return Entry(entity);

//            Dictionary<string, string> list = entity.GetTranslateProperties();
//            CultureDictionary languages = new CultureDictionary();
//            Dictionary<string, IEnumerable<string>> result = await MrTranslate.TranslateAsync(list.Values);
//            foreach (KeyValuePair<string, IEnumerable<string>> item in result)
//                languages.AddOrUpdate(item.Key, new PropertyTranslate(list.Keys.Zip(item.Value).ToDictionary(x => x.First, x => x.Second)));
//            entityProp.SetMethod.Invoke(entity, new object[] { languages });
//            return Entry(entity);
//        }

//        public virtual new EntityEntry<TEntity> AddWithTranslate<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//        {
//            Add(entity);
//            return Translate(entity);
//        }

//        public virtual new async Task<EntityEntry<TEntity>> AddWithTranslateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
//        {
//            await AddAsync(entity);
//            return await TranslateAsync(entity);
//        }

//        public override int SaveChanges()
//            => SaveChanges(acceptAllChangesOnSuccess: true);

//        public override int SaveChanges(bool acceptAllChangesOnSuccess)
//        {
//            _beforeSaveChangesSignature?.Invoke();
//            BeforeSaveChanges();
//            return base.SaveChanges(acceptAllChangesOnSuccess);
//        }


//        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//            => SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);


//        public override Task<int> SaveChangesAsync(
//          bool acceptAllChangesOnSuccess,
//          CancellationToken cancellationToken = default)
//        {
//            _beforeSaveChangesSignature?.Invoke();
//            BeforeSaveChanges();
//            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
//        }


//        public virtual int SaveChangesDeleted()
//        {
//            int task = SaveChanges(acceptAllChangesOnSuccess: true);
//            DetachDeleted();
//            return task;
//        }

//        public virtual async Task<int> SaveChangesDeletedAsync(CancellationToken cancellationToken = default)
//        {
//            int task = await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
//            DetachDeleted();
//            return task;
//        }



//        protected virtual void DetachDeleted()
//        {
//            var deletedTracker = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Unchanged && entry.ToEntity<IDeletable>().DateDeleted.HasValue);
//            foreach (var entity in deletedTracker)
//                entity.State = EntityState.Detached;
//        }


//        protected virtual void BeforeSaveChanges()
//        {

//            TKey? actionBy = HttpResolverService?.GetCurrentUserId<TKey>(); // ?? default(TKey);

//            foreach (EntityEntry entry in ChangeTracker.Entries())
//            {
//                IBaseEntity<TKey> entity = entry.Entity.AsTo<IBaseEntity<TKey>>();
//                if (entity is null) continue;

//                switch (entry.State)
//                {
//                    case EntityState.Detached:

//                        break;

//                    case EntityState.Unchanged:

//                        break;

//                    case EntityState.Deleted:

//                        break;

//                    case EntityState.Modified:
//                        if (entity.DateDeleted is null)
//                        {
//                            entity.UpdatedBy = actionBy;
//                            entity.DateUpdated = DateTime.Now.ToLocalTime();
//                        }
//                        else
//                            entity.DeletedBy = actionBy;

//                        break;

//                    case EntityState.Added:
//                        entity.CreatedBy = actionBy;
//                        entity.DateCreated = DateTime.Now.ToLocalTime();
//                        break;
//                }


//            }
//        }





//        #region -   Private   -

//        private BeforeSaveChangesSignature _beforeSaveChangesSignature;


//        private static void AddQueryFilter(ModelBuilder builder, Expression<Func<IBaseEntity<TKey>, bool>> filterSoftDelete, IMutableEntityType entityType)
//        {
//            var newParam = Expression.Parameter(entityType.ClrType);
//            var newbody = ReplacingExpressionVisitor.Replace(filterSoftDelete.Parameters.Single(), newParam, filterSoftDelete.Body);
//            builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newbody, newParam));
//        }

//        private static void AddIndex(ModelBuilder builder, IMutableEntityType entityType, params string[] propertyNames)
//        {
//            builder.Entity(entityType.ClrType).HasIndex(propertyNames);
//        }

//        private static bool ModifyDeletable<TEntity>(ref TEntity entity) where TEntity : class, IBaseEntity<TKey>
//        {
//            if (entity is null) return false;
//            if (entity.DateDeleted.HasValue) return false;
//            entity.DateDeleted = DateTime.Now.ToLocalTime();
//            return true;
//        }

//        private EntityEntry<TEntity> BuildSoftDeleteTree<TEntity>(ref TEntity entity, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
//             => !ModifyDeletable(ref entity) ? default : SoftDeleteTraversal(base.Entry<TEntity>(entity), includeTypes);

//        private EntityEntry<TEntity> BuildSoftDeleteTree<TEntity>(ref TEntity entity, string[] navigationPropertyPath, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
//            => !ModifyDeletable(ref entity) ? default : SoftDeleteTraversal(base.Entry<TEntity>(entity), navigationPropertyPath, includeTypes);


//        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry) where TEntity : class, IBaseEntity<TKey>
//            => _softDeleteTraversal(entry);

//        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, IEnumerable<Type> includeType) where TEntity : class, IBaseEntity<TKey>
//            => _softDeleteTraversal(entry, includeType);

//        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
//           => _softDeleteTraversal(entry, includeTypes);

//        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, string[] navigationPropertyPath, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
//         => _softDeleteTraversal(entry, includeTypes, navigationPropertyPath);



//        private EntityEntry<TEntity> _softDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, IEnumerable<Type> includeTypes = default, string[] navigationPropertyPath = default) where TEntity : class, IBaseEntity<TKey>
//        {
//            includeTypes = includeTypes ?? new List<Type>();
//            navigationPropertyPath = navigationPropertyPath ?? new string[0];
//            bool ifOnLastTracking = !includeTypes.Any() && navigationPropertyPath.Length == 0;
//            IEnumerable<string> _navigationPropertyPath = navigationPropertyPath.SelectMany(x => x.Split('.')).Distinct();
//            foreach (var navigationEntry in entry.Navigations.Where(n => !((INavigation)n.Metadata).IsOnDependent && (ifOnLastTracking || (includeTypes.Contains(n.Metadata.ClrType) || _navigationPropertyPath.Contains(n.Metadata.Name)))))
//            {
//                if (navigationEntry.CurrentValue is null) continue;
//                if (navigationEntry is CollectionEntry collectionEntry)
//                {
//                    foreach (var dependentEntry in collectionEntry.CurrentValue)
//                    {
//                        var _base = dependentEntry.CastTo<IDeletable>();
//                        if (_base.DateDeleted.HasValue) continue;
//                        _base.DateDeleted = DateTime.Now.ToLocalTime();
//                        _softDeleteTraversal(Entry(dependentEntry), includeTypes, _navigationPropertyPath);
//                    }
//                }
//                else
//                {
//                    var dependentEntry = navigationEntry.CurrentValue.AsTo<IDeletable>();
//                    if (dependentEntry != null)
//                    {
//                        dependentEntry.DateDeleted = DateTime.Now.ToLocalTime();
//                    }
//                }
//            }

//            return entry;
//        }



//        private void _softDeleteTraversal(EntityEntry entry, IEnumerable<Type> outType = default, IEnumerable<string> navigationPropertyPath = default)
//        {
//            bool ifOnLastTracking = !outType.Any() && !navigationPropertyPath.Any();
//            foreach (var navigationEntry in entry.Navigations.Where(n => !((INavigation)n.Metadata).IsOnDependent && (ifOnLastTracking || (outType.Contains(n.Metadata.ClrType) || navigationPropertyPath.Contains(n.Metadata.Name)))))
//            {
//                if (navigationEntry.CurrentValue is null) continue;
//                if (navigationEntry is CollectionEntry collectionEntry)
//                {
//                    foreach (var dependentEntry in collectionEntry.CurrentValue)
//                    {
//                        var _base = dependentEntry.CastTo<IDeletable>();
//                        if (_base.DateDeleted.HasValue) continue;
//                        _base.DateDeleted = DateTime.Now.ToLocalTime();
//                        _softDeleteTraversal(Entry(dependentEntry), outType, navigationPropertyPath);
//                    }
//                }
//            }
//        }

//        #endregion

//    }
//}
