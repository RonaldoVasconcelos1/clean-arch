using FluentValidation.Results;

namespace API.Validation;

public class ValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> errors)
        : base("Validation failed")
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
    }

    public ValidationException(string message, IEnumerable<ValidationFailure> errors)
        : base(message)
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
    }

    public IEnumerable<string> GetErrorMessages()
    {
        return Errors.Select(e => e.ErrorMessage);
    }
}