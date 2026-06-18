namespace E_Learning.Api.Controllers.Invtensives
{
    public sealed record AddInvtensiveRequest(
        string Title,
        string Description,
        decimal Price
    );
}
