using Meteors.AspNetCore.Common.AuxiliaryImplemental.Interfaces;
using Meteors.AspNetCore.Core;
using Meteors.AspNetCore.Helper.Validations.Constants;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
using NoteLinqApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.BoundedContext.DataTransferObjects
{
    public class ClassificationDto : IIndex<Guid>, INominal, ISelector<Classification, ClassificationDto>
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TypeConstants.LongNounString)]
        public string Name { get; set; }

        public Colors Color { get; set; }

        public static Expression<Func<Classification, ClassificationDto>> Selector { get; set; } = o => new ClassificationDto()
        {
            Id = o.Id,
            Name = o.Name,
            Color = o.Color
        };
        public static Expression<Func<ClassificationDto, Classification>> InverseSelector { get; set; } = o => new Classification()
        {
            Id = o.Id,
            Name = o.Name,
            Color = o.Color

        };
        public static Action<ClassificationDto, Classification> AssignSelector { get; set; } = (o, entity) =>
        {
            entity.Name = o.Name;
            entity.Color = o.Color;
        };
    }
}
