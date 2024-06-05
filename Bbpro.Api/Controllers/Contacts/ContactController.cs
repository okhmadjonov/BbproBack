using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.MainContact;
using Bbpro.Domain.Models.Contacts;
using Bbpro.Domain.Models.Response;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Contacts;

[Route("customapi/[controller]")]
[ApiController]
public sealed class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;
    public ContactController(IContactRepository contactRepository)
        => _contactRepository = contactRepository;

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<ContactModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
    => ResponseHandler.ReturnResponseList(await _contactRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<ContactModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromBody] ContactCreateDto contactCreateDto)
        => ResponseHandler.ReturnIActionResponse(await _contactRepository.CreateAsync(contactCreateDto));

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _contactRepository.DeleteAsync(id));


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<ContactModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _contactRepository.GetAsync(u => u.Id == id));


    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<ContactModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromBody] ContactUpdateDto contactForUpdateDTO)
        => ResponseHandler.ReturnIActionResponse(await _contactRepository.UpdateAsync(id, contactForUpdateDTO));
}
