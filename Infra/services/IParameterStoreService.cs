namespace Infra.services;

public interface IParameterStoreService
{
    Task<string> GetParameterAsync(string parameterName);
}