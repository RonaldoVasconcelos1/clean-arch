using API.Validation;
using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValidationException = FluentValidation.ValidationException;

namespace API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MainController> _logger;
    private readonly IValidatorService _validatorService;
    
    public MainController(IMediator mediator, ILogger<MainController> logger, IValidatorService validatorService)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validatorService = validatorService;
    }

    protected async Task<IActionResult> SendCommand<TCommand>(TCommand command)
        where TCommand : IRequest<GenericCommandResult>
    {
        if (command == null)
        {
            _logger.LogWarning("Received null command");
            return BadRequestResponse("Command cannot be null", null);
        }

        try
        {
            
            var result = await _mediator.Send(command);

            if (result.IsCreated)
            {
                _logger.LogInformation("Entity created successfully");
                return CreatedResponse(result.Message, result.Data);
            }

            if (result.Success)
            {
                _logger.LogInformation("Command processed successfully");
                return OkResponse(result.Message, result.Data);
            }
            else
            {
                _logger.LogWarning("Command processing failed: {Message}", result.Message);
                return BadRequestResponse(result.Message, result.Data);
            }
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation exception occurred: {Message}", ex.Message);
            return UnprocessableEntityResponse(ex.Message, ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while processing the command");
            return InternalServerErrorResponse(ex.Message, null);
        }
    }

    // Custom response methods

    protected IActionResult OkResponse(string message, object data) =>
        Ok(new GenericCommandResult(true, message, data));

    protected IActionResult CreatedResponse(string message, object data) =>
        Created(string.Empty, new GenericCommandResult(true, message, data));

    protected IActionResult NoContentResponse(string message = "No content") =>
        NoContent();

    protected IActionResult NotFoundResponse(string message) =>
        NotFound(new GenericCommandResult(false, message, null));
    
    protected IActionResult BadRequestResponse(string message, object data) =>
        Ok(new GenericCommandResult(true, message, data));

    protected IActionResult FailedDependencyResponse(string message) =>
        StatusCode(424, new GenericCommandResult(false, message, null));

    protected IActionResult UnprocessableEntityResponse(string message, object errors) =>
        StatusCode(422, new GenericCommandResult(false, message, errors));

    protected IActionResult InternalServerErrorResponse(string message, object data) =>
        StatusCode(500, new GenericCommandResult(false, message, data));
    
}