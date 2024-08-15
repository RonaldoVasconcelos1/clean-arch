namespace Infra.services;

public interface ISecretsManagerService
{
    Task<string> GetSecretValueAsync(string arnName);
}