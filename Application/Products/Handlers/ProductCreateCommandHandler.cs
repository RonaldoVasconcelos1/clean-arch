using Application.DTOs;
using Application.Products.Commands;
using Core.Handler;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;


namespace Application.Products.Handlers;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, ProductDTO>
{
    private readonly IProductRepository _productRepository;

    public ProductCreateCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDTO> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image);

        if (product == null)
        {
            throw new ApplicationException($"Error creating entity.");
        }

        product.CategoryId = request.CategoryId;
        await _productRepository.AddAsync(product);
        
        return new ProductDTO()
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Image = product.Image,
            Price = product.Price
        };
    }
}