namespace API.Validation;

public interface IValidatorService
{
    Task ValidateCommandAsync<TCommand>(TCommand command);
}