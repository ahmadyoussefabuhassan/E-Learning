namespace E_Learning.Api.Controllers.Invtensives
{
    public sealed record UpdateInvtensiveRequest(
        string Title,
        string Description,
        decimal Price
    );
}
