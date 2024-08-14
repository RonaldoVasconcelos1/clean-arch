using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.Extensions.Logging;

namespace Infra.services;

public class ParameterStoreService: IParameterStoreService
{
    private readonly IAmazonSimpleSystemsManagement _ssmClient;
    private readonly ILogger<ParameterStoreService> _logger;

    public ParameterStoreService(IAmazonSimpleSystemsManagement ssmClient, ILogger<ParameterStoreService> logger)
    {
        _ssmClient = ssmClient;
        _logger = logger;
    }

    public async Task<string> GetParameterAsync(string parameterName)
    {
        try
        {
            var request = new GetParameterRequest
            {
                Name = parameterName,
                WithDecryption = true
            };

            var response = await _ssmClient.GetParameterAsync(request);
            return response.Parameter.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching parameter {parameterName}: {ex.Message}");
            throw;
        }
    }
}