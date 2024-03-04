//using Meteors.AspNetCore.Common.AuxiliaryImplemental.Interfaces;
//using Meteors.AspNetCore.OperationContext;
//using Meteors.AspNetCore.Core.Shared;
//using Meteors.AspNetCore.Helper.ExtensionMethods.Enumerable;
//using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
//using Meteors.AspNetCore.Helper.ExtensionMethods.String;
//using Meteors.AspNetCore.Infrastructure.EntityFramework.Context;
//using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
//using Meteors.AspNetCore.Infrastructure.ModelEntity.Localization;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Meteors.AspNetCore.Service.BoundedContext.General.Localization
//{


//    public interface IMrRepositoryGeneral<TKey, TEntity, TDto> : IMrRepositoryGeneral<TKey, TDto>
//        where TEntity : class, IBaseEntity<TKey> where TKey : struct, IEquatable<TKey> where TDto : ISelector<TEntity, TDto>
//    { }

//    public interface IMrRepositoryGeneral<TKey, TDto> : Meteors.AspNetCore.Service.BoundedContext.General.IMrRepositoryGeneral<TKey, TDto> where TKey : struct, IEquatable<TKey> where TDto : ISelector<TDto>
//    {
//        Task<OperationResult<TDto>> AddWithTranslateAsync(TDto dto);
//    }


//    public class MrRepositoryGeneral<TContext, TKey, TEntity, TDto> : Meteors.AspNetCore.Service.BoundedContext.General.MrRepositoryGeneral<TContext, TKey, TEntity, TDto> , IMrRepositoryGeneral<TKey, TEntity, TDto>
//           where TEntity : class, IBaseEntity<TKey>, ILanguages where TKey : struct, IEquatable<TKey>
//     where TContext : IMrIdentityDbContext<TKey> where TDto : ISelector<TEntity, TDto>
//    {
//        protected MrRepositoryGeneral(TContext context) : base(context) { }

//        public virtual async Task<OperationResult<TDto>> AddWithTranslateAsync(TDto dto) => await RepositoryHandler(_addwithtranslate(dto));
//        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _addwithtranslate(TDto dto)
//          => async operation =>
//          {
//              TEntity entity = Selector.GetProfInverseSelector<TDto, TEntity>().CompileInverseSelector(dto);
//              if (Options.Translator)
//                  await Context.AddWithTranslateAsync(entity);
//              else
//                  await Context.AddAsync(entity);
//              await Context.SaveChangesAsync();
//              return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
//          };

//    }
//}