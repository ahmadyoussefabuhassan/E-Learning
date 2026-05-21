using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamExplanations.Events
{
    public sealed record ExamExplanationCreatedEvent(Guid Id, string Title, string Description, decimal Price, Guid CourseId) : IDomainEvent;

}
