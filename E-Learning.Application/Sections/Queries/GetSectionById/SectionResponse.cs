

namespace E_Learning.Application.Sections.Queries.GetSectionById
{
    public sealed record SectionResponse(Guid Id,
        string Title,
        decimal Price);
}
