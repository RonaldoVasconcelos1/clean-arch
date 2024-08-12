using Core.Command;
using MediatR;

namespace Core.Handler;

public class CommandHandlerAdapter<TCommand, TResponse> : IRequestHandler<CommandAdapter<TCommand, TResponse>, TResponse>
    where TCommand : ICommand<TResponse>
{
    private readonly ICommandHandler<TCommand, TResponse> _handler;

    public CommandHandlerAdapter(ICommandHandler<TCommand, TResponse> handler)
    {
        _handler = handler;
    }

    public async Task<TResponse> Handle(CommandAdapter<TCommand, TResponse> request, CancellationToken cancellationToken)
    {
        return await _handler.Handle(request.Command, cancellationToken);
    }
}