//using Meteors.AspNetCore.Common.AuxiliaryImplemental.Interfaces;
//using Meteors.AspNetCore.OperationContext;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Meteors.AspNetCore.MVC.Localization
//{
//    public class MrControllerBaseGeneral<TKey, TRepository, TDto> : Meteors.AspNetCore.MVC.MrControllerBaseGeneral<TKey, TRepository, TDto> where TKey : struct, IEquatable<TKey>
//        where TRepository : Meteors.AspNetCore.Service.BoundedContext.General.Localization.IMrRepositoryGeneral<TKey, TDto> where TDto : ISelector<TDto>
//    {
//        public MrControllerBaseGeneral(TRepository repository) : base(repository) { }

//        [HttpPost]
//        public virtual async Task<IActionResult> AddWithTranslate(TDto dto) => await repository.AddWithTranslateAsync(dto).ToJsonResultAsync();
//    }
//}
