using Meteors;
using Meteors.AspNetCore.Service.BoundedContext;
using NorLinqApp.DataTransferObjects.Security;
using NoteLinqApp.DataTransferObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.Domain
{
    public interface IAccountRepository : IMrRepository
    {
        Task<OperationResult<AccessTokenDto>> Login(LoginDto dto);
    }
}
