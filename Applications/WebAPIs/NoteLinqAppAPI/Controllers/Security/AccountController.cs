using NoteLinqApp.DataTransferObjects.Security;
using NoteLinqApp.Domain;
using Meteors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Meteors.AspNetCore.MVC;
using NorLinqApp.DataTransferObjects.Security;

namespace NoteLinqApp.Controllers.Security
{
    [ApiController]
    [Route("api/security/[controller]/[action]")]
    public class AccountController : MrControllerBase<Guid, IAccountRepository>
    {
        public AccountController(IAccountRepository repository) : base(repository) { }

        /// <summary>
        /// { username:user, password:user}
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]        
        public async Task<ActionResult<AccessTokenDto>> Login([FromBody] LoginDto dto)
        {
            return await repository.Login(dto).ToJsonResultAsync();
        }

    }
}
