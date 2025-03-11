namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";


        private const string IntId = "/{id:int}";
        private const string StringId = "/{id}";

        public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + IntId;
            public const string Create = Prefix + "/Create";
            public const string Paginate = Prefix + "/Paginate";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete" + IntId;

        }
        public static class DepartmentRouting
        {
            public const string Prefix = Rule + "Department";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "Id";
            public const string Create = Prefix + "/Create";
            public const string Paginate = Prefix + "/Paginate";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete" + IntId;

        }
        public static class AccountRouting
        {
            public const string Prefix = Rule + "Account";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + IntId;
            public const string GetUser = Prefix + "/GetUser";
            public const string Create = Prefix + "/Create";
            public const string Paginate = Prefix + "/Paginate";
            public const string Edit = Prefix + "/Edit";
            public const string ChangePassword = Prefix + "/ChangePassword";
            public const string Delete = Prefix + "/Delete" + StringId;

        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Rule + "Authentication";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + IntId;
            public const string GetUser = Prefix + "/GetUser";
            public const string Login = Prefix + "/Login";
            public const string RefreshToken = Prefix + "/RefreshToken";
            public const string ValidateToken = Prefix + "/ValidateToken";
            public const string Paginate = Prefix + "/Paginate";
            public const string Edit = Prefix + "/Edit";
            public const string ChangePassword = Prefix + "/ChangePassword";
            public const string Delete = Prefix + "/Delete" + StringId;

        }
    }
}
