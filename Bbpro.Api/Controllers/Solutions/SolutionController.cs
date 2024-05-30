using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Solutions;
using Bbpro.Domain.Models.Response;
using Bbpro.Domain.Models.Solutions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Solutions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Solutions;

[Route("customapi/[controller]")]
[ApiController]
public sealed class SolutionController : ControllerBase
{
    private readonly ISolutionRepository _solutionRepository;

    public SolutionController(ISolutionRepository solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<SolutionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
      => ResponseHandler.ReturnResponseList(await _solutionRepository.GetAll(@params));

    [HttpPost]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<SolutionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] SolutionCreateDto solutionCreateDTO)
     => ResponseHandler.ReturnIActionResponse(await _solutionRepository.CreateAsync(solutionCreateDTO));


    [HttpDelete("{id}")]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _solutionRepository.DeleteAsync(id));


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<SolutionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _solutionRepository.GetAsync(u => u.Id == id));


    [HttpPut]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<SolutionModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] SolutionUpdateDto solutionUpdateDTO)
     => ResponseHandler.ReturnIActionResponse(await _solutionRepository.UpdateAsync(id, solutionUpdateDTO));

}
