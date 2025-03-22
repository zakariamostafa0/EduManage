﻿namespace SchoolProject.Core.Features.Authentication.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
