﻿using Bbpro.Domain.Entities.Multilanguage;
using Microsoft.AspNetCore.Http;

namespace Bbpro.Domain.Dto.Projects;

public class ProjectUpdateDto
{
    public IFormFile ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
    public string? DownloadLink { get; set; }
}
