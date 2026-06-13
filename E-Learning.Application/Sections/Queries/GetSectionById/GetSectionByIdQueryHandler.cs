using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Sections;

namespace E_Learning.Application.Sections.Queries.GetSectionById
{
    public sealed class GetSectionByIdQueryHandler : IQueryHandler<GetSectionByIdQuery, SectionResponse>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetSectionByIdQueryHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<Result<SectionResponse>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(request.sectionId, cancellationToken);
            if (section is null)
                return Result.Failure<SectionResponse>(SectionErrors.NotFound);
            var response = new SectionResponse(
                section.Id,
                section.SectionTitle.Value,
                section.Price.Value
            );
            return Result.Success( response );
        }
    }
}
