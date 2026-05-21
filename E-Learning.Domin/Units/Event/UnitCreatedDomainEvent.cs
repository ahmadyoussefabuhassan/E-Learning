using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Units.Event
{
    public sealed record UnitCreatedDomainEvent(Guid UnitId, string Title, string Description, Guid SectionId) : IDomainEvent
    {
    }
}
