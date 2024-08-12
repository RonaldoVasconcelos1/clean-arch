namespace Core.Command;

using MediatR;

public class CommandAdapter<TCommand, TResponse> : IRequest<TResponse>
    where TCommand : ICommand<TResponse>
{
    public TCommand Command { get; }

    public CommandAdapter(TCommand command)
    {
        Command = command;
    }
}