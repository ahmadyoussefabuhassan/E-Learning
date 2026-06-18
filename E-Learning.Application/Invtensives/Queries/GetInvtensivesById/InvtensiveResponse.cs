

namespace E_Learning.Application.Invtensives.Queries.GetInvtensivesById
{
    public sealed record InvtensiveResponse(
         Guid Id,
        string Title,
        string Description,
        decimal Price
    );
}
