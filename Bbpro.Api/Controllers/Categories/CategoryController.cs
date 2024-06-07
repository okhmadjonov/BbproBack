using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.CategoryDto;
using Bbpro.Domain.Models.Categories;
using Bbpro.Domain.Models.Response;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Categories;


[Route("customapi/[controller]")]
[ApiController]
public sealed class CategoryController : ControllerBase
{
    protected readonly ICategoryRepository _categoryRepository;
    public CategoryController(ICategoryRepository categoryRepository)
    { _categoryRepository = categoryRepository; }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<CategoryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        => ResponseHandler.ReturnResponseList(await _categoryRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<CategoryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] CategoryCreateDto categoryDto)
    {
        return ResponseHandler.ReturnIActionResponse(await _categoryRepository.CreateAsync(categoryDto));
    }


    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _categoryRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<CategoryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _categoryRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<CategoryModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] CategoryUpdateDto categoryDto)
        => ResponseHandler.ReturnIActionResponse(await _categoryRepository.UpdateAsync(id, categoryDto));
}

