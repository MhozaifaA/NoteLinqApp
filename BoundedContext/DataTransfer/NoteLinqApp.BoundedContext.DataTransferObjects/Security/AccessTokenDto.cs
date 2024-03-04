using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.DataTransferObjects.Security
{
    public record AccessTokenDto(string Name, string UserName, string Email, string? AccessToken);
}
