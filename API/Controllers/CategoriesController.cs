﻿using API.Validation;
using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : MainController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService, IMediator mediator, ILogger<CategoriesController> logger, IValidatorService validatorService) 
        : base(mediator, logger, validatorService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await _categoryService.GetCategories();
        if (categories == null)
        {
            return NotFound("Category not found");
        }

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _categoryService.GetById(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
    {
        if (categoryDto == null)
            return BadRequest("Infalid Data");

        await _categoryService.Add(categoryDto);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.Id }, categoryDto);
    }
    [Route("api/categories/teste")]

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand categoryDto)
    {
        return await SendCommand(categoryDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
    {
        if (id != categoryDto.Id)
            return BadRequest();

        if (categoryDto == null)
            return BadRequest();

        await _categoryService.Update(categoryDto);

        return Ok(categoryDto);
    }

    [HttpDelete]
    public async Task<ActionResult<CategoryDTO>> Delete(int id)
    {
        var category = await _categoryService.GetById(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        await _categoryService.Remove(id);

        return Ok();
    }
}