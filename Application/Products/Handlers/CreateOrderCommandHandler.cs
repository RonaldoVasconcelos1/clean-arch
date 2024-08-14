using Application.DTOs;
using MediatR;

namespace Application.Products.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = 123;

        // Retornar um resultado de sucesso utilizando o formato esperado
        return CommandResult.SuccessResult(new { OrderId = orderId });

    }
}