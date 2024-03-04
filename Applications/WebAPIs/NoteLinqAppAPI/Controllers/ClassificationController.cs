using Meteors;
using Meteors.AspNetCore.MVC;
using Meteors.AspNetCore.MVC.Attributes;
using Meteors.AspNetCore.MVC.Security.Attribute;
using Microsoft.AspNetCore.Mvc;
using NoteLinqApp;
using NoteLinqApp.BoundedContext.DataTransferObjects;
using NoteLinqApp.Domain;

namespace NoteLinqAppAPI.Controllers
{
    [ApiController, AuthorizeJwt, DefaultRoute]
    public class ClassificationController : MrControllerBaseGeneral<Guid, IClassificationService, ClassificationDto>
    {
        public ClassificationController(IClassificationService repository) : base(repository)
        {
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return await repository.List().ToJsonResultAsync();
        }
    }
}
