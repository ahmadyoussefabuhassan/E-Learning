

namespace E_Learning.Application.Exceptions
{
    public sealed record ValidationError(string PropertyName, string ErrorMessage);
}
