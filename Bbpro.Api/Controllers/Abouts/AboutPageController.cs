using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.About;
using Bbpro.Domain.Models.AboutModel;
using Bbpro.Domain.Models.Response;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Abouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Abouts;

[Route("customapi/[controller]")]
[ApiController]
public sealed class AboutPageController : ControllerBase
{
    protected readonly IAboutRepository _aboutRepository;
    public AboutPageController(IAboutRepository aboutRepository)
        => _aboutRepository = aboutRepository;

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<AboutModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        => ResponseHandler.ReturnResponseList(await _aboutRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<AboutModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] AboutCreateDto aboutForCreationDTO)
    {
        return ResponseHandler.ReturnIActionResponse(await _aboutRepository.CreateAsync(aboutForCreationDTO));
    }


    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _aboutRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<AboutModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _aboutRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<AboutModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] AboutUpdateDto aboutForUpdateDTO)
        => ResponseHandler.ReturnIActionResponse(await _aboutRepository.UpdateAsync(id, aboutForUpdateDTO));
}
