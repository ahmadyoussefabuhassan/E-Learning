

namespace E_Learning.Application.Invtensives.Queries.GetInvtensivesByIdForStudent
{
    public sealed record InvtensiveResponse(
          Guid Id,
         string Title,
         string Description,
         decimal Price,
         bool IsLocked
     );
}
