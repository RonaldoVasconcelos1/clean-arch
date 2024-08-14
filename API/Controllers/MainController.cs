using API.Validation;
using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        where TCommand : IRequest<CommandResult>
    {
        if (command == null)
        {
            _logger.LogWarning("Received null command");
            return BadRequestResponse("Command cannot be null");
        }

        try
        {
            var result = await _mediator.Send(command);

            if (result.Data != null && result.Data is IEnumerable<object> listData && !listData.Any())
            {
                return NoContentResponse();
            }

            if (result.Data == null)
            {
                _logger.LogWarning("Data not found");
                return NotFoundResponse(result.Errors);
            }

            if (result.Data != null)
            {
                _logger.LogInformation("Command processed successfully");
                return OkResponse(result.Data);
            }

            _logger.LogWarning("Command processing failed");
            return BadRequestResponse("Command processing failed");
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation exception occurred: {Message}", ex.Message);
            return UnprocessableEntityResponse(ex.Errors.Select(e => e.ErrorMessage).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while processing the command");
            return InternalServerErrorResponse("An unexpected error occurred");
        }
    }

    // Custom response methods

    protected IActionResult OkResponse(object data) =>
        Ok(CommandResult.SuccessResult(data));

    protected IActionResult CreatedResponse(object data) =>
        Created(string.Empty, CommandResult.SuccessResult(data));

    protected IActionResult NoContentResponse() =>
        NoContent();

    protected IActionResult NotFoundResponse(List<string> message) =>
        NotFound(CommandResult.ErrorResult(message));

    protected IActionResult BadRequestResponse(string message) =>
        BadRequest(CommandResult.ErrorResult(new List<string> { message }));

    protected IActionResult FailedDependencyResponse(string message) =>
        StatusCode(424, CommandResult.ErrorResult(new List<string> { message }));

    protected IActionResult UnprocessableEntityResponse(List<string> errors) =>
        StatusCode(422, CommandResult.ErrorResult(errors));

    protected IActionResult InternalServerErrorResponse(string message) =>
        StatusCode(500, CommandResult.ErrorResult(new List<string> { message }));
}