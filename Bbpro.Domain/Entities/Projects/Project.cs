using Bbpro.Domain.Commons;
using Bbpro.Domain.Entities.Multilanguage;
using Microsoft.AspNetCore.Http;

namespace Bbpro.Domain.Entities.Projects;

public class Project: Auditable
{
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
    public string DownloadLink { get; set; }
}
