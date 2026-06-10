using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.Students.Commands.RegisterStudent
{
    public sealed record RegisterStudentCommand(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        string Address,
       
        string Education
    ) : ICommand<Guid>;
}
