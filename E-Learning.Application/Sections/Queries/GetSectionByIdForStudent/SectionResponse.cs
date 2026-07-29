

namespace E_Learning.Application.Sections.Queries.GetSectionByIdForStudent
{
    public sealed record SectionResponse(Guid Id,
          string Title,
          decimal Price,
          bool Islouked
    );
}
