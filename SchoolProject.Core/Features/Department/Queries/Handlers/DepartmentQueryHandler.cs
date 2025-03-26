using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entities.Procedure;
using Serilog;

namespace SchoolProject.Core.Features.Department.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
                                        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>,
                                        IRequestHandler<GetDepartmentsStudentsCountQuery, Response<List<GetDepartmentsStudentsCountResult>>>,
                                        IRequestHandler<GetDepartmentStudentsCountByIdQuery, Response<GetDepartmentStudentsCountByIdResult>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor 
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> localizer,
            IDepartmentService departmentServices, IMapper mapper, IStudentService studentService) : base(localizer)
        {
            _localizer = localizer;
            _departmentService = departmentServices;
            _mapper = mapper;
            _studentService = studentService;
        }
        #endregion

        #region Handle Function
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _departmentService.GetDepartmentById(request.Id);
            // Check if Null or not 
            if (response == null) return NotFound<GetDepartmentByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);
            // Mapping
            var Mapper = _mapper.Map<GetDepartmentByIdResponse>(response);
            // pagination of the student list in side the department
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Name);
            var StudentQuery = _studentService.GetAllStudentsByDepartmentIdQueryable(request.Id);
            var StudentsAfterPagination = await StudentQuery.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            Mapper.StudentListPagination = StudentsAfterPagination;
            Log.Information("Hello from Serilog");
            return Success(Mapper);
        }

        public async Task<Response<List<GetDepartmentsStudentsCountResult>>> Handle(GetDepartmentsStudentsCountQuery request, CancellationToken cancellationToken)
        {
            var viewDepartmentsStudentsCountResult = await _departmentService.GetViewDepartments();
            var result = _mapper.Map<List<GetDepartmentsStudentsCountResult>>(viewDepartmentsStudentsCountResult);
            return Success(result);
        }

        public async Task<Response<GetDepartmentStudentsCountByIdResult>> Handle(GetDepartmentStudentsCountByIdQuery request, CancellationToken cancellationToken)
        {
            //may use mapper also
            var parameters = new DepartmentStudentsCountProcParmeters
            {
                Did = request.DID
            };

            var viewDepartmentStudentsCountResult = await _departmentService.GetDepartmentStudentsCountProc(parameters);
            var result = _mapper.Map<GetDepartmentStudentsCountByIdResult>(viewDepartmentStudentsCountResult.FirstOrDefault());
            return Success(result);
        }

        #endregion
    }
}
