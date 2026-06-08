namespace Syrian_Platform.Domain.Abstractions;
public class PaginationRequest
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string ? Query { get; set; } = "";
}


