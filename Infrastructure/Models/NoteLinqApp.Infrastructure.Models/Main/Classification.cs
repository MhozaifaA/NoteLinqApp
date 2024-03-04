using Meteors.AspNetCore.Core;
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
    public class Classification : BaseEntity<Guid>, INominal
    {
        public Classification()
        {
            Notes = new HashSet<Note>();
        }

        [Required]
        [ColumnDataType(DataBaseTypes.NVARCHAR, TypeConstants.LongNounString)]
        public required string Name { get; set; }

        public Colors Color { get; set; }

        //[Required]
        //public Guid UserId { get; set; }
        //public Account User { get; set; }


        public ICollection<Note> Notes { get; set; }
    }
}
