
namespace E_Learning.Application.Units.Queries.GetUnitById
{
    public sealed record UnitResponse(
        Guid unitId,
        string Title,
        string Description);
}
