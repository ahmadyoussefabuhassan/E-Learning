using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Sections.Queries.GetSectionByIdForStudent
{
    public sealed record GetSectionByIdForStudentQuery(
        Guid SectionId
    ) : IQuery<SectionResponse>;
}
