using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.DTOs;

public class CreateOrderCommand :  IRequest<CommandResult>
{
    public CreateOrderCommand(string? name)
    {
        Name = name;
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "The Name is required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId cannot be empty");
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId cannot be empty");

    }
}