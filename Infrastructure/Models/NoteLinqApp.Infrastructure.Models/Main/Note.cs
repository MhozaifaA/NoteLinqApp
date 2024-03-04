using Meteors.AspNetCore.Helper.Validations.Attribute.DataBaseAnnotations;
using Meteors.AspNetCore.Helper.Validations.Constants;
using Meteors.AspNetCore.Helper.Validations.Enum;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.Infrastructure.Models
{
    public class Note : BaseEntity<Guid>
    {
        [Required]
        [ColumnDataType(DataBaseTypes.NVARCHAR, TypeConstants.LongNounString)]
        public string Title { get; set; }


        /// <summary>
        /// html/plain text .., from text editor
        /// </summary>
        [ColumnDataType(DataBaseTypes.NVARCHAR)]
        public string? Body { get; set; }


        /// <summary>
        /// Note: in-memory there is no validation on relationship ..this will allow to enter any value
        /// </summary>
        public Guid? ClassificationId { get; set; }
        public Classification Classification { get; set; }


        /// <summary>
        /// Who added note, can use Createdby.. but this for admin level control
        /// </summary>
        //[Required]
        //public Guid UserId { get; set; }
        //public Account User { get; set; }
    }
}
