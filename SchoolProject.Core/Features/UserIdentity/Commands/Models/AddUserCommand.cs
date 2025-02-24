﻿namespace SchoolProject.Core.Features.UserIdentity.Commands.Models
{
    public class AddUserCommand : IRequest<Response<bool>>
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
