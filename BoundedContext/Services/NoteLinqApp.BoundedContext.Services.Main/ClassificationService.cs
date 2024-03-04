using Meteors;
using Meteors.AspNetCore.Service.BoundedContext.General;
using Meteors.OperationContext;
using Microsoft.EntityFrameworkCore;
using NoteLinqApp.BoundedContext.DataTransferObjects;
using NoteLinqApp.Domain;
using NoteLinqApp.Infrastructure.Databases.InMemory;
using NoteLinqApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.BoundedContext
{
    [AutoService]
    public class ClassificationService : MrRepositoryGeneral<NoteLinqAppInMemoryDbContext, Guid, Classification, ClassificationDto>, IClassificationService
    {
        public ClassificationService(NoteLinqAppInMemoryDbContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<ClassificationDto>>> List()
        {
            var userId = Context.CurrentUserId;
            if (userId is null)
                return ("invalid user", Statuses.Forbidden);

            return  (await Query.Where(x => x.CreatedBy == userId).
                Select(ClassificationDto.Selector).ToListAsync(), "fetch only user data");
        }

        public override Task<OperationResult<ClassificationDto>> AddAsync(ClassificationDto dto)
        {
            dto.Id = Guid.Empty;
            return base.AddAsync(dto);
        }

    }
}
