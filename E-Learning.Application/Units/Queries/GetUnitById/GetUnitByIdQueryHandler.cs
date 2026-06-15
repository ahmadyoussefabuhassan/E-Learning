using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Units;

namespace E_Learning.Application.Units.Queries.GetUnitById
{
    public sealed class GetUnitByIdQueryHandler : IQueryHandler<GetUnitByIdQuery, UnitResponse>
    {
        private readonly IUnitRepository _unitRepository;

        public GetUnitByIdQueryHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<Result<UnitResponse>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.Id , cancellationToken);
            if (unit is null)
                return Result.Failure<UnitResponse>(UnitsErrors.NotFound);
            var response = new UnitResponse(
                unit.Id,
                unit.UnitTitle.Value,
                unit.Description.Value
            );
            return Result.Success(response);
        }
    }
}
