using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Courses.Events
{
    public sealed record CourseCreatedDomainEvent(Guid Id, string Name, decimal Price, Guid TeacherId, Guid ClassesId) : IDomainEvent;
}
