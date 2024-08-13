using Application.DTOs;
using MediatR;

namespace Application.Products.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, GenericCommandResult>
{
    public async Task<GenericCommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Implementação do comando
        return new GenericCommandResult(true, "Order created successfully", new { OrderId = 123 });
    }
}