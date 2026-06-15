using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Units.Queries.GetUnitById
{
    public sealed record GetUnitByIdQuery(Guid Id) : IQuery<UnitResponse>;
}
