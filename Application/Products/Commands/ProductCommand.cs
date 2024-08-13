using Application.DTOs;
using MediatR;

namespace Application.Products.Commands;

public class ProductCommand: IRequest<ProductDTO>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Image { get; set; }
    public int CategoryId { get; set; }
}