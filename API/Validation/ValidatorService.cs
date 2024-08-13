using FluentValidation;

namespace API.Validation;

public class ValidatorService : IValidatorService
{
    private readonly IEnumerable<IValidator> _validators;

    public ValidatorService(IEnumerable<IValidator> validators)
    {
        _validators = validators;
    }

    public async Task ValidateCommandAsync<TCommand>(TCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command), "Command cannot be null");
        }
        
        var validator = _validators
            .OfType<IValidator<TCommand>>()
            .FirstOrDefault();

        if (validator == null)
        {
            throw new InvalidOperationException($"No validator found for command type {typeof(TCommand).Name}");
        }
        
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
    
}