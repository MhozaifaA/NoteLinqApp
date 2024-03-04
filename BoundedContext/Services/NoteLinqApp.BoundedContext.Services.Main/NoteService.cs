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
    public class NoteService : MrRepositoryGeneral<NoteLinqAppInMemoryDbContext, Guid, Note, NoteDto>, INoteService
    {
        public NoteService(NoteLinqAppInMemoryDbContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<NoteDto>>> List()
        {
            var userId = Context.CurrentUserId;
            if (userId is null)
                return ("invalid user", Statuses.Forbidden);
            //pagantion can be enabled by header page, quantity...

            return (await Query.Where(x => x.CreatedBy == userId).
                Select(NoteDto.Selector).ToListAsync(), "fetch only user data");
        }


         /* deleted sot delete and more .. all are should be inhance that user can delete only his data and update only his data...*/
        public override Task<OperationResult<NoteDto>> AddAsync(NoteDto dto)
        {
            dto.Id = Guid.Empty;
            return base.AddAsync(dto);
        }
    }
}
