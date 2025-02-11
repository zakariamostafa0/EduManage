using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule =root + "/" + version+"/";


        private const string Id = "/{id}:int";
        public static class StudentRouting
        {
            public const string Prefix = Rule+"Student";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + Id;
            public const string Create = Prefix + "/Create";
        }
    }
}
