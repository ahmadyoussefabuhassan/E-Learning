using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Sections;

namespace E_Learning.Application.Sections.Queries.GetSectionByIdForStudent
{
    public sealed class GetSectionByIdForStudentQueryHandler : IQueryHandler<GetSectionByIdForStudentQuery, SectionResponse>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetSectionByIdForStudentQueryHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<Result<SectionResponse>> Handle(GetSectionByIdForStudentQuery request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(request.SectionId, cancellationToken);
            if (section is null)
                return Result.Failure<SectionResponse>(SectionErrors.NotFound);
            var response = new SectionResponse(
                section.Id,
                section.SectionTitle.Value,
                section.Price.Value,
                section.IsLocked
            );
            return Result.Success(response);
        }
    }
}
