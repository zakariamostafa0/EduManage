using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class EmailController : AppControllerBase
    {
        [HttpPost(Router.EmailRouting.Prefix)]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
