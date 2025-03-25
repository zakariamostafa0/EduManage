using SchoolProject.Core.Features.Instructors.Queries.Models;

namespace SchoolProject.Core.Features.Instructors.Queries.Handlers
{
    public class InstructorQueryHandler : ResponseHandler,
                            IRequestHandler<GetInstructorsSalarySummationQuery, Response<decimal>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly IInstructorService _instructorService;
        #endregion

        #region Constructor 
        public InstructorQueryHandler(
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper,
            IInstructorService instructorService) : base(localizer)
        {
            _localizer = localizer;
            _mapper = mapper;
            _instructorService = instructorService;
        }

        #endregion

        #region Handle Function
        public async Task<Response<decimal>> Handle(GetInstructorsSalarySummationQuery request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.GetSalarySummationOfInstructor();
            return Success(result);
        }
        #endregion
    }
}
