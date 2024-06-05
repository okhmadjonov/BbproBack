using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Brands;
using Bbpro.Domain.Models.BrandsModel;
using Bbpro.Domain.Models.Response;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Brands;

[Route("customapi/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    protected readonly IBrandRepository _brandRepository;
    public BrandsController(IBrandRepository brnadRepository)
        => _brandRepository = brnadRepository;

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<BrandModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
      => ResponseHandler.ReturnResponseList(await _brandRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<BrandModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] BrandDto brandDto)
     => ResponseHandler.ReturnIActionResponse(await _brandRepository.CreateAsync(brandDto));


    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _brandRepository.DeleteAsync(id));


    [HttpGet("{id}")]

    [ProducesResponseType(typeof(ResponseModel<BrandModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _brandRepository.GetAsync(u => u.Id == id));


    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<BrandModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] BrandDto branddto)
     => ResponseHandler.ReturnIActionResponse(await _brandRepository.UpdateAsync(id, branddto));

}
