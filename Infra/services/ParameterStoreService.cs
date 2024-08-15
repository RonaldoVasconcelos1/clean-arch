using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.Extensions.Logging;

namespace Infra.services;

public class SecretsManagerService: ISecretsManagerService
{
    private readonly IAmazonSimpleSystemsManagement _ssmClient;
    private readonly ILogger<ParameterStoreService> _logger;

    public ParameterStoreService(IAmazonSimpleSystemsManagement ssmClient)
    {
        _ssmClient = ssmClient;
        _logger = logger;
    }

    private async Task<string> GetSecretValueAsync(string secretArn)
    {
        var response = await _secretsManager.GetSecretValueAsync(new GetSecretValueRequest
        {
            SecretId = secretArn
        });

        return response.SecretString;
    }
}