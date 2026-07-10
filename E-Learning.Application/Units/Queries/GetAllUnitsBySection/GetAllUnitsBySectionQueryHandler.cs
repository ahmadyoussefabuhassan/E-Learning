using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Units;

namespace E_Learning.Application.Units.Queries.GetAllUnitsBySection
{
    public sealed class GetAllUnitsBySectionQueryHandler : IQueryHandler<GetAllUnitsBySectionQuery, IEnumerable<UnitResponse>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IUnitRepository _unitRepository;

        public GetAllUnitsBySectionQueryHandler(ISectionRepository sectionRepository, IUnitRepository unitRepository)
        {
            _sectionRepository = sectionRepository;
            _unitRepository = unitRepository;
        }

        public async Task<Result<IEnumerable<UnitResponse>>> Handle(GetAllUnitsBySectionQuery request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(request.sectionId, cancellationToken);
            if(section is null)
                return Result.Failure<IEnumerable<UnitResponse>>(SectionErrors.NotFound);
            var units =  await _unitRepository.GetAllBySectionAsync(request.sectionId, cancellationToken);
            if(!units.Any())
                return Result.Success(Enumerable.Empty<UnitResponse>());
            var response = units.Select(unit => new UnitResponse(
                unit.Id,
                unit.UnitTitle.Value,
                unit.Description.Value
            ));
            return Result.Success(response);


        }
    }
}
