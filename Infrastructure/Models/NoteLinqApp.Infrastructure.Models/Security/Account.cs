using Meteors;
using Meteors.AspNetCore.Core;
using Meteors.AspNetCore.Helper.Validations.Attribute.DataBaseAnnotations;
using Meteors.AspNetCore.Helper.Validations.Constants;
using Meteors.AspNetCore.Helper.Validations.Enum;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Securty;
using System.ComponentModel.DataAnnotations;


namespace NoteLinqApp.Infrastructure.Models
{
    /*
     * User account to add his notes / classification
     */
    public class Account : MrIdentityUser<Guid>, INominal
    {
        public Account()
        {
            //Classifications = new HashSet<Classification>();
            //Notes = new HashSet<Note>();
        }

        [ColumnDataType(DataBaseTypes.NVARCHAR, TypeConstants.MediumString)]
        [Required]
        public string Name { get; set; }


        //public ICollection<Classification> Classifications { get; set; }
        //public ICollection<Note> Notes { get; set; }

    }
}
