

namespace E_Learning.Application.Invtensives.Queries.GetAllInvtensivesByCourse
{
    public sealed record InvtensiveResponse(
         Guid Id,
        string Title,
        string Description,
        decimal Price
    );
}
