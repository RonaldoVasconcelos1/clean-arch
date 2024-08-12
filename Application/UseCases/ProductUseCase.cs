using Application.DTOs;
using Application.Interfaces;
using Application.Products.Commands;
using Application.Products.Queries;
using AutoMapper;
using MediatR;

namespace Application.UseCases;

public class ProductUseCase : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductUseCase(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery == null)
            throw new Exception($"Entity could not be loaded.");

        var result = await _mediator.Send(productsQuery);

        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<ProductDTO> GetById(int? id)
    {
        var productByIdQuery = new GetProductByIdQuery(id.Value);

        if (productByIdQuery == null)
            throw new Exception($"Entity could not be loaded.");

        var result = await _mediator.Send(productByIdQuery);

        return _mapper.Map<ProductDTO>(result);
    }

    // public async Task<ProductDTO> GetByCategoryId(int? id)
    // {
    //     var productByIdQuery = new GetProductByIdQuery(id.Value);

    //     if (productByIdQuery == null)
    //         throw new Exception($"Entity could not be loaded.");

    //     var result = await _mediator.Send(productByIdQuery);

    //     return _mapper.Map<ProductDTO>(result);
    // }

    public async Task Add(ProductDTO productDto)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Update(ProductDTO productDto)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
        await _mediator.Send(productUpdateCommand);
    }

    public async Task Remove(int? id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id.Value);
        if (productRemoveCommand == null)
            throw new Exception($"Entity could not be loaded.");

        await _mediator.Send(productRemoveCommand);
    }
}