using Meteors;
using Meteors.AspNetCore.Service.BoundedContext.General;
using NoteLinqApp.BoundedContext.DataTransferObjects;
using NoteLinqApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.Domain
{
    public interface INoteService : IMrRepositoryGeneral<Guid, Note, NoteDto>
    {
        Task<OperationResult<IEnumerable<NoteDto>>> List();
    }
}
