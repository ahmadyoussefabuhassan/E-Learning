using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Sections.Queries.GetAllSectionsByCourse
{
    public sealed record GetAllSectionsByCourseQuery(Guid courseId) : IQuery<IEnumerable<SectionResponse>>;
}
