using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Projects;
using Bbpro.Domain.Models.Projects;
using Bbpro.Domain.Models.Response;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Projects;

[Route("customapi/[controller]")]
[ApiController]
public sealed class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<ProjectModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
      => ResponseHandler.ReturnIActionResponse(await _projectRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<ProjectModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromForm] ProjectCreateDto projectCreateDTO)
     => ResponseHandler.ReturnIActionResponse(await _projectRepository.CreateAsync(projectCreateDTO));


    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _projectRepository.DeleteAsync(id));


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<ProjectModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
     => ResponseHandler.ReturnIActionResponse(await _projectRepository.GetAsync(u => u.Id == id));


    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<ProjectModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromForm] ProjectUpdateDto projectUpdateDTO)
     => ResponseHandler.ReturnIActionResponse(await _projectRepository.UpdateAsync(id, projectUpdateDTO));

}
