namespace E_Learning.Api.Controllers.Sections
{
    public sealed record AddSectionRequest(
        string Title,
        decimal Price
    );
}
