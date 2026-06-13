using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Sections.Queries.GetSectionById
{
    public sealed record GetSectionByIdQuery(Guid sectionId) : IQuery<SectionResponse>;
}
