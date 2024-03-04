using Meteors.AspNetCore.Common.AuxiliaryImplemental.Interfaces;
using Meteors.AspNetCore.Helper.Validations.Attribute.DataBaseAnnotations;
using Meteors.AspNetCore.Helper.Validations.Constants;
using Meteors.AspNetCore.Helper.Validations.Enum;
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
    public class NoteDto : IIndex<Guid>, ISelector<Note, NoteDto>
    {
        /// <summary>
        /// dont pass in insertion
        /// </summary>
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TypeConstants.LongNounString)]
        public string Title { get; set; }


        /// <summary>
        /// html/plain text .., from text editor
        /// </summary>
        public string? Body { get; set; }


        public Guid? ClassificationId { get; set; }



        public static Expression<Func<Note, NoteDto>> Selector { get; set; } = o => new NoteDto()
        {
            Id = o.Id,
            Title = o.Title,
            Body = o.Body,
            ClassificationId = o.ClassificationId,
        };
        public static Expression<Func<NoteDto, Note>> InverseSelector { get; set; } = o => new Note()
        {
            Id = o.Id,
            Title = o.Title,
            Body = o.Body,
            ClassificationId = o.ClassificationId,
        };
        public static Action<NoteDto, Note> AssignSelector { get; set; } = (o, entity) =>
        {
            entity.Title = o.Title;
            entity.Body = o.Body;
            entity.ClassificationId = o.ClassificationId;
        };


    }
}
