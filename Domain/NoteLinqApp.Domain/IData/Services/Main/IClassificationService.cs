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
    public interface IClassificationService : IMrRepositoryGeneral<Guid, Classification, ClassificationDto>
    {
        Task<OperationResult<IEnumerable<ClassificationDto>>> List();
    }
}
