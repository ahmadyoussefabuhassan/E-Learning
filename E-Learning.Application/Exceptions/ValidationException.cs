

namespace E_Learning.Application.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; private set; }
        public ValidationException(IEnumerable<ValidationError> errors)
            => Errors = errors;

    }
}
