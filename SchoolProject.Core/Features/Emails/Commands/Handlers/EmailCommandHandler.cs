using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers
{
    public class EmailCommandHandler : ResponseHandler,
                    IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IEmailService _emailService;
        #endregion

        #region Construcors
        public EmailCommandHandler(IStringLocalizer<SharedResources> localizer
                                    , IEmailService emailService) : base(localizer)
        {
            _localizer = localizer;
            _emailService = emailService;
        }
        #endregion

        #region Handle Methods
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmailAsync(request.Email, request.Message, request.Reason);
            if (response == "Success")
                return Success(response);
            return BadRequest<string>(_localizer[SharedResourcesKeys.SendEmailFailes]);
        }
        #endregion
    }
}
