

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllSectionsSubscriptionsByStudent
{
    public sealed record SectionResponse(Guid Id,
          string Title,
          decimal Price,
          bool Islouked
    );
}
