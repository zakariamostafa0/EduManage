using SchoolProject.Core.Features.Instructors.Commands.Models;

namespace SchoolProject.Core.Features.Instructors.Commands.Handlers
{
    internal class InstructorCommandHandler : ResponseHandler,
                                                         IRequestHandler<AddInstructorCommand, Response<string>>
    {
        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Construcors
        public InstructorCommandHandler(
                                    IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    IInstructorService instructorService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _instructorService = instructorService;
        }


        #endregion

        #region Handle Methods
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstructorAsync(instructor, request.Image);
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_localizer[SharedResourcesKeys.NoImage]);
                case "FailedToUploadImage": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToUploadImage]);
                case "FailedInAdd": return BadRequest<string>(_localizer[SharedResourcesKeys.AddFailed]);
            }
            return Success("");
        }
        #endregion

    }
}
