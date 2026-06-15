using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Units.Queries.GetAllUnitsBySection
{
    public sealed record GetAllUnitsBySectionQuery(Guid sectionId) : IQuery<IEnumerable<UnitResponse>>;
}
