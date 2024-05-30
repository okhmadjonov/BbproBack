
using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Latests;
using Bbpro.Domain.Models.Latests;
using Bbpro.Domain.Models.Response;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Latests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Latests;

[Route("customapi/[controller]")]
[ApiController]
public sealed class LatestController : ControllerBase
{
    private readonly ILatestRepository _latestRepository;
    public LatestController(ILatestRepository latestRepository)
    {
        _latestRepository = latestRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<LatestModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
      => ResponseHandler.ReturnResponseList(await _latestRepository.GetAll(@params));

    [HttpPost]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<LatestModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] LatestCreateDto latestCreateDTO)
     => ResponseHandler.ReturnIActionResponse(await _latestRepository.CreateAsync(latestCreateDTO));


    [HttpDelete("{id}")]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _latestRepository.DeleteAsync(id));


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<LatestModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _latestRepository.GetAsync(u => u.Id == id));


    [HttpPut]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<LatestModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] LatestUpdateDto latestUpdateDTO)
     => ResponseHandler.ReturnIActionResponse(await _latestRepository.UpdateAsync(id, latestUpdateDTO));
}
