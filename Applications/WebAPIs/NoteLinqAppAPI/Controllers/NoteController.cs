using Meteors.AspNetCore.MVC.Attributes;
using Meteors.AspNetCore.MVC;
using Meteors;
using Microsoft.AspNetCore.Mvc;
using NoteLinqApp.BoundedContext.DataTransferObjects;
using NoteLinqApp.Domain;
using NoteLinqApp;
using System.ComponentModel.DataAnnotations;

namespace NoteLinqAppAPI.Controllers
{

    [ApiController, AuthorizeJwt, DefaultRoute]
    public class NoteController : MrControllerBase<Guid, INoteService>
    {
        public NoteController(INoteService repository) : base(repository)
        {
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return await repository.List().ToJsonResultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody][Required] NoteDto dto)
        {
            return await repository.AddAsync(dto).ToJsonResultAsync();
        }

        [HttpPut]
        public async Task<IActionResult> Modify([FromBody][Required] NoteDto dto)
        {
            return await repository.ModifyAsync(dto).ToJsonResultAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute][Required] Guid id)
        {
            return await repository.DeleteAsync(id).ToJsonResultAsync();
        }

    }
}
